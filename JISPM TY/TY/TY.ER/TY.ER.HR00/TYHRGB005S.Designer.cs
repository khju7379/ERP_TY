namespace TY.ER.HR00
{
    partial class TYHRGB005S
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
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_HR_85BC1011 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_85BC1011_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.LBL51_CIVEND = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_CIVEND = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_CIGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_CIGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_CIWORK = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CIWORK = new TY.Service.Library.Controls.TYTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_85BC1011)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_85BC1011_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1013, 38);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // FPS91_TY_S_HR_85BC1011
            // 
            this.FPS91_TY_S_HR_85BC1011.AccessibleDescription = "";
            this.FPS91_TY_S_HR_85BC1011.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_85BC1011.FactoryID = "";
            this.FPS91_TY_S_HR_85BC1011.FactoryName = null;
            this.FPS91_TY_S_HR_85BC1011.Location = new System.Drawing.Point(1, 72);
            this.FPS91_TY_S_HR_85BC1011.Name = "FPS91_TY_S_HR_85BC1011";
            this.FPS91_TY_S_HR_85BC1011.PopMenuVisible = false;
            this.FPS91_TY_S_HR_85BC1011.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_85BC1011.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_85BC1011_Sheet1});
            this.FPS91_TY_S_HR_85BC1011.Size = new System.Drawing.Size(1174, 688);
            this.FPS91_TY_S_HR_85BC1011.TabIndex = 9;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_85BC1011.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_HR_85BC1011_Sheet1
            // 
            this.FPS91_TY_S_HR_85BC1011_Sheet1.Reset();
            this.FPS91_TY_S_HR_85BC1011_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_85BC1011_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_85BC1011_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_85BC1011_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CIWORK);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_CIWORK);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CIGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_CIGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CIVEND);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_CIVEND);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_85BC1011);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 760);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 38);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 100;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // LBL51_CIVEND
            // 
            this.LBL51_CIVEND.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CIVEND.FactoryID = "";
            this.LBL51_CIVEND.FactoryName = null;
            this.LBL51_CIVEND.IsCreated = false;
            this.LBL51_CIVEND.Location = new System.Drawing.Point(5, 39);
            this.LBL51_CIVEND.Name = "LBL51_CIVEND";
            this.LBL51_CIVEND.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CIVEND.TabIndex = 99;
            this.LBL51_CIVEND.Text = "거 래 처";
            this.LBL51_CIVEND.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_CIVEND
            // 
            this.CBH01_CIVEND.BindedDataRow = null;
            this.CBH01_CIVEND.CodeBoxWidth = 0;
            this.CBH01_CIVEND.DummyValue = null;
            this.CBH01_CIVEND.FactoryID = "";
            this.CBH01_CIVEND.FactoryName = null;
            this.CBH01_CIVEND.Location = new System.Drawing.Point(111, 39);
            this.CBH01_CIVEND.MinLength = 0;
            this.CBH01_CIVEND.Name = "CBH01_CIVEND";
            this.CBH01_CIVEND.Size = new System.Drawing.Size(223, 20);
            this.CBH01_CIVEND.TabIndex = 98;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 97;
            this.label1.Text = "-";
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 96;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 95;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.ForeColor = System.Drawing.Color.Black;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 94;
            this.LBL51_STDATE.Text = "일     자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_CIGUBUN
            // 
            this.CBO01_CIGUBUN.FactoryID = "";
            this.CBO01_CIGUBUN.FactoryName = null;
            this.CBO01_CIGUBUN.Location = new System.Drawing.Point(502, 12);
            this.CBO01_CIGUBUN.Name = "CBO01_CIGUBUN";
            this.CBO01_CIGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_CIGUBUN.TabIndex = 101;
            // 
            // LBL51_CIGUBUN
            // 
            this.LBL51_CIGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CIGUBUN.FactoryID = "";
            this.LBL51_CIGUBUN.FactoryName = null;
            this.LBL51_CIGUBUN.IsCreated = false;
            this.LBL51_CIGUBUN.Location = new System.Drawing.Point(396, 12);
            this.LBL51_CIGUBUN.Name = "LBL51_CIGUBUN";
            this.LBL51_CIGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CIGUBUN.TabIndex = 102;
            this.LBL51_CIGUBUN.Text = "작업 구분";
            this.LBL51_CIGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_CIWORK
            // 
            this.LBL51_CIWORK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CIWORK.FactoryID = "";
            this.LBL51_CIWORK.FactoryName = null;
            this.LBL51_CIWORK.IsCreated = false;
            this.LBL51_CIWORK.Location = new System.Drawing.Point(396, 38);
            this.LBL51_CIWORK.Name = "LBL51_CIWORK";
            this.LBL51_CIWORK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CIWORK.TabIndex = 103;
            this.LBL51_CIWORK.Text = "작  업  명";
            this.LBL51_CIWORK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CIWORK
            // 
            this.TXT01_CIWORK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_CIWORK.FactoryID = "";
            this.TXT01_CIWORK.FactoryName = null;
            this.TXT01_CIWORK.Location = new System.Drawing.Point(502, 38);
            this.TXT01_CIWORK.MinLength = 0;
            this.TXT01_CIWORK.Name = "TXT01_CIWORK";
            this.TXT01_CIWORK.Size = new System.Drawing.Size(280, 21);
            this.TXT01_CIWORK.TabIndex = 104;
            this.TXT01_CIWORK.TabIndexCustom = false;
            // 
            // TYHRGB005S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGB005S";
            this.Load += new System.EventHandler(this.TYHRGB005S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_85BC1011)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_85BC1011_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_85BC1011;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_85BC1011_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYLabel LBL51_STDATE;
        private Service.Library.Controls.TYLabel LBL51_CIVEND;
        private Service.Library.Controls.TYCodeBox CBH01_CIVEND;
        private Service.Library.Controls.TYButton BTN61_PRT;
        private Service.Library.Controls.TYComboBox CBO01_CIGUBUN;
        private Service.Library.Controls.TYLabel LBL51_CIGUBUN;
        private Service.Library.Controls.TYLabel LBL51_CIWORK;
        private Service.Library.Controls.TYTextBox TXT01_CIWORK;
    }
}