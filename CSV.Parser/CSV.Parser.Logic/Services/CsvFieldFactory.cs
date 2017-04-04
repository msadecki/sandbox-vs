using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Models;

namespace CSV.Parser.Logic.Services
{
    public class CsvFieldFactory : ICsvFieldFactory
    {
        public ICsvField Create(string fieldContent)
        {
            return new CsvField
            {
                Content = fieldContent
            };
        }
    }
}