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
    /// Summary description for TYACTP009R.
    /// </summary>
    public partial class TYACTP009R1 : GrapeCity.ActiveReports.SectionReport
    {
        DataTable _dt = new DataTable();
        DataTable _dt2 = new DataTable();
        DataTable _dt3 = new DataTable();

        public TYACTP009R1(DataTable dt, DataTable dt3)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _dt = dt;
            _dt3 = dt3;
        }

        private void reportHeader_Format(object sender, EventArgs e)
        {
            _dt2 = this.DataSource as DataTable;

            if (_dt2.Rows.Count > 0)
            {
                DATE.Text = string.Format("{0:yyyy년 MM월 dd일}", System.DateTime.Now);

                ASMSANGHO.Text = _dt3.Rows[0]["ASMSANGHO"].ToString();
                ASMNAMENM.Text = _dt3.Rows[0]["ASMNAMENM"].ToString();
                ASMNAMENM2.Text = _dt3.Rows[0]["ASMNAMENM"].ToString();
                ASMSAUPNO.Text = _dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(0,3) + "-" + _dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(3,2) + "-" + _dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(5,5);
                ASMVNADDRS.Text = _dt3.Rows[0]["ASMVNADDRS"].ToString();
                ASMTELNUM.Text = _dt3.Rows[0]["ASMTELNUM"].ToString();
                OOFFNAME.Text = _dt3.Rows[0]["OOFFNAME"].ToString() + "세무서장";

                WREVYYMM.Text = _dt2.Rows[0]["WREVYYMM"].ToString().Substring(0, 4) + "년 " + _dt2.Rows[0]["WREVYYMM"].ToString().Substring(4, 2) + "월";
                WGIVYYMM.Text = _dt2.Rows[0]["WGIVYYMM"].ToString().Substring(0, 4) + "년 " + _dt2.Rows[0]["WGIVYYMM"].ToString().Substring(4, 2) + "월";

                WPAYGUBNNM1.Text = _dt2.Rows[0]["WPAYGUBNNM"].ToString();
                WPAYGUBN1.Text = _dt2.Rows[0]["WPAYGUBN"].ToString();
                WPEOPLE1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WPEOPLE"]);
                WPAYAMOUNT1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WPAYAMOUNT"]);
                WCHINCTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WCHINCTAX"]);
                WCHRURTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WCHRURTAX"]);
                WCHADDTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WCHADDTAX"]);
                WDNINCTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WDNINCTAX"]);
                WSNINCTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WSNINCTAX"]);
                WSNRURTAX1.Text = string.Format("{0:#,###}", _dt2.Rows[0]["WSNRURTAX"]);

                WPAYGUBNNM2.Text = _dt2.Rows[1]["WPAYGUBNNM"].ToString();
                WPAYGUBN2.Text = _dt2.Rows[1]["WPAYGUBN"].ToString();
                WPEOPLE2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WPEOPLE"]);
                WPAYAMOUNT2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WPAYAMOUNT"]);
                WCHINCTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WCHINCTAX"]);
                WCHRURTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WCHRURTAX"]);
                WCHADDTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WCHADDTAX"]);
                WDNINCTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WDNINCTAX"]);
                WSNINCTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WSNINCTAX"]);
                WSNRURTAX2.Text = string.Format("{0:#,###}", _dt2.Rows[1]["WSNRURTAX"]);

                WPAYGUBNNM3.Text = _dt2.Rows[2]["WPAYGUBNNM"].ToString();
                WPAYGUBN3.Text = _dt2.Rows[2]["WPAYGUBN"].ToString();
                WPEOPLE3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WPEOPLE"]);
                WPAYAMOUNT3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WPAYAMOUNT"]);
                WCHINCTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WCHINCTAX"]);
                WCHRURTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WCHRURTAX"]);
                WCHADDTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WCHADDTAX"]);
                WDNINCTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WDNINCTAX"]);
                WSNINCTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WSNINCTAX"]);
                WSNRURTAX3.Text = string.Format("{0:#,###}", _dt2.Rows[2]["WSNRURTAX"]);

                WPAYGUBNNM4.Text = _dt2.Rows[3]["WPAYGUBNNM"].ToString();
                WPAYGUBN4.Text = _dt2.Rows[3]["WPAYGUBN"].ToString();
                WPEOPLE4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WPEOPLE"]);
                WPAYAMOUNT4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WPAYAMOUNT"]);
                WCHINCTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WCHINCTAX"]);
                WCHRURTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WCHRURTAX"]);
                WCHADDTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WCHADDTAX"]);
                WDNINCTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WDNINCTAX"]);
                WSNINCTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WSNINCTAX"]);
                WSNRURTAX4.Text = string.Format("{0:#,###}", _dt2.Rows[3]["WSNRURTAX"]);

                WPAYGUBNNM5.Text = _dt2.Rows[4]["WPAYGUBNNM"].ToString();
                WPAYGUBN5.Text = _dt2.Rows[4]["WPAYGUBN"].ToString();
                WPEOPLE5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WPEOPLE"]);
                WPAYAMOUNT5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WPAYAMOUNT"]);
                WCHINCTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WCHINCTAX"]);
                WCHRURTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WCHRURTAX"]);
                WCHADDTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WCHADDTAX"]);
                WDNINCTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WDNINCTAX"]);
                WSNINCTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WSNINCTAX"]);
                WSNRURTAX5.Text = string.Format("{0:#,###}", _dt2.Rows[4]["WSNRURTAX"]);

                WPAYGUBNNM6.Text = _dt2.Rows[5]["WPAYGUBNNM"].ToString();
                WPAYGUBN6.Text = _dt2.Rows[5]["WPAYGUBN"].ToString();
                WPEOPLE6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WPEOPLE"]);
                WPAYAMOUNT6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WPAYAMOUNT"]);
                WCHINCTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WCHINCTAX"]);
                WCHRURTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WCHRURTAX"]);
                WCHADDTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WCHADDTAX"]);
                WDNINCTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WDNINCTAX"]);
                WSNINCTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WSNINCTAX"]);
                WSNRURTAX6.Text = string.Format("{0:#,###}", _dt2.Rows[5]["WSNRURTAX"]);

                WPAYGUBNNM7.Text = _dt2.Rows[6]["WPAYGUBNNM"].ToString();
                WPAYGUBN7.Text = _dt2.Rows[6]["WPAYGUBN"].ToString();
                WPEOPLE7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WPEOPLE"]);
                WPAYAMOUNT7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WPAYAMOUNT"]);
                WCHINCTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WCHINCTAX"]);
                WCHRURTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WCHRURTAX"]);
                WCHADDTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WCHADDTAX"]);
                WDNINCTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WDNINCTAX"]);
                WSNINCTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WSNINCTAX"]);
                WSNRURTAX7.Text = string.Format("{0:#,###}", _dt2.Rows[6]["WSNRURTAX"]);

                WPAYGUBNNM8.Text = _dt2.Rows[7]["WPAYGUBNNM"].ToString();
                WPAYGUBN8.Text = _dt2.Rows[7]["WPAYGUBN"].ToString();
                WPEOPLE8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WPEOPLE"]);
                WPAYAMOUNT8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WPAYAMOUNT"]);
                WCHINCTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WCHINCTAX"]);
                WCHRURTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WCHRURTAX"]);
                WCHADDTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WCHADDTAX"]);
                WDNINCTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WDNINCTAX"]);
                WSNINCTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WSNINCTAX"]);
                WSNRURTAX8.Text = string.Format("{0:#,###}", _dt2.Rows[7]["WSNRURTAX"]);

                WPAYGUBNNM9.Text = _dt2.Rows[8]["WPAYGUBNNM"].ToString();
                WPAYGUBN9.Text = _dt2.Rows[8]["WPAYGUBN"].ToString();
                WPEOPLE9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WPEOPLE"]);
                WPAYAMOUNT9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WPAYAMOUNT"]);
                WCHINCTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WCHINCTAX"]);
                WCHRURTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WCHRURTAX"]);
                WCHADDTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WCHADDTAX"]);
                WDNINCTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WDNINCTAX"]);
                WSNINCTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WSNINCTAX"]);
                WSNRURTAX9.Text = string.Format("{0:#,###}", _dt2.Rows[8]["WSNRURTAX"]);

                WPAYGUBNNM10.Text = _dt2.Rows[9]["WPAYGUBNNM"].ToString();
                WPAYGUBN10.Text = _dt2.Rows[9]["WPAYGUBN"].ToString();
                WPEOPLE10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WPEOPLE"]);
                WPAYAMOUNT10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WPAYAMOUNT"]);
                WCHINCTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WCHINCTAX"]);
                WCHRURTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WCHRURTAX"]);
                WCHADDTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WCHADDTAX"]);
                WDNINCTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WDNINCTAX"]);
                WSNINCTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WSNINCTAX"]);
                WSNRURTAX10.Text = string.Format("{0:#,###}", _dt2.Rows[9]["WSNRURTAX"]);

                WPAYGUBNNM11.Text = _dt2.Rows[10]["WPAYGUBNNM"].ToString();
                WPAYGUBN11.Text = _dt2.Rows[10]["WPAYGUBN"].ToString();
                WPEOPLE11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WPEOPLE"]);
                WPAYAMOUNT11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WPAYAMOUNT"]);
                WCHINCTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WCHINCTAX"]);
                WCHRURTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WCHRURTAX"]);
                WCHADDTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WCHADDTAX"]);
                WDNINCTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WDNINCTAX"]);
                WSNINCTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WSNINCTAX"]);
                WSNRURTAX11.Text = string.Format("{0:#,###}", _dt2.Rows[10]["WSNRURTAX"]);

                WPAYGUBNNM12.Text = _dt2.Rows[11]["WPAYGUBNNM"].ToString();
                WPAYGUBN12.Text = _dt2.Rows[11]["WPAYGUBN"].ToString();
                WPEOPLE12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WPEOPLE"]);
                WPAYAMOUNT12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WPAYAMOUNT"]);
                WCHINCTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WCHINCTAX"]);
                WCHRURTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WCHRURTAX"]);
                WCHADDTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WCHADDTAX"]);
                WDNINCTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WDNINCTAX"]);
                WSNINCTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WSNINCTAX"]);
                WSNRURTAX12.Text = string.Format("{0:#,###}", _dt2.Rows[11]["WSNRURTAX"]);

                WPAYGUBNNM13.Text = _dt2.Rows[12]["WPAYGUBNNM"].ToString();
                WPAYGUBN13.Text = _dt2.Rows[12]["WPAYGUBN"].ToString();
                WPEOPLE13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WPEOPLE"]);
                WPAYAMOUNT13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WPAYAMOUNT"]);
                WCHINCTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WCHINCTAX"]);
                WCHRURTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WCHRURTAX"]);
                WCHADDTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WCHADDTAX"]);
                WDNINCTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WDNINCTAX"]);
                WSNINCTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WSNINCTAX"]);
                WSNRURTAX13.Text = string.Format("{0:#,###}", _dt2.Rows[12]["WSNRURTAX"]);

                WPAYGUBNNM14.Text = _dt2.Rows[13]["WPAYGUBNNM"].ToString();
                WPAYGUBN14.Text = _dt2.Rows[13]["WPAYGUBN"].ToString();
                WPEOPLE14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WPEOPLE"]);
                WPAYAMOUNT14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WPAYAMOUNT"]);
                WCHINCTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WCHINCTAX"]);
                WCHRURTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WCHRURTAX"]);
                WCHADDTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WCHADDTAX"]);
                WDNINCTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WDNINCTAX"]);
                WSNINCTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WSNINCTAX"]);
                WSNRURTAX14.Text = string.Format("{0:#,###}", _dt2.Rows[13]["WSNRURTAX"]);

                WPAYGUBNNM15.Text = _dt2.Rows[14]["WPAYGUBNNM"].ToString();
                WPAYGUBN15.Text = _dt2.Rows[14]["WPAYGUBN"].ToString();
                WPEOPLE15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WPEOPLE"]);
                WPAYAMOUNT15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WPAYAMOUNT"]);
                WCHINCTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WCHINCTAX"]);
                WCHRURTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WCHRURTAX"]);
                WCHADDTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WCHADDTAX"]);
                WDNINCTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WDNINCTAX"]);
                WSNINCTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WSNINCTAX"]);
                WSNRURTAX15.Text = string.Format("{0:#,###}", _dt2.Rows[14]["WSNRURTAX"]);

                WPAYGUBNNM16.Text = _dt2.Rows[15]["WPAYGUBNNM"].ToString();
                WPAYGUBN16.Text = _dt2.Rows[15]["WPAYGUBN"].ToString();
                WPEOPLE16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WPEOPLE"]);
                WPAYAMOUNT16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WPAYAMOUNT"]);
                WCHINCTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WCHINCTAX"]);
                WCHRURTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WCHRURTAX"]);
                WCHADDTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WCHADDTAX"]);
                WDNINCTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WDNINCTAX"]);
                WSNINCTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WSNINCTAX"]);
                WSNRURTAX16.Text = string.Format("{0:#,###}", _dt2.Rows[15]["WSNRURTAX"]);

                WPAYGUBNNM17.Text = _dt2.Rows[16]["WPAYGUBNNM"].ToString();
                WPAYGUBN17.Text = _dt2.Rows[16]["WPAYGUBN"].ToString();
                WPEOPLE17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WPEOPLE"]);
                WPAYAMOUNT17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WPAYAMOUNT"]);
                WCHINCTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WCHINCTAX"]);
                WCHRURTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WCHRURTAX"]);
                WCHADDTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WCHADDTAX"]);
                WDNINCTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WDNINCTAX"]);
                WSNINCTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WSNINCTAX"]);
                WSNRURTAX17.Text = string.Format("{0:#,###}", _dt2.Rows[16]["WSNRURTAX"]);

                WPAYGUBNNM18.Text = _dt2.Rows[17]["WPAYGUBNNM"].ToString();
                WPAYGUBN18.Text = _dt2.Rows[17]["WPAYGUBN"].ToString();
                WPEOPLE18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WPEOPLE"]);
                WPAYAMOUNT18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WPAYAMOUNT"]);
                WCHINCTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WCHINCTAX"]);
                WCHRURTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WCHRURTAX"]);
                WCHADDTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WCHADDTAX"]);
                WDNINCTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WDNINCTAX"]);
                WSNINCTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WSNINCTAX"]);
                WSNRURTAX18.Text = string.Format("{0:#,###}", _dt2.Rows[17]["WSNRURTAX"]);

                WPAYGUBNNM19.Text = _dt2.Rows[18]["WPAYGUBNNM"].ToString();
                WPAYGUBN19.Text = _dt2.Rows[18]["WPAYGUBN"].ToString();
                WPEOPLE19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WPEOPLE"]);
                WPAYAMOUNT19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WPAYAMOUNT"]);
                WCHINCTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WCHINCTAX"]);
                WCHRURTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WCHRURTAX"]);
                WCHADDTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WCHADDTAX"]);
                WDNINCTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WDNINCTAX"]);
                WSNINCTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WSNINCTAX"]);
                WSNRURTAX19.Text = string.Format("{0:#,###}", _dt2.Rows[18]["WSNRURTAX"]);

                WPAYGUBNNM20.Text = _dt2.Rows[19]["WPAYGUBNNM"].ToString();
                WPAYGUBN20.Text = _dt2.Rows[19]["WPAYGUBN"].ToString();
                WPEOPLE20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WPEOPLE"]);
                WPAYAMOUNT20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WPAYAMOUNT"]);
                WCHINCTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WCHINCTAX"]);
                WCHRURTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WCHRURTAX"]);
                WCHADDTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WCHADDTAX"]);
                WDNINCTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WDNINCTAX"]);
                WSNINCTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WSNINCTAX"]);
                WSNRURTAX20.Text = string.Format("{0:#,###}", _dt2.Rows[19]["WSNRURTAX"]);

                WPAYGUBNNM21.Text = _dt2.Rows[20]["WPAYGUBNNM"].ToString();
                WPAYGUBN21.Text = _dt2.Rows[20]["WPAYGUBN"].ToString();
                WPEOPLE21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WPEOPLE"]);
                WPAYAMOUNT21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WPAYAMOUNT"]);
                WCHINCTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WCHINCTAX"]);
                WCHRURTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WCHRURTAX"]);
                WCHADDTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WCHADDTAX"]);
                WDNINCTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WDNINCTAX"]);
                WSNINCTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WSNINCTAX"]);
                WSNRURTAX21.Text = string.Format("{0:#,###}", _dt2.Rows[20]["WSNRURTAX"]);

                WPAYGUBNNM22.Text = _dt2.Rows[21]["WPAYGUBNNM"].ToString();
                WPAYGUBN22.Text = _dt2.Rows[21]["WPAYGUBN"].ToString();
                WPEOPLE22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WPEOPLE"]);
                WPAYAMOUNT22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WPAYAMOUNT"]);
                WCHINCTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WCHINCTAX"]);
                WCHRURTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WCHRURTAX"]);
                WCHADDTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WCHADDTAX"]);
                WDNINCTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WDNINCTAX"]);
                WSNINCTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WSNINCTAX"]);
                WSNRURTAX22.Text = string.Format("{0:#,###}", _dt2.Rows[21]["WSNRURTAX"]);

                WPAYGUBNNM23.Text = _dt2.Rows[22]["WPAYGUBNNM"].ToString();
                WPAYGUBN23.Text = _dt2.Rows[22]["WPAYGUBN"].ToString();
                WPEOPLE23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WPEOPLE"]);
                WPAYAMOUNT23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WPAYAMOUNT"]);
                WCHINCTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WCHINCTAX"]);
                WCHRURTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WCHRURTAX"]);
                WCHADDTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WCHADDTAX"]);
                WDNINCTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WDNINCTAX"]);
                WSNINCTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WSNINCTAX"]);
                WSNRURTAX23.Text = string.Format("{0:#,###}", _dt2.Rows[22]["WSNRURTAX"]);

                WPAYGUBNNM24.Text = _dt2.Rows[23]["WPAYGUBNNM"].ToString();
                WPAYGUBN24.Text = _dt2.Rows[23]["WPAYGUBN"].ToString();
                WPEOPLE24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WPEOPLE"]);
                WPAYAMOUNT24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WPAYAMOUNT"]);
                WCHINCTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WCHINCTAX"]);
                WCHRURTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WCHRURTAX"]);
                WCHADDTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WCHADDTAX"]);
                WDNINCTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WDNINCTAX"]);
                WSNINCTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WSNINCTAX"]);
                WSNRURTAX24.Text = string.Format("{0:#,###}", _dt2.Rows[23]["WSNRURTAX"]);

                WPAYGUBNNM25.Text = _dt2.Rows[24]["WPAYGUBNNM"].ToString();
                WPAYGUBN25.Text = _dt2.Rows[24]["WPAYGUBN"].ToString();
                WPEOPLE25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WPEOPLE"]);
                WPAYAMOUNT25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WPAYAMOUNT"]);
                WCHINCTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WCHINCTAX"]);
                WCHRURTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WCHRURTAX"]);
                WCHADDTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WCHADDTAX"]);
                WDNINCTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WDNINCTAX"]);
                WSNINCTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WSNINCTAX"]);
                WSNRURTAX25.Text = string.Format("{0:#,###}", _dt2.Rows[24]["WSNRURTAX"]);

                if (_dt.Rows.Count > 0)
                {
                    WJMISTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WJMISTAX"]);
                    WJGIRTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WJGIRTAX"]);
                    WJDEDTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WJDEDTAX"]);
                    WDGENTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WDGENTAX"]);
                    WDTRUTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WDTRUTAX"]);
                    WDBANKTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WDBANKTAX"]);
                    WDMERGTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WDMERGTAX"]);
                    WNMEDTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WNMEDTAX"]);
                    WNNOWTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WNNOWTAX"]);
                    WNNEXTTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WNNEXTTAX"]);
                    WNREFTAX.Text = string.Format("{0:#,###}", _dt.Rows[0]["WNREFTAX"]);
                }

                if (_dt2.Rows[24]["WRETURNGB"].ToString() == "1")
                {
                    WRETURNGB1.Text = "○";
                }
                else if (_dt2.Rows[24]["WRETURNGB"].ToString() == "2")
                {
                    WRETURNGB2.Text = "○";
                }
                if (_dt2.Rows[24]["WAMENDEGB"].ToString() == "Y")
                {
                    WAMENDEGB.Text = "○";
                }
                if (_dt2.Rows[24]["WYEARTXGB"].ToString() == "Y")
                {
                    WYEARTXGB.Text = "○";
                }
                if (_dt2.Rows[24]["WDISOISGB"].ToString() == "Y")
                {
                    WDISOISGB.Text = "○";
                }
                if (_dt2.Rows[24]["WREQUESGB"].ToString() == "Y")
                {
                    WREQUESGB.Text = "○";
                }

                if (_dt2.Rows[24]["WBATHCHGB"].ToString() == "Y")
                {
                    WBATHCHGB1.Text = "○";
                }
                else if (_dt2.Rows[24]["WBATHCHGB"].ToString() == "N")
                {
                    WBATHCHGB2.Text = "○";
                }

                if (_dt2.Rows[24]["WBUSINEGB"].ToString() == "Y")
                {
                    WBUSINEGB1.Text = "○";
                }
                else if (_dt2.Rows[24]["WBUSINEGB"].ToString() == "N")
                {
                    WBUSINEGB2.Text = "○";
                }

                if (_dt2.Rows[24]["WDETAILGB"].ToString() == "Y")
                {
                    WDETAILGB.Text = "○";

                    reportFooter.Visible = true;
                    TYACTP009R2 subReport = new TYACTP009R2(_dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(0, 3) + "-" + _dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(3, 2) + "-" + _dt3.Rows[0]["ASMSAUPNO"].ToString().Substring(5, 5));
                    subReport.DataSource = _dt2;
                    TYACTP009R2.Report = subReport;
                }
                else
                {
                    reportFooter.Visible = false;
                }
                if (_dt2.Rows[24]["WREFUNDGB"].ToString() == "Y")
                {
                    WREFUNDGB.Text = "○";
                }
                if (_dt2.Rows[24]["WSUCCESGB"].ToString() == "Y")
                {
                    WSUCCESGB.Text = "○";
                }
            }
        }
    }
}
