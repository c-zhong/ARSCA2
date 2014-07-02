/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_All.cs
/// Function:   get makers, get details
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;

namespace Experience_Based_Cyber_SA
{
    public class Util_All
    {
        public static string h_tree_root = "START_POINT";
        public static string e_tree_root = "NULL_ACTION_OBSERVATION";
        public static string init_h = "REASONING_BRANCH";

        public static String currentDir()
        {
            return "..\\..\\Bin\\Release\\";
        }
         // [6-25 TERM/PORT LOOKUP ]
        public static string lookupTermInfo(string term) 
        {
            string output = "";

             StreamReader objReader = new StreamReader(Util_ConfigData.getFilePath(Util_ConfigData.Enum_Scenario.VAST, Util_ConfigData.Enum_FileType.TERM));

            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                //count++;
                if (sLine != null && !sLine.Equals(""))
                {
                    string[] array = sLine.Split(':');
                    if (array != null && array.Length > 0)
                    {
                        if (array[0].ToLower().Contains(term) || term.Contains(array[0].ToLower()))
                        {
                            output += sLine + "\n\r";
                        }
                    }
                }
            }

            return output;

        }

        // [6-25 TERM/PORT LOOKUP ]
        public static string lookupPortInfo(int port)
        {
            string output = "";
            //int count = 0; 
            StreamReader objReader = new StreamReader(Util_ConfigData.getFilePath(Util_ConfigData.Enum_Scenario.VAST, Util_ConfigData.Enum_FileType.PORT));

            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                //count++;
                if (sLine != null && !sLine.Equals(""))
                {
                    string[] array = sLine.Split('\t');
                    if (array != null && !array[0].Equals(""))
                    {
                        if (array[0].Equals("6543"))
                            output += " ";
                        string[] numberArray = array[0].Split('-');
                        if (numberArray.Length == 1)
                        {
                            int num = Convert.ToInt32(numberArray[0]);
                           
                            if (num == port)
                            {
                                output += sLine + "\n\r";
                            }
                        }
                        else if (numberArray.Length == 2)
                        {
                            int num1 = Convert.ToInt32(numberArray[0]);
                            int num2 = Convert.ToInt32(numberArray[1]);
                            if (port >= num1 && port <= num2)
                            {
                                output += sLine + "\n\r";
                            }
                        }
                    }
                }
                    
            }
            objReader.Close();

