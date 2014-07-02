/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_MissingAlert.cs
/// Function:   to construct a missing alert
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_MissingAlert
    {
        public string time = "";
        public string src = "";
        public string des = "";
        public string description = "";
        public Con_Hypo hypo = null;

        public string toString()
        {
            if (hypo == null)
                hypo = new Con_Hypo();

            return "Hypothesis-"+hypo.hypo+";\r\n "+"Time-" + time + ";\r\n source-" + src + ";\r\n destination-" + des + ";\r\n description-" + description + ".";
        }
    }
}
