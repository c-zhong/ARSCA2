using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Observable_Trace_Item
    {
        public string timestamp = "";
        public string operation = "";

        public Con_Observable_Trace_Item(string time, string op)
        {
            timestamp = time;
            operation = op;
        }

        
    }
}
