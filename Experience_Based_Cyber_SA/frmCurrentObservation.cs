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
    public partial class frmCurrentObservation : Form
    {
        frmMain main = null;

        public frmCurrentObservation(frmMain m)
        {
            InitializeComponent();

            main = m;
        }

        public void loadCurrentObs()
        {
            clearList();

            if (main.confirmedObs != null)
            {
                foreach (SD sd in main.confirmedObs.sds)
                {
                    this.lbCurrentObservation.Items.Add(sd.toObsString());
                }
            }
        }

        private void removeSelectedItem(ListBox lb)
        {
            while (lb.SelectedItems.Count != 0)
            {
                lb.Items.RemoveAt(lb.SelectedIndices[0]);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<SD> sds = new List<SD>();

                foreach (string item in this.lbCurrentObservation.SelectedItems)
                {
                    int index = lbCurrentObservation.Items.IndexOf(item);
                    sds.Add(main.confirmedObs.sds[index]);

                }
                removeSelectedItem(this.lbCurrentObservation);

                foreach (SD sd in sds)
                {
                    main.confirmedObs.sds.Remove(sd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void addSDs2List(List<SD> newSDs)
        {
            foreach (SD sd in newSDs)
            {
                lbCurrentObservation.Items.Add(sd.toObsString());
            }
        }

      

        public void addSD2List(SD newSD)
        {
            if (newSD != null)
            {
                lbCurrentObservation.Items.Add(newSD.toObsString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            Hide();
        }

        public void clearList()
        {
            this.lbCurrentObservation.Items.Clear();
        }

        private void btnNewhypo_Click(object sender, EventArgs e)
        {
            main.actionToolStripMenuItem_Click(sender, e);
        }

      

        private void frmCurrentObservation_Activated(object sender, EventArgs e)
        {
            loadCurrentObs();
        }

      


    }
}
