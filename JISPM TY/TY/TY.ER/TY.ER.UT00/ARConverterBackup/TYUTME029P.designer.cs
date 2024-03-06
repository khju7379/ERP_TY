namespace TY.ER.UT00
{
    partial class TYUTME029P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH01_CHHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CHHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_UTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_UTDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_DOWN = new TY.Service.Library.Controls.TYButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.CBO01_GPRTGN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GPRTGN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(512, 481);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_CHHWAJU
            // 
            this.CBH01_CHHWAJU.BindedDataRow = null;
            this.CBH01_CHHWAJU.CodeBoxWidth = 0;
            this.CBH01_CHHWAJU.DummyValue = null;
            this.CBH01_CHHWAJU.FactoryID = "";
            this.CBH01_CHHWAJU.FactoryName = null;
            this.CBH01_CHHWAJU.Location = new System.Drawing.Point(512, 399);
            this.CBH01_CHHWAJU.MinLength = 0;
            this.CBH01_CHHWAJU.Name = "CBH01_CHHWAJU";
            this.CBH01_CHHWAJU.Size = new System.Drawing.Size(250, 20);
            this.CBH01_CHHWAJU.TabIndex = 2;
            // 
            // LBL51_CHHWAJU
            // 
            this.LBL51_CHHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHHWAJU.FactoryID = "";
            this.LBL51_CHHWAJU.FactoryName = null;
            this.LBL51_CHHWAJU.IsCreated = false;
            this.LBL51_CHHWAJU.Location = new System.Drawing.Point(406, 399);
            this.LBL51_CHHWAJU.Name = "LBL51_CHHWAJU";
            this.LBL51_CHHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHHWAJU.TabIndex = 3;
            this.LBL51_CHHWAJU.Text = "화주";
            this.LBL51_CHHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_UTDATE
            // 
            this.DTP01_UTDATE.FactoryID = "";
            this.DTP01_UTDATE.FactoryName = null;
            this.DTP01_UTDATE.Location = new System.Drawing.Point(512, 372);
            this.DTP01_UTDATE.Name = "DTP01_UTDATE";
            this.DTP01_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_UTDATE.TabIndex = 4;
            // 
            // LBL51_UTDATE
            // 
            this.LBL51_UTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_UTDATE.FactoryID = "";
            this.LBL51_UTDATE.FactoryName = null;
            this.LBL51_UTDATE.IsCreated = false;
            this.LBL51_UTDATE.Location = new System.Drawing.Point(406, 372);
            this.LBL51_UTDATE.Name = "LBL51_UTDATE";
            this.LBL51_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_UTDATE.TabIndex = 5;
            this.LBL51_UTDATE.Text = "매출일자";
            this.LBL51_UTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GPRTGN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GPRTGN);
            this.GBX80_CONTROLS.Controls.Add(this.txtFolder);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SEARCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_DOWN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_UTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_UTDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(512, 453);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(433, 21);
            this.txtFolder.TabIndex = 27;
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(951, 453);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 26;
            this.BTN61_SEARCH.Text = "경로지정";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // BTN61_DOWN
            // 
            this.BTN61_DOWN.FactoryID = "";
            this.BTN61_DOWN.FactoryName = null;
            this.BTN61_DOWN.Location = new System.Drawing.Point(593, 481);
            this.BTN61_DOWN.Name = "BTN61_DOWN";
            this.BTN61_DOWN.Size = new System.Drawing.Size(75, 21);
            this.BTN61_DOWN.TabIndex = 13;
            this.BTN61_DOWN.Text = "다운로드";
            this.BTN61_DOWN.UseVisualStyleBackColor = true;
            this.BTN61_DOWN.Click += new System.EventHandler(this.BTN61_DOWN_Click);
            // 
            // CBO01_GPRTGN
            // 
            this.CBO01_GPRTGN.FactoryID = "";
            this.CBO01_GPRTGN.FactoryName = null;
            this.CBO01_GPRTGN.Location = new System.Drawing.Point(512, 426);
            this.CBO01_GPRTGN.Name = "CBO01_GPRTGN";
            this.CBO01_GPRTGN.Size = new System.Drawing.Size(60, 20);
            this.CBO01_GPRTGN.TabIndex = 29;
            // 
            // LBL51_GPRTGN
            // 
            this.LBL51_GPRTGN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GPRTGN.FactoryID = "";
            this.LBL51_GPRTGN.FactoryName = null;
            this.LBL51_GPRTGN.IsCreated = false;
            this.LBL51_GPRTGN.Location = new System.Drawing.Point(406, 426);
            this.LBL51_GPRTGN.Name = "LBL51_GPRTGN";
            this.LBL51_GPRTGN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GPRTGN.TabIndex = 28;
            this.LBL51_GPRTGN.Text = "기타매출포함";
            this.LBL51_GPRTGN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUTME029P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME029P";
            this.Load += new System.EventHandler(this.TYUTME029P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_CHHWAJU;
        private TY.Service.Library.Controls.TYLabel LBL51_CHHWAJU;
        private TY.Service.Library.Controls.TYDatePicker DTP01_UTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_UTDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_DOWN;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtFolder;
        private Service.Library.Controls.TYButton BTN61_SEARCH;
        private Service.Library.Controls.TYComboBox CBO01_GPRTGN;
        private Service.Library.Controls.TYLabel LBL51_GPRTGN;
    }
}