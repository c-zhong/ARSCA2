//[4-08 GRAPHVIZ]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Experience_Based_Cyber_SA
{
    public class Util_GraphvizGenerator
    {
        public string text = "";
        public string base_sub = "";
        public List<String> clusters = new List<String>();
 
        Dictionary<String, List<String>> nodeMap = new Dictionary<string,List<string>>();

        public Util_GraphvizGenerator()
        {
            text = "digraph Trace {";
            base_sub = "subgraph cluster_base {node [style=filled, color = white]; style = filled; color = lightgrey; label=\"Base Graph\";";
            
        }

        private void  updateBaseGraph(string content)
        {
            base_sub += content;
        }

        private int newCluster(string traceid)
        {
            String str = "subgraph cluster_"+traceid+" { node [style=filled]; label = \""+traceid +"\"; color = blue;";
            clusters.Add(str);
            return clusters.IndexOf(str);
           
        }
        private void updateCluster(int cluster_index, string content)
        {
            string str = clusters[cluster_index] + content;
            clusters[cluster_index] = str;
        }


        private string interClusterEdgeCode()
        {
            string code = "";

            foreach (KeyValuePair<string, List<string>> pair in nodeMap)
            {
                List<string> dups = pair.Value;
                foreach (string dup in dups)
                {
                    code += pair.Key + " -> " + dup + " [style = dotted, color = blue]; ";
                }
            }
            return code;
        }
        public void finishGraph()
        {
            //base subgraph
            base_sub += "}";
            text += base_sub;

            //clusters
            for (int i = 0; i < clusters.Count; i++)
            {
                clusters[i] = clusters[i] + "}";
                text += clusters[i];
            }
            //intercluster edges
            text += interClusterEdgeCode();

            text += "}";

            //write
            output2File();
        }


        private void output2File()
        {
            string filename = Util_All.currentDir() + "Trace\\trace_graph.txt";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(text);
                writer.Close();
            }
        }

        private void addEU(Con_Experience_Unit eu)
        {
            if (eu != null)
            {
                foreach (Con_Hypo hypo in eu.nextHypoList)
                {
                    updateBaseGraph(eu.id + " -> " + hypo.id + "; ");
                    nodeMap.Add(hypo.id, new List<string>());

                    if (hypo.next_unit != null)
                    {
                        updateBaseGraph(hypo.id + " -> " + hypo.next_unit.id + "; ");
                        addEU(hypo.next_unit);
                    }
                }
            }
        }

        public void addETree(Con_Experience_Tree tree)
        {
            Con_Experience_Unit eu = tree.root;
            if (eu != null)
            {
                addEU(eu);
            }
        }

        private string getNodeID(string node, string trace)
        {
            return node + trace;
        }
        private string getNodeLbl(string node, string trace)
        {
            return node;
        }

        private void updateNodeMap(string id, string duid)
        {
            if (nodeMap.ContainsKey(id))
            {
                List<string> dupHypos = nodeMap[id];
                if (!dupHypos.Contains(duid))
                {
                    dupHypos.Add(duid);
                }
            }
        }
        private string edgeCode(string id1, string id2, string traceid, string time, string label)
        {
            string code = "";
            code += getNodeID(id1, traceid) + " -> " + getNodeID(id2, traceid) + " [label=\"" + time + " "+label+"\"]; ";
            code += getNodeID(id1, traceid) +" [label=\""+getNodeLbl(id1, traceid)+"\"]; ";
            code += getNodeID(id2, traceid) +" [label=\""+getNodeLbl(id2, traceid)+"\"]; ";

            updateNodeMap(id1, getNodeID(id1, traceid));
            updateNodeMap(id2, getNodeID(id2, traceid));

            //code += id1 + " -> " + getNodeID(id1, traceid) + "[style = dotted, color = blue]; ";
            return code;
        }

        //[4-08 GRAPHVIZ] return a graphcode
        private string traceItem2GraphCode(string time, string operation, string traceid)
        {
            //string code = "";
            string[] array = operation.Split('\n');
            if(array.Length > 1)
            {
                string op_type = array[0];
                if (op_type.Contains("JUMP_FROM_TO"))
                {
                    string[] subarray1 = array[1].Split(' '); 
                    string[] subarray2 = array[2].Split(' ');
                    return edgeCode(subarray1[0], subarray2[0], traceid, time, "JUMP_FROM_TO");
                }
                else if (op_type.Contains("NEW"))
                {
                    string[] subarray1 = array[1].Split(' ');
                    string[] subarray2 = array[2].Split(' ');
                    return edgeCode(subarray1[0], subarray2[0], traceid, time, "NEW");
                }
                else if (op_type.Contains("ADD_CHILD"))
                {
                    string[] subarray1 = array[1].Split(' ');
                    string[] subarray2 = array[2].Split(' ');
                    return edgeCode(subarray1[0], subarray2[0], traceid, time, "ADD_CHILD");
                }
                else if (op_type.Contains("CHANGE_TRUTH_VALUE"))
                {
                    string[] subarray1 = array[1].Split(' ');
                    return edgeCode(subarray1[0], subarray1[0], traceid, time, "CHANGE_TRUTH_VALUE");
                }
                else if (op_type.Contains("CHANGE_CONTENT"))
                {
                    string[] subarray1 = array[1].Split(' ');
                    return edgeCode(subarray1[0], subarray1[0], traceid, time, "CHANGE_CONTENT");
                    
                }
            }
            return "";
        }

        public void addTrace(Con_Observable_Trace trace)
        {
            int index = newCluster(trace.id);
            string content = "";
            foreach(Con_Observable_Trace_Item item in trace.trace)
            {
                content += traceItem2GraphCode(item.timestamp, item.operation, trace.id);

            }
            updateCluster(index, content);
        }
    }
}
