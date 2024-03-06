namespace TY.ER.UT00
{
    partial class TYUTIL016P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.DTP01_YYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_YYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(181, 105);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(100, 105);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // DTP01_YYYYMM
            // 
            this.DTP01_YYYYMM.FactoryID = "";
            this.DTP01_YYYYMM.FactoryName = null;
            this.DTP01_YYYYMM.Location = new System.Drawing.Point(181, 55);
            this.DTP01_YYYYMM.Name = "DTP01_YYYYMM";
            this.DTP01_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_YYYYMM.TabIndex = 2;
            // 
            // LBL51_YYYYMM
            // 
            this.LBL51_YYYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_YYYYMM.FactoryID = "";
            this.LBL51_YYYYMM.FactoryName = null;
            this.LBL51_YYYYMM.IsCreated = false;
            this.LBL51_YYYYMM.Location = new System.Drawing.Point(75, 55);
            this.LBL51_YYYYMM.Name = "LBL51_YYYYMM";
            this.LBL51_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYYYMM.TabIndex = 3;
            this.LBL51_YYYYMM.Text = "기준 년월";
            this.LBL51_YYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_YYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_YYYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(355, 180);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUTIL016P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 181);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTIL016P";
            this.Load += new System.EventHandler(this.TYUTIL016P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYDatePicker DTP01_YYYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_YYYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}