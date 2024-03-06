using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUTGB012S : TYBase
    {
        public string fsCHCHULIL = string.Empty;
        public string fsCHTKNO   = string.Empty;
        public string fsGUBUN    = string.Empty;        

        #region Description : 페이지 로드
        public TYUTGB012S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYUTGB012S(string sGUBUN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsGUBUN = sGUBUN.ToString();            
        }

        private void TYUTGB012S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6ADD5360.Initialize();

            this.DTP01_STIPHANG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDIPHANG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.CBH01_CHACTHJ.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sVNRPCODE = string.Empty;

            // 대표거래처 코드 가져오기
            sVNRPCODE = Get_VNCODE(this.CBH01_CHACTHJ.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_6ADD2359",
               sVNRPCODE.ToString(),
               this.CBH01_CHHWAMUL.GetValue().ToString(),
               Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
               Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
               this.CBH01_CHCHHJ.GetValue().ToString(),
               this.TXT01_CHCARNO.GetValue().ToString(),
               this.TXT01_CHCONTNUM.GetValue().ToString(),
               this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
               this.CBO01_JIWKTYPE.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6ADD5360.SetValue(dt);

                double dCHMTQTY = 0;

                for (int i = 0; i < FPS91_TY_S_UT_6ADD5360.CurrentRowCount; i++)
                {
                    dCHMTQTY = dCHMTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_6ADD5360.GetValue(i, "CHMTQTY").ToString()));
                }

                this.TXT01_CHMTQTY.SetValue((dCHMTQTY).ToString("0.000"));

                this.DbConnector.Attach
                   (
                   "TY_P_UT_7C5HN190",
                   sVNRPCODE.ToString(),
                   this.CBH01_CHHWAMUL.GetValue().ToString(),
                   Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                   Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                   this.CBH01_CHCHHJ.GetValue().ToString(),
                   this.TXT01_CHCARNO.GetValue().ToString(),
                   this.TXT01_CHCONTNUM.GetValue().ToString(),
                   this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                   this.CBO01_JIWKTYPE.GetValue().ToString()
                   );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_UT_7C5HO191.SetValue(dt);
                }

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_UT_6ADD5360.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_6ADD5360.GetValue(i, "CHCHTANK").ToString() != this.FPS91_TY_S_UT_6ADD5360.GetValue(i, "CHIPTANK").ToString())
                    {
                        this.FPS91_TY_S_UT_6ADD5360.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_UT_6ADD5360.SetValue(dt);

                //this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 차량번호 이벤트
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

        private void BTN61_CARNO_Click(object sender, EventArgs e)
        {
            TYUTGB011S popup = new TYUTGB011S(this.TXT01_CHCARNO.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_CHCARNO.SetValue(popup.fsCARNUMBER); // 차량번호
                SetFocus(this.TXT01_CHCARNO);
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6ADD5360_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (fsGUBUN == "CHULGO")
            {
                fsCHCHULIL = this.FPS91_TY_S_UT_6ADD5360.GetValue("CHCHULIL").ToString();
                fsCHTKNO   = this.FPS91_TY_S_UT_6ADD5360.GetValue("CHTKNO").ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
        }
        #endregion
    }
}