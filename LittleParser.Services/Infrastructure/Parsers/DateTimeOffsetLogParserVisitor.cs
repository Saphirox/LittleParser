namespace LittleParser.Services.Infrastructure.Parsers
{
    using LittleParser.Common.Helpers;

    public class DateTimeOffsetLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            var line = apacheLogParser.ProccessingString.Split('[', ']')[1];
            
            apacheLogParser.Result.DateTimeOffset = DateTimeHelpers.ConvertApacheLogDateTime(line);

            return true;
        }
    }
}