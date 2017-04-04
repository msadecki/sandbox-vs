using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvField : ICsvField
    {
        public string Content { get; set; }
    }
}
