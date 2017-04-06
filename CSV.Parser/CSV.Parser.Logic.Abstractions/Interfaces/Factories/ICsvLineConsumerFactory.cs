using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ICsvLineConsumerFactory
    {
        ICsvLineConsumer Create(
            IOutputConfiguration outputConfiguration,
            IEncodingConfiguration encodingConfiguration);
    }
}