using System;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Email class to hold specific information about Email
/// </summary>
public class Email : Message {
    private String subject;

    /// <summary>
    /// Gets and sets the email subject
    /// @return email subject
    /// </summary>
    public String Subject {
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
    private String RemoveLinks(String body) {
        //string urlExamples = @"http|https";
        //MatchCollection match = Regex.Matches(body, urlExamples, RegexOptions.IgnoreCase);

        String[] words = body.Split(' ');
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
            return sb.ToString();
        }

        return null;
    }

    /// <summary>
    /// Overrides the inherited setter to remove URLs from the message body
    /// </summary>
    public override string Body { 
        get => base.Body; 
        set => base.Body = RemoveLinks(value); 
    }

    /// <summary>
    /// Overrides the inherited DetectMessageType to check if the email is of standard or incident type
    /// @return char: E - standrd, I - incident
    /// </summary>
    public override char DetectMessageType()
    {
        if (subject.StartsWith("SIR "))
        {
            return 'I';
        }
        else
        {
            return 'E';
        }
    }
}