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
    /// Summary description for TYACTP009R2.
    /// </summary>
    public partial class TYACTP009R2 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        string _sSAUPNUM = string.Empty;
        public TYACTP009R2(string sSAUPNUM)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _sSAUPNUM = sSAUPNUM;
        }

        private void reportHeader1_Format(object sender, EventArgs e)
        {
            _dt = this.DataSource as DataTable;

            ASMSAUPNO2.Text = _sSAUPNUM;

            WPAYGUBN1.Text = _dt.Rows[25]["WPAYGUBN"].ToString();
            WPEOPLE1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WPEOPLE"]);
            WPAYAMOUNT1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WPAYAMOUNT"]);
            WCHINCTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WCHINCTAX"]);
            WCHRURTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WCHRURTAX"]);
            WCHADDTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WCHADDTAX"]);
            WDNINCTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WDNINCTAX"]);
            WSNINCTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WSNINCTAX"]);
            WSNRURTAX1.Text = string.Format("{0:#,###}", _dt.Rows[25]["WSNRURTAX"]);

            WPAYGUBN2.Text = _dt.Rows[26]["WPAYGUBN"].ToString();
            WPEOPLE2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WPEOPLE"]);
            WPAYAMOUNT2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WPAYAMOUNT"]);
            WCHINCTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WCHINCTAX"]);
            WCHRURTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WCHRURTAX"]);
            WCHADDTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WCHADDTAX"]);
            WDNINCTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WDNINCTAX"]);
            WSNINCTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WSNINCTAX"]);
            WSNRURTAX2.Text = string.Format("{0:#,###}", _dt.Rows[26]["WSNRURTAX"]);

            WPAYGUBN3.Text = _dt.Rows[27]["WPAYGUBN"].ToString();
            WPEOPLE3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WPEOPLE"]);
            WPAYAMOUNT3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WPAYAMOUNT"]);
            WCHINCTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WCHINCTAX"]);
            WCHRURTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WCHRURTAX"]);
            WCHADDTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WCHADDTAX"]);
            WDNINCTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WDNINCTAX"]);
            WSNINCTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WSNINCTAX"]);
            WSNRURTAX3.Text = string.Format("{0:#,###}", _dt.Rows[27]["WSNRURTAX"]);

            WPAYGUBN4.Text = _dt.Rows[28]["WPAYGUBN"].ToString();
            WPEOPLE4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WPEOPLE"]);
            WPAYAMOUNT4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WPAYAMOUNT"]);
            WCHINCTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WCHINCTAX"]);
            WCHRURTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WCHRURTAX"]);
            WCHADDTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WCHADDTAX"]);
            WDNINCTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WDNINCTAX"]);
            WSNINCTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WSNINCTAX"]);
            WSNRURTAX4.Text = string.Format("{0:#,###}", _dt.Rows[28]["WSNRURTAX"]);

            WPAYGUBN5.Text = _dt.Rows[29]["WPAYGUBN"].ToString();
            WPEOPLE5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WPEOPLE"]);
            WPAYAMOUNT5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WPAYAMOUNT"]);
            WCHINCTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WCHINCTAX"]);
            WCHRURTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WCHRURTAX"]);
            WCHADDTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WCHADDTAX"]);
            WDNINCTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WDNINCTAX"]);
            WSNINCTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WSNINCTAX"]);
            WSNRURTAX5.Text = string.Format("{0:#,###}", _dt.Rows[29]["WSNRURTAX"]);

            WPAYGUBN6.Text = _dt.Rows[30]["WPAYGUBN"].ToString();
            WPEOPLE6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WPEOPLE"]);
            WPAYAMOUNT6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WPAYAMOUNT"]);
            WCHINCTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WCHINCTAX"]);
            WCHRURTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WCHRURTAX"]);
            WCHADDTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WCHADDTAX"]);
            WDNINCTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WDNINCTAX"]);
            WSNINCTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WSNINCTAX"]);
            WSNRURTAX6.Text = string.Format("{0:#,###}", _dt.Rows[30]["WSNRURTAX"]);

            WPAYGUBN7.Text = _dt.Rows[31]["WPAYGUBN"].ToString();
            WPEOPLE7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WPEOPLE"]);
            WPAYAMOUNT7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WPAYAMOUNT"]);
            WCHINCTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WCHINCTAX"]);
            WCHRURTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WCHRURTAX"]);
            WCHADDTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WCHADDTAX"]);
            WDNINCTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WDNINCTAX"]);
            WSNINCTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WSNINCTAX"]);
            WSNRURTAX7.Text = string.Format("{0:#,###}", _dt.Rows[31]["WSNRURTAX"]);

            WPAYGUBN8.Text = _dt.Rows[32]["WPAYGUBN"].ToString();
            WPEOPLE8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WPEOPLE"]);
            WPAYAMOUNT8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WPAYAMOUNT"]);
            WCHINCTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WCHINCTAX"]);
            WCHRURTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WCHRURTAX"]);
            WCHADDTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WCHADDTAX"]);
            WDNINCTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WDNINCTAX"]);
            WSNINCTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WSNINCTAX"]);
            WSNRURTAX8.Text = string.Format("{0:#,###}", _dt.Rows[32]["WSNRURTAX"]);

            WPAYGUBN9.Text = _dt.Rows[33]["WPAYGUBN"].ToString();
            WPEOPLE9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WPEOPLE"]);
            WPAYAMOUNT9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WPAYAMOUNT"]);
            WCHINCTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WCHINCTAX"]);
            WCHRURTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WCHRURTAX"]);
            WCHADDTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WCHADDTAX"]);
            WDNINCTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WDNINCTAX"]);
            WSNINCTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WSNINCTAX"]);
            WSNRURTAX9.Text = string.Format("{0:#,###}", _dt.Rows[33]["WSNRURTAX"]);

            WPAYGUBN10.Text = _dt.Rows[34]["WPAYGUBN"].ToString();
            WPEOPLE10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WPEOPLE"]);
            WPAYAMOUNT10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WPAYAMOUNT"]);
            WCHINCTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WCHINCTAX"]);
            WCHRURTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WCHRURTAX"]);
            WCHADDTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WCHADDTAX"]);
            WDNINCTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WDNINCTAX"]);
            WSNINCTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WSNINCTAX"]);
            WSNRURTAX10.Text = string.Format("{0:#,###}", _dt.Rows[34]["WSNRURTAX"]);

            WPAYGUBN11.Text = _dt.Rows[35]["WPAYGUBN"].ToString();
            WPEOPLE11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WPEOPLE"]);
            WPAYAMOUNT11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WPAYAMOUNT"]);
            WCHINCTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WCHINCTAX"]);
            WCHRURTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WCHRURTAX"]);
            WCHADDTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WCHADDTAX"]);
            WDNINCTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WDNINCTAX"]);
            WSNINCTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WSNINCTAX"]);
            WSNRURTAX11.Text = string.Format("{0:#,###}", _dt.Rows[35]["WSNRURTAX"]);

            WPAYGUBN12.Text = _dt.Rows[36]["WPAYGUBN"].ToString();
            WPEOPLE12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WPEOPLE"]);
            WPAYAMOUNT12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WPAYAMOUNT"]);
            WCHINCTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WCHINCTAX"]);
            WCHRURTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WCHRURTAX"]);
            WCHADDTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WCHADDTAX"]);
            WDNINCTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WDNINCTAX"]);
            WSNINCTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WSNINCTAX"]);
            WSNRURTAX12.Text = string.Format("{0:#,###}", _dt.Rows[36]["WSNRURTAX"]);

            WPAYGUBN13.Text = _dt.Rows[37]["WPAYGUBN"].ToString();
            WPEOPLE13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WPEOPLE"]);
            WPAYAMOUNT13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WPAYAMOUNT"]);
            WCHINCTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WCHINCTAX"]);
            WCHRURTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WCHRURTAX"]);
            WCHADDTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WCHADDTAX"]);
            WDNINCTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WDNINCTAX"]);
            WSNINCTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WSNINCTAX"]);
            WSNRURTAX13.Text = string.Format("{0:#,###}", _dt.Rows[37]["WSNRURTAX"]);

            WPAYGUBN14.Text = _dt.Rows[38]["WPAYGUBN"].ToString();
            WPEOPLE14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WPEOPLE"]);
            WPAYAMOUNT14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WPAYAMOUNT"]);
            WCHINCTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WCHINCTAX"]);
            WCHRURTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WCHRURTAX"]);
            WCHADDTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WCHADDTAX"]);
            WDNINCTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WDNINCTAX"]);
            WSNINCTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WSNINCTAX"]);
            WSNRURTAX14.Text = string.Format("{0:#,###}", _dt.Rows[38]["WSNRURTAX"]);

            WPAYGUBN15.Text = _dt.Rows[39]["WPAYGUBN"].ToString();
            WPEOPLE15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WPEOPLE"]);
            WPAYAMOUNT15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WPAYAMOUNT"]);
            WCHINCTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WCHINCTAX"]);
            WCHRURTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WCHRURTAX"]);
            WCHADDTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WCHADDTAX"]);
            WDNINCTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WDNINCTAX"]);
            WSNINCTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WSNINCTAX"]);
            WSNRURTAX15.Text = string.Format("{0:#,###}", _dt.Rows[39]["WSNRURTAX"]);

            WPAYGUBN16.Text = _dt.Rows[40]["WPAYGUBN"].ToString();
            WPEOPLE16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WPEOPLE"]);
            WPAYAMOUNT16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WPAYAMOUNT"]);
            WCHINCTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WCHINCTAX"]);
            WCHRURTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WCHRURTAX"]);
            WCHADDTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WCHADDTAX"]);
            WDNINCTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WDNINCTAX"]);
            WSNINCTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WSNINCTAX"]);
            WSNRURTAX16.Text = string.Format("{0:#,###}", _dt.Rows[40]["WSNRURTAX"]);

            WPAYGUBN17.Text = _dt.Rows[41]["WPAYGUBN"].ToString();
            WPEOPLE17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WPEOPLE"]);
            WPAYAMOUNT17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WPAYAMOUNT"]);
            WCHINCTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WCHINCTAX"]);
            WCHRURTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WCHRURTAX"]);
            WCHADDTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WCHADDTAX"]);
            WDNINCTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WDNINCTAX"]);
            WSNINCTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WSNINCTAX"]);
            WSNRURTAX17.Text = string.Format("{0:#,###}", _dt.Rows[41]["WSNRURTAX"]);

            WPAYGUBN18.Text = _dt.Rows[42]["WPAYGUBN"].ToString();
            WPEOPLE18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WPEOPLE"]);
            WPAYAMOUNT18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WPAYAMOUNT"]);
            WCHINCTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WCHINCTAX"]);
            WCHRURTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WCHRURTAX"]);
            WCHADDTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WCHADDTAX"]);
            WDNINCTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WDNINCTAX"]);
            WSNINCTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WSNINCTAX"]);
            WSNRURTAX18.Text = string.Format("{0:#,###}", _dt.Rows[42]["WSNRURTAX"]);

            WPAYGUBN19.Text = _dt.Rows[43]["WPAYGUBN"].ToString();
            WPEOPLE19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WPEOPLE"]);
            WPAYAMOUNT19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WPAYAMOUNT"]);
            WCHINCTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WCHINCTAX"]);
            WCHRURTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WCHRURTAX"]);
            WCHADDTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WCHADDTAX"]);
            WDNINCTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WDNINCTAX"]);
            WSNINCTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WSNINCTAX"]);
            WSNRURTAX19.Text = string.Format("{0:#,###}", _dt.Rows[43]["WSNRURTAX"]);

            WPAYGUBN20.Text = _dt.Rows[44]["WPAYGUBN"].ToString();
            WPEOPLE20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WPEOPLE"]);
            WPAYAMOUNT20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WPAYAMOUNT"]);
            WCHINCTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WCHINCTAX"]);
            WCHRURTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WCHRURTAX"]);
            WCHADDTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WCHADDTAX"]);
            WDNINCTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WDNINCTAX"]);
            WSNINCTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WSNINCTAX"]);
            WSNRURTAX20.Text = string.Format("{0:#,###}", _dt.Rows[44]["WSNRURTAX"]);

            WPAYGUBN21.Text = _dt.Rows[45]["WPAYGUBN"].ToString();
            WPEOPLE21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WPEOPLE"]);
            WPAYAMOUNT21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WPAYAMOUNT"]);
            WCHINCTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WCHINCTAX"]);
            WCHRURTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WCHRURTAX"]);
            WCHADDTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WCHADDTAX"]);
            WDNINCTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WDNINCTAX"]);
            WSNINCTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WSNINCTAX"]);
            WSNRURTAX21.Text = string.Format("{0:#,###}", _dt.Rows[45]["WSNRURTAX"]);

            WPAYGUBN22.Text = _dt.Rows[46]["WPAYGUBN"].ToString();
            WPEOPLE22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WPEOPLE"]);
            WPAYAMOUNT22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WPAYAMOUNT"]);
            WCHINCTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WCHINCTAX"]);
            WCHRURTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WCHRURTAX"]);
            WCHADDTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WCHADDTAX"]);
            WDNINCTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WDNINCTAX"]);
            WSNINCTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WSNINCTAX"]);
            WSNRURTAX22.Text = string.Format("{0:#,###}", _dt.Rows[46]["WSNRURTAX"]);

            WPAYGUBN23.Text = _dt.Rows[47]["WPAYGUBN"].ToString();
            WPEOPLE23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WPEOPLE"]);
            WPAYAMOUNT23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WPAYAMOUNT"]);
            WCHINCTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WCHINCTAX"]);
            WCHRURTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WCHRURTAX"]);
            WCHADDTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WCHADDTAX"]);
            WDNINCTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WDNINCTAX"]);
            WSNINCTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WSNINCTAX"]);
            WSNRURTAX23.Text = string.Format("{0:#,###}", _dt.Rows[47]["WSNRURTAX"]);

            WPAYGUBN24.Text = _dt.Rows[48]["WPAYGUBN"].ToString();
            WPEOPLE24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WPEOPLE"]);
            WPAYAMOUNT24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WPAYAMOUNT"]);
            WCHINCTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WCHINCTAX"]);
            WCHRURTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WCHRURTAX"]);
            WCHADDTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WCHADDTAX"]);
            WDNINCTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WDNINCTAX"]);
            WSNINCTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WSNINCTAX"]);
            WSNRURTAX24.Text = string.Format("{0:#,###}", _dt.Rows[48]["WSNRURTAX"]);

            WPAYGUBN25.Text = _dt.Rows[49]["WPAYGUBN"].ToString();
            WPEOPLE25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WPEOPLE"]);
            WPAYAMOUNT25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WPAYAMOUNT"]);
            WCHINCTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WCHINCTAX"]);
            WCHRURTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WCHRURTAX"]);
            WCHADDTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WCHADDTAX"]);
            WDNINCTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WDNINCTAX"]);
            WSNINCTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WSNINCTAX"]);
            WSNRURTAX25.Text = string.Format("{0:#,###}", _dt.Rows[49]["WSNRURTAX"]);

            WPAYGUBN26.Text = _dt.Rows[50]["WPAYGUBN"].ToString();
            WPEOPLE26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WPEOPLE"]);
            WPAYAMOUNT26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WPAYAMOUNT"]);
            WCHINCTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WCHINCTAX"]);
            WCHRURTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WCHRURTAX"]);
            WCHADDTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WCHADDTAX"]);
            WDNINCTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WDNINCTAX"]);
            WSNINCTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WSNINCTAX"]);
            WSNRURTAX26.Text = string.Format("{0:#,###}", _dt.Rows[50]["WSNRURTAX"]);

            WPAYGUBN27.Text = _dt.Rows[51]["WPAYGUBN"].ToString();
            WPEOPLE27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WPEOPLE"]);
            WPAYAMOUNT27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WPAYAMOUNT"]);
            WCHINCTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WCHINCTAX"]);
            WCHRURTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WCHRURTAX"]);
            WCHADDTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WCHADDTAX"]);
            WDNINCTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WDNINCTAX"]);
            WSNINCTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WSNINCTAX"]);
            WSNRURTAX27.Text = string.Format("{0:#,###}", _dt.Rows[51]["WSNRURTAX"]);

            WPAYGUBN28.Text = _dt.Rows[52]["WPAYGUBN"].ToString();
            WPEOPLE28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WPEOPLE"]);
            WPAYAMOUNT28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WPAYAMOUNT"]);
            WCHINCTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WCHINCTAX"]);
            WCHRURTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WCHRURTAX"]);
            WCHADDTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WCHADDTAX"]);
            WDNINCTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WDNINCTAX"]);
            WSNINCTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WSNINCTAX"]);
            WSNRURTAX28.Text = string.Format("{0:#,###}", _dt.Rows[52]["WSNRURTAX"]);

            WPAYGUBN29.Text = _dt.Rows[53]["WPAYGUBN"].ToString();
            WPEOPLE29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WPEOPLE"]);
            WPAYAMOUNT29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WPAYAMOUNT"]);
            WCHINCTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WCHINCTAX"]);
            WCHRURTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WCHRURTAX"]);
            WCHADDTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WCHADDTAX"]);
            WDNINCTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WDNINCTAX"]);
            WSNINCTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WSNINCTAX"]);
            WSNRURTAX29.Text = string.Format("{0:#,###}", _dt.Rows[53]["WSNRURTAX"]);

            WPAYGUBN30.Text = _dt.Rows[54]["WPAYGUBN"].ToString();
            WPEOPLE30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WPEOPLE"]);
            WPAYAMOUNT30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WPAYAMOUNT"]);
            WCHINCTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WCHINCTAX"]);
            WCHRURTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WCHRURTAX"]);
            WCHADDTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WCHADDTAX"]);
            WDNINCTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WDNINCTAX"]);
            WSNINCTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WSNINCTAX"]);
            WSNRURTAX30.Text = string.Format("{0:#,###}", _dt.Rows[54]["WSNRURTAX"]);

            WPAYGUBN31.Text = _dt.Rows[55]["WPAYGUBN"].ToString();
            WPEOPLE31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WPEOPLE"]);
            WPAYAMOUNT31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WPAYAMOUNT"]);
            WCHINCTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WCHINCTAX"]);
            WCHRURTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WCHRURTAX"]);
            WCHADDTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WCHADDTAX"]);
            WDNINCTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WDNINCTAX"]);
            WSNINCTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WSNINCTAX"]);
            WSNRURTAX31.Text = string.Format("{0:#,###}", _dt.Rows[55]["WSNRURTAX"]);

            WPAYGUBN32.Text = _dt.Rows[56]["WPAYGUBN"].ToString();
            WPEOPLE32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WPEOPLE"]);
            WPAYAMOUNT32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WPAYAMOUNT"]);
            WCHINCTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WCHINCTAX"]);
            WCHRURTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WCHRURTAX"]);
            WCHADDTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WCHADDTAX"]);
            WDNINCTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WDNINCTAX"]);
            WSNINCTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WSNINCTAX"]);
            WSNRURTAX32.Text = string.Format("{0:#,###}", _dt.Rows[56]["WSNRURTAX"]);

            WPAYGUBN33.Text = _dt.Rows[57]["WPAYGUBN"].ToString();
            WPEOPLE33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WPEOPLE"]);
            WPAYAMOUNT33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WPAYAMOUNT"]);
            WCHINCTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WCHINCTAX"]);
            WCHRURTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WCHRURTAX"]);
            WCHADDTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WCHADDTAX"]);
            WDNINCTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WDNINCTAX"]);
            WSNINCTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WSNINCTAX"]);
            WSNRURTAX33.Text = string.Format("{0:#,###}", _dt.Rows[57]["WSNRURTAX"]);

            WPAYGUBN34.Text = _dt.Rows[58]["WPAYGUBN"].ToString();
            WPEOPLE34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WPEOPLE"]);
            WPAYAMOUNT34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WPAYAMOUNT"]);
            WCHINCTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WCHINCTAX"]);
            WCHRURTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WCHRURTAX"]);
            WCHADDTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WCHADDTAX"]);
            WDNINCTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WDNINCTAX"]);
            WSNINCTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WSNINCTAX"]);
            WSNRURTAX34.Text = string.Format("{0:#,###}", _dt.Rows[58]["WSNRURTAX"]);

            WPAYGUBN35.Text = _dt.Rows[59]["WPAYGUBN"].ToString();
            WPEOPLE35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WPEOPLE"]);
            WPAYAMOUNT35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WPAYAMOUNT"]);
            WCHINCTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WCHINCTAX"]);
            WCHRURTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WCHRURTAX"]);
            WCHADDTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WCHADDTAX"]);
            WDNINCTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WDNINCTAX"]);
            WSNINCTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WSNINCTAX"]);
            WSNRURTAX35.Text = string.Format("{0:#,###}", _dt.Rows[59]["WSNRURTAX"]);

            WPAYGUBN36.Text = _dt.Rows[60]["WPAYGUBN"].ToString();
            WPEOPLE36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WPEOPLE"]);
            WPAYAMOUNT36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WPAYAMOUNT"]);
            WCHINCTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WCHINCTAX"]);
            WCHRURTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WCHRURTAX"]);
            WCHADDTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WCHADDTAX"]);
            WDNINCTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WDNINCTAX"]);
            WSNINCTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WSNINCTAX"]);
            WSNRURTAX36.Text = string.Format("{0:#,###}", _dt.Rows[60]["WSNRURTAX"]);

            WPAYGUBN37.Text = _dt.Rows[61]["WPAYGUBN"].ToString();
            WPEOPLE37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WPEOPLE"]);
            WPAYAMOUNT37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WPAYAMOUNT"]);
            WCHINCTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WCHINCTAX"]);
            WCHRURTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WCHRURTAX"]);
            WCHADDTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WCHADDTAX"]);
            WDNINCTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WDNINCTAX"]);
            WSNINCTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WSNINCTAX"]);
            WSNRURTAX37.Text = string.Format("{0:#,###}", _dt.Rows[61]["WSNRURTAX"]);

            WPAYGUBN38.Text = _dt.Rows[62]["WPAYGUBN"].ToString();
            WPEOPLE38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WPEOPLE"]);
            WPAYAMOUNT38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WPAYAMOUNT"]);
            WCHINCTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WCHINCTAX"]);
            WCHRURTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WCHRURTAX"]);
            WCHADDTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WCHADDTAX"]);
            WDNINCTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WDNINCTAX"]);
            WSNINCTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WSNINCTAX"]);
            WSNRURTAX38.Text = string.Format("{0:#,###}", _dt.Rows[62]["WSNRURTAX"]);

            WPAYGUBN39.Text = _dt.Rows[63]["WPAYGUBN"].ToString();
            WPEOPLE39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WPEOPLE"]);
            WPAYAMOUNT39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WPAYAMOUNT"]);
            WCHINCTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WCHINCTAX"]);
            WCHRURTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WCHRURTAX"]);
            WCHADDTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WCHADDTAX"]);
            WDNINCTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WDNINCTAX"]);
            WSNINCTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WSNINCTAX"]);
            WSNRURTAX39.Text = string.Format("{0:#,###}", _dt.Rows[63]["WSNRURTAX"]);

            WPAYGUBN40.Text = _dt.Rows[64]["WPAYGUBN"].ToString();
            WPEOPLE40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WPEOPLE"]);
            WPAYAMOUNT40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WPAYAMOUNT"]);
            WCHINCTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WCHINCTAX"]);
            WCHRURTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WCHRURTAX"]);
            WCHADDTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WCHADDTAX"]);
            WDNINCTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WDNINCTAX"]);
            WSNINCTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WSNINCTAX"]);
            WSNRURTAX40.Text = string.Format("{0:#,###}", _dt.Rows[64]["WSNRURTAX"]);

            WPAYGUBN41.Text = _dt.Rows[65]["WPAYGUBN"].ToString();
            WPEOPLE41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WPEOPLE"]);
            WPAYAMOUNT41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WPAYAMOUNT"]);
            WCHINCTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WCHINCTAX"]);
            WCHRURTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WCHRURTAX"]);
            WCHADDTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WCHADDTAX"]);
            WDNINCTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WDNINCTAX"]);
            WSNINCTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WSNINCTAX"]);
            WSNRURTAX41.Text = string.Format("{0:#,###}", _dt.Rows[65]["WSNRURTAX"]);

            WPAYGUBN42.Text = _dt.Rows[66]["WPAYGUBN"].ToString();
            WPEOPLE42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WPEOPLE"]);
            WPAYAMOUNT42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WPAYAMOUNT"]);
            WCHINCTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WCHINCTAX"]);
            WCHRURTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WCHRURTAX"]);
            WCHADDTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WCHADDTAX"]);
            WDNINCTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WDNINCTAX"]);
            WSNINCTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WSNINCTAX"]);
            WSNRURTAX42.Text = string.Format("{0:#,###}", _dt.Rows[66]["WSNRURTAX"]);

            WPAYGUBN43.Text = _dt.Rows[67]["WPAYGUBN"].ToString();
            WPEOPLE43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WPEOPLE"]);
            WPAYAMOUNT43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WPAYAMOUNT"]);
            WCHINCTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WCHINCTAX"]);
            WCHRURTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WCHRURTAX"]);
            WCHADDTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WCHADDTAX"]);
            WDNINCTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WDNINCTAX"]);
            WSNINCTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WSNINCTAX"]);
            WSNRURTAX43.Text = string.Format("{0:#,###}", _dt.Rows[67]["WSNRURTAX"]);

            WPAYGUBN44.Text = _dt.Rows[68]["WPAYGUBN"].ToString();
            WPEOPLE44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WPEOPLE"]);
            WPAYAMOUNT44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WPAYAMOUNT"]);
            WCHINCTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WCHINCTAX"]);
            WCHRURTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WCHRURTAX"]);
            WCHADDTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WCHADDTAX"]);
            WDNINCTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WDNINCTAX"]);
            WSNINCTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WSNINCTAX"]);
            WSNRURTAX44.Text = string.Format("{0:#,###}", _dt.Rows[68]["WSNRURTAX"]);

            WPAYGUBN45.Text = _dt.Rows[69]["WPAYGUBN"].ToString();
            WPEOPLE45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WPEOPLE"]);
            WPAYAMOUNT45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WPAYAMOUNT"]);
            WCHINCTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WCHINCTAX"]);
            WCHRURTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WCHRURTAX"]);
            WCHADDTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WCHADDTAX"]);
            WDNINCTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WDNINCTAX"]);
            WSNINCTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WSNINCTAX"]);
            WSNRURTAX45.Text = string.Format("{0:#,###}", _dt.Rows[69]["WSNRURTAX"]);

            WPAYGUBN46.Text = _dt.Rows[70]["WPAYGUBN"].ToString();
            WPEOPLE46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WPEOPLE"]);
            WPAYAMOUNT46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WPAYAMOUNT"]);
            WCHINCTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WCHINCTAX"]);
            WCHRURTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WCHRURTAX"]);
            WCHADDTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WCHADDTAX"]);
            WDNINCTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WDNINCTAX"]);
            WSNINCTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WSNINCTAX"]);
            WSNRURTAX46.Text = string.Format("{0:#,###}", _dt.Rows[70]["WSNRURTAX"]);

            WPAYGUBN47.Text = _dt.Rows[71]["WPAYGUBN"].ToString();
            WPEOPLE47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WPEOPLE"]);
            WPAYAMOUNT47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WPAYAMOUNT"]);
            WCHINCTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WCHINCTAX"]);
            WCHRURTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WCHRURTAX"]);
            WCHADDTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WCHADDTAX"]);
            WDNINCTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WDNINCTAX"]);
            WSNINCTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WSNINCTAX"]);
            WSNRURTAX47.Text = string.Format("{0:#,###}", _dt.Rows[71]["WSNRURTAX"]);

            WPAYGUBN48.Text = _dt.Rows[72]["WPAYGUBN"].ToString();
            WPEOPLE48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WPEOPLE"]);
            WPAYAMOUNT48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WPAYAMOUNT"]);
            WCHINCTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WCHINCTAX"]);
            WCHRURTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WCHRURTAX"]);
            WCHADDTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WCHADDTAX"]);
            WDNINCTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WDNINCTAX"]);
            WSNINCTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WSNINCTAX"]);
            WSNRURTAX48.Text = string.Format("{0:#,###}", _dt.Rows[72]["WSNRURTAX"]);

            WPAYGUBN49.Text = _dt.Rows[73]["WPAYGUBN"].ToString();
            WPEOPLE49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WPEOPLE"]);
            WPAYAMOUNT49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WPAYAMOUNT"]);
            WCHINCTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WCHINCTAX"]);
            WCHRURTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WCHRURTAX"]);
            WCHADDTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WCHADDTAX"]);
            WDNINCTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WDNINCTAX"]);
            WSNINCTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WSNINCTAX"]);
            WSNRURTAX49.Text = string.Format("{0:#,###}", _dt.Rows[73]["WSNRURTAX"]);

            WPAYGUBN50.Text = _dt.Rows[74]["WPAYGUBN"].ToString();
            WPEOPLE50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WPEOPLE"]);
            WPAYAMOUNT50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WPAYAMOUNT"]);
            WCHINCTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WCHINCTAX"]);
            WCHRURTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WCHRURTAX"]);
            WCHADDTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WCHADDTAX"]);
            WDNINCTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WDNINCTAX"]);
            WSNINCTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WSNINCTAX"]);
            WSNRURTAX50.Text = string.Format("{0:#,###}", _dt.Rows[74]["WSNRURTAX"]);

            WPAYGUBN51.Text = _dt.Rows[75]["WPAYGUBN"].ToString();
            WPEOPLE51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WPEOPLE"]);
            WPAYAMOUNT51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WPAYAMOUNT"]);
            WCHINCTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WCHINCTAX"]);
            WCHRURTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WCHRURTAX"]);
            WCHADDTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WCHADDTAX"]);
            WDNINCTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WDNINCTAX"]);
            WSNINCTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WSNINCTAX"]);
            WSNRURTAX51.Text = string.Format("{0:#,###}", _dt.Rows[75]["WSNRURTAX"]);

            WPAYGUBN52.Text = _dt.Rows[76]["WPAYGUBN"].ToString();
            WPEOPLE52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WPEOPLE"]);
            WPAYAMOUNT52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WPAYAMOUNT"]);
            WCHINCTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WCHINCTAX"]);
            WCHRURTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WCHRURTAX"]);
            WCHADDTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WCHADDTAX"]);
            WDNINCTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WDNINCTAX"]);
            WSNINCTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WSNINCTAX"]);
            WSNRURTAX52.Text = string.Format("{0:#,###}", _dt.Rows[76]["WSNRURTAX"]);

            WPAYGUBN53.Text = _dt.Rows[77]["WPAYGUBN"].ToString();
            WPEOPLE53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WPEOPLE"]);
            WPAYAMOUNT53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WPAYAMOUNT"]);
            WCHINCTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WCHINCTAX"]);
            WCHRURTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WCHRURTAX"]);
            WCHADDTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WCHADDTAX"]);
            WDNINCTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WDNINCTAX"]);
            WSNINCTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WSNINCTAX"]);
            WSNRURTAX53.Text = string.Format("{0:#,###}", _dt.Rows[77]["WSNRURTAX"]);

            WPAYGUBN54.Text = _dt.Rows[78]["WPAYGUBN"].ToString();
            WPEOPLE54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WPEOPLE"]);
            WPAYAMOUNT54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WPAYAMOUNT"]);
            WCHINCTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WCHINCTAX"]);
            WCHRURTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WCHRURTAX"]);
            WCHADDTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WCHADDTAX"]);
            WDNINCTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WDNINCTAX"]);
            WSNINCTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WSNINCTAX"]);
            WSNRURTAX54.Text = string.Format("{0:#,###}", _dt.Rows[78]["WSNRURTAX"]);

            WPAYGUBN55.Text = _dt.Rows[79]["WPAYGUBN"].ToString();
            WPEOPLE55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WPEOPLE"]);
            WPAYAMOUNT55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WPAYAMOUNT"]);
            WCHINCTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WCHINCTAX"]);
            WCHRURTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WCHRURTAX"]);
            WCHADDTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WCHADDTAX"]);
            WDNINCTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WDNINCTAX"]);
            WSNINCTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WSNINCTAX"]);
            WSNRURTAX55.Text = string.Format("{0:#,###}", _dt.Rows[79]["WSNRURTAX"]);

            WPAYGUBN56.Text = _dt.Rows[80]["WPAYGUBN"].ToString();
            WPEOPLE56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WPEOPLE"]);
            WPAYAMOUNT56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WPAYAMOUNT"]);
            WCHINCTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WCHINCTAX"]);
            WCHRURTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WCHRURTAX"]);
            WCHADDTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WCHADDTAX"]);
            WDNINCTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WDNINCTAX"]);
            WSNINCTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WSNINCTAX"]);
            WSNRURTAX56.Text = string.Format("{0:#,###}", _dt.Rows[80]["WSNRURTAX"]);

            WPAYGUBN57.Text = _dt.Rows[81]["WPAYGUBN"].ToString();
            WPEOPLE57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WPEOPLE"]);
            WPAYAMOUNT57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WPAYAMOUNT"]);
            WCHINCTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WCHINCTAX"]);
            WCHRURTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WCHRURTAX"]);
            WCHADDTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WCHADDTAX"]);
            WDNINCTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WDNINCTAX"]);
            WSNINCTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WSNINCTAX"]);
            WSNRURTAX57.Text = string.Format("{0:#,###}", _dt.Rows[81]["WSNRURTAX"]);

            WPAYGUBN58.Text = _dt.Rows[82]["WPAYGUBN"].ToString();
            WPEOPLE58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WPEOPLE"]);
            WPAYAMOUNT58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WPAYAMOUNT"]);
            WCHINCTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WCHINCTAX"]);
            WCHRURTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WCHRURTAX"]);
            WCHADDTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WCHADDTAX"]);
            WDNINCTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WDNINCTAX"]);
            WSNINCTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WSNINCTAX"]);
            WSNRURTAX58.Text = string.Format("{0:#,###}", _dt.Rows[82]["WSNRURTAX"]);

            WPAYGUBN59.Text = _dt.Rows[83]["WPAYGUBN"].ToString();
            WPEOPLE59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WPEOPLE"]);
            WPAYAMOUNT59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WPAYAMOUNT"]);
            WCHINCTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WCHINCTAX"]);
            WCHRURTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WCHRURTAX"]);
            WCHADDTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WCHADDTAX"]);
            WDNINCTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WDNINCTAX"]);
            WSNINCTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WSNINCTAX"]);
            WSNRURTAX59.Text = string.Format("{0:#,###}", _dt.Rows[83]["WSNRURTAX"]);

            WPAYGUBN60.Text = _dt.Rows[84]["WPAYGUBN"].ToString();
            WPEOPLE60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WPEOPLE"]);
            WPAYAMOUNT60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WPAYAMOUNT"]);
            WCHINCTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WCHINCTAX"]);
            WCHRURTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WCHRURTAX"]);
            WCHADDTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WCHADDTAX"]);
            WDNINCTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WDNINCTAX"]);
            WSNINCTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WSNINCTAX"]);
            WSNRURTAX60.Text = string.Format("{0:#,###}", _dt.Rows[84]["WSNRURTAX"]);

            WPAYGUBN61.Text = _dt.Rows[85]["WPAYGUBN"].ToString();
            WPEOPLE61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WPEOPLE"]);
            WPAYAMOUNT61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WPAYAMOUNT"]);
            WCHINCTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WCHINCTAX"]);
            WCHRURTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WCHRURTAX"]);
            WCHADDTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WCHADDTAX"]);
            WDNINCTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WDNINCTAX"]);
            WSNINCTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WSNINCTAX"]);
            WSNRURTAX61.Text = string.Format("{0:#,###}", _dt.Rows[85]["WSNRURTAX"]);

            WPAYGUBN62.Text = _dt.Rows[86]["WPAYGUBN"].ToString();
            WPEOPLE62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WPEOPLE"]);
            WPAYAMOUNT62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WPAYAMOUNT"]);
            WCHINCTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WCHINCTAX"]);
            WCHRURTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WCHRURTAX"]);
            WCHADDTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WCHADDTAX"]);
            WDNINCTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WDNINCTAX"]);
            WSNINCTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WSNINCTAX"]);
            WSNRURTAX62.Text = string.Format("{0:#,###}", _dt.Rows[86]["WSNRURTAX"]);

            WPAYGUBN63.Text = _dt.Rows[87]["WPAYGUBN"].ToString();
            WPEOPLE63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WPEOPLE"]);
            WPAYAMOUNT63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WPAYAMOUNT"]);
            WCHINCTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WCHINCTAX"]);
            WCHRURTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WCHRURTAX"]);
            WCHADDTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WCHADDTAX"]);
            WDNINCTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WDNINCTAX"]);
            WSNINCTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WSNINCTAX"]);
            WSNRURTAX63.Text = string.Format("{0:#,###}", _dt.Rows[87]["WSNRURTAX"]);

            WPAYGUBN64.Text = _dt.Rows[88]["WPAYGUBN"].ToString();
            WPEOPLE64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WPEOPLE"]);
            WPAYAMOUNT64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WPAYAMOUNT"]);
            WCHINCTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WCHINCTAX"]);
            WCHRURTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WCHRURTAX"]);
            WCHADDTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WCHADDTAX"]);
            WDNINCTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WDNINCTAX"]);
            WSNINCTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WSNINCTAX"]);
            WSNRURTAX64.Text = string.Format("{0:#,###}", _dt.Rows[88]["WSNRURTAX"]);

            WPAYGUBN65.Text = _dt.Rows[89]["WPAYGUBN"].ToString();
            WPEOPLE65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WPEOPLE"]);
            WPAYAMOUNT65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WPAYAMOUNT"]);
            WCHINCTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WCHINCTAX"]);
            WCHRURTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WCHRURTAX"]);
            WCHADDTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WCHADDTAX"]);
            WDNINCTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WDNINCTAX"]);
            WSNINCTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WSNINCTAX"]);
            WSNRURTAX65.Text = string.Format("{0:#,###}", _dt.Rows[89]["WSNRURTAX"]);

            WPAYGUBN66.Text = _dt.Rows[90]["WPAYGUBN"].ToString();
            WPEOPLE66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WPEOPLE"]);
            WPAYAMOUNT66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WPAYAMOUNT"]);
            WCHINCTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WCHINCTAX"]);
            WCHRURTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WCHRURTAX"]);
            WCHADDTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WCHADDTAX"]);
            WDNINCTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WDNINCTAX"]);
            WSNINCTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WSNINCTAX"]);
            WSNRURTAX66.Text = string.Format("{0:#,###}", _dt.Rows[90]["WSNRURTAX"]);

            WPAYGUBN67.Text = _dt.Rows[91]["WPAYGUBN"].ToString();
            WPEOPLE67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WPEOPLE"]);
            WPAYAMOUNT67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WPAYAMOUNT"]);
            WCHINCTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WCHINCTAX"]);
            WCHRURTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WCHRURTAX"]);
            WCHADDTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WCHADDTAX"]);
            WDNINCTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WDNINCTAX"]);
            WSNINCTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WSNINCTAX"]);
            WSNRURTAX67.Text = string.Format("{0:#,###}", _dt.Rows[91]["WSNRURTAX"]);

            WPAYGUBN68.Text = _dt.Rows[92]["WPAYGUBN"].ToString();
            WPEOPLE68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WPEOPLE"]);
            WPAYAMOUNT68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WPAYAMOUNT"]);
            WCHINCTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WCHINCTAX"]);
            WCHRURTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WCHRURTAX"]);
            WCHADDTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WCHADDTAX"]);
            WDNINCTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WDNINCTAX"]);
            WSNINCTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WSNINCTAX"]);
            WSNRURTAX68.Text = string.Format("{0:#,###}", _dt.Rows[92]["WSNRURTAX"]);

            WPAYGUBN69.Text = _dt.Rows[93]["WPAYGUBN"].ToString();
            WPEOPLE69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WPEOPLE"]);
            WPAYAMOUNT69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WPAYAMOUNT"]);
            WCHINCTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WCHINCTAX"]);
            WCHRURTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WCHRURTAX"]);
            WCHADDTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WCHADDTAX"]);
            WDNINCTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WDNINCTAX"]);
            WSNINCTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WSNINCTAX"]);
            WSNRURTAX69.Text = string.Format("{0:#,###}", _dt.Rows[93]["WSNRURTAX"]);

            WPAYGUBN70.Text = _dt.Rows[94]["WPAYGUBN"].ToString();
            WPEOPLE70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WPEOPLE"]);
            WPAYAMOUNT70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WPAYAMOUNT"]);
            WCHINCTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WCHINCTAX"]);
            WCHRURTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WCHRURTAX"]);
            WCHADDTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WCHADDTAX"]);
            WDNINCTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WDNINCTAX"]);
            WSNINCTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WSNINCTAX"]);
            WSNRURTAX70.Text = string.Format("{0:#,###}", _dt.Rows[94]["WSNRURTAX"]);
        }
    }
}
