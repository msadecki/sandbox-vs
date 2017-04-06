using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvFieldBuilderState : ICsvFieldBuilderState
    {
        public char? CurrentCharacter { get; set; }

        public int EndOfLineLengthToMatch { get; set; }

        public bool IsDelimiterSeekEnabled { get; set; }

        public bool IsEndOfLineSeekEnabled { get; set; }

        public bool IsQuotationMarkFirstInField { get; set; }

        public bool IsDelimiterOrEndOfLineRequired { get; set; }
    }
}