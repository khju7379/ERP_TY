namespace TY.ER.HR00
{
    partial class TYHRES002S
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_COPY = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_ORGADD = new TY.Service.Library.Controls.TYButton();
            this.BTN61_ORGDEL = new TY.Service.Library.Controls.TYButton();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.CBH01_PRIOR_ORG_CD = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_PRIOR_ORG_CD = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ENTER_CD = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ENTER_CD = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ORG_CHART_NM = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ORG_CHART_NM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_28N20517 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_28N20517_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FPS91_TY_S_HR_28N21518 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_28N21518_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TRV01_ORG = new TY.Service.Library.Controls.TYTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N20517)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N20517_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N21518)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N21518_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_COPY
            // 
            this.BTN61_COPY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_COPY.FactoryID = "";
            this.BTN61_COPY.FactoryName = null;
            this.BTN61_COPY.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_COPY.Name = "BTN61_COPY";
            this.BTN61_COPY.Size = new System.Drawing.Size(75, 21);
            this.BTN61_COPY.TabIndex = 0;
            this.BTN61_COPY.Text = "복사";
            this.BTN61_COPY.UseVisualStyleBackColor = true;
            this.BTN61_COPY.Click += new System.EventHandler(this.BTN61_COPY_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(843, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_ORGADD
            // 
            this.BTN61_ORGADD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_ORGADD.FactoryID = "";
            this.BTN61_ORGADD.FactoryName = null;
            this.BTN61_ORGADD.Location = new System.Drawing.Point(924, 12);
            this.BTN61_ORGADD.Name = "BTN61_ORGADD";
            this.BTN61_ORGADD.Size = new System.Drawing.Size(80, 21);
            this.BTN61_ORGADD.TabIndex = 2;
            this.BTN61_ORGADD.Text = "조직도저장";
            this.BTN61_ORGADD.UseVisualStyleBackColor = true;
            this.BTN61_ORGADD.Click += new System.EventHandler(this.BTN61_ORGADD_Click);
            // 
            // BTN61_ORGDEL
            // 
            this.BTN61_ORGDEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_ORGDEL.FactoryID = "";
            this.BTN61_ORGDEL.FactoryName = null;
            this.BTN61_ORGDEL.Location = new System.Drawing.Point(1010, 12);
            this.BTN61_ORGDEL.Name = "BTN61_ORGDEL";
            this.BTN61_ORGDEL.Size = new System.Drawing.Size(80, 21);
            this.BTN61_ORGDEL.TabIndex = 3;
            this.BTN61_ORGDEL.Text = "조직도삭제";
            this.BTN61_ORGDEL.UseVisualStyleBackColor = true;
            this.BTN61_ORGDEL.Click += new System.EventHandler(this.BTN61_ORGDEL_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(589, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 4;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(508, 13);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 5;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBH01_PRIOR_ORG_CD
            // 
            this.CBH01_PRIOR_ORG_CD.BindedDataRow = null;
            this.CBH01_PRIOR_ORG_CD.CodeBoxWidth = 0;
            this.CBH01_PRIOR_ORG_CD.DummyValue = null;
            this.CBH01_PRIOR_ORG_CD.FactoryID = "";
            this.CBH01_PRIOR_ORG_CD.FactoryName = null;
            this.CBH01_PRIOR_ORG_CD.Location = new System.Drawing.Point(116, 39);
            this.CBH01_PRIOR_ORG_CD.MinLength = 0;
            this.CBH01_PRIOR_ORG_CD.Name = "CBH01_PRIOR_ORG_CD";
            this.CBH01_PRIOR_ORG_CD.Size = new System.Drawing.Size(211, 20);
            this.CBH01_PRIOR_ORG_CD.TabIndex = 6;
            // 
            // LBL51_PRIOR_ORG_CD
            // 
            this.LBL51_PRIOR_ORG_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PRIOR_ORG_CD.FactoryID = "";
            this.LBL51_PRIOR_ORG_CD.FactoryName = null;
            this.LBL51_PRIOR_ORG_CD.IsCreated = false;
            this.LBL51_PRIOR_ORG_CD.Location = new System.Drawing.Point(10, 39);
            this.LBL51_PRIOR_ORG_CD.Name = "LBL51_PRIOR_ORG_CD";
            this.LBL51_PRIOR_ORG_CD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PRIOR_ORG_CD.TabIndex = 7;
            this.LBL51_PRIOR_ORG_CD.Text = "상위조직코드";
            this.LBL51_PRIOR_ORG_CD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ENTER_CD
            // 
            this.TXT01_ENTER_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ENTER_CD.FactoryID = "";
            this.TXT01_ENTER_CD.FactoryName = null;
            this.TXT01_ENTER_CD.Location = new System.Drawing.Point(116, 12);
            this.TXT01_ENTER_CD.MinLength = 0;
            this.TXT01_ENTER_CD.Name = "TXT01_ENTER_CD";
            this.TXT01_ENTER_CD.Size = new System.Drawing.Size(66, 21);
            this.TXT01_ENTER_CD.TabIndex = 8;
            // 
            // LBL51_ENTER_CD
            // 
            this.LBL51_ENTER_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ENTER_CD.FactoryID = "";
            this.LBL51_ENTER_CD.FactoryName = null;
            this.LBL51_ENTER_CD.IsCreated = false;
            this.LBL51_ENTER_CD.Location = new System.Drawing.Point(10, 12);
            this.LBL51_ENTER_CD.Name = "LBL51_ENTER_CD";
            this.LBL51_ENTER_CD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ENTER_CD.TabIndex = 9;
            this.LBL51_ENTER_CD.Text = "회사구분";
            this.LBL51_ENTER_CD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ORG_CHART_NM
            // 
            this.TXT01_ORG_CHART_NM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ORG_CHART_NM.FactoryID = "";
            this.TXT01_ORG_CHART_NM.FactoryName = null;
            this.TXT01_ORG_CHART_NM.Location = new System.Drawing.Point(295, 12);
            this.TXT01_ORG_CHART_NM.MinLength = 0;
            this.TXT01_ORG_CHART_NM.Name = "TXT01_ORG_CHART_NM";
            this.TXT01_ORG_CHART_NM.Size = new System.Drawing.Size(100, 21);
            this.TXT01_ORG_CHART_NM.TabIndex = 10;
            // 
            // LBL51_ORG_CHART_NM
            // 
            this.LBL51_ORG_CHART_NM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ORG_CHART_NM.FactoryID = "";
            this.LBL51_ORG_CHART_NM.FactoryName = null;
            this.LBL51_ORG_CHART_NM.IsCreated = false;
            this.LBL51_ORG_CHART_NM.Location = new System.Drawing.Point(189, 12);
            this.LBL51_ORG_CHART_NM.Name = "LBL51_ORG_CHART_NM";
            this.LBL51_ORG_CHART_NM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ORG_CHART_NM.TabIndex = 11;
            this.LBL51_ORG_CHART_NM.Text = "조직도명";
            this.LBL51_ORG_CHART_NM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_28N20517
            // 
            this.FPS91_TY_S_HR_28N20517.AccessibleDescription = "";
            this.FPS91_TY_S_HR_28N20517.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_28N20517.FactoryID = "";
            this.FPS91_TY_S_HR_28N20517.FactoryName = null;
            this.FPS91_TY_S_HR_28N20517.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_28N20517.Name = "FPS91_TY_S_HR_28N20517";
            this.FPS91_TY_S_HR_28N20517.PopMenuVisible = false;
            this.FPS91_TY_S_HR_28N20517.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_28N20517.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_28N20517_Sheet1});
            this.FPS91_TY_S_HR_28N20517.Size = new System.Drawing.Size(1174, 167);
            this.FPS91_TY_S_HR_28N20517.TabIndex = 12;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_28N20517.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_28N20517.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_28N20517_CellDoubleClick);
            // 
            // FPS91_TY_S_HR_28N20517_Sheet1
            // 
            this.FPS91_TY_S_HR_28N20517_Sheet1.Reset();
            this.FPS91_TY_S_HR_28N20517_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_28N20517_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_28N20517_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_28N20517_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FPS91_TY_S_HR_28N21518
            // 
            this.FPS91_TY_S_HR_28N21518.AccessibleDescription = "";
            this.FPS91_TY_S_HR_28N21518.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_28N21518.FactoryID = "";
            this.FPS91_TY_S_HR_28N21518.FactoryName = null;
            this.FPS91_TY_S_HR_28N21518.Location = new System.Drawing.Point(1, 75);
            this.FPS91_TY_S_HR_28N21518.Name = "FPS91_TY_S_HR_28N21518";
            this.FPS91_TY_S_HR_28N21518.PopMenuVisible = false;
            this.FPS91_TY_S_HR_28N21518.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_28N21518.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_28N21518_Sheet1});
            this.FPS91_TY_S_HR_28N21518.Size = new System.Drawing.Size(669, 564);
            this.FPS91_TY_S_HR_28N21518.TabIndex = 13;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_28N21518.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_HR_28N21518.RowInserted += new Shoveling2010.SmartClient.SystemUtility.Controls.TSpread.TRowEventHandler(this.FPS91_TY_S_HR_28N21518_RowInserted);
            this.FPS91_TY_S_HR_28N21518.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.FPS91_TY_S_HR_28N21518_EnterCell);
            // 
            // FPS91_TY_S_HR_28N21518_Sheet1
            // 
            this.FPS91_TY_S_HR_28N21518_Sheet1.Reset();
            this.FPS91_TY_S_HR_28N21518_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_28N21518_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_28N21518_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_28N21518_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.panel2);
            this.GBX80_CONTROLS.Controls.Add(this.panel1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_COPY);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_ORGADD);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_ORGDEL);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_28N20517);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.TRV01_ORG);
            this.panel2.Location = new System.Drawing.Point(1, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 642);
            this.panel2.TabIndex = 15;
            // 
            // TRV01_ORG
            // 
            this.TRV01_ORG.BackColor = System.Drawing.SystemColors.Info;
            this.TRV01_ORG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TRV01_ORG.FactoryID = "";
            this.TRV01_ORG.FactoryName = null;
            this.TRV01_ORG.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TRV01_ORG.ImageIndex = 0;
            this.TRV01_ORG.LineColor = System.Drawing.Color.Maroon;
            this.TRV01_ORG.Location = new System.Drawing.Point(0, 0);
            this.TRV01_ORG.Name = "TRV01_ORG";
            this.TRV01_ORG.SelectedImageIndex = 0;
            this.TRV01_ORG.Size = new System.Drawing.Size(500, 642);
            this.TRV01_ORG.TabIndex = 1;
            this.TRV01_ORG.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TRV01_ORG_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.LBL51_PRIOR_ORG_CD);
            this.panel1.Controls.Add(this.LBL51_ORG_CHART_NM);
            this.panel1.Controls.Add(this.TXT01_ORG_CHART_NM);
            this.panel1.Controls.Add(this.LBL51_ENTER_CD);
            this.panel1.Controls.Add(this.TXT01_ENTER_CD);
            this.panel1.Controls.Add(this.BTN61_REM);
            this.panel1.Controls.Add(this.FPS91_TY_S_HR_28N21518);
            this.panel1.Controls.Add(this.CBH01_PRIOR_ORG_CD);
            this.panel1.Controls.Add(this.BTN61_SAV);
            this.panel1.Location = new System.Drawing.Point(502, 215);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 642);
            this.panel1.TabIndex = 14;
            // 
            // TYHRES002S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRES002S";
            this.Load += new System.EventHandler(this.TYHRES002S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N20517)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N20517_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N21518)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_28N21518_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_COPY;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_ORGADD;
        private TY.Service.Library.Controls.TYButton BTN61_ORGDEL;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYCodeBox CBH01_PRIOR_ORG_CD;
        private TY.Service.Library.Controls.TYLabel LBL51_PRIOR_ORG_CD;
        private TY.Service.Library.Controls.TYTextBox TXT01_ENTER_CD;
        private TY.Service.Library.Controls.TYLabel LBL51_ENTER_CD;
        private TY.Service.Library.Controls.TYTextBox TXT01_ORG_CHART_NM;
        private TY.Service.Library.Controls.TYLabel LBL51_ORG_CHART_NM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_28N20517;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_28N20517_Sheet1;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_28N21518;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_28N21518_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Service.Library.Controls.TYTreeView TRV01_ORG;
    }
}