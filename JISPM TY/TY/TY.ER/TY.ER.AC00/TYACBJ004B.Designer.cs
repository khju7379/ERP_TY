namespace TY.ER.AC00
{
    partial class TYACBJ004B
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
            this.CBO01_GOKCR = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GOKCR = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GEDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_GSTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(212, 129);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(293, 129);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // CBO01_GOKCR
            // 
            this.CBO01_GOKCR.FactoryID = "";
            this.CBO01_GOKCR.FactoryName = null;
            this.CBO01_GOKCR.Location = new System.Drawing.Point(230, 83);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GOKCR.TabIndex = 2;
            // 
            // LBL51_GOKCR
            // 
            this.LBL51_GOKCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GOKCR.FactoryID = "";
            this.LBL51_GOKCR.FactoryName = null;
            this.LBL51_GOKCR.Location = new System.Drawing.Point(124, 83);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 3;
            this.LBL51_GOKCR.Text = "생성구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GEDDATE
            // 
            this.DTP01_GEDDATE.FactoryID = "";
            this.DTP01_GEDDATE.FactoryName = null;
            this.DTP01_GEDDATE.Location = new System.Drawing.Point(356, 56);
            this.DTP01_GEDDATE.Name = "DTP01_GEDDATE";
            this.DTP01_GEDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GEDDATE.TabIndex = 4;
            // 
            // DTP01_GSTDATE
            // 
            this.DTP01_GSTDATE.FactoryID = "";
            this.DTP01_GSTDATE.FactoryName = null;
            this.DTP01_GSTDATE.Location = new System.Drawing.Point(230, 56);
            this.DTP01_GSTDATE.Name = "DTP01_GSTDATE";
            this.DTP01_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTDATE.TabIndex = 6;
            // 
            // LBL51_GSTDATE
            // 
            this.LBL51_GSTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTDATE.FactoryID = "";
            this.LBL51_GSTDATE.FactoryName = null;
            this.LBL51_GSTDATE.Location = new System.Drawing.Point(124, 56);
            this.LBL51_GSTDATE.Name = "LBL51_GSTDATE";
            this.LBL51_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTDATE.TabIndex = 7;
            this.LBL51_GSTDATE.Text = "시작일자";
            this.LBL51_GSTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GEDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GSTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GSTDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(580, 207);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "~";
            // 
            // TYACBJ004B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 214);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACBJ004B";
            this.Load += new System.EventHandler(this.TYACBJ004B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYComboBox CBO01_GOKCR;
        private TY.Service.Library.Controls.TYLabel LBL51_GOKCR;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GEDDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
    }
}