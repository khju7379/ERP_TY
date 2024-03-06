namespace TY.ER.US00
{
    partial class TYUSME015B
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
            this.LBL51_GDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.MTB01_GDATE = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.CBO01_GMEGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GMEGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_EDHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LBL51_STHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_STHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_GHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_GGOKJONG = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GGOKJONG = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(452, 489);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 1;
            this.BTN61_BATCH.Text = "전표 처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_GDATE
            // 
            this.LBL51_GDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GDATE.FactoryID = "";
            this.LBL51_GDATE.FactoryName = null;
            this.LBL51_GDATE.IsCreated = false;
            this.LBL51_GDATE.Location = new System.Drawing.Point(346, 321);
            this.LBL51_GDATE.Name = "LBL51_GDATE";
            this.LBL51_GDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GDATE.TabIndex = 4;
            this.LBL51_GDATE.Text = "매출일자";
            this.LBL51_GDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GDATE);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GMEGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GMEGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_EDHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_STHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_GHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_GGOKJONG);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGOKJONG);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // MTB01_GDATE
            // 
            this.MTB01_GDATE.FactoryID = "";
            this.MTB01_GDATE.FactoryName = null;
            this.MTB01_GDATE.Location = new System.Drawing.Point(452, 321);
            this.MTB01_GDATE.Mask = "0000-00-00";
            this.MTB01_GDATE.Name = "MTB01_GDATE";
            this.MTB01_GDATE.Size = new System.Drawing.Size(74, 21);
            this.MTB01_GDATE.TabIndex = 363;
            this.MTB01_GDATE.ValidatingType = typeof(System.DateTime);
            // 
            // CBO01_GMEGUBUN
            // 
            this.CBO01_GMEGUBUN.FactoryID = "";
            this.CBO01_GMEGUBUN.FactoryName = null;
            this.CBO01_GMEGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GMEGUBUN.Location = new System.Drawing.Point(452, 453);
            this.CBO01_GMEGUBUN.Name = "CBO01_GMEGUBUN";
            this.CBO01_GMEGUBUN.Size = new System.Drawing.Size(150, 20);
            this.CBO01_GMEGUBUN.TabIndex = 340;
            // 
            // LBL51_GMEGUBUN
            // 
            this.LBL51_GMEGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GMEGUBUN.FactoryID = "";
            this.LBL51_GMEGUBUN.FactoryName = null;
            this.LBL51_GMEGUBUN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_GMEGUBUN.IsCreated = false;
            this.LBL51_GMEGUBUN.Location = new System.Drawing.Point(346, 453);
            this.LBL51_GMEGUBUN.Name = "LBL51_GMEGUBUN";
            this.LBL51_GMEGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GMEGUBUN.TabIndex = 339;
            this.LBL51_GMEGUBUN.Text = "매출 구분";
            this.LBL51_GMEGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_EDHANGCHA
            // 
            this.CBH01_EDHANGCHA.BindedDataRow = null;
            this.CBH01_EDHANGCHA.CodeBoxWidth = 0;
            this.CBH01_EDHANGCHA.DummyValue = null;
            this.CBH01_EDHANGCHA.FactoryID = "";
            this.CBH01_EDHANGCHA.FactoryName = null;
            this.CBH01_EDHANGCHA.Location = new System.Drawing.Point(786, 348);
            this.CBH01_EDHANGCHA.MinLength = 0;
            this.CBH01_EDHANGCHA.Name = "CBH01_EDHANGCHA";
            this.CBH01_EDHANGCHA.Size = new System.Drawing.Size(311, 20);
            this.CBH01_EDHANGCHA.TabIndex = 338;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(769, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 337;
            this.label1.Text = "-";
            // 
            // LBL51_STHANGCHA
            // 
            this.LBL51_STHANGCHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STHANGCHA.FactoryID = "";
            this.LBL51_STHANGCHA.FactoryName = null;
            this.LBL51_STHANGCHA.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_STHANGCHA.ForeColor = System.Drawing.Color.Black;
            this.LBL51_STHANGCHA.IsCreated = false;
            this.LBL51_STHANGCHA.Location = new System.Drawing.Point(346, 348);
            this.LBL51_STHANGCHA.Name = "LBL51_STHANGCHA";
            this.LBL51_STHANGCHA.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STHANGCHA.TabIndex = 336;
            this.LBL51_STHANGCHA.Text = "항 차";
            this.LBL51_STHANGCHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_STHANGCHA
            // 
            this.CBH01_STHANGCHA.BindedDataRow = null;
            this.CBH01_STHANGCHA.CodeBoxWidth = 0;
            this.CBH01_STHANGCHA.DummyValue = null;
            this.CBH01_STHANGCHA.FactoryID = "";
            this.CBH01_STHANGCHA.FactoryName = null;
            this.CBH01_STHANGCHA.Location = new System.Drawing.Point(452, 348);
            this.CBH01_STHANGCHA.MinLength = 0;
            this.CBH01_STHANGCHA.Name = "CBH01_STHANGCHA";
            this.CBH01_STHANGCHA.Size = new System.Drawing.Size(311, 20);
            this.CBH01_STHANGCHA.TabIndex = 335;
            this.CBH01_STHANGCHA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBH01_STHANGCHA_KeyDown);
            // 
            // CBH01_GHWAJU
            // 
            this.CBH01_GHWAJU.BindedDataRow = null;
            this.CBH01_GHWAJU.CodeBoxWidth = 0;
            this.CBH01_GHWAJU.DummyValue = null;
            this.CBH01_GHWAJU.FactoryID = "";
            this.CBH01_GHWAJU.FactoryName = null;
            this.CBH01_GHWAJU.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_GHWAJU.Location = new System.Drawing.Point(452, 401);
            this.CBH01_GHWAJU.MinLength = 0;
            this.CBH01_GHWAJU.Name = "CBH01_GHWAJU";
            this.CBH01_GHWAJU.Size = new System.Drawing.Size(311, 20);
            this.CBH01_GHWAJU.TabIndex = 330;
            // 
            // LBL51_GHWAJU
            // 
            this.LBL51_GHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GHWAJU.FactoryID = "";
            this.LBL51_GHWAJU.FactoryName = null;
            this.LBL51_GHWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_GHWAJU.ForeColor = System.Drawing.Color.Black;
            this.LBL51_GHWAJU.IsCreated = false;
            this.LBL51_GHWAJU.Location = new System.Drawing.Point(346, 401);
            this.LBL51_GHWAJU.Name = "LBL51_GHWAJU";
            this.LBL51_GHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GHWAJU.TabIndex = 327;
            this.LBL51_GHWAJU.Text = "화 주";
            this.LBL51_GHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_GGOKJONG
            // 
            this.CBH01_GGOKJONG.BindedDataRow = null;
            this.CBH01_GGOKJONG.CodeBoxWidth = 0;
            this.CBH01_GGOKJONG.DummyValue = null;
            this.CBH01_GGOKJONG.FactoryID = "";
            this.CBH01_GGOKJONG.FactoryName = null;
            this.CBH01_GGOKJONG.Location = new System.Drawing.Point(452, 375);
            this.CBH01_GGOKJONG.MinLength = 0;
            this.CBH01_GGOKJONG.Name = "CBH01_GGOKJONG";
            this.CBH01_GGOKJONG.Size = new System.Drawing.Size(311, 20);
            this.CBH01_GGOKJONG.TabIndex = 316;
            // 
            // LBL51_GGOKJONG
            // 
            this.LBL51_GGOKJONG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGOKJONG.FactoryID = "";
            this.LBL51_GGOKJONG.FactoryName = null;
            this.LBL51_GGOKJONG.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_GGOKJONG.ForeColor = System.Drawing.Color.Black;
            this.LBL51_GGOKJONG.IsCreated = false;
            this.LBL51_GGOKJONG.Location = new System.Drawing.Point(346, 375);
            this.LBL51_GGOKJONG.Name = "LBL51_GGOKJONG";
            this.LBL51_GGOKJONG.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGOKJONG.TabIndex = 315;
            this.LBL51_GGOKJONG.Text = "곡 종";
            this.LBL51_GGOKJONG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(452, 427);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GGUBUN.TabIndex = 311;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(346, 427);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 310;
            this.LBL51_GGUBUN.Text = "생성 구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUSME015B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSME015B";
            this.Load += new System.EventHandler(this.TYUSME015B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYLabel LBL51_GDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYCodeBox CBH01_GGOKJONG;
        private Service.Library.Controls.TYLabel LBL51_GGOKJONG;
        private Service.Library.Controls.TYLabel LBL51_GHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_GHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_EDHANGCHA;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYLabel LBL51_STHANGCHA;
        private Service.Library.Controls.TYCodeBox CBH01_STHANGCHA;
        private Service.Library.Controls.TYComboBox CBO01_GMEGUBUN;
        private Service.Library.Controls.TYLabel LBL51_GMEGUBUN;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GDATE;
    }
}