using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;
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