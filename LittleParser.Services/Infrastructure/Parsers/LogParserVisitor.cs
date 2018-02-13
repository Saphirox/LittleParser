namespace LittleParser.Services.Infrastructure.Parsers
{
    /// <summary>
    /// Visitor for retrieving tokens from parsing string
    /// </summary>
    public abstract class LogParserVisitor 
    {
        public abstract bool Handle(ApacheLogParser apacheLogParser);
    }
}