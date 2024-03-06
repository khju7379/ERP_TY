namespace TY.ER.AC00
{
    partial class TYACDE009S
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
            this.LBL51_GCDBK = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GDATE = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBH01_GCDBK = new TY.Service.Library.Controls.TYCodeBox();
            this.FPS91_TY_S_AC_24K5P796 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_24K5P796_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24K5P796)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24K5P796_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1013, 12);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // LBL51_GCDBK
            // 
            this.LBL51_GCDBK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GCDBK.FactoryID = "";
            this.LBL51_GCDBK.FactoryName = null;
            this.LBL51_GCDBK.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GCDBK.Name = "LBL51_GCDBK";
            this.LBL51_GCDBK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GCDBK.TabIndex = 3;
            this.LBL51_GCDBK.Text = "은행코드";
            this.LBL51_GCDBK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GDATE
            // 
            this.DTP01_GDATE.FactoryID = "";
            this.DTP01_GDATE.FactoryName = null;
            this.DTP01_GDATE.Location = new System.Drawing.Point(564, 12);
            this.DTP01_GDATE.Name = "DTP01_GDATE";
            this.DTP01_GDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GDATE.TabIndex = 4;
            // 
            // LBL51_GDATE
            // 
            this.LBL51_GDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GDATE.FactoryID = "";
            this.LBL51_GDATE.FactoryName = null;
            this.LBL51_GDATE.Location = new System.Drawing.Point(458, 12);
            this.LBL51_GDATE.Name = "LBL51_GDATE";
            this.LBL51_GDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GDATE.TabIndex = 5;
            this.LBL51_GDATE.Text = "일자";
            this.LBL51_GDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FPS91_TY_S_AC_24K5P796);
            this.groupBox1.Controls.Add(this.CBH01_GCDBK);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_GDATE);
            this.groupBox1.Controls.Add(this.DTP01_GDATE);
            this.groupBox1.Controls.Add(this.BTN61_PRT);
            this.groupBox1.Controls.Add(this.LBL51_GCDBK);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // CBH01_GCDBK
            // 
            this.CBH01_GCDBK.BindedDataRow = null;
            this.CBH01_GCDBK.CodeBoxWidth = 0;
            this.CBH01_GCDBK.DummyValue = null;
            this.CBH01_GCDBK.FactoryID = "";
            this.CBH01_GCDBK.FactoryName = null;
            this.CBH01_GCDBK.Location = new System.Drawing.Point(111, 12);
            this.CBH01_GCDBK.MinLength = 0;
            this.CBH01_GCDBK.Name = "CBH01_GCDBK";
            this.CBH01_GCDBK.Size = new System.Drawing.Size(339, 20);
            this.CBH01_GCDBK.TabIndex = 7;
            // 
            // FPS91_TY_S_AC_24K5P796
            // 
            this.FPS91_TY_S_AC_24K5P796.AccessibleDescription = "";
            this.FPS91_TY_S_AC_24K5P796.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_24K5P796.FactoryID = "";
            this.FPS91_TY_S_AC_24K5P796.FactoryName = null;
            this.FPS91_TY_S_AC_24K5P796.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_24K5P796.Name = "FPS91_TY_S_AC_24K5P796";
            this.FPS91_TY_S_AC_24K5P796.PopMenuVisible = false;
            this.FPS91_TY_S_AC_24K5P796.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_24K5P796.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_24K5P796_Sheet1});
            this.FPS91_TY_S_AC_24K5P796.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_AC_24K5P796.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_24K5P796.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_24K5P796_Sheet1
            // 
            this.FPS91_TY_S_AC_24K5P796_Sheet1.Reset();
            this.FPS91_TY_S_AC_24K5P796_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_24K5P796_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_24K5P796_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_24K5P796_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYACDE009S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACDE009S";
            this.Load += new System.EventHandler(this.TYACDE009S_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24K5P796)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24K5P796_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYLabel LBL51_GCDBK;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GDATE;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYCodeBox CBH01_GCDBK;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_24K5P796;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_24K5P796_Sheet1;
    }
}