using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Configurations
{
    public interface ICsvConfiguration
    {
        char Delimiter { get; }

        string EndOfLine { get; }

        Encoding DefaultInputEncoding { get; }

        Encoding OutputEncoding { get; }
    }
}
