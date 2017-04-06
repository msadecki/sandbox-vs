using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class EncodingConfiguration : IEncodingConfiguration
    {
        public Encoding DefaultFileInputEncoding { get; } = Encoding.UTF8;

        public Encoding ConsoleOutputEncoding { get; } = Encoding.GetEncoding(852);

        public Encoding FileOutputEncoding { get; } = Encoding.UTF8;
    }
}