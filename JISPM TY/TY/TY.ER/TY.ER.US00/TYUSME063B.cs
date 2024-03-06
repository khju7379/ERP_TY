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

namespace TY.ER.US00
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
    public partial class TYUSME063B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSME063B()
        {
            InitializeComponent();
        }

        private void TYUSME063B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            this.MTB01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.MTB01_GDATE);
        }
        #endregion

        #region Description : 전표처리 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sB2SSID = string.Empty;

            string sOUTMSG = string.Empty;

            string sMAEGUBN = string.Empty;
            string sGUBUN = string.Empty;

            sMAEGUBN = "";
            sGUBUN = "SOGUB";

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
            {
                sMAEGUBN = "12";
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
            {
                sMAEGUBN = "13";
            }

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 매출 전표 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92RFE937", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6).ToString(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString(),
                                                        this.CBH01_GHWAJU.GetValue().ToString(),
                                                        this.CBO01_GGUBUN.GetValue().ToString(),
                                                        sMAEGUBN.ToString(),
                                                        sGUBUN.ToString(),
                                                        "TYUSME063B",
                                                        "0351-F",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    this.ShowMessage("TY_M_MR_31945578"); // 저장 메세지
                }
                else
                {
                    this.ShowMessage("TY_M_UT_74BHW252"); // 저장 메세지
                }
            }
            else
            {
                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    this.ShowMessage("TY_M_MR_3194D581"); // 저장 메세지
                }
                else
                {
                    this.ShowMessage("TY_M_UT_74BHX253"); // 저장 메세지
                }
            }

            SetFocus(this.CBH01_GHWAJU.CodeText);
        }
        #endregion

        #region Description : 전표 생성 ProcessCheck
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            // 매출발생월 이후의 미수금 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        ""
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.MTB01_GDATE);

                e.Successed = false;
                return;
            }

            if (this.CBO01_GGUBUN.GetText().ToString() == "취소")
            {
                // 전표 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9449F233", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                            this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString().Trim(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        // AHSLGLF 전표 존재 유무 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_92RDL927", dt.Rows[i]["SGJPNO"].ToString().Substring(0, 6).ToString(),
                                                                    dt.Rows[i]["SGJPNO"].ToString().Substring(6, 8).ToString(),
                                                                    dt.Rows[i]["SGJPNO"].ToString().Substring(14, 3).ToString());

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt1.Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_UT_73KHN007");

                            SetFocus(this.MTB01_GDATE);

                            e.Successed = false;
                            return;
                        }

                        // 전자 세금계산서 전표 존재 유무 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_C3SBD196", "6108110449", dt.Rows[i]["SGJPNO"].ToString());

                        dt2 = this.DbConnector.ExecuteDataTable();

                        if (dt2.Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_UT_73KHQ009");

                            SetFocus(this.MTB01_GDATE);

                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
            else
            {
                // 전표 존재유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_9449F233", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                            this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString().Trim(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_73LI0046");

                    SetFocus(this.MTB01_GDATE);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_AC_25O8J618"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_AC_25O8K619"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion
    }
}
