using System.Collections.Generic;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvWriter
    {
        void Write(string filePath, bool append, IEnumerable<string[]> lines);
    }
}