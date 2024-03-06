using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_73OH3107 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTME016I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        private string fsVSJUBAN = string.Empty;
        private string fsVSGROSS = string.Empty;

        #region Description : 페이지 로드
        public TYUTME016I()
        {
            InitializeComponent();
        }

        private void TYUTME016I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_73OH3107.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_73OH3107.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_73OH3106",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_73OH3107.SetValue(dt);


            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly(false);

            UP_Field_Clear();

            UP_BTN_DISPLAY("NEW");

            SetFocus(this.DTP01_M1DATE);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_Save();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_Del();
        }
        #endregion

        #region Description : 저장 메소드
        private void UP_Save()
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73R9E109",
                                        Get_Date(this.DTP01_M1DATE.GetValue().ToString().Trim()),     // 매출일자
                                        Get_Date(this.DTP01_M1IPHANG.GetValue().ToString().Trim()),   // 입항일자
                                        this.CBH01_M1BONSUN.GetValue().ToString().Trim().ToUpper(),   // 본선
                                        this.CBH01_M1HWAJU.GetValue().ToString(),                     // 화주
                                        this.CBH01_M1HWAMUL.GetValue().ToString(),                    // 화물
                                        Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString()),          // 순번
                                        this.TXT01_M1ENIPGO.GetValue().ToString(),
                                        this.TXT01_M1ENBBLS.GetValue().ToString(),
                                        this.TXT01_M1ENHMAM.GetValue().ToString(),
                                        "0",
                                        "0",
                                        this.TXT01_M1ENHAY.GetValue().ToString(),
                                        "0",
                                        this.TXT01_M1RATE.GetValue().ToString(),
                                        this.TXT01_M1SUMHJ.GetValue().ToString(),
                                        this.CBH01_M1CURRCD.GetValue().ToString(),
                                        this.TXT01_M1EDDOLLAR.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()                             // 작성사번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73R9G110",
                                        this.TXT01_M1ENIPGO.GetValue().ToString(),
                                        this.TXT01_M1ENBBLS.GetValue().ToString(),
                                        this.TXT01_M1ENHMAM.GetValue().ToString(),
                                        "0",
                                        "0",
                                        this.TXT01_M1ENHAY.GetValue().ToString(),
                                        "0",
                                        this.TXT01_M1RATE.GetValue().ToString(),
                                        this.TXT01_M1SUMHJ.GetValue().ToString(),
                                        this.CBH01_M1CURRCD.GetValue().ToString(),
                                        this.TXT01_M1EDDOLLAR.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                 // 작성사번
                                        Get_Date(this.DTP01_M1DATE.GetValue().ToString().Trim()),     // 매출일자
                                        Get_Date(this.DTP01_M1IPHANG.GetValue().ToString().Trim()),   // 입항일자
                                        this.CBH01_M1BONSUN.GetValue().ToString().Trim().ToUpper(),   // 본선
                                        this.CBH01_M1HWAJU.GetValue().ToString(),                     // 화주
                                        this.CBH01_M1HWAMUL.GetValue().ToString(),                    // 화물
                                        Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())           // 순번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_Del()
        {    
            DataTable dt = new DataTable();

            // 하역료 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73R9G111",
                                    Get_Date(this.DTP01_M1DATE.GetValue().ToString().Trim()),   // 매출일자
                                    Get_Date(this.DTP01_M1IPHANG.GetValue().ToString().Trim()), // 입항일자
                                    this.CBH01_M1BONSUN.GetValue().ToString().Trim().ToUpper(), // 본선
                                    this.CBH01_M1HWAJU.GetValue().ToString(),                   // 화주
                                    this.CBH01_M1HWAMUL.GetValue().ToString(),                  // 화물
                                    Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())         // 순번
                                    );

            this.DbConnector.ExecuteNonQuery();            

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_73OHG108",
                Get_Date(this.DTP01_M1DATE.GetValue().ToString()),
                Get_Date(this.DTP01_M1IPHANG.GetValue().ToString()),
                this.CBH01_M1BONSUN.GetValue().ToString(),
                this.CBH01_M1HWAJU.GetValue().ToString(),
                this.CBH01_M1HWAMUL.GetValue().ToString(),
                Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                if (this.TXT01_M1JPNO.GetValue().ToString() != "")
                {
                    fsGUBUN = "";
                }

                UP_Field_ReadOnly(true);
            }
            else
            {
                fsGUBUN = "NEW";
            }

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.TXT01_M1ENIPGO);
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN == "NEW")
            {
                // 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_825FT576",
                    Get_Date(this.DTP01_M1DATE.GetValue().ToString()),
                    Get_Date(this.DTP01_M1IPHANG.GetValue().ToString()),
                    this.CBH01_M1BONSUN.GetValue().ToString(),
                    this.CBH01_M1HWAJU.GetValue().ToString(),
                    this.CBH01_M1HWAMUL.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_M1SEQ.SetValue(dt.Rows[0]["M1SEQ"].ToString());
                }

                // 동일자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_73OHG108",
                    Get_Date(this.DTP01_M1DATE.GetValue().ToString()),
                    Get_Date(this.DTP01_M1IPHANG.GetValue().ToString()),
                    this.CBH01_M1BONSUN.GetValue().ToString(),
                    this.CBH01_M1HWAJU.GetValue().ToString(),
                    this.CBH01_M1HWAMUL.GetValue().ToString(),
                    Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.DTP01_M1DATE.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString().Trim()) == "0")
                {
                    this.ShowMessage("TY_M_UT_825FR575");
                    this.DTP01_M1DATE.Focus();

                    e.Successed = false;
                    return;
                }
            }


            // 화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736DD855", this.CBH01_M1HWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VNPGUBN"].ToString() == "6")
                {
           
                    if (double.Parse(Get_Numeric(this.TXT01_M1RATE.GetValue().ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_UT_736D8847");

                        SetFocus(this.TXT01_M1RATE);

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_M1CURRCD.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_736D8848");

                        SetFocus(this.CBH01_M1CURRCD.CodeText);

                        e.Successed = false;
                        return;
                    }
                    
                    double dM1EDHMAM = 0;
                    double dM1EDHAY = 0;

                    double dFieldCompute = 0;

                    dM1EDHMAM = double.Parse(Get_Numeric(this.TXT01_M1ENHMAM.GetValue().ToString()));
                    dM1EDHAY = double.Parse(Get_Numeric(this.TXT01_M1ENHAY.GetValue().ToString()));

                    dFieldCompute = dM1EDHMAM + dM1EDHAY;

                    if (dFieldCompute == 0)
                    {
                        this.ShowMessage("TY_M_UT_73R9V112");

                        SetFocus(this.TXT01_M1ENHMAM);

                        e.Successed = false;
                        return;
                    }

                    dFieldCompute = ((dFieldCompute / Convert.ToDouble(Get_Numeric(this.TXT01_M1RATE.GetValue().ToString()))) * 100);
                    dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                    dFieldCompute = (dFieldCompute / 100);

                    if (double.Parse(Get_Numeric(this.TXT01_M1EDDOLLAR.GetValue().ToString())) != dFieldCompute)
                    {
                        this.ShowMessage("TY_M_AC_43P8Z962");

                        SetFocus(this.TXT01_M1EDDOLLAR);

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                }
            }
            else
            {
                this.ShowMessage("TY_M_UT_736F3857");

                SetFocus(this.CBH01_M1HWAJU.CodeText);

                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_73OHG108",
                Get_Date(this.DTP01_M1DATE.GetValue().ToString()),
                Get_Date(this.DTP01_M1IPHANG.GetValue().ToString()),
                this.CBH01_M1BONSUN.GetValue().ToString(),
                this.CBH01_M1HWAJU.GetValue().ToString(),
                this.CBH01_M1HWAMUL.GetValue().ToString(),
                Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["M1JPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;

                    SetFocus(this.DTP01_M1DATE);

                    return;
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sVSBONSUN = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_73OHG108",
                Get_Date(this.DTP01_M1DATE.GetValue().ToString()),
                Get_Date(this.DTP01_M1IPHANG.GetValue().ToString()),
                this.CBH01_M1BONSUN.GetValue().ToString(),
                this.CBH01_M1HWAJU.GetValue().ToString(),
                this.CBH01_M1HWAMUL.GetValue().ToString(),
                Get_Numeric(this.TXT01_M1SEQ.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["M1JPNO"].ToString() != "")
                {

                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;

                    SetFocus(this.DTP01_M1DATE);

                    return;
                }
            }

            // 삭제 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_UT_73OH3107_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_M1DATE.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1DATE").ToString());
            this.DTP01_M1IPHANG.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1IPHANG").ToString());
            this.CBH01_M1BONSUN.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1BONSUN").ToString());
            this.CBH01_M1HWAJU.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1HWAJU").ToString());
            this.CBH01_M1HWAMUL.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1HWAMUL").ToString());
            this.TXT01_M1SEQ.SetValue(this.FPS91_TY_S_UT_73OH3107.GetValue("M1SEQ").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            this.DTP01_M1DATE.SetReadOnly(boolean);
            this.DTP01_M1IPHANG.SetReadOnly(boolean);
            this.CBH01_M1BONSUN.SetReadOnly(boolean);
            this.CBH01_M1HWAJU.SetReadOnly(boolean);
            this.CBH01_M1HWAMUL.SetReadOnly(boolean);
            this.TXT01_M1SEQ.SetReadOnly(boolean);
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            //this.CBH01_M1BONSUN.SetValue("");
            //this.CBH01_M1HWAJU.SetValue("");
            //this.CBH01_M1HWAMUL.SetValue("");

            //this.TXT01_M1ENIPGO.SetValue("");
            //this.TXT01_M1ENBBLS.SetValue("");
            //this.TXT01_M1ENHMAM.SetValue("");
            //this.TXT01_M1ENHAY.SetValue("");

            ////this.TXT01_M1ENTUCK.SetValue("");
            ////this.TXT01_M1ENINJI.SetValue("");
            ////this.TXT01_M1ENJUB.SetValue("");
            //this.TXT01_M1SUMHJ.SetValue("");

            //this.CBH01_M1CURRCD.SetValue("");
            //this.TXT01_M1RATE.SetValue("");
            //this.TXT01_M1EDDOLLAR.SetValue("");
            //this.TXT01_M1JPNO.SetValue("");
        }
        #endregion

        #region Description : 날짜 이벤트
        private void DTP01_EDDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetFocus(this.CBH01_SHWAJU.CodeText);
        }
        #endregion

        #region Description : 화폐 이벤트
        private void CBH01_M1CURRCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_M1RATE);
            }
        }
        #endregion

        #region Description : 외화 금액 이벤트
        private void TXT01_M1EDDOLLAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_SAV);
            }
        }

        private void TXT01_M1EDDOLLAR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #region Description : 화주 이벤트
        private void CBH01_SHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    // FOCUS
                    Timer tmr = new Timer();

                    tmr.Tick += delegate
                    {
                        tmr.Stop();
                        this.SetFocus(this.BTN61_INQ);
                    };

                    tmr.Interval = 100;
                    tmr.Start();
                }
            }
        }
        #endregion

        #region Description : 순번 이벤트
        private void TXT01_M1SEQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_M1ENIPGO);
            }
        }
        #endregion

        private void CBH01_SHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if(Convert.ToInt32(e.KeyCode) == 13)
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    // FOCUS
                    Timer tmr = new Timer();

                    tmr.Tick += delegate
                    {
                        tmr.Stop();
                        this.SetFocus(this.BTN61_INQ);
                    };

                    tmr.Interval = 100;
                    tmr.Start();
                }
            }
        }

        private void DTP01_STDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.DTP01_EDDATE);
                };

                tmr.Interval = 100;
                tmr.Start();
            }
        }
    }
}