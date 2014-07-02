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
    public partial class frmFilter : Form
    {
        frmMain main = null;
        public string currentCondition = "";
        string original_condition = "";
        DataGridView currentdgv = null;

        public frmFilter(DataGridView current, frmMain m, string condition)
        {
            InitializeComponent();
            original_condition = condition;

           // btnOK.DialogResult = DialogResult.OK;
           // btnCancel.DialogResult = DialogResult.Cancel;
            main = m;
            currentCondition = condition;
            currentdgv = current;

            loadCmbField(current);

            cmbAndOR.SelectedIndex = 0;
            cmbField.SelectedIndex = 0;
            cmbRela.SelectedIndex = 0;

            txtFilterRule.Text = currentCondition;

        }


        private void loadCmbField(DataGridView current)
        {
            cmbField.Items.Clear();
            if (current != null)
            {
                int colnum = current.Columns.Count;
                //cmbToolRela.Items.Add("ALL");
                for (int i = 0; i < colnum; i++)
                {
                    cmbField.Items.Add(current.Columns[i].HeaderText);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbAndOR.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose AND / OR");
                this.cmbAndOR.Focus();
                return;
            }

            if (cmbField.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a field to filter");
                this.cmbField.Focus();
                return;
            }

            if (this.cmbRela.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an relation");
                this.cmbRela.Focus();
                return;
            }

            else if (this.txtValue.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input a filter value in the text box");
                txtValue.Focus();
                return;
            }



            string newrule = cmbField.SelectedItem.ToString() + " " + cmbRela.SelectedItem.ToString() + " \'" + txtValue.Text + "\'";


            if (!newrule.Trim().Equals(""))
            {
                if (!currentCondition.Equals(""))
                {
                    currentCondition += " "+cmbAndOR.SelectedItem.ToString()+" ";
                }


                if (currentCondition.Contains(newrule))
                {
                    MessageBox.Show("This condition exists.");
                }
                else
                {
                    currentCondition += newrule;
                }
               
            }

            txtFilterRule.Text = currentCondition;

            this.cmbField.SelectedIndex = -1;
            this.cmbRela.SelectedIndex = -1;
            this.txtValue.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(original_condition.Equals(currentCondition))
            {
                DialogResult result1 = MessageBox.Show(@"You haven't add any new condition. Add one by pressing the button with PLUS icon. 
                Do you want to add a new condition?",
        "Attention",  MessageBoxButtons.YesNo);
                
                if (result1 == DialogResult.Yes)
                {
                    this.btnAdd.Focus();
                    return;
                }
                this.DialogResult = DialogResult.Cancel;
            }


            if (main.getCurrentDGVIndex() == 1)
            {
                main.ids_filter_rule = currentCondition;
            }
            else if(main.getCurrentDGVIndex() == 2)
            {
                main.firewall_filter_rule = currentCondition;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtFilterRule_TextChanged(object sender, EventArgs e)
        {
            currentCondition = txtFilterRule.Text;
        }

      
    }
}
