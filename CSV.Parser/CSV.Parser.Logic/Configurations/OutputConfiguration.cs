using System;
using CSV.Parser.Logic.Abstractions.Enums;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class OutputConfiguration  : IOutputConfiguration
    {
        public OutputTarget OutputTarget => OutputTarget.Console;

        public string Delimiter => "|";

        public string EndOfLine => Environment.NewLine;
    }
}