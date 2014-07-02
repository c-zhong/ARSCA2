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
    public partial class frmWriteDownObservation : Form
    {
        frmMain main = null;

        public frmWriteDownObservation(frmMain m)
        {
            main = m;

            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Trim().Equals(""))
            {
                MessageBox.Show("Text box is empty. Please describe your observation in the text box.");
                return;
            }
            else
            {
                if (main != null)
                {
                    SD_Description sddes = new SD_Description(txtContent.Text);
                    main.confirmedObs.add(sddes);
                    main.confirmedAct.add(sddes);
                }
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
