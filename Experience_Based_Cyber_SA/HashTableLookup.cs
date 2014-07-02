using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Deepak
namespace Experience_Based_Cyber_SA
{
    /*
    public enum Host_type
    {
        PC=0,
        WEBSERVER,
        MAILSERVER,
        DBSERVER,
        DNSSERVER,
        FILESERVER,
        OTHER
    }

    public enum Virus
    {
        TrojanHorse = 0,
        OTHER
    }

    struct packet_dump_hash
    {
        public Host_type sourcetype;
        public Host_type destinationtype;
        public string protocol;
    }

    struct antivirus_log_hash
    {
        public Host_type host;
        public Virus virus;
    }

    struct webserver_log_hash
    {
        public string ip_address;
    }

    struct auth_log_hash
    {
        public string ip_address;
    }

    struct port_info_hash
    {
        public Host_type host;
        public int port_number;
        public int status; // 0 = close, 1 = open
    }

    struct network_conn_hash
    {
        public Host_type sourcetype;
        public Host_type destinationtype;
        public int is_connected;
    }
    */


    class HashTableLookup
    {
        public static string get_host_type(string ip)
        {
            if (ip.Equals("130.203.158.101") || ip.Equals("130.203.158.102") || ip.Equals("130.203.158.103") || ip.Equals("130.203.158.104") || ip.Equals("130.203.158.105"))
                return "PC";
            else if (ip.Equals("130.203.157.203"))
                return "DBSERVER";
            else if (ip.Equals("130.203.157.212"))
                return "FILESERVER";
            else if (ip.Equals("130.203.50.22"))
                return "MAILSERVER";
            else if (ip.Equals("130.203.50.11"))
                return "WEBSERVER";

            return "OTHER";
        }

        public static Hashtable EU_lookup = new Hashtable();
        public static Hashtable snort_hashtable = new Hashtable();
        public static Hashtable pkt_dump_hashtable = new Hashtable();
        public static Hashtable vulnerability_hashtable = new Hashtable();
        public static Hashtable antiviruslog_hashtable = new Hashtable();
        public static Hashtable webserverlog_hashtable = new Hashtable();
        public static Hashtable authlog_hashtable = new Hashtable();
        public static Hashtable portinfo_hashtable = new Hashtable();
        public static Hashtable networkconn_hashtable = new Hashtable();
        public static List<string> alert_corr_euidlist = new List<string>();

        static public void EU_lookup_init(Con_Experience_Unit eu)
        {
            if(!EU_lookup.ContainsKey(eu.id))
             EU_lookup.Add(eu.id, eu);
        }

        static public void init_snort_hashtable(string id, SD sd)
        {
            List<string> nodelist = new List<string>();
            SD_IDSlog sd_ids = (SD_IDSlog)sd;

            if (snort_hashtable.ContainsKey(sd_ids.sid))
            {
                nodelist = (List<string>)snort_hashtable[sd_ids.sid];
                nodelist.Add(id);
                //MessageBox.Show("Adding:" + id + "!");
                snort_hashtable.Remove(sd_ids.sid);
                snort_hashtable.Add(sd_ids.sid, nodelist);
            }
            else
            {
                nodelist.Add(id);
                //MessageBox.Show("Adding:" + id + "!");
                snort_hashtable.Add(sd_ids.sid, nodelist);
            }

        }

        static public void init_pkdump_hashtable(string id, SD sd)
        {
            List<string> nodelist = new List<string>();
            SD_PacketDump sd_ids = (SD_PacketDump)sd;
                        
            string srctype = get_host_type(sd_ids.src_ip);
            string destype = get_host_type(sd_ids.des_ip);
            string proto = sd_ids.proto;

            string hashed_concat_string = srctype + destype + proto;

            if (pkt_dump_hashtable.ContainsKey(hashed_concat_string))
            {
                nodelist = (List<string>)pkt_dump_hashtable[hashed_concat_string];
                nodelist.Add(id);
                pkt_dump_hashtable.Remove(hashed_concat_string);
                pkt_dump_hashtable.Add(hashed_concat_string, nodelist);
            }
            else
            {
                nodelist.Add(id);
                pkt_dump_hashtable.Add(hashed_concat_string, nodelist);
            }


        }

