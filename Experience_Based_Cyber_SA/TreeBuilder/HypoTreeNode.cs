/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      HypoTreeNode.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experience_Based_Cyber_SA.TreeBuilder
{
    public class HypoTreeNode : TreeNode
    {
        public Con_Hypo hypo = null;

        #region Constructor

        public HypoTreeNode(Con_Hypo h)
        {
            hypo = h;
        }
        #endregion


    }
}
