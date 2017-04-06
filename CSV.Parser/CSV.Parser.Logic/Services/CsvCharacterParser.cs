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

                    _csvLineConsumer.Consume(_csvFieldBuilder.CurrentCsvLine);

                    _csvFieldBuilder.InitNewLine();
                }
            }
        }

        public int ParseTail()
        {
            if (_csvFieldBuilder.RawFieldBuilderLength > 0)
            {
                _csvFieldBuilder.BuildNewFieldFromTail();
            }

            if (_csvFieldBuilder.CurrentCsvLine.Fields.Any())
            {
                _csvLineConsumer.Consume(_csvFieldBuilder.CurrentCsvLine);

                _csvFieldBuilder.InitNewLine();
            }

            return _csvFieldBuilder.CreatedLinesCount;
        }
    }
}