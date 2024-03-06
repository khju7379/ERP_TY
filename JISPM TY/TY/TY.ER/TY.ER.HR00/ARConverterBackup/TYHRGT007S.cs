using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근태사항 현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.11 17:39
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CBGW725 : 근태사항 현황 조회
    ///  TY_P_HR_4CBHI731 : 콤보박스 - 근태관리
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CBH3726 : 근태사항 현황 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  KBBUSEO : 부서
    ///  KBSABUN : 사번
    ///  GGUBUN : 구분
    ///  KBJKCD : 직급
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGT007S : TYBase
    {
        
        #region Description : 페이지 로드
        public TYHRGT007S()
        {
            InitializeComponent();            
        }

        private void TYHRGT007S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            DTP01_STDATE.SetValue(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBO01_GGUBUN.SetValue("1");
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4CBGW725",
                DTP01_STDATE.GetString(),
                DTP01_EDDATE.GetString(),
                CBH01_KBBUSEO.GetValue().ToString(),
                CBH01_KBSABUN.GetValue().ToString(),
                Get_KBJKCD(),
                CBO01_GGUBUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4CBH3726.SetValue(dt);

            //if( dt.Rows.Count > 1)
            //{
            //    UP_Spread_Desc(dt);
            //}
            
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_5BAAQ136",
                DTP01_STDATE.GetString(),
                DTP01_EDDATE.GetString(),
                CBH01_KBBUSEO.GetValue().ToString(),
                CBH01_KBSABUN.GetValue().ToString(),
                Get_KBJKCD(),
                CBO01_GGUBUN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            ActiveReport rpt = new TYHRGT007R();
            (new TYERGB001P(rpt, dt)).ShowDialog();
        }
        #endregion

        #region Description : 스프레트 틀 만들기
        private void UP_Spread_Desc(DataTable dt)
        {
            int iSTrowNum = 0;
            int iRowsCount = 0;

            this.FPS91_TY_S_HR_4CBH3726_Sheet1.ColumnCount = 41;
            this.FPS91_TY_S_HR_4CBH3726_Sheet1.RowCount = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (i > 0)
                {
                    if (dt.Rows[i - 1]["KBBUSEO"].ToString() != dt.Rows[i]["KBBUSEO"].ToString())
                    {
                        if (iRowsCount == 0)
                        {
                            iRowsCount = 1;
                        }
                        this.FPS91_TY_S_HR_4CBH3726_Sheet1.AddSpanCell(iSTrowNum, 1, iRowsCount, 1);
                        iSTrowNum = i;
                        iRowsCount = -1;
                    }
                }
                iRowsCount++;
            }
            this.FPS91_TY_S_HR_4CBH3726_Sheet1.AddSpanCell(iSTrowNum, 1, dt.Rows.Count - iSTrowNum, 1);
            
        }
        #endregion

        #region Description : 직급가져오기
        private string Get_KBJKCD()
        {
            string KBJKCD = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBO01_KBJKCD.GetValue().ToString() == "")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_4CBIW754");

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            KBJKCD = dt.Rows[i]["CDCODE"].ToString();
                        }
                        else
                        {
                            KBJKCD += "," + dt.Rows[i]["CDCODE"].ToString();
                        }
                    }
                }
            }
            else
            {
                KBJKCD = CBO01_KBJKCD.GetValue().ToString().Replace("'", "");
            }

            return KBJKCD;
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4CBH3726_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
           if ((new TYHRGT005P(this.FPS91_TY_S_HR_4CBH3726.GetValue("GIDATE").ToString().Replace("-",""), this.FPS91_TY_S_HR_4CBH3726.GetValue("GISABUN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
               this.BTN61_INQ_Click(null, null);

           
        }
        #endregion
    }
}
