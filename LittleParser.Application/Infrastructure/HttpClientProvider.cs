using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LittleParser.Common.Constants;


namespace LittleParser.Application.Infrastructure
{
    using System.Net.Http.Headers;

    public class HttpClientProvider : IDisposable
    {
        private readonly HttpClient _client;

        public HttpClientProvider(HttpClient client)
        {
            this._client = client;
        }

        public async Task SendAsJson(object obj)
        {
            var uri = new Uri(ApplicationConstants.PARSER_SERVER_URL_ADD_RANGE); 
            
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ApplicationConstants.POST_CONTENT_TYPE));

            try
            {
                var response = await _client.PostAsJsonAsync(uri, obj);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw;
            }
           
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}