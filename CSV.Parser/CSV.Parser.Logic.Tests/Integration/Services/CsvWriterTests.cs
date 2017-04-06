using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Factories;
using CSV.Parser.Logic.Models;
using CSV.Parser.Logic.Services;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class CsvWriterTests
    {
        private readonly ICsvWriter _csvWriter = new CsvWriter(
            new CsvConfiguration(),
            new EncodingConfiguration(),
            new TextWriterFactory());

        [Fact]
        public void Write_Should_Write_To_Csv_File()
        {
            // Arrange
            IList<ICsvLine> csvLines = new List<ICsvLine>
            {
                new CsvLine
                {
                    Fields = new List<ICsvField>
                    {
                        new CsvField { Content = "H1.1" },
                        new CsvField { Content = " H2 " },
                        new CsvField { Content = @"""H3""" },
                        new CsvField { Content = "H4" }
                    }
                },
                new CsvLine
                {
                    Fields = new List<ICsvField>
                    {
                        new CsvField { Content = "V2.1" },
                        new CsvField { Content = " V2 " },
                        new CsvField { Content = @"""V3""" },
                        new CsvField { Content = "V4: [ąćęłńóśźż] [ĄĆĘŁŃÓŚŹŻ] " }
                    }
                },
                new CsvLine
                {
                    Fields = new List<ICsvField>
                    {
                        new CsvField { Content = "V3.1" },
                        new CsvField { Content = " V2 " },
                        new CsvField { Content = @"V3
                                                   -1,
                                                   -2,
                                                 ""...""

                                                      " },
                        new CsvField { Content = "V4" }
                    }
                },
                new CsvLine
                {
                    Fields = new List<ICsvField>
                    {
                        new CsvField { Content = null },
                        new CsvField { Content = string.Empty },
                        new CsvField { Content = null },
                        new CsvField { Content = null }
                    }
                },
                new CsvLine
                {
                    Fields = new List<ICsvField>
                    {
                        new CsvField { Content = "V5.1" },
                        new CsvField { Content = null },
                        new CsvField { Content = @"V3""" },
                        new CsvField { Content = null }
                    }
                }
            };

            // Act
            _csvWriter.Write(csvLines);

            // Assert
        }
    }
}