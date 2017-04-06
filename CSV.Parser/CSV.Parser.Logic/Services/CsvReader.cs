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
        private readonly ITextWriterFactory _textWriterFactory;
        private readonly IOuputLineFactory _ouputLineFactory;
        private readonly ICsvLineConsumerFactory _csvLineConsumerFactory;
        private readonly ICsvStreamReaderFactory _csvStreamReaderFactory;

        public CsvReader(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            IBufferableReaderConfiguration bufferableReaderConfiguration,
            IOutputConfiguration outputConfiguration,
            IEncodingConfiguration encodingConfiguration,
            ITextReaderFactory textReaderFactory,
            ITextWriterFactory textWriterFactory,
            IOuputLineFactory ouputLineFactory,
            ICsvLineConsumerFactory csvLineConsumerFactory,
            ICsvStreamReaderFactory csvStreamReaderFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _bufferableReaderConfiguration = bufferableReaderConfiguration;
            _outputConfiguration = outputConfiguration;
            _encodingConfiguration = encodingConfiguration;
            _textReaderFactory = textReaderFactory;
            _textWriterFactory = textWriterFactory;
            _ouputLineFactory = ouputLineFactory;
            _csvLineConsumerFactory = csvLineConsumerFactory;
            _csvStreamReaderFactory = csvStreamReaderFactory;
        }

        public int Read(string filePath)
        {
            using (var textReader = _textReaderFactory.Create(filePath, _encodingConfiguration))
            {
                using (var textWriter = _textWriterFactory.Create(_outputConfiguration.OutputTarget, _encodingConfiguration))
                {
                    var csvLineConsumer = _csvLineConsumerFactory.Create(
                        textWriter,
                        _ouputLineFactory);

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