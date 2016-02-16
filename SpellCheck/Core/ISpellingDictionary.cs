namespace SpellCheck.Core
{
    public interface ISpellingDictionary
    {
        bool IsWordCorrect(string word);
    }
}