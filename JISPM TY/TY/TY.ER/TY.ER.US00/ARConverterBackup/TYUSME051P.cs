using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 화물료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.12.18 14:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_7CIGU310 : 화물료 거래명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  HMGOKJONG : 곡종
    ///  HMHWAJU : 화주
    ///  MAECHIL : 매출일자
    ///  HMIPHANG : 입항일자
    /// </summary>
    public partial class TYUSME051P : TYBase
    {
        #region Description : 폼 로드
        public TYUSME051P()
        {
            InitializeComponent();
        }

        private void TYUSME051P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_MAECHIL.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_HMIPHANG.SetValue("");

            SetStartingFocus(this.DTP01_MAECHIL);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sIPDATE = string.Empty;

            if (this.DTP01_HMIPHANG.GetString() == "" || this.DTP01_HMIPHANG.GetString() == "19000101")
            {
                sIPDATE = "";
            }
            else
            {
                sIPDATE = this.DTP01_HMIPHANG.GetString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_7CIGU310", this.DTP01_MAECHIL.GetString().ToString(),
                                                        this.CBH01_HMHWAJU.GetValue().ToString(),
                                                        this.CBH01_HMGOKJONG.GetValue().ToString(),
                                                        sIPDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUSME051R();
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
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
