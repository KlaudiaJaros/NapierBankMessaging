using System;

/// <summary>
/// EmailSIR class to hold specific information about EmailSIR
/// </summary>
public class EmailSIR : Email
{
    private string sortCode;
    private string incident;

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

}