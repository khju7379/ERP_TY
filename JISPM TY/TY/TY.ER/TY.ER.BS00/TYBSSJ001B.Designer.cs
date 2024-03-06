namespace TY.ER.BS00
{
    partial class TYBSSJ001B
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
            this.DTP01_BSJYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_BSJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GOKCR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBH01_BIJYYMM = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_BIJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GOKCR = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(168, 235);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(249, 235);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // DTP01_BSJYYMM
            // 
            this.DTP01_BSJYYMM.FactoryID = "";
            this.DTP01_BSJYYMM.FactoryName = null;
            this.DTP01_BSJYYMM.Location = new System.Drawing.Point(249, 50);
            this.DTP01_BSJYYMM.Name = "DTP01_BSJYYMM";
            this.DTP01_BSJYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_BSJYYMM.TabIndex = 2;
            // 
            // LBL51_BSJYYMM
            // 
            this.LBL51_BSJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BSJYYMM.FactoryID = "";
            this.LBL51_BSJYYMM.FactoryName = null;
            this.LBL51_BSJYYMM.IsCreated = false;
            this.LBL51_BSJYYMM.Location = new System.Drawing.Point(143, 50);
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
            this.LBL51_GOKCR.Location = new System.Drawing.Point(143, 194);
            this.LBL51_GOKCR.Name = "LBL51_GOKCR";
            this.LBL51_GOKCR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GOKCR.TabIndex = 5;
            this.LBL51_GOKCR.Text = "생성구분";
            this.LBL51_GOKCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.label3);
            this.GBX80_CONTROLS.Controls.Add(this.label2);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BIJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BIJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GOKCR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_BSJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BSJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GOKCR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(575, 285);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(143, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "※ 실적적용년월 선택 차이";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(161, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "- 입력시:  실적적용년월 기준의 실적/계획 금액이 유지  ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(156, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = " - 미입력시: 실적생성년월 기준의 실적/계획으로 신규 생성";
            // 
            // CBH01_BIJYYMM
            // 
            this.CBH01_BIJYYMM.BindedDataRow = null;
            this.CBH01_BIJYYMM.CodeBoxWidth = 0;
            this.CBH01_BIJYYMM.DummyValue = null;
            this.CBH01_BIJYYMM.FactoryID = "";
            this.CBH01_BIJYYMM.FactoryName = null;
            this.CBH01_BIJYYMM.Location = new System.Drawing.Point(249, 77);
            this.CBH01_BIJYYMM.MinLength = 0;
            this.CBH01_BIJYYMM.Name = "CBH01_BIJYYMM";
            this.CBH01_BIJYYMM.Size = new System.Drawing.Size(75, 20);
            this.CBH01_BIJYYMM.TabIndex = 12;
            // 
            // LBL51_BIJYYMM
            // 
            this.LBL51_BIJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BIJYYMM.FactoryID = "";
            this.LBL51_BIJYYMM.FactoryName = null;
            this.LBL51_BIJYYMM.IsCreated = false;
            this.LBL51_BIJYYMM.Location = new System.Drawing.Point(143, 76);
            this.LBL51_BIJYYMM.Name = "LBL51_BIJYYMM";
            this.LBL51_BIJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BIJYYMM.TabIndex = 7;
            this.LBL51_BIJYYMM.Text = "실적적용년월";
            this.LBL51_BIJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GOKCR
            // 
            this.CBO01_GOKCR.FactoryID = "";
            this.CBO01_GOKCR.FactoryName = null;
            this.CBO01_GOKCR.Location = new System.Drawing.Point(249, 194);
            this.CBO01_GOKCR.Name = "CBO01_GOKCR";
            this.CBO01_GOKCR.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GOKCR.TabIndex = 6;
            // 
            // TYBSSJ001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 289);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYBSSJ001B";
            this.Load += new System.EventHandler(this.TYBSSJ001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYDatePicker DTP01_BSJYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_BSJYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GOKCR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYComboBox CBO01_GOKCR;
        private Service.Library.Controls.TYLabel LBL51_BIJYYMM;
        private Service.Library.Controls.TYCodeBox CBH01_BIJYYMM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}