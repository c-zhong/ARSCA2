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
    public partial class frmContext : Form
    {
        frmMain main = null;
        Con_Experience_Unit parentEU = null;

        public frmContext(frmMain m, Con_Experience_Unit eu)
        {
            InitializeComponent();

            parentEU = eu;
            main = m;
        }

        private void loadCurrentObs()
        {
            if (parentEU != null && parentEU.obs != null)
            {
                Con_Observation obs = parentEU.obs;

                foreach (SD sd in obs.sds)
                {
                    this.lbCurrentObservation.Items.Add(sd.toObsString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                main.btnAddSibling_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
