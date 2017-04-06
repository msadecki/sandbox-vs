using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvStreamReader : ICsvStreamReader
    {
        private readonly ICsvCharacterParserFactory _csvCharacterParserFactory;
        private readonly IBufferableReaderFactory _bufferableReaderFactory;

        public CsvStreamReader(
            ICsvCharacterParserFactory csvCharacterParserFactory,
            IBufferableReaderFactory bufferableReaderFactory)
        {
            _csvCharacterParserFactory = csvCharacterParserFactory;
            _bufferableReaderFactory = bufferableReaderFactory;
        }

        public int Read(TextReader textReader, ICsvLineConsumer csvLineConsumer)
        {
            var bufferableReader = _bufferableReaderFactory.Create(textReader);

            var csvCharacterParser = _csvCharacterParserFactory.Create(csvLineConsumer);

            while (bufferableReader.ReadBuffer())
            {
                for (var pos = 0; pos < bufferableReader.BufferLength; pos++)
                {
                    csvCharacterParser.ParseCharacter(bufferableReader.Buffer[pos]);
                }
            }

            return csvCharacterParser.ParseTail();
        }
    }
}