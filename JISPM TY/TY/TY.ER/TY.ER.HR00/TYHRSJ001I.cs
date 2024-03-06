using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 상조회코드관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.11.26 15:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5BQEM219 : 상조회코드관리 조회
    ///  TY_P_HR_5BQF1223 : 상조회코드관리 등록
    ///  TY_P_HR_5BQF3224 : 상조회코드관리 수정
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
    ///  SJACHL1 : 상조회 상위계정1
    ///  SJACHL2 : 상조회 상위계정2
    ///  SJACHL3 : 상조회 상위계정3
    ///  SJFUNDGN : 수입,지출 구분
    ///  SJABAC : 상조회 계정약명
    ///  SJCODE : 상조회 계정과목
    ///  SJNMAC : 상조회 계정명
    ///  SJTAGGN : 실계정 유무
    /// </summary>
    public partial class TYHRSJ001I : TYBase
    {
        private string fsSJCODE;

        #region  Description : 폼 로드 이벤트
        public TYHRSJ001I(string sSJCODE)
        {
            InitializeComponent();

            fsSJCODE = sSJCODE;
        }

        private void TYHRSJ001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsSJCODE))
            {
                UP_FieldClear();
                this.SetStartingFocus(this.TXT01_SJCODE);
            }
            else
            {
                this.TXT01_SJCODE.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BQEM219", this.fsSJCODE, "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_SJCODE.SetValue(dt.Rows[0]["SJCODE"].ToString());
                    this.TXT01_SJNMAC.SetValue(dt.Rows[0]["SJNMAC"].ToString());
                    this.TXT01_SJABAC.SetValue(dt.Rows[0]["SJABAC"].ToString());
                    this.CBH01_SJACHL1.SetValue(dt.Rows[0]["SJACHL1"].ToString());
                    this.CBH01_SJACHL2.SetValue(dt.Rows[0]["SJACHL2"].ToString());
                    this.CBH01_SJACHL3.SetValue(dt.Rows[0]["SJACHL3"].ToString());
                    this.CBO01_SJFUNDGN.SetValue(dt.Rows[0]["SJFUNDGN"].ToString());
                    this.CKB01_SJTAGGN.SetValue(dt.Rows[0]["SJTAGGN"].ToString());
                }

                this.SetStartingFocus(this.TXT01_SJNMAC);
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsSJCODE))
            {                
                this.DbConnector.Attach("TY_P_HR_5BQF1223", this.TXT01_SJCODE.GetValue().ToString(),
                                                            this.TXT01_SJNMAC.GetValue().ToString(),
                                                            this.TXT01_SJABAC.GetValue().ToString(),
                                                            this.CBH01_SJACHL1.GetValue(),
                                                            this.CBH01_SJACHL2.GetValue(),
                                                            this.CBH01_SJACHL3.GetValue(),
                                                            this.CBO01_SJFUNDGN.GetValue().ToString(),
                                                            this.CKB01_SJTAGGN.GetValue(),
                                                            TYUserInfo.EmpNo);
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_5BQF3224", this.TXT01_SJNMAC.GetValue().ToString(),
                                                            this.TXT01_SJABAC.GetValue().ToString(),
                                                            this.CBH01_SJACHL1.GetValue(),
                                                            this.CBH01_SJACHL2.GetValue(),
                                                            this.CBH01_SJACHL3.GetValue(),
                                                            this.CBO01_SJFUNDGN.GetValue().ToString(),
                                                            this.CKB01_SJTAGGN.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.TXT01_SJCODE.GetValue().ToString());
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 필드 클리어 함수
        private void UP_FieldClear()
        {
            this.TXT01_SJCODE.SetValue("");
            this.TXT01_SJNMAC.SetValue("");
            this.TXT01_SJABAC.SetValue("");
            this.CBH01_SJACHL1.SetValue("");
            this.CBH01_SJACHL2.SetValue("");
            this.CBH01_SJACHL3.SetValue("");
            this.CBO01_SJFUNDGN.SetValue("1");
            this.CKB01_SJTAGGN.SetValue("N");
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion


    }
}
