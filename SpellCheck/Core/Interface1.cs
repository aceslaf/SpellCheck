using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
   public  interface ILineSpellChecker
    {
        List<string> FindSepllingErrors(string line);
    }
}
