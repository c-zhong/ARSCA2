/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Actions.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Actions
    {
        public List<SD> sds = new List<SD>();

        public void add(SD s)
        {
            if (sds == null)
            {
                sds = new List<SD>();
            }
            if (!sds.Contains(s))
            {
                sds.Add(s);
            }
        }
    }
}
