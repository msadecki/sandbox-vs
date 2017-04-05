using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvStreamReaderFactory : ICsvStreamReaderFactory
    {
        public ICsvStreamReader Create()
        {
            // TODO: Consider using DI Container - Autofac or Castle
            return new CsvStreamReader(
                new CsvCharacterParserFactory(
                    new CsvConfiguration(),
                    new CsvFieldBuilderFactory(
                        new CsvFieldBuilderConfiguration(),
                        new CsvLineFactory(),
                        new CsvFieldFactory(),
                        new CsvFieldBuilderStateFactory())),
                new BufferableReaderFactory(
                    new BufferableReaderConfiguration()));
        }
    }
}