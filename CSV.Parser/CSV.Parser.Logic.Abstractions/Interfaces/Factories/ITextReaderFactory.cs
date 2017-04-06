using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextReaderFactory
    {
        TextReader Create(
            string filePath,
            IEncodingConfiguration encodingConfiguration);
    }
}