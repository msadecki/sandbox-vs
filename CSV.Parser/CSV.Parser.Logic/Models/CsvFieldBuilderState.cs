using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvFieldBuilderState : ICsvFieldBuilderState, IReadOnlyCsvFieldBuilderState
    {
        // TODO//Verify done: Move to Model CsvFieldBuilderState (mutable and return readonly) - do not add methods to it (create methods here)
        public IReadOnlyCsvFieldBuilderState ReadOnlyState => this;

        public StringBuilder RawFieldBuilder { get; set; }

        public int RawFieldBuilderLength => RawFieldBuilder.Length;

        public char CurrentCharacter { get; set; }

        public ICsvLine CurrentCsvLine { get; set; }

        public int CreatedLinesCount { get; set; }

        public int EndOfLineLengthToMatch { get; set; }

        public bool IsDelimiterSeekEnabled { get; set; }

        public bool IsEndOfLineSeekEnabled { get; set; }

        public bool IsQuotationMarkStartFieldSeekEnabled { get; set; }

        public bool IsQuotationMarkEndFieldSeekEnabled { get; set; }
    }
}