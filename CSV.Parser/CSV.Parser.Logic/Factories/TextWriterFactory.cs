using System;
using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;

namespace CSV.Parser.Logic.Factories
{
    public class TextWriterFactory : ITextWriterFactory
    {
        private readonly IEncodingConfiguration _encodingConfiguration;

        public TextWriterFactory(
            IEncodingConfiguration encodingConfiguration)
        {
            _encodingConfiguration = encodingConfiguration;
        }

        public TextWriter Create()
        {
            return CreateStreamWriter();
        }

        private StreamWriter CreateStreamWriter()
        {
            return new StreamWriter(Console.OpenStandardOutput(), _encodingConfiguration.ConsoleOutputEncoding)
            {
                AutoFlush = true
            };
        }
    }
}