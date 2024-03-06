namespace TY.ER.UT00
{
    partial class TYUTME019B
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
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_MCDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(512, 432);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "월말 생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // LBL51_MCDATE
            // 
            this.LBL51_MCDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_MCDATE.FactoryID = "";
            this.LBL51_MCDATE.FactoryName = null;
            this.LBL51_MCDATE.IsCreated = false;
            this.LBL51_MCDATE.Location = new System.Drawing.Point(406, 373);
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
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label2);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_MCDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_MCDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(635, 400);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 329;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 328;
            this.label2.Text = "-";
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.Font = new System.Drawing.Font("굴림", 9F);
            this.LBL51_STDATE.ForeColor = System.Drawing.Color.Black;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(406, 400);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 314;
            this.LBL51_STDATE.Text = "작업일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(512, 400);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 313;
            // 
            // DTP01_MCDATE
            // 
            this.DTP01_MCDATE.FactoryID = "";
            this.DTP01_MCDATE.FactoryName = null;
            this.DTP01_MCDATE.Location = new System.Drawing.Point(512, 373);
            this.DTP01_MCDATE.Name = "DTP01_MCDATE";
            this.DTP01_MCDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_MCDATE.TabIndex = 156;
            // 
            // TYUTME019B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME019B";
            this.Load += new System.EventHandler(this.TYUTME019B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYLabel LBL51_MCDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_MCDATE;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYLabel LBL51_STDATE;
        private System.Windows.Forms.Label label2;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
    }
}