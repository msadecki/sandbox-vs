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
            IOutputLineFactory outputLineFactory)
        {
            return new CsvLineConsumer(
                textWriter,
                outputLineFactory);
        }
    }
}