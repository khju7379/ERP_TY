namespace TY.ER.AC00
{
    partial class TYACHF009B
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
            this.DTP01_GBPRYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GBPRYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_TGBPRYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(142, 169);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(223, 169);
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
            this.CBO01_GOKCR.Location = new System.Drawing.Point(180, 102);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GOKCR.TabIndex = 2;
            // 
            // LBL51_GOKCR
            // 
            this.LBL51_GOKCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GOKCR.FactoryID = "";
            this.LBL51_GOKCR.FactoryName = null;
            this.LBL51_GOKCR.IsCreated = false;
            this.LBL51_GOKCR.Location = new System.Drawing.Point(74, 101);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 3;
            this.LBL51_GOKCR.Text = "생성구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GBPRYYMM
            // 
            this.DTP01_GBPRYYMM.FactoryID = "";
            this.DTP01_GBPRYYMM.FactoryName = null;
            this.DTP01_GBPRYYMM.Location = new System.Drawing.Point(180, 68);
            this.DTP01_GBPRYYMM.Name = "DTP01_GBPRYYMM";
            this.DTP01_GBPRYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GBPRYYMM.TabIndex = 4;
            // 
            // LBL51_GBPRYYMM
            // 
            this.LBL51_GBPRYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GBPRYYMM.FactoryID = "";
            this.LBL51_GBPRYYMM.FactoryName = null;
            this.LBL51_GBPRYYMM.IsCreated = false;
            this.LBL51_GBPRYYMM.Location = new System.Drawing.Point(74, 68);
            this.LBL51_GBPRYYMM.Name = "LBL51_GBPRYYMM";
            this.LBL51_GBPRYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GBPRYYMM.TabIndex = 5;
            this.LBL51_GBPRYYMM.Text = "처리년월";
            this.LBL51_GBPRYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_TGBPRYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GBPRYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GBPRYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(12, 12);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(469, 275);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_TGBPRYYMM
            // 
            this.DTP01_TGBPRYYMM.FactoryID = "";
            this.DTP01_TGBPRYYMM.FactoryName = null;
            this.DTP01_TGBPRYYMM.Location = new System.Drawing.Point(286, 68);
            this.DTP01_TGBPRYYMM.Name = "DTP01_TGBPRYYMM";
            this.DTP01_TGBPRYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_TGBPRYYMM.TabIndex = 6;
            // 
            // TYACHF009B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 293);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACHF009B";
            this.Load += new System.EventHandler(this.TYACHF009B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYComboBox CBO01_GOKCR;
        private TY.Service.Library.Controls.TYLabel LBL51_GOKCR;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GBPRYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GBPRYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_TGBPRYYMM;
    }
}