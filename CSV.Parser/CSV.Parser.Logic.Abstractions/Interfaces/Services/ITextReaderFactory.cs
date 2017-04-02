using System.IO;
using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ITextReaderFactory
    {
        TextReader Create(string filePath);

        /// <summary>
        /// TODO: Method is to be deleted in final implementation
        /// </summary>
        Encoding GetCurrentEncoding(string filePath);
    }
}