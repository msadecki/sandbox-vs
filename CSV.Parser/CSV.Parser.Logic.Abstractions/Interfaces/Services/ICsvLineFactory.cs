using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvLineFactory
    {
        CsvLine Create(string rawLine);
    }
}