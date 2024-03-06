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
    /// 현업 탱크별 재고 대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.21 19:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_7338Y829 : 재고대장 SP
    ///  TY_P_UT_738GZ879 : 탱크별 재고대장 출력
    ///  TY_P_UT_73DAJ890 : 재고대장 임시파일 삭제
    ///  TY_P_UT_73EHN940 : 재고대장 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73LJC053 : 현업 탱크별 재고 대장
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  PRGUBN : PRGUBN
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  TUGOTK : 탱크 번호
    /// </summary>
    public partial class TYUTPR022S : TYBase
    {
        #region Descriotion : 폼 로드
        public TYUTPR022S()
        {
            InitializeComponent();
        }

        private void TYUTPR022S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            Set_PrtBtn();

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriotion : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sPRGUBN = string.Empty;

                // 운영팀
                if (this.CBO01_PRGUBN.GetValue().ToString() == "T")
                {
                    sPRGUBN = "T";
                }
                // 안전환경팀
                else
                {
                    sPRGUBN = "";
                }

                this.DbConnector.CommandClear();
                //// 재고대장 임시파일 삭제
                this.DbConnector.Attach("TY_P_UT_73EHN940");
                this.DbConnector.ExecuteTranQueryList();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73DAJ890");
                this.DbConnector.ExecuteTranQueryList();

                this.DbConnector.CommandClear();
                // 재고대장 생성 SP 호출
                this.DbConnector.Attach("TY_P_UT_7338Y829",
                                        this.DTP01_STDATE.GetString(),
                                        this.DTP01_EDDATE.GetString(),
                                        "",
                                        "",
                                        "",
                                        sPRGUBN,
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        ""
                                        );

                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.ShowMessage("TY_M_MR_2BF50354");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.DTP01_STDATE.GetString().Substring(0, 6) != this.DTP01_EDDATE.GetString().Substring(0, 6))
            {
                this.ShowCustomMessage("시작년월과 종료년월이 다릅니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt32(this.DTP01_STDATE.GetString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Descriotion : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_73LJC053.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            // 운영팀
            if (this.CBO01_PRGUBN.GetValue().ToString() == "T")
            {
                this.DbConnector.Attach("TY_P_UT_738GZ879", this.TXT01_TUGOTK.GetValue().ToString().Trim());
                this.FPS91_TY_S_UT_73LJC053.SetValue(this.DbConnector.ExecuteDataTable());
            }
            // 안전환경팀
            else
            {
                string sCHMSPGUBN = string.Empty;

                if (this.CBO01_CHMSPGUBN.GetValue().ToString() != "" && this.CBO01_CHMSPGUBN.GetValue().ToString() != "''")
                {
                    sCHMSPGUBN = this.CBO01_CHMSPGUBN.GetValue().ToString().Replace("'", "");

                    this.DbConnector.Attach("TY_P_UT_B1JGW364", sCHMSPGUBN.ToString());

                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_75PDY595");
                }

                this.FPS91_TY_S_UT_75PE3601.SetValue(this.DbConnector.ExecuteDataTable());
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_738GZ879", this.TXT01_TUGOTK.GetValue().ToString().Trim());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR022R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        private void Set_PrtBtn()
        {
            this.CBO01_PRGUBN.Visible = false;
            this.LBL51_PRGUBN.Visible = false;

            //안전환경팀
            if (TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0374-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0404-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0410-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0411-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0311-M" ||
                TYUserInfo.EmpNo.ToString().Trim().ToUpper() == "0430-M")
            {
                this.BTN61_PRT.Visible = false;
                this.CBO01_PRGUBN.SetValue("N");
                this.FPS91_TY_S_UT_75PE3601.Visible = true;
                this.FPS91_TY_S_UT_73LJC053.Visible = false;

                this.LBL51_CHMSPGUBN.Visible = true;
                this.CBO01_CHMSPGUBN.Visible = true;
            }
            //운영팀
            else
            {
                this.BTN61_PRT.Visible = true;
                this.CBO01_PRGUBN.SetValue("T");
                this.FPS91_TY_S_UT_75PE3601.Visible = false;
                this.FPS91_TY_S_UT_73LJC053.Visible = true;

                this.LBL51_CHMSPGUBN.Visible = false;
                this.CBO01_CHMSPGUBN.Visible = false;
            }
        }
    }
}
