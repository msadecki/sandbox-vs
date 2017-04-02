using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services
{
    public interface IFileEncodingResolver
    {
        Encoding GetEncoding(string filename, Encoding defaultEncoding);
    }
}