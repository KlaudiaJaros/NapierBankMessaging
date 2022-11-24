using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NapierBankMessaging.Data
{
    /// <summary>
    /// Data class singleton to hold tag trends, retrieve and access tags and mentions data files
    /// </summary>
    public class TrendsData
    {
        private static string path = DataFacade.GetPath();
        private static Dictionary<string, int> tags;
        private const string tagsFilename = "tagsData.csv"; 
        private const string mentionsFilename = "mentionsData.csv";
        private static TrendsData trendsDataSystem; // singleton instance

        /// <summary>
        /// Retrieves the only TrendsData sigleton instance. Static, because it has to be accessible without initialising the object.
        /// </summary>
        public static TrendsData TrendsDataInstance
        {
            get
            {
                if (trendsDataSystem == null)
                {
                    trendsDataSystem = new TrendsData(); // initialise the singleton if accessed for the first time
                    LoadTags();
                }
                return trendsDataSystem;
            }
        }

        /// <summary>
        /// Returns a dictionary of all saved trends in a descending order
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetTrends()
        {
            return (from entry in tags orderby entry.Value descending select entry).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Load existing tags from the app files.
        /// </summary>
        public static void LoadTags()
        {
            tags = new Dictionary<string, int>();
            string fullPath = Path.Combine(path, tagsFilename);

            // if the file exists, get all lines and separate each line by a comma:
            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] separated = lines[i].Split(',');
                    tags.Add(separated[0], int.Parse(separated[1]));
                }
            }
        }

        /// <summary>
        /// SaveTags method to be used externally
        /// </summary>
        /// <param name="tweet">Tweet which tags should be added to the tags trends</param>
        public void SaveTags(Tweet tweet)
        {
            LoadTags();
            UpdateTags(tweet);
            SaveTagsToAFile();
        }

        /// <summary>
        /// Updates the tags dictionary stored in this class by checking the tweet's tags against existing entries and increments the tag value if the tag already exsits or adds a new entry.
        /// </summary>
        /// <param name="tweet">Tweet which tags should be added to the tags dictionary</param>
        public void UpdateTags(Tweet tweet)
        {
            foreach (string tag in tweet.Tags)
            {
                if (tags.ContainsKey(tag))
                {
                    tags.TryGetValue(tag, out int value);
                    tags[tag] = value + 1;
                }
                else
                {
                    tags.Add(tag, 1);
                }
            }
        }

        /// <summary>
        /// Saves the tags dictionary contained within this class to a file
        /// </summary>
        public void SaveTagsToAFile()
        {
            string fullPath = Path.Combine(path, tagsFilename);
            using (StreamWriter file = new StreamWriter(fullPath))
            {
                foreach (KeyValuePair<string, int> entry in tags)
                {
                    file.WriteLine(entry.Key + "," + entry.Value.ToString());
                }
            }
        }

        /// <summary>
        /// Saves mentions in the provided tweet
        /// </summary>
        public void SaveMentions(Tweet tweet)
        {
            string fullPath = Path.Combine(path, mentionsFilename);
            using (StreamWriter file = new StreamWriter(fullPath, true))
            {
                foreach(string mention in tweet.Mentions)
                {
                    file.WriteLine(mention);
                }
            }
        }

        /// <summary>
        /// Retrieves saved mentions
        /// </summary>
        /// <returns>A list of mentions</returns>
        public List<string> GetMentions()
        {
            List<string> mentions = new List<string>();
            string fullPath = Path.Combine(path, mentionsFilename);
            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    mentions.Add(line);
                }
            }
            return mentions;
        }
    }
}
