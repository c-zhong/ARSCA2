/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      frmHypothesis.cs
/// Function:   to generate a new hypothesis
/// Note:    
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
    public partial class frmHypothesis : Form
    {
        frmMain main = null;
        Con_Observation observation =null;
        Con_Hypo newHypo = null;

        bool isInit = false;

        public frmHypothesis(frmMain m, Con_Observation obs, Con_Hypo new_hypo)
        {
            InitializeComponent();

            main = m;
            observation = obs;
            newHypo = new_hypo;
           

            this.listObservation.HorizontalScrollbar = true;
            this.listObservation.ScrollAlwaysVisible = true;
            loadListObservations();

            if (obs != null && obs.sds != null)
            {
                if (obs.sds.Count > 0)
                {
                    if (obs.sds[0].toOriginalString().Contains("ROOT Node of E-Tree"))
                    {
                        isInit = true;
                        this.textBox1.Text =
        @"You are creating an INIT HYPOTHESIS, which is for structure use only.
You don't need to add meaningful content for it.
After you have created the INIT-HYPOTHESE,
you can gather new observation, and the new action-observation node
will be a child of this INIT HYPOTHESES,.
";
                        textBox1.Enabled = false;

                        
                    }
                }
            }

            if (isInit)
            {
                textBox1.Visible = true;
                this.txtHypo.Visible = false;
            }
            else
            {
                txtHypo.Visible = true;
                textBox1.Visible = false;
            }
        }

        

        void loadListObservations()
        {
            if (observation == null)
                return;

            foreach (SD item in observation.sds)
            {
                
                this.listObservation.Items.Add(item.toObsString());
                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(cmbHypoType.SelectedItem == null)
            {
                MessageBox.Show("Please select the type of the hypothesis first");
                return ;
            }

            bool flag = false;

            if (txtHypo.Text.Trim().Equals(""))
            {
                if (!isInit)
                {
                    DialogResult result1 = MessageBox.Show(@"You haven't enter any content. 
Do you want to create a hypothesis without content? 
[Yes:create a hypothesis No: Go back to the window]",
            "Attention", MessageBoxButtons.YesNo);

                    if (result1 == DialogResult.No)
                    {
                        txtHypo.Focus();
                        return;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                flag = true;
            }

            if(flag)
            {
                if (newHypo == null)
                {
                    //main.confirmedHypo.hypo = txtHypo.Text;
                    //main.confirmedHypo.type = cmbHypoType.SelectedIndex;
                }
                else
                {
                    newHypo.hypo = txtHypo.Text;
                    newHypo.type = cmbHypoType.SelectedIndex;
                }
            }

            main.isfrmHypothesisDone = true;

            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHypothesis_Load(object sender, EventArgs e)
        {
            cmbHypoType.SelectedIndex = 0;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
