using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class NetSpellDictWrapper : ISpellingDictionary
    {
        private Spelling Spelling { get; set; }
        public NetSpellDictWrapper()
        {
            WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();

            oDict.DictionaryFile = "en-US.dic";
            //load and initialize the dictionary 
            oDict.Initialize();
            Spelling = new Spelling();
            Spelling.Dictionary = oDict;
        }
        public bool IsWordCorrect(string word)
        {
            return Spelling.TestWord(word);
        }
    }
}
