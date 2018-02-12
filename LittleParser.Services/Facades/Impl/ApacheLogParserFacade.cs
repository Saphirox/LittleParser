using LittleParser.Models.Entities;

namespace LittleParser.Services.Facades
{
    public interface IApacheLogParserFacade
    {
        bool TryParse(string line, out ApacheLog log);
    }
}