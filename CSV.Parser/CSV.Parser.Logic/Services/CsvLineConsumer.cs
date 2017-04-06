using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public sealed class CsvLineConsumer : ICsvLineConsumer
    {
        private readonly TextWriter _textWriter;
        private readonly IOuputLineFactory _ouputLineFactory;

        public CsvLineConsumer(
            TextWriter textWriter,
            IOuputLineFactory ouputLineFactory)
        {
            _textWriter = textWriter;
            _ouputLineFactory = ouputLineFactory;
        }

        public void Consume(ICsvLine csvLine)
        {
            _textWriter.Write(_ouputLineFactory.Create(csvLine));
        }
    }
}