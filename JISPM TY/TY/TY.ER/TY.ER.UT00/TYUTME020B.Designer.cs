namespace TY.ER.UT00
{
    partial class TYUTME020B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.LBL51_MCDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_MCHWAMUL = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_MCHWAMUL = new TY.Service.Library.Controls.TYCodeBox();
            this.CBH01_MCHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_MCHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_MCDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.CBO01_VNCLGUBN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_VNCLGUBN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(512, 497);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "처리";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_MCDATE
            // 
            this.LBL51_MCDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCDATE.FactoryID = "";
            this.LBL51_MCDATE.FactoryName = null;
            this.LBL51_MCDATE.IsCreated = false;
            this.LBL51_MCDATE.Location = new System.Drawing.Point(406, 361);
            this.LBL51_MCDATE.Name = "LBL51_MCDATE";
            this.LBL51_MCDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MCDATE.TabIndex = 4;
            this.LBL51_MCDATE.Text = "기준일자";
            this.LBL51_MCDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_VNCLGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_VNCLGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_MCHWAMUL);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_MCHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_MCDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(406, 466);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 319;
            this.LBL51_GGUBUN.Text = "생성구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(512, 466);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GGUBUN.TabIndex = 318;
            // 
            // LBL51_MCHWAMUL
            // 
            this.LBL51_MCHWAMUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCHWAMUL.FactoryID = "";
            this.LBL51_MCHWAMUL.FactoryName = null;
            this.LBL51_MCHWAMUL.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_MCHWAMUL.ForeColor = System.Drawing.Color.Black;
            this.LBL51_MCHWAMUL.IsCreated = false;
            this.LBL51_MCHWAMUL.Location = new System.Drawing.Point(406, 414);
            this.LBL51_MCHWAMUL.Name = "LBL51_MCHWAMUL";
            this.LBL51_MCHWAMUL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MCHWAMUL.TabIndex = 317;
            this.LBL51_MCHWAMUL.Text = "화 물";
            this.LBL51_MCHWAMUL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_MCHWAMUL
            // 
            this.CBH01_MCHWAMUL.BindedDataRow = null;
            this.CBH01_MCHWAMUL.CodeBoxWidth = 0;
            this.CBH01_MCHWAMUL.DummyValue = null;
            this.CBH01_MCHWAMUL.FactoryID = "";
            this.CBH01_MCHWAMUL.FactoryName = null;
            this.CBH01_MCHWAMUL.Font = new System.Drawing.Font("굴림", 9F);
            this.CBH01_MCHWAMUL.Location = new System.Drawing.Point(512, 414);
            this.CBH01_MCHWAMUL.MinLength = 0;
            this.CBH01_MCHWAMUL.Name = "CBH01_MCHWAMUL";
            this.CBH01_MCHWAMUL.Size = new System.Drawing.Size(300, 20);
            this.CBH01_MCHWAMUL.TabIndex = 316;
            // 
            // CBH01_MCHWAJU
            // 
            this.CBH01_MCHWAJU.BindedDataRow = null;
            this.CBH01_MCHWAJU.CodeBoxWidth = 0;
            this.CBH01_MCHWAJU.DummyValue = null;
            this.CBH01_MCHWAJU.FactoryID = "";
            this.CBH01_MCHWAJU.FactoryName = null;
            this.CBH01_MCHWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.CBH01_MCHWAJU.Location = new System.Drawing.Point(512, 388);
            this.CBH01_MCHWAJU.MinLength = 0;
            this.CBH01_MCHWAJU.Name = "CBH01_MCHWAJU";
            this.CBH01_MCHWAJU.Size = new System.Drawing.Size(300, 20);
            this.CBH01_MCHWAJU.TabIndex = 315;
            // 
            // LBL51_MCHWAJU
            // 
            this.LBL51_MCHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCHWAJU.FactoryID = "";
            this.LBL51_MCHWAJU.FactoryName = null;
            this.LBL51_MCHWAJU.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_MCHWAJU.ForeColor = System.Drawing.Color.Black;
            this.LBL51_MCHWAJU.IsCreated = false;
            this.LBL51_MCHWAJU.Location = new System.Drawing.Point(406, 388);
            this.LBL51_MCHWAJU.Name = "LBL51_MCHWAJU";
            this.LBL51_MCHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_MCHWAJU.TabIndex = 314;
            this.LBL51_MCHWAJU.Text = "화 주";
            this.LBL51_MCHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_MCDATE
            // 
            this.DTP01_MCDATE.FactoryID = "";
            this.DTP01_MCDATE.FactoryName = null;
            this.DTP01_MCDATE.Location = new System.Drawing.Point(512, 361);
            this.DTP01_MCDATE.Name = "DTP01_MCDATE";
            this.DTP01_MCDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_MCDATE.TabIndex = 156;
            // 
            // CBO01_VNCLGUBN
            // 
            this.CBO01_VNCLGUBN.FactoryID = "";
            this.CBO01_VNCLGUBN.FactoryName = null;
            this.CBO01_VNCLGUBN.Location = new System.Drawing.Point(512, 440);
            this.CBO01_VNCLGUBN.Name = "CBO01_VNCLGUBN";
            this.CBO01_VNCLGUBN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_VNCLGUBN.TabIndex = 336;
            // 
            // LBL51_VNCLGUBN
            // 
            this.LBL51_VNCLGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_VNCLGUBN.FactoryID = "";
            this.LBL51_VNCLGUBN.FactoryName = null;
            this.LBL51_VNCLGUBN.ForeColor = System.Drawing.Color.Black;
            this.LBL51_VNCLGUBN.IsCreated = false;
            this.LBL51_VNCLGUBN.Location = new System.Drawing.Point(406, 440);
            this.LBL51_VNCLGUBN.Name = "LBL51_VNCLGUBN";
            this.LBL51_VNCLGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_VNCLGUBN.TabIndex = 335;
            this.LBL51_VNCLGUBN.Text = "매출 마감";
            this.LBL51_VNCLGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUTME020B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME020B";
            this.Load += new System.EventHandler(this.TYUTME020B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYLabel LBL51_MCDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_MCDATE;
        private Service.Library.Controls.TYLabel LBL51_MCHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_MCHWAJU;
        private Service.Library.Controls.TYLabel LBL51_MCHWAMUL;
        private Service.Library.Controls.TYCodeBox CBH01_MCHWAMUL;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_VNCLGUBN;
        private Service.Library.Controls.TYLabel LBL51_VNCLGUBN;
    }
}