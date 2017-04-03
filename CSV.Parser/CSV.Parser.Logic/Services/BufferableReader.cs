using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class BufferableReader : IBufferableReader
    {
        public char[] Buffer { get; private set; }

        public int BufferLength { get; private set; }

        private int BufferSize => Buffer.Length;

        private TextReader TextReader { get; set; }

        private bool IsEndOfTextReaderReached { get; set; }

        public BufferableReader(
            IBufferableReaderConfiguration bufferableReaderConfiguration,
            TextReader textReader)
        {
            Buffer = new char[bufferableReaderConfiguration.ReaderBufferSize];

            TextReader = textReader;
        }

        public bool ReadBuffer()
        {
            if (IsEndOfTextReaderReached)
            {
                return false;
            }

            BufferLength = TextReader.Read(Buffer, 0, BufferSize);

            if (BufferLength > 0)
            {
                return true;
            }

            IsEndOfTextReaderReached = true;

            TextReader = null;

            Buffer = null;

            return false;
        }
    }
}