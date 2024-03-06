namespace TY.ER.HR00
{
    partial class TYHRPY06C4
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV_BATCH = new TY.Service.Library.Controls.TYButton();
            this.TXT01_PTPAYRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_PTPAYRATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(306, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV_BATCH
            // 
            this.BTN61_SAV_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV_BATCH.FactoryID = "";
            this.BTN61_SAV_BATCH.FactoryName = null;
            this.BTN61_SAV_BATCH.Location = new System.Drawing.Point(225, 12);
            this.BTN61_SAV_BATCH.Name = "BTN61_SAV_BATCH";
            this.BTN61_SAV_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV_BATCH.TabIndex = 1;
            this.BTN61_SAV_BATCH.Text = "일괄등록";
            this.BTN61_SAV_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_SAV_BATCH.Click += new System.EventHandler(this.BTN61_SAV_BATCH_Click);
            // 
            // TXT01_PTPAYRATE
            // 
            this.TXT01_PTPAYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_PTPAYRATE.FactoryID = "";
            this.TXT01_PTPAYRATE.FactoryName = null;
            this.TXT01_PTPAYRATE.Location = new System.Drawing.Point(112, 12);
            this.TXT01_PTPAYRATE.MinLength = 0;
            this.TXT01_PTPAYRATE.Name = "TXT01_PTPAYRATE";
            this.TXT01_PTPAYRATE.Size = new System.Drawing.Size(65, 21);
            this.TXT01_PTPAYRATE.TabIndex = 2;
            // 
            // LBL51_PTPAYRATE
            // 
            this.LBL51_PTPAYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PTPAYRATE.FactoryID = "";
            this.LBL51_PTPAYRATE.FactoryName = null;
            this.LBL51_PTPAYRATE.IsCreated = false;
            this.LBL51_PTPAYRATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_PTPAYRATE.Name = "LBL51_PTPAYRATE";
            this.LBL51_PTPAYRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PTPAYRATE.TabIndex = 3;
            this.LBL51_PTPAYRATE.Text = "지급율";
            this.LBL51_PTPAYRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_PTPAYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PTPAYRATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(397, 40);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYHRPY06C4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 45);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY06C4";
            this.Load += new System.EventHandler(this.TYHRPY06C4_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV_BATCH;
        private TY.Service.Library.Controls.TYTextBox TXT01_PTPAYRATE;
        private TY.Service.Library.Controls.TYLabel LBL51_PTPAYRATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}