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
    /// 월 상각 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.05.03 14:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_35328592 : 고정자산상각 생성
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
    ///  GOKCR : 생성구분
    ///  GBPRYYMM : 처리년월
    /// </summary>
    public partial class TYACHF009B : TYBase
    {
        private string sSABUN = string.Empty;

        public TYACHF009B()
        {
            InitializeComponent();
        }

        private void TYACHF009B_Load(object sender, System.EventArgs e)
        {
            // 로그인 사번 가져오기
            this.sSABUN = TYUserInfo.EmpNo.Trim().ToUpper();
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            UP_Start_date_set(); // 마지막 상각월 세팅하기

        }

        #region Description : 마지막 상각월 세팅하기
        private void UP_Start_date_set()
        {
            string sYYMM = string.Empty;
            string sYY = string.Empty;
            string sMM = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37T2P261");
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
                this.DTP01_TGBPRYYMM.SetValue(sYYMM);
            }

        }
        #endregion

        #region Description : 닫기
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

            Int16 iSMM = Convert.ToInt16(this.DTP01_GBPRYYMM.GetString().ToString().Substring(4, 2));
            Int16 iEMM = Convert.ToInt16(this.DTP01_TGBPRYYMM.GetString().ToString().Substring(4, 2));

            string sYYMM = string.Empty;

            // 생성처리 

            if (s처리구분 == "INS")
            {
                for (int i = iSMM; i <= iEMM; i++)
                {
                    sYYMM = this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4) + Set_Fill2(Convert.ToString(i));

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        //"TY_P_AC_35328592",
                        "TY_P_AC_64BIE782",
                        sYYMM,
                        s처리구분,
                        this.sSABUN,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
            }
            else
            {
                // 삭제 처리 
                for (int i = iEMM; i >= iSMM; i--)
                {
                    sYYMM = this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4) + Set_Fill2(Convert.ToString(i));

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        //"TY_P_AC_35328592",
                        "TY_P_AC_64BIE782",
                        sYYMM,
                        s처리구분,
                        this.sSABUN,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
            }

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            this.SetFocus(this.CBO01_GOKCR);
        }
        #endregion


        #region Description : 처리 BTN61_BATCH ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //2017년이전 상각을 생성할수 없다.(2018.07.16)
            if (Convert.ToInt16(this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4)) <= 2017)
            {
                this.ShowCustomMessage("해당년도는 상각작업을 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_GBPRYYMM);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(this.DTP01_TGBPRYYMM.GetString().ToString().Substring(0, 4)) <= 2017)
            {
                this.ShowCustomMessage("해당년도는 상각작업을 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_TGBPRYYMM);
                e.Successed = false;
                return;
            }

            if (this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4) != this.DTP01_TGBPRYYMM.GetString().ToString().Substring(0, 4))
            {
                this.ShowCustomMessage("시작년도 와 종료 년도가 다릅니다." , "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_GBPRYYMM);
                e.Successed = false;
                return;
            }

            if (  Convert.ToInt16( this.DTP01_GBPRYYMM.GetString().ToString().Substring(4, 2)) > Convert.ToInt16(this.DTP01_TGBPRYYMM.GetString().ToString().Substring(4, 2)))
            {
                this.ShowCustomMessage("시작월 와 종료과 월 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_GBPRYYMM);
                e.Successed = false;
                return;
            }

            // 생성 이전월이 존재하는지 체크
            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();
            if (s전체처리구분 == "A")
            {
                string sYYMM = string.Empty;
                string sYY = string.Empty;
                string sMM = string.Empty;

                if (this.DTP01_GBPRYYMM.GetString().ToString().Substring(4, 2) == "01")
                {
                    sYY = Set_Fill4(Convert.ToString(Convert.ToInt16(this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4)) - 1));
                    sYYMM = sYY + "12";
                }
                else
                {
                    sMM = Set_Fill2(Convert.ToString( Convert.ToInt16(this.DTP01_GBPRYYMM.GetString().ToString().Substring(4, 2)) - 1 )) ;
                    sYYMM = this.DTP01_GBPRYYMM.GetString().ToString().Substring(0, 4) + sMM;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37T27260", sYYMM.ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.ShowCustomMessage("시작월 이전 상각자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.DTP01_GBPRYYMM);
                    e.Successed = false;
                    return;
                }
            }

        }
        #endregion


        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
