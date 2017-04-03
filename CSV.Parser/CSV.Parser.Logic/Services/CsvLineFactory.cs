using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;
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