using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 반제설정 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.26 09:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29Q9V306 : 반제설정 마스타 조회
    ///  TY_P_AC_29Q9W307 : 반제설정 정리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29Q9Y309 : 반제설정 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  B7CDAC : 계정코드
    ///  B7VEND : 거래처
    /// </summary>
    public partial class TYACFP006S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFP006S()
        {
            InitializeComponent();
        }

        private void TYACFP006S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_29Q9Y309, "B7WCJP");

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetValue();


            this.BTN61_PRT.Visible = false;

            this.FPS91_TY_S_AC_2CSAZ425.Visible = true;
            this.FPS91_TY_S_AC_29Q9Y309.Visible = false; 

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSort = "";
     
            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.BTN61_PRT.Visible = true;

                this.FPS91_TY_S_AC_2CSAZ425.Visible = true;
                this.FPS91_TY_S_AC_29Q9Y309.Visible = false;

                this.FPS91_TY_S_AC_2CSAZ425.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CSAY422",this.DTP01_GEDDATE.GetString(),  this.DTP01_GEDDATE.GetString() , this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B7CDAC.GetValue(), this.CBH01_B7VEND.GetValue(), this.CBH01_B2DPMK.GetValue());
                
                if( this.CBH01_B7CDAC.GetValue().ToString() != "" && this.CBH01_B7VEND.GetValue().ToString() == "" )
                {
                    sSort = "1";  //계정코드만 입력 경우
                }
                else if( this.CBH01_B7CDAC.GetValue().ToString() == "" && this.CBH01_B7VEND.GetValue().ToString() != "" )
                {
                    sSort = "2"; //거래처만 입력 경우
                }
                else
                {
                    sSort = "3";  //둘다 입력하거나 둘다 입력 안한경우
                }

                DataTable dt = UP_SuTotal_dt(this.DbConnector.ExecuteDataTable(), sSort);

                this.FPS91_TY_S_AC_2CSAZ425.SetValue(dt);

                if (this.FPS91_TY_S_AC_2CSAZ425.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2CSAZ425, "B7VENDNM", "[소     계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2CSAZ425, "B7VENDNM", "[총     계]", SumRowType.Total);
                }

                for (int i = 0; i < this.FPS91_TY_S_AC_2CSAZ425.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2CSAZ425.GetValue(i, "B7WCJP").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_2CSAZ425_Sheet1.Cells[i, 10].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                    if (this.FPS91_TY_S_AC_2CSAZ425.GetValue(i, "B8BJJP").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_2CSAZ425_Sheet1.Cells[i, 12].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
            else
            {
                // this.BTN61_PRT.SetReadOnly(false);
                this.BTN61_PRT.Visible = false;

                this.FPS91_TY_S_AC_2CSAZ425.Visible = false;
                this.FPS91_TY_S_AC_29Q9Y309.Visible = true; 

                this.FPS91_TY_S_AC_29Q9Y309.Initialize();

                DataSet ds = new DataSet();
                DataTable dt0, dt1, dtM, dtS;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29Q9V306",this.DTP01_GEDDATE.GetString(), this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B7CDAC.GetValue(), this.CBH01_B7VEND.GetValue(), this.CBH01_B2DPMK.GetValue());
                dtM = new DataTable("TABLE0");
                dtM = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29Q9W307", this.DTP01_GEDDATE.GetString(), this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B7CDAC.GetValue(), this.CBH01_B7VEND.GetValue(), this.CBH01_B2DPMK.GetValue());
                dtS = new DataTable("TABLE1");
                dtS = this.DbConnector.ExecuteDataTable();

                dt0 = dtM.Clone();
                dt0.TableName = "TABLE0";
                foreach (DataRow dr in dtM.Select())
                    dt0.Rows.Add(dr.ItemArray);
                ds.Tables.Add(dt0);

                dt1 = dtS.Clone();
                dt1.TableName = "TABLE1";
                foreach (DataRow dr in dtS.Select())
                    dt1.Rows.Add(dr.ItemArray);
                ds.Tables.Add(dt1);

                ds.Relations.Add("RELATION_0_1", dt0.Columns["KEY"], dt1.Columns["PARENTKEY"]);

                this.FPS91_TY_S_AC_29Q9Y309.SetValue(ds);
            }

            //if (this.CBH01_B7VEND.GetValue().ToString() != "")
            //{
            //    this.CBH01_B7VEND.SetValue("");
            //}
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29Q9Y309_ChildViewCreated 이벤트
        private void FPS91_TY_S_AC_29Q9Y309_ChildViewCreated(object sender, FarPoint.Win.Spread.ChildViewCreatedEventArgs e)
        {
            GeneralCellType tmpCellType = new GeneralCellType();
            tmpCellType.FormatString = "#,##0";

            if (e.SheetView.ParentRelationName == "RELATION_0_1")
            {
                if (e.SheetView.RowCount > 0)
                {
                    e.SheetView.ActiveRow.Locked = true;
                    e.SheetView.Columns[0].Visible = false;
                    e.SheetView.Columns[1].Visible = false;
                    e.SheetView.ColumnHeader.Cells[0, 2].Value = "정리전표번호";
                    e.SheetView.ColumnHeader.Cells[0, 3].Value = "반제정리금액";
                    e.SheetView.Columns[2].Width = 200;
                    e.SheetView.Columns[3].Width = 200;
                    e.SheetView.Columns[3].CellType = tmpCellType;
                }
                else
                {
                    e.SheetView.ColumnHeaderVisible = false;   
                }
            }
        }
        #endregion

        #region Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetValue();
        }
        #endregion

        #region Description : 소계 넣기
        private DataTable UP_SuTotal_dt(DataTable dt, string sSort)
        {
            string sB7SORT = "";

            double dB7AMATTotal = 0;
            double dB8AMBJTotal = 0;
            double dB7AMJNTotal = 0;


            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt.Clone();
            if (sSort == "1")
            {
                //foreach (DataRow dr in dt.Select("", "B7VEND, KEY, B8BJJP, B7CDAC ASC"))
                foreach (DataRow dr in dt.Select("", "B7VEND, KEY, B7CDAC ASC"))
                    table.Rows.Add(dr.ItemArray);
            }
            if (sSort == "2")
            {
               // foreach (DataRow dr in dt.Select("", "B7CDAC, KEY, B8BJJP, B7VEND ASC"))
                  foreach (DataRow dr in dt.Select("", "B7CDAC, KEY, B7VEND ASC"))
                    table.Rows.Add(dr.ItemArray);
            }
            if (sSort == "3")
            {
                //foreach (DataRow dr in dt.Select("", "KEY, B8BJJP, B7VEND, B7CDAC ASC"))
                    foreach (DataRow dr in dt.Select("", "KEY,  B7VEND, B7CDAC ASC"))
                    table.Rows.Add(dr.ItemArray);
            }
            
            int i = 0;
            
            DataRow row;
            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                for (i = 1; i < nNum; i++)
                {
                    if (sSort == "1")
                    {
                        dB7AMATTotal += Convert.ToDouble(table.Rows[i - 1]["B7AMAT"].ToString());
                        dB8AMBJTotal += Convert.ToDouble(table.Rows[i - 1]["B8AMBJ"].ToString());
                        dB7AMJNTotal += Convert.ToDouble(table.Rows[i - 1]["B7AMJN"].ToString());

                        //계정을 입력하면 거래처 소계
                        if (table.Rows[i - 1]["B7VEND"].ToString() != table.Rows[i]["B7VEND"].ToString())
                        {
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["B7VENDNM"] = "[소     계]";

                            sB7SORT = "B7VEND = '" + table.Rows[i - 1]["B7VEND"].ToString() + "' ";

                            // 거래처 합계
                            table.Rows[i]["B7AMJN"] = table.Compute("Sum(B7AMJN)", sB7SORT).ToString();

                            nNum = nNum + 1;

                            i = i + 1;
                        }
                    }
                    if (sSort == "2")
                    {
                        dB7AMATTotal += Convert.ToDouble(table.Rows[i - 1]["B7AMAT"].ToString());
                        dB8AMBJTotal += Convert.ToDouble(table.Rows[i - 1]["B8AMBJ"].ToString());
                        dB7AMJNTotal += Convert.ToDouble(table.Rows[i - 1]["B7AMJN"].ToString());

                        //거래처 입력하면 계정 소계
                        if (table.Rows[i - 1]["B7CDAC"].ToString() != table.Rows[i]["B7CDAC"].ToString())
                        {
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["B7VENDNM"] = "[소     계]";

                            sB7SORT = "B7CDAC = '" + table.Rows[i - 1]["B7CDAC"].ToString() + "' ";

                            // 거래처 합계
                            table.Rows[i]["B7AMJN"] = table.Compute("Sum(B7AMJN)", sB7SORT).ToString();

                            nNum = nNum + 1;

                            i = i + 1;
                        }
                    }


                } //for..end


                dB7AMATTotal += Convert.ToDouble(table.Rows[i-1]["B7AMAT"].ToString());
                dB8AMBJTotal += Convert.ToDouble(table.Rows[i-1]["B8AMBJ"].ToString());
                dB7AMJNTotal += Convert.ToDouble(table.Rows[i-1]["B7AMJN"].ToString());


                if (sSort == "1")
                {                    

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["B7VENDNM"] = "[소     계]";


                    sB7SORT = "B7VEND = '" + table.Rows[i - 1]["B7VEND"].ToString() + "' ";

                    table.Rows[i]["B7AMJN"] = table.Compute("Sum(B7AMJN)", sB7SORT).ToString();

                    i = i + 1;

                    //총 합계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["B7VENDNM"] = "[총     계]";

                    table.Rows[i]["B7AMAT"] = dB7AMATTotal.ToString();
                    table.Rows[i]["B8AMBJ"] = dB8AMBJTotal.ToString();
                    table.Rows[i]["B7AMJN"] = dB7AMJNTotal.ToString();

                }
                if (sSort == "2")
                {                   
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["B7VENDNM"] = "[소     계]";

                    sB7SORT = "B7CDAC = '" + table.Rows[i - 1]["B7CDAC"].ToString() + "' ";

                    table.Rows[i]["B7AMJN"] = table.Compute("Sum(B7AMJN)", sB7SORT).ToString();

                    i = i + 1;

                    //총 합계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["B7VENDNM"] = "[총     계]";

                    table.Rows[i]["B7AMAT"] = dB7AMATTotal.ToString();
                    table.Rows[i]["B8AMBJ"] = dB8AMBJTotal.ToString();
                    table.Rows[i]["B7AMJN"] = dB7AMJNTotal.ToString();

                }

                if (sSort == "3")
                {                   

                    //총 합계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["B7VENDNM"] = "[총     계]";

                    table.Rows[i]["B7AMAT"] = table.Compute("Sum(B7AMAT)", "").ToString();
                    table.Rows[i]["B8AMBJ"] = table.Compute("Sum(B8AMBJ)", "").ToString();
                    table.Rows[i]["B7AMJN"] = table.Compute("Sum(B7AMJN)", "").ToString();
                }

            }

            return table;
        }
        #endregion

        #region Desription : 출력 처리
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            DataTable dt = this.FPS91_TY_S_AC_2CSAZ425.GetDataSourceInclude(TSpread.TActionType.All, "B7VEND", "B7VENDNM", "B7CDAC", "B7CDACNM", "B7AMAT", "B8AMBJ", "B7AMJN", "B7WCJP", "B8BJJP", "B2RKAC");

            //string sSort = "";

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2CSAY422", this.DTP01_GEDDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B7CDAC.GetValue(), this.CBH01_B7VEND.GetValue(), this.CBH01_B2DPMK.GetValue());

            //if (this.CBH01_B7CDAC.GetValue().ToString() != "" && this.CBH01_B7VEND.GetValue().ToString() == "")
            //{
            //    sSort = "1";  //계정코드만 입력 경우
            //}
            //else if (this.CBH01_B7CDAC.GetValue().ToString() == "" && this.CBH01_B7VEND.GetValue().ToString() != "")
            //{
            //    sSort = "2"; //거래처만 입력 경우
            //}
            //else
            //{
            //    sSort = "3";  //둘다 입력하거나 둘다 입력 안한경우
            //}

            //DataTable dt = UP_SuTotal_dt(this.DbConnector.ExecuteDataTable(), sSort);

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1") // 상세 일때만 출력함
            {
                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACFP006R();

                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                    return;
                }
            }
        } 
        #endregion

        #region Description : CBO01_INQOPTION_SelectedIndexChanged
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_INQOPTION.GetValue().ToString() == "1") // 상세 일때만 출력함
            {
                this.BTN61_PRT.Visible = true;
            }
            else
            {
                this.BTN61_PRT.Visible = false;
            }

        } 
        #endregion

        #region Description : CBO01_INQOPTION_SelectedIndexChanged
        private void FPS91_TY_S_AC_2CSAZ425_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string sB2DPMK = "";
            string sB2DTMK = "";
            string sB2NOSQ = "";

            if (e.Column.ToString() == "10") // 설정전표 
            {
                 sB2DPMK = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B7WCJP").ToString().Substring(0, 6);
                 sB2DTMK = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B7WCJP").ToString().Substring(7, 8);
                 sB2NOSQ = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B7WCJP").ToString().Substring(16, 3);

                if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

            if (e.Column.ToString() == "12") // 정리 전표
            {
                 sB2DPMK = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B8BJJP").ToString().Substring(0, 6);
                 sB2DTMK = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B8BJJP").ToString().Substring(7, 8);
                 sB2NOSQ = this.FPS91_TY_S_AC_2CSAZ425.GetValue("B8BJJP").ToString().Substring(16, 3);

                if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

        }
        #endregion


    }
}
