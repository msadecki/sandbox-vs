namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface IBufferableReader
    {
        char[] Buffer { get; }

        int BufferLength { get; }

        bool ReadBuffer();
    }
}