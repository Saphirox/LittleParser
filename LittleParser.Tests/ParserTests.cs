using System;
using LittleParser.Services.Facades;
using LittleParser.Services.Facades.Impl;
using NUnit.Framework;

namespace LittleParser.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private readonly IApacheLogParserFacade _sut;

        public ParserTests()
        {
            _sut = new ApacheLogParserFacade();
        }

        [Test]
        public void DataWithoutUnpropriateFilesAndWithoutQueryParamers_Test()
        {
            var testData = "199.72.81.55 - - [01/Jul/1995:00:00:01 -0400] \"GET /history/apollo/ HTTP/1.0\" 200 6245";

            _sut.TryParse(testData, out var result);
            
            Assert.AreEqual(result.ContentSize, 6245L);
//            Assert.AreEqual(result.DateTimeOffset, ApacheLogParserFacade.ConvertApacheLogDateTime("01/Jul/1995:00:00:01 -0400"));
            Assert.AreEqual(result.Host, "199.72.81.55");
            Assert.AreEqual(result.Route, "/history/apollo/");
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.QueryParameters, string.Empty);
            Assert.AreEqual(result.Geolocation, TimeZoneInfo.Local.StandardName);
        }

        [Test]
        public void DataWithoutUnpropriateFilesAndWithQueryParamers_Test()
        {
            var testData = "199.72.81.55 - - [01/Jul/1995:00:00:01 -0400] \"GET /history/apollo?131,21 HTTP/1.0\" 200 6245";

            _sut.TryParse(testData, out var result);
            
            Assert.AreEqual(result.QueryParameters, "?131,21");
        }
        
        [Test]
        public void DataWithUnpropriateFilesAndWithQueryParamers_Test()
        {
            var testData = "gater3.sematech.org - - [01/Jul/1995:00:04:31 -0400] \"GET /cgi-bin/imagemap/countdown.js?370,274 HTTP/1.0\" 302 68";

            var result = _sut.TryParse(testData, out var _);
            
            Assert.IsFalse(result);
        }
    }
}