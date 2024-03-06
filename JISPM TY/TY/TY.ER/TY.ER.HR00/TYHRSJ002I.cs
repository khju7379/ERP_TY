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
    /// 상조회입출금관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2016.09.19 14:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_69JES153 : 상조회 등록
    ///  TY_P_HR_69JET154 : 상조회 수정
    ///  TY_P_HR_69JEV156 : 상조회 확인
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
    ///  SJIOAMT : 상조회 금액
    ///  SJIOCODE : 상조회 계정코드
    ///  SJIODATE : 상조회입출금일자
    ///  SJIORKAC : 상조회 적요
    ///  SJIOSABUN : 상조회 사번
    /// </summary>
    public partial class TYHRSJ002I : TYBase
    {
        private string fsSJIODATE;
        private string fsSJIOCODE;
        private string fsSJIOSABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRSJ002I(string sSJIODATE, string sSJIOCODE, string sSJIOSABUN)
        {
            InitializeComponent();

            fsSJIODATE = sSJIODATE;
            fsSJIOCODE = sSJIOCODE;
            fsSJIOSABUN = sSJIOSABUN;

        }

        private void TYHRSJ002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);


            if (string.IsNullOrEmpty(this.fsSJIODATE))
            {
                this.DTP01_SJIODATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                this.SetStartingFocus(this.DTP01_SJIODATE);
            }
            else
            {
                DTP01_SJIODATE.SetValue(fsSJIODATE);
                CBH01_SJIOCODE.SetValue(fsSJIOCODE);
                CBH01_SJIOSABUN.SetValue(fsSJIOSABUN);

                this.DTP01_SJIODATE.SetReadOnly(true);
                this.CBH01_SJIOCODE.SetReadOnly(true);
                this.CBH01_SJIOSABUN.SetReadOnly(true);

                UP_DataBinding();
                this.SetStartingFocus(this.TXT01_SJIOAMT);
            }
        }
        #endregion

        #region  Description : UP_DataBinding 함수 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_69JEV156", DTP01_SJIODATE.GetString(), CBH01_SJIOCODE.GetValue(), CBH01_SJIOSABUN.GetValue() );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsSJIODATE))
            {
                this.DbConnector.Attach("TY_P_HR_69JES153", DTP01_SJIODATE.GetString(), 
                                                            CBH01_SJIOCODE.GetValue(), 
                                                            CBH01_SJIOSABUN.GetValue(),
                                                            TXT01_SJIOAMT.GetValue(),
                                                            TXT01_SJIORKAC.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_69JET154", TXT01_SJIOAMT.GetValue(),
                                                            TXT01_SJIORKAC.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            DTP01_SJIODATE.GetString(),
                                                            CBH01_SJIOCODE.GetValue(),
                                                            CBH01_SJIOSABUN.GetValue()
                                                            );
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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
