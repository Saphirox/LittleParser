using System;

namespace LittleParser.Services.Infrastructure.Parsers
{
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Check is valid string and parse route from string 
    /// </summary>
    public class RouteLogParserVisitor : LogParserVisitor
    {
        private readonly string[] _appropriatedExtensions = {"html"};
        
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var routeWithQueries = apacheLogParser.ProccessingString.Split('\"')[1].Split(' ')[1];

            if (!IsAppropriateUrl(routeWithQueries))
                return false;

            var questionMarkIndex = routeWithQueries.IndexOf('?');

            if (questionMarkIndex != -1)
            {
                apacheLogParser.Result.Route = routeWithQueries.Substring(0, questionMarkIndex - 1);
                apacheLogParser.Result.QueryParameters = routeWithQueries.Substring(questionMarkIndex, routeWithQueries.Length - questionMarkIndex);
            }
            else
            {
                apacheLogParser.Result.Route = routeWithQueries;
                apacheLogParser.Result.QueryParameters = string.Empty;
            }

            return true;
        }
        
        private bool IsAppropriateUrl(string route)
        {
            try
            {
                string[] tokens = route.Split('.');

                if (tokens.Length > 1)
                {
                    return _appropriatedExtensions.Any(tokens.Contains);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}