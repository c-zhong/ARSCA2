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
    public partial class frmTip : Form
    {
        frmMain main = null;
        int index = -1; //1: quick find 2:press key

        public frmTip(frmMain m, string msg, int i)
        {
            InitializeComponent();
           
            this.lblContent.Text = msg;
            main = m;
            index = i;

            //lblContent.Text = msg;
            CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (index == 1 && main != null)
                    main.firstPopTip = true;
                else if (index == 2 && main != null)
                    main.firstPopTip_selectobs = true;
            }
            this.Close();
        }

        private void frmTip_Load(object sender, EventArgs e)
        {

        }
    }
}
