namespace TY.ER.UT00
{
    partial class TYUTVS002S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance5 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.CBH01_VESLCODE = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_VESLCODE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_UT_6BOFS853 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_6BOFS853_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_NEW = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6BOFS853)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6BOFS853_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(933, 11);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // CBH01_VESLCODE
            // 
            this.CBH01_VESLCODE.BindedDataRow = null;
            this.CBH01_VESLCODE.CodeBoxWidth = 0;
            this.CBH01_VESLCODE.DummyValue = null;
            this.CBH01_VESLCODE.FactoryID = "";
            this.CBH01_VESLCODE.FactoryName = null;
            this.CBH01_VESLCODE.Location = new System.Drawing.Point(111, 12);
            this.CBH01_VESLCODE.MinLength = 0;
            this.CBH01_VESLCODE.Name = "CBH01_VESLCODE";
            this.CBH01_VESLCODE.Size = new System.Drawing.Size(250, 20);
            this.CBH01_VESLCODE.TabIndex = 1;
            // 
            // LBL51_VESLCODE
            // 
            this.LBL51_VESLCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_VESLCODE.FactoryID = "";
            this.LBL51_VESLCODE.FactoryName = null;
            this.LBL51_VESLCODE.IsCreated = false;
            this.LBL51_VESLCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_VESLCODE.Name = "LBL51_VESLCODE";
            this.LBL51_VESLCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_VESLCODE.TabIndex = 2;
            this.LBL51_VESLCODE.Text = "선박코드";
            this.LBL51_VESLCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_UT_6BOFS853
            // 
            this.FPS91_TY_S_UT_6BOFS853.AccessibleDescription = "";
            this.FPS91_TY_S_UT_6BOFS853.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_6BOFS853.FactoryID = "";
            this.FPS91_TY_S_UT_6BOFS853.FactoryName = null;
            this.FPS91_TY_S_UT_6BOFS853.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_6BOFS853.Name = "FPS91_TY_S_UT_6BOFS853";
            this.FPS91_TY_S_UT_6BOFS853.PopMenuVisible = false;
            this.FPS91_TY_S_UT_6BOFS853.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_6BOFS853.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_6BOFS853_Sheet1});
            this.FPS91_TY_S_UT_6BOFS853.Size = new System.Drawing.Size(1174, 815);
            this.FPS91_TY_S_UT_6BOFS853.TabIndex = 3;
            tipAppearance5.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_6BOFS853.TextTipAppearance = tipAppearance5;
            this.FPS91_TY_S_UT_6BOFS853.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_UT_6BOFS853_CellDoubleClick);
            // 
            // FPS91_TY_S_UT_6BOFS853_Sheet1
            // 
            this.FPS91_TY_S_UT_6BOFS853_Sheet1.Reset();
            this.FPS91_TY_S_UT_6BOFS853_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_6BOFS853_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_6BOFS853_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_6BOFS853_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_VESLCODE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_VESLCODE);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_UT_6BOFS853);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1014, 11);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 4;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1095, 11);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 5;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // TYUTVS002S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTVS002S";
            this.Load += new System.EventHandler(this.TYUTVS002S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6BOFS853)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6BOFS853_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYCodeBox CBH01_VESLCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_VESLCODE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_UT_6BOFS853;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_6BOFS853_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_NEW;
        private Service.Library.Controls.TYButton BTN61_REM;
    }
}