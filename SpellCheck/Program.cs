using SpellCheck.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{
    class Program
    {
        public const string DictionaryFilePath= @"C:\Users\Ace\Documents\Visual Studio 2015\Projects\SpellCheck\SpellCheck\bin\Debug\en-US.dic";
        IFilePathConverter PathConverter { get; set; }
        ISpellingDictionary Dictionary { get; set; }
        IWordSplitter WordSplitter { get; set; }
        ILineSpellChecker LineChecker { get; set; }
        IFileSpellChecker FileChecker { get; set; }
        IErrorDisplay ErrorDisplay { get; set; }
        ISet<string> DistinctMisspeledWords {get;set;}
        public IWordDisplay SummaryDisplay { get; private set; }

        string Options;
        
        TextReader Input;
        TextWriter Output;
       

        public Program(TextReader input, TextWriter output, string options)
        {
            Input = input;
            Output = output;
            Options = options.ToLower();
           
        }

        static void Main(string[] args)
        {
            var options = string.Join("", args) ?? "";
            var program = new Program(Console.In, Console.Out, options);
            program.Run();
        }

        public void Run()
        {
            //INIT add exceptions
            if (Options.IndexOf("-a")>=0)
            {
                if (Options.Replace("-a","").Trim().Length > 0)
                {
                    Output.WriteLine("-A must be the only flag. Terminated");
                    return;
                }
                AddExceptions();
                return;
            }


            //INIT spellcheck
            DistinctMisspeledWords = new HashSet<String>();
            PathConverter = new LinuxToWindowsPathConverter();
            Dictionary = new SimpleSpellDictionary(DictionaryFilePath, SimpleSpellDictionary.HaspelLineToEntriesExtractor);
            WordSplitter = new WordSplitter();
            LineChecker = new LineChecker(Dictionary, WordSplitter);
            FileChecker = new FileChecker(LineChecker,PathConverter);
            CreateDisplays();
            ProcessFilesFromStream(Console.In,Console.Out);
            SummaryDisplay.DisplayWords(DistinctMisspeledWords);
        }

        private void CreateDisplays()
        {
            if (Options.IndexOf("--silent".ToLower()) >= 0)
            {
                ErrorDisplay = new VoidDisplay();
            }
            else
            {
                bool lineNumbers = !(Options.IndexOf("--noLineNumbers".ToLower()) < 0);
                ErrorDisplay = new ErrorDisplayEngine(Console.Out,lineNumbers);
            }

            if (Options.IndexOf("--summary") >= 0)
            {
                SummaryDisplay = new WordPerLineDIsplay(Console.Out);
            }
            else
            {
                SummaryDisplay = new VoidDisplay();
            }
        }

        private  void ProcessFilesFromStream(TextReader reader,TextWriter output)
        {
            string fileName = reader.ReadLine();
            while (!string.IsNullOrEmpty(fileName)) 
            {
               
                try
                {
                    ProcesSpellingMistakesInFile(fileName);
                }
                catch (Exception e)
                {
                    output.WriteLine("Error {0} for file: {1} ", e, fileName);
                }
                fileName = reader.ReadLine();
            } 
        }

        private void AddExceptions()
        {
            using (StreamWriter sw = File.AppendText(DictionaryFilePath))
            {
                string word = string.Empty;
                do
                {
                    word = Input.ReadLine();
                    var formated = word.ToLower().Trim();
                    if (!string.IsNullOrWhiteSpace(formated))
                    {
                        sw.WriteLine(formated);
                    }
                } while (!string.IsNullOrEmpty(word));
            }
        }


        private  void ProcesSpellingMistakesInFile(string fileName)
        {
            var fileErrors = FileChecker.FindSpellingErrors(fileName);
            var allMisspeledWords = fileErrors.LineErrors.SelectMany(x => x.MisspelledWords);
            foreach (var misspelledWord in allMisspeledWords)
            {
                DistinctMisspeledWords.Add(misspelledWord);
            }
            ErrorDisplay.DisplayErrors(fileErrors);
        }
    }
}
