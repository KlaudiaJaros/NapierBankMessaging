using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NapierBankMessagingTest
{
    [TestClass]
    public class EmailSIRTest
    {
        [TestMethod]
        public void ConstructorTestEmail()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk SIR 01/10/22         sort code: 88-99-00\nnature of incident: Bomb Threat\n Hello this is to inform you there was an incident involving Bomb Threat, kind regards, Derek";
            EmailSIR testMessage = new EmailSIR(header, messageBody);

            string actualSortCode = testMessage.SortCode;
            string actualIncident = testMessage.Incident;

            string exptectedSortCode = "88-99-00";
            string expectedIncident = "Bomb Threat";

            Assert.IsTrue(string.Equals(actualSortCode, exptectedSortCode));
            Assert.IsTrue(string.Equals(actualIncident, expectedIncident));
        }

        [TestMethod]
        public void IncidentInfoTest()
        {
            string header = "E012345678";
            string messageBody = "derek@yahoo.co.uk SIR 01/10/22         sort code: 88-99-00\nnature of incident: Bomb Threat\n Hello this is to inform you there was an incident involving Bomb Threat, kind regards, Derek";
            EmailSIR testMessage = new EmailSIR(header, messageBody);

            string actualIncidentInfo = testMessage.IncidentInfo();
            string expectedIncidentInfo = "88-99-00,Bomb Threat";

            Assert.IsTrue(string.Equals(actualIncidentInfo, expectedIncidentInfo));
        }
    }
}
