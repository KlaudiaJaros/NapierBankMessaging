using System;

/// <summary>
/// EmailSIR class to hold specific information about EmailSIR
/// </summary>
public class EmailSIR : Email
{
    private String sortCode;
    private String incident;

    /// <summary>
    /// Gets and sets the sort code
    /// @return sort code
    /// </summary>
    public String SortCode {
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
    public String Incident {
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