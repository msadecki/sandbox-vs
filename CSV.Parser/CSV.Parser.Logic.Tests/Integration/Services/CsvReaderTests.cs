using System;
using System.Collections.Generic;
using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
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
    public class CsvReaderTests : IDisposable
    {
        private sealed class CsvLineConsumerDecorator : ICsvLineConsumer
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

            public void Dispose()
            {
                _csvLineConsumer.Dispose();
            }
        }

        private readonly ICsvReader _csvReader;

        private readonly Mock<ICsvLineConsumerFactory> _csvLineConsumerFactoryMock;

        private CsvLineConsumerDecorator _csvLineConsumerDecorator;

        public CsvReaderTests()
        {
            _csvLineConsumerFactoryMock = new Mock<ICsvLineConsumerFactory>();
            _csvLineConsumerFactoryMock
                .Setup(x => x.Create(It.IsAny<IOutputConfiguration>(), It.IsAny<IEncodingConfiguration>()))
                .Returns((IOutputConfiguration outputConfiguration, IEncodingConfiguration encodingConfiguration) =>
                {
                    var csvLineConsumerFactory = new CsvLineConsumerFactory();
                    var csvLineConsumer = csvLineConsumerFactory.Create(outputConfiguration, encodingConfiguration);
                    _csvLineConsumerDecorator = new CsvLineConsumerDecorator(csvLineConsumer);
                    return _csvLineConsumerDecorator;
                });

            _csvReader = new CsvReader(
                new CsvConfiguration(),
                new CsvFieldBuilderConfiguration(),
                new BufferableReaderConfiguration(),
                new OutputConfiguration(),
                new EncodingConfiguration(),
                new TextReaderFactory(),
                _csvLineConsumerFactoryMock.Object,
                new CsvStreamReaderFactory());
        }

        void IDisposable.Dispose()
        {
            _csvLineConsumerDecorator?.Dispose();
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
                .Verify(x => x.Create(It.IsAny<IOutputConfiguration>(), It.IsAny<IEncodingConfiguration>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestCases))]
        public void Read_Should_Throw_CsvInvalidFormatException_With_ExpectedMessage(string filePath, string expectedExceptionMessage)
        {
            // Arrange

            // Act & Assert
            var actualException = Assert.Throws<CsvInvalidFormatException>(() => _csvReader.Read(filePath));
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