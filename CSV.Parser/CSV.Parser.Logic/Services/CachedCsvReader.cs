using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CachedCsvReader : ICachedCsvReader
    {
        private readonly IEncodingConfiguration _encodingConfiguration;
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly IBufferableReaderConfiguration _bufferableReaderConfiguration;
        private readonly ITextReaderFactory _textReaderFactory;
        private readonly ICachedCsvLineConsumerFactory _cachedCsvLineConsumerFactory;
        private readonly ICsvStreamReaderFactory _csvStreamReaderFactory;

        public CachedCsvReader(
            IEncodingConfiguration encodingConfiguration,
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            IBufferableReaderConfiguration bufferableReaderConfiguration,
            ITextReaderFactory textReaderFactory,
            ICachedCsvLineConsumerFactory cachedCsvLineConsumerFactory,
            ICsvStreamReaderFactory csvStreamReaderFactory)
        {
            _encodingConfiguration = encodingConfiguration;
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _bufferableReaderConfiguration = bufferableReaderConfiguration;
            _textReaderFactory = textReaderFactory;
            _cachedCsvLineConsumerFactory = cachedCsvLineConsumerFactory;
            _csvStreamReaderFactory = csvStreamReaderFactory;
        }

        public IList<ICsvLine> Read(string filePath)
        {
            using (var textReader = _textReaderFactory.Create(filePath, _encodingConfiguration))
            {
                using (var cachedCsvLineConsumer = _cachedCsvLineConsumerFactory.Create())
                {
                    _csvStreamReaderFactory
                        .Create(_csvConfiguration, _csvFieldBuilderConfiguration, _bufferableReaderConfiguration)
                        .Read(textReader, cachedCsvLineConsumer);

                    return cachedCsvLineConsumer.CsvLines;
                }
            }
        }
    }
}