namespace TY.ER.ED00
{
    partial class TYEDKB13C2
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
            this.FPS91_TY_S_UT_A4DGP261 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_A4DGP261_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A4DGP261)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A4DGP261_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1195, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // FPS91_TY_S_UT_A4DGP261
            // 
            this.FPS91_TY_S_UT_A4DGP261.AccessibleDescription = "";
            this.FPS91_TY_S_UT_A4DGP261.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_A4DGP261.FactoryID = "";
            this.FPS91_TY_S_UT_A4DGP261.FactoryName = null;
            this.FPS91_TY_S_UT_A4DGP261.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_A4DGP261.Name = "FPS91_TY_S_UT_A4DGP261";
            this.FPS91_TY_S_UT_A4DGP261.PopMenuVisible = false;
            this.FPS91_TY_S_UT_A4DGP261.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_A4DGP261.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_A4DGP261_Sheet1});
            this.FPS91_TY_S_UT_A4DGP261.Size = new System.Drawing.Size(1274, 354);
            this.FPS91_TY_S_UT_A4DGP261.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_A4DGP261.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_UT_A4DGP261_Sheet1
            // 
            this.FPS91_TY_S_UT_A4DGP261_Sheet1.Reset();
            this.FPS91_TY_S_UT_A4DGP261_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_A4DGP261_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_A4DGP261_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_A4DGP261_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_UT_A4DGP261);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1278, 401);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYEDKB13C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 404);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYEDKB13C2";
            this.Load += new System.EventHandler(this.TYEDKB13C2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A4DGP261)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A4DGP261_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_UT_A4DGP261;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_A4DGP261_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}