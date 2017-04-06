using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Factories
{
    public class OutputLineFactory : IOutputLineFactory
    {
        private readonly IOutputConfiguration _outputConfiguration;

        public OutputLineFactory(
            IOutputConfiguration outputConfiguration)
        {
            _outputConfiguration = outputConfiguration;
        }

        public string Create(ICsvLine csvLine)
        {
            var outputLine = new StringBuilder();

            foreach (var field in csvLine.Fields)
            {
                outputLine.Append(_outputConfiguration.Delimiter);
                outputLine.Append(field.Content);
            }

            if (outputLine.Length > 0)
            {
                outputLine.Append(_outputConfiguration.Delimiter);
            }

            outputLine.Append(_outputConfiguration.EndOfLine);

            return outputLine.ToString();
        }
    }
}