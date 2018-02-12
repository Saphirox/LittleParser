namespace LittleParser.Services.Providers.Impl
{
    using LittleParser.Models.Entities;
    using LittleParser.Services.Facades;
    using LittleParser.Services.Infrastructure.Parsers;

    public class ApacheLogParserProvider : IApacheLogParserProvider
    {
        public bool TryParse(string line, out ApacheLog log)
        {
            var parser = new ApacheLogParser(line, 
                new RouteLogParserVisitor(), 
                new HostLogParserVisitor(), 
                new DateTimeOffsetLogParserVisitor(),
                new ContentSizeVisitorLogParser(),
                new GeolocationLogParserVisitor(),
                new StatusCodeLogParserVisitor());

            log = parser.Parse();
            
            return log != null;
        }
     }
}