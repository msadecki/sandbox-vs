using System.IO;
using CSV.Parser.Logic.Abstractions.Enums;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextWriterFactory
    {
        TextWriter Create(
            OutputTarget outputTarget,
            IEncodingConfiguration encodingConfiguration);
    }
}