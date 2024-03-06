namespace TY.ER.UT00
{
    partial class TYUTGB022S
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
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_UT_75TG5663 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_75TG5663_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LBL51_SHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_75TG5663)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_75TG5663_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(933, 12);
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
            this.BTN61_INQ.Location = new System.Drawing.Point(852, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
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
            this.LBL51_STDATE.TabIndex = 9;
            this.LBL51_STDATE.Text = "접안일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_UT_75TG5663
            // 
            this.FPS91_TY_S_UT_75TG5663.AccessibleDescription = "";
            this.FPS91_TY_S_UT_75TG5663.FactoryID = "";
            this.FPS91_TY_S_UT_75TG5663.FactoryName = null;
            this.FPS91_TY_S_UT_75TG5663.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_75TG5663.Name = "FPS91_TY_S_UT_75TG5663";
            this.FPS91_TY_S_UT_75TG5663.PopMenuVisible = false;
            this.FPS91_TY_S_UT_75TG5663.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_75TG5663.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_75TG5663_Sheet1});
            this.FPS91_TY_S_UT_75TG5663.Size = new System.Drawing.Size(1013, 327);
            this.FPS91_TY_S_UT_75TG5663.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_75TG5663.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_UT_75TG5663.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_UT_75TG5663_CellDoubleClick);
            // 
            // FPS91_TY_S_UT_75TG5663_Sheet1
            // 
            this.FPS91_TY_S_UT_75TG5663_Sheet1.Reset();
            this.FPS91_TY_S_UT_75TG5663_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_75TG5663_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_75TG5663_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_75TG5663_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LBL51_SHWAJU);
            this.groupBox1.Controls.Add(this.CBH01_SHWAJU);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DTP01_EDDATE);
            this.groupBox1.Controls.Add(this.DTP01_STDATE);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.LBL51_STDATE);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_UT_75TG5663);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1014, 372);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // LBL51_SHWAJU
            // 
            this.LBL51_SHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SHWAJU.FactoryID = "";
            this.LBL51_SHWAJU.FactoryName = null;
            this.LBL51_SHWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_SHWAJU.ForeColor = System.Drawing.Color.White;
            this.LBL51_SHWAJU.IsCreated = false;
            this.LBL51_SHWAJU.Location = new System.Drawing.Point(371, 12);
            this.LBL51_SHWAJU.Name = "LBL51_SHWAJU";
            this.LBL51_SHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SHWAJU.TabIndex = 216;
            this.LBL51_SHWAJU.Text = "화 주";
            this.LBL51_SHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SHWAJU
            // 
            this.CBH01_SHWAJU.BindedDataRow = null;
            this.CBH01_SHWAJU.CodeBoxWidth = 0;
            this.CBH01_SHWAJU.DummyValue = null;
            this.CBH01_SHWAJU.FactoryID = "";
            this.CBH01_SHWAJU.FactoryName = null;
            this.CBH01_SHWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.CBH01_SHWAJU.Location = new System.Drawing.Point(477, 12);
            this.CBH01_SHWAJU.MinLength = 0;
            this.CBH01_SHWAJU.Name = "CBH01_SHWAJU";
            this.CBH01_SHWAJU.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SHWAJU.TabIndex = 215;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "-";
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 28;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 26;
            // 
            // TYUTGB022S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 385);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUTGB022S";
            this.Load += new System.EventHandler(this.TYUTGB022S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_75TG5663)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_75TG5663_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_STDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_UT_75TG5663;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_75TG5663_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYLabel LBL51_SHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_SHWAJU;
    }
}