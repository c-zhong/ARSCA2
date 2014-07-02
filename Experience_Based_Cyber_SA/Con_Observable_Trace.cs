/***
 * This class is created to capture the TAP (Trace of Analytical reasoning Process)
 * 
 ***/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Experience_Based_Cyber_SA
{
    public class Con_Observable_Trace
    {
        public string id = "";
        public LinkedList<Con_Observable_Trace_Item> trace = new LinkedList<Con_Observable_Trace_Item>();

        
        public Con_Observable_Trace()
        {
            id = Util_All.generateID("TAP");
        }

        public Boolean isEmpty()
        {
            return (trace == null) || (trace.Count == 0);
        }

        public void AddItem(Con_Observable_Trace_Item item)
        {
            if (trace == null)
                trace = new LinkedList<Con_Observable_Trace_Item>();

            if (item != null)
            {
                trace.AddLast(item);
            }
        }

        public void AddItem(string time, string op)
        {
            if(trace == null)
                trace = new LinkedList<Con_Observable_Trace_Item>();

            
            Con_Observable_Trace_Item item = new Con_Observable_Trace_Item(time, op);
            trace.AddLast(item);
                      
        }


        public void Output_XML()
        {
            string filename = Util_All.currentDir()+"Trace\\traces.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            XmlNode rootXmlNode = xmlDoc.SelectSingleNode("Traces");

            XmlElement newTraceXmlNode = xmlDoc.CreateElement("Trace");

            newTraceXmlNode.SetAttribute("ID", id);

            foreach (Con_Observable_Trace_Item item in trace)
            {
                XmlElement itemXmlNode = xmlDoc.CreateElement("Item");
                itemXmlNode.SetAttribute("Timestamp", item.timestamp);
                itemXmlNode.InnerText = "\n"+item.operation+"\n";

                newTraceXmlNode.AppendChild(itemXmlNode);

            }

            rootXmlNode.AppendChild(newTraceXmlNode);

            xmlDoc.Save(filename);
        }
    }
}
