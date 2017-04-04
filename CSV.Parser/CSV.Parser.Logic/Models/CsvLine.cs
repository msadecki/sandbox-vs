using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvLine : ICsvLine
    {
        public IList<ICsvField> Fields { get; set; }
    }
}