using NapierBankMessaging.Data;

/// <summary>
/// SMS class to hold information about SMS
/// </summary>
public class SMS : Message
{
    public SMS(string header, string body) : base(header, body) 
    {
        base.Body = DataFacade.ConvertAbbreviations(body);
    }
}