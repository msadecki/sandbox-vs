using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvReader : ICsvReader
    {
        private readonly ITextReaderFactory _textReaderFactory;
        private readonly ICsvLineConsumerFactory _csvLineConsumerFactory;
        private readonly ICsvStreamReaderFactory _csvStreamReaderFactory;

        public CsvReader(
            ITextReaderFactory textReaderFactory,
            ICsvLineConsumerFactory csvLineConsumerFactory,
            ICsvStreamReaderFactory csvStreamReaderFactory)
        {
            _textReaderFactory = textReaderFactory;
            _csvLineConsumerFactory = csvLineConsumerFactory;
            _csvStreamReaderFactory = csvStreamReaderFactory;
        }

        public int Read(string filePath)
        {
            using (var textReader = _textReaderFactory.Create(filePath))
            {
                using (var csvLineConsumer = _csvLineConsumerFactory.Create())
                {
                    return _csvStreamReaderFactory
                        .Create()
                        .Read(textReader, csvLineConsumer);
                }
            }
        }
    }
}