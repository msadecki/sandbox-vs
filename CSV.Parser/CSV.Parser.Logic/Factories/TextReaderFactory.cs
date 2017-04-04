using System.IO;
using System.Text;
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
            return CreateStreamReader(filePath);
        }

        /// <summary>
        /// TODO: Method is to be deleted in final implementation
        /// </summary>
        public Encoding GetCurrentEncoding(string filePath)
        {
            using (var streamReader = CreateStreamReader(filePath))
            {
                streamReader.Peek();

                return streamReader.CurrentEncoding;
            }
        }

        private StreamReader CreateStreamReader(string filePath)
        {
            return new StreamReader(filePath, _encodingConfiguration.DefaultInputEncoding, true);
        }
    }
}