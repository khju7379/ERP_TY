namespace TY.ER.AC00
{
    partial class TYACNC021B
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
            this.DTP01_GBPRYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GBPRYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(100, 278);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(182, 278);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // DTP01_GBPRYYMM
            // 
            this.DTP01_GBPRYYMM.FactoryID = "";
            this.DTP01_GBPRYYMM.FactoryName = null;
            this.DTP01_GBPRYYMM.Location = new System.Drawing.Point(190, 91);
            this.DTP01_GBPRYYMM.Name = "DTP01_GBPRYYMM";
            this.DTP01_GBPRYYMM.Size = new System.Drawing.Size(84, 21);
            this.DTP01_GBPRYYMM.TabIndex = 2;
            // 
            // LBL51_GBPRYYMM
            // 
            this.LBL51_GBPRYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GBPRYYMM.FactoryID = "";
            this.LBL51_GBPRYYMM.FactoryName = null;
            this.LBL51_GBPRYYMM.IsCreated = false;
            this.LBL51_GBPRYYMM.Location = new System.Drawing.Point(83, 91);
            this.LBL51_GBPRYYMM.Name = "LBL51_GBPRYYMM";
            this.LBL51_GBPRYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GBPRYYMM.TabIndex = 3;
            this.LBL51_GBPRYYMM.Text = "처리년월";
            this.LBL51_GBPRYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GBPRYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GBPRYYMM);
            this.GBX80_CONTROLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(0, 0);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(407, 389);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYACNC021B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 389);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACNC021B";
            this.Load += new System.EventHandler(this.TYACNC021B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GBPRYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GBPRYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}