using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
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