using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 휴무관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.26 17:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQFH546 : 휴무관리 확인
    ///  TY_P_HR_4BQFJ547 : 휴무관리 등록
    ///  TY_P_HR_4BQFM548 : 휴무관리 수정
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
    ///  GHCODE : 휴무코드
    ///  GHINSABUN : 입력사번
    ///  GHGUBN : 출장구분
    ///  GHEDDATE : 종료일자
    ///  GHSTDATE : 시작일자
    ///  GHDATE : 휴무일자
    ///  GHEDTIME : 종료시간
    ///  GHGWID : GW문서번호
    ///  GHGWURL : GW URL
    ///  GHHAENG : 행선지
    ///  GHSAYU : 휴무사유
    ///  GHSTTIME : 시작시간
    ///  GHTEL : 전화번호
    ///  GHTRWAY : 교통편
    /// </summary>
    public partial class TYHRGT003I : TYBase
    {
        private string fsGHSABUN;
        private string fsGHDATE;
        private string fsGHCODE;
        private string fsGHSEQ;

        #region  Description : 폼 로드 이벤트
        public TYHRGT003I(string sGHSABUN, string sGHDATE, string sGHCODE, string sGHSEQ)
        {
            InitializeComponent();

            fsGHSABUN = sGHSABUN;
            fsGHDATE = sGHDATE;
            fsGHCODE = sGHCODE;
            fsGHSEQ = sGHSEQ;
        }

        private void TYHRGT003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_GHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GHSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GHEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            if (string.IsNullOrEmpty(this.fsGHSABUN))
            {
                this.SetStartingFocus(this.CBH01_GHSABUN.CodeText);
            }
            else
            {
                this.UP_FieldLock();

                this.UP_Run();

                this.SetStartingFocus(this.DTP01_GHSTDATE);
            }
        }
        #endregion

        #region  Description : 확인 UP_RUN 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQFH546", this.fsGHSABUN, this.fsGHDATE, this.fsGHCODE, this.fsGHSEQ);
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
            this.CBH01_GHSABUN.SetReadOnly(true);
            this.DTP01_GHDATE.SetReadOnly(true);
            this.CBH01_GHCODE.SetReadOnly(true);
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
            if (string.IsNullOrEmpty(this.fsGHSABUN))
                this.DbConnector.Attach("TY_P_HR_4BQFJ547", this.CBH01_GHSABUN.GetValue(),
                                                            this.DTP01_GHDATE.GetString(),
                                                            this.CBH01_GHCODE.GetValue(),
                                                            this.TXT01_GHSEQ.GetValue(),
                                                            this.CBO01_GHGUBN.GetValue(),
                                                            this.DTP01_GHSTDATE.GetString(),
                                                            this.MTB01_GHSTTIME.GetValue().ToString().Replace(":", ""),
                                                            this.DTP01_GHEDDATE.GetString(),
                                                            this.MTB01_GHEDTIME.GetValue().ToString().Replace(":", ""),
                                                            this.TXT01_GHSAYU.GetValue(),
                                                            this.TXT01_GHHAENG.GetValue(),
                                                            this.TXT01_GHTEL.GetValue(),
                                                            this.CBH01_GHINSABUN.GetValue(),
                                                            "",
                                                            this.TXT01_GHTRWAY.GetValue(),
                                                            "",
                                                            "",
                                                            TYUserInfo.EmpNo
                                                            );
            else
                this.DbConnector.Attach("TY_P_HR_4BQFM548", this.CBO01_GHGUBN.GetValue(),
                                                            this.DTP01_GHSTDATE.GetString(),
                                                            this.MTB01_GHSTTIME.GetValue().ToString().Replace(":", ""),
                                                            this.DTP01_GHEDDATE.GetString(),
                                                            this.MTB01_GHEDTIME.GetValue().ToString().Replace(":", ""),
                                                            this.TXT01_GHSAYU.GetValue(),
                                                            this.TXT01_GHHAENG.GetValue(),
                                                            this.TXT01_GHTEL.GetValue(),
                                                            this.CBH01_GHINSABUN.GetValue(),
                                                            "",
                                                            this.TXT01_GHTRWAY.GetValue(),
                                                            this.TXT01_GHGWID.GetValue(),
                                                            this.TXT01_GHGWURL.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_GHSABUN.GetValue(),
                                                            this.DTP01_GHDATE.GetString(),
                                                            this.CBH01_GHCODE.GetValue(),
                                                            this.TXT01_GHSEQ.GetValue()
                                                            );
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsGHSABUN))
            {
                //순번생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BQIG564", this.CBH01_GHSABUN.GetValue(), this.DTP01_GHDATE.GetString(), this.CBH01_GHCODE.GetValue());
                Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                TXT01_GHSEQ.SetValue(Set_Fill3(iSeq.ToString()));
            }

            if (string.IsNullOrEmpty(this.fsGHSABUN))
            {
                //년차, 하기휴가, 반년차는 일자,사번,휴무코드 체크하여 이중 등록을 방지한다.
                if (this.CBH01_GHCODE.GetValue().ToString() == "120" || this.CBH01_GHCODE.GetValue().ToString() == "130" || this.CBH01_GHCODE.GetValue().ToString() == "140")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_69NHQ245", this.CBH01_GHSABUN.GetValue(), this.DTP01_GHDATE.GetString(), this.CBH01_GHCODE.GetValue());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_HR_69NHT247");
                        e.Successed = false;
                        return;
                    }
                }
            }


            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : DTP01_GHSTDATE_KeyPress 이벤트
        private void DTP01_GHSTDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.SetStartingFocus(this.MTB01_GHSTTIME);
        }
        #endregion

        #region  Description : DTP01_GHEDDATE_KeyPress 이벤트
        private void DTP01_GHEDDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.SetStartingFocus(this.MTB01_GHEDTIME);
        }
        #endregion


    }
}
