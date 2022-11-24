using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessaging.Data;
using System.Collections.Generic;
using System.IO;

namespace NapierBankMessagingTest.DataTests
{
    [TestClass]
    public class MessageDataTest
    {
        [TestMethod]
        public void SaveMessage_CreatesFile()
        {
            Tweet tweet = new Tweet("T100011112", "@testing this is save message test");
            DataFacade.SaveMessage(tweet);

            string path = DataFacade.GetPath();
            string messageFilename = "MessageData.json";

            string fullPath = Path.Combine(path, messageFilename);
            Assert.IsTrue(File.Exists(fullPath));
        }

        [TestMethod]
        public void SaveMessage_SavesTweets()
        {
            Tweet tweet = new Tweet("T100011112", "@testing this is save message test");
            string expectedHeader = tweet.Header;
            string expectedBody = tweet.Body;

            DataFacade.SaveMessage(tweet);
            List<Message> messages = DataFacade.ReadMessages();

            Assert.IsTrue(messages.Exists(x => x.Header.Equals(expectedHeader) && x.Body.Equals(expectedBody)));
        }

        [TestMethod]
        public void SaveMessage_SavesSMS()
        {
            SMS sms = new SMS("S100011112", "0711111111 this is save message test");
            string expectedHeader = sms.Header;
            string expectedBody = sms.Body;

            DataFacade.SaveMessage(sms);
            List<Message> messages = DataFacade.ReadMessages();

            Assert.IsTrue(messages.Exists(x => x.Header.Equals(expectedHeader) && x.Body.Equals(expectedBody)));
        }

        [TestMethod]
        public void SaveMessage_SavesEmail()
        {
            Email email = new Email("E100011112", "me@testing.com Subject: testing     This is save message test");
            string expectedHeader = email.Header;
            string expectedBody = email.Body;

            DataFacade.SaveMessage(email);
            List<Message> messages = DataFacade.ReadMessages();

            Assert.IsTrue(messages.Exists(x => x.Header.Equals(expectedHeader) && x.Body.Equals(expectedBody)));
        }

        [TestMethod]
        public void SaveMessage_SavesSIR()
        {
            EmailSIR sir = new EmailSIR("E200011112", "test1@yahoo.co.uk SIR 01/10/22          sort code: 09-11-11\nnature of incident: Bomb Threat\n Hello this is a test");
            string expectedHeader = sir.Header;
            string expectedBody = sir.Body;

            DataFacade.SaveMessage(sir);
            List<Message> messages = DataFacade.ReadMessages();

            Assert.IsTrue(messages.Exists(x => x.Header.Equals(expectedHeader) && x.Body.Equals(expectedBody)));
        }

        [TestMethod]
        public void SaveIncident_CreatesFile()
        {
            EmailSIR incident = new EmailSIR("E100011111", "test1@yahoo.co.uk SIR 01/10/22          sort code: 10-11-11\nnature of incident: Bomb Threat\n Hello this is a test");
            DataFacade.SaveIncident(incident);

            string path = DataFacade.GetPath();
            string incidentFilename = "incidentsData.csv";
            
            string fullPath = Path.Combine(path, incidentFilename);
            Assert.IsTrue(File.Exists(fullPath));
        }

        [TestMethod]
        public void GetIncidents_ReturnsIncidents()
        {
            EmailSIR incident = new EmailSIR("E100011111", "test1@yahoo.co.uk SIR 01/10/22          sort code: 11-11-11\nnature of incident: Bomb Threat\n Hello this is a test");
            DataFacade.SaveIncident(incident);

            Dictionary<string, string> incidents = DataFacade.GetIncidents();

            string expectedSortCode = "11-11-11";
            string expectedIncident = "Bomb Threat";

            Assert.IsTrue(incidents.ContainsKey(expectedSortCode));
            Assert.IsTrue(incidents[expectedSortCode].Equals(expectedIncident));

        }
        [TestMethod]
        public void SaveURL_CreatesFile()
        {
            string url = "https\\://testing.com";
            DataFacade.SaveURL(url);

            string path = DataFacade.GetPath();
            string urlFilename = "quarantine.txt";

            string fullPath = Path.Combine(path, urlFilename);
            Assert.IsTrue(File.Exists(fullPath));
        }
    }
}
