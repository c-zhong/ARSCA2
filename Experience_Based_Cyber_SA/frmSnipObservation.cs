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
    public partial class frmSnipObservation : Form
    {
        public Image image = null;
        public frmMain main = null;
        public SD_Image newImageSD = null;

        public frmSnipObservation(SD_Image sd, Image image1)
        {
            InitializeComponent();
            txtImageSource.Visible = false;

            image = image1;
            this.pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if(sd!=null)
                newImageSD = sd;
        }

        public frmSnipObservation(frmMain main1, Image image1)
        {
            InitializeComponent();
            txtImageSource.Visible = false;

            image = image1;
            this.pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            main = main1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           // main.obsHasCreated = false;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtImageSource.Text.Trim().Equals(""))
            {
                txtImageSource.Visible = true;
                MessageBox.Show("Please specify where the screenshot is captured in the textbox.");
                txtImageSource.Focus();
                return;
            }

            string path = Util_All.storeImage(pictureBox1.Image);
            if (path.Equals(""))
            {
                MessageBox.Show("Fail to store the image!");
                return; 
            }

            

            if (main != null)
            {
                SD_Image newSD = new SD_Image(path, txtNote.Text.Trim(), txtImageSource.Text.Trim());
                main.confirmedObs.add(newSD);

                //[7-24 ONGOING OBS]
              //[9-11 Bug]  main.currentobs.addSD2List(newSD);

                main.confirmedAct.add(newSD);
            }
            else if (newImageSD != null)
            {
                newImageSD.setSDImage(path, txtNote.Text.Trim(), txtImageSource.Text.Trim()); 
            }

           // main.obsHasCreated = true;

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 3)
            {
                txtImageSource.Text = comboBox1.SelectedItem.ToString();
                txtImageSource.Visible = false;
            }
            else if(comboBox1.SelectedIndex == 3){
                txtImageSource.Text = "";
                txtImageSource.Visible = true;
            }
           
        }

    }
}
