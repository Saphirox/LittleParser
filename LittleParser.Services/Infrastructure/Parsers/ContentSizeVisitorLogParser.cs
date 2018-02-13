namespace LittleParser.Services.Infrastructure.Parsers
{
    /// <summary>
    /// Retrieve Content size from string
    /// </summary>
    public class ContentSizeVisitorLogParser : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var tokens = apacheLogParser.ProccessingString.Split(' ');
            
            apacheLogParser.Result.ContentSize = long.TryParse(tokens[tokens.Length - 1], out var result) ? result : default;

            return true;
        }
    }
}