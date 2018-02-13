namespace LittleParser.Services.Infrastructure.Parsers
{
    using System.Collections.Generic;
    using Models.Entities;


    /// <summary>
    /// Main implemenation of Apache parser, 
    /// it takes Visitors for insepecting suitable pars of string and put it to model
    /// </summary>
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

        /// <summary>
        /// return null if parse is unsuccessed or ApacheLog if successed
        /// </summary>
        /// <returns></returns>
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