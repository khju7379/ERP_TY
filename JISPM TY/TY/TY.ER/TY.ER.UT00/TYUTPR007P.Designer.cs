namespace TY.ER.UT00
{
    partial class TYUTPR007P
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
            this.TXT01_GATANKNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_GATANKNO = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(179, 114);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(98, 114);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TXT01_GATANKNO
            // 
            this.TXT01_GATANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_GATANKNO.FactoryID = "";
            this.TXT01_GATANKNO.FactoryName = null;
            this.TXT01_GATANKNO.Location = new System.Drawing.Point(179, 63);
            this.TXT01_GATANKNO.MinLength = 0;
            this.TXT01_GATANKNO.Name = "TXT01_GATANKNO";
            this.TXT01_GATANKNO.Size = new System.Drawing.Size(80, 21);
            this.TXT01_GATANKNO.TabIndex = 2;
            this.TXT01_GATANKNO.TabIndexCustom = false;
            // 
            // LBL51_GATANKNO
            // 
            this.LBL51_GATANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GATANKNO.FactoryID = "";
            this.LBL51_GATANKNO.FactoryName = null;
            this.LBL51_GATANKNO.IsCreated = false;
            this.LBL51_GATANKNO.Location = new System.Drawing.Point(73, 63);
            this.LBL51_GATANKNO.Name = "LBL51_GATANKNO";
            this.LBL51_GATANKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GATANKNO.TabIndex = 3;
            this.LBL51_GATANKNO.Text = "TANK번호";
            this.LBL51_GATANKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_GATANKNO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GATANKNO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(335, 210);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUTPR007P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 212);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTPR007P";
            this.Load += new System.EventHandler(this.TYUTPR007P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYTextBox TXT01_GATANKNO;
        private TY.Service.Library.Controls.TYLabel LBL51_GATANKNO;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}