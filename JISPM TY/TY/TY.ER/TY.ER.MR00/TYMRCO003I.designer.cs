namespace TY.ER.MR00
{
    partial class TYMRCO003I
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.CBO01_MLMCODE = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_MLMCODE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_MMDESC = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_MMDESC = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_MR_2B24P045 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_MR_2B24P045_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_MR_3783P030 = new TY.Service.Library.Controls.TYSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2B24P045)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2B24P045_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_3783P030)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(932, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 1;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 2;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBO01_MLMCODE
            // 
            this.CBO01_MLMCODE.FactoryID = "";
            this.CBO01_MLMCODE.FactoryName = null;
            this.CBO01_MLMCODE.Location = new System.Drawing.Point(111, 12);
            this.CBO01_MLMCODE.Name = "CBO01_MLMCODE";
            this.CBO01_MLMCODE.Size = new System.Drawing.Size(250, 20);
            this.CBO01_MLMCODE.TabIndex = 3;
            // 
            // LBL51_MLMCODE
            // 
            this.LBL51_MLMCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MLMCODE.FactoryID = "";
            this.LBL51_MLMCODE.FactoryName = null;
            this.LBL51_MLMCODE.IsCreated = false;
            this.LBL51_MLMCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_MLMCODE.Name = "LBL51_MLMCODE";
            this.LBL51_MLMCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MLMCODE.TabIndex = 4;
            this.LBL51_MLMCODE.Text = "대분류 코드";
            this.LBL51_MLMCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_MMDESC
            // 
            this.TXT01_MMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_MMDESC.FactoryID = "";
            this.TXT01_MMDESC.FactoryName = null;
            this.TXT01_MMDESC.Location = new System.Drawing.Point(473, 12);
            this.TXT01_MMDESC.MinLength = 0;
            this.TXT01_MMDESC.Name = "TXT01_MMDESC";
            this.TXT01_MMDESC.Size = new System.Drawing.Size(250, 21);
            this.TXT01_MMDESC.TabIndex = 5;
            // 
            // LBL51_MMDESC
            // 
            this.LBL51_MMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MMDESC.FactoryID = "";
            this.LBL51_MMDESC.FactoryName = null;
            this.LBL51_MMDESC.IsCreated = false;
            this.LBL51_MMDESC.Location = new System.Drawing.Point(367, 12);
            this.LBL51_MMDESC.Name = "LBL51_MMDESC";
            this.LBL51_MMDESC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MMDESC.TabIndex = 6;
            this.LBL51_MMDESC.Text = "중분류명";
            this.LBL51_MMDESC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_MR_2B24P045
            // 
            this.FPS91_TY_S_MR_2B24P045.AccessibleDescription = "";
            this.FPS91_TY_S_MR_2B24P045.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_MR_2B24P045.FactoryID = "";
            this.FPS91_TY_S_MR_2B24P045.FactoryName = null;
            this.FPS91_TY_S_MR_2B24P045.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_MR_2B24P045.Name = "FPS91_TY_S_MR_2B24P045";
            this.FPS91_TY_S_MR_2B24P045.PopMenuVisible = false;
            this.FPS91_TY_S_MR_2B24P045.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_MR_2B24P045.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_MR_2B24P045_Sheet1});
            this.FPS91_TY_S_MR_2B24P045.Size = new System.Drawing.Size(1175, 411);
            this.FPS91_TY_S_MR_2B24P045.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_MR_2B24P045.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_MR_2B24P045.RowInserted += new Shoveling2010.SmartClient.SystemUtility.Controls.TSpread.TRowEventHandler(this.FPS91_TY_S_MR_2B24P045_RowInserted);
            this.FPS91_TY_S_MR_2B24P045.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_MR_2B24P045_CellDoubleClick);
            // 
            // FPS91_TY_S_MR_2B24P045_Sheet1
            // 
            this.FPS91_TY_S_MR_2B24P045_Sheet1.Reset();
            this.FPS91_TY_S_MR_2B24P045_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_MR_2B24P045_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_MR_2B24P045_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_MR_2B24P045_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_MR_2B24P045);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_MLMCODE);
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Controls.Add(this.BTN61_REM);
            this.groupBox1.Controls.Add(this.CBO01_MLMCODE);
            this.groupBox1.Controls.Add(this.LBL51_MMDESC);
            this.groupBox1.Controls.Add(this.TXT01_MMDESC);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FPS91_TY_S_MR_3783P030);
            this.groupBox2.Location = new System.Drawing.Point(0, 455);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1175, 405);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // FPS91_TY_S_MR_3783P030
            // 
            this.FPS91_TY_S_MR_3783P030.AccessibleDescription = "";
            this.FPS91_TY_S_MR_3783P030.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_MR_3783P030.FactoryID = "";
            this.FPS91_TY_S_MR_3783P030.FactoryName = null;
            this.FPS91_TY_S_MR_3783P030.Location = new System.Drawing.Point(0, 7);
            this.FPS91_TY_S_MR_3783P030.Name = "FPS91_TY_S_MR_3783P030";
            this.FPS91_TY_S_MR_3783P030.PopMenuVisible = false;
            this.FPS91_TY_S_MR_3783P030.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_MR_3783P030.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.FPS91_TY_S_MR_3783P030.Size = new System.Drawing.Size(1175, 398);
            this.FPS91_TY_S_MR_3783P030.TabIndex = 6;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_MR_3783P030.TextTipAppearance = tipAppearance2;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.AutoUpdateNotes = true;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYMRCO003I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYMRCO003I";
            this.Load += new System.EventHandler(this.TYMRCO003I_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2B24P045)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2B24P045_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_3783P030)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYComboBox CBO01_MLMCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_MLMCODE;
        private TY.Service.Library.Controls.TYTextBox TXT01_MMDESC;
        private TY.Service.Library.Controls.TYLabel LBL51_MMDESC;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_MR_2B24P045;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_MR_2B24P045_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Service.Library.Controls.TYSpread FPS91_TY_S_MR_3783P030;
        private FarPoint.Win.Spread.SheetView sheetView1;
    }
}