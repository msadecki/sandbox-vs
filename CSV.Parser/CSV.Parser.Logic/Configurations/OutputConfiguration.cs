using System;
using CSV.Parser.Logic.Abstractions.Enums;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class OutputConfiguration  : IOutputConfiguration
    {
        public OutputTarget OutputTarget { get; } = OutputTarget.Console;

        public string Delimiter { get; } = "|";

        public string EndOfLine { get; } = Environment.NewLine;
    }
}