/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_VirLog.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_VirLog : SD
    {
        public String ip = "";
        public Host_type host_type;
        public String time = "";
        public String virus = "";

        public double ip_weight = 0.2;
        public double baseweight = 0.8;


        public SD_VirLog()
            : base(Enum_SDType.SDType.VIRLOG)
        {
        }

         public SD_VirLog(string i, string t, string info): base(Enum_SDType.SDType.VIRLOG)
        {
            ip = i;
            time = t;
            virus = info;
        }
         public override string toObsString()
         {
             string output = "VIRUS-[" + ip + "]-[" + virus + "]";
             return output;
         }
         public override string toActString()
         {
             return "Check [VIRUS LOG] of ["+ip+"]";
         }
        public override string toOriginalString(){
            string output = this.id + ";";

            output += ip + ";";
            output += time + ";";
            output += virus + ";";
            return output;
        }

        public SD_VirLog(string str)
            : base(Enum_SDType.SDType.VIRLOG)
        {
            string[] array = str.Split(';');
            ip = array[0];
            time = array[1];
            virus = array[2];
        }

        public double get_weight(SD_VirLog s1, SD_VirLog s2)
        {
            double val = 0;

            val = val + baseweight;

            if (s1.ip.Equals(s2.ip))
                val = val + ip_weight;
            
            return val;
        }
    }
}
