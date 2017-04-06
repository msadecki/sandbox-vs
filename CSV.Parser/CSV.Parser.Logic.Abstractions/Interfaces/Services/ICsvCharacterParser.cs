namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvCharacterParser
    {
        void ParseCharacter(char character);

        int ParseTail();
    }
}