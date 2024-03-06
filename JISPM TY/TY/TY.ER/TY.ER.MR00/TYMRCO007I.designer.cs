namespace TY.ER.MR00
{
    partial class TYMRCO007I
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TXT01_FINAME = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_ATTACH_FILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_MR_342BE406 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.BTN61_NEXT = new TY.Service.Library.Controls.TYButton();
            this.BTN61_PRE = new TY.Service.Library.Controls.TYButton();
            this.PBX01_IMG = new TY.Service.Library.Controls.TYPictureBox();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.LBL51_FINAME = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_ATTACH_FILENAME = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.TXT01_FISEQ = new TY.Service.Library.Controls.TYTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_342BE406)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2BFBJ342_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX01_IMG)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(709, 39);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(547, 39);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TXT01_FISEQ);
            this.groupBox1.Controls.Add(this.TXT01_FINAME);
            this.groupBox1.Controls.Add(this.TXT01_ATTACH_FILENAME);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_MR_342BE406);
            this.groupBox1.Controls.Add(this.BTN61_NEXT);
            this.groupBox1.Controls.Add(this.BTN61_PRE);
            this.groupBox1.Controls.Add(this.PBX01_IMG);
            this.groupBox1.Controls.Add(this.BTN61_REM);
            this.groupBox1.Controls.Add(this.LBL51_FINAME);
            this.groupBox1.Controls.Add(this.LBL51_ATTACH_FILENAME);
            this.groupBox1.Controls.Add(this.BTN61_SEARCH);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 649);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // TXT01_FINAME
            // 
            this.TXT01_FINAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_FINAME.Enabled = false;
            this.TXT01_FINAME.FactoryID = "";
            this.TXT01_FINAME.FactoryName = null;
            this.TXT01_FINAME.Location = new System.Drawing.Point(111, 39);
            this.TXT01_FINAME.MinLength = 0;
            this.TXT01_FINAME.Name = "TXT01_FINAME";
            this.TXT01_FINAME.Size = new System.Drawing.Size(231, 21);
            this.TXT01_FINAME.TabIndex = 169;
            // 
            // TXT01_ATTACH_FILENAME
            // 
            this.TXT01_ATTACH_FILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ATTACH_FILENAME.Enabled = false;
            this.TXT01_ATTACH_FILENAME.FactoryID = "";
            this.TXT01_ATTACH_FILENAME.FactoryName = null;
            this.TXT01_ATTACH_FILENAME.Location = new System.Drawing.Point(111, 12);
            this.TXT01_ATTACH_FILENAME.MinLength = 0;
            this.TXT01_ATTACH_FILENAME.Name = "TXT01_ATTACH_FILENAME";
            this.TXT01_ATTACH_FILENAME.Size = new System.Drawing.Size(592, 21);
            this.TXT01_ATTACH_FILENAME.TabIndex = 168;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 167;
            this.label1.Text = "(용량 : 최대 1메가)";
            // 
            // FPS91_TY_S_MR_342BE406
            // 
            this.FPS91_TY_S_MR_342BE406.AccessibleDescription = "FPS91_TY_S_MR_342BE406";
            this.FPS91_TY_S_MR_342BE406.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_MR_342BE406.FactoryID = "";
            this.FPS91_TY_S_MR_342BE406.FactoryName = null;
            this.FPS91_TY_S_MR_342BE406.Location = new System.Drawing.Point(5, 71);
            this.FPS91_TY_S_MR_342BE406.Name = "FPS91_TY_S_MR_342BE406";
            this.FPS91_TY_S_MR_342BE406.PopMenuVisible = false;
            this.FPS91_TY_S_MR_342BE406.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_MR_342BE406.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1});
            this.FPS91_TY_S_MR_342BE406.Size = new System.Drawing.Size(309, 578);
            this.FPS91_TY_S_MR_342BE406.TabIndex = 166;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_MR_342BE406.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_MR_342BE406.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_MR_342BE406_CellDoubleClick);
            // 
            // FPS91_TY_S_MR_2BFBJ342_Sheet1
            // 
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1.Reset();
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_MR_2BFBJ342_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // BTN61_NEXT
            // 
            this.BTN61_NEXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEXT.FactoryID = "";
            this.BTN61_NEXT.FactoryName = null;
            this.BTN61_NEXT.Location = new System.Drawing.Point(595, 612);
            this.BTN61_NEXT.Name = "BTN61_NEXT";
            this.BTN61_NEXT.Size = new System.Drawing.Size(36, 21);
            this.BTN61_NEXT.TabIndex = 88;
            this.BTN61_NEXT.Text = "▶";
            this.BTN61_NEXT.UseVisualStyleBackColor = true;
            this.BTN61_NEXT.Click += new System.EventHandler(this.BTN61_NEXT_Click);
            // 
            // BTN61_PRE
            // 
            this.BTN61_PRE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRE.FactoryID = "";
            this.BTN61_PRE.FactoryName = null;
            this.BTN61_PRE.Location = new System.Drawing.Point(507, 612);
            this.BTN61_PRE.Name = "BTN61_PRE";
            this.BTN61_PRE.Size = new System.Drawing.Size(36, 21);
            this.BTN61_PRE.TabIndex = 87;
            this.BTN61_PRE.Text = "◀";
            this.BTN61_PRE.UseVisualStyleBackColor = true;
            this.BTN61_PRE.Click += new System.EventHandler(this.BTN61_PRE_Click);
            // 
            // PBX01_IMG
            // 
            this.PBX01_IMG.FactoryID = "";
            this.PBX01_IMG.FactoryName = null;
            this.PBX01_IMG.Location = new System.Drawing.Point(320, 71);
            this.PBX01_IMG.Name = "PBX01_IMG";
            this.PBX01_IMG.Size = new System.Drawing.Size(463, 535);
            this.PBX01_IMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBX01_IMG.TabIndex = 86;
            this.PBX01_IMG.TabStop = false;
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(628, 39);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 85;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // LBL51_FINAME
            // 
            this.LBL51_FINAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_FINAME.FactoryID = "";
            this.LBL51_FINAME.FactoryName = null;
            this.LBL51_FINAME.IsCreated = false;
            this.LBL51_FINAME.Location = new System.Drawing.Point(5, 38);
            this.LBL51_FINAME.Name = "LBL51_FINAME";
            this.LBL51_FINAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_FINAME.TabIndex = 52;
            this.LBL51_FINAME.Text = "파일이름";
            this.LBL51_FINAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_ATTACH_FILENAME
            // 
            this.LBL51_ATTACH_FILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ATTACH_FILENAME.FactoryID = "";
            this.LBL51_ATTACH_FILENAME.FactoryName = null;
            this.LBL51_ATTACH_FILENAME.IsCreated = false;
            this.LBL51_ATTACH_FILENAME.Location = new System.Drawing.Point(5, 12);
            this.LBL51_ATTACH_FILENAME.Name = "LBL51_ATTACH_FILENAME";
            this.LBL51_ATTACH_FILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ATTACH_FILENAME.TabIndex = 51;
            this.LBL51_ATTACH_FILENAME.Text = "파일경로";
            this.LBL51_ATTACH_FILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(709, 12);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 50;
            this.BTN61_SEARCH.Text = "찾아보기";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "OpenFile";
            // 
            // TXT01_FISEQ
            // 
            this.TXT01_FISEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_FISEQ.Enabled = false;
            this.TXT01_FISEQ.FactoryID = "";
            this.TXT01_FISEQ.FactoryName = null;
            this.TXT01_FISEQ.Location = new System.Drawing.Point(547, 612);
            this.TXT01_FISEQ.MinLength = 0;
            this.TXT01_FISEQ.Name = "TXT01_FISEQ";
            this.TXT01_FISEQ.Size = new System.Drawing.Size(42, 21);
            this.TXT01_FISEQ.TabIndex = 170;
            // 
            // TYMRCO007I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 662);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYMRCO007I";
            this.Load += new System.EventHandler(this.TYMRCO007I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_342BE406)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_MR_2BFBJ342_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBX01_IMG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYButton BTN61_SEARCH;
        private Service.Library.Controls.TYLabel LBL51_ATTACH_FILENAME;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private Service.Library.Controls.TYLabel LBL51_FINAME;
        private Service.Library.Controls.TYButton BTN61_REM;
        private Service.Library.Controls.TYPictureBox PBX01_IMG;
        private Service.Library.Controls.TYButton BTN61_NEXT;
        private Service.Library.Controls.TYButton BTN61_PRE;
        private Service.Library.Controls.TYSpread FPS91_TY_S_MR_342BE406;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_MR_2BFBJ342_Sheet1;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYTextBox TXT01_ATTACH_FILENAME;
        private Service.Library.Controls.TYTextBox TXT01_FINAME;
        private Service.Library.Controls.TYTextBox TXT01_FISEQ;
    }
}