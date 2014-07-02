/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_EU_Compare.cs
/// Function:   
/// Note:    together with Util_EU_Score
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Util_EU_Compare: IComparer<Util_EU_Score>
    {
        public int Compare(Util_EU_Score eus1, Util_EU_Score eus2)
        {
            if (eus1.score < eus2.score)
            {
                return 1;
            }
            else if (eus1.score == eus2.score)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
