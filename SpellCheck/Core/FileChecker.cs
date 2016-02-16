using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck.Core
{
    public class FileChecker : IFileSpellChecker
    {
        private ILineSpellChecker _lineChecker;
        private IFilePathConverter _pathConverter;
        public FileChecker(ILineSpellChecker lineChecker,IFilePathConverter converter)
        {
            _lineChecker = lineChecker;
            _pathConverter = converter;
        }
        public FileSpellingErrors FindSpellingErrors(string file)
        {
            var errors = new List<LineSpellingErrors>();
            string[] lines = System.IO.File.ReadAllLines(_pathConverter.Convert(file));
            int lineNum = 1;

            foreach (var line in lines)
            {   
                try
                {
                    var newErrors = _lineChecker.FindSepllingErrors(line);
                    if (newErrors != null && newErrors.Count > 0)
                    {
                        errors.Add(new LineSpellingErrors
                        {
                            LineNumber = lineNum,
                            MisspelledWords = newErrors
                        });
                    }
                }
                catch (Exception e)
                {
                    //Todo is this stop worthy? If not, abstract away repporting for this exception
                }
                
                lineNum++;
            }

            return new FileSpellingErrors
            {
                FileName = file,
                LineErrors = errors
            };
        }
    }
}
