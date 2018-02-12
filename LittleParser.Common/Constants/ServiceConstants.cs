using System;
using System.Collections.Generic;

namespace LittleParser.Common.Constants
{
    /// <summary>
    /// Constants for service layer
    /// </summary>
    public class ServiceConstants
    {
        public static IEnumerable<TimeZoneInfo> TimeZones = TimeZoneInfo.GetSystemTimeZones();
    }
}