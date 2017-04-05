using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Models;

namespace CSV.Parser.Logic.Factories
{
    public class CsvFieldBuilderStateFactory : ICsvFieldBuilderStateFactory
    {
        public ICsvFieldBuilderState Create()
        {
            return new CsvFieldBuilderState();
        }
    }
}