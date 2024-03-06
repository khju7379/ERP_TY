using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using System.IO;

namespace TY.ER.GB00
{
    public partial class TYERGB010P : TYBase
    {
        private string _claim_No;
        private byte[] _AttachFile;

        public TYERGB010P()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYERGB010P(string claim_No)
            : this()
        {
            this._claim_No = claim_No;
        }

        private void TYERGB010P_Load(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_25U2C732", this._claim_No);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.Close();
                return;
            }

            this.CurrentDataTableRowMapping(dt, "01");
            this._AttachFile = dt.Rows[0]["ATTACH_FILE"] as byte[];
            this.BTN61_DWN.SetReadOnly(this._AttachFile == null);
            this.SetStartingFocus(this.BTN61_CLO);
            this.ReadOnly_Controls("01");
        }

        private void BTN61_CLO_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            FileStream stream = null;

            try
            {
                this.sfd01_FILE.FileName = this.TXT01_ATTACH_FILENAME.GetValue().ToString();
                if (this.sfd01_FILE.ShowDialog() == DialogResult.Cancel)
                    return;

                string fileName = this.sfd01_FILE.FileName;
                int ArraySize = _AttachFile.GetUpperBound(0);
                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_AttachFile, 0, ArraySize + 1);

                this.ShowMessage("TY_M_GB_25UAA711");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
