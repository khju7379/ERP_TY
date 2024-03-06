using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
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
    ///  TY_7
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUTME005B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUTME005B()
        {
            InitializeComponent();
        }

        private void TYUTME005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            this.DTP01_M2DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_M2DATE);
        }
        #endregion

        #region Description : 전표생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 선급금 전표생성 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73DBJ904", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                                        "TYUTME005B",
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        this.CBO01_GGUBUN.GetValue().ToString(),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_25O8K620"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_UT_73D99886"); // 저장 메세지
            }
        }
        #endregion

        #region Description : 전표출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_738FQ876", Get_Date(this.DTP01_M2DATE.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sB2DPMK = dt.Rows[0][0].ToString().Substring(0, 6);
                sB2DTMK = dt.Rows[0][0].ToString().Substring(6, 8);

                // 순번 가져오기
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sB2NOSQ == "")
                    {
                        sB2NOSQ = dt.Rows[i]["JPNO"].ToString().Substring(14, 3);
                    }
                    else
                    {
                        sB2NOSQ = sB2NOSQ + "," + dt.Rows[i]["JPNO"].ToString().Substring(14, 3);
                    }
                }


                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_738FY877",
                    sB2DPMK.ToString(),
                    sB2DTMK.ToString(),
                    sB2NOSQ.ToString()
                    );

                if (Convert.ToDouble(sB2DTMK.ToString().Substring(0, 4)) > 2014)
                {
                    SectionReport rpt = new TYACBJ0012R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    DataTable dt1 = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt1))).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ001R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    DataTable dt1 = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 전표생성 ProcessCheck
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                // 전표 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_738FA866", Get_Date(this.DTP01_M2DATE.GetValue().ToString()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_25F8V482");

                    SetFocus(this.DTP01_M2DATE);

                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_738FD867", Get_Date(this.DTP01_M2DATE.GetValue().ToString()));

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // UTT 거래처 확인
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_669GM112", dt.Rows[i]["M2HWAJU"].ToString().Trim());

                            dt1 = this.DbConnector.ExecuteDataTable();

                            if (dt1.Rows.Count > 0)
                            {
                                if (SetDefaultValue(dt1.Rows[0]["VNSAUPNO"].ToString()).Trim() == "")
                                {
                                    this.ShowMessage("TY_M_UT_738FL871");

                                    SetFocus(this.DTP01_M2DATE);

                                    e.Successed = false;
                                    return;
                                }
                                else
                                {
                                    // 회계거래처 - 사업자번호 확인
                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach("TY_P_UT_738FJ870", dt1.Rows[0]["VNSAUPNO"].ToString().Trim());

                                    dt2 = this.DbConnector.ExecuteDataTable();

                                    if (dt2.Rows.Count <= 0)
                                    {
                                        this.ShowMessage("TY_M_UT_738FM872");

                                        SetFocus(this.DTP01_M2DATE);

                                        e.Successed = false;
                                        return;
                                    }
                                }
                            }

                            // 해외영업(UTT)
                            if (dt.Rows[i]["VNPGUBN"].ToString().Trim() == "6")
                            {
                                if (double.Parse(dt.Rows[i]["M2RATE"].ToString().Trim()) == 0)
                                {
                                    this.ShowMessage("TY_M_UT_738FN873");

                                    SetFocus(this.DTP01_M2DATE);

                                    e.Successed = false;
                                    return;
                                }

                                if (SetDefaultValue(dt.Rows[i]["M2CURRCD"].ToString()).Trim() == "")
                                {
                                    this.ShowMessage("TY_M_UT_738FO874");

                                    SetFocus(this.DTP01_M2DATE);

                                    e.Successed = false;
                                    return;
                                }

                                if (double.Parse(dt.Rows[i]["M2ENINDOL"].ToString().Trim()) == 0)
                                {
                                    this.ShowMessage("TY_M_UT_738FP875");

                                    SetFocus(this.DTP01_M2DATE);

                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            // 생성하시겠습니까?
            if (!this.ShowMessage("TY_M_UT_73DBW907"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
