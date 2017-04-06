namespace CSV.Parser.Logic.Abstractions.Interfaces.Models
{
    public interface ICsvFieldBuilderState
    {
        int? FieldsCount { get; set; }

        char? AppendantCharacter { get; set; }

        char? CurrentCharacter { get; set; }

        int EndOfLineLengthToMatch { get; set; }

        bool IsDelimiterSeekEnabled { get; set; }

        bool IsEndOfLineSeekEnabled { get; set; }

        bool IsQuotationMarkFirstInField { get; set; }

        bool IsDelimiterOrEndOfLineRequired { get; set; }
    }
}