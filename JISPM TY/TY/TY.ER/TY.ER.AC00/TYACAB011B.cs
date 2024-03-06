using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 입금표 생성작업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.12 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26C3Y830 : 입금표 생성
    ///  TY_P_AC_26C6O832 : 입금표 생성 체크
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_26C6Q834 : 시작 순번이후 자료가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  ARCDSB :  수령자
    ///  ARLOCAL :  지　　역
    ///  ARDATE :  수령일자
    ///  ARCNT : 발행매수
    ///  ARYEAR :  년　　도
    ///  EDLOCAL : 발행지역
    ///  EDSEQ : 발행순번
    ///  EDYEAR : 발행년도
    ///  STLOCAL : 발행지역
    ///  STSEQ : 발행순번
    ///  STYEAR : 발행년도
    /// </summary>
    public partial class TYACAB011B : TYBase
    {
        #region Description : 페이지 로드
        public TYACAB011B()
        {
            InitializeComponent();
        }

        private void TYACAB011B_Load(object sender, System.EventArgs e)
        {
            // 부서
            this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.TXT01_ARYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

            this.TXT01_STDPMK.SetReadOnly(true);
            this.TXT01_STYEAR.SetReadOnly(true);
            this.TXT01_STSEQ.SetReadOnly(true);
            this.TXT01_EDDPMK.SetReadOnly(true);
            this.TXT01_EDYEAR.SetReadOnly(true);
            this.TXT01_EDSEQ.SetReadOnly(true);

            SetStartingFocus(this.TXT01_ARYEAR);
        }
        #endregion

        #region Description : 배치 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTSTARSEQ = string.Empty;
            string sOUTEDARSEQ = string.Empty;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26C3Y830",
                this.TXT01_ARYEAR.GetValue().ToString(),
                this.CBH01_ARDPMK.GetValue().ToString(),
                this.TXT01_STSEQ.GetValue().ToString(),
                this.DTP01_ARDATE.GetValue().ToString(),
                this.CBH01_ARCDSB.GetValue().ToString(),
                this.TXT01_ARCNT.GetValue().ToString(),
                ""
                );

            sOUTEDARSEQ = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.TXT01_STDPMK.SetValue(this.CBH01_ARDPMK.GetValue());
            this.TXT01_EDDPMK.SetValue(this.CBH01_ARDPMK.GetValue());
            this.TXT01_STYEAR.SetValue(this.TXT01_ARYEAR.GetValue());
            this.TXT01_EDYEAR.SetValue(this.TXT01_ARYEAR.GetValue());
            this.TXT01_EDSEQ.SetValue(sOUTEDARSEQ.ToString());
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.TXT01_STSEQ.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_26C6O832",
                    this.CBH01_ARDPMK.GetValue(),
                    this.TXT01_ARYEAR.GetValue(),
                    this.TXT01_STSEQ.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_26C6Q834");
                    e.Successed = false;
                    return;
                }
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2BN69624",
                this.CBH01_ARDPMK.GetValue(),
                this.TXT01_ARYEAR.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_STSEQ.SetValue(dt.Rows[0]["ARSEQ"].ToString());
            }
        }
        #endregion

        private void TXT01_ARYEAR_TextChanged(object sender, EventArgs e)
        {
            // 부서
            //this.CBH01_ARDPMK.DummyValue = this.DTP01_ARDATE.GetValue().ToString();

            if (TXT01_ARYEAR.GetValue().ToString() != "")
            {
                this.CBH01_ARDPMK.DummyValue = TXT01_ARYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }

        }
    }
}