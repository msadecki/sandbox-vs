using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvFieldBuilderFactory : ICsvFieldBuilderFactory
    {
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;

        public CsvFieldBuilderFactory(
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory)
        {
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;
        }

        public ICsvFieldBuilder Create(ICsvConfiguration csvConfiguration)
        {
            return new CsvFieldBuilder(
                csvConfiguration,
                _csvLineFactory,
                _csvFieldFactory);
        }
    }
}