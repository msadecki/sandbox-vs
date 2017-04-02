using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Configurations;

namespace CSV.Parser.Logic.Sandbox.Configurations
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
        public string EndOfLine { get; } = @"\r\n";

        public Encoding DefaultInputEncoding { get; } = Encoding.UTF8;

        public Encoding OutputEncoding { get; } = Encoding.UTF8;
    }
}
