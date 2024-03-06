namespace TY.ER.US00
{
    partial class TYUSME060I
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
            this.MTB01_GEDDATE = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.MTB01_GSTDATE = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.LBL51_GSTDATE = new TY.Service.Library.Controls.TYLabel();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_US_92DCV741 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_92DCV741_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.CBO01_GMEGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GMEGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92DCV741)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92DCV741_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1313, 15);
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
            this.groupBox1.Controls.Add(this.CBO01_GMEGUBUN);
            this.groupBox1.Controls.Add(this.LBL51_GMEGUBUN);
            this.groupBox1.Controls.Add(this.MTB01_GEDDATE);
            this.groupBox1.Controls.Add(this.MTB01_GSTDATE);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BTN61_BATCH);
            this.groupBox1.Controls.Add(this.LBL51_GSTDATE);
            this.groupBox1.Controls.Add(this.groupBox27);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1475, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // MTB01_GEDDATE
            // 
            this.MTB01_GEDDATE.FactoryID = "";
            this.MTB01_GEDDATE.FactoryName = null;
            this.MTB01_GEDDATE.Location = new System.Drawing.Point(209, 15);
            this.MTB01_GEDDATE.Mask = "0000-00-00";
            this.MTB01_GEDDATE.Name = "MTB01_GEDDATE";
            this.MTB01_GEDDATE.Size = new System.Drawing.Size(74, 21);
            this.MTB01_GEDDATE.TabIndex = 376;
            this.MTB01_GEDDATE.ValidatingType = typeof(System.DateTime);
            // 
            // MTB01_GSTDATE
            // 
            this.MTB01_GSTDATE.FactoryID = "";
            this.MTB01_GSTDATE.FactoryName = null;
            this.MTB01_GSTDATE.Location = new System.Drawing.Point(111, 15);
            this.MTB01_GSTDATE.Mask = "0000-00-00";
            this.MTB01_GSTDATE.Name = "MTB01_GSTDATE";
            this.MTB01_GSTDATE.Size = new System.Drawing.Size(74, 21);
            this.MTB01_GSTDATE.TabIndex = 375;
            this.MTB01_GSTDATE.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 373;
            this.label1.Text = "~";
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(1394, 15);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 372;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // LBL51_GSTDATE
            // 
            this.LBL51_GSTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_GSTDATE.FactoryID = "";
            this.LBL51_GSTDATE.FactoryName = null;
            this.LBL51_GSTDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_GSTDATE.IsCreated = false;
            this.LBL51_GSTDATE.Location = new System.Drawing.Point(5, 15);
            this.LBL51_GSTDATE.Name = "LBL51_GSTDATE";
            this.LBL51_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTDATE.TabIndex = 223;
            this.LBL51_GSTDATE.Text = "발생월";
            this.LBL51_GSTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.FPS91_TY_S_US_92DCV741);
            this.groupBox27.Font = new System.Drawing.Font("굴림", 9F);
            this.groupBox27.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox27.Location = new System.Drawing.Point(0, 39);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(1475, 828);
            this.groupBox27.TabIndex = 220;
            this.groupBox27.TabStop = false;
            // 
            // FPS91_TY_S_US_92DCV741
            // 
            this.FPS91_TY_S_US_92DCV741.AccessibleDescription = "FPS91_TY_S_US_92DCV741";
            this.FPS91_TY_S_US_92DCV741.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_92DCV741.FactoryID = "";
            this.FPS91_TY_S_US_92DCV741.FactoryName = null;
            this.FPS91_TY_S_US_92DCV741.Location = new System.Drawing.Point(0, 20);
            this.FPS91_TY_S_US_92DCV741.Name = "FPS91_TY_S_US_92DCV741";
            this.FPS91_TY_S_US_92DCV741.PopMenuVisible = false;
            this.FPS91_TY_S_US_92DCV741.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_92DCV741.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_92DCV741_Sheet1});
            this.FPS91_TY_S_US_92DCV741.Size = new System.Drawing.Size(1475, 802);
            this.FPS91_TY_S_US_92DCV741.TabIndex = 180;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_92DCV741.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_US_92DCV741.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FPS91_TY_S_US_92DCV741_ButtonClicked);
            // 
            // FPS91_TY_S_US_92DCV741_Sheet1
            // 
            this.FPS91_TY_S_US_92DCV741_Sheet1.Reset();
            this.FPS91_TY_S_US_92DCV741_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_92DCV741_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_92DCV741_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_92DCV741_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // CBO01_GMEGUBUN
            // 
            this.CBO01_GMEGUBUN.FactoryID = "";
            this.CBO01_GMEGUBUN.FactoryName = null;
            this.CBO01_GMEGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GMEGUBUN.Location = new System.Drawing.Point(395, 15);
            this.CBO01_GMEGUBUN.Name = "CBO01_GMEGUBUN";
            this.CBO01_GMEGUBUN.Size = new System.Drawing.Size(150, 20);
            this.CBO01_GMEGUBUN.TabIndex = 378;
            // 
            // LBL51_GMEGUBUN
            // 
            this.LBL51_GMEGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_GMEGUBUN.FactoryID = "";
            this.LBL51_GMEGUBUN.FactoryName = null;
            this.LBL51_GMEGUBUN.ForeColor = System.Drawing.Color.White;
            this.LBL51_GMEGUBUN.IsCreated = false;
            this.LBL51_GMEGUBUN.Location = new System.Drawing.Point(289, 15);
            this.LBL51_GMEGUBUN.Name = "LBL51_GMEGUBUN";
            this.LBL51_GMEGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GMEGUBUN.TabIndex = 377;
            this.LBL51_GMEGUBUN.Text = "매출 구분";
            this.LBL51_GMEGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUSME060I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUSME060I";
            this.Load += new System.EventHandler(this.TYUSME060I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92DCV741)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92DCV741_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox27;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_92DCV741;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_92DCV741_Sheet1;
        private Service.Library.Controls.TYLabel LBL51_GSTDATE;
        private Service.Library.Controls.TYButton BTN61_BATCH;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GSTDATE;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GEDDATE;
        private Service.Library.Controls.TYComboBox CBO01_GMEGUBUN;
        private Service.Library.Controls.TYLabel LBL51_GMEGUBUN;
    }
}