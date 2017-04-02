namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface ICsvConfiguration
    {
        char Delimiter { get; }

        string EndOfLine { get; }
    }
}
