using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface IBufferableReaderFactory
    {
        IBufferableReader Create(TextReader textReader);
    }
}