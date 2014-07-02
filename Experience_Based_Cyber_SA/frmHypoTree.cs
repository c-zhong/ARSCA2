/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      frmHypoTree.cs
/// Function:   H-Tree view
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
using System.Collections;


namespace Experience_Based_Cyber_SA
{
    public partial class frmHypoTree : Form
    {
        public Con_Hypo_Tree tree;
        public Font f;
        public TreeNode selectedNode = null;
        public frmMain main = null;
        public string currentNodeTag = " ■ ";
        
        //[TRACE 3-24] 
        string lastTruthValue = "Unknown"; //not represented as digital, say 0/1/-1, but True/False/Unknown
        string lastHypoContent = "";


        public frmHypoTree(Con_Hypo_Tree t, frmMain m)
        {
            InitializeComponent();
            main = m;

            f = Font;

            tree = t;

            loadHypoTree();
        }

        #region Load form and load HypoTree by a Con_Hypo

        private void frmHypoTree_Load(object sender, EventArgs e)
        {
            
            BindComboBox();
            //Datagridview 
            //dgvBindData(tree.root);
            //this.treeHypo.SelectedNode = null;
            cmb_Temp.Visible = false;
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            this.dataGridViewDetail.Controls.Add(cmb_Temp);
        }
                                                 

        public void loadHypoTree()
        {//Treenode.tag is a Con_hypo instance
            
            if (tree != null && !tree.isNull())
            {
                Con_Hypo root = tree.root;
                TreeNode treeRoot = new TreeNode();
                treeRoot.Text = root.id;
                if (root.type == 1)
                {
                    treeRoot.Text += Util_All.getMissingMarker();
                }
                else if (root.type == 2)
                {
                    treeRoot.Text += Util_All.getFalseMarker();
                }
                if (root == main.lastHypo)
                {
                    treeRoot.Text += currentNodeTag;
                }
                treeRoot.Tag = root;

                
                Util_All.setTreeNodeFont(f, treeRoot,root.truthValue);
                treeHypo.Nodes.Add(treeRoot);
                fillChildTree(root.childrenHypos, treeRoot);
                treeHypo.ExpandAll();
                lblLastHypo.Text = main.lastHypo.id;

            }
            //treeHypo.HideSelection = false;
        }

        public void fillChildTree(List<Con_Hypo> list, TreeNode treeParent)
        {
            foreach (Con_Hypo child in list)
            {
                TreeNode treeChild = new TreeNode();
                treeChild.Text = child.id;
                if (child.type == 1)
                {
                    treeChild.Text += Util_All.getMissingMarker();
                }
                else if (child.type == 2)
                {
                    treeChild.Text += Util_All.getFalseMarker();
                }

                if (child == main.lastHypo)
                {
                    treeChild.Text += currentNodeTag;
                }
                
                treeChild.Tag = child;
                
                Util_All.setTreeNodeFont(f, treeChild, child.truthValue);

                List<Con_Hypo> childList = child.childrenHypos;
                fillChildTree(childList, treeChild);
                treeParent.Nodes.Add(treeChild);
            }
        }

        //update current treeview color and * tag
        public void updateTreeView(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                Con_Hypo hypo = (Con_Hypo)node.Tag;
                Util_All.setTreeNodeFont(f, node, hypo.truthValue);

                node.Text = hypo.id;
                
                if (hypo.type == 1)
                {
                    node.Text += Util_All.getMissingMarker();
                }
                else if (hypo.type == 2)
                {
                    node.Text += Util_All.getFalseMarker();
                }

                if (node == selectedNode)
                {
                    node.Text+= currentNodeTag;
                }
               
                updateTreeView(node.Nodes);
            }
        }


        private void updateTreeViewAndDataGridView()
        {
            dgvBindData();

            this.cmb_Temp.Visible = false;
           
            updateTreeView(treeHypo.Nodes);
            lblLastHypo.Text = selectedNode.Text;
        }

        #endregion

       

        #region Bind TreeHypo, ComboBox, DataGridView Data

       

