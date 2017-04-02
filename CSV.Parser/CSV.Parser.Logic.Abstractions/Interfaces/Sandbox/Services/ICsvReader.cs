using System.Collections.Generic;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services
{
    public interface ICsvReader
    {
        IList<string[]> Read(string filePath);
    }
}
