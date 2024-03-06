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
    /// 학자금 지원상세내역 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.20 17:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73KEA989 : 학자금 지원상세내역관리 확인
    ///  TY_P_HR_73KEE990 : 학자금 지원상세내역관리 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HKSSABUN : 사번
    ///  HKSHLGUBN : 학력구분
    ///  HKSCRDATE : 지원일자
    ///  HKSPYDATE : 급여년월
    ///  HKSBIGO : 비고
    ///  HKSBUSEO : 부서
    ///  HKSCRBUNGI : 지원분기
    ///  HKSCRYEAR : 지원년도
    ///  HKSDUROKAMT : 등록금
    ///  HKSHAKGI : 학기
    ///  HKSHAKSANGAMT : 학생회비
    ///  HKSHAKYEAR : 학년
    ///  HKSIPHAKAMT : 입학금
    ///  HKSJANGHAKAMT : 장학금
    ///  HKSJKCD : 직급
    ///  HKSSSEQ : 순번
    ///  HKSTOTALAMT : 지급총액
    ///  HKSYEAR : 관리번호
    /// </summary>
    public partial class TYHRKB021I : TYBase
    {
        private string fsHKSHLGUBN; 
        private string fsHKSCRYEAR;
        private string fsHKSCRBUNGI;
        private string fsHKSCRDATE; 
        private string fsHKSSABUN; 
        private string fsHKSYEAR;
        private string fsHKSSSEQ;   

        #region  Description : 폼 로드 이벤트
        public TYHRKB021I(string sHKSHLGUBN,string sHKSCRYEAR,string sHKSCRBUNGI,string sHKSCRDATE,string sHKSSABUN,string sHKSYEAR,string sHKSSSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsHKSHLGUBN = sHKSHLGUBN;
            fsHKSCRYEAR = sHKSCRYEAR;
            fsHKSCRBUNGI = sHKSCRBUNGI;
            fsHKSCRDATE = sHKSCRDATE;
            fsHKSSABUN = sHKSSABUN;
            fsHKSYEAR = sHKSYEAR;
            fsHKSSSEQ = sHKSSSEQ;   
        }

        private void TYHRKB021I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_Run();
        }
        #endregion    
   
        #region  Description : 확인 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73KEA989", fsHKSHLGUBN, fsHKSCRYEAR, fsHKSCRBUNGI, fsHKSCRDATE, fsHKSSABUN, fsHKSYEAR, fsHKSSSEQ );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (fsHKSHLGUBN == "3")
                {
                    LBL51_HKSHAKYEAR.Text = "학년/분기";
                    LBL51_HKINGHAKGI.Text = "지원분기수";
                }

                CBH01_HKSBUSEO.DummyValue = dt.Rows[0]["HKSCRDATE"].ToString();

                this.CurrentDataTableRowMapping(dt, "01");

                //급여지급 완료시 저장 버튼 잠금
                if (dt.Rows[0]["HKSFIXGUBN"].ToString() == "Y")
                {
                    this.BTN61_SAV.SetReadOnly(true);
                    this.BTN61_REM.SetReadOnly(true);
                }

                this.SetStartingFocus(TXT01_HKSHAKYEAR);
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73KEE990", TXT01_HKSHAKYEAR.GetValue().ToString(),
                                                        TXT01_HKSHAKGI.GetValue().ToString(),
                                                        TXT01_HKSIPHAKAMT.GetValue().ToString(),
                                                        TXT01_HKSDUROKAMT.GetValue().ToString(),
                                                        TXT01_HKSHAKSANGAMT.GetValue().ToString(),
                                                        TXT01_HKSTOTALAMT.GetValue().ToString(),
                                                        TXT01_HKSJANGHAKAMT.GetValue().ToString(),
                                                        TXT01_HKSBIGO.GetValue().ToString(),
                                                        TYUserInfo.EmpNo,
                                                        CBH01_HKSHLGUBN.GetValue().ToString(),
                                                        TXT01_HKSCRYEAR.GetValue().ToString(),
                                                        TXT01_HKSCRBUNGI.GetValue().ToString(),
                                                        DTP01_HKSCRDATE.GetString().ToString(),
                                                        CBH01_HKSSABUN.GetValue().ToString(),
                                                        TXT01_HKSYEAR.GetValue().ToString(),
                                                        TXT01_HKSSSEQ.GetValue().ToString()
                                                     );
            // 학자금 기본사항 학년,학기 저장
            this.DbConnector.Attach("TY_P_HR_73LBH021", TXT01_HKSHAKYEAR.GetValue().ToString(),
                                                        TXT01_HKSHAKGI.GetValue().ToString(),
                                                       TYUserInfo.EmpNo,
                                                       CBH01_HKSSABUN.GetValue().ToString(),
                                                       TXT01_HKSYEAR.GetValue().ToString(),
                                                       TXT01_HKSSSEQ.GetValue().ToString()
                                                    );

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //지급총액 계산
            double dTotal = 0;

            dTotal = Convert.ToDouble(Get_Numeric(TXT01_HKSIPHAKAMT.GetValue().ToString())) +
                     Convert.ToDouble(Get_Numeric(TXT01_HKSDUROKAMT.GetValue().ToString())) +
                     Convert.ToDouble(Get_Numeric(TXT01_HKSHAKSANGAMT.GetValue().ToString()));

            TXT01_HKSTOTALAMT.SetValue(dTotal.ToString());

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sLast_HKSHAKYEAR = string.Empty;
            string sLast_HKSHAKGI = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73L8N010",
                                                        CBH01_HKSHLGUBN.GetValue().ToString(),
                                                        TXT01_HKSCRYEAR.GetValue().ToString(),
                                                        TXT01_HKSCRBUNGI.GetValue().ToString(),
                                                        DTP01_HKSCRDATE.GetString().ToString(),
                                                        CBH01_HKSSABUN.GetValue().ToString(),
                                                        TXT01_HKSYEAR.GetValue().ToString(),
                                                        TXT01_HKSSSEQ.GetValue().ToString()
                                                     );
            this.DbConnector.ExecuteTranQuery();

            //학자금 지원내역에서 마지막 지원내역 찾기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73L8Q012", CBH01_HKSSABUN.GetValue().ToString(),
                                                        TXT01_HKSYEAR.GetValue().ToString(),
                                                        TXT01_HKSSSEQ.GetValue().ToString()
                                                     );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sLast_HKSHAKYEAR = dt.Rows[i]["HKSHAKYEAR"].ToString();
                    sLast_HKSHAKGI = dt.Rows[i]["HKSHAKGI"].ToString();
                }
            }
            else
            {
                sLast_HKSHAKYEAR = "1";
                sLast_HKSHAKGI = "1";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73L8P011", sLast_HKSHAKYEAR,
                                                        sLast_HKSHAKGI,
                                                        TYUserInfo.EmpNo,
                                                        CBH01_HKSSABUN.GetValue().ToString(),
                                                        TXT01_HKSYEAR.GetValue().ToString(),
                                                        TXT01_HKSSSEQ.GetValue().ToString()
                                                     );
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //급여 지급내역 체크
            if (CBO01_HKSFIXGUBN.GetValue().ToString() == "Y")
            {
                this.ShowCustomMessage("지급이 완료된 자료는 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }            

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
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
