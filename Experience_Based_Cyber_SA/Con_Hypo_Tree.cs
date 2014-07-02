/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Hypo_Tree.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Hypo_Tree
    {
        //the first node is not useful 
        string id = "";
        public Con_Hypo root = new Con_Hypo();
      //  public List<Con_Hypo> rootchildren = new List<Con_Hypo>();

        public Con_Hypo_Tree()
        {
            id = Util_All.generateID("HT");
            root.id = Util_All.generateID(Util_All.h_tree_root);
        }
        public Con_Hypo_Tree(Con_Hypo r)
        {
            if(r!=null)
             root = r;
        }

        public Con_Hypo getRoot()
        {
            return root;
        }
        
        public bool isNull()
        {
            return (root.childrenHypos.Count < 1);
        }
        public bool isRoot(Con_Hypo hypo)
        {
            return hypo.id.Equals(root.id);
        }
        
    }
}
