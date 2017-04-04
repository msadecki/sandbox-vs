using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvFieldBuilder : ICsvFieldBuilder
    {
        private readonly ICsvFieldBuilderConfiguration _csvFieldBuilderConfiguration;
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;
        private char _currentCharacter;

        public ICsvLine CurrentCsvLine { get; private set; }

        public int RawFieldBuilderLength => RawFieldBuilder.Length;

        public int CreatedLinesCount { get; private set; } = -1;

        private StringBuilder RawFieldBuilder { get; }

        private int EndOfLineLengthToMatch { get; set; }

        private bool IsDelimiterSeekEnabled { get; set; }

        private bool IsEndOfLineSeekEnabled { get; set; }

        private int InitialEndOfLineLengthToMatch => _csvConfiguration.EndOfLine.Length;

        public CsvFieldBuilder(
            ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration,
            ICsvConfiguration csvConfiguration,
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory)
        {
            _csvFieldBuilderConfiguration = csvFieldBuilderConfiguration;
            _csvConfiguration = csvConfiguration;
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;

            RawFieldBuilder = new StringBuilder(csvFieldBuilderConfiguration.RawFieldBuilderCapacity);

            InitNewLine();
        }

        public void Append(char currentCharacter)
        {
            _currentCharacter = currentCharacter;

            RawFieldBuilder.Append(currentCharacter);
        }

        public void InitNewLine()
        {
            CurrentCsvLine = _csvLineFactory.Create(_csvFieldBuilderConfiguration.CsvLineFieldsCapacity);
            CreatedLinesCount++;

            InitNewField();
        }

        public void InitNewField()
        {
            RawFieldBuilder.Clear();
            EndOfLineLengthToMatch = InitialEndOfLineLengthToMatch;
            IsDelimiterSeekEnabled = true;
            IsEndOfLineSeekEnabled = true;
        }

        public bool IsDelimiterMatched()
        {
            return IsDelimiterSeekEnabled && _currentCharacter == _csvConfiguration.Delimiter;
        }

        public bool IsEndOfLineMatched()
        {
            return EndOfLineLengthToMatch == 0;
        }

        public void EnsureEndOfLineLengthToMatch()
        {
            if (IsEndOfLineSeekEnabled && EndOfLineLengthToMatch > 0)
            {
                if (_currentCharacter == _csvConfiguration.EndOfLine[InitialEndOfLineLengthToMatch - EndOfLineLengthToMatch])
                {
                    EndOfLineLengthToMatch--;
                }
                else if (EndOfLineLengthToMatch != InitialEndOfLineLengthToMatch)
                {
                    EndOfLineLengthToMatch = InitialEndOfLineLengthToMatch;
                }
            }
        }

        public void BuildNewField(int charactersToIgnoreCount)
        {
            var fieldContent = RawFieldBuilder.ToString(0, RawFieldBuilder.Length - charactersToIgnoreCount);

            CurrentCsvLine.Fields.Add(_csvFieldFactory.Create(fieldContent));
        }
    }
}
