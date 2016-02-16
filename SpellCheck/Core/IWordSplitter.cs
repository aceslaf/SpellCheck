using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public interface IWordSplitter
    {
        void Reset();
        bool HasWord { get; }
        string Word { get; }
        void FeedLetter(char c);
    }
}
