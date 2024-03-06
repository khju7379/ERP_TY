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
    /// 사용자정의값 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.10.12 14:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5ACDI970 : 사용자정의값 등록
    ///  TY_P_HR_5ACDK972 : 사용자정의값 수정
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
    ///  USRGUBN : 사용구분
    ///  USRCDDESC : 코드 설명
    ///  USRCDNAME : 사용정의코드명
    ///  USRCDVALUE : 코드값
    ///  USRCODE : 사용자정의코드
    ///  USRDATATYPE : DATA 타입
    ///  USREDATE : 종료일자
    ///  USRSDATE : 시작일자
    /// </summary>
    public partial class TYHRPY022I : TYBase
    {
        private string  fsUSRGUBN;
	    private string  fsUSRCODE;
	    private string  fsUSRSDATE;

        #region  Description : 폼 로드 이벤트
        public TYHRPY022I(string sUSRGUBN, string sUSRCODE, string sUSRSDATE)
        {
            InitializeComponent();

            this.fsUSRGUBN = sUSRGUBN;
            this.fsUSRCODE = sUSRCODE;
            this.fsUSRSDATE = sUSRSDATE;
        }

        private void TYHRPY022I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsUSRGUBN))
            {
                //등록
                this.UP_SetFileClear();
                this.UP_SetFileLock(false);
                this.SetStartingFocus(this.CBO01_USRGUBN);
            }
            else
            {
                //수정
                this.UP_DataBinding();
                this.UP_SetFileLock(true);
                this.SetStartingFocus(this.TXT01_USRCDNAME);
            }
        }
        #endregion

        #region  Description : UP_DataBinding 함수
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5ACFZ978", this.fsUSRGUBN, this.fsUSRCODE, this.fsUSRSDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CBO01_USRGUBN.SetValue(dt.Rows[0]["USRGUBN"].ToString());
                this.TXT01_USRCODE.SetValue(dt.Rows[0]["USRCODE"].ToString());
                this.DTP01_USRSDATE.SetValue(dt.Rows[0]["USRSDATE"].ToString());
                this.TXT01_USRCDNAME.SetValue(dt.Rows[0]["USRCDNAME"].ToString());
                this.TXT01_USRCDDESC.SetValue(dt.Rows[0]["USRCDDESC"].ToString());
                this.CBO01_USRDATATYPE.SetValue(dt.Rows[0]["USRDATATYPE"].ToString());
                this.TXT01_USRCDVALUE.SetValue(dt.Rows[0]["USRCDVALUE"].ToString());
                this.DTP01_USREDATE.SetValue(dt.Rows[0]["USREDATE"].ToString());
            }
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
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
            if (string.IsNullOrEmpty(this.fsUSRGUBN))
            {
              this.DbConnector.Attach("TY_P_HR_5ACDI970", this.CBO01_USRGUBN.GetValue(),
                                        this.TXT01_USRCODE.GetValue(),
                                        this.DTP01_USRSDATE.GetValue(),
                                        this.TXT01_USRCDNAME.GetValue(),
                                        this.TXT01_USRCDDESC.GetValue(),
                                        this.CBO01_USRDATATYPE.GetValue(),
                                        this.TXT01_USRCDVALUE.GetValue(),
                                        this.DTP01_USREDATE.GetValue(),
                                        TYUserInfo.EmpNo
                                        );
            }
            else
            {                

                this.DbConnector.Attach("TY_P_HR_5ACDK972", this.TXT01_USRCDNAME.GetValue(),
                                        this.TXT01_USRCDDESC.GetValue(),
                                        this.CBO01_USRDATATYPE.GetValue(),
                                        this.TXT01_USRCDVALUE.GetValue(),
                                        this.DTP01_USREDATE.GetValue(),
                                        TYUserInfo.EmpNo,
                                        this.CBO01_USRGUBN.GetValue(),
                                        this.TXT01_USRCODE.GetValue(),
                                        this.DTP01_USRSDATE.GetValue()
                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

        #region  Description : 필드 클리어
        private void UP_SetFileClear()
        {
            this.CBO01_USRGUBN.SetValue("01");
            this.TXT01_USRCODE.SetValue("");
            this.DTP01_USRSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_USRCDNAME.SetValue("");
            this.TXT01_USRCDDESC.SetValue("");
            this.CBO01_USRDATATYPE.SetValue("C");
            this.TXT01_USRCDVALUE.SetValue("");
            this.DTP01_USREDATE.SetValue("");
        }       
        #endregion

        #region  Description : 필드 lock 
        private void UP_SetFileLock(bool bValue)
        {
            this.CBO01_USRGUBN.SetReadOnly(bValue);
            this.TXT01_USRCODE.SetReadOnly(bValue);
            this.DTP01_USRSDATE.SetReadOnly(bValue);
        }
        #endregion

    }
}
