using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class WordSplitter : IWordSplitter
    {
        
        private bool InWord { get; set; }
        public char PrevChar { get; private set; }
        public char CurrentChar { get; private set; }        
        private StringBuilder Sb { get; set; }

        public WordSplitter()
        {
            Reset();
        }

        public void Reset()
        {
            InWord = false;
            Sb = new StringBuilder();
            PrevChar = ',';
            CurrentChar = ',';
        }

        public bool HasWord
        {
            get { return !string.IsNullOrWhiteSpace(Word); }
        }

        public string Word
        {
            get;
            private set;
        }

        public void FeedLetter(char c)
        {
            Word = string.Empty;
            PrevChar = CurrentChar;
            CurrentChar = c;

            if (IsWordEnd(PrevChar, CurrentChar))
            {
                InWord = false;
                Word = Sb.ToString();
            }
           
           
            if (IsWordStart(CurrentChar))
            {
                InWord = true;
                Sb.Clear();
            }


            if (InWord)
            {
                Sb.Append(CurrentChar);
            }
            
        }

        private bool IsWordEnd(char prev, char current)
        {
            return (char.IsLetter(prev) && !char.IsLetter(current)) 
                   ||
                   char.IsUpper(current);
        }

        private bool IsWordStart(char current)
        {
            return  !InWord && char.IsLetter(current); 
        }
    }
}
