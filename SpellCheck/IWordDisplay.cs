using System.Collections.Generic;

namespace SpellCheck
{
    public interface IWordDisplay
    {
        void DisplayWords(ICollection<string> words);
    }
}