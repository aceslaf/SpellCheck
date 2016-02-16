using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    class SimpleSpellDictionary : ISpellingDictionary
    {
        private HashSet<string> allLegalWords { get; set; }
        public SimpleSpellDictionary(string fileName, Func<string, List<string>> lineToEntriesExtractor)
        {
            allLegalWords = new HashSet<string>();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (string line in lines)
            {

                try
                {
                    var newWOrds = lineToEntriesExtractor(line);

                    newWOrds.ForEach(x => allLegalWords.Add(x.ToLower().Trim()));
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                
            }

        }
        public bool IsWordCorrect(string word)
        {
           return  allLegalWords.Contains(word.ToLower().Trim());
        }

        public static List<string> HaspelLineToEntriesExtractor(string line)
        {
            if (string.IsNullOrEmpty(line)) {
                return new List<string>();
            }

            var words = line.Split('/');
            if (words == null || words.Length <= 1)
            {
                return new List<string> { line };
            }

            var res = new List<string> { words[0] };
            if (words[1].Contains('S')
                ||
                (
                 words[0].EndsWith("r") || words[0].EndsWith("g")) 
                 && 
                 words[1].Contains("M")
                )
            {
                res.Add(words[0] + 's');
            }

            return res;
        }

        public static string MakePlural(string word)
        {
            //ch, -s, -sh, -x, -z to form a 
            if (word.EndsWith("s") || word.EndsWith("ch") || word.EndsWith("sh") || word.EndsWith("x") || word.EndsWith("z"))
            {
                return word + "es";
            }

            //a,e,i,o,u
            if (word.EndsWith("y"))
            {
                if (word.EndsWith("ay") || word.EndsWith("ey") || word.EndsWith("iy") || word.EndsWith("oy") || word.EndsWith("uy"))
                {
                    return word + "s";
                }
                else
                {
                    return word.TrimEnd('y') + "ies";
                }
            }

            return word + "s";
        }
    }
}
