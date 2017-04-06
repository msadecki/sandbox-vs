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

        public int CreatedLinesCount { get; private set; }

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
        }

        public void InitNewLine()
        {
            CurrentCsvLine = _csvLineFactory.Create(_csvFieldBuilderConfiguration.CsvLineFieldsCapacity);
            CreatedLinesCount++;

            InitNewField();
        }

        public void InitNewField()
        {
            _rawFieldBuilder.Clear();

            ClearState();
        }

        public bool Append(char character)
        {
            if (character == _csvConfiguration.QuotationMark)
            {
                if (_state.IsDelimiterOrEndOfLineRequired)
                {
                    throw new CsvInvalidFormatException($"Only delimiter or new line is expected. {GetPosition()}");
                }

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
            }
            else if (_state.CurrentCharacter == _csvConfiguration.QuotationMark)
            {
                _rawFieldBuilder.Remove(RawFieldBuilderLength - _csvConfiguration.QuotationMarkLenght, _csvConfiguration.QuotationMarkLenght);

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
                    throw new CsvInvalidFormatException($"Only delimiter or new line is expected. {GetPosition()}");
                }
                else if (_state.EndOfLineLengthToMatch != _csvConfiguration.EndOfLineLength)
                {
                    _state.EndOfLineLengthToMatch = _csvConfiguration.EndOfLineLength;
                }
            }
        }

        public void BuildNewFieldAfterDelimiter()
        {
            BuildNewField(_csvConfiguration.DelimiterLenght);
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

        private string GetPosition()
        {
            return GetPosition(
                CreatedLinesCount,
                CurrentCsvLine.Fields.Count,
                RawFieldBuilderLength);
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