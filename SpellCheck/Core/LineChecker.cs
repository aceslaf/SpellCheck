using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class LineChecker : ILineSpellChecker
    {
        private ISpellingDictionary spellChecker { get; set; }
        private IWordSplitter Splitter { get; set; }
        public LineChecker(ISpellingDictionary dict,IWordSplitter splitter)
        {
            spellChecker = dict;
            Splitter = splitter;
        }
        public List<string> FindSepllingErrors(string line)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(line))
            {
                return errors;
            }

            if(line.Length<2)
            {
                return errors;
            }

            Splitter.Reset();
            foreach (var c in line)
            {
                Splitter.FeedLetter(c);
                if (!Splitter.HasWord)
                {
                    continue;
                }

                bool wordIsCorrect = spellChecker.IsWordCorrect(Splitter.Word);
                if (!wordIsCorrect)
                {
                    errors.Add(Splitter.Word);
                }
            }

            return errors;
        }
    }
}
