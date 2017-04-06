using System;

namespace CSV.Parser.Logic.Exceptions
{
    public class CsvInvalidFormatException : Exception
    {
        public CsvInvalidFormatException()
        {
        }

        public CsvInvalidFormatException(string message)
            : base(message)
        {
        }

        public CsvInvalidFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}