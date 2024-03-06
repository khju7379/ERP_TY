namespace TY.ER.HR00
{
    partial class TYHRGT003S
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
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.CBH01_GHCODE = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GHCODE = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_GHSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GHSABUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_KBBUSEO = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_4BQFC544 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_4BQFC544_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_NEW = new TY.Service.Library.Controls.TYButton();
            this.CBH01_KBBUSEO = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN62_NEW = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4BQFC544)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4BQFC544_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(932, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 1;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // CBH01_GHCODE
            // 
            this.CBH01_GHCODE.BindedDataRow = null;
            this.CBH01_GHCODE.CodeBoxWidth = 0;
            this.CBH01_GHCODE.DummyValue = null;
            this.CBH01_GHCODE.FactoryID = "";
            this.CBH01_GHCODE.FactoryName = null;
            this.CBH01_GHCODE.Location = new System.Drawing.Point(449, 12);
            this.CBH01_GHCODE.MinLength = 0;
            this.CBH01_GHCODE.Name = "CBH01_GHCODE";
            this.CBH01_GHCODE.Size = new System.Drawing.Size(142, 20);
            this.CBH01_GHCODE.TabIndex = 2;
            // 
            // LBL51_GHCODE
            // 
            this.LBL51_GHCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GHCODE.FactoryID = "";
            this.LBL51_GHCODE.FactoryName = null;
            this.LBL51_GHCODE.IsCreated = false;
            this.LBL51_GHCODE.Location = new System.Drawing.Point(343, 12);
            this.LBL51_GHCODE.Name = "LBL51_GHCODE";
            this.LBL51_GHCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GHCODE.TabIndex = 3;
            this.LBL51_GHCODE.Text = "휴무코드";
            this.LBL51_GHCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_GHSABUN
            // 
            this.CBH01_GHSABUN.BindedDataRow = null;
            this.CBH01_GHSABUN.CodeBoxWidth = 0;
            this.CBH01_GHSABUN.DummyValue = null;
            this.CBH01_GHSABUN.FactoryID = "";
            this.CBH01_GHSABUN.FactoryName = null;
            this.CBH01_GHSABUN.Location = new System.Drawing.Point(703, 12);
            this.CBH01_GHSABUN.MinLength = 0;
            this.CBH01_GHSABUN.Name = "CBH01_GHSABUN";
            this.CBH01_GHSABUN.Size = new System.Drawing.Size(155, 20);
            this.CBH01_GHSABUN.TabIndex = 4;
            // 
            // LBL51_GHSABUN
            // 
            this.LBL51_GHSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GHSABUN.FactoryID = "";
            this.LBL51_GHSABUN.FactoryName = null;
            this.LBL51_GHSABUN.IsCreated = false;
            this.LBL51_GHSABUN.Location = new System.Drawing.Point(597, 12);
            this.LBL51_GHSABUN.Name = "LBL51_GHSABUN";
            this.LBL51_GHSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GHSABUN.TabIndex = 5;
            this.LBL51_GHSABUN.Text = "사   번";
            this.LBL51_GHSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(237, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 6;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 8;
            this.DTP01_SDATE.ValueChanged += new System.EventHandler(this.DTP01_SDATE_ValueChanged);
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 9;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_KBBUSEO
            // 
            this.LBL51_KBBUSEO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBBUSEO.FactoryID = "";
            this.LBL51_KBBUSEO.FactoryName = null;
            this.LBL51_KBBUSEO.IsCreated = false;
            this.LBL51_KBBUSEO.Location = new System.Drawing.Point(5, 39);
            this.LBL51_KBBUSEO.Name = "LBL51_KBBUSEO";
            this.LBL51_KBBUSEO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBBUSEO.TabIndex = 11;
            this.LBL51_KBBUSEO.Text = "부서";
            this.LBL51_KBBUSEO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_4BQFC544
            // 
            this.FPS91_TY_S_HR_4BQFC544.AccessibleDescription = "";
            this.FPS91_TY_S_HR_4BQFC544.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_4BQFC544.FactoryID = "";
            this.FPS91_TY_S_HR_4BQFC544.FactoryName = null;
            this.FPS91_TY_S_HR_4BQFC544.Location = new System.Drawing.Point(1, 75);
            this.FPS91_TY_S_HR_4BQFC544.Name = "FPS91_TY_S_HR_4BQFC544";
            this.FPS91_TY_S_HR_4BQFC544.PopMenuVisible = false;
            this.FPS91_TY_S_HR_4BQFC544.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_4BQFC544.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_4BQFC544_Sheet1});
            this.FPS91_TY_S_HR_4BQFC544.Size = new System.Drawing.Size(1174, 781);
            this.FPS91_TY_S_HR_4BQFC544.TabIndex = 12;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_4BQFC544.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_4BQFC544.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_4BQFC544_CellDoubleClick);
            // 
            // FPS91_TY_S_HR_4BQFC544_Sheet1
            // 
            this.FPS91_TY_S_HR_4BQFC544_Sheet1.Reset();
            this.FPS91_TY_S_HR_4BQFC544_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_4BQFC544_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_4BQFC544_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_4BQFC544_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN62_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBBUSEO);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_GHCODE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GHCODE);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_GHSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GHSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBBUSEO);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_4BQFC544);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 15;
            this.BTN61_NEW.Text = " 신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // CBH01_KBBUSEO
            // 
            this.CBH01_KBBUSEO.BindedDataRow = null;
            this.CBH01_KBBUSEO.CodeBoxWidth = 0;
            this.CBH01_KBBUSEO.DummyValue = null;
            this.CBH01_KBBUSEO.FactoryID = "";
            this.CBH01_KBBUSEO.FactoryName = null;
            this.CBH01_KBBUSEO.Location = new System.Drawing.Point(111, 39);
            this.CBH01_KBBUSEO.MinLength = 0;
            this.CBH01_KBBUSEO.Name = "CBH01_KBBUSEO";
            this.CBH01_KBBUSEO.Size = new System.Drawing.Size(217, 20);
            this.CBH01_KBBUSEO.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "~";
            // 
            // BTN62_NEW
            // 
            this.BTN62_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN62_NEW.FactoryID = "";
            this.BTN62_NEW.FactoryName = null;
            this.BTN62_NEW.Location = new System.Drawing.Point(1013, 38);
            this.BTN62_NEW.Name = "BTN62_NEW";
            this.BTN62_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN62_NEW.TabIndex = 16;
            this.BTN62_NEW.Text = "일괄등록";
            this.BTN62_NEW.UseVisualStyleBackColor = true;
            this.BTN62_NEW.Click += new System.EventHandler(this.BTN62_NEW_Click);
            // 
            // TYHRGT003S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGT003S";
            this.Load += new System.EventHandler(this.TYHRGT003S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4BQFC544)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4BQFC544_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYCodeBox CBH01_GHCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_GHCODE;
        private TY.Service.Library.Controls.TYCodeBox CBH01_GHSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_GHSABUN;
        private TY.Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_KBBUSEO;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_4BQFC544;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_4BQFC544_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCodeBox CBH01_KBBUSEO;
        private Service.Library.Controls.TYButton BTN61_NEW;
        private Service.Library.Controls.TYButton BTN62_NEW;
    }
}