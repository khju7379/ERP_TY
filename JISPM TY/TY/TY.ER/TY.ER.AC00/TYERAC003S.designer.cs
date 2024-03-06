namespace TY.ER.AC00
{
    partial class TYERAC003S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance9 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.DTP01_YYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_YYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_51M9Z207 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_51M9Z207_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX01_SEARCH = new System.Windows.Forms.GroupBox();
            this.GRP02_LIST = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_51M9Z207)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_51M9Z207_Sheet1)).BeginInit();
            this.GBX01_SEARCH.SuspendLayout();
            this.GRP02_LIST.SuspendLayout();
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
            this.LBL51_YYYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_YYYYMM.Name = "LBL51_YYYYMM";
            this.LBL51_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_YYYYMM.TabIndex = 3;
            this.LBL51_YYYYMM.Text = "기준 년월";
            this.LBL51_YYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_51M9Z207
            // 
            this.FPS91_TY_S_AC_51M9Z207.AccessibleDescription = "";
            this.FPS91_TY_S_AC_51M9Z207.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FPS91_TY_S_AC_51M9Z207.FactoryID = "";
            this.FPS91_TY_S_AC_51M9Z207.FactoryName = null;
            this.FPS91_TY_S_AC_51M9Z207.Location = new System.Drawing.Point(3, 17);
            this.FPS91_TY_S_AC_51M9Z207.Name = "FPS91_TY_S_AC_51M9Z207";
            this.FPS91_TY_S_AC_51M9Z207.PopMenuVisible = false;
            this.FPS91_TY_S_AC_51M9Z207.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_51M9Z207.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_51M9Z207_Sheet1});
            this.FPS91_TY_S_AC_51M9Z207.Size = new System.Drawing.Size(1240, 801);
            this.FPS91_TY_S_AC_51M9Z207.TabIndex = 4;
            tipAppearance9.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_51M9Z207.TextTipAppearance = tipAppearance9;
            // 
            // FPS91_TY_S_AC_51M9Z207_Sheet1
            // 
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.Reset();
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX01_SEARCH
            // 
            this.GBX01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // GRP02_LIST
            // 
            this.GRP02_LIST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP02_LIST.Controls.Add(this.FPS91_TY_S_AC_51M9Z207);
            this.GRP02_LIST.Location = new System.Drawing.Point(2, 45);
            this.GRP02_LIST.Name = "GRP02_LIST";
            this.GRP02_LIST.Size = new System.Drawing.Size(1246, 821);
            this.GRP02_LIST.TabIndex = 2;
            this.GRP02_LIST.TabStop = false;
            // 
            // TYERAC003S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 878);
            this.Controls.Add(this.GBX01_SEARCH);
            this.Controls.Add(this.GRP02_LIST);
            this.Name = "TYERAC003S";
            this.Load += new System.EventHandler(this.TYERAC003S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_51M9Z207)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_51M9Z207_Sheet1)).EndInit();
            this.GBX01_SEARCH.ResumeLayout(false);
            this.GRP02_LIST.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYDatePicker DTP01_YYYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_YYYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_51M9Z207;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_51M9Z207_Sheet1;
		private System.Windows.Forms.GroupBox GBX01_SEARCH;
        private System.Windows.Forms.GroupBox GRP02_LIST;
    }
}