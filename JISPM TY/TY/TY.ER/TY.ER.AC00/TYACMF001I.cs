using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 통장거래내역관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.10.30 16:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2AU52929 : 통장거래내역관리 등록
    ///  TY_P_AC_2AU5A930 : 통장거래내역 수정
    ///  TY_P_AC_2AU5A931 : 통장거래내역 삭제
    ///  TY_P_AC_2AU5K932 : 통장거래내역 순번 등록
    ///  TY_P_AC_2AU5K933 : 통장거래내역 순번 수정
    ///  TY_P_AC_2AU5M934 : 통장거래내역 순번 최대값 조회
    ///  TY_P_AC_2AV9K940 : 통장거래내역 일,월 내역 정리 SP
    ///  TY_P_AC_2AVBA948 : 통장거래내역 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  B1CDBK : 은  행
    ///  B1GURA : 거래구분
    ///  B1NOAC : 계좌번호
    ///  B1IOGB : 입지명
    ///  B1YNGB : 처리명
    ///  B1AMIO : 거래금액
    ///  B1NAME :  입금인
    ///  B1NOSQ :  거래순번
    /// </summary>
    public partial class TYACMF001I : TYBase
    {
        private TYData DAT01_B1HISAB;

        private bool _Isloaded = false;

        private string fsB1CDBK;
        private string fsB1NOAC;
        private string fsB1DATE;
        private string fsB1NOSQ;

        #region Description : 폼 로드 이벤트
        public TYACMF001I(string sB1CDBK, string sB1NOAC, string sB1DATE, string sB1NOSQ)
        {
            InitializeComponent();
            this.SetPopupStyle();

            this.fsB1CDBK = sB1CDBK;
            this.fsB1NOAC = sB1NOAC;
            this.fsB1DATE = sB1DATE;
            this.fsB1NOSQ = sB1NOSQ;

            this.DAT01_B1HISAB = new TYData("DAT01_B1HISAB", TYUserInfo.EmpNo);
        }

        private void TYACMF001I_Load(object sender, System.EventArgs e)
        {
            

            this.ControlFactory.Add(this.DAT01_B1HISAB);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.CBH01_B1CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_B1CDBK_CodeBoxDataBinded);

            this.CBH01_B1CDBK.OnCodeBoxDataBinded(null, null); 

            if (string.IsNullOrEmpty(this.fsB1CDBK))
            {
                this.DTP01_B1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));    

                this.CBH01_B1CDBK.SetReadOnly(false);
                //this.CBH01_B1NOAC.SetReadOnly(false);
                this.DTP01_B1DATE.SetReadOnly(false);
                this.TXT01_B1NOSQ.SetReadOnly(true);

                this.SetStartingFocus(this.CBH01_B1CDBK.CodeText);
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AVBA948", this.fsB1CDBK, this.fsB1NOAC,this.fsB1DATE,this.fsB1NOSQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                this.CBH01_B1CDBK.SetReadOnly(true);
                this.CBH01_B1NOAC.SetReadOnly(true);
                this.DTP01_B1DATE.SetReadOnly(true);
                this.TXT01_B1NOSQ.SetReadOnly(true);

                this.SetStartingFocus(this.CBO01_B1IOGB); 
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

            if (string.IsNullOrEmpty(this.fsB1CDBK))  //등록
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AU52929", this.ControlFactory,"01");
                this.DbConnector.ExecuteTranQuery();                
            }
            else //수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AU5A930", this.ControlFactory, "01");
                this.DbConnector.ExecuteTranQuery(); 
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2AV9K940", this.CBH01_B1CDBK.GetValue().ToString(), this.CBH01_B1NOAC.GetValue().ToString(), this.DTP01_B1DATE.GetString().ToString(),
                                                        this.TXT01_B1NOSQ.GetValue().ToString(), this.DAT01_B1HISAB.GetValue().ToString(), "A", sOUTMSG.ToString());

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD873");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

                //this.SetFocus(this.CBH01_B1CDBK.CodeText);

                //this.CBH01_B1CDBK.SetValue("");
                //this.CBH01_B1NOAC.SetValue("");
                //this.TXT01_B1NOSQ.SetValue("");
                //this.TXT01_B1AMIO.SetValue("");
                //this.TXT01_B1NAME.SetValue("");

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
            // 금액 체크
            if (this.TXT01_B1AMIO.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_382BD291");
                e.Successed = false;
                this.SetFocus(this.TXT01_B1AMIO);
                return;
            }

            //은행,계좌번호 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_667F1067", this.CBH01_B1CDBK.GetValue(),  this.CBH01_B1NOAC.GetValue());
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iCnt == 0)
            {
                this.ShowCustomMessage("은행코드과 계좌번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.CBH01_B1CDBK);
                return;
            }

            // 년말 이월후 이전 거래일자로 입력 안되도록 체크
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_883B2500", Get_Date(this.DTP01_B1DATE.GetValue().ToString()).Substring(0, 4).ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_883BA501");
                e.Successed = false;
                this.SetFocus(this.DTP01_B1DATE);
                return;
            }


            //순번 
            if (string.IsNullOrEmpty(this.fsB1CDBK))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AU5M934", this.CBH01_B1CDBK.GetValue(), this.DTP01_B1DATE.GetString(), this.CBH01_B1NOAC.GetValue());
                string sSeq = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (Convert.ToInt16(sSeq.Trim()) == 1)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AU5K932", this.CBH01_B1CDBK.GetValue(), this.DTP01_B1DATE.GetString(), this.CBH01_B1NOAC.GetValue(), sSeq.Trim(), Employer.EmpNo);
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AU5K933", sSeq.Trim(), Employer.EmpNo, this.CBH01_B1CDBK.GetValue(), this.DTP01_B1DATE.GetString(), this.CBH01_B1NOAC.GetValue());
                }
                this.DbConnector.ExecuteTranQuery();

                this.TXT01_B1NOSQ.SetValue(sSeq);  
            }

            

            if (CBO01_B1IOGB.GetValue().ToString() == "1")
            {
                CBH01_B1GURA.SetValue("10"); 
            }
            else
            {
                CBH01_B1GURA.SetValue("30"); 
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 계좌번호 CBH01_B1CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_B1CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {

            string groupCode = this.CBH01_B1CDBK.GetValue().ToString();

            this.CBH01_B1NOAC.DummyValue = groupCode;
            this.CBH01_B1NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_B1NOAC.Initialize();
        }
        #endregion
    }
}
