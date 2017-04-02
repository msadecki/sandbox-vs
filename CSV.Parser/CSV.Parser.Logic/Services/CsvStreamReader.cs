using System.IO;
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
        private readonly ICsvLineFactory _csvLineFactory;

        public CsvStreamReader(
            ICsvLineFactory csvLineFactory)
        {
            _csvLineFactory = csvLineFactory;
        }

        public int Read(TextReader textReader, ICsvLineConsumer csvLineConsumer)
        {
            var counter = 0;

            string rawLine;
            while ((rawLine = textReader.ReadLine()) != null)
            {
                // TODO/REMARKS: Empty lines should not be ignored
                var csvLine = _csvLineFactory.Create(rawLine);

                csvLineConsumer.Consume(csvLine);

                counter++;
            }

            return counter;
        }
    }
}