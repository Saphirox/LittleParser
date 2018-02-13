namespace LittleParser.Services.Infrastructure.Parsers
{
    using Common.Helpers;

    /// <summary>
    /// Get geolocation from timezone
    /// Cannot use online services because it need additional coordinates of user 
    /// </summary>
    public class GeolocationLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            apacheLogParser.Result.Geolocation = apacheLogParser.Result.DateTimeOffset.GetTimeZone();

            return true;
        }
    }
}