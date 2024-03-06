using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

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
    ///  TY_S_UT_7269L654 : 하역료 단가 관리
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
    public partial class TYUTME003B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUTME003B()
        {
            InitializeComponent();
        }

        private void TYUTME003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_M2DATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));


            this.DTP01_STDATE.SetValue(Get_Date(this.DTP01_STDATE.GetValue().ToString()).Substring(0, 6) + "26");
            this.DTP01_EDDATE.SetValue(Get_Date(this.DTP01_EDDATE.GetValue().ToString()).Substring(0, 6) + "25");

            this.DTP01_M2DATE.SetValue(Get_Date(this.DTP01_M2DATE.GetValue().ToString()).Substring(0, 6) + "27");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sM2DATE = string.Empty;
            string sOUTMSG = string.Empty;

            sM2DATE = Get_Date(this.DTP01_M2DATE.GetValue().ToString());

            // 선급금 생성 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736FG858", sM2DATE.ToString(),
                                                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_SHWAJU.GetValue().ToString(),
                                                        this.CBH01_SHWAMUL.GetValue().ToString(),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_GB_26E30875"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876"); // 저장 메세지
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

        #region Description : 생성 ProcessCheck
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sDATE = string.Empty;

            sDATE = Get_Date(this.DTP01_EDDATE.GetValue().ToString()).Substring(0, 6) + "27";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7BGED017",
                                    sDATE.ToString(),
                                    this.CBH01_SHWAJU.GetValue().ToString(),
                                    this.CBH01_SHWAMUL.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_7BGEC015");
                e.Successed = false;
                return;
            }

            // 생성하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sVNCODE = string.Empty;

            sVNCODE = Get_VNCODE(this.CBH01_SHWAJU.GetValue().ToString());

            string sDATE = string.Empty;
            sDATE = System.DateTime.Now.AddDays(1).ToString("yyyyMMdd");

            sDATE = sDATE.Substring(0, 4) + "/" + sDATE.Substring(4, 2) + "/" + sDATE.Substring(6, 2);

            DataTable dt = new DataTable();

            if (sVNCODE != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7BFBB011", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sVNCODE,
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sDATE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7BFBC012", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sDATE);
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTME003R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
 