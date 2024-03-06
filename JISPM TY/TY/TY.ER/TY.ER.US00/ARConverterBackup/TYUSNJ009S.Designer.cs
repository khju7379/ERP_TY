namespace TY.ER.US00
{
    partial class TYUSNJ009S
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
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.FPS91_TY_S_US_9439I226 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_US_9439I226_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9439I226)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9439I226_Sheet1)).BeginInit();
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
            this.LBL51_SDATE.Text = "작업년월";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.DTP01_SDATE);
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_US_9439I226);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_SDATE);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 344;
            // 
            // FPS91_TY_S_US_9439I226
            // 
            this.FPS91_TY_S_US_9439I226.AccessibleDescription = "";
            this.FPS91_TY_S_US_9439I226.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_US_9439I226.FactoryID = "";
            this.FPS91_TY_S_US_9439I226.FactoryName = null;
            this.FPS91_TY_S_US_9439I226.Location = new System.Drawing.Point(1, 39);
            this.FPS91_TY_S_US_9439I226.Name = "FPS91_TY_S_US_9439I226";
            this.FPS91_TY_S_US_9439I226.PopMenuVisible = false;
            this.FPS91_TY_S_US_9439I226.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_US_9439I226.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_US_9439I226_Sheet1});
            this.FPS91_TY_S_US_9439I226.Size = new System.Drawing.Size(1175, 821);
            this.FPS91_TY_S_US_9439I226.TabIndex = 47;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_US_9439I226.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_US_9439I226_Sheet1
            // 
            this.FPS91_TY_S_US_9439I226_Sheet1.Reset();
            this.FPS91_TY_S_US_9439I226_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_US_9439I226_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_US_9439I226_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_US_9439I226_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYUSNJ009S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSNJ009S";
            this.Load += new System.EventHandler(this.TYUSNJ009S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9439I226)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_US_9439I226_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_US_9439I226;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_US_9439I226_Sheet1;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
    }
}