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
    /// 투하자금 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.08.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29484790 : 월말결산_투하자금 생성_02
    ///  TY_P_AC_29489787 : 월말결산_투하자금 생성_01
    ///  TY_P_AC_2948E792 : 월말결산_투하자금 생성_03
    ///  TY_P_AC_2957C839 : 월말결산_투하자금생성_재고자산
    ///  TY_P_AC_2CS4L418 : 월말결산_투하자금 생성(무역재고 구하기)
    ///  TY_P_AC_47LHT073 : 월말결산_투하자금 생성_채권_01 [2014년07월 부터]
    ///  TY_P_AC_47LHU074 : 월말결산_투하자금 생성_채무_02 [2014년07월 부터]
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
    public partial class TYACNC018B : TYBase
    {
 
        public TYACNC018B()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYACNC018B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GBPRYYMM.Focus();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
        } 
        #endregion

        #region  Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            string sDPAC   = string.Empty;
            string sB80100 = string.Empty;
            string sB80200 = string.Empty;
            string sB80200_S = string.Empty;

            /* DB2 프로시져 호츨 처리 */
            // 2014년 07월 부터 생성
            if (Convert.ToDouble(this.DTP01_GBPRYYMM.GetValue().ToString().Substring(0, 6)) >= 201407)
            {
                this.DbConnector.CommandClear(); // 채권
                this.DbConnector.Attach("TY_P_AC_47LHT073", this.DTP01_GBPRYYMM.GetValue(), Employer.EmpNo, sOUTMSG.ToString());
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    this.DbConnector.CommandClear(); // 채무
                    this.DbConnector.Attach("TY_P_AC_47LHU074", this.DTP01_GBPRYYMM.GetValue(), Employer.EmpNo, sOUTMSG.ToString());
                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }

            }
            else  // 2014년 07월이전 생성 처리
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29489787", this.DTP01_GBPRYYMM.GetValue(), Employer.EmpNo, sOUTMSG.ToString());
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29484790", this.DTP01_GBPRYYMM.GetValue(), Employer.EmpNo, sOUTMSG.ToString());
                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }


                // 승인자료를 읽어 있는 농업자원의 수입재고 가액을 읽어 반영한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CS4L418", this.DTP01_GBPRYYMM.GetValue() + "31");
                DataTable dt_B802 = this.DbConnector.ExecuteDataTable();
                if (dt_B802.Rows.Count != 0)
                {
                    sB80200_S = Convert.ToString(double.Parse(dt_B802.Rows[0]["SUAMT"].ToString()));
                }

                // 오라클에 있는 재고대장 가액을 읽어 반영한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2948E792", this.DTP01_GBPRYYMM.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sDPAC = dt.Rows[i]["DPAC"].ToString();
                        if (sDPAC == "B80100")
                        {
                            sB80100 = Convert.ToString(double.Parse(dt.Rows[i]["PDMCJAMT18"].ToString()) + double.Parse(dt.Rows[i]["PDSUIPAMT19"].ToString()));
                        }
                        else
                        {
                            sB80200 = Convert.ToString(double.Parse(dt.Rows[i]["PDMCJAMT18"].ToString()) + double.Parse(dt.Rows[i]["PDSUIPAMT19"].ToString()) + double.Parse(sB80200_S));
                        }
                    }
                    /* ----------------  B80100  ----------------  */
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2957C839",
                        this.DTP01_GBPRYYMM.GetValue(),
                        "01",
                        "11200100",
                        "B80100",
                        sB80100,
                         DateTime.Now.ToString("yyyy-MM-dd"),
                         Employer.EmpNo
                        );
                    this.DbConnector.ExecuteNonQuery();

                    /* ----------------  B80200  ----------------  */
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2957C839",
                        this.DTP01_GBPRYYMM.GetValue(),
                        "01",
                        "11200100",
                        "B80200",
                        sB80200,
                         DateTime.Now.ToString("yyyy-MM-dd"),
                         Employer.EmpNo
                        );
                    this.DbConnector.ExecuteNonQuery();
                }
            }

            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        } 
        #endregion

        #region  Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
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
