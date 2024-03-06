namespace TY.ER.AC00
{
    partial class TYACKF008B
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
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.RDB01_CHK02 = new TY.Service.Library.Controls.TYRadioButton();
            this.RDB01_CHK01 = new TY.Service.Library.Controls.TYRadioButton();
            this.LBL51_CHK_EDI = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(125, 119);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(206, 119);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(64, 39);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 5;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label2);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.RDB01_CHK02);
            this.GBX80_CONTROLS.Controls.Add(this.RDB01_CHK01);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHK_EDI);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(1, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(449, 166);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(315, 39);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(113, 21);
            this.DTP01_EDATE.TabIndex = 363;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 362;
            this.label2.Text = "~";
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(172, 39);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(117, 21);
            this.DTP01_SDATE.TabIndex = 361;
            // 
            // RDB01_CHK02
            // 
            this.RDB01_CHK02.AutoSize = true;
            this.RDB01_CHK02.FactoryID = "";
            this.RDB01_CHK02.FactoryName = null;
            this.RDB01_CHK02.Location = new System.Drawing.Point(251, 66);
            this.RDB01_CHK02.Name = "RDB01_CHK02";
            this.RDB01_CHK02.Size = new System.Drawing.Size(71, 16);
            this.RDB01_CHK02.TabIndex = 360;
            this.RDB01_CHK02.Text = "자금원천";
            this.RDB01_CHK02.UseVisualStyleBackColor = true;
            // 
            // RDB01_CHK01
            // 
            this.RDB01_CHK01.AutoSize = true;
            this.RDB01_CHK01.Checked = true;
            this.RDB01_CHK01.FactoryID = "";
            this.RDB01_CHK01.FactoryName = null;
            this.RDB01_CHK01.Location = new System.Drawing.Point(170, 66);
            this.RDB01_CHK01.Name = "RDB01_CHK01";
            this.RDB01_CHK01.Size = new System.Drawing.Size(71, 16);
            this.RDB01_CHK01.TabIndex = 359;
            this.RDB01_CHK01.TabStop = true;
            this.RDB01_CHK01.Text = "자금실적";
            this.RDB01_CHK01.UseVisualStyleBackColor = true;
            // 
            // LBL51_CHK_EDI
            // 
            this.LBL51_CHK_EDI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHK_EDI.FactoryID = "";
            this.LBL51_CHK_EDI.FactoryName = null;
            this.LBL51_CHK_EDI.IsCreated = false;
            this.LBL51_CHK_EDI.Location = new System.Drawing.Point(64, 64);
            this.LBL51_CHK_EDI.Name = "LBL51_CHK_EDI";
            this.LBL51_CHK_EDI.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHK_EDI.TabIndex = 358;
            this.LBL51_CHK_EDI.Text = "구  분";
            this.LBL51_CHK_EDI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYACKF008B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 170);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACKF008B";
            this.Load += new System.EventHandler(this.TYACKF008B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYRadioButton RDB01_CHK02;
        private Service.Library.Controls.TYRadioButton RDB01_CHK01;
        private Service.Library.Controls.TYLabel LBL51_CHK_EDI;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private System.Windows.Forms.Label label2;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
    }
}