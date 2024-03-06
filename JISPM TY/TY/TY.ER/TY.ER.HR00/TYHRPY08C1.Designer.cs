namespace TY.ER.HR00
{
    partial class TYHRPY08C1
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.CBH01_KBSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_KBSABUN = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_4CTEV990 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_4CTEV990_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CTEV990)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CTEV990_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(703, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // CBH01_KBSABUN
            // 
            this.CBH01_KBSABUN.BindedDataRow = null;
            this.CBH01_KBSABUN.CodeBoxWidth = 0;
            this.CBH01_KBSABUN.DummyValue = null;
            this.CBH01_KBSABUN.FactoryID = "";
            this.CBH01_KBSABUN.FactoryName = null;
            this.CBH01_KBSABUN.Location = new System.Drawing.Point(111, 12);
            this.CBH01_KBSABUN.MinLength = 0;
            this.CBH01_KBSABUN.Name = "CBH01_KBSABUN";
            this.CBH01_KBSABUN.Size = new System.Drawing.Size(150, 20);
            this.CBH01_KBSABUN.TabIndex = 1;
            // 
            // LBL51_KBSABUN
            // 
            this.LBL51_KBSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_KBSABUN.FactoryID = "";
            this.LBL51_KBSABUN.FactoryName = null;
            this.LBL51_KBSABUN.IsCreated = false;
            this.LBL51_KBSABUN.Location = new System.Drawing.Point(5, 12);
            this.LBL51_KBSABUN.Name = "LBL51_KBSABUN";
            this.LBL51_KBSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_KBSABUN.TabIndex = 2;
            this.LBL51_KBSABUN.Text = "사번";
            this.LBL51_KBSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_4CTEV990
            // 
            this.FPS91_TY_S_HR_4CTEV990.AccessibleDescription = "";
            this.FPS91_TY_S_HR_4CTEV990.FactoryID = "";
            this.FPS91_TY_S_HR_4CTEV990.FactoryName = null;
            this.FPS91_TY_S_HR_4CTEV990.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_4CTEV990.Name = "FPS91_TY_S_HR_4CTEV990";
            this.FPS91_TY_S_HR_4CTEV990.PopMenuVisible = false;
            this.FPS91_TY_S_HR_4CTEV990.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_4CTEV990.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_4CTEV990_Sheet1});
            this.FPS91_TY_S_HR_4CTEV990.Size = new System.Drawing.Size(785, 448);
            this.FPS91_TY_S_HR_4CTEV990.TabIndex = 5;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_4CTEV990.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_HR_4CTEV990.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_4CTEV990_CellDoubleClick);
            // 
            // FPS91_TY_S_HR_4CTEV990_Sheet1
            // 
            this.FPS91_TY_S_HR_4CTEV990_Sheet1.Reset();
            this.FPS91_TY_S_HR_4CTEV990_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_4CTEV990_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_4CTEV990_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_4CTEV990_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_KBSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_4CTEV990);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(786, 493);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYHRPY08C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 498);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY08C1";
            this.Load += new System.EventHandler(this.TYHRPY08C1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CTEV990)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CTEV990_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYCodeBox CBH01_KBSABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_KBSABUN;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_4CTEV990;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_4CTEV990_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}