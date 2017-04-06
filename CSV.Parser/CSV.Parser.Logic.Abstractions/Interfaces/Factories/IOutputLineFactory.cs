using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface IOutputLineFactory
    {
        string Create(ICsvLine csvLine);
    }
}