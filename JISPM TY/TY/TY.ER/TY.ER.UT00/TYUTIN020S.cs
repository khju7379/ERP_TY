using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using TY.Service.Library;
using TY.Service.Library.Controls;

using Shoveling2010.SmartClient.SystemUtility;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Component;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;

namespace TY.ER.UT00
{
    /// <summary>
    /// 출고지시조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.17 11:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66LG3327 : 출고지시 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66LG4329 : 출고지시 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  CNHWAMUL : 화물
    ///  DATE : 일자
    /// </summary>
    public partial class TYUTIN020S : TYBase
    {
        public TYUTIN020S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYUTIN020S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now);
            this.DTP01_EDDATE.SetValue(System.DateTime.Now);

            SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null); 
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dJISTMTQTY = 0;
            double dJISTLTQTY = 0;
            double dJICHQTY = 0;

            if (UP_KeyCheck())
            {
                string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

                // 대표거래처 코드 가져오기
                sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

                DataTable  dt = new DataTable();

                this.FPS91_TY_S_UT_66LG4329.Initialize();

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {
                    this.DbConnector.Attach("TY_P_UT_66LG3327", this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                sHWAJU,
                                                                this.CBH01_CNHWAMUL.GetValue().ToString(),
                                                                this.TXT01_JITANKNO.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString(),
                                                                this.TXT01_CHCARNO.GetValue().ToString(),
                                                                this.TXT01_JICONTNUM.GetValue().ToString());
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_66LGY333", this.DTP01_STDATE.GetString(),
                                                                this.DTP01_EDDATE.GetString(),
                                                                this.CBH01_CNHWAMUL.GetValue().ToString(),
                                                                this.TXT01_JITANKNO.GetValue().ToString().Trim(),
                                                                this.CBH01_CHCHHJ.GetValue().ToString(),
                                                                this.TXT01_CHCARNO.GetValue().ToString(),
                                                                this.TXT01_JICONTNUM.GetValue().ToString());
                }

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_66LG4329.SetValue(dt);

                for (int i = 0; i < FPS91_TY_S_UT_66LG4329.CurrentRowCount; i++)
                {
                    dJISTMTQTY = dJISTMTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_66LG4329.GetValue(i, "JISTMTQTY").ToString()));
                    dJISTLTQTY = dJISTLTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_66LG4329.GetValue(i, "JISTLTQTY").ToString()));
                    dJICHQTY = dJICHQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_66LG4329.GetValue(i, "JICHQTY").ToString()));
                }

                // 지시수량(MT)
                this.TXT01_JISTMTQTY.SetValue((dJISTMTQTY).ToString("0.000"));
                // 지시수량(L)
                this.TXT01_JISTLTQTY.SetValue((dJISTLTQTY).ToString("0,000"));

                // 출고수량
                this.TXT01_JICHQTY.SetValue((dJICHQTY).ToString("0.000"));
            }
        }
        #endregion

        #region Description : 키 체크
        private bool UP_KeyCheck()
        {
            bool bVal = true;

            if (this.DTP01_STDATE.GetValue().ToString() == "" && this.DTP01_EDDATE.GetValue().ToString() == "" && this.CBH01_CNHWAJU.GetValue().ToString() == "" && this.CBH01_CNHWAMUL.GetValue().ToString() == "" &&
                this.TXT01_JITANKNO.GetValue().ToString().Trim() == "" && this.CBH01_CHCHHJ.GetValue().ToString() == "" && this.TXT01_CHCARNO.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("검색조건을 입력 하세요.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                bVal = false;
            }

            return bVal;
        }
        #endregion

        #region Description : 차량번호 이벤트
        private void BTN61_CARNO_Click(object sender, EventArgs e)
        {
            TYUTGB011S popup = new TYUTGB011S(this.TXT01_CHCARNO.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHCARNO.SetValue(popup.fsCARNUMBER); // 차량번호
                SetFocus(this.TXT01_CHCARNO);
            }
        }

        private void TXT01_CHCARNO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB011S popup = new TYUTGB011S(this.TXT01_CHCARNO.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_CHCARNO.SetValue(popup.fsCARNUMBER); // 차량번호
                    SetFocus(this.TXT01_CHCARNO);
                }
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_66LG4329_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                string sJIYYMM = this.FPS91_TY_S_UT_66LG4329.GetValue("JISI").ToString().Substring(0, 8);
                string sJISEQ  = this.FPS91_TY_S_UT_66LG4329.GetValue("JISI").ToString().Substring(9, 3);

                // 탭 메뉴 생성하는 소스
                //TabPage_Add(new TYUTIN013I(sJIYYMM, sJISEQ), "TY69MAY210", "출고지시관리", "TYUTIN013I");

                if (this.OpenModalPopup(new TYUTIN013I(sJIYYMM, sJISEQ)) != System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else if (e.Column == 12)
            {
                if (this.FPS91_TY_S_UT_66LG4329.GetValue("CHUL").ToString() != "" &&
                    this.FPS91_TY_S_UT_66LG4329.GetValue("CHUL").ToString().Length == 12)
                {
                    string sCHCHULIL = this.FPS91_TY_S_UT_66LG4329.GetValue("CHUL").ToString().Substring(0, 8);
                    string sCHTKNO = this.FPS91_TY_S_UT_66LG4329.GetValue("CHUL").ToString().Substring(9, 3);

                    if (this.OpenModalPopup(new TYUTIN006I(sCHCHULIL, sCHTKNO)) != System.Windows.Forms.DialogResult.OK)
                        this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion
    }
}
