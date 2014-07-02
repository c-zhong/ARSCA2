/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_Other.cs
/// Function:   
/// Note:    to be in case
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_Other: SD
    {
        public string note = "";

        public SD_Other(): base(Enum_SDType.SDType.OTHER)
        {
            
        }
        public override string toObsString()
        {
            if (note.Equals(""))
                return "Other";
            else
                return note;
        }
        public override string toActString()
        {
            if (note.Equals(""))
                return "Other";
            else
                return note;
        }
        public override string toOriginalString()
        {
            if (note.Equals(""))
                return "Other";
            else
                return note;
        }
    }
}
