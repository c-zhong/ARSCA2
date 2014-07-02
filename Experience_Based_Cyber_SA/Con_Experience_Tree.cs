/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Experience_Tree.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Experience_Tree
    {
               
        public Con_Experience_Unit root = null;
        public Con_Experience_Unit lastExU = null;
        public Con_Hypo lastHypo = null;
        public String id = "";

        #region Constructor

        public Con_Experience_Tree()
        {

            id = Util_All.generateID("ExT");
            root = new Con_Experience_Unit();
            root.id = Util_All.generateID(Util_All.e_tree_root);

            SD_Other sd = new SD_Other();
            sd.note = "ROOT Node of E-Tree, having no actual meaning";
            Con_Observation newobs = new Con_Observation();
            newobs.add(sd);
            root.obs = newobs;

        }
        //public Con_Experience_Tree(Con_Experience_Unit r)
        //{
        //    id = Util_All.generateID("ExT");
        //    root = r;
        //}

        #endregion

        //add first-level eu, and return the auto-generated init_hypo;
        public Con_Hypo addFirstLevelEU(Con_Experience_Unit r)
        {
            if (root != null)
            {
                Con_Hypo init_hypo = new Con_Hypo();
                init_hypo.id = Util_All.init_h + r.id.Substring(2,r.id.Length-2);
                r.preHypo = init_hypo;
                init_hypo.next_unit = r;

                init_hypo.pre_unit = root;
                root.addNextHypo(init_hypo);

                return init_hypo;
            }
            return null;
        }
        public bool isNull()
        {
            bool result = false;
            if (root == null)
                result = true;
            else
            {
                if (root.nextHypoList.Count < 1)
                    result = true;
            }
            return result;
        }
        
         
    }
}


//List<Con_Experience_Unit> euList = new List<Con_Experience_Unit>();
//List<Con_Hypo> hList = new List<Con_Hypo>();