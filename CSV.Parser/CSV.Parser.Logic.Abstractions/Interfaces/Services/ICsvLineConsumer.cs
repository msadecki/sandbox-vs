using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvLineConsumer
    {
        void Consume(ICsvLine csvLine);
    }
}