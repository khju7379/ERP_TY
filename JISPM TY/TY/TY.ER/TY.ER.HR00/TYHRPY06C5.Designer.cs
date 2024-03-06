namespace TY.ER.HR00
{
    partial class TYHRPY06C5
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.CBH01_PTGUBN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_PTGUBN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_PTJIDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_PTJIDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_PTYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_PTYYMM = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ESCEMPCNT = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ESCEMPCNT = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_73LA3015 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_73LA3015_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_73LA3015)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_73LA3015_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(578, 48);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(416, 48);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(497, 48);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 2;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBH01_PTGUBN
            // 
            this.CBH01_PTGUBN.BindedDataRow = null;
            this.CBH01_PTGUBN.CodeBoxWidth = 0;
            this.CBH01_PTGUBN.DummyValue = null;
            this.CBH01_PTGUBN.FactoryID = "";
            this.CBH01_PTGUBN.FactoryName = null;
            this.CBH01_PTGUBN.Location = new System.Drawing.Point(112, 12);
            this.CBH01_PTGUBN.MinLength = 0;
            this.CBH01_PTGUBN.Name = "CBH01_PTGUBN";
            this.CBH01_PTGUBN.Size = new System.Drawing.Size(100, 20);
            this.CBH01_PTGUBN.TabIndex = 3;
            // 
            // LBL51_PTGUBN
            // 
            this.LBL51_PTGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PTGUBN.FactoryID = "";
            this.LBL51_PTGUBN.FactoryName = null;
            this.LBL51_PTGUBN.IsCreated = false;
            this.LBL51_PTGUBN.Location = new System.Drawing.Point(5, 12);
            this.LBL51_PTGUBN.Name = "LBL51_PTGUBN";
            this.LBL51_PTGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PTGUBN.TabIndex = 4;
            this.LBL51_PTGUBN.Text = "급여구분";
            this.LBL51_PTGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_PTJIDATE
            // 
            this.DTP01_PTJIDATE.FactoryID = "";
            this.DTP01_PTJIDATE.FactoryName = null;
            this.DTP01_PTJIDATE.Location = new System.Drawing.Point(536, 12);
            this.DTP01_PTJIDATE.Name = "DTP01_PTJIDATE";
            this.DTP01_PTJIDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_PTJIDATE.TabIndex = 5;
            // 
            // LBL51_PTJIDATE
            // 
            this.LBL51_PTJIDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PTJIDATE.FactoryID = "";
            this.LBL51_PTJIDATE.FactoryName = null;
            this.LBL51_PTJIDATE.IsCreated = false;
            this.LBL51_PTJIDATE.Location = new System.Drawing.Point(430, 12);
            this.LBL51_PTJIDATE.Name = "LBL51_PTJIDATE";
            this.LBL51_PTJIDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PTJIDATE.TabIndex = 6;
            this.LBL51_PTJIDATE.Text = "지급일자";
            this.LBL51_PTJIDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_PTYYMM
            // 
            this.DTP01_PTYYMM.FactoryID = "";
            this.DTP01_PTYYMM.FactoryName = null;
            this.DTP01_PTYYMM.Location = new System.Drawing.Point(324, 12);
            this.DTP01_PTYYMM.Name = "DTP01_PTYYMM";
            this.DTP01_PTYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_PTYYMM.TabIndex = 7;
            // 
            // LBL51_PTYYMM
            // 
            this.LBL51_PTYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PTYYMM.FactoryID = "";
            this.LBL51_PTYYMM.FactoryName = null;
            this.LBL51_PTYYMM.IsCreated = false;
            this.LBL51_PTYYMM.Location = new System.Drawing.Point(218, 12);
            this.LBL51_PTYYMM.Name = "LBL51_PTYYMM";
            this.LBL51_PTYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PTYYMM.TabIndex = 8;
            this.LBL51_PTYYMM.Text = "급여년월";
            this.LBL51_PTYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ESCEMPCNT
            // 
            this.TXT01_ESCEMPCNT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ESCEMPCNT.FactoryID = "";
            this.TXT01_ESCEMPCNT.FactoryName = null;
            this.TXT01_ESCEMPCNT.Location = new System.Drawing.Point(112, 38);
            this.TXT01_ESCEMPCNT.MinLength = 0;
            this.TXT01_ESCEMPCNT.Name = "TXT01_ESCEMPCNT";
            this.TXT01_ESCEMPCNT.Size = new System.Drawing.Size(54, 21);
            this.TXT01_ESCEMPCNT.TabIndex = 9;
            this.TXT01_ESCEMPCNT.TabIndexCustom = false;
            // 
            // LBL51_ESCEMPCNT
            // 
            this.LBL51_ESCEMPCNT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ESCEMPCNT.FactoryID = "";
            this.LBL51_ESCEMPCNT.FactoryName = null;
            this.LBL51_ESCEMPCNT.IsCreated = false;
            this.LBL51_ESCEMPCNT.Location = new System.Drawing.Point(5, 38);
            this.LBL51_ESCEMPCNT.Name = "LBL51_ESCEMPCNT";
            this.LBL51_ESCEMPCNT.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ESCEMPCNT.TabIndex = 10;
            this.LBL51_ESCEMPCNT.Text = "종업원수";
            this.LBL51_ESCEMPCNT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_73LA3015
            // 
            this.FPS91_TY_S_HR_73LA3015.AccessibleDescription = "";
            this.FPS91_TY_S_HR_73LA3015.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_73LA3015.FactoryID = "";
            this.FPS91_TY_S_HR_73LA3015.FactoryName = null;
            this.FPS91_TY_S_HR_73LA3015.Location = new System.Drawing.Point(1, 75);
            this.FPS91_TY_S_HR_73LA3015.Name = "FPS91_TY_S_HR_73LA3015";
            this.FPS91_TY_S_HR_73LA3015.PopMenuVisible = false;
            this.FPS91_TY_S_HR_73LA3015.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_73LA3015.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_73LA3015_Sheet1});
            this.FPS91_TY_S_HR_73LA3015.Size = new System.Drawing.Size(656, 584);
            this.FPS91_TY_S_HR_73LA3015.TabIndex = 11;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_73LA3015.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_HR_73LA3015_Sheet1
            // 
            this.FPS91_TY_S_HR_73LA3015_Sheet1.Reset();
            this.FPS91_TY_S_HR_73LA3015_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_73LA3015_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_73LA3015_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_73LA3015_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PTGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PTGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PTJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PTJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_ESCEMPCNT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_ESCEMPCNT);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_73LA3015);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(659, 665);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "(명)";
            // 
            // TYHRPY06C5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 672);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY06C5";
            this.Load += new System.EventHandler(this.TYHRPY06C5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_73LA3015)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_73LA3015_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYCodeBox CBH01_PTGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PTGUBN;
        private TY.Service.Library.Controls.TYDatePicker DTP01_PTJIDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_PTJIDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_PTYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_PTYYMM;
        private TY.Service.Library.Controls.TYTextBox TXT01_ESCEMPCNT;
        private TY.Service.Library.Controls.TYLabel LBL51_ESCEMPCNT;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_73LA3015;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_73LA3015_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
    }
}