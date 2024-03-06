namespace TY.ER.BS00
{
    partial class TYBSSJ015B
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
            this.CBH01_BLJYYMM = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_BLJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CKB01_BPCHK_ALL = new TY.Service.Library.Controls.TYCheckBox();
            this.CKB01_BPCHK_PR = new TY.Service.Library.Controls.TYCheckBox();
            this.CKB01_BPCHK_CM = new TY.Service.Library.Controls.TYCheckBox();
            this.CKB01_BPCHK_IN = new TY.Service.Library.Controls.TYCheckBox();
            this.CKB01_BPCHK_MA = new TY.Service.Library.Controls.TYCheckBox();
            this.LBL51_BLCHKMC = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(160, 222);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(241, 222);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // CBH01_BLJYYMM
            // 
            this.CBH01_BLJYYMM.BindedDataRow = null;
            this.CBH01_BLJYYMM.CodeBoxWidth = 0;
            this.CBH01_BLJYYMM.DummyValue = null;
            this.CBH01_BLJYYMM.FactoryID = "";
            this.CBH01_BLJYYMM.FactoryName = null;
            this.CBH01_BLJYYMM.Location = new System.Drawing.Point(237, 67);
            this.CBH01_BLJYYMM.MinLength = 0;
            this.CBH01_BLJYYMM.Name = "CBH01_BLJYYMM";
            this.CBH01_BLJYYMM.Size = new System.Drawing.Size(100, 20);
            this.CBH01_BLJYYMM.TabIndex = 2;
            this.CBH01_BLJYYMM.CodeBoxDataBinded += new Shoveling2010.SmartClient.SystemUtility.Controls.TCodeBox.TCodeBoxEventHandler(this.CBH01_BLJYYMM_CodeBoxDataBinded);
            // 
            // LBL51_BLJYYMM
            // 
            this.LBL51_BLJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BLJYYMM.FactoryID = "";
            this.LBL51_BLJYYMM.FactoryName = null;
            this.LBL51_BLJYYMM.IsCreated = false;
            this.LBL51_BLJYYMM.Location = new System.Drawing.Point(131, 67);
            this.LBL51_BLJYYMM.Name = "LBL51_BLJYYMM";
            this.LBL51_BLJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BLJYYMM.TabIndex = 3;
            this.LBL51_BLJYYMM.Text = "실적생성년월";
            this.LBL51_BLJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_BPCHK_ALL);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_BPCHK_PR);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_BPCHK_CM);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_BPCHK_IN);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_BPCHK_MA);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BLCHKMC);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BLJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BLJYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(469, 279);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CKB01_BPCHK_ALL
            // 
            this.CKB01_BPCHK_ALL.AutoSize = true;
            this.CKB01_BPCHK_ALL.FactoryID = "";
            this.CKB01_BPCHK_ALL.FactoryName = null;
            this.CKB01_BPCHK_ALL.Location = new System.Drawing.Point(237, 99);
            this.CKB01_BPCHK_ALL.Name = "CKB01_BPCHK_ALL";
            this.CKB01_BPCHK_ALL.Size = new System.Drawing.Size(52, 16);
            this.CKB01_BPCHK_ALL.TabIndex = 30;
            this.CKB01_BPCHK_ALL.Text = "전 체";
            this.CKB01_BPCHK_ALL.UseVisualStyleBackColor = true;
            this.CKB01_BPCHK_ALL.CheckedChanged += new System.EventHandler(this.CKB01_BPCHK_ALL_CheckedChanged);
            // 
            // CKB01_BPCHK_PR
            // 
            this.CKB01_BPCHK_PR.AutoSize = true;
            this.CKB01_BPCHK_PR.FactoryID = "";
            this.CKB01_BPCHK_PR.FactoryName = null;
            this.CKB01_BPCHK_PR.Location = new System.Drawing.Point(237, 187);
            this.CKB01_BPCHK_PR.Name = "CKB01_BPCHK_PR";
            this.CKB01_BPCHK_PR.Size = new System.Drawing.Size(84, 16);
            this.CKB01_BPCHK_PR.TabIndex = 28;
            this.CKB01_BPCHK_PR.Text = "영업외손익";
            this.CKB01_BPCHK_PR.UseVisualStyleBackColor = true;
            // 
            // CKB01_BPCHK_CM
            // 
            this.CKB01_BPCHK_CM.AutoSize = true;
            this.CKB01_BPCHK_CM.FactoryID = "";
            this.CKB01_BPCHK_CM.FactoryName = null;
            this.CKB01_BPCHK_CM.Location = new System.Drawing.Point(237, 166);
            this.CKB01_BPCHK_CM.Name = "CKB01_BPCHK_CM";
            this.CKB01_BPCHK_CM.Size = new System.Drawing.Size(72, 16);
            this.CKB01_BPCHK_CM.TabIndex = 26;
            this.CKB01_BPCHK_CM.Text = "영업비용";
            this.CKB01_BPCHK_CM.UseVisualStyleBackColor = true;
            // 
            // CKB01_BPCHK_IN
            // 
            this.CKB01_BPCHK_IN.AutoSize = true;
            this.CKB01_BPCHK_IN.FactoryID = "";
            this.CKB01_BPCHK_IN.FactoryName = null;
            this.CKB01_BPCHK_IN.Location = new System.Drawing.Point(237, 143);
            this.CKB01_BPCHK_IN.Name = "CKB01_BPCHK_IN";
            this.CKB01_BPCHK_IN.Size = new System.Drawing.Size(76, 16);
            this.CKB01_BPCHK_IN.TabIndex = 24;
            this.CKB01_BPCHK_IN.Text = "투자,수선";
            this.CKB01_BPCHK_IN.UseVisualStyleBackColor = true;
            // 
            // CKB01_BPCHK_MA
            // 
            this.CKB01_BPCHK_MA.AutoSize = true;
            this.CKB01_BPCHK_MA.FactoryID = "";
            this.CKB01_BPCHK_MA.FactoryName = null;
            this.CKB01_BPCHK_MA.Location = new System.Drawing.Point(237, 119);
            this.CKB01_BPCHK_MA.Name = "CKB01_BPCHK_MA";
            this.CKB01_BPCHK_MA.Size = new System.Drawing.Size(100, 16);
            this.CKB01_BPCHK_MA.TabIndex = 22;
            this.CKB01_BPCHK_MA.Text = "매출액,취급량";
            this.CKB01_BPCHK_MA.UseVisualStyleBackColor = true;
            // 
            // LBL51_BLCHKMC
            // 
            this.LBL51_BLCHKMC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BLCHKMC.FactoryID = "";
            this.LBL51_BLCHKMC.FactoryName = null;
            this.LBL51_BLCHKMC.IsCreated = false;
            this.LBL51_BLCHKMC.Location = new System.Drawing.Point(131, 97);
            this.LBL51_BLCHKMC.Name = "LBL51_BLCHKMC";
            this.LBL51_BLCHKMC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BLCHKMC.TabIndex = 21;
            this.LBL51_BLCHKMC.Text = "마감구분";
            this.LBL51_BLCHKMC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYBSSJ015B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 283);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYBSSJ015B";
            this.Load += new System.EventHandler(this.TYBSSJ015B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYCodeBox CBH01_BLJYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_BLJYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCheckBox CKB01_BPCHK_ALL;
        private Service.Library.Controls.TYCheckBox CKB01_BPCHK_PR;
        private Service.Library.Controls.TYCheckBox CKB01_BPCHK_CM;
        private Service.Library.Controls.TYCheckBox CKB01_BPCHK_IN;
        private Service.Library.Controls.TYCheckBox CKB01_BPCHK_MA;
        private Service.Library.Controls.TYLabel LBL51_BLCHKMC;
    }
}