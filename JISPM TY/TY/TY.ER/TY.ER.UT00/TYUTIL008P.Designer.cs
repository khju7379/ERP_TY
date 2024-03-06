namespace TY.ER.UT00
{
    partial class TYUTIL008P
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
            this.TXT01_JLTANK = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_JLTANK = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(175, 115);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(94, 115);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TXT01_JLTANK
            // 
            this.TXT01_JLTANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_JLTANK.FactoryID = "";
            this.TXT01_JLTANK.FactoryName = null;
            this.TXT01_JLTANK.Location = new System.Drawing.Point(175, 51);
            this.TXT01_JLTANK.MinLength = 0;
            this.TXT01_JLTANK.Name = "TXT01_JLTANK";
            this.TXT01_JLTANK.Size = new System.Drawing.Size(100, 21);
            this.TXT01_JLTANK.TabIndex = 2;
            this.TXT01_JLTANK.TabIndexCustom = false;
            // 
            // LBL51_JLTANK
            // 
            this.LBL51_JLTANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_JLTANK.FactoryID = "";
            this.LBL51_JLTANK.FactoryName = null;
            this.LBL51_JLTANK.IsCreated = false;
            this.LBL51_JLTANK.Location = new System.Drawing.Point(69, 51);
            this.LBL51_JLTANK.Name = "LBL51_JLTANK";
            this.LBL51_JLTANK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JLTANK.TabIndex = 3;
            this.LBL51_JLTANK.Text = "탱크번호";
            this.LBL51_JLTANK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.label5);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_JLTANK);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_JLTANK);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(355, 180);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(58, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 12);
            this.label5.TabIndex = 162;
            this.label5.Text = "전체 출력시 탱크번호를 입력하지 마세요.";
            // 
            // TYUTIL008P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 182);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTIL008P";
            this.Load += new System.EventHandler(this.TYUTIL008P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYTextBox TXT01_JLTANK;
        private TY.Service.Library.Controls.TYLabel LBL51_JLTANK;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label5;
    }
}