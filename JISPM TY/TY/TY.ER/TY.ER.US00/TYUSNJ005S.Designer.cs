namespace TY.ER.US00
{
    partial class TYUSNJ005S
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
            this.BTN61_NEW = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_US_949GM305 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_949GM305_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_HIGOKJONG = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_HIGOKJONG = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_HIHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_HIHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_949GM305)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_949GM305_Sheet1)).BeginInit();
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
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 2;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // FPS91_TY_S_US_949GM305
            // 
            this.FPS91_TY_S_US_949GM305.AccessibleDescription = "";
            this.FPS91_TY_S_US_949GM305.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_949GM305.FactoryID = "";
            this.FPS91_TY_S_US_949GM305.FactoryName = null;
            this.FPS91_TY_S_US_949GM305.Location = new System.Drawing.Point(0, 65);
            this.FPS91_TY_S_US_949GM305.Name = "FPS91_TY_S_US_949GM305";
            this.FPS91_TY_S_US_949GM305.PopMenuVisible = false;
            this.FPS91_TY_S_US_949GM305.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_949GM305.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_949GM305_Sheet1});
            this.FPS91_TY_S_US_949GM305.Size = new System.Drawing.Size(1173, 795);
            this.FPS91_TY_S_US_949GM305.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_949GM305.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_US_949GM305.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_US_949GM305_CellDoubleClick);
            // 
            // FPS91_TY_S_US_949GM305_Sheet1
            // 
            this.FPS91_TY_S_US_949GM305_Sheet1.Reset();
            this.FPS91_TY_S_US_949GM305_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_949GM305_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_949GM305_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_949GM305_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_HIGOKJONG);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_HIGOKJONG);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_HIHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_HIHANGCHA);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_US_949GM305);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_HIGOKJONG
            // 
            this.LBL51_HIGOKJONG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_HIGOKJONG.FactoryID = "";
            this.LBL51_HIGOKJONG.FactoryName = null;
            this.LBL51_HIGOKJONG.IsCreated = false;
            this.LBL51_HIGOKJONG.Location = new System.Drawing.Point(5, 38);
            this.LBL51_HIGOKJONG.Name = "LBL51_HIGOKJONG";
            this.LBL51_HIGOKJONG.Size = new System.Drawing.Size(100, 21);
            this.LBL51_HIGOKJONG.TabIndex = 351;
            this.LBL51_HIGOKJONG.Text = "곡 종";
            this.LBL51_HIGOKJONG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_HIGOKJONG
            // 
            this.CBH01_HIGOKJONG.BindedDataRow = null;
            this.CBH01_HIGOKJONG.CodeBoxWidth = 0;
            this.CBH01_HIGOKJONG.DummyValue = null;
            this.CBH01_HIGOKJONG.FactoryID = "";
            this.CBH01_HIGOKJONG.FactoryName = null;
            this.CBH01_HIGOKJONG.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_HIGOKJONG.Location = new System.Drawing.Point(111, 39);
            this.CBH01_HIGOKJONG.MinLength = 0;
            this.CBH01_HIGOKJONG.Name = "CBH01_HIGOKJONG";
            this.CBH01_HIGOKJONG.Size = new System.Drawing.Size(202, 20);
            this.CBH01_HIGOKJONG.TabIndex = 350;
            // 
            // CBH01_HIHANGCHA
            // 
            this.CBH01_HIHANGCHA.BindedDataRow = null;
            this.CBH01_HIHANGCHA.CodeBoxWidth = 0;
            this.CBH01_HIHANGCHA.DummyValue = null;
            this.CBH01_HIHANGCHA.FactoryID = "";
            this.CBH01_HIHANGCHA.FactoryName = null;
            this.CBH01_HIHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_HIHANGCHA.Location = new System.Drawing.Point(473, 12);
            this.CBH01_HIHANGCHA.MinLength = 0;
            this.CBH01_HIHANGCHA.Name = "CBH01_HIHANGCHA";
            this.CBH01_HIHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH01_HIHANGCHA.TabIndex = 349;
            // 
            // LBL51_HIHANGCHA
            // 
            this.LBL51_HIHANGCHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_HIHANGCHA.FactoryID = "";
            this.LBL51_HIHANGCHA.FactoryName = null;
            this.LBL51_HIHANGCHA.IsCreated = false;
            this.LBL51_HIHANGCHA.Location = new System.Drawing.Point(367, 12);
            this.LBL51_HIHANGCHA.Name = "LBL51_HIHANGCHA";
            this.LBL51_HIHANGCHA.Size = new System.Drawing.Size(100, 21);
            this.LBL51_HIHANGCHA.TabIndex = 348;
            this.LBL51_HIHANGCHA.Text = "항 차";
            this.LBL51_HIHANGCHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 347;
            this.label1.Text = "~";
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(249, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(112, 21);
            this.DTP01_EDATE.TabIndex = 346;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(112, 21);
            this.DTP01_SDATE.TabIndex = 345;
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
            this.LBL51_SDATE.TabIndex = 25;
            this.LBL51_SDATE.Text = "작업일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUSNJ005S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSNJ005S";
            this.Load += new System.EventHandler(this.TYUSNJ005S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_949GM305)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_949GM305_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_US_949GM305;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_949GM305_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_SDATE;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private Service.Library.Controls.TYLabel LBL51_HIHANGCHA;
        private Service.Library.Controls.TYCodeBox CBH01_HIHANGCHA;
        private Service.Library.Controls.TYLabel LBL51_HIGOKJONG;
        private Service.Library.Controls.TYCodeBox CBH01_HIGOKJONG;
    }
}