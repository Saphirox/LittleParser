using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LittleParser.Common.Constants;

namespace LittleParser.Application.Infrastructure
{
    public class HttpClientFacade : IDisposable
    {
        private readonly HttpClient _client;

        public HttpClientFacade(HttpClient client)
        {
            this._client = client;
        }

        public async Task SendAsJson(object obj)
        {
            var uri = new Uri(ApplicationConstants.PARSER_SERVER_URL_ADD_RANGE); 
            
            var json = new JavaScriptSerializer().Serialize(obj);
            
            var stringContent = new StringContent(json, Encoding.UTF8, ApplicationConstants.POST_CONTENT_TYPE);
            
            var response = await _client.PostAsync(uri, stringContent);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}