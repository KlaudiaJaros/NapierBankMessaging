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
        private static string path = DataFacade.GetPath();
        private const string incidentFilename = "incidentsData.csv";
        private const string messageFilename = "MessageData.json";
        private const string quarantineFilename = "quarantine.txt";
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
        /// Reads all messages from the user input file, processes them and saves them
        /// </summary>
        public void InputFileMessages()
        {
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
                    char messageType = char.ToUpper(separated[0].TrimStart('\"')[0]);
                    if (messageType == 'E')
                    {
                        int bodyStartIndex = separated[0].Length + separated[1].Length + separated[2].Length + 3; // skip id, sender and subject + 3 commas
                        if (separated[2].ToUpper().StartsWith("SIR"))
                        {
                            bodyStartIndex = bodyStartIndex + separated[3].Length + separated[4].Length +2; // skip sortcode and incident + 2 commas
                            EmailSIR email = new EmailSIR
                            {
                                Header = separated[0].TrimStart('\"'),
                                Sender = separated[1],
                                Subject = separated[2],
                                SortCode = separated[3].Substring(11).TrimEnd('\n', '\r'), // 11 chars for 'Sort code: '
                                Incident = separated[4].Substring(20).TrimEnd('\n', '\r'), // 20 chars for 'Nature of Incident: '
                                Body = lines[i].Substring(bodyStartIndex).TrimEnd('\"'), // extract the email body
                            };
                            SaveIncident(email);
                            message = email;
                        }
                        else
                        {
                            Email email = new Email
                            {
                                Header = separated[0].TrimStart('\"'),
                                Sender = separated[1],
                                Subject = separated[2],
                                Body = lines[i].Substring(bodyStartIndex).TrimEnd('\"'),
                            };
                            email.RemoveLinks();
                            message = email;
                        }
                    }
                    else if (messageType == 'S')
                    {
                        SMS sms = new SMS(separated[0].TrimStart('\"'), lines[i].Substring(separated[0].Length + 1).TrimEnd('\"'));
                        message = sms;
                    }
                    else if (messageType == 'T')
                    {
                        Tweet tweet = new Tweet(separated[0].TrimStart('\"'), lines[i].Substring(separated[0].Length + 1).TrimEnd('\"'));
                        DataFacade.UpdateTags(tweet);
                        DataFacade.SaveMentions(tweet);
                        message = tweet;
                    }
                    SaveMessage(message);
                }
            }
            DataFacade.SaveTagsToAFile();
        }

        /// <summary>
        /// Reads the saved messages json file, deserialises json objects and returns a list of all the messages using their correct object types
        /// </summary>
        /// <returns>A list of all the stored messages</returns>
        public List<Message> ReadMessages()
        {
            List<Message> messages = new List<Message>();
            string fullPath = Path.Combine(path, messageFilename);

            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);
                Message deserializedMessage = new Message();
                Message saveMessage = new Message();
                var ms = new MemoryStream();
                var ser = new DataContractJsonSerializer(typeof(Message));

                // for each json line in the saved messages file:
                foreach (string line in lines)
                {
                    // deserialised as a general message type:
                    ms = new MemoryStream(Encoding.UTF8.GetBytes(line));
                    ser = new DataContractJsonSerializer(typeof(Message));
                    deserializedMessage = ser.ReadObject(ms) as Message;

                    // find out the specific type and deserialise each type:
                    ms = new MemoryStream(Encoding.UTF8.GetBytes(line));
                    if (deserializedMessage.Header.ToUpper().StartsWith("E"))
                    {
                        ser = new DataContractJsonSerializer(typeof(Email));
                        Email email = ser.ReadObject(ms) as Email;
                        if (email.Subject.ToUpper().StartsWith("SIR "))
                        {
                            ms = new MemoryStream(Encoding.UTF8.GetBytes(line));
                            ser = new DataContractJsonSerializer(typeof(EmailSIR));
                            EmailSIR sir = ser.ReadObject(ms) as EmailSIR;
                            email = sir;
                        }
                        saveMessage = email;
                    }
                    else if (deserializedMessage.Header.ToUpper().StartsWith("T"))
                    {
                        ser = new DataContractJsonSerializer(typeof(Tweet));
                        Tweet tweet = (Tweet)ser.ReadObject(ms);
                        saveMessage = tweet;
                    }
                    else if(deserializedMessage.Header.ToUpper().StartsWith("S"))
                    {
                        ser = new DataContractJsonSerializer(typeof(SMS));
                        SMS sms = ser.ReadObject(ms) as SMS;
                        saveMessage = sms;
                    }
                    messages.Add(saveMessage);
                }
                ms.Close();
            }
            return messages;
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
        public Dictionary<string, string> GetIncidents()
        {
            Dictionary<string, string> incidents = new Dictionary<string, string>();
            string fullPath = Path.Combine(path, incidentFilename);
            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    string[] separated = line.Split(',');
                    incidents.Add(separated[0], separated[1]);
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

        /// <summary>
        /// Saves the URL from emails to an quarantine list
        /// </summary>
        /// <param name="email">URL to be saved</param>
        public void SaveURL(string url)
        {
            string fullPath = Path.Combine(path, quarantineFilename);
            using (StreamWriter file = new StreamWriter(fullPath, true))
            {
                file.WriteLine(url);
            }
        }
    }
}
