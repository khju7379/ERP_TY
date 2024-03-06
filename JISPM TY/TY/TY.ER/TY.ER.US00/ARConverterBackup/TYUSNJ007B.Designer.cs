namespace TY.ER.US00
{
    partial class TYUSNJ007B
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.MTB01_GDATE = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.CBH01_EDHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LBL51_STHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_STHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(343, 90);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 1;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_GDATE
            // 
            this.LBL51_GDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GDATE.FactoryID = "";
            this.LBL51_GDATE.FactoryName = null;
            this.LBL51_GDATE.IsCreated = false;
            this.LBL51_GDATE.Location = new System.Drawing.Point(24, 56);
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
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GDATE);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_EDHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_STHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(804, 122);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(424, 90);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 368;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // MTB01_GDATE
            // 
            this.MTB01_GDATE.FactoryID = "";
            this.MTB01_GDATE.FactoryName = null;
            this.MTB01_GDATE.Location = new System.Drawing.Point(130, 56);
            this.MTB01_GDATE.Mask = "0000-00-00";
            this.MTB01_GDATE.Name = "MTB01_GDATE";
            this.MTB01_GDATE.Size = new System.Drawing.Size(74, 21);
            this.MTB01_GDATE.TabIndex = 363;
            this.MTB01_GDATE.ValidatingType = typeof(System.DateTime);
            // 
            // CBH01_EDHANGCHA
            // 
            this.CBH01_EDHANGCHA.BindedDataRow = null;
            this.CBH01_EDHANGCHA.CodeBoxWidth = 0;
            this.CBH01_EDHANGCHA.DummyValue = null;
            this.CBH01_EDHANGCHA.FactoryID = "";
            this.CBH01_EDHANGCHA.FactoryName = null;
            this.CBH01_EDHANGCHA.Location = new System.Drawing.Point(464, 30);
            this.CBH01_EDHANGCHA.MinLength = 0;
            this.CBH01_EDHANGCHA.Name = "CBH01_EDHANGCHA";
            this.CBH01_EDHANGCHA.Size = new System.Drawing.Size(311, 20);
            this.CBH01_EDHANGCHA.TabIndex = 338;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(447, 34);
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
            this.LBL51_STHANGCHA.Location = new System.Drawing.Point(24, 30);
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
            this.CBH01_STHANGCHA.Location = new System.Drawing.Point(130, 30);
            this.CBH01_STHANGCHA.MinLength = 0;
            this.CBH01_STHANGCHA.Name = "CBH01_STHANGCHA";
            this.CBH01_STHANGCHA.Size = new System.Drawing.Size(311, 20);
            this.CBH01_STHANGCHA.TabIndex = 335;
            // 
            // TYUSNJ007B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 124);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSNJ007B";
            this.Load += new System.EventHandler(this.TYUSNJ007B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYLabel LBL51_GDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_EDHANGCHA;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYLabel LBL51_STHANGCHA;
        private Service.Library.Controls.TYCodeBox CBH01_STHANGCHA;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GDATE;
        private Service.Library.Controls.TYButton BTN61_CLO;
    }
}