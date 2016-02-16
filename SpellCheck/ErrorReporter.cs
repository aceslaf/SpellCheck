using SpellCheck.Core;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace SpellCheck
{
    public class ErrorDisplayEngine: IErrorDisplay
    {
        private TextWriter _output;
        private bool _displayLineNumbers;
        public ErrorDisplayEngine(TextWriter output,bool displayLineNumbers)
        {
            _output = output;
            _displayLineNumbers = displayLineNumbers;
        }
        public void DisplayErrors(FileSpellingErrors fileErrors)
        {
            _output.WriteLine(fileErrors.FileName + ":");
            foreach (var error in fileErrors.LineErrors)
            {
                //if (_displayLineNumbers)
                //{
                    _output.Write(error.LineNumber);
                //}

                foreach (var word in error.MisspelledWords)
                {
                    _output.Write(" " + word);
                }
                _output.Write(_output.NewLine);
            }
            _output.WriteLine();
            _output.WriteLine();
        }
    }
}