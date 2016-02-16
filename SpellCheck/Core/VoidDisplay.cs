using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class VoidDisplay : IErrorDisplay,IWordDisplay
    {
        public void DisplayErrors(FileSpellingErrors errors)
        {
           
        }
        
        public void DisplayWords(ICollection<string> words)
        {
            
        }
    }
}
