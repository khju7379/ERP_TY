using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUSME20C2 : TYBase
    {
        public string fsUTMAECH   = string.Empty;
        public string fsUTDATE    = string.Empty;
        public string fsUTHWAJU   = string.Empty;

        public string fsUTWNJPNO  = string.Empty;

        #region Description : 페이지 로드
        public TYUSME20C2(string sUTMAECH, string sUTDATE, string sUTHWAJU)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsUTMAECH = sUTMAECH.ToString();

            fsUTDATE  = sUTDATE.ToString();
            fsUTHWAJU = sUTHWAJU.ToString();
        }

        private void TYUSME20C2_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_92JDK819.Initialize();

            this.DTP01_STDATE.SetValue(fsUTDATE.ToString());
            this.DTP01_EDDATE.SetValue(fsUTDATE.ToString());
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_US_92JDN821",
               fsUTHWAJU.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_SHWAJU.SetValue(dt.Rows[0]["VNCODE"].ToString());

                this.BTN61_INQ_Click(null, null);
            }

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sB2CDAC = string.Empty;
            
            if (fsUTMAECH.ToString() == "접안료")
            {
                sB2CDAC = "41200200";
            }
            else if (fsUTMAECH.ToString() == "시설사용료")
            {
                sB2CDAC = "41200500";
            }
            else if (fsUTMAECH.ToString() == "하역료")
            {
                sB2CDAC = "41200300";
            }
            else if (fsUTMAECH.ToString() == "보관료")
            {
                sB2CDAC = "41200100";
            }
            else if (fsUTMAECH.ToString() == "조출료")
            {
                sB2CDAC = "41200600";
            }
            else if (fsUTMAECH.ToString() == "개포작업비")
            {
                sB2CDAC = "41201000";
            }
            else if (fsUTMAECH.ToString() == "화물료")
            {
                sB2CDAC = "41200700";
            }
            else if (fsUTMAECH.ToString() == "현대화기금")
            {
                sB2CDAC = "41200800";
            }
            else if (fsUTMAECH.ToString() == "이송료")
            {
                sB2CDAC = "41200400";
            }
            else if (fsUTMAECH.ToString() == "중개수수료")
            {
                sB2CDAC = "41200900";
            }
            else if (fsUTMAECH.ToString() == "소급분-시설사용료")
            {
                sB2CDAC = "41200500";
            }
            else if (fsUTMAECH.ToString() == "소급분-하역료")
            {
                sB2CDAC = "41200300";
            }
            else if (fsUTMAECH.ToString() == "소급분-현대화기금")
            {
                sB2CDAC = "41200800";
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_US_92JDI818",
               Get_Date(this.DTP01_STDATE.GetValue().ToString()),
               Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
               sB2CDAC.ToString(),
               this.CBH01_SHWAJU.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_92JDK819.SetValue(dt);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_US_92JDK819_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsUTWNJPNO = "";

            fsUTWNJPNO = this.FPS91_TY_S_US_92JDK819.GetValue("JUNNO").ToString();
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}