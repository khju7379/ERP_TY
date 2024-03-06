namespace TY.ER.US00
{
    partial class TYUSPR006S
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
            this.LBL51_IHHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH02_IHHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBH01_IHHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.FPS91_TY_S_US_973BQ989 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_973BQ989_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_973BQ989)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_973BQ989_Sheet1)).BeginInit();
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
            // LBL51_IHHANGCHA
            // 
            this.LBL51_IHHANGCHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_IHHANGCHA.FactoryID = "";
            this.LBL51_IHHANGCHA.FactoryName = null;
            this.LBL51_IHHANGCHA.ForeColor = System.Drawing.Color.White;
            this.LBL51_IHHANGCHA.IsCreated = false;
            this.LBL51_IHHANGCHA.Location = new System.Drawing.Point(5, 12);
            this.LBL51_IHHANGCHA.Name = "LBL51_IHHANGCHA";
            this.LBL51_IHHANGCHA.Size = new System.Drawing.Size(100, 21);
            this.LBL51_IHHANGCHA.TabIndex = 6;
            this.LBL51_IHHANGCHA.Text = "항 차";
            this.LBL51_IHHANGCHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.BTN61_PRT);
            this.GRP01_SEARCH.Controls.Add(this.CBH02_IHHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.label1);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_IHHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_US_973BQ989);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_IHHANGCHA);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 345;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH02_IHHANGCHA
            // 
            this.CBH02_IHHANGCHA.BindedDataRow = null;
            this.CBH02_IHHANGCHA.CodeBoxWidth = 0;
            this.CBH02_IHHANGCHA.DummyValue = null;
            this.CBH02_IHHANGCHA.FactoryID = "";
            this.CBH02_IHHANGCHA.FactoryName = null;
            this.CBH02_IHHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH02_IHHANGCHA.Location = new System.Drawing.Point(339, 12);
            this.CBH02_IHHANGCHA.MinLength = 0;
            this.CBH02_IHHANGCHA.Name = "CBH02_IHHANGCHA";
            this.CBH02_IHHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH02_IHHANGCHA.TabIndex = 344;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 343;
            this.label1.Text = "~";
            // 
            // CBH01_IHHANGCHA
            // 
            this.CBH01_IHHANGCHA.BindedDataRow = null;
            this.CBH01_IHHANGCHA.CodeBoxWidth = 0;
            this.CBH01_IHHANGCHA.DummyValue = null;
            this.CBH01_IHHANGCHA.FactoryID = "";
            this.CBH01_IHHANGCHA.FactoryName = null;
            this.CBH01_IHHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_IHHANGCHA.Location = new System.Drawing.Point(111, 12);
            this.CBH01_IHHANGCHA.MinLength = 0;
            this.CBH01_IHHANGCHA.Name = "CBH01_IHHANGCHA";
            this.CBH01_IHHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH01_IHHANGCHA.TabIndex = 342;
            // 
            // FPS91_TY_S_US_973BQ989
            // 
            this.FPS91_TY_S_US_973BQ989.AccessibleDescription = "";
            this.FPS91_TY_S_US_973BQ989.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_973BQ989.FactoryID = "";
            this.FPS91_TY_S_US_973BQ989.FactoryName = null;
            this.FPS91_TY_S_US_973BQ989.Location = new System.Drawing.Point(1, 39);
            this.FPS91_TY_S_US_973BQ989.Name = "FPS91_TY_S_US_973BQ989";
            this.FPS91_TY_S_US_973BQ989.PopMenuVisible = false;
            this.FPS91_TY_S_US_973BQ989.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_973BQ989.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_973BQ989_Sheet1});
            this.FPS91_TY_S_US_973BQ989.Size = new System.Drawing.Size(1175, 821);
            this.FPS91_TY_S_US_973BQ989.TabIndex = 47;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_973BQ989.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_US_973BQ989_Sheet1
            // 
            this.FPS91_TY_S_US_973BQ989_Sheet1.Reset();
            this.FPS91_TY_S_US_973BQ989_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_973BQ989_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_973BQ989_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_973BQ989_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYUSPR006S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSPR006S";
            this.Load += new System.EventHandler(this.TYUSPR006S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_973BQ989)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_973BQ989_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_IHHANGCHA;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_973BQ989;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_973BQ989_Sheet1;
        private Service.Library.Controls.TYCodeBox CBH01_IHHANGCHA;
        private Service.Library.Controls.TYCodeBox CBH02_IHHANGCHA;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYButton BTN61_PRT;
    }
}