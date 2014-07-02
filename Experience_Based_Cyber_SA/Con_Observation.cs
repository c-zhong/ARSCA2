/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Observation.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Observation
    {
        //List<SD_AuthLog> sd_authlog = null;
        //List<SD_Config> sd_config = null;
        //List<SD_IDSlog> sd_idslog = null;
        //List<SD_NetCon> sd_netcon = null;
        //List<SD_PacketDump> sd_packetdump = null;
        //List<SD_VirLog> sd_virlog = null;
        //List<SD_WebServerLog> sd_webserverlog = null;

        

        public List<SD> sds = new List<SD>();

        public Con_Observation()
        {

        }
        public Boolean isNull()
        {
            return sds.Count == 0;
        }

        public void add(SD s)
        {
            if (sds == null)
            {
                sds = new List<SD>();
            }
            if (!sds.Contains(s))
            {
                sds.Add(s);
            }
        }

    }
}
