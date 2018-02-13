namespace LittleParser.Services.Providers.Impl
{
    using Models.Entities;
    using Infrastructure.Parsers;


    /// <summary>
    // Facade for using ApacheParser
    /// </summary>
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