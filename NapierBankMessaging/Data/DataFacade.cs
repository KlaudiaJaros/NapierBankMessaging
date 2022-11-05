using System;
using System.Collections.Generic;

namespace NapierBankMessaging.Data
{
    /// <summary>
    /// Data Facade to use within the system. Holds instances of all data classes used to access and save data within the application.
    /// </summary>
    public static class DataFacade
    {
        // singleton instances:
        private static readonly AbbreviationsData abbreviationsData = AbbreviationsData.AbbreviationsDataInstance;
        private static readonly MessageData messageData = MessageData.MessageDataInstance;
        private static readonly TrendsData trendsData = TrendsData.TrendsDataInstance;
        public static string GetPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public static string ConvertAbbreviations(string message)
        {
            return abbreviationsData.ConvertAbbreviations(message);
        }

        public static void SaveMessage(Message message)
        {
            messageData.SaveMessage(message);
        }

        public static void SaveIncident(EmailSIR email)
        {
            messageData.SaveIncident(email);
        }

        public static void SaveTags(Tweet tweet)
        {
            trendsData.SaveTags(tweet);
        }
        public static List<Message> InputFileMessages()
        {
            return messageData.InputFileMessages();
        }
        public static void UpdateTags(Tweet tweet)
        {
            trendsData.UpdateTags(tweet);
        }
        public static void SaveTagsToAFile()
        {
            trendsData.SaveTagsToAFile();
        }
        public static List<string> GetMentions()
        {
            return trendsData.GetMentions();
        }
        public static Dictionary<string, int> GetTrends()
        {
            return trendsData.GetTrends();
        }
        public static void SaveMentions(Tweet tweet)
        {
            trendsData.SaveMentions(tweet);
        }
        public static Dictionary<string, string> GetIncidents()
        {
            return messageData.GetIncidents();
        }
        public static void SaveURL(string url)
        {
            messageData.SaveURL(url);
        }
    }
}
