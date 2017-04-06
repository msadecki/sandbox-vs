using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvReaderFactory : ICsvReaderFactory
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly IBufferableReaderConfiguration _bufferableReaderConfiguration;
        private readonly IOutputConfiguration _outputConfiguration;
        private readonly IEncodingConfiguration _encodingConfiguration;

        public CsvReaderFactory(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            IBufferableReaderConfiguration bufferableReaderConfiguration,
            IOutputConfiguration outputConfiguration,
            IEncodingConfiguration encodingConfiguration)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _bufferableReaderConfiguration = bufferableReaderConfiguration;
            _outputConfiguration = outputConfiguration;
            _encodingConfiguration = encodingConfiguration;
        }

        public ICsvReader Create()
        {
            return new CsvReader(
                _csvConfiguration,
                _csvFieldBuilderConfiguration,
                _bufferableReaderConfiguration,
                _outputConfiguration,
                _encodingConfiguration,
                new TextReaderFactory(),
                new CsvLineConsumerFactory(),
                new CsvStreamReaderFactory());
        }
    }
}