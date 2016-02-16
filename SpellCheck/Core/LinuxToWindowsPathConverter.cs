using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class LinuxToWindowsPathConverter : IFilePathConverter
    {
        public string Convert(string path)
        {
            var trimmed = path.TrimStart('/');
            Regex rgx = new Regex(@"w*/");
            var match = rgx.Match(trimmed);
            return rgx.Replace(trimmed, match.Value.TrimEnd('/')+@":/", 1);
        }
    }
}
