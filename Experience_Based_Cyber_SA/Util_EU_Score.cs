/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_EU_Score.cs
/// Function:   sort EUs
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Util_EU_Score
    {
        public string euID = "#";
        public double score = 0;

        #region Constructor

        public Util_EU_Score()
        {
            score = 1;
        }
        public Util_EU_Score(string e)
        {
            euID = e;
            score = 1;
        }
        public Util_EU_Score(string e, double s)
        {
            euID = e;
            score = s;
        }

        #endregion

        public bool isSameEUID(string eid)
        {
            return (!euID.Equals("#")) && (euID.Equals(eid));
        }

        
    }
}
