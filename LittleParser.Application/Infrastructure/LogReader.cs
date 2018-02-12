using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LittleParser.Models.Entities;
using LittleParser.Services.Facades;

namespace LittleParser.Application.Infrastructure
{
    public class LogReader
    {
        private readonly IApacheLogParserFacade _apacheLogParserFacade;
        private readonly HttpClientFacade _httpClientFacade;

        public LogReader(IApacheLogParserFacade apacheLogParserFacade, HttpClientFacade httpClientFacade)
        {
            _apacheLogParserFacade = apacheLogParserFacade;
            _httpClientFacade = httpClientFacade;
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
                        if (_apacheLogParserFacade.TryParse(await streamReader.ReadLineAsync(), out var result))
                        {
                            logs.Add(result);
                        }

                        logs.Add(result);

                        if (logs.Count > count)
                        {
                            await _httpClientFacade.SendAsJson(logs);
                            logs.Clear();
                        }
                    }
                }
            }
        }
    }
}