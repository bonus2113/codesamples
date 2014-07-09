using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DataLocationAttribute : Attribute
{
    public string Location;

    public DataLocationAttribute(string _loc)
    {
        Location = _loc;
    }
}
