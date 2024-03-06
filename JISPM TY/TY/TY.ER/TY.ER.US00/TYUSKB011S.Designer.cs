namespace TY.ER.US00
{
    partial class TYUSKB011S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.MTB01_CHCHTIME = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.MTB01_CHIPTIME = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.LBL51_CHIPTIME = new TY.Service.Library.Controls.TYLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_US_92IF4800 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_92IF4800_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.MTB01_SDATE = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92IF4800)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92IF4800_Sheet1)).BeginInit();
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
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 6;
            this.LBL51_SDATE.Text = "출고일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.MTB01_SDATE);
            this.GRP01_SEARCH.Controls.Add(this.MTB01_CHCHTIME);
            this.GRP01_SEARCH.Controls.Add(this.MTB01_CHIPTIME);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_CHIPTIME);
            this.GRP01_SEARCH.Controls.Add(this.label1);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_US_92IF4800);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SDATE);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // MTB01_CHCHTIME
            // 
            this.MTB01_CHCHTIME.FactoryID = "";
            this.MTB01_CHCHTIME.FactoryName = null;
            this.MTB01_CHCHTIME.Location = new System.Drawing.Point(391, 12);
            this.MTB01_CHCHTIME.Mask = "00:00";
            this.MTB01_CHCHTIME.Name = "MTB01_CHCHTIME";
            this.MTB01_CHCHTIME.Size = new System.Drawing.Size(42, 21);
            this.MTB01_CHCHTIME.TabIndex = 139;
            this.MTB01_CHCHTIME.ValidatingType = typeof(System.DateTime);
            this.MTB01_CHCHTIME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MTB01_CHCHTIME_KeyPress);
            // 
            // MTB01_CHIPTIME
            // 
            this.MTB01_CHIPTIME.FactoryID = "";
            this.MTB01_CHIPTIME.FactoryName = null;
            this.MTB01_CHIPTIME.Location = new System.Drawing.Point(323, 12);
            this.MTB01_CHIPTIME.Mask = "00:00";
            this.MTB01_CHIPTIME.Name = "MTB01_CHIPTIME";
            this.MTB01_CHIPTIME.Size = new System.Drawing.Size(42, 21);
            this.MTB01_CHIPTIME.TabIndex = 138;
            this.MTB01_CHIPTIME.ValidatingType = typeof(System.DateTime);
            // 
            // LBL51_CHIPTIME
            // 
            this.LBL51_CHIPTIME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CHIPTIME.FactoryID = "";
            this.LBL51_CHIPTIME.FactoryName = null;
            this.LBL51_CHIPTIME.ForeColor = System.Drawing.Color.White;
            this.LBL51_CHIPTIME.IsCreated = false;
            this.LBL51_CHIPTIME.Location = new System.Drawing.Point(217, 12);
            this.LBL51_CHIPTIME.Name = "LBL51_CHIPTIME";
            this.LBL51_CHIPTIME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHIPTIME.TabIndex = 90;
            this.LBL51_CHIPTIME.Text = "출고시간";
            this.LBL51_CHIPTIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 88;
            this.label1.Text = "~";
            // 
            // FPS91_TY_S_US_92IF4800
            // 
            this.FPS91_TY_S_US_92IF4800.AccessibleDescription = "";
            this.FPS91_TY_S_US_92IF4800.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_92IF4800.FactoryID = "";
            this.FPS91_TY_S_US_92IF4800.FactoryName = null;
            this.FPS91_TY_S_US_92IF4800.Location = new System.Drawing.Point(1, 39);
            this.FPS91_TY_S_US_92IF4800.Name = "FPS91_TY_S_US_92IF4800";
            this.FPS91_TY_S_US_92IF4800.PopMenuVisible = false;
            this.FPS91_TY_S_US_92IF4800.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_92IF4800.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_92IF4800_Sheet1});
            this.FPS91_TY_S_US_92IF4800.Size = new System.Drawing.Size(1175, 821);
            this.FPS91_TY_S_US_92IF4800.TabIndex = 47;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_92IF4800.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_US_92IF4800_Sheet1
            // 
            this.FPS91_TY_S_US_92IF4800_Sheet1.Reset();
            this.FPS91_TY_S_US_92IF4800_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_92IF4800_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_92IF4800_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_92IF4800_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // MTB01_SDATE
            // 
            this.MTB01_SDATE.FactoryID = "";
            this.MTB01_SDATE.FactoryName = null;
            this.MTB01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.MTB01_SDATE.Mask = "0000-00-00";
            this.MTB01_SDATE.Name = "MTB01_SDATE";
            this.MTB01_SDATE.Size = new System.Drawing.Size(74, 21);
            this.MTB01_SDATE.TabIndex = 201;
            this.MTB01_SDATE.ValidatingType = typeof(System.DateTime);
            this.MTB01_SDATE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MTB01_SDATE_KeyPress);
            // 
            // TYUSKB011S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSKB011S";
            this.Load += new System.EventHandler(this.TYUSKB011S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92IF4800)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_92IF4800_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_92IF4800;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_92IF4800_Sheet1;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYLabel LBL51_CHIPTIME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_CHCHTIME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_CHIPTIME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_SDATE;
    }
}