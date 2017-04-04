namespace CSV.Parser.Logic.Abstractions.Interfaces.Services
{
    public interface ICsvCharacterParser
    {
        void ParseCharacter(char currentCharacter);

        int ParseTail();
    }
}