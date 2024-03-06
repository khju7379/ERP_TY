using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 승호생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.30 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522E0249 : 승호생성 인사기본사항 조회
    ///  TY_P_HR_522ED250 : 승호파일 등록
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_522EF252 : 승호파일 기준년월 이상 존재 유무
    ///  TY_P_HR_522EG253 : 승호파일 발련번호 존재 유무
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GGUBUN : 구분
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRCS001B : TYBase
    {
        public string fsYYDATE  = string.Empty;
        public string fsYYSABUN = string.Empty;

        #region Description : 폼 로드
        public TYHRCS001B()
        {
            InitializeComponent();
        }

        private void TYHRCS001B_Load(object sender, System.EventArgs e)
        {
            // 처리 체크
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sOUT_MSG = string.Empty;

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_5CLG5316",
                                        this.DTP01_STDATE.GetValue(),
                                        this.DTP01_EDDATE.GetValue(),
                                        this.CBO01_G2GUBN.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        this.CBO01_GGUBUN.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        sOUT_MSG.ToString()
                                        );

                sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
                {
                    this.ShowMessage("TY_M_HR_5CVDV359");
                }
                else
                {
                    this.ShowMessage("TY_M_HR_5CVDV360");
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 외부 고정 식수
            if (this.CBO01_G2GUBN.GetValue().ToString() == "3")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_5CVE3365",
                    this.DTP01_STDATE.GetString(),
                    this.DTP01_EDDATE.GetString(),
                    this.CBO01_G2GUBN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_HR_5CVDW361");
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_HR_5CVDX363");
                        e.Successed = false;
                        return;
                    }
                }
            }
            else // 직원 식수
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_5CVE5366",
                    this.DTP01_STDATE.GetString(),
                    this.DTP01_EDDATE.GetString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_HR_5CVDX362");
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_HR_5CVDY364");
                        e.Successed = false;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}