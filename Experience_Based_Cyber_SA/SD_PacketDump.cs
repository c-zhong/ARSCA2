/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_PacketDump.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_PacketDump : SD
    {
        public String src_ip = "";
        public Host_type src_type;
        public String des_ip = "";
        public Host_type des_type;
        public String time = "";
        public String proto = "";
        public String info = "";

        public double src_ip_weight = 0.2;
        public double des_ip_weight = 0.2;
        public double baseweight = 0.6;

         public SD_PacketDump(string s, string d, string t, string p)
            : base(Enum_SDType.SDType.PACKETDUMP)
        {
            src_ip = s;
            des_ip = d;
            time = t;
            proto = p;
        }

         public override string toObsString()
         {
             string output = "PACKET-[" + time + "]-["+proto+"]-(" + src_ip + ", "+des_ip+")";
             return output;
         }
        public override string toActString(){
            return "Check [PACKET DUMP] between (" + src_ip + ", " + des_ip + ")";
        }

        public override string toOriginalString()
        {
            string output = this.id + ";";
            //src_ip
            output += src_ip + ";";
            //des_ip
            output += des_ip + ";";
            //time
            output += time + ";";
            //proto
            output += proto + ";";
            //infor
            //output += info = ";";
            return output;
        }

        public SD_PacketDump()
            : base(Enum_SDType.SDType.PACKETDUMP)
        { }

        public SD_PacketDump(string str)
            : base(Enum_SDType.SDType.PACKETDUMP)
        {
            string[] array = str.Split(';');
            src_ip = array[0];
            des_ip = array[1];
            time = array[2];
            proto = array[3];
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
