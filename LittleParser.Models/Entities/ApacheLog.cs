using System;
using System.Collections.Generic;

namespace LittleParser.Models.Entities
{
    public class ApacheLog : IEntity
    {
        public long Id { get; set; }
        
        public string Host { get; set; }
        
        public string Geolocation { get; set; }
        
        public string Route { get; set; }
        
        public int StatusCode { get; set; }
        
        public DateTimeOffset DateTimeOffset { get; set; }

        public long ContentSize { get; set; }
        
        public string QueryParameters { get; set; }
    }
}