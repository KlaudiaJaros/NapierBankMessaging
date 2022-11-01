using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NapierBankMessaging.Data
{
    /// <summary>
    /// Data class singleton to save and retrieve messages
    /// </summary>
    public class MessageData
    {
        private const string path = "C:\\Users\\klaud\\OneDrive - Edinburgh Napier University\\Year 3\\Software Engineering\\coursework";
        private const string incidentFilename = "incidentsData.csv";
        private const string messageFilename = "MessageData.json";
        private static MessageData messageDataSystem; // singleton instance

        /// <summary>
        /// Retrieves the only MessageData sigleton instance. Static, because it has to be accessible without initialising the object.
        /// </summary>
        public static MessageData MessageDataInstance
        {
            get
            {
                if (messageDataSystem == null)
                {
                    messageDataSystem = new MessageData(); // initialise the singleton if accessed for the first time
                }
                return messageDataSystem;
            }
        }

        /// <summary>
        /// Prompts the user to choose an input file and returns a path.
        /// </summary>
        /// <returns>Path to the user input file</returns>
        private string GetPathFromUser()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\Users\\klaud\\OneDrive - Edinburgh Napier University\\Year 3\\Software Engineering\\coursework",
                Filter = "Plain text files (*.csv;*.txt)|*.csv;*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                //Get the path of specified file
                return openFileDialog.FileName;
            }
            return string.Empty;
        }
        /// <summary>
        /// Reads all messages from the user input file, turns them into a Message object and returns a list of type Message with all messages from the file.
        /// </summary>
        /// <returns>List of Message objects created from the input file</returns>
        public List<Message> InputFileMessages()
        {
            List<Message> inputFileMessages = new List<Message>();
            Message message = new Message();
            string filePath = GetPathFromUser();

            // if the file exists, get all lines and separate each line by a comma:
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    message = new Message();
                    string[] separated = lines[i].Split(',');
                    char messageType = separated[0].TrimStart('\"')[0];
                    if (messageType == 'E')
                    {
                        int index = separated[0].Length + separated[1].Length + separated[2].Length + 3;
                        if (separated[2].StartsWith("SIR"))
                        {
                            index = index + separated[3].Length + separated[4].Length;
                            EmailSIR email = new EmailSIR
                            {
                                Header = separated[0],
                                Sender = separated[1],
                                Subject = separated[2],
                                SortCode = separated[3],
                                Incident = separated[4],
                                Body = lines[i].Substring(index),
                            };
                            SaveIncident(email);
                            message = email;
                        }
                        else
                        {
                            Email email = new Email
                            {
                                Header = separated[0],
                                Sender = separated[1],
                                Subject = separated[2],
                                Body = lines[i].Substring(index),
                            };
                            message = email;
                        }
                    }
                    else if (messageType == 'S')
                    {
                        SMS sms = new SMS(separated[0], lines[i].Substring(separated[0].Length) + 1);
                        message = sms;
                    }
                    else if (messageType == 'T')
                    {
                        Tweet tweet = new Tweet(separated[0], lines[i].Substring(separated[0].Length + 1));
                        DataFacade.UpdateTags(tweet);
                        message = tweet;
                    }
                    inputFileMessages.Add(message);
                    SaveMessage(message);
                }
            }
            DataFacade.SaveTagsToAFile();
            return inputFileMessages;
        }

        /// <summary>
        /// Saves the incident from SIR email to an incident list
        /// </summary>
        /// <param name="email">SIR email to be saved</param>
        public void SaveIncident(EmailSIR email)
        {
            string fullPath = Path.Combine(path, incidentFilename);
            using (StreamWriter file = new StreamWriter(fullPath, true))
            {
                file.WriteLine(email.IncidentInfo());
            }
        }

        /// <summary>
        /// Retrieves the list of all saved incidents
        /// </summary>
        /// <returns>List of incidents</returns>
        public List<string> GetIncidents()
        {
            List<string> incidents = new List<string>();
            string fullPath = Path.Combine(path, incidentFilename);
            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    incidents.Add(line);
                }
            }
            return incidents;
        }

        /// <summary>
        /// Converts the provided Message object to JSON and writes it to a JSON file.
        /// </summary>
        /// <param name="newMessage">Message object to be saved in a JSON file.</param>
        public void SaveMessage(Message newMessage)
        {
            string json = ConvertToJson(newMessage);
            string fullPath = Path.Combine(path, messageFilename);
            using (StreamWriter file = new StreamWriter(fullPath, true))
            {
                file.WriteLine(json);
            }
        }

        /// <summary>
        /// Converts the provided Message object to a json string
        /// </summary>
        /// <param name="newMessage">Message object to be converted</param>
        /// <returns>Message object as json string</returns>
        private string ConvertToJson(Message newMessage)
        {
            MemoryStream memStream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Message));
            if (newMessage is SMS)
            {
                ser = new DataContractJsonSerializer(typeof(SMS));
            }
            else if (newMessage is EmailSIR)
            {
                ser = new DataContractJsonSerializer(typeof(EmailSIR));
            }
            else if (newMessage is Email)
            {
                ser = new DataContractJsonSerializer(typeof(Email));
            }
            else if (newMessage is Tweet)
            {
                ser = new DataContractJsonSerializer(typeof(Tweet));
            }

            ser.WriteObject(memStream, newMessage);
            byte[] json = memStream.ToArray();
            memStream.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
