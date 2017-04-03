using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc4180
    /// </summary>
    public class CsvConfiguration : ICsvConfiguration
    {
        /// <summary>
        /// ','
        /// </summary>
        public char Delimiter { get; } = ',';

        /// <summary>
        /// CRLF
        /// "\r\n"
        /// Environment.NewLine 
        /// </summary>
        public string EndOfLine { get; } = "\r\n";
    }
}
