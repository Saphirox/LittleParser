using LittleParser.Models.Entities;

namespace LittleParser.Services.Facades
{
    public interface IApacheLogParserProvider
    {
        bool TryParse(string line, out ApacheLog log);
    }
}