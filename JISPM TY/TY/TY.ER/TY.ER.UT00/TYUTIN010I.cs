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
    ///  TNTANKNO : TANK번호
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
    public partial class TYUTIN010I : TYBase
    {
        private string fsTNTANKNO = string.Empty;
        private string fsHMTEMPH  = string.Empty;
        private string fsHMTEMPL  = string.Empty;
        private string fsHWAMUL   = string.Empty;
        private string fsTNHWAMUL = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN010I(string sTNTANKNO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsTNTANKNO = sTNTANKNO;
        }

        private void TYUTIN010I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsTNTANKNO))
            {
                this.TXT01_TNTANKNO.SetReadOnly(false);

                SetStartingFocus(this.TXT01_TNTANKNO);
            }
            else
            {
                fsHWAMUL  = "";
                fsHMTEMPH = "";
                fsHMTEMPL = "";

                this.TXT01_TNTANKNO.SetReadOnly(true);

                this.TXT01_TNTANKNO.SetValue(this.fsTNTANKNO.ToString().Trim());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AQKH593", this.fsTNTANKNO.ToString().Trim());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    fsHWAMUL  = dt.Rows[0]["TNHWAMUL"].ToString();
                    fsHMTEMPH = dt.Rows[0]["HMTEMPH"].ToString();
                    fsHMTEMPL = dt.Rows[0]["HMTEMPL"].ToString();
                }

                SetStartingFocus(this.CBH01_TNHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sGUBUN = string.Empty;
            string sTNH   = string.Empty;
            string sTNHH  = string.Empty;
            string sTNL   = string.Empty;
            string sTNLL  = string.Empty;

            sTNH  = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_TNH.GetValue().ToString())) * 10));
            sTNH  = Convert.ToString(double.Parse(sTNH.ToString()) / 10);
            sTNHH = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_TNHH.GetValue().ToString())) * 10));
            sTNHH = Convert.ToString(double.Parse(sTNHH.ToString()) / 10);
            sTNL  = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_TNL.GetValue().ToString())) * 10));
            sTNL  = Convert.ToString(double.Parse(sTNL.ToString()) / 10);
            sTNLL = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_TNLL.GetValue().ToString())) * 10));
            sTNLL = Convert.ToString(double.Parse(sTNLL.ToString()) / 10);

            DataTable dt = new DataTable();

            // 경유자동화 SERVER전송파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6B1DP636",
                this.TXT01_TNTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPDATE";
            }
            else
            {
                sGUBUN = "ADD";
            }

            // 경유자동화 SERVER전송파일 저장
            if (sGUBUN.ToString() == "ADD")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6B1DN633", Set_TankNo(this.TXT01_TNTANKNO.GetValue().ToString()),          // 탱크번호
                                                            this.CBH01_TNLOCATE.GetValue().ToString().Trim(),               // 위치
                                                            Get_Numeric(this.TXT01_TNDIA.GetValue().ToString().Trim()),     // 구경
                                                            Get_Numeric(this.TXT01_TNHIGH.GetValue().ToString().Trim()),    // 높이
                                                            Get_Numeric(this.TXT01_TNCAPA.GetValue().ToString().Trim()),    // 용량
                                                            Get_Numeric(sTNH.ToString()),                                   // HIGH
                                                            Get_Numeric(sTNHH.ToString()),                                  // HIGH-HIGH
                                                            Get_Numeric(sTNL.ToString()),                                   // LOW
                                                            Get_Numeric(sTNLL.ToString()),                                  // LOW-LOW
                                                            fsHMTEMPH.ToString(),                                           // TEMP-H
                                                            fsHMTEMPL.ToString(),                                           // TEMP-L
                                                            Get_Numeric(this.TXT01_TNDENSITY.GetValue().ToString().Trim()), // DENSITY
                                                            Get_Numeric(this.TXT01_TNWATER.GetValue().ToString().Trim()),   // WATER
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                // 화주
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                 // 화주명
                                                            fsHWAMUL.ToString().Trim(),                                     // 화물
                                                            fsTNHWAMUL.ToString(),                                          // 화물명
                                                            "N"                                                             // 전송유무
                                                            ); // 저장

                this.DbConnector.ExecuteNonQuery();
            }
            else // 경유자동화 SERVER전송파일 수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6B1DP635", this.CBH01_TNLOCATE.GetValue().ToString().Trim(),               // 위치
                                                            Get_Numeric(this.TXT01_TNDIA.GetValue().ToString().Trim()),     // 구경
                                                            Get_Numeric(this.TXT01_TNHIGH.GetValue().ToString().Trim()),    // 높이
                                                            Get_Numeric(this.TXT01_TNCAPA.GetValue().ToString().Trim()),    // 용량
                                                            Get_Numeric(sTNH.ToString()),                                   // HIGH
                                                            Get_Numeric(sTNHH.ToString()),                                  // HIGH-HIGH
                                                            Get_Numeric(sTNL.ToString()),                                   // LOW
                                                            Get_Numeric(sTNLL.ToString()),                                  // LOW-LOW
                                                            fsHMTEMPH.ToString(),                                           // TEMP-H
                                                            fsHMTEMPL.ToString(),                                           // TEMP-L
                                                            Get_Numeric(this.TXT01_TNDENSITY.GetValue().ToString().Trim()), // DENSITY
                                                            Get_Numeric(this.TXT01_TNWATER.GetValue().ToString().Trim()),   // WATER
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                // 화주
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                 // 화주명
                                                            fsHWAMUL.ToString().Trim(),                                     // 화물
                                                            fsTNHWAMUL.ToString(),                                          // 화물명
                                                            "N",                                                            // 전송유무
                                                            this.TXT01_TNTANKNO.GetValue().ToString().Trim()                // 탱크번호
                                                            ); // 수정

                this.DbConnector.ExecuteNonQuery();
            }

            // 탱크제원파일 등록
            if (string.IsNullOrEmpty(this.fsTNTANKNO))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B1IFJ335", Set_TankNo(this.TXT01_TNTANKNO.GetValue().ToString()),          // 탱크번호
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                // 화주
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                 // 화주명
							                                this.CBH01_TNLOCATE.GetValue().ToString().Trim(),               // 위치
                                                            Get_Numeric(this.TXT01_TNDIA.GetValue().ToString().Trim()),     // 구경
                                                            Get_Numeric(this.TXT01_TNHIGH.GetValue().ToString().Trim()),    // 높이
                                                            Get_Numeric(this.TXT01_TNCAPA.GetValue().ToString().Trim()),    // 용량
                                                            Get_Numeric(this.TXT01_TNH.GetValue().ToString().Trim()),       // HIGH
                                                            Get_Numeric(this.TXT01_TNHH.GetValue().ToString().Trim()),      // HIGH-HIGH
                                                            Get_Numeric(this.TXT01_TNL.GetValue().ToString().Trim()),       // LOW
                                                            Get_Numeric(this.TXT01_TNLL.GetValue().ToString().Trim()),      // LOW-LOW
                                                            this.CBH01_TNMETERI.GetValue().ToString().Trim(),               // 재질
							                                this.CBO01_TNSTEAM.GetValue().ToString(),                       // STEAM COIL
                                                            this.CBO01_TNCOVER.GetValue().ToString(),                       // SHELL COVER
                                                            this.CBO01_TNBREATH.GetValue().ToString(),                      // BREATHER VALVE
                                                            this.CBO01_TNPURGE.GetValue().ToString(),                       // N2 PURGE PIPE
                                                            Get_Numeric(this.TXT01_TNFIRE.GetValue().ToString().Trim()),    // FIRE LINE DIA
                                                            Get_Numeric(this.TXT01_TNWATER.GetValue().ToString().Trim()),   // WATER LINE DIA
                                                            Get_Numeric(this.TXT01_TNINLEN.GetValue().ToString().Trim()),   // IN PIPE
                                                            Get_Numeric(this.TXT01_TNINDIA.GetValue().ToString().Trim()),   // IN DIA
                                                            Get_Numeric(this.TXT01_TNOUTLEN.GetValue().ToString().Trim()),  // OUT PIPE
                                                            Get_Numeric(this.TXT01_TNOUTDIA.GetValue().ToString().Trim()),  // OUT DIA
                                                            this.CBH01_TNOWNER.GetValue().ToString().Trim(),                // OWNER
                                                            Get_Date(DTP01_TNWANGONG.GetValue().ToString().Trim()),         // 완공일자
                                                            this.TXT01_TNJEJAK.GetValue().ToString().Trim(),                // 제작회사
                                                            this.TXT01_TNUPDOWN.GetValue().ToString().Trim(),               // 출구상하단지구분
                                                            this.TXT01_TNUPDOWNGB.GetValue().ToString().Trim(),             // 출구GATE
                                                            Get_Numeric(this.TXT01_TNDENSITY.GetValue().ToString().Trim()), // DENSITY
                                                            this.CKB01_TNNICHARGE.GetValue().ToString(),                    // 질소대청구
                                                            this.TXT01_TNFIRECAPA.GetValue().ToString(),                    // 소방용량
							                                TYUserInfo.EmpNo                                                // 작성사번
                                                            ); // 수정

                this.DbConnector.ExecuteNonQuery();
            }
            else // 탱크제원파일 수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B1IFJ336", this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                // 화주
                                                            this.CBH01_TNHWAJU.GetValue().ToString().Trim(),                 // 화주명
                                                            this.CBH01_TNLOCATE.GetValue().ToString().Trim(),               // 위치
                                                            Get_Numeric(this.TXT01_TNDIA.GetValue().ToString().Trim()),     // 구경
                                                            Get_Numeric(this.TXT01_TNHIGH.GetValue().ToString().Trim()),    // 높이
                                                            Get_Numeric(this.TXT01_TNCAPA.GetValue().ToString().Trim()),    // 용량
                                                            Get_Numeric(this.TXT01_TNH.GetValue().ToString().Trim()),       // HIGH
                                                            Get_Numeric(this.TXT01_TNHH.GetValue().ToString().Trim()),      // HIGH-HIGH
                                                            Get_Numeric(this.TXT01_TNL.GetValue().ToString().Trim()),       // LOW
                                                            Get_Numeric(this.TXT01_TNLL.GetValue().ToString().Trim()),      // LOW-LOW
                                                            this.CBH01_TNMETERI.GetValue().ToString().Trim(),               // 재질
                                                            this.CBO01_TNSTEAM.GetValue().ToString(),                       // STEAM COIL
                                                            this.CBO01_TNCOVER.GetValue().ToString(),                       // SHELL COVER
                                                            this.CBO01_TNBREATH.GetValue().ToString(),                      // BREATHER VALVE
                                                            this.CBO01_TNPURGE.GetValue().ToString(),                       // N2 PURGE PIPE
                                                            Get_Numeric(this.TXT01_TNFIRE.GetValue().ToString().Trim()),    // FIRE LINE DIA
                                                            Get_Numeric(this.TXT01_TNWATER.GetValue().ToString().Trim()),   // WATER LINE DIA
                                                            Get_Numeric(this.TXT01_TNINLEN.GetValue().ToString().Trim()),   // IN PIPE
                                                            Get_Numeric(this.TXT01_TNINDIA.GetValue().ToString().Trim()),   // IN DIA
                                                            Get_Numeric(this.TXT01_TNOUTLEN.GetValue().ToString().Trim()),  // OUT PIPE
                                                            Get_Numeric(this.TXT01_TNOUTDIA.GetValue().ToString().Trim()),  // OUT DIA
                                                            this.CBH01_TNOWNER.GetValue().ToString().Trim(),                // OWNER
                                                            Get_Date(DTP01_TNWANGONG.GetValue().ToString().Trim()),         // 완공일자
                                                            this.TXT01_TNJEJAK.GetValue().ToString().Trim(),                // 제작회사
                                                            this.TXT01_TNUPDOWN.GetValue().ToString().Trim(),               // 출구상하단지구분
                                                            this.TXT01_TNUPDOWNGB.GetValue().ToString().Trim(),             // 출구GATE
                                                            Get_Numeric(this.TXT01_TNDENSITY.GetValue().ToString().Trim()), // DENSITY
                                                            this.CKB01_TNNICHARGE.GetValue().ToString(),                    // 질소대청구
                                                            this.TXT01_TNFIRECAPA.GetValue().ToString(),                    // 소방용량
                                                            TYUserInfo.EmpNo,                                               // 작성사번
                                                            this.TXT01_TNTANKNO.GetValue().ToString().Trim()                // 탱크번호
                                                            ); // 수정

                this.DbConnector.ExecuteNonQuery();
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
            if (fsHWAMUL != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66FAH184", "HM", fsHWAMUL.ToString(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsTNHWAMUL = dt.Rows[0]["CODE_NAME"].ToString();
                }
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
