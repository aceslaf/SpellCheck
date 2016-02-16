using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class LineSpellingErrors
    {
        public int LineNumber { get; set; }
        public IList<string> MisspelledWords { get; set; }
    }
}
