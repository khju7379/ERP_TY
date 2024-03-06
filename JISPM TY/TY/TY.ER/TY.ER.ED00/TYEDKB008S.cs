using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 시점재고 조회(수입) 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.03 13:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_733DN833 : 시점 재고 조회(수입)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_733DO835 : 시점 재고 조회(수입)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB008S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB008S()
        {
            InitializeComponent();
        }

        private void TYEDKB008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            UP_SetLockCheck();

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcId = string.Empty;

            if (CBO01_GOKCR.GetValue().ToString() == "1") //수입
            {
                this.FPS91_TY_S_UT_733DO835.Visible = true;
                this.FPS91_TY_S_UT_733H0840.Visible = false;

                sProcId = CBO01_EDIGJ.GetValue().ToString() == "T" ? "TY_P_UT_733DN833" : "TY_P_UT_733I1841";

                this.FPS91_TY_S_UT_733DO835.Initialize();
                this.DbConnector.CommandClear();
                if (CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.Attach(sProcId, DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString());
                }
                else
                {
                    this.DbConnector.Attach(sProcId, DTP01_SDATE.GetString().ToString());
                }
                this.FPS91_TY_S_UT_733DO835.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_UT_733DO835.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_UT_733DO835, "CDDESC11", "합   계", SumRowType.Sum, "IPBSQTY", "CASE");
                }
            }
            else  //내국
            {
                this.FPS91_TY_S_UT_733DO835.Visible = false;
                this.FPS91_TY_S_UT_733H0840.Visible = true;

                sProcId = CBO01_EDIGJ.GetValue().ToString() == "T" ? "TY_P_UT_733GY839" : "TY_P_UT_72SBO815";

                this.FPS91_TY_S_UT_733H0840.Initialize();
                this.DbConnector.CommandClear();
                if (CBO01_EDIGJ.GetValue().ToString() == "T")
                {
                    this.DbConnector.Attach(sProcId, DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString());
                }
                else
                {
                    //this.DbConnector.Attach(sProcId, DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString(), DTP01_SDATE.GetString().ToString());
                }
                this.FPS91_TY_S_UT_733H0840.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_UT_733H0840.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_UT_733H0840, "CDDESC1", "합   계", SumRowType.Sum, "IPBSQTY", "IPJEGOQTY");
                }

            }            

        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_EDIGJ.GetValue().ToString() == "S" && CBO01_GOKCR.GetValue().ToString() == "2")
            {                
                this.ShowCustomMessage("SILO는 내국화물 시점재고 자료가 존재하지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion
    }
}
