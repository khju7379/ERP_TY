namespace TY.ER.AC00
{
    partial class TYACNC035S
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
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.DTP01_GSTYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_33J3E343 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_33J3E343_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_33J3E343)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_33J3E343_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
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
            // DTP01_GSTYYMM
            // 
            this.DTP01_GSTYYMM.FactoryID = "";
            this.DTP01_GSTYYMM.FactoryName = null;
            this.DTP01_GSTYYMM.Location = new System.Drawing.Point(111, 12);
            this.DTP01_GSTYYMM.Name = "DTP01_GSTYYMM";
            this.DTP01_GSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTYYMM.TabIndex = 2;
            // 
            // LBL51_GSTYYMM
            // 
            this.LBL51_GSTYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTYYMM.FactoryID = "";
            this.LBL51_GSTYYMM.FactoryName = null;
            this.LBL51_GSTYYMM.IsCreated = false;
            this.LBL51_GSTYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GSTYYMM.Name = "LBL51_GSTYYMM";
            this.LBL51_GSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTYYMM.TabIndex = 3;
            this.LBL51_GSTYYMM.Text = "시작년월";
            this.LBL51_GSTYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_33J3E343
            // 
            this.FPS91_TY_S_AC_33J3E343.AccessibleDescription = "";
            this.FPS91_TY_S_AC_33J3E343.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_33J3E343.FactoryID = "";
            this.FPS91_TY_S_AC_33J3E343.FactoryName = null;
            this.FPS91_TY_S_AC_33J3E343.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_33J3E343.Name = "FPS91_TY_S_AC_33J3E343";
            this.FPS91_TY_S_AC_33J3E343.PopMenuVisible = false;
            this.FPS91_TY_S_AC_33J3E343.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_33J3E343.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_33J3E343_Sheet1});
            this.FPS91_TY_S_AC_33J3E343.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_AC_33J3E343.TabIndex = 4;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_33J3E343.TextTipAppearance = tipAppearance2;
            // 
            // FPS91_TY_S_AC_33J3E343_Sheet1
            // 
            this.FPS91_TY_S_AC_33J3E343_Sheet1.Reset();
            this.FPS91_TY_S_AC_33J3E343_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_33J3E343_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_33J3E343_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_33J3E343_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GSTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GSTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_33J3E343);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYACNC035S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACNC035S";
            this.Load += new System.EventHandler(this.TYACNC035S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_33J3E343)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_33J3E343_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_33J3E343;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_33J3E343_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}