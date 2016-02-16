using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class FileSpellingErrors
    {
        public string FileName { get; set; }
        public List<LineSpellingErrors> LineErrors { get; set; }
    }
}
