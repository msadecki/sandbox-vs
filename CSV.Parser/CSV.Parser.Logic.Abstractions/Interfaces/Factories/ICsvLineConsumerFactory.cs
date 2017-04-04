using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ICsvLineConsumerFactory
    {
        ICsvLineConsumer Create();
    }
}