using System;
using System.IO;
using CSV.Parser.Logic.Abstractions.Enums;
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

        public TextWriter Create(OutputTarget outputTarget)
        {
            switch (outputTarget)
            {
                case OutputTarget.Console:
                    return CreateConsoleStreamWriter();
                case OutputTarget.File:
                    return CreateFileStreamWriter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputTarget), outputTarget, null);
            }
        }

        private StreamWriter CreateConsoleStreamWriter()
        {
            return new StreamWriter(Console.OpenStandardOutput(), _encodingConfiguration.ConsoleOutputEncoding)
            {
                AutoFlush = true
            };
        }

        private StreamWriter CreateFileStreamWriter()
        {
            var filePath = "./TODO.Some.Safe.Place.To.Write.File.And.UniqueTimestamp.txt";
            return new StreamWriter(filePath, false, _encodingConfiguration.FileOutputEncoding);
        }
    }
}