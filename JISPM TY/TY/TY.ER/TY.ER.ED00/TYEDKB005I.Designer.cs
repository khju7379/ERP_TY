namespace TY.ER.ED00
{
    partial class TYEDKB005I
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_EDIREJBGB = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_EDIREJBGB = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LBL52_EDIREJBGB = new TY.Service.Library.Controls.TYLabel();
            this.TXT05_EDIREJBGB = new TY.Service.Library.Controls.TYTextBox();
            this.TXT02_EDIREJBGB = new TY.Service.Library.Controls.TYTextBox();
            this.LBL55_EDIREJBGB = new TY.Service.Library.Controls.TYLabel();
            this.TXT04_EDIREJBGB = new TY.Service.Library.Controls.TYTextBox();
            this.TXT03_EDIREJBGB = new TY.Service.Library.Controls.TYTextBox();
            this.LBL53_EDIREJBGB = new TY.Service.Library.Controls.TYLabel();
            this.LBL54_EDIREJBGB = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AFFILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFFILENAME = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(703, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(363, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "수신/변환";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            this.BTN61_SAV.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_SAV_InvokerStart);
            this.BTN61_SAV.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_SAV_InvokerEnd);
            // 
            // TXT01_EDIREJBGB
            // 
            this.TXT01_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_EDIREJBGB.FactoryID = "";
            this.TXT01_EDIREJBGB.FactoryName = null;
            this.TXT01_EDIREJBGB.Location = new System.Drawing.Point(112, 53);
            this.TXT01_EDIREJBGB.MinLength = 0;
            this.TXT01_EDIREJBGB.Name = "TXT01_EDIREJBGB";
            this.TXT01_EDIREJBGB.Size = new System.Drawing.Size(41, 21);
            this.TXT01_EDIREJBGB.TabIndex = 2;
            this.TXT01_EDIREJBGB.TabIndexCustom = false;
            // 
            // LBL51_EDIREJBGB
            // 
            this.LBL51_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EDIREJBGB.FactoryID = "";
            this.LBL51_EDIREJBGB.FactoryName = null;
            this.LBL51_EDIREJBGB.IsCreated = false;
            this.LBL51_EDIREJBGB.Location = new System.Drawing.Point(6, 53);
            this.LBL51_EDIREJBGB.Name = "LBL51_EDIREJBGB";
            this.LBL51_EDIREJBGB.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EDIREJBGB.TabIndex = 3;
            this.LBL51_EDIREJBGB.Text = "반입예정";
            this.LBL51_EDIREJBGB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.pgBar);
            this.GBX80_CONTROLS.Controls.Add(this.groupBox1);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFFILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFFILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(784, 186);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(444, 20);
            this.pgBar.MarqueeAnimationSpeed = 10;
            this.pgBar.Maximum = 1000;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(252, 10);
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgBar.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LBL51_EDIREJBGB);
            this.groupBox1.Controls.Add(this.TXT01_EDIREJBGB);
            this.groupBox1.Controls.Add(this.LBL52_EDIREJBGB);
            this.groupBox1.Controls.Add(this.TXT05_EDIREJBGB);
            this.groupBox1.Controls.Add(this.TXT02_EDIREJBGB);
            this.groupBox1.Controls.Add(this.LBL55_EDIREJBGB);
            this.groupBox1.Controls.Add(this.TXT04_EDIREJBGB);
            this.groupBox1.Controls.Add(this.TXT03_EDIREJBGB);
            this.groupBox1.Controls.Add(this.LBL53_EDIREJBGB);
            this.groupBox1.Controls.Add(this.LBL54_EDIREJBGB);
            this.groupBox1.Location = new System.Drawing.Point(6, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(777, 134);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "변환내역";
            // 
            // LBL52_EDIREJBGB
            // 
            this.LBL52_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL52_EDIREJBGB.FactoryID = "";
            this.LBL52_EDIREJBGB.FactoryName = null;
            this.LBL52_EDIREJBGB.IsCreated = false;
            this.LBL52_EDIREJBGB.Location = new System.Drawing.Point(159, 53);
            this.LBL52_EDIREJBGB.Name = "LBL52_EDIREJBGB";
            this.LBL52_EDIREJBGB.Size = new System.Drawing.Size(100, 21);
            this.LBL52_EDIREJBGB.TabIndex = 5;
            this.LBL52_EDIREJBGB.Text = "반출승인";
            this.LBL52_EDIREJBGB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT05_EDIREJBGB
            // 
            this.TXT05_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT05_EDIREJBGB.FactoryID = "";
            this.TXT05_EDIREJBGB.FactoryName = null;
            this.TXT05_EDIREJBGB.Location = new System.Drawing.Point(724, 53);
            this.TXT05_EDIREJBGB.MinLength = 0;
            this.TXT05_EDIREJBGB.Name = "TXT05_EDIREJBGB";
            this.TXT05_EDIREJBGB.Size = new System.Drawing.Size(41, 21);
            this.TXT05_EDIREJBGB.TabIndex = 15;
            this.TXT05_EDIREJBGB.TabIndexCustom = false;
            // 
            // TXT02_EDIREJBGB
            // 
            this.TXT02_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT02_EDIREJBGB.FactoryID = "";
            this.TXT02_EDIREJBGB.FactoryName = null;
            this.TXT02_EDIREJBGB.Location = new System.Drawing.Point(265, 53);
            this.TXT02_EDIREJBGB.MinLength = 0;
            this.TXT02_EDIREJBGB.Name = "TXT02_EDIREJBGB";
            this.TXT02_EDIREJBGB.Size = new System.Drawing.Size(41, 21);
            this.TXT02_EDIREJBGB.TabIndex = 14;
            this.TXT02_EDIREJBGB.TabIndexCustom = false;
            // 
            // LBL55_EDIREJBGB
            // 
            this.LBL55_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL55_EDIREJBGB.FactoryID = "";
            this.LBL55_EDIREJBGB.FactoryName = null;
            this.LBL55_EDIREJBGB.IsCreated = false;
            this.LBL55_EDIREJBGB.Location = new System.Drawing.Point(618, 53);
            this.LBL55_EDIREJBGB.Name = "LBL55_EDIREJBGB";
            this.LBL55_EDIREJBGB.Size = new System.Drawing.Size(100, 21);
            this.LBL55_EDIREJBGB.TabIndex = 11;
            this.LBL55_EDIREJBGB.Text = "오류통보";
            this.LBL55_EDIREJBGB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT04_EDIREJBGB
            // 
            this.TXT04_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT04_EDIREJBGB.FactoryID = "";
            this.TXT04_EDIREJBGB.FactoryName = null;
            this.TXT04_EDIREJBGB.Location = new System.Drawing.Point(571, 53);
            this.TXT04_EDIREJBGB.MinLength = 0;
            this.TXT04_EDIREJBGB.Name = "TXT04_EDIREJBGB";
            this.TXT04_EDIREJBGB.Size = new System.Drawing.Size(41, 21);
            this.TXT04_EDIREJBGB.TabIndex = 12;
            this.TXT04_EDIREJBGB.TabIndexCustom = false;
            // 
            // TXT03_EDIREJBGB
            // 
            this.TXT03_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT03_EDIREJBGB.FactoryID = "";
            this.TXT03_EDIREJBGB.FactoryName = null;
            this.TXT03_EDIREJBGB.Location = new System.Drawing.Point(418, 53);
            this.TXT03_EDIREJBGB.MinLength = 0;
            this.TXT03_EDIREJBGB.Name = "TXT03_EDIREJBGB";
            this.TXT03_EDIREJBGB.Size = new System.Drawing.Size(41, 21);
            this.TXT03_EDIREJBGB.TabIndex = 13;
            this.TXT03_EDIREJBGB.TabIndexCustom = false;
            // 
            // LBL53_EDIREJBGB
            // 
            this.LBL53_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL53_EDIREJBGB.FactoryID = "";
            this.LBL53_EDIREJBGB.FactoryName = null;
            this.LBL53_EDIREJBGB.IsCreated = false;
            this.LBL53_EDIREJBGB.Location = new System.Drawing.Point(312, 53);
            this.LBL53_EDIREJBGB.Name = "LBL53_EDIREJBGB";
            this.LBL53_EDIREJBGB.Size = new System.Drawing.Size(100, 21);
            this.LBL53_EDIREJBGB.TabIndex = 7;
            this.LBL53_EDIREJBGB.Text = "접수통보";
            this.LBL53_EDIREJBGB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL54_EDIREJBGB
            // 
            this.LBL54_EDIREJBGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL54_EDIREJBGB.FactoryID = "";
            this.LBL54_EDIREJBGB.FactoryName = null;
            this.LBL54_EDIREJBGB.IsCreated = false;
            this.LBL54_EDIREJBGB.Location = new System.Drawing.Point(465, 53);
            this.LBL54_EDIREJBGB.Name = "LBL54_EDIREJBGB";
            this.LBL54_EDIREJBGB.Size = new System.Drawing.Size(100, 21);
            this.LBL54_EDIREJBGB.TabIndex = 9;
            this.LBL54_EDIREJBGB.Text = "정정결과";
            this.LBL54_EDIREJBGB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AFFILENAME
            // 
            this.TXT01_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFFILENAME.FactoryID = "";
            this.TXT01_AFFILENAME.FactoryName = null;
            this.TXT01_AFFILENAME.Location = new System.Drawing.Point(111, 12);
            this.TXT01_AFFILENAME.MinLength = 0;
            this.TXT01_AFFILENAME.Name = "TXT01_AFFILENAME";
            this.TXT01_AFFILENAME.Size = new System.Drawing.Size(247, 21);
            this.TXT01_AFFILENAME.TabIndex = 17;
            this.TXT01_AFFILENAME.TabIndexCustom = false;
            // 
            // LBL51_AFFILENAME
            // 
            this.LBL51_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFFILENAME.FactoryID = "";
            this.LBL51_AFFILENAME.FactoryName = null;
            this.LBL51_AFFILENAME.IsCreated = false;
            this.LBL51_AFFILENAME.Location = new System.Drawing.Point(5, 12);
            this.LBL51_AFFILENAME.Name = "LBL51_AFFILENAME";
            this.LBL51_AFFILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFFILENAME.TabIndex = 16;
            this.LBL51_AFFILENAME.Text = "반입예정";
            this.LBL51_AFFILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYEDKB005I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 191);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYEDKB005I";
            this.Load += new System.EventHandler(this.TYEDKB005I_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYTextBox TXT01_EDIREJBGB;
        private TY.Service.Library.Controls.TYLabel LBL51_EDIREJBGB;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT05_EDIREJBGB;
        private Service.Library.Controls.TYTextBox TXT02_EDIREJBGB;
        private Service.Library.Controls.TYTextBox TXT03_EDIREJBGB;
        private Service.Library.Controls.TYTextBox TXT04_EDIREJBGB;
        private Service.Library.Controls.TYLabel LBL55_EDIREJBGB;
        private Service.Library.Controls.TYLabel LBL54_EDIREJBGB;
        private Service.Library.Controls.TYLabel LBL53_EDIREJBGB;
        private Service.Library.Controls.TYLabel LBL52_EDIREJBGB;
        private Service.Library.Controls.TYTextBox TXT01_AFFILENAME;
        private Service.Library.Controls.TYLabel LBL51_AFFILENAME;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}