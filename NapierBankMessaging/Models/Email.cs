using NapierBankMessaging.Data;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// Email class to hold specific information about Email
/// </summary>
[DataContract]
public class Email : Message
{
    [DataMember]
    private string subject;
    public Email() { }

    /// <summary>
    /// Initialises an Email object and modifies the message body to extract the email subject and removes any URLs.
    /// </summary>
    /// <param name="header">Message header</param>
    /// <param name="body">Message body</param>
    public Email(string header, string body) : base(header, body)
    {
        ExtractSubject();
        RemoveLinks();
    }

    /// <summary>
    /// Extracts the 20 character email subject from the message body
    /// </summary>
    /// <param name="body">Message body</param>
    private void ExtractSubject()
    {
        subject = Body.Substring(0, 20);
        Body = Body.Substring(22);
    }

    /// <summary>
    /// Gets and sets the email subject
    /// @return email subject
    /// </summary>
    public string Subject
    {
        get => subject;
        set => subject = value;
    }

    /// <summary>
    /// Removes URLs from the message body
    /// @param String body
    /// </summary>
    public void RemoveLinks()
    {
        string[] words = Body.Split(' ');
        StringBuilder sb = new StringBuilder();
        string replace = "< URL Quarantined >";
        foreach (string word in words)
        {
            if(word.StartsWith("http") || word.StartsWith("https"))
            {
                sb.Append(replace + " ");
                DataFacade.SaveURL(word);
            }
            else
            {
                sb.Append(word + " ");
            }
        }
        sb.Remove(sb.Length - 1, 1); // remove the last space character
        Body = sb.ToString();
    }

    /// <summary>
    /// Overrides the inherited DetectMessageType to check if the email is of standard or incident type
    /// @return char: E - standrd, I - incident
    /// </summary>
    public override char DetectMessageType()
    {
        return subject.StartsWith("SIR ") ? 'I' : 'E';
    }
}