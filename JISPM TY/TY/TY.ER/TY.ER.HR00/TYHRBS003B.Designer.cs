namespace TY.ER.HR00
{
    partial class TYHRBS003B
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
            this.TXT01_BOTYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTYEAR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.TXT01_BOTSINDUSTRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTSINDUSTRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BOTUINDUSTRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTUINDUSTRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BOTEMPLOYRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTEMPLOYRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BOTLTERMRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTLTERMRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BOTHEALTRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTHEALTRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BOTNATIORATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BOTNATIORATE = new TY.Service.Library.Controls.TYLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CKB01_CHK_POERCOST = new TY.Service.Library.Controls.TYCheckBox();
            this.CKB01_CHK_INSCOST = new TY.Service.Library.Controls.TYCheckBox();
            this.LBL52_BOTYEAR = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(213, 239);
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
            this.BTN61_CREATE.Location = new System.Drawing.Point(132, 239);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // TXT01_BOTYEAR
            // 
            this.TXT01_BOTYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTYEAR.FactoryID = "";
            this.TXT01_BOTYEAR.FactoryName = null;
            this.TXT01_BOTYEAR.Location = new System.Drawing.Point(156, 45);
            this.TXT01_BOTYEAR.MinLength = 0;
            this.TXT01_BOTYEAR.Name = "TXT01_BOTYEAR";
            this.TXT01_BOTYEAR.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTYEAR.TabIndex = 2;
            this.TXT01_BOTYEAR.TabIndexCustom = false;
            // 
            // LBL51_BOTYEAR
            // 
            this.LBL51_BOTYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTYEAR.FactoryID = "";
            this.LBL51_BOTYEAR.FactoryName = null;
            this.LBL51_BOTYEAR.IsCreated = false;
            this.LBL51_BOTYEAR.Location = new System.Drawing.Point(50, 45);
            this.LBL51_BOTYEAR.Name = "LBL51_BOTYEAR";
            this.LBL51_BOTYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTYEAR.TabIndex = 3;
            this.LBL51_BOTYEAR.Text = "시작일자";
            this.LBL51_BOTYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.groupBox1);
            this.GBX80_CONTROLS.Controls.Add(this.LBL52_BOTYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_CHK_INSCOST);
            this.GBX80_CONTROLS.Controls.Add(this.CKB01_CHK_POERCOST);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BOTYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BOTYEAR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(472, 271);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TXT01_BOTSINDUSTRATE
            // 
            this.TXT01_BOTSINDUSTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTSINDUSTRATE.FactoryID = "";
            this.TXT01_BOTSINDUSTRATE.FactoryName = null;
            this.TXT01_BOTSINDUSTRATE.Location = new System.Drawing.Point(312, 79);
            this.TXT01_BOTSINDUSTRATE.MinLength = 0;
            this.TXT01_BOTSINDUSTRATE.Name = "TXT01_BOTSINDUSTRATE";
            this.TXT01_BOTSINDUSTRATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTSINDUSTRATE.TabIndex = 15;
            this.TXT01_BOTSINDUSTRATE.TabIndexCustom = false;
            // 
            // LBL51_BOTSINDUSTRATE
            // 
            this.LBL51_BOTSINDUSTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTSINDUSTRATE.FactoryID = "";
            this.LBL51_BOTSINDUSTRATE.FactoryName = null;
            this.LBL51_BOTSINDUSTRATE.IsCreated = false;
            this.LBL51_BOTSINDUSTRATE.Location = new System.Drawing.Point(206, 78);
            this.LBL51_BOTSINDUSTRATE.Name = "LBL51_BOTSINDUSTRATE";
            this.LBL51_BOTSINDUSTRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTSINDUSTRATE.TabIndex = 14;
            this.LBL51_BOTSINDUSTRATE.Text = "산재보험(서울)";
            this.LBL51_BOTSINDUSTRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BOTUINDUSTRATE
            // 
            this.TXT01_BOTUINDUSTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTUINDUSTRATE.FactoryID = "";
            this.TXT01_BOTUINDUSTRATE.FactoryName = null;
            this.TXT01_BOTUINDUSTRATE.Location = new System.Drawing.Point(116, 79);
            this.TXT01_BOTUINDUSTRATE.MinLength = 0;
            this.TXT01_BOTUINDUSTRATE.Name = "TXT01_BOTUINDUSTRATE";
            this.TXT01_BOTUINDUSTRATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTUINDUSTRATE.TabIndex = 13;
            this.TXT01_BOTUINDUSTRATE.TabIndexCustom = false;
            // 
            // LBL51_BOTUINDUSTRATE
            // 
            this.LBL51_BOTUINDUSTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTUINDUSTRATE.FactoryID = "";
            this.LBL51_BOTUINDUSTRATE.FactoryName = null;
            this.LBL51_BOTUINDUSTRATE.IsCreated = false;
            this.LBL51_BOTUINDUSTRATE.Location = new System.Drawing.Point(10, 78);
            this.LBL51_BOTUINDUSTRATE.Name = "LBL51_BOTUINDUSTRATE";
            this.LBL51_BOTUINDUSTRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTUINDUSTRATE.TabIndex = 12;
            this.LBL51_BOTUINDUSTRATE.Text = "산해보험(울산)";
            this.LBL51_BOTUINDUSTRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BOTEMPLOYRATE
            // 
            this.TXT01_BOTEMPLOYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTEMPLOYRATE.FactoryID = "";
            this.TXT01_BOTEMPLOYRATE.FactoryName = null;
            this.TXT01_BOTEMPLOYRATE.Location = new System.Drawing.Point(312, 25);
            this.TXT01_BOTEMPLOYRATE.MinLength = 0;
            this.TXT01_BOTEMPLOYRATE.Name = "TXT01_BOTEMPLOYRATE";
            this.TXT01_BOTEMPLOYRATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTEMPLOYRATE.TabIndex = 11;
            this.TXT01_BOTEMPLOYRATE.TabIndexCustom = false;
            // 
            // LBL51_BOTEMPLOYRATE
            // 
            this.LBL51_BOTEMPLOYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTEMPLOYRATE.FactoryID = "";
            this.LBL51_BOTEMPLOYRATE.FactoryName = null;
            this.LBL51_BOTEMPLOYRATE.IsCreated = false;
            this.LBL51_BOTEMPLOYRATE.Location = new System.Drawing.Point(206, 24);
            this.LBL51_BOTEMPLOYRATE.Name = "LBL51_BOTEMPLOYRATE";
            this.LBL51_BOTEMPLOYRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTEMPLOYRATE.TabIndex = 10;
            this.LBL51_BOTEMPLOYRATE.Text = "고용보험";
            this.LBL51_BOTEMPLOYRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BOTLTERMRATE
            // 
            this.TXT01_BOTLTERMRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTLTERMRATE.FactoryID = "";
            this.TXT01_BOTLTERMRATE.FactoryName = null;
            this.TXT01_BOTLTERMRATE.Location = new System.Drawing.Point(312, 52);
            this.TXT01_BOTLTERMRATE.MinLength = 0;
            this.TXT01_BOTLTERMRATE.Name = "TXT01_BOTLTERMRATE";
            this.TXT01_BOTLTERMRATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTLTERMRATE.TabIndex = 9;
            this.TXT01_BOTLTERMRATE.TabIndexCustom = false;
            // 
            // LBL51_BOTLTERMRATE
            // 
            this.LBL51_BOTLTERMRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTLTERMRATE.FactoryID = "";
            this.LBL51_BOTLTERMRATE.FactoryName = null;
            this.LBL51_BOTLTERMRATE.IsCreated = false;
            this.LBL51_BOTLTERMRATE.Location = new System.Drawing.Point(206, 51);
            this.LBL51_BOTLTERMRATE.Name = "LBL51_BOTLTERMRATE";
            this.LBL51_BOTLTERMRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTLTERMRATE.TabIndex = 8;
            this.LBL51_BOTLTERMRATE.Text = "장기요양";
            this.LBL51_BOTLTERMRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BOTHEALTRATE
            // 
            this.TXT01_BOTHEALTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTHEALTRATE.FactoryID = "";
            this.TXT01_BOTHEALTRATE.FactoryName = null;
            this.TXT01_BOTHEALTRATE.Location = new System.Drawing.Point(116, 52);
            this.TXT01_BOTHEALTRATE.MinLength = 0;
            this.TXT01_BOTHEALTRATE.Name = "TXT01_BOTHEALTRATE";
            this.TXT01_BOTHEALTRATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTHEALTRATE.TabIndex = 7;
            this.TXT01_BOTHEALTRATE.TabIndexCustom = false;
            // 
            // LBL51_BOTHEALTRATE
            // 
            this.LBL51_BOTHEALTRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTHEALTRATE.FactoryID = "";
            this.LBL51_BOTHEALTRATE.FactoryName = null;
            this.LBL51_BOTHEALTRATE.IsCreated = false;
            this.LBL51_BOTHEALTRATE.Location = new System.Drawing.Point(10, 51);
            this.LBL51_BOTHEALTRATE.Name = "LBL51_BOTHEALTRATE";
            this.LBL51_BOTHEALTRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTHEALTRATE.TabIndex = 6;
            this.LBL51_BOTHEALTRATE.Text = "건강보험";
            this.LBL51_BOTHEALTRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BOTNATIORATE
            // 
            this.TXT01_BOTNATIORATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BOTNATIORATE.FactoryID = "";
            this.TXT01_BOTNATIORATE.FactoryName = null;
            this.TXT01_BOTNATIORATE.Location = new System.Drawing.Point(116, 25);
            this.TXT01_BOTNATIORATE.MinLength = 0;
            this.TXT01_BOTNATIORATE.Name = "TXT01_BOTNATIORATE";
            this.TXT01_BOTNATIORATE.Size = new System.Drawing.Size(51, 21);
            this.TXT01_BOTNATIORATE.TabIndex = 5;
            this.TXT01_BOTNATIORATE.TabIndexCustom = false;
            // 
            // LBL51_BOTNATIORATE
            // 
            this.LBL51_BOTNATIORATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BOTNATIORATE.FactoryID = "";
            this.LBL51_BOTNATIORATE.FactoryName = null;
            this.LBL51_BOTNATIORATE.IsCreated = false;
            this.LBL51_BOTNATIORATE.Location = new System.Drawing.Point(10, 24);
            this.LBL51_BOTNATIORATE.Name = "LBL51_BOTNATIORATE";
            this.LBL51_BOTNATIORATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BOTNATIORATE.TabIndex = 4;
            this.LBL51_BOTNATIORATE.Text = "국민연금";
            this.LBL51_BOTNATIORATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "(%)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "(%)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "(%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(369, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "(%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "(%)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "(%)";
            // 
            // CKB01_CHK_POERCOST
            // 
            this.CKB01_CHK_POERCOST.AutoSize = true;
            this.CKB01_CHK_POERCOST.FactoryID = "";
            this.CKB01_CHK_POERCOST.FactoryName = null;
            this.CKB01_CHK_POERCOST.Location = new System.Drawing.Point(156, 74);
            this.CKB01_CHK_POERCOST.Name = "CKB01_CHK_POERCOST";
            this.CKB01_CHK_POERCOST.Size = new System.Drawing.Size(60, 16);
            this.CKB01_CHK_POERCOST.TabIndex = 22;
            this.CKB01_CHK_POERCOST.Text = "인건비";
            this.CKB01_CHK_POERCOST.UseVisualStyleBackColor = true;
            // 
            // CKB01_CHK_INSCOST
            // 
            this.CKB01_CHK_INSCOST.AutoSize = true;
            this.CKB01_CHK_INSCOST.FactoryID = "";
            this.CKB01_CHK_INSCOST.FactoryName = null;
            this.CKB01_CHK_INSCOST.Location = new System.Drawing.Point(222, 74);
            this.CKB01_CHK_INSCOST.Name = "CKB01_CHK_INSCOST";
            this.CKB01_CHK_INSCOST.Size = new System.Drawing.Size(66, 16);
            this.CKB01_CHK_INSCOST.TabIndex = 23;
            this.CKB01_CHK_INSCOST.Text = "4대보험";
            this.CKB01_CHK_INSCOST.UseVisualStyleBackColor = true;
            this.CKB01_CHK_INSCOST.CheckedChanged += new System.EventHandler(this.CKB01_CHK_INSCOST_CheckedChanged);
            // 
            // LBL52_BOTYEAR
            // 
            this.LBL52_BOTYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL52_BOTYEAR.FactoryID = "";
            this.LBL52_BOTYEAR.FactoryName = null;
            this.LBL52_BOTYEAR.IsCreated = false;
            this.LBL52_BOTYEAR.Location = new System.Drawing.Point(49, 72);
            this.LBL52_BOTYEAR.Name = "LBL52_BOTYEAR";
            this.LBL52_BOTYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL52_BOTYEAR.TabIndex = 24;
            this.LBL52_BOTYEAR.Text = "예산구분";
            this.LBL52_BOTYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TXT01_BOTSINDUSTRATE);
            this.groupBox1.Controls.Add(this.LBL51_BOTNATIORATE);
            this.groupBox1.Controls.Add(this.TXT01_BOTNATIORATE);
            this.groupBox1.Controls.Add(this.LBL51_BOTHEALTRATE);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TXT01_BOTHEALTRATE);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.LBL51_BOTLTERMRATE);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TXT01_BOTLTERMRATE);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LBL51_BOTEMPLOYRATE);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TXT01_BOTEMPLOYRATE);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LBL51_BOTUINDUSTRATE);
            this.groupBox1.Controls.Add(this.TXT01_BOTUINDUSTRATE);
            this.groupBox1.Controls.Add(this.LBL51_BOTSINDUSTRATE);
            this.groupBox1.Location = new System.Drawing.Point(45, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 128);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "4대보험 요율";
            // 
            // TYHRBS003B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 275);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRBS003B";
            this.Load += new System.EventHandler(this.TYHRBS003B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYTextBox TXT01_BOTYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_BOTYEAR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT01_BOTSINDUSTRATE;
        private Service.Library.Controls.TYLabel LBL51_BOTSINDUSTRATE;
        private Service.Library.Controls.TYTextBox TXT01_BOTUINDUSTRATE;
        private Service.Library.Controls.TYLabel LBL51_BOTUINDUSTRATE;
        private Service.Library.Controls.TYTextBox TXT01_BOTEMPLOYRATE;
        private Service.Library.Controls.TYLabel LBL51_BOTEMPLOYRATE;
        private Service.Library.Controls.TYTextBox TXT01_BOTLTERMRATE;
        private Service.Library.Controls.TYLabel LBL51_BOTLTERMRATE;
        private Service.Library.Controls.TYTextBox TXT01_BOTHEALTRATE;
        private Service.Library.Controls.TYLabel LBL51_BOTHEALTRATE;
        private Service.Library.Controls.TYTextBox TXT01_BOTNATIORATE;
        private Service.Library.Controls.TYLabel LBL51_BOTNATIORATE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYLabel LBL52_BOTYEAR;
        private Service.Library.Controls.TYCheckBox CKB01_CHK_INSCOST;
        private Service.Library.Controls.TYCheckBox CKB01_CHK_POERCOST;
    }
}