using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    interface IErrorDisplay
    {
        void DisplayErrors(FileSpellingErrors errors);
    }
}
