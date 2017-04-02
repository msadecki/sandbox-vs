using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;

namespace CSV.Parser.Logic.Services
{
    public class CsvLineConsumerFactory : ICsvLineConsumerFactory
    {
        public ICsvLineConsumer Create()
        {
            return new CsvLineConsumer(
                new OutputConfiguration(),
                new TextWriterFactory(new EncodingConfiguration()));
        }
    }
}