using System.Collections.Generic;
using System.Text;
using CSV.Parser.Logic.Abstractions.Enums;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Abstractions.Interfaces.Models;
using CSV.Parser.Logic.Abstractions.Interfaces.Services;

namespace CSV.Parser.Logic.Services
{
    public class CsvWriter : ICsvWriter
    {
        private readonly ICsvConfiguration _csvConfiguration;
        private readonly ITextWriterFactory _textWriterFactory;

        public CsvWriter(
            ICsvConfiguration csvConfiguration,
            ITextWriterFactory textWriterFactory)
        {
            _csvConfiguration = csvConfiguration;
            _textWriterFactory = textWriterFactory;
        }

        // TODO/REMARKS: This class/method is not part of coding exercise but it can be usefull to generate some test csv files.
        public void Write(IEnumerable<ICsvLine> csvLines)
        {
            using (var textWriter = _textWriterFactory.Create(OutputTarget.File))
            {
                var oneQuotationMark = _csvConfiguration.QuotationMark.ToString();
                var twoQuotationMarks = string.Concat(_csvConfiguration.QuotationMark, _csvConfiguration.QuotationMark);

                StringBuilder lineBuilder = null;
                string csvFieldContent = null;
                foreach (var csvLine in csvLines)
                {
                    if (lineBuilder == null)
                    {
                        lineBuilder = new StringBuilder();
                    }
                    else
                    {
                        lineBuilder.Clear();
                        textWriter.Write(_csvConfiguration.EndOfLine);
                    }

                    foreach (var csvField in csvLine.Fields)
                    {
                        csvFieldContent = csvField.Content;
                        if (csvFieldContent != null)
                        {
                            lineBuilder.Append(_csvConfiguration.QuotationMark);
                            lineBuilder.Append(csvFieldContent.Replace(oneQuotationMark, twoQuotationMarks));
                            lineBuilder.Append(_csvConfiguration.QuotationMark);
                        }
                        lineBuilder.Append(_csvConfiguration.Delimiter);
                    }

                    textWriter.Write(lineBuilder.Length > 0
                        ? lineBuilder.ToString(0, lineBuilder.Length - _csvConfiguration.DelimiterLenght)
                        : string.Empty);
                }

                if (csvFieldContent == null && lineBuilder != null)
                {
                    textWriter.Write(_csvConfiguration.EndOfLine);
                }
            }
        }
    }
}