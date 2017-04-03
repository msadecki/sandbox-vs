using System;
using System.IO;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    /// <summary>
    /// TODO: Clean that comment and all others in solution.
    /// 
    /// http://stackoverflow.com/questions/9266747/writing-and-polishing-a-csv-parser
    /// Careful, this doesn't work for fields with embedded newlines, fields containing the delimiter as a literal, and quote-enclosed fields. – Corbin March Oct 17 '12 at 14:00
    /// 
    /// http://www.dreamincode.net/forums/topic/378128-how-to-import-csv-file-data-into-datagridview-in-load-function/
    /// TextFieldParser is slow because it actually follows RFC 4180 with regards to how to correctly parse CSV files, unlike String.Split() which assumes that there are no embedded separators within quotes, or any escaped characters. 
    /// </summary>
    public class CsvStreamReader : ICsvStreamReader
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ICsvLineFactory _csvLineFactory;
        private readonly ICsvFieldFactory _csvFieldFactory;
        private readonly IBufferableReaderFactory _bufferableReaderFactory;

        public CsvStreamReader(
            ICsvConfiguration csvConfiguration,
            ICsvLineFactory csvLineFactory,
            ICsvFieldFactory csvFieldFactory,
            IBufferableReaderFactory bufferableReaderFactory)
        {
            _csvConfiguration = csvConfiguration;
            _csvLineFactory = csvLineFactory;
            _csvFieldFactory = csvFieldFactory;
            _bufferableReaderFactory = bufferableReaderFactory;
        }

        public int Read(TextReader textReader, ICsvLineConsumer csvLineConsumer)
        {
            // TODO: Implement in clean way
            // TODO: Implement rfc4180 standard in CsvStreamReader & CsvLineFactory (it may be removed or refactored)
            // TODO/REMARKS: Empty lines should not be ignored
            var counter = 0;

            var bufferableReader = _bufferableReaderFactory.Create(textReader);

            var rawLineBuilder = new StringBuilder(1024);
            var endOfLineLengthToMatch = _csvConfiguration.EndOfLine.Length;
            var isEndOfLineSeekEnabled = true;
            var isDelimiterSeekEnabled = true;

            while (bufferableReader.ReadBuffer())
            {
                for (var pos = 0; pos < bufferableReader.BufferLength; pos++)
                {
                    var currentCharacter = bufferableReader.Buffer[pos];

                    rawLineBuilder.Append(currentCharacter);

                    if (isEndOfLineSeekEnabled
                        && endOfLineLengthToMatch > 0
                        && currentCharacter == _csvConfiguration.EndOfLine[_csvConfiguration.EndOfLine.Length - endOfLineLengthToMatch])
                    {
                        endOfLineLengthToMatch--;
                    }

                    if (endOfLineLengthToMatch == 0)
                    {
                        var rawLine = rawLineBuilder.ToString(0, rawLineBuilder.Length - _csvConfiguration.EndOfLine.Length);

                        // TODO: DRY (try to move to CsvFieldParser)
                        var csvLine = _csvLineFactory.Create(128);
                        Array.ForEach(
                            rawLine.Split(_csvConfiguration.Delimiter),
                            fieldContent => csvLine.Fields.Add(_csvFieldFactory.Create(fieldContent)));
                        csvLineConsumer.Consume(csvLine);
                        counter++;

                        rawLineBuilder.Clear();
                        endOfLineLengthToMatch = _csvConfiguration.EndOfLine.Length;
                        isEndOfLineSeekEnabled = true;
                    }
                }
            }

            if (rawLineBuilder.Length > 0)
            {
                var rawLine = rawLineBuilder.ToString();

                var csvLine = _csvLineFactory.Create(128);
                Array.ForEach(
                    rawLine.Split(_csvConfiguration.Delimiter),
                    fieldContent => csvLine.Fields.Add(_csvFieldFactory.Create(fieldContent)));
                csvLineConsumer.Consume(csvLine);
                counter++;

                rawLineBuilder.Clear();
                endOfLineLengthToMatch = _csvConfiguration.EndOfLine.Length;
                isEndOfLineSeekEnabled = true;
            }

            return counter;
        }
    }
}