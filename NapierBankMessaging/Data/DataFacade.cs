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

        public static string ConvertAbbreviations(string message)
        {
            return abbreviationsData.ConvertAbbreviations(message);
        }

    }
}
