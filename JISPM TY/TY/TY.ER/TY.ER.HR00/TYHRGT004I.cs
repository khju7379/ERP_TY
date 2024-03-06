using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 20:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPK2533 : 연장관리 등록
    ///  TY_P_HR_4BPK4535 : 연장관리 수정
    ///  TY_P_HR_4BPKA536 : 연장관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  GYDGSABUN : 대근자
    ///  GYINSABUN : 입력자
    ///  GYSABUN : 사  번
    ///  GYGUBN : 신청형태
    ///  GYDATE : 연장일자
    ///  GYEDTIME : 종료시간
    ///  GYSAYU : 사 유
    ///  GYSTTIME : 시작시간
    /// </summary>
    public partial class TYHRGT004I : TYBase
    {
        private string fsGYSABUN;
        private string fsGYDATE;
        private string fsGYGUBN;

        #region  Description : 폼 로드 이벤트
        public TYHRGT004I(string sGYDATE, string sGYSABUN, string sGYGUBN)
        {
            InitializeComponent();

            this.fsGYDATE = sGYDATE;
            this.fsGYSABUN = sGYSABUN;
            this.fsGYGUBN = sGYGUBN;
        }

        private void TYHRGT004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_GYDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            if (string.IsNullOrEmpty(this.fsGYDATE))
            {
                SetStartingFocus(this.DTP01_GYDATE);
            }
            else
            {
                this.UP_FieldLock();
                this.UP_Run();
            }
            
        }
        #endregion

        #region  Description : 확인 UP_RUN 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPKA536", this.fsGYDATE, this.fsGYSABUN, this.fsGYGUBN);
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(ds.Tables[0], "01");
            }
        }
        #endregion

        #region Description : 필드 LOCK 이벤트
        private void UP_FieldLock()
        {
            this.DTP01_GYDATE.SetReadOnly(true);
            this.CBH01_GYSABUN.SetReadOnly(true);
            this.CBO01_GYGUBN.SetReadOnly(true);
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsGYDATE))
                this.DbConnector.Attach("TY_P_HR_4BPK2533", this.DTP01_GYDATE.GetString(), 
                                                            this.CBH01_GYSABUN.GetValue(),
                                                            this.CBO01_GYGUBN.GetValue(),
                                                            this.TXT01_GYSAYU.GetValue(),
                                                            this.MTB01_GYSTTIME.GetValue().ToString().Replace(":",""),
                                                            this.MTB01_GYEDTIME.GetValue().ToString().Replace(":",""),
                                                            this.CBH01_GYINSABUN.GetValue(),
                                                            "",
                                                            this.CBH01_GYDGSABUN.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            else
                this.DbConnector.Attach("TY_P_HR_4BPK4535", this.TXT01_GYSAYU.GetValue(),
                                                            this.MTB01_GYSTTIME.GetValue().ToString().Replace(":",""),
                                                            this.MTB01_GYEDTIME.GetValue().ToString().Replace(":",""),
                                                            this.CBH01_GYINSABUN.GetValue(),
                                                            "",
                                                            this.CBH01_GYDGSABUN.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_GYDATE.GetString(),
                                                            this.CBH01_GYSABUN.GetValue(),
                                                            this.CBO01_GYGUBN.GetValue()
                                                            );
            this.DbConnector.ExecuteTranQuery();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //신청형태
            if (this.CBO01_GYGUBN.GetValue().ToString() == "3")
            {
                if (this.CBH01_GYDGSABUN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_HR_4BQDS541");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.CBH01_GYDGSABUN.SetValue("");
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }        
        #endregion
    }
}
