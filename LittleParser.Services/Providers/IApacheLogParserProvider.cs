using LittleParser.Models.Entities;

namespace LittleParser.Services.Providers
{
    /// <summary>
    // Facade for using ApacheParser
    /// </summary>
    public interface IApacheLogParserProvider
    {
        bool TryParse(string line, out ApacheLog log);
    }
}