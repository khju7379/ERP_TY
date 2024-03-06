namespace TY.ER.AC00
{
    partial class TYACNC012B
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_AC_296AJ855 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_296AJ855_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.TXT01_GTADDROW = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_GTADDROW = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_BTNADDROW = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_296AJ855)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_296AJ855_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(544, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 0;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // FPS91_TY_S_AC_296AJ855
            // 
            this.FPS91_TY_S_AC_296AJ855.AccessibleDescription = "";
            this.FPS91_TY_S_AC_296AJ855.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_296AJ855.FactoryID = "";
            this.FPS91_TY_S_AC_296AJ855.FactoryName = null;
            this.FPS91_TY_S_AC_296AJ855.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_296AJ855.Name = "FPS91_TY_S_AC_296AJ855";
            this.FPS91_TY_S_AC_296AJ855.PopMenuVisible = false;
            this.FPS91_TY_S_AC_296AJ855.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_296AJ855.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_296AJ855_Sheet1});
            this.FPS91_TY_S_AC_296AJ855.Size = new System.Drawing.Size(704, 611);
            this.FPS91_TY_S_AC_296AJ855.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_296AJ855.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_296AJ855_Sheet1
            // 
            this.FPS91_TY_S_AC_296AJ855_Sheet1.Reset();
            this.FPS91_TY_S_AC_296AJ855_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_296AJ855_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_296AJ855_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_296AJ855_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BTNADDROW);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_GTADDROW);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GTADDROW);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_296AJ855);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(706, 660);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TXT01_GTADDROW
            // 
            this.TXT01_GTADDROW.FactoryID = "";
            this.TXT01_GTADDROW.FactoryName = null;
            this.TXT01_GTADDROW.Location = new System.Drawing.Point(111, 12);
            this.TXT01_GTADDROW.MinLength = 0;
            this.TXT01_GTADDROW.Name = "TXT01_GTADDROW";
            this.TXT01_GTADDROW.Size = new System.Drawing.Size(73, 21);
            this.TXT01_GTADDROW.TabIndex = 10;
            // 
            // LBL51_GTADDROW
            // 
            this.LBL51_GTADDROW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GTADDROW.FactoryID = "";
            this.LBL51_GTADDROW.FactoryName = null;
            this.LBL51_GTADDROW.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GTADDROW.Name = "LBL51_GTADDROW";
            this.LBL51_GTADDROW.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GTADDROW.TabIndex = 11;
            this.LBL51_GTADDROW.Text = "빈칸갯수";
            this.LBL51_GTADDROW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_BTNADDROW
            // 
            this.BTN61_BTNADDROW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BTNADDROW.FactoryID = "";
            this.BTN61_BTNADDROW.FactoryName = null;
            this.BTN61_BTNADDROW.Location = new System.Drawing.Point(190, 12);
            this.BTN61_BTNADDROW.Name = "BTN61_BTNADDROW";
            this.BTN61_BTNADDROW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BTNADDROW.TabIndex = 12;
            this.BTN61_BTNADDROW.Text = "저장";
            this.BTN61_BTNADDROW.UseVisualStyleBackColor = true;
            this.BTN61_BTNADDROW.Click += new System.EventHandler(this.BTN61_BTNADDROW_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(625, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 13;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TYACNC012B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 662);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACNC012B";
            this.Load += new System.EventHandler(this.TYACNC012B_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_296AJ855)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_296AJ855_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_296AJ855;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_296AJ855_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT01_GTADDROW;
        private Service.Library.Controls.TYLabel LBL51_GTADDROW;
        private Service.Library.Controls.TYButton BTN61_BTNADDROW;
        private Service.Library.Controls.TYButton BTN61_CLO;
    }
}