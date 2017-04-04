namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface ICsvFieldBuilderConfiguration
    {
        int RawFieldBuilderCapacity { get; }

        int CsvLineFieldsCapacity { get; }
    }
}