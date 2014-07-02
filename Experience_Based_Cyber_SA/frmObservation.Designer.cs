namespace Experience_Based_Cyber_SA
{
    partial class frmObservation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObservation));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDeleteAct = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listSelectedAction = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectAction = new System.Windows.Forms.Button();
            this.listPreActions = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteObs = new System.Windows.Forms.Button();
            this.btnRelateObs = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.listSelectedObservation = new System.Windows.Forms.ListBox();
            this.listObservations = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectObservation = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteAct);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.listSelectedAction);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectAction);
            this.splitContainer1.Panel1.Controls.Add(this.listPreActions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteObs);
            this.splitContainer1.Panel2.Controls.Add(this.btnRelateObs);
            this.splitContainer1.Panel2.Controls.Add(this.btnDone);
            this.splitContainer1.Panel2.Controls.Add(this.listSelectedObservation);
            this.splitContainer1.Panel2.Controls.Add(this.listObservations);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectObservation);
            this.splitContainer1.Size = new System.Drawing.Size(993, 458);
            this.splitContainer1.SplitterDistance = 446;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnDeleteAct
            // 
            this.btnDeleteAct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAct.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteAct.BackgroundImage")));
            this.btnDeleteAct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeleteAct.Location = new System.Drawing.Point(399, 401);
            this.btnDeleteAct.Name = "btnDeleteAct";
            this.btnDeleteAct.Size = new System.Drawing.Size(41, 42);
            this.btnDeleteAct.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnDeleteAct, "Delete selected actions");
            this.btnDeleteAct.UseVisualStyleBackColor = true;
            this.btnDeleteAct.Click += new System.EventHandler(this.btnDeleteAct_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Confirm what you did...";
            // 
            // listSelectedAction
            // 
            this.listSelectedAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSelectedAction.FormattingEnabled = true;
            this.listSelectedAction.HorizontalScrollbar = true;
            this.listSelectedAction.Location = new System.Drawing.Point(3, 29);
            this.listSelectedAction.Name = "listSelectedAction";
            this.listSelectedAction.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSelectedAction.Size = new System.Drawing.Size(440, 420);
            this.listSelectedAction.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Save for later";
            this.label3.Visible = false;
            // 
            // btnSelectAction
            // 
            this.btnSelectAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectAction.BackgroundImage")));
            this.btnSelectAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectAction.Location = new System.Drawing.Point(190, 324);
            this.btnSelectAction.Name = "btnSelectAction";
            this.btnSelectAction.Size = new System.Drawing.Size(35, 34);
            this.btnSelectAction.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnSelectAction, "Save selected actions for later");
            this.btnSelectAction.UseVisualStyleBackColor = true;
            this.btnSelectAction.Visible = false;
            // 
            // listPreActions
            // 
            this.listPreActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listPreActions.FormattingEnabled = true;
            this.listPreActions.Location = new System.Drawing.Point(0, 364);
            this.listPreActions.Name = "listPreActions";
            this.listPreActions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listPreActions.Size = new System.Drawing.Size(440, 82);
            this.listPreActions.TabIndex = 1;
            this.listPreActions.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(485, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 43);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeleteObs
            // 
            this.btnDeleteObs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteObs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteObs.BackgroundImage")));
            this.btnDeleteObs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeleteObs.Location = new System.Drawing.Point(435, 401);
            this.btnDeleteObs.Name = "btnDeleteObs";
            this.btnDeleteObs.Size = new System.Drawing.Size(41, 42);
            this.btnDeleteObs.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnDeleteObs, "Delete selected observations");
            this.btnDeleteObs.UseVisualStyleBackColor = true;
            this.btnDeleteObs.Click += new System.EventHandler(this.btnDeleteObs_Click);
            // 
            // btnRelateObs
            // 
            this.btnRelateObs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRelateObs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRelateObs.BackgroundImage")));
            this.btnRelateObs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRelateObs.Location = new System.Drawing.Point(386, 402);
            this.btnRelateObs.Name = "btnRelateObs";
            this.btnRelateObs.Size = new System.Drawing.Size(43, 42);
            this.btnRelateObs.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnRelateObs, "Link selected observations");
            this.btnRelateObs.UseVisualStyleBackColor = true;
            this.btnRelateObs.Click += new System.EventHandler(this.btnRelateObs_Click);
            // 
            // btnDone
            // 
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.Location = new System.Drawing.Point(485, 404);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(58, 39);
            this.btnDone.TabIndex = 5;
            this.btnDone.Text = "OK";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // listSelectedObservation
            // 
            this.listSelectedObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSelectedObservation.FormattingEnabled = true;
            this.listSelectedObservation.HorizontalScrollbar = true;
            this.listSelectedObservation.Location = new System.Drawing.Point(3, 29);
            this.listSelectedObservation.Name = "listSelectedObservation";
            this.listSelectedObservation.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSelectedObservation.Size = new System.Drawing.Size(476, 420);
            this.listSelectedObservation.TabIndex = 4;
            // 
            // listObservations
            // 
            this.listObservations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listObservations.FormattingEnabled = true;
            this.listObservations.Location = new System.Drawing.Point(2, 364);
            this.listObservations.Name = "listObservations";
            this.listObservations.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listObservations.Size = new System.Drawing.Size(477, 82);
            this.listObservations.TabIndex = 2;
            this.listObservations.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirm what you observed...";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Save for later";
            this.label4.Visible = false;
            // 
            // btnSelectObservation
            // 
            this.btnSelectObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectObservation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectObservation.BackgroundImage")));
            this.btnSelectObservation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectObservation.Location = new System.Drawing.Point(229, 325);
            this.btnSelectObservation.Name = "btnSelectObservation";
            this.btnSelectObservation.Size = new System.Drawing.Size(35, 34);
            this.btnSelectObservation.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnSelectObservation, "Save selected observations for later");
            this.btnSelectObservation.UseVisualStyleBackColor = true;
            this.btnSelectObservation.Visible = false;
            // 
            // frmObservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(993, 458);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmObservation";
            this.Text = "Confirm your Action and Observation";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSelectAction;
        private System.Windows.Forms.ListBox listPreActions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listObservations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectObservation;
        private System.Windows.Forms.ListBox listSelectedAction;
        private System.Windows.Forms.ListBox listSelectedObservation;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnDeleteAct;
        private System.Windows.Forms.Button btnDeleteObs;
        private System.Windows.Forms.Button btnRelateObs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
    }
}