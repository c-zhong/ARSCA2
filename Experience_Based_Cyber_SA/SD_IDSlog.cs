/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_IDSlog.cs
/// Function:   
/// Note:   Debug: "l" in "IDSlog" is not "L"
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_IDSlog : SD
    {
        public String time = "";
        public String src_ip = "";
        public Host_type src_type;
        public String des_ip = "";
        public Host_type des_type;
        public String sid = "";
        public String des = "";

        public double src_ip_weight = 0.2;
        public double des_ip_weight = 0.2;
        public double host_weight = 0.1;
        public double baseweight = 0.6;

        
         public SD_IDSlog(string s, string d, string id)
            : base(Enum_SDType.SDType.IDSLOG)
        {
            src_ip = s;
            des_ip = d;
            sid = id;
        }
         public SD_IDSlog()
             : base(Enum_SDType.SDType.IDSLOG)
         {
         }
         public override string toObsString()
         {
             string output = "A" +sid+"-["+ src_ip + "]-[" + des_ip + "]";
             return output;
         }
         public override string toActString()
         {
             return "Check [IDS LOG] id="+sid;
         }
         public override string toOriginalString()
         {
             string output = this.id + ";";
             //time
             output += time + ";";
             //src_ip
             output += src_ip + ";";
             //des_ip
             output += des_ip + ";";
             //sid
             output += sid + ";";
             //des
//             output += des + ";";
             return output;
         }

         public SD_IDSlog(string str)
             : base(Enum_SDType.SDType.IDSLOG)
         {
             string[] array = str.Split(';');
             time = array[0];
             src_ip = array[1];
             des_ip = array[2];
             sid = array[3];
         }

         public double get_weight(SD_IDSlog s1, SD_IDSlog s2)
         {
             double val = 0;

             if (s1.sid.Equals(s2.sid))
             {
                 val = val + baseweight;

                 if (s1.src_ip.Equals(s2.src_ip))
                     val = val + src_ip_weight;
                 else if (SD.get_node_type(s1.src_ip) == SD.get_node_type(s2.src_ip))
                     val = val + host_weight;

                 if (s1.des_ip.Equals(s2.des_ip))
                     val = val + des_ip_weight;
                 else if (SD.get_node_type(s1.des_ip) == SD.get_node_type(s2.des_ip))
                     val = val + host_weight;
             }
             return val;
         }
    }
}
