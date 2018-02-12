namespace LittleParser.Services.Infrastructure.Parsers
{
    using LittleParser.Common.Helpers;

    public class GeolocationLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            apacheLogParser.Result.Geolocation = apacheLogParser.Result.DateTimeOffset.GetTimeZone();

            return true;
        }
    }
}