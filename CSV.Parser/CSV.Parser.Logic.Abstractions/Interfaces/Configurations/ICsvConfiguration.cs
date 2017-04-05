namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface ICsvConfiguration
    {
        char Delimiter { get; }

        char QuotationMark { get; }

        string EndOfLine { get; }

        int DelimiterLenght { get; }

        int EndOfLineLength { get; }
    }
}