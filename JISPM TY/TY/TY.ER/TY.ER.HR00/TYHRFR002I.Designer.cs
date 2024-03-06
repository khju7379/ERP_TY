namespace TY.ER.HR00
{
    partial class TYHRFR002I
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBH01_PAYGUBN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_PAYGUBN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_53IEI702 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_53IEI702_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53IEI702)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53IEI702_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1093, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // CBH01_PAYGUBN
            // 
            this.CBH01_PAYGUBN.BindedDataRow = null;
            this.CBH01_PAYGUBN.CodeBoxWidth = 0;
            this.CBH01_PAYGUBN.DummyValue = null;
            this.CBH01_PAYGUBN.FactoryID = "";
            this.CBH01_PAYGUBN.FactoryName = null;
            this.CBH01_PAYGUBN.Location = new System.Drawing.Point(475, 13);
            this.CBH01_PAYGUBN.MinLength = 0;
            this.CBH01_PAYGUBN.Name = "CBH01_PAYGUBN";
            this.CBH01_PAYGUBN.Size = new System.Drawing.Size(155, 20);
            this.CBH01_PAYGUBN.TabIndex = 1;
            // 
            // LBL51_PAYGUBN
            // 
            this.LBL51_PAYGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYGUBN.FactoryID = "";
            this.LBL51_PAYGUBN.FactoryName = null;
            this.LBL51_PAYGUBN.IsCreated = false;
            this.LBL51_PAYGUBN.Location = new System.Drawing.Point(369, 12);
            this.LBL51_PAYGUBN.Name = "LBL51_PAYGUBN";
            this.LBL51_PAYGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYGUBN.TabIndex = 2;
            this.LBL51_PAYGUBN.Text = "급여구분";
            this.LBL51_PAYGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.LBL51_SDATE.TabIndex = 6;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_53IEI702
            // 
            this.FPS91_TY_S_HR_53IEI702.AccessibleDescription = "";
            this.FPS91_TY_S_HR_53IEI702.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_53IEI702.FactoryID = "";
            this.FPS91_TY_S_HR_53IEI702.FactoryName = null;
            this.FPS91_TY_S_HR_53IEI702.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_53IEI702.Name = "FPS91_TY_S_HR_53IEI702";
            this.FPS91_TY_S_HR_53IEI702.PopMenuVisible = false;
            this.FPS91_TY_S_HR_53IEI702.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_53IEI702.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_53IEI702_Sheet1});
            this.FPS91_TY_S_HR_53IEI702.Size = new System.Drawing.Size(1174, 812);
            this.FPS91_TY_S_HR_53IEI702.TabIndex = 7;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_53IEI702.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_HR_53IEI702.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FPS91_TY_S_HR_53IEI702_ButtonClicked);
            // 
            // FPS91_TY_S_HR_53IEI702_Sheet1
            // 
            this.FPS91_TY_S_HR_53IEI702_Sheet1.Reset();
            this.FPS91_TY_S_HR_53IEI702_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_53IEI702_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_53IEI702_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_53IEI702_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_53IEI702);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(250, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(113, 21);
            this.DTP01_EDATE.TabIndex = 12;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(113, 21);
            this.DTP01_SDATE.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "~";
            // 
            // TYHRFR002I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRFR002I";
            this.Load += new System.EventHandler(this.TYHRFR002I_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53IEI702)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53IEI702_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYCodeBox CBH01_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_53IEI702;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_53IEI702_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
    }
}