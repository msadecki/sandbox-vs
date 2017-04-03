using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;

namespace CSV.Parser.Logic.Services
{
    public class CsvStreamReaderFactory : ICsvStreamReaderFactory
    {
        public ICsvStreamReader Create()
        {
            var csvConfiguration = new CsvConfiguration();

            return new CsvStreamReader(
                csvConfiguration,
                new CsvLineFactory(),
                new CsvFieldFactory(),
                new BufferableReaderFactory(
                    new BufferableReaderConfiguration()));
        }
    }
}