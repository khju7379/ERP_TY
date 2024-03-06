using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 탱크제원관리 프로그램입니다.
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
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  TNBONSUN : 본선
    ///  TNHWAJU : 화주
    ///  TNHWAMUL : 화물
    ///  TNLOCATE : 위치
    ///  TNMETERI : 재질
    ///  TNOWNER : OWNER
    ///  TTBONSUN : 본선
    ///  TTHM : 화물
    ///  TNBREATH : BREATHER VALVE
    ///  TNCOVER : SHELL COVER
    ///  TNSTEAM : STEAM COIL
    ///  TNCLDAT : 세척일자
    ///  TNIPHANG : 입항일자
    ///  TNWANGONG : 완공일자
    ///  TTIPHANG : 입항일자
    ///  TNBIJUNG : W.C.F
    ///  TNCAPA : 용량
    ///  TNDENSITY : DENSITY
    ///  TNDIA : 구경
    ///  TNFIRE : FIRE LINE DIA
    ///  TNH : HIGH
    ///  TNHH : HIGH-HIGH
    ///  TNHIGH : 높이
    ///  TNHMGB : 화물구분
    ///  TNHWAJUNM : 화주이름
    ///  TNINDIA : IN DIA
    ///  TNINLEN : IN PIPE
    ///  TNJEJAK : 제작회사
    ///  TNKESAN : 계산방법
    ///  TNL : LOW
    ///  TNLL : LOW-LOW
    ///  TNOUTDIA : OUT DIA
    ///  TNOUTLEN : OUT PIPE
    ///  TNPURGE : N2 PURGE PIPE
    ///  SNTANKNO : TANK번호
    ///  TNTEMP : 온도
    ///  TNTEMPGB : 온도구분
    ///  TNTEMPH : TEMP-H
    ///  TNTEMPL : TEMP-L
    ///  TNUPDOWN : 출구상하단지구분
    ///  TNUPDOWNGB : 출구ＧＡＴＥ
    ///  TNVCF : V.C
    ///  TNWATER : WATER LINE DIA
    ///  TTBIJUNG : W.C.F
    ///  TTHMGB : 화물구분
    ///  TTKESAN : 계산벙법
    ///  TTTEMP : 온도
    ///  TTTEMPGB : 온도구분
    ///  TTVCF : V.C
    /// </summary>
    public partial class TYUTIN008I : TYBase
    {
        private string fsSNTANKNO = string.Empty;
        private string fsSNDATE   = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN008I(string sSNDATE, string sSNTANKNO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsSNDATE   = sSNDATE;
            this.fsSNTANKNO = sSNTANKNO;
        }

        private void TYUTIN008I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsSNTANKNO))
            {
                this.DTP01_SNDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                this.DTP01_SNDATE.SetReadOnly(false);
                this.TXT01_SNTANKNO.SetReadOnly(false);

                SetStartingFocus(this.DTP01_SNDATE);
            }
            else
            {
                this.DTP01_SNDATE.SetReadOnly(true);
                this.TXT01_SNTANKNO.SetReadOnly(true);

                this.DTP01_SNDATE.SetValue(fsSNDATE.ToString());
                this.TXT01_SNTANKNO.SetValue(this.fsSNTANKNO.ToString().Trim());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BPBZ862", this.fsSNTANKNO.ToString().Trim(), this.fsSNDATE.ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                SetStartingFocus(this.TXT01_SNHIGH);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            string sSIGN   = string.Empty;
            string sGUBUN  = string.Empty;

            if (string.IsNullOrEmpty(this.fsSNTANKNO))
            {
                sGUBUN = "A";
            }
            else
            {
                sGUBUN = "C";
            }

            if (double.Parse(Get_Numeric(this.TXT01_SNTEMP.GetValue().ToString())) < 0)
            {
                sSIGN = "-";
            }
            else
            {
                sSIGN = "+";
            }

            // 지시 일괄 등록 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BMGV830", Set_TankNo(this.TXT01_SNTANKNO.GetValue().ToString()),
                                                        Get_Date(this.DTP01_SNDATE.GetValue().ToString()),
                                                        Get_Numeric(this.TXT01_SNHIGH.GetValue().ToString()),
                                                        Get_Numeric(this.TXT01_SNTEMP.GetValue().ToString()),
                                                        sSIGN.ToString(),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        sGUBUN.ToString(),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_UT_6BPEA870");

                return;
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BOI7859", this.TXT01_SNTANKNO.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_676GD601");

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
