/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Enum_SDType.cs
/// Function:   
/// Note:    10 types regarding the data sources, add [OTHER]
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Enum_SDType
    {
        public enum SDType
        {
            AUTHLOG,
            PORT,
            VUL,
            IDSLOG,
            NETCON,
            PACKETDUMP,
            VIRLOG,
            WEBSERVERLOG,
            ALERTS_RELA,
            FIREWALLlOG,
            IMAGE,
            LINK,
            DESCRIPTION,
            OTHER

        }
       
        static public String getType(SDType type)
        {
            switch (type)
            {
                case SDType.AUTHLOG: return "AUTH LOG";
                case SDType.PORT: return "PORT";
                case SDType.VUL: return "VULNERABILITY";
                case SDType.IDSLOG: return "IDS LOG";
                case SDType.NETCON: return "NET CONNECTION";
                case SDType.PACKETDUMP: return "PACKET DUMP";
                case SDType.VIRLOG: return "VIRUS LOG";
                case SDType.WEBSERVERLOG: return "WEB LOG";
                case SDType.ALERTS_RELA: return "ALERT PATTERN";
                case SDType.FIREWALLlOG: return "FIREWALL LOG";
                case SDType.IMAGE: return "IMG";
                case SDType.LINK: return "LINK TOGETHER";
                case SDType.DESCRIPTION: return "DESCRIPTION";
                default: return "";
            }
        }
    }
}
