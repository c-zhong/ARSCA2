/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_Vul.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_Vul : SD
    {
        public String ip = "";
        public Host_type host_type;
        public String vul_id = "";
        public String des = "";

        public double ip_weight = 0.3;
        public double host_weight = 0.2;
        public double baseweight = 0.7;
        public double des_weight = 0;

        public SD_Vul()
            : base(Enum_SDType.SDType.VUL)
        { }

        public SD_Vul(string i, string vid)
            : base(Enum_SDType.SDType.VUL)
        {
            ip = i;
            vul_id = vid;
        }
         public override string toObsString()
         {
             string output = "VUL-["+ip+"]-"+vul_id;
             return output;
         }
         public override string toActString()
         {
             return "Check [VULNERABILITY] of [" + ip + "]";
         }
         public override string toOriginalString()
         {
             string output = this.id + ";";

             output += ip + ";";
             output += vul_id + ";";
             //output += des + ";";
             return output;
         }
         public SD_Vul(string str)
             : base(Enum_SDType.SDType.VUL)
         {
             string[] array = str.Split(';');
             ip = array[0];
             vul_id = array[1];
         }

         public double get_weight(SD_Vul s1, SD_Vul s2)
         {
             double val = 0;
             
             val = val + baseweight;

             if (s1.ip.Equals(s2.ip))
                 val = val + ip_weight;

             else if (SD.get_node_type(s1.ip) == SD.get_node_type(s2.ip))
                 val = val + host_weight;

             return val;
         }
    }
}
