using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

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
    ///  TY_S_US_96EAS833 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSNJ003I : TYBase
    {
        #region Description : 페이지 로드
        public TYUSNJ003I()
        {
            InitializeComponent();
        }

        private void TYUSNJ003I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96EAS833, "HUYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96EAS833, "HUSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96EAS833, "HUSEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96EAS833, "HUYEAR");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            
            this.FPS91_TY_S_US_96EAS833.Initialize();

            this.DTP01_HUYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_HUYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_96EAS833.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach("TY_P_US_96EAP829", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", Get_Date(this.DTP01_HUYYMM.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_96EAS833.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96EAP830", ds.Tables[0].Rows[i]["HUYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUNAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUBIRTH"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["HUJUMIN"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["HUJUSO"].ToString(),
                                                            ds.Tables[0].Rows[i]["HUGUBN"].ToString()
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96EAP831", ds.Tables[1].Rows[i]["HUNAME"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUBIRTH"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[1].Rows[i]["HUJUMIN"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[1].Rows[i]["HUJUSO"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUGUBN"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUSABUN"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUSEQ"].ToString(),
                                                            ds.Tables[1].Rows[i]["HUYEAR"].ToString()
                                                            );
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96EAQ832", ds.Tables[2].Rows[i]["HUYYMM"].ToString(),
                                                            ds.Tables[2].Rows[i]["HUSABUN"].ToString(),
                                                            ds.Tables[2].Rows[i]["HUSEQ"].ToString(),
                                                            ds.Tables[2].Rows[i]["HUYEAR"].ToString()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();
            

            this.ShowMessage("TY_M_MR_2BF50354"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_96EAS833.GetDataSourceInclude(TSpread.TActionType.New,    "HUYYMM", "HUSABUN", "HUSEQ", "HUYEAR", "HUNAME", "HUBIRTH", "HUJUMIN", "HUJUSO", "HUGUBN"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_96EAS833.GetDataSourceInclude(TSpread.TActionType.Update, "HUYYMM", "HUSABUN", "HUSEQ", "HUYEAR", "HUNAME", "HUBIRTH", "HUJUMIN", "HUJUSO", "HUGUBN"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_96EAS833.GetDataSourceInclude(TSpread.TActionType.Remove, "HUYYMM", "HUSABUN", "HUSEQ", "HUYEAR"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 항운노조 년별 명부 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96E9M817", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", ds.Tables[0].Rows[i]["HUYYMM"].ToString().Substring(0, 4), ds.Tables[0].Rows[i]["HUSABUN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 순번
                    ds.Tables[0].Rows[i]["HUYEAR"] = "1";
                    // 적용년도
                    ds.Tables[0].Rows[i]["HUYEAR"] = ds.Tables[0].Rows[i]["HUYYMM"].ToString().Substring(0, 4);
                    // 이름
                    ds.Tables[0].Rows[i]["HUNAME"] = dt.Rows[0]["HMNAME"].ToString();
                    // 생년월일
                    ds.Tables[0].Rows[i]["HUBIRTH"] = dt.Rows[0]["HMJUMIN1"].ToString();
                    // 주민번호
                    ds.Tables[0].Rows[i]["HUJUMIN"] = dt.Rows[0]["HMJUMIN2"].ToString();
                    // 주소
                    ds.Tables[0].Rows[i]["HUJUSO"] = dt.Rows[0]["HMJUSO"].ToString();
                    // 반
                    ds.Tables[0].Rows[i]["HUGUBN"] = dt.Rows[0]["HMBANGB"].ToString();
                }
                else
                {
                    this.ShowMessage("TY_M_US_96EAZ834");

                    SetFocus(this.DTP01_HUYYMM);

                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 항운노조 년별 명부 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96E9M817", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y",  ds.Tables[1].Rows[i]["HUYYMM"].ToString().Substring(0, 4), ds.Tables[1].Rows[i]["HUSABUN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 이름
                    ds.Tables[1].Rows[i]["HUNAME"]  = dt.Rows[0]["HMNAME"].ToString();
                    // 생년월일
                    ds.Tables[1].Rows[i]["HUBIRTH"] = dt.Rows[0]["HMJUMIN1"].ToString();
                    // 주민번호
                    ds.Tables[1].Rows[i]["HUJUMIN"] = dt.Rows[0]["HMJUMIN2"].ToString();
                    // 주소
                    ds.Tables[1].Rows[i]["HUJUSO"]  = dt.Rows[0]["HMJUSO"].ToString();
                    // 반
                    ds.Tables[1].Rows[i]["HUGUBN"]  = dt.Rows[0]["HMBANGB"].ToString();
                }
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96EB5835", ds.Tables[2].Rows[i]["HUYYMM"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_96EB6836");

                    SetFocus(this.DTP01_HUYYMM);

                    e.Successed = false;
                    return;
                }
            }            

            // 저장하시겠습니까?
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