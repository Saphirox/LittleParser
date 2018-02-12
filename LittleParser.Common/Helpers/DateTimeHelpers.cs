using System;
using System.Linq;

namespace LittleParser.Common.Helpers
{
    public static class DateTimeHelpers
    {
        public static string GetTimeZone(this DateTimeOffset time)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            
            return TimeZoneInfo.GetSystemTimeZones()
                .FirstOrDefault(c => c.GetUtcOffset(now) == time.Offset)?.StandardName;
        }
    }
}