        static public void init_authlog_hashtable(string id, SD sd)
        {

            List<string> nodelist = new List<string>();
            SD_AuthLog sd_ids = (SD_AuthLog)sd;

            string ip_addr = sd_ids.ip;

            if (authlog_hashtable.ContainsKey(get_host_type(ip_addr)))
            {
                nodelist = (List<string>)authlog_hashtable[get_host_type(ip_addr)];
                nodelist.Add(id);
                authlog_hashtable.Remove(get_host_type(ip_addr));
                authlog_hashtable.Add(get_host_type(ip_addr), nodelist);
            }
            else
            {
                nodelist.Add(id);
                authlog_hashtable.Add(get_host_type(ip_addr), nodelist);
            }

        }

        static public void init_netconn_hashtable(string id, SD sd)
        {

            List<string> nodelist = new List<string>();
            SD_NetCon sd_ids = (SD_NetCon)sd;

            string srctype = get_host_type(sd_ids.src_ip);
            string destype = get_host_type(sd_ids.des_ip);
            string is_conn = sd_ids.isCon;


            string hashed_concat_string = srctype + destype + is_conn;

            if (networkconn_hashtable.ContainsKey(hashed_concat_string))
            {
                nodelist = (List<string>)networkconn_hashtable[hashed_concat_string];
                nodelist.Add(id);
                networkconn_hashtable.Remove(hashed_concat_string);
                networkconn_hashtable.Add(hashed_concat_string, nodelist);
            }
            else
            {
                nodelist.Add(id);
                networkconn_hashtable.Add(hashed_concat_string, nodelist);
            }

        }

        static public void init_portinfo_hashtable(string id, SD sd)
        {

            List<string> nodelist = new List<string>();
            SD_Port sd_ids = (SD_Port)sd;

            string srctype = get_host_type(sd_ids.ip);
            string port = sd_ids.port;


            string hashed_concat_string = srctype + port + sd_ids.status;

            if (portinfo_hashtable.ContainsKey(hashed_concat_string))
            {
                nodelist = (List<string>)portinfo_hashtable[hashed_concat_string];
                nodelist.Add(id);
                portinfo_hashtable.Remove(hashed_concat_string);
                portinfo_hashtable.Add(hashed_concat_string, nodelist);
            }
            else
            {
                nodelist.Add(id);
                portinfo_hashtable.Add(hashed_concat_string, nodelist);
            }
        }


        static public void init_viruslog_hashtable(string id, SD sd)
        {
            List<string> nodelist = new List<string>();
            SD_VirLog sd_ids = (SD_VirLog)sd;

            string srctype = get_host_type(sd_ids.ip);
            string virus = sd_ids.virus;

            string hashed_concat_string = srctype + virus;

            if (antiviruslog_hashtable.ContainsKey(hashed_concat_string))
            {
                nodelist = (List<string>)antiviruslog_hashtable[hashed_concat_string];
                nodelist.Add(id);
                antiviruslog_hashtable.Remove(hashed_concat_string);
                antiviruslog_hashtable.Add(hashed_concat_string, nodelist);
            }
            else
            {
                nodelist.Add(id);
                antiviruslog_hashtable.Add(hashed_concat_string, nodelist);
            }

        }

        static public void init_vlist_hashtable(string id, SD sd)
        {
            List<string> nodelist = new List<string>();
            SD_Vul sd_ids = (SD_Vul)sd;

            if (vulnerability_hashtable.ContainsKey(sd_ids.vul_id))
            {
                nodelist = (List<string>)vulnerability_hashtable[sd_ids.vul_id];
                nodelist.Add(id);
                vulnerability_hashtable.Remove(sd_ids.vul_id);
                vulnerability_hashtable.Add(sd_ids.vul_id, nodelist);
            }
            else
            {
                nodelist.Add(id);
                vulnerability_hashtable.Add(sd_ids.vul_id, nodelist);
            }

        }

        static public void init_webservlog_hashtable(string id, SD sd)
        {
            List<string> nodelist = new List<string>();
            SD_WebServerLog sd_ids = (SD_WebServerLog)sd;

            string ip_addr = sd_ids.src_ip;

            if (webserverlog_hashtable.ContainsKey(get_host_type(ip_addr)))
            {
                nodelist = (List<string>)webserverlog_hashtable[get_host_type(ip_addr)];
                nodelist.Add(id);
                webserverlog_hashtable.Remove(get_host_type(ip_addr));
                webserverlog_hashtable.Add(get_host_type(ip_addr), nodelist);
            }
            else
            {
                nodelist.Add(id);
                webserverlog_hashtable.Add(get_host_type(ip_addr), nodelist);
            }

        }
        

    }

}
