//[VAST-DATA 4-1]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Experience_Based_Cyber_SA
{
    public class SD_FirewallLog : SD
    {
        public String src_ip = "";
        public String des_ip = "";
        public String src_port = "";
        public String des_port = "";
        public String priority = "";
        public String time = "";
        public String operation = "";
        public String message = "";
        public String protocol = "";
        public String des_server = "";
        public String direction = "";

        public double src_ip_weight = 0.2;
        public double des_ip_weight = 0.2;
        public double baseweight = 0.6;

         public SD_FirewallLog(string s, string sp, string d, string dp, string t, string prior, string op, string msg, string proc, string server,  string dir)
            : base(Enum_SDType.SDType.FIREWALLlOG)
        {
            src_ip = s;
            des_ip = d;
            src_port = sp;
            des_port = dp;
            time = t;
            priority = prior;
            operation = op;
            message = msg;
            protocol = proc;
            des_server = server;
            direction = dir;
        }

         public override string toObsString()
         {
             string output = "FIREWALL-[" + time + "]-["+operation+"]-["+ protocol+"](" + src_ip + ", "+des_ip+")";
             return output;
         }
        public override string toActString(){
            return "Check [FIREWALL LOG] between (" + src_ip + ", " + des_ip + ")";
        }

        public override string toOriginalString()
        {
            string output = this.id + ";";
            //src_ip
            output += src_ip + ";" + src_port + ";";
            //des_ip
            output += des_ip + ";" + des_port + ";";
            //time
            output += time + ";";
            //operation + proto
            output += operation + ";" + protocol + ";";
            //infor
            //output += info = ";";
            return output;
        }

        public SD_FirewallLog()
            : base(Enum_SDType.SDType.FIREWALLlOG)
        { }

        public SD_FirewallLog(string str)
            : base(Enum_SDType.SDType.FIREWALLlOG)
        {
            string[] array = str.Split(';');
            src_ip = array[0];
            src_port = array[1];
            des_ip = array[2];
            des_port = array[3];
            time = array[4];
            operation = array[5];
            protocol = array[6];
        }

        public double get_weight(SD_PacketDump s1, SD_PacketDump s2)
        {
            double val = 0;

            val = val + baseweight;
           
            if (s1.src_ip.Equals(s2.src_ip))
                val = val + src_ip_weight;
           
            if (s1.des_ip.Equals(s2.des_ip))
                val = val + des_ip_weight;
            
            return val;
        }
    }
}
