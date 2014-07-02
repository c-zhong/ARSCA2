/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Hypo.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Hypo
    {
        public String hypo = "";
        public Con_Experience_Unit pre_unit = null;
        public Con_Experience_Unit next_unit = null;
        public Con_Hypo parentHypo = null;
        public List<Con_Hypo> childrenHypos = new List<Con_Hypo>();
        public String id = "";
        public int truthValue = 0;
        //0: Unknown, 1: True -1: False
        //Hypo Type
        //0: nomal   1: missing [a guess]   2:false alert [suspect]
        public int type = 0; 

        #region Construction
        public Con_Hypo()
        {
            id = Util_All.generateID("H");
        }

        public Con_Hypo(String h, Con_Experience_Unit pre, Con_Experience_Unit next)
        {
            id = Util_All.generateID("H");
            hypo = h;
            pre_unit = pre;
            next_unit = next;

            updateID(); 
        }
        public Con_Hypo(Con_Hypo h)
        {
            id = Util_All.generateID("H");
            hypo = h.hypo;
            type = h.type;

            updateID();
        }
        public Con_Hypo(String h, int t)
        {
            id = Util_All.generateID("H");
            hypo = h;
            type = t;

            updateID();
        }

        #endregion

        //called after hypo has been updated.
        public void updateID()
        {
            if (!hypo.Trim().Equals(""))
            {
                //[8-20 MEANINGFUL NODE]
                string clean = Util_All.cleanHypoText_firstword(hypo);
                //id += "-" + clean;
                string tail = id.Substring(1, id.Length - 1);
                id = "H_" + clean + tail;
                
            }
        }
        public void setType(int t)
        {

        }
        public void setMissing()
        {
            type = 1;
        }
        public void setFalsePositive()
        {
            type = 2;
        }

        public void setPreUnit(Con_Experience_Unit ex)
        {
            pre_unit = ex;
        }

        public void setNextUnit(Con_Experience_Unit next)
        {
            next_unit = next;
        }
        public void setParentHypo(Con_Hypo p)
        {
            parentHypo = p;
        }
        public void addChildrenHypo(Con_Hypo c)
        {
            if (!childrenHypos.Contains(c))
            {
                childrenHypos.Add(c);
            }
        }
        public void setChildrenHypo(List<Con_Hypo> chps)
        {
            childrenHypos = chps;
        }

        public void setFalse()
        {
            truthValue = -1;
            //[7-27] not mark offsprings false
            //foreach (Con_Hypo child in childrenHypos)
            //{
            //    child.setFalse();
            //}
        }

        public string truthValueString()
        {
            if (truthValue == 0)
                return "Unknown";
            else if (truthValue == 1)
                return "True";
            else
                return "False";
        }

        public static LinkedList<Con_Observation> getContextObs(Con_Hypo currentHypo)
        {
            if (currentHypo == null)
                return null;

            LinkedList<Con_Observation> obsList = new LinkedList<Con_Observation>();

            Con_Experience_Unit eu = currentHypo.pre_unit;

            while (eu != null)
            {
                obsList.AddFirst(eu.obs);
                Con_Hypo preHypo = eu.preHypo;
                if (preHypo == null)
                {
                    break;
                }
                eu = preHypo.pre_unit;
            }
            return obsList;
        }

        public bool allChildAreTrue()
        {
            foreach (Con_Hypo child in childrenHypos)
            {
                if (child.truthValue != 1)
                    return false;
            }
            return true;
        }

        public bool isOffSpringOf(Con_Hypo ancestor)
        {
            Con_Hypo parent = this.parentHypo;

            while (parent != null)
            {
                if (ancestor == parent)
                    return true;
                else
                    parent = parent.parentHypo;
            }
            return false;
            //bool isoffspring = false;
            
            //foreach (Con_Hypo hypo in ancestor.childrenHypos)
            //{
            //    if (hypo != null)
            //    {
            //        if (this == hypo)
            //            isoffspring = true;
            //        else
            //        {
            //            isoffspring = isOffSpringOf(hypo);
            //        }
            //    }
            //}
            //return isoffspring;
        }
       
    }
}
