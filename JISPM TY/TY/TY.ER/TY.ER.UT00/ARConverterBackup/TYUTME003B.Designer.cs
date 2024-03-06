namespace TY.ER.UT00
{
    partial class TYUTME003B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.LBL51_SHWAMUL = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SHWAMUL = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_SHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_M2DATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_M2DATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(91, 123);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(253, 123);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 2;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 4;
            this.LBL51_STDATE.Text = "기준일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M2DATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_M2DATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_SHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_SHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(420, 153);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(172, 123);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 314;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // LBL51_SHWAMUL
            // 
            this.LBL51_SHWAMUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SHWAMUL.FactoryID = "";
            this.LBL51_SHWAMUL.FactoryName = null;
            this.LBL51_SHWAMUL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_SHWAMUL.IsCreated = false;
            this.LBL51_SHWAMUL.Location = new System.Drawing.Point(5, 92);
            this.LBL51_SHWAMUL.Name = "LBL51_SHWAMUL";
            this.LBL51_SHWAMUL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SHWAMUL.TabIndex = 313;
            this.LBL51_SHWAMUL.Text = "화 물";
            this.LBL51_SHWAMUL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SHWAMUL
            // 
            this.CBH01_SHWAMUL.BindedDataRow = null;
            this.CBH01_SHWAMUL.CodeBoxWidth = 0;
            this.CBH01_SHWAMUL.DummyValue = null;
            this.CBH01_SHWAMUL.FactoryID = "";
            this.CBH01_SHWAMUL.FactoryName = null;
            this.CBH01_SHWAMUL.Location = new System.Drawing.Point(111, 92);
            this.CBH01_SHWAMUL.MinLength = 0;
            this.CBH01_SHWAMUL.Name = "CBH01_SHWAMUL";
            this.CBH01_SHWAMUL.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SHWAMUL.TabIndex = 312;
            // 
            // LBL51_SHWAJU
            // 
            this.LBL51_SHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SHWAJU.FactoryID = "";
            this.LBL51_SHWAJU.FactoryName = null;
            this.LBL51_SHWAJU.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_SHWAJU.IsCreated = false;
            this.LBL51_SHWAJU.Location = new System.Drawing.Point(5, 66);
            this.LBL51_SHWAJU.Name = "LBL51_SHWAJU";
            this.LBL51_SHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SHWAJU.TabIndex = 310;
            this.LBL51_SHWAJU.Text = "화 주";
            this.LBL51_SHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SHWAJU
            // 
            this.CBH01_SHWAJU.BindedDataRow = null;
            this.CBH01_SHWAJU.CodeBoxWidth = 0;
            this.CBH01_SHWAJU.DummyValue = null;
            this.CBH01_SHWAJU.FactoryID = "";
            this.CBH01_SHWAJU.FactoryName = null;
            this.CBH01_SHWAJU.Location = new System.Drawing.Point(111, 66);
            this.CBH01_SHWAJU.MinLength = 0;
            this.CBH01_SHWAJU.Name = "CBH01_SHWAJU";
            this.CBH01_SHWAJU.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SHWAJU.TabIndex = 309;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 224;
            this.label1.Text = "-";
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 223;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 156;
            // 
            // DTP01_M2DATE
            // 
            this.DTP01_M2DATE.FactoryID = "";
            this.DTP01_M2DATE.FactoryName = null;
            this.DTP01_M2DATE.Location = new System.Drawing.Point(111, 39);
            this.DTP01_M2DATE.Name = "DTP01_M2DATE";
            this.DTP01_M2DATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_M2DATE.TabIndex = 315;
            // 
            // LBL51_M2DATE
            // 
            this.LBL51_M2DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M2DATE.FactoryID = "";
            this.LBL51_M2DATE.FactoryName = null;
            this.LBL51_M2DATE.IsCreated = false;
            this.LBL51_M2DATE.Location = new System.Drawing.Point(5, 39);
            this.LBL51_M2DATE.Name = "LBL51_M2DATE";
            this.LBL51_M2DATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M2DATE.TabIndex = 316;
            this.LBL51_M2DATE.Text = "생성일자";
            this.LBL51_M2DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUTME003B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 155);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME003B";
            this.Load += new System.EventHandler(this.TYUTME003B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCodeBox CBH01_SHWAJU;
        private Service.Library.Controls.TYLabel LBL51_SHWAJU;
        private Service.Library.Controls.TYLabel LBL51_SHWAMUL;
        private Service.Library.Controls.TYCodeBox CBH01_SHWAMUL;
        private Service.Library.Controls.TYButton BTN61_PRT;
        private Service.Library.Controls.TYLabel LBL51_M2DATE;
        private Service.Library.Controls.TYDatePicker DTP01_M2DATE;
    }
}