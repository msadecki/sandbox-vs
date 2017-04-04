using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvCharacterParserFactory : ICsvCharacterParserFactory
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderFactory _csvFieldBuilderFactory;

        public CsvCharacterParserFactory(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderFactory csvFieldBuilderFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderFactory = csvFieldBuilderFactory;
        }

        public ICsvCharacterParser Create(ICsvLineConsumer csvLineConsumer)
        {
            return new CsvCharacterParser(
                csvLineConsumer,
                _csvConfiguration,
                _csvFieldBuilderFactory);
        }
    }
}