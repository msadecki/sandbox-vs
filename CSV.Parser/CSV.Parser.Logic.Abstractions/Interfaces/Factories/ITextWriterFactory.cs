using System.IO;
using CSV.Parser.Logic.Abstractions.Enums;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextWriterFactory
    {
        TextWriter Create(OutputTarget outputTarget);
    }
}