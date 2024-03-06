namespace TY.ER.UT00
{
    partial class TYUTIL014P
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
            this.CBH01_EMHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_EMHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_YYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_YYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_DOWN = new TY.Service.Library.Controls.TYButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
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
            // CBH01_EMHWAJU
            // 
            this.CBH01_EMHWAJU.BindedDataRow = null;
            this.CBH01_EMHWAJU.CodeBoxWidth = 0;
            this.CBH01_EMHWAJU.DummyValue = null;
            this.CBH01_EMHWAJU.FactoryID = "";
            this.CBH01_EMHWAJU.FactoryName = null;
            this.CBH01_EMHWAJU.Location = new System.Drawing.Point(512, 397);
            this.CBH01_EMHWAJU.MinLength = 0;
            this.CBH01_EMHWAJU.Name = "CBH01_EMHWAJU";
            this.CBH01_EMHWAJU.Size = new System.Drawing.Size(200, 20);
            this.CBH01_EMHWAJU.TabIndex = 3;
            // 
            // LBL51_EMHWAJU
            // 
            this.LBL51_EMHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EMHWAJU.FactoryID = "";
            this.LBL51_EMHWAJU.FactoryName = null;
            this.LBL51_EMHWAJU.IsCreated = false;
            this.LBL51_EMHWAJU.Location = new System.Drawing.Point(406, 397);
            this.LBL51_EMHWAJU.Name = "LBL51_EMHWAJU";
            this.LBL51_EMHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EMHWAJU.TabIndex = 4;
            this.LBL51_EMHWAJU.Text = "화주";
            this.LBL51_EMHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_YYYYMM
            // 
            this.DTP01_YYYYMM.FactoryID = "";
            this.DTP01_YYYYMM.FactoryName = null;
            this.DTP01_YYYYMM.Location = new System.Drawing.Point(512, 370);
            this.DTP01_YYYYMM.Name = "DTP01_YYYYMM";
            this.DTP01_YYYYMM.Size = new System.Drawing.Size(80, 21);
            this.DTP01_YYYYMM.TabIndex = 5;
            // 
            // LBL51_YYYYMM
            // 
            this.LBL51_YYYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_YYYYMM.FactoryID = "";
            this.LBL51_YYYYMM.FactoryName = null;
            this.LBL51_YYYYMM.IsCreated = false;
            this.LBL51_YYYYMM.Location = new System.Drawing.Point(406, 370);
            this.LBL51_YYYYMM.Name = "LBL51_YYYYMM";
            this.LBL51_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYYYMM.TabIndex = 6;
            this.LBL51_YYYYMM.Text = "일자";
            this.LBL51_YYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_EMHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EMHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_YYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_YYYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(512, 423);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(433, 21);
            this.txtFolder.TabIndex = 31;
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(951, 423);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 30;
            this.BTN61_SEARCH.Text = "경로지정";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // BTN61_DOWN
            // 
            this.BTN61_DOWN.FactoryID = "";
            this.BTN61_DOWN.FactoryName = null;
            this.BTN61_DOWN.Location = new System.Drawing.Point(673, 455);
            this.BTN61_DOWN.Name = "BTN61_DOWN";
            this.BTN61_DOWN.Size = new System.Drawing.Size(75, 21);
            this.BTN61_DOWN.TabIndex = 13;
            this.BTN61_DOWN.Text = "다운로드";
            this.BTN61_DOWN.UseVisualStyleBackColor = true;
            this.BTN61_DOWN.Click += new System.EventHandler(this.BTN61_DOWN_Click);
            // 
            // TYUTIL014P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTIL014P";
            this.Load += new System.EventHandler(this.TYUTIL014P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_EMHWAJU;
        private TY.Service.Library.Controls.TYLabel LBL51_EMHWAJU;
        private TY.Service.Library.Controls.TYDatePicker DTP01_YYYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_YYYYMM;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_DOWN;
        private System.Windows.Forms.TextBox txtFolder;
        private Service.Library.Controls.TYButton BTN61_SEARCH;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}