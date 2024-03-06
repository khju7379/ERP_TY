namespace TY.ER.AC00
{
    partial class TYACTP020I
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
            this.LBL51_WJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_WJYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_AC_BB8EH709 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_BB8EH709_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_BB8EH709)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_BB8EH709_Sheet1)).BeginInit();
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
            this.groupBox1.Controls.Add(this.BTN61_BATCH);
            this.groupBox1.Controls.Add(this.LBL51_WJYYMM);
            this.groupBox1.Controls.Add(this.DTP01_WJYYMM);
            this.groupBox1.Controls.Add(this.groupBox27);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1475, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
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
            // LBL51_WJYYMM
            // 
            this.LBL51_WJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_WJYYMM.FactoryID = "";
            this.LBL51_WJYYMM.FactoryName = null;
            this.LBL51_WJYYMM.ForeColor = System.Drawing.Color.White;
            this.LBL51_WJYYMM.IsCreated = false;
            this.LBL51_WJYYMM.Location = new System.Drawing.Point(5, 15);
            this.LBL51_WJYYMM.Name = "LBL51_WJYYMM";
            this.LBL51_WJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_WJYYMM.TabIndex = 223;
            this.LBL51_WJYYMM.Text = "발생월";
            this.LBL51_WJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_WJYYMM
            // 
            this.DTP01_WJYYMM.FactoryID = "";
            this.DTP01_WJYYMM.FactoryName = null;
            this.DTP01_WJYYMM.Location = new System.Drawing.Point(111, 15);
            this.DTP01_WJYYMM.Name = "DTP01_WJYYMM";
            this.DTP01_WJYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_WJYYMM.TabIndex = 221;
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.FPS91_TY_S_AC_BB8EH709);
            this.groupBox27.Font = new System.Drawing.Font("굴림", 9F);
            this.groupBox27.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox27.Location = new System.Drawing.Point(0, 39);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(1475, 828);
            this.groupBox27.TabIndex = 220;
            this.groupBox27.TabStop = false;
            // 
            // FPS91_TY_S_AC_BB8EH709
            // 
            this.FPS91_TY_S_AC_BB8EH709.AccessibleDescription = "FPS91_TY_S_AC_BB8EH709";
            this.FPS91_TY_S_AC_BB8EH709.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_BB8EH709.FactoryID = "";
            this.FPS91_TY_S_AC_BB8EH709.FactoryName = null;
            this.FPS91_TY_S_AC_BB8EH709.Location = new System.Drawing.Point(0, 20);
            this.FPS91_TY_S_AC_BB8EH709.Name = "FPS91_TY_S_AC_BB8EH709";
            this.FPS91_TY_S_AC_BB8EH709.PopMenuVisible = false;
            this.FPS91_TY_S_AC_BB8EH709.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_BB8EH709.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_BB8EH709_Sheet1});
            this.FPS91_TY_S_AC_BB8EH709.Size = new System.Drawing.Size(1475, 802);
            this.FPS91_TY_S_AC_BB8EH709.TabIndex = 180;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_BB8EH709.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_AC_BB8EH709.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.FPS91_TY_S_AC_BB8EH709_Change);
            this.FPS91_TY_S_AC_BB8EH709.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.FPS91_TY_S_AC_BB8EH709_LeaveCell);
            this.FPS91_TY_S_AC_BB8EH709.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.FPS91_TY_S_AC_BB8EH709_EnterCell);
            // 
            // FPS91_TY_S_AC_BB8EH709_Sheet1
            // 
            this.FPS91_TY_S_AC_BB8EH709_Sheet1.Reset();
            this.FPS91_TY_S_AC_BB8EH709_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_BB8EH709_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_BB8EH709_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_BB8EH709_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYACTP020I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACTP020I";
            this.Load += new System.EventHandler(this.TYACTP020I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_BB8EH709)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_BB8EH709_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox27;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_BB8EH709;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_BB8EH709_Sheet1;
        private Service.Library.Controls.TYDatePicker DTP01_WJYYMM;
        private Service.Library.Controls.TYLabel LBL51_WJYYMM;
        private Service.Library.Controls.TYButton BTN61_BATCH;
    }
}