namespace TY.Service.Launcher
{
    partial class Login
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
            this.BTN01_LOGIN = new Shoveling2010.SmartClient.SystemUtility.Controls.TButton();
            this.TXT01_USER_ID = new Shoveling2010.SmartClient.SystemUtility.Controls.TTextBox();
            this.TXT01_PASSWORD = new Shoveling2010.SmartClient.SystemUtility.Controls.TTextBox();
            this.PNL01_TOP = new System.Windows.Forms.Panel();
            this.PNL01_CENTER = new System.Windows.Forms.Panel();
            this.CHK01_SAVEID = new Shoveling2010.SmartClient.SystemUtility.Controls.TCheckBox();
            this.PNL01_MAIN = new System.Windows.Forms.Panel();
            this.PNL01_CENTER.SuspendLayout();
            this.PNL01_MAIN.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN01_LOGIN
            // 
            this.BTN01_LOGIN.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.btn_login;
            this.BTN01_LOGIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BTN01_LOGIN.FactoryID = "";
            this.BTN01_LOGIN.FactoryName = null;
            this.BTN01_LOGIN.FlatAppearance.BorderSize = 0;
            this.BTN01_LOGIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN01_LOGIN.Location = new System.Drawing.Point(581, 201);
            this.BTN01_LOGIN.Margin = new System.Windows.Forms.Padding(0);
            this.BTN01_LOGIN.Name = "BTN01_LOGIN";
            this.BTN01_LOGIN.Size = new System.Drawing.Size(55, 55);
            this.BTN01_LOGIN.TabIndex = 4;
            this.BTN01_LOGIN.UseVisualStyleBackColor = true;
            this.BTN01_LOGIN.Click += new System.EventHandler(this.BTN01_LOGIN_Click);
            // 
            // TXT01_USER_ID
            // 
            this.TXT01_USER_ID.FactoryID = "";
            this.TXT01_USER_ID.FactoryName = null;
            this.TXT01_USER_ID.Location = new System.Drawing.Point(438, 203);
            this.TXT01_USER_ID.MinLength = 0;
            this.TXT01_USER_ID.Name = "TXT01_USER_ID";
            this.TXT01_USER_ID.Size = new System.Drawing.Size(140, 21);
            this.TXT01_USER_ID.TabIndex = 1;
            this.TXT01_USER_ID.TabIndexCustom = false;
            this.TXT01_USER_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TXT01_USER_ID_KeyDown);
            // 
            // TXT01_PASSWORD
            // 
            this.TXT01_PASSWORD.FactoryID = "";
            this.TXT01_PASSWORD.FactoryName = null;
            this.TXT01_PASSWORD.Location = new System.Drawing.Point(438, 233);
            this.TXT01_PASSWORD.MinLength = 0;
            this.TXT01_PASSWORD.Name = "TXT01_PASSWORD";
            this.TXT01_PASSWORD.Size = new System.Drawing.Size(140, 21);
            this.TXT01_PASSWORD.TabIndex = 2;
            this.TXT01_PASSWORD.TabIndexCustom = false;
            this.TXT01_PASSWORD.UseSystemPasswordChar = true;
            this.TXT01_PASSWORD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TXT01_PASSWORD_KeyDown);
            // 
            // PNL01_TOP
            // 
            this.PNL01_TOP.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.login_bg;
            this.PNL01_TOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.PNL01_TOP.Location = new System.Drawing.Point(2, 2);
            this.PNL01_TOP.Name = "PNL01_TOP";
            this.PNL01_TOP.Size = new System.Drawing.Size(710, 53);
            this.PNL01_TOP.TabIndex = 4;
            this.PNL01_TOP.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDoubleClick);
            // 
            // PNL01_CENTER
            // 
            this.PNL01_CENTER.BackColor = System.Drawing.Color.White;
            this.PNL01_CENTER.BackgroundImage = global::TY.Service.Launcher.Properties.Resources.login_form_bg_no;
            this.PNL01_CENTER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PNL01_CENTER.Controls.Add(this.CHK01_SAVEID);
            this.PNL01_CENTER.Controls.Add(this.TXT01_USER_ID);
            this.PNL01_CENTER.Controls.Add(this.TXT01_PASSWORD);
            this.PNL01_CENTER.Controls.Add(this.BTN01_LOGIN);
            this.PNL01_CENTER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PNL01_CENTER.Location = new System.Drawing.Point(2, 55);
            this.PNL01_CENTER.Name = "PNL01_CENTER";
            this.PNL01_CENTER.Size = new System.Drawing.Size(710, 447);
            this.PNL01_CENTER.TabIndex = 5;
            this.PNL01_CENTER.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDoubleClick);
            // 
            // CHK01_SAVEID
            // 
            this.CHK01_SAVEID.AutoSize = true;
            this.CHK01_SAVEID.FactoryID = "";
            this.CHK01_SAVEID.FactoryName = null;
            this.CHK01_SAVEID.Location = new System.Drawing.Point(563, 270);
            this.CHK01_SAVEID.Name = "CHK01_SAVEID";
            this.CHK01_SAVEID.Size = new System.Drawing.Size(15, 14);
            this.CHK01_SAVEID.TabIndex = 3;
            this.CHK01_SAVEID.UseVisualStyleBackColor = true;
            // 
            // PNL01_MAIN
            // 
            this.PNL01_MAIN.BackColor = System.Drawing.Color.Black;
            this.PNL01_MAIN.Controls.Add(this.PNL01_CENTER);
            this.PNL01_MAIN.Controls.Add(this.PNL01_TOP);
            this.PNL01_MAIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PNL01_MAIN.Location = new System.Drawing.Point(0, 0);
            this.PNL01_MAIN.Name = "PNL01_MAIN";
            this.PNL01_MAIN.Padding = new System.Windows.Forms.Padding(2);
            this.PNL01_MAIN.Size = new System.Drawing.Size(714, 504);
            this.PNL01_MAIN.TabIndex = 6;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(714, 504);
            this.Controls.Add(this.PNL01_MAIN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDoubleClick);
            this.PNL01_CENTER.ResumeLayout(false);
            this.PNL01_CENTER.PerformLayout();
            this.PNL01_MAIN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Shoveling2010.SmartClient.SystemUtility.Controls.TButton BTN01_LOGIN;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TTextBox TXT01_USER_ID;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TTextBox TXT01_PASSWORD;
        private System.Windows.Forms.Panel PNL01_TOP;
        private System.Windows.Forms.Panel PNL01_CENTER;
        private Shoveling2010.SmartClient.SystemUtility.Controls.TCheckBox CHK01_SAVEID;
        private System.Windows.Forms.Panel PNL01_MAIN;
    }
}