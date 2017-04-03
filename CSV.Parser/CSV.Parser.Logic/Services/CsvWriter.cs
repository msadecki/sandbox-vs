using System.Collections.Generic;
using System.IO;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvWriter : ICsvWriter
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly IEncodingConfiguration _encodingConfiguration;

        public CsvWriter(
            ICsvConfiguration csvConfiguration,
            IEncodingConfiguration encodingConfiguration)
        {
            _csvConfiguration = csvConfiguration;
            _encodingConfiguration = encodingConfiguration;
        }

        public void Write(string filePath, bool append, IEnumerable<string[]> lines)
        {
            using (var writer = new StreamWriter(filePath, append, _encodingConfiguration.FileOutputEncoding))
            {
                foreach (var line in lines)
                {
                    var outputLine = new StringBuilder();

                    foreach (var field in line)
                    {
                        outputLine.Append(field);
                        outputLine.Append(_csvConfiguration.Delimiter);
                    }

                    writer.Write(outputLine.Length > 0
                        ? outputLine.ToString(0, outputLine.Length - 1)
                        : string.Empty);

                    writer.Write(_csvConfiguration.EndOfLine);
                }
            }
        }
    }
}