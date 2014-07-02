/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      frmObservation.cs
/// Function:   to confirm captured observation and action
/// Note:    One observation/action has several SDs
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experience_Based_Cyber_SA
{
    public partial class frmObservation : Form
    {
        public frmMain main = null;

        //Con_Observation preObs = null;
        //Con_Actions preAct = null;

        public Con_Observation afterObs = new Con_Observation();
        public Con_Actions afterAct = new Con_Actions();

        //bool isDone = false;

        public frmObservation(frmMain m, Con_Observation tempo, Con_Actions tempa)
        {
            InitializeComponent();
            btnSelectAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            btnSelectObservation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            

            main = m;
            afterObs = tempo;
            afterAct = tempa;
            

            loadObservationList();
            loadActionList();




        }

        private void loadActionList()
        {
           // if (main.isFirstTime)
           // {
            //    this.listPreActions.Items.Add("Observe IDS Logs");
            //}
            //else
           // {
               //[6-27 FRM-OBV]
                //foreach (SD sd in preAct.sds)
                //{
                //    this.listPreActions.Items.Add(sd.toActString());
                //}
                foreach (SD sd in this.afterAct.sds)
                {
                    this.listSelectedAction.Items.Add(sd.toActString());
                }
            //}
        }

        private void loadObservationList()
        {
            //[6-27 FRM-OBV]
            //foreach (SD sd in preObs.sds)
            //{
            //        this.listObservations.Items.Add(sd.toObsString());
            //}
            foreach (SD sd in afterObs.sds)
            {
                this.listSelectedObservation.Items.Add(sd.toObsString());
            }
        }

        
        /*
        private void btnSelectAction_Click(object sender, EventArgs e)
        {
          //  if (main.isFirstTime)
           //     return;
            foreach (int index in this.listPreActions.SelectedIndices)
            {
                afterAct.add(preAct.sds.ToArray()[index]);
            }
            foreach (string item in this.listPreActions.SelectedItems)
            {
                listSelectedAction.Items.Add(item);
            }
            removeSelectedItem(listPreActions);
        }

        private void btnSelectObservation_Click(object sender, EventArgs e)
        {
            foreach (string item in listObservations.SelectedItems)
            {
                int index = listObservations.Items.IndexOf(item);
                afterObs.add(preObs.sds.ToArray()[index]);
                listSelectedObservation.Items.Add(item);
            }
                removeSelectedItem(this.listObservations);
        }
         */

        private void removeSelectedItem(ListBox lb){
            while (lb.SelectedItems.Count != 0)
            {
                lb.Items.RemoveAt(lb.SelectedIndices[0]);
            }
        }

       

        private void btnDone_Click(object sender, EventArgs e)
        {
           
            //add comfirmed obs and acts and remove temp obs and acts
            foreach (SD item in afterObs.sds)
            {
                main.confirmedObs.sds.Add(item);
                //[7-24 ONGOING OBS]
                //main.currentobs.addSD2List(item);


            }
            //remove temp obs and acts
           // if (!main.isFirstTime)
           // {
                foreach (SD item in afterAct.sds)
                {
                    main.confirmedAct.sds.Add(item);

                }
           // }

                main.tempObs = new Con_Observation();
                main.tempAct = new Con_Actions();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //set isObsPoped false, so that this form can be poped up next time    
            main.isObsPoped = false;
          //  main.isFirstTime = false;
            
        }

       

        private void btnDeleteAct_Click(object sender, EventArgs e)
        {
            List<SD> sds = new List<SD>();
            foreach (string item in this.listSelectedAction.SelectedItems)
            {
                int index = listSelectedAction.Items.IndexOf(item);
                sds.Add(afterAct.sds[index]);
                //afterObs.add(preObs.sds.ToArray()[index]);
               // listSelectedObservation.Items.Add(item);
            }
            removeSelectedItem(this.listSelectedAction);
            foreach(SD sd in sds){
                afterAct.sds.Remove(sd);
            }
           
        }

        private void btnDeleteObs_Click(object sender, EventArgs e)
        {
            List<SD> sds = new List<SD>();
            foreach (string item in this.listSelectedObservation.SelectedItems)
            {
                int index = listSelectedObservation.Items.IndexOf(item);
                sds.Add(afterObs.sds[index]);
                //afterObs.add(preObs.sds.ToArray()[index]);
                // listSelectedObservation.Items.Add(item);
            }
            removeSelectedItem(this.listSelectedObservation);
            foreach (SD sd in sds)
            {
                afterObs.sds.Remove(sd);
            }
        }

        private void btnRelateObs_Click(object sender, EventArgs e)
        {
            if (listSelectedObservation.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"If you select some observation items with a 
same feature, you can link them with this [Link function]. 
First, you need select these items. Then, click this button. ");
                return;
            }
            List<SD> sds = new List<SD>();
            foreach (string item in this.listSelectedObservation.SelectedItems)
            {
                int index = listSelectedObservation.Items.IndexOf(item);
                sds.Add(afterObs.sds[index]);
            }
            frmObservation_Link frmlink = new frmObservation_Link(this, sds);
            frmlink.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

       

    }
}
