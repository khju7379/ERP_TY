namespace TY.ER.UT00
{
    partial class TYUTME018B
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
            this.LBL51_M1DATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_M1SUMHJ = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_M1SUMHJ = new TY.Service.Library.Controls.TYTextBox();
            this.CBH01_M1HWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_M1HWAJU = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_M1DATE = new TY.Service.Library.Controls.TYDatePicker();
            this.TXT01_M1SEQ = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_M1SEQ = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(504, 478);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "전표생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_M1DATE
            // 
            this.LBL51_M1DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M1DATE.FactoryID = "";
            this.LBL51_M1DATE.FactoryName = null;
            this.LBL51_M1DATE.IsCreated = false;
            this.LBL51_M1DATE.Location = new System.Drawing.Point(398, 340);
            this.LBL51_M1DATE.Name = "LBL51_M1DATE";
            this.LBL51_M1DATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M1DATE.TabIndex = 4;
            this.LBL51_M1DATE.Text = "매출일자";
            this.LBL51_M1DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M1SEQ);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_M1SEQ);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M1SUMHJ);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_M1SUMHJ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_M1HWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M1HWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_M1DATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M1DATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_M1SUMHJ
            // 
            this.LBL51_M1SUMHJ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M1SUMHJ.FactoryID = "";
            this.LBL51_M1SUMHJ.FactoryName = null;
            this.LBL51_M1SUMHJ.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_M1SUMHJ.ForeColor = System.Drawing.Color.Black;
            this.LBL51_M1SUMHJ.IsCreated = false;
            this.LBL51_M1SUMHJ.Location = new System.Drawing.Point(398, 393);
            this.LBL51_M1SUMHJ.Name = "LBL51_M1SUMHJ";
            this.LBL51_M1SUMHJ.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M1SUMHJ.TabIndex = 327;
            this.LBL51_M1SUMHJ.Text = "집계화주";
            this.LBL51_M1SUMHJ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_M1SUMHJ
            // 
            this.TXT01_M1SUMHJ.AcceptsReturn = true;
            this.TXT01_M1SUMHJ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_M1SUMHJ.FactoryID = "";
            this.TXT01_M1SUMHJ.FactoryName = null;
            this.TXT01_M1SUMHJ.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_M1SUMHJ.Location = new System.Drawing.Point(504, 393);
            this.TXT01_M1SUMHJ.MinLength = 0;
            this.TXT01_M1SUMHJ.Name = "TXT01_M1SUMHJ";
            this.TXT01_M1SUMHJ.Size = new System.Drawing.Size(30, 21);
            this.TXT01_M1SUMHJ.TabIndex = 326;
            this.TXT01_M1SUMHJ.TabIndexCustom = false;
            // 
            // CBH01_M1HWAJU
            // 
            this.CBH01_M1HWAJU.BindedDataRow = null;
            this.CBH01_M1HWAJU.CodeBoxWidth = 0;
            this.CBH01_M1HWAJU.DummyValue = null;
            this.CBH01_M1HWAJU.FactoryID = "";
            this.CBH01_M1HWAJU.FactoryName = null;
            this.CBH01_M1HWAJU.Location = new System.Drawing.Point(504, 367);
            this.CBH01_M1HWAJU.MinLength = 0;
            this.CBH01_M1HWAJU.Name = "CBH01_M1HWAJU";
            this.CBH01_M1HWAJU.Size = new System.Drawing.Size(311, 20);
            this.CBH01_M1HWAJU.TabIndex = 316;
            // 
            // LBL51_M1HWAJU
            // 
            this.LBL51_M1HWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M1HWAJU.FactoryID = "";
            this.LBL51_M1HWAJU.FactoryName = null;
            this.LBL51_M1HWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_M1HWAJU.ForeColor = System.Drawing.Color.Black;
            this.LBL51_M1HWAJU.IsCreated = false;
            this.LBL51_M1HWAJU.Location = new System.Drawing.Point(398, 367);
            this.LBL51_M1HWAJU.Name = "LBL51_M1HWAJU";
            this.LBL51_M1HWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M1HWAJU.TabIndex = 315;
            this.LBL51_M1HWAJU.Text = "화 주";
            this.LBL51_M1HWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(585, 478);
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
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(504, 447);
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
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(398, 446);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 310;
            this.LBL51_GGUBUN.Text = "생성구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_M1DATE
            // 
            this.DTP01_M1DATE.FactoryID = "";
            this.DTP01_M1DATE.FactoryName = null;
            this.DTP01_M1DATE.Location = new System.Drawing.Point(504, 340);
            this.DTP01_M1DATE.Name = "DTP01_M1DATE";
            this.DTP01_M1DATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_M1DATE.TabIndex = 156;
            // 
            // TXT01_M1SEQ
            // 
            this.TXT01_M1SEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_M1SEQ.FactoryID = "";
            this.TXT01_M1SEQ.FactoryName = null;
            this.TXT01_M1SEQ.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_M1SEQ.Location = new System.Drawing.Point(504, 420);
            this.TXT01_M1SEQ.MinLength = 0;
            this.TXT01_M1SEQ.Name = "TXT01_M1SEQ";
            this.TXT01_M1SEQ.Size = new System.Drawing.Size(50, 21);
            this.TXT01_M1SEQ.TabIndex = 334;
            this.TXT01_M1SEQ.TabIndexCustom = false;
            // 
            // LBL51_M1SEQ
            // 
            this.LBL51_M1SEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M1SEQ.FactoryID = "";
            this.LBL51_M1SEQ.FactoryName = null;
            this.LBL51_M1SEQ.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_M1SEQ.ForeColor = System.Drawing.Color.Black;
            this.LBL51_M1SEQ.IsCreated = false;
            this.LBL51_M1SEQ.Location = new System.Drawing.Point(398, 420);
            this.LBL51_M1SEQ.Name = "LBL51_M1SEQ";
            this.LBL51_M1SEQ.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M1SEQ.TabIndex = 335;
            this.LBL51_M1SEQ.Text = "순 번";
            this.LBL51_M1SEQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUTME018B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME018B";
            this.Load += new System.EventHandler(this.TYUTME018B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYLabel LBL51_M1DATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_M1DATE;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYButton BTN61_PRT;
        private Service.Library.Controls.TYCodeBox CBH01_M1HWAJU;
        private Service.Library.Controls.TYLabel LBL51_M1HWAJU;
        private Service.Library.Controls.TYTextBox TXT01_M1SUMHJ;
        private Service.Library.Controls.TYLabel LBL51_M1SUMHJ;
        private Service.Library.Controls.TYLabel LBL51_M1SEQ;
        private Service.Library.Controls.TYTextBox TXT01_M1SEQ;
    }
}