using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;

namespace CSV.Parser.Logic.Factories
{
    public class TextReaderFactory : ITextReaderFactory
    {
        public TextReader Create(
            string filePath,
            IEncodingConfiguration encodingConfiguration)
        {
            return new StreamReader(
                filePath,
                encodingConfiguration.DefaultFileInputEncoding,
                true);
        }
    }
}