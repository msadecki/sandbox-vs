namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvLineConsumerFactory
    {
        ICsvLineConsumer Create();
    }
}