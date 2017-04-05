using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvFieldBuilder : ICsvFieldBuilder
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;
        private readonly ICsvFieldBuilderState _state;

        public IReadOnlyCsvFieldBuilderState State => _state.ReadOnlyState;

        public CsvFieldBuilder(
            ICsvConfiguration csvConfiguration,
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory,
            ICsvFieldBuilderStateFactory csvFieldBuilderStateFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;

            _state = csvFieldBuilderStateFactory.Create(csvFieldBuilderConfiguration);

            InitNewLine();
        }

        public void Append(char currentCharacter)
        {
            _state.CurrentCharacter = currentCharacter;

            _state.RawFieldBuilder.Append(currentCharacter);
        }

        public void InitNewLine()
        {
            _state.CurrentCsvLine = _csvLineFactory.Create(_csvFieldBuilderConfiguration.CsvLineFieldsCapacity);
            _state.CreatedLinesCount++;

            InitNewField();
        }

        public void InitNewField()
        {
            _state.RawFieldBuilder.Clear();
            _state.EndOfLineLengthToMatch = _csvConfiguration.EndOfLineLength;
            _state.IsDelimiterSeekEnabled = true;
            _state.IsEndOfLineSeekEnabled = true;
            _state.IsQuotationMarkStartFieldSeekEnabled = true;
            _state.IsQuotationMarkEndFieldSeekEnabled = false;
        }

        public bool IsDelimiterMatched()
        {
            return _state.IsDelimiterSeekEnabled && _state.CurrentCharacter == _csvConfiguration.Delimiter;
        }

        public bool IsEndOfLineMatched()
        {
            return _state.EndOfLineLengthToMatch == 0;
        }

        public void EnsureEndOfLineLengthToMatch()
        {
            if (_state.IsEndOfLineSeekEnabled && _state.EndOfLineLengthToMatch > 0)
            {
                if (_state.CurrentCharacter == _csvConfiguration.EndOfLine[_csvConfiguration.EndOfLineLength - _state.EndOfLineLengthToMatch])
                {
                    _state.EndOfLineLengthToMatch--;
                }
                else if (_state.EndOfLineLengthToMatch != _csvConfiguration.EndOfLineLength)
                {
                    _state.EndOfLineLengthToMatch = _csvConfiguration.EndOfLineLength;
                }
            }
        }

        public void BuildNewField(int charactersToIgnoreCount)
        {
            var fieldContent = _state.RawFieldBuilder.ToString(0, _state.RawFieldBuilder.Length - charactersToIgnoreCount);

            _state.CurrentCsvLine.Fields.Add(_csvFieldFactory.Create(fieldContent));
        }
    }
}