namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvStreamReaderFactory
    {
        ICsvStreamReader Create();
    }
}