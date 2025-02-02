﻿namespace LittleParser.Services.Infrastructure.Parsers
{
    /// <summary>
    /// Retrive status code from string 
    /// </summary>
    public class StatusCodeLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var tokens = apacheLogParser.ProccessingString.Split(' ');

            apacheLogParser.Result.StatusCode = int.TryParse(tokens[tokens.Length - 2], out var result) ? result : 0;
            
            return true;
        }
    }
}