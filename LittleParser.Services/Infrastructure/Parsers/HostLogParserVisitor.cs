using System.Linq;

namespace LittleParser.Services.Infrastructure.Parsers
{
    /// <summary>
    /// Retrieve host from string
    /// </summary>
    public class HostLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var tokens = apacheLogParser.ProccessingString.Split(' ');

            apacheLogParser.Result.Host = tokens.First();
            
            return true;
        }
    }
}