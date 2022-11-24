using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NapierBankMessagingTest
{
    [TestClass]
    public class SMSTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            string header = "S012345678";
            string messageBody = "07123456789 OMW to Napier Bank! I need to see an advisor. B4N";
            SMS testMessage = new SMS(header, messageBody);

            string actualHeader = testMessage.Header;
            string actualSender = testMessage.Sender;
            string actualMessageBody = testMessage.Body;

            string expectedHeader = "S012345678";
            string expectedSender = "07123456789";
            string expectedMessageBody = "<On my way> to Napier Bank! I need to see an advisor. <Bye for now> ";

            Assert.IsTrue(string.Equals(actualHeader, expectedHeader));
            Assert.IsTrue(string.Equals(actualSender, expectedSender));
            Assert.IsTrue(string.Equals(actualMessageBody, expectedMessageBody));
        }
    }
}
