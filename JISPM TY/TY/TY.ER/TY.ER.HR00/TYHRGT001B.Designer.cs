namespace TY.ER.HR00
{
    partial class TYHRGT001B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.TXT01_VSYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_VSYEAR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(231, 116);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(150, 116);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // TXT01_VSYEAR
            // 
            this.TXT01_VSYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_VSYEAR.FactoryID = "";
            this.TXT01_VSYEAR.FactoryName = null;
            this.TXT01_VSYEAR.Location = new System.Drawing.Point(258, 59);
            this.TXT01_VSYEAR.MinLength = 0;
            this.TXT01_VSYEAR.Name = "TXT01_VSYEAR";
            this.TXT01_VSYEAR.Size = new System.Drawing.Size(50, 21);
            this.TXT01_VSYEAR.TabIndex = 2;
            // 
            // LBL51_VSYEAR
            // 
            this.LBL51_VSYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_VSYEAR.FactoryID = "";
            this.LBL51_VSYEAR.FactoryName = null;
            this.LBL51_VSYEAR.IsCreated = false;
            this.LBL51_VSYEAR.Location = new System.Drawing.Point(152, 59);
            this.LBL51_VSYEAR.Name = "LBL51_VSYEAR";
            this.LBL51_VSYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_VSYEAR.TabIndex = 3;
            this.LBL51_VSYEAR.Text = "기준년도";
            this.LBL51_VSYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_VSYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_VSYEAR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(460, 185);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYHRGT001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 187);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGT001B";
            this.Load += new System.EventHandler(this.TYHRGT001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYTextBox TXT01_VSYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_VSYEAR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}