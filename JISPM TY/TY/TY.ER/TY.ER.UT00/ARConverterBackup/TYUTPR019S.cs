using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 계약대장 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.16 20:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75GKJ541 : 계약대장 조회
    ///  TY_P_UT_75GKK542 : 계약대장 조회(화주X)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_75GKL543 : 계약대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CNHWAJU : 화주
    ///  CNCONTEN : 계약종료일자
    ///  CNTANKNO : 탱크번호
    /// </summary>
    public partial class TYUTPR019S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR019S()
        {
            InitializeComponent();
        }

        private void TYUTPR019S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_CNHWAJU.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_75GKL543.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_CNHWAJU.GetValue().ToString() != "")
            {
                string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

                // 대표거래처 코드 가져오기
                sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

                this.DbConnector.Attach("TY_P_UT_75GKJ541", sHWAJU,
                                                            this.TXT01_CNTANKNO.GetValue().ToString(),
                                                            this.TXT01_CNCONTEN.GetValue().ToString());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_75GKK542", this.TXT01_CNTANKNO.GetValue().ToString(),
                                                            this.TXT01_CNCONTEN.GetValue().ToString());
            }

            this.FPS91_TY_S_UT_75GKL543.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBH01_CNHWAJU.GetValue().ToString() != "")
            {
                string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

                // 대표거래처 코드 가져오기
                sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

                this.DbConnector.Attach("TY_P_UT_75GKJ541", sHWAJU,
                                                            this.TXT01_CNTANKNO.GetValue().ToString(),
                                                            this.TXT01_CNCONTEN.GetValue().ToString());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_75GKK542", this.TXT01_CNTANKNO.GetValue().ToString(),
                                                            this.TXT01_CNCONTEN.GetValue().ToString());
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();
            
            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR019R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
