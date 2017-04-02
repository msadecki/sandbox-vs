using System.Collections.Generic;
using System.IO;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services;

namespace CSV.Parser.Logic.Sandbox.Services
{
    /// <summary>
    /// http://stackoverflow.com/questions/9266747/writing-and-polishing-a-csv-parser
    /// Careful, this doesn't work for fields with embedded newlines, fields containing the delimiter as a literal, and quote-enclosed fields. – Corbin March Oct 17 '12 at 14:00
    /// 
    /// http://www.dreamincode.net/forums/topic/378128-how-to-import-csv-file-data-into-datagridview-in-load-function/
    /// TextFieldParser is slow because it actually follows RFC 4180 with regards to how to correctly parse CSV files, unlike String.Split() which assumes that there are no embedded separators within quotes, or any escaped characters. 
    /// </summary>
    public class CsvReader : ICsvReader
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly IFileEncodingResolver _fileEncodingResolver;

        public CsvReader(
            ICsvConfiguration csvConfiguration,
            IFileEncodingResolver fileEncodingResolver)
        {
            _csvConfiguration = csvConfiguration;
            _fileEncodingResolver = fileEncodingResolver;
        }

        public IList<string[]> Read(string filePath)
        {
            var lines = new List<string[]>();

            // TODO: IFileEncodingResolver is to delete
            //var encoding = _fileEncodingResolver.GetEncoding(filePath, _csvConfiguration.DefaultInputEncoding);
            var encoding = _csvConfiguration.DefaultInputEncoding;

            using (var reader = new StreamReader(filePath, encoding, true))
            {
                string rawLine;
                while ((rawLine = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(rawLine))
                    {
                        lines.Add(rawLine.Split(_csvConfiguration.Delimiter));
                    }
                }
            }

            return lines;
        }
    }
}