using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 접안일자별 재고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.16 11:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66GB1255 : 접안일자별 재고조회(마스타)
    ///  TY_P_UT_66GBA256 : 접안일자별 재고조회(디테일)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66GBB257 : 접안일자별 재고조회(마스타)
    ///  TY_S_UT_66GBB258 : 접안일자별 재고조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  IPHANG : 접안일자
    ///  CJCHQTYTOT : 총 출고량
    ///  CJJEQTYTOT : 총 재고량
    /// </summary>
    public partial class TYUTIN018S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIN018S()
        {
            InitializeComponent();
        }

        private void TYUTIN018S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_IPHANG.SetValue(System.DateTime.Now);
            this.DTP01_EDDATE.SetValue(System.DateTime.Now);

            this.DTP01_IPHANG.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_IPHANG);
        }
        #endregion

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66GBB257.Initialize();
            this.FPS91_TY_S_UT_66GBB258.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66GB1255", this.DTP01_IPHANG.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66GBB257.SetValue(dt);

            this.TXT01_CJJEQTYTOT.Text = "0.000";
            this.TXT01_CJCHQTYTOT.Text = "0.000";
        }
        #endregion

        #region Description : 마스타 그리드선택
        private void FPS91_TY_S_UT_66GBB257_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66GBB258.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66GBA256", this.FPS91_TY_S_UT_66GBB257.GetValue("IPIPHANG").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPBONSUN").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPHWAJU").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPHWAMUL").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPBLNO").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPMSNSEQ").ToString(),
                                                        this.FPS91_TY_S_UT_66GBB257.GetValue("IPHSNSEQ").ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66GBB258.SetValue(UP_ChangeDatatable(dt));
        }
        #endregion

        #region Description : 데이터 테이블 변경
        private DataTable UP_ChangeDatatable(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            double dIPMTQTY = 0;
            double dIPPAQTY = 0;
            string sCHCHULIL = string.Empty;
            double dCHQ = 0;
            double dCUJGQTY = 0;
            double dCHQTOT = 0;

            DataRow row;

            rtnDt.Columns.Add("IPMTQTY", typeof(System.String));
            rtnDt.Columns.Add("IPPAQTY", typeof(System.String));
            rtnDt.Columns.Add("CHCHULIL", typeof(System.String));
            rtnDt.Columns.Add("CHQ", typeof(System.String));
            rtnDt.Columns.Add("CUJGQTY", typeof(System.String));
            rtnDt.Columns.Add("IPACTHJ", typeof(System.String));
            rtnDt.Columns.Add("VNSANGHO", typeof(System.String));



            // 우선 테이타 셋의 테이블의 처음부터 끝까지 도는 루프 코딩
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1. row 를 생성하고 각각의 값을 채워넣자.
                row = rtnDt.NewRow();

                if (i == 0)
                {
                    // 입고량
                    dIPMTQTY = double.Parse(dt.Rows[i]["IPMTQTY"].ToString());
                    row["IPMTQTY"] = String.Format("{0,9:N3}", dIPMTQTY);

                    // 통관량					
                    dIPPAQTY = double.Parse(dt.Rows[i]["IPPAQTY"].ToString());
                    row["IPPAQTY"] = String.Format("{0,9:N3}", dIPPAQTY);
                }
                else
                {
                    // 입고량
                    row["IPMTQTY"] = "";
                    // 통관량
                    row["IPPAQTY"] = "";
                }

                // 출고일
                row["CHCHULIL"] = dt.Rows[i]["CHCHULIL"].ToString();

                dCHQ = double.Parse(Get_Numeric(dt.Rows[i]["CHQ"].ToString()));
                // 출고량
                row["CHQ"] = String.Format("{0,9:N3}", dCHQ);

                dCHQTOT = dCHQTOT + dCHQ;

                // 통관재고
                if (i == 0)
                {
                    dCUJGQTY = dIPPAQTY - dCHQ;
                }
                else
                {
                    dCUJGQTY = dCUJGQTY - dCHQ;
                }

                row["CUJGQTY"] = String.Format("{0,9:N3}", dCUJGQTY);

                // 통관화주
                row["IPACTHJ"] = dt.Rows[i]["IPACTHJ"].ToString();
                row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();

                rtnDt.Rows.Add(row);
            }

            this.TXT01_CJJEQTYTOT.Text = string.Format("{0:#,##0.000}", dCUJGQTY);
            this.TXT01_CJCHQTYTOT.Text = string.Format("{0:#,##0.000}", dCHQTOT);

            return rtnDt;
        }
        #endregion
    }
}
