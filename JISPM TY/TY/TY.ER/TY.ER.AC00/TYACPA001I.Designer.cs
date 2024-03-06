namespace TY.ER.AC00
{
    partial class TYACPA001I
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_ECMONTH = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ECMONTH = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ECYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ECYEAR = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_27C97009 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_27C97009_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_27C97009)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_27C97009_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 2;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // TXT01_ECMONTH
            // 
            this.TXT01_ECMONTH.FactoryID = "";
            this.TXT01_ECMONTH.FactoryName = null;
            this.TXT01_ECMONTH.Location = new System.Drawing.Point(283, 12);
            this.TXT01_ECMONTH.MinLength = 0;
            this.TXT01_ECMONTH.Name = "TXT01_ECMONTH";
            this.TXT01_ECMONTH.Size = new System.Drawing.Size(30, 21);
            this.TXT01_ECMONTH.TabIndex = 3;
            // 
            // LBL51_ECMONTH
            // 
            this.LBL51_ECMONTH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ECMONTH.FactoryID = "";
            this.LBL51_ECMONTH.FactoryName = null;
            this.LBL51_ECMONTH.Location = new System.Drawing.Point(177, 12);
            this.LBL51_ECMONTH.Name = "LBL51_ECMONTH";
            this.LBL51_ECMONTH.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ECMONTH.TabIndex = 4;
            this.LBL51_ECMONTH.Text = "월";
            this.LBL51_ECMONTH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ECYEAR
            // 
            this.TXT01_ECYEAR.FactoryID = "";
            this.TXT01_ECYEAR.FactoryName = null;
            this.TXT01_ECYEAR.Location = new System.Drawing.Point(111, 12);
            this.TXT01_ECYEAR.MinLength = 0;
            this.TXT01_ECYEAR.Name = "TXT01_ECYEAR";
            this.TXT01_ECYEAR.Size = new System.Drawing.Size(60, 21);
            this.TXT01_ECYEAR.TabIndex = 5;
            // 
            // LBL51_ECYEAR
            // 
            this.LBL51_ECYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ECYEAR.FactoryID = "";
            this.LBL51_ECYEAR.FactoryName = null;
            this.LBL51_ECYEAR.Location = new System.Drawing.Point(5, 12);
            this.LBL51_ECYEAR.Name = "LBL51_ECYEAR";
            this.LBL51_ECYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ECYEAR.TabIndex = 6;
            this.LBL51_ECYEAR.Text = "년도";
            this.LBL51_ECYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_27C97009
            // 
            this.FPS91_TY_S_AC_27C97009.AccessibleDescription = "";
            this.FPS91_TY_S_AC_27C97009.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_27C97009.FactoryID = "";
            this.FPS91_TY_S_AC_27C97009.FactoryName = null;
            this.FPS91_TY_S_AC_27C97009.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_27C97009.Name = "FPS91_TY_S_AC_27C97009";
            this.FPS91_TY_S_AC_27C97009.PopMenuVisible = false;
            this.FPS91_TY_S_AC_27C97009.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_27C97009.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_27C97009_Sheet1});
            this.FPS91_TY_S_AC_27C97009.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_AC_27C97009.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_27C97009.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_27C97009_Sheet1
            // 
            this.FPS91_TY_S_AC_27C97009_Sheet1.Reset();
            this.FPS91_TY_S_AC_27C97009_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_27C97009_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_27C97009_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_27C97009_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FPS91_TY_S_AC_27C97009);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_ECYEAR);
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Controls.Add(this.BTN61_REM);
            this.groupBox1.Controls.Add(this.TXT01_ECYEAR);
            this.groupBox1.Controls.Add(this.LBL51_ECMONTH);
            this.groupBox1.Controls.Add(this.TXT01_ECMONTH);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // TYACPA001I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACPA001I";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.TYACPA001I_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_27C97009)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_27C97009_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYTextBox TXT01_ECMONTH;
        private TY.Service.Library.Controls.TYLabel LBL51_ECMONTH;
        private TY.Service.Library.Controls.TYTextBox TXT01_ECYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_ECYEAR;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_27C97009;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_27C97009_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}