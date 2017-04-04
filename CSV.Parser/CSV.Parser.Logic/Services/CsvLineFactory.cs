using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Models;

namespace CSV.Parser.Logic.Services
{
    public class CsvLineFactory : ICsvLineFactory
    {
        public ICsvLine Create(int capacity)
        {
            return new CsvLine
            {
                Fields = new List<ICsvField>(capacity)
            };
        }
    }
}