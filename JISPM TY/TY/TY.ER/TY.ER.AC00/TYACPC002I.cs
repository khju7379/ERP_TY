using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 인건비 실적금액 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.23 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_27NBN195 : EIS 인건비 실적금액관리 조회
    ///  TY_P_AC_27NBP196 : EIS 인건비 실적금액관리 등록
    ///  TY_P_AC_27NBQ197 : EIS 인건비 실적금액관리 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  ELMYYMM : 년월
    ///  ELMCNEAA : 임원수관리공통
    ///  ELMCNEO1 : 임원수화학
    ///  ELMCNEO2 : 임원수자원
    ///  ELMCNESS : 임원수SILO
    ///  ELMCNETO : 임원수전체
    ///  ELMCNETT : 임원수UTT
    ///  ELMCNIAA : 직원수관리공통
    ///  ELMCNIO1 : 직원수화학
    ///  ELMCNIO2 : 직원수자원
    ///  ELMCNISS : 직원수SILO
    ///  ELMCNITO : 직원수전체
    ///  ELMCNITT : 직원수UTT
    ///  ELMCR00 : 이월계획금액
    ///  ELMCRAA : 계획관리공통
    ///  ELMCRO1 : 계획화학팀
    ///  ELMCRO2 : 계획자원팀
    ///  ELMCRSS : 계획SILO
    ///  ELMCRTO : 계획합계
    ///  ELMCRTT : 계획UTT
    ///  ELMDR00 : 이월실적금액
    ///  ELMDRAA : 실적관리공통
    ///  ELMDRO1 : 실적화학팀
    ///  ELMDRO2 : 실적자원팀
    ///  ELMDRSS : 실적SILO
    ///  ELMDRTO : 실적합계
    ///  ELMDRTT : 실적UTT
    /// </summary>
    public partial class TYACPC002I : TYBase
    {
        private string _ELMYYMM;

        #region Description : 페이지 로드
        public TYACPC002I(string ELMYYMM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._ELMYYMM = ELMYYMM;
        }

        private void TYACPC002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this._ELMYYMM))
            {
                this.DTP01_ELMYYMM.SetReadOnly(false);

                SetStartingFocus(this.DTP01_ELMYYMM);
            }
            else
            {
                this.DTP01_ELMYYMM.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27NBN195", this._ELMYYMM);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                SetStartingFocus(this.TXT01_ELMCRTT);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._ELMYYMM))
            {
                // 등록
                this.DbConnector.Attach("TY_P_AC_27NBP196", this.ControlFactory, "01");
            }
            else
            {
                // 수정
                this.DbConnector.Attach("TY_P_AC_27NBQ197", this.ControlFactory, "01");
            }

            this.DbConnector.ExecuteNonQuery();
            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this._ELMYYMM))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2CE42202",
                    this.DTP01_ELMYYMM.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_26D6A858");
                    e.Successed = false;
                    return;
                }
            }

            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_27H64059",
                this.DTP01_ELMYYMM.GetValue().ToString().Substring(0, 4),
                this.DTP01_ELMYYMM.GetValue().ToString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["ECGUBUN"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 계획UTT 텍스트 박스
        private void TXT01_ELMCRTT_TextChanged(object sender, EventArgs e)
        {
            double dELMCR00 = 0;
            double dELMCRTT = 0;
            double dELMCRSS = 0;
            double dELMCRO1 = 0;
            double dELMCRO2 = 0;
            double dELMCRAA = 0;

            double dELMCRHAP = 0;

            dELMCRTT = double.Parse(Get_Numeric(this.TXT01_ELMCRTT.GetValue().ToString()));
            dELMCRSS = double.Parse(Get_Numeric(this.TXT01_ELMCRSS.GetValue().ToString()));
            dELMCRO1 = double.Parse(Get_Numeric(this.TXT01_ELMCRO1.GetValue().ToString()));
            dELMCRO2 = double.Parse(Get_Numeric(this.TXT01_ELMCRO2.GetValue().ToString()));
            dELMCRAA = double.Parse(Get_Numeric(this.TXT01_ELMCRAA.GetValue().ToString()));

            dELMCRHAP = dELMCR00 + dELMCRTT + dELMCRSS + dELMCRO1 + dELMCRO2 + dELMCRAA;

            this.TXT01_ELMCRTO.SetValue(Convert.ToString(dELMCRHAP));
        }
        #endregion

        #region Description : 계획SILO 텍스트 박스
        private void TXT01_ELMCRSS_TextChanged(object sender, EventArgs e)
        {
            double dELMCR00 = 0;
            double dELMCRTT = 0;
            double dELMCRSS = 0;
            double dELMCRO1 = 0;
            double dELMCRO2 = 0;
            double dELMCRAA = 0;

            double dELMCRHAP = 0;

            dELMCRTT = double.Parse(Get_Numeric(this.TXT01_ELMCRTT.GetValue().ToString()));
            dELMCRSS = double.Parse(Get_Numeric(this.TXT01_ELMCRSS.GetValue().ToString()));
            dELMCRO1 = double.Parse(Get_Numeric(this.TXT01_ELMCRO1.GetValue().ToString()));
            dELMCRO2 = double.Parse(Get_Numeric(this.TXT01_ELMCRO2.GetValue().ToString()));
            dELMCRAA = double.Parse(Get_Numeric(this.TXT01_ELMCRAA.GetValue().ToString()));

            dELMCRHAP = dELMCR00 + dELMCRTT + dELMCRSS + dELMCRO1 + dELMCRO2 + dELMCRAA;

            this.TXT01_ELMCRTO.SetValue(Convert.ToString(dELMCRHAP));
        }
        #endregion

        #region Description : 계획화학팀 텍스트 박스
        private void TXT01_ELMCRO1_TextChanged(object sender, EventArgs e)
        {
            double dELMCR00 = 0;
            double dELMCRTT = 0;
            double dELMCRSS = 0;
            double dELMCRO1 = 0;
            double dELMCRO2 = 0;
            double dELMCRAA = 0;

            double dELMCRHAP = 0;

            dELMCRTT = double.Parse(Get_Numeric(this.TXT01_ELMCRTT.GetValue().ToString()));
            dELMCRSS = double.Parse(Get_Numeric(this.TXT01_ELMCRSS.GetValue().ToString()));
            dELMCRO1 = double.Parse(Get_Numeric(this.TXT01_ELMCRO1.GetValue().ToString()));
            dELMCRO2 = double.Parse(Get_Numeric(this.TXT01_ELMCRO2.GetValue().ToString()));
            dELMCRAA = double.Parse(Get_Numeric(this.TXT01_ELMCRAA.GetValue().ToString()));

            dELMCRHAP = dELMCR00 + dELMCRTT + dELMCRSS + dELMCRO1 + dELMCRO2 + dELMCRAA;

            this.TXT01_ELMCRTO.SetValue(Convert.ToString(dELMCRHAP));
        }
        #endregion

        #region Description : 계획자원팀 텍스트 박스
        private void TXT01_ELMCRO2_TextChanged(object sender, EventArgs e)
        {
            double dELMCR00 = 0;
            double dELMCRTT = 0;
            double dELMCRSS = 0;
            double dELMCRO1 = 0;
            double dELMCRO2 = 0;
            double dELMCRAA = 0;

            double dELMCRHAP = 0;

            dELMCRTT = double.Parse(Get_Numeric(this.TXT01_ELMCRTT.GetValue().ToString()));
            dELMCRSS = double.Parse(Get_Numeric(this.TXT01_ELMCRSS.GetValue().ToString()));
            dELMCRO1 = double.Parse(Get_Numeric(this.TXT01_ELMCRO1.GetValue().ToString()));
            dELMCRO2 = double.Parse(Get_Numeric(this.TXT01_ELMCRO2.GetValue().ToString()));
            dELMCRAA = double.Parse(Get_Numeric(this.TXT01_ELMCRAA.GetValue().ToString()));

            dELMCRHAP = dELMCR00 + dELMCRTT + dELMCRSS + dELMCRO1 + dELMCRO2 + dELMCRAA;

            this.TXT01_ELMCRTO.SetValue(Convert.ToString(dELMCRHAP));
        }
        #endregion

        #region Description : 계획관리공통 텍스트 박스
        private void TXT01_ELMCRAA_TextChanged(object sender, EventArgs e)
        {
            double dELMCR00 = 0;
            double dELMCRTT = 0;
            double dELMCRSS = 0;
            double dELMCRO1 = 0;
            double dELMCRO2 = 0;
            double dELMCRAA = 0;

            double dELMCRHAP = 0;

            dELMCRTT = double.Parse(Get_Numeric(this.TXT01_ELMCRTT.GetValue().ToString()));
            dELMCRSS = double.Parse(Get_Numeric(this.TXT01_ELMCRSS.GetValue().ToString()));
            dELMCRO1 = double.Parse(Get_Numeric(this.TXT01_ELMCRO1.GetValue().ToString()));
            dELMCRO2 = double.Parse(Get_Numeric(this.TXT01_ELMCRO2.GetValue().ToString()));
            dELMCRAA = double.Parse(Get_Numeric(this.TXT01_ELMCRAA.GetValue().ToString()));

            dELMCRHAP = dELMCR00 + dELMCRTT + dELMCRSS + dELMCRO1 + dELMCRO2 + dELMCRAA;

            this.TXT01_ELMCRTO.SetValue(Convert.ToString(dELMCRHAP));
        }
        #endregion

        #region Description : 실적UTT 텍스트 박스
        private void TXT01_ELMDRTT_TextChanged(object sender, EventArgs e)
        {
            double dELMDR00 = 0;
            double dELMDRTT = 0;
            double dELMDRSS = 0;
            double dELMDRO1 = 0;
            double dELMDRO2 = 0;
            double dELMDRAA = 0;

            double dELMDRHAP = 0;

            dELMDRTT = double.Parse(Get_Numeric(this.TXT01_ELMDRTT.GetValue().ToString()));
            dELMDRSS = double.Parse(Get_Numeric(this.TXT01_ELMDRSS.GetValue().ToString()));
            dELMDRO1 = double.Parse(Get_Numeric(this.TXT01_ELMDRO1.GetValue().ToString()));
            dELMDRO2 = double.Parse(Get_Numeric(this.TXT01_ELMDRO2.GetValue().ToString()));
            dELMDRAA = double.Parse(Get_Numeric(this.TXT01_ELMDRAA.GetValue().ToString()));

            dELMDRHAP = dELMDR00 + dELMDRTT + dELMDRSS + dELMDRO1 + dELMDRO2 + dELMDRAA;

            this.TXT01_ELMDRTO.SetValue(Convert.ToString(dELMDRHAP));
        }
        #endregion

        #region Description : 실적SILO 텍스트 박스
        private void TXT01_ELMDRSS_TextChanged(object sender, EventArgs e)
        {
            double dELMDR00 = 0;
            double dELMDRTT = 0;
            double dELMDRSS = 0;
            double dELMDRO1 = 0;
            double dELMDRO2 = 0;
            double dELMDRAA = 0;

            double dELMDRHAP = 0;

            dELMDRTT = double.Parse(Get_Numeric(this.TXT01_ELMDRTT.GetValue().ToString()));
            dELMDRSS = double.Parse(Get_Numeric(this.TXT01_ELMDRSS.GetValue().ToString()));
            dELMDRO1 = double.Parse(Get_Numeric(this.TXT01_ELMDRO1.GetValue().ToString()));
            dELMDRO2 = double.Parse(Get_Numeric(this.TXT01_ELMDRO2.GetValue().ToString()));
            dELMDRAA = double.Parse(Get_Numeric(this.TXT01_ELMDRAA.GetValue().ToString()));

            dELMDRHAP = dELMDR00 + dELMDRTT + dELMDRSS + dELMDRO1 + dELMDRO2 + dELMDRAA;

            this.TXT01_ELMDRTO.SetValue(Convert.ToString(dELMDRHAP));
        }
        #endregion

        #region Description : 실적화학팀 텍스트 박스
        private void TXT01_ELMDRO1_TextChanged(object sender, EventArgs e)
        {
            double dELMDR00 = 0;
            double dELMDRTT = 0;
            double dELMDRSS = 0;
            double dELMDRO1 = 0;
            double dELMDRO2 = 0;
            double dELMDRAA = 0;

            double dELMDRHAP = 0;

            dELMDRTT = double.Parse(Get_Numeric(this.TXT01_ELMDRTT.GetValue().ToString()));
            dELMDRSS = double.Parse(Get_Numeric(this.TXT01_ELMDRSS.GetValue().ToString()));
            dELMDRO1 = double.Parse(Get_Numeric(this.TXT01_ELMDRO1.GetValue().ToString()));
            dELMDRO2 = double.Parse(Get_Numeric(this.TXT01_ELMDRO2.GetValue().ToString()));
            dELMDRAA = double.Parse(Get_Numeric(this.TXT01_ELMDRAA.GetValue().ToString()));

            dELMDRHAP = dELMDR00 + dELMDRTT + dELMDRSS + dELMDRO1 + dELMDRO2 + dELMDRAA;

            this.TXT01_ELMDRTO.SetValue(Convert.ToString(dELMDRHAP));
        }
        #endregion

        #region Description : 실적자원팀 텍스트 박스
        private void TXT01_ELMDRO2_TextChanged(object sender, EventArgs e)
        {
            double dELMDR00 = 0;
            double dELMDRTT = 0;
            double dELMDRSS = 0;
            double dELMDRO1 = 0;
            double dELMDRO2 = 0;
            double dELMDRAA = 0;

            double dELMDRHAP = 0;

            dELMDRTT = double.Parse(Get_Numeric(this.TXT01_ELMDRTT.GetValue().ToString()));
            dELMDRSS = double.Parse(Get_Numeric(this.TXT01_ELMDRSS.GetValue().ToString()));
            dELMDRO1 = double.Parse(Get_Numeric(this.TXT01_ELMDRO1.GetValue().ToString()));
            dELMDRO2 = double.Parse(Get_Numeric(this.TXT01_ELMDRO2.GetValue().ToString()));
            dELMDRAA = double.Parse(Get_Numeric(this.TXT01_ELMDRAA.GetValue().ToString()));

            dELMDRHAP = dELMDR00 + dELMDRTT + dELMDRSS + dELMDRO1 + dELMDRO2 + dELMDRAA;

            this.TXT01_ELMDRTO.SetValue(Convert.ToString(dELMDRHAP));
        }
        #endregion

        #region Description : 실적관리공통 텍스트 박스
        private void TXT01_ELMDRAA_TextChanged(object sender, EventArgs e)
        {
            double dELMDR00 = 0;
            double dELMDRTT = 0;
            double dELMDRSS = 0;
            double dELMDRO1 = 0;
            double dELMDRO2 = 0;
            double dELMDRAA = 0;

            double dELMDRHAP = 0;

            dELMDRTT = double.Parse(Get_Numeric(this.TXT01_ELMDRTT.GetValue().ToString()));
            dELMDRSS = double.Parse(Get_Numeric(this.TXT01_ELMDRSS.GetValue().ToString()));
            dELMDRO1 = double.Parse(Get_Numeric(this.TXT01_ELMDRO1.GetValue().ToString()));
            dELMDRO2 = double.Parse(Get_Numeric(this.TXT01_ELMDRO2.GetValue().ToString()));
            dELMDRAA = double.Parse(Get_Numeric(this.TXT01_ELMDRAA.GetValue().ToString()));

            dELMDRHAP = dELMDR00 + dELMDRTT + dELMDRSS + dELMDRO1 + dELMDRO2 + dELMDRAA;

            this.TXT01_ELMDRTO.SetValue(Convert.ToString(dELMDRHAP));
        }
        #endregion

        #region Description : UTT-직원수 텍스트 박스
        private void TXT01_ELMCNITT_TextChanged(object sender, EventArgs e)
        {
            double dELMCNITT = 0;
            double dELMCNISS = 0;
            double dELMCNIO1 = 0;
            double dELMCNIO2 = 0;
            double dELMCNIAA = 0;

            double dELMCNIHAP = 0;

            dELMCNITT = double.Parse(Get_Numeric(this.TXT01_ELMCNITT.GetValue().ToString()));
            dELMCNISS = double.Parse(Get_Numeric(this.TXT01_ELMCNISS.GetValue().ToString()));
            dELMCNIO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO1.GetValue().ToString()));
            dELMCNIO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO2.GetValue().ToString()));
            dELMCNIAA = double.Parse(Get_Numeric(this.TXT01_ELMCNIAA.GetValue().ToString()));

            dELMCNIHAP = dELMCNITT + dELMCNISS + dELMCNIO1 + dELMCNIO2 + dELMCNIAA;

            this.TXT01_ELMCNITO.SetValue(Convert.ToString(dELMCNIHAP));

        }
        #endregion

        #region Description : SILO-직원수 텍스트 박스
        private void TXT01_ELMCNISS_TextChanged(object sender, EventArgs e)
        {
            double dELMCNITT = 0;
            double dELMCNISS = 0;
            double dELMCNIO1 = 0;
            double dELMCNIO2 = 0;
            double dELMCNIAA = 0;

            double dELMCNIHAP = 0;

            dELMCNITT = double.Parse(Get_Numeric(this.TXT01_ELMCNITT.GetValue().ToString()));
            dELMCNISS = double.Parse(Get_Numeric(this.TXT01_ELMCNISS.GetValue().ToString()));
            dELMCNIO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO1.GetValue().ToString()));
            dELMCNIO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO2.GetValue().ToString()));
            dELMCNIAA = double.Parse(Get_Numeric(this.TXT01_ELMCNIAA.GetValue().ToString()));

            dELMCNIHAP = dELMCNITT + dELMCNISS + dELMCNIO1 + dELMCNIO2 + dELMCNIAA;

            this.TXT01_ELMCNITO.SetValue(Convert.ToString(dELMCNIHAP));
        }
        #endregion

        #region Description : 화학-직원수 텍스트 박스
        private void TXT01_ELMCNIO1_TextChanged(object sender, EventArgs e)
        {
            double dELMCNITT = 0;
            double dELMCNISS = 0;
            double dELMCNIO1 = 0;
            double dELMCNIO2 = 0;
            double dELMCNIAA = 0;

            double dELMCNIHAP = 0;

            dELMCNITT = double.Parse(Get_Numeric(this.TXT01_ELMCNITT.GetValue().ToString()));
            dELMCNISS = double.Parse(Get_Numeric(this.TXT01_ELMCNISS.GetValue().ToString()));
            dELMCNIO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO1.GetValue().ToString()));
            dELMCNIO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO2.GetValue().ToString()));
            dELMCNIAA = double.Parse(Get_Numeric(this.TXT01_ELMCNIAA.GetValue().ToString()));

            dELMCNIHAP = dELMCNITT + dELMCNISS + dELMCNIO1 + dELMCNIO2 + dELMCNIAA;

            this.TXT01_ELMCNITO.SetValue(Convert.ToString(dELMCNIHAP));
        }
        #endregion

        #region Description : 자원-직원수 텍스트 박스
        private void TXT01_ELMCNIO2_TextChanged(object sender, EventArgs e)
        {
            double dELMCNITT = 0;
            double dELMCNISS = 0;
            double dELMCNIO1 = 0;
            double dELMCNIO2 = 0;
            double dELMCNIAA = 0;

            double dELMCNIHAP = 0;

            dELMCNITT = double.Parse(Get_Numeric(this.TXT01_ELMCNITT.GetValue().ToString()));
            dELMCNISS = double.Parse(Get_Numeric(this.TXT01_ELMCNISS.GetValue().ToString()));
            dELMCNIO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO1.GetValue().ToString()));
            dELMCNIO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO2.GetValue().ToString()));
            dELMCNIAA = double.Parse(Get_Numeric(this.TXT01_ELMCNIAA.GetValue().ToString()));

            dELMCNIHAP = dELMCNITT + dELMCNISS + dELMCNIO1 + dELMCNIO2 + dELMCNIAA;

            this.TXT01_ELMCNITO.SetValue(Convert.ToString(dELMCNIHAP));
        }
        #endregion

        #region Description : 관리공통-직원수 텍스트 박스
        private void TXT01_ELMCNIAA_TextChanged(object sender, EventArgs e)
        {
            double dELMCNITT = 0;
            double dELMCNISS = 0;
            double dELMCNIO1 = 0;
            double dELMCNIO2 = 0;
            double dELMCNIAA = 0;

            double dELMCNIHAP = 0;

            dELMCNITT = double.Parse(Get_Numeric(this.TXT01_ELMCNITT.GetValue().ToString()));
            dELMCNISS = double.Parse(Get_Numeric(this.TXT01_ELMCNISS.GetValue().ToString()));
            dELMCNIO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO1.GetValue().ToString()));
            dELMCNIO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNIO2.GetValue().ToString()));
            dELMCNIAA = double.Parse(Get_Numeric(this.TXT01_ELMCNIAA.GetValue().ToString()));

            dELMCNIHAP = dELMCNITT + dELMCNISS + dELMCNIO1 + dELMCNIO2 + dELMCNIAA;

            this.TXT01_ELMCNITO.SetValue(Convert.ToString(dELMCNIHAP));
        }
        #endregion

        #region Description : UTT-임원수 텍스트 박스
        private void TXT01_ELMCNETT_TextChanged(object sender, EventArgs e)
        {
            double dELMCNETT = 0;
            double dELMCNESS = 0;
            double dELMCNEO1 = 0;
            double dELMCNEO2 = 0;
            double dELMCNEAA = 0;

            double dELMCNEHAP = 0;

            dELMCNETT = double.Parse(Get_Numeric(this.TXT01_ELMCNETT.GetValue().ToString()));
            dELMCNESS = double.Parse(Get_Numeric(this.TXT01_ELMCNESS.GetValue().ToString()));
            dELMCNEO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO1.GetValue().ToString()));
            dELMCNEO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO2.GetValue().ToString()));
            dELMCNEAA = double.Parse(Get_Numeric(this.TXT01_ELMCNEAA.GetValue().ToString()));

            dELMCNEHAP = dELMCNETT + dELMCNESS + dELMCNEO1 + dELMCNEO2 + dELMCNEAA;

            this.TXT01_ELMCNETO.SetValue(Convert.ToString(dELMCNEHAP));
        }
        #endregion

        #region Description : SILO-임원수 텍스트 박스
        private void TXT01_ELMCNESS_TextChanged(object sender, EventArgs e)
        {
            double dELMCNETT = 0;
            double dELMCNESS = 0;
            double dELMCNEO1 = 0;
            double dELMCNEO2 = 0;
            double dELMCNEAA = 0;

            double dELMCNEHAP = 0;

            dELMCNETT = double.Parse(Get_Numeric(this.TXT01_ELMCNETT.GetValue().ToString()));
            dELMCNESS = double.Parse(Get_Numeric(this.TXT01_ELMCNESS.GetValue().ToString()));
            dELMCNEO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO1.GetValue().ToString()));
            dELMCNEO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO2.GetValue().ToString()));
            dELMCNEAA = double.Parse(Get_Numeric(this.TXT01_ELMCNEAA.GetValue().ToString()));

            dELMCNEHAP = dELMCNETT + dELMCNESS + dELMCNEO1 + dELMCNEO2 + dELMCNEAA;

            this.TXT01_ELMCNETO.SetValue(Convert.ToString(dELMCNEHAP));
        }
        #endregion

        #region Description : 화학-임원수 텍스트 박스
        private void TXT01_ELMCNEO1_TextChanged(object sender, EventArgs e)
        {
            double dELMCNETT = 0;
            double dELMCNESS = 0;
            double dELMCNEO1 = 0;
            double dELMCNEO2 = 0;
            double dELMCNEAA = 0;

            double dELMCNEHAP = 0;

            dELMCNETT = double.Parse(Get_Numeric(this.TXT01_ELMCNETT.GetValue().ToString()));
            dELMCNESS = double.Parse(Get_Numeric(this.TXT01_ELMCNESS.GetValue().ToString()));
            dELMCNEO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO1.GetValue().ToString()));
            dELMCNEO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO2.GetValue().ToString()));
            dELMCNEAA = double.Parse(Get_Numeric(this.TXT01_ELMCNEAA.GetValue().ToString()));

            dELMCNEHAP = dELMCNETT + dELMCNESS + dELMCNEO1 + dELMCNEO2 + dELMCNEAA;

            this.TXT01_ELMCNETO.SetValue(Convert.ToString(dELMCNEHAP));
        }
        #endregion

        #region Description : 자원-임원수 텍스트 박스
        private void TXT01_ELMCNEO2_TextChanged(object sender, EventArgs e)
        {
            double dELMCNETT = 0;
            double dELMCNESS = 0;
            double dELMCNEO1 = 0;
            double dELMCNEO2 = 0;
            double dELMCNEAA = 0;

            double dELMCNEHAP = 0;

            dELMCNETT = double.Parse(Get_Numeric(this.TXT01_ELMCNETT.GetValue().ToString()));
            dELMCNESS = double.Parse(Get_Numeric(this.TXT01_ELMCNESS.GetValue().ToString()));
            dELMCNEO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO1.GetValue().ToString()));
            dELMCNEO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO2.GetValue().ToString()));
            dELMCNEAA = double.Parse(Get_Numeric(this.TXT01_ELMCNEAA.GetValue().ToString()));

            dELMCNEHAP = dELMCNETT + dELMCNESS + dELMCNEO1 + dELMCNEO2 + dELMCNEAA;

            this.TXT01_ELMCNETO.SetValue(Convert.ToString(dELMCNEHAP));
        }
        #endregion

        #region Description : 관리공통-임원수 텍스트 박스
        private void TXT01_ELMCNEAA_TextChanged(object sender, EventArgs e)
        {
            double dELMCNETT = 0;
            double dELMCNESS = 0;
            double dELMCNEO1 = 0;
            double dELMCNEO2 = 0;
            double dELMCNEAA = 0;

            double dELMCNEHAP = 0;

            dELMCNETT = double.Parse(Get_Numeric(this.TXT01_ELMCNETT.GetValue().ToString()));
            dELMCNESS = double.Parse(Get_Numeric(this.TXT01_ELMCNESS.GetValue().ToString()));
            dELMCNEO1 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO1.GetValue().ToString()));
            dELMCNEO2 = double.Parse(Get_Numeric(this.TXT01_ELMCNEO2.GetValue().ToString()));
            dELMCNEAA = double.Parse(Get_Numeric(this.TXT01_ELMCNEAA.GetValue().ToString()));

            dELMCNEHAP = dELMCNETT + dELMCNESS + dELMCNEO1 + dELMCNEO2 + dELMCNEAA;

            this.TXT01_ELMCNETO.SetValue(Convert.ToString(dELMCNEHAP));
        }
        #endregion
    }
}