using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.HR00
{
    /// <summary>
    /// Summary description for TYHRGT009R.
    /// </summary>
    public partial class TYHRGT009R : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();

        int iRowsCount = 0;
        string _sSTEDDATE = string.Empty;

        double _dGIJOTIME1_HAP = 0;
        double _dGIHTTIME1_HAP = 0;
        double _dGIOTTIME1_HAP = 0;
        double _dGINTTIME1_HAP = 0;
        double _dGIHUTIME1_HAP = 0;
        double _dGINUTIME1_HAP = 0;
        double _dGIGJTIME1_HAP = 0;
        double _dGIINTIME1_HAP = 0;
        double _dGIAMT1_HAP = 0;
        double _dGJAMT1_HAP = 0;

        double _dGIJOTIME2_HAP = 0;
        double _dGIHTTIME2_HAP = 0;
        double _dGIOTTIME2_HAP = 0;
        double _dGINTTIME2_HAP = 0;
        double _dGIHUTIME2_HAP = 0;
        double _dGINUTIME2_HAP = 0;
        double _dGIGJTIME2_HAP = 0;
        double _dGIINTIME2_HAP = 0;
        double _dGIAMT2_HAP = 0;
        double _dGJAMT2_HAP = 0;

        double _dGIJOTIME3_HAP = 0;
        double _dGIHTTIME3_HAP = 0;
        double _dGIOTTIME3_HAP = 0;
        double _dGINTTIME3_HAP = 0;
        double _dGIHUTIME3_HAP = 0;
        double _dGINUTIME3_HAP = 0;
        double _dGIGJTIME3_HAP = 0;
        double _dGIINTIME3_HAP = 0;
        double _dGIAMT3_HAP = 0;
        double _dGJAMT3_HAP = 0;

        double _dGIJOTIME4_HAP = 0;
        double _dGIHTTIME4_HAP = 0;
        double _dGIOTTIME4_HAP = 0;
        double _dGINTTIME4_HAP = 0;
        double _dGIHUTIME4_HAP = 0;
        double _dGINUTIME4_HAP = 0;
        double _dGIGJTIME4_HAP = 0;
        double _dGIINTIME4_HAP = 0;
        double _dGIAMT4_HAP = 0;
        double _dGJAMT4_HAP = 0;

        public TYHRGT009R(string STEDDATE)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _sSTEDDATE = STEDDATE;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            this.GISTEDDATE.Text = _sSTEDDATE;
        }

        private void detail_Format(object sender, EventArgs e)
        {

            this.GIJOTIME.Text = "";
            this.GIHTTIME.Text = "";
            this.GIOTTIME.Text = "";
            this.GINTTIME.Text = "";
            this.GIHUTIME.Text = "";
            this.GINUTIME.Text = "";
            this.GIGJTIME.Text = "";
            this.GIINTIME.Text = "";

            if (double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString()) > 0) this.GIJOTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GIJOTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString()) > 0) this.GIHTTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GIHTTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString()) > 0) this.GIOTTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GIOTTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString()) > 0) this.GINTTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GINTTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString()) > 0) this.GIHUTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GIHUTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString()) > 0) this.GINUTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GINUTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString()) > 0) this.GIGJTIME.Text = string.Format("{0:#,##0.0}", _dt.Rows[iRowsCount]["GIGJTIME"]);
            if (double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString()) > 0) this.GIINTIME.Text = string.Format("{0:#,##0.00}", _dt.Rows[iRowsCount]["GIINTIME"]);
            this.PMORDPAY.Text = string.Format("{0:#,###}", _dt.Rows[iRowsCount]["PMORDPAY"]);
            this.GIAMT.Text = string.Format("{0:#,###}", _dt.Rows[iRowsCount]["GIAMT"]);
            this.GJAMT.Text = string.Format("{0:#,###}", _dt.Rows[iRowsCount]["GJAMT"]);

            if (iRowsCount > 0)
            {
                //-----------개인별 합계출력---------------
                if (_dt.Rows[iRowsCount - 1]["GISABUN"].ToString() != _dt.Rows[iRowsCount]["GISABUN"].ToString())
                {

                    this.GIJOTIMETOTAL1.Text = "";
                    this.GIHTTIMETOTAL1.Text = "";
                    this.GIOTTIMETOTAL1.Text = "";
                    this.GINTTIMETOTAL1.Text = "";
                    this.GIHUTIMETOTAL1.Text = "";
                    this.GINUTIMETOTAL1.Text = "";
                    this.GIGJTIMETOTAL1.Text = "";
                    this.GIINTIMETOTAL1.Text = "";


                    if (_dGIJOTIME1_HAP > 0) this.GIJOTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIJOTIME1_HAP);
                    if (_dGIHTTIME1_HAP > 0) this.GIHTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIHTTIME1_HAP);
                    if (_dGIOTTIME1_HAP > 0) this.GIOTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIOTTIME1_HAP);
                    if (_dGINTTIME1_HAP > 0) this.GINTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGINTTIME1_HAP);
                    if (_dGIHUTIME1_HAP > 0) this.GIHUTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIHUTIME1_HAP);
                    if (_dGINUTIME1_HAP > 0) this.GINUTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGINUTIME1_HAP);
                    if (_dGIGJTIME1_HAP > 0) this.GIGJTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIGJTIME1_HAP);
                    if (_dGIINTIME1_HAP > 0) this.GIINTIMETOTAL1.Text = string.Format("{0:#,##0.00}", _dGIINTIME1_HAP);


                    this.GIAMTTOTAL1.Text = string.Format("{0:#,###}", _dGIAMT1_HAP);
                    this.GJAMTTOTAL1.Text = string.Format("{0:#,###}", _dGJAMT1_HAP);

                    _dGIJOTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT1_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
                else
                {
                    _dGIJOTIME1_HAP = _dGIJOTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME1_HAP = _dGIHTTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME1_HAP = _dGIOTTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME1_HAP = _dGINTTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME1_HAP = _dGIHUTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME1_HAP = _dGINUTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME1_HAP = _dGIGJTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME1_HAP = _dGIINTIME1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT1_HAP = _dGIAMT1_HAP + double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT1_HAP = _dGJAMT1_HAP + double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
                //-----------부서별 합계출력---------------
                if (_dt.Rows[iRowsCount - 1]["KBBSTEAM"].ToString() != _dt.Rows[iRowsCount]["KBBSTEAM"].ToString())
                {
                    this.GIJOTIMETOTAL2.Text = "";
                    this.GIHTTIMETOTAL2.Text = "";
                    this.GIOTTIMETOTAL2.Text = "";
                    this.GINTTIMETOTAL2.Text = "";
                    this.GIHUTIMETOTAL2.Text = "";
                    this.GINUTIMETOTAL2.Text = "";
                    this.GIGJTIMETOTAL2.Text = "";
                    this.GIINTIMETOTAL2.Text = "";

                    if (_dGIJOTIME2_HAP > 0) this.GIJOTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIJOTIME2_HAP);
                    if (_dGIHTTIME2_HAP > 0) this.GIHTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIHTTIME2_HAP);
                    if (_dGIOTTIME2_HAP > 0) this.GIOTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIOTTIME2_HAP);
                    if (_dGINTTIME2_HAP > 0) this.GINTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGINTTIME2_HAP);
                    if (_dGIHUTIME2_HAP > 0) this.GIHUTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIHUTIME2_HAP);
                    if (_dGINUTIME2_HAP > 0) this.GINUTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGINUTIME2_HAP);
                    if (_dGIGJTIME2_HAP > 0) this.GIGJTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIGJTIME2_HAP);
                    if (_dGIINTIME2_HAP > 0) this.GIINTIMETOTAL2.Text = string.Format("{0:#,##0.00}", _dGIINTIME2_HAP);
                    this.GIAMTTOTAL2.Text = string.Format("{0:#,###}", _dGIAMT2_HAP);
                    this.GJAMTTOTAL2.Text = string.Format("{0:#,###}", _dGJAMT2_HAP);

                    _dGIJOTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT2_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
                else
                {
                    _dGIJOTIME2_HAP = _dGIJOTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME2_HAP = _dGIHTTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME2_HAP = _dGIOTTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME2_HAP = _dGINTTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME2_HAP = _dGIHUTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME2_HAP = _dGINUTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME2_HAP = _dGIGJTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME2_HAP = _dGIINTIME2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT2_HAP = _dGIAMT2_HAP + double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT2_HAP = _dGJAMT2_HAP + double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
                //-----------소속별 합계출력---------------
                if (_dt.Rows[iRowsCount - 1]["KBSOSOK"].ToString() != _dt.Rows[iRowsCount]["KBSOSOK"].ToString())
                {
                    this.GIJOTIMETOTAL3.Text = "";
                    this.GIHTTIMETOTAL3.Text = "";
                    this.GIOTTIMETOTAL3.Text = "";
                    this.GINTTIMETOTAL3.Text = "";
                    this.GIHUTIMETOTAL3.Text = "";
                    this.GINUTIMETOTAL3.Text = "";
                    this.GIGJTIMETOTAL3.Text = "";
                    this.GIINTIMETOTAL3.Text = "";

                    if (_dGIJOTIME3_HAP > 0) this.GIJOTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIJOTIME3_HAP);
                    if (_dGIHTTIME3_HAP > 0) this.GIHTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIHTTIME3_HAP);
                    if (_dGIOTTIME3_HAP > 0) this.GIOTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIOTTIME3_HAP);
                    if (_dGINTTIME3_HAP > 0) this.GINTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGINTTIME3_HAP);
                    if (_dGIHUTIME3_HAP > 0) this.GIHUTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIHUTIME3_HAP);
                    if (_dGINUTIME3_HAP > 0) this.GINUTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGINUTIME3_HAP);
                    if (_dGIGJTIME3_HAP > 0) this.GIGJTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIGJTIME3_HAP);
                    if (_dGIINTIME3_HAP > 0) this.GIINTIMETOTAL3.Text = string.Format("{0:#,##0.00}", _dGIINTIME3_HAP);
                    this.GIAMTTOTAL3.Text = string.Format("{0:#,###}", _dGIAMT3_HAP);
                    this.GJAMTTOTAL3.Text = string.Format("{0:#,###}", _dGJAMT3_HAP);

                    _dGIJOTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT3_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
                else
                {
                    _dGIJOTIME3_HAP = _dGIJOTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                    _dGIHTTIME3_HAP = _dGIHTTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                    _dGIOTTIME3_HAP = _dGIOTTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                    _dGINTTIME3_HAP = _dGINTTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                    _dGIHUTIME3_HAP = _dGIHUTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                    _dGINUTIME3_HAP = _dGINUTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                    _dGIGJTIME3_HAP = _dGIGJTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                    _dGIINTIME3_HAP = _dGIINTIME3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                    _dGIAMT3_HAP = _dGIAMT3_HAP + double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                    _dGJAMT3_HAP = _dGJAMT3_HAP + double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
                }
            }
            else
            {
                _dGIJOTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                _dGIHTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                _dGIOTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                _dGINTTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                _dGIHUTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                _dGINUTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                _dGIGJTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                _dGIINTIME1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                _dGIAMT1_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                _dGJAMT1_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());

                _dGIJOTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                _dGIHTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                _dGIOTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                _dGINTTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                _dGIHUTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                _dGINUTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                _dGIGJTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                _dGIINTIME2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                _dGIAMT2_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                _dGJAMT2_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());

                _dGIJOTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
                _dGIHTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
                _dGIOTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
                _dGINTTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
                _dGIHUTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
                _dGINUTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
                _dGIGJTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
                _dGIINTIME3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
                _dGIAMT3_HAP = double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
                _dGJAMT3_HAP = double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());
            }
            _dGIJOTIME4_HAP = _dGIJOTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIJOTIME"].ToString());
            _dGIHTTIME4_HAP = _dGIHTTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHTTIME"].ToString());
            _dGIOTTIME4_HAP = _dGIOTTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIOTTIME"].ToString());
            _dGINTTIME4_HAP = _dGINTTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GINTTIME"].ToString());
            _dGIHUTIME4_HAP = _dGIHUTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIHUTIME"].ToString());
            _dGINUTIME4_HAP = _dGINUTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GINUTIME"].ToString());
            _dGIGJTIME4_HAP = _dGIGJTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIGJTIME"].ToString());
            _dGIINTIME4_HAP = _dGIINTIME4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIINTIME"].ToString());
            _dGIAMT4_HAP = _dGIAMT4_HAP + double.Parse(_dt.Rows[iRowsCount]["GIAMT"].ToString());
            _dGJAMT4_HAP = _dGJAMT4_HAP + double.Parse(_dt.Rows[iRowsCount]["GJAMT"].ToString());

            this.GIJOTIMETOTAL1.Text = "";
            this.GIHTTIMETOTAL1.Text = "";
            this.GIOTTIMETOTAL1.Text = "";
            this.GINTTIMETOTAL1.Text = "";
            this.GIHUTIMETOTAL1.Text = "";
            this.GINUTIMETOTAL1.Text = "";
            this.GIGJTIMETOTAL1.Text = "";
            this.GIINTIMETOTAL1.Text = "";

            if (_dGIJOTIME1_HAP > 0) this.GIJOTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIJOTIME1_HAP);
            if (_dGIHTTIME1_HAP > 0) this.GIHTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIHTTIME1_HAP);
            if (_dGIOTTIME1_HAP > 0) this.GIOTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIOTTIME1_HAP);
            if (_dGINTTIME1_HAP > 0) this.GINTTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGINTTIME1_HAP);
            if (_dGIHUTIME1_HAP > 0) this.GIHUTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIHUTIME1_HAP);
            if (_dGINUTIME1_HAP > 0) this.GINUTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGINUTIME1_HAP);
            if (_dGIGJTIME1_HAP > 0) this.GIGJTIMETOTAL1.Text = string.Format("{0:#,##0.0}", _dGIGJTIME1_HAP);
            if (_dGIINTIME1_HAP > 0) this.GIINTIMETOTAL1.Text = string.Format("{0:#,##0.00}", _dGIINTIME1_HAP);
            this.GIAMTTOTAL1.Text = string.Format("{0:#,###}", _dGIAMT1_HAP);
            this.GJAMTTOTAL1.Text = string.Format("{0:#,###}", _dGJAMT1_HAP);

            this.GIJOTIMETOTAL2.Text = "";
            this.GIHTTIMETOTAL2.Text = "";
            this.GIOTTIMETOTAL2.Text = "";
            this.GINTTIMETOTAL2.Text = "";
            this.GIHUTIMETOTAL2.Text = "";
            this.GINUTIMETOTAL2.Text = "";
            this.GIGJTIMETOTAL2.Text = "";
            this.GIINTIMETOTAL2.Text = "";

            if (_dGIJOTIME2_HAP > 0) this.GIJOTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIJOTIME2_HAP);
            if (_dGIHTTIME2_HAP > 0) this.GIHTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIHTTIME2_HAP);
            if (_dGIOTTIME2_HAP > 0) this.GIOTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIOTTIME2_HAP);
            if (_dGINTTIME2_HAP > 0) this.GINTTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGINTTIME2_HAP);
            if (_dGIHUTIME2_HAP > 0) this.GIHUTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIHUTIME2_HAP);
            if (_dGINUTIME2_HAP > 0) this.GINUTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGINUTIME2_HAP);
            if (_dGIGJTIME2_HAP > 0) this.GIGJTIMETOTAL2.Text = string.Format("{0:#,##0.0}", _dGIGJTIME2_HAP);
            if (_dGIINTIME2_HAP > 0) this.GIINTIMETOTAL2.Text = string.Format("{0:#,##0.00}", _dGIINTIME2_HAP);
            this.GIAMTTOTAL2.Text = string.Format("{0:#,###}", _dGIAMT2_HAP);
            this.GJAMTTOTAL2.Text = string.Format("{0:#,###}", _dGJAMT2_HAP);

            this.GIJOTIMETOTAL3.Text = "";
            this.GIHTTIMETOTAL3.Text = "";
            this.GIOTTIMETOTAL3.Text = "";
            this.GINTTIMETOTAL3.Text = "";
            this.GIHUTIMETOTAL3.Text = "";
            this.GINUTIMETOTAL3.Text = "";
            this.GIGJTIMETOTAL3.Text = "";
            this.GIINTIMETOTAL3.Text = "";

            if (_dGIJOTIME3_HAP > 0) this.GIJOTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIJOTIME3_HAP);
            if (_dGIHTTIME3_HAP > 0) this.GIHTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIHTTIME3_HAP);
            if (_dGIOTTIME3_HAP > 0) this.GIOTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIOTTIME3_HAP);
            if (_dGINTTIME3_HAP > 0) this.GINTTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGINTTIME3_HAP);
            if (_dGIHUTIME3_HAP > 0) this.GIHUTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIHUTIME3_HAP);
            if (_dGINUTIME3_HAP > 0) this.GINUTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGINUTIME3_HAP);
            if (_dGIGJTIME3_HAP > 0) this.GIGJTIMETOTAL3.Text = string.Format("{0:#,##0.0}", _dGIGJTIME3_HAP);
            if (_dGIINTIME3_HAP > 0) this.GIINTIMETOTAL3.Text = string.Format("{0:#,##0.00}", _dGIINTIME3_HAP);
            this.GIAMTTOTAL3.Text = string.Format("{0:#,###}", _dGIAMT3_HAP);
            this.GJAMTTOTAL3.Text = string.Format("{0:#,###}", _dGJAMT3_HAP);

            if (_dGIJOTIME4_HAP > 0) this.GIJOTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGIJOTIME4_HAP);
            if (_dGIHTTIME4_HAP > 0) this.GIHTTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGIHTTIME4_HAP);
            if (_dGIOTTIME4_HAP > 0) this.GIOTTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGIOTTIME4_HAP);
            if (_dGINTTIME4_HAP > 0) this.GINTTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGINTTIME4_HAP);
            if (_dGIHUTIME4_HAP > 0) this.GIHUTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGIHUTIME4_HAP);
            if (_dGINUTIME4_HAP > 0) this.GINUTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGINUTIME4_HAP);
            if (_dGIGJTIME4_HAP > 0) this.GIGJTIMETOTAL4.Text = string.Format("{0:#,##0.0}", _dGIGJTIME4_HAP);
            if (_dGIINTIME4_HAP > 0) this.GIINTIMETOTAL4.Text = string.Format("{0:#,##0.00}", _dGIINTIME4_HAP);
            this.GIAMTTOTAL4.Text = string.Format("{0:#,###}", _dGIAMT4_HAP);
            this.GJAMTTOTAL4.Text = string.Format("{0:#,###}", _dGJAMT4_HAP);

            iRowsCount++;
        }
    }
}
