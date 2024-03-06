namespace TY.ER.HR00
{
    partial class TYHRYB001B
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_YYSABUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_YYSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(133, 122);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(214, 122);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_YYSABUN
            // 
            this.LBL51_YYSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_YYSABUN.FactoryID = "";
            this.LBL51_YYSABUN.FactoryName = null;
            this.LBL51_YYSABUN.IsCreated = false;
            this.LBL51_YYSABUN.Location = new System.Drawing.Point(21, 60);
            this.LBL51_YYSABUN.Name = "LBL51_YYSABUN";
            this.LBL51_YYSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYSABUN.TabIndex = 3;
            this.LBL51_YYSABUN.Text = "사 번";
            this.LBL51_YYSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(127, 34);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 4;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(21, 34);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 5;
            this.LBL51_STDATE.Text = "작업일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_YYSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_YYSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(431, 181);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_YYSABUN
            // 
            this.CBH01_YYSABUN.BindedDataRow = null;
            this.CBH01_YYSABUN.CodeBoxWidth = 0;
            this.CBH01_YYSABUN.DummyValue = null;
            this.CBH01_YYSABUN.FactoryID = "";
            this.CBH01_YYSABUN.FactoryName = null;
            this.CBH01_YYSABUN.Location = new System.Drawing.Point(127, 61);
            this.CBH01_YYSABUN.MinLength = 0;
            this.CBH01_YYSABUN.Name = "CBH01_YYSABUN";
            this.CBH01_YYSABUN.Size = new System.Drawing.Size(140, 20);
            this.CBH01_YYSABUN.TabIndex = 19;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(252, 34);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "~";
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(21, 86);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 51;
            this.LBL51_GGUBUN.Text = "구 분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(127, 87);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(140, 20);
            this.CBO01_GGUBUN.TabIndex = 52;
            // 
            // TYHRYB001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 183);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRYB001B";
            this.Load += new System.EventHandler(this.TYHRYB001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_YYSABUN;
        private TY.Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCodeBox CBH01_YYSABUN;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
    }
}