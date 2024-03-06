using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 충당금 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.11 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_463DW669 : 충당금 명세서 조회
    ///  TY_P_AC_46BD8724 : 충당금 명세서 집계 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46BDB726 : 충당금 명세서 조회
    ///  TY_S_AC_46BD1725 : 충당금 명세서 집계 조회
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  FXMYYMM : 상각년월
    /// </summary>
    public partial class TYACHF013S : TYBase
    {
        #region Description : 페이지 로드
        public TYACHF013S()
        {
            InitializeComponent();
        }

        private void TYACHF013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.TXT01_FXMYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            // 내역
            if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "01")
            {
                this.FPS91_TY_S_AC_46BDB726.Visible = true;
                this.FPS91_TY_S_AC_46BD1725.Visible = false;
                this.FPS91_TY_S_AC_8B8AO112.Visible = false;
            }
            else if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "02" || this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "03")
            {
                this.FPS91_TY_S_AC_46BDB726.Visible = false;
                this.FPS91_TY_S_AC_46BD1725.Visible = true;
                this.FPS91_TY_S_AC_8B8AO112.Visible = false;
            }
            else
            {
                this.FPS91_TY_S_AC_46BDB726.Visible = false;
                this.FPS91_TY_S_AC_46BD1725.Visible = false;
                this.FPS91_TY_S_AC_8B8AO112.Visible = true;
            }

            this.SetStartingFocus(this.TXT01_FXMYYMM);
            BTN61_PRT.Hide();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;

            if (this.TXT01_FXMYYMM.GetValue().ToString().Substring(4, 2) == "01")
            {
                sSDATE = this.TXT01_FXMYYMM.GetValue().ToString().Substring(0, 4) + "00";
                sEDATE = this.TXT01_FXMYYMM.GetValue().ToString().Substring(0, 4) + "00";
            }
            else
            {
                sSDATE = this.TXT01_FXMYYMM.GetValue().ToString().Substring(0, 4) + "01";
                int sDD = Convert.ToInt16(this.TXT01_FXMYYMM.GetValue().ToString().Substring(4, 2)) - 1;
                sEDATE = this.TXT01_FXMYYMM.GetValue().ToString().Substring(0, 4) + Set_Fill2(sDD.ToString());
            }

            string s조회년월 = this.TXT01_FXMYYMM.GetValue().ToString().Trim();
            string s자산처리년도 = this.TXT01_FXMYYMM.GetValue().ToString().Trim().Substring(0, 4);


            string sBUSEO = string.Empty;

            switch (this.CBO01_ASSEBUSE.GetValue().ToString().Trim())
            {
                case "ALL":
                    sBUSEO = ""; //전체
                    break;
                case "T":
                    sBUSEO = "T"; //UTT
                    break;
                case "S":
                    sBUSEO = "S"; //SILO
                    break;
                case "B":
                    sBUSEO = "B"; //무역
                    break;
                case "C":
                    sBUSEO = "C"; //기획공통
                    break;
                case "A":
                    sBUSEO = "A"; //관리
                    break;
            }


            DataTable dt = new DataTable();

            // 내역
            if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "01" || this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "02")
            {
                string sProcedID = string.Empty;

                this.FPS91_TY_S_AC_46BDB726.Visible = true;
                this.FPS91_TY_S_AC_46BD1725.Visible = false;
                this.FPS91_TY_S_AC_8B8AO112.Visible = false;

                this.FPS91_TY_S_AC_46BDB726.Initialize();

                //if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "01")
                //{ sProcedID = "TY_P_AC_463DW669"; }
                //else 
                //{ sProcedID = "TY_P_AC_474DE940"; }

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach(sProcedID, s자산처리년도, s자산처리년도,
                //                                   s조회년월, s조회년월, s조회년월, s조회년월, s조회년월,sBUSEO ,
                //                                   s자산처리년도, s자산처리년도,sSDATE, sEDATE, sBUSEO);


                if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "01")
                { sProcedID = "TY_P_AC_63NGH647"; }
                else
                { sProcedID = "TY_P_AC_63O9D649"; }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProcedID, s조회년월, this.CBH01_FXLMASCODE.GetValue().ToString(), TXT01_SDATE.GetValue(), TXT01_FXNAME.GetValue(), sBUSEO);

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    dt = UP_DataSetRowHap(this.DbConnector.ExecuteDataTable());
                }

            }
            else if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "03") // 집계
            {
                this.FPS91_TY_S_AC_46BDB726.Visible = false;
                this.FPS91_TY_S_AC_46BD1725.Visible = true;
                this.FPS91_TY_S_AC_8B8AO112.Visible = false;

                this.FPS91_TY_S_AC_46BD1725.Initialize();
                this.DbConnector.CommandClear();
                ////this.DbConnector.Attach("TY_P_AC_46BD8724", s자산처리년도, s자산처리년도, s자산처리년도,
                ////                                            s조회년월, s조회년월, s조회년월, s조회년월, sBUSEO ,
                ////                                            s자산처리년도, s자산처리년도, sSDATE, sEDATE, sBUSEO, sSDATE, s조회년월, sBUSEO);

                this.DbConnector.Attach("TY_P_AC_63OHG651", s조회년월, this.CBH01_FXLMASCODE.GetValue().ToString(), TXT01_SDATE.GetValue(), TXT01_FXNAME.GetValue(), sBUSEO);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.FPS91_TY_S_AC_46BDB726.Visible = false;
                this.FPS91_TY_S_AC_46BD1725.Visible = false;
                this.FPS91_TY_S_AC_8B8AO112.Visible = true;

                this.FPS91_TY_S_AC_8B8AO112.Initialize();
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_8B8AN110", s조회년월, this.CBH01_FXLMASCODE.GetValue().ToString(), TXT01_SDATE.GetValue(), TXT01_FXNAME.GetValue(), sBUSEO);
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

            // 내역
            if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "01" || this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "02")
            {
                this.FPS91_TY_S_AC_46BDB726.SetValue(dt);

                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[건물 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[구축물 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[기계장치 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[중기 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[차량운반구 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[공구와기구 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[집기비품 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_46BDB726, "FXSNAME", "[합     계]", SumRowType.Total);
            }
            else if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "03")
            {
                this.FPS91_TY_S_AC_46BD1725.SetValue(dt);

                if (this.FPS91_TY_S_AC_46BD1725.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_46BD1725, "ASTGUBN", "합 계", SumRowType.Total);
                }
            }
            else
            {
                this.FPS91_TY_S_AC_8B8AO112.SetValue(UP_DataSetBuseoRowHap(dt));

                if (this.FPS91_TY_S_AC_8B8AO112.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_8B8AO112, "FXSAUPNM", "[부서 소계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_8B8AO112, "FXSAUPNM", "[합     계]", SumRowType.Total);
                }
            }

            this.SetFocus(this.TXT01_FXMYYMM);
        }
        #endregion

        #region Description : 조회번턴 처리
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            string sSDATE = string.Empty;
            string sEDATE = string.Empty;


            if (this.TXT01_FXMYYMM.GetValue().ToString().Trim().Length != 6)
            {
                this.SetFocus(this.TXT01_FXMYYMM);
                this.ShowCustomMessage("년월을 정확하게 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if ( Convert.ToInt16(this.TXT01_FXMYYMM.GetValue().ToString().Trim().Substring(4,2)) > 12)
            {
                this.SetFocus(this.TXT01_FXMYYMM);
                this.ShowCustomMessage("년월을 정확하게 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(this.TXT01_FXMYYMM.GetValue().ToString().Trim().Substring(4, 2)) < 0)
            {
                this.SetFocus(this.TXT01_FXMYYMM);
                this.ShowCustomMessage("년월을 정확하게 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37T27260", this.TXT01_FXMYYMM.GetValue().ToString().Trim());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("해당월 상각자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.TXT01_FXMYYMM);
                e.Successed = false;
                return;
            }

        } 
        #endregion


        #region Description : 자산 소계 함수
        private DataTable UP_DataSetRowHap(DataTable dt )
        {
            string sFilter = string.Empty;

            DataTable table = new DataTable();
            table = dt;
            DataRow row;
            int i = 0;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["ASTGUBN"].ToString() != table.Rows[i]["ASTGUBN"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    sFilter = "ASTGUBN = '" + table.Rows[i - 1]["ASTGUBN"].ToString() + "' ";

                    table.Rows[i]["FXSNAME"] = "["+UP_Get_ASSETNAME(table.Rows[i - 1]["ASTGUBN"].ToString().Substring(0,1)) + " 소계]";

                    //취득가액
                    table.Rows[i]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
                    //전기이월액
                    table.Rows[i]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
                    //당기증가
                    table.Rows[i]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
                    //당기감소
                    table.Rows[i]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
                    //기말잔액
                    table.Rows[i]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
                    //이월금액
                    table.Rows[i]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
                    //처분액
                    table.Rows[i]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
                    //당기상각액
                    table.Rows[i]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
                    //상각금액계
                    table.Rows[i]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
                    //미상각잔액
                    table.Rows[i]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());


                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            sFilter = "ASTGUBN = '" + table.Rows[i - 1]["ASTGUBN"].ToString() + "' ";

            table.Rows[i]["FXSNAME"] = "[" + UP_Get_ASSETNAME(table.Rows[i - 1]["ASTGUBN"].ToString().Substring(0, 1)) + " 소계]";

            //취득가액
            table.Rows[i]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
            //전기이월액
            table.Rows[i]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
            //당기증가
            table.Rows[i]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
            //당기감소
            table.Rows[i]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
            //기말잔액
            table.Rows[i]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
            //이월금액
            table.Rows[i]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
            //처분액
            table.Rows[i]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
            //당기상각액
            table.Rows[i]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
            //상각금액계
            table.Rows[i]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
            //미상각잔액
            table.Rows[i]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());


            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);           

            // 합 계 이름 넣기
            sFilter = "ASTGUBN <> '' ";

            table.Rows[i + 1]["FXSNAME"] = "[합     계]";

            //취득가액
            table.Rows[i + 1]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
            //전기이월액
            table.Rows[i + 1]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
            //당기증가
            table.Rows[i + 1]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
            //당기감소
            table.Rows[i + 1]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
            //기말잔액
            table.Rows[i + 1]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
            //이월금액
            table.Rows[i + 1]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
            //처분액
            table.Rows[i + 1]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
            //당기상각액
            table.Rows[i + 1]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
            //상각금액계
            table.Rows[i + 1]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
            //미상각잔액
            table.Rows[i + 1]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());
            

            return table;
        }
        #endregion

        #region Description : 부서 소계 함수
        private DataTable UP_DataSetBuseoRowHap(DataTable dt)
        {
            string sFilter = string.Empty;

            DataTable table = new DataTable();
            table = dt;
            DataRow row;
            int i = 0;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["FXSAUP"].ToString() != table.Rows[i]["FXSAUP"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    sFilter = "FXSAUP = '" + table.Rows[i - 1]["FXSAUP"].ToString() + "' ";

                    table.Rows[i]["FXSAUPNM"] = "[부서 소계]";
                    //취득가액
                    table.Rows[i]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
                    //전기이월액
                    table.Rows[i]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
                    //당기증가
                    table.Rows[i]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
                    //당기감소
                    table.Rows[i]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
                    //기말잔액
                    table.Rows[i]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
                    //이월금액
                    table.Rows[i]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
                    //처분액
                    table.Rows[i]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
                    //당기상각액
                    table.Rows[i]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
                    //상각금액계
                    table.Rows[i]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
                    //미상각잔액
                    table.Rows[i]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());


                    nNum = nNum + 1;
                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            sFilter = "FXSAUP = '" + table.Rows[i - 1]["FXSAUP"].ToString() + "' ";

            table.Rows[i]["FXSAUPNM"] = "[부서 소계]";
            //취득가액
            table.Rows[i]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
            //전기이월액
            table.Rows[i]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
            //당기증가
            table.Rows[i]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
            //당기감소
            table.Rows[i]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
            //기말잔액
            table.Rows[i]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
            //이월금액
            table.Rows[i]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
            //처분액
            table.Rows[i]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
            //당기상각액
            table.Rows[i]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
            //상각금액계
            table.Rows[i]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
            //미상각잔액
            table.Rows[i]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());


            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            sFilter = "FXSAUP <> '' ";

            // 합 계 이름 넣기
            table.Rows[i + 1]["FXSAUPNM"] = "[합     계]";

            //취득가액
            table.Rows[i+1]["FXMGETAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMGETAMOUNT)", sFilter).ToString());
            //전기이월액
            table.Rows[i + 1]["FXMOVERAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMOVERAMOUNT)", sFilter).ToString());
            //당기증가
            table.Rows[i + 1]["FXMINCAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMINCAMOUNT)", sFilter).ToString());
            //당기감소
            table.Rows[i + 1]["FXMDECAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMDECAMOUNT)", sFilter).ToString());
            //기말잔액
            table.Rows[i + 1]["FXMAMMALAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMAMMALAMOUNT)", sFilter).ToString());
            //이월금액
            table.Rows[i + 1]["FXMJUNKIREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMJUNKIREPAMOUNT)", sFilter).ToString());
            //처분액
            table.Rows[i + 1]["SUBSUM"] = Convert.ToDouble(table.Compute("Sum(SUBSUM)", sFilter).ToString());
            //당기상각액
            table.Rows[i + 1]["FXMREPAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNT)", sFilter).ToString());
            //상각금액계
            table.Rows[i + 1]["FXMREPAMOUNTSUM"] = Convert.ToDouble(table.Compute("Sum(FXMREPAMOUNTSUM)", sFilter).ToString());
            //미상각잔액
            table.Rows[i + 1]["FXMREPJANAMOUNT"] = Convert.ToDouble(table.Compute("Sum(FXMREPJANAMOUNT)", sFilter).ToString());


            return table;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {   
            string sBUSEO = string.Empty;

            switch (this.CBO01_ASSEBUSE.GetValue().ToString().Trim())
            {
                case "A":
                    sBUSEO = ""; //전체
                    break;
                case "T":
                    sBUSEO = "T"; //UTT
                    break;
                case "S":
                    sBUSEO = "S"; //SILO
                    break;
            }
            
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_644KJ686", this.TXT01_FXMYYMM.GetValue().ToString().Trim(), this.CBH01_FXLMASCODE.GetValue().ToString(), sBUSEO);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACHF013R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : CBO01_QUIRGUBN_SelectedValueChanged 이벤트
        private void CBO01_QUIRGUBN_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.CBO01_QUIRGUBN.GetValue().ToString().Trim() == "02")
            {
                BTN61_PRT.Show();
            }
            else
            {
                BTN61_PRT.Hide();
            }
        }
        #endregion

        #region Description : UP_Get_ASSETNAME 자산분류명 가져오기 
        private string UP_Get_ASSETNAME(string sCode)
        {
            string sValueName = string.Empty;

            switch (sCode)
            {
                case "1":
                    sValueName = "토지";
                    break;
                case "2":
                    sValueName = "건물";
                    break;
                case "3":
                    sValueName = "구축물";
                    break;
                case "4":
                    sValueName = "기계장치";
                    break;
                case "5":
                    sValueName = "중기";
                    break;
                case "6":
                    sValueName = "차량운반구";
                    break;
                case "7":
                    sValueName = "공구와기구";
                    break;
                case "8":
                    sValueName = "집기비품";
                    break;
            }


            return sValueName;
            
        }
        #endregion

    }
}