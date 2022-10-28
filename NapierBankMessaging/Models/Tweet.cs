using System;
using System.Collections.Generic;

/// <summary>
/// Tweet class to hold specific information about Tweets
/// </summary>
public class Tweet : Message {
    private List<String> tags;

    /// <summary>
    /// Add tags to the tweet's tag list
    /// @param String tag - tag to be added
    /// </summary>
    public void AddTags(String tag) {
        tags.Add(tag);
    }

}