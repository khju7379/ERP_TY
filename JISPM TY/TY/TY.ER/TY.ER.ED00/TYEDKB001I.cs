using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// EDI 환경설정 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.31 16:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73VG5178 : EDI 환경설정 등록
    ///  TY_P_UT_73VGB179 : EDI 환경설정 수정
    ///  TY_P_UT_73VGB180 : EDI 환경설정 삭제
    ///  TY_P_UT_73VGC181 : EDI 환경설정 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EDNCEONAME : 대표자
    ///  EDNCUSTKWA : 과
    ///  EDNCUSTLOC : 세관
    ///  EDNIMPSIGN : 보세창고부호
    ///  EDNMEMO : 비고
    ///  EDNSDMASTID : 송수신식별자
    ///  EDNSTMASTID : 기본식별자
    /// </summary>
    public partial class TYEDKB001I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB001I()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_DataBinding();

        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VGC181" );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }         
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VGB180", this.TXT01_EDNIMPSIGN.GetValue());
            this.DbConnector.ExecuteTranQuery();

            this.Initialize_Controls("01");

            this.SetFocus(TXT01_EDNIMPSIGN);

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VGI182", TXT01_EDNIMPSIGN.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73VGB179", this.TXT01_EDNSTMASTID.GetValue(),
                                                            this.TXT01_EDNSDMASTID.GetValue(),
                                                            this.TXT01_EDNCUSTLOC.GetValue(),
                                                            this.TXT01_EDNCUSTKWA.GetValue(),
                                                            this.TXT01_EDNCEONAME.GetValue(),
                                                            this.TXT01_EDNMEMO.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.TXT01_EDNIMPSIGN.GetValue()
                                                            );
                this.DbConnector.ExecuteTranQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73VG5178", this.TXT01_EDNIMPSIGN.GetValue(),
                                                            this.TXT01_EDNSTMASTID.GetValue(),
                                                            this.TXT01_EDNSDMASTID.GetValue(),
                                                            this.TXT01_EDNCUSTLOC.GetValue(),
                                                            this.TXT01_EDNCUSTKWA.GetValue(),
                                                            this.TXT01_EDNCEONAME.GetValue(),
                                                            this.TXT01_EDNMEMO.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
                this.DbConnector.ExecuteTranQuery();
            }

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
            this.Close();
        }
        #endregion

    }
}
