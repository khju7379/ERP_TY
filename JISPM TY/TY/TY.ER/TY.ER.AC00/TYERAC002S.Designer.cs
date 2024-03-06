namespace TY.ER.AC00
{
    partial class TYERAC002S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.DTP01_YYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_YYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_23M9Z823 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_23M9Z823_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX01_SEARCH = new System.Windows.Forms.GroupBox();
            this.LBL51_GSEMOK = new TY.Service.Library.Controls.TYLabel();
            this.GRP02_LIST = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_AC_5B5FX103 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_5B5FX103_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.CBO01_GSEMOK = new TY.Service.Library.Controls.TYComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_23M9Z823)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_23M9Z823_Sheet1)).BeginInit();
            this.GBX01_SEARCH.SuspendLayout();
            this.GRP02_LIST.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_5B5FX103)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_5B5FX103_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1086, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 22);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            this.BTN61_INQ.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_INQ_InvokerStart);
            this.BTN61_INQ.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_INQ_InvokerEnd);
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1167, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 22);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            this.BTN61_PRT.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_PRT_InvokerStart);
            this.BTN61_PRT.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_PRT_InvokerEnd);
            // 
            // DTP01_YYYYMM
            // 
            this.DTP01_YYYYMM.FactoryID = "";
            this.DTP01_YYYYMM.FactoryName = null;
            this.DTP01_YYYYMM.Location = new System.Drawing.Point(112, 12);
            this.DTP01_YYYYMM.Name = "DTP01_YYYYMM";
            this.DTP01_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_YYYYMM.TabIndex = 2;
            // 
            // LBL51_YYYYMM
            // 
            this.LBL51_YYYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_YYYYMM.FactoryID = "";
            this.LBL51_YYYYMM.FactoryName = null;
            this.LBL51_YYYYMM.ForeColor = System.Drawing.Color.White;
            this.LBL51_YYYYMM.IsCreated = false;
            this.LBL51_YYYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_YYYYMM.Name = "LBL51_YYYYMM";
            this.LBL51_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYYYMM.TabIndex = 3;
            this.LBL51_YYYYMM.Text = "기준 년월";
            this.LBL51_YYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_23M9Z823
            // 
            this.FPS91_TY_S_AC_23M9Z823.AccessibleDescription = "";
            this.FPS91_TY_S_AC_23M9Z823.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FPS91_TY_S_AC_23M9Z823.FactoryID = "";
            this.FPS91_TY_S_AC_23M9Z823.FactoryName = null;
            this.FPS91_TY_S_AC_23M9Z823.Location = new System.Drawing.Point(3, 17);
            this.FPS91_TY_S_AC_23M9Z823.Name = "FPS91_TY_S_AC_23M9Z823";
            this.FPS91_TY_S_AC_23M9Z823.PopMenuVisible = false;
            this.FPS91_TY_S_AC_23M9Z823.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_23M9Z823.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_23M9Z823_Sheet1});
            this.FPS91_TY_S_AC_23M9Z823.Size = new System.Drawing.Size(1240, 801);
            this.FPS91_TY_S_AC_23M9Z823.TabIndex = 4;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_23M9Z823.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_AC_23M9Z823_Sheet1
            // 
            this.FPS91_TY_S_AC_23M9Z823_Sheet1.Reset();
            this.FPS91_TY_S_AC_23M9Z823_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_23M9Z823_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_23M9Z823_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_23M9Z823_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX01_SEARCH
            // 
            this.GBX01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX01_SEARCH.Controls.Add(this.CBO01_GSEMOK);
            this.GBX01_SEARCH.Controls.Add(this.LBL51_GSEMOK);
            this.GBX01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GBX01_SEARCH.Controls.Add(this.BTN61_PRT);
            this.GBX01_SEARCH.Controls.Add(this.DTP01_YYYYMM);
            this.GBX01_SEARCH.Controls.Add(this.LBL51_YYYYMM);
            this.GBX01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GBX01_SEARCH.Name = "GBX01_SEARCH";
            this.GBX01_SEARCH.Size = new System.Drawing.Size(1246, 45);
            this.GBX01_SEARCH.TabIndex = 1;
            this.GBX01_SEARCH.TabStop = false;
            // 
            // LBL51_GSEMOK
            // 
            this.LBL51_GSEMOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSEMOK.FactoryID = "";
            this.LBL51_GSEMOK.FactoryName = null;
            this.LBL51_GSEMOK.IsCreated = false;
            this.LBL51_GSEMOK.Location = new System.Drawing.Point(218, 12);
            this.LBL51_GSEMOK.Name = "LBL51_GSEMOK";
            this.LBL51_GSEMOK.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSEMOK.TabIndex = 5;
            this.LBL51_GSEMOK.Text = "계정세목";
            this.LBL51_GSEMOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP02_LIST
            // 
            this.GRP02_LIST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP02_LIST.Controls.Add(this.FPS91_TY_S_AC_5B5FX103);
            this.GRP02_LIST.Controls.Add(this.FPS91_TY_S_AC_23M9Z823);
            this.GRP02_LIST.Location = new System.Drawing.Point(2, 45);
            this.GRP02_LIST.Name = "GRP02_LIST";
            this.GRP02_LIST.Size = new System.Drawing.Size(1246, 821);
            this.GRP02_LIST.TabIndex = 2;
            this.GRP02_LIST.TabStop = false;
            // 
            // FPS91_TY_S_AC_5B5FX103
            // 
            this.FPS91_TY_S_AC_5B5FX103.AccessibleDescription = "";
            this.FPS91_TY_S_AC_5B5FX103.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_5B5FX103.FactoryID = "";
            this.FPS91_TY_S_AC_5B5FX103.FactoryName = null;
            this.FPS91_TY_S_AC_5B5FX103.Location = new System.Drawing.Point(3, 17);
            this.FPS91_TY_S_AC_5B5FX103.Name = "FPS91_TY_S_AC_5B5FX103";
            this.FPS91_TY_S_AC_5B5FX103.PopMenuVisible = false;
            this.FPS91_TY_S_AC_5B5FX103.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_5B5FX103.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_5B5FX103_Sheet1});
            this.FPS91_TY_S_AC_5B5FX103.Size = new System.Drawing.Size(1239, 800);
            this.FPS91_TY_S_AC_5B5FX103.TabIndex = 11;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_5B5FX103.TextTipAppearance = tipAppearance4;
            // 
            // FPS91_TY_S_AC_5B5FX103_Sheet1
            // 
            this.FPS91_TY_S_AC_5B5FX103_Sheet1.Reset();
            this.FPS91_TY_S_AC_5B5FX103_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_5B5FX103_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_5B5FX103_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_5B5FX103_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // CBO01_GSEMOK
            // 
            this.CBO01_GSEMOK.FactoryID = "";
            this.CBO01_GSEMOK.FactoryName = null;
            this.CBO01_GSEMOK.Location = new System.Drawing.Point(324, 12);
            this.CBO01_GSEMOK.Name = "CBO01_GSEMOK";
            this.CBO01_GSEMOK.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GSEMOK.TabIndex = 20;
            // 
            // TYERAC002S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 878);
            this.Controls.Add(this.GBX01_SEARCH);
            this.Controls.Add(this.GRP02_LIST);
            this.Name = "TYERAC002S";
            this.Load += new System.EventHandler(this.TYERAC002S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_23M9Z823)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_23M9Z823_Sheet1)).EndInit();
            this.GBX01_SEARCH.ResumeLayout(false);
            this.GRP02_LIST.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_5B5FX103)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_5B5FX103_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYDatePicker DTP01_YYYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_YYYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_23M9Z823;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_23M9Z823_Sheet1;
		private System.Windows.Forms.GroupBox GBX01_SEARCH;
        private System.Windows.Forms.GroupBox GRP02_LIST;
        private Service.Library.Controls.TYLabel LBL51_GSEMOK;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_5B5FX103;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_5B5FX103_Sheet1;
        private Service.Library.Controls.TYComboBox CBO01_GSEMOK;
    }
}