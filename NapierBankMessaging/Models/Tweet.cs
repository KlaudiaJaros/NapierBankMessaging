using NapierBankMessaging.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Tweet class to hold specific information about Tweets
/// </summary>
[DataContract]
public class Tweet : Message {
    [DataMember]
    private List<string> tags;
    [DataMember]
    private List<string> mentions;

    public Tweet(string header, string body) : base(header, body) 
    {
        Body = DataFacade.ConvertAbbreviations(Body);
        tags = new List<string>();
        mentions = new List<string>();
        TagsMentions();
    }
    public List<string> Tags => tags;
    public List<string> Mentions => mentions;
    /// <summary>
    /// Checks the message body for tags and mentions and adds them to appropriate lists.
    /// </summary>
    private void TagsMentions()
    {
        string[] separated = Body.Split(' ');
        foreach(string word in separated)
        {
            if(word.StartsWith("#"))
            {
                tags.Add(word);
            }
            else if(word.StartsWith("@"))
            {
                mentions.Add(word);
            }
        }
    }

}