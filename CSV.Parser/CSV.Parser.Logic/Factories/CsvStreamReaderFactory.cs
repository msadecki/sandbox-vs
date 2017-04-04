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
            return new CsvStreamReader(
                new CsvCharacterParserFactory(
                    new CsvConfiguration(),
                    new CsvFieldBuilderFactory(
                        new CsvLineFactory(),
                        new CsvFieldFactory())),
                new BufferableReaderFactory(
                    new BufferableReaderConfiguration()));
        }
    }
}