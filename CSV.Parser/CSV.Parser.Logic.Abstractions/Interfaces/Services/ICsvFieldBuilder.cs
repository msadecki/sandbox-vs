﻿using CSV.Parser.Logic.Abstractions.Interfaces.Models;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvFieldBuilder
    {
        bool IsDelimiterMatched { get; }

        bool IsEndOfLineMatched { get; }

        int RawFieldBuilderLength { get; }

        ICsvLine CurrentCsvLine { get; }

        int CreatedLinesCount { get; }

        void InitNewLine();

        void InitNewField();

        bool Append(char character);

        void EnsureEndOfLineLengthToMatch();

        void BuildNewFieldAfterDelimiter();

        void BuildNewFieldAfterEndOfLine();

        void BuildNewFieldFromTail();
    }
}