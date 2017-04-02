using System.IO;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvStreamReader
    {
        int Read(TextReader textReader, ICsvLineConsumer csvLineConsumer);
    }
}