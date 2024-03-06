using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 하역료 단가 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_675E1569 : 하역료 단가 수정
    ///  TY_P_UT_675E1570 : 하역료 단가 삭제
    ///  TY_P_UT_675E3567 : 하역료 단가 조회
    ///  TY_P_UT_675E7568 : 하역료 단가 등록
    ///  TY_P_UT_675EF572 : 하역료 단가 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_675E4571 : 하역료 단가 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CHYMHWAJU : 화주
    ///  CHYMDATE : 기준일자
    ///  DATE : 일자
    ///  AFSEQ : 순번
    ///  CHYM1AMT : 1구간단가
    ///  CHYM1QTY : 1구간종료량
    ///  CHYM2AMT : 2구간단가
    ///  CHYM2QTY : 2구간종료량
    ///  CHYM3AMT : 3구간단가
    ///  CHYM3QTY : 3구간종료량
    ///  CHYM4AMT : 4구간단가
    ///  CHYM4QTY : 4구간종료량
    ///  CHYM5AMT : 5구간단가
    ///  CHYM5QTY : 5구간종료량
    ///  CHYMBASEAMT : 기본요금
    ///  CHYMBIGO : 비고
    ///  CHYMHALYUL : 할인요율
    ///  CHYMINAMT : 내항단가
    ///  CHYMOUTAMT : 외항단가
    ///  CHYMSEQ : 순번
    ///  EICHYMD : 년월일
    /// </summary>
    public partial class TYUTIN004I : TYBase
    {
        private string fsCHYMDATE;
        private string fsCHYMSEQ;

        public TYUTIN004I(string sCHYMDATE, string sCHYMSEQ)
        {
            InitializeComponent();

            fsCHYMDATE = sCHYMDATE;
            fsCHYMSEQ = sCHYMSEQ;
        }

        #region Descriptino : 페이지 로드
        private void TYUTIN004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.TXT01_CHYMSEQ.SetReadOnly(true);
            UP_Init();

            if (string.IsNullOrEmpty(fsCHYMDATE) && string.IsNullOrEmpty(fsCHYMSEQ))
            {
                this.DTP01_CHYMDATE.SetReadOnly(false);
                

                SetStartingFocus(this.DTP01_CHYMDATE);
            }
            else
            {
                this.DTP01_CHYMDATE.SetReadOnly(true);

                UP_SetText(fsCHYMDATE, fsCHYMSEQ);

                SetStartingFocus(this.CBH01_CHYMHWAJU.CodeText);
            }

            
        }
        #endregion

        #region Descriptino : 닫기버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descriptino : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sSEQ = string.Empty;

                // 수정
                if (UP_KEY_Check())
                {
                    sSEQ = this.TXT01_CHYMSEQ.GetValue().ToString();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_675E1569", this.CBH01_CHYMHWAJU.GetValue().ToString(),
                                                                this.TXT01_CHYMBASEAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM1QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM1AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM2QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM2AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM3QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM3AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM4QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM4AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM5QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM5AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMBIGO.GetValue().ToString(),
                                                                this.TXT01_CHYMINAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMOUTAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMHALYUL.GetValue().ToString(),
                                                                this.TXT01_CHYMSEAMT.GetValue().ToString(),
                                                                "C",
                                                                TYUserInfo.EmpNo,
                                                                this.DTP01_CHYMDATE.GetString(),
                                                                sSEQ
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }
                // 신규 등록
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_675EF572", this.DTP01_CHYMDATE.GetString());

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    sSEQ = dt.Rows[0]["SEQ"].ToString();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_675E7568", this.DTP01_CHYMDATE.GetString(),
                                                                sSEQ,
                                                                this.CBH01_CHYMHWAJU.GetValue().ToString(),
                                                                this.TXT01_CHYMBASEAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM1QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM1AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM2QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM2AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM3QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM3AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM4QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM4AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM5QTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYM5AMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMBIGO.GetValue().ToString(),
                                                                this.TXT01_CHYMINAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMOUTAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_CHYMHALYUL.GetValue().ToString(),
                                                                this.TXT01_CHYMSEAMT.GetValue().ToString(),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }
                UP_SetText(this.DTP01_CHYMDATE.GetString(), Set_Fill2(sSEQ));
                this.DTP01_CHYMDATE.SetReadOnly(true);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }

        }
        #endregion

        #region Description : 필드 값 입력
        private void UP_SetText(String sCHYMDATE, String sCHYMSEQ)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_67CFA719", sCHYMDATE, sCHYMSEQ);

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DTP01_CHYMDATE.SetValue(dt.Rows[0]["CHYMDATE"].ToString());
                this.TXT01_CHYMSEQ.Text = dt.Rows[0]["CHYMSEQ"].ToString();

                this.CBH01_CHYMHWAJU.SetValue(dt.Rows[0]["CHYMHWAJU"].ToString());
                this.TXT01_CHYMBASEAMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYMBASAMT"].ToString()));
                this.TXT01_CHYM1QTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["CHYM1QTY"].ToString()));
                this.TXT01_CHYM1AMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYM1AMT"].ToString()));
                this.TXT01_CHYM2QTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["CHYM2QTY"].ToString()));
                this.TXT01_CHYM2AMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYM2AMT"].ToString()));
                this.TXT01_CHYM3QTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["CHYM3QTY"].ToString()));
                this.TXT01_CHYM3AMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYM3AMT"].ToString()));
                this.TXT01_CHYM4QTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["CHYM4QTY"].ToString()));
                this.TXT01_CHYM4AMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYM4AMT"].ToString()));
                this.TXT01_CHYM5QTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["CHYM5QTY"].ToString()));
                this.TXT01_CHYM5AMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYM5AMT"].ToString()));
                this.TXT01_CHYMBIGO.Text = dt.Rows[0]["CHYMBIGO"].ToString();

                this.TXT01_CHYMINAMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYMINAMT"].ToString()));
                this.TXT01_CHYMOUTAMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYMOUTAMT"].ToString()));
                this.TXT01_CHYMHALYUL.Text = dt.Rows[0]["CHYMHALYUL"].ToString();

                this.TXT01_CHYMSEAMT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["CHYMSEAMT"].ToString()));
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Init()
        {
            this.DTP01_CHYMDATE.SetValue("");
            this.TXT01_CHYMSEQ.Text = "";

            this.CBH01_CHYMHWAJU.SetValue("");
            this.TXT01_CHYMBASEAMT.Text = "";
            this.TXT01_CHYM1QTY.Text = "";
            this.TXT01_CHYM1AMT.Text = "";
            this.TXT01_CHYM2QTY.Text = "";
            this.TXT01_CHYM2AMT.Text = "";
            this.TXT01_CHYM3QTY.Text = "";
            this.TXT01_CHYM3AMT.Text = "";
            this.TXT01_CHYM4QTY.Text = "";
            this.TXT01_CHYM4AMT.Text = "";
            this.TXT01_CHYM5QTY.Text = "";
            this.TXT01_CHYM5AMT.Text = "";
            this.TXT01_CHYMBIGO.Text = "";

            this.TXT01_CHYMINAMT.Text = "";
            this.TXT01_CHYMOUTAMT.Text = "";
            this.TXT01_CHYMHALYUL.Text = "";

            this.TXT01_CHYMSEAMT.Text = "";
        }
        #endregion

        #region Description : 키 체크
        private bool UP_KEY_Check()
        {
            bool bRst = false;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67CFA719", this.DTP01_CHYMDATE.GetString(), this.TXT01_CHYMSEQ.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRst = true;
            }

            return bRst;
        }
        #endregion
    }
}
