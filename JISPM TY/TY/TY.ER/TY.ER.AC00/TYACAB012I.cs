using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 입금표관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.13 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26D2M849 : 입금표 체크
    ///  TY_P_AC_26D95843 : 입금표 등록
    ///  TY_P_AC_26D9A844 : 입금표 수정
    ///  TY_P_AC_26D9D845 : 입금표 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_AC_26D47852 : 전표번호를 확인하세요!
    ///  TY_M_AC_26D40854 : 구분을 확인하세요!
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  ARCDSB :  수령자
    ///  ARVEND :  입금처
    ///  ARLOCAL :  지　　역
    ///  ARDATE :  수령일자
    ///  ARAMT :  입금액
    ///  ARCHK :  전표확인
    ///  ARCHKDATE :  확인일자
    ///  ARJUNNO :  전표번호
    ///  ARRKAC :  적　　요
    ///  ARSEQ :  순　　번
    ///  ARYEAR :  년　　도
    /// </summary>
    public partial class TYACAB012I : TYBase
    {
        private string _ARDPMK;
        private string _ARYEAR;
        private string _ARSEQ;

        private TYData DAT01_ARHISAB;
        private TYData DAT01_ARHIGB;

        #region Description : 페이지 로드
        public TYACAB012I(string ARYEAR, string ARDPMK, string ARSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._ARYEAR = ARYEAR;
            this._ARDPMK = ARDPMK;
            this._ARSEQ  = ARSEQ;

            // 로그인 사번 가져오기
            this.DAT01_ARHISAB = new TYData("DAT01_ARHISAB", TYUserInfo.EmpNo);
            this.DAT01_ARHIGB  = new TYData("DAT01_ARHIGB", "");
        }

        private void TYACAB012I_Load(object sender, System.EventArgs e)
        {
            // 부서
            this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_ARHISAB);
            this.ControlFactory.Add(this.DAT01_ARHIGB);

            if (string.IsNullOrEmpty(this._ARDPMK) && string.IsNullOrEmpty(this._ARYEAR) && string.IsNullOrEmpty(this._ARSEQ))
            {
                this.TXT01_ARYEAR.SetReadOnly(false);
                this.CBH01_ARDPMK.SetReadOnly(false);
                this.TXT01_ARSEQ.SetReadOnly(false);

                this.TXT01_ARYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

                SetStartingFocus(this.TXT01_ARYEAR);
            }
            else
            {
                this.CBH01_ARDPMK.DummyValue = _ARYEAR + "0101";
                this.CBH01_ARDPMK.SetReadOnly(true);
                this.TXT01_ARYEAR.SetReadOnly(true);
                this.TXT01_ARSEQ.SetReadOnly(true);

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26D9D845",
                    this._ARYEAR,
                    this._ARDPMK,
                    this._ARSEQ
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                SetStartingFocus(this.DTP01_ARDATE);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 등록
            if (string.IsNullOrEmpty(this._ARDPMK) && string.IsNullOrEmpty(this._ARYEAR) && string.IsNullOrEmpty(this._ARSEQ))
            {
                this.DAT01_ARHIGB.SetValue("A");

                this.DbConnector.Attach("TY_P_AC_26D95843", this.ControlFactory, "01");
            }
            else // 수정
            {
                this.DAT01_ARHIGB.SetValue("C");

                // 수정
                this.DbConnector.Attach("TY_P_AC_26D9A844", this.ControlFactory, "01");
            }

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 등록
            if (string.IsNullOrEmpty(this._ARDPMK) && string.IsNullOrEmpty(this._ARYEAR) && string.IsNullOrEmpty(this._ARSEQ))
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26D9D845",
                    this.TXT01_ARYEAR.GetValue().ToString(),
                    this.CBH01_ARDPMK.GetValue().ToString(),
                    this.TXT01_ARSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_26D6A858");
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_ARJUNNO.GetValue().ToString() != "")
                {
                    // 전표번호 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_26D2M849",
                        this.TXT01_ARJUNNO.GetValue().ToString().Substring(0, 6),
                        this.TXT01_ARJUNNO.GetValue().ToString().Substring(6, 8),
                        this.TXT01_ARJUNNO.GetValue().ToString().Substring(14, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_AC_26D47852");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        // 현재 날짜 가져오는 부분
                        // DateTime.Now.ToString("yyyyMMdd")
                        this.TXT01_ARCHK1.SetValue("*");
                        this.TXT01_ARCHKDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                    }
                }
            }

            // 수정
            if (this._ARDPMK != "" && this._ARYEAR != "" && this._ARSEQ != "")
            {
                if (this.TXT01_ARJUNNO.GetValue().ToString() != "" && this.CBO01_ARGUBN.GetValue().ToString() != "1")
                {
                    this.ShowMessage("TY_M_AC_26D40854");
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
    }
}
