using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CachedCsvLineConsumerFactory : ICachedCsvLineConsumerFactory
    {
        public ICachedCsvLineConsumer Create()
        {
            return new CachedCsvLineConsumer();
        }
    }
}