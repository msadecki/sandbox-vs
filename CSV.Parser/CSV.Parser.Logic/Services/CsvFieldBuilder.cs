using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Exceptions;

namespace CSV.Parser.Logic.Services
{
    public class CsvFieldBuilder : ICsvFieldBuilder
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;
        private readonly ICsvFieldBuilderState _state;
        private readonly StringBuilder _rawFieldBuilder;

        public bool IsDelimiterMatched => _state.IsDelimiterSeekEnabled && _state.CurrentCharacter == _csvConfiguration.Delimiter;

        public bool IsEndOfLineMatched => _state.EndOfLineLengthToMatch == 0;

        public int RawFieldBuilderLength => _rawFieldBuilder.Length;

        public ICsvLine CurrentCsvLine { get; private set; }

        public int CurrentCsvLineIndex { get; private set; }

        public CsvFieldBuilder(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory,
            ICsvFieldBuilderState state)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;
            _state = state;
            _rawFieldBuilder = new StringBuilder(csvFieldBuilderConfiguration.RawFieldBuilderCapacity);

            CurrentCsvLine = _csvLineFactory.Create(_csvFieldBuilderConfiguration.CsvLineFieldsCapacity);

            ClearState();
            _state.FieldsCount = null;
            _state.AppendantCharacter = null;
        }

        public void InitNewLine()
        {
            if (_state.FieldsCount == null)
            {
                _state.FieldsCount = CurrentCsvLine.Fields.Count;
            }
            else if (_state.FieldsCount != CurrentCsvLine.Fields.Count)
            {
                throw CreateCsvInvalidFormatException("Each line should contain the same number of fields throughout the file.");
            }

            CurrentCsvLine = _csvLineFactory.Create(_csvFieldBuilderConfiguration.CsvLineFieldsCapacity);
            CurrentCsvLineIndex++;

            InitNewField();
        }

        public void InitNewField()
        {
            _rawFieldBuilder.Clear();

            ClearState();
        }

        public bool Append(char character)
        {
            _state.AppendantCharacter = character;

            if (character == _csvConfiguration.QuotationMark)
            {
                if (_state.IsQuotationMarkFirstInField)
                {
                    if (_state.CurrentCharacter == _csvConfiguration.QuotationMark)
                    {
                        _state.CurrentCharacter = null;

                        return false;
                    }
                }
                else if (RawFieldBuilderLength == 0)
                {
                    _state.IsDelimiterSeekEnabled = false;
                    _state.IsEndOfLineSeekEnabled = false;
                    _state.IsQuotationMarkFirstInField = true;

                    return false;
                }
                else
                {
                    throw CreateCsvInvalidFormatException("Quotation mark is not allowed inside a field that is not enclosed with quotation mark.");
                }
            }
            else if (_state.CurrentCharacter == _csvConfiguration.QuotationMark)
            {
                _rawFieldBuilder.Remove(RawFieldBuilderLength - _csvConfiguration.QuotationMarkLength, _csvConfiguration.QuotationMarkLength);

                _state.IsDelimiterSeekEnabled = true;
                _state.IsEndOfLineSeekEnabled = true;
                _state.IsDelimiterOrEndOfLineRequired = true;
            }

            _state.CurrentCharacter = character;

            _rawFieldBuilder.Append(character);

            return true;
        }

        public void EnsureEndOfLineLengthToMatch()
        {
            if (_state.IsEndOfLineSeekEnabled && _state.EndOfLineLengthToMatch > 0)
            {
                if (_state.CurrentCharacter == _csvConfiguration.EndOfLine[_csvConfiguration.EndOfLineLength - _state.EndOfLineLengthToMatch])
                {
                    _state.EndOfLineLengthToMatch--;
                }
                else if (_state.IsDelimiterOrEndOfLineRequired)
                {
                    throw CreateCsvInvalidFormatException("Only delimiter or new line is allowed.");
                }
                else if (_state.EndOfLineLengthToMatch != _csvConfiguration.EndOfLineLength)
                {
                    _state.EndOfLineLengthToMatch = _csvConfiguration.EndOfLineLength;
                }
            }
        }

        public void EnsureLastAppendantCharacterIsNotDelimiter()
        {
            if (_state.AppendantCharacter == _csvConfiguration.Delimiter)
            {
                throw CreateCsvInvalidFormatException("The last character in the last line must not be a field delimiter not followed by new line separator.");
            }
        }

        public void BuildNewFieldAfterDelimiter()
        {
            BuildNewField(_csvConfiguration.DelimiterLength);
        }

        public void BuildNewFieldAfterEndOfLine()
        {
            BuildNewField(_csvConfiguration.EndOfLineLength);
        }

        public void BuildNewFieldFromTail()
        {
            BuildNewField(0);
        }

        private void BuildNewField(int charactersToIgnoreCount)
        {
            var fieldContent = _rawFieldBuilder.ToString(0, RawFieldBuilderLength - charactersToIgnoreCount);

            CurrentCsvLine.Fields.Add(_csvFieldFactory.Create(fieldContent));
        }

        private void ClearState()
        {
            _state.CurrentCharacter = null;
            _state.EndOfLineLengthToMatch = _csvConfiguration.EndOfLineLength;
            _state.IsDelimiterSeekEnabled = true;
            _state.IsEndOfLineSeekEnabled = true;
            _state.IsQuotationMarkFirstInField = false;
            _state.IsDelimiterOrEndOfLineRequired = false;
        }

        private CsvInvalidFormatException CreateCsvInvalidFormatException(string message)
        {
            return new CsvInvalidFormatException($"{message} {GetPosition()}");
        }

        private string GetPosition()
        {
            return GetPosition(
                CurrentCsvLineIndex + 1,
                CurrentCsvLine.Fields.Count,
                RawFieldBuilderLength);
        }

        private static string GetPosition(
            int line,
            int field,
            int fieldContentLength)
        {
            return $"{{ Line: {line}, Field: {field}, FieldContentLength: {fieldContentLength} }}";
        }
    }
}