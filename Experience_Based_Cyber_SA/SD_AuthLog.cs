/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_AuthLog.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experience_Based_Cyber_SA
{
    public class SD_AuthLog : SD
    {
       public String time = "";
       public String ip = "";
       public Host_type host_type;
       public String info = "";
       public String atype = "";

       public double hosttype_weight = 0.5;
       public double ip_weight = 1;
       //public double atype_weight = 1;

       public SD_AuthLog()
           : base(Enum_SDType.SDType.AUTHLOG)
       { }
       
        public SD_AuthLog(string t, string ty, string i, string inf)
            : base(Enum_SDType.SDType.AUTHLOG)
        {
            time = t;
            atype = ty;
            ip = i;
            info = inf;
        }

        public override string toObsString()
        {
            string output = "AUTH-["+atype+"]-["+ip+"]";
           
            return output;
        }
        public override string toActString()
        {
            return "Check [AUTH LOG] where ip=[" + ip + "]";
        }

        public override string toOriginalString()
        {
            string output = this.id + ";";
            //time
            output += time + ";";
            //ip
            output += ip + ";";
            //info
            //output += info + ";";
            //atype
            output += atype + ";";
            return output;
        }

        public SD_AuthLog(string str)
            : base(Enum_SDType.SDType.AUTHLOG)
        {
            string[] array = str.Split(';');
            time = array[0];
            ip = array[1];
            //info = array[2];
            atype = array[2];
        }


        public double get_weight (SD_AuthLog s1, SD_AuthLog s2)
        {
            double val = 0;

            if (s1.ip.Equals(s2.ip))
                val = val + ip_weight ;
            else if (SD.get_node_type(s1.ip) == SD.get_node_type(s2.ip))
                val = val + hosttype_weight;

            //MessageBox.Show(s1.atype + " " + s2.atype);
            return val;
        }
    }
}
