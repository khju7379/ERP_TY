namespace TY.ER.US00
{
    partial class TYUSME062I
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
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_US_92CE5728 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_92CE5728_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92CE5728)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92CE5728_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(584, 12);
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
            this.groupBox1.Controls.Add(this.BTN61_BATCH);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.LBL51_STDATE);
            this.groupBox1.Controls.Add(this.DTP01_EDDATE);
            this.groupBox1.Controls.Add(this.DTP01_STDATE);
            this.groupBox1.Controls.Add(this.groupBox27);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(827, 339);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(665, 12);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 368;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(746, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 367;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 223;
            this.LBL51_STDATE.Text = "일 자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 222;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 221;
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.FPS91_TY_S_US_92CE5728);
            this.groupBox27.Font = new System.Drawing.Font("굴림", 9F);
            this.groupBox27.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox27.Location = new System.Drawing.Point(0, 39);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(827, 300);
            this.groupBox27.TabIndex = 220;
            this.groupBox27.TabStop = false;
            // 
            // FPS91_TY_S_US_92CE5728
            // 
            this.FPS91_TY_S_US_92CE5728.AccessibleDescription = "FPS91_TY_S_US_92CE5728";
            this.FPS91_TY_S_US_92CE5728.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_92CE5728.FactoryID = "";
            this.FPS91_TY_S_US_92CE5728.FactoryName = null;
            this.FPS91_TY_S_US_92CE5728.Location = new System.Drawing.Point(0, 20);
            this.FPS91_TY_S_US_92CE5728.Name = "FPS91_TY_S_US_92CE5728";
            this.FPS91_TY_S_US_92CE5728.PopMenuVisible = false;
            this.FPS91_TY_S_US_92CE5728.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_92CE5728.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_92CE5728_Sheet1});
            this.FPS91_TY_S_US_92CE5728.Size = new System.Drawing.Size(827, 274);
            this.FPS91_TY_S_US_92CE5728.TabIndex = 180;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_92CE5728.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_US_92CE5728_Sheet1
            // 
            this.FPS91_TY_S_US_92CE5728_Sheet1.Reset();
            this.FPS91_TY_S_US_92CE5728_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_92CE5728_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_92CE5728_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_92CE5728_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 121;
            this.label2.Text = "-";
            // 
            // TYUSME062I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 341);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUSME062I";
            this.Load += new System.EventHandler(this.TYUSME062I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92CE5728)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92CE5728_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox27;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_92CE5728;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_92CE5728_Sheet1;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYLabel LBL51_STDATE;
        private Service.Library.Controls.TYButton BTN61_CLO;
        private Service.Library.Controls.TYButton BTN61_BATCH;
    }
}