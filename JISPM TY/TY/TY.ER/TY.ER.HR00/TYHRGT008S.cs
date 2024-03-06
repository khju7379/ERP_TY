using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인별 근태현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.17 15:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CHEU853 : 개인별 근태현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CHFW855 : 개인별 근태현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  GHCODE : 휴무코드
    ///  GTGUBN : 근태구분
    ///  S1BRANCH : 사업장
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGT008S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRGT008S()
        {
            InitializeComponent();
        }

        private void TYHRGT008S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
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

            string BRANCH = string.Empty;
            string GTGUBN = string.Empty;

            //근태구분 1-출근 2-연장 3-지각 4-사적외출 5-공적외출 6-특이자 7-지각+사적외출

            //if (CBO01_S1BRANCH.GetValue().ToString() == "1")
            //{
            //    BRANCH = "2";
            //}
            //else
            //{
            //    BRANCH = "1";
            //}

         


            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4CHEU853",
                DTP01_STDATE.GetString(),
                DTP01_EDDATE.GetString(),
                CBO01_S1BRANCH.GetValue().ToString(),
                CBH01_KBSABUN.GetValue().ToString(),
                CBO01_GTGUBN.GetValue().ToString(),
                Get_GHCODE()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4CHFW855.SetValue(dt);
      
        }
        #endregion

        #region Description : 휴무코드 가져오기
        private string Get_GHCODE()
        {
            string GHCODE = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBO01_GHCODE.GetValue().ToString().Replace("'", "") == "")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_4CBJ4764","HM");

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            GHCODE = "'','" + dt.Rows[i]["CODE"].ToString() + "'";
                        }
                        else
                        {
                            GHCODE += ",'" + dt.Rows[i]["CODE"].ToString() + "'";
                        }
                    }
                }
            }
            else
            {
                GHCODE = CBO01_GHCODE.GetValue().ToString();
            }

            return GHCODE;
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

        #region  Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4CHFW855_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGT005P(this.FPS91_TY_S_HR_4CHFW855.GetValue("GIDATE").ToString().Replace("-", ""), this.FPS91_TY_S_HR_4CHFW855.GetValue("GISABUN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
