/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_FileWriter.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace Experience_Based_Cyber_SA
{
    public class Util_FileWriter
    {
        /*
        static public void outputExperienceTree(Con_Experience_Tree exTree)
        {
            XmlDocument  xmlDocument = new XmlDocument();
         
            XmlDeclaration xmlDeclare =xmlDocument.CreateXmlDeclaration("1.0","utf-8",null);
           
            XmlElement elementRoot =xmlDocument.CreateElement("Experience");

            xmlDocument.AppendChild(elementRoot);

            Con_Experience_Unit currentExU = exTree.root;
          
            while (currentExU != null)
            {
                XmlElement exuXML = xmlDocument.CreateElement("ExperienceUnit");
                Con_Actions act = currentExU.act;
                if (act != null && act.sds.Count > 0)
                {
                    XmlElement actXML = xmlDocument.CreateElement("Action");
                    foreach (SD sd in act.sds)
                    {
                        XmlElement sdXML = xmlDocument.CreateElement("SD");
                        sdXML.InnerText = sd.toActString();
                        actXML.AppendChild(sdXML);
                    }
                    exuXML.AppendChild(actXML);  
                }
                Con_Observation obs = currentExU.obs;
                if (obs != null && obs.sds.Count > 0)
                {
                    XmlElement obsXML = xmlDocument.CreateElement("Observation");
                    foreach (SD sd in obs.sds)
                    {
                        XmlElement sdXML = xmlDocument.CreateElement("SD");
                        sdXML.SetAttribute("type", sd.getType());
                        sdXML.InnerText = sd.toActString();
                        obsXML.AppendChild(sdXML);
                    }
                    exuXML.AppendChild(obsXML);
                }

                elementRoot.AppendChild(exuXML);
                List<Con_Hypo> hypos = currentExU.nextHypoList;
                if (hypos.Count == 0)
                    break;
                else
                {
                    XmlElement hypoXML = xmlDocument.CreateElement("Hypothesis");
                    hypoXML.SetAttribute("hypo", hypos.First().hypo);
                    hypoXML.SetAttribute("truth_value", hypos.First().truthValue.ToString());
                    elementRoot.AppendChild(hypoXML);
                    currentExU = hypos.First().next_unit;
                }

            }


            //Save the file

            xmlDocument.Save("..\\..\\Experience\\experience.xml");

        }
         * */

        static private void addExperienceUnit(XmlDocument detailXmlDoc, XmlDocument treeXmlDoc, XmlNode detailParent, XmlNode treeParent, Con_Experience_Unit currentExU)
        {
            XmlElement newDetailXmlNode = null;

            if (!currentExU.isEmpty())
                 {
                     newDetailXmlNode = detailXmlDoc.CreateElement("Experience_Unit");
                     newDetailXmlNode.SetAttribute("ID", currentExU.id);
                     //newDetailXmlNode.SetAttribute("truth_value","U");
                     //Act
                     Con_Actions act = currentExU.act;
                     if (act != null && act.sds.Count > 0)
                     {
                         XmlElement actXML = detailXmlDoc.CreateElement("Action");
                         foreach (SD sd in act.sds)
                         {
                             XmlElement sdXML = detailXmlDoc.CreateElement("SD");
                             sdXML.SetAttribute("type", sd.getType());
                             sdXML.SetAttribute("content", sd.toOriginalString());
                             actXML.AppendChild(sdXML);
                         }
                         newDetailXmlNode.AppendChild(actXML);
                     }
                     //Obs
                     Con_Observation obs = currentExU.obs;
                     if (obs != null && obs.sds.Count > 0)
                     {
                         XmlElement obsXML = detailXmlDoc.CreateElement("Observation");
                         foreach (SD sd in obs.sds)
                         {
                             XmlElement sdXML = detailXmlDoc.CreateElement("SD");
                             sdXML.SetAttribute("type", sd.getType());
                             sdXML.SetAttribute("content", sd.toOriginalString());
                             obsXML.AppendChild(sdXML);
                         }
                         newDetailXmlNode.AppendChild(obsXML);
                     }
                     //save new node
                     detailParent.AppendChild(newDetailXmlNode);
                 }

                 XmlElement newTreeXmlNode = treeXmlDoc.CreateElement("Experience_Unit");
                 newTreeXmlNode.SetAttribute("ID", currentExU.id);
                 newTreeXmlNode.SetAttribute("truth_value", "N");
                 newTreeXmlNode.SetAttribute("detail", "N");

                 foreach(Con_Hypo hypo in currentExU.nextHypoList)
                 {
                     XmlElement hypoXml = treeXmlDoc.CreateElement("Hypothesis");
                     hypoXml.SetAttribute("ID", hypo.id);
                     hypoXml.SetAttribute("truth_value", hypo.truthValue+"");
                     hypoXml.SetAttribute("type", hypo.type + "");
                     hypoXml.SetAttribute("detail", hypo.hypo);
                     newTreeXmlNode.AppendChild(hypoXml);

                     Con_Experience_Unit nextEU = hypo.next_unit;
                     if(nextEU != null)
                     {
                         addExperienceUnit(detailXmlDoc,treeXmlDoc,detailParent,hypoXml,nextEU);
                     }
                 }

                treeParent.AppendChild(newTreeXmlNode);
        }

        static public void addExperienceTree(Con_Experience_Tree tree)
        {
            string treeFileName = Util_All.currentDir() + "Experience\\experience_tree.xml";
            string detailFileName = Util_All.currentDir() + "Experience\\experience.xml";
            XmlDocument treeXmlDoc = new XmlDocument();
            XmlDocument detailXmlDoc = new XmlDocument();
            treeXmlDoc.Load(treeFileName);
            detailXmlDoc.Load(detailFileName);


            XmlNode rootTreeXmlNode = treeXmlDoc.SelectSingleNode("Experience_Trees");
            XmlNode rootDetailXmlNode = detailXmlDoc.SelectSingleNode("Experiences");

            XmlElement parentTreeXmlNode = treeXmlDoc.CreateElement("Experience_Tree");
            parentTreeXmlNode.SetAttribute("ID", tree.id);

            Con_Experience_Unit currentExU = tree.root;

             if (currentExU != null)
             {
                 addExperienceUnit(detailXmlDoc, treeXmlDoc, rootDetailXmlNode, parentTreeXmlNode, currentExU);
             }
             rootTreeXmlNode.AppendChild(parentTreeXmlNode);
             treeXmlDoc.Save(treeFileName);
             detailXmlDoc.Save(detailFileName);
        }



        static public bool writeTestTimes (List<string> list)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(Util_All.currentDir() + "test_time.csv");

                foreach (string s in list)
                {
                    writer.WriteLine(s);
                }

                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
    }

}
