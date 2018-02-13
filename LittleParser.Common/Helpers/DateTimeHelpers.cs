using System;
using System.Linq;

namespace LittleParser.Common.Helpers
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// Get all system timezones
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeZone(this DateTimeOffset time)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            
            return TimeZoneInfo.GetSystemTimeZones()
                .FirstOrDefault(c => c.GetUtcOffset(now) == time.Offset)?.StandardName;
        }

        
        /// <summary>
        /// Converter apache time to c# datetime with invariant culture
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ConvertApacheLogDateTime(string dateTime)
                => DateTimeOffset.ParseExact(dateTime,
                    "dd/MMM/yyyy:HH:mm:ss zzzz", System.Globalization.CultureInfo.InvariantCulture);
    }
}