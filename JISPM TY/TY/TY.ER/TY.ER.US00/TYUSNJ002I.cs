using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
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
    ///  TY_S_US_96E9M818 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSNJ002I : TYBase
    {
        #region Description : 페이지 로드
        public TYUSNJ002I()
        {
            InitializeComponent();
        }

        private void TYUSNJ002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96E9M818, "HMYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96E9M818, "HMJUMIN1");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96E9M818, "HMJUMIN2");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96E9M818, "HMSABUN");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_US_96E9M818.Initialize();

            this.TXT01_HMYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            SetStartingFocus(this.TXT01_HMYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_96E9M818.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach("TY_P_US_96E9M817", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", Get_Date(this.TXT01_HMYEAR.GetValue().ToString()), "");

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_96E9M818.SetValue(dt);
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
                this.DbConnector.Attach("TY_P_US_96E9Y825", ds.Tables[0].Rows[i]["HMYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMJUMIN1"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["HMJUMIN2"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["HMNAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMJUSO"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMTEL01"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMTEL02"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMBANGB"].ToString(),
                                                            ds.Tables[0].Rows[i]["HMTSDATE"].ToString()
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96E9Z826", ds.Tables[1].Rows[i]["HMNAME"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMJUSO"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMTEL01"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMTEL02"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMBANGB"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMTSDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["HMSABUN"].ToString(),
                                                             TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[1].Rows[i]["HMJUMIN1"].ToString(),
                                                             TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[1].Rows[i]["HMJUMIN2"].ToString()
                                                            );
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_96EA2827", ds.Tables[2].Rows[i]["HMYEAR"].ToString(),
                                                            ds.Tables[2].Rows[i]["HMSABUN"].ToString(),
                                                            TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[2].Rows[i]["HMJUMIN1"].ToString(),
                                                            TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[2].Rows[i]["HMJUMIN2"].ToString()
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
            ds.Tables.Add(this.FPS91_TY_S_US_96E9M818.GetDataSourceInclude(TSpread.TActionType.New, "HMYEAR", "HMJUMIN1", "HMJUMIN2", "HMSABUN", "HMNAME", "HMJUSO", "HMTEL01", "HMTEL02", "HMBANGB", "HMTSDATE"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_96E9M818.GetDataSourceInclude(TSpread.TActionType.Update, "HMYEAR", "HMJUMIN1", "HMJUMIN2", "HMSABUN", "HMNAME", "HMJUSO", "HMTEL01", "HMTEL02", "HMBANGB", "HMTSDATE"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_96E9M818.GetDataSourceInclude(TSpread.TActionType.Remove, "HMYEAR", "HMJUMIN1", "HMJUMIN2", "HMSABUN"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 이름
                if (ds.Tables[0].Rows[i]["HMNAME"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9T819");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 주소
                if (ds.Tables[0].Rows[i]["HMJUSO"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9T820");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 휴대폰
                if (ds.Tables[0].Rows[i]["HMTEL01"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9U821");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 반
                if (ds.Tables[0].Rows[i]["HMBANGB"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9U822");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 이름
                if (ds.Tables[1].Rows[i]["HMNAME"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9T819");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 주소
                if (ds.Tables[1].Rows[i]["HMJUSO"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9T820");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 휴대폰
                if (ds.Tables[1].Rows[i]["HMTEL01"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9U821");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }

                // 반
                if (ds.Tables[1].Rows[i]["HMBANGB"].ToString() == "")
                {
                    this.ShowMessage("TY_M_US_96E9U822");

                    SetFocus(this.TXT01_HMYEAR);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96E9V823", ds.Tables[2].Rows[i]["HMYEAR"].ToString(), ds.Tables[2].Rows[i]["HMSABUN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_96E9W824");

                    SetFocus(this.TXT01_HMYEAR);

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