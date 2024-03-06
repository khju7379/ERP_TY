namespace TY.ER.AC00
{
    partial class TYACMF009B
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.DTP01_H1DATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_H1DATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP02_H1DATE = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(173, 96);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(254, 96);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // DTP01_H1DATE
            // 
            this.DTP01_H1DATE.FactoryID = "";
            this.DTP01_H1DATE.FactoryName = null;
            this.DTP01_H1DATE.Location = new System.Drawing.Point(228, 53);
            this.DTP01_H1DATE.Name = "DTP01_H1DATE";
            this.DTP01_H1DATE.Size = new System.Drawing.Size(61, 21);
            this.DTP01_H1DATE.TabIndex = 2;
            // 
            // LBL51_H1DATE
            // 
            this.LBL51_H1DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_H1DATE.FactoryID = "";
            this.LBL51_H1DATE.FactoryName = null;
            this.LBL51_H1DATE.Location = new System.Drawing.Point(122, 53);
            this.LBL51_H1DATE.Name = "LBL51_H1DATE";
            this.LBL51_H1DATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_H1DATE.TabIndex = 3;
            this.LBL51_H1DATE.Text = "거래일자";
            this.LBL51_H1DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.DTP02_H1DATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_H1DATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_H1DATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(502, 183);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "->";
            // 
            // DTP02_H1DATE
            // 
            this.DTP02_H1DATE.FactoryID = "";
            this.DTP02_H1DATE.FactoryName = null;
            this.DTP02_H1DATE.Location = new System.Drawing.Point(320, 53);
            this.DTP02_H1DATE.Name = "DTP02_H1DATE";
            this.DTP02_H1DATE.Size = new System.Drawing.Size(60, 21);
            this.DTP02_H1DATE.TabIndex = 5;
            // 
            // TYACMF009B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 188);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACMF009B";
            this.Load += new System.EventHandler(this.TYACMF009B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYDatePicker DTP01_H1DATE;
        private TY.Service.Library.Controls.TYLabel LBL51_H1DATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP02_H1DATE;
        private System.Windows.Forms.Label label1;
    }
}