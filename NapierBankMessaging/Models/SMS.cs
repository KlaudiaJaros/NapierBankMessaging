using NapierBankMessaging.Data;
using System.Runtime.Serialization;

/// <summary>
/// SMS class to hold information about SMS
/// </summary>
[DataContract]
public class SMS : Message
{
    public SMS(string header, string body) : base(header, body)
    {
        Body = DataFacade.ConvertAbbreviations(Body);
    }
}