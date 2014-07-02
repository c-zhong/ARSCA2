/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_NetCon.cs
/// Function:   
/// Note:    network connection
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_NetCon : SD
    {
        
        public String src_ip = "";
        public Host_type src_type;
        public String des_ip = "";
        public Host_type des_type;
        public String isCon = "";

        public String des = "";

        public double src_ip_weight = 0.1;
        public double des_ip_weight = 0.1;
        public double baseweight = 0.8;

        public SD_NetCon()
            : base(Enum_SDType.SDType.NETCON)
        { 
            
        }

        //index has no meaning, just to distinguish the function with the following one
        public SD_NetCon(string host, int index)
            : base(Enum_SDType.SDType.NETCON)
        {
            des = host;
        }

        public SD_NetCon(string str)
            : base(Enum_SDType.SDType.NETCON)
        {
            string[] array = str.Split(';');
            if (array.Length > 1)
            {
                src_ip = array[0];
                des_ip = array[1];
                isCon = array[2];
            }
            else
            {
                des = array[0];
            }
        }

         public SD_NetCon(string s, string d, string isC)
            : base(Enum_SDType.SDType.NETCON)
        {
            src_ip = s;
            des_ip = d;
            isCon = isC;
        }
         public override string toObsString()
         {
             if (isCon.Equals(""))
             {
                 return "[NET_CONNECT] (" + des + ")";
             }
             else
             {
                 string output = "CON[" + isCon + "] (" + src_ip + ", " + des_ip + ")";
                 return output;
             }
         }
         public override string toActString()
         {
             if (isCon.Equals(""))
             {
                 return "Check [NET_CONNECT] (" + des + ")";
             }
             else
             {
                 return "Check [NET CONNECT] (" + src_ip + ", " + des_ip + ")";
             }
         }

         public override string toOriginalString()
         {
             if (isCon.Equals(""))
             {
                 return "des";
             }
             else
             {
                 string output = this.id + ";";
                 //src_ip
                 output += src_ip + ";";
                 //des_ip
                 output += des_ip + ";";
                 //isCon
                 output += isCon + ";";
                 return output;
             }
         }
       

         public double get_weight(SD_NetCon s1, SD_NetCon s2)
         {
             double val = 0;

             if (s1.isCon.Equals(s2.isCon))
             {
                 val = val + baseweight;
                 
                 if (SD.get_node_type(s1.src_ip) == SD.get_node_type(s2.src_ip))
                     val = val + src_ip_weight;             
                                
                 if (SD.get_node_type(s1.des_ip) == SD.get_node_type(s2.des_ip))
                     val = val + des_ip_weight;                
             }

             return val;
         }
    }
}
