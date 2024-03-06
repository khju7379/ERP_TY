namespace TY.ER.US00
{
    partial class TYUSGA004P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CKB01_CHPRTGB = new TY.Service.Library.Controls.TYCheckBox();
            this.LBL51_CHCHULDAT = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CHNUMBER = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CHNUMBER = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CHTKNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CHTKNO = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_US_98EBJ116 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_98EBJ116_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.AVW01_REPORT = new DataDynamics.ActiveReports.Viewer.Viewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DTP01_CHCHULDAT = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL_MESSAGE = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_98EBJ116)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_98EBJ116_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(933, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1014, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CKB01_CHPRTGB
            // 
            this.CKB01_CHPRTGB.FactoryID = "";
            this.CKB01_CHPRTGB.FactoryName = null;
            this.CKB01_CHPRTGB.Location = new System.Drawing.Point(4, 0);
            this.CKB01_CHPRTGB.Name = "CKB01_CHPRTGB";
            this.CKB01_CHPRTGB.Size = new System.Drawing.Size(75, 21);
            this.CKB01_CHPRTGB.TabIndex = 2;
            this.CKB01_CHPRTGB.Text = "내부출력";
            // 
            // LBL51_CHCHULDAT
            // 
            this.LBL51_CHCHULDAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHCHULDAT.FactoryID = "";
            this.LBL51_CHCHULDAT.FactoryName = null;
            this.LBL51_CHCHULDAT.IsCreated = false;
            this.LBL51_CHCHULDAT.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CHCHULDAT.Name = "LBL51_CHCHULDAT";
            this.LBL51_CHCHULDAT.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHCHULDAT.TabIndex = 5;
            this.LBL51_CHCHULDAT.Text = "출고일자";
            this.LBL51_CHCHULDAT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CHNUMBER
            // 
            this.TXT01_CHNUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_CHNUMBER.FactoryID = "";
            this.TXT01_CHNUMBER.FactoryName = null;
            this.TXT01_CHNUMBER.Location = new System.Drawing.Point(485, 12);
            this.TXT01_CHNUMBER.MinLength = 0;
            this.TXT01_CHNUMBER.Name = "TXT01_CHNUMBER";
            this.TXT01_CHNUMBER.Size = new System.Drawing.Size(100, 21);
            this.TXT01_CHNUMBER.TabIndex = 8;
            this.TXT01_CHNUMBER.TabIndexCustom = false;
            // 
            // LBL51_CHNUMBER
            // 
            this.LBL51_CHNUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHNUMBER.FactoryID = "";
            this.LBL51_CHNUMBER.FactoryName = null;
            this.LBL51_CHNUMBER.IsCreated = false;
            this.LBL51_CHNUMBER.Location = new System.Drawing.Point(379, 12);
            this.LBL51_CHNUMBER.Name = "LBL51_CHNUMBER";
            this.LBL51_CHNUMBER.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHNUMBER.TabIndex = 9;
            this.LBL51_CHNUMBER.Text = "차량번호";
            this.LBL51_CHNUMBER.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CHTKNO
            // 
            this.TXT01_CHTKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_CHTKNO.FactoryID = "";
            this.TXT01_CHTKNO.FactoryName = null;
            this.TXT01_CHTKNO.Location = new System.Drawing.Point(323, 12);
            this.TXT01_CHTKNO.MinLength = 0;
            this.TXT01_CHTKNO.Name = "TXT01_CHTKNO";
            this.TXT01_CHTKNO.Size = new System.Drawing.Size(50, 21);
            this.TXT01_CHTKNO.TabIndex = 10;
            this.TXT01_CHTKNO.TabIndexCustom = false;
            // 
            // LBL51_CHTKNO
            // 
            this.LBL51_CHTKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHTKNO.FactoryID = "";
            this.LBL51_CHTKNO.FactoryName = null;
            this.LBL51_CHTKNO.IsCreated = false;
            this.LBL51_CHTKNO.Location = new System.Drawing.Point(217, 12);
            this.LBL51_CHTKNO.Name = "LBL51_CHTKNO";
            this.LBL51_CHTKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHTKNO.TabIndex = 11;
            this.LBL51_CHTKNO.Text = "TICKET번호";
            this.LBL51_CHTKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_US_98EBJ116
            // 
            this.FPS91_TY_S_US_98EBJ116.AccessibleDescription = "";
            this.FPS91_TY_S_US_98EBJ116.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_98EBJ116.FactoryID = "";
            this.FPS91_TY_S_US_98EBJ116.FactoryName = null;
            this.FPS91_TY_S_US_98EBJ116.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_US_98EBJ116.Name = "FPS91_TY_S_US_98EBJ116";
            this.FPS91_TY_S_US_98EBJ116.PopMenuVisible = false;
            this.FPS91_TY_S_US_98EBJ116.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_98EBJ116.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_98EBJ116_Sheet1});
            this.FPS91_TY_S_US_98EBJ116.Size = new System.Drawing.Size(1174, 815);
            this.FPS91_TY_S_US_98EBJ116.TabIndex = 12;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_98EBJ116.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_US_98EBJ116.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FPS91_TY_S_US_98EBJ116_ButtonClicked);
            // 
            // FPS91_TY_S_US_98EBJ116_Sheet1
            // 
            this.FPS91_TY_S_US_98EBJ116_Sheet1.Reset();
            this.FPS91_TY_S_US_98EBJ116_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_98EBJ116_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_98EBJ116_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_98EBJ116_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.LBL_MESSAGE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.AVW01_REPORT);
            this.GBX80_CONTROLS.Controls.Add(this.panel1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_CHCHULDAT);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHCHULDAT);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_CHNUMBER);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHNUMBER);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_CHTKNO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHTKNO);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_US_98EBJ116);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1095, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 312;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // AVW01_REPORT
            // 
            this.AVW01_REPORT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AVW01_REPORT.BackColor = System.Drawing.SystemColors.Control;
            this.AVW01_REPORT.Document = new DataDynamics.ActiveReports.Document.Document("ARNet Document");
            this.AVW01_REPORT.Location = new System.Drawing.Point(734, 12);
            this.AVW01_REPORT.Name = "AVW01_REPORT";
            this.AVW01_REPORT.ReportViewer.CurrentPage = 0;
            this.AVW01_REPORT.ReportViewer.MultiplePageCols = 3;
            this.AVW01_REPORT.ReportViewer.MultiplePageRows = 2;
            this.AVW01_REPORT.ReportViewer.ViewType = DataDynamics.ActiveReports.Viewer.ViewType.Normal;
            this.AVW01_REPORT.Size = new System.Drawing.Size(40, 21);
            this.AVW01_REPORT.TabIndex = 2;
            this.AVW01_REPORT.TableOfContents.Text = "Table Of Contents";
            this.AVW01_REPORT.TableOfContents.Width = 200;
            this.AVW01_REPORT.TabTitleLength = 35;
            this.AVW01_REPORT.Toolbar.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.AVW01_REPORT.Toolbar.RenderMode = DataDynamics.ActiveReports.Viewer.ToolbarRenderMode.Professional;
            this.AVW01_REPORT.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CKB01_CHPRTGB);
            this.panel1.Location = new System.Drawing.Point(780, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 21);
            this.panel1.TabIndex = 311;
            // 
            // DTP01_CHCHULDAT
            // 
            this.DTP01_CHCHULDAT.FactoryID = "";
            this.DTP01_CHCHULDAT.FactoryName = null;
            this.DTP01_CHCHULDAT.Font = new System.Drawing.Font("굴림", 9F);
            this.DTP01_CHCHULDAT.Location = new System.Drawing.Point(111, 12);
            this.DTP01_CHCHULDAT.Name = "DTP01_CHCHULDAT";
            this.DTP01_CHCHULDAT.Size = new System.Drawing.Size(100, 21);
            this.DTP01_CHCHULDAT.TabIndex = 310;
            // 
            // LBL_MESSAGE
            // 
            this.LBL_MESSAGE.AutoSize = true;
            this.LBL_MESSAGE.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LBL_MESSAGE.ForeColor = System.Drawing.Color.Blue;
            this.LBL_MESSAGE.Location = new System.Drawing.Point(871, 17);
            this.LBL_MESSAGE.Name = "LBL_MESSAGE";
            this.LBL_MESSAGE.Size = new System.Drawing.Size(50, 13);
            this.LBL_MESSAGE.TabIndex = 313;
            this.LBL_MESSAGE.Text = "label1";
            // 
            // TYUSGA004P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSGA004P";
            this.Load += new System.EventHandler(this.TYUSGA004P_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_98EBJ116)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_98EBJ116_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCheckBox CKB01_CHPRTGB;
        private TY.Service.Library.Controls.TYLabel LBL51_CHCHULDAT;
        private TY.Service.Library.Controls.TYTextBox TXT01_CHNUMBER;
        private TY.Service.Library.Controls.TYLabel LBL51_CHNUMBER;
        private TY.Service.Library.Controls.TYTextBox TXT01_CHTKNO;
        private TY.Service.Library.Controls.TYLabel LBL51_CHTKNO;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_US_98EBJ116;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_98EBJ116_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_CHCHULDAT;
        private System.Windows.Forms.Panel panel1;
        private DataDynamics.ActiveReports.Viewer.Viewer AVW01_REPORT;
        private Service.Library.Controls.TYButton BTN61_CLO;
        private System.Windows.Forms.Label LBL_MESSAGE;
    }
}