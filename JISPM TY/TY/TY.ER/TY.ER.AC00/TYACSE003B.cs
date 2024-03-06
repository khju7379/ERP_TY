using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 월말결산_영업비용 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.12 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46NGS860 : 결산관리 채권채무 생성
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GBPRYYMM : 처리년월
    /// </summary>
    public partial class TYACSE003B : TYBase
    {
        public TYACSE003B()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYACSE003B_Load(object sender, System.EventArgs e)
        {
            
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GBPRYYMM.Focus();
            UP_Start_date_set(); // 마지막 상각월 세팅하기

        } 
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();
            string s처리구분 = string.Empty;
            string sOUTMSG = string.Empty;

            if (s전체처리구분 == "A")
            {
                s처리구분 = "INS";
            }
            else
            {
                s처리구분 = "DEL";
            };

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46NGS860", this.DTP01_GBPRYYMM.GetValue(), s처리구분, Employer.EmpNo.ToString().Trim(), sOUTMSG.ToString());

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            
        } 
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Description : 마지막 상각월 세팅하기
        private void UP_Start_date_set()
        {
            string sYYMM = string.Empty;
            string sYY = string.Empty;
            string sMM = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46O9H862");
            string sSYYMM = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sSYYMM != "")
            {
                if (sSYYMM.Substring(4, 2) == "12")
                {
                    sYY = Set_Fill4(Convert.ToString(Convert.ToInt16(sSYYMM.Substring(0, 4)) + 1));
                    sYYMM = sYY + "01";
                }
                else
                {
                    sMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sSYYMM.Substring(4, 2)) + 1));
                    sYYMM = sSYYMM.Substring(0, 4) + sMM;
                }

                this.DTP01_GBPRYYMM.SetValue(sYYMM);
            }

        }
        #endregion


        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_27H64059", this.DTP01_GBPRYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GBPRYYMM.GetValue().ToString().Substring(4, 2));
            //DataTable dt1 = this.DbConnector.ExecuteDataTable();

            //if (dt1.Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
            //    e.Successed = false;
            //    return;
            //}
            //else
            //{
            //    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
            //    {
            //        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
            //        e.Successed = false;
            //        return;
            //    }
            //}

        }
        #endregion
    }
}
