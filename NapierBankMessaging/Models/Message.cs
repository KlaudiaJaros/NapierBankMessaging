using System;

/// <summary>
/// Message class to hold general information about any message. 
/// </summary>
public class Message {

    private String header;
    private String body;
    private String sender;

    public Message(){}
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
    public String Header
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
    public virtual String Body {
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
    public String Sender {
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
    public virtual Char DetectMessageType() {
            return header[0];
    }

    /// <summary>
    /// Returns all information stored by the class in a human friendly string. 
    /// @return ToString of the Message class
    /// </summary>
    public String ToString() {
        return "Message Type: " + header[0] + ", sender: " + sender + ", body: " + body;
    }

}