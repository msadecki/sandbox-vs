using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    /// <summary>
    /// TODO: Clean that comment and all others in solution.
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
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilder _csvFieldBuilder;

        public CsvCharacterParser(
            ICsvLineConsumer csvLineConsumer,
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderFactory csvFieldBuilderFactory)
        {
            _csvLineConsumer = csvLineConsumer;
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilder = csvFieldBuilderFactory.Create(csvConfiguration);
        }

        // TODO/Verify done: Do not leave private methods - these move to sth like CsvCurrentCharacterParser
        public void ParseCharacter(char currentCharacter)
        {
            // TODO: Implement in clean way
            // TODO: Implement rfc4180 standard in CsvStreamReader & CsvLineFactory (it may be removed or refactored)
            // TODO/REMARKS: Empty lines should not be ignored (TODO: How to count lines in 1 column CSV - if all fields are empty then ..., if some are nonempty then ... ?)
            // TODO/REMARKS: First line should determine fields count - use it to validate all other lines (use exception throwing or some validation result)
            _csvFieldBuilder.Append(currentCharacter);

            if (_csvFieldBuilder.IsDelimiterMatched())
            {
                _csvFieldBuilder.BuildNewField(1);
                _csvFieldBuilder.InitNewField();
            }

            _csvFieldBuilder.EnsureEndOfLineLengthToMatch();

            if (_csvFieldBuilder.IsEndOfLineMatched())
            {
                _csvFieldBuilder.BuildNewField(_csvConfiguration.EndOfLine.Length);

                _csvLineConsumer.Consume(_csvFieldBuilder.CurrentCsvLine);
                _csvFieldBuilder.InitNewLine();
            }
        }

        public int ParseTail()
        {
            if (_csvFieldBuilder.RawFieldBuilderLength > 0)
            {
                // TODO: Test the logic - "pl: czy nie pomija szukania nowej lini (e tym miejscu zawsze z reszty tworzy ostatnie pole - upeniæ siê, ¿e nie pominiêto czegoœ) itp."
                _csvFieldBuilder.BuildNewField(0);

                _csvLineConsumer.Consume(_csvFieldBuilder.CurrentCsvLine);
                _csvFieldBuilder.InitNewLine();
            }

            return _csvFieldBuilder.CreatedLinesCount;
        }
    }
}