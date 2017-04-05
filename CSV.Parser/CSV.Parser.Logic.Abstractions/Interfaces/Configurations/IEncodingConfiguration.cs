using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface IEncodingConfiguration
    {
        Encoding DefaultFileInputEncoding { get; }

        Encoding ConsoleOutputEncoding { get; }

        Encoding FileOutputEncoding { get; }
    }
}