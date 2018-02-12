namespace LittleParser.Services.Infrastructure.Parsers
{
    public abstract class LogParserVisitor 
    {
        public abstract bool Handle(ApacheLogParser apacheLogParser);
    }
}