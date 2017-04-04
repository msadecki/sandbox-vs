using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;
using CSV.Parser.Logic.Services;

namespace CSV.Parser.Logic.Factories
{
    public class BufferableReaderFactory : IBufferableReaderFactory
    {
        private readonly IBufferableReaderConfiguration _bufferableReaderConfiguration;

        public BufferableReaderFactory(
            IBufferableReaderConfiguration bufferableReaderConfiguration)
        {
            _bufferableReaderConfiguration = bufferableReaderConfiguration;
        }

        public IBufferableReader Create(TextReader textReader)
        {
            return new BufferableReader(
                _bufferableReaderConfiguration,
                textReader);
        }
    }
}