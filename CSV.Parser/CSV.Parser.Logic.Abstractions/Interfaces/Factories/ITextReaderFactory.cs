using System.IO;
using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextReaderFactory
    {
        TextReader Create(string filePath);

        Encoding GetCurrentEncoding(string filePath);
    }
}