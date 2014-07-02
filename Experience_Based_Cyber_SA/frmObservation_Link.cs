//[6-27 LINK_OBS]

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
    public partial class frmObservation_Link : Form
    {
        frmObservation fobs = null;
        List<SD> obs = null;
        int checkednum = 0;
        

        public frmObservation_Link(frmObservation frmobs, List<SD> selectedobs)
        {
            InitializeComponent();
            fobs = frmobs;
            obs = selectedobs;
            ckbOther.Checked = true;

            foreach (SD sd in obs)
            {
                this.listSelectedObservation.Items.Add(sd.toObsString());
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generateLinkSD()
        {
            List<String> reasons = new List<String>();
            if (ckbDesIp.Checked)
            {
                reasons.Add("Same DesIp");
            }
            if (ckbSrcIp.Checked)
            {
                reasons.Add("Same SrcIp");
            } 
            if (ckbTime.Checked)
            {
                reasons.Add("Same Time");
            }
            if (ckbSourcePrt.Checked)
            {
                reasons.Add("Same SrcPort");
            }
            if (ckbDesPort.Checked)
            {
                reasons.Add("Same DesPort");
            }
            if (ckbOther.Checked)
            {
                if (txtOther.Text.Trim().Equals(""))
                {
                    reasons.Add("Other");
                }
                else
                {
                    reasons.Add(txtOther.Text);
                }
            }
            if (reasons.Count > 0)
            {
                SD_Link sd = new SD_Link(obs, reasons);
                fobs.afterAct.sds.Add(sd);
                fobs.afterObs.sds.Add(sd);


                //[6-29 TRACE]
                this.fobs.main.trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Link_2_String(sd)));
               
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool flag = ckbSrcIp.Checked || ckbDesIp.Checked || ckbTime.Checked || ckbSourcePrt.Checked || ckbDesPort.Checked;

            if (flag)
            {
                if (txtOther.Text.Trim().Equals("") && ckbOther.Checked)
                {
                    ckbOther.Checked = false;
                }
            }

            if (ckbOther.Checked)
            {

                if (txtOther.Text.Trim().Equals(""))
                {
                    DialogResult result1 = MessageBox.Show("Do you want to specify why your link these observations in the textbox?",
        "Important Question",
        MessageBoxButtons.YesNoCancel);
                    if (result1 == DialogResult.Yes)
                    {
                        txtOther.Focus();
                    }
                    else if (result1 == DialogResult.No)
                    {
                        generateLinkSD();
                        Close();
                    }
                }
                else
                {
                    generateLinkSD();
                    Close();
                }
            }
            else
            {
                generateLinkSD();
                Close();
            }

        }

       
    }
}
