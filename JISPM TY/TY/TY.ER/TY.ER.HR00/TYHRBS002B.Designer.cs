namespace TY.ER.HR00
{
    partial class TYHRBS002B
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_BISYEAR = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_INQOPTION = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_INQOPTION = new TY.Service.Library.Controls.TYComboBox();
            this.TXT01_BISYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_PTPAYRATE = new TY.Service.Library.Controls.TYTextBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(111, 82);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(192, 82);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_BISYEAR
            // 
            this.LBL51_BISYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BISYEAR.FactoryID = "";
            this.LBL51_BISYEAR.FactoryName = null;
            this.LBL51_BISYEAR.IsCreated = false;
            this.LBL51_BISYEAR.Location = new System.Drawing.Point(5, 12);
            this.LBL51_BISYEAR.Name = "LBL51_BISYEAR";
            this.LBL51_BISYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BISYEAR.TabIndex = 4;
            this.LBL51_BISYEAR.Text = "년 도";
            this.LBL51_BISYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_INQOPTION
            // 
            this.LBL51_INQOPTION.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_INQOPTION.FactoryID = "";
            this.LBL51_INQOPTION.FactoryName = null;
            this.LBL51_INQOPTION.IsCreated = false;
            this.LBL51_INQOPTION.Location = new System.Drawing.Point(5, 40);
            this.LBL51_INQOPTION.Name = "LBL51_INQOPTION";
            this.LBL51_INQOPTION.Size = new System.Drawing.Size(100, 21);
            this.LBL51_INQOPTION.TabIndex = 22;
            this.LBL51_INQOPTION.Text = "성과급지급구분";
            this.LBL51_INQOPTION.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.lblRate);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_PTPAYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BISYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BISYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_INQOPTION);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(358, 132);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_INQOPTION
            // 
            this.CBO01_INQOPTION.FactoryID = "";
            this.CBO01_INQOPTION.FactoryName = null;
            this.CBO01_INQOPTION.Location = new System.Drawing.Point(111, 39);
            this.CBO01_INQOPTION.Name = "CBO01_INQOPTION";
            this.CBO01_INQOPTION.Size = new System.Drawing.Size(50, 20);
            this.CBO01_INQOPTION.TabIndex = 47;
            this.CBO01_INQOPTION.SelectedValueChanged += new System.EventHandler(this.CBO01_INQOPTION_SelectedValueChanged);
            // 
            // TXT01_BISYEAR
            // 
            this.TXT01_BISYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BISYEAR.FactoryID = "";
            this.TXT01_BISYEAR.FactoryName = null;
            this.TXT01_BISYEAR.Location = new System.Drawing.Point(111, 12);
            this.TXT01_BISYEAR.MinLength = 0;
            this.TXT01_BISYEAR.Name = "TXT01_BISYEAR";
            this.TXT01_BISYEAR.Size = new System.Drawing.Size(50, 21);
            this.TXT01_BISYEAR.TabIndex = 41;
            this.TXT01_BISYEAR.TabIndexCustom = false;
            // 
            // TXT01_PTPAYRATE
            // 
            this.TXT01_PTPAYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_PTPAYRATE.FactoryID = "";
            this.TXT01_PTPAYRATE.FactoryName = null;
            this.TXT01_PTPAYRATE.Location = new System.Drawing.Point(167, 39);
            this.TXT01_PTPAYRATE.MinLength = 0;
            this.TXT01_PTPAYRATE.Name = "TXT01_PTPAYRATE";
            this.TXT01_PTPAYRATE.Size = new System.Drawing.Size(66, 21);
            this.TXT01_PTPAYRATE.TabIndex = 48;
            this.TXT01_PTPAYRATE.TabIndexCustom = false;
            this.TXT01_PTPAYRATE.Text = "100";
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(239, 44);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(25, 12);
            this.lblRate.TabIndex = 49;
            this.lblRate.Text = "(%)";
            // 
            // TYHRBS002B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 136);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRBS002B";
            this.Load += new System.EventHandler(this.TYHRBS002B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_BISYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_INQOPTION;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT01_BISYEAR;
        private Service.Library.Controls.TYComboBox CBO01_INQOPTION;
        private System.Windows.Forms.Label lblRate;
        private Service.Library.Controls.TYTextBox TXT01_PTPAYRATE;
    }
}