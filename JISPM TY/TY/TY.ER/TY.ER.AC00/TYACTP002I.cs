using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 이자배당소득 추가입력 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.09.19 15:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49JFL983 : 이자배당소득 추가입력 조회(그룹)
    ///  TY_P_AC_49JGP986 : 이자배당소득 추가입력 조회(상세)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_49JFJ981 : 이자배당소득 추가입력 조회(그룹)
    ///  TY_S_AC_49JFM984 : 이자배당소득 추가입력 조회(상세)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INP_MONE : 저장
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WICCODE : 소득 종류
    ///  WSINCOME : 소득자 구분
    /// </summary>
    public partial class TYACTP002I : TYBase
    {
        public TYACTP002I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFJ981, "WINCOME", "WINCOMENM", "WINCOME");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFJ981, "WICCODE", "WICCODENM", "WICCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFJ981, "WFINANC", "WFINANCNM", "WFINANC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFJ981, "WSPECTAX", "WSPECTAXNM", "WSPECTAX");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFJ981, "WPMPOTAX", "WPMPOTAXNM", "WPMPOTAX");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JFM984, "WSBANKCD", "WSBANKCDNM", "WSBANKCD");
        }

        private void TYACTP002I_Load(object sender, System.EventArgs e)
        {
            // 마스타 Key필드 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JFJ981, "WINCOME", "WINCOMENM", "WICCODE", "WICCODENM", "WSECOUR");
            // 디테일 Key필드 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JFM984, "WSINCOME", "WSINCOMENM", "WSICCODE", "WSICCODENM", "WSRESIDE");
            // 마스타 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 마스타 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            // 디테일 등록 체크
            this.BTN61_INP_MONE.ProcessCheck += new TButton.CheckHandler(BTN61_INP_MONE_ProcessCheck);
            // 디테일 삭제 체크
            this.BTN61_DETAIL_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_DETAIL_DEL_ProcessCheck);
            
            this.FPS91_TY_S_AC_49JFJ981.Initialize();
            this.FPS91_TY_S_AC_49JFM984.Initialize();

            this.BTN61_INQ_Click(null, null);

            BTN61_DETAIL_DEL.Visible = false;
            BTN61_INP_MONE.Visible = false;
        }

        #region Description : 이자배당소득 마스타 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_49JFL983",
                this.CBH01_WINCOME.GetValue().ToString(),
                this.CBH01_WICCODE.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_49JFJ981.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 이자배당소득 디테일 조회
        private void FPS91_TY_S_AC_49JFJ981_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_AC_49JGP986",
                this.FPS91_TY_S_AC_49JFJ981.GetValue("WINCOME").ToString(),
                this.FPS91_TY_S_AC_49JFJ981.GetValue("WICCODE").ToString(),
                TYUserInfo.SecureKey,
                "Y",
                ""
                );

            this.FPS91_TY_S_AC_49JFM984.SetValue(this.DbConnector.ExecuteDataTable());

            BTN61_DETAIL_DEL.Visible = true;
            BTN61_INP_MONE.Visible = true;
        }
        #endregion

        #region Description : 이자배당소득 디테일 조회(저장,삭제 이벤트 후)
        private void FPS91_TY_S_AC_49JFJ981_CellDoubleClick(string WINCOME, string WICCODE)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_AC_49JGP986",
                WINCOME,
                WICCODE,
                TYUserInfo.SecureKey,
                "Y",
                ""
                );

            this.FPS91_TY_S_AC_49JFM984.SetValue(this.DbConnector.ExecuteDataTable());

            BTN61_DETAIL_DEL.Visible = true;
            BTN61_INP_MONE.Visible = true;
        }
        #endregion

        #region Description : 이자배당소득 마스타 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {   
                this.DbConnector.Attach("TY_P_AC_49NBV042", ds.Tables[0].Rows[i]["WINCOME"],
                                                            ds.Tables[0].Rows[i]["WICCODE"],
                                                            ds.Tables[0].Rows[i]["WSECOUR"],
                                                            ds.Tables[0].Rows[i]["WTRUSTYN"],
                                                            ds.Tables[0].Rows[i]["WFINANC"],
                                                            ds.Tables[0].Rows[i]["WSPECTAX"],
                                                            ds.Tables[0].Rows[i]["WREALNAM"],
                                                            ds.Tables[0].Rows[i]["WPMPOTAX"],
                                                            ds.Tables[0].Rows[i]["WINTRATE"]); //저장
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49NBW043", ds.Tables[1].Rows[i]["WTRUSTYN"],
                                                            ds.Tables[1].Rows[i]["WFINANC"],
                                                            ds.Tables[1].Rows[i]["WSPECTAX"],
                                                            ds.Tables[1].Rows[i]["WREALNAM"],
                                                            ds.Tables[1].Rows[i]["WPMPOTAX"],
                                                            ds.Tables[1].Rows[i]["WINTRATE"],
                                                            ds.Tables[1].Rows[i]["WINCOME"],
                                                            ds.Tables[1].Rows[i]["WICCODE"],
                                                            ds.Tables[1].Rows[i]["WSECOUR"]); //수정
            }
            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 이자배당소득 디테일 저장
        private void BTN61_INP_MONE_Click(object sender, EventArgs e)
        {
            string sWSINCOME = string.Empty;
            string sWSICCODE = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49PEJ070", ds.Tables[0].Rows[i]["WSINCOME"],
                                                            ds.Tables[0].Rows[i]["WSICCODE"],
                                                            ds.Tables[0].Rows[i]["WSRESIDE"],
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["WSCMNAME"],
                                                            ds.Tables[0].Rows[i]["WSADDRES"],
                                                            ds.Tables[0].Rows[i]["WSBANKCD"],
                                                            ds.Tables[0].Rows[i]["WSACCNUM"]); //저장
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49PEK071", ds.Tables[1].Rows[i]["WSCMNAME"],
                                                            ds.Tables[1].Rows[i]["WSADDRES"],
                                                            ds.Tables[1].Rows[i]["WSBANKCD"],
                                                            ds.Tables[1].Rows[i]["WSACCNUM"],
                                                            ds.Tables[1].Rows[i]["WSINCOME"],
                                                            ds.Tables[1].Rows[i]["WSICCODE"],
                                                            TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[1].Rows[i]["WSRESIDE"]); //수정
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                sWSINCOME = ds.Tables[0].Rows[0]["WSINCOME"].ToString();
                sWSICCODE = ds.Tables[0].Rows[0]["WSICCODE"].ToString();
            }
            else
            {
                sWSINCOME = ds.Tables[1].Rows[0]["WSINCOME"].ToString();
                sWSICCODE = ds.Tables[1].Rows[0]["WSICCODE"].ToString();
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.FPS91_TY_S_AC_49JFJ981_CellDoubleClick(sWSINCOME, sWSICCODE);
        }
        #endregion

        #region Description : 이자배당소득 마스타 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49NGC048", dt.Rows[i]["WINCOME"],
                                                            dt.Rows[i]["WICCODE"],
                                                            dt.Rows[i]["WSECOUR"]);
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 이자배당소득 디테일 삭제
        private void BTN61_DETAIL_DEL_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49Q9J081", dt.Rows[i]["WSINCOME"],
                                                            dt.Rows[i]["WSICCODE"],
                                                            TYUserInfo.SecureKey, "Y",
                                                            dt.Rows[i]["WSRESIDE"]);
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_49JFJ981_CellDoubleClick(dt.Rows[0]["WSINCOME"].ToString(), dt.Rows[0]["WSICCODE"].ToString());
            }
        }
        #endregion

        #region Description : 마스타 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = "";

            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_49JFJ981.GetDataSourceInclude(TSpread.TActionType.New, "WINCOME", "WICCODE", "WSECOUR", "WTRUSTYN", "WFINANC", "WSPECTAX", "WREALNAM", "WPMPOTAX", "WINTRATE"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_49JFJ981.GetDataSourceInclude(TSpread.TActionType.Update, "WINCOME", "WICCODE", "WSECOUR", "WTRUSTYN", "WFINANC", "WSPECTAX", "WREALNAM", "WPMPOTAX", "WINTRATE"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_49NC8044",
                                       ds.Tables[0].Rows[i]["WINCOME"].ToString(),
                                       ds.Tables[0].Rows[i]["WICCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["WSECOUR"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
                if (Convert.ToInt32(ds.Tables[0].Rows[i]["WICCODE"]) >= 11 && Convert.ToInt32(ds.Tables[0].Rows[i]["WICCODE"]) <= 19 && ds.Tables[0].Rows[i]["WINTRATE"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_49PH4076");
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 디테일 저장 ProcessCheck 이벤트
        private void BTN61_INP_MONE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = "";

            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_49JFM984.GetDataSourceInclude(TSpread.TActionType.New, "WSINCOME", "WSICCODE", "WSRESIDE", "WSCMNAME", "WSADDRES", "WSBANKCD", "WSACCNUM"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_49JFM984.GetDataSourceInclude(TSpread.TActionType.Update, "WSINCOME", "WSICCODE", "WSRESIDE", "WSCMNAME", "WSADDRES", "WSBANKCD", "WSACCNUM"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_49ND1045",
                                       ds.Tables[0].Rows[i]["WSINCOME"].ToString(),
                                       ds.Tables[0].Rows[i]["WSICCODE"].ToString(),
                                       TYUserInfo.SecureKey, "Y",
                                       ds.Tables[0].Rows[i]["WSRESIDE"].ToString()
                                       );
                
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 마스타 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_49JFJ981.GetDataSourceInclude(TSpread.TActionType.Remove, "WINCOME", "WICCODE", "WSECOUR");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_49ND1045",
                                       dt.Rows[i]["WINCOME"].ToString(),
                                       dt.Rows[i]["WICCODE"].ToString(),
                                       TYUserInfo.SecureKey, "Y",
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_49PHQ078");
                    e.Successed = false;
                    return;
                }
            }

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 디테일 삭제 ProcessCheck 이벤트
        private void BTN61_DETAIL_DEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_49JFM984.GetDataSourceInclude(TSpread.TActionType.Remove, "WSINCOME", "WSICCODE", "WSRESIDE");

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach(
            //                           "TY_P_AC_49ND1045",
            //                           dt.Rows[i]["WSINCOME"].ToString(),
            //                           dt.Rows[i]["WICCODE"].ToString(),
            //                           ""
            //                           );

            //    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            //    {
            //        this.ShowMessage("TY_M_AC_49PHQ078");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 마스타 행추가 이벤트
        private void FPS91_TY_S_AC_49JFM984_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_49JFM984.SetValue(e.RowIndex, "WSINCOME", this.FPS91_TY_S_AC_49JFJ981.GetValue("WINCOME").ToString());
            this.FPS91_TY_S_AC_49JFM984.SetValue(e.RowIndex, "WSINCOMENM", this.FPS91_TY_S_AC_49JFJ981.GetValue("WINCOMENM").ToString());
            this.FPS91_TY_S_AC_49JFM984.SetValue(e.RowIndex, "WSICCODE", this.FPS91_TY_S_AC_49JFJ981.GetValue("WICCODE").ToString());
            this.FPS91_TY_S_AC_49JFM984.SetValue(e.RowIndex, "WSICCODENM", this.FPS91_TY_S_AC_49JFJ981.GetValue("WICCODENM").ToString());
        }
        #endregion
    }
}
