namespace TY.ER.UT00
{
    partial class TYUTME022P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH01_CHHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CHHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_CHHWAMUL = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CHHWAMUL = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_UTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_UTDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_DOWN = new TY.Service.Library.Controls.TYButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(511, 455);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(592, 455);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 2;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_CHHWAJU
            // 
            this.CBH01_CHHWAJU.BindedDataRow = null;
            this.CBH01_CHHWAJU.CodeBoxWidth = 0;
            this.CBH01_CHHWAJU.DummyValue = null;
            this.CBH01_CHHWAJU.FactoryID = "";
            this.CBH01_CHHWAJU.FactoryName = null;
            this.CBH01_CHHWAJU.Location = new System.Drawing.Point(512, 370);
            this.CBH01_CHHWAJU.MinLength = 0;
            this.CBH01_CHHWAJU.Name = "CBH01_CHHWAJU";
            this.CBH01_CHHWAJU.Size = new System.Drawing.Size(250, 20);
            this.CBH01_CHHWAJU.TabIndex = 3;
            // 
            // LBL51_CHHWAJU
            // 
            this.LBL51_CHHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHHWAJU.FactoryID = "";
            this.LBL51_CHHWAJU.FactoryName = null;
            this.LBL51_CHHWAJU.IsCreated = false;
            this.LBL51_CHHWAJU.Location = new System.Drawing.Point(406, 370);
            this.LBL51_CHHWAJU.Name = "LBL51_CHHWAJU";
            this.LBL51_CHHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHHWAJU.TabIndex = 4;
            this.LBL51_CHHWAJU.Text = "화주";
            this.LBL51_CHHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_CHHWAMUL
            // 
            this.CBH01_CHHWAMUL.BindedDataRow = null;
            this.CBH01_CHHWAMUL.CodeBoxWidth = 0;
            this.CBH01_CHHWAMUL.DummyValue = null;
            this.CBH01_CHHWAMUL.FactoryID = "";
            this.CBH01_CHHWAMUL.FactoryName = null;
            this.CBH01_CHHWAMUL.Location = new System.Drawing.Point(512, 397);
            this.CBH01_CHHWAMUL.MinLength = 0;
            this.CBH01_CHHWAMUL.Name = "CBH01_CHHWAMUL";
            this.CBH01_CHHWAMUL.Size = new System.Drawing.Size(250, 20);
            this.CBH01_CHHWAMUL.TabIndex = 5;
            // 
            // LBL51_CHHWAMUL
            // 
            this.LBL51_CHHWAMUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHHWAMUL.FactoryID = "";
            this.LBL51_CHHWAMUL.FactoryName = null;
            this.LBL51_CHHWAMUL.IsCreated = false;
            this.LBL51_CHHWAMUL.Location = new System.Drawing.Point(406, 397);
            this.LBL51_CHHWAMUL.Name = "LBL51_CHHWAMUL";
            this.LBL51_CHHWAMUL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHHWAMUL.TabIndex = 6;
            this.LBL51_CHHWAMUL.Text = "화물";
            this.LBL51_CHHWAMUL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_UTDATE
            // 
            this.DTP01_UTDATE.FactoryID = "";
            this.DTP01_UTDATE.FactoryName = null;
            this.DTP01_UTDATE.Location = new System.Drawing.Point(512, 343);
            this.DTP01_UTDATE.Name = "DTP01_UTDATE";
            this.DTP01_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_UTDATE.TabIndex = 7;
            // 
            // LBL51_UTDATE
            // 
            this.LBL51_UTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_UTDATE.FactoryID = "";
            this.LBL51_UTDATE.FactoryName = null;
            this.LBL51_UTDATE.IsCreated = false;
            this.LBL51_UTDATE.Location = new System.Drawing.Point(406, 343);
            this.LBL51_UTDATE.Name = "LBL51_UTDATE";
            this.LBL51_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_UTDATE.TabIndex = 8;
            this.LBL51_UTDATE.Text = "매출일자";
            this.LBL51_UTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.txtFolder);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SEARCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_DOWN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_CHHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_UTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_UTDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_DOWN
            // 
            this.BTN61_DOWN.FactoryID = "";
            this.BTN61_DOWN.FactoryName = null;
            this.BTN61_DOWN.Location = new System.Drawing.Point(673, 455);
            this.BTN61_DOWN.Name = "BTN61_DOWN";
            this.BTN61_DOWN.Size = new System.Drawing.Size(75, 21);
            this.BTN61_DOWN.TabIndex = 9;
            this.BTN61_DOWN.Text = "다운로드";
            this.BTN61_DOWN.UseVisualStyleBackColor = true;
            this.BTN61_DOWN.Click += new System.EventHandler(this.BTN61_DOWN_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(512, 423);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(433, 21);
            this.txtFolder.TabIndex = 25;
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(951, 423);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 24;
            this.BTN61_SEARCH.Text = "경로지정";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // TYUTME022P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME022P";
            this.Load += new System.EventHandler(this.TYUTME022P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_CHHWAJU;
        private TY.Service.Library.Controls.TYLabel LBL51_CHHWAJU;
        private TY.Service.Library.Controls.TYCodeBox CBH01_CHHWAMUL;
        private TY.Service.Library.Controls.TYLabel LBL51_CHHWAMUL;
        private TY.Service.Library.Controls.TYDatePicker DTP01_UTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_UTDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_DOWN;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtFolder;
        private Service.Library.Controls.TYButton BTN61_SEARCH;
    }
}