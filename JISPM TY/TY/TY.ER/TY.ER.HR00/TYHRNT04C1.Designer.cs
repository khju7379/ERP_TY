namespace TY.ER.HR00
{
    partial class TYHRNT04C1
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
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_EDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LBL51_ACEXTRAINCOME = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ACEXTRAINCOME = new TY.Service.Library.Controls.TYTextBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(505, 12);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(586, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(348, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 2;
            // 
            // LBL51_EDATE
            // 
            this.LBL51_EDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EDATE.FactoryID = "";
            this.LBL51_EDATE.FactoryName = null;
            this.LBL51_EDATE.IsCreated = false;
            this.LBL51_EDATE.Location = new System.Drawing.Point(242, 12);
            this.LBL51_EDATE.Name = "LBL51_EDATE";
            this.LBL51_EDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EDATE.TabIndex = 3;
            this.LBL51_EDATE.Text = "종료일자";
            this.LBL51_EDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 4;
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 5;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_ACEXTRAINCOME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_ACEXTRAINCOME);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(675, 68);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "->";
            // 
            // LBL51_ACEXTRAINCOME
            // 
            this.LBL51_ACEXTRAINCOME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ACEXTRAINCOME.FactoryID = "";
            this.LBL51_ACEXTRAINCOME.FactoryName = null;
            this.LBL51_ACEXTRAINCOME.IsCreated = false;
            this.LBL51_ACEXTRAINCOME.Location = new System.Drawing.Point(242, 39);
            this.LBL51_ACEXTRAINCOME.Name = "LBL51_ACEXTRAINCOME";
            this.LBL51_ACEXTRAINCOME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ACEXTRAINCOME.TabIndex = 7;
            this.LBL51_ACEXTRAINCOME.Text = "기본금액";
            this.LBL51_ACEXTRAINCOME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ACEXTRAINCOME
            // 
            this.TXT01_ACEXTRAINCOME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ACEXTRAINCOME.FactoryID = "";
            this.TXT01_ACEXTRAINCOME.FactoryName = null;
            this.TXT01_ACEXTRAINCOME.Location = new System.Drawing.Point(348, 39);
            this.TXT01_ACEXTRAINCOME.MinLength = 0;
            this.TXT01_ACEXTRAINCOME.Name = "TXT01_ACEXTRAINCOME";
            this.TXT01_ACEXTRAINCOME.Size = new System.Drawing.Size(100, 21);
            this.TXT01_ACEXTRAINCOME.TabIndex = 37;
            this.TXT01_ACEXTRAINCOME.TabIndexCustom = false;
            // 
            // TYHRNT04C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 72);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRNT04C1";
            this.Load += new System.EventHandler(this.TYHRNT04C1_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_EDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYLabel LBL51_ACEXTRAINCOME;
        private Service.Library.Controls.TYTextBox TXT01_ACEXTRAINCOME;
    }
}