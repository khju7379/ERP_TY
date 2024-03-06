namespace TY.ER.UT00
{
    partial class TYUTME014B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.LBL51_JBDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_JBSEQ = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_JBSEQ = new TY.Service.Library.Controls.TYTextBox();
            this.CBH01_JBBRANCH = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_JBBRANCH = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_JBIPHANG = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_JBIPHANG = new TY.Service.Library.Controls.TYDatePicker();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_JBDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(491, 453);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "전표생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_JBDATE
            // 
            this.LBL51_JBDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_JBDATE.FactoryID = "";
            this.LBL51_JBDATE.FactoryName = null;
            this.LBL51_JBDATE.IsCreated = false;
            this.LBL51_JBDATE.Location = new System.Drawing.Point(385, 315);
            this.LBL51_JBDATE.Name = "LBL51_JBDATE";
            this.LBL51_JBDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBDATE.TabIndex = 4;
            this.LBL51_JBDATE.Text = "기준일자";
            this.LBL51_JBDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_JBSEQ);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_JBSEQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_JBBRANCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_JBBRANCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_JBIPHANG);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_JBIPHANG);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_JBDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_JBDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_JBSEQ
            // 
            this.LBL51_JBSEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_JBSEQ.FactoryID = "";
            this.LBL51_JBSEQ.FactoryName = null;
            this.LBL51_JBSEQ.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_JBSEQ.ForeColor = System.Drawing.Color.Black;
            this.LBL51_JBSEQ.IsCreated = false;
            this.LBL51_JBSEQ.Location = new System.Drawing.Point(385, 395);
            this.LBL51_JBSEQ.Name = "LBL51_JBSEQ";
            this.LBL51_JBSEQ.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBSEQ.TabIndex = 327;
            this.LBL51_JBSEQ.Text = "순 번";
            this.LBL51_JBSEQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_JBSEQ
            // 
            this.TXT01_JBSEQ.AcceptsReturn = true;
            this.TXT01_JBSEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_JBSEQ.FactoryID = "";
            this.TXT01_JBSEQ.FactoryName = null;
            this.TXT01_JBSEQ.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_JBSEQ.Location = new System.Drawing.Point(491, 395);
            this.TXT01_JBSEQ.MinLength = 0;
            this.TXT01_JBSEQ.Name = "TXT01_JBSEQ";
            this.TXT01_JBSEQ.Size = new System.Drawing.Size(30, 21);
            this.TXT01_JBSEQ.TabIndex = 326;
            this.TXT01_JBSEQ.TabIndexCustom = false;
            // 
            // CBH01_JBBRANCH
            // 
            this.CBH01_JBBRANCH.BindedDataRow = null;
            this.CBH01_JBBRANCH.CodeBoxWidth = 0;
            this.CBH01_JBBRANCH.DummyValue = null;
            this.CBH01_JBBRANCH.FactoryID = "";
            this.CBH01_JBBRANCH.FactoryName = null;
            this.CBH01_JBBRANCH.Location = new System.Drawing.Point(491, 369);
            this.CBH01_JBBRANCH.MinLength = 0;
            this.CBH01_JBBRANCH.Name = "CBH01_JBBRANCH";
            this.CBH01_JBBRANCH.Size = new System.Drawing.Size(311, 20);
            this.CBH01_JBBRANCH.TabIndex = 316;
            // 
            // LBL51_JBBRANCH
            // 
            this.LBL51_JBBRANCH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_JBBRANCH.FactoryID = "";
            this.LBL51_JBBRANCH.FactoryName = null;
            this.LBL51_JBBRANCH.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_JBBRANCH.ForeColor = System.Drawing.Color.Black;
            this.LBL51_JBBRANCH.IsCreated = false;
            this.LBL51_JBBRANCH.Location = new System.Drawing.Point(385, 369);
            this.LBL51_JBBRANCH.Name = "LBL51_JBBRANCH";
            this.LBL51_JBBRANCH.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBBRANCH.TabIndex = 315;
            this.LBL51_JBBRANCH.Text = "대 리 점";
            this.LBL51_JBBRANCH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_JBIPHANG
            // 
            this.LBL51_JBIPHANG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_JBIPHANG.FactoryID = "";
            this.LBL51_JBIPHANG.FactoryName = null;
            this.LBL51_JBIPHANG.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_JBIPHANG.ForeColor = System.Drawing.Color.Black;
            this.LBL51_JBIPHANG.IsCreated = false;
            this.LBL51_JBIPHANG.Location = new System.Drawing.Point(385, 342);
            this.LBL51_JBIPHANG.Name = "LBL51_JBIPHANG";
            this.LBL51_JBIPHANG.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBIPHANG.TabIndex = 314;
            this.LBL51_JBIPHANG.Text = "접안일자";
            this.LBL51_JBIPHANG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_JBIPHANG
            // 
            this.DTP01_JBIPHANG.FactoryID = "";
            this.DTP01_JBIPHANG.FactoryName = null;
            this.DTP01_JBIPHANG.Location = new System.Drawing.Point(491, 342);
            this.DTP01_JBIPHANG.Name = "DTP01_JBIPHANG";
            this.DTP01_JBIPHANG.Size = new System.Drawing.Size(100, 21);
            this.DTP01_JBIPHANG.TabIndex = 313;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(572, 453);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 312;
            this.BTN61_PRT.Text = "전표출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(491, 422);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GGUBUN.TabIndex = 311;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(385, 422);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 310;
            this.LBL51_GGUBUN.Text = "생성구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_JBDATE
            // 
            this.DTP01_JBDATE.FactoryID = "";
            this.DTP01_JBDATE.FactoryName = null;
            this.DTP01_JBDATE.Location = new System.Drawing.Point(491, 315);
            this.DTP01_JBDATE.Name = "DTP01_JBDATE";
            this.DTP01_JBDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_JBDATE.TabIndex = 156;
            // 
            // TYUTME014B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME014B";
            this.Load += new System.EventHandler(this.TYUTME014B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYLabel LBL51_JBDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_JBDATE;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYButton BTN61_PRT;
        private Service.Library.Controls.TYDatePicker DTP01_JBIPHANG;
        private Service.Library.Controls.TYLabel LBL51_JBIPHANG;
        private Service.Library.Controls.TYCodeBox CBH01_JBBRANCH;
        private Service.Library.Controls.TYLabel LBL51_JBBRANCH;
        private Service.Library.Controls.TYTextBox TXT01_JBSEQ;
        private Service.Library.Controls.TYLabel LBL51_JBSEQ;
    }
}