namespace TY.ER.HR00
{
    partial class TYHRCS002I
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBO01_KBGUNMU = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_KBGUNMU = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_616BU409 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_P_HR_616BB408_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS1 = new System.Windows.Forms.GroupBox();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ_FXM = new TY.Service.Library.Controls.TYButton();
            this.LBL51_FOGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_FOGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_FODATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_FODATE = new TY.Service.Library.Controls.TYDatePicker();
            this.FPS91_TY_S_HR_616E9414 = new TY.Service.Library.Controls.TYSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_616BU409)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_HR_616BB408_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.GBX80_CONTROLS1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_616E9414)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(375, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // CBO01_KBGUNMU
            // 
            this.CBO01_KBGUNMU.FactoryID = "";
            this.CBO01_KBGUNMU.FactoryName = null;
            this.CBO01_KBGUNMU.Location = new System.Drawing.Point(111, 39);
            this.CBO01_KBGUNMU.Name = "CBO01_KBGUNMU";
            this.CBO01_KBGUNMU.Size = new System.Drawing.Size(150, 20);
            this.CBO01_KBGUNMU.TabIndex = 4;
            // 
            // LBL51_KBGUNMU
            // 
            this.LBL51_KBGUNMU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBGUNMU.FactoryID = "";
            this.LBL51_KBGUNMU.FactoryName = null;
            this.LBL51_KBGUNMU.IsCreated = false;
            this.LBL51_KBGUNMU.Location = new System.Drawing.Point(5, 39);
            this.LBL51_KBGUNMU.Name = "LBL51_KBGUNMU";
            this.LBL51_KBGUNMU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBGUNMU.TabIndex = 5;
            this.LBL51_KBGUNMU.Text = "근 무 지";
            this.LBL51_KBGUNMU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.LBL51_STDATE.TabIndex = 9;
            this.LBL51_STDATE.Text = "일 자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_616BU409
            // 
            this.FPS91_TY_S_HR_616BU409.AccessibleDescription = "";
            this.FPS91_TY_S_HR_616BU409.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_616BU409.FactoryID = "";
            this.FPS91_TY_S_HR_616BU409.FactoryName = null;
            this.FPS91_TY_S_HR_616BU409.Location = new System.Drawing.Point(1, 99);
            this.FPS91_TY_S_HR_616BU409.Name = "FPS91_TY_S_HR_616BU409";
            this.FPS91_TY_S_HR_616BU409.PopMenuVisible = false;
            this.FPS91_TY_S_HR_616BU409.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_616BU409.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_P_HR_616BB408_Sheet1});
            this.FPS91_TY_S_HR_616BU409.Size = new System.Drawing.Size(455, 761);
            this.FPS91_TY_S_HR_616BU409.TabIndex = 10;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_616BU409.TextTipAppearance = tipAppearance3;
            this.FPS91_TY_S_HR_616BU409.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_P_HR_616BB408_CellDoubleClick);
            // 
            // FPS91_TY_P_HR_616BB408_Sheet1
            // 
            this.FPS91_TY_P_HR_616BB408_Sheet1.Reset();
            this.FPS91_TY_P_HR_616BB408_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_P_HR_616BB408_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_P_HR_616BB408_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_P_HR_616BB408_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_KBGUNMU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBGUNMU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_616BU409);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(456, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(5, 65);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 22;
            this.LBL51_GGUBUN.Text = "진행구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(111, 65);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(150, 20);
            this.CBO01_GGUBUN.TabIndex = 21;
            this.CBO01_GGUBUN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CBO01_GGUBUN_KeyPress);
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(237, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 20;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "~";
            // 
            // GBX80_CONTROLS1
            // 
            this.GBX80_CONTROLS1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS1.Controls.Add(this.FPS91_TY_S_HR_616E9414);
            this.GBX80_CONTROLS1.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS1.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS1.Controls.Add(this.BTN61_INQ_FXM);
            this.GBX80_CONTROLS1.Controls.Add(this.LBL51_FOGUBUN);
            this.GBX80_CONTROLS1.Controls.Add(this.CBO01_FOGUBUN);
            this.GBX80_CONTROLS1.Controls.Add(this.LBL51_FODATE);
            this.GBX80_CONTROLS1.Controls.Add(this.DTP01_FODATE);
            this.GBX80_CONTROLS1.Location = new System.Drawing.Point(464, 1);
            this.GBX80_CONTROLS1.Name = "GBX80_CONTROLS1";
            this.GBX80_CONTROLS1.Size = new System.Drawing.Size(714, 860);
            this.GBX80_CONTROLS1.TabIndex = 2;
            this.GBX80_CONTROLS1.TabStop = false;
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(552, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 26;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(633, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 25;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // BTN61_INQ_FXM
            // 
            this.BTN61_INQ_FXM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ_FXM.FactoryID = "";
            this.BTN61_INQ_FXM.FactoryName = null;
            this.BTN61_INQ_FXM.Location = new System.Drawing.Point(471, 12);
            this.BTN61_INQ_FXM.Name = "BTN61_INQ_FXM";
            this.BTN61_INQ_FXM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ_FXM.TabIndex = 24;
            this.BTN61_INQ_FXM.Text = "조회";
            this.BTN61_INQ_FXM.UseVisualStyleBackColor = true;
            this.BTN61_INQ_FXM.Click += new System.EventHandler(this.BTN61_INQ_FXM_Click);
            // 
            // LBL51_FOGUBUN
            // 
            this.LBL51_FOGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_FOGUBUN.FactoryID = "";
            this.LBL51_FOGUBUN.FactoryName = null;
            this.LBL51_FOGUBUN.IsCreated = false;
            this.LBL51_FOGUBUN.Location = new System.Drawing.Point(218, 12);
            this.LBL51_FOGUBUN.Name = "LBL51_FOGUBUN";
            this.LBL51_FOGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_FOGUBUN.TabIndex = 23;
            this.LBL51_FOGUBUN.Text = "식수 구분";
            this.LBL51_FOGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_FOGUBUN
            // 
            this.CBO01_FOGUBUN.FactoryID = "";
            this.CBO01_FOGUBUN.FactoryName = null;
            this.CBO01_FOGUBUN.Location = new System.Drawing.Point(324, 12);
            this.CBO01_FOGUBUN.Name = "CBO01_FOGUBUN";
            this.CBO01_FOGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_FOGUBUN.TabIndex = 22;
            // 
            // LBL51_FODATE
            // 
            this.LBL51_FODATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_FODATE.FactoryID = "";
            this.LBL51_FODATE.FactoryName = null;
            this.LBL51_FODATE.IsCreated = false;
            this.LBL51_FODATE.Location = new System.Drawing.Point(6, 12);
            this.LBL51_FODATE.Name = "LBL51_FODATE";
            this.LBL51_FODATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_FODATE.TabIndex = 21;
            this.LBL51_FODATE.Text = "일 자";
            this.LBL51_FODATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_FODATE
            // 
            this.DTP01_FODATE.FactoryID = "";
            this.DTP01_FODATE.FactoryName = null;
            this.DTP01_FODATE.Location = new System.Drawing.Point(112, 12);
            this.DTP01_FODATE.Name = "DTP01_FODATE";
            this.DTP01_FODATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_FODATE.TabIndex = 20;
            // 
            // FPS91_TY_S_HR_616E9414
            // 
            this.FPS91_TY_S_HR_616E9414.AccessibleDescription = "";
            this.FPS91_TY_S_HR_616E9414.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_616E9414.FactoryID = "";
            this.FPS91_TY_S_HR_616E9414.FactoryName = null;
            this.FPS91_TY_S_HR_616E9414.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_616E9414.Name = "FPS91_TY_S_HR_616E9414";
            this.FPS91_TY_S_HR_616E9414.PopMenuVisible = false;
            this.FPS91_TY_S_HR_616E9414.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_616E9414.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.FPS91_TY_S_HR_616E9414.Size = new System.Drawing.Size(713, 815);
            this.FPS91_TY_S_HR_616E9414.TabIndex = 27;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_616E9414.TextTipAppearance = tipAppearance4;
            this.FPS91_TY_S_HR_616E9414.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.FPS91_TY_S_HR_616E9414_EnterCell);
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.AutoUpdateNotes = true;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYHRCS002I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Controls.Add(this.GBX80_CONTROLS1);
            this.Name = "TYHRCS002I";
            this.Load += new System.EventHandler(this.TYHRCS002I_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_616BU409)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_HR_616BB408_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.GBX80_CONTROLS1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_616E9414)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYComboBox CBO01_KBGUNMU;
        private TY.Service.Library.Controls.TYLabel LBL51_KBGUNMU;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_616BU409;
        private FarPoint.Win.Spread.SheetView FPS91_TY_P_HR_616BB408_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS1;
        private Service.Library.Controls.TYLabel LBL51_FOGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_FOGUBUN;
        private Service.Library.Controls.TYLabel LBL51_FODATE;
        private Service.Library.Controls.TYDatePicker DTP01_FODATE;
        private Service.Library.Controls.TYButton BTN61_INQ_FXM;
        private Service.Library.Controls.TYButton BTN61_REM;
        private Service.Library.Controls.TYButton BTN61_SAV;
        private Service.Library.Controls.TYSpread FPS91_TY_S_HR_616E9414;
        private FarPoint.Win.Spread.SheetView sheetView1;
    }
}