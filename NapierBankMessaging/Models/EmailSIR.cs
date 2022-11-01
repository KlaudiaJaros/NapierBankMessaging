using System;
using System.Runtime.Serialization;

/// <summary>
/// EmailSIR class to hold specific information about EmailSIR
/// </summary>
[DataContract]
public class EmailSIR : Email
{
    [DataMember]
    private string sortCode;
    [DataMember]
    private string incident;

    public EmailSIR() { }

    /// <summary>
    /// Contructor for creating incident emails from user input form. Extracts the sort code and incident from the message body.
    /// </summary>
    /// <param name="header">Header</param>
    /// <param name="body">Message body</param>
    public EmailSIR(string header, string body) : base(header, body) 
    {
        Extract();
    }

    private void Extract()
    {
        string[] separated = Body.Split('\n');
        sortCode = separated[0].Substring(11).TrimEnd('\n','\r'); // 11 chars for 'Sort code:'
        incident = separated[1].Substring(20).TrimEnd('\n','\r'); ; // 20 chars for 'Nature of Incident:'
    }

    /// <summary>
    /// Gets and sets the sort code
    /// @return sort code
    /// </summary>
    public string SortCode
    {
        get
        {
            return sortCode;

        }
        set
        {
            sortCode = value;
        }
    }

    /// <summary>
    /// Gets and sets the incident type
    /// @return incident type
    /// </summary>
    public string Incident
    {
        get
        {
            return incident;

        }
        set
        {
            incident = value;
        }
    }
    public string IncidentInfo()
    {
        return sortCode + "," + incident;
    }
}