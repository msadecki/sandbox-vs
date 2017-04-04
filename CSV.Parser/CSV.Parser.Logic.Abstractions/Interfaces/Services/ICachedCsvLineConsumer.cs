using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICachedCsvLineConsumer : ICsvLineConsumer
    {
        IList<ICsvLine> CsvLines { get; }
    }
}