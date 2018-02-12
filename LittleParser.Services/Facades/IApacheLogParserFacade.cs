using LittleParser.Models.Entities;

namespace LittleParser.Services.Services
{
    public interface IApacheLogParserFacade
    {
        ApacheLog Parse(string line);
    }
}