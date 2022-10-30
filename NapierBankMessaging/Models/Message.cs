using System;

/// <summary>
/// Message class to hold general information about any message. 
/// </summary>
public class Message {

    private string header;
    private string body;
    private string sender;

    public Message() { }
    public Message(string header, string message)
    {
        Header = header;
        Sender = message.Split(' ')[0];
        Body = message;
    }

    /// <summary>
    /// Gets and sets the message header
    /// @return String - message header
    /// </summary>
    public string Header
    {
        get
        {
            return header;

        }
        set
        {
            header = value;
        }

    }

    /// <summary>
    /// Gets and sets the message body
    /// @return String - message body
    /// </summary>
    public virtual string Body
    {
        get
        {
            return body;

        }
        set
        {
            body = value;
        }
    }

    /// <summary>
    /// Gets and sets the message sender
    /// @return String - message sender
    /// </summary>
    public string Sender
    {
        get
        {
            return sender;

        }
        set
        {
            sender = value;
        }
    }

    /// <summary>
    /// @return message type: returns Char: S - SMS, E - email, T - twitter, I - incident email
    /// </summary>
    public virtual char DetectMessageType()
    {
        return char.ToUpper(header[0]);
    }

    /// <summary>
    /// @return message type: returns string: S - SMS, E - email, T - twitter, I - incident email
    /// </summary>
    public virtual string DetectMessageTypeFullName()
    {
        switch (char.ToUpper(header[0]))
        {
            case 'S':
                return "SMS";
            case 'E':
                return "Email";
            case 'T':
                return "Twit";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Returns all information stored by the class in a human friendly string. 
    /// @return ToString of the Message class
    /// </summary>
    public override string ToString() {
        return "Message Type: " + header[0] + ", sender: " + sender + ", body: " + body;
    }

}