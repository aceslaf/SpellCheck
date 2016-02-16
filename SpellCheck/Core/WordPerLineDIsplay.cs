using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    class WordPerLineDIsplay : IWordDisplay
    {
        private TextWriter _output;
        public WordPerLineDIsplay(TextWriter output)
        {
            _output = output;
        }
        public void DisplayWords(ICollection<string> words)
        {
            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }
                var formated = word.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(formated))
                {
                    continue;
                }

                _output.WriteLine(formated);
            }
        }
    }
}
