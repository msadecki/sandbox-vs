using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICachedCsvReader
    {
        IList<ICsvLine> Read(string filePath);
    }
}
