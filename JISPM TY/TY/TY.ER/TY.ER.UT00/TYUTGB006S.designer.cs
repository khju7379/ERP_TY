namespace TY.ER.UT00
{
    partial class TYUTGB006S
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
            this.LBL51_STIPHANG = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_UT_684EV961 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_684EV961_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DTP01_STIPHANG = new TY.Service.Library.Controls.TYDatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_684EV961)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_684EV961_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1119, 12);
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
            this.BTN61_INQ.Location = new System.Drawing.Point(1038, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // LBL51_STIPHANG
            // 
            this.LBL51_STIPHANG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STIPHANG.FactoryID = "";
            this.LBL51_STIPHANG.FactoryName = null;
            this.LBL51_STIPHANG.IsCreated = false;
            this.LBL51_STIPHANG.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STIPHANG.Name = "LBL51_STIPHANG";
            this.LBL51_STIPHANG.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STIPHANG.TabIndex = 9;
            this.LBL51_STIPHANG.Text = "입항일자";
            this.LBL51_STIPHANG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_UT_684EV961
            // 
            this.FPS91_TY_S_UT_684EV961.AccessibleDescription = "";
            this.FPS91_TY_S_UT_684EV961.FactoryID = "";
            this.FPS91_TY_S_UT_684EV961.FactoryName = null;
            this.FPS91_TY_S_UT_684EV961.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_684EV961.Name = "FPS91_TY_S_UT_684EV961";
            this.FPS91_TY_S_UT_684EV961.PopMenuVisible = false;
            this.FPS91_TY_S_UT_684EV961.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_684EV961.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_684EV961_Sheet1});
            this.FPS91_TY_S_UT_684EV961.Size = new System.Drawing.Size(1200, 327);
            this.FPS91_TY_S_UT_684EV961.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_684EV961.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_UT_684EV961.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_UT_684EV961_CellDoubleClick);
            // 
            // FPS91_TY_S_UT_684EV961_Sheet1
            // 
            this.FPS91_TY_S_UT_684EV961_Sheet1.Reset();
            this.FPS91_TY_S_UT_684EV961_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_684EV961_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_684EV961_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_684EV961_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DTP01_STIPHANG);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.LBL51_STIPHANG);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_UT_684EV961);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1200, 372);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // DTP01_STIPHANG
            // 
            this.DTP01_STIPHANG.FactoryID = "";
            this.DTP01_STIPHANG.FactoryName = null;
            this.DTP01_STIPHANG.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STIPHANG.Name = "DTP01_STIPHANG";
            this.DTP01_STIPHANG.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STIPHANG.TabIndex = 26;
            // 
            // TYUTGB006S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 385);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUTGB006S";
            this.Load += new System.EventHandler(this.TYUTGB006S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_684EV961)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_684EV961_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_STIPHANG;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_UT_684EV961;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_684EV961_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYDatePicker DTP01_STIPHANG;
    }
}