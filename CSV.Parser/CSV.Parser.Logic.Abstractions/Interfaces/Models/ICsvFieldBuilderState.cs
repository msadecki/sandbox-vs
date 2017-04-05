using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Models
{
    public interface ICsvFieldBuilderState
    {
        IReadOnlyCsvFieldBuilderState ReadOnlyState { get; }

        StringBuilder RawFieldBuilder { get; }

        int RawFieldBuilderLength { get; }

        char CurrentCharacter { get; set; }

        ICsvLine CurrentCsvLine { get; set; }

        int CreatedLinesCount { get; set; }

        int EndOfLineLengthToMatch { get; set; }

        bool IsDelimiterSeekEnabled { get; set; }

        bool IsEndOfLineSeekEnabled { get; set; }

        bool IsQuotationMarkStartFieldSeekEnabled { get; set; }

        bool IsQuotationMarkEndFieldSeekEnabled { get; set; }
    }
}