        private void BindComboBox()
        {
            DataTable dtValue = new DataTable();
            dtValue.Columns.Add("Value");
            dtValue.Columns.Add("Name");
            DataRow drValue = dtValue.NewRow();
            drValue[0] = "1";
            drValue[1] = "True";
            dtValue.Rows.Add(drValue);
            drValue = dtValue.NewRow();
            drValue[0] = "-1";
            drValue[1] = "False";
            dtValue.Rows.Add(drValue);
            drValue = dtValue.NewRow();
            drValue[0] = "0";
            drValue[1] = "Unknown";
            dtValue.Rows.Add(drValue);
            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtValue;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dgvBindData()
        {
            if (selectedNode != null)
            {
                Con_Hypo hypo = (Con_Hypo)selectedNode.Tag;
                if (hypo != null) 
                {
                    DataTable dt = Util_All.getHypoDetail(hypo);
                    dataGridViewDetail.DataSource = dt;
                    dataGridViewDetail.Columns[0].ReadOnly = true;
                    //color changed in DataBindingComplete
                }
                if (hypo.truthValue == -1)
                {
                    dataGridViewDetail.BackgroundColor = Color.LightGray;
                }

                dataGridViewDetail.Columns[0].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewDetail.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        #endregion

        

        #region Visible/Invisible ComboBox

        private void dataGridViewDetail_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            //treeHypo.Focus();
        }

        private void dataGridViewDetail_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            //treeHypo.Focus();
        }

        private void dataGridViewDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            /*
            for (int i = 0; i < this.dataGridViewDetail.Rows.Count; i++)
            {
                if (dataGridViewDetail.Rows[i].Cells[0].Value != null && dataGridViewDetail.Rows[i].Cells[0].ColumnIndex == 0)
                {
                    dataGridViewDetail.Rows[i].Cells[0].Tag = dataGridViewDetail.Rows[i].Cells[0].Value.ToString();
                    if (dataGridViewDetail.Rows[i].Cells[0].Value.ToString() == "1")
                    {
                        dataGridViewDetail.Rows[i].Cells[0].Value = "True";
                    }
                    else if (dataGridViewDetail.Rows[i].Cells[0].Value.ToString() == "-1")
                    {
                        dataGridViewDetail.Rows[i].Cells[0].Value = "False";
                        dataGridViewDetail.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else if (dataGridViewDetail.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridViewDetail.Rows[i].Cells[0].Value = "Unknown";
                    }
                    
                }
             
             }
             * * */
            // treeHypo.Focus();
            if(dataGridViewDetail.Rows[0].Cells[1].Value != null)
            {
                dataGridViewDetail.Rows[0].Cells[1].Tag = dataGridViewDetail.Rows[0].Cells[1].Value.ToString();
                if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "1")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "True";
                }
                else if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "-1")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "False";
                    dataGridViewDetail.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "0")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "Unknown";
                }

            }
        }
        
        private void dataGridViewDetail_CurrentCellChanged(object sender, EventArgs e)
        {

            /*
            if (this.dataGridViewDetail.CurrentCell == null)
                return;

            if (this.dataGridViewDetail.CurrentCell.ColumnIndex == 0 && dataGridViewDetail.CurrentCell.RowIndex > 0)
            {
                Rectangle rect = dataGridViewDetail.GetCellDisplayRectangle(dataGridViewDetail.CurrentCell.ColumnIndex, dataGridViewDetail.CurrentCell.RowIndex, false);
                string truthValue = dataGridViewDetail.CurrentCell.Value.ToString();
                if (truthValue == "True")
                {
                    cmb_Temp.Text = "True";
                }
                else if (truthValue == "False")
                {
                    cmb_Temp.Text = "False";
                }
                else
                {
                    cmb_Temp.Text = "Unknown";
                }
                cmb_Temp.Left = rect.Left;
                cmb_Temp.Top = rect.Top;
                cmb_Temp.Width = rect.Width;
                cmb_Temp.Height = rect.Height;
                cmb_Temp.Visible = true;

            }
            else
            {
                cmb_Temp.Visible = false;
            }
           // treeHypo.Focus();
             * */
        }

        
        private void dataGridViewDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDetail.CurrentCell.RowIndex == 0 && dataGridViewDetail.CurrentCell.ColumnIndex == 1)
            {
                Rectangle rect = dataGridViewDetail.GetCellDisplayRectangle(dataGridViewDetail.CurrentCell.ColumnIndex, dataGridViewDetail.CurrentCell.RowIndex, false);
                string truthValue = dataGridViewDetail.CurrentCell.Value.ToString();
                
                if (truthValue.Equals("1") || truthValue.Equals("True"))
                {
                    cmb_Temp.Text = "True";
                }
                else if (truthValue.Equals("-1") || truthValue.Equals("False"))
                {
                    cmb_Temp.Text = "False";
                }
                else if (truthValue.Equals("0") || truthValue.Equals("Unknown"))
                {
                    cmb_Temp.Text = "Unknown";
                }
                cmb_Temp.Left = rect.Left;
                cmb_Temp.Top = rect.Top;
                cmb_Temp.Width = rect.Width;
                cmb_Temp.Height = rect.Height;
                cmb_Temp.Visible = true;

            }
            else
            {
                cmb_Temp.Visible = false;

                if (dataGridViewDetail.CurrentCell.RowIndex == 2 && dataGridViewDetail.CurrentCell.ColumnIndex == 1)
                {
                    //[TRACE 3-24]
                    lastHypoContent = dataGridViewDetail.CurrentCell.Value.ToString();
                }
            }

        }  

        #endregion

        
        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedNode == null)
                return;
            
            Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;

            //[TRACE 3-24]
            lastTruthValue = nodehypo.truthValueString();
                       
            //if (((ComboBox)sender).Text == "True")
            if(cmb_Temp.SelectedIndex == 0 )
            {
                dataGridViewDetail.CurrentCell.Value = "True";
                dataGridViewDetail.CurrentCell.Tag = "1";

                if (selectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.truthValue = 1;

                    if (nodehypo.type == 1)
                    {
                        /*[4-07 REMOVE MISSING ALERT]
                        Con_MissingAlert missing = Util_All.constructMissingAlert(nodehypo);

                        if (missing != null)
                        {
                            if (!main.trueMissingHypos.Contains(missing.hypo))
                            {
                                main.trueMissingHypos.Add(missing.hypo);
                                Util_All.addMissingAlert(main.missingAlertDt, missing);
                                MessageBox.Show("New Missing Alert:+\r\n" + missing.toString());
                                main.updateMissingDGV();
                            }
                        }
                        */
                        
                    }
                }
                
            }
            //else if (((ComboBox)sender).Text == "False")
            else if(cmb_Temp.SelectedIndex == 1)
            {
                dataGridViewDetail.CurrentCell.Value = "False";
                dataGridViewDetail.CurrentCell.Tag = "-1";

                //set all decedents false
                if (selectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.setFalse();//not only this one, but also its offsprings
                }

               // dataGridViewDetail.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
            }
            else
            {
                dataGridViewDetail.CurrentCell.Value = "Unknown";
                dataGridViewDetail.CurrentCell.Tag = "0";

                if (selectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.truthValue = 0;
                   
                }
            }

            //begin [TRACE 3-24]
            string currentTruthValue = dataGridViewDetail.CurrentCell.Value.ToString();
            if (!currentTruthValue.Equals(lastTruthValue))
            {
                string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.CHANGE_TRUTH_VALUE_3, nodehypo, lastTruthValue, currentTruthValue);
                main.trace.AddItem(Util_All.currentTime(), op);
            }
            //end

           updateTreeViewAndDataGridView();
        }


 
        private void treeHypo_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (treeHypo.SelectedNode != null)
            {
                selectedNode = treeHypo.SelectedNode;
                dgvBindData();
            } 
        }



        

        private void dataGridViewDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDetail.CurrentCell.ColumnIndex == 1)
            {
                Con_Hypo hypo = (Con_Hypo)selectedNode.Tag;
                int row = dataGridViewDetail.CurrentCell.RowIndex;
                if (row == 1)
                {
                    MessageBox.Show("Sorry, you can't edit Id.");
                }
                else if (row == 2)
                {
                    //begin [TRACE 3-24]
                    lastHypoContent = hypo.hypo;
                    string currentHypoContent = dataGridViewDetail.CurrentCell.Value.ToString();
                    if (!lastHypoContent.Equals(currentHypoContent))
                    {
                        string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.CHANGE_CONTENT_3, hypo, lastHypoContent, currentHypoContent);
                        main.trace.AddItem(Util_All.currentTime(), op);
                    }
                    //end
                    
                    hypo.hypo = dataGridViewDetail.CurrentCell.Value.ToString();
                }
            }
        }

        private void treeHypo_DoubleClick(object sender, EventArgs e)
        {
            selectedNode = treeHypo.SelectedNode;
            

            Con_Hypo hypo  = (Con_Hypo)selectedNode.Tag;
            if (hypo.id.Substring(0, 2).Equals("HT"))
                return;

            //begin [TRACE 3-24]
            string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.JUMP_FROM_TO_2, main.lastHypo, hypo);
            main.trace.AddItem(Util_All.currentTime(), op);
            //end

            main.lastHypo = hypo;
            LinkedList<Con_Observation> obsList = Con_Hypo.getContextObs(hypo);

            main.sortETNList(obsList);
            main.updateTreeViewFont();

            updateTreeViewAndDataGridView();

             
        }


        private void btnAddChild_Click(object sender, EventArgs e)
        {
            if (selectedNode == null)
            {
                MessageBox.Show("You need to select a node in the tree first :)");
            }
            else
            {
                Con_Hypo hypo = (Con_Hypo)selectedNode.Tag;
                Con_Experience_Unit nextEU = hypo.next_unit;

                frmHypothesis fhp = null;
                if (nextEU == null)
                {
                    MessageBox.Show("Sorry, you can't make new hypothesis because there is no new observation got after generating " + hypo.id+"\r\n You'd better generate a sibling hypothesis of "+hypo.id);
                    return;
                }
                else
                {
                    fhp = new frmHypothesis(main, nextEU.obs, main.confirmedHypo);

                }
                fhp.ShowDialog();

                if (!main.isfrmHypothesisDone)
                    return;

                main.isfrmHypothesisDone = false;

                main.lastHypo = hypo;

                main.newHypo(nextEU, null, main.confirmedHypo,Con_Operation.Enum_Hypo_Op.ADD_CHILD_2);

                //this.treeHypo.Focus();
                //updateTreeViewAndDataGridView();   

                treeHypo.Nodes.Clear();
                loadHypoTree();
                dgvBindData();

                main.refreshETree();
            }
        }



    }
}
