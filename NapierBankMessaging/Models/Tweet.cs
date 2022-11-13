using NapierBankMessaging.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// Tweet class to hold specific information about Tweets
/// </summary>
[DataContract]
public class Tweet : Message
{
    [DataMember]
    public List<string> Tags { get; set; }
    [DataMember]
    public List<string> Mentions { get; set; }

    public Tweet(string header, string body) : base(header, body)
    {
        Body = DataFacade.ConvertAbbreviations(Body);
        Tags = new List<string>();
        Mentions = new List<string>();
        TagsMentions();
    }
    public Tweet()
    {
        Tags = new List<string>();
        Mentions = new List<string>();
    }

    /// <summary>
    /// Checks the message body for tags and mentions and adds them to appropriate lists.
    /// </summary>
    private void TagsMentions()
    {
        string[] separated = Body.Split(' ');
        foreach (string word in separated)
        {
            if (word.StartsWith("#"))
            {
                Tags.Add(word.TrimEnd('\n','\r'));
            }
            else if (word.StartsWith("@"))
            {
                Mentions.Add(word.TrimEnd('\n', '\r'));
            }
        }
    }

    /// <summary>
    /// Returns all tweet tags in a form of a string
    /// </summary>
    /// <returns>String with all tags</returns>
    public string TagsToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach(string tag in Tags)
        {
            sb.Append(tag + " ");
        }
        return sb.ToString().TrimEnd(' ');
    }
}