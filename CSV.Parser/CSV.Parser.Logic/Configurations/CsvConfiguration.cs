﻿using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class CsvConfiguration : ICsvConfiguration
    {
        public char Delimiter { get; } = ',';

        public char QuotationMark { get; } = '"';

        public string EndOfLine { get; } = "\r\n";

        public int DelimiterLength { get; } = 1;

        public int QuotationMarkLength { get; } = 1;

        public int EndOfLineLength { get; } = 2;
    }
}