namespace TY.ER.AC00
{
    partial class TYACDE010S
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
            this.CBO01_GCDBK = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GCDBK = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GNOAC = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GNOAC = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GEDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_GSTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTDATE = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_AC_24O3N834 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_24O3N834_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24O3N834)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24O3N834_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1010, 12);
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
            // CBO01_GCDBK
            // 
            this.CBO01_GCDBK.FactoryID = "";
            this.CBO01_GCDBK.FactoryName = null;
            this.CBO01_GCDBK.Location = new System.Drawing.Point(451, 12);
            this.CBO01_GCDBK.Name = "CBO01_GCDBK";
            this.CBO01_GCDBK.Size = new System.Drawing.Size(226, 20);
            this.CBO01_GCDBK.TabIndex = 2;
            this.CBO01_GCDBK.SelectedIndexChanged += new System.EventHandler(this.CBO01_GCDBK_SelectedIndexChanged);
            // 
            // LBL51_GCDBK
            // 
            this.LBL51_GCDBK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GCDBK.FactoryID = "";
            this.LBL51_GCDBK.FactoryName = null;
            this.LBL51_GCDBK.Location = new System.Drawing.Point(345, 12);
            this.LBL51_GCDBK.Name = "LBL51_GCDBK";
            this.LBL51_GCDBK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GCDBK.TabIndex = 3;
            this.LBL51_GCDBK.Text = "은행코드";
            this.LBL51_GCDBK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GNOAC
            // 
            this.CBO01_GNOAC.FactoryID = "";
            this.CBO01_GNOAC.FactoryName = null;
            this.CBO01_GNOAC.Location = new System.Drawing.Point(791, 12);
            this.CBO01_GNOAC.Name = "CBO01_GNOAC";
            this.CBO01_GNOAC.Size = new System.Drawing.Size(170, 20);
            this.CBO01_GNOAC.TabIndex = 4;
            // 
            // LBL51_GNOAC
            // 
            this.LBL51_GNOAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GNOAC.FactoryID = "";
            this.LBL51_GNOAC.FactoryName = null;
            this.LBL51_GNOAC.Location = new System.Drawing.Point(685, 12);
            this.LBL51_GNOAC.Name = "LBL51_GNOAC";
            this.LBL51_GNOAC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GNOAC.TabIndex = 5;
            this.LBL51_GNOAC.Text = "계좌번호";
            this.LBL51_GNOAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GEDDATE
            // 
            this.DTP01_GEDDATE.FactoryID = "";
            this.DTP01_GEDDATE.FactoryName = null;
            this.DTP01_GEDDATE.Location = new System.Drawing.Point(237, 12);
            this.DTP01_GEDDATE.Name = "DTP01_GEDDATE";
            this.DTP01_GEDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GEDDATE.TabIndex = 6;
            // 
            // DTP01_GSTDATE
            // 
            this.DTP01_GSTDATE.FactoryID = "";
            this.DTP01_GSTDATE.FactoryName = null;
            this.DTP01_GSTDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_GSTDATE.Name = "DTP01_GSTDATE";
            this.DTP01_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTDATE.TabIndex = 8;
            // 
            // LBL51_GSTDATE
            // 
            this.LBL51_GSTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTDATE.FactoryID = "";
            this.LBL51_GSTDATE.FactoryName = null;
            this.LBL51_GSTDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GSTDATE.Name = "LBL51_GSTDATE";
            this.LBL51_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTDATE.TabIndex = 9;
            this.LBL51_GSTDATE.Text = "일자";
            this.LBL51_GSTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FPS91_TY_S_AC_24O3N834);
            this.groupBox1.Controls.Add(this.CBO01_GNOAC);
            this.groupBox1.Controls.Add(this.LBL51_GNOAC);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_GSTDATE);
            this.groupBox1.Controls.Add(this.BTN61_PRT);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CBO01_GCDBK);
            this.groupBox1.Controls.Add(this.LBL51_GCDBK);
            this.groupBox1.Controls.Add(this.DTP01_GSTDATE);
            this.groupBox1.Controls.Add(this.DTP01_GEDDATE);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "~";
            // 
            // FPS91_TY_S_AC_24O3N834
            // 
            this.FPS91_TY_S_AC_24O3N834.AccessibleDescription = "";
            this.FPS91_TY_S_AC_24O3N834.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_24O3N834.FactoryID = "";
            this.FPS91_TY_S_AC_24O3N834.FactoryName = null;
            this.FPS91_TY_S_AC_24O3N834.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_24O3N834.Name = "FPS91_TY_S_AC_24O3N834";
            this.FPS91_TY_S_AC_24O3N834.PopMenuVisible = false;
            this.FPS91_TY_S_AC_24O3N834.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_24O3N834.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_24O3N834_Sheet1});
            this.FPS91_TY_S_AC_24O3N834.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_AC_24O3N834.TabIndex = 11;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_24O3N834.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_24O3N834_Sheet1
            // 
            this.FPS91_TY_S_AC_24O3N834_Sheet1.Reset();
            this.FPS91_TY_S_AC_24O3N834_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_24O3N834_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_24O3N834_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_24O3N834_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYACDE010S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACDE010S";
            this.Load += new System.EventHandler(this.TYACDE010S_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24O3N834)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_24O3N834_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYComboBox CBO01_GCDBK;
        private TY.Service.Library.Controls.TYLabel LBL51_GCDBK;
        private TY.Service.Library.Controls.TYComboBox CBO01_GNOAC;
        private TY.Service.Library.Controls.TYLabel LBL51_GNOAC;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GEDDATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTDATE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_24O3N834;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_24O3N834_Sheet1;
    }
}