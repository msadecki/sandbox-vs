using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class CsvLineConsumerFactory : ICsvLineConsumerFactory
    {
        public ICsvLineConsumer Create(
            TextWriter textWriter,
            IOuputLineFactory ouputLineFactory)
        {
            return new CsvLineConsumer(
                textWriter,
                ouputLineFactory);
        }
    }
}