using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LittleParser.Common.Constants;
using LittleParser.Models.Entities;
using LittleParser.Services.Providers;

namespace LittleParser.Application.Infrastructure
{
    public class LogFacade
    {
        private readonly IApacheLogParserProvider _apacheLogParserProvider;
        private readonly HttpClientProvider _httpClientProvider;

        public LogFacade(IApacheLogParserProvider apacheLogParserProvider, HttpClientProvider httpClientProvider)
        {
            _apacheLogParserProvider = apacheLogParserProvider;
            _httpClientProvider = httpClientProvider;
        }

        /// <summary>
        /// Read user file and send parsing result to server
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task ReadAndSendAsync(string fileName)
        {
            var logs = new List<ApacheLog>();

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read , ApplicationConstants.FILE_BUFFER_SIZE, FileOptions.Asynchronous);

            using (fileStream)
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while (streamReader.Peek() > -1)
                        {
                            var line = await streamReader.ReadLineAsync();

                            if (_apacheLogParserProvider.TryParse(line, out var result))
                            {
                                logs.Add(result);

                                if (logs.Count > ApplicationConstants.MAX_REQUEST_ELEMENTS)
                                {
                                    await _httpClientProvider.SendAsJson(logs);

                                    logs.Clear();
                                }
                            }
                        }

                        if (logs.Any())
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