namespace TY.ER.HR00
{
    partial class TYHRKB013I
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
            this.CBH01_GISABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GISABUN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GICHLTIME = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GIDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GIENDTIME = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GIYACHLTM = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_GIYAENDTM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_GIDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.MTB01_GICHLTIME = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.MTB01_GIENDTIME = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.MTB01_GIYACHLTM = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.MTB01_GIYAENDTM = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.TXT01_SYYOILCD = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SYYOILCD = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(564, 12);
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
            this.BTN61_SAV.Location = new System.Drawing.Point(483, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBH01_GISABUN
            // 
            this.CBH01_GISABUN.BindedDataRow = null;
            this.CBH01_GISABUN.CodeBoxWidth = 0;
            this.CBH01_GISABUN.DummyValue = null;
            this.CBH01_GISABUN.FactoryID = "";
            this.CBH01_GISABUN.FactoryName = null;
            this.CBH01_GISABUN.Location = new System.Drawing.Point(111, 39);
            this.CBH01_GISABUN.MinLength = 0;
            this.CBH01_GISABUN.Name = "CBH01_GISABUN";
            this.CBH01_GISABUN.Size = new System.Drawing.Size(150, 20);
            this.CBH01_GISABUN.TabIndex = 2;
            // 
            // LBL51_GISABUN
            // 
            this.LBL51_GISABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GISABUN.FactoryID = "";
            this.LBL51_GISABUN.FactoryName = null;
            this.LBL51_GISABUN.IsCreated = false;
            this.LBL51_GISABUN.Location = new System.Drawing.Point(5, 39);
            this.LBL51_GISABUN.Name = "LBL51_GISABUN";
            this.LBL51_GISABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GISABUN.TabIndex = 3;
            this.LBL51_GISABUN.Text = "사원번호";
            this.LBL51_GISABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GICHLTIME
            // 
            this.LBL51_GICHLTIME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GICHLTIME.FactoryID = "";
            this.LBL51_GICHLTIME.FactoryName = null;
            this.LBL51_GICHLTIME.IsCreated = false;
            this.LBL51_GICHLTIME.Location = new System.Drawing.Point(5, 66);
            this.LBL51_GICHLTIME.Name = "LBL51_GICHLTIME";
            this.LBL51_GICHLTIME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GICHLTIME.TabIndex = 5;
            this.LBL51_GICHLTIME.Text = "주간출근시간";
            this.LBL51_GICHLTIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GIDATE
            // 
            this.LBL51_GIDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GIDATE.FactoryID = "";
            this.LBL51_GIDATE.FactoryName = null;
            this.LBL51_GIDATE.IsCreated = false;
            this.LBL51_GIDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GIDATE.Name = "LBL51_GIDATE";
            this.LBL51_GIDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GIDATE.TabIndex = 7;
            this.LBL51_GIDATE.Text = "근태일자";
            this.LBL51_GIDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GIENDTIME
            // 
            this.LBL51_GIENDTIME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GIENDTIME.FactoryID = "";
            this.LBL51_GIENDTIME.FactoryName = null;
            this.LBL51_GIENDTIME.IsCreated = false;
            this.LBL51_GIENDTIME.Location = new System.Drawing.Point(217, 66);
            this.LBL51_GIENDTIME.Name = "LBL51_GIENDTIME";
            this.LBL51_GIENDTIME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GIENDTIME.TabIndex = 9;
            this.LBL51_GIENDTIME.Text = "주간퇴근시간";
            this.LBL51_GIENDTIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GIYACHLTM
            // 
            this.LBL51_GIYACHLTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GIYACHLTM.FactoryID = "";
            this.LBL51_GIYACHLTM.FactoryName = null;
            this.LBL51_GIYACHLTM.IsCreated = false;
            this.LBL51_GIYACHLTM.Location = new System.Drawing.Point(5, 93);
            this.LBL51_GIYACHLTM.Name = "LBL51_GIYACHLTM";
            this.LBL51_GIYACHLTM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GIYACHLTM.TabIndex = 11;
            this.LBL51_GIYACHLTM.Text = "야간출근시간";
            this.LBL51_GIYACHLTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_GIYAENDTM
            // 
            this.LBL51_GIYAENDTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GIYAENDTM.FactoryID = "";
            this.LBL51_GIYAENDTM.FactoryName = null;
            this.LBL51_GIYAENDTM.IsCreated = false;
            this.LBL51_GIYAENDTM.Location = new System.Drawing.Point(217, 93);
            this.LBL51_GIYAENDTM.Name = "LBL51_GIYAENDTM";
            this.LBL51_GIYAENDTM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GIYAENDTM.TabIndex = 13;
            this.LBL51_GIYAENDTM.Text = "야간퇴근시간";
            this.LBL51_GIYAENDTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SYYOILCD);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SYYOILCD);
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GIYAENDTM);
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GIYACHLTM);
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GIENDTIME);
            this.GBX80_CONTROLS.Controls.Add(this.MTB01_GICHLTIME);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_GISABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GISABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GICHLTIME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GIENDTIME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GIYACHLTM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GIYAENDTM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(644, 147);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_GIDATE
            // 
            this.DTP01_GIDATE.FactoryID = "";
            this.DTP01_GIDATE.FactoryName = null;
            this.DTP01_GIDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_GIDATE.Name = "DTP01_GIDATE";
            this.DTP01_GIDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GIDATE.TabIndex = 14;
            this.DTP01_GIDATE.ValueChanged += new System.EventHandler(this.DTP01_GIDATE_ValueChanged);
            // 
            // MTB01_GICHLTIME
            // 
            this.MTB01_GICHLTIME.FactoryID = "";
            this.MTB01_GICHLTIME.FactoryName = null;
            this.MTB01_GICHLTIME.Font = new System.Drawing.Font("새굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MTB01_GICHLTIME.Location = new System.Drawing.Point(111, 66);
            this.MTB01_GICHLTIME.Mask = "00:00";
            this.MTB01_GICHLTIME.Name = "MTB01_GICHLTIME";
            this.MTB01_GICHLTIME.Size = new System.Drawing.Size(49, 22);
            this.MTB01_GICHLTIME.TabIndex = 145;
            this.MTB01_GICHLTIME.Text = "1200";
            // 
            // MTB01_GIENDTIME
            // 
            this.MTB01_GIENDTIME.FactoryID = "";
            this.MTB01_GIENDTIME.FactoryName = null;
            this.MTB01_GIENDTIME.Font = new System.Drawing.Font("새굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MTB01_GIENDTIME.Location = new System.Drawing.Point(323, 66);
            this.MTB01_GIENDTIME.Mask = "00:00";
            this.MTB01_GIENDTIME.Name = "MTB01_GIENDTIME";
            this.MTB01_GIENDTIME.Size = new System.Drawing.Size(49, 22);
            this.MTB01_GIENDTIME.TabIndex = 146;
            this.MTB01_GIENDTIME.Text = "1200";
            // 
            // MTB01_GIYACHLTM
            // 
            this.MTB01_GIYACHLTM.FactoryID = "";
            this.MTB01_GIYACHLTM.FactoryName = null;
            this.MTB01_GIYACHLTM.Font = new System.Drawing.Font("새굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MTB01_GIYACHLTM.Location = new System.Drawing.Point(111, 93);
            this.MTB01_GIYACHLTM.Mask = "00:00";
            this.MTB01_GIYACHLTM.Name = "MTB01_GIYACHLTM";
            this.MTB01_GIYACHLTM.Size = new System.Drawing.Size(49, 22);
            this.MTB01_GIYACHLTM.TabIndex = 149;
            this.MTB01_GIYACHLTM.Text = "1200";
            // 
            // MTB01_GIYAENDTM
            // 
            this.MTB01_GIYAENDTM.FactoryID = "";
            this.MTB01_GIYAENDTM.FactoryName = null;
            this.MTB01_GIYAENDTM.Font = new System.Drawing.Font("새굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MTB01_GIYAENDTM.Location = new System.Drawing.Point(323, 93);
            this.MTB01_GIYAENDTM.Mask = "00:00";
            this.MTB01_GIYAENDTM.Name = "MTB01_GIYAENDTM";
            this.MTB01_GIYAENDTM.Size = new System.Drawing.Size(49, 22);
            this.MTB01_GIYAENDTM.TabIndex = 151;
            this.MTB01_GIYAENDTM.Text = "1200";
            // 
            // TXT01_SYYOILCD
            // 
            this.TXT01_SYYOILCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SYYOILCD.FactoryID = "";
            this.TXT01_SYYOILCD.FactoryName = null;
            this.TXT01_SYYOILCD.Location = new System.Drawing.Point(323, 12);
            this.TXT01_SYYOILCD.MinLength = 0;
            this.TXT01_SYYOILCD.Name = "TXT01_SYYOILCD";
            this.TXT01_SYYOILCD.Size = new System.Drawing.Size(100, 21);
            this.TXT01_SYYOILCD.TabIndex = 152;
            // 
            // LBL51_SYYOILCD
            // 
            this.LBL51_SYYOILCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SYYOILCD.FactoryID = "";
            this.LBL51_SYYOILCD.FactoryName = null;
            this.LBL51_SYYOILCD.IsCreated = false;
            this.LBL51_SYYOILCD.Location = new System.Drawing.Point(217, 12);
            this.LBL51_SYYOILCD.Name = "LBL51_SYYOILCD";
            this.LBL51_SYYOILCD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SYYOILCD.TabIndex = 153;
            this.LBL51_SYYOILCD.Text = "요일";
            this.LBL51_SYYOILCD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYHRKB013I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 149);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRKB013I";
            this.Load += new System.EventHandler(this.TYHRKB013I_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYCodeBox CBH01_GISABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_GISABUN;
        private TY.Service.Library.Controls.TYLabel LBL51_GICHLTIME;
        private TY.Service.Library.Controls.TYLabel LBL51_GIDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GIENDTIME;
        private TY.Service.Library.Controls.TYLabel LBL51_GIYACHLTM;
        private TY.Service.Library.Controls.TYLabel LBL51_GIYAENDTM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_GIDATE;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GICHLTIME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GIENDTIME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GIYACHLTM;
        private Service.Library.Controls.TYMaskedTextBox MTB01_GIYAENDTM;
        private Service.Library.Controls.TYTextBox TXT01_SYYOILCD;
        private Service.Library.Controls.TYLabel LBL51_SYYOILCD;
    }
}