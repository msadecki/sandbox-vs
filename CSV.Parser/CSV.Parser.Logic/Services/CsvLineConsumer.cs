using System.IO;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvLineConsumer : ICsvLineConsumer
    {
        private readonly IOutputConfiguration _outputConfiguration;
        private readonly TextWriter _textWriter;

        public CsvLineConsumer(
            IOutputConfiguration outputConfiguration,
            ITextWriterFactory textWriterFactory)
        {
            _outputConfiguration = outputConfiguration;
            _textWriter = textWriterFactory.Create();
        }

        public void Consume(ICsvLine csvLine)
        {
            _textWriter.Write(CreateOuputLine(csvLine));
        }

        private StringBuilder CreateOuputLine(ICsvLine csvLine)
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

            return outputLine;
        }

        public void Dispose()
        {
            _textWriter?.Dispose();
        }
    }
}