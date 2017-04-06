using System.Linq;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    /// <summary>
    /// TODO: Make DoD in README.md that contains some comments and TODO's
    /// TODO: Clean that comment and all others TODO's and comments in solution (move it to readme or "backlog").
    /// TODO: Clean R# code issues
    /// 
    /// http://stackoverflow.com/questions/9266747/writing-and-polishing-a-csv-parser
    /// Careful, this doesn't work for fields with embedded newlines, fields containing the delimiter as a literal, and quote-enclosed fields. – Corbin March Oct 17 '12 at 14:00
    /// 
    /// http://www.dreamincode.net/forums/topic/378128-how-to-import-csv-file-data-into-datagridview-in-load-function/
    /// TextFieldParser is slow because it actually follows RFC 4180 with regards to how to correctly parse CSV files, unlike String.Split() which assumes that there are no embedded separators within quotes, or any escaped characters. 
    /// </summary>
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

        // TODO/Verify done: Do not leave private methods - these move to sth like CsvCurrentCharacterParser
        public void ParseCharacter(char character)
        {
            // TODO: Implement in clean way
            // TODO: Implement rfc4180 standard in CsvStreamReader & CsvLineFactory (it may be removed or refactored)
            // TODO/REMARKS: Empty lines should not be ignored (TODO: How to count lines in 1 column CSV - if all fields are empty then ..., if some are nonempty then ... ?)
            // TODO/REMARKS: First line should determine fields count - use it to validate all other lines (use exception throwing or some validation result)
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
            // TODO: Verify that it always works correctly (pl: mo¿e jak nie ma koñca linii, pole jest puste i na koñcu by³ delimiter to jest to b³¹d formatu? See doc "The last field in the record must not be followed by a comma")
            if (_csvFieldBuilder.RawFieldBuilderLength > 0)
            {
                _csvFieldBuilder.BuildNewFieldFromTail();
            }

            // TODO: Check fields count with first line on every InitNewLine (CSV.14.UTF8.BOM.FieldsCount.Invalid.txt, ...)
            if (_csvFieldBuilder.CurrentCsvLine.Fields.Any())
            {
                _csvLineConsumer.Consume(_csvFieldBuilder.CurrentCsvLine);

                _csvFieldBuilder.InitNewLine();
            }

            return _csvFieldBuilder.CreatedLinesCount;
        }
    }
}