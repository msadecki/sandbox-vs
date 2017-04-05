using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public sealed class CachedCsvLineConsumer : ICachedCsvLineConsumer
    {
        private readonly List<ICsvLine> _csvCsvLines = new List<ICsvLine>();

        public IList<ICsvLine> CsvLines => _csvCsvLines;

        public void Consume(ICsvLine csvLine)
        {
            _csvCsvLines.Add(csvLine);
        }

        public void Dispose()
        {
        }
    }
}