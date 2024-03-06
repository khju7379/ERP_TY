using System;
using System.Data;
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
    public partial class TYUTGB015S : TYBase
    {
        public string fsIPHANG    = string.Empty;
        public string fsBONSUN    = string.Empty;
        public string fsHWAJU     = string.Empty;
        public string fsHWAMUL    = string.Empty;
        public string fsBLNO      = string.Empty;
        public string fsMSNSEQ    = string.Empty;
        public string fsHSNSEQ    = string.Empty;
        public string fsCUSTIL    = string.Empty;
        public string fsCHASU     = string.Empty;
        public string fsACTHJ     = string.Empty;
        public string fsYDHWAJU   = string.Empty;
        public string fsYSHWAJU   = string.Empty;
        public string fsYSHWAJUNM = string.Empty;
        public string fsJGHWAJU   = string.Empty;
        public string fsJGHWAJUNM = string.Empty;
        public string fsYSDATE    = string.Empty;
        public string fsYSSEQ     = string.Empty;
        public string fsYDSEQ     = string.Empty;
        public string fsCHTANK    = string.Empty;

        public string fsVNRPCODE  = string.Empty;
        public string fsVNCODE    = string.Empty;

        public string fsCJCUQTY   = string.Empty;
        public string fsCJCHQTY   = string.Empty;
        public string fsCJJEQTY   = string.Empty;

        public string fsJUNG      = string.Empty;

        public string fsDJPOQTY   = string.Empty;
        public string fsDJCHQTY   = string.Empty;
        public string fsDJJEQTY   = string.Empty;

        public string fsJIYSHWAJU = string.Empty;

        #region Description : 페이지 로드
        public TYUTGB015S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYUTGB015S(string sJIYSHWAJU)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsJIYSHWAJU = sJIYSHWAJU.ToString();
        }

        private void TYUTGB015S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6C9EJ045.Initialize();

            this.CBH01_SHWAJU.SetValue(fsJIYSHWAJU.ToString());

            SetStartingFocus(this.CBH01_SHWAJU.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAMUL = string.Empty;

            sHWAMUL = this.CBO01_SHWAMUL.GetValue().ToString().Substring(0, 3);

            if (sHWAMUL.ToString() == "ALL")
            {
                sHWAMUL = "";
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_6C9EE044",
               fsVNRPCODE.ToString(),
               sHWAMUL.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6C9EJ045.SetValue(dt);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6C9EJ045_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsIPHANG  = "";
            fsBONSUN  = "";
            fsHWAJU   = "";
            fsHWAMUL  = "";
            fsBLNO    = "";
            fsMSNSEQ  = "";
            fsHSNSEQ  = "";
            fsCUSTIL  = "";
            fsCHASU   = "";
            fsACTHJ   = "";
            fsYDHWAJU = "";
            fsYSHWAJU = "";
            fsJGHWAJU = "";
            fsYSDATE  = "";
            fsYSSEQ   = "";
            fsYDSEQ   = "";

            fsJUNG    = "";

            fsCJCUQTY = "";
            fsCJCHQTY = "";
            fsCJJEQTY = "";

            fsDJPOQTY = "";
            fsDJCHQTY = "";
            fsDJJEQTY = "";

            fsCHTANK  = "";

            fsIPHANG  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJIPHANG").ToString();
            fsBONSUN  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJBONSUN").ToString();
            fsHWAJU   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJHWAJU").ToString();
            fsHWAMUL  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJHWAMUL").ToString();
            fsBLNO    = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJBLNO").ToString();
            fsMSNSEQ  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJMSNSEQ").ToString();
            fsHSNSEQ  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJHSNSEQ").ToString();

            fsCUSTIL  = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJCUSTIL").ToString();
            fsCHASU   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJCHASU").ToString();
            fsACTHJ   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJACTHWAJU").ToString();
            fsYDHWAJU = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJYDHWAJU").ToString();
            fsYSHWAJU = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJYSHWAJU").ToString();

            // 재고화주
            fsJGHWAJU   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJJGHWAJU").ToString();
            fsJGHWAJUNM = this.FPS91_TY_S_UT_6C9EJ045.GetValue("HJDESC2").ToString();

            fsYSDATE    = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJYSDATE").ToString();
            fsYSSEQ     = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJYSSEQ").ToString();
            fsYDSEQ     = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJYDSEQ").ToString();

            fsCJCUQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("CJCUQTY").ToString();
            fsCJCHQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("CJCHQTY").ToString();
            fsCJJEQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("CJJEQTY").ToString();

            fsJUNG      = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJJUNG").ToString();

            fsDJPOQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJPOQTY").ToString();
            fsDJCHQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJCHQTY").ToString();
            fsDJJEQTY   = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DJJEQTY").ToString();

            fsCHTANK    = this.FPS91_TY_S_UT_6C9EJ045.GetValue("DPCHTANK").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 화주에 따른 화물 가져오기
        private void CBH01_SHWAJU_TextChanged(object sender, EventArgs e)
        {
            UP_Get_HWAMUL();
        }

        private void CBH01_SHWAJU_Leave(object sender, EventArgs e)
        {
            UP_Get_HWAMUL();
        }

        private void CBH01_SHWAJU_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            UP_Get_HWAMUL();
        }

        private void CBH01_SHWAJU_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            UP_Get_HWAMUL();
        }

        private void UP_Get_HWAMUL()
        {
            this.CBO01_SHWAMUL.Initialize();

            fsVNRPCODE = this.CBH01_SHWAJU.GetValue().ToString();
            fsHWAJU = this.CBH01_SHWAJU.GetValue().ToString();

            if (this.CBH01_SHWAJU.GetValue().ToString().Length == 3)
            {
                DataTable dt = new DataTable();

                // 대표거래처 코드 가져오기
                fsVNRPCODE = Get_VNCODE(this.CBH01_SHWAJU.GetValue().ToString());

                // 거래처에 재고 있는 화물 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7BHHT049", fsVNRPCODE.ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                // 콤보박스에 바인드
                this.CBO01_SHWAMUL.DataBind(dt, false);
            }
            else
            {
                this.CBO01_SHWAMUL.Initialize();
            }
        }
        #endregion

        private void CBH01_SHWAJU_Enter(object sender, EventArgs e)
        {
            UP_Get_HWAMUL();
        }
    }
}