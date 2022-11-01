using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

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
        RemoveLinks();
        ExtractSubject();
    }

    /// <summary>
    /// Extracts the 20 character email subject from the message body
    /// </summary>
    /// <param name="body">Message body</param>
    private void ExtractSubject()
    {
        int indexStart = Sender.Length + 1;
        subject = Body.Substring(0, 20);
        Body = Body.Substring(23); // 20 char subject + two white spaces chars
    }

    /// <summary>
    /// Gets and sets the email subject
    /// @return email subject
    /// </summary>
    public string Subject
    {
        get
        {
            return subject;

        }
        set
        {
            subject = value;
        }
    }

    /// <summary>
    /// Removes URLs from the message body
    /// @param String body
    /// </summary>
    private void RemoveLinks()
    {
        //string urlExamples = @"http|https";
        //MatchCollection match = Regex.Matches(body, urlExamples, RegexOptions.IgnoreCase);

        string[] words = Body.Split(' ');
        StringBuilder sb = new StringBuilder();
        string replace = "< URL Quarantined >";
        foreach (string word in words)
        {
            if(word.StartsWith("http") || word.StartsWith("https"))
            {
                sb.Append(replace + " ");
                // TODO add the link to a list somewhere
            }
            else
            {
                sb.Append(word + " ");
            }
        }
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