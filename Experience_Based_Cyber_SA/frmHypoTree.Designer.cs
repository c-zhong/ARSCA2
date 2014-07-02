namespace Experience_Based_Cyber_SA
{
    partial class frmHypoTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeHypo = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGuide = new System.Windows.Forms.TextBox();
            this.lblLastHypo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddChild = new System.Windows.Forms.Button();
            this.cmb_Temp = new System.Windows.Forms.ComboBox();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeHypo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtGuide);
            this.splitContainer1.Panel2.Controls.Add(this.lblLastHypo);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddChild);
            this.splitContainer1.Panel2.Controls.Add(this.cmb_Temp);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewDetail);
            this.splitContainer1.Size = new System.Drawing.Size(587, 367);
            this.splitContainer1.SplitterDistance = 306;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeHypo
            // 
            this.treeHypo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeHypo.HideSelection = false;
            this.treeHypo.Location = new System.Drawing.Point(0, 0);
            this.treeHypo.Name = "treeHypo";
            this.treeHypo.Size = new System.Drawing.Size(306, 367);
            this.treeHypo.TabIndex = 0;
            this.treeHypo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeHypo_AfterSelect_1);
            this.treeHypo.DoubleClick += new System.EventHandler(this.treeHypo_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Guide:";
            // 
            // txtGuide
            // 
            this.txtGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtGuide.Location = new System.Drawing.Point(6, 278);
            this.txtGuide.Multiline = true;
            this.txtGuide.Name = "txtGuide";
            this.txtGuide.Size = new System.Drawing.Size(268, 64);
            this.txtGuide.TabIndex = 5;
            this.txtGuide.Text = "1. Change context : double click the node in the H-tree\r\n2. Change truth value:cl" +
                "ick the cell,choose a value and then click anywhere else.\r\n\r\n\r\n\r\n";
            // 
            // lblLastHypo
            // 
            this.lblLastHypo.AutoSize = true;
            this.lblLastHypo.Location = new System.Drawing.Point(122, 345);
            this.lblLastHypo.Name = "lblLastHypo";
            this.lblLastHypo.Size = new System.Drawing.Size(0, 13);
            this.lblLastHypo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Hypothesis ID:";
            // 
            // btnAddChild
            // 
            this.btnAddChild.Location = new System.Drawing.Point(3, 188);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(163, 44);
            this.btnAddChild.TabIndex = 2;
            this.btnAddChild.Text = "Add Another Child Option";
            this.btnAddChild.UseVisualStyleBackColor = true;
            this.btnAddChild.Click += new System.EventHandler(this.btnAddChild_Click);
            // 
            // cmb_Temp
            // 
            this.cmb_Temp.FormattingEnabled = true;
            this.cmb_Temp.Location = new System.Drawing.Point(23, 12);
            this.cmb_Temp.Name = "cmb_Temp";
            this.cmb_Temp.Size = new System.Drawing.Size(121, 21);
            this.cmb_Temp.TabIndex = 1;
            // 
            // dataGridViewDetail
            // 
            this.dataGridViewDetail.AllowUserToAddRows = false;
            this.dataGridViewDetail.AllowUserToDeleteRows = false;
            this.dataGridViewDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDetail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewDetail.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.Size = new System.Drawing.Size(277, 182);
            this.dataGridViewDetail.TabIndex = 0;
            this.dataGridViewDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDetail_CellClick);
            this.dataGridViewDetail.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDetail_CellEndEdit);
            this.dataGridViewDetail.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewDetail_ColumnWidthChanged);
            this.dataGridViewDetail.CurrentCellChanged += new System.EventHandler(this.dataGridViewDetail_CurrentCellChanged);
            this.dataGridViewDetail.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewDetail_DataBindingComplete);
            this.dataGridViewDetail.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewDetail_Scroll);
            // 
            // frmHypoTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 367);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmHypoTree";
            this.Text = "H-Tree";
            this.Load += new System.EventHandler(this.frmHypoTree_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeHypo;
        private System.Windows.Forms.DataGridView dataGridViewDetail;
        private System.Windows.Forms.ComboBox cmb_Temp;
        private System.Windows.Forms.Button btnAddChild;
        private System.Windows.Forms.Label lblLastHypo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGuide;
    }
}