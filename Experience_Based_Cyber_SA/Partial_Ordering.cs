using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public enum Operator
    {
        AFTER=0,
        CONCURRENT,
        OVERLAP
    }

    public class Partial_Ordering
    {
        
        public static List<string> ids_sequence = new List<string>();
        public static List<string> ids_des_sequence = new List<string>();

    }
}
