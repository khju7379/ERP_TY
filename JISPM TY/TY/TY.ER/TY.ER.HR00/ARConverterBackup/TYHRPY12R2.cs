using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRPY12R2.
    /// </summary>
    public partial class TYHRPY12R2 : DataDynamics.ActiveReports.ActiveReport
    {
        DataTable _dt = new DataTable();
        DataTable _dtSum = new DataTable();
        int _iRowCount = 0;
        int _iCount = 0;
        int _iBuseoCount = 0;

        public TYHRPY12R2(DataTable dt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dtSum = dt;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            if (_iRowCount > 0)
            {
                if (((_iRowCount) % 5) == 0)
                {
                    MONTH.Text = _dt.Rows[_iCount - 1]["REYYMM"].ToString().Substring(4, 2);
                    PMBUSEO.Text = _dt.Rows[_iCount - 1]["REDEPT"].ToString();
                    PMBUSEONM.Text = _dt.Rows[_iCount - 1]["REDEPTNM"].ToString();
                    PMJIDATE.Text = _dt.Rows[_iCount - 1]["REJIDATE"].ToString();
                }
                else
                {
                    MONTH.Text = _dt.Rows[_iCount]["REYYMM"].ToString().Substring(4, 2);
                    PMBUSEO.Text = _dt.Rows[_iCount]["REDEPT"].ToString();
                    PMBUSEONM.Text = _dt.Rows[_iCount]["REDEPTNM"].ToString();
                    PMJIDATE.Text = _dt.Rows[_iCount]["REJIDATE"].ToString();
                }
            }
            else
            {
                MONTH.Text = _dt.Rows[_iCount]["REYYMM"].ToString().Substring(4, 2);
                PMBUSEO.Text = _dt.Rows[_iCount]["REDEPT"].ToString();
                PMBUSEONM.Text = _dt.Rows[_iCount]["REDEPTNM"].ToString();
                PMJIDATE.Text = _dt.Rows[_iCount]["REJIDATE"].ToString();
            }

            if (_dt.Rows[_iCount]["REJPTITILE1"].ToString() == "")
            {
                REJPAMOUNT1.Visible = false;
                PSPAYBUSEO1.Visible = false;
                PSPAYTOTAL1.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE2"].ToString() == "")
            {
                REJPAMOUNT2.Visible = false;
                PSPAYBUSEO2.Visible = false;
                PSPAYTOTAL2.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE3"].ToString() == "")
            {
                REJPAMOUNT3.Visible = false;
                PSPAYBUSEO3.Visible = false;
                PSPAYTOTAL3.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE4"].ToString() == "")
            {
                REJPAMOUNT4.Visible = false;
                PSPAYBUSEO4.Visible = false;
                PSPAYTOTAL4.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE5"].ToString() == "")
            {
                REJPAMOUNT5.Visible = false;
                PSPAYBUSEO5.Visible = false;
                PSPAYTOTAL5.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE6"].ToString() == "")
            {
                REJPAMOUNT6.Visible = false;
                PSPAYBUSEO6.Visible = false;
                PSPAYTOTAL6.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE7"].ToString() == "")
            {
                REJPAMOUNT7.Visible = false;
                PSPAYBUSEO7.Visible = false;
                PSPAYTOTAL7.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE8"].ToString() == "")
            {
                REJPAMOUNT8.Visible = false;
                PSPAYBUSEO8.Visible = false;
                PSPAYTOTAL8.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE9"].ToString() == "")
            {
                REJPAMOUNT9.Visible = false;
                PSPAYBUSEO9.Visible = false;
                PSPAYTOTAL9.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE10"].ToString() == "")
            {
                REJPAMOUNT10.Visible = false;
                PSPAYBUSEO10.Visible = false;
                PSPAYTOTAL10.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE11"].ToString() == "")
            {
                REJPAMOUNT11.Visible = false;
                PSPAYBUSEO11.Visible = false;
                PSPAYTOTAL11.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE12"].ToString() == "")
            {
                REJPAMOUNT12.Visible = false;
                PSPAYBUSEO12.Visible = false;
                PSPAYTOTAL12.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE13"].ToString() == "")
            {
                REJPAMOUNT13.Visible = false;
                PSPAYBUSEO13.Visible = false;
                PSPAYTOTAL13.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE14"].ToString() == "")
            {
                REJPAMOUNT14.Visible = false;
                PSPAYBUSEO14.Visible = false;
                PSPAYTOTAL14.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE15"].ToString() == "")
            {
                REJPAMOUNT15.Visible = false;
                PSPAYBUSEO15.Visible = false;
                PSPAYTOTAL15.Visible = false;
            }
            if (_dt.Rows[_iCount]["REJPTITILE16"].ToString() == "")
            {
                REJPAMOUNT16.Visible = false;
                PSPAYBUSEO16.Visible = false;
                PSPAYTOTAL16.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE1"].ToString() == "")
            {
                REGPAMOUNT1.Visible = false;
                PSPAYBUSEO17.Visible = false;
                PSPAYTOTAL17.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE2"].ToString() == "")
            {
                REGPAMOUNT2.Visible = false;
                PSPAYBUSEO18.Visible = false;
                PSPAYTOTAL18.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE3"].ToString() == "")
            {
                REGPAMOUNT3.Visible = false;
                PSPAYBUSEO19.Visible = false;
                PSPAYTOTAL19.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE4"].ToString() == "")
            {
                REGPAMOUNT4.Visible = false;
                PSPAYBUSEO20.Visible = false;
                PSPAYTOTAL20.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE5"].ToString() == "")
            {
                REGPAMOUNT5.Visible = false;
                PSPAYBUSEO21.Visible = false;
                PSPAYTOTAL21.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE6"].ToString() == "")
            {
                REGPAMOUNT6.Visible = false;
                PSPAYBUSEO22.Visible = false;
                PSPAYTOTAL22.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE7"].ToString() == "")
            {
                REGPAMOUNT7.Visible = false;
                PSPAYBUSEO23.Visible = false;
                PSPAYTOTAL23.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE8"].ToString() == "")
            {
                REGPAMOUNT8.Visible = false;
                PSPAYBUSEO24.Visible = false;
                PSPAYTOTAL24.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE9"].ToString() == "")
            {
                REGPAMOUNT9.Visible = false;
                PSPAYBUSEO25.Visible = false;
                PSPAYTOTAL25.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE10"].ToString() == "")
            {
                REGPAMOUNT10.Visible = false;
                PSPAYBUSEO26.Visible = false;
                PSPAYTOTAL26.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE11"].ToString() == "")
            {
                REGPAMOUNT11.Visible = false;
                PSPAYBUSEO27.Visible = false;
                PSPAYTOTAL27.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE12"].ToString() == "")
            {
                REGPAMOUNT12.Visible = false;
                PSPAYBUSEO28.Visible = false;
                PSPAYTOTAL28.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE13"].ToString() == "")
            {
                REGPAMOUNT13.Visible = false;
                PSPAYBUSEO29.Visible = false;
                PSPAYTOTAL29.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE14"].ToString() == "")
            {
                REGPAMOUNT14.Visible = false;
                PSPAYBUSEO30.Visible = false;
                PSPAYTOTAL30.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE15"].ToString() == "")
            {
                REGPAMOUNT15.Visible = false;
                PSPAYBUSEO31.Visible = false;
                PSPAYTOTAL31.Visible = false;
            }
            if (_dt.Rows[_iCount]["REGPTITILE16"].ToString() == "")
            {
                REGPAMOUNT16.Visible = false;
                PSPAYBUSEO32.Visible = false;
                PSPAYTOTAL32.Visible = false;
            }
        }

        private void detail_Format(object sender, EventArgs e)
        {   
            // 한 페이지당 5줄까지 출력
            if (((_iRowCount+1) % 5) == 0)
            {
                this.detail.NewPage = NewPage.After;
            }
            else
            {       
                this.detail.NewPage = NewPage.None;
            }

            if (_dt.Rows.Count-1 != _iCount)
            {
                _iCount++;
            }
            _iRowCount++;
            SEQ.Text = _iCount.ToString();
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            COUNTBUSEO.Text = _iRowCount.ToString();

            PSPAYBUSEO1.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT1"].ToString()));
            PSPAYBUSEO2.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT2"].ToString()));
            PSPAYBUSEO3.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT3"].ToString()));
            PSPAYBUSEO4.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT4"].ToString()));
            PSPAYBUSEO5.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT5"].ToString()));
            PSPAYBUSEO6.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT6"].ToString()));
            PSPAYBUSEO7.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT7"].ToString()));
            PSPAYBUSEO8.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT8"].ToString()));
            PSPAYBUSEO9.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT9"].ToString()));
            PSPAYBUSEO10.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT10"].ToString()));

            PSPAYBUSEO11.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT11"].ToString()));
            PSPAYBUSEO12.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT12"].ToString()));
            PSPAYBUSEO13.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT13"].ToString()));
            PSPAYBUSEO14.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT14"].ToString()));
            PSPAYBUSEO15.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT15"].ToString()));
            PSPAYBUSEO16.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT16"].ToString()));

            PSPAYBUSEO17.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT1"].ToString()));
            PSPAYBUSEO18.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT2"].ToString()));
            PSPAYBUSEO19.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT3"].ToString()));
            PSPAYBUSEO20.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT4"].ToString()));
            PSPAYBUSEO21.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT5"].ToString()));
            PSPAYBUSEO22.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT6"].ToString()));
            PSPAYBUSEO23.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT7"].ToString()));
            PSPAYBUSEO24.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT8"].ToString()));
            PSPAYBUSEO25.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT9"].ToString()));
            PSPAYBUSEO26.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT10"].ToString()));

            PSPAYBUSEO27.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT11"].ToString()));
            PSPAYBUSEO28.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT12"].ToString()));
            PSPAYBUSEO29.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT13"].ToString()));
            PSPAYBUSEO30.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT14"].ToString()));
            PSPAYBUSEO31.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT15"].ToString()));
            PSPAYBUSEO32.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT16"].ToString()));

            REJPTOTALBUSEO.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPTOTAL"].ToString()));
            REGPTOTALBUSEO.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPTOTAL"].ToString()));
            RECHAINBUSEO.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["RECHAIN"].ToString()));

            if (_iCount != _dt.Rows.Count-1)
            {
                this.groupFooter2.NewPage = NewPage.After;
            }
            else
            {
                if (((_iRowCount+1) % 5) == 0)
                {
                    this.groupFooter2.NewPage = NewPage.After;
                }
                else
                {
                    this.groupFooter2.NewPage = NewPage.None;
                }
            }
            
            _iRowCount=0;
            _iBuseoCount++;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            COUNTTOTAL.Text = _dt.Rows.Count.ToString();
            PSPAYTOTAL1.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT1"].ToString()));
            PSPAYTOTAL2.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT2"].ToString()));
            PSPAYTOTAL3.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT3"].ToString()));
            PSPAYTOTAL4.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT4"].ToString()));
            PSPAYTOTAL5.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT5"].ToString()));
            PSPAYTOTAL6.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT6"].ToString()));
            PSPAYTOTAL7.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT7"].ToString()));
            PSPAYTOTAL8.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT8"].ToString()));
            PSPAYTOTAL9.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT9"].ToString()));
            PSPAYTOTAL10.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT10"].ToString()));

            PSPAYTOTAL11.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT11"].ToString()));
            PSPAYTOTAL12.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT12"].ToString()));
            PSPAYTOTAL13.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT13"].ToString()));
            PSPAYTOTAL14.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT14"].ToString()));
            PSPAYTOTAL15.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT15"].ToString()));
            PSPAYTOTAL16.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPAMOUNT16"].ToString()));

            PSPAYTOTAL17.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT1"].ToString()));
            PSPAYTOTAL18.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT2"].ToString()));
            PSPAYTOTAL19.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT3"].ToString()));
            PSPAYTOTAL20.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT4"].ToString()));
            PSPAYTOTAL21.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT5"].ToString()));
            PSPAYTOTAL22.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT6"].ToString()));
            PSPAYTOTAL23.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT7"].ToString()));
            PSPAYTOTAL24.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT8"].ToString()));
            PSPAYTOTAL25.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT9"].ToString()));
            PSPAYTOTAL26.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT10"].ToString()));

            PSPAYTOTAL27.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT11"].ToString()));
            PSPAYTOTAL28.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT12"].ToString()));
            PSPAYTOTAL29.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT13"].ToString()));
            PSPAYTOTAL30.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT14"].ToString()));
            PSPAYTOTAL31.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT15"].ToString()));
            PSPAYTOTAL32.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPAMOUNT16"].ToString()));

            REJPTOTALSUM.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REJPTOTAL"].ToString()));
            REGPTOTALSUM.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["REGPTOTAL"].ToString()));
            RECHAINSUM.Text = string.Format("{0:#,##0}", double.Parse(_dtSum.Rows[_iBuseoCount]["RECHAIN"].ToString()));
        }
    }
}
