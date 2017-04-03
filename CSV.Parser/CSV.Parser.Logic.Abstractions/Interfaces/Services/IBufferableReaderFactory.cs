using System.IO;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface IBufferableReaderFactory
    {
        IBufferableReader Create(TextReader textReader);
    }
}