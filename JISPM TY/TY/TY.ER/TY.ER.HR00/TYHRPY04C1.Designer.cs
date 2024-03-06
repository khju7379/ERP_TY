namespace TY.ER.HR00
{
    partial class TYHRPY04C1
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_HR_4CBES714 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_4CBES714_Sheet1 = new FarPoint.Win.Spread.SheetView();
            FarPoint.Win.Spread.TipAppearance FPS91_TY_S_HR_4CBES714_ta1 = new FarPoint.Win.Spread.TipAppearance();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CBES714_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CBES714)).BeginInit();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Location = new System.Drawing.Point(700, 17);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Location = new System.Drawing.Point(700, 44);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // FPS91_TY_S_HR_4CBES714_Sheet1
            // 
            this.FPS91_TY_S_HR_4CBES714_Sheet1.Reset();
            this.FPS91_TY_S_HR_4CBES714_Sheet1.SheetName = "Sheet1";
            this.FPS91_TY_S_HR_4CBES714_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_4CBES714_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_4CBES714_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FPS91_TY_S_HR_4CBES714
            // 
            this.FPS91_TY_S_HR_4CBES714.Location = new System.Drawing.Point(900, 17);
            this.FPS91_TY_S_HR_4CBES714.Name = "FPS91_TY_S_HR_4CBES714";
            this.FPS91_TY_S_HR_4CBES714.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {this.FPS91_TY_S_HR_4CBES714_Sheet1});
            this.FPS91_TY_S_HR_4CBES714.Size = new System.Drawing.Size(150, 150);
            FPS91_TY_S_HR_4CBES714_ta1.BackColor = System.Drawing.SystemColors.Info;
            FPS91_TY_S_HR_4CBES714_ta1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            FPS91_TY_S_HR_4CBES714_ta1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_4CBES714.TextTipAppearance = FPS91_TY_S_HR_4CBES714_ta1;
			// 
            // GBX80_CONTROLS
            //
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_4CBES714);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(12, 12);
			this.GBX80_CONTROLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1270, 1000);
            this.GBX80_CONTROLS.Text = "정의된 필드사전 목록";
            // 
            // TYHRPY04C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 1000);
            this.Name = "TYHRPY04C1";
            this.Controls.Add(this.GBX80_CONTROLS);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CBES714_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_4CBES714)).EndInit();
            this.Load += new System.EventHandler(this.TYHRPY04C1_Load);
            this.ResumeLayout(false);
        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_4CBES714;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_4CBES714_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}