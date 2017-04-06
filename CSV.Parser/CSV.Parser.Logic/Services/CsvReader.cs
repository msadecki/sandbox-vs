using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvReader : ICsvReader
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly IBufferableReaderConfiguration _bufferableReaderConfiguration;
        private readonly IOutputConfiguration _outputConfiguration;
        private readonly IEncodingConfiguration _encodingConfiguration;
        private readonly ITextReaderFactory _textReaderFactory;
        private readonly ICsvLineConsumerFactory _csvLineConsumerFactory;
        private readonly ICsvStreamReaderFactory _csvStreamReaderFactory;

        public CsvReader(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            IBufferableReaderConfiguration bufferableReaderConfiguration,
            IOutputConfiguration outputConfiguration,
            IEncodingConfiguration encodingConfiguration,
            ITextReaderFactory textReaderFactory,
            ICsvLineConsumerFactory csvLineConsumerFactory,
            ICsvStreamReaderFactory csvStreamReaderFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _bufferableReaderConfiguration = bufferableReaderConfiguration;
            _outputConfiguration = outputConfiguration;
            _encodingConfiguration = encodingConfiguration;
            _textReaderFactory = textReaderFactory;
            _csvLineConsumerFactory = csvLineConsumerFactory;
            _csvStreamReaderFactory = csvStreamReaderFactory;
        }

        public int Read(string filePath)
        {
            using (var textReader = _textReaderFactory.Create(filePath, _encodingConfiguration))
            {
                using (var csvLineConsumer = _csvLineConsumerFactory.Create(_outputConfiguration, _encodingConfiguration))
                {
                    var csvStreamReader = _csvStreamReaderFactory.Create(
                        _csvConfiguration,
                        _csvFieldBuilderConfiguration,
                        _bufferableReaderConfiguration);

                    return csvStreamReader.Read(textReader, csvLineConsumer);
                }
            }
        }
    }
}