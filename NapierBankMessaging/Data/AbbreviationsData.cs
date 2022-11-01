using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Data
{
    public class AbbreviationsData
    {
        private const string path = "C:\\Users\\klaud\\OneDrive - Edinburgh Napier University\\Year 3\\Software Engineering\\coursework";
        private const string textWordsFilename = "textwords.csv"; 
        private static AbbreviationsData readDataSystem; // singleton instance
        private static Dictionary<string, string> abbreviations;

        /// <summary>
        /// Retrieves the only AbbreviationsData sigleton instance. Static, because it has to be accessible without initialising the object.
        /// </summary>
        public static AbbreviationsData AbbreviationsDataInstance
        {
            get
            {
                if (readDataSystem == null)
                {
                    readDataSystem = new AbbreviationsData(); // initialise the singleton if accessed for the first time
                    LoadAbbreviations();
                }
                return readDataSystem;
            }
        }

        /// <summary>
        /// Loads the list of abbreviations
        /// </summary>
        private static void LoadAbbreviations()
        {
            abbreviations = new Dictionary<string, string>();
            string abbreviationPath = Path.Combine(path, textWordsFilename);

            // if the file exists, get all lines and separate each line by a comma:
            if (File.Exists(abbreviationPath))
            {
                string[] lines = File.ReadAllLines(abbreviationPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] separated = lines[i].Split(',');
                    abbreviations.Add(separated[0], separated[1]);
                }
            }
        }

        /// <summary>
        /// Converts the provided abbreviation to a full phrase 
        /// </summary>
        /// <param name="abbreviation">Abbreviation
        /// <returns>A full phrase for the provided abbreviation</returns>
        public string GetFullPhrase(string abbreviation)
        {
            return abbreviations[abbreviation];
        }

        /// <summary>
        /// Returns the dictionary with all abbreviations
        /// </summary>
        /// <returns>Dictionary with all abbreviations</returns>
        public Dictionary <string, string> GetAbbreviationDictionary() => abbreviations;

        /// <summary>
        /// Examines the provided message body to check for abbreviations and converts abbreviations to full phrases, if any are found. 
        /// </summary>
        /// <param name="message">Message to be checked for abbreviations.</param>
        /// <returns>Message with no abbreviations.</returns>
        public string ConvertAbbreviations(string message)
        {
            string[] separated = message.Split(' ');
            StringBuilder sb = new StringBuilder();
            string phrase;

            for (int i=0; i < separated.Length; i++)
            {
                phrase = string.Empty;
                abbreviations.TryGetValue(separated[i], out phrase);
                if (!String.IsNullOrEmpty(phrase))
                {
                    sb.Append("<" + phrase + "> ");
                }
                else
                {
                    sb.Append(separated[i] + " ");
                }
            }

            return sb.ToString();
        }
    }
}
