using System.Collections.Generic;
using System.IO;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services;

namespace CSV.Parser.Logic.Sandbox.Services
{
    public class CsvWriter : ICsvWriter
    {
        private readonly ICsvConfiguration _csvConfiguration;

        public CsvWriter(
            ICsvConfiguration csvConfiguration)
        {
            _csvConfiguration = csvConfiguration;
        }

        public void Write(string filePath, bool append, IEnumerable<string[]> lines)
        {
            using (var writer = new StreamWriter(filePath, append, _csvConfiguration.OutputEncoding))
            {
                foreach (var line in lines)
                {
                    var outputLine = new StringBuilder();

                    foreach (var field in line)
                    {
                        outputLine.Append(field);
                        outputLine.Append(_csvConfiguration.Delimiter);
                    }

                    writer.WriteLine(outputLine.Length > 0
                        ? outputLine.ToString(0, outputLine.Length - 1)
                        : string.Empty);
                }
            }
        }
    }
}