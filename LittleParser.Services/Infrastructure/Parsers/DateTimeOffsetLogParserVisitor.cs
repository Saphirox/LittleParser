using System;

namespace LittleParser.Services.Infrastructure.Parsers
{
    using Common.Helpers;


    /// <summary>
    /// Retrieve Content size from string
    /// </summary>
    public class DateTimeOffsetLogParserVisitor : LogParserVisitor
    {
        public override bool Handle(ApacheLogParser apacheLogParser)
        {
            string line = null;

            const int middlePivot = 1;

            line = apacheLogParser.ProccessingString.Split('[', ']')[middlePivot];

            apacheLogParser.Result.DateTimeOffset = DateTimeHelpers.ConvertApacheLogDateTime(line);

            return true;
        }
    }
}