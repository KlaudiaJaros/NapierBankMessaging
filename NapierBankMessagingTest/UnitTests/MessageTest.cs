using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NapierBankMessagingTest
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            string header = "S012345678";
            string messageBody = "07123456789 OMW to Napier Bank! I need to see an advisor. B4N";
            Message testMessage = new Message(header, messageBody);

            string actualHeader = testMessage.Header;
            string actualSender = testMessage.Sender;
            string actualMessageBody = testMessage.Body;

            string expectedHeader = "S012345678";
            string expectedSender = "07123456789";
            string expectedMessageBody = "OMW to Napier Bank! I need to see an advisor. B4N";

            Assert.IsTrue(string.Equals(actualHeader, expectedHeader));
            Assert.IsTrue(string.Equals(actualSender, expectedSender));
            Assert.IsTrue(string.Equals(actualMessageBody, expectedMessageBody));
        }

        [TestMethod]
        public void DetectMessageTypeTest_IsSMS()
        {
            string header = "S012345678";
            string messageBody = "07123456789 OMW to Napier Bank! I need to see an advisor. B4N";
            Message testMessageSMS = new Message(header, messageBody);

            char actualTypeSMS = testMessageSMS.DetectMessageType();
            string actualTypeFullSMS = testMessageSMS.DetectMessageTypeFullName();
            char expectedTypeSMS = 'S';
            string expectedTypeFullSMS = "SMS";

            Assert.IsTrue(Equals(actualTypeSMS, expectedTypeSMS));
            Assert.IsTrue(Equals(actualTypeFullSMS, expectedTypeFullSMS));
        }

        [TestMethod]
        public void DetectMessageTypeTest_IsEmail()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk Napier Bank Event Hello,I'd like to confirm that I will be attending the NapierBank event, regards";
            Message testMessageEmail = new Message(header, messageBody);

            char actualTypeEmail = testMessageEmail.DetectMessageType();
            string actualTypeFullEmail = testMessageEmail.DetectMessageTypeFullName();
            char expectedTypeEmail = 'E';
            string expectedTypeFullEmail = "Email";

            Assert.IsTrue(Equals(actualTypeEmail, expectedTypeEmail));
            Assert.IsTrue(Equals(actualTypeFullEmail, expectedTypeFullEmail));
        }

        [TestMethod]
        public void DetectMessageTypeTest_IsTweet()
        {
            string header = "T012345678";
            string messageBody = "@clare what a beautiful day in #Edinburgh! OMW to Napier Bank! #bank #sunny";
            Message testMessageTweet = new Message(header, messageBody);

            char actualTypeTweet = testMessageTweet.DetectMessageType();
            string actualTypeFullTweet = testMessageTweet.DetectMessageTypeFullName();
            char expectedTypeTweet = 'T';
            string expectedTypeFullTweet = "Tweet";

            Assert.IsTrue(Equals(actualTypeTweet, expectedTypeTweet));
            Assert.IsTrue(Equals(actualTypeFullTweet, expectedTypeFullTweet));
        }
    }
}
