/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_FileReader.cs
/// Function:   
/// Note:    also include some search functions
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

using System.Xml;

using System.ComponentModel;
using System.Drawing;
using System.Data.OleDb;


namespace Experience_Based_Cyber_SA
{
    public class Util_FileReader
    {

        #region TXT Data Source



        static public ArrayList loadAlerts()
        {
            StreamReader objReader = new StreamReader(Util_All.currentDir()+"SourceData\\snort alerts1.txt");
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            // MessageBox.Show(LineList.ToString());

            return LineList;
        }

        static public ArrayList loadAlerts(string IDSfilepath)
        {
            StreamReader objReader = new StreamReader(IDSfilepath);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            // MessageBox.Show(LineList.ToString());

            return LineList;
        }

        

        static public DataTable loadAlertsTable()
        {
            ArrayList list = loadAlerts();
            DataTable dt = new DataTable();
            if (list != null)
            {
                dt.Columns.Add("Date");
                dt.Columns.Add("Time");
                dt.Columns.Add("SourceIP");
                dt.Columns.Add("DestinationIP");
                dt.Columns.Add("SnortID");
                dt.Columns.Add("Description");

                int flag = 0;

                if (Partial_Ordering.ids_sequence.Count == 0)
                    flag = 1;

                foreach (String item in list)
                {
                    String[] itemarray = Regex.Split(item, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    for (i = 0; i < 5; i++)
                    {
                        dr[i] = itemarray[i];
                    }
                    dr[5] = "";
                    for (i = 5; i < itemarray.Length; i++)
                    {
                        dr[5] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);

                    if (flag == 1)
                    {
                        Partial_Ordering.ids_sequence.Add(dr[4].ToString());
                        Partial_Ordering.ids_des_sequence.Add(dr[5].ToString());
                    }

                }
            }
            return dt;
        }


        #region CSV Reader [6-15 NEW IDS]

        static public DataTable loadAlertsTable(string IDSfilepath)
        {
            StreamReader objReader = new StreamReader(IDSfilepath);
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("DataTime");               //0
            dt.Columns.Add("SourceIP");               //1
            dt.Columns.Add("DestIP");          //2
            dt.Columns.Add("Category");          //3
            dt.Columns.Add("Priority");                //4
            dt.Columns.Add("Description");             //5
            dt.Columns.Add("SourcePort");             //6
            dt.Columns.Add("DestPort");        //7

            objReader.Peek();

            while (objReader.Peek()>0)
            {
                sLine = objReader.ReadLine();

                if (!sLine.Equals(""))
                {
                    //String[] itemarray = Regex.Split(sLine, @"\s+");
                    String[] itemarray = sLine.Split(',');
                    DataRow dr = dt.NewRow();

                    dr[0] = itemarray[0];
                    dr[1] = itemarray[1];
                    dr[2] = itemarray[3];
                    dr[3] = itemarray[5];
                    dr[4] = itemarray[6];
                    dr[5] = itemarray[7];
                    dr[6] = itemarray[2];
                    dr[7] = itemarray[4];

                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }
        
        //[VAST-DATA 4-1]
        static public DataTable loadFirewallTable(string filepath)
        {
            StreamReader objReader = new StreamReader(filepath);
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("DataTime");            //0   itemarray 0     
            dt.Columns.Add("Priority");             //1   itemarray 1          
            dt.Columns.Add("Operation");            //2   itemarray 2

            dt.Columns.Add("Protocol");             //3   itemarray 4
            dt.Columns.Add("SrcIP");               //4    5
            dt.Columns.Add("DesIP");               //5    6
            dt.Columns.Add("SrcPort");             //6    9
            dt.Columns.Add("DesPort");             //7    10
            dt.Columns.Add("DestService");  //8   11
            dt.Columns.Add("Direction");            //9   12

            dt.Columns.Add("MsgCode");             //10 itemarray 3

            objReader.Peek();

            while (objReader.Peek() > 0)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = sLine.Split(',');
                    DataRow dr = dt.NewRow();

                    dr[0] = itemarray[0];
                    dr[1] = itemarray[1];
                    dr[2] = itemarray[2];
                    dr[3] = itemarray[4];
                    dr[4] = itemarray[5];
                    dr[5] = itemarray[6];
                    dr[6] = itemarray[9];
                    dr[7] = itemarray[10];
                    dr[8] = itemarray[11];
                    dr[9] = itemarray[12];
                    dr[10] = itemarray[3];

                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }
        
        #endregion

        /*
        static public DataTable loadAlertsTable(string IDSfilepath)
        {
            ArrayList list = loadAlerts(IDSfilepath);
            DataTable dt = new DataTable();
            if (list != null)
            {
                dt.Columns.Add("Date");
                dt.Columns.Add("Time");
                dt.Columns.Add("Source IP");
                dt.Columns.Add("Destination IP");
                dt.Columns.Add("Classification");
                dt.Columns.Add("Description");
                dt.Columns.Add("Priority");
                dt.Columns.Add("Snort ID");
                dt.Columns.Add("Source Port");
                dt.Columns.Add("Destination Port");
                

                int flag = 0;

                if (Partial_Ordering.ids_sequence.Count == 0)
                    flag = 1;

                foreach (String item in list)
                {
                    String[] itemarray = Regex.Split(item, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    for (i = 0; i < 5; i++)
                    {
                        dr[i] = itemarray[i];
                    }
                    dr[5] = "";
                    for (i = 5; i < itemarray.Length; i++)
                    {
                        dr[5] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);

                    if (flag == 1)
                    {
                        Partial_Ordering.ids_sequence.Add(dr[4].ToString());
                        Partial_Ordering.ids_des_sequence.Add(dr[5].ToString());
                    }

                }
            }
            return dt;
        }
*/

        
        
        static public DataTable loadPacketDump()
        {
            StreamReader objReader = new StreamReader(Util_All.currentDir()+"SourceData\\packet dump.txt");
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Time");
            dt.Columns.Add("Source IP");
            dt.Columns.Add("Destination IP");
            dt.Columns.Add("Proto");
            dt.Columns.Add("Information");

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = Regex.Split(sLine, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    for (i = 0; i < 5; i++)
                    {
                        dr[i] = itemarray[i];
                    }
                    dr[5] = "";
                    for (i = 5; i < itemarray.Length; i++)
                    {
                        dr[5] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }


        static public DataTable loadWebServerLog()
        {
            StreamReader objReader = new StreamReader(Util_All.currentDir()+"SourceData\\webserver log.txt");
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Time");
            dt.Columns.Add("IP");
            dt.Columns.Add("Information");

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = Regex.Split(sLine, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    dr[0] = itemarray[0] + " " + itemarray[1];
                    dr[1] = itemarray[2];
                    dr[2] = itemarray[3];
                    dr[3] = "";
                    for (i = 4; i < itemarray.Length; i++)
                    {
                        dr[3] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }

        static public DataTable loadAuthLog()
        {
            StreamReader objReader = new StreamReader(Util_All.currentDir()+"SourceData\\auth log.txt");
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Time");
            dt.Columns.Add("Type");
            dt.Columns.Add("IP");
            dt.Columns.Add("Information");

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = Regex.Split(sLine, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    dr[0] = itemarray[0];
                    dr[1] = itemarray[1];
                    dr[2] = itemarray[2];
                    dr[3] = itemarray[4];
                    dr[4] = "";
                    for (i = 5; i < itemarray.Length; i++)
                    {
                        dr[4] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }

        static public DataTable loadAntiVirLog(int index)
        {
            String file = "";
            if (index == 5 || index == 6 || index == 7 || index == 8 || index == 9)
            {
                file = Util_All.currentDir()+"SourceData\\anti-vir log pc" + (index - 4) + ".txt";
            }

            StreamReader objReader = new StreamReader(file);
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Information");

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = Regex.Split(sLine, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    dr[0] = itemarray[0];
                    for (i = 1; i < itemarray.Length; i++)
                    {
                        dr[1] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }

        static public DataTable loadVul(int index)
        {
            String file = "";

            if (index == 5 || index == 6 || index == 7 || index == 8 || index == 9)
            {
                file = Util_All.currentDir()+"SourceData\\vul pc" + (index - 4) + ".txt";
            }
            if (index == 0)
            {
                file = Util_All.currentDir()+"SourceData\\vul web server.txt";
            }
            else if (index == 1)
            {
                file = Util_All.currentDir()+"SourceData\\vul mail server.txt";
            }
            else if (index == 2)
            {
                file = Util_All.currentDir()+"SourceData\\vul DNS.txt";
            }
            else if (index == 3)
            {
                file = Util_All.currentDir()+"SourceData\\vul database.txt";
            }
            else if (index == 4)
            {
                file = Util_All.currentDir()+"SourceData\\vul file server.txt";
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("Vul ID");
            dt.Columns.Add("Description");

            StreamReader objReader = new StreamReader(file);
            string sLine = "";
            String dr0 = "";
            String dr1 = "";
            int count = 0;
            while (sLine != null)
            {
                sLine = objReader.ReadLine();

                if (sLine != null && !sLine.Equals(""))
                {
                    count++;
                    String[] itemarray = Regex.Split(sLine, @"[#]+");

                    if (itemarray.Length == 2)
                    {
                        if (count > 1)
                        {
                            dt.Rows.Add(dr0, dr1);
                        }
                        dr0 = itemarray[0];
                        dr1 += itemarray[1] + "\r\n";
                    }
                    else
                    {
                        dr1 += itemarray[0] + "\r\n";
                    }
                }

            }
            dt.Rows.Add(dr0, dr1);
            objReader.Close();
            return dt;
        }


        static public DataTable loadPortInfo(int index)
        {
            // MessageBox.Show(index.ToString());
            String file = "";

            if (index == 5 || index == 6 || index == 7 || index == 8 || index == 9)
            {
                file = Util_All.currentDir()+"SourceData\\port pc" + (index - 4) + ".txt";
            }
            if (index == 0)
            {
                file = Util_All.currentDir()+"SourceData\\port web server.txt";
            }
            else if (index == 1)
            {
                file = Util_All.currentDir()+"SourceData\\port mail server.txt";
            }
            else if (index == 2)
            {
                file = Util_All.currentDir()+"SourceData\\port DNS.txt";
            }
            else if (index == 3)
            {
                file = Util_All.currentDir()+"SourceData\\port database.txt";
            }
            else if (index == 4)
            {
                file = Util_All.currentDir()+"SourceData\\port file server.txt";
            }

            //MessageBox.Show(file.ToString());

            StreamReader objReader = new StreamReader(file);
            string sLine = "";
            DataTable dt = new DataTable();

            dt.Columns.Add("Port");
            dt.Columns.Add("State");
            dt.Columns.Add("Service");
            dt.Columns.Add("Version");

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    String[] itemarray = Regex.Split(sLine, @"\s+");
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    dr[0] = itemarray[0];
                    dr[1] = itemarray[1];
                    dr[2] = itemarray[2];
                    dr[3] = "";
                    for (i = 3; i < itemarray.Length; i++)
                    {
                        dr[3] += itemarray[i] + " ";
                    }
                    dt.Rows.Add(dr);
                }
            }
            objReader.Close();
            return dt;
        }

        #endregion

        #region XML

        /*
        static public String loadExperienceFile()
        {
            String str = "";
            StreamReader objReader = new StreamReader("..\\..\\Experience\\experience.xml");
            string sLine = "";
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    str += sLine + "\r\n";
                }
            }
            return str;
        }
        */


        public static void setTreeRootNode(XmlNode rootXml, TreeNode rootTree)
        {
            rootTree.Text = "Tree-" + rootXml.Attributes["ID"].Value;
            rootTree.Tag = "N";
            rootTree.ToolTipText = "N";
        }


        //used for Capture
        static public void loadExperienceTree(TreeView treeView)
        {
            treeView.Nodes.Clear();
            treeView.BeginUpdate();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Util_All.currentDir() + "Experience\\experience_tree.xml");

            XmlNodeList list = xmlDoc.GetElementsByTagName("Experience_Tree");


            foreach (XmlNode node in list)
            {
                //one tree
                TreeNode treeNode = new TreeNode();
                setTreeRootNode(node, treeNode);

                loadExperienceTreeChildNodes(node, treeNode);
                treeView.Nodes.Add(treeNode);
            }

            treeView.EndUpdate();
            treeView.Refresh();
        }

        public static void loadExperienceTreeChildNodes(XmlNode xmlNode, TreeNode treeNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                TreeNode child = new TreeNode();
                string id = node.Attributes["ID"].Value;
                child.Text = id;

                if (id.Substring(0, 2).Equals("H-"))
                {
                    //hypo.id = id;
                    Con_Hypo hypo = new Con_Hypo();
                    hypo.id = id;
                    hypo.hypo = node.Attributes["detail"].Value;
                    hypo.type = Int16.Parse(node.Attributes["type"].Value);
                    child.Tag = hypo;
                    hypo.updateID();
                }
                else
                {
                    child.Tag = node.Attributes["detail"].Value;
                }
                child.ToolTipText = node.Attributes["truth_value"].Value;

                loadExperienceTreeChildNodes(node, child);

                treeNode.Nodes.Add(child);
            }
        }


        //[06-1 UI]
        static public void loadExperienceDetail(TreeView treeView, Con_Experience_Unit eu)
        {
            //List<frmSnippedImageViewer> imglist = new List<frmSnippedImageViewer>();

            if (eu == null)
            {
                MessageBox.Show("Sorry, no details found.");
                return;
            }

            treeView.Nodes.Clear();
            treeView.BeginUpdate();

            TreeNode treeNode = new TreeNode();
            treeNode.Text = eu.id;

            //Act

            TreeNode actNode = new TreeNode();
            actNode.Text = "Action";
            Con_Actions act = eu.act;
            if (act != null)
            {
                foreach (SD sd in act.sds)
                {
                    TreeNode sdNode = new TreeNode();
                    sdNode.Text = sd.getType();

                    sdNode.Text += "-" + sd.toActString();
                    actNode.Nodes.Add(sdNode);
                }
            }
            treeNode.Nodes.Add(actNode);

            //Obs

            TreeNode obsNode = new TreeNode();
            obsNode.Text = "Observation";
            Con_Observation obs = eu.obs;
            if (obs != null)
            {
                foreach (SD obssd in obs.sds)
                {
                    TreeNode sdObsNode = new TreeNode();
                    sdObsNode.Text = obssd.getType();
                    if (obssd.type == Enum_SDType.SDType.IMAGE)
                    {
                        SD_Image sd_img = (SD_Image)obssd;
                        frmSnippedImageViewer imageviewer = new frmSnippedImageViewer(sd_img.imagepath, sd_img);
                        imageviewer.Show();
                  
                    }
                    sdObsNode.Text += "-" + obssd.toObsString();
                 
                    obsNode.Nodes.Add(sdObsNode);
                }
            }
            treeNode.Nodes.Add(obsNode);


            treeView.Nodes.Add(treeNode);
            treeView.EndUpdate();
            treeView.Refresh();
        }


        static public void loadExperienceDetail(TreeView treeView, string id)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Util_All.currentDir() + "Experience\\experience.xml");
            string xpath = "/Experiences/Experience_Unit[@ID=\"" + id + "\"]";
            XmlNode sameId = xmlDoc.SelectSingleNode(xpath);

            if (sameId == null)
            {
                MessageBox.Show("Sorry, no details found.");
                return;
            }

            treeView.Nodes.Clear();
            treeView.BeginUpdate();

            TreeNode treeNode = new TreeNode();
            treeNode.Text = sameId.Attributes["ID"].Value;

            //Act
            foreach (XmlNode xmlNode in sameId.SelectNodes("Action"))
            {
                TreeNode actNode = new TreeNode();
                actNode.Text = "Action";
                foreach (XmlNode sdXml in xmlNode.SelectNodes("SD"))
                {
                    TreeNode sdNode = new TreeNode();
                    sdNode.Text = sdXml.Attributes["type"].Value;
                    //sdNode.Tag = sdXml.InnerText;
                    //sdNode.Text += "-"+ sdXml.InnerText;
                    sdNode.Text += "-" + sdXml.Attributes["content"].Value;
                    actNode.Nodes.Add(sdNode);
                }
                treeNode.Nodes.Add(actNode);
            }
            //Obs
            foreach (XmlNode obsXml in sameId.SelectNodes("Observation"))
            {
                TreeNode obsNode = new TreeNode();
                obsNode.Text = "Observation";
                foreach (XmlNode obsSdXml in obsXml.SelectNodes("SD"))
                {
                    TreeNode sdObsNode = new TreeNode();
                    sdObsNode.Text = obsSdXml.Attributes["type"].Value;
                    sdObsNode.Text += "-" + obsSdXml.Attributes["content"].Value;
                    obsNode.Nodes.Add(sdObsNode);
                }
                treeNode.Nodes.Add(obsNode);
            }

            treeView.Nodes.Add(treeNode);
            treeView.EndUpdate();
            treeView.Refresh();
        }

        static public void addEUIDinEUSList(List<Util_EU_Score> list, string euID)
        {
            bool contain = false;
            foreach (Util_EU_Score item in list)
            {
                if (item.isSameEUID(euID))
                {
                    item.score++;
                    contain = true;
                    break;
                }
            }
            if (!contain)
            {
                Util_EU_Score newItem = new Util_EU_Score(euID);
                list.Add(newItem);
            }
        }

        static private bool listContainsId(List<string> treeList, string id)
        {
            foreach (string treeid in treeList)
            {
                if (treeid.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }
        static private void addIDList(List<string> treeList, string id)
        {
            bool contains = false;
            foreach (string treeid in treeList)
            {
                if (treeid.Equals(id))
                {
                    contains = true;
                    break;
                }
            }
            if (!contains)
            {
                treeList.Add(id);
            }
        }


        /// <summary>
        ///transfer xmlNode to EU detail 
        ///used for search
        ///because action is not needed for search, we don't load action
        ///so eu is INCOMPLETE! but enough for search
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="eu"></param>
        static public void xmlNode2EU(XmlNode xml, Con_Experience_Unit eu)
        {
            //MessageBox.Show("I am " + xml.Attributes["ID"].Value);
            eu.id = xml.Attributes["ID"].Value;
            XmlNode xmlObs = xml.SelectSingleNode("Observation");
            if (xmlObs != null)
            {
                eu.obs = new Con_Observation();
                XmlNodeList xmlSDs = xmlObs.SelectNodes("SD");
                foreach (XmlNode xmlSD in xmlSDs)
                {
                    string type = xmlSD.Attributes["type"].Value;
                    string content = xmlSD.Attributes["content"].Value;
                    SD sd = SD.transfer2SD(type, content);
                    eu.obs.sds.Add(sd);
                }
            }


        }


        static public void setETreeNode(XmlNode xml, TreeNode node, XmlDocument xmlDoc)
        {
            string id = xml.Attributes["ID"].Value;
            string detail = xml.Attributes["detail"].Value;
            node.Text = id;
            node.ToolTipText = xml.Attributes["truth_value"].Value;
            if (id.Substring(0, 2).Equals("H-"))
            {
                string type = xml.Attributes["type"].Value;
                Con_Hypo hypo = new Con_Hypo(detail, Int16.Parse(type));
                hypo.id = id;
                node.Tag = hypo;
            }
            else if (id.Substring(0, 3).Equals("EU-"))
            {
                //MessageBox.Show("I am here " +  id);
                //generate EU detail and attach it as a tag of the TreeNode
                Con_Experience_Unit eu = new Con_Experience_Unit();
                eu.id = id;
                string xpath = "/Experiences/Experience_Unit[@ID=\"" + id + "\"]";
                XmlNode sameId = xmlDoc.SelectSingleNode(xpath);
                xmlNode2EU(sameId, eu);
                node.Tag = eu;
            }

        }


        static public void load_etree()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(Util_All.currentDir() + "Experience\\experience_tree.xml");

            XmlNodeList list = xmlDoc.GetElementsByTagName("Experience_Tree");


            foreach (XmlNode node in list)
            {

                foreach (XmlNode eunode in node.SelectNodes("Experience_Unit"))
                {
                    foreach (Con_Experience_Unit curr_eu in frmMain.EUList)
                    {
                        if (curr_eu.id.Equals(eunode.Attributes["ID"].Value))
                        {
                            curr_eu.isroot = 1;

                            XmlNodeList hypolist = eunode.SelectNodes("Hypothesis");

                            set_next_hlist(curr_eu, hypolist);
                            recurs_to_leaf(curr_eu, hypolist, xmlDoc);
                            //  foreach (Con_Experience_Unit unit in curr_eu.nextEUList)
                            //      MessageBox.Show(" Parent: " + curr_eu.id + " Child: " + unit.id);
                        }
                    }

                }
            }
        }


        static public void set_next_hlist(Con_Experience_Unit eu, XmlNodeList hlist)
        {
            List<Con_Hypo> hypolist = new List<Con_Hypo>();

            foreach (XmlNode hnode in hlist)
            {
                string id = hnode.Attributes["ID"].Value;
                string detail = hnode.Attributes["detail"].Value;
                string type = hnode.Attributes["type"].Value;
                Con_Hypo hypo = new Con_Hypo(detail, Int16.Parse(type));
                hypo.id = id;
                hypolist.Add(hypo);
            }

            eu.nextHypoList = hypolist;

        }

        static public void recurs_to_leaf(Con_Experience_Unit curr_eu, XmlNodeList hypolist, XmlDocument xmlDoc)
        {
            List<XmlNode> temp_xml_mode_list = new List<XmlNode>();
            List<Con_Experience_Unit> temp_eu_list = new List<Con_Experience_Unit>();

            if (hypolist == null || curr_eu == null)
            {
            }

            else
            {

                foreach (XmlNode hyp_node in hypolist)
                {
                    XmlNode eu_child_node = hyp_node.SelectSingleNode("Experience_Unit");

                    if (eu_child_node == null)
                        break;

                    string euid = eu_child_node.Attributes["ID"].Value;

                    foreach (Con_Experience_Unit eu in frmMain.EUList)
                    {
                        if (eu.id.Equals(euid))
                        {
                            eu.pre_EU = curr_eu;
                            curr_eu.nextEUList.Add(eu);
                            curr_eu.MatchingDegreeOfSubtrees.Add(0);
                            temp_xml_mode_list.Add(eu_child_node);
                            temp_eu_list.Add(eu);
                        }
                    }

                }
            }

            if (temp_eu_list != null)
            {
                int i = 0;

                foreach (Con_Experience_Unit next_eu in temp_eu_list)
                {
                    XmlNode next_node = temp_xml_mode_list.ElementAt(i);
                    XmlNodeList hyplist = next_node.SelectNodes("Hypothesis");
                    set_next_hlist(next_eu, hyplist);
                    recurs_to_leaf(next_eu, hyplist, xmlDoc);
                    ++i;
                }
            }
        }



        //Deepak initial hash
        /*
        static public void loadExperienceInitialHash()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("..\\..\\Experience\\experience.xml");

            XmlNodeList list = xmlDoc.GetElementsByTagName("Experience_Unit");


            foreach (XmlNode node in list)
            {
                Con_Experience_Unit eu = new Con_Experience_Unit();
                xmlNode2EU(node, eu);

                //MessageBox.Show("Adding:" + eu.id + "!");

                frmMain.EUList.Add(eu);
                HashTableLookup.EU_lookup_init(eu);
                eu.MatchingDegreeOfSubtrees.DefaultIfEmpty(0);

                String euid = eu.id;

                foreach (SD sd in eu.obs.sds)
                {
                    if (sd.type == Enum_SDType.SDType.IDSLOG)
                        HashTableLookup.init_snort_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.PACKETDUMP)
                        HashTableLookup.init_pkdump_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.AUTHLOG)
                        HashTableLookup.init_authlog_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.NETCON)
                        HashTableLookup.init_netconn_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.PORT)
                        HashTableLookup.init_portinfo_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.VIRLOG)
                        HashTableLookup.init_viruslog_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.VUL)
                        HashTableLookup.init_vlist_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.WEBSERVERLOG)
                        HashTableLookup.init_webservlog_hashtable(euid, sd);
                    else if (sd.type == Enum_SDType.SDType.ALERTS_RELA)
                        HashTableLookup.alert_corr_euidlist.Add(euid);
                }
            }
        }
        */

        //search for a list of ETs based on an obs
        static public void searchExperienceTree(Con_Observation obs, List<Util_ETN_Score> searchedETNList)
        {
            // List<Util_ETN_Score> trees = new List<Util_ETN_Score>();

            XmlDocument xmlDetail = new XmlDocument();

            xmlDetail.Load(Util_All.currentDir() + "Experience\\experience.xml");


            List<Util_EU_Score> results = new List<Util_EU_Score>();

            foreach (SD sd in obs.sds)
            {
                string detailxPath = "//Observation/SD[@type=\"" + sd.getType() + "\" and " + "@content=\"" + sd.toOriginalString() + "\"]";
                XmlNodeList list = xmlDetail.SelectNodes(detailxPath);
                foreach (XmlNode sdXmlNode in list)
                {
                    XmlNode euXmlNode = sdXmlNode.ParentNode.ParentNode;
                    addEUIDinEUSList(results, euXmlNode.Attributes["ID"].Value);
                }
            }
            results.Sort(new Util_EU_Compare());

            //result : sorted list of EU id

            XmlDocument xmlTree = new XmlDocument();
            xmlTree.Load(Util_All.currentDir() + "Experience\\experience_tree.xml");

            foreach (Util_EU_Score item in results)
            {
                string treexPath = "/Experience_Trees/Experience_Tree/Experience_Unit[@ID=\"" + item.euID + "\"]";

                XmlNodeList rootlist = xmlTree.SelectNodes(treexPath);

                if (searchedETNList == null)
                    searchedETNList = new List<Util_ETN_Score>();

                List<string> treeList = new List<string>();

                foreach (XmlNode rootEuXml in rootlist)
                {
                    XmlNode treeXml = rootEuXml.ParentNode;
                    string treeXmlId = treeXml.Attributes["ID"].Value;

                    if (!listContainsId(treeList, treeXmlId))
                    {
                        treeList.Add(treeXmlId);

                        //new E-Tree TreeNode and score
                        Util_ETN_Score newETS = new Util_ETN_Score();
                        //TreeNode rootTree = new TreeNode();
                        setTreeRootNode(treeXml, newETS.tree);


                        //new root
                        TreeNode rootEuTree = new TreeNode();
                        Con_Experience_Unit root = new Con_Experience_Unit();
                        //rootEuTree.Text = rootEuXml.Attributes["ID"].Value;
                        //newETS.tree.Nodes.Add(rootEuTree);
                        setETreeNode(rootEuXml, rootEuTree, xmlDetail);

                        loadETNNodes(rootEuXml, rootEuTree, xmlDetail);

                        newETS.tree.Nodes.Add(rootEuTree);

                        searchedETNList.Add(newETS);
                    }

                }

            }

        }

        //also generate Experience_Unit and Hypo, attach them in the tag of each TreeNode
        static private void loadETNNodes(XmlNode parentXml, TreeNode parentTree, XmlDocument xmlDoc)
        {
            foreach (XmlNode node in parentXml.ChildNodes)
            {
                TreeNode treeNode = new TreeNode();
                setETreeNode(node, treeNode, xmlDoc);

                loadETNNodes(node, treeNode, xmlDoc);

                parentTree.Nodes.Add(treeNode);
            }
        }





        #endregion

        #region DATABASE

        public static DataTable dbLoadData(string file)
        {
            string connString = Util_ConfigData.getDBString();

            DataTable results = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM "+file, conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(results);
            }
            return results;
        }
        #endregion
    }



}
