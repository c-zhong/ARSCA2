/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_Port.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_Port:SD
    {
        public String ip = "";
        public Host_type host_type;
        public String port = "";
        public String service = "";
        public String version = "";
        public String status = "";


        public double ip_weight = 0.2;
        public double baseweight = 0.8;

        public SD_Port()
            : base(Enum_SDType.SDType.PORT)
        {
        }
       
         public SD_Port(string i, string p, string st, string serv, string vers)
            : base(Enum_SDType.SDType.PORT)
        {
            ip = i;
            port = p;
            service = serv;
            version = vers;
            status = st;
        }
         public override string toObsString()
         {
             string output = "PORT-["+ip+"]-["+port+"]-["+status+"]";
             return output;
         }
         public override string toActString()
         {
             return "Check [PORT CONFIG] of [" + ip + "]";
         }

         public override string toOriginalString()
         {
             string output = this.id + ";";
             output += ip + ";";
             output += port + ";";
             output += service + ";";
             output += version + ";";
             output += status + ";";
             return output;
         }

         public SD_Port(string str)
             : base(Enum_SDType.SDType.PORT)
         {
             string[] array = str.Split(';');
             ip = array[0];
             port = array[1];
             service = array[2];
             version = array[3];
             status = array[4];
     
         }

         public double get_weight(SD_Port s1, SD_Port s2)
         {
             double val = 0;

             val = val + baseweight;
             
             if (SD.get_node_type(s1.ip) == SD.get_node_type(s2.ip))
                     val = val + ip_weight;               
             
             return val;
         }
    }
}
