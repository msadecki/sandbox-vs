using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ICsvFieldBuilderFactory
    {
        ICsvFieldBuilder Create(ICsvConfiguration csvConfiguration);
    }
}