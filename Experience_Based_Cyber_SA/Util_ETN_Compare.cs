/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_ETN_Compare.cs
/// Function:   
/// Note:   together with Util_ETN_Score 
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Util_ETN_Compare : IComparer<Util_ETN_Score>
    {
        public int Compare(Util_ETN_Score ets1, Util_ETN_Score ets2)
        {
            if (ets1.score < ets2.score)
            {
                return 1;
            }
            else if (ets1.score == ets2.score)
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
