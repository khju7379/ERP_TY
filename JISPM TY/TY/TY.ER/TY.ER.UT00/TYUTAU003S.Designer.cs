namespace TY.ER.UT00
{
    partial class TYUTAU003S
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
            this.BTN61_NEW = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_UT_71DA4440 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_71DA4440_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.LBL51_SHWAMUL = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SHWAMUL = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_SHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_SFTANKNO = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SFTANKNO = new TY.Service.Library.Controls.TYTextBox();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_71DA4440)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_71DA4440_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(932, 39);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1013, 39);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 1;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1094, 39);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 2;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 6;
            this.LBL51_STDATE.Text = "계약년도";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.TXT01_SFTANKNO);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SFTANKNO);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SHWAMUL);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_SHWAMUL);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SHWAJU);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_SHWAJU);
            this.GRP01_SEARCH.Controls.Add(this.DTP01_EDDATE);
            this.GRP01_SEARCH.Controls.Add(this.DTP01_STDATE);
            this.GRP01_SEARCH.Controls.Add(this.label1);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_UT_71DA4440);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_NEW);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_REM);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_STDATE);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 93;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 92;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 88;
            this.label1.Text = "-";
            // 
            // FPS91_TY_S_UT_71DA4440
            // 
            this.FPS91_TY_S_UT_71DA4440.AccessibleDescription = "";
            this.FPS91_TY_S_UT_71DA4440.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_71DA4440.FactoryID = "";
            this.FPS91_TY_S_UT_71DA4440.FactoryName = null;
            this.FPS91_TY_S_UT_71DA4440.Location = new System.Drawing.Point(1, 78);
            this.FPS91_TY_S_UT_71DA4440.Name = "FPS91_TY_S_UT_71DA4440";
            this.FPS91_TY_S_UT_71DA4440.PopMenuVisible = false;
            this.FPS91_TY_S_UT_71DA4440.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_71DA4440.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_71DA4440_Sheet1});
            this.FPS91_TY_S_UT_71DA4440.Size = new System.Drawing.Size(1175, 751);
            this.FPS91_TY_S_UT_71DA4440.TabIndex = 47;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_71DA4440.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_UT_71DA4440.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_UT_71DA4440_CellDoubleClick);
            // 
            // FPS91_TY_S_UT_71DA4440_Sheet1
            // 
            this.FPS91_TY_S_UT_71DA4440_Sheet1.Reset();
            this.FPS91_TY_S_UT_71DA4440_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_71DA4440_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_71DA4440_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_71DA4440_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // LBL51_SHWAMUL
            // 
            this.LBL51_SHWAMUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SHWAMUL.FactoryID = "";
            this.LBL51_SHWAMUL.FactoryName = null;
            this.LBL51_SHWAMUL.ForeColor = System.Drawing.Color.White;
            this.LBL51_SHWAMUL.IsCreated = false;
            this.LBL51_SHWAMUL.Location = new System.Drawing.Point(5, 39);
            this.LBL51_SHWAMUL.Name = "LBL51_SHWAMUL";
            this.LBL51_SHWAMUL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SHWAMUL.TabIndex = 103;
            this.LBL51_SHWAMUL.Text = "화 물";
            this.LBL51_SHWAMUL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SHWAMUL
            // 
            this.CBH01_SHWAMUL.BindedDataRow = null;
            this.CBH01_SHWAMUL.CodeBoxWidth = 0;
            this.CBH01_SHWAMUL.DummyValue = null;
            this.CBH01_SHWAMUL.FactoryID = "";
            this.CBH01_SHWAMUL.FactoryName = null;
            this.CBH01_SHWAMUL.Location = new System.Drawing.Point(111, 39);
            this.CBH01_SHWAMUL.MinLength = 0;
            this.CBH01_SHWAMUL.Name = "CBH01_SHWAMUL";
            this.CBH01_SHWAMUL.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SHWAMUL.TabIndex = 102;
            // 
            // LBL51_SHWAJU
            // 
            this.LBL51_SHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SHWAJU.FactoryID = "";
            this.LBL51_SHWAJU.FactoryName = null;
            this.LBL51_SHWAJU.ForeColor = System.Drawing.Color.White;
            this.LBL51_SHWAJU.IsCreated = false;
            this.LBL51_SHWAJU.Location = new System.Drawing.Point(417, 12);
            this.LBL51_SHWAJU.Name = "LBL51_SHWAJU";
            this.LBL51_SHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SHWAJU.TabIndex = 101;
            this.LBL51_SHWAJU.Text = "화 주";
            this.LBL51_SHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SHWAJU
            // 
            this.CBH01_SHWAJU.BindedDataRow = null;
            this.CBH01_SHWAJU.CodeBoxWidth = 0;
            this.CBH01_SHWAJU.DummyValue = null;
            this.CBH01_SHWAJU.FactoryID = "";
            this.CBH01_SHWAJU.FactoryName = null;
            this.CBH01_SHWAJU.Location = new System.Drawing.Point(523, 12);
            this.CBH01_SHWAJU.MinLength = 0;
            this.CBH01_SHWAJU.Name = "CBH01_SHWAJU";
            this.CBH01_SHWAJU.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SHWAJU.TabIndex = 100;
            // 
            // LBL51_SFTANKNO
            // 
            this.LBL51_SFTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SFTANKNO.FactoryID = "";
            this.LBL51_SFTANKNO.FactoryName = null;
            this.LBL51_SFTANKNO.ForeColor = System.Drawing.Color.White;
            this.LBL51_SFTANKNO.IsCreated = false;
            this.LBL51_SFTANKNO.Location = new System.Drawing.Point(417, 39);
            this.LBL51_SFTANKNO.Name = "LBL51_SFTANKNO";
            this.LBL51_SFTANKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SFTANKNO.TabIndex = 213;
            this.LBL51_SFTANKNO.Text = "탱크 번호";
            this.LBL51_SFTANKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SFTANKNO
            // 
            this.TXT01_SFTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SFTANKNO.FactoryID = "";
            this.TXT01_SFTANKNO.FactoryName = null;
            this.TXT01_SFTANKNO.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_SFTANKNO.Location = new System.Drawing.Point(523, 39);
            this.TXT01_SFTANKNO.MinLength = 0;
            this.TXT01_SFTANKNO.Name = "TXT01_SFTANKNO";
            this.TXT01_SFTANKNO.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SFTANKNO.TabIndex = 214;
            this.TXT01_SFTANKNO.TabIndexCustom = false;
            // 
            // TYUTAU003S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUTAU003S";
            this.Load += new System.EventHandler(this.TYUTAU003S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_71DA4440)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_71DA4440_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_UT_71DA4440;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_71DA4440_Sheet1;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYLabel LBL51_SHWAMUL;
        private Service.Library.Controls.TYCodeBox CBH01_SHWAMUL;
        private Service.Library.Controls.TYLabel LBL51_SHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_SHWAJU;
        private Service.Library.Controls.TYLabel LBL51_SFTANKNO;
        private Service.Library.Controls.TYTextBox TXT01_SFTANKNO;
    }
}