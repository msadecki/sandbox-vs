using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvFieldBuilderFactory : ICsvFieldBuilderFactory
    {
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;
        private readonly ICsvFieldBuilderStateFactory _csvFieldBuilderStateFactory;

        public CsvFieldBuilderFactory(
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory,
            ICsvFieldBuilderStateFactory csvFieldBuilderStateFactory)
        {
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;
            _csvFieldBuilderStateFactory = csvFieldBuilderStateFactory;
        }

        public ICsvFieldBuilder Create(ICsvConfiguration csvConfiguration)
        {
            return new CsvFieldBuilder(
                csvConfiguration,
                _csvFieldBuilderConfiguration,
                _csvLineFactory,
                _csvFieldFactory,
                _csvFieldBuilderStateFactory.Create());
        }
    }
}