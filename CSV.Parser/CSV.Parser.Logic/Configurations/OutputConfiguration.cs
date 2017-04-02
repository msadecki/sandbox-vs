using System;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class OutputConfiguration  : IOutputConfiguration
    {
        public string Delimiter { get; } = "|";

        public string EndOfLine { get; } = Environment.NewLine;
    }
}