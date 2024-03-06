namespace TY.ER.UT00
{
    partial class TYUTPS017I
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBO01_CHMSPGUBN1 = new TY.Service.Library.Controls.TYComboBox();
            this.CBO01_CHMEMGUBN1 = new TY.Service.Library.Controls.TYComboBox();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.LBL51_CHMSPGUBN1 = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_CHMEMGUBN1 = new TY.Service.Library.Controls.TYLabel();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_UT_B1FE5328 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_B1FE5328_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.LBL51_CHMCODE = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_CHMCODE = new TY.Service.Library.Controls.TYCodeBox();
            this.groupBox1.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_B1FE5328)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_B1FE5328_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1130, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 2;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LBL51_CHMCODE);
            this.groupBox1.Controls.Add(this.CBH01_CHMCODE);
            this.groupBox1.Controls.Add(this.CBO01_CHMSPGUBN1);
            this.groupBox1.Controls.Add(this.CBO01_CHMEMGUBN1);
            this.groupBox1.Controls.Add(this.BTN61_BATCH);
            this.groupBox1.Controls.Add(this.LBL51_CHMSPGUBN1);
            this.groupBox1.Controls.Add(this.LBL51_CHMEMGUBN1);
            this.groupBox1.Controls.Add(this.groupBox27);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1292, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // CBO01_CHMSPGUBN1
            // 
            this.CBO01_CHMSPGUBN1.FactoryID = "";
            this.CBO01_CHMSPGUBN1.FactoryName = null;
            this.CBO01_CHMSPGUBN1.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_CHMSPGUBN1.Location = new System.Drawing.Point(882, 12);
            this.CBO01_CHMSPGUBN1.Name = "CBO01_CHMSPGUBN1";
            this.CBO01_CHMSPGUBN1.Size = new System.Drawing.Size(180, 20);
            this.CBO01_CHMSPGUBN1.TabIndex = 370;
            // 
            // CBO01_CHMEMGUBN1
            // 
            this.CBO01_CHMEMGUBN1.FactoryID = "";
            this.CBO01_CHMEMGUBN1.FactoryName = null;
            this.CBO01_CHMEMGUBN1.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_CHMEMGUBN1.Location = new System.Drawing.Point(570, 12);
            this.CBO01_CHMEMGUBN1.Name = "CBO01_CHMEMGUBN1";
            this.CBO01_CHMEMGUBN1.Size = new System.Drawing.Size(180, 20);
            this.CBO01_CHMEMGUBN1.TabIndex = 369;
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(1211, 12);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 368;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // LBL51_CHMSPGUBN1
            // 
            this.LBL51_CHMSPGUBN1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CHMSPGUBN1.FactoryID = "";
            this.LBL51_CHMSPGUBN1.FactoryName = null;
            this.LBL51_CHMSPGUBN1.ForeColor = System.Drawing.Color.White;
            this.LBL51_CHMSPGUBN1.IsCreated = false;
            this.LBL51_CHMSPGUBN1.Location = new System.Drawing.Point(756, 12);
            this.LBL51_CHMSPGUBN1.Name = "LBL51_CHMSPGUBN1";
            this.LBL51_CHMSPGUBN1.Size = new System.Drawing.Size(120, 21);
            this.LBL51_CHMSPGUBN1.TabIndex = 225;
            this.LBL51_CHMSPGUBN1.Text = "특별 화학물질 포함";
            this.LBL51_CHMSPGUBN1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_CHMEMGUBN1
            // 
            this.LBL51_CHMEMGUBN1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CHMEMGUBN1.FactoryID = "";
            this.LBL51_CHMEMGUBN1.FactoryName = null;
            this.LBL51_CHMEMGUBN1.ForeColor = System.Drawing.Color.White;
            this.LBL51_CHMEMGUBN1.IsCreated = false;
            this.LBL51_CHMEMGUBN1.Location = new System.Drawing.Point(444, 12);
            this.LBL51_CHMEMGUBN1.Name = "LBL51_CHMEMGUBN1";
            this.LBL51_CHMEMGUBN1.Size = new System.Drawing.Size(120, 21);
            this.LBL51_CHMEMGUBN1.TabIndex = 223;
            this.LBL51_CHMEMGUBN1.Text = "유해 화학물질 포함";
            this.LBL51_CHMEMGUBN1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.FPS91_TY_S_UT_B1FE5328);
            this.groupBox27.Font = new System.Drawing.Font("굴림", 9F);
            this.groupBox27.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox27.Location = new System.Drawing.Point(0, 39);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(1292, 821);
            this.groupBox27.TabIndex = 220;
            this.groupBox27.TabStop = false;
            // 
            // FPS91_TY_S_UT_B1FE5328
            // 
            this.FPS91_TY_S_UT_B1FE5328.AccessibleDescription = "FPS91_TY_S_UT_B1FE5328";
            this.FPS91_TY_S_UT_B1FE5328.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_B1FE5328.FactoryID = "";
            this.FPS91_TY_S_UT_B1FE5328.FactoryName = null;
            this.FPS91_TY_S_UT_B1FE5328.Location = new System.Drawing.Point(0, 20);
            this.FPS91_TY_S_UT_B1FE5328.Name = "FPS91_TY_S_UT_B1FE5328";
            this.FPS91_TY_S_UT_B1FE5328.PopMenuVisible = false;
            this.FPS91_TY_S_UT_B1FE5328.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_B1FE5328.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_B1FE5328_Sheet1});
            this.FPS91_TY_S_UT_B1FE5328.Size = new System.Drawing.Size(1292, 795);
            this.FPS91_TY_S_UT_B1FE5328.TabIndex = 180;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_B1FE5328.TextTipAppearance = tipAppearance2;
            // 
            // FPS91_TY_S_UT_B1FE5328_Sheet1
            // 
            this.FPS91_TY_S_UT_B1FE5328_Sheet1.Reset();
            this.FPS91_TY_S_UT_B1FE5328_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_B1FE5328_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_B1FE5328_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_B1FE5328_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // LBL51_CHMCODE
            // 
            this.LBL51_CHMCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CHMCODE.FactoryID = "";
            this.LBL51_CHMCODE.FactoryName = null;
            this.LBL51_CHMCODE.ForeColor = System.Drawing.Color.White;
            this.LBL51_CHMCODE.IsCreated = false;
            this.LBL51_CHMCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CHMCODE.Name = "LBL51_CHMCODE";
            this.LBL51_CHMCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHMCODE.TabIndex = 372;
            this.LBL51_CHMCODE.Text = "화 물";
            this.LBL51_CHMCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_CHMCODE
            // 
            this.CBH01_CHMCODE.BindedDataRow = null;
            this.CBH01_CHMCODE.CodeBoxWidth = 0;
            this.CBH01_CHMCODE.DummyValue = null;
            this.CBH01_CHMCODE.FactoryID = "";
            this.CBH01_CHMCODE.FactoryName = null;
            this.CBH01_CHMCODE.Location = new System.Drawing.Point(111, 12);
            this.CBH01_CHMCODE.MinLength = 0;
            this.CBH01_CHMCODE.Name = "CBH01_CHMCODE";
            this.CBH01_CHMCODE.Size = new System.Drawing.Size(300, 20);
            this.CBH01_CHMCODE.TabIndex = 371;
            // 
            // TYUTPS017I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUTPS017I";
            this.Load += new System.EventHandler(this.TYUTPS017I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_B1FE5328)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_B1FE5328_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox27;
        private Service.Library.Controls.TYSpread FPS91_TY_S_UT_B1FE5328;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_B1FE5328_Sheet1;
        private Service.Library.Controls.TYLabel LBL51_CHMEMGUBN1;
        private Service.Library.Controls.TYButton BTN61_BATCH;
        private Service.Library.Controls.TYLabel LBL51_CHMSPGUBN1;
        private Service.Library.Controls.TYComboBox CBO01_CHMSPGUBN1;
        private Service.Library.Controls.TYComboBox CBO01_CHMEMGUBN1;
        private Service.Library.Controls.TYLabel LBL51_CHMCODE;
        private Service.Library.Controls.TYCodeBox CBH01_CHMCODE;
    }
}