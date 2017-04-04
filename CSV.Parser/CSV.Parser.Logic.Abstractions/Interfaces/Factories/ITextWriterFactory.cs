using System.IO;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Factories
{
    public interface ITextWriterFactory
    {
        TextWriter Create();
    }
}