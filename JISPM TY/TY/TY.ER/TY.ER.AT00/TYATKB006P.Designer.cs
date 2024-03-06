namespace TY.ER.AT00
{
    partial class TYATKB006P
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
            this.DTP01_AMRYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_AMRYYMM = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AMRHOSU = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AMRHOSU = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(166, 157);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(85, 157);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // DTP01_AMRYYMM
            // 
            this.DTP01_AMRYYMM.FactoryID = "";
            this.DTP01_AMRYYMM.FactoryName = null;
            this.DTP01_AMRYYMM.Location = new System.Drawing.Point(166, 68);
            this.DTP01_AMRYYMM.Name = "DTP01_AMRYYMM";
            this.DTP01_AMRYYMM.Size = new System.Drawing.Size(80, 21);
            this.DTP01_AMRYYMM.TabIndex = 2;
            // 
            // LBL51_AMRYYMM
            // 
            this.LBL51_AMRYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AMRYYMM.FactoryID = "";
            this.LBL51_AMRYYMM.FactoryName = null;
            this.LBL51_AMRYYMM.IsCreated = false;
            this.LBL51_AMRYYMM.Location = new System.Drawing.Point(60, 68);
            this.LBL51_AMRYYMM.Name = "LBL51_AMRYYMM";
            this.LBL51_AMRYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AMRYYMM.TabIndex = 3;
            this.LBL51_AMRYYMM.Text = "작업년월";
            this.LBL51_AMRYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AMRHOSU
            // 
            this.TXT01_AMRHOSU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AMRHOSU.FactoryID = "";
            this.TXT01_AMRHOSU.FactoryName = null;
            this.TXT01_AMRHOSU.Location = new System.Drawing.Point(166, 95);
            this.TXT01_AMRHOSU.MinLength = 0;
            this.TXT01_AMRHOSU.Name = "TXT01_AMRHOSU";
            this.TXT01_AMRHOSU.Size = new System.Drawing.Size(80, 21);
            this.TXT01_AMRHOSU.TabIndex = 4;
            this.TXT01_AMRHOSU.TabIndexCustom = false;
            // 
            // LBL51_AMRHOSU
            // 
            this.LBL51_AMRHOSU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AMRHOSU.FactoryID = "";
            this.LBL51_AMRHOSU.FactoryName = null;
            this.LBL51_AMRHOSU.IsCreated = false;
            this.LBL51_AMRHOSU.Location = new System.Drawing.Point(60, 95);
            this.LBL51_AMRHOSU.Name = "LBL51_AMRHOSU";
            this.LBL51_AMRHOSU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AMRHOSU.TabIndex = 5;
            this.LBL51_AMRHOSU.Text = "호 수";
            this.LBL51_AMRHOSU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_AMRYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AMRYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AMRHOSU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AMRHOSU);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(325, 260);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYATKB006P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYATKB006P";
            this.Load += new System.EventHandler(this.TYATKB006P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYDatePicker DTP01_AMRYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_AMRYYMM;
        private TY.Service.Library.Controls.TYTextBox TXT01_AMRHOSU;
        private TY.Service.Library.Controls.TYLabel LBL51_AMRHOSU;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}