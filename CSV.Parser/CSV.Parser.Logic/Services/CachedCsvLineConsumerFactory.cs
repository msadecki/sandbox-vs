using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CachedCsvLineConsumerFactory : ICachedCsvLineConsumerFactory
    {
        public ICachedCsvLineConsumer Create()
        {
            return new CachedCsvLineConsumer();
        }
    }
}