/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_WebServerLog.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_WebServerLog : SD
    {
        public String time = "";
        public String src_ip = "";

        public double src_ip_weight = 0.5;
        public double baseweight = 0.5;

        public Host_type src_type;

        public SD_WebServerLog()
            : base(Enum_SDType.SDType.WEBSERVERLOG)
        { }

         public SD_WebServerLog(string t, string ip)
            : base(Enum_SDType.SDType.WEBSERVERLOG)
        {
            time = t;
            src_ip = ip;
        }
         public override string toObsString()
         {
             string output = "WEBLOG-[" + time + "]-[" + src_ip + "]";
             return output;
         }
         public override string toActString()
         {
             return "Check [WEB SERV LOG] where ip=[" + src_ip + "]";
         }
         public override string toOriginalString()
         {
             string output = this.id + ";";
             output+= time + ";";
             output+= src_ip + ";";
             return output;
         }

         public SD_WebServerLog(string str)
             : base(Enum_SDType.SDType.WEBSERVERLOG)
         {
             string[] array = str.Split(';');
             time = array[0];
             src_ip = array[1];
         }

         public double get_weight(SD_WebServerLog s1, SD_WebServerLog s2)
         {
             double val = 0;

             val = val + baseweight;

             if (s1.src_ip.Equals(s2.src_ip))
                 val = val + src_ip_weight;
            
             return val;
         }
    }
}
