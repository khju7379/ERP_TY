using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 징계사항 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.18 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BIAM441 : 징계사항 등록
    ///  TY_P_HR_4BIAO442 : 징계사항 수정
    ///  TY_P_HR_4BIBC443 : 징계사항 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3219C986 : 동일 자료가 존재합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLSABUN : 사번
    ///  SBGUBUN : 징계코드
    ///  SBDATE : 징계일자
    ///  SBKDATE1 : 징계기간1
    ///  SBKDATE2 : 징계기간2
    ///  GYBIGO : 비고
    ///  SBDESC : 징계내용
    ///  SBPAYAMT : 감봉금액
    ///  SBPAYRATE : 감봉율
    /// </summary>
    public partial class TYHRKB02C8 : TYBase
    {
        string fsSBSABUN = string.Empty;
        string fsSBGUBUN = string.Empty;
        string fsSBDATE = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C8(string SBSABUN, string SBGUBUN, string SBDATE)
        {
            fsSBSABUN = SBSABUN;
            fsSBGUBUN = SBGUBUN;
            fsSBDATE = SBDATE;

            InitializeComponent();
        }

        private void TYHRKB02C8_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsSBGUBUN))
            {
                CBH01_HLSABUN.SetValue(fsSBSABUN);
                DTP01_SBDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_SBKDATE1.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_SBKDATE2.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                SetStartingFocus(CBH01_SBGUBUN.CodeText);
            }
            else
            {
                UP_Select();
                CBH01_SBGUBUN.SetReadOnly(true);
                DTP01_SBDATE.SetReadOnly(true);
                SetStartingFocus(TXT01_SBDESC);
            }
            CBH01_HLSABUN.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsSBGUBUN))   //등록
            {
                this.DbConnector.Attach("TY_P_HR_4BIAM441", CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_SBGUBUN.GetValue().ToString(),
                                                            DTP01_SBDATE.GetValue().ToString(),
                                                            TXT01_SBDESC.GetValue().ToString(),
                                                            DTP01_SBKDATE1.GetValue().ToString(),
                                                            DTP01_SBKDATE2.GetValue().ToString(),
                                                            TXT01_SBPAYRATE.GetValue().ToString(),
                                                            TXT01_SBPAYAMT.GetValue().ToString(),
                                                            TXT01_GYBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else                                    // 수정
            {
                this.DbConnector.Attach("TY_P_HR_4BIAO442", TXT01_SBDESC.GetValue().ToString(),
                                                            DTP01_SBKDATE1.GetValue().ToString(),
                                                            DTP01_SBKDATE2.GetValue().ToString(),
                                                            TXT01_SBPAYRATE.GetValue().ToString(),
                                                            TXT01_SBPAYAMT.GetValue().ToString(),
                                                            TXT01_GYBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_SBGUBUN.GetValue().ToString(),
                                                            DTP01_SBDATE.GetValue().ToString()
                                                            );
            }
            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsSBGUBUN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    ("TY_P_HR_4BIBC443",
                    CBH01_HLSABUN.GetValue().ToString(),
                    CBH01_SBGUBUN.GetValue().ToString(),
                    DTP01_SBDATE.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BIBC443",
                                    fsSBSABUN,
                                    fsSBGUBUN,
                                    fsSBDATE
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsSBSABUN);
                CBH01_SBGUBUN.SetValue(fsSBGUBUN);
                DTP01_SBDATE.SetValue(fsSBDATE);
                TXT01_SBDESC.SetValue(dt.Rows[0]["SBDESC"].ToString());
                DTP01_SBKDATE1.SetValue(dt.Rows[0]["SBKDATE1"].ToString());
                DTP01_SBKDATE2.SetValue(dt.Rows[0]["SBKDATE2"].ToString());
                TXT01_SBPAYRATE.SetValue(dt.Rows[0]["SBPAYRATE"].ToString());
                TXT01_SBPAYAMT.SetValue(dt.Rows[0]["SBPAYAMT"].ToString());
                TXT01_GYBIGO.SetValue(dt.Rows[0]["SBBIGO"].ToString());
            }
        }
        #endregion
    }
}
