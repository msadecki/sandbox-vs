using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Factories;
using CSV.Parser.Logic.Services;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class CsvReaderTests
    {
        private readonly ICsvReader _csvReader;

        public CsvReaderTests()
        {
            _csvReader = new CsvReader(
                new TextReaderFactory(
                    new EncodingConfiguration()),
                new CsvLineConsumerFactory(),
                new CsvStreamReaderFactory());
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Read_Should_Parse_Csv_File(string filePath, int expectedLinesCount)
        {
            // Arrange

            // Act
            var actualResult = _csvReader.Read(filePath);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedLinesCount, actualResult);
        }

        private static IEnumerable<object[]> GetTestCases()
        {
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.01.UTF8.No.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.02.UTF8.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.03.UTF8.BOM.FieldWithCrLf.txt", 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.04.Empty.No.BOM.txt", 0 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.05.UTF16BE.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.06.UTF16LE.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.07.UTF32.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.08.UTF8.BOM.EmptyLinesOnly.txt", 4 };
        }
    }
}