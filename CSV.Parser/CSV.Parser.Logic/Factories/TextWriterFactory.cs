using System;
using System.Diagnostics;
using System.IO;
using CSV.Parser.Logic.Abstractions.Enums;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;

namespace CSV.Parser.Logic.Factories
{
    public class TextWriterFactory : ITextWriterFactory
    {
        public TextWriter Create(
            OutputTarget outputTarget,
            IEncodingConfiguration encodingConfiguration)
        {
            switch (outputTarget)
            {
                case OutputTarget.Console:
                    return CreateConsoleStreamWriter(encodingConfiguration);
                case OutputTarget.File:
                    return CreateFileStreamWriter(encodingConfiguration);
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputTarget), outputTarget, null);
            }
        }

        private StreamWriter CreateConsoleStreamWriter(IEncodingConfiguration encodingConfiguration)
        {
            return new StreamWriter(Console.OpenStandardOutput(), encodingConfiguration.ConsoleOutputEncoding)
            {
                AutoFlush = true
            };
        }

        private StreamWriter CreateFileStreamWriter(IEncodingConfiguration encodingConfiguration)
        {
            var filePath = GetFilePath();
            return new StreamWriter(filePath, false, encodingConfiguration.FileOutputEncoding);
        }

        private string GetFilePath()
        {
            var frame = new StackTrace().GetFrame(3);
            var method = frame?.GetMethod();
            var methodName = method?.Name;
            var methodsClass = method?.DeclaringType;

            return $"./_.{methodsClass}.{methodName}.Output.csv";
        }
    }
}