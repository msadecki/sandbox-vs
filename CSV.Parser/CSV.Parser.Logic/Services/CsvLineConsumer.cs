using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvLineConsumer : ICsvLineConsumer
    {
        private readonly TextWriter _textWriter;
        private readonly IOutputLineFactory _outputLineFactory;

        public CsvLineConsumer(
            TextWriter textWriter,
            IOutputLineFactory outputLineFactory)
        {
            _textWriter = textWriter;
            _outputLineFactory = outputLineFactory;
        }

        public void Consume(ICsvLine csvLine)
        {
            _textWriter.Write(_outputLineFactory.Create(csvLine));
        }
    }
}