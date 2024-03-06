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
    ///  TY_S_US_91OGM581 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME005I : TYBase
    {
        private string fsHMYYMMDD = string.Empty;
        private string fsHMMCYYMM = string.Empty;
        private string fsHMHANGCHA = string.Empty;
        private string fsVSDESC1 = string.Empty;
        private string fsHMIPHANG = string.Empty;
        private string fsHMIANDAT = string.Empty;
        private string fsHMPAYDAT = string.Empty;

        #region Description : 페이지 로드
        public TYUSME005I()
        {
            InitializeComponent();

            // 항차
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91OGM581, "USHANGCHA", "VSCDNM", "USHANGCHA");
            // 곡종
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91OGM581, "USGOKJONG", "GKCDNM", "USGOKJONG");
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_91OGM581, "USHWAJU", "VNSANGHO", "USHWAJU");
        }

        private void TYUSME005I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_91OGM581.Sheets[0].Columns[15].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_91OGM581, "BTN");

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OGM581, "USMCYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OGM581, "USYYMMDD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OGM581, "USHANGCHA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OGM581, "USGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OGM581, "USHWAJU");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_US_91OGM581.Initialize();

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_91OGM581.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_91OGB580",
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHANGCHA.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91OGM581.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_91OGM581.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_91OGM581.ActiveSheet.Cells[i, 12].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91OGM581.ActiveSheet.Cells[i, 12].BackColor = Color.SkyBlue;

                this.FPS91_TY_S_US_91OGM581.ActiveSheet.Cells[i, 13].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91OGM581.ActiveSheet.Cells[i, 13].BackColor = Color.SkyBlue;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            double dUSUSEDVAT = 0;


            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dUSUSEDVAT = 0;

                    dUSUSEDVAT = double.Parse(ds.Tables[0].Rows[i]["USUSEDAMT"].ToString()) * 0.1;

                    this.DbConnector.Attach("TY_P_US_91FGK510", ds.Tables[0].Rows[i]["USMCYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["USHANGCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["USGOKJONG"].ToString(),
                                                                ds.Tables[0].Rows[i]["USHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["USSEQ"].ToString(),
                                                                Get_Date(ds.Tables[0].Rows[i]["USYYMMDD"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["USBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["USHWAKQTY"].ToString()),
                                                                ds.Tables[0].Rows[i]["USCONTNO"].ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["USUSEDAMT"].ToString()),
                                                                Convert.ToString(dUSUSEDVAT),
                                                                "12",
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
                    dUSUSEDVAT = 0;

                    dUSUSEDVAT = double.Parse(ds.Tables[1].Rows[i]["USUSEDAMT"].ToString()) * 0.1;

                    this.DbConnector.Attach("TY_P_US_91OHY593", Get_Numeric(ds.Tables[1].Rows[i]["USBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["USHWAKQTY"].ToString()),
                                                                ds.Tables[1].Rows[i]["USCONTNO"].ToString(),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["USUSEDAMT"].ToString()),
                                                                Convert.ToString(dUSUSEDVAT),
                                                                "12",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["USMCYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["USHANGCHA"].ToString(),
                                                                ds.Tables[1].Rows[i]["USGOKJONG"].ToString(),
                                                                ds.Tables[1].Rows[i]["USHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["USSEQ"].ToString(),
                                                                Get_Date(ds.Tables[1].Rows[i]["USYYMMDD"].ToString())
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
                    this.DbConnector.Attach("TY_P_US_91OHZ594", ds.Tables[2].Rows[i]["USMCYYMM"].ToString(),
                                                                ds.Tables[2].Rows[i]["USHANGCHA"].ToString(),
                                                                ds.Tables[2].Rows[i]["USGOKJONG"].ToString(),
                                                                ds.Tables[2].Rows[i]["USHWAJU"].ToString(),
                                                                ds.Tables[2].Rows[i]["USSEQ"].ToString(),
                                                                Get_Date(ds.Tables[2].Rows[i]["USYYMMDD"].ToString())
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
            ds.Tables.Add(this.FPS91_TY_S_US_91OGM581.GetDataSourceInclude(TSpread.TActionType.New, "USMCYYMM", "USYYMMDD", "USHANGCHA", "USGOKJONG", "USHWAJU", "USSEQ", "USBEJNQTY", "USHWAKQTY", "USCONTNO", "USUSEDAMT", "USUSEDVAT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_91OGM581.GetDataSourceInclude(TSpread.TActionType.Update, "USMCYYMM", "USYYMMDD", "USHANGCHA", "USGOKJONG", "USHWAJU", "USSEQ", "USBEJNQTY", "USHWAKQTY", "USCONTNO", "USUSEDAMT", "USUSEDVAT", "USJPNO"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_91OGM581.GetDataSourceInclude(TSpread.TActionType.Remove, "USMCYYMM", "USYYMMDD", "USHANGCHA", "USGOKJONG", "USHWAJU", "USSEQ", "USJPNO"));

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
                this.DbConnector.Attach("TY_P_US_917AH422", ds.Tables[0].Rows[i]["USMCYYMM"].ToString(),
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
                                       ds.Tables[0].Rows[i]["USHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHP589");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 재고파일(USIJEGOF) 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91OHR590",
                                       ds.Tables[0].Rows[i]["USHANGCHA"].ToString(),
                                       ds.Tables[0].Rows[i]["USGOKJONG"].ToString(),
                                       ds.Tables[0].Rows[i]["USHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHS591");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }


                // 확정량과 배정량 체크
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["USBEJNQTY"].ToString())) < double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["USHWAKQTY"].ToString())))
                {
                    this.ShowMessage("TY_M_US_91OHT592");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 계약번호 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_8BJHK186",
                                       ds.Tables[0].Rows[i]["USCONTNO"].ToString().Substring(0, 4),
                                       ds.Tables[0].Rows[i]["USCONTNO"].ToString().Substring(4, 2)
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BE2E304");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }


                // 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91FGG509",
                                       ds.Tables[0].Rows[i]["USMCYYMM"].ToString(),
                                       ds.Tables[0].Rows[i]["USYYMMDD"].ToString(),
                                       ds.Tables[0].Rows[i]["USHANGCHA"].ToString(),
                                       ds.Tables[0].Rows[i]["USGOKJONG"].ToString(),
                                       ds.Tables[0].Rows[i]["USHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ds.Tables[0].Rows[i]["USSEQ"] = dt.Rows[0]["USSEQ"].ToString();
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["USJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 회계 거래처 등록 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91OHO588",
                                       ds.Tables[1].Rows[i]["USHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHP589");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 재고파일(USIJEGOF) 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_91OHR590",
                                       ds.Tables[1].Rows[i]["USHANGCHA"].ToString(),
                                       ds.Tables[1].Rows[i]["USGOKJONG"].ToString(),
                                       ds.Tables[1].Rows[i]["USHWAJU"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_91OHS591");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }

                // 확정량과 배정량 체크
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["USBEJNQTY"].ToString())) < double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["USHWAKQTY"].ToString())))
                {
                    this.ShowMessage("TY_M_US_91OHT592");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }


                // 계약번호 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_8BJHK186",
                                       ds.Tables[1].Rows[i]["USCONTNO"].ToString().Substring(0, 4),
                                       ds.Tables[1].Rows[i]["USCONTNO"].ToString().Substring(4, 2)
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_2BE2E304");

                    SetFocus(this.DTP01_GDATE);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                if (ds.Tables[2].Rows[i]["USJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.DTP01_GDATE);

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

        #region Description : 화물료 확인 이벤트
        private void UP_RUN()
        {
            //DataTable dt = new DataTable();

            //this.DbConnector.Attach
            //    (
            //    "TY_P_US_7CIA4289",
            //    fsHMYYMMDD.ToString(),
            //    fsHMMCYYMM.ToString(),
            //    fsHMHANGCHA.ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_US_91OGM581.SetValue(dt);

            //for (int i = 0; i < FPS91_TY_S_US_91OGM581.CurrentRowCount; i++)
            //{
            //    if (this.FPS91_TY_S_US_91OGM581.GetValue(i, "USJPNO").ToString() != "")
            //    {
            //        this.FPS91_TY_S_US_91OGM581_Sheet1.Cells[i, 14].Locked = true;
            //    }
            //}
        }
        #endregion

        //#region Description : 스프레드 이벤트
        //private void FPS91_TY_S_US_91OGM581_RowInserted(object sender, TSpread.TAlterEventRow e)
        //{
        //    if (this.FPS91_TY_S_US_91OGM581.ActiveSheet.RowCount > 1)
        //    {
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMYYMMDD",  fsHMYYMMDD);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMMCYYMM",  fsHMMCYYMM);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMHANGCHA", fsHMHANGCHA);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "VSDESC1",   fsVSDESC1);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMIPHANG",  fsHMIPHANG);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMIANDAT",  fsHMIANDAT);
        //        this.FPS91_TY_S_US_91OGM581.SetValue(e.RowIndex, "HMPAYDAT",  fsHMPAYDAT);
        //    }
        //}
        //#endregion

        #region Description : 전표 출력
        private void FPS91_TY_S_US_91OGM581_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "15")
            {
                if (this.FPS91_TY_S_US_91OGM581.GetValue("USJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_US_91OGM581.GetValue("USJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_US_91OGM581.GetValue("USJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_US_91OGM581.GetValue("USJPNO").ToString().Substring(14, 3);

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
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
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