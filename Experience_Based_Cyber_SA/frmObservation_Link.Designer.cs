namespace Experience_Based_Cyber_SA
{
    partial class frmObservation_Link
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
            this.listSelectedObservation = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.ckbOther = new System.Windows.Forms.CheckBox();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.ckbDesIp = new System.Windows.Forms.CheckBox();
            this.ckbSrcIp = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ckbDesPort = new System.Windows.Forms.CheckBox();
            this.ckbSourcePrt = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listSelectedObservation
            // 
            this.listSelectedObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSelectedObservation.FormattingEnabled = true;
            this.listSelectedObservation.Location = new System.Drawing.Point(-1, 85);
            this.listSelectedObservation.Name = "listSelectedObservation";
            this.listSelectedObservation.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSelectedObservation.Size = new System.Drawing.Size(611, 186);
            this.listSelectedObservation.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbSourcePrt);
            this.groupBox1.Controls.Add(this.ckbDesPort);
            this.groupBox1.Controls.Add(this.txtOther);
            this.groupBox1.Controls.Add(this.ckbOther);
            this.groupBox1.Controls.Add(this.ckbTime);
            this.groupBox1.Controls.Add(this.ckbDesIp);
            this.groupBox1.Controls.Add(this.ckbSrcIp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(688, 79);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose the reason why you connect the selected observations:";
            // 
            // txtOther
            // 
            this.txtOther.Location = new System.Drawing.Point(70, 40);
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(169, 20);
            this.txtOther.TabIndex = 4;
            // 
            // ckbOther
            // 
            this.ckbOther.AutoSize = true;
            this.ckbOther.Location = new System.Drawing.Point(12, 42);
            this.ckbOther.Name = "ckbOther";
            this.ckbOther.Size = new System.Drawing.Size(52, 17);
            this.ckbOther.TabIndex = 3;
            this.ckbOther.Text = "Other";
            this.ckbOther.UseVisualStyleBackColor = true;
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(302, 19);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(79, 17);
            this.ckbTime.TabIndex = 2;
            this.ckbTime.Text = "Same Time";
            this.ckbTime.UseVisualStyleBackColor = true;
            // 
            // ckbDesIp
            // 
            this.ckbDesIp.AutoSize = true;
            this.ckbDesIp.Location = new System.Drawing.Point(151, 19);
            this.ckbDesIp.Name = "ckbDesIp";
            this.ckbDesIp.Size = new System.Drawing.Size(122, 17);
            this.ckbDesIp.TabIndex = 1;
            this.ckbDesIp.Text = "Same Destination IP";
            this.ckbDesIp.UseVisualStyleBackColor = true;
            // 
            // ckbSrcIp
            // 
            this.ckbSrcIp.AutoSize = true;
            this.ckbSrcIp.Location = new System.Drawing.Point(12, 19);
            this.ckbSrcIp.Name = "ckbSrcIp";
            this.ckbSrcIp.Size = new System.Drawing.Size(103, 17);
            this.ckbSrcIp.TabIndex = 0;
            this.ckbSrcIp.Text = "Same Source IP";
            this.ckbSrcIp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Selected Observations:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(613, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(613, 241);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ckbDesPort
            // 
            this.ckbDesPort.AutoSize = true;
            this.ckbDesPort.Location = new System.Drawing.Point(519, 19);
            this.ckbDesPort.Name = "ckbDesPort";
            this.ckbDesPort.Size = new System.Drawing.Size(131, 17);
            this.ckbDesPort.TabIndex = 5;
            this.ckbDesPort.Text = "Same Destination Port";
            this.ckbDesPort.UseVisualStyleBackColor = true;
            // 
            // ckbSourcePrt
            // 
            this.ckbSourcePrt.AutoSize = true;
            this.ckbSourcePrt.Location = new System.Drawing.Point(401, 19);
            this.ckbSourcePrt.Name = "ckbSourcePrt";
            this.ckbSourcePrt.Size = new System.Drawing.Size(112, 17);
            this.ckbSourcePrt.TabIndex = 6;
            this.ckbSourcePrt.Text = "Same Source Port";
            this.ckbSourcePrt.UseVisualStyleBackColor = true;
            // 
            // frmObservation_Link
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 276);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listSelectedObservation);
            this.Name = "frmObservation_Link";
            this.Text = "Link related observations";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listSelectedObservation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.CheckBox ckbOther;
        private System.Windows.Forms.CheckBox ckbTime;
        private System.Windows.Forms.CheckBox ckbDesIp;
        private System.Windows.Forms.CheckBox ckbSrcIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox ckbSourcePrt;
        private System.Windows.Forms.CheckBox ckbDesPort;
    }
}