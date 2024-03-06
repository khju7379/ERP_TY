namespace TY.ER.US00
{
    partial class TYUSKB008S
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
            this.LBL51_JBHANGCHA = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.TXT01_JBBLNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_JBBLNO = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_JBHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_JBHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_JBGOKJONG = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_JBGOKJONG = new TY.Service.Library.Controls.TYLabel();
            this.CBH02_JBHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_JBHANGCHA = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_US_92FGH784 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_92FGH784_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92FGH784)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92FGH784_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // LBL51_JBHANGCHA
            // 
            this.LBL51_JBHANGCHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_JBHANGCHA.FactoryID = "";
            this.LBL51_JBHANGCHA.FactoryName = null;
            this.LBL51_JBHANGCHA.ForeColor = System.Drawing.Color.White;
            this.LBL51_JBHANGCHA.IsCreated = false;
            this.LBL51_JBHANGCHA.Location = new System.Drawing.Point(5, 12);
            this.LBL51_JBHANGCHA.Name = "LBL51_JBHANGCHA";
            this.LBL51_JBHANGCHA.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBHANGCHA.TabIndex = 6;
            this.LBL51_JBHANGCHA.Text = "항 차";
            this.LBL51_JBHANGCHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.TXT01_JBBLNO);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_JBBLNO);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_JBHWAJU);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_JBHWAJU);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_JBGOKJONG);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_JBGOKJONG);
            this.GRP01_SEARCH.Controls.Add(this.CBH02_JBHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.CBH01_JBHANGCHA);
            this.GRP01_SEARCH.Controls.Add(this.label1);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_US_92FGH784);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_JBHANGCHA);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // TXT01_JBBLNO
            // 
            this.TXT01_JBBLNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_JBBLNO.FactoryID = "";
            this.TXT01_JBBLNO.FactoryName = null;
            this.TXT01_JBBLNO.Location = new System.Drawing.Point(653, 38);
            this.TXT01_JBBLNO.MinLength = 0;
            this.TXT01_JBBLNO.Name = "TXT01_JBBLNO";
            this.TXT01_JBBLNO.Size = new System.Drawing.Size(183, 21);
            this.TXT01_JBBLNO.TabIndex = 345;
            this.TXT01_JBBLNO.TabIndexCustom = false;
            // 
            // LBL51_JBBLNO
            // 
            this.LBL51_JBBLNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_JBBLNO.FactoryID = "";
            this.LBL51_JBBLNO.FactoryName = null;
            this.LBL51_JBBLNO.ForeColor = System.Drawing.Color.White;
            this.LBL51_JBBLNO.IsCreated = false;
            this.LBL51_JBBLNO.Location = new System.Drawing.Point(547, 38);
            this.LBL51_JBBLNO.Name = "LBL51_JBBLNO";
            this.LBL51_JBBLNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBBLNO.TabIndex = 344;
            this.LBL51_JBBLNO.Text = "B/L번호";
            this.LBL51_JBBLNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_JBHWAJU
            // 
            this.LBL51_JBHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_JBHWAJU.FactoryID = "";
            this.LBL51_JBHWAJU.FactoryName = null;
            this.LBL51_JBHWAJU.ForeColor = System.Drawing.Color.White;
            this.LBL51_JBHWAJU.IsCreated = false;
            this.LBL51_JBHWAJU.Location = new System.Drawing.Point(5, 38);
            this.LBL51_JBHWAJU.Name = "LBL51_JBHWAJU";
            this.LBL51_JBHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBHWAJU.TabIndex = 343;
            this.LBL51_JBHWAJU.Text = "곡 종";
            this.LBL51_JBHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_JBHWAJU
            // 
            this.CBH01_JBHWAJU.BindedDataRow = null;
            this.CBH01_JBHWAJU.CodeBoxWidth = 0;
            this.CBH01_JBHWAJU.DummyValue = null;
            this.CBH01_JBHWAJU.FactoryID = "";
            this.CBH01_JBHWAJU.FactoryName = null;
            this.CBH01_JBHWAJU.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_JBHWAJU.Location = new System.Drawing.Point(111, 38);
            this.CBH01_JBHWAJU.MinLength = 0;
            this.CBH01_JBHWAJU.Name = "CBH01_JBHWAJU";
            this.CBH01_JBHWAJU.Size = new System.Drawing.Size(202, 20);
            this.CBH01_JBHWAJU.TabIndex = 342;
            // 
            // CBH01_JBGOKJONG
            // 
            this.CBH01_JBGOKJONG.BindedDataRow = null;
            this.CBH01_JBGOKJONG.CodeBoxWidth = 0;
            this.CBH01_JBGOKJONG.DummyValue = null;
            this.CBH01_JBGOKJONG.FactoryID = "";
            this.CBH01_JBGOKJONG.FactoryName = null;
            this.CBH01_JBGOKJONG.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_JBGOKJONG.Location = new System.Drawing.Point(653, 12);
            this.CBH01_JBGOKJONG.MinLength = 0;
            this.CBH01_JBGOKJONG.Name = "CBH01_JBGOKJONG";
            this.CBH01_JBGOKJONG.Size = new System.Drawing.Size(183, 20);
            this.CBH01_JBGOKJONG.TabIndex = 341;
            // 
            // LBL51_JBGOKJONG
            // 
            this.LBL51_JBGOKJONG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_JBGOKJONG.FactoryID = "";
            this.LBL51_JBGOKJONG.FactoryName = null;
            this.LBL51_JBGOKJONG.ForeColor = System.Drawing.Color.White;
            this.LBL51_JBGOKJONG.IsCreated = false;
            this.LBL51_JBGOKJONG.Location = new System.Drawing.Point(547, 12);
            this.LBL51_JBGOKJONG.Name = "LBL51_JBGOKJONG";
            this.LBL51_JBGOKJONG.Size = new System.Drawing.Size(100, 21);
            this.LBL51_JBGOKJONG.TabIndex = 340;
            this.LBL51_JBGOKJONG.Text = "곡 종";
            this.LBL51_JBGOKJONG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH02_JBHANGCHA
            // 
            this.CBH02_JBHANGCHA.BindedDataRow = null;
            this.CBH02_JBHANGCHA.CodeBoxWidth = 0;
            this.CBH02_JBHANGCHA.DummyValue = null;
            this.CBH02_JBHANGCHA.FactoryID = "";
            this.CBH02_JBHANGCHA.FactoryName = null;
            this.CBH02_JBHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH02_JBHANGCHA.Location = new System.Drawing.Point(339, 12);
            this.CBH02_JBHANGCHA.MinLength = 0;
            this.CBH02_JBHANGCHA.Name = "CBH02_JBHANGCHA";
            this.CBH02_JBHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH02_JBHANGCHA.TabIndex = 339;
            // 
            // CBH01_JBHANGCHA
            // 
            this.CBH01_JBHANGCHA.BindedDataRow = null;
            this.CBH01_JBHANGCHA.CodeBoxWidth = 0;
            this.CBH01_JBHANGCHA.DummyValue = null;
            this.CBH01_JBHANGCHA.FactoryID = "";
            this.CBH01_JBHANGCHA.FactoryName = null;
            this.CBH01_JBHANGCHA.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CBH01_JBHANGCHA.Location = new System.Drawing.Point(111, 12);
            this.CBH01_JBHANGCHA.MinLength = 0;
            this.CBH01_JBHANGCHA.Name = "CBH01_JBHANGCHA";
            this.CBH01_JBHANGCHA.Size = new System.Drawing.Size(202, 20);
            this.CBH01_JBHANGCHA.TabIndex = 338;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 88;
            this.label1.Text = "~";
            // 
            // FPS91_TY_S_US_92FGH784
            // 
            this.FPS91_TY_S_US_92FGH784.AccessibleDescription = "";
            this.FPS91_TY_S_US_92FGH784.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_92FGH784.FactoryID = "";
            this.FPS91_TY_S_US_92FGH784.FactoryName = null;
            this.FPS91_TY_S_US_92FGH784.Location = new System.Drawing.Point(1, 64);
            this.FPS91_TY_S_US_92FGH784.Name = "FPS91_TY_S_US_92FGH784";
            this.FPS91_TY_S_US_92FGH784.PopMenuVisible = false;
            this.FPS91_TY_S_US_92FGH784.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_92FGH784.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_92FGH784_Sheet1});
            this.FPS91_TY_S_US_92FGH784.Size = new System.Drawing.Size(1175, 796);
            this.FPS91_TY_S_US_92FGH784.TabIndex = 47;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_92FGH784.TextTipAppearance = tipAppearance2;
            // 
            // FPS91_TY_S_US_92FGH784_Sheet1
            // 
            this.FPS91_TY_S_US_92FGH784_Sheet1.Reset();
            this.FPS91_TY_S_US_92FGH784_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_92FGH784_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_92FGH784_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_92FGH784_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYUSKB008S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSKB008S";
            this.Load += new System.EventHandler(this.TYUSKB008S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92FGH784)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92FGH784_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_JBHANGCHA;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_92FGH784;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_92FGH784_Sheet1;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYCodeBox CBH02_JBHANGCHA;
        private Service.Library.Controls.TYCodeBox CBH01_JBHANGCHA;
        private Service.Library.Controls.TYLabel LBL51_JBGOKJONG;
        private Service.Library.Controls.TYCodeBox CBH01_JBGOKJONG;
        private Service.Library.Controls.TYLabel LBL51_JBHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_JBHWAJU;
        private Service.Library.Controls.TYLabel LBL51_JBBLNO;
        private Service.Library.Controls.TYTextBox TXT01_JBBLNO;
    }
}