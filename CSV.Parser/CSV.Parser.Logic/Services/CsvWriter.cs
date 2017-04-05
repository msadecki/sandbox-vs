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

                StringBuilder outputLine = null;

                foreach (var csvLine in csvLines)
                {
                    if (outputLine == null)
                    {
                        outputLine = new StringBuilder();
                    }
                    else
                    {
                        outputLine.Clear();
                        textWriter.Write(_csvConfiguration.EndOfLine);
                    }

                    foreach (var csvField in csvLine.Fields)
                    {
                        if (csvField.Content != null)
                        {
                            outputLine.Append(_csvConfiguration.QuotationMark);
                            outputLine.Append(csvField.Content.Replace(oneQuotationMark, twoQuotationMarks));
                            outputLine.Append(_csvConfiguration.QuotationMark);
                        }
                        outputLine.Append(_csvConfiguration.Delimiter);
                    }

                    textWriter.Write(outputLine.Length > 0
                        ? outputLine.ToString(0, outputLine.Length - _csvConfiguration.DelimiterLenght)
                        : string.Empty);
                }
            }
        }
    }
}