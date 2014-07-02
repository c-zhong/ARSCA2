/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_ConfigData.cs
/// Function:   configuration
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Data;

namespace Experience_Based_Cyber_SA
{
    public class Util_ConfigData
    {
        public enum Enum_Scenario
        {
            SY,
            VAST
        }
        public enum Enum_FileType
        {
            TOPOLOGY_IMG,
            IDS,
            FIREWALL,
            PORT,
            TERM,

        }

        static public int MaxSelection()
        {
            return 1000;
        }

        static public string getCurrentFirewall()
        {
            return "Firewall_shift1";
        }
        static public string getCurrentIDS()
        {
            return "IDS_shift1";
        }
        static public string getDBString()
        {
            return
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Util_All.currentDir() + "SourceData\\VAST.accdb;Persist Security Info=False";
        }
        static public string getFilePath(Enum_Scenario scenario, Enum_FileType filetype)
        {
            if (scenario == Enum_Scenario.SY)
            {
                return "";
            }
            else if (scenario == Enum_Scenario.VAST)
            {
                if (filetype == Enum_FileType.IDS)
                {
                    //return Util_All.currentDir()+"SourceData\\VAST\\Test-IDS.csv";
                    return Util_All.currentDir() + "SourceData\\VAST\\Task4-IDS.csv";
                    //return "..\\..\\SourceData\\VAST\\Task4-IDS.csv";
                }
                else if (filetype == Enum_FileType.FIREWALL)
                {
                   //return Util_All.currentDir()+"SourceData\\VAST\\Test-Firewall.csv";
                   return Util_All.currentDir() + "SourceData\\VAST\\Task4-Firewall.csv";
                    //return Util_All.currentDir()+"SourceData\\VAST\\Task4-Firewall.csv";
                }
                else if (filetype == Enum_FileType.TOPOLOGY_IMG)
                {
                    return Util_All.currentDir()+"SourceData\\VAST\\typology.png";
                }
                else if (filetype == Enum_FileType.PORT)
                {
                    return Util_All.currentDir()+"SourceData\\VAST\\port.txt";
                }
                else if (filetype == Enum_FileType.TERM)
                {
                    return Util_All.currentDir()+"SourceData\\VAST\\term.txt";
                }
            }
            return "";
        }

        static public DataTable getNetConnection()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add("Nodes");
            dt.Columns.Add("WebServer  130.203.50.11", typeof(String));
            dt.Columns.Add("MailServer 130.203.50.22", typeof(String));
            dt.Columns.Add("DNS 130.203.50.2", typeof(String));
            dt.Columns.Add("Database 130.203.157.203", typeof(String));
            dt.Columns.Add("FileServer 130.203.157.212", typeof(String));
            dt.Columns.Add("PC1 130.203.158.101", typeof(String));
            dt.Columns.Add("PC2 130.203.158.102", typeof(String));
            dt.Columns.Add("PC3 130.203.158.103", typeof(String));
            dt.Columns.Add("PC4 130.203.158.104", typeof(String));
            dt.Columns.Add("PC5 130.203.158.105", typeof(String));
            dt.Columns.Add("Enternal 10.1.0.10", typeof(String));



            dt.Rows.Add("WebServer  130.203.50.11","","Y","Y","N","N","Y","Y","Y","Y","Y","Y");
            dt.Rows.Add("MailServer 130.203.50.22","Y","","Y","N","N","Y","Y","Y","Y","Y","Y");
            dt.Rows.Add("DNS 130.203.50.2","Y","Y","","N","N","Y","Y","Y","Y","Y","N");
            dt.Rows.Add("Database 130.203.157.203", "N", "N", "N", "", "N", "Y", "Y", "Y", "Y", "Y","N");
            dt.Rows.Add("FileServer 130.203.157.212","N","N","N","N","","Y", "Y", "Y", "Y", "Y","N");
            dt.Rows.Add("PC1 130.203.158.101","Y","Auth","Y","Auth","Auth", "", "Y", "Y", "Y", "Y","N");
            dt.Rows.Add("PC2 130.203.158.102","Y","Auth","Y","Auth","Auth","Y", "", "Y", "Y", "Y","N");
            dt.Rows.Add("PC3 130.203.158.103","Y","Auth","Y","Auth","Auth","Y", "Y", "", "Y", "Y","N");
            dt.Rows.Add("PC4 130.203.158.104","Y","Auth","Y","Auth","Auth","Y", "Y", "Y", "", "Y","N");
            dt.Rows.Add("PC5 130.203.158.105", "Y", "Auth", "Y", "Auth", "Auth", "Y", "Y", "Y", "Y", "","N");
            dt.Rows.Add("External 10.1.0.10", "Y","Y","N","N","N","N","N","N","N","N", "");

            return dt;
        }

        static public List<String> getNodes()
        {
            List<String> list = new List<String>();
            list.Add("Web Server 130.203.50.11"); //0
            list.Add("Mail Server 130.203.50.22"); //1
            list.Add("DNS 130.203.50.2"); //2
            list.Add("Database 130.203.157.203");//3
            list.Add("File Server 130.203.157.212");//4
            list.Add("PC1 130.203.158.101");//5
            list.Add("PC2 130.203.158.102");//6
            list.Add("PC3 130.203.158.103");//7
            list.Add("PC4 130.203.158.104");//8
            list.Add("PC5 130.203.158.105");//9
            return list;
        }

        static public string getNode(int index)
        {
            switch (index)
            {
                case 0: return "Web Server 130.203.50.11"; 
                case 1: return "Mail Server 130.203.50.22"; 
                case 2: return "DNS 130.203.50.2";
                case 3: return "Database 130.203.157.203";
                case 4: return "File Server 130.203.157.212";
                case 5: return "PC1 130.203.158.101";
                case 6: return "PC2 130.203.158.102";
                case 7: return "PC3 130.203.158.103";
                case 8: return "PC4 130.203.158.104";
                case 9: return "PC5 130.203.158.105";
                default: return "null";
            }
        }

        static public string getIP(int index)
        {
            switch (index)
            {
                case 0: return "130.203.50.11";
                case 1: return "130.203.50.22";
                case 2: return "130.203.50.2";
                case 3: return "130.203.157.203";
                case 4: return "130.203.157.212";
                case 5: return "130.203.158.101";
                case 6: return "130.203.158.102";
                case 7: return "130.203.158.103";
                case 8: return "130.203.158.104";
                case 9: return "130.203.158.105";
                default: return "null";
            }
        }
      
      

    }
}
