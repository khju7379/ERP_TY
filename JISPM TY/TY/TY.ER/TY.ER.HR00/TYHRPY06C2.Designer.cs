namespace TY.ER.HR00
{
    partial class TYHRPY06C2
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.CBH01_PAYGUBN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_PAYGUBN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_PAYJIDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_PAYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_4CMBP922 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_4CMBP922_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_PAYJIDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_PAYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CMBP922)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CMBP922_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(552, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(471, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 1;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(390, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 2;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBH01_PAYGUBN
            // 
            this.CBH01_PAYGUBN.BindedDataRow = null;
            this.CBH01_PAYGUBN.CodeBoxWidth = 0;
            this.CBH01_PAYGUBN.DummyValue = null;
            this.CBH01_PAYGUBN.FactoryID = "";
            this.CBH01_PAYGUBN.FactoryName = null;
            this.CBH01_PAYGUBN.Location = new System.Drawing.Point(111, 12);
            this.CBH01_PAYGUBN.MinLength = 0;
            this.CBH01_PAYGUBN.Name = "CBH01_PAYGUBN";
            this.CBH01_PAYGUBN.Size = new System.Drawing.Size(130, 20);
            this.CBH01_PAYGUBN.TabIndex = 3;
            // 
            // LBL51_PAYGUBN
            // 
            this.LBL51_PAYGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYGUBN.FactoryID = "";
            this.LBL51_PAYGUBN.FactoryName = null;
            this.LBL51_PAYGUBN.IsCreated = false;
            this.LBL51_PAYGUBN.Location = new System.Drawing.Point(5, 12);
            this.LBL51_PAYGUBN.Name = "LBL51_PAYGUBN";
            this.LBL51_PAYGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYGUBN.TabIndex = 4;
            this.LBL51_PAYGUBN.Text = "급여구분";
            this.LBL51_PAYGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_PAYJIDATE
            // 
            this.LBL51_PAYJIDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYJIDATE.FactoryID = "";
            this.LBL51_PAYJIDATE.FactoryName = null;
            this.LBL51_PAYJIDATE.IsCreated = false;
            this.LBL51_PAYJIDATE.Location = new System.Drawing.Point(204, 39);
            this.LBL51_PAYJIDATE.Name = "LBL51_PAYJIDATE";
            this.LBL51_PAYJIDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYJIDATE.TabIndex = 6;
            this.LBL51_PAYJIDATE.Text = "지급일자";
            this.LBL51_PAYJIDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_PAYYYMM
            // 
            this.LBL51_PAYYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYYYMM.FactoryID = "";
            this.LBL51_PAYYYMM.FactoryName = null;
            this.LBL51_PAYYYMM.IsCreated = false;
            this.LBL51_PAYYYMM.Location = new System.Drawing.Point(5, 39);
            this.LBL51_PAYYYMM.Name = "LBL51_PAYYYMM";
            this.LBL51_PAYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYYYMM.TabIndex = 8;
            this.LBL51_PAYYYMM.Text = "급여년월";
            this.LBL51_PAYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_4CMBP922
            // 
            this.FPS91_TY_S_HR_4CMBP922.AccessibleDescription = "";
            this.FPS91_TY_S_HR_4CMBP922.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_4CMBP922.FactoryID = "";
            this.FPS91_TY_S_HR_4CMBP922.FactoryName = null;
            this.FPS91_TY_S_HR_4CMBP922.Location = new System.Drawing.Point(1, 72);
            this.FPS91_TY_S_HR_4CMBP922.Name = "FPS91_TY_S_HR_4CMBP922";
            this.FPS91_TY_S_HR_4CMBP922.PopMenuVisible = false;
            this.FPS91_TY_S_HR_4CMBP922.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_4CMBP922.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_4CMBP922_Sheet1});
            this.FPS91_TY_S_HR_4CMBP922.Size = new System.Drawing.Size(634, 472);
            this.FPS91_TY_S_HR_4CMBP922.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_4CMBP922.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_4CMBP922.RowInserted += new Shoveling2010.SmartClient.SystemUtility.Controls.TSpread.TRowEventHandler(this.FPS91_TY_S_HR_4CMBP922_RowInserted);
            // 
            // FPS91_TY_S_HR_4CMBP922_Sheet1
            // 
            this.FPS91_TY_S_HR_4CMBP922_Sheet1.Reset();
            this.FPS91_TY_S_HR_4CMBP922_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_4CMBP922_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_4CMBP922_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_4CMBP922_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PAYJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PAYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_4CMBP922);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(635, 517);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_PAYJIDATE
            // 
            this.DTP01_PAYJIDATE.FactoryID = "";
            this.DTP01_PAYJIDATE.FactoryName = null;
            this.DTP01_PAYJIDATE.Location = new System.Drawing.Point(310, 39);
            this.DTP01_PAYJIDATE.Name = "DTP01_PAYJIDATE";
            this.DTP01_PAYJIDATE.Size = new System.Drawing.Size(113, 21);
            this.DTP01_PAYJIDATE.TabIndex = 10;
            // 
            // DTP01_PAYYYMM
            // 
            this.DTP01_PAYYYMM.FactoryID = "";
            this.DTP01_PAYYYMM.FactoryName = null;
            this.DTP01_PAYYYMM.Location = new System.Drawing.Point(111, 39);
            this.DTP01_PAYYYMM.Name = "DTP01_PAYYYMM";
            this.DTP01_PAYYYMM.Size = new System.Drawing.Size(87, 21);
            this.DTP01_PAYYYMM.TabIndex = 11;
            // 
            // TYHRPY06C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 522);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY06C2";
            this.Load += new System.EventHandler(this.TYHRPY06C2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CMBP922)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CMBP922_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYCodeBox CBH01_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYJIDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_4CMBP922;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_4CMBP922_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_PAYJIDATE;
        private Service.Library.Controls.TYDatePicker DTP01_PAYYYMM;
    }
}