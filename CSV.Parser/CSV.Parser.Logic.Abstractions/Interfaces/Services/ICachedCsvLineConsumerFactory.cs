namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICachedCsvLineConsumerFactory
    {
        ICachedCsvLineConsumer Create();
    }
}