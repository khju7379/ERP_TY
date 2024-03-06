namespace TY.ER.GB00
{
    partial class TYERGB004P
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
            this.RTB01_TEXT = new Shoveling2010.SmartClient.SystemUtility.Controls.TRichTextBox();
            this.BTN61_LOGOUT = new Shoveling2010.SmartClient.SystemUtility.Controls.TButton();
            this.BTN61_SHUTDOWN = new Shoveling2010.SmartClient.SystemUtility.Controls.TButton();
            this.BTN61_CANCEL = new Shoveling2010.SmartClient.SystemUtility.Controls.TButton();
            this.SuspendLayout();
            // 
            // RTB01_TEXT
            // 
            this.RTB01_TEXT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB01_TEXT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB01_TEXT.Enabled = false;
            this.RTB01_TEXT.FactoryID = "";
            this.RTB01_TEXT.FactoryName = null;
            this.RTB01_TEXT.Location = new System.Drawing.Point(12, 12);
            this.RTB01_TEXT.MinLength = 0;
            this.RTB01_TEXT.Name = "RTB01_TEXT";
            this.RTB01_TEXT.Size = new System.Drawing.Size(374, 78);
            this.RTB01_TEXT.TabIndex = 1;
            this.RTB01_TEXT.Text = "";
            // 
            // BTN61_LOGOUT
            // 
            this.BTN61_LOGOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_LOGOUT.FactoryID = "";
            this.BTN61_LOGOUT.FactoryName = null;
            this.BTN61_LOGOUT.Location = new System.Drawing.Point(149, 96);
            this.BTN61_LOGOUT.Name = "BTN61_LOGOUT";
            this.BTN61_LOGOUT.Size = new System.Drawing.Size(75, 23);
            this.BTN61_LOGOUT.TabIndex = 2;
            this.BTN61_LOGOUT.Text = "로그오프";
            this.BTN61_LOGOUT.UseVisualStyleBackColor = true;
            this.BTN61_LOGOUT.Click += new System.EventHandler(this.BTN61_LOGOUT_Click);
            // 
            // BTN61_SHUTDOWN
            // 
            this.BTN61_SHUTDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SHUTDOWN.FactoryID = "";
            this.BTN61_SHUTDOWN.FactoryName = null;
            this.BTN61_SHUTDOWN.Location = new System.Drawing.Point(230, 96);
            this.BTN61_SHUTDOWN.Name = "BTN61_SHUTDOWN";
            this.BTN61_SHUTDOWN.Size = new System.Drawing.Size(75, 23);
            this.BTN61_SHUTDOWN.TabIndex = 3;
            this.BTN61_SHUTDOWN.Text = "완전종료";
            this.BTN61_SHUTDOWN.UseVisualStyleBackColor = true;
            this.BTN61_SHUTDOWN.Click += new System.EventHandler(this.BTN61_SHUTDOWN_Click);
            // 
            // BTN61_CANCEL
            // 
            this.BTN61_CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CANCEL.FactoryID = "";
            this.BTN61_CANCEL.FactoryName = null;
            this.BTN61_CANCEL.Location = new System.Drawing.Point(311, 96);
            this.BTN61_CANCEL.Name = "BTN61_CANCEL";
            this.BTN61_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BTN61_CANCEL.TabIndex = 4;
            this.BTN61_CANCEL.Text = "종료취소";
            this.BTN61_CANCEL.UseVisualStyleBackColor = true;
            this.BTN61_CANCEL.Click += new System.EventHandler(this.BTN61_CANCEL_Click);
            // 
            // TYERGB004P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 128);
            this.Controls.Add(this.BTN61_CANCEL);
            this.Controls.Add(this.BTN61_SHUTDOWN);
            this.Controls.Add(this.BTN61_LOGOUT);
            this.Controls.Add(this.RTB01_TEXT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TYERGB004P";
            this.Text = "TYERGB004P";
            this.Load += new System.EventHandler(this.TYERGB004P_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Shoveling2010.SmartClient.SystemUtility.Controls.TRichTextBox RTB01_TEXT;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TButton BTN61_LOGOUT;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TButton BTN61_SHUTDOWN;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TButton BTN61_CANCEL;
    }
}