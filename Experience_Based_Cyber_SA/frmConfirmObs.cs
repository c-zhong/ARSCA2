/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      frmConfirmObs.cs
/// Function:   
/// Note:    used for search
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Experience_Based_Cyber_SA
{
    public partial class frmConfirmObs : Form
    {
        frmMain main = null;

        LinkedList<SD> totalSds = new LinkedList<SD>();
        LinkedList<Con_Observation> totalObs = new LinkedList<Con_Observation>();

        public frmConfirmObs(frmMain m)
        {
            main = m;
            main.isSearchReady = false;
            InitializeComponent();
            btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            loadListObservations();
        }

        void loadListObservations()
        {
            if (main.lastHypo == null)
            {
                
            }
            else
            {
                LinkedList<Con_Observation> obsList = Con_Hypo.getContextObs(main.lastHypo);
                foreach (Con_Observation item in obsList)
                {
                    foreach (SD sditem in item.sds)
                    {
                        this.listObservation.Items.Add(sditem.toObsString());
                        totalSds.AddLast(sditem);
                        totalObs.AddLast(item);
                    }
                    this.listObservation.Items.Add(" ");
                    totalSds.AddLast(new SD_Other());
                    totalObs.AddLast(item);
                }
            }
            foreach (SD item in main.confirmedObs.sds)
                {
                    this.listObservation.Items.Add(item.toObsString());
                    totalObs.AddLast(main.confirmedObs);
                    totalSds.AddLast(item);
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            removeSelectedItem(this.listObservation);
        }

        private void removeSelectedItem(ListBox lb)
        {
            Con_Observation[] obsarray = totalObs.ToArray();
            SD[] sdarray = totalSds.ToArray();
           
            while (lb.SelectedIndices.Count != 0)
            {
                int index = lb.SelectedIndices[0];
                lb.Items.RemoveAt(lb.SelectedIndices[0]);
                Con_Observation thisObs = obsarray[index];
                SD thisSD = sdarray[index];
                if (thisSD.type != Enum_SDType.SDType.OTHER)
                {
                    thisObs.sds.Remove(thisSD);
                }
                totalObs.Remove(obsarray[index]);
                totalSds.Remove(sdarray[index]);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            main.isSearchReady = true;
            Close();
        }

       

    }
}
