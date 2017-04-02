using System.IO;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ITextWriterFactory
    {
        TextWriter Create();
    }
}