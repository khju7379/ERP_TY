namespace TY.ER.AC00
{
    partial class TYACMF007B
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
            this.BTN61_DWN = new TY.Service.Library.Controls.TYButton();
            this.CBO01_G2CDBK = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_G2CDBK = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GEDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GEDDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GSTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GOKCR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_GOKCR = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(179, 171);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(341, 171);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_DWN
            // 
            this.BTN61_DWN.FactoryID = "";
            this.BTN61_DWN.FactoryName = null;
            this.BTN61_DWN.Location = new System.Drawing.Point(260, 171);
            this.BTN61_DWN.Name = "BTN61_DWN";
            this.BTN61_DWN.Size = new System.Drawing.Size(75, 21);
            this.BTN61_DWN.TabIndex = 2;
            this.BTN61_DWN.Text = "다운";
            this.BTN61_DWN.UseVisualStyleBackColor = true;
            this.BTN61_DWN.Click += new System.EventHandler(this.BTN61_DWN_Click);
            // 
            // CBO01_G2CDBK
            // 
            this.CBO01_G2CDBK.FactoryID = "";
            this.CBO01_G2CDBK.FactoryName = null;
            this.CBO01_G2CDBK.Location = new System.Drawing.Point(296, 98);
            this.CBO01_G2CDBK.Name = "CBO01_G2CDBK";
            this.CBO01_G2CDBK.Size = new System.Drawing.Size(100, 20);
            this.CBO01_G2CDBK.TabIndex = 3;
            // 
            // LBL51_G2CDBK
            // 
            this.LBL51_G2CDBK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_G2CDBK.FactoryID = "";
            this.LBL51_G2CDBK.FactoryName = null;
            this.LBL51_G2CDBK.Location = new System.Drawing.Point(190, 98);
            this.LBL51_G2CDBK.Name = "LBL51_G2CDBK";
            this.LBL51_G2CDBK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_G2CDBK.TabIndex = 4;
            this.LBL51_G2CDBK.Text = "이체은행";
            this.LBL51_G2CDBK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GEDDATE
            // 
            this.DTP01_GEDDATE.FactoryID = "";
            this.DTP01_GEDDATE.FactoryName = null;
            this.DTP01_GEDDATE.Location = new System.Drawing.Point(296, 71);
            this.DTP01_GEDDATE.Name = "DTP01_GEDDATE";
            this.DTP01_GEDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GEDDATE.TabIndex = 5;
            // 
            // LBL51_GEDDATE
            // 
            this.LBL51_GEDDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GEDDATE.FactoryID = "";
            this.LBL51_GEDDATE.FactoryName = null;
            this.LBL51_GEDDATE.Location = new System.Drawing.Point(190, 71);
            this.LBL51_GEDDATE.Name = "LBL51_GEDDATE";
            this.LBL51_GEDDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GEDDATE.TabIndex = 6;
            this.LBL51_GEDDATE.Text = "종료일자";
            this.LBL51_GEDDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GSTDATE
            // 
            this.DTP01_GSTDATE.FactoryID = "";
            this.DTP01_GSTDATE.FactoryName = null;
            this.DTP01_GSTDATE.Location = new System.Drawing.Point(296, 44);
            this.DTP01_GSTDATE.Name = "DTP01_GSTDATE";
            this.DTP01_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTDATE.TabIndex = 7;
            // 
            // LBL51_GSTDATE
            // 
            this.LBL51_GSTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTDATE.FactoryID = "";
            this.LBL51_GSTDATE.FactoryName = null;
            this.LBL51_GSTDATE.Location = new System.Drawing.Point(190, 44);
            this.LBL51_GSTDATE.Name = "LBL51_GSTDATE";
            this.LBL51_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTDATE.TabIndex = 8;
            this.LBL51_GSTDATE.Text = "시작일자";
            this.LBL51_GSTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GOKCR
            // 
            this.LBL51_GOKCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GOKCR.FactoryID = "";
            this.LBL51_GOKCR.FactoryName = null;
            this.LBL51_GOKCR.Location = new System.Drawing.Point(190, 124);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 10;
            this.LBL51_GOKCR.Text = "생성구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_DWN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_G2CDBK);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_G2CDBK);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GEDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GEDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GSTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GSTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(582, 248);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_GOKCR
            // 
            this.CBO01_GOKCR.FactoryID = "";
            this.CBO01_GOKCR.FactoryName = null;
            this.CBO01_GOKCR.Location = new System.Drawing.Point(296, 124);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GOKCR.TabIndex = 11;
            // 
            // TYACMF007B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 255);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACMF007B";
            this.Load += new System.EventHandler(this.TYACMF007B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_DWN;
        private TY.Service.Library.Controls.TYComboBox CBO01_G2CDBK;
        private TY.Service.Library.Controls.TYLabel LBL51_G2CDBK;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GEDDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GEDDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GOKCR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYComboBox CBO01_GOKCR;
    }
}