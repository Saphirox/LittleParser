namespace LittleParser.Services.Infrastructure.Parsers
{
    public class HostLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var tokens = apacheLogParser.ProccessingString.Split(' ');

            apacheLogParser.Result.Host = tokens[0];
            
            return true;
        }
    }
}