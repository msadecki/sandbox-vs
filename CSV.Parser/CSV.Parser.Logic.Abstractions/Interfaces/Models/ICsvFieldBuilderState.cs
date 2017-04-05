namespace CSV.Parser.Logic.Abstractions.Interfaces.Models
{
    public interface ICsvFieldBuilderState
    {
        char CurrentCharacter { get; set; }

        int EndOfLineLengthToMatch { get; set; }

        bool IsDelimiterSeekEnabled { get; set; }

        bool IsEndOfLineSeekEnabled { get; set; }

        bool IsQuotationMarkStartFieldSeekEnabled { get; set; }

        bool IsQuotationMarkEndFieldSeekEnabled { get; set; }
    }
}