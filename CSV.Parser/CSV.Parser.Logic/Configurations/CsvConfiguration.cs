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
        public char Delimiter => ',';

        /// <summary>
        /// '"'
        /// </summary>
        public char QuotationMark => '"';

        /// <summary>
        /// CRLF
        /// "\r\n"
        /// Environment.NewLine 
        /// </summary>
        public string EndOfLine => "\r\n";

        public int DelimiterLenght => 1;

        public int QuotationMarkLenght => 1;

        public int EndOfLineLength => EndOfLine.Length;
    }
}