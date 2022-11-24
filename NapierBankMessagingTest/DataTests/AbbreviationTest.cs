using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessaging.Data;
using System.Linq;

namespace NapierBankMessagingTest.DataTests
{
    [TestClass]
    public class AbbreviationTest
    {

        [TestMethod]
        public void LoadAbbreviationsTest()
        {
            int expectedNumberOfAbbrev = 255;
            int actualNumberOfAbbrev = DataFacade.GetAbbreviationDictionary().Count();

            Assert.IsTrue(expectedNumberOfAbbrev == actualNumberOfAbbrev);
        }

        [TestMethod]
        public void GetFullText_ReturnsCorrectText()
        {
            string abbreviation = "AYEC";
            string expectedText = "At your earliest convenience";
            string actualText = DataFacade.GetFullPhrase(abbreviation);

            Assert.IsTrue(expectedText.Equals(actualText));
        }

        [TestMethod]
        public void GetFullText_ReturnsNull()
        {
            string abbreviation = "TEST";
            string actualText = DataFacade.GetFullPhrase(abbreviation);

            Assert.IsTrue(actualText == null);
        }

        [TestMethod]
        public void ConvertAbbreviations_ConvertsAbbreviations()
        {
            string messageBody = "Hi, I urgently need to speak to an advisor. OMW to Napier Bank! B4N";

            string expectedText = "Hi, I urgently need to speak to an advisor. <On my way> to Napier Bank! <Bye for now> ";
            string actualText = DataFacade.ConvertAbbreviations(messageBody);

            Assert.IsTrue(expectedText.Equals(actualText));
        }

        [TestMethod]
        public void ConvertAbbreviations_NoAbbreviations()
        {
            string messageBody = "Hi, I urgently need to speak to an advisor. Going to Napier Bank!";

            string expectedText = "Hi, I urgently need to speak to an advisor. Going to Napier Bank! ";
            string actualText = DataFacade.ConvertAbbreviations(messageBody);

            Assert.IsTrue(expectedText.Equals(actualText));
        }
    }
}
