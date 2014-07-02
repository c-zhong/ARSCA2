/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      Con_Experience_Unit.cs
/// Function:   EU
/// Note:    Observation + Action
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Experience_Based_Cyber_SA
{
    public class Con_Experience_Unit
    {
        public string id = "";
        public Con_Actions act = null;
        public Con_Observation obs = null;

        public Con_Hypo preHypo = null;
        public List<Con_Hypo> nextHypoList = new List<Con_Hypo>();

        public List<double> MatchingDegreeOfSubtrees = new List<double>();
        public Con_Experience_Unit pre_EU = null;
        public List<Con_Experience_Unit> nextEUList = new List<Con_Experience_Unit>();

    
        public int isroot = 0;
        public double MatchingDegree = 0;
        //public List<int> is_marked = new List<int>();
        

        #region Construction
        public Con_Experience_Unit()
        {
            id = Util_All.generateID("EU");
        }

        public Con_Experience_Unit(Con_Experience_Unit curr)
        {
            this.act = curr.act;
            this.id = curr.id;
           //this.is_marked = curr.is_marked;
            this.nextEUList = curr.nextEUList;
            this.nextHypoList = curr.nextHypoList;
            this.isroot = curr.isroot;
            this.MatchingDegree = curr.MatchingDegree;
            this.obs = curr.obs;
            this.pre_EU = curr.pre_EU;
            this.preHypo = curr.preHypo;
            this.MatchingDegreeOfSubtrees = curr.MatchingDegreeOfSubtrees;
        }

        public Con_Experience_Unit(Con_Actions a, Con_Observation o)
        {
            id = Util_All.generateID("EU");
            act = a;
            obs = o;
        }
        #endregion


        public bool isEmpty()
        {
            bool actEmpty = (act == null) || (act.sds.Count == 0);
            bool obsEmpty = (obs == null) || (obs.sds.Count == 0);
            return actEmpty && obsEmpty;
        }

        public void setEU(Con_Actions a, Con_Observation o)
        {
            act = a;
            obs = o;
        }
        /*
        public void setPreHypo(Con_Hypo h)
        {
            preHypoList.Add(h);
            if(h != null)
              h.setNextUnit(this);
        }
         * */
        public void setPreHypo(Con_Hypo h)
        {
            preHypo = h;
        }

        public void addNextHypo(Con_Hypo h)
        {
            nextHypoList.Add(h);
            if(h!=null)
              h.setPreUnit(this);
        }

    }
}
