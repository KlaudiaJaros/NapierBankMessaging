using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Data
{
    public static class DataFacade
    {
        // singleton instances:
        private static readonly AbbreviationsData abbreviationsData = AbbreviationsData.AbbreviationsDataInstance;
        private static readonly MessageData messageData = MessageData.MessageDataInstance;

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
            messageData.SaveTags(tweet);
        }
        public static List<Message> InputFileMessages()
        {
            return messageData.InputFileMessages();
        }
    }
}
