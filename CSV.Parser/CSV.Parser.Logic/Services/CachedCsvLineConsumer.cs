using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Services
{
    public class CachedCsvLineConsumer : ICachedCsvLineConsumer
    {
        private readonly List<CsvLine> _csvCsvLines = new List<CsvLine>();

        public IList<CsvLine> CsvLines => _csvCsvLines;

        public void Consume(CsvLine csvLine)
        {
            _csvCsvLines.Add(csvLine);
        }

        public void Dispose()
        {
        }
    }
}