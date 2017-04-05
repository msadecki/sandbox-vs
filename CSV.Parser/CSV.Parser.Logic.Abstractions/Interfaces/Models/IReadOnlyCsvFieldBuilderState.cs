namespace CSV.Parser.Logic.Abstractions.Interfaces.Models
{
    public interface IReadOnlyCsvFieldBuilderState
    {
        int RawFieldBuilderLength { get; }

        char CurrentCharacter { get; }

        ICsvLine CurrentCsvLine { get; }

        int CreatedLinesCount { get; }

        int EndOfLineLengthToMatch { get; }

        bool IsDelimiterSeekEnabled { get; }

        bool IsEndOfLineSeekEnabled { get; }

        bool IsQuotationMarkStartFieldSeekEnabled { get; }

        bool IsQuotationMarkEndFieldSeekEnabled { get; }
    }
}