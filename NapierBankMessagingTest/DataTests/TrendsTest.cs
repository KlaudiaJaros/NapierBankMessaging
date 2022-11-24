using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessaging.Data;
using System.Collections.Generic;
using System.IO;

namespace NapierBankMessagingTest.DataTests
{
    [TestClass]
    public class TrendsTest
    {
        [TestMethod]
        public void UpdateTags_TagsAreUpdates()
        {
            Tweet tweet = new Tweet("T100011111", "@daniel Hello this is a test tweet #testTag1 #testTag1 I need some help #testTag2");
            
            DataFacade.UpdateTags(tweet);

            Dictionary<string, int> tags = DataFacade.GetTrends();

            int expectedOccurence1 = 2;
            int expectedOccurence2 = 1;
            string exptectedTag1 = "#testTag1";
            string exptectedTag2 = "#testTag2";
            int actualOccurence1 = tags[exptectedTag1];
            int actualOccurence2 = tags[exptectedTag2];

            Assert.IsTrue(tags.ContainsKey(exptectedTag1) && tags.ContainsKey(exptectedTag2));
            Assert.IsTrue(expectedOccurence1 == actualOccurence1);
            Assert.IsTrue(expectedOccurence2 == actualOccurence2);
        }

        [TestMethod]
        public void GetMentions_ReturnsMentions()
        {
            Tweet tweet = new Tweet("T100011111", "@daniel Hello this is a test tweet @testMention1 I need some help @testMention2");

            DataFacade.SaveMentions(tweet);

            List<string> mentions = DataFacade.GetMentions();

            string exptectedMention1 = "@testMention1";
            string exptectedMention2 = "@testMention2";

            Assert.IsTrue(mentions.Contains(exptectedMention1));
            Assert.IsTrue(mentions.Contains(exptectedMention2));
        }

        [TestMethod]
        public void SaveMentions_CreatesFile()
        {
            Tweet tweet = new Tweet("T100011111", "@daniel Hello this is a test tweet @testMention1 I need some help @testMention2");

            DataFacade.SaveMentions(tweet);
            string tagsFilename = "tagsData.csv";
            string path = DataFacade.GetPath();
            string mentionsFilename = "mentionsData.csv";
            string fullPath = Path.Combine(path, mentionsFilename);
            Assert.IsTrue(File.Exists(fullPath));
        }

        [TestMethod]
        public void SaveTags_CreatesFile()
        {
            Tweet tweet = new Tweet("T100011111", "@daniel Hello this is a test tweet #testTag1 #testTag1 I need some help #testTag2");

            DataFacade.SaveTags(tweet);

            string tagsFilename = "tagsData.csv";
            string path = DataFacade.GetPath();
            string fullPath = Path.Combine(path, tagsFilename);
            Assert.IsTrue(File.Exists(fullPath));
        }
    }
}

