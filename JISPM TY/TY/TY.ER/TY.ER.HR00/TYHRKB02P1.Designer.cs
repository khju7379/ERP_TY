namespace TY.ER.HR00
{
    partial class TYHRKB02P1
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_BLDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_BLDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.TRV01_ORG = new TY.Service.Library.Controls.TYTreeView();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(316, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_BLDATE
            // 
            this.LBL51_BLDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BLDATE.FactoryID = "";
            this.LBL51_BLDATE.FactoryName = null;
            this.LBL51_BLDATE.IsCreated = false;
            this.LBL51_BLDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_BLDATE.Name = "LBL51_BLDATE";
            this.LBL51_BLDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BLDATE.TabIndex = 2;
            this.LBL51_BLDATE.Text = "발령일자";
            this.LBL51_BLDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.TRV01_ORG);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_BLDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BLDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(397, 488);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_BLDATE
            // 
            this.DTP01_BLDATE.FactoryID = "";
            this.DTP01_BLDATE.FactoryName = null;
            this.DTP01_BLDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_BLDATE.Name = "DTP01_BLDATE";
            this.DTP01_BLDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_BLDATE.TabIndex = 9;
            // 
            // TRV01_ORG
            // 
            this.TRV01_ORG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TRV01_ORG.BackColor = System.Drawing.SystemColors.Info;
            this.TRV01_ORG.FactoryID = "";
            this.TRV01_ORG.FactoryName = null;
            this.TRV01_ORG.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TRV01_ORG.ImageIndex = 0;
            this.TRV01_ORG.LineColor = System.Drawing.Color.Maroon;
            this.TRV01_ORG.Location = new System.Drawing.Point(5, 39);
            this.TRV01_ORG.Name = "TRV01_ORG";
            this.TRV01_ORG.SelectedImageIndex = 0;
            this.TRV01_ORG.Size = new System.Drawing.Size(388, 444);
            this.TRV01_ORG.TabIndex = 10;
            this.TRV01_ORG.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TRV01_ORG_MouseDoubleClick);
            // 
            // TYHRKB02P1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 493);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRKB02P1";
            this.Load += new System.EventHandler(this.TYHRKB02P1_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_BLDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_BLDATE;
        private Service.Library.Controls.TYTreeView TRV01_ORG;
    }
}