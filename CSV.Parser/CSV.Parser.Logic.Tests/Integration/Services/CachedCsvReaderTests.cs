using System.Collections.Generic;
using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Exceptions;
using CSV.Parser.Logic.Factories;
using CSV.Parser.Logic.Services;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class CachedCsvReaderTests
    {
        private readonly ICachedCsvReader _cachedCsvReader;

        public CachedCsvReaderTests()
        {
            _cachedCsvReader = new CachedCsvReader(
                new EncodingConfiguration(),
                new CsvConfiguration(),
                new CsvFieldBuilderConfiguration(),
                new BufferableReaderConfiguration(),
                new TextReaderFactory(),
                new CachedCsvLineConsumerFactory(),
                new CsvStreamReaderFactory());
        }

        [Theory]
        [MemberData(nameof(GetValidTestCases))]
        public void Read_Should_Parse_Csv_File(
            string filePath,
            int expectedLinesCount,
            int? expectedFieldsCount)
        {
            // Arrange

            // Act
            var actualResult = _cachedCsvReader.Read(filePath);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedLinesCount, actualResult.Count);
            if (expectedFieldsCount.HasValue)
            {
                Assert.Equal(expectedFieldsCount.Value, actualResult.First().Fields.Count);
            }
            // TODO: Here we can assert returned IList<ICsvLine> using FluentAssertions
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestCases))]
        public void Read_Should_Throw_CsvInvalidFormatException_With_ExpectedMessage(string filePath, string expectedExceptionMessage)
        {
            // Arrange & Act & Assert
            var actualException = Assert.Throws<CsvInvalidFormatException>(() => _cachedCsvReader.Read(filePath));
            Assert.Equal(expectedExceptionMessage, actualException.Message);

        }

        private static IEnumerable<object[]> GetValidTestCases()
        {
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.01.UTF8.No.BOM.txt", 8, 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.02.UTF8.BOM.txt", 8, 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.03.UTF8.BOM.FieldWithCrLf.txt", 5, 4 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.04.Empty.No.BOM.txt", 0, null };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.05.UTF16BE.BOM.txt", 8, 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.06.UTF16LE.BOM.txt", 8, 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.07.UTF32.BOM.txt", 8, 5 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.08.UTF8.BOM.EmptyLinesOnly.txt", 4, 1 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.09.UTF8.BOM.SingleLine.txt", 1, 4 };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.10.UTF8.BOM.LastDelimiter.txt", 1, 4 };
        }

        private static IEnumerable<object[]> GetInvalidTestCases()
        {
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.11.UTF8.BOM.Invalid.txt", $"Only delimiter or new line is expected. {GetPosition(0, 3, 1)}" };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.12.UTF8.BOM.LastDelimiter.Invalid.txt", $"Only delimiter or new line is expected. {GetPosition(0, 0, 0)}" };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.13.UTF8.BOM.QuotationMark.Invalid.txt", $"Only delimiter or new line is expected. {GetPosition(0, 0, 0)}" };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.14.UTF8.BOM.FieldsCount.Invalid.txt", $"Only delimiter or new line is expected. {GetPosition(0, 0, 0)}" };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.15.UTF8.BOM.FieldsCount.Invalid.txt", $"Only delimiter or new line is expected. {GetPosition(0, 0, 0)}" };
        }

        private static string GetPosition(
            int lineIndex,
            int fieldIndex,
            int fieldContentIndex)
        {
            return $"{{ LineIndex: {lineIndex}, FieldIndex: {fieldIndex}, FieldContentIndex: {fieldContentIndex} }}";
        }
    }
}