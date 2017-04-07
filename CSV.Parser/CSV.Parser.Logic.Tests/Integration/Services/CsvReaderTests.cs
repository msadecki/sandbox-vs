using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Exceptions;
using CSV.Parser.Logic.Factories;
using CSV.Parser.Logic.Services;
using Moq;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class CsvReaderTests
    {
        private class CsvLineConsumerDecorator : ICsvLineConsumer
        {
            private readonly ICsvLineConsumer _csvLineConsumer;

            public IList<ICsvLine> CsvLines { get; } = new List<ICsvLine>();

            public CsvLineConsumerDecorator(
                ICsvLineConsumer csvLineConsumer)
            {
                _csvLineConsumer = csvLineConsumer;
            }

            public void Consume(ICsvLine csvLine)
            {
                _csvLineConsumer.Consume(csvLine);
                CsvLines.Add(csvLine);
            }
        }

        private readonly ICsvReader _csvReader;

        private readonly Mock<ICsvLineConsumerFactory> _csvLineConsumerFactoryMock;

        private CsvLineConsumerDecorator _csvLineConsumerDecorator;

        public CsvReaderTests()
        {
            // Arrange
            _csvLineConsumerFactoryMock = new Mock<ICsvLineConsumerFactory>();
            _csvLineConsumerFactoryMock
                .Setup(x => x.Create(It.IsAny<TextWriter>(), It.IsAny<IOutputLineFactory>()))
                .Returns((TextWriter textWriter, IOutputLineFactory outputLineFactory) =>
                {
                    var csvLineConsumerFactory = new CsvLineConsumerFactory();
                    var csvLineConsumer = csvLineConsumerFactory.Create(textWriter, outputLineFactory);
                    _csvLineConsumerDecorator = new CsvLineConsumerDecorator(csvLineConsumer);
                    return _csvLineConsumerDecorator;
                });

            var outputConfiguration = new OutputConfiguration();

            _csvReader = new CsvReader(
                new CsvConfiguration(),
                new CsvFieldBuilderConfiguration(),
                new BufferableReaderConfiguration(),
                outputConfiguration,
                new EncodingConfiguration(),
                new TextReaderFactory(),
                new TextWriterFactory(),
                new OutputLineFactory(outputConfiguration),
                _csvLineConsumerFactoryMock.Object,
                new CsvStreamReaderFactory());
        }

        [Theory]
        [MemberData(nameof(GetValidTestCases))]
        public void Read_Should_Parse_Csv_File(
            string filePath,
            int expectedLinesCount,
            int? expectedFieldsCount)
        {
            // Act
            var actualResult = _csvReader.Read(filePath);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedLinesCount, actualResult);
            Assert.Equal(expectedLinesCount, _csvLineConsumerDecorator.CsvLines.Count);
            if (expectedFieldsCount.HasValue)
            {
                Assert.Equal(expectedFieldsCount.Value, _csvLineConsumerDecorator.CsvLines.First().Fields.Count);
            }

            _csvLineConsumerFactoryMock
                .Verify(x => x.Create(It.IsAny<TextWriter>(), It.IsAny<IOutputLineFactory>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestCases))]
        public void Read_Should_Throw_CsvInvalidFormatException_With_ExpectedMessage(string filePath, string expectedMessage)
        {
            // Act & Assert
            var actualException = Assert.Throws<CsvInvalidFormatException>(() => _csvReader.Read(filePath));
            Assert.Equal(expectedMessage, actualException.Message);
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
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.11.UTF8.BOM.Invalid.txt", GetExceptionMessage("Only delimiter or new line is allowed.", 1, 4, 1) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.12.UTF8.BOM.LastDelimiter.Invalid.txt", GetExceptionMessage("The last character in the last line must not be a field delimiter not followed by new line separator.", 1, 4, 0) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.13.UTF8.BOM.QuotationMark.Invalid.txt", GetExceptionMessage("Quotation mark is not allowed inside a field that is not enclosed with quotation mark.", 1, 2, 1) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.14.UTF8.BOM.FieldsCount.Invalid.txt", GetExceptionMessage("Each line should contain the same number of fields throughout the file.", 4, 3, 4) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.15.UTF8.BOM.FieldsCount.Invalid.txt", GetExceptionMessage("Each line should contain the same number of fields throughout the file.", 2, 7, 4) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.16.UTF8.BOM.Invalid.txt", GetExceptionMessage("Only delimiter or new line is allowed.", 1,3, 20) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.17.UTF8.BOM.QuotationMark.Invalid.txt", GetExceptionMessage("Only delimiter or new line is allowed.", 1, 2, 1) };
            yield return new object[] { @".\Integration\TestCases\CsvReader\CSV.18.UTF8.BOM.QuotationMark.Invalid.txt", GetExceptionMessage("Only delimiter or new line is allowed.", 2, 1, 1) };
        }

        private static string GetExceptionMessage(
            string message,
            int line,
            int field,
            int content)
        {
            return $"{message} {{ Line: {line}, Field: {field}, Content: {content} }}";
        }
    }
}