using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Services
{
    public class CachedCsvReader : ICachedCsvReader
    {
        private readonly ITextReaderFactory _textReaderFactory;
        private readonly ICachedCsvLineConsumerFactory _cachedCsvLineConsumerFactory;
        private readonly ICsvStreamReaderFactory _csvStreamReaderFactory;

        public CachedCsvReader(
            ITextReaderFactory textReaderFactory,
            ICachedCsvLineConsumerFactory cachedCsvLineConsumerFactory,
            ICsvStreamReaderFactory csvStreamReaderFactory)
        {
            _textReaderFactory = textReaderFactory;
            _cachedCsvLineConsumerFactory = cachedCsvLineConsumerFactory;
            _csvStreamReaderFactory = csvStreamReaderFactory;
        }

        public IList<CsvLine> Read(string filePath)
        {
            using (var textReader = _textReaderFactory.Create(filePath))
            {
                using (var cachedCsvLineConsumer = _cachedCsvLineConsumerFactory.Create())
                {
                    _csvStreamReaderFactory
                        .Create()
                        .Read(textReader, cachedCsvLineConsumer);

                    return cachedCsvLineConsumer.CsvLines;
                }
            }
        }
    }
}