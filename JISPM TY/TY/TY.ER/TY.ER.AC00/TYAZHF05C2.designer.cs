namespace TY.ER.AC00
{
    partial class TYAZHF05C2
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
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.TXT01_SMCODE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SMCODE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SMDESC = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SMDESC = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_37G4N118 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_37G4N118_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_37G4N118)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_37G4N118_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(764, 16);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(683, 16);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // TXT01_SMCODE
            // 
            this.TXT01_SMCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SMCODE.FactoryID = "";
            this.TXT01_SMCODE.FactoryName = null;
            this.TXT01_SMCODE.Location = new System.Drawing.Point(112, 17);
            this.TXT01_SMCODE.MinLength = 0;
            this.TXT01_SMCODE.Name = "TXT01_SMCODE";
            this.TXT01_SMCODE.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SMCODE.TabIndex = 2;
            // 
            // LBL51_SMCODE
            // 
            this.LBL51_SMCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SMCODE.FactoryID = "";
            this.LBL51_SMCODE.FactoryName = null;
            this.LBL51_SMCODE.IsCreated = false;
            this.LBL51_SMCODE.Location = new System.Drawing.Point(6, 17);
            this.LBL51_SMCODE.Name = "LBL51_SMCODE";
            this.LBL51_SMCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SMCODE.TabIndex = 3;
            this.LBL51_SMCODE.Text = "소분류 코드";
            this.LBL51_SMCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SMDESC
            // 
            this.TXT01_SMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SMDESC.FactoryID = "";
            this.TXT01_SMDESC.FactoryName = null;
            this.TXT01_SMDESC.Location = new System.Drawing.Point(328, 16);
            this.TXT01_SMDESC.MinLength = 0;
            this.TXT01_SMDESC.Name = "TXT01_SMDESC";
            this.TXT01_SMDESC.Size = new System.Drawing.Size(234, 21);
            this.TXT01_SMDESC.TabIndex = 4;
            // 
            // LBL51_SMDESC
            // 
            this.LBL51_SMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SMDESC.FactoryID = "";
            this.LBL51_SMDESC.FactoryName = null;
            this.LBL51_SMDESC.IsCreated = false;
            this.LBL51_SMDESC.Location = new System.Drawing.Point(222, 16);
            this.LBL51_SMDESC.Name = "LBL51_SMDESC";
            this.LBL51_SMDESC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SMDESC.TabIndex = 5;
            this.LBL51_SMDESC.Text = "소분류명";
            this.LBL51_SMDESC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_37G4N118
            // 
            this.FPS91_TY_S_AC_37G4N118.AccessibleDescription = "";
            this.FPS91_TY_S_AC_37G4N118.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_37G4N118.FactoryID = "";
            this.FPS91_TY_S_AC_37G4N118.FactoryName = null;
            this.FPS91_TY_S_AC_37G4N118.Location = new System.Drawing.Point(0, 44);
            this.FPS91_TY_S_AC_37G4N118.Name = "FPS91_TY_S_AC_37G4N118";
            this.FPS91_TY_S_AC_37G4N118.PopMenuVisible = false;
            this.FPS91_TY_S_AC_37G4N118.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_37G4N118.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_37G4N118_Sheet1});
            this.FPS91_TY_S_AC_37G4N118.Size = new System.Drawing.Size(845, 518);
            this.FPS91_TY_S_AC_37G4N118.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_37G4N118.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_AC_37G4N118.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_AC_37G4N118_CellDoubleClick);
            // 
            // FPS91_TY_S_AC_37G4N118_Sheet1
            // 
            this.FPS91_TY_S_AC_37G4N118_Sheet1.Reset();
            this.FPS91_TY_S_AC_37G4N118_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_37G4N118_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_37G4N118_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_37G4N118_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SMCODE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SMCODE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SMDESC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SMDESC);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_37G4N118);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(845, 568);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYAZHF05C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 575);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYAZHF05C2";
            this.Load += new System.EventHandler(this.TYAZHF05C2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_37G4N118)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_37G4N118_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYTextBox TXT01_SMCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_SMCODE;
        private TY.Service.Library.Controls.TYTextBox TXT01_SMDESC;
        private TY.Service.Library.Controls.TYLabel LBL51_SMDESC;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_37G4N118;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_37G4N118_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}