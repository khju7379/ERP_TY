using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 특기사항 프로그램입니다.
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
    ///  TY_P_HR_4BIHK446 : 특기사항 조회
    ///  TY_P_HR_4BIHM447 : 특기사항 등록
    ///  TY_P_HR_4BIHO448 : 특기사항 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLSABUN : 사번
    ///  SPGUBUN : 구분
    ///  SPDESC : 특기사항
    ///  SPNUM : 순번
    /// </summary>
    public partial class TYHRKB03C1 : TYBase
    {
        string fsSPSABUN = string.Empty;
        string fsSPNUM = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB03C1(string SPSABUN, string SPNUM)
        {
            fsSPSABUN = SPSABUN;
            fsSPNUM = SPNUM;

            InitializeComponent();
        }

        private void TYHRKB03C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsSPNUM))
            {
                CBH01_HLSABUN.SetValue(fsSPSABUN);
                UP_GetSEQ();
            }
            else
            {
                UP_Select();
            }
            CBH01_HLSABUN.SetReadOnly(true);
            TXT01_SPNUM.SetReadOnly(true);
            SetStartingFocus(CBH01_SPGUBUN.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if(string.IsNullOrEmpty(fsSPNUM))   //등록
            {
                UP_GetSEQ();

                this.DbConnector.Attach("TY_P_HR_4BIHM447", CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_SPNUM.GetValue().ToString(),
                                                            CBH01_SPGUBUN.GetValue().ToString(),
                                                            TXT01_SPDESC.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); 
            }
            else                                //수정
            {
                this.DbConnector.Attach("TY_P_HR_4BIHO448", CBH01_SPGUBUN.GetValue().ToString(),
                                                            TXT01_SPDESC.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_SPNUM.GetValue().ToString()
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
            this.DbConnector.Attach("TY_P_HR_4BIHK446",
                                    fsSPSABUN,
                                    fsSPNUM
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsSPSABUN);
                TXT01_SPNUM.SetValue(Set_Fill3(Convert.ToInt16(fsSPNUM).ToString()));
                CBH01_SPGUBUN.SetValue(dt.Rows[0]["SPGUBUN"].ToString());
                TXT01_SPDESC.SetValue(dt.Rows[0]["SPDESC"].ToString());
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private void UP_GetSEQ()
        {
            string SEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BJAP454", fsSPSABUN);

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_SPNUM.SetValue(Set_Fill3(iCnt.ToString()));
        }
        #endregion
    }
}
