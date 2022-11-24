using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NapierBankMessagingTest
{
    [TestClass]
    public class TweetTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            string header = "T012345678";
            string messageBody = "@rob Hi @jamesHere, AFAIK @NapierBank is pretty decent, check them out! BOL #NapierBank #NapierBankAccount";
            Tweet testMessage = new Tweet(header, messageBody);

            string actualHeader = testMessage.Header;
            string actualSender = testMessage.Sender;
            string actualMessageBody = testMessage.Body;

            string expectedHeader = "T012345678";
            string expectedSender = "@rob";
            string expectedMessageBody = "Hi @jamesHere, <As far as I know> @NapierBank is pretty decent, check them out! <Best of luck> #NapierBank #NapierBankAccount ";

            Assert.IsTrue(string.Equals(actualHeader, expectedHeader));
            Assert.IsTrue(string.Equals(actualSender, expectedSender));
            Assert.IsTrue(string.Equals(actualMessageBody, expectedMessageBody));
        }

        [TestMethod]
        public void TagsSaveTest()
        {
            string header = "T012345678";
            string messageBody = "@rob Hi @jamesHere, AFAIK @NapierBank #bank is pretty decent, check them out! BOL #NapierBank #NapierBankAccount";
            Tweet testMessage = new Tweet(header, messageBody);

            List<string> actualTags = testMessage.Tags;

            string expectedTag1 = "#NapierBank";
            string expectedTag2 = "#NapierBankAccount";
            string expectedTag3 = "#bank";

            Assert.IsTrue(actualTags.Count == 3);
            Assert.IsTrue(actualTags.Contains(expectedTag1));
            Assert.IsTrue(actualTags.Contains(expectedTag2));
            Assert.IsTrue(actualTags.Contains(expectedTag3));
        }

        [TestMethod]
        public void MentionsSaveTest()
        {
            string header = "T012345678";
            string messageBody = "@rob Hi @jamesHere AFAIK @NapierBank #bank is pretty decent, check them out! BOL #NapierBank #NapierBankAccount";
            Tweet testMessage = new Tweet(header, messageBody);

            List<string> actualMentions = testMessage.Mentions;

            string expectedMention1 = "@NapierBank";
            string expectedMention2 = "@jamesHere";

            Assert.IsTrue(actualMentions.Count == 2);
            Assert.IsTrue(actualMentions.Contains(expectedMention1));
            Assert.IsTrue(actualMentions.Contains(expectedMention2));
        }

        [TestMethod]
        public void TagsStringTest()
        {
            string header = "T012345678";
            string messageBody = "@rob Hi @jamesHere, AFAIK @NapierBank #bank is pretty decent, check them out! BOL #NapierBank #NapierBankAccount";
            Tweet testMessage = new Tweet(header, messageBody);

            string expectedTagToString = "#bank #NapierBank #NapierBankAccount";
            string actualTagToString = testMessage.TagsToString();

            Assert.IsTrue(string.Equals(actualTagToString, expectedTagToString));
        }
    }
}
