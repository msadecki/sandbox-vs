using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvFieldFactory
    {
        CsvField Create(string fieldContent);
    }
}