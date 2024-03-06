using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 포상사항 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.17 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BHI1437 : 포상사항 조회
    ///  TY_P_HR_4BHI3438 : 포상사항 등록
    ///  TY_P_HR_4BHIA439 : 포상사항 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLSABUN : 사번
    ///  PRGUBUN : 포상코드
    ///  PRDATE : 포상일자
    ///  GYBIGO : 비고
    ///  PRAMOUNT : 포상금액
    ///  PRDESC : 포상내용
    ///  PRGRADE : 포상성적
    /// </summary>
    public partial class TYHRKB02C7 : TYBase
    {
        string fsPRSABUN = string.Empty;
        string fsPRGUBUN = string.Empty;
        string fsPRDATE = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C7(string PRSABUN, string PRGUBUN, string PRDATE)
        {
            fsPRSABUN = PRSABUN;
            fsPRGUBUN = PRGUBUN;
            fsPRDATE = PRDATE;

            InitializeComponent();
        }

        private void TYHRKB02C7_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsPRGUBUN))
            {
                CBH01_HLSABUN.SetValue(fsPRSABUN);
                DTP01_PRDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                SetStartingFocus(CBH01_PRGUBUN.CodeText);
            }
            else
            {
                UP_Select();
                CBH01_PRGUBUN.SetReadOnly(true);
                DTP01_PRDATE.SetReadOnly(true);
                SetStartingFocus(TXT01_PRDESC);
            }
            CBH01_HLSABUN.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsPRGUBUN))   //등록
            {
                this.DbConnector.Attach("TY_P_HR_4BHI3438", CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_PRGUBUN.GetValue().ToString(),
                                                            DTP01_PRDATE.GetValue().ToString(),
                                                            TXT01_PRDESC.GetValue().ToString(),
                                                            TXT01_PRGRADE.GetValue().ToString(),
                                                            TXT01_PRAMOUNT.GetValue().ToString(),
                                                            TXT01_GYBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else                                    // 수정
            {
                this.DbConnector.Attach("TY_P_HR_4BHIA439", TXT01_PRDESC.GetValue().ToString(),
                                                            TXT01_PRGRADE.GetValue().ToString(),
                                                            TXT01_PRAMOUNT.GetValue().ToString(),
                                                            TXT01_GYBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_PRGUBUN.GetValue().ToString(),
                                                            DTP01_PRDATE.GetValue().ToString()
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
            if (string.IsNullOrEmpty(this.fsPRGUBUN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4BHI1437",
                    CBH01_HLSABUN.GetValue().ToString(),
                    CBH01_PRGUBUN.GetValue().ToString(),
                    DTP01_PRDATE.GetValue().ToString()
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
            this.DbConnector.Attach("TY_P_HR_4BHI1437",
                                    fsPRSABUN,
                                    fsPRGUBUN,
                                    fsPRDATE
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsPRSABUN);
                CBH01_PRGUBUN.SetValue(fsPRGUBUN);
                DTP01_PRDATE.SetValue(fsPRDATE);
                TXT01_PRDESC.SetValue(dt.Rows[0]["PRDESC"].ToString());
                TXT01_PRGRADE.SetValue(dt.Rows[0]["PRGRADE"].ToString());
                TXT01_PRAMOUNT.SetValue(dt.Rows[0]["PRAMOUNT"].ToString());
                TXT01_GYBIGO.SetValue(dt.Rows[0]["PRBIGO"].ToString());
            }
        }
        #endregion
    }
}
