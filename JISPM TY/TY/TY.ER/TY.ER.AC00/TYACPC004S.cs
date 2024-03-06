using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 재무상태표 관리 프로그램입니다.
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
    ///  TY_P_AC_27N71223 : EIS 재무상태표 확인
    ///  TY_P_AC_27N71224 : EIS 재무상태표 등록
    ///  TY_P_AC_27N72225 : EIS 재무상태표 수정
    ///  TY_P_AC_27PAT272 : EIS 재무상태표 조회
    ///  TY_P_AC_27O6X262 : 경영정보 마감 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27N78227 : EIS 재무상태표 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_AC_27P47279 : 년도를 입력하세요.
    ///  TY_M_AC_27P4C280 : 계정과목을 입력하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  EBMCDAC : 계정코드
    ///  EBMTAG01 : 차/대(D/C)
    ///  EBMCR01 : 대변01
    ///  EBMCR02 : 대변02
    ///  EBMCR03 : 대변03
    ///  EBMCR04 : 대변04
    ///  EBMCR05 : 대변05
    ///  EBMCR06 : 대변06
    ///  EBMCR07 : 대변07
    ///  EBMCR08 : 대변08
    ///  EBMCR09 : 대변09
    ///  EBMCR10 : 대변10
    ///  EBMCR11 : 대변11
    ///  EBMCR12 : 대변12
    ///  EBMDR01 : 차변01
    ///  EBMDR02 : 차변02
    ///  EBMDR03 : 차변03
    ///  EBMDR04 : 차변04
    ///  EBMDR05 : 차변05
    ///  EBMDR06 : 차변06
    ///  EBMDR07 : 차변07
    ///  EBMDR08 : 차변08
    ///  EBMDR09 : 차변09
    ///  EBMDR10 : 차변10
    ///  EBMDR11 : 차변11
    ///  EBMDR12 : 차변12
    ///  EBMYYHD : 년도
    /// </summary>
    public partial class TYACPC004S : TYBase
    {
        private string fsEBMDR00 = string.Empty;
        private string fsEBMCR00 = string.Empty;
        private int    fiECMONTH = 0;

        Boolean bDoubleClick = false;

        #region Description : 페이지 로드
        public TYACPC004S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27N78227, "EBMCDAC", "APCNMAC", "EBMCDAC"); // 스프레드 CODE HELP (대출과목)
        }

        private void TYACPC004S_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N78227, "EBMYYHD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N78227, "EBMCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N78227, "APCNMAC");

            SetStartingFocus(this.TXT01_EBMYYHD);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsEBMDR00 = "0";
            fsEBMCR00 = "0";
            fiECMONTH = 0;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27PAT272",
                this.TXT01_EBMYYHD.GetValue(),
                this.CBH01_EBMCDAC.GetValue()
                );

            this.FPS91_TY_S_AC_27N78227.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27N78227.Focus();

            UP_Set_ReadOnly(true, 12);

            this.CBO01_EBMTAG01.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27N71224", this.TXT01_EBMYYHD.GetValue(),
                                                        this.CBH01_EBMCDAC.GetValue(),
                                                        fsEBMDR00.ToString(),
                                                        this.TXT01_EBMDR01.GetValue(),
                                                        this.TXT01_EBMDR02.GetValue(),
                                                        this.TXT01_EBMDR03.GetValue(),
                                                        this.TXT01_EBMDR04.GetValue(),
                                                        this.TXT01_EBMDR05.GetValue(),
                                                        this.TXT01_EBMDR06.GetValue(),
                                                        this.TXT01_EBMDR07.GetValue(),
                                                        this.TXT01_EBMDR08.GetValue(),
                                                        this.TXT01_EBMDR09.GetValue(),
                                                        this.TXT01_EBMDR10.GetValue(),
                                                        this.TXT01_EBMDR11.GetValue(),
                                                        this.TXT01_EBMDR12.GetValue(),
                                                        fsEBMCR00.ToString(),
                                                        this.TXT01_EBMCR01.GetValue(),
                                                        this.TXT01_EBMCR02.GetValue(),
                                                        this.TXT01_EBMCR03.GetValue(),
                                                        this.TXT01_EBMCR04.GetValue(),
                                                        this.TXT01_EBMCR05.GetValue(),
                                                        this.TXT01_EBMCR06.GetValue(),
                                                        this.TXT01_EBMCR07.GetValue(),
                                                        this.TXT01_EBMCR08.GetValue(),
                                                        this.TXT01_EBMCR09.GetValue(),
                                                        this.TXT01_EBMCR10.GetValue(),
                                                        this.TXT01_EBMCR11.GetValue(),
                                                        this.TXT01_EBMCR12.GetValue(),
                                                        this.CBO01_EBMTAG01.GetValue(),
                                                        this.TXT01_EBMYYHD.GetValue(),
                                                        this.CBH01_EBMCDAC.GetValue()); //저장

            this.DbConnector.Attach("TY_P_AC_27N72225", fsEBMDR00.ToString(),
                                                        this.TXT01_EBMDR01.GetValue(),
                                                        this.TXT01_EBMDR02.GetValue(),
                                                        this.TXT01_EBMDR03.GetValue(),
                                                        this.TXT01_EBMDR04.GetValue(),
                                                        this.TXT01_EBMDR05.GetValue(),
                                                        this.TXT01_EBMDR06.GetValue(),
                                                        this.TXT01_EBMDR07.GetValue(),
                                                        this.TXT01_EBMDR08.GetValue(),
                                                        this.TXT01_EBMDR09.GetValue(),
                                                        this.TXT01_EBMDR10.GetValue(),
                                                        this.TXT01_EBMDR11.GetValue(),
                                                        this.TXT01_EBMDR12.GetValue(),
                                                        fsEBMCR00.ToString(),
                                                        this.TXT01_EBMCR01.GetValue(),
                                                        this.TXT01_EBMCR02.GetValue(),
                                                        this.TXT01_EBMCR03.GetValue(),
                                                        this.TXT01_EBMCR04.GetValue(),
                                                        this.TXT01_EBMCR05.GetValue(),
                                                        this.TXT01_EBMCR06.GetValue(),
                                                        this.TXT01_EBMCR07.GetValue(),
                                                        this.TXT01_EBMCR08.GetValue(),
                                                        this.TXT01_EBMCR09.GetValue(),
                                                        this.TXT01_EBMCR10.GetValue(),
                                                        this.TXT01_EBMCR11.GetValue(),
                                                        this.TXT01_EBMCR12.GetValue(),
                                                        this.CBO01_EBMTAG01.GetValue(),
                                                        this.TXT01_EBMYYHD.GetValue(),
                                                        this.CBH01_EBMCDAC.GetValue()); //수정

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_27N78227_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_FieldClear();

            fsEBMDR00 = "0";
            fsEBMCR00 = "0";
            fiECMONTH = 0;

            #region Description : 스프레드 클릭 값 가져오기

            this.TXT01_EBMYYHD.SetValue(this.FPS91_TY_S_AC_27N78227.GetValue("EBMYYHD").ToString());
            this.CBH01_EBMCDAC.SetValue(this.FPS91_TY_S_AC_27N78227.GetValue("EBMCDAC").ToString());

            // 이월 차변금액
            fsEBMDR00 = Get_Numeric(this.FPS91_TY_S_AC_27N78227.GetValue("EBMDR00").ToString());
            // 이월 대변금액
            fsEBMCR00 = Get_Numeric(this.FPS91_TY_S_AC_27N78227.GetValue("EBMCR00").ToString());

            #endregion

            if (this.TXT01_EBMYYHD.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_27P47279");
                return;
            }

            if (this.CBH01_EBMCDAC.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_27P4C280");
                return;
            }

            UP_Set_ReadOnly(false, 12);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27N71223",
                this.TXT01_EBMYYHD.GetValue(),
                this.CBH01_EBMCDAC.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }

            // 스프레드 CellClick이벤트 취소
            e.Cancel = true;

            #region Description : 마지막 마감 월에 따른 포커스 이동

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27O6X262",
                this.TXT01_EBMYYHD.GetValue()
                );

            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count > 0)
            {
                bDoubleClick = true;

                this.CBO01_EBMTAG01.SetReadOnly(true);

                fiECMONTH = int.Parse(dt1.Rows[0]["ECMONTH"].ToString());

                UP_Set_ReadOnly(true, fiECMONTH);

                if (fiECMONTH == 0)
                {
                    this.CBO01_EBMTAG01.SetReadOnly(false);

                    this.CBO01_EBMTAG01.Focus();
                }
                else if (fiECMONTH == 1)
                {
                    this.TXT01_EBMCR02.Focus();
                }
                else if (fiECMONTH == 2)
                {
                    this.TXT01_EBMCR03.Focus();
                }
                else if (fiECMONTH == 3)
                {
                    this.TXT01_EBMCR04.Focus();
                }
                else if (fiECMONTH == 4)
                {
                    this.TXT01_EBMCR05.Focus();
                }
                else if (fiECMONTH == 5)
                {
                    this.TXT01_EBMCR06.Focus();
                }
                else if (fiECMONTH == 6)
                {
                    this.TXT01_EBMCR07.Focus();
                }
                else if (fiECMONTH == 7)
                {
                    this.TXT01_EBMCR08.Focus();
                }
                else if (fiECMONTH == 8)
                {
                    this.TXT01_EBMCR09.Focus();
                }
                else if (fiECMONTH == 9)
                {
                    this.TXT01_EBMCR10.Focus();
                }
                else if (fiECMONTH == 10)
                {
                    this.TXT01_EBMCR11.Focus();
                }
                else if (fiECMONTH == 11)
                {
                    this.TXT01_EBMCR12.Focus();
                }
            }
            else
            {
                this.CBO01_EBMTAG01.SetReadOnly(false);

                this.CBO01_EBMTAG01.Focus();
            }

            #endregion
        }
        #endregion

        #region Description : 텍스트 박스 ReadOnly
        private void UP_Set_ReadOnly(bool bResult, int iResult)
        {
            if (iResult >= 1)
            {
                this.TXT01_EBMCR01.SetReadOnly(bResult);
                this.TXT01_EBMDR01.SetReadOnly(bResult);
            }

            if (iResult >= 2)
            {
                this.TXT01_EBMCR02.SetReadOnly(bResult);
                this.TXT01_EBMDR02.SetReadOnly(bResult);
            }

            if (iResult >= 3)
            {
                this.TXT01_EBMCR03.SetReadOnly(bResult);
                this.TXT01_EBMDR03.SetReadOnly(bResult);
            }

            if (iResult >= 4)
            {
                this.TXT01_EBMCR04.SetReadOnly(bResult);
                this.TXT01_EBMDR04.SetReadOnly(bResult);
            }

            if (iResult >= 5)
            {
                this.TXT01_EBMCR05.SetReadOnly(bResult);
                this.TXT01_EBMDR05.SetReadOnly(bResult);
            }

            if (iResult >= 6)
            {
                this.TXT01_EBMCR06.SetReadOnly(bResult);
                this.TXT01_EBMDR06.SetReadOnly(bResult);
            }

            if (iResult >= 7)
            {
                this.TXT01_EBMCR07.SetReadOnly(bResult);
                this.TXT01_EBMDR07.SetReadOnly(bResult);
            }

            if (iResult >= 8)
            {
                this.TXT01_EBMCR08.SetReadOnly(bResult);
                this.TXT01_EBMDR08.SetReadOnly(bResult);
            }

            if (iResult >= 9)
            {
                this.TXT01_EBMCR09.SetReadOnly(bResult);
                this.TXT01_EBMDR09.SetReadOnly(bResult);
            }

            if (iResult >= 10)
            {
                this.TXT01_EBMCR10.SetReadOnly(bResult);
                this.TXT01_EBMDR10.SetReadOnly(bResult);
            }

            if (iResult >= 11)
            {
                this.TXT01_EBMCR11.SetReadOnly(bResult);
                this.TXT01_EBMDR11.SetReadOnly(bResult);
            }

            if (iResult >= 12)
            {
                this.TXT01_EBMCR12.SetReadOnly(bResult);
                this.TXT01_EBMDR12.SetReadOnly(bResult);
            }
        }
        #endregion

        #region Description : 필드클리어
        private void UP_FieldClear()
        {
            this.TXT01_EBMDR01.SetValue("");
            this.TXT01_EBMDR02.SetValue("");
            this.TXT01_EBMDR03.SetValue("");
            this.TXT01_EBMDR04.SetValue("");
            this.TXT01_EBMDR05.SetValue("");
            this.TXT01_EBMDR06.SetValue("");
            this.TXT01_EBMDR07.SetValue("");
            this.TXT01_EBMDR08.SetValue("");
            this.TXT01_EBMDR09.SetValue("");
            this.TXT01_EBMDR10.SetValue("");
            this.TXT01_EBMDR11.SetValue("");
            this.TXT01_EBMDR12.SetValue("");

            this.TXT01_EBMCR01.SetValue("");
            this.TXT01_EBMCR02.SetValue("");
            this.TXT01_EBMCR03.SetValue("");
            this.TXT01_EBMCR04.SetValue("");
            this.TXT01_EBMCR05.SetValue("");
            this.TXT01_EBMCR06.SetValue("");
            this.TXT01_EBMCR07.SetValue("");
            this.TXT01_EBMCR08.SetValue("");
            this.TXT01_EBMCR09.SetValue("");
            this.TXT01_EBMCR10.SetValue("");
            this.TXT01_EBMCR11.SetValue("");
            this.TXT01_EBMCR12.SetValue("");

            this.CBO01_EBMTAG01.Initialize();
        }
        #endregion

        #region Description : 계정과목 포커스 이동
        private void CBH01_EBMCDAC_Leave(object sender, EventArgs e)
        {
            this.BTN61_INQ.Focus();
        }
        #endregion

        #region Description : 차변 12월 금액 포커스 이동
        private void TXT01_EBMDR12_Leave(object sender, EventArgs e)
        {
            this.BTN61_SAV.Focus();
        }
        #endregion

        #region Description : 엑셀 버튼 이벤트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPC004B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}