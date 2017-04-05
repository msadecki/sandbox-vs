using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;

namespace CSV.Parser.Logic.Factories
{
    public class TextReaderFactory : ITextReaderFactory
    {
        private readonly IEncodingConfiguration _encodingConfiguration;

        public TextReaderFactory(
            IEncodingConfiguration encodingConfiguration)
        {
            _encodingConfiguration = encodingConfiguration;
        }

        public TextReader Create(string filePath)
        {
            return new StreamReader(
                filePath,
                _encodingConfiguration.DefaultFileInputEncoding,
                true);
        }
    }
}