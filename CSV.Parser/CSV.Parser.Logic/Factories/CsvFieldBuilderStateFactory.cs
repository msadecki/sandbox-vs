using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Models;

namespace CSV.Parser.Logic.Factories
{
    public class CsvFieldBuilderStateFactory : ICsvFieldBuilderStateFactory
    {
        public ICsvFieldBuilderState Create(ICsvFieldBuilderConfiguration csvFieldBuilderConfiguration)
        {
            return new CsvFieldBuilderState
            {
                RawFieldBuilder = new StringBuilder(csvFieldBuilderConfiguration.RawFieldBuilderCapacity),
                CreatedLinesCount = -1
            };
        }
    }
}
