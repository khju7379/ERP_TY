using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 통장거래내역관리(외화) 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.01 15:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B11H981 : 통장거래내역관리(외화) 등록
    ///  TY_P_AC_2B11I982 : 통장거래내역(외화) 수정
    ///  TY_P_AC_2B11S987 : 통장거래내역(외화) 일,월 내역 정리 SP
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
    ///  H1CDBK : 은 행
    ///  H1EXGB : 외화구분
    ///  H1NOAC : 계좌번호
    ///  H1IOGB : 입지명
    ///  H1DATE : 거래일자
    ///  H1AMIO : 거래외화금액
    ///  H1AMJAN : 거래후잔액
    ///  H1AMWON : 원화금액
    ///  H1NAME : 입금인
    ///  H1NOSQ : 거래순번
    ///  H1YUL : 환　율
    /// </summary>
    public partial class TYACMF002I : TYBase
    {
        private TYData DAT01_H1HISAB;

        private bool _Isloaded = false;

        private string fsH1CDBK;
        private string fsH1NOAC;
        private string fsH1DATE;
        private string fsH1NOSQ;

        #region Description : 폼 로드 이벤트
        public TYACMF002I(string sH1CDBK, string sH1NOAC, string sH1DATE, string sH1NOSQ)
        {
            
            InitializeComponent();
            this.SetPopupStyle();

            this.fsH1CDBK = sH1CDBK;
            this.fsH1NOAC = sH1NOAC;
            this.fsH1DATE = sH1DATE;
            this.fsH1NOSQ = sH1NOSQ;

            this.DAT01_H1HISAB = new TYData("DAT01_H1HISAB", TYUserInfo.EmpNo);
        }

        private void TYACMF002I_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT01_H1HISAB);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.CBH01_H1CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_H1CDBK_CodeBoxDataBinded);

            this.CBH01_H1CDBK.OnCodeBoxDataBinded(null, null);

            if (string.IsNullOrEmpty(this.fsH1CDBK))
            {
                this.DTP01_H1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                this.CBH01_H1CDBK.SetReadOnly(false);
                this.CBH01_H1NOAC.SetReadOnly(false);
                this.DTP01_H1DATE.SetReadOnly(false);
                this.TXT01_H1NOSQ.SetReadOnly(true);

                this.SetStartingFocus(this.CBH01_H1CDBK.CodeText);
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B14S994", this.fsH1CDBK, this.fsH1NOAC, this.fsH1DATE, this.fsH1NOSQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                this.CBH01_H1CDBK.SetReadOnly(true);
                this.CBH01_H1NOAC.SetReadOnly(true);
                this.DTP01_H1DATE.SetReadOnly(true);
                this.TXT01_H1NOSQ.SetReadOnly(true);

                this.SetStartingFocus(this.CBO01_H1IOGB);
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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            if (string.IsNullOrEmpty(this.fsH1CDBK))  //등록
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B11H981", this.ControlFactory, "01");
                this.DbConnector.ExecuteTranQuery();
            }
            else //수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B11I982", this.ControlFactory, "01");
                this.DbConnector.ExecuteTranQuery();
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2B11S987", this.CBH01_H1CDBK.GetValue().ToString(), this.CBH01_H1NOAC.GetValue().ToString(), this.DTP01_H1DATE.GetString().ToString(),
                                                        this.TXT01_H1NOSQ.GetValue().ToString(), this.CBH01_H1EXGB.GetValue().ToString(),this.DAT01_H1HISAB.GetValue().ToString(), "A", sOUTMSG.ToString());

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD873");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

                //this.CBH01_H1CDBK.SetValue("");
                //this.CBH01_H1NOAC.SetValue("");
                //this.TXT01_H1NOSQ.SetValue("");
                //this.CBH01_H1EXGB.SetValue("");
                //this.TXT01_H1YUL.SetValue("");
                //this.TXT01_H1AMIO.SetValue("");
                //this.TXT01_H1AMWON.SetValue("");
                //this.TXT01_H1AMJAN.SetValue("");
                //this.TXT01_H1NAME.SetValue("");

            }
            else
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            // 환율 체크
            if (this.TXT01_H1YUL.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_382BK293");
                e.Successed = false;
                this.SetFocus(this.TXT01_H1YUL);
                return;
            }

            // 외화거래금액
            if (this.TXT01_H1AMIO.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_382BD291");
                e.Successed = false;
                this.SetFocus(this.TXT01_H1AMIO);
                return;
            }

            // 원화금액
            if (this.TXT01_H1AMIO.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_382BD291");
                e.Successed = false;
                this.SetFocus(this.TXT01_H1AMWON);
                return;
            }

            // 년말 이월후 이전 거래일자로 입력 안되도록 체크
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_883C2502", Get_Date(this.DTP01_H1DATE.GetValue().ToString()).Substring(0, 4).ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_883BA501");
                e.Successed = false;
                this.SetFocus(this.DTP01_H1DATE);
                return;
            }

            //순번 
            if (string.IsNullOrEmpty(this.fsH1CDBK))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AU5M934", this.CBH01_H1CDBK.GetValue(), this.DTP01_H1DATE.GetString(), this.CBH01_H1NOAC.GetValue());
                string sSeq = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (Convert.ToInt16(sSeq.Trim()) == 1)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AU5K932", this.CBH01_H1CDBK.GetValue(), this.DTP01_H1DATE.GetString(), this.CBH01_H1NOAC.GetValue(), sSeq.Trim(), this.DAT01_H1HISAB.GetValue() );
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AU5K933", sSeq.Trim(), Employer.EmpNo, this.CBH01_H1CDBK.GetValue(), this.DTP01_H1DATE.GetString(), this.CBH01_H1NOAC.GetValue());
                }
                this.DbConnector.ExecuteTranQuery();

                this.TXT01_H1NOSQ.SetValue(sSeq);
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 계좌번호 CBH01_H1CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_H1CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_H1CDBK.GetValue().ToString();
            this.CBH01_H1NOAC.DummyValue = groupCode;
            this.CBH01_H1NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_H1NOAC.Initialize();
        }
        #endregion

        #region  Description : 계좌번호 CBH01_H1CDBK_CodeBoxDataBinded 이벤트
        private void TXT01_H1AMWON_Enter(object sender, EventArgs e)
        {
            double dWonAmt = 0;

            dWonAmt = Convert.ToDouble(Get_Numeric(TXT01_H1AMIO.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(TXT01_H1YUL.GetValue().ToString()));

            dWonAmt = Math.Floor(dWonAmt);

            this.TXT01_H1AMWON.SetValue(dWonAmt.ToString());              
        }
        #endregion
    }
}
