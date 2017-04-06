using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;

namespace CSV.Parser.Logic.Configurations
{
    public class CsvFieldBuilderConfiguration : ICsvFieldBuilderConfiguration
    {
        public int RawFieldBuilderCapacity => 1024;

        public int CsvLineFieldsCapacity => 128;
    }
}