namespace TY.ER.HR00
{
    partial class TYHRKB018B
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
            this.LBL51_TOYEAR = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_TOYEAR = new TY.Service.Library.Controls.TYCodeBox();
            this.pgb_Status = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(485, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_TOYEAR
            // 
            this.LBL51_TOYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_TOYEAR.FactoryID = "";
            this.LBL51_TOYEAR.FactoryName = null;
            this.LBL51_TOYEAR.IsCreated = false;
            this.LBL51_TOYEAR.Location = new System.Drawing.Point(5, 12);
            this.LBL51_TOYEAR.Name = "LBL51_TOYEAR";
            this.LBL51_TOYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_TOYEAR.TabIndex = 2;
            this.LBL51_TOYEAR.Text = "년도";
            this.LBL51_TOYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.pgb_Status);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_TOYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_TOYEAR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(566, 54);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_TOYEAR
            // 
            this.CBH01_TOYEAR.BindedDataRow = null;
            this.CBH01_TOYEAR.CodeBoxWidth = 0;
            this.CBH01_TOYEAR.DummyValue = null;
            this.CBH01_TOYEAR.FactoryID = "";
            this.CBH01_TOYEAR.FactoryName = null;
            this.CBH01_TOYEAR.Location = new System.Drawing.Point(111, 12);
            this.CBH01_TOYEAR.MinLength = 0;
            this.CBH01_TOYEAR.Name = "CBH01_TOYEAR";
            this.CBH01_TOYEAR.Size = new System.Drawing.Size(111, 20);
            this.CBH01_TOYEAR.TabIndex = 52;
            // 
            // pgb_Status
            // 
            this.pgb_Status.Location = new System.Drawing.Point(5, 36);
            this.pgb_Status.Name = "pgb_Status";
            this.pgb_Status.Size = new System.Drawing.Size(555, 12);
            this.pgb_Status.Step = 1;
            this.pgb_Status.TabIndex = 53;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TYHRKB018B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 58);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRKB018B";
            this.Load += new System.EventHandler(this.TYHRKB018B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_TOYEAR;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYCodeBox CBH01_TOYEAR;
        private System.Windows.Forms.ProgressBar pgb_Status;
        private System.Windows.Forms.Timer timer1;
    }
}