using CSV.Parser.Logic.Abstractions.Enums;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface IOutputConfiguration
    {
        OutputTarget OutputTarget { get; }

        string Delimiter { get; }

        string EndOfLine { get; }
    }
}