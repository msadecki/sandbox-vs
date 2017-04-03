using System.Collections.Generic;

namespace CSV.Parser.Logic.Abstractions.Models
{
    public interface ICsvLine
    {
        IList<ICsvField> Fields { get; }
    }
}