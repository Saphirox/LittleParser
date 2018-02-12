namespace LittleParser.Services.Infrastructure.Parsers
{
    using System.Collections.Generic;
    using LittleParser.Models.Entities;

    public class ApacheLogParser
    {
        private readonly IEnumerable<LogParserVisitor> _visitors;
        public readonly string ProccessingString;
        public ApacheLog Result;
        
        public ApacheLogParser(string proccessingString, params LogParserVisitor[] visitors)
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
}