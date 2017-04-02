namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface IOutputConfiguration
    {
        string Delimiter { get; }

        string EndOfLine { get; }
    }
}