namespace TY.ER.HR00
{
    partial class TYHRPY023S
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
            this.LBL51_PEJKCD = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_PESABUN = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_PEYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_PEYEAR = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_5B5GJ107 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_PESABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_PEJKCD = new TY.Service.Library.Controls.TYCodeBox();
            this.BTN61_INQ_FXL = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_5B5GJ107)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_5B5GJ107_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
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
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 1;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // LBL51_PEJKCD
            // 
            this.LBL51_PEJKCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PEJKCD.FactoryID = "";
            this.LBL51_PEJKCD.FactoryName = null;
            this.LBL51_PEJKCD.IsCreated = false;
            this.LBL51_PEJKCD.Location = new System.Drawing.Point(310, 12);
            this.LBL51_PEJKCD.Name = "LBL51_PEJKCD";
            this.LBL51_PEJKCD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PEJKCD.TabIndex = 3;
            this.LBL51_PEJKCD.Text = "직급";
            this.LBL51_PEJKCD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_PESABUN
            // 
            this.LBL51_PESABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PESABUN.FactoryID = "";
            this.LBL51_PESABUN.FactoryName = null;
            this.LBL51_PESABUN.IsCreated = false;
            this.LBL51_PESABUN.Location = new System.Drawing.Point(5, 12);
            this.LBL51_PESABUN.Name = "LBL51_PESABUN";
            this.LBL51_PESABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PESABUN.TabIndex = 5;
            this.LBL51_PESABUN.Text = "사번";
            this.LBL51_PESABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_PEYEAR
            // 
            this.TXT01_PEYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_PEYEAR.FactoryID = "";
            this.TXT01_PEYEAR.FactoryName = null;
            this.TXT01_PEYEAR.Location = new System.Drawing.Point(720, 12);
            this.TXT01_PEYEAR.MinLength = 0;
            this.TXT01_PEYEAR.Name = "TXT01_PEYEAR";
            this.TXT01_PEYEAR.Size = new System.Drawing.Size(100, 21);
            this.TXT01_PEYEAR.TabIndex = 6;
            this.TXT01_PEYEAR.TabIndexCustom = false;
            // 
            // LBL51_PEYEAR
            // 
            this.LBL51_PEYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PEYEAR.FactoryID = "";
            this.LBL51_PEYEAR.FactoryName = null;
            this.LBL51_PEYEAR.IsCreated = false;
            this.LBL51_PEYEAR.Location = new System.Drawing.Point(614, 12);
            this.LBL51_PEYEAR.Name = "LBL51_PEYEAR";
            this.LBL51_PEYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PEYEAR.TabIndex = 7;
            this.LBL51_PEYEAR.Text = "년도";
            this.LBL51_PEYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_5B5GJ107
            // 
            this.FPS91_TY_S_HR_5B5GJ107.AccessibleDescription = "";
            this.FPS91_TY_S_HR_5B5GJ107.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_5B5GJ107.FactoryID = "";
            this.FPS91_TY_S_HR_5B5GJ107.FactoryName = null;
            this.FPS91_TY_S_HR_5B5GJ107.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_5B5GJ107.Name = "FPS91_TY_S_HR_5B5GJ107";
            this.FPS91_TY_S_HR_5B5GJ107.PopMenuVisible = false;
            this.FPS91_TY_S_HR_5B5GJ107.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_5B5GJ107.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1});
            this.FPS91_TY_S_HR_5B5GJ107.Size = new System.Drawing.Size(1175, 813);
            this.FPS91_TY_S_HR_5B5GJ107.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_5B5GJ107.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_5B5GJ107.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_5B5GJ107_CellDoubleClick);
            // 
            // FPS91_TY_S_HR_5B5GJ107_Sheet1
            // 
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1.Reset();
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_5B5GJ107_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ_FXL);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PEJKCD);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PESABUN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PEJKCD);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PESABUN);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_PEYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PEYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_5B5GJ107);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_PESABUN
            // 
            this.CBH01_PESABUN.BindedDataRow = null;
            this.CBH01_PESABUN.CodeBoxWidth = 0;
            this.CBH01_PESABUN.DummyValue = null;
            this.CBH01_PESABUN.FactoryID = "";
            this.CBH01_PESABUN.FactoryName = null;
            this.CBH01_PESABUN.Location = new System.Drawing.Point(111, 12);
            this.CBH01_PESABUN.MinLength = 0;
            this.CBH01_PESABUN.Name = "CBH01_PESABUN";
            this.CBH01_PESABUN.Size = new System.Drawing.Size(192, 20);
            this.CBH01_PESABUN.TabIndex = 9;
            // 
            // CBH01_PEJKCD
            // 
            this.CBH01_PEJKCD.BindedDataRow = null;
            this.CBH01_PEJKCD.CodeBoxWidth = 0;
            this.CBH01_PEJKCD.DummyValue = null;
            this.CBH01_PEJKCD.FactoryID = "";
            this.CBH01_PEJKCD.FactoryName = null;
            this.CBH01_PEJKCD.Location = new System.Drawing.Point(416, 12);
            this.CBH01_PEJKCD.MinLength = 0;
            this.CBH01_PEJKCD.Name = "CBH01_PEJKCD";
            this.CBH01_PEJKCD.Size = new System.Drawing.Size(192, 20);
            this.CBH01_PEJKCD.TabIndex = 10;
            // 
            // BTN61_INQ_FXL
            // 
            this.BTN61_INQ_FXL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ_FXL.FactoryID = "";
            this.BTN61_INQ_FXL.FactoryName = null;
            this.BTN61_INQ_FXL.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_INQ_FXL.Name = "BTN61_INQ_FXL";
            this.BTN61_INQ_FXL.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ_FXL.TabIndex = 11;
            this.BTN61_INQ_FXL.Text = "호봉인상";
            this.BTN61_INQ_FXL.UseVisualStyleBackColor = true;
            this.BTN61_INQ_FXL.Click += new System.EventHandler(this.BTN61_INQ_FXL_Click);
            // 
            // TYHRPY023S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY023S";
            this.Load += new System.EventHandler(this.TYHRPY023S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_5B5GJ107)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_5B5GJ107_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYLabel LBL51_PEJKCD;
        private TY.Service.Library.Controls.TYLabel LBL51_PESABUN;
        private TY.Service.Library.Controls.TYTextBox TXT01_PEYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_PEYEAR;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_5B5GJ107;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_5B5GJ107_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_PEJKCD;
        private Service.Library.Controls.TYCodeBox CBH01_PESABUN;
        private Service.Library.Controls.TYButton BTN61_INQ_FXL;
    }
}