using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NapierBankMessagingTest
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void ConstructorTestEmail()
        {
            string header = "E012345678";
            string messageBody = "bob@gmail.com Special offer email  Hello please follow this link for special offers http://specialoffer.napierbank.com";
            Email testMessage = new Email(header, messageBody);

            string actualHeader = testMessage.Header;
            string actualSender = testMessage.Sender;
            string actualSubject = testMessage.Subject;
            string actualMessageBody = testMessage.Body;

            string expectedHeader = "E012345678";
            string expectedSender = "bob@gmail.com";
            string expectedSubject = "Special offer email ";
            string expectedMessageBody = "Hello please follow this link for special offers < URL Quarantined > ";

            Assert.IsTrue(string.Equals(actualHeader, expectedHeader));
            Assert.IsTrue(string.Equals(actualSender, expectedSender));
            Assert.IsTrue(string.Equals(actualSubject, expectedSubject));
            Assert.IsTrue(string.Equals(actualMessageBody, expectedMessageBody));
        }

        [TestMethod]
        public void ConstructorTestIncident()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk SIR 01/10/22         sort code: 88-99-00\n nature of incident: Bomb Threat\n Hello this is to inform you there was an incident involving Bomb Threat, kind regards, Derek";
            Email testMessage = new Email(header, messageBody);

            string actualHeader = testMessage.Header;
            string actualSender = testMessage.Sender;
            string actualSubject = testMessage.Subject;
            string actualMessageBody = testMessage.Body;

            string expectedHeader = "E012345678";
            string expectedSender = "derek@yahoo.co.uk";
            string expectedSubject = "SIR 01/10/22        ";
            string expectedMessageBody = "sort code: 88-99-00\n nature of incident: Bomb Threat\n Hello this is to inform you there was an incident involving Bomb Threat, kind regards, Derek";

            Assert.IsTrue(string.Equals(actualHeader, expectedHeader));
            Assert.IsTrue(string.Equals(actualSender, expectedSender));
            Assert.IsTrue(string.Equals(actualSubject, expectedSubject));
            Assert.IsTrue(string.Equals(actualMessageBody, expectedMessageBody));
        }

        [TestMethod]
        public void DetectMessageTypeTest_IsEmail()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk Napier Bank Event Hello,I'd like to confirm that I will be attending the NapierBank event, regards";
            Email testMessageEmail = new Email(header, messageBody);

            char actualTypeEmail = testMessageEmail.DetectMessageType();
            char expectedTypeEmail = 'E';

            Assert.IsTrue(Equals(actualTypeEmail, expectedTypeEmail));
        }

        [TestMethod]
        public void DetectMessageTypeTest_IsIncident()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk SIR 01/10/22         sort code: 88-99-00\n nature of incident: Bomb Threat\n Hello this is to inform you there was an incident involving Bomb Threat, kind regards, Derek";
            Email testMessageEmail = new Email(header, messageBody);

            char actualTypeEmail = testMessageEmail.DetectMessageType();
            char expectedTypeEmail = 'I';

            Assert.IsTrue(Equals(actualTypeEmail, expectedTypeEmail));
        }
    }
}
