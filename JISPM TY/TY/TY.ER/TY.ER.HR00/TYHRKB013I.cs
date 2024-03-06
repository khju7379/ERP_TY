using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 근태현황 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.13 11:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53DC0670 : 용역직 근태현황 조회(팝업)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  GISABUN : 사원번호
    ///  GICHLTIME : 주간출근시간
    ///  GIDATE : 연장일자
    ///  GIENDTIME : 주간퇴근시간
    ///  GIYACHLTM : 야간출근시간
    ///  GIYAENDTM : 야간퇴근시간
    /// </summary>
    public partial class TYHRKB013I : TYBase
    {
        string fsGIDATE = string.Empty;
        string fsGISABUN = string.Empty;

        #region Description : 폼 로드
        public TYHRKB013I(string sGIDATE, string sGISABUN)
        {
            fsGIDATE = sGIDATE;
            fsGISABUN = sGISABUN;

            InitializeComponent();
        }

        private void TYHRKB013I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (fsGIDATE != "")
            {
                UP_Select(fsGIDATE, fsGISABUN);
                DTP01_GIDATE.SetReadOnly(true);
                CBH01_GISABUN.SetReadOnly(true);
            }
            else
            {
                MTB01_GICHLTIME.SetValue("");
                MTB01_GIENDTIME.SetValue("");
                MTB01_GIYACHLTM.SetValue("");
                MTB01_GIYAENDTM.SetValue("");
                DTP01_GIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_GIDATE.SetReadOnly(false);
                CBH01_GISABUN.SetReadOnly(false);
                SetStartingFocus(DTP01_GIDATE);
            }
            TXT01_SYYOILCD.SetReadOnly(true);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();

                //수정
                if (fsGIDATE != "")
                {   
                    this.DbConnector.Attach("TY_P_HR_53DFW672", MTB01_GICHLTIME.GetValue().ToString().Replace(":",""),
                                                                MTB01_GIENDTIME.GetValue().ToString().Replace(":", ""),
                                                                MTB01_GIYACHLTM.GetValue().ToString().Replace(":", ""),
                                                                MTB01_GIYAENDTM.GetValue().ToString().Replace(":", ""),
                                                                TYUserInfo.EmpNo,
                                                                DTP01_GIDATE.GetString(),
                                                                CBH01_GISABUN.GetValue().ToString()
                                                                );
                }
                //등록
                else
                {   
                    this.DbConnector.Attach("TY_P_HR_53DDK671", DTP01_GIDATE.GetString(),
                                                                CBH01_GISABUN.GetValue().ToString(),
                                                                GetYOIL(TXT01_SYYOILCD.GetValue().ToString()),
                                                                "",
                                                                MTB01_GICHLTIME.GetValue().ToString().Replace(":", ""),
                                                                MTB01_GIENDTIME.GetValue().ToString().Replace(":", ""),
                                                                MTB01_GIYACHLTM.GetValue().ToString().Replace(":", ""),
                                                                MTB01_GIYAENDTM.GetValue().ToString().Replace(":", ""),
                                                                "",
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteNonQuery();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ShowMessage("TY_M_GB_23NAD873");
                this.Close();
            }
            catch
            {
                this.ShowCustomMessage("저장을 실패했습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53DC0670",
                                    DTP01_GIDATE.GetString(),
                                    CBH01_GISABUN.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_3219C986");
                e.Successed = false;
                return;
            }
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select(string sGIDATE, string sGISABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53DC0670",
                                    this.fsGIDATE,
                                    this.fsGISABUN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_GIDATE.SetValue(dt.Rows[0]["GIDATE"].ToString());
                CBH01_GISABUN.SetValue(dt.Rows[0]["GISABUN"].ToString());
                MTB01_GICHLTIME.SetValue(dt.Rows[0]["GICHLTIME"].ToString());
                MTB01_GIENDTIME.SetValue(dt.Rows[0]["GIENDTIME"].ToString());
                MTB01_GIYACHLTM.SetValue(dt.Rows[0]["GIYACHLTM"].ToString());
                MTB01_GIYAENDTM.SetValue(dt.Rows[0]["GIYAENDTM"].ToString());
            }
        }
        #endregion

        #region Description : 일자선택 이벤트
        private void DTP01_GIDATE_ValueChanged(object sender, EventArgs e)
        {
            string sDate = DTP01_GIDATE.GetString();

            DateTime dateValue = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));

            if (dateValue.ToString("ddd") == "일")
            {
                this.TXT01_SYYOILCD.SetValue("일요일");
            }
            else if (dateValue.ToString("ddd") == "월")
            {
                this.TXT01_SYYOILCD.SetValue("월요일");
            }
            else if (dateValue.ToString("ddd") == "화")
            {
                this.TXT01_SYYOILCD.SetValue("화요일");
            }
            else if (dateValue.ToString("ddd") == "수")
            {
                this.TXT01_SYYOILCD.SetValue("수요일");
            }
            else if (dateValue.ToString("ddd") == "목")
            {
                this.TXT01_SYYOILCD.SetValue("목요일");
            }
            else if (dateValue.ToString("ddd") == "금")
            {
                this.TXT01_SYYOILCD.SetValue("금요일");
            }
            else if (dateValue.ToString("ddd") == "토")
            {
                this.TXT01_SYYOILCD.SetValue("토요일");
            }
        }
        #endregion

        #region Description : 요일코드 가져오기
        private string GetYOIL(string SYYOILCD)
        {
            string rtnValue = string.Empty;

            if (SYYOILCD == "일요일")
            {
                rtnValue = "1";
            }
            else if (SYYOILCD == "월요일")
            {
                rtnValue = "2";
            }
            else if (SYYOILCD == "화요일")
            {
                rtnValue = "3";
            }
            else if (SYYOILCD == "수요일")
            {
                rtnValue = "4";
            }
            else if (SYYOILCD == "목요일")
            {
                rtnValue = "5";
            }
            else if (SYYOILCD == "금요일")
            {
                rtnValue = "6";
            }
            else if (SYYOILCD == "토요일")
            {
                rtnValue = "7";
            }

            return rtnValue;
        }
        #endregion
    }
}
