namespace TY.ER.UT00
{
    partial class TYUTGB011S
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
            this.LBL51_TRNUMNO2 = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_UT_6ACLT354 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_6ACLT354_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TXT01_TRNUMNO2 = new TY.Service.Library.Controls.TYTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6ACLT354)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6ACLT354_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(432, 12);
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
            this.BTN61_INQ.Location = new System.Drawing.Point(351, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // LBL51_TRNUMNO2
            // 
            this.LBL51_TRNUMNO2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_TRNUMNO2.FactoryID = "";
            this.LBL51_TRNUMNO2.FactoryName = null;
            this.LBL51_TRNUMNO2.ForeColor = System.Drawing.Color.White;
            this.LBL51_TRNUMNO2.IsCreated = false;
            this.LBL51_TRNUMNO2.Location = new System.Drawing.Point(5, 12);
            this.LBL51_TRNUMNO2.Name = "LBL51_TRNUMNO2";
            this.LBL51_TRNUMNO2.Size = new System.Drawing.Size(100, 21);
            this.LBL51_TRNUMNO2.TabIndex = 9;
            this.LBL51_TRNUMNO2.Text = "차량번호";
            this.LBL51_TRNUMNO2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_UT_6ACLT354
            // 
            this.FPS91_TY_S_UT_6ACLT354.AccessibleDescription = "";
            this.FPS91_TY_S_UT_6ACLT354.FactoryID = "";
            this.FPS91_TY_S_UT_6ACLT354.FactoryName = null;
            this.FPS91_TY_S_UT_6ACLT354.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_6ACLT354.Name = "FPS91_TY_S_UT_6ACLT354";
            this.FPS91_TY_S_UT_6ACLT354.PopMenuVisible = false;
            this.FPS91_TY_S_UT_6ACLT354.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_6ACLT354.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_6ACLT354_Sheet1});
            this.FPS91_TY_S_UT_6ACLT354.Size = new System.Drawing.Size(512, 331);
            this.FPS91_TY_S_UT_6ACLT354.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_6ACLT354.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_UT_6ACLT354.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_UT_6ACLT354_CellDoubleClick);
            this.FPS91_TY_S_UT_6ACLT354.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FPS91_TY_S_UT_6ACLT354_KeyPress);
            // 
            // FPS91_TY_S_UT_6ACLT354_Sheet1
            // 
            this.FPS91_TY_S_UT_6ACLT354_Sheet1.Reset();
            this.FPS91_TY_S_UT_6ACLT354_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_6ACLT354_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_6ACLT354_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_6ACLT354_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TXT01_TRNUMNO2);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.LBL51_TRNUMNO2);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_UT_6ACLT354);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 372);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // TXT01_TRNUMNO2
            // 
            this.TXT01_TRNUMNO2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_TRNUMNO2.FactoryID = "";
            this.TXT01_TRNUMNO2.FactoryName = null;
            this.TXT01_TRNUMNO2.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_TRNUMNO2.Location = new System.Drawing.Point(111, 12);
            this.TXT01_TRNUMNO2.MinLength = 0;
            this.TXT01_TRNUMNO2.Name = "TXT01_TRNUMNO2";
            this.TXT01_TRNUMNO2.Size = new System.Drawing.Size(150, 21);
            this.TXT01_TRNUMNO2.TabIndex = 218;
            this.TXT01_TRNUMNO2.TabIndexCustom = false;
            // 
            // TYUTGB011S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 385);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUTGB011S";
            this.Load += new System.EventHandler(this.TYUTGB011S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6ACLT354)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6ACLT354_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_TRNUMNO2;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_UT_6ACLT354;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_6ACLT354_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYTextBox TXT01_TRNUMNO2;
    }
}