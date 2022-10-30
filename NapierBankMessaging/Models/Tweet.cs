using NapierBankMessaging.Data;
using System;
using System.Collections.Generic;

/// <summary>
/// Tweet class to hold specific information about Tweets
/// </summary>
public class Tweet : Message {
    private List<string> tags;

    public Tweet(string header, string body) : base(header, body) 
    {
        base.Body = DataFacade.ConvertAbbreviations(body);
    }

    /// <summary>
    /// Add tags to the tweet's tag list
    /// @param String tag - tag to be added
    /// </summary>
    public void AddTags(string tag) {
        tags.Add(tag);
    }

}