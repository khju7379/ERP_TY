namespace TY.ER.HR00
{
    partial class TYHRGT008S
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
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBH01_KBSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBSABUN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GHCODE = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GTGUBN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GTGUBN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_S1BRANCH = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_S1BRANCH = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_4CHFW855 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_4CHFW855_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_GHCODE = new TY.Service.Library.Controls.TYCheckComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CHFW855)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CHFW855_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
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
            this.CBH01_KBSABUN.Location = new System.Drawing.Point(661, 12);
            this.CBH01_KBSABUN.MinLength = 0;
            this.CBH01_KBSABUN.Name = "CBH01_KBSABUN";
            this.CBH01_KBSABUN.Size = new System.Drawing.Size(150, 20);
            this.CBH01_KBSABUN.TabIndex = 1;
            // 
            // LBL51_KBSABUN
            // 
            this.LBL51_KBSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBSABUN.FactoryID = "";
            this.LBL51_KBSABUN.FactoryName = null;
            this.LBL51_KBSABUN.IsCreated = false;
            this.LBL51_KBSABUN.Location = new System.Drawing.Point(555, 12);
            this.LBL51_KBSABUN.Name = "LBL51_KBSABUN";
            this.LBL51_KBSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBSABUN.TabIndex = 2;
            this.LBL51_KBSABUN.Text = "사번";
            this.LBL51_KBSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GHCODE
            // 
            this.LBL51_GHCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GHCODE.FactoryID = "";
            this.LBL51_GHCODE.FactoryName = null;
            this.LBL51_GHCODE.IsCreated = false;
            this.LBL51_GHCODE.Location = new System.Drawing.Point(343, 39);
            this.LBL51_GHCODE.Name = "LBL51_GHCODE";
            this.LBL51_GHCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GHCODE.TabIndex = 4;
            this.LBL51_GHCODE.Text = "휴무코드";
            this.LBL51_GHCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GTGUBN
            // 
            this.CBO01_GTGUBN.FactoryID = "";
            this.CBO01_GTGUBN.FactoryName = null;
            this.CBO01_GTGUBN.Location = new System.Drawing.Point(111, 39);
            this.CBO01_GTGUBN.Name = "CBO01_GTGUBN";
            this.CBO01_GTGUBN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GTGUBN.TabIndex = 5;
            // 
            // LBL51_GTGUBN
            // 
            this.LBL51_GTGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GTGUBN.FactoryID = "";
            this.LBL51_GTGUBN.FactoryName = null;
            this.LBL51_GTGUBN.IsCreated = false;
            this.LBL51_GTGUBN.Location = new System.Drawing.Point(5, 39);
            this.LBL51_GTGUBN.Name = "LBL51_GTGUBN";
            this.LBL51_GTGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GTGUBN.TabIndex = 6;
            this.LBL51_GTGUBN.Text = "근태구분";
            this.LBL51_GTGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_S1BRANCH
            // 
            this.CBO01_S1BRANCH.FactoryID = "";
            this.CBO01_S1BRANCH.FactoryName = null;
            this.CBO01_S1BRANCH.Location = new System.Drawing.Point(449, 12);
            this.CBO01_S1BRANCH.Name = "CBO01_S1BRANCH";
            this.CBO01_S1BRANCH.Size = new System.Drawing.Size(100, 20);
            this.CBO01_S1BRANCH.TabIndex = 7;
            // 
            // LBL51_S1BRANCH
            // 
            this.LBL51_S1BRANCH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_S1BRANCH.FactoryID = "";
            this.LBL51_S1BRANCH.FactoryName = null;
            this.LBL51_S1BRANCH.IsCreated = false;
            this.LBL51_S1BRANCH.Location = new System.Drawing.Point(343, 12);
            this.LBL51_S1BRANCH.Name = "LBL51_S1BRANCH";
            this.LBL51_S1BRANCH.Size = new System.Drawing.Size(100, 21);
            this.LBL51_S1BRANCH.TabIndex = 8;
            this.LBL51_S1BRANCH.Text = "구분";
            this.LBL51_S1BRANCH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(237, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 9;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 11;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 12;
            this.LBL51_STDATE.Text = "근태일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_4CHFW855
            // 
            this.FPS91_TY_S_HR_4CHFW855.AccessibleDescription = "";
            this.FPS91_TY_S_HR_4CHFW855.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_4CHFW855.FactoryID = "";
            this.FPS91_TY_S_HR_4CHFW855.FactoryName = null;
            this.FPS91_TY_S_HR_4CHFW855.Location = new System.Drawing.Point(1, 72);
            this.FPS91_TY_S_HR_4CHFW855.Name = "FPS91_TY_S_HR_4CHFW855";
            this.FPS91_TY_S_HR_4CHFW855.PopMenuVisible = false;
            this.FPS91_TY_S_HR_4CHFW855.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_4CHFW855.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_4CHFW855_Sheet1});
            this.FPS91_TY_S_HR_4CHFW855.Size = new System.Drawing.Size(1174, 676);
            this.FPS91_TY_S_HR_4CHFW855.TabIndex = 13;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_4CHFW855.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_4CHFW855.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_4CHFW855_CellDoubleClick);
            // 
            // FPS91_TY_S_HR_4CHFW855_Sheet1
            // 
            this.FPS91_TY_S_HR_4CHFW855_Sheet1.Reset();
            this.FPS91_TY_S_HR_4CHFW855_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_4CHFW855_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_4CHFW855_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_4CHFW855_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GHCODE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GHCODE);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GTGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GTGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_S1BRANCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_S1BRANCH);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_4CHFW855);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 748);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_GHCODE
            // 
            this.CBO01_GHCODE.FactoryID = "";
            this.CBO01_GHCODE.FactoryName = null;
            this.CBO01_GHCODE.FormattingEnabled = true;
            this.CBO01_GHCODE.IntegralHeight = false;
            this.CBO01_GHCODE.Location = new System.Drawing.Point(449, 40);
            this.CBO01_GHCODE.Name = "CBO01_GHCODE";
            this.CBO01_GHCODE.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GHCODE.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "~";
            // 
            // TYHRGT008S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 750);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGT008S";
            this.Load += new System.EventHandler(this.TYHRGT008S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CHFW855)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CHFW855_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYCodeBox CBH01_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_GHCODE;
        private TY.Service.Library.Controls.TYComboBox CBO01_GTGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_GTGUBN;
        private TY.Service.Library.Controls.TYComboBox CBO01_S1BRANCH;
        private TY.Service.Library.Controls.TYLabel LBL51_S1BRANCH;
        private TY.Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_4CHFW855;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_4CHFW855_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCheckComboBox CBO01_GHCODE;
    }
}