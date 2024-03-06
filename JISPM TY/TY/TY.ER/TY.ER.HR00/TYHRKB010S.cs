using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인사기록 통합 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.12 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51D9V117 : 인사기록통합조회(학력)
    ///  TY_P_HR_51D9X119 : 인사기록통합조회(경력)
    ///  TY_P_HR_51D9Y120 : 인사기록통합조회(교육)
    ///  TY_P_HR_51DAN121 : 인사기록통합조회(자격)
    ///  TY_P_HR_51DAQ122 : 인사기록통합조회(가족)
    ///  TY_P_HR_51DAV123 : 인사기록통합조회(포상)
    ///  TY_P_HR_51DAY124 : 인사기록통합조회(징계)
    ///  TY_P_HR_51DB5125 : 인사기록통합조회(보증)
    ///  TY_P_HR_51DD6126 : 인사기록통합조회(병력)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_51DFY128 : 인사기록통합조회(학력)
    ///  TY_S_HR_51DFZ129 : 인사기록통합조회(경력)
    ///  TY_S_HR_51E9N135 : 인사기록통합조회(교육)
    ///  TY_S_HR_51E9S136 : 인사기록통합조회(자격)
    ///  TY_S_HR_51E9T137 : 인사기록통합조회(보증)
    ///  TY_S_HR_51E9T138 : 인사기록통합조회(가족)
    ///  TY_S_HR_51E9U139 : 인사기록통합조회(병력)
    ///  TY_S_HR_51E9W140 : 인사기록통합조회(포상)
    ///  TY_S_HR_51E9X142 : 인사기록통합조회(징계)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  BJSABUN : 사번
    ///  GBGBCODE : 군별코드
    ///  GBSABUN : 사번
    ///  GBYJCODE : 역종코드
    ///  GJCODE : 가족코드
    ///  GJSABUN : 사번
    ///  GYSABUN : 사  번
    ///  HLHAKKYO : 학교
    ///  HLSABUN : 사번
    ///  JKCODE : 자격코드
    ///  JKSABUN : 사번
    ///  KBBUSEO : 부서
    ///  KLSABUN : 사번
    ///  PRGUBUN : 포상코드
    ///  PRSABUN : 사번
    ///  SBGUBUN : 징계코드
    ///  SBSABUN : 사번
    ///  BJGUBUN : 보증구분
    ///  BJIDATE1 : 보증시작일
    ///  EDDATE : 종료일자
    ///  GYIDATE : 교육시작일
    ///  GYJDATE : 교육종료일
    ///  HLIDATE : 입학일자
    ///  HLJDATE : 졸업일자
    ///  SBKDATE1 : 징계기간1
    ///  SBKDATE2 : 징계기간2
    ///  STDATE : 시작일자
    ///  GJJUMIN : 주민번호6자리
    ///  KLJIKJGNG : 회사명
    /// </summary>
    public partial class TYHRKB010S : TYBase
    {
        #region Descripgion : 폼 로드
        public TYHRKB010S()
        {
            InitializeComponent();
        }

        private void TYHRKB010S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.DTP01_HLIDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_HLJDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GYIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GYJDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_BJIDATE1.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_SBKDATE1.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_SBKDATE2.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);
        }
        #endregion

        #region Descripgion : 학력사항 조회버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(this.DTP01_HLIDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_HLJDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }        
            

            this.FPS91_TY_S_HR_51DFY128.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51D9V117", this.CBH01_HLSABUN.GetValue().ToString(),
                                                        this.DTP01_HLIDATE.GetString(),
                                                        this.DTP01_HLJDATE.GetString(),
                                                        this.CBH01_HLHAKKYO.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51DFY128.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 경력사항 조회버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_51DFZ129.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51D9X119", this.CBH01_KLSABUN.GetValue().ToString(),
                                                        this.TXT01_KLJIKJGNG.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51DFZ129.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 교육사항 조회버튼 이벤트
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            string sGYJDATE = string.Empty;

            //if (this.DTP01_GYJDATE.GetString() == "19000101")
            //{
            //    sGYJDATE = "99991231";
            //}

            if (Convert.ToInt32(this.DTP01_GYIDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_GYJDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }          

            this.FPS91_TY_S_HR_51E9N135.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51D9Y120", this.CBH01_GYSABUN.GetValue().ToString(),
                                                        this.DTP01_GYIDATE.GetString(),
                                                        this.DTP01_GYJDATE.GetString().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9N135.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 자격사항 조회버튼 이벤트
        private void BTN64_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_51E9S136.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DAN121", this.CBH01_KBBUSEO.GetValue().ToString(),
                                                        this.CBH01_JKSABUN.GetValue().ToString(),
                                                        this.CBH01_JKCODE.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9S136.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 보증사항 조회버튼 이벤트
        private void BTN65_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_51E9T137.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DB5125", this.CBH01_BJSABUN.GetValue().ToString(),
                                                        this.DTP01_BJIDATE1.GetString(),
                                                        this.CBO01_BJGUBUN.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9T137.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 가족사항 조회버튼 이벤트
        private void BTN66_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_51E9T138.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DAQ122", TYUserInfo.SecureKey, 
                                                        CBO01_INQ_AUTH.GetValue().ToString(),
                                                        this.CBH01_GJSABUN.GetValue().ToString(),
                                                        this.CBH01_GJCODE.GetValue().ToString(),
                                                        this.TXT01_GJJUMIN.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9T138.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 병력사항 조회버튼 이벤트
        private void BTN67_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_51E9U139.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DD6126", this.CBH01_GBSABUN.GetValue().ToString(),
                                                        this.CBH01_GBGBCODE.GetValue().ToString(),
                                                        this.CBH01_GBYJCODE.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9U139.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 포상사항 조회버튼 이벤트
        private void BTN68_INQ_Click(object sender, EventArgs e)
        {
            string sEDDATE = string.Empty;

            //if (this.DTP01_EDDATE.GetString() == "19000101")
            //{
            //    sEDDATE = "99991231";
            //}

            if (Convert.ToInt32(this.DTP01_STDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }          

            this.FPS91_TY_S_HR_51E9W140.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DAV123", this.CBH01_SBSABUN.GetValue().ToString(),
                                                        this.CBH01_PRGUBUN.GetValue().ToString(),
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9W140.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descripgion : 징계사항 조회버튼 이벤트
        private void BTN69_INQ_Click(object sender, EventArgs e)
        {
            string sSBKDATE2 = string.Empty;

            //if (this.DTP01_SBKDATE2.GetString() == "19000101")
            //{
            //    sSBKDATE2 = "99991231";
            //}

            if (Convert.ToInt32(this.DTP01_SBKDATE1.GetString().ToString()) > Convert.ToInt32(this.DTP01_SBKDATE2.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }         

            this.FPS91_TY_S_HR_51E9X142.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51DAY124", this.CBH01_PRSABUN.GetValue().ToString(),
                                                        this.CBH01_SBGUBUN.GetValue().ToString(),
                                                        this.DTP01_SBKDATE1.GetString(),
                                                        this.DTP01_SBKDATE2.GetString().ToString()
                                                        );

            this.FPS91_TY_S_HR_51E9X142.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}
