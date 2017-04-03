using System.Collections.Generic;
using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvLine : ICsvLine
    {
        public IList<ICsvField> Fields { get; set; }
    }
}