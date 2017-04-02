using System;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvLineConsumer : IDisposable
    {
        void Consume(CsvLine csvLine);
    }
}