namespace TY.ER.US00
{
    partial class TYUSGA01C1
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
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BINO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BINO = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_US_9A19B269 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_9A19B269_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9A19B269)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9A19B269_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(745, 12);
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
            this.BTN61_INQ.Location = new System.Drawing.Point(664, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 2;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 3;
            this.LBL51_STDATE.Text = "시작일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BINO
            // 
            this.TXT01_BINO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BINO.FactoryID = "";
            this.TXT01_BINO.FactoryName = null;
            this.TXT01_BINO.Location = new System.Drawing.Point(323, 12);
            this.TXT01_BINO.MinLength = 0;
            this.TXT01_BINO.Name = "TXT01_BINO";
            this.TXT01_BINO.Size = new System.Drawing.Size(60, 21);
            this.TXT01_BINO.TabIndex = 4;
            this.TXT01_BINO.TabIndexCustom = false;
            // 
            // LBL51_BINO
            // 
            this.LBL51_BINO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BINO.FactoryID = "";
            this.LBL51_BINO.FactoryName = null;
            this.LBL51_BINO.IsCreated = false;
            this.LBL51_BINO.Location = new System.Drawing.Point(217, 12);
            this.LBL51_BINO.Name = "LBL51_BINO";
            this.LBL51_BINO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BINO.TabIndex = 5;
            this.LBL51_BINO.Text = "BIN";
            this.LBL51_BINO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_US_9A19B269
            // 
            this.FPS91_TY_S_US_9A19B269.AccessibleDescription = "";
            this.FPS91_TY_S_US_9A19B269.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_9A19B269.FactoryID = "";
            this.FPS91_TY_S_US_9A19B269.FactoryName = null;
            this.FPS91_TY_S_US_9A19B269.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_US_9A19B269.Name = "FPS91_TY_S_US_9A19B269";
            this.FPS91_TY_S_US_9A19B269.PopMenuVisible = false;
            this.FPS91_TY_S_US_9A19B269.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_9A19B269.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_9A19B269_Sheet1});
            this.FPS91_TY_S_US_9A19B269.Size = new System.Drawing.Size(824, 315);
            this.FPS91_TY_S_US_9A19B269.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_9A19B269.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_US_9A19B269.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_US_9A19B269_CellDoubleClick);
            this.FPS91_TY_S_US_9A19B269.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FPS91_TY_S_US_9A19B269_KeyPress);
            // 
            // FPS91_TY_S_US_9A19B269_Sheet1
            // 
            this.FPS91_TY_S_US_9A19B269_Sheet1.Reset();
            this.FPS91_TY_S_US_9A19B269_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_9A19B269_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_9A19B269_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_9A19B269_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BINO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BINO);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_US_9A19B269);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(825, 360);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUSGA01C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 361);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSGA01C1";
            this.Load += new System.EventHandler(this.TYUSGA01C1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9A19B269)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9A19B269_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private TY.Service.Library.Controls.TYTextBox TXT01_BINO;
        private TY.Service.Library.Controls.TYLabel LBL51_BINO;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_US_9A19B269;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_9A19B269_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}