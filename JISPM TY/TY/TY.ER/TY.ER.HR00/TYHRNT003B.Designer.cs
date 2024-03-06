namespace TY.ER.HR00
{
    partial class TYHRNT003B
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_RESIGNDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_RESIGNDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_PMYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_PMYYMM = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GOKCR = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GOKCR = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_INQOPTION = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_INQOPTION = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_KBSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBSABUN = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SDATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(266, 233);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_RESIGNDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_RESIGNDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PMYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PMYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(609, 285);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_RESIGNDATE
            // 
            this.DTP01_RESIGNDATE.FactoryID = "";
            this.DTP01_RESIGNDATE.FactoryName = null;
            this.DTP01_RESIGNDATE.Location = new System.Drawing.Point(266, 178);
            this.DTP01_RESIGNDATE.Name = "DTP01_RESIGNDATE";
            this.DTP01_RESIGNDATE.Size = new System.Drawing.Size(117, 21);
            this.DTP01_RESIGNDATE.TabIndex = 60;
            // 
            // LBL51_RESIGNDATE
            // 
            this.LBL51_RESIGNDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_RESIGNDATE.FactoryID = "";
            this.LBL51_RESIGNDATE.FactoryName = null;
            this.LBL51_RESIGNDATE.IsCreated = false;
            this.LBL51_RESIGNDATE.Location = new System.Drawing.Point(160, 178);
            this.LBL51_RESIGNDATE.Name = "LBL51_RESIGNDATE";
            this.LBL51_RESIGNDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_RESIGNDATE.TabIndex = 59;
            this.LBL51_RESIGNDATE.Text = "퇴사일자";
            this.LBL51_RESIGNDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_PMYYMM
            // 
            this.DTP01_PMYYMM.FactoryID = "";
            this.DTP01_PMYYMM.FactoryName = null;
            this.DTP01_PMYYMM.Location = new System.Drawing.Point(266, 151);
            this.DTP01_PMYYMM.Name = "DTP01_PMYYMM";
            this.DTP01_PMYYMM.Size = new System.Drawing.Size(82, 21);
            this.DTP01_PMYYMM.TabIndex = 58;
            // 
            // LBL51_PMYYMM
            // 
            this.LBL51_PMYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PMYYMM.FactoryID = "";
            this.LBL51_PMYYMM.FactoryName = null;
            this.LBL51_PMYYMM.IsCreated = false;
            this.LBL51_PMYYMM.Location = new System.Drawing.Point(160, 151);
            this.LBL51_PMYYMM.Name = "LBL51_PMYYMM";
            this.LBL51_PMYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PMYYMM.TabIndex = 57;
            this.LBL51_PMYYMM.Text = "급여적용년월";
            this.LBL51_PMYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GOKCR
            // 
            this.CBO01_GOKCR.FactoryID = "";
            this.CBO01_GOKCR.FactoryName = null;
            this.CBO01_GOKCR.Location = new System.Drawing.Point(266, 125);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(140, 20);
            this.CBO01_GOKCR.TabIndex = 56;
            // 
            // LBL51_GOKCR
            // 
            this.LBL51_GOKCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GOKCR.FactoryID = "";
            this.LBL51_GOKCR.FactoryName = null;
            this.LBL51_GOKCR.IsCreated = false;
            this.LBL51_GOKCR.Location = new System.Drawing.Point(160, 125);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 55;
            this.LBL51_GOKCR.Text = "작업구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_INQOPTION
            // 
            this.CBO01_INQOPTION.FactoryID = "";
            this.CBO01_INQOPTION.FactoryName = null;
            this.CBO01_INQOPTION.Location = new System.Drawing.Point(266, 99);
            this.CBO01_INQOPTION.Name = "CBO01_INQOPTION";
            this.CBO01_INQOPTION.Size = new System.Drawing.Size(140, 20);
            this.CBO01_INQOPTION.TabIndex = 54;
            // 
            // LBL51_INQOPTION
            // 
            this.LBL51_INQOPTION.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_INQOPTION.FactoryID = "";
            this.LBL51_INQOPTION.FactoryName = null;
            this.LBL51_INQOPTION.IsCreated = false;
            this.LBL51_INQOPTION.Location = new System.Drawing.Point(160, 99);
            this.LBL51_INQOPTION.Name = "LBL51_INQOPTION";
            this.LBL51_INQOPTION.Size = new System.Drawing.Size(100, 21);
            this.LBL51_INQOPTION.TabIndex = 53;
            this.LBL51_INQOPTION.Text = "정산구분";
            this.LBL51_INQOPTION.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_KBSABUN
            // 
            this.CBH01_KBSABUN.BindedDataRow = null;
            this.CBH01_KBSABUN.CodeBoxWidth = 0;
            this.CBH01_KBSABUN.DummyValue = null;
            this.CBH01_KBSABUN.FactoryID = "";
            this.CBH01_KBSABUN.FactoryName = null;
            this.CBH01_KBSABUN.Location = new System.Drawing.Point(266, 74);
            this.CBH01_KBSABUN.MinLength = 0;
            this.CBH01_KBSABUN.Name = "CBH01_KBSABUN";
            this.CBH01_KBSABUN.Size = new System.Drawing.Size(178, 20);
            this.CBH01_KBSABUN.TabIndex = 38;
            // 
            // LBL51_KBSABUN
            // 
            this.LBL51_KBSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBSABUN.FactoryID = "";
            this.LBL51_KBSABUN.FactoryName = null;
            this.LBL51_KBSABUN.IsCreated = false;
            this.LBL51_KBSABUN.Location = new System.Drawing.Point(160, 74);
            this.LBL51_KBSABUN.Name = "LBL51_KBSABUN";
            this.LBL51_KBSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBSABUN.TabIndex = 39;
            this.LBL51_KBSABUN.Text = "사 번";
            this.LBL51_KBSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SDATE
            // 
            this.TXT01_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SDATE.FactoryID = "";
            this.TXT01_SDATE.FactoryName = null;
            this.TXT01_SDATE.Location = new System.Drawing.Point(266, 46);
            this.TXT01_SDATE.MinLength = 0;
            this.TXT01_SDATE.Name = "TXT01_SDATE";
            this.TXT01_SDATE.Size = new System.Drawing.Size(52, 21);
            this.TXT01_SDATE.TabIndex = 36;
            this.TXT01_SDATE.TabIndexCustom = false;
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(160, 47);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 37;
            this.LBL51_SDATE.Text = "귀속년도";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(185, 233);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 1;
            this.BTN61_BATCH.Text = "생성";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // TYHRNT003B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 289);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRNT003B";
            this.Load += new System.EventHandler(this.TYHRNT003B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Service.Library.Controls.TYButton BTN61_BATCH;
        private Service.Library.Controls.TYTextBox TXT01_SDATE;
        private Service.Library.Controls.TYLabel LBL51_SDATE;
        private Service.Library.Controls.TYCodeBox CBH01_KBSABUN;
        private Service.Library.Controls.TYLabel LBL51_KBSABUN;
        private Service.Library.Controls.TYComboBox CBO01_GOKCR;
        private Service.Library.Controls.TYLabel LBL51_GOKCR;
        private Service.Library.Controls.TYComboBox CBO01_INQOPTION;
        private Service.Library.Controls.TYLabel LBL51_INQOPTION;
        private Service.Library.Controls.TYLabel LBL51_PMYYMM;
        private Service.Library.Controls.TYDatePicker DTP01_PMYYMM;
        private Service.Library.Controls.TYDatePicker DTP01_RESIGNDATE;
        private Service.Library.Controls.TYLabel LBL51_RESIGNDATE;
    }
}