using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LittleParser.Models.Entities;
using LittleParser.Services.Facades;

namespace LittleParser.Application.Infrastructure
{
    public class LogReader
    {
        private readonly IApacheLogParserProvider _apacheLogParserProvider;
        private readonly HttpClientProvider _httpClientProvider;

        public LogReader(IApacheLogParserProvider apacheLogParserProvider, HttpClientProvider httpClientProvider)
        {
            _apacheLogParserProvider = apacheLogParserProvider;
            _httpClientProvider = httpClientProvider;
        }

        public async Task ReadAndSendAsync(string fileName)
        {
            var count = 100 * 10 * 900;

            var logs = new List<ApacheLog>(count);

            var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            using (BufferedStream bufferedStream = new BufferedStream(fileStream))
            {
                using (StreamReader streamReader = new StreamReader(bufferedStream))
                {
                    while (streamReader.Peek() > -1)
                    {
                        if (_apacheLogParserProvider.TryParse(await streamReader.ReadLineAsync(), out var result))
                        {
                            logs.Add(result);
                        }

                        logs.Add(result);

                        if (logs.Count > count)
                        {
                            await _httpClientProvider.SendAsJson(logs);
                            logs.Clear();
                        }
                    }
                }
            }
        }
    }
}