            return output;

        }

        //[6-11 UI ADV]
        public static string getImageName(String path)
        {
            if (path.Trim().Equals(""))
            {
                return "";
            }
            String[] array = path.Split('\\');
            return array.Last();
        }

        #region store image
        //[6-01 SNIP]
        public static String storeImage(Image image)
        {
            if (image == null)
                return "";
            else
            {
                string path = Util_All.currentDir()+"Obs-Image\\" + generateID("S") + ".png";
                image.Save(path);
                return path;
            }

        }
        //[6-15 UI ILIKE]
        public static string storeIlikeImage(Image image)
        {
            if (image == null)
                return "";
            else
            {
                string path = Util_All.currentDir()+"\\Ilike-Image\\" + generateID("L") + ".png";
                image.Save(path);
                return path;
            }
        }
        #endregion

        #region Marker
        public static string getCurrentMarker()
        {
            return " ■";
        }
        public static string getMissingMarker()
        {
            return " 【?】";
        }
        public static string getFalseMarker()
        {
            return " 【-】";
        }

        
        public static string trimMarker(string str)
        {
            string output = "";

            output += str.Replace(getCurrentMarker(), "");
            output = output.Replace(getMissingMarker(), "");
            output = output.Replace(getFalseMarker(), "");
            return output;

        }
        public static void trimTreeMarker(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Text = trimMarker(node.Text);
                trimTreeMarker(node.Nodes);
                node.ImageIndex = 0;
            }
        }
        #endregion

        #region generate ID
        public static String generateID(String head)
        {
            //[4-08 GRAPHVIZ]
            //id can't contain "-" otherwise graphviz will report error
            //original: String str = head + "-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + DateTime.Now.Millisecond;
            String str = head + "  ("+ DateTime.Now.Millisecond + DateTime.Now.ToString("ddss");
            Random random = new Random();
            str += random.Next(1, 10) + ")";
            return str;
        }
        #endregion

        public static string currentTime()
        {
            //return DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return DateTime.Now.ToString("MM/dd HH:mm:ss");
        }


       public static DataTable getHypoDetail(Con_Hypo hypo)
        {
           DataTable dt = new DataTable();
           /*
           dt.Columns.Add("T/F");
           dt.Columns.Add("ID");
           dt.Columns.Add("Content");
           DataRow dr = dt.NewRow();
           dr["T/F"] = hypo.truthValue;
           dr["ID"] = hypo.id;
           dr["Content"] = hypo.hypo;

           dt.Rows.Add(dr);
            
           return dt;
           */
           dt.Columns.Add("Name");
           dt.Columns.Add("Value");
           DataRow dr = dt.NewRow();
           dr["Name"] = "T/F";
           dr["Value"] = hypo.truthValue;
           dt.Rows.Add(dr);
           dr = dt.NewRow();
           dr["Name"] = "ID";
           dr["Value"] = hypo.id;
           dt.Rows.Add(dr);
           dr = dt.NewRow();
           dr["Name"] = "Description";
           dr["Value"] = hypo.hypo;
           dt.Rows.Add(dr);
           return dt;
        }

       #region Missing alert
       static public void addMissingColumns(DataTable dt)
       {
           if (dt == null)
               dt = new DataTable();

           dt.Columns.Add("Time");
           dt.Columns.Add("Source");
           dt.Columns.Add("Destination");
           dt.Columns.Add("Description");
           dt.Columns.Add("Details");

       }
        static public void addMissingAlert(DataTable dt, Con_MissingAlert missing)
        {
            if (dt == null)
            {
                dt = new DataTable();
                addMissingColumns(dt);
            }

            DataRow dr = dt.NewRow();
            dr["Time"] = missing.time;
            dr["Source"] = missing.src;
            dr["Destination"] = missing.des;
            dr["Description"] = missing.hypo.hypo;
            dr["Details"] = missing.description;
            dt.Rows.Add(dr);
        }

       static public Con_MissingAlert constructMissingAlert(Con_Hypo hypo)
       {
           Con_MissingAlert missing = new Con_MissingAlert();
           missing.hypo = hypo;

           List<string> srcIp = new List<string>();
           List<string> desIp = new List<string>();
           List<string> time = new List<string>();

           Con_Experience_Unit nextEu = hypo.next_unit;
           if (nextEu == null)
               return null;

           foreach(SD sd in nextEu.obs.sds)
           {
               switch (sd.type)
               {
                   case Enum_SDType.SDType.IDSLOG:
                       {
                           SD_IDSlog sd2=(SD_IDSlog)sd;
                           if(!srcIp.Contains(sd2.src_ip))
                               srcIp.Add(sd2.src_ip);
                           if(!desIp.Contains(sd2.des_ip))
                               desIp.Add(sd2.des_ip);
                           time.Add(sd2.time);
                       }break;
                   case Enum_SDType.SDType.NETCON:
                       {
                           SD_NetCon sd2=(SD_NetCon)sd;
                           if(!srcIp.Contains(sd2.src_ip))
                               srcIp.Add(sd2.src_ip);
                           if(!desIp.Contains(sd2.des_ip))
                               desIp.Add(sd2.des_ip);

                       }break;
                   case Enum_SDType.SDType.PACKETDUMP:
                       {
                           SD_PacketDump sd2 = (SD_PacketDump)sd;
                           if(!srcIp.Contains(sd2.src_ip))
                               srcIp.Add(sd2.src_ip);
                           if(!desIp.Contains(sd2.des_ip))
                               desIp.Add(sd2.des_ip);
                           time.Add(sd2.time);
                       }break;
                   case Enum_SDType.SDType.PORT:
                       {
                           SD_Port sd2 = (SD_Port)sd;
                           if(!desIp.Contains(sd2.ip))
                               desIp.Add(sd2.ip);

                           missing.description += "Alert about "+sd.toObsString()+"; ";
                           
                       }break;
                   case Enum_SDType.SDType.VIRLOG:
                       {
                           SD_VirLog sd2 = (SD_VirLog)sd;
                           if(!desIp.Contains(sd2.ip))
                               desIp.Add(sd2.ip);
                           missing.description += "Alert about" + sd2.toObsString()+"; ";
                           time.Add(sd2.time);
                       }break;
                   case Enum_SDType.SDType.VUL:
                       {
                           SD_Vul sd2 = (SD_Vul)sd;
                           if (!desIp.Contains(sd2.ip))
                               desIp.Add(sd2.ip);
                           missing.description += "Alert about" + sd2.toObsString() + "; ";
                          // time.Add(sd2.time);
                       }break;
                   case Enum_SDType.SDType.WEBSERVERLOG:
                       {
                           SD_WebServerLog sd2 = (SD_WebServerLog)sd;
                           if (!srcIp.Contains(sd2.src_ip))
                               srcIp.Add(sd2.src_ip);
                           time.Add(sd2.time);
                       }break;
               }
           }
           foreach (string src in srcIp)
           {
               missing.src += src + "; ";
           }
           foreach (string des in desIp)
           {
               missing.des += des + "; ";
           }
           if (time.Count > 1)
               missing.time += time[0];

           return missing;

       }
       #endregion

       static public string checkNodeType(string id)
       {
           char[] array = id.ToArray();
           if (array[0] == 'E' && array[1] == 'U')
           {
               return "EU";
           }
           else if (array[0] == 'H')
           {
               return "H";
           }
           else
           {
               return "Other";
           }
       }

       static public void setTreeNodeFont(Font f, TreeNode node, int truthvalue)
       {
           
           if (truthvalue == -1)
           {
               node.NodeFont = new Font(f, FontStyle.Strikeout);
               //node.BackColor = Color.LightSalmon;
               node.BackColor = Color.LightYellow;
           }
           else if (truthvalue == 0)
           {
               node.NodeFont = new Font(f, FontStyle.Regular);
               node.BackColor = Color.LightYellow;
           }
           else if (truthvalue == 1)
           {
               node.NodeFont = new Font(f, FontStyle.Underline);
               //node.BackColor = Color.LightGreen;
               node.BackColor = Color.LightYellow;
           }
           else 
           {
               node.NodeFont = new Font(f, FontStyle.Regular);
               node.BackColor = Color.White;
           }

           bool isgrey = false;

           if (node.Text.Length >= init_h.Length)
           {
               if (node.Text.Substring(0, init_h.Length).Equals(Util_All.init_h))
               {
                   isgrey = true;
               }

           }
           if (!isgrey && node.Text.Length >= e_tree_root.Length)
           {
               if (node.Text.Substring(0, e_tree_root.Length).Equals(e_tree_root))
               {
                   isgrey = true;
               }
           }
           if (!isgrey && node.Text.Length >= h_tree_root.Length)
           {
               if (node.Text.Substring(0, h_tree_root.Length).Equals(h_tree_root))
               {
                   isgrey = true;
               }
           }
           if (isgrey)
           {
               node.ForeColor = Color.LightGray;
           }
       }

       public static bool isHTRoot(string id)
       {
           if (id.Length >= h_tree_root.Length)
               {
                   if (id.Substring(0, h_tree_root.Length).Equals(h_tree_root))
                   {
                       return true;
                   }
               }
           return false;
       }


       public static bool isINITHypo(string id)
       {

           if (id.Length > init_h.Length)
           {
               if (id.Substring(0, init_h.Length).Equals(init_h))
               {
                   return true;
               }

           }
           return false;
       }


       static public string cleanHypoText_firstword(string hypo)
       {
           try
           {
               string[] stopWordsArrary = new string[] { "a", "about", "actually", "after", "also", "am", "an", "and", "any", "are", "as", "at", "be", "because", "but", "by", 
                                                "could", "do", "each", "either", "en", "for", "from", "has", "have", "how", 
                                                "i", "if", "in", "is", "it", "its", "just", "of", "or", "so", "some", "such", "that", 
                                                "the", "their", "these", "thing", "this", "to", "too", "very", "was", "we", "well", "what", "when", "where",
                                                "who", "will", "with", "you", "your" 
                                            };

               string hypotrim = hypo.Replace("\\", string.Empty)
                                               .Replace("|", string.Empty)
                                               .Replace("(", string.Empty)
                                               .Replace(")", string.Empty)
                                               .Replace("[", string.Empty)
                                               .Replace("]", string.Empty)
                                               .Replace("*", string.Empty)
                                               .Replace("?", string.Empty)
                                               .Replace("}", string.Empty)
                                               .Replace("{", string.Empty)
                                               .Replace("^", string.Empty)
                                               .Replace("+", string.Empty);

               // transform search string into array of words
               char[] wordSeparators = new char[] { ' ', '\n', '\r', ',', ';', '.', '!', '?', '-', ' ', '"', '\'' };
               string[] words = hypotrim.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

               // Create and initializes a new StringCollection.
               StringCollection myStopWordsCol = new StringCollection();
               // Add a range of elements from an array to the end of the StringCollection.
               myStopWordsCol.AddRange(stopWordsArrary);

               StringBuilder sb = new StringBuilder();
               int flag = 0;

               for (int i = 0; i < words.Length; i++)
               {
                   string word = words[i].ToLowerInvariant().Trim();
                   if (word.Length > 1 && !myStopWordsCol.Contains(word))
                   {
                       //sb.Append(word + " ");
                       if (flag >= 3)
                           break;
                       flag++;
                       sb.Append(word + "_");
                   }
               }

               //return sb.ToString();
               return sb.ToString();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       #region Bind E-Tree

       private static void setETreeEUNode(TreeNode treeNode, Con_Experience_Unit eu)
       {
           treeNode.Text = eu.id;
           treeNode.Tag = "N";
           treeNode.ToolTipText = "N";
       }
       private static void setETreeHypoNode(TreeNode treeNode, Con_Hypo h)
       {
           treeNode.Text = h.id;
           treeNode.Tag = h.truthValue;
           treeNode.ToolTipText = h.hypo;
       }
       private static void setETreeRootNode(TreeNode rootTree, Con_Experience_Tree tree)
       {
           rootTree.Text = "Tree-" + tree.id;
           rootTree.Tag = "N";
           rootTree.ToolTipText = "N";
       }

        //bind a E-tree to a TreeView 
       static public void bindETree(TreeView treeView, Con_Experience_Tree tree)
       {
           treeView.Nodes.Clear();
           treeView.BeginUpdate();

           Con_Experience_Unit root = tree.root;
           if (root != null)
           {
               //Tree Node
               TreeNode treeNode = new TreeNode();
               setETreeRootNode(treeNode, tree);

               //Root of the tree
               TreeNode rootNode = new TreeNode();
               setETreeEUNode(rootNode, root);
               addChildEHypoTree(rootNode, root.nextHypoList);
               //Util_All.setTreeNodeFont(rootNode);
               treeNode.Nodes.Add(rootNode);

               //Util_All.setTreeNodeFont(treeNode);
               treeView.Nodes.Add(treeNode);
           }
           treeView.EndUpdate();
           treeView.Refresh();
       }

       static private void addChildEHypoTree(TreeNode parentNode, List<Con_Hypo> hypoList) 
       {
           foreach (Con_Hypo hypo in hypoList)
           {
               TreeNode hypoNode = new TreeNode();
               setETreeHypoNode(hypoNode, hypo);
               Con_Experience_Unit eu = hypo.next_unit;
               if (eu != null)
               {
                   TreeNode euNode = new TreeNode();
                   setETreeEUNode(euNode, eu);
                   addChildEHypoTree(euNode, eu.nextHypoList);

                   //Util_All.setTreeNodeFont(euNode);
                   hypoNode.Nodes.Add(euNode);
               }
               //Util_All.setTreeNodeFont(hypoNode);
               parentNode.Nodes.Add(hypoNode);
           }
       }


       #endregion

       //Compare a test observation to a target observation
       //[  similarity degree = hits/targetObs.sds.count  ]  
       static public double obsSimilarity(Con_Observation targetObs, Con_Observation testObs)
       {
           double score = 0;

           if (targetObs == null)
               return -1;

           double hits = 0;

           List<SD> targetSds = targetObs.sds;
           foreach (SD testSD in testObs.sds)
           {
               hits += SD.containsNum(targetSds, testSD);
           }

           score = hits / (double)targetSds.Count;

           return score;
       }

        //Sort E-Tree based on the score
        //the score is stored in the root, it is an accumulated number of all nodes
       static public void sortETree(List<Con_Experience_Tree> treeList)
       {

       }


    }
}
