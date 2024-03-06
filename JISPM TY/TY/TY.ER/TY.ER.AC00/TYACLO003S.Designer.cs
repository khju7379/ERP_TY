namespace TY.ER.AC00
{
    partial class TYACLO003S
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.LBL51_LOCCURRTYPE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_AC_88AD7528 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_88AD7528_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.CBH01_LOCCURRTYPE = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_88AD7528)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_88AD7528_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1014, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1095, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // LBL51_LOCCURRTYPE
            // 
            this.LBL51_LOCCURRTYPE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_LOCCURRTYPE.FactoryID = "";
            this.LBL51_LOCCURRTYPE.FactoryName = null;
            this.LBL51_LOCCURRTYPE.IsCreated = false;
            this.LBL51_LOCCURRTYPE.Location = new System.Drawing.Point(217, 12);
            this.LBL51_LOCCURRTYPE.Name = "LBL51_LOCCURRTYPE";
            this.LBL51_LOCCURRTYPE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_LOCCURRTYPE.TabIndex = 3;
            this.LBL51_LOCCURRTYPE.Text = "통화유형";
            this.LBL51_LOCCURRTYPE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 4;
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
            this.LBL51_STDATE.TabIndex = 5;
            this.LBL51_STDATE.Text = "시작일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_88AD7528);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_LOCCURRTYPE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_LOCCURRTYPE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // FPS91_TY_S_AC_88AD7528
            // 
            this.FPS91_TY_S_AC_88AD7528.AccessibleDescription = "";
            this.FPS91_TY_S_AC_88AD7528.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_88AD7528.FactoryID = "";
            this.FPS91_TY_S_AC_88AD7528.FactoryName = null;
            this.FPS91_TY_S_AC_88AD7528.Location = new System.Drawing.Point(1, 42);
            this.FPS91_TY_S_AC_88AD7528.Name = "FPS91_TY_S_AC_88AD7528";
            this.FPS91_TY_S_AC_88AD7528.PopMenuVisible = false;
            this.FPS91_TY_S_AC_88AD7528.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_88AD7528.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_88AD7528_Sheet1});
            this.FPS91_TY_S_AC_88AD7528.Size = new System.Drawing.Size(1174, 818);
            this.FPS91_TY_S_AC_88AD7528.TabIndex = 98;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_88AD7528.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_88AD7528_Sheet1
            // 
            this.FPS91_TY_S_AC_88AD7528_Sheet1.Reset();
            this.FPS91_TY_S_AC_88AD7528_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // CBH01_LOCCURRTYPE
            // 
            this.CBH01_LOCCURRTYPE.BindedDataRow = null;
            this.CBH01_LOCCURRTYPE.CodeBoxWidth = 0;
            this.CBH01_LOCCURRTYPE.DummyValue = null;
            this.CBH01_LOCCURRTYPE.FactoryID = "";
            this.CBH01_LOCCURRTYPE.FactoryName = null;
            this.CBH01_LOCCURRTYPE.Location = new System.Drawing.Point(323, 13);
            this.CBH01_LOCCURRTYPE.MinLength = 0;
            this.CBH01_LOCCURRTYPE.Name = "CBH01_LOCCURRTYPE";
            this.CBH01_LOCCURRTYPE.Size = new System.Drawing.Size(100, 20);
            this.CBH01_LOCCURRTYPE.TabIndex = 97;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(429, 12);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 99;
            this.LBL51_GGUBUN.Text = "잔액";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(535, 12);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(102, 20);
            this.CBO01_GGUBUN.TabIndex = 245;
            // 
            // TYACLO003S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACLO003S";
            this.Load += new System.EventHandler(this.TYACLO003S_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_88AD7528)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_88AD7528_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYLabel LBL51_LOCCURRTYPE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_LOCCURRTYPE;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_88AD7528;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_88AD7528_Sheet1;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
    }
}