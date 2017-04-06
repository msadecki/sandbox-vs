using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvLineConsumerFactory : ICsvLineConsumerFactory
    {
        public ICsvLineConsumer Create(
            IOutputConfiguration outputConfiguration,
            IEncodingConfiguration encodingConfiguration)
        {
            return new CsvLineConsumer(
                outputConfiguration,
                new TextWriterFactory(encodingConfiguration));
        }
    }
}