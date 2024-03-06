using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인 급여 통합 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.23 10:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CNJ0949 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_P_HR_4CNJ2950 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CNJE951 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_S_HR_4CNJK952 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQ_FXM : 조회
    ///  KBBUSEO : 부서
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRPY008S : TYBase
    {
        string fsGUBN = string.Empty;

        #region Description : 페이지 로드
        public TYHRPY008S()
        {
            InitializeComponent();

            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
        }

        private void TYHRPY008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CNJE951, "BTNPRINT");

            (this.FPS91_TY_S_HR_4CNJE951.Sheets[0].Columns[16].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;

            UP_Spread_Title();

            this.SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회(급여지급) 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4CNJE951.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CNJ0949",
                                    this.DTP01_GSTYYMM.GetValue().ToString(),
                                    this.DTP01_GEDYYMM.GetValue().ToString(),
                                    this.CBH01_PAYGUBN.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4CNJE951.SetValue(dt);
            this.FPS91_TY_S_HR_4CNJK952.Initialize();

            for (int i = 0; i < this.FPS91_TY_S_HR_4CNJE951.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_4CNJE951.GetValue(i, "PAYGUBN").ToString() != "M1"
                    && this.FPS91_TY_S_HR_4CNJE951.GetValue(i, "PAYGUBN").ToString() == "S1"
                    && Convert.ToInt32(this.FPS91_TY_S_HR_4CNJE951.GetValue(i, "PAYYYMM").ToString()) >= 201501)
                {
                     this.FPS91_TY_S_HR_4CNJE951_Sheet1.Cells[i, 16].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
                else
                {
                    if (Convert.ToInt32(this.FPS91_TY_S_HR_4CNJE951.GetValue(i, "PAYCNT").ToString()) <= 0)
                    {
                        this.FPS91_TY_S_HR_4CNJE951_Sheet1.Cells[i, 16].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }

            fsGUBN = "N";
        }
        #endregion

        #region Description : 급여지급 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4CNJE951_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = UP_Select(this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYGUBN").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYYYMM").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYJIDATE").ToString(),
                                          "",
                                          "",
                                          "");

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_4CNJK952.SetValue(UP_DatatableChenge(dt));
                fsGUBN = "Y";
                for (int i = 0; i < this.FPS91_TY_S_HR_4CNJK952.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_4CNJK952.GetValue(i, "PMGUBN").ToString() == "합 계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_HR_4CNJK952.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                    
                }
            }
            else
            {
                this.FPS91_TY_S_HR_4CNJK952.SetValue(dt);
                fsGUBN = "N";
            }
        }
        #endregion

        #region Description : 조회(급여결과) 버튼 이벤트
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            if (fsGUBN == "Y")
            {

                DataTable dt =  UP_Select(this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYGUBN").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYYYMM").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYJIDATE").ToString(),
                                          this.CBH01_KBSABUN.GetValue().ToString(),
                                          this.CBH01_KBJKCD.GetValue().ToString(),
                                          this.CBH01_KBBUSEO.GetValue().ToString());

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_HR_4CNJK952.SetValue(UP_DatatableChenge(dt));

                    if (this.FPS91_TY_S_HR_4CNJK952.CurrentRowCount > 0)
                    {
                        for (int i = 0; i < this.FPS91_TY_S_HR_4CNJK952.ActiveSheet.RowCount; i++)
                        {
                            if (this.FPS91_TY_S_HR_4CNJK952.GetValue(i, "PMGUBN").ToString() == "합 계")
                            {
                                // 특정 ROW 색깔 입히기
                                this.FPS91_TY_S_HR_4CNJK952.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                            }
                        }
                    }
                }
                else
                {
                    this.FPS91_TY_S_HR_4CNJK952.SetValue(dt);
                }
                
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            // 급여 마스타 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53BFM649", dt);
            this.DbConnector.ExecuteNonQueryList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 급여 내역 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_61QA4484", dt.Rows[i]["PMGUBN"].ToString(),
                                                            dt.Rows[i]["PMYYMM"].ToString(),
                                                            dt.Rows[i]["PMJIDATE"].ToString(),
                                                            dt.Rows[i]["PMSABUN"].ToString()
                                                            );
                this.DbConnector.ExecuteNonQueryList();
            }

            DataTable dt2 = UP_Select(this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYGUBN").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYYYMM").ToString(),
                                          this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYJIDATE").ToString(),
                                          this.CBH01_KBSABUN.GetValue().ToString(),
                                          this.CBH01_KBJKCD.GetValue().ToString(),
                                          this.CBH01_KBBUSEO.GetValue().ToString());

            if (dt2.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_4CNJK952.SetValue(UP_DatatableChenge(dt2));
            }
            else
            {
                this.FPS91_TY_S_HR_4CNJK952.SetValue(dt2);
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CNJK952.GetDataSourceInclude(TSpread.TActionType.Remove, "PMGUBN", "PMYYMM", "PMJIDATE", "PMSABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //펌뱅킹 이체 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53K94751", dt.Rows[0]["PMYYMM"].ToString(), dt.Rows[0]["PMGUBN"].ToString(), dt.Rows[0]["PMJIDATE"].ToString());
            DataTable dtBank = this.DbConnector.ExecuteDataTable();

            if (dtBank.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_HR_4COHY978");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : 급여결과 마스타 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4CNJK952_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_4CNJK952.GetValue("PMGUBN").ToString() == "합 계")
            {
                ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                if ((new TYHRPY008I(this.FPS91_TY_S_HR_4CNJK952.GetValue("PMGUBN").ToString(), this.FPS91_TY_S_HR_4CNJK952.GetValue("PMYYMM").ToString()
                                    , this.FPS91_TY_S_HR_4CNJK952.GetValue("PMJIDATE").ToString(), this.FPS91_TY_S_HR_4CNJK952.GetValue("PMSABUN").ToString(),"EDIT"
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_FXM_Click(null, null);
            }
        }
        #endregion

        #region Description : 급여결과 마스타 그리드 합계 추가
        private DataTable UP_DatatableChenge(DataTable dt)
        {
            DataTable rtnDt = dt;

            double dPMPAYTOTAL = 0;     //지급합계
            double dPMTAXTOTAL = 0;     //공제합계
            double dPMAFTERTOTAL = 0;   //차인지급액
            double dPMINCOMETAX = 0;    //소득세
            double dPMRESTAX = 0;       //주민세
            double dPMNATIONAMT = 0;    //국민연금
            double dPMHEALTHAMT = 0;    //건강보험
            double dPMEMPLOYAMT = 0;    //고용보험
            double dPMLTERMAMT = 0;     //장기요양보험
            double dPMHFAMOUNT = 0;     //심야OT
            double dPMOTAMOUNT = 0;     //연장OT
            double dPMNTAMOUNT = 0;     //야간OT
            double dPMHTAMOUNT = 0;     //휴일OT
            double dPMWTAMOUNT = 0;     //명절심야
            double dPMGJAMOUNT = 0;     //고정OT

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dPMPAYTOTAL += Convert.ToDouble(dt.Rows[i]["PMPAYTOTAL"]);
                dPMTAXTOTAL += Convert.ToDouble(dt.Rows[i]["PMTAXTOTAL"]);
                dPMAFTERTOTAL += Convert.ToDouble(dt.Rows[i]["PMAFTERTOTAL"]);
                dPMINCOMETAX += Convert.ToDouble(dt.Rows[i]["PMINCOMETAX"]);
                dPMRESTAX += Convert.ToDouble(dt.Rows[i]["PMRESTAX"]);
                dPMNATIONAMT += Convert.ToDouble(dt.Rows[i]["PMNATIONAMT"]);
                dPMHEALTHAMT += Convert.ToDouble(dt.Rows[i]["PMHEALTHAMT"]);
                dPMEMPLOYAMT += Convert.ToDouble(dt.Rows[i]["PMEMPLOYAMT"]);
                dPMLTERMAMT += Convert.ToDouble(dt.Rows[i]["PMLTERMAMT"]);
                dPMHFAMOUNT += Convert.ToDouble(dt.Rows[i]["PMHFAMOUNT"]);
                dPMOTAMOUNT += Convert.ToDouble(dt.Rows[i]["PMOTAMOUNT"]);
                dPMNTAMOUNT += Convert.ToDouble(dt.Rows[i]["PMNTAMOUNT"]);
                dPMHTAMOUNT += Convert.ToDouble(dt.Rows[i]["PMHTAMOUNT"]);
                dPMWTAMOUNT += Convert.ToDouble(dt.Rows[i]["PMWTAMOUNT"]);
                dPMGJAMOUNT += Convert.ToDouble(dt.Rows[i]["PMGJAMOUNT"]);
            }
            
            DataRow row;

            row = rtnDt.NewRow();

            row["PMGUBN"] = "합 계";
            row["PMGUBNNM"] = DBNull.Value;
            row["PMYYMM"] = DBNull.Value;
            row["PMJIDATE"] = DBNull.Value;
            row["PMSABUN"] = DBNull.Value;
            row["KBHANGL"] = DBNull.Value;
            row["PMBUSEO"] = DBNull.Value;
            row["PMBUSEONM"] = DBNull.Value;
            row["PMJJCD"] = DBNull.Value;
            row["PMJJCDNM"] = DBNull.Value;
            row["PMJKCD"] = DBNull.Value;
            row["PMJKCDNM"] = DBNull.Value;
            row["PMHOBN"] = DBNull.Value;

            row["PMPAYTOTAL"] = string.Format("{0:#,###}", dPMPAYTOTAL.ToString());
            row["PMTAXTOTAL"] = string.Format("{0:#,###}", dPMTAXTOTAL.ToString());
            row["PMAFTERTOTAL"] = string.Format("{0:#,###}", dPMAFTERTOTAL.ToString());

            row["PMINCOMETAX"] = string.Format("{0:#,###}", dPMINCOMETAX.ToString());
            row["PMRESTAX"] = string.Format("{0:#,###}", dPMRESTAX.ToString());
            row["PMNATIONAMT"] = string.Format("{0:#,###}", dPMNATIONAMT.ToString());
            row["PMHEALTHAMT"] = string.Format("{0:#,###}", dPMHEALTHAMT.ToString());
            row["PMEMPLOYAMT"] = string.Format("{0:#,###}", dPMEMPLOYAMT.ToString());
            row["PMLTERMAMT"] = string.Format("{0:#,###}", dPMLTERMAMT.ToString());
            row["PMGOEPYO"] = DBNull.Value;
            row["PMTAXAMT"] = DBNull.Value;
            row["PMEARINCOME"] = DBNull.Value;

            row["PMORDPAY"] = DBNull.Value;
            row["PMORDOTPAY"] = DBNull.Value;

            row["PMHFOTTIME"] = DBNull.Value;
            row["PMWKOTTIME"] = DBNull.Value;
            row["PMNTOTTIME"] = DBNull.Value;
            row["PMHTOTTIME"] = DBNull.Value;
            row["PMGJOTTIME"] = DBNull.Value;

            row["PMHFAMOUNT"] = string.Format("{0:#,###}", dPMHFAMOUNT.ToString());
            row["PMOTAMOUNT"] = string.Format("{0:#,###}", dPMOTAMOUNT.ToString());
            row["PMNTAMOUNT"] = string.Format("{0:#,###}", dPMNTAMOUNT.ToString());
            row["PMHTAMOUNT"] = string.Format("{0:#,###}", dPMHTAMOUNT.ToString());
            row["PMWTAMOUNT"] = string.Format("{0:#,###}", dPMWTAMOUNT.ToString());
            row["PMGJAMOUNT"] = string.Format("{0:#,###}", dPMGJAMOUNT.ToString());

            row["PMPAYRATE"] = DBNull.Value;
            row["PMWKDATE"] = DBNull.Value;
            row["PMWKDAY"] = DBNull.Value;
            row["PMEXCD"] = DBNull.Value;
            row["PMEXMEMO"] = DBNull.Value;
            row["PMEXDATE"] = DBNull.Value;
            row["PMEXRATE"] = DBNull.Value;
            row["PMWKDAYS"] = DBNull.Value;
            row["PMWKTIMES"] = DBNull.Value;

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 13);
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 3);
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 9);
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.AddColumnHeaderSpanCell(0, 25, 1, 14);
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.AddColumnHeaderSpanCell(0, 39, 1, 8);

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 0].Value = "기  본  사  항";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 13].Value = "지  급";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 16].Value = "공  제";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 25].Value = "연  장";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 39].Value = "근로 및 예외사항";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 0].Value = "급여구분";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 1].Value = "급여명";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 2].Value = "급여년월";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 3].Value = "지급일자";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 4].Value = "사번";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 5].Value = "이름";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 6].Value = "부서";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 7].Value = "부서명";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 8].Value = "직위";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 9].Value = "직위명";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 10].Value = "직급";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 11].Value = "직급명";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 12].Value = "호봉";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 13].Value = "지급합계";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 14].Value = "공제합계";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 15].Value = "차인지급액";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 16].Value = "소득세";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 17].Value = "주민세";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 18].Value = "국민연금";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 19].Value = "건강보험";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 20].Value = "고용보험";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 21].Value = "장기요양보험";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 22].Value = "과표액";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 23].Value = "산출세액";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 24].Value = "근로공제";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 25].Value = "통상임금";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 26].Value = "OT용상여금";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 27].Value = "심야시간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 28].Value = "연장시간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 29].Value = "야간시간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 30].Value = "휴일시간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 31].Value = "명절심야";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 32].Value = "고정시간";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 33].Value = "심야금액";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 34].Value = "연장금액";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 35].Value = "연장심야";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 36].Value = "휴일심야";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 37].Value = "명절심야";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 38].Value = "고정금액";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 39].Value = "지급율";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 40].Value = "근무일자";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 41].Value = "출근일수";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 42].Value = "근로시간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 43].Value = "예외코드";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 44].Value = "예외사유";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 45].Value = "예외기간";
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[1, 46].Value = "예외지급율";

            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 25].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_4CNJK952_Sheet1.ColumnHeader.Cells[0, 39].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : UP_Select: 조회 이벤트 함수
        private DataTable UP_Select(string PAYGUBN, string PAYYYMM, string PAYJIDATE, string KBSABUN, string KBJKCD, string KBBUSEO)
        {
            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CNJ2950",
                                    PAYGUBN,
                                    PAYYYMM,
                                    PAYJIDATE,
                                    KBSABUN,
                                    KBJKCD,
                                    KBBUSEO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region Description : 그리드 버튼 클릭 이벤트
        private void FPS91_TY_S_HR_4CNJE951_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "16")
            {
                if (this.OpenModalPopup(new TYHRPY12C1(this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYYYMM").ToString(), this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYGUBN").ToString(), this.FPS91_TY_S_HR_4CNJE951.GetValue("PAYJIDATE").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
