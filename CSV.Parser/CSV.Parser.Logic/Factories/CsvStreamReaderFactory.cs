using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvStreamReaderFactory : ICsvStreamReaderFactory
    {
        public ICsvStreamReader Create(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            IBufferableReaderConfiguration bufferableReaderConfiguration)
        {
            return new CsvStreamReader(
                new CsvCharacterParserFactory(
                    csvConfiguration,
                    new CsvFieldBuilderFactory(
                        csvFieldBuilderConfiguration,
                        new CsvLineFactory(),
                        new CsvFieldFactory(),
                        new CsvFieldBuilderStateFactory())),
                new BufferableReaderFactory(
                    bufferableReaderConfiguration));
        }
    }
}