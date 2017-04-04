using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvFieldBuilder
    {
        ICsvLine CurrentCsvLine { get; }

        int RawFieldBuilderLength { get; }

        int CreatedLinesCount { get; }

        void Append(char currentCharacter);

        void InitNewLine();

        void InitNewField();

        bool IsDelimiterMatched();

        bool IsEndOfLineMatched();

        void EnsureEndOfLineLengthToMatch();

        void BuildNewField(int charactersToIgnoreCount);
    }
}