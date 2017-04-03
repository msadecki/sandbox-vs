using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class EncodingConfiguration : IEncodingConfiguration
    {
        public Encoding DefaultInputEncoding { get; } = Encoding.UTF8;

        /// <summary>
        /// Encoding examples without BOM:
        /// Encoding.GetEncoding(852) - It works with polish characters in Console.Output
        /// Encoding.GetEncoding("Windows-1250")
        /// Encoding.ASCII
        /// new UTF8Encoding(false, true)
        /// new UnicodeEncoding(false, false, true)
        /// </summary>
        public Encoding ConsoleOutputEncoding { get; } = Encoding.GetEncoding(852);

        public Encoding FileOutputEncoding { get; } = Encoding.UTF8;
    }
}