namespace TY.ER.US00
{
    partial class TYUSNJ016S
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
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.CKB01_INQOPTION = new TY.Service.Library.Controls.TYCheckBox();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.CBH02_CHHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBH01_CHHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CHHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.FPS91_TY_S_US_943DO230 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_943DO230_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_943DO230)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_943DO230_Sheet1)).BeginInit();
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
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 39);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 6;
            this.LBL51_SDATE.Text = "작업년월";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.BTN61_PRT);
            this.GRP01_SEARCH.Controls.Add(this.CKB01_INQOPTION);
            this.GRP01_SEARCH.Controls.Add(this.DTP01_EDATE);
            this.GRP01_SEARCH.Controls.Add(this.label2);
            this.GRP01_SEARCH.Controls.Add(this.CBH02_CHHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.label1);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_CHHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_CHHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.DTP01_SDATE);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_US_943DO230);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SDATE);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // CKB01_INQOPTION
            // 
            this.CKB01_INQOPTION.AutoSize = true;
            this.CKB01_INQOPTION.FactoryID = "";
            this.CKB01_INQOPTION.FactoryName = null;
            this.CKB01_INQOPTION.Location = new System.Drawing.Point(364, 40);
            this.CKB01_INQOPTION.Name = "CKB01_INQOPTION";
            this.CKB01_INQOPTION.Size = new System.Drawing.Size(60, 16);
            this.CKB01_INQOPTION.TabIndex = 351;
            this.CKB01_INQOPTION.Text = "소급분";
            this.CKB01_INQOPTION.UseVisualStyleBackColor = true;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(235, 39);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 350;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 349;
            this.label2.Text = "~";
            // 
            // CBH02_CHHANGCHA
            // 
            this.CBH02_CHHANGCHA.BindedDataRow = null;
            this.CBH02_CHHANGCHA.CodeBoxWidth = 0;
            this.CBH02_CHHANGCHA.DummyValue = null;
            this.CBH02_CHHANGCHA.FactoryID = "";
            this.CBH02_CHHANGCHA.FactoryName = null;
            this.CBH02_CHHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH02_CHHANGCHA.Location = new System.Drawing.Point(337, 12);
            this.CBH02_CHHANGCHA.MinLength = 0;
            this.CBH02_CHHANGCHA.Name = "CBH02_CHHANGCHA";
            this.CBH02_CHHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH02_CHHANGCHA.TabIndex = 348;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 347;
            this.label1.Text = "~";
            // 
            // CBH01_CHHANGCHA
            // 
            this.CBH01_CHHANGCHA.BindedDataRow = null;
            this.CBH01_CHHANGCHA.CodeBoxWidth = 0;
            this.CBH01_CHHANGCHA.DummyValue = null;
            this.CBH01_CHHANGCHA.FactoryID = "";
            this.CBH01_CHHANGCHA.FactoryName = null;
            this.CBH01_CHHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_CHHANGCHA.Location = new System.Drawing.Point(109, 12);
            this.CBH01_CHHANGCHA.MinLength = 0;
            this.CBH01_CHHANGCHA.Name = "CBH01_CHHANGCHA";
            this.CBH01_CHHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH01_CHHANGCHA.TabIndex = 346;
            // 
            // LBL51_CHHANGCHA
            // 
            this.LBL51_CHHANGCHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CHHANGCHA.FactoryID = "";
            this.LBL51_CHHANGCHA.FactoryName = null;
            this.LBL51_CHHANGCHA.ForeColor = System.Drawing.Color.White;
            this.LBL51_CHHANGCHA.IsCreated = false;
            this.LBL51_CHHANGCHA.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CHHANGCHA.Name = "LBL51_CHHANGCHA";
            this.LBL51_CHHANGCHA.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHHANGCHA.TabIndex = 345;
            this.LBL51_CHHANGCHA.Text = "항  차";
            this.LBL51_CHHANGCHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(109, 39);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 344;
            // 
            // FPS91_TY_S_US_943DO230
            // 
            this.FPS91_TY_S_US_943DO230.AccessibleDescription = "";
            this.FPS91_TY_S_US_943DO230.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_943DO230.FactoryID = "";
            this.FPS91_TY_S_US_943DO230.FactoryName = null;
            this.FPS91_TY_S_US_943DO230.Location = new System.Drawing.Point(1, 66);
            this.FPS91_TY_S_US_943DO230.Name = "FPS91_TY_S_US_943DO230";
            this.FPS91_TY_S_US_943DO230.PopMenuVisible = false;
            this.FPS91_TY_S_US_943DO230.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_943DO230.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_943DO230_Sheet1});
            this.FPS91_TY_S_US_943DO230.Size = new System.Drawing.Size(1175, 794);
            this.FPS91_TY_S_US_943DO230.TabIndex = 47;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_943DO230.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_US_943DO230_Sheet1
            // 
            this.FPS91_TY_S_US_943DO230_Sheet1.Reset();
            this.FPS91_TY_S_US_943DO230_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_943DO230_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_943DO230_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_943DO230_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 352;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TYUSNJ016S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSNJ016S";
            this.Load += new System.EventHandler(this.TYUSNJ016S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_943DO230)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_943DO230_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_943DO230;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_943DO230_Sheet1;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private Service.Library.Controls.TYLabel LBL51_CHHANGCHA;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private System.Windows.Forms.Label label2;
        private Service.Library.Controls.TYCodeBox CBH02_CHHANGCHA;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCodeBox CBH01_CHHANGCHA;
        private Service.Library.Controls.TYCheckBox CKB01_INQOPTION;
        private Service.Library.Controls.TYButton BTN61_PRT;
    }
}