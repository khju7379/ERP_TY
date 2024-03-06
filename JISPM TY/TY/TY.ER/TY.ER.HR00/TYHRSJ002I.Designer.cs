namespace TY.ER.HR00
{
    partial class TYHRSJ002I
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_SJIOAMT = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SJIOAMT = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SJIOCODE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SJIODATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SJIORKAC = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SJIORKAC = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SJIOSABUN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_SJIODATE = new TY.Service.Library.Controls.TYDatePicker();
            this.CBH01_SJIOCODE = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_SJIOSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(907, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(826, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // TXT01_SJIOAMT
            // 
            this.TXT01_SJIOAMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SJIOAMT.FactoryID = "";
            this.TXT01_SJIOAMT.FactoryName = null;
            this.TXT01_SJIOAMT.Location = new System.Drawing.Point(111, 39);
            this.TXT01_SJIOAMT.MinLength = 0;
            this.TXT01_SJIOAMT.Name = "TXT01_SJIOAMT";
            this.TXT01_SJIOAMT.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SJIOAMT.TabIndex = 2;
            this.TXT01_SJIOAMT.TabIndexCustom = false;
            // 
            // LBL51_SJIOAMT
            // 
            this.LBL51_SJIOAMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SJIOAMT.FactoryID = "";
            this.LBL51_SJIOAMT.FactoryName = null;
            this.LBL51_SJIOAMT.IsCreated = false;
            this.LBL51_SJIOAMT.Location = new System.Drawing.Point(5, 39);
            this.LBL51_SJIOAMT.Name = "LBL51_SJIOAMT";
            this.LBL51_SJIOAMT.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SJIOAMT.TabIndex = 3;
            this.LBL51_SJIOAMT.Text = "상조회 금액";
            this.LBL51_SJIOAMT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SJIOCODE
            // 
            this.LBL51_SJIOCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SJIOCODE.FactoryID = "";
            this.LBL51_SJIOCODE.FactoryName = null;
            this.LBL51_SJIOCODE.IsCreated = false;
            this.LBL51_SJIOCODE.Location = new System.Drawing.Point(217, 12);
            this.LBL51_SJIOCODE.Name = "LBL51_SJIOCODE";
            this.LBL51_SJIOCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SJIOCODE.TabIndex = 5;
            this.LBL51_SJIOCODE.Text = "상조회 계정코드";
            this.LBL51_SJIOCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SJIODATE
            // 
            this.LBL51_SJIODATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SJIODATE.FactoryID = "";
            this.LBL51_SJIODATE.FactoryName = null;
            this.LBL51_SJIODATE.IsCreated = false;
            this.LBL51_SJIODATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_SJIODATE.Name = "LBL51_SJIODATE";
            this.LBL51_SJIODATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SJIODATE.TabIndex = 7;
            this.LBL51_SJIODATE.Text = "상조회입출금일자";
            this.LBL51_SJIODATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SJIORKAC
            // 
            this.TXT01_SJIORKAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SJIORKAC.FactoryID = "";
            this.TXT01_SJIORKAC.FactoryName = null;
            this.TXT01_SJIORKAC.Location = new System.Drawing.Point(323, 39);
            this.TXT01_SJIORKAC.MinLength = 0;
            this.TXT01_SJIORKAC.Name = "TXT01_SJIORKAC";
            this.TXT01_SJIORKAC.Size = new System.Drawing.Size(374, 21);
            this.TXT01_SJIORKAC.TabIndex = 8;
            this.TXT01_SJIORKAC.TabIndexCustom = false;
            // 
            // LBL51_SJIORKAC
            // 
            this.LBL51_SJIORKAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SJIORKAC.FactoryID = "";
            this.LBL51_SJIORKAC.FactoryName = null;
            this.LBL51_SJIORKAC.IsCreated = false;
            this.LBL51_SJIORKAC.Location = new System.Drawing.Point(217, 39);
            this.LBL51_SJIORKAC.Name = "LBL51_SJIORKAC";
            this.LBL51_SJIORKAC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SJIORKAC.TabIndex = 9;
            this.LBL51_SJIORKAC.Text = "상조회 적요";
            this.LBL51_SJIORKAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SJIOSABUN
            // 
            this.LBL51_SJIOSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SJIOSABUN.FactoryID = "";
            this.LBL51_SJIOSABUN.FactoryName = null;
            this.LBL51_SJIOSABUN.IsCreated = false;
            this.LBL51_SJIOSABUN.Location = new System.Drawing.Point(491, 12);
            this.LBL51_SJIOSABUN.Name = "LBL51_SJIOSABUN";
            this.LBL51_SJIOSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SJIOSABUN.TabIndex = 11;
            this.LBL51_SJIOSABUN.Text = "상조회 사번";
            this.LBL51_SJIOSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_SJIOSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_SJIOCODE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SJIODATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SJIOAMT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SJIOAMT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SJIOCODE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SJIODATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SJIORKAC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SJIORKAC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SJIOSABUN);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(989, 83);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_SJIODATE
            // 
            this.DTP01_SJIODATE.FactoryID = "";
            this.DTP01_SJIODATE.FactoryName = null;
            this.DTP01_SJIODATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SJIODATE.Name = "DTP01_SJIODATE";
            this.DTP01_SJIODATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SJIODATE.TabIndex = 16;
            // 
            // CBH01_SJIOCODE
            // 
            this.CBH01_SJIOCODE.BindedDataRow = null;
            this.CBH01_SJIOCODE.CodeBoxWidth = 0;
            this.CBH01_SJIOCODE.DummyValue = null;
            this.CBH01_SJIOCODE.FactoryID = "";
            this.CBH01_SJIOCODE.FactoryName = null;
            this.CBH01_SJIOCODE.Location = new System.Drawing.Point(323, 12);
            this.CBH01_SJIOCODE.MinLength = 0;
            this.CBH01_SJIOCODE.Name = "CBH01_SJIOCODE";
            this.CBH01_SJIOCODE.Size = new System.Drawing.Size(162, 20);
            this.CBH01_SJIOCODE.TabIndex = 17;
            // 
            // CBH01_SJIOSABUN
            // 
            this.CBH01_SJIOSABUN.BindedDataRow = null;
            this.CBH01_SJIOSABUN.CodeBoxWidth = 0;
            this.CBH01_SJIOSABUN.DummyValue = null;
            this.CBH01_SJIOSABUN.FactoryID = "";
            this.CBH01_SJIOSABUN.FactoryName = null;
            this.CBH01_SJIOSABUN.Location = new System.Drawing.Point(597, 12);
            this.CBH01_SJIOSABUN.MinLength = 0;
            this.CBH01_SJIOSABUN.Name = "CBH01_SJIOSABUN";
            this.CBH01_SJIOSABUN.Size = new System.Drawing.Size(182, 20);
            this.CBH01_SJIOSABUN.TabIndex = 18;
            // 
            // TYHRSJ002I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 88);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRSJ002I";
            this.Load += new System.EventHandler(this.TYHRSJ002I_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYTextBox TXT01_SJIOAMT;
        private TY.Service.Library.Controls.TYLabel LBL51_SJIOAMT;
        private TY.Service.Library.Controls.TYLabel LBL51_SJIOCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_SJIODATE;
        private TY.Service.Library.Controls.TYTextBox TXT01_SJIORKAC;
        private TY.Service.Library.Controls.TYLabel LBL51_SJIORKAC;
        private TY.Service.Library.Controls.TYLabel LBL51_SJIOSABUN;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_SJIODATE;
        private Service.Library.Controls.TYCodeBox CBH01_SJIOCODE;
        private Service.Library.Controls.TYCodeBox CBH01_SJIOSABUN;
    }
}