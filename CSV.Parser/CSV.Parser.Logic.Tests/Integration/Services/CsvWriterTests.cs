using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Services;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class CsvWriterTests
    {
        private readonly ICsvWriter _csvWriter = new CsvWriter(
            new CsvConfiguration(),
            new EncodingConfiguration());

        [Fact]
        public void Write_Should_Write_To_Csv_File()
        {
            // Arrange
            var filePath = @".\Integration\TestCases\Output.Write_Should_Write_To_Csv_File.csv";
            var lines = new List<string[]>
            {
                new [] {"H1.0", " H2 ", @"""H3""", "H4"},
                
                new [] {"V1.1", " V2 ", @"""V3""", "V4: [ąćęłńóśźż] [ĄĆĘŁŃÓŚŹŻ] "},
                new [] {"V1.2", " V2 ", @"V3
-1,
-2,
    ...

", "V4"},
                new [] {"V1.3", " V2 ", @"V3""", "V4"}
            };

            // Act & Assert
            _csvWriter.Write(filePath, false, lines);
        }
    }
}