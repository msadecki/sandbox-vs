using CSV.Parser.Logic.Abstractions.Models;

namespace CSV.Parser.Logic.Models
{
    public class CsvField : ICsvField
    {
        public string Content { get; set; }
    }
}
