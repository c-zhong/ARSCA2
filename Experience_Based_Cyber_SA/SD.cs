/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD.cs
/// Function:   items in BOTH observation and action
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{

    public enum Host_type
    {
        PC = 0,
        WEBSERVER,
        MAILSERVER,
        DBSERVER,
        DNSSERVER,
        FILESERVER,
        OTHER
    }

    public abstract class SD
    {
        public Enum_SDType.SDType type;
        public string id;

     

         public SD(Enum_SDType.SDType t)
        {
            type = t;
            id = Util_All.generateID("SD");
        }
         public string getID()
         {
             return id;
         }

         public String getType()
         {
             return Enum_SDType.getType(type);
         }

         public static Host_type get_node_type(string ip)
         {
             if (ip.Equals("130.203.158.101") || ip.Equals("130.203.158.102") || ip.Equals("130.203.158.103") || ip.Equals("130.203.158.104") || ip.Equals("130.203.158.105"))
                 return Host_type.PC;
             else if (ip.Equals("130.203.157.203"))
                 return Host_type.DBSERVER;
             else if (ip.Equals("130.203.157.212"))
                 return Host_type.FILESERVER;
             else if (ip.Equals("130.203.50.22"))
                 return Host_type.MAILSERVER;
             else if (ip.Equals("130.203.50.11"))
                 return Host_type.WEBSERVER;

             return Host_type.OTHER;
         }

         public static int is_same_network(string ip1, string ip2)
         {
             string[] phrase1 = ip1.Split('.');
             string[] phrase2 = ip2.Split('.');

             if ((Int16.Parse(phrase1[0]) < 128) && (Int16.Parse(phrase2[0]) < 128))
                 return 1;
             else if ((Int16.Parse(phrase1[0]) < 192) && (Int16.Parse(phrase2[0]) < 192) && (Int16.Parse(phrase1[0]) > 127) && (Int16.Parse(phrase2[0]) > 127))
                 return 1;
             else if ((Int16.Parse(phrase1[0]) < 224) && (Int16.Parse(phrase2[0]) < 224) && (Int16.Parse(phrase1[0]) > 191) && (Int16.Parse(phrase2[0]) > 191))
                 return 1;
             else if ((Int16.Parse(phrase1[0]) < 240) && (Int16.Parse(phrase2[0]) < 240) && (Int16.Parse(phrase1[0]) > 223) && (Int16.Parse(phrase2[0]) > 223))
                 return 1;
             else if ((Int16.Parse(phrase1[0]) < 256) && (Int16.Parse(phrase2[0]) < 256) && (Int16.Parse(phrase1[0]) > 239) && (Int16.Parse(phrase2[0]) > 239))
                 return 1;

             return 0;
         }

         

         abstract public string toObsString();
         abstract public string toActString();
         abstract public string toOriginalString();

         public string getTrimedOriginalString()
         {
             
             string[] array = this.toOriginalString().Split(';');
             string result  = "";
             foreach (string str in array)
             {
                 string strTrim = str.Trim();
                 if (strTrim.Equals("") || strTrim.Equals(";"))
                 {

                 }
                 else
                 {
                     result += str+";";
                 }
             }
             return result;
         }

         public static bool isSame(SD s1, SD s2)
         {

             if (s1.type == s2.type)
             {
                 return s1.getTrimedOriginalString().Equals(s2.getTrimedOriginalString());

             }
             else
                 return false;
         }

        public static int containsNum(List<SD> list, SD sd)
        {
            int count = 0;
            foreach (SD item in list)
            {
                if(SD.isSame(item, sd))
                {
                    count ++;
                }
            }
            return count;
        }

        public static SD transfer2SD(string type, string str)
        {
            SD sd = null;

            if (type.Equals("AUTH LOG"))
            {
                sd = new SD_AuthLog(str);
                sd.type = Enum_SDType.SDType.AUTHLOG;
                
            }
            else if (type.Equals("PORT"))
            {
                sd = new SD_Port(str);
                sd.type = Enum_SDType.SDType.PORT;
                
            }
            else if (type.Equals("VULNERABILITY"))
            {
                sd = new SD_Vul(str);
                sd.type = Enum_SDType.SDType.VUL;
               
            }
            else if (type.Equals("IDS LOG"))
            {
                sd = new SD_IDSlog(str);
                sd.type = Enum_SDType.SDType.IDSLOG;
                
            }
            else if (type.Equals("NET CONNECTION"))
            {
                sd = new SD_NetCon(str);
                sd.type = Enum_SDType.SDType.NETCON;
                
            }
            else if (type.Equals("PACKET DUMP"))
            {
                sd = new SD_PacketDump(str);
                sd.type = Enum_SDType.SDType.PACKETDUMP;
                
            }
            else if (type.Equals("VIRUS LOG"))
            {
                sd = new SD_VirLog(str);
                sd.type = Enum_SDType.SDType.VIRLOG;
                
            }
            else if (type.Equals("WEB LOG"))
            {
                sd = new SD_WebServerLog(str);
                sd.type = Enum_SDType.SDType.WEBSERVERLOG;
                
            }
            else if (type.Equals("ALERT PATTERN"))
            {
                sd = new SD_Alerts_Rela(str);
                sd.type = Enum_SDType.SDType.ALERTS_RELA;

            }
            else if (type.Equals("FIREWALL LOG"))
            {
                sd = new SD_FirewallLog(str);
                sd.type = Enum_SDType.SDType.FIREWALLlOG;
            }
            return sd;

        }
      
    }
}
