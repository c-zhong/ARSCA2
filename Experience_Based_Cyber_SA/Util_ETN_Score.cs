/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Util_ETN_Score.cs
/// Function:   sort ETs
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Experience_Based_Cyber_SA
{
    public class Util_ETN_Score
    {
        //each TreeNode is a E-tree
        public TreeNode tree = new TreeNode(); 
        public double score = -1;

        //level: in which level the eu in the tree has 0 similarity
        public double level = -1;

        public Util_ETN_Score()
        {
        }

        public Util_ETN_Score(TreeNode t)
        {
            tree = t;
            score = -1;
        }
        public Util_ETN_Score(TreeNode t, double s)
        {
            tree = t;
            score = s;
        }

        //update the score according to the obsList
        //return: in which level the eu in the tree has score=0;
        //root = 0;
        //EU nodes which score is 0, will be collapsed
        public bool updateScoreTree(LinkedList<Con_Observation> obsList)
        {
                       
            bool obsOutofTree = true; 

            if (tree == null || tree.Nodes.Count < 1)
            {
                score = -1;
                level = -1;
                return obsOutofTree;
            }

            //trim all nodes's currentMarker and reset imageIndex
            Util_All.trimTreeMarker(tree.Nodes);

            score = 0;
            level = 0;
            tree.Expand();

            TreeNode root = tree.Nodes[0];
            

            LinkedList<TreeNode> eusInCurrentLevel = new LinkedList<TreeNode>();
            LinkedList<TreeNode> temp = new LinkedList<TreeNode>();
            eusInCurrentLevel.AddLast(root);

            TreeNode lastnode = null;

            foreach(Con_Observation obs in obsList)
            {
                bool hasNone0Score = false;
                //bool isHypolevel = false;

                //List<TreeNode> temp = new List<TreeNode>();
                
                foreach (TreeNode node in eusInCurrentLevel)
                {
                    if (node == null)
                        continue;
/*
                    if (node.Text.Substring(0, 2).Equals("H-"))
                    {
                        foreach (TreeNode childnode in node.Nodes)
                        {
                            temp.Add(childnode);
                            isHypolevel = true;
                            //level--;//because level will ++; if H, level should not change
                            node.Expand();
                            lastnode = node;
                        }

                    }
 * */
                     if (node.Text.Substring(0, 3).Equals("EU-"))
                    {
                        //isHypolevel = false; 
                        Con_Experience_Unit eu = (Con_Experience_Unit)node.Tag;
                        double euScore = Util_All.obsSimilarity(eu.obs, obs);
                        if (euScore != 0)
                        {
                            hasNone0Score = true;
                            score += euScore*(Math.Pow(10,level));
                            lastnode = node;
                            node.Expand();
                            
                            foreach (TreeNode childnode in node.Nodes)
                            {
                                //temp.Add(childnode); //this is H-Node
                                if (childnode != null)
                                {
                                    childnode.Expand();
                                    lastnode = childnode;
                                }
                                
                                foreach (TreeNode childchildnode in childnode.Nodes)
                                {
                                    temp.AddLast(childchildnode);
                                }

                            }
                        }
                        else
                        {
                            node.Collapse();
                        }
                    }
                   
                }
                /*
                if (isHypolevel)
                {
                    eusInCurrentLevel.Clear();
                    eusInCurrentLevel = temp;
                }
                 * */
                
                    if (hasNone0Score)
                    {
                        level++;

                        eusInCurrentLevel.Clear();
                        foreach (TreeNode node in temp)
                        {
                            eusInCurrentLevel.AddLast(node);
                        }
                        temp.Clear();
                        if (obs == obsList.Last.Value)
                            obsOutofTree = false;
                    }
                    else
                    {
                        break;
                    }
                
            }
            /*
            if (eusInCurrentLevel.Count > 0)
            {
                foreach (TreeNode node in eusInCurrentLevel)
                {
                    if (node.Text.Substring(0, 2).Equals("H-"))
                    {
                        lastnode = node;
                    }
                }
            }
             * */
            if (lastnode != null)
            {
                lastnode.Collapse();
                //mark lastnode by assign 9 to its imageindex(other attributes are occupied)
                lastnode.ImageIndex = 9;
            }

            /*
            if (lastnode.Text.Substring(0, 2).Equals("H-"))
            {
                lastnode.Text = ((Con_Hypo)lastnode.Tag).id + " ■";
            }
            else
            {
                lastnode.Text = ((Con_Experience_Unit)lastnode.Tag).id + " ■";
            }
             * */
            return obsOutofTree;
        }


    }
}

