using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 어음,수표용지 수령사항 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.27 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29R30328 : 어음수표용지 수령사항 등록
    ///  TY_P_AC_29R30330 : 어음수표용지 수령사항 번호체크
    ///  TY_P_AC_29R33329 : 어음수표용지 수령사항 최대값
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_29R33333 : 이미 등록된 용지번호 입니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  CONFIRM : 확인
    ///  F5CDBK : 은행코드
    ///  F5CDDP : 수령부서
    ///  F5NOAC : 계좌번호
    ///  F5KDNC : 종류
    ///  F5DTRC : 수령일자
    ///  F5NONC : 용지번호
    /// </summary>
    public partial class TYACEI017B : TYBase
    {
        private bool _Isloaded =false;

        #region Description : 폼 로드 이벤트
        public TYACEI017B()
        {
            InitializeComponent();


        }

        private void TYACEI017B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.CBH01_F5CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_F5CDBK_CodeBoxDataBinded);            

            this.DTP01_F5DTRC.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_F5CDDP.DummyValue = this.DTP01_F5DTRC.GetValue();

            TXT03_F5NONC.SetReadOnly(true);

            LBL53_F5NONC.Text = "매 수";
            LBL51_F5NONC.Text = "용지번호 From";
            LBL52_F5NONC.Text = "용지번호 To";
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sFrom = TXT01_F5NONC.GetValue().ToString();
            string sTo = TXT02_F5NONC.GetValue().ToString();

            this.DbConnector.CommandClear();
            for (long i = long.Parse(sFrom); i <= long.Parse(sTo); i++)
            {
                this.DbConnector.Attach("TY_P_AC_29R30328", this.CBH01_F5NOAC.GetValue(), i.ToString(), CBH01_F5CDDP.GetValue(), 
                                                            CBH01_F5CDDP.GetValue().ToString().Substring(0,1)+"00000",CBO01_F5KDNC.GetValue(),CBH01_F5CDBK.GetValue(),DTP01_F5DTRC.GetString());
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 매수 계산 이벤트
        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            string sFrom = TXT01_F5NONC.GetValue().ToString();
            string sTo   = TXT02_F5NONC.GetValue().ToString();

            if (sFrom != "" && sTo != "")
            {
                long lFrom = long.Parse(TXT01_F5NONC.GetValue().ToString());
                long lTo = long.Parse(TXT02_F5NONC.GetValue().ToString());

                long lResult = (lTo - lFrom) + 1;

                TXT03_F5NONC.SetValue(lResult.ToString());
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29R30330", this.TXT01_F5NONC.GetValue(), this.TXT02_F5NONC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                string sMin = dt.Rows[0]["MIN"].ToString().Trim();
                string sMax = dt.Rows[0]["MAX"].ToString().Trim();

                if (sMin != "" || sMax != "")
                {
                    this.ShowMessage("TY_M_AC_29R33333");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
           
        }
        #endregion
       
        #region Description :  CBH01_F5CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_F5CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_F5CDBK.GetValue().ToString();
            this.CBH01_F5NOAC.DummyValue = groupCode;
            this.CBH01_F5NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_F5NOAC.Initialize();
        }
        #endregion

        #region Description :  DTP01_F5DTRC_ValueChanged 이벤트
        private void DTP01_F5DTRC_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_F5CDDP.DummyValue = this.DTP01_F5DTRC.GetValue();
        }
        #endregion

        #region Description :  CBH01_F5NOAC_TextChanged 이벤트
        private void CBH01_F5NOAC_TextChanged(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29R33329", DateTime.Now.ToString("yyyyMMdd"));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAX"].ToString().Trim() != "")
                {
                    TXT01_F5NONC.SetValue(Convert.ToString(Convert.ToDecimal(dt.Rows[0]["MAX"].ToString().Trim()) + 1));
                }
                else
                {
                    TXT01_F5NONC.SetValue(DateTime.Now.ToString("yyyyMMdd").Trim() + "001");
                }
            }
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
