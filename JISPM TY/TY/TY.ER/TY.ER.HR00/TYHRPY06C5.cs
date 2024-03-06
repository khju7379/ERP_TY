using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금 지급대상자 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.21 09:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CCDK789 : 개인급여추가제외관리 등록
    ///  TY_P_HR_73LA2014 : 학자금 지급대상자 조회
    ///  TY_P_HR_73LA6017 : 학자금 지원상세내역 급여지급구분 UPDATE
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73LA3015 : 학자금 지급대상자 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  SAV : 저장
    ///  PTGUBN : 급여구분
    ///  PTJIDATE : 지급일자
    ///  PTYYMM : 급여년월
    ///  ESCEMPCNT : 종업원수
    /// </summary>
    public partial class TYHRPY06C5 : TYBase
    {

        private string fsPTGUBN = string.Empty;
        private string fsPTYYMM = string.Empty;
        private string fsPTJIDATE = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRPY06C5(string sPTGUBN, string sPTYYMM, string sPTJIDATE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsPTGUBN = sPTGUBN;
            this.fsPTYYMM = sPTYYMM;
            this.fsPTJIDATE = sPTJIDATE;

        }

        private void TYHRPY06C5_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_PTGUBN.SetValue(fsPTGUBN);
            this.DTP01_PTYYMM.SetValue(fsPTYYMM);
            this.DTP01_PTJIDATE.SetValue(fsPTJIDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73LA3015.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73LA2014", this.DTP01_PTYYMM.GetString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_73LA3015.SetValue(dt);

            if (this.FPS91_TY_S_HR_73LA3015.CurrentRowCount > 0)
            {
                TXT01_ESCEMPCNT.SetValue(dt.Rows.Count.ToString());

                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_73LA3015, "HKSSABUNNM", "합   계", SumRowType.Sum, "HKSTOTALAMT");                
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73LA2014", this.DTP01_PTYYMM.GetString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //개인급여추가항목관리
                    this.DbConnector.Attach("TY_P_HR_4CCDK789", "A",
                                                              dt.Rows[i]["HKSSABUN"].ToString(),
                                                              CBH01_PTGUBN.GetValue().ToString(),
                                                              "1510",
                                                              DTP01_PTJIDATE.GetString().ToString().Substring(0,6)+"01",
                                                              dt.Rows[i]["HKSTOTALAMT"].ToString(),
                                                              DTP01_PTJIDATE.GetString().ToString().Substring(0,6) + UP_GetLastDay(DTP01_PTJIDATE.GetString().ToString().Substring(0,6)),
                                                              "학자금",
                                                              "",
                                                              TYUserInfo.EmpNo
                                                              );
                    this.DbConnector.Attach("TY_P_HR_73LA6017", "Y",
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[i]["HKSSABUN"].ToString(),
                                                                DTP01_PTYYMM.GetString().ToString().Substring(0,6)
                                                             );

                }
                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_HR_73LAW019");

            this.BTN61_CLO_Click(null, null);
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_HR_73LAH018"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private string  UP_GetLastDay(string sYYMM)
        {
            int iDD = 0;

            string sYear = sYYMM.Substring(0, 4);
            string sMonth = sYYMM.Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

            return iDD.ToString();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
