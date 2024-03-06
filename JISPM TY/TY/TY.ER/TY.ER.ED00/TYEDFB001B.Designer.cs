namespace TY.ER.ED00
{
    partial class TYEDFB001B
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
            this.LBL51_CHK_EDI = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.BTN61_REC = new TY.Service.Library.Controls.TYButton();
            this.RDB01_CHK3 = new TY.Service.Library.Controls.TYRadioButton();
            this.RDB01_CHK2 = new TY.Service.Library.Controls.TYRadioButton();
            this.RDB01_CHK1 = new TY.Service.Library.Controls.TYRadioButton();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(130, 149);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "전송";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // LBL51_CHK_EDI
            // 
            this.LBL51_CHK_EDI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHK_EDI.FactoryID = "";
            this.LBL51_CHK_EDI.FactoryName = null;
            this.LBL51_CHK_EDI.IsCreated = false;
            this.LBL51_CHK_EDI.Location = new System.Drawing.Point(5, 64);
            this.LBL51_CHK_EDI.Name = "LBL51_CHK_EDI";
            this.LBL51_CHK_EDI.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHK_EDI.TabIndex = 5;
            this.LBL51_CHK_EDI.Text = "EDI 체크 옵션";
            this.LBL51_CHK_EDI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 38);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 11;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.pgBar);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REC);
            this.GBX80_CONTROLS.Controls.Add(this.RDB01_CHK3);
            this.GBX80_CONTROLS.Controls.Add(this.RDB01_CHK2);
            this.GBX80_CONTROLS.Controls.Add(this.RDB01_CHK1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHK_EDI);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(363, 308);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(458, 188);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(3, 175);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(452, 8);
            this.pgBar.TabIndex = 28;
            // 
            // BTN61_REC
            // 
            this.BTN61_REC.FactoryID = "";
            this.BTN61_REC.FactoryName = null;
            this.BTN61_REC.Location = new System.Drawing.Point(211, 149);
            this.BTN61_REC.Name = "BTN61_REC";
            this.BTN61_REC.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REC.TabIndex = 27;
            this.BTN61_REC.Text = "수신";
            this.BTN61_REC.UseVisualStyleBackColor = true;
            this.BTN61_REC.Click += new System.EventHandler(this.BTN61_REC_Click);
            // 
            // RDB01_CHK3
            // 
            this.RDB01_CHK3.AutoSize = true;
            this.RDB01_CHK3.FactoryID = "";
            this.RDB01_CHK3.FactoryName = null;
            this.RDB01_CHK3.Location = new System.Drawing.Point(289, 66);
            this.RDB01_CHK3.Name = "RDB01_CHK3";
            this.RDB01_CHK3.Size = new System.Drawing.Size(83, 16);
            this.RDB01_CHK3.TabIndex = 20;
            this.RDB01_CHK3.TabStop = true;
            this.RDB01_CHK3.Text = "보험영수증";
            this.RDB01_CHK3.UseVisualStyleBackColor = true;
            this.RDB01_CHK3.CheckedChanged += new System.EventHandler(this.RDB01_CHK3_CheckedChanged);
            // 
            // RDB01_CHK2
            // 
            this.RDB01_CHK2.AutoSize = true;
            this.RDB01_CHK2.FactoryID = "";
            this.RDB01_CHK2.FactoryName = null;
            this.RDB01_CHK2.Location = new System.Drawing.Point(200, 66);
            this.RDB01_CHK2.Name = "RDB01_CHK2";
            this.RDB01_CHK2.Size = new System.Drawing.Size(83, 16);
            this.RDB01_CHK2.TabIndex = 19;
            this.RDB01_CHK2.TabStop = true;
            this.RDB01_CHK2.Text = "반출보고서";
            this.RDB01_CHK2.UseVisualStyleBackColor = true;
            this.RDB01_CHK2.CheckedChanged += new System.EventHandler(this.RDB01_CHK2_CheckedChanged);
            // 
            // RDB01_CHK1
            // 
            this.RDB01_CHK1.AutoSize = true;
            this.RDB01_CHK1.Checked = true;
            this.RDB01_CHK1.FactoryID = "";
            this.RDB01_CHK1.FactoryName = null;
            this.RDB01_CHK1.Location = new System.Drawing.Point(111, 66);
            this.RDB01_CHK1.Name = "RDB01_CHK1";
            this.RDB01_CHK1.Size = new System.Drawing.Size(83, 16);
            this.RDB01_CHK1.TabIndex = 18;
            this.RDB01_CHK1.TabStop = true;
            this.RDB01_CHK1.Text = "반입보고서";
            this.RDB01_CHK1.UseVisualStyleBackColor = true;
            this.RDB01_CHK1.CheckedChanged += new System.EventHandler(this.RDB01_CHK1_CheckedChanged);
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(237, 38);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "~";
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 38);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 13;
            // 
            // TYEDFB001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYEDFB001B";
            this.Load += new System.EventHandler(this.TYEDFB001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYLabel LBL51_CHK_EDI;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYRadioButton RDB01_CHK2;
        private Service.Library.Controls.TYRadioButton RDB01_CHK1;
        private Service.Library.Controls.TYRadioButton RDB01_CHK3;
        private Service.Library.Controls.TYButton BTN61_REC;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}