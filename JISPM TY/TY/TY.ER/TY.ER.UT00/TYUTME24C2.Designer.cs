namespace TY.ER.UT00
{
    partial class TYUTME24C2
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.CBH01_MCHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_MCHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_MCDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_MCDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_UT_767DA720 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_767DA720_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.CBH01_IHHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.GBX80_CONTROLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_767DA720)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_767DA720_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1095, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // CBH01_MCHWAJU
            // 
            this.CBH01_MCHWAJU.BindedDataRow = null;
            this.CBH01_MCHWAJU.CodeBoxWidth = 0;
            this.CBH01_MCHWAJU.DummyValue = null;
            this.CBH01_MCHWAJU.FactoryID = "";
            this.CBH01_MCHWAJU.FactoryName = null;
            this.CBH01_MCHWAJU.Location = new System.Drawing.Point(324, 12);
            this.CBH01_MCHWAJU.MinLength = 0;
            this.CBH01_MCHWAJU.Name = "CBH01_MCHWAJU";
            this.CBH01_MCHWAJU.Size = new System.Drawing.Size(219, 20);
            this.CBH01_MCHWAJU.TabIndex = 1;
            // 
            // LBL51_MCHWAJU
            // 
            this.LBL51_MCHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCHWAJU.FactoryID = "";
            this.LBL51_MCHWAJU.FactoryName = null;
            this.LBL51_MCHWAJU.IsCreated = false;
            this.LBL51_MCHWAJU.Location = new System.Drawing.Point(218, 12);
            this.LBL51_MCHWAJU.Name = "LBL51_MCHWAJU";
            this.LBL51_MCHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MCHWAJU.TabIndex = 2;
            this.LBL51_MCHWAJU.Text = "화주";
            this.LBL51_MCHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_MCDATE
            // 
            this.DTP01_MCDATE.FactoryID = "";
            this.DTP01_MCDATE.FactoryName = null;
            this.DTP01_MCDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_MCDATE.Name = "DTP01_MCDATE";
            this.DTP01_MCDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_MCDATE.TabIndex = 3;
            // 
            // LBL51_MCDATE
            // 
            this.LBL51_MCDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCDATE.FactoryID = "";
            this.LBL51_MCDATE.FactoryName = null;
            this.LBL51_MCDATE.IsCreated = false;
            this.LBL51_MCDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_MCDATE.Name = "LBL51_MCDATE";
            this.LBL51_MCDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MCDATE.TabIndex = 4;
            this.LBL51_MCDATE.Text = "매출일자";
            this.LBL51_MCDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_IHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_UT_767DA720);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_MCHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_MCDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1179, 295);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // FPS91_TY_S_UT_767DA720
            // 
            this.FPS91_TY_S_UT_767DA720.AccessibleDescription = "";
            this.FPS91_TY_S_UT_767DA720.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_767DA720.FactoryID = "";
            this.FPS91_TY_S_UT_767DA720.FactoryName = null;
            this.FPS91_TY_S_UT_767DA720.Location = new System.Drawing.Point(1, 39);
            this.FPS91_TY_S_UT_767DA720.Name = "FPS91_TY_S_UT_767DA720";
            this.FPS91_TY_S_UT_767DA720.PopMenuVisible = false;
            this.FPS91_TY_S_UT_767DA720.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_767DA720.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_767DA720_Sheet1});
            this.FPS91_TY_S_UT_767DA720.Size = new System.Drawing.Size(1177, 253);
            this.FPS91_TY_S_UT_767DA720.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_767DA720.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_UT_767DA720_Sheet1
            // 
            this.FPS91_TY_S_UT_767DA720_Sheet1.Reset();
            this.FPS91_TY_S_UT_767DA720_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_767DA720_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_767DA720_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_767DA720_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // CBH01_IHHWAJU
            // 
            this.CBH01_IHHWAJU.BindedDataRow = null;
            this.CBH01_IHHWAJU.CodeBoxWidth = 0;
            this.CBH01_IHHWAJU.DummyValue = null;
            this.CBH01_IHHWAJU.FactoryID = "";
            this.CBH01_IHHWAJU.FactoryName = null;
            this.CBH01_IHHWAJU.Location = new System.Drawing.Point(324, 12);
            this.CBH01_IHHWAJU.MinLength = 0;
            this.CBH01_IHHWAJU.Name = "CBH01_IHHWAJU";
            this.CBH01_IHHWAJU.Size = new System.Drawing.Size(219, 20);
            this.CBH01_IHHWAJU.TabIndex = 11;
            // 
            // TYUTME24C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 299);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME24C2";
            this.Load += new System.EventHandler(this.TYUTME24C2_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_767DA720)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_767DA720_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYCodeBox CBH01_MCHWAJU;
        private TY.Service.Library.Controls.TYLabel LBL51_MCHWAJU;
        private TY.Service.Library.Controls.TYDatePicker DTP01_MCDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_MCDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYSpread FPS91_TY_S_UT_767DA720;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_767DA720_Sheet1;
        private Service.Library.Controls.TYCodeBox CBH01_IHHWAJU;
    }
}