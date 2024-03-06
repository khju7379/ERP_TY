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
    public partial class TYUTGB008S : TYBase
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
        public string fsYNGUBUN   = string.Empty;

        public string fsVNRPCODE = string.Empty;
        public string fsVNCODE   = string.Empty;

        public string fsCJCUQTY  = string.Empty;
        public string fsCJCHQTY  = string.Empty;
        public string fsCJJEQTY  = string.Empty;
        public string fsDPJEQTY  = string.Empty;



        public string fsIPMTQTY  = string.Empty;
        public string fsIPPAQTY  = string.Empty;
        public string fsIPCHQTY  = string.Empty;
        public string fsIPJEQTY  = string.Empty;
        public string fsIPJANQTY = string.Empty;

        public string fsTANKNO    = string.Empty;
        public string fsSVJGQTY   = string.Empty;
        public string fsSVMTQTY   = string.Empty;


        public string fsJIYSHWAJU = string.Empty;

        #region Description : 페이지 로드
        public TYUTGB008S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYUTGB008S(string sJIYSHWAJU)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsJIYSHWAJU = sJIYSHWAJU.ToString();
        }

        private void TYUTGB008S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_699EV135.Initialize();

            this.CBH01_SHWAJU.SetValue(fsJIYSHWAJU.ToString());

            SetStartingFocus(this.CBH01_SHWAJU.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dCJCUQTY = 0;
            double dCJJEQTY = 0;
            double dDRUJQTY = 0;

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
               "TY_P_UT_6AAD0330",
               fsVNRPCODE.ToString(),
               sHWAMUL.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_699EV135.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(FPS91_TY_S_UT_699EV135);

                dCJCUQTY = 0;
                dCJJEQTY = 0;
                dDRUJQTY = 0;

                for (int i = 0; i < FPS91_TY_S_UT_699EV135.CurrentRowCount; i++)
                {
                    dCJCUQTY = dCJCUQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_699EV135.GetValue(i, "CJCUQTY").ToString()));
                    dCJJEQTY = dCJJEQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_699EV135.GetValue(i, "CJJEQTY").ToString()));
                    dDRUJQTY = dDRUJQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_699EV135.GetValue(i, "DRUJQTY").ToString()));
                }

                this.TXT01_CJCUQTY.SetValue((dCJCUQTY).ToString("0.000"));
                this.TXT01_CJJEQTY.SetValue((dCJJEQTY).ToString("0.000"));
                this.TXT01_CJDRQTY.SetValue((dDRUJQTY).ToString("0.000"));
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 통관화주 재고 스프레드 이벤트
        private void FPS91_TY_S_UT_699EV135_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_GET_DATA();
        }

        private void FPS91_TY_S_UT_699EV135_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_GET_DATA();
            }
        }
        #endregion

        #region Description : SURVEY 스프레드 이벤트
        private void FPS91_TY_S_UT_6ACKB350_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_GET_TANKNO_DATA();
        }

        private void FPS91_TY_S_UT_6ACKB350_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            UP_GET_TANKNO_DATA();
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

        private void CBH01_SHWAJU_Enter(object sender, EventArgs e)
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
                this.DbConnector.Attach("TY_P_UT_69UGV289", fsVNRPCODE.ToString()
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

        #region Description : 스프레드 데이터 가져오기
        private void UP_GET_DATA()
        {
            fsIPHANG   = "";
            fsBONSUN   = "";
            fsHWAJU    = "";
            fsHWAMUL   = "";
            fsBLNO     = "";
            fsMSNSEQ   = "";
            fsHSNSEQ   = "";
            fsCUSTIL   = "";
            fsCHASU    = "";
            fsACTHJ    = "";
            fsYDHWAJU  = "";
            fsYSHWAJU  = "";
            fsJGHWAJU  = "";
            fsYSDATE   = "";
            fsYSSEQ    = "";
            fsYDSEQ    = "";
            fsYNGUBUN  = "";

            fsTANKNO   = "";
            fsSVJGQTY  = "";
            fsSVMTQTY  = "";

            fsIPHANG  = this.FPS91_TY_S_UT_699EV135.GetValue("CJIPHANG").ToString();
            fsBONSUN  = this.FPS91_TY_S_UT_699EV135.GetValue("CJBONSUN").ToString();
            fsHWAJU   = this.FPS91_TY_S_UT_699EV135.GetValue("CJHWAJU").ToString();
            fsHWAMUL  = this.FPS91_TY_S_UT_699EV135.GetValue("CJHWAMUL").ToString();
            fsBLNO    = this.FPS91_TY_S_UT_699EV135.GetValue("CJBLNO").ToString();
            fsMSNSEQ  = this.FPS91_TY_S_UT_699EV135.GetValue("CJMSNSEQ").ToString();
            fsHSNSEQ  = this.FPS91_TY_S_UT_699EV135.GetValue("CJHSNSEQ").ToString();
            
            fsCUSTIL  = this.FPS91_TY_S_UT_699EV135.GetValue("CJCUSTIL").ToString();
            fsCHASU   = this.FPS91_TY_S_UT_699EV135.GetValue("CJCHASU").ToString();
            fsACTHJ   = this.FPS91_TY_S_UT_699EV135.GetValue("CJACTHJ").ToString();
            fsYDHWAJU = this.FPS91_TY_S_UT_699EV135.GetValue("CJYDHWAJU").ToString();
            fsYSHWAJU = this.FPS91_TY_S_UT_699EV135.GetValue("CJYSHWAJU").ToString();
            fsYNGUBUN = this.FPS91_TY_S_UT_699EV135.GetValue("YNGUBUN").ToString();

            // 재고화주
            fsJGHWAJU   = this.FPS91_TY_S_UT_699EV135.GetValue("CJJGHWAJU").ToString();
            fsJGHWAJUNM = this.FPS91_TY_S_UT_699EV135.GetValue("HJDESC5").ToString();

            fsYSDATE  = this.FPS91_TY_S_UT_699EV135.GetValue("CJYSDATE").ToString();
            fsYSSEQ   = this.FPS91_TY_S_UT_699EV135.GetValue("CJYSSEQ").ToString();
            fsYDSEQ   = this.FPS91_TY_S_UT_699EV135.GetValue("CJYDSEQ").ToString();

            fsCJCUQTY = this.FPS91_TY_S_UT_699EV135.GetValue("CJCUQTY").ToString();
            fsCJCHQTY = this.FPS91_TY_S_UT_699EV135.GetValue("CJCHQTY").ToString();
            fsCJJEQTY = this.FPS91_TY_S_UT_699EV135.GetValue("CJJEQTY").ToString();

            fsDPJEQTY = this.FPS91_TY_S_UT_699EV135.GetValue("DRUJQTY").ToString();

            fsIPMTQTY  = this.FPS91_TY_S_UT_699EV135.GetValue("IPMTQTY").ToString();
            fsIPPAQTY  = this.FPS91_TY_S_UT_699EV135.GetValue("IPPAQTY").ToString();
            fsIPCHQTY  = this.FPS91_TY_S_UT_699EV135.GetValue("IPCHQTY").ToString();
            fsIPJEQTY  = this.FPS91_TY_S_UT_699EV135.GetValue("IPJEQTY").ToString();
            fsIPJANQTY = this.FPS91_TY_S_UT_699EV135.GetValue("IPJANQTY").ToString();

            // SURVEY 데이터 가져오기
            UP_GET_SURVEY();
        }
        #endregion

        #region Description : SURVEY 데이터 가져오기
        private void UP_GET_SURVEY()
        {
            this.FPS91_TY_S_UT_6ACKB350.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_6ACKB349",
               fsIPHANG.ToString(),
               fsBONSUN.ToString(),
               fsHWAJU.ToString(),
               fsHWAMUL.ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6ACKB350.SetValue(dt);

                SetFocus(FPS91_TY_S_UT_6ACKB350);
            }
            else
            {
                this.FPS91_TY_S_UT_6ACKB350.SetValue(dt);
            }
        }
        #endregion

        #region Description : SURVEY 탱크 데이터 선택
        private void UP_GET_TANKNO_DATA()
        {
            fsTANKNO  = "";
            fsSVJGQTY = "";
            fsSVMTQTY = "";

            fsTANKNO  = this.FPS91_TY_S_UT_6ACKB350.GetValue("SVTANKNO").ToString();
            fsSVJGQTY = this.FPS91_TY_S_UT_6ACKB350.GetValue("SVJGQTY").ToString();
            fsSVMTQTY = this.FPS91_TY_S_UT_6ACKB350.GetValue("SVMTQTY").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}