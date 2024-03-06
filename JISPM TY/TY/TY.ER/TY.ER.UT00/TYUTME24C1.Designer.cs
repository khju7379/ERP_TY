namespace TY.ER.UT00
{
    partial class TYUTME24C1
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
            this.BTN61_BILL_SEND = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.TXT01_BYR_EMAIL = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BYR_EMAIL = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CONVERSATION_ID = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CONVERSATION_ID = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BILL_SEND
            // 
            this.BTN61_BILL_SEND.FactoryID = "";
            this.BTN61_BILL_SEND.FactoryName = null;
            this.BTN61_BILL_SEND.Location = new System.Drawing.Point(234, 55);
            this.BTN61_BILL_SEND.Name = "BTN61_BILL_SEND";
            this.BTN61_BILL_SEND.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BILL_SEND.TabIndex = 0;
            this.BTN61_BILL_SEND.Text = "계산서발행";
            this.BTN61_BILL_SEND.UseVisualStyleBackColor = true;
            this.BTN61_BILL_SEND.Click += new System.EventHandler(this.BTN61_BILL_SEND_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(315, 55);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TXT01_BYR_EMAIL
            // 
            this.TXT01_BYR_EMAIL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BYR_EMAIL.FactoryID = "";
            this.TXT01_BYR_EMAIL.FactoryName = null;
            this.TXT01_BYR_EMAIL.Location = new System.Drawing.Point(455, 12);
            this.TXT01_BYR_EMAIL.MinLength = 0;
            this.TXT01_BYR_EMAIL.Name = "TXT01_BYR_EMAIL";
            this.TXT01_BYR_EMAIL.Size = new System.Drawing.Size(193, 21);
            this.TXT01_BYR_EMAIL.TabIndex = 2;
            this.TXT01_BYR_EMAIL.TabIndexCustom = false;
            // 
            // LBL51_BYR_EMAIL
            // 
            this.LBL51_BYR_EMAIL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BYR_EMAIL.FactoryID = "";
            this.LBL51_BYR_EMAIL.FactoryName = null;
            this.LBL51_BYR_EMAIL.IsCreated = false;
            this.LBL51_BYR_EMAIL.Location = new System.Drawing.Point(349, 12);
            this.LBL51_BYR_EMAIL.Name = "LBL51_BYR_EMAIL";
            this.LBL51_BYR_EMAIL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BYR_EMAIL.TabIndex = 3;
            this.LBL51_BYR_EMAIL.Text = "공급받는자 메일";
            this.LBL51_BYR_EMAIL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CONVERSATION_ID
            // 
            this.TXT01_CONVERSATION_ID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_CONVERSATION_ID.FactoryID = "";
            this.TXT01_CONVERSATION_ID.FactoryName = null;
            this.TXT01_CONVERSATION_ID.Location = new System.Drawing.Point(111, 12);
            this.TXT01_CONVERSATION_ID.MinLength = 0;
            this.TXT01_CONVERSATION_ID.Name = "TXT01_CONVERSATION_ID";
            this.TXT01_CONVERSATION_ID.Size = new System.Drawing.Size(232, 21);
            this.TXT01_CONVERSATION_ID.TabIndex = 4;
            this.TXT01_CONVERSATION_ID.TabIndexCustom = false;
            // 
            // LBL51_CONVERSATION_ID
            // 
            this.LBL51_CONVERSATION_ID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CONVERSATION_ID.FactoryID = "";
            this.LBL51_CONVERSATION_ID.FactoryName = null;
            this.LBL51_CONVERSATION_ID.IsCreated = false;
            this.LBL51_CONVERSATION_ID.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CONVERSATION_ID.Name = "LBL51_CONVERSATION_ID";
            this.LBL51_CONVERSATION_ID.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CONVERSATION_ID.TabIndex = 5;
            this.LBL51_CONVERSATION_ID.Text = "전자세금계산서 ID";
            this.LBL51_CONVERSATION_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BILL_SEND);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BYR_EMAIL);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BYR_EMAIL);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_CONVERSATION_ID);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CONVERSATION_ID);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(665, 96);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUTME24C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 102);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME24C1";
            this.Load += new System.EventHandler(this.TYUTME24C1_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BILL_SEND;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYTextBox TXT01_BYR_EMAIL;
        private TY.Service.Library.Controls.TYLabel LBL51_BYR_EMAIL;
        private TY.Service.Library.Controls.TYTextBox TXT01_CONVERSATION_ID;
        private TY.Service.Library.Controls.TYLabel LBL51_CONVERSATION_ID;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}