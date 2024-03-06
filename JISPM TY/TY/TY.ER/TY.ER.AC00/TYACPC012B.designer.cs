namespace TY.ER.AC00
{
    partial class TYACPC012B
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_EICHYMD = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_EICHYMD = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(234, 34);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 0;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // TXT01_EICHYMD
            // 
            this.TXT01_EICHYMD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_EICHYMD.FactoryID = "";
            this.TXT01_EICHYMD.FactoryName = null;
            this.TXT01_EICHYMD.Location = new System.Drawing.Point(123, 35);
            this.TXT01_EICHYMD.MinLength = 0;
            this.TXT01_EICHYMD.Name = "TXT01_EICHYMD";
            this.TXT01_EICHYMD.Size = new System.Drawing.Size(100, 21);
            this.TXT01_EICHYMD.TabIndex = 1;
            // 
            // LBL51_EICHYMD
            // 
            this.LBL51_EICHYMD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EICHYMD.FactoryID = "";
            this.LBL51_EICHYMD.FactoryName = null;
            this.LBL51_EICHYMD.Location = new System.Drawing.Point(17, 35);
            this.LBL51_EICHYMD.Name = "LBL51_EICHYMD";
            this.LBL51_EICHYMD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EICHYMD.TabIndex = 2;
            this.LBL51_EICHYMD.Text = "년월일";
            this.LBL51_EICHYMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_EICHYMD);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EICHYMD);
            this.GBX80_CONTROLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(0, 0);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(394, 93);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(313, 34);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 3;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TYACPC012B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 93);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACPC012B";
            this.Load += new System.EventHandler(this.TYACPC012B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYTextBox TXT01_EICHYMD;
        private TY.Service.Library.Controls.TYLabel LBL51_EICHYMD;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_CLO;
    }
}