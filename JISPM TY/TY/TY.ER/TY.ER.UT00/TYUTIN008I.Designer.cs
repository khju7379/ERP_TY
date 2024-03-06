namespace TY.ER.UT00
{
    partial class TYUTIN008I
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LBL51_SNDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SNBALANCE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNBALANCE = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_SNHIGH = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SNHIGH = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNNOTE = new TY.Service.Library.Controls.TYTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LBL51_SNMTQTY = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNMTQTY = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SNHWAMUL = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_SNHWAMUL = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_SNNOTE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNGKLQTY = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SNGKLQTY = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_SNNKLQTY = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNNKLQTY = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SNTEMP = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_SNTEMP = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_SNTANKNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SNTANKNO = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_SNDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(771, 12);
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
            this.BTN61_SAV.Location = new System.Drawing.Point(690, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.TXT01_SNTANKNO);
            this.groupBox1.Controls.Add(this.LBL51_SNTANKNO);
            this.groupBox1.Controls.Add(this.DTP01_SNDATE);
            this.groupBox1.Controls.Add(this.LBL51_SNDATE);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(852, 173);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // LBL51_SNDATE
            // 
            this.LBL51_SNDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNDATE.FactoryID = "";
            this.LBL51_SNDATE.FactoryName = null;
            this.LBL51_SNDATE.IsCreated = false;
            this.LBL51_SNDATE.Location = new System.Drawing.Point(6, 12);
            this.LBL51_SNDATE.Name = "LBL51_SNDATE";
            this.LBL51_SNDATE.Size = new System.Drawing.Size(99, 21);
            this.LBL51_SNDATE.TabIndex = 29;
            this.LBL51_SNDATE.Text = "완공일자";
            this.LBL51_SNDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SNBALANCE
            // 
            this.LBL51_SNBALANCE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNBALANCE.FactoryID = "";
            this.LBL51_SNBALANCE.FactoryName = null;
            this.LBL51_SNBALANCE.IsCreated = false;
            this.LBL51_SNBALANCE.Location = new System.Drawing.Point(417, 96);
            this.LBL51_SNBALANCE.Name = "LBL51_SNBALANCE";
            this.LBL51_SNBALANCE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNBALANCE.TabIndex = 39;
            this.LBL51_SNBALANCE.Text = "BALANCE";
            this.LBL51_SNBALANCE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNBALANCE
            // 
            this.TXT01_SNBALANCE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNBALANCE.FactoryID = "";
            this.TXT01_SNBALANCE.FactoryName = null;
            this.TXT01_SNBALANCE.Location = new System.Drawing.Point(523, 96);
            this.TXT01_SNBALANCE.MinLength = 0;
            this.TXT01_SNBALANCE.Name = "TXT01_SNBALANCE";
            this.TXT01_SNBALANCE.Size = new System.Drawing.Size(80, 21);
            this.TXT01_SNBALANCE.TabIndex = 38;
            this.TXT01_SNBALANCE.TabIndexCustom = false;
            // 
            // TXT01_SNHIGH
            // 
            this.TXT01_SNHIGH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNHIGH.FactoryID = "";
            this.TXT01_SNHIGH.FactoryName = null;
            this.TXT01_SNHIGH.Location = new System.Drawing.Point(111, 15);
            this.TXT01_SNHIGH.MinLength = 0;
            this.TXT01_SNHIGH.Name = "TXT01_SNHIGH";
            this.TXT01_SNHIGH.Size = new System.Drawing.Size(80, 21);
            this.TXT01_SNHIGH.TabIndex = 46;
            this.TXT01_SNHIGH.TabIndexCustom = false;
            // 
            // LBL51_SNHIGH
            // 
            this.LBL51_SNHIGH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNHIGH.FactoryID = "";
            this.LBL51_SNHIGH.FactoryName = null;
            this.LBL51_SNHIGH.IsCreated = false;
            this.LBL51_SNHIGH.Location = new System.Drawing.Point(6, 15);
            this.LBL51_SNHIGH.Name = "LBL51_SNHIGH";
            this.LBL51_SNHIGH.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNHIGH.TabIndex = 47;
            this.LBL51_SNHIGH.Text = "높이";
            this.LBL51_SNHIGH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNNOTE
            // 
            this.TXT01_SNNOTE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNNOTE.FactoryID = "";
            this.TXT01_SNNOTE.FactoryName = null;
            this.TXT01_SNNOTE.Location = new System.Drawing.Point(111, 94);
            this.TXT01_SNNOTE.MinLength = 0;
            this.TXT01_SNNOTE.Name = "TXT01_SNNOTE";
            this.TXT01_SNNOTE.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SNNOTE.TabIndex = 34;
            this.TXT01_SNNOTE.TabIndexCustom = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LBL51_SNMTQTY);
            this.groupBox2.Controls.Add(this.TXT01_SNMTQTY);
            this.groupBox2.Controls.Add(this.LBL51_SNHWAMUL);
            this.groupBox2.Controls.Add(this.CBH01_SNHWAMUL);
            this.groupBox2.Controls.Add(this.LBL51_SNNOTE);
            this.groupBox2.Controls.Add(this.LBL51_SNBALANCE);
            this.groupBox2.Controls.Add(this.TXT01_SNBALANCE);
            this.groupBox2.Controls.Add(this.TXT01_SNGKLQTY);
            this.groupBox2.Controls.Add(this.TXT01_SNHIGH);
            this.groupBox2.Controls.Add(this.LBL51_SNHIGH);
            this.groupBox2.Controls.Add(this.LBL51_SNGKLQTY);
            this.groupBox2.Controls.Add(this.LBL51_SNNKLQTY);
            this.groupBox2.Controls.Add(this.TXT01_SNNKLQTY);
            this.groupBox2.Controls.Add(this.LBL51_SNTEMP);
            this.groupBox2.Controls.Add(this.TXT01_SNTEMP);
            this.groupBox2.Controls.Add(this.TXT01_SNNOTE);
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(852, 127);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // LBL51_SNMTQTY
            // 
            this.LBL51_SNMTQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNMTQTY.FactoryID = "";
            this.LBL51_SNMTQTY.FactoryName = null;
            this.LBL51_SNMTQTY.IsCreated = false;
            this.LBL51_SNMTQTY.Location = new System.Drawing.Point(417, 69);
            this.LBL51_SNMTQTY.Name = "LBL51_SNMTQTY";
            this.LBL51_SNMTQTY.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNMTQTY.TabIndex = 95;
            this.LBL51_SNMTQTY.Text = "M/T";
            this.LBL51_SNMTQTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNMTQTY
            // 
            this.TXT01_SNMTQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNMTQTY.FactoryID = "";
            this.TXT01_SNMTQTY.FactoryName = null;
            this.TXT01_SNMTQTY.Location = new System.Drawing.Point(523, 69);
            this.TXT01_SNMTQTY.MinLength = 0;
            this.TXT01_SNMTQTY.Name = "TXT01_SNMTQTY";
            this.TXT01_SNMTQTY.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SNMTQTY.TabIndex = 94;
            this.TXT01_SNMTQTY.TabIndexCustom = false;
            // 
            // LBL51_SNHWAMUL
            // 
            this.LBL51_SNHWAMUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNHWAMUL.FactoryID = "";
            this.LBL51_SNHWAMUL.FactoryName = null;
            this.LBL51_SNHWAMUL.IsCreated = false;
            this.LBL51_SNHWAMUL.Location = new System.Drawing.Point(5, 67);
            this.LBL51_SNHWAMUL.Name = "LBL51_SNHWAMUL";
            this.LBL51_SNHWAMUL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNHWAMUL.TabIndex = 7;
            this.LBL51_SNHWAMUL.Text = "화물";
            this.LBL51_SNHWAMUL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_SNHWAMUL
            // 
            this.CBH01_SNHWAMUL.BindedDataRow = null;
            this.CBH01_SNHWAMUL.CodeBoxWidth = 0;
            this.CBH01_SNHWAMUL.DummyValue = null;
            this.CBH01_SNHWAMUL.FactoryID = "";
            this.CBH01_SNHWAMUL.FactoryName = null;
            this.CBH01_SNHWAMUL.Location = new System.Drawing.Point(111, 68);
            this.CBH01_SNHWAMUL.MinLength = 0;
            this.CBH01_SNHWAMUL.Name = "CBH01_SNHWAMUL";
            this.CBH01_SNHWAMUL.Size = new System.Drawing.Size(300, 20);
            this.CBH01_SNHWAMUL.TabIndex = 6;
            // 
            // LBL51_SNNOTE
            // 
            this.LBL51_SNNOTE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNNOTE.FactoryID = "";
            this.LBL51_SNNOTE.FactoryName = null;
            this.LBL51_SNNOTE.IsCreated = false;
            this.LBL51_SNNOTE.Location = new System.Drawing.Point(6, 95);
            this.LBL51_SNNOTE.Name = "LBL51_SNNOTE";
            this.LBL51_SNNOTE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNNOTE.TabIndex = 5;
            this.LBL51_SNNOTE.Text = "장부";
            this.LBL51_SNNOTE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNGKLQTY
            // 
            this.TXT01_SNGKLQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNGKLQTY.FactoryID = "";
            this.TXT01_SNGKLQTY.FactoryName = null;
            this.TXT01_SNGKLQTY.Location = new System.Drawing.Point(111, 41);
            this.TXT01_SNGKLQTY.MinLength = 0;
            this.TXT01_SNGKLQTY.Name = "TXT01_SNGKLQTY";
            this.TXT01_SNGKLQTY.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SNGKLQTY.TabIndex = 32;
            this.TXT01_SNGKLQTY.TabIndexCustom = false;
            // 
            // LBL51_SNGKLQTY
            // 
            this.LBL51_SNGKLQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNGKLQTY.FactoryID = "";
            this.LBL51_SNGKLQTY.FactoryName = null;
            this.LBL51_SNGKLQTY.IsCreated = false;
            this.LBL51_SNGKLQTY.Location = new System.Drawing.Point(5, 40);
            this.LBL51_SNGKLQTY.Name = "LBL51_SNGKLQTY";
            this.LBL51_SNGKLQTY.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNGKLQTY.TabIndex = 33;
            this.LBL51_SNGKLQTY.Text = "G-KL";
            this.LBL51_SNGKLQTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_SNNKLQTY
            // 
            this.LBL51_SNNKLQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNNKLQTY.FactoryID = "";
            this.LBL51_SNNKLQTY.FactoryName = null;
            this.LBL51_SNNKLQTY.IsCreated = false;
            this.LBL51_SNNKLQTY.Location = new System.Drawing.Point(417, 42);
            this.LBL51_SNNKLQTY.Name = "LBL51_SNNKLQTY";
            this.LBL51_SNNKLQTY.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNNKLQTY.TabIndex = 85;
            this.LBL51_SNNKLQTY.Text = "N-KL";
            this.LBL51_SNNKLQTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNNKLQTY
            // 
            this.TXT01_SNNKLQTY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNNKLQTY.FactoryID = "";
            this.TXT01_SNNKLQTY.FactoryName = null;
            this.TXT01_SNNKLQTY.Location = new System.Drawing.Point(523, 42);
            this.TXT01_SNNKLQTY.MinLength = 0;
            this.TXT01_SNNKLQTY.Name = "TXT01_SNNKLQTY";
            this.TXT01_SNNKLQTY.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SNNKLQTY.TabIndex = 84;
            this.TXT01_SNNKLQTY.TabIndexCustom = false;
            // 
            // LBL51_SNTEMP
            // 
            this.LBL51_SNTEMP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNTEMP.FactoryID = "";
            this.LBL51_SNTEMP.FactoryName = null;
            this.LBL51_SNTEMP.IsCreated = false;
            this.LBL51_SNTEMP.Location = new System.Drawing.Point(417, 15);
            this.LBL51_SNTEMP.Name = "LBL51_SNTEMP";
            this.LBL51_SNTEMP.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNTEMP.TabIndex = 73;
            this.LBL51_SNTEMP.Text = "온도";
            this.LBL51_SNTEMP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_SNTEMP
            // 
            this.TXT01_SNTEMP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNTEMP.FactoryID = "";
            this.TXT01_SNTEMP.FactoryName = null;
            this.TXT01_SNTEMP.Location = new System.Drawing.Point(523, 15);
            this.TXT01_SNTEMP.MinLength = 0;
            this.TXT01_SNTEMP.Name = "TXT01_SNTEMP";
            this.TXT01_SNTEMP.Size = new System.Drawing.Size(64, 21);
            this.TXT01_SNTEMP.TabIndex = 72;
            this.TXT01_SNTEMP.TabIndexCustom = false;
            // 
            // TXT01_SNTANKNO
            // 
            this.TXT01_SNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SNTANKNO.FactoryID = "";
            this.TXT01_SNTANKNO.FactoryName = null;
            this.TXT01_SNTANKNO.Location = new System.Drawing.Point(523, 12);
            this.TXT01_SNTANKNO.MinLength = 0;
            this.TXT01_SNTANKNO.Name = "TXT01_SNTANKNO";
            this.TXT01_SNTANKNO.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SNTANKNO.TabIndex = 70;
            this.TXT01_SNTANKNO.TabIndexCustom = false;
            // 
            // LBL51_SNTANKNO
            // 
            this.LBL51_SNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SNTANKNO.FactoryID = "";
            this.LBL51_SNTANKNO.FactoryName = null;
            this.LBL51_SNTANKNO.IsCreated = false;
            this.LBL51_SNTANKNO.Location = new System.Drawing.Point(417, 12);
            this.LBL51_SNTANKNO.Name = "LBL51_SNTANKNO";
            this.LBL51_SNTANKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SNTANKNO.TabIndex = 71;
            this.LBL51_SNTANKNO.Text = "TANK번호";
            this.LBL51_SNTANKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_SNDATE
            // 
            this.DTP01_SNDATE.FactoryID = "";
            this.DTP01_SNDATE.FactoryName = null;
            this.DTP01_SNDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_SNDATE.Name = "DTP01_SNDATE";
            this.DTP01_SNDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SNDATE.TabIndex = 28;
            // 
            // TYUTIN008I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 177);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUTIN008I";
            this.Load += new System.EventHandler(this.TYUTIN008I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private Service.Library.Controls.TYLabel LBL51_SNNOTE;
        private Service.Library.Controls.TYLabel LBL51_SNDATE;
        private Service.Library.Controls.TYTextBox TXT01_SNGKLQTY;
        private Service.Library.Controls.TYLabel LBL51_SNGKLQTY;
        private Service.Library.Controls.TYTextBox TXT01_SNNOTE;
        private Service.Library.Controls.TYTextBox TXT01_SNBALANCE;
        private Service.Library.Controls.TYLabel LBL51_SNBALANCE;
        private Service.Library.Controls.TYTextBox TXT01_SNHIGH;
        private Service.Library.Controls.TYLabel LBL51_SNHIGH;
        private Service.Library.Controls.TYTextBox TXT01_SNTEMP;
        private Service.Library.Controls.TYLabel LBL51_SNTEMP;
        private Service.Library.Controls.TYTextBox TXT01_SNNKLQTY;
        private Service.Library.Controls.TYLabel LBL51_SNNKLQTY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Service.Library.Controls.TYLabel LBL51_SNHWAMUL;
        private Service.Library.Controls.TYCodeBox CBH01_SNHWAMUL;
        private Service.Library.Controls.TYTextBox TXT01_SNTANKNO;
        private Service.Library.Controls.TYLabel LBL51_SNTANKNO;
        private Service.Library.Controls.TYLabel LBL51_SNMTQTY;
        private Service.Library.Controls.TYTextBox TXT01_SNMTQTY;
        private Service.Library.Controls.TYDatePicker DTP01_SNDATE;
    }
}