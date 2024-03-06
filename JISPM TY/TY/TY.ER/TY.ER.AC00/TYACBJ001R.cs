using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.SectionReportModel;

using System.Data;

namespace TY.ER.AC00
{
    /// <summary>
    /// Summary description for TYACBJ001R.
    /// </summary>
    public partial class TYACBJ001R : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable dt = new DataTable();

        private int _RowCount = 0;
        private int _fiCount = 0;
        private int _fiseq = 0;

        private double _fdTMB2AMDR = 0;
        private double _fdTMB2AMCR = 0;
        private double _fdTOTTMB2AMDR = 0;
        private double _fdTOTTMB2AMCR = 0;

        private double _fiNowPage = 0;


        public TYACBJ001R()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this._RowCount++;

            if (this._RowCount == 1)
            {
                this._fiseq = 1;
                this._fiNowPage = 1;
            }

            if (_fiCount >= _RowCount)
            {
                if (this._fiseq % 6 == 0)
                {
                    this.TMB2DPNM.Text = dt.Rows[_RowCount - 1]["TMB2DPNM"].ToString();
                    this.TMB2DPMK.Text = dt.Rows[_RowCount - 1]["TMB2DPMK"].ToString();
                    this.DATE.Text = dt.Rows[_RowCount - 1]["DATE"].ToString();
                    this.TMB2HISAB.Text = dt.Rows[_RowCount - 1]["TMB2HISAB"].ToString();
                    this.TMB2NOSQ.Text = dt.Rows[_RowCount - 1]["TMB2NOSQ"].ToString();
                    this.TMB2IDJP.Text = dt.Rows[_RowCount - 1]["TMB2IDJP"].ToString();
                    this.TMB2CDNM.Text = dt.Rows[_RowCount - 1]["TMB2CDNM"].ToString();
                    this.TMB2VLMI1.Text = dt.Rows[_RowCount - 1]["TMB2VLMI1"].ToString();
                    this.TMB2VLMI2.Text = dt.Rows[_RowCount - 1]["TMB2VLMI2"].ToString();
                    this.TMB2VLMI3.Text = dt.Rows[_RowCount - 1]["TMB2VLMI3"].ToString();
                    this.TMB2VLMI4.Text = dt.Rows[_RowCount - 1]["TMB2VLMI4"].ToString();
                    this.TMB2VLMI5.Text = dt.Rows[_RowCount - 1]["TMB2VLMI5"].ToString();
                    this.TMB2VLMI6.Text = dt.Rows[_RowCount - 1]["TMB2VLMI6"].ToString();
                    this.TMB2ACNM.Text = dt.Rows[_RowCount - 1]["TMB2ACNM"].ToString();
                    this.TMB2RKAC.Text = dt.Rows[_RowCount - 1]["TMB2RKAC"].ToString();
                    this.TMB2AMDR.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString()));
                    this.TMB2AMCR.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString()));
                    this.TMB2NOLN.Text = dt.Rows[_RowCount - 1]["TMB2NOLN"].ToString();
                    this.TMB2BCDNM.Text = dt.Rows[_RowCount - 1]["TMB2BCDNM"].ToString();

                    //this.TMNOWDRHAP.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMNOWDRHAP"].ToString()));
                    //this.TMNOWCRHAP.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMNOWCRHAP"].ToString()));
                    //this.TMB2DRHAP.Text  = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2DRHAP"].ToString()));
                    //this.TMB2CRHAP.Text  = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2CRHAP"].ToString()));

                    this.NOWPAGE.Text = Convert.ToString(this._fiNowPage); // 현재페이지
                    this.TOTALPAGE.Text = dt.Rows[_RowCount - 1]["TOTALPAGE"].ToString();

                    _fdTMB2AMDR = _fdTMB2AMDR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString());
                    _fdTMB2AMCR = _fdTMB2AMCR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString());

                    this.TMNOWDRHAP.Text = string.Format("{0:#,###}", _fdTMB2AMDR);
                    this.TMNOWCRHAP.Text = string.Format("{0:#,###}", _fdTMB2AMCR);

                    _fdTMB2AMDR = 0;
                    _fdTMB2AMCR = 0;

                    if (_fiCount != _RowCount)
                    {
                        // 새로운 페이지에 레코드를 인쇄 후에 페이지를 나누어라.
                        this.detail.NewPage = NewPage.After;
                        this._fiNowPage++;
                    }
                }
                else
                {
                    // 현재 페이지에 레코드를 인쇄해라.
                    this.detail.NewPage = NewPage.None;

                    this.TMB2DPNM.Text = dt.Rows[_RowCount - 1]["TMB2DPNM"].ToString();
                    this.TMB2DPMK.Text = dt.Rows[_RowCount - 1]["TMB2DPMK"].ToString();
                    this.DATE.Text = dt.Rows[_RowCount - 1]["DATE"].ToString();
                    this.TMB2HISAB.Text = dt.Rows[_RowCount - 1]["TMB2HISAB"].ToString();
                    this.TMB2NOSQ.Text = dt.Rows[_RowCount - 1]["TMB2NOSQ"].ToString();
                    this.TMB2IDJP.Text = dt.Rows[_RowCount - 1]["TMB2IDJP"].ToString();
                    this.TMB2CDNM.Text = dt.Rows[_RowCount - 1]["TMB2CDNM"].ToString();
                    this.TMB2VLMI1.Text = dt.Rows[_RowCount - 1]["TMB2VLMI1"].ToString();
                    this.TMB2VLMI2.Text = dt.Rows[_RowCount - 1]["TMB2VLMI2"].ToString();
                    this.TMB2VLMI3.Text = dt.Rows[_RowCount - 1]["TMB2VLMI3"].ToString();
                    this.TMB2VLMI4.Text = dt.Rows[_RowCount - 1]["TMB2VLMI4"].ToString();
                    this.TMB2VLMI5.Text = dt.Rows[_RowCount - 1]["TMB2VLMI5"].ToString();
                    this.TMB2VLMI6.Text = dt.Rows[_RowCount - 1]["TMB2VLMI6"].ToString();
                    this.TMB2ACNM.Text = dt.Rows[_RowCount - 1]["TMB2ACNM"].ToString();
                    this.TMB2RKAC.Text = dt.Rows[_RowCount - 1]["TMB2RKAC"].ToString();
                    this.TMB2AMDR.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString()));
                    this.TMB2AMCR.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString()));
                    this.TMB2NOLN.Text = dt.Rows[_RowCount - 1]["TMB2NOLN"].ToString();
                    this.TMB2BCDNM.Text = dt.Rows[_RowCount - 1]["TMB2BCDNM"].ToString();
                    //this.TMNOWDRHAP.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMNOWDRHAP"].ToString()));
                    //this.TMNOWCRHAP.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[_RowCount - 1]["TMNOWCRHAP"].ToString()));
                    this.TMB2DRHAP.Text = "";
                    this.TMB2CRHAP.Text = "";
                    this.NOWPAGE.Text = Convert.ToString(this._fiNowPage); // 현재페이지
                    //this.NOWPAGE.Text    = dt.Rows[_RowCount - 1]["NOWPAGE"].ToString();
                    this.TOTALPAGE.Text = dt.Rows[_RowCount - 1]["TOTALPAGE"].ToString();

                    _fdTMB2AMDR = _fdTMB2AMDR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString());
                    _fdTMB2AMCR = _fdTMB2AMCR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString());
                }

                if (_fiCount > _RowCount)
                {
                    if (int.Parse(dt.Rows[_RowCount - 1]["TMB2NOSQ"].ToString()) != int.Parse(dt.Rows[_RowCount]["TMB2NOSQ"].ToString()))
                    {
                        _fdTOTTMB2AMDR = _fdTOTTMB2AMDR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString());
                        _fdTOTTMB2AMCR = _fdTOTTMB2AMCR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString());

                        this.TMB2DRHAP.Text = string.Format("{0:#,###}", _fdTOTTMB2AMDR);
                        this.TMB2CRHAP.Text = string.Format("{0:#,###}", _fdTOTTMB2AMCR);

                        _fdTOTTMB2AMDR = 0;
                        _fdTOTTMB2AMCR = 0;

                        this._fiseq = 1;

                        _fiNowPage = 1;
                    }
                    else
                    {
                        _fdTOTTMB2AMDR = _fdTOTTMB2AMDR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString());
                        _fdTOTTMB2AMCR = _fdTOTTMB2AMCR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString());

                        this._fiseq++;
                    }
                }
            }

            if (_fiCount == _RowCount)
            {
                _fdTOTTMB2AMDR = _fdTOTTMB2AMDR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMDR"].ToString());
                _fdTOTTMB2AMCR = _fdTOTTMB2AMCR + double.Parse(dt.Rows[_RowCount - 1]["TMB2AMCR"].ToString());

                this.TMB2DRHAP.Text = string.Format("{0:#,###}", _fdTOTTMB2AMDR);
                this.TMB2CRHAP.Text = string.Format("{0:#,###}", _fdTOTTMB2AMCR);
            }
        }

        private void TYACBJ001R_DataInitialize(object sender, EventArgs e)
        {
            dt = (DataTable)this.DataSource;

            if (dt != null)
            {
                _fiCount = dt.Rows.Count;
            }
        }
    }
}