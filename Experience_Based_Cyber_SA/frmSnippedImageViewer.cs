using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using CSharpWin_JD.CaptureImage;


namespace Experience_Based_Cyber_SA
{
    public partial class frmSnippedImageViewer : Form
    {
        public SD_Image theSD = null;
        public Image image = null;
        public String path = "";

        public frmSnippedImageViewer(String img_path, SD_Image sd)
        {
            InitializeComponent();
            

            path = img_path;
            theSD = sd;


            pictureBox1.Image = Image.FromFile(path);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SizeFromClientSize(pictureBox1.Size);

            this.Text = path;
        }

        //[6-11 UI ADV]
        private void reCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureImageTool capture = new CaptureImageTool();

            //this.Hide();
            //this.Visible = false;
            

            if (capture.ShowDialog() == DialogResult.OK)
            {
                Image newimage = capture.Image;

                if (theSD == null)
                {
                    MessageBox.Show(">_<，observation object does not exist. Please close this window and try again.");
                    return;
                }
                frmSnipObservation newFrmSObs = new frmSnipObservation(theSD, newimage);
                newFrmSObs.ShowDialog();

                MessageBox.Show("^_^ Update completed");

           

                pictureBox1.Image = Image.FromFile(theSD.imagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }

            
        }

        //[6-11 UI ADV]
        private void copyToTheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MessageBox.Show(">_< No image. Please close the window and try again.");
                return;
            }
            //Clipboard.SetImage(this.pictureBox1.Image);
        
            /*
            CaptureImageTool capture = new CaptureImageTool();
           
            if (capture.ShowDialog() == DialogResult.OK)
            {
                Image screenshot = capture.Image;

                Clipboard.SetImage(screenshot);
            }
            */
            //Rectangle bounds = this.Bounds;
            /*
            int X = this.Bounds.X+126;
            int Y = this.Bounds.Y+28;
            int width = this.Bounds.Width - 126;
            int height = this.Bounds.Height - 28;
            
            Rectangle bounds = new Rectangle(X, Y, width, height);

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                Clipboard.SetImage(bitmap);
               
            }
             * */
            int X = this.Bounds.X+pictureBox2.Bounds.X;
            int Y = this.Bounds.Y+pictureBox2.Bounds.Y+25;
            int width = pictureBox2.Bounds.Width;
            int height = pictureBox2.Bounds.Height;

            Rectangle bounds = new Rectangle(X, Y, width, height);

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                Clipboard.SetImage(bitmap);

            }

            MessageBox.Show("^_^ Image has been copied in your clipboard.");
        }

        

        //[06-11 UI ADV]
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDetail.Text = "";
            if(checkedListBox1.GetItemChecked(0) == true){
                txtDetail.Text += "NAME: "+Util_All.getImageName (theSD.imagepath)+";  ";
            }
            if(checkedListBox1.GetItemChecked(1) == true){
                txtDetail.Text += "NOTE: "+theSD.note+";  ";
            }
            if(checkedListBox1.GetItemChecked(2) == true){
                txtDetail.Text += "FROM: "+theSD.imageSource+";  ";
            }
            if(checkedListBox1.GetItemChecked(3) == true){
                txtDetail.Text += "PATH: "+theSD.imagepath+".";
            }
        }


    }
}
