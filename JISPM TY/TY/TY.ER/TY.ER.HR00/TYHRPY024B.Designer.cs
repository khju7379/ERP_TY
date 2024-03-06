namespace TY.ER.HR00
{
    partial class TYHRPY024B
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
            this.LBL51_INQOPTION = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SDATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_INQOPTION = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_EDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(153, 131);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(234, 131);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_INQOPTION
            // 
            this.LBL51_INQOPTION.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_INQOPTION.FactoryID = "";
            this.LBL51_INQOPTION.FactoryName = null;
            this.LBL51_INQOPTION.IsCreated = false;
            this.LBL51_INQOPTION.Location = new System.Drawing.Point(128, 60);
            this.LBL51_INQOPTION.Name = "LBL51_INQOPTION";
            this.LBL51_INQOPTION.Size = new System.Drawing.Size(100, 21);
            this.LBL51_INQOPTION.TabIndex = 3;
            this.LBL51_INQOPTION.Text = "조회구분";
            this.LBL51_INQOPTION.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SDATE
            // 
            this.TXT01_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SDATE.FactoryID = "";
            this.TXT01_SDATE.FactoryName = null;
            this.TXT01_SDATE.Location = new System.Drawing.Point(234, 33);
            this.TXT01_SDATE.MinLength = 0;
            this.TXT01_SDATE.Name = "TXT01_SDATE";
            this.TXT01_SDATE.Size = new System.Drawing.Size(58, 21);
            this.TXT01_SDATE.TabIndex = 4;
            this.TXT01_SDATE.TabIndexCustom = false;
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(128, 33);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 5;
            this.LBL51_SDATE.Text = "기준년도";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(493, 162);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_INQOPTION
            // 
            this.CBO01_INQOPTION.FactoryID = "";
            this.CBO01_INQOPTION.FactoryName = null;
            this.CBO01_INQOPTION.Location = new System.Drawing.Point(234, 60);
            this.CBO01_INQOPTION.Name = "CBO01_INQOPTION";
            this.CBO01_INQOPTION.Size = new System.Drawing.Size(100, 20);
            this.CBO01_INQOPTION.TabIndex = 40;
            // 
            // LBL51_EDATE
            // 
            this.LBL51_EDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EDATE.FactoryID = "";
            this.LBL51_EDATE.FactoryName = null;
            this.LBL51_EDATE.IsCreated = false;
            this.LBL51_EDATE.Location = new System.Drawing.Point(128, 86);
            this.LBL51_EDATE.Name = "LBL51_EDATE";
            this.LBL51_EDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EDATE.TabIndex = 41;
            this.LBL51_EDATE.Text = "제출일자";
            this.LBL51_EDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(234, 86);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(113, 21);
            this.DTP01_EDATE.TabIndex = 60;
            // 
            // TYHRPY024B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 165);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY024B";
            this.Load += new System.EventHandler(this.TYHRPY024B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_INQOPTION;
        private TY.Service.Library.Controls.TYTextBox TXT01_SDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYComboBox CBO01_INQOPTION;
        private Service.Library.Controls.TYLabel LBL51_EDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}