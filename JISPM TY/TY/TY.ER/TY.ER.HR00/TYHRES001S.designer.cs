namespace TY.ER.HR00
{
    partial class TYHRES001S
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBH01_EJSAUP = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_EJSAUP = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EJYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_EJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_28M2B487 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_28M2B487_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_EJSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_EJSABUN = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28M2B487)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28M2B487_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(1094, 12);
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
            this.BTN61_INQ.Location = new System.Drawing.Point(932, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // CBH01_EJSAUP
            // 
            this.CBH01_EJSAUP.BindedDataRow = null;
            this.CBH01_EJSAUP.CodeBoxWidth = 0;
            this.CBH01_EJSAUP.DummyValue = null;
            this.CBH01_EJSAUP.FactoryID = "";
            this.CBH01_EJSAUP.FactoryName = null;
            this.CBH01_EJSAUP.Location = new System.Drawing.Point(381, 12);
            this.CBH01_EJSAUP.MinLength = 0;
            this.CBH01_EJSAUP.Name = "CBH01_EJSAUP";
            this.CBH01_EJSAUP.Size = new System.Drawing.Size(158, 20);
            this.CBH01_EJSAUP.TabIndex = 2;
            // 
            // LBL51_EJSAUP
            // 
            this.LBL51_EJSAUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EJSAUP.FactoryID = "";
            this.LBL51_EJSAUP.FactoryName = null;
            this.LBL51_EJSAUP.Location = new System.Drawing.Point(275, 12);
            this.LBL51_EJSAUP.Name = "LBL51_EJSAUP";
            this.LBL51_EJSAUP.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EJSAUP.TabIndex = 3;
            this.LBL51_EJSAUP.Text = "사업부";
            this.LBL51_EJSAUP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EJYYMM
            // 
            this.DTP01_EJYYMM.FactoryID = "";
            this.DTP01_EJYYMM.FactoryName = null;
            this.DTP01_EJYYMM.Location = new System.Drawing.Point(111, 12);
            this.DTP01_EJYYMM.Name = "DTP01_EJYYMM";
            this.DTP01_EJYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EJYYMM.TabIndex = 4;
            this.DTP01_EJYYMM.ValueChanged += new System.EventHandler(this.DTP01_EJYYMM_ValueChanged);
            // 
            // LBL51_EJYYMM
            // 
            this.LBL51_EJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EJYYMM.FactoryID = "";
            this.LBL51_EJYYMM.FactoryName = null;
            this.LBL51_EJYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_EJYYMM.Name = "LBL51_EJYYMM";
            this.LBL51_EJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EJYYMM.TabIndex = 5;
            this.LBL51_EJYYMM.Text = "기준년월";
            this.LBL51_EJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_28M2B487
            // 
            this.FPS91_TY_S_HR_28M2B487.AccessibleDescription = "";
            this.FPS91_TY_S_HR_28M2B487.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_28M2B487.FactoryID = "";
            this.FPS91_TY_S_HR_28M2B487.FactoryName = null;
            this.FPS91_TY_S_HR_28M2B487.Location = new System.Drawing.Point(1, 75);
            this.FPS91_TY_S_HR_28M2B487.Name = "FPS91_TY_S_HR_28M2B487";
            this.FPS91_TY_S_HR_28M2B487.PopMenuVisible = false;
            this.FPS91_TY_S_HR_28M2B487.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_28M2B487.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_28M2B487_Sheet1});
            this.FPS91_TY_S_HR_28M2B487.Size = new System.Drawing.Size(1174, 781);
            this.FPS91_TY_S_HR_28M2B487.TabIndex = 8;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_28M2B487.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_HR_28M2B487_Sheet1
            // 
            this.FPS91_TY_S_HR_28M2B487_Sheet1.Reset();
            this.FPS91_TY_S_HR_28M2B487_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_28M2B487_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_28M2B487_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_28M2B487_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_EJSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EJSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_EJSAUP);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EJSAUP);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_28M2B487);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_EJSABUN
            // 
            this.CBH01_EJSABUN.BindedDataRow = null;
            this.CBH01_EJSABUN.CodeBoxWidth = 0;
            this.CBH01_EJSABUN.DummyValue = null;
            this.CBH01_EJSABUN.FactoryID = "";
            this.CBH01_EJSABUN.FactoryName = null;
            this.CBH01_EJSABUN.Location = new System.Drawing.Point(111, 39);
            this.CBH01_EJSABUN.MinLength = 0;
            this.CBH01_EJSABUN.Name = "CBH01_EJSABUN";
            this.CBH01_EJSABUN.Size = new System.Drawing.Size(158, 20);
            this.CBH01_EJSABUN.TabIndex = 9;
            // 
            // LBL51_EJSABUN
            // 
            this.LBL51_EJSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EJSABUN.FactoryID = "";
            this.LBL51_EJSABUN.FactoryName = null;
            this.LBL51_EJSABUN.Location = new System.Drawing.Point(5, 39);
            this.LBL51_EJSABUN.Name = "LBL51_EJSABUN";
            this.LBL51_EJSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EJSABUN.TabIndex = 10;
            this.LBL51_EJSABUN.Text = "관리사번";
            this.LBL51_EJSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 11;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // TYHRES001S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRES001S";
            this.Load += new System.EventHandler(this.TYHRES001S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28M2B487)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28M2B487_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYCodeBox CBH01_EJSAUP;
        private TY.Service.Library.Controls.TYLabel LBL51_EJSAUP;
        private TY.Service.Library.Controls.TYDatePicker DTP01_EJYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_EJYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_28M2B487;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_28M2B487_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_EJSABUN;
        private Service.Library.Controls.TYLabel LBL51_EJSABUN;
        private Service.Library.Controls.TYButton BTN61_REM;
    }
}