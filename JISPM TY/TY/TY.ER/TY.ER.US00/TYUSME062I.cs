using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_92CE5728 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUSME062I : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM    = string.Empty;
        private string fsRRNUM    = string.Empty;
        private string fsVEND     = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND   = string.Empty;
        private string fsCHGUBUN  = string.Empty;
        private string fsGUBUN    = string.Empty;

        private string fsIJWKTYPE  = string.Empty;
        private string fsIJTMGUBN  = string.Empty;
        private string fsIJIPDATE  = string.Empty;

        private string fsIJHWAJU   = string.Empty;
        private string fsIJHWAMUL  = string.Empty;
        private string fsIJTANKNO  = string.Empty;
        private string fsIJIPQTY   = string.Empty;

        private string fsIJCARNO   = string.Empty;
        private string fsIJCONTAIN = string.Empty;
        private string fsIJSEALNUM = string.Empty;

        private string fsIJIPTIME1 = string.Empty;
        private string fsIJIPTIME2 = string.Empty;

        private string fsIJDESC    = string.Empty;

        private string fsHJDESC1   = string.Empty;
        private string fsHMDESC1   = string.Empty;

        #region Description : 페이지 로드
        public TYUSME062I()
        {
            InitializeComponent();
        }

        private void TYUSME062I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92CE5728, "SDNDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92CE5728, "SDNMAECH");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92CE5728, "SDNGUBN");

            this.DTP01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            


            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_US_92CE5728.Initialize();

            this.BTN61_INQ_Click(null, null);

            this.SetFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_92CE4727",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_92CE5728.SetValue(dt);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sIJIPTIME = string.Empty;
            string sIJNUMBER = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 저장
            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                this.DbConnector.Attach("TY_P_US_92CE1724", ds.Tables[0].Rows[i]["SDNDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["SDNMAECH"].ToString(),
                                                            ds.Tables[0].Rows[i]["SDNGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["SDNDANGA"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_92CE3725", ds.Tables[1].Rows[i]["SDNDANGA"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["SDNDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["SDNMAECH"].ToString(),
                                                            ds.Tables[1].Rows[i]["SDNGUBN"].ToString()
                                                            );
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_92CE3726", ds.Tables[2].Rows[i]["SDNDATE"].ToString(),
                                                            ds.Tables[2].Rows[i]["SDNMAECH"].ToString(),
                                                            ds.Tables[2].Rows[i]["SDNGUBN"].ToString()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_2BF50354"); // 처리 메세지
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_92CE5728.GetDataSourceInclude(TSpread.TActionType.New,    "SDNDATE", "SDNMAECH", "SDNGUBN", "SDNDANGA"));
            ds.Tables.Add(this.FPS91_TY_S_US_92CE5728.GetDataSourceInclude(TSpread.TActionType.Update, "SDNDATE", "SDNMAECH", "SDNGUBN", "SDNDANGA"));
            ds.Tables.Add(this.FPS91_TY_S_US_92CE5728.GetDataSourceInclude(TSpread.TActionType.Remove, "SDNDATE", "SDNMAECH", "SDNGUBN"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            // 처리 하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}