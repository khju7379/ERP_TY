namespace TY.ER.HR00
{
    partial class TYHRYB001P
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
            this.CBH01_YYSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_YYSABUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_YYDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_YYDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_INQOPTION = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_INQOPTION = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(224, 133);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(143, 133);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_YYSABUN
            // 
            this.CBH01_YYSABUN.BindedDataRow = null;
            this.CBH01_YYSABUN.CodeBoxWidth = 0;
            this.CBH01_YYSABUN.DummyValue = null;
            this.CBH01_YYSABUN.FactoryID = "";
            this.CBH01_YYSABUN.FactoryName = null;
            this.CBH01_YYSABUN.Location = new System.Drawing.Point(224, 65);
            this.CBH01_YYSABUN.MinLength = 0;
            this.CBH01_YYSABUN.Name = "CBH01_YYSABUN";
            this.CBH01_YYSABUN.Size = new System.Drawing.Size(160, 20);
            this.CBH01_YYSABUN.TabIndex = 2;
            // 
            // LBL51_YYSABUN
            // 
            this.LBL51_YYSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_YYSABUN.FactoryID = "";
            this.LBL51_YYSABUN.FactoryName = null;
            this.LBL51_YYSABUN.IsCreated = false;
            this.LBL51_YYSABUN.Location = new System.Drawing.Point(118, 65);
            this.LBL51_YYSABUN.Name = "LBL51_YYSABUN";
            this.LBL51_YYSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYSABUN.TabIndex = 3;
            this.LBL51_YYSABUN.Text = "사　　번";
            this.LBL51_YYSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_YYDATE
            // 
            this.DTP01_YYDATE.FactoryID = "";
            this.DTP01_YYDATE.FactoryName = null;
            this.DTP01_YYDATE.Location = new System.Drawing.Point(224, 38);
            this.DTP01_YYDATE.Name = "DTP01_YYDATE";
            this.DTP01_YYDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_YYDATE.TabIndex = 4;
            // 
            // LBL51_YYDATE
            // 
            this.LBL51_YYDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_YYDATE.FactoryID = "";
            this.LBL51_YYDATE.FactoryName = null;
            this.LBL51_YYDATE.IsCreated = false;
            this.LBL51_YYDATE.Location = new System.Drawing.Point(118, 38);
            this.LBL51_YYDATE.Name = "LBL51_YYDATE";
            this.LBL51_YYDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYDATE.TabIndex = 5;
            this.LBL51_YYDATE.Text = "지급년월";
            this.LBL51_YYDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_YYSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_YYSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_YYDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_YYDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(460, 165);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_INQOPTION
            // 
            this.LBL51_INQOPTION.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_INQOPTION.FactoryID = "";
            this.LBL51_INQOPTION.FactoryName = null;
            this.LBL51_INQOPTION.IsCreated = false;
            this.LBL51_INQOPTION.Location = new System.Drawing.Point(118, 93);
            this.LBL51_INQOPTION.Name = "LBL51_INQOPTION";
            this.LBL51_INQOPTION.Size = new System.Drawing.Size(100, 21);
            this.LBL51_INQOPTION.TabIndex = 6;
            this.LBL51_INQOPTION.Text = "출력구분";
            this.LBL51_INQOPTION.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_INQOPTION
            // 
            this.CBO01_INQOPTION.FactoryID = "";
            this.CBO01_INQOPTION.FactoryName = null;
            this.CBO01_INQOPTION.Location = new System.Drawing.Point(224, 93);
            this.CBO01_INQOPTION.Name = "CBO01_INQOPTION";
            this.CBO01_INQOPTION.Size = new System.Drawing.Size(100, 20);
            this.CBO01_INQOPTION.TabIndex = 7;
            // 
            // TYHRYB001P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 167);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRYB001P";
            this.Load += new System.EventHandler(this.TYHRYB001P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_YYSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_YYSABUN;
        private TY.Service.Library.Controls.TYDatePicker DTP01_YYDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_YYDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_INQOPTION;
        private Service.Library.Controls.TYComboBox CBO01_INQOPTION;
    }
}