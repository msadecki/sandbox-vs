using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class BufferableReaderConfiguration : IBufferableReaderConfiguration
    {
        public int ReaderBufferSize { get; } = 1024;
    }
}