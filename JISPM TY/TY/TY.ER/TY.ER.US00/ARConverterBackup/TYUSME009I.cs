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
    ///  TY_S_US_91SE0611 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME009I : TYBase
    {
        private string fsHMYYMMDD  = string.Empty;
        private string fsHMMCYYMM  = string.Empty;
        private string fsHMHANGCHA = string.Empty;
        private string fsVSDESC1   = string.Empty;
        private string fsHMIPHANG  = string.Empty;
        private string fsHMIANDAT  = string.Empty;
        private string fsHMPAYDAT  = string.Empty;

        #region Description : 페이지 로드
        public TYUSME009I()
        {
            InitializeComponent();

            // 항차
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91SE0611, "LMHANGCHA", "VSDESC1", "LMHANGCHA");
            // 곡종
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91SE0611, "LMGOKJONG", "GKDESC1", "LMGOKJONG");
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91SE0611, "LMHWAJU", "VNSANGHO", "LMHWAJU");
        }

        private void TYUSME009I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_91SE0611.Sheets[0].Columns[13].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_91SE0611, "BTN");

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91SE0611, "LMMCYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91SE0611, "LMYYMMDD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91SE0611, "LMHANGCHA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91SE0611, "LMGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91SE0611, "LMHWAJU");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            
            this.FPS91_TY_S_US_91SE0611.Initialize();

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_91SE0611.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_91SDW608",
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHANGCHA.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91SE0611.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_91SE0611.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 9].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;

                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 10].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 10].BackColor = Color.SkyBlue;

                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91SE0611.ActiveSheet.Cells[i, 11].BackColor = Color.SkyBlue;
            }
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_91PBT598", ds.Tables[0].Rows[i]["LMMCYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["LMHANGCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["LMGOKJONG"].ToString(),
                                                                ds.Tables[0].Rows[i]["LMHWAJU"].ToString(),
                                                                Get_Date(ds.Tables[0].Rows[i]["LMYYMMDD"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["LMBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["LMHJGONG"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["LMHJVAT"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["LMHJAMT"].ToString()),
                                                                "15",
                                                                "1",
                                                                TYUserInfo.EmpNo
                                                                ); // 저장
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_91SDZ610", Get_Numeric(ds.Tables[1].Rows[i]["LMBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["LMHJGONG"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["LMHJVAT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["LMHJAMT"].ToString()),
                                                                "15",
                                                                "1",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["LMMCYYMM"].ToString(),
                                                                Get_Date(ds.Tables[1].Rows[i]["LMYYMMDD"].ToString()),
                                                                ds.Tables[1].Rows[i]["LMHANGCHA"].ToString(),
                                                                ds.Tables[1].Rows[i]["LMGOKJONG"].ToString(),
                                                                ds.Tables[1].Rows[i]["LMHWAJU"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_91SDY609", ds.Tables[2].Rows[i]["LMMCYYMM"].ToString(),
                                                                Get_Date(ds.Tables[2].Rows[i]["LMYYMMDD"].ToString()),
                                                                ds.Tables[2].Rows[i]["LMHANGCHA"].ToString(),
                                                                ds.Tables[2].Rows[i]["LMGOKJONG"].ToString(),
                                                                ds.Tables[2].Rows[i]["LMHWAJU"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }

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
            ds.Tables.Add(this.FPS91_TY_S_US_91SE0611.GetDataSourceInclude(TSpread.TActionType.New,    "LMMCYYMM", "LMYYMMDD", "LMHANGCHA", "LMGOKJONG", "LMHWAJU", "LMBEJNQTY", "LMHJGONG", "LMHJVAT", "LMHJAMT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_91SE0611.GetDataSourceInclude(TSpread.TActionType.Update, "LMMCYYMM", "LMYYMMDD", "LMHANGCHA", "LMGOKJONG", "LMHWAJU", "LMBEJNQTY", "LMHJGONG", "LMHJVAT", "LMHJAMT", "LMJPNO"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_91SE0611.GetDataSourceInclude(TSpread.TActionType.Remove, "LMMCYYMM", "LMYYMMDD", "LMHANGCHA", "LMGOKJONG", "LMHWAJU", "LMJPNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 매출발생월 다음달 미수금액이 존재하는지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_917AH422", ds.Tables[0].Rows[i]["LMMCYYMM"].ToString(),
                                                            "");
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_917AL423");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 회계 거래처 등록 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91OHO588",
                                       ds.Tables[0].Rows[i]["LMHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHP589");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 입항파일 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91SEK613",
                                       ds.Tables[0].Rows[i]["LMHANGCHA"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91SEL614");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 조출료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91SEM615",
                                       ds.Tables[0].Rows[i]["LMMCYYMM"].ToString(),
                                       ds.Tables[0].Rows[i]["LMYYMMDD"].ToString(),
                                       ds.Tables[0].Rows[i]["LMHANGCHA"].ToString(),
                                       ds.Tables[0].Rows[i]["LMGOKJONG"].ToString(),
                                       ds.Tables[0].Rows[i]["LMHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_91SEN618");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 공급가
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LMHJGONG"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_91SEY621");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 부가세
                ds.Tables[0].Rows[i]["LMHJVAT"] = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LMHJGONG"].ToString())) * 0.1));

                // 금액 = 공급가 + 부가세액
                ds.Tables[0].Rows[i]["LMHJAMT"] = Convert.ToString(double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LMHJGONG"].ToString())) + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["LMHJVAT"].ToString())));
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["LMJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 매출발생월 다음달 미수금액이 존재하는지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_917AH422", ds.Tables[1].Rows[i]["LMMCYYMM"].ToString(),
                                                            "");
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_917AL423");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 회계 거래처 등록 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91OHO588",
                                       ds.Tables[1].Rows[i]["LMHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHP589");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 입항파일 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91SEK613",
                                       ds.Tables[1].Rows[i]["LMHANGCHA"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91SEL614");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }                

                // 조출료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91SEM615",
                                       ds.Tables[1].Rows[i]["LMMCYYMM"].ToString(),
                                       ds.Tables[1].Rows[i]["LMYYMMDD"].ToString(),
                                       ds.Tables[1].Rows[i]["LMHANGCHA"].ToString(),
                                       ds.Tables[1].Rows[i]["LMGOKJONG"].ToString(),
                                       ds.Tables[1].Rows[i]["LMHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91NBH550");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt.Rows[0]["LMJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_US_7CIDF300");

                        SetFocus(this.DTP01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }

                // 공급가
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["LMHJGONG"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_91SEY621");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 부가세
                ds.Tables[1].Rows[i]["LMHJVAT"] = UP_DotDelete(Convert.ToString(double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["LMHJGONG"].ToString())) * 0.1));

                // 금액 = 공급가 + 부가세액
                ds.Tables[1].Rows[i]["LMHJAMT"] = Convert.ToString(double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["LMHJGONG"].ToString())) + double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["LMHJVAT"].ToString())));
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                if (ds.Tables[2].Rows[i]["LMJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 매출발생월 다음달 미수금액이 존재하는지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_917AH422", ds.Tables[2].Rows[i]["LMMCYYMM"].ToString(),
                                                            "");
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_917AL423");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 조출료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91SEM615",
                                       ds.Tables[2].Rows[i]["LMMCYYMM"].ToString(),
                                       ds.Tables[2].Rows[i]["LMYYMMDD"].ToString(),
                                       ds.Tables[2].Rows[i]["LMHANGCHA"].ToString(),
                                       ds.Tables[2].Rows[i]["LMGOKJONG"].ToString(),
                                       ds.Tables[2].Rows[i]["LMHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91NBH550");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt.Rows[0]["LMJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_US_7CIDF300");

                        SetFocus(this.DTP01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }
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

        #region Description : 전표 출력
        private void FPS91_TY_S_US_91SE0611_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "13")
            {
                if (this.FPS91_TY_S_US_91SE0611.GetValue("LMJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_US_91SE0611.GetValue("LMJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_US_91SE0611.GetValue("LMJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_US_91SE0611.GetValue("LMJPNO").ToString().Substring(14, 3);

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