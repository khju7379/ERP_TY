using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화주 화물별 DRUM 재고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.22 09:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66M9D343 : 화주 화물별 DRUM 재고현황 조회(마스타)
    ///  TY_P_UT_66M9G344 : 화주 화물별 DRUM 재고현황 조회(디테일)
    ///  TY_P_UT_66M9G345 : 화주 화물별 DRUM 재고현황 전월 재고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66M9I346 : 화주 화물별 DRUM 재고현황 조회(마스타)
    ///  TY_S_UT_66M9J347 : 화주 화물별 DRUM 재고현황 조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTIN022S : TYBase
    {
        public TYUTIN022S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYUTIN022S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region : Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_66M9I346.Initialize();
            this.FPS91_TY_S_UT_66M9J347.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66M9D343", this.DTP01_SDATE.GetString(),
                                                        this.DTP01_EDATE.GetString(),
                                                        this.CBH01_DPHWAJU.GetValue().ToString(),
                                                        this.CBH01_DPHWAMUL.GetValue().ToString(),
                                                        this.TXT01_DPCHTANK.GetValue().ToString().Trim());

            this.FPS91_TY_S_UT_66M9I346.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_UT_66M9I346_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_UT_66M9J347.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66M9G344", this.DTP01_SDATE.GetString(),
                                                        this.DTP01_EDATE.GetString(),
                                                        this.FPS91_TY_S_UT_66M9I346.GetValue("DPACTHWAJU").ToString(),
                                                        this.FPS91_TY_S_UT_66M9I346.GetValue("DPHWAMUL").ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();
            
            this.FPS91_TY_S_UT_66M9J347.SetValue(UP_ChangeDatatable(dt));

        }
        #endregion

        #region Description : 데이터 테이블 변경
        private DataTable UP_ChangeDatatable(DataTable dt)
        {
            string sSql = string.Empty;
            string sCHECK = string.Empty;
            string sIPHANG = string.Empty;
            string sCDDESC1 = string.Empty;
            string sVNSANGHO = string.Empty;
            string sBLNO = string.Empty;
            string sMSNSEQ = string.Empty;
            string sHSNSEQ = string.Empty;
            string sJUNG = string.Empty;
            double dDPDRQTY = 0;
            double dDCDRQTY = 0;
            double dJEGO = 0;

            DataTable rtnDt = new DataTable();

            rtnDt.Columns.Add("DPACTHWAJU", typeof(System.String));
            rtnDt.Columns.Add("DPHWAMUL", typeof(System.String));
            rtnDt.Columns.Add("DPIPHANG", typeof(System.String));
            rtnDt.Columns.Add("DPBONSUN", typeof(System.String));
            rtnDt.Columns.Add("CDDESC1", typeof(System.String));
            rtnDt.Columns.Add("DPHWAJU", typeof(System.String));
            rtnDt.Columns.Add("VNSANGHO", typeof(System.String));
            rtnDt.Columns.Add("DPBLNO", typeof(System.String));
            rtnDt.Columns.Add("DPMSNSEQ", typeof(System.String));
            rtnDt.Columns.Add("DPHSNSEQ", typeof(System.String));
            rtnDt.Columns.Add("DPJUNG", typeof(System.String));
            rtnDt.Columns.Add("DPDRQTY", typeof(System.String));
            rtnDt.Columns.Add("CHDATE", typeof(System.String));
            rtnDt.Columns.Add("CHDRQTY", typeof(System.String));
            rtnDt.Columns.Add("JEGO", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sCHECK = "*";
                // 포장수량
                dDPDRQTY = double.Parse(Get_Numeric(dt.Rows[i]["DPDRQTY"].ToString()));
                // 재고수량
                dJEGO = dDPDRQTY;

                // 전월 재고 출고량을 가져옴
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66M9G345", dt.Rows[i]["DPACTHWAJU"].ToString(),
                                                            dt.Rows[i]["DPHWAMUL"].ToString(),
                                                            dt.Rows[i]["DPIPHANG"].ToString(),
                                                            dt.Rows[i]["DPBONSUN"].ToString(),
                                                            dt.Rows[i]["DPHWAJU"].ToString(),
                                                            dt.Rows[i]["DPBLNO"].ToString(),
                                                            dt.Rows[i]["DPMSNSEQ"].ToString(),
                                                            dt.Rows[i]["DPHSNSEQ"].ToString(),
                                                            dt.Rows[i]["DPJUNG"].ToString()
                                                            );

                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                for (int j = 0;  j < dt1.Rows.Count; j++)
                {
                    // 출고수량
                    dDCDRQTY = double.Parse(Get_Numeric(dt1.Rows[j]["DCDRQTY"].ToString()));
                    // 재고수량
                    dJEGO = dJEGO - dDCDRQTY;

                    if (sCHECK == "*")
                    {
                        // 입항일자
                        sIPHANG = dt.Rows[i]["DPIPHANG"].ToString();
                        // 본선명
                        sCDDESC1 = dt.Rows[i]["CDDESC1"].ToString();
                        // 화주명
                        sVNSANGHO = dt.Rows[i]["VNSANGHO"].ToString();
                        // BL번호
                        sBLNO = dt.Rows[i]["DPBLNO"].ToString();
                        // MSN
                        sMSNSEQ = dt.Rows[i]["DPMSNSEQ"].ToString();
                        // HSN
                        sHSNSEQ = dt.Rows[i]["DPHSNSEQ"].ToString();
                        // 중량
                        sJUNG = dt.Rows[i]["DPJUNG"].ToString();

                        // 체크
                        sCHECK = "";
                    }
                    else
                    {
                        // 입항일자
                        sIPHANG = "";
                        // 본선명
                        sCDDESC1 = "";
                        // 화주명
                        sVNSANGHO = "";
                        // BL번호
                        sBLNO = "";
                        // MSN
                        sMSNSEQ = "";
                        // HSN
                        sHSNSEQ = "";
                        // 중량
                        sJUNG = "";
                        dDPDRQTY = 0;
                    }

                    DataRow row = rtnDt.NewRow();

                    // 통관화주
                    row["DPACTHWAJU"] = dt.Rows[i]["DPACTHWAJU"].ToString();
                    // 화물
                    row["DPHWAMUL"] = dt.Rows[i]["DPHWAMUL"].ToString();
                    // 입항일자
                    row["DPIPHANG"] = sIPHANG.ToString();
                    // 본선
                    row["DPBONSUN"] = dt.Rows[i]["DPBONSUN"].ToString();
                    // 본선명
                    row["CDDESC1"] = sCDDESC1.ToString();
                    // 화주
                    row["DPHWAJU"] = dt.Rows[i]["DPHWAJU"].ToString();
                    // 화주명
                    row["VNSANGHO"] = sVNSANGHO.ToString();
                    // BL번호
                    row["DPBLNO"] = sBLNO.ToString();
                    // MSN
                    row["DPMSNSEQ"] = sMSNSEQ.ToString();
                    // HSN
                    row["DPHSNSEQ"] = sHSNSEQ.ToString();
                    // 중량
                    row["DPJUNG"] = sJUNG.ToString();
                    // 포장수량
                    row["DPDRQTY"] = dDPDRQTY;
                    // 출고일자
                    row["CHDATE"] = dt1.Rows[j]["DCDATE"].ToString();
                    // 출고수량
                    row["CHDRQTY"] = dDCDRQTY;
                    // 재고수량
                    row["JEGO"] = dJEGO;

                    rtnDt.Rows.Add(row);
                }
            }
            return rtnDt;
        }
        #endregion
    }
}
