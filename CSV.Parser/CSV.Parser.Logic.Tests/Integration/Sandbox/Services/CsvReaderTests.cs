using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services;
using CSV.Parser.Logic.Sandbox.Configurations;
using CSV.Parser.Logic.Sandbox.Services;
using System.Collections.Generic;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Sandbox.Services
{
    public class CsvReaderTests
    {
        private readonly ICsvReader _csvReader = new CsvReader(
            new CsvConfiguration(),
            new FileEncodingResolver());

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Read_Should_Parse_Csv_File(string filePath, int expectedLinesCount)
        {
            // Arrange

            // Act
            var actualResult = _csvReader.Read(filePath);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedLinesCount, actualResult.Count);
        }

        private static IEnumerable<object[]> GetTestCases()
        {
            yield return new object[] { @".\Integration\TestCases\Test.01.UTF8.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.02.UTF8.BOM.txt", 8 };
            yield return new object[] { @".\Integration\TestCases\Test.03.Empty.No.BOM.txt", 0 };
            yield return new object[] { @".\Integration\TestCases\Test.04.UTF16BE.BOM.txt", 8 };
        }
    }
}