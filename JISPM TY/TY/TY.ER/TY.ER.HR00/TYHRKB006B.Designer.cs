namespace TY.ER.HR00
{
    partial class TYHRKB006B
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.TXT01_AFFILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFFILENAME = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(623, 11);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(542, 11);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            this.BTN61_SAV.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_SAV_InvokerStart);
            this.BTN61_SAV.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_SAV_InvokerEnd);
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(447, 11);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 2;
            this.BTN61_SEARCH.Text = "찾아보기";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // TXT01_AFFILENAME
            // 
            this.TXT01_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFFILENAME.FactoryID = "";
            this.TXT01_AFFILENAME.FactoryName = null;
            this.TXT01_AFFILENAME.Location = new System.Drawing.Point(111, 12);
            this.TXT01_AFFILENAME.MinLength = 0;
            this.TXT01_AFFILENAME.Name = "TXT01_AFFILENAME";
            this.TXT01_AFFILENAME.Size = new System.Drawing.Size(330, 21);
            this.TXT01_AFFILENAME.TabIndex = 3;
            this.TXT01_AFFILENAME.TabIndexCustom = false;
            // 
            // LBL51_AFFILENAME
            // 
            this.LBL51_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFFILENAME.FactoryID = "";
            this.LBL51_AFFILENAME.FactoryName = null;
            this.LBL51_AFFILENAME.IsCreated = false;
            this.LBL51_AFFILENAME.Location = new System.Drawing.Point(5, 12);
            this.LBL51_AFFILENAME.Name = "LBL51_AFFILENAME";
            this.LBL51_AFFILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFFILENAME.TabIndex = 4;
            this.LBL51_AFFILENAME.Text = "파일명";
            this.LBL51_AFFILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SEARCH);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFFILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFFILENAME);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(704, 148);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // TYHRKB006B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 153);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.MaximizeBox = false;
            this.Name = "TYHRKB006B";
            this.Load += new System.EventHandler(this.TYHRKB006B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYButton BTN61_SEARCH;
        private TY.Service.Library.Controls.TYTextBox TXT01_AFFILENAME;
        private TY.Service.Library.Controls.TYLabel LBL51_AFFILENAME;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}