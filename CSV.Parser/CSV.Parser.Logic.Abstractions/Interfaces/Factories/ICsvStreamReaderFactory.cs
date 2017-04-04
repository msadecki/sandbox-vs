using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ICsvStreamReaderFactory
    {
        ICsvStreamReader Create();
    }
}