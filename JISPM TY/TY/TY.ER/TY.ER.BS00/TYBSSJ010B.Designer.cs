namespace TY.ER.BS00
{
    partial class TYBSSJ010B
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
            this.LBL51_BSJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GOKCR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_GOKCR = new TY.Service.Library.Controls.TYComboBox();
            this.CBH01_BSJYYMM = new TY.Service.Library.Controls.TYCodeBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(158, 176);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(239, 176);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_BSJYYMM
            // 
            this.LBL51_BSJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BSJYYMM.FactoryID = "";
            this.LBL51_BSJYYMM.FactoryName = null;
            this.LBL51_BSJYYMM.IsCreated = false;
            this.LBL51_BSJYYMM.Location = new System.Drawing.Point(143, 70);
            this.LBL51_BSJYYMM.Name = "LBL51_BSJYYMM";
            this.LBL51_BSJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BSJYYMM.TabIndex = 3;
            this.LBL51_BSJYYMM.Text = "실적생성년월";
            this.LBL51_BSJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GOKCR
            // 
            this.LBL51_GOKCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GOKCR.FactoryID = "";
            this.LBL51_GOKCR.FactoryName = null;
            this.LBL51_GOKCR.IsCreated = false;
            this.LBL51_GOKCR.Location = new System.Drawing.Point(143, 106);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 5;
            this.LBL51_GOKCR.Text = "생성구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BSJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BSJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(509, 264);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_GOKCR
            // 
            this.CBO01_GOKCR.FactoryID = "";
            this.CBO01_GOKCR.FactoryName = null;
            this.CBO01_GOKCR.Location = new System.Drawing.Point(249, 107);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(75, 20);
            this.CBO01_GOKCR.TabIndex = 14;
            // 
            // CBH01_BSJYYMM
            // 
            this.CBH01_BSJYYMM.BindedDataRow = null;
            this.CBH01_BSJYYMM.CodeBoxWidth = 0;
            this.CBH01_BSJYYMM.DummyValue = null;
            this.CBH01_BSJYYMM.FactoryID = "";
            this.CBH01_BSJYYMM.FactoryName = null;
            this.CBH01_BSJYYMM.Location = new System.Drawing.Point(249, 71);
            this.CBH01_BSJYYMM.MinLength = 0;
            this.CBH01_BSJYYMM.Name = "CBH01_BSJYYMM";
            this.CBH01_BSJYYMM.Size = new System.Drawing.Size(75, 20);
            this.CBH01_BSJYYMM.TabIndex = 13;
            // 
            // TYBSSJ010B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 268);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYBSSJ010B";
            this.Load += new System.EventHandler(this.TYBSSJ010B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_BSJYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GOKCR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_BSJYYMM;
        private Service.Library.Controls.TYComboBox CBO01_GOKCR;
    }
}