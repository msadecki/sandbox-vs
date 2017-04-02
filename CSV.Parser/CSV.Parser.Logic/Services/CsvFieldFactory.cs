using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Services
{
    public class CsvFieldFactory : ICsvFieldFactory
    {
        public CsvField Create(string fieldContent)
        {
            return new CsvField
            {
                Content = fieldContent
            };
        }
    }
}