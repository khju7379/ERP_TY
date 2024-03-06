using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예적금관리내역 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.13 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24G14670 : 예적금관리 확인
    ///  TY_P_AC_24G1B671 : 예적금관리 등록
    ///  TY_P_AC_24G1C672 : 예적금관리 수정
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
    ///  E3CDAC : 계정코드
    ///  E3CDBK : 은행코드
    ///  E3CDDP : 부서코드
    ///  E3YNIT : 이자구분1:단2:복
    ///  E3DTCC : 제한해지일자
    ///  E3DTCL : 해약일자
    ///  E3DTET : 가입일자
    ///  E3DTIN : 불입일자
    ///  E3DTLT : 사용제한일자
    ///  E3DTSV : 만기일자
    ///  E3YYCH : 변동일자
    ///  E3AMCN : 총계약고
    ///  E3ANINMM : 매월불입액
    ///  E3NOAC : 계좌번호
    ///  E3NTTT : 총불입횟수
    ///  E3RKSV : 적요
    ///  E3RNCL : 해약사유
    ///  E3RNLT : 제한사유
    ///  E3RTSV : 이자율
    /// </summary>
    public partial class TYACDE001I : TYBase
    {
        private string _E3NOAC;
        private TYData DAT01_E3HISAB;

        #region Description : 페이지 로드
        public TYACDE001I(string E3NOAC)
        {
            InitializeComponent();

            this.SetPopupStyle();

            
            // 파라미터값 가져오기
            this._E3NOAC = E3NOAC;

            // 로그인 사번 가져오기
            this.DAT01_E3HISAB = new TYData("DAT01_E3HISAB", TYUserInfo.EmpNo);
            //this.DAT01_E3HISAB = new TYData("DAT01_E3HISAB", (object)"0311-M");
        }

        private void TYACDE001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_E3HISAB);

            if (string.IsNullOrEmpty(this._E3NOAC))
            {
                this.TXT01_E3NOAC.SetReadOnly(false);

                this.DTP01_E3DTET.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.DTP01_E3DTSV.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.DTP01_E3YYCH.SetValue("");
                this.DTP01_E3DTCL.SetValue("");
                DTP01_E3DTLT.SetValue("");
                DTP01_E3DTCC.SetValue("");
                DTP01_E3DTIN.SetValue("");

                SetStartingFocus(this.TXT01_E3NOAC);
            }
            else
            {
                this.TXT01_E3NOAC.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_24G14670", this._E3NOAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                SetStartingFocus(this.CBH01_E3CDBK);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._E3NOAC))
            {
                // INDEX 순번 가져오는 SP
                this.DbConnector.Attach("TY_P_AC_24G1B671", this.ControlFactory, "01");
            }
            else
                // 수정
                this.DbConnector.Attach("TY_P_AC_24G1C672", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this._E3NOAC))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2CE3T201",
                    this.TXT01_E3NOAC.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_26D6A858");
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void DTP01_E3DTET_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E3CDDP.DummyValue = this.DTP01_E3DTET.GetString();
        }
    }
}