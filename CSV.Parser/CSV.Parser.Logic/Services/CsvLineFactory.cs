using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Services
{
    public class CsvLineFactory : ICsvLineFactory
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldFactory _csvFieldFactory;

        public CsvLineFactory(
            ICsvConfiguration csvConfiguration,
            ICsvFieldFactory csvFieldFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldFactory = csvFieldFactory;
        }

        public CsvLine Create(string rawLine)
        {
            var fields = rawLine
                .Split(_csvConfiguration.Delimiter)
                .Select(_csvFieldFactory.Create)
                .ToList();

            return new CsvLine
            {
                Fields = fields
            };
        }
    }
}