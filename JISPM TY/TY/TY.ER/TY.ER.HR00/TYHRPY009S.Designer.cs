namespace TY.ER.HR00
{
    partial class TYHRPY009S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBH01_KBSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBSABUN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_KBCODE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_54DDN173 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_54DDN173_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_KBJKCD = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBJKCD = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_KBCODE = new TY.Service.Library.Controls.TYComboBox();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_54DDN173)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_54DDN173_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(1113, 12);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1032, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // CBH01_KBSABUN
            // 
            this.CBH01_KBSABUN.BindedDataRow = null;
            this.CBH01_KBSABUN.CodeBoxWidth = 0;
            this.CBH01_KBSABUN.DummyValue = null;
            this.CBH01_KBSABUN.FactoryID = "";
            this.CBH01_KBSABUN.FactoryName = null;
            this.CBH01_KBSABUN.Location = new System.Drawing.Point(597, 12);
            this.CBH01_KBSABUN.MinLength = 0;
            this.CBH01_KBSABUN.Name = "CBH01_KBSABUN";
            this.CBH01_KBSABUN.Size = new System.Drawing.Size(162, 20);
            this.CBH01_KBSABUN.TabIndex = 2;
            // 
            // LBL51_KBSABUN
            // 
            this.LBL51_KBSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBSABUN.FactoryID = "";
            this.LBL51_KBSABUN.FactoryName = null;
            this.LBL51_KBSABUN.IsCreated = false;
            this.LBL51_KBSABUN.Location = new System.Drawing.Point(491, 12);
            this.LBL51_KBSABUN.Name = "LBL51_KBSABUN";
            this.LBL51_KBSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBSABUN.TabIndex = 3;
            this.LBL51_KBSABUN.Text = "사번";
            this.LBL51_KBSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_KBCODE
            // 
            this.LBL51_KBCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBCODE.FactoryID = "";
            this.LBL51_KBCODE.FactoryName = null;
            this.LBL51_KBCODE.IsCreated = false;
            this.LBL51_KBCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_KBCODE.Name = "LBL51_KBCODE";
            this.LBL51_KBCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBCODE.TabIndex = 5;
            this.LBL51_KBCODE.Text = "조회구분";
            this.LBL51_KBCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_54DDN173
            // 
            this.FPS91_TY_S_HR_54DDN173.AccessibleDescription = "";
            this.FPS91_TY_S_HR_54DDN173.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_54DDN173.FactoryID = "";
            this.FPS91_TY_S_HR_54DDN173.FactoryName = null;
            this.FPS91_TY_S_HR_54DDN173.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_54DDN173.Name = "FPS91_TY_S_HR_54DDN173";
            this.FPS91_TY_S_HR_54DDN173.PopMenuVisible = false;
            this.FPS91_TY_S_HR_54DDN173.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_54DDN173.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_54DDN173_Sheet1});
            this.FPS91_TY_S_HR_54DDN173.Size = new System.Drawing.Size(1273, 810);
            this.FPS91_TY_S_HR_54DDN173.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_54DDN173.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_HR_54DDN173_Sheet1
            // 
            this.FPS91_TY_S_HR_54DDN173_Sheet1.Reset();
            this.FPS91_TY_S_HR_54DDN173_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_54DDN173_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_54DDN173_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_54DDN173_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBJKCD);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBJKCD);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_KBCODE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBCODE);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_54DDN173);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1275, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_KBJKCD
            // 
            this.CBH01_KBJKCD.BindedDataRow = null;
            this.CBH01_KBJKCD.CodeBoxWidth = 0;
            this.CBH01_KBJKCD.DummyValue = null;
            this.CBH01_KBJKCD.FactoryID = "";
            this.CBH01_KBJKCD.FactoryName = null;
            this.CBH01_KBJKCD.Location = new System.Drawing.Point(323, 12);
            this.CBH01_KBJKCD.MinLength = 0;
            this.CBH01_KBJKCD.Name = "CBH01_KBJKCD";
            this.CBH01_KBJKCD.Size = new System.Drawing.Size(162, 20);
            this.CBH01_KBJKCD.TabIndex = 19;
            // 
            // LBL51_KBJKCD
            // 
            this.LBL51_KBJKCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBJKCD.FactoryID = "";
            this.LBL51_KBJKCD.FactoryName = null;
            this.LBL51_KBJKCD.IsCreated = false;
            this.LBL51_KBJKCD.Location = new System.Drawing.Point(217, 12);
            this.LBL51_KBJKCD.Name = "LBL51_KBJKCD";
            this.LBL51_KBJKCD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBJKCD.TabIndex = 20;
            this.LBL51_KBJKCD.Text = "직급";
            this.LBL51_KBJKCD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_KBCODE
            // 
            this.CBO01_KBCODE.FactoryID = "";
            this.CBO01_KBCODE.FactoryName = null;
            this.CBO01_KBCODE.Location = new System.Drawing.Point(111, 12);
            this.CBO01_KBCODE.Name = "CBO01_KBCODE";
            this.CBO01_KBCODE.Size = new System.Drawing.Size(100, 20);
            this.CBO01_KBCODE.TabIndex = 18;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1194, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 21;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TYHRPY009S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY009S";
            this.Load += new System.EventHandler(this.TYHRPY009S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_54DDN173)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_54DDN173_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYCodeBox CBH01_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_KBCODE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_54DDN173;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_54DDN173_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYComboBox CBO01_KBCODE;
        private Service.Library.Controls.TYCodeBox CBH01_KBJKCD;
        private Service.Library.Controls.TYLabel LBL51_KBJKCD;
        private Service.Library.Controls.TYButton BTN61_CLO;
    }
}