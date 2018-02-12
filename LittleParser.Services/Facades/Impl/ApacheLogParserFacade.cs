using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LittleParser.Common.Helpers;
using LittleParser.Models.Entities;

namespace LittleParser.Services.Facades.Impl
{
    public class ApacheLogParserFacade : IApacheLogParserFacade
    {
        public ApacheLogParserFacade()
        {}
        
        public bool TryParse(string line, out ApacheLog log)
        {
            var parser = new Parser(line, 
                new RouteParserVisitor(), 
                new HostParserVisitor(), 
                new DateTimeOffsetParserVisitor(),
                new ContentSizeVisitorParser(),
                new GeolocationParserVisitor(),
                new StatusCodeParserVisitor());

            log = parser.Parse();
            
            return log != null;
        }
     }

    public class Parser
    {
        private readonly IEnumerable<ParserVisitor> _visitors;
        public readonly string ProccessingString;
        public ApacheLog Result;
        
        public Parser(string proccessingString, params ParserVisitor[] visitors)
        {
            _visitors = visitors;
            ProccessingString = proccessingString;
        }

        public ApacheLog Parse()
        {
            Result = new ApacheLog();
            
            foreach (var handler in _visitors)
            {
                if (!handler.Handle(this))
                    return null;
            }

            return Result;
        }
    }

    public abstract class ParserVisitor 
    {
        public abstract bool Handle(Parser parser);
    }

    public class RouteParserVisitor : ParserVisitor
    {
        private readonly string[] _appropriatedExtensions = {string.Empty, ".html"};
        
        public override bool Handle(Parser parser)
        {
            var routeWithQueries = parser.ProccessingString.Split('\"')[1].Split(' ')[1];

            if (!IsAppropriateUrl(routeWithQueries))
                return false;

            var questionMarkIndex = routeWithQueries.IndexOf('?');

            if (questionMarkIndex != -1)
            {
                parser.Result.Route = routeWithQueries.Substring(0, questionMarkIndex - 1);
                parser.Result.QueryParameters = routeWithQueries.Substring(questionMarkIndex, routeWithQueries.Length - questionMarkIndex);
            }
            else
            {
                parser.Result.Route = routeWithQueries;
                parser.Result.QueryParameters = string.Empty;
            }

            return true;
        }
        
        private bool IsAppropriateUrl(string route)
        {
            var ext = Path.GetExtension(route);

            return _appropriatedExtensions.Contains(ext);
        }
    }

    public class HostParserVisitor : ParserVisitor
    {
        public override bool Handle(Parser parser)
        {
            var tokens = parser.ProccessingString.Split(' ');

            parser.Result.Host = tokens[0];
            
            return true;
        }
    }

    public class DateTimeOffsetParserVisitor : ParserVisitor
    {
        public override bool Handle(Parser parser)
        {
            var line = parser.ProccessingString.Split('[', ']')[1];
            
            parser.Result.DateTimeOffset = ConvertApacheLogDateTime(line);

            return true;
        }

        private static DateTimeOffset ConvertApacheLogDateTime(string dateTime) 
            => DateTimeOffset.ParseExact(dateTime,
                "dd/MMM/yyyy:h:mm:ss zzzz", System.Globalization.CultureInfo.InvariantCulture);
    }

    public class ContentSizeVisitorParser : ParserVisitor
    {
        public override bool Handle(Parser parser)
        {
            var tokens = parser.ProccessingString.Split(' ');
            
            parser.Result.ContentSize = long.TryParse(tokens[tokens.Length - 1], out var result) ? result : default;

            return true;
        }
    }
    
    public class GeolocationParserVisitor : ParserVisitor
    {
        public override bool Handle(Parser parser)
        {
            parser.Result.Geolocation = parser.Result.DateTimeOffset.GetTimeZone();

            return true;
        }
    }

    public class StatusCodeParserVisitor : ParserVisitor
    {
        public override bool Handle(Parser parser)
        {
            var tokens = parser.ProccessingString.Split(' ');

            parser.Result.StatusCode = int.TryParse(tokens[tokens.Length - 2], out var result) ? result : 0;
            
            return true;
        }
    }
}