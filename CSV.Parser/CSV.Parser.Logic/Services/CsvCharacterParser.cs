using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvCharacterParser : ICsvCharacterParser
    {
        private readonly ICsvLineConsumer _csvLineConsumer;
        private readonly ICsvFieldBuilder _csvFieldBuilder;

        public CsvCharacterParser(
            ICsvLineConsumer csvLineConsumer,
            ICsvFieldBuilder csvFieldBuilder)
        {
            _csvLineConsumer = csvLineConsumer;
            _csvFieldBuilder = csvFieldBuilder;
        }

        public void ParseCharacter(char character)
        {
            if (!_csvFieldBuilder.Append(character))
            {
                return;
            }

            if (_csvFieldBuilder.IsDelimiterMatched)
            {
                _csvFieldBuilder.BuildNewFieldAfterDelimiter();

                _csvFieldBuilder.InitNewField();
            }
            else
            {
                _csvFieldBuilder.EnsureEndOfLineLengthToMatch();

                if (_csvFieldBuilder.IsEndOfLineMatched)
                {
                    _csvFieldBuilder.BuildNewFieldAfterEndOfLine();

                    var currentCsvLine = _csvFieldBuilder.CurrentCsvLine;

                    _csvFieldBuilder.InitNewLine();

                    _csvLineConsumer.Consume(currentCsvLine);
                }
            }
        }

        public int ParseTail()
        {
            if (_csvFieldBuilder.RawFieldBuilderLength > 0)
            {
                _csvFieldBuilder.BuildNewFieldFromTail();
            }

            var currentCsvLine = _csvFieldBuilder.CurrentCsvLine;

            if (currentCsvLine.Fields.Any())
            {
                _csvFieldBuilder.EnsureLastAppendantCharacterIsNotDelimiter();

                _csvFieldBuilder.InitNewLine();

                _csvLineConsumer.Consume(currentCsvLine);
            }

            return _csvFieldBuilder.CurrentCsvLineIndex;
        }
    }
}