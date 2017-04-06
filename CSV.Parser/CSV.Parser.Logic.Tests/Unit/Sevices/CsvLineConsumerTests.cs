using System;
using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;
using Moq;
using Xunit;

namespace CSV.Parser.Logic.Tests.Unit.Sevices
{
    public class CsvLineConsumerTests
    {
        private readonly ICsvLineConsumer _csvLineConsumer;

        private readonly Mock<TextWriter> _textWriterMock;
        private readonly Mock<IOutputLineFactory> _outputLineFactoryMock;

        public CsvLineConsumerTests()
        {
            _textWriterMock = new Mock<TextWriter>();
            _outputLineFactoryMock = new Mock<IOutputLineFactory>();

            _csvLineConsumer = new CsvLineConsumer(
                _textWriterMock.Object,
                _outputLineFactoryMock.Object);
        }

        [Fact]
        public void Consume_Should_Call_Expected_Methods()
        {
            // Arrange
            var csvLineMock = new Mock<ICsvLine>();

            var expectedOutputLine = $"|{Guid.NewGuid():N}||{Environment.NewLine}";
            _outputLineFactoryMock
                .Setup(x => x.Create(csvLineMock.Object))
                .Returns(expectedOutputLine);

            // Act
            _csvLineConsumer.Consume(csvLineMock.Object);

            // Assert
            _outputLineFactoryMock
                .Verify(x => x.Create(csvLineMock.Object), Times.Once);
            _outputLineFactoryMock
                .Verify(x => x.Create(It.IsAny<ICsvLine>()), Times.Once);

            _textWriterMock
                .Verify(x => x.Write(expectedOutputLine), Times.Once);
            _textWriterMock
                .Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }
    }
}
