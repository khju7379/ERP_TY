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
    ///  TY_S_US_92DCV741 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME060I : TYBase
    {
        private string fsHMYYMMDD  = string.Empty;
        private string fsHMMCYYMM  = string.Empty;
        private string fsHMHANGCHA = string.Empty;
        private string fsVSDESC1   = string.Empty;
        private string fsHMIPHANG  = string.Empty;
        private string fsHMIANDAT  = string.Empty;
        private string fsHMPAYDAT  = string.Empty;

        #region Description : 페이지 로드
        public TYUSME060I()
        {
            InitializeComponent();

            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_92DCV741, "SGHWAJU",  "VNSANGHO", "SGHWAJU");
            // 항차
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_92DCV741, "SGHANGCHA", "VSDESC1", "SGHANGCHA");
            // 곡종
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_92DCV741, "SGGOKJONG", "GKDESC1", "SGGOKJONG");
        }

        private void TYUSME060I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_92DCV741.Sheets[0].Columns[19].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92DCV741, "BTN");

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGMAECH");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGYYMMDD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGHWAJU");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGHANGCHA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGSEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_92DCV741, "SGMCDATE");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            
            this.FPS91_TY_S_US_92DCV741.Initialize();


            this.MTB01_GSTDATE.SetValue(DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"));
            this.MTB01_GEDDATE.SetValue(DateTime.Now.AddMonths(+1).ToString("yyyy-MM-dd"));

            SetStartingFocus(this.MTB01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92DCV741.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_92DBY740",
                Get_Date(this.MTB01_GSTDATE.GetValue().ToString()),
                Get_Date(this.MTB01_GEDDATE.GetValue().ToString()),
                this.CBO01_GMEGUBUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_92DCV741.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_92DCV741.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_92DCV741.ActiveSheet.Cells[i, 16].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_92DCV741.ActiveSheet.Cells[i, 16].BackColor = Color.SkyBlue;

                this.FPS91_TY_S_US_92DCV741.ActiveSheet.Cells[i, 17].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_92DCV741.ActiveSheet.Cells[i, 17].BackColor = Color.SkyBlue;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_92DDU742", ds.Tables[0].Rows[i]["SGMAECH"].ToString(),
                                                            Get_Date(ds.Tables[0].Rows[i]["SGYYMMDD"].ToString()),
                                                            ds.Tables[0].Rows[i]["SGHWAJU"].ToString(),
                                                            ds.Tables[0].Rows[i]["SGHANGCHA"].ToString(),
                                                            ds.Tables[0].Rows[i]["SGGOKJONG"].ToString(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGSEQ"].ToString()),
                                                            Get_Date(ds.Tables[0].Rows[i]["SGMCDATE"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGBEJNQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGHWAKQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGCHQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGYDQTY"].ToString()),
                                                            ds.Tables[0].Rows[i]["SGCONTNO"].ToString(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGDANGA"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGAMT"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["SGVAT"].ToString()),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_92DDW743", Get_Numeric(ds.Tables[1].Rows[i]["SGBEJNQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGHWAKQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGCHQTY"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGYDQTY"].ToString()),
                                                            ds.Tables[1].Rows[i]["SGCONTNO"].ToString(),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGDANGA"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGAMT"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGVAT"].ToString()),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["SGMAECH"].ToString(),
                                                            Get_Date(ds.Tables[1].Rows[i]["SGYYMMDD"].ToString()),
                                                            ds.Tables[1].Rows[i]["SGHWAJU"].ToString(),
                                                            ds.Tables[1].Rows[i]["SGHANGCHA"].ToString(),
                                                            ds.Tables[1].Rows[i]["SGGOKJONG"].ToString(),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["SGSEQ"].ToString()),
                                                            Get_Date(ds.Tables[1].Rows[i]["SGMCDATE"].ToString())
                                                            );
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_92DDW744", ds.Tables[2].Rows[i]["SGMAECH"].ToString(),
                                                            Get_Date(ds.Tables[2].Rows[i]["SGYYMMDD"].ToString()),
                                                            ds.Tables[2].Rows[i]["SGHWAJU"].ToString(),
                                                            ds.Tables[2].Rows[i]["SGHANGCHA"].ToString(),
                                                            ds.Tables[2].Rows[i]["SGGOKJONG"].ToString(),
                                                            Get_Numeric(ds.Tables[2].Rows[i]["SGSEQ"].ToString()),
                                                            Get_Date(ds.Tables[2].Rows[i]["SGMCDATE"].ToString())
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BF50354"); // 처리 메세지
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
            ds.Tables.Add(this.FPS91_TY_S_US_92DCV741.GetDataSourceInclude(TSpread.TActionType.New,    "SGMAECH", "SGYYMMDD", "SGHWAJU", "SGHANGCHA", "SGGOKJONG", "SGSEQ", "SGMCDATE", "SGBEJNQTY", "SGHWAKQTY", "SGCHQTY", "SGYDQTY", "SGCONTNO", "SGDANGA", "SGAMT", "SGVAT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_92DCV741.GetDataSourceInclude(TSpread.TActionType.Update, "SGMAECH", "SGYYMMDD", "SGHWAJU", "SGHANGCHA", "SGGOKJONG", "SGSEQ", "SGMCDATE", "SGBEJNQTY", "SGHWAKQTY", "SGCHQTY", "SGYDQTY", "SGCONTNO", "SGDANGA", "SGAMT", "SGVAT", "SGJPNO"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_92DCV741.GetDataSourceInclude(TSpread.TActionType.Remove, "SGMAECH", "SGYYMMDD", "SGHWAJU", "SGHANGCHA", "SGGOKJONG", "SGSEQ", "SGMCDATE", "SGJPNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["SGJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.MTB01_GSTDATE);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                if (ds.Tables[2].Rows[i]["SGJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.MTB01_GSTDATE);

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

        #region Description : 전표 출력
        private void FPS91_TY_S_US_92DCV741_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "19")
            {
                if (this.FPS91_TY_S_US_92DCV741.GetValue("SGJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_US_92DCV741.GetValue("SGJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_US_92DCV741.GetValue("SGJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_US_92DCV741.GetValue("SGJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }

            }
        }
        #endregion
    }
}