using System;

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
        // TODO implement here
        return null;
    }

    /// <summary>
    /// Overrides the inherited setter to remove URLs from the message body
    /// </summary>
    public override string Body { 
        get => base.Body; 
        set => base.Body = RemoveLinks(value); 
    }
}