using System.Collections.Generic;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Models
{
    public interface ICsvLine
    {
        IList<ICsvField> Fields { get; }
    }
}