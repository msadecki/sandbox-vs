using System.IO;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextReaderFactory
    {
        TextReader Create(string filePath);
    }
}