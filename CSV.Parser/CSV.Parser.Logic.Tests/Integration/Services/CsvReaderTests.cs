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
            yield return new object[] { @".\Integration\TestCases\Test.00.UTF8.No.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.01.UTF8.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.02.UTF8.BOM.FieldWithCrLf.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.03.Empty.No.BOM.txt", 0 };
            yield return new object[] { @".\Integration\TestCases\Test.04.UTF16BE.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.05.UTF16LE.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.06.ANSII.Windows-1250.No.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.07.UTF32.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.08.UTF8.BOM.EmptyLinesOnly.txt", 4 };
        }
    }
}