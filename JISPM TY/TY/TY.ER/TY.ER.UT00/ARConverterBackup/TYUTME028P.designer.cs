namespace TY.ER.UT00
{
    partial class TYUTME028P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH01_KBSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBSABUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(512, 450);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_KBSABUN
            // 
            this.CBH01_KBSABUN.BindedDataRow = null;
            this.CBH01_KBSABUN.CodeBoxWidth = 0;
            this.CBH01_KBSABUN.DummyValue = null;
            this.CBH01_KBSABUN.FactoryID = "";
            this.CBH01_KBSABUN.FactoryName = null;
            this.CBH01_KBSABUN.Location = new System.Drawing.Point(512, 419);
            this.CBH01_KBSABUN.MinLength = 0;
            this.CBH01_KBSABUN.Name = "CBH01_KBSABUN";
            this.CBH01_KBSABUN.Size = new System.Drawing.Size(180, 20);
            this.CBH01_KBSABUN.TabIndex = 2;
            // 
            // LBL51_KBSABUN
            // 
            this.LBL51_KBSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBSABUN.FactoryID = "";
            this.LBL51_KBSABUN.FactoryName = null;
            this.LBL51_KBSABUN.IsCreated = false;
            this.LBL51_KBSABUN.Location = new System.Drawing.Point(406, 419);
            this.LBL51_KBSABUN.Name = "LBL51_KBSABUN";
            this.LBL51_KBSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBSABUN.TabIndex = 3;
            this.LBL51_KBSABUN.Text = "사번";
            this.LBL51_KBSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(638, 392);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 4;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(512, 392);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 6;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(406, 392);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 7;
            this.LBL51_STDATE.Text = "시작일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.label2);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 396);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 320;
            this.label2.Text = "~";
            // 
            // TYUTME028P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME028P";
            this.Load += new System.EventHandler(this.TYUTME028P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_KBSABUN;
        private TY.Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label2;
    }
}