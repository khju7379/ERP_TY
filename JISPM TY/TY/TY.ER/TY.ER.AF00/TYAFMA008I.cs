using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 차입금 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.10.07 17:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_3A75B977 : EIS 계열사 차입금 관리 등록
    ///  TY_P_AC_3A75B978 : EIS 계열사 차입금 관리 삭제
    ///  TY_P_AC_3A75B979 : EIS 계열사 차입금 관리 수정
    ///  TY_P_AC_3A75C980 : EIS 계열사 차입금 관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3A75E983 : EIS 계열사 차입금 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ESBMCUST : 계열사구분
    ///  ESLOCDAC : 계정과목
    ///  ESLOYYMM : 년월
    /// </summary>
    public partial class TYAFMA008I : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYAFMA008I()
        {
            InitializeComponent(); 

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3A75E983, "ESLOCDAC", "A1NMAC", "ESLOCDAC"); // 스프레드 CODE HELP (계정과목)
        }

        #region Description : Page_load
        private void TYAFMA008I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            //키필드는 신규일때만 수정된다.
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I31090, "ELOYYMM");

            //this.TXT01_ESLOYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            //this.TXT01_ESLOYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMM"));

            UP_start_dsp_month();

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESBMCUST.SetValue(fsCompanyCode);
                this.CBH01_ESBMCUST.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ESLOYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESBMCUST.CodeText);
            }

            if (fsCompanyCode != "TL")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
            this.BTN61_INQ_Click(null, null);

           

        }
        
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_3A75C980", this.CBH01_ESBMCUST.GetValue(), this.TXT01_ESLOYYMM.GetValue().ToString().Replace("-", ""), this.CBH01_ESLOCDAC.GetValue());
            //this.FPS91_TY_S_AC_3A75E983.SetValue(this.DbConnector.ExecuteDataTable());

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3A75E983.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기 
                for (int i = 0; i < this.FPS91_TY_S_AC_3A75E983.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3A75E983.GetValue(i, "A1NMAC").ToString() == "합         계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_3A75E983.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244); // Color.FromArgb(218, 239, 244);

                        this.FPS91_TY_S_AC_3A75E983.ActiveSheet.Rows[i].Locked = true;
                    }
                }

            }
            else
            {
                //this.ShowMessage("TY_M_AC_2422N250");
                return;
            };
        }
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A75B978", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A75B977", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_3A75B979", ds.Tables[1]); //수정
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); 
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_3A75E983.GetDataSourceInclude(TSpread.TActionType.New, "ESLOCUST", "ESLOYYMM", "ESLOCDAC", "ESLOVEVL", "ESLOJM00", "ESLOJMBYL", "ESLOREPAY", "ESLOCAPIT", "ESLOMMJMT", "ESLOMMBYL"));
            ds.Tables.Add(this.FPS91_TY_S_AC_3A75E983.GetDataSourceInclude(TSpread.TActionType.Update, "ESLOCUST", "ESLOYYMM", "ESLOCDAC", "ESLOVEVL", "ESLOJM00", "ESLOJMBYL", "ESLOREPAY", "ESLOCAPIT", "ESLOMMJMT", "ESLOMMBYL"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A75T984", ds.Tables[0].Rows[i]["ESLOCUST"].ToString(), ds.Tables[0].Rows[i]["ESLOYYMM"].ToString(), ds.Tables[0].Rows[i]["ESLOCDAC"].ToString());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                // 마감 완료 CHECK 
                this.DbConnector.CommandClear(); // TY_P_AC_27H64059
                this.DbConnector.Attach("TY_P_AC_3C92V659", ds.Tables[0].Rows[i]["ESLOYYMM"].ToString().Substring(0, 4), ds.Tables[0].Rows[i]["ESLOYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            //수정시 CHECK
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear(); // TY_P_AC_27H64059
                this.DbConnector.Attach("TY_P_AC_3C92V659", ds.Tables[1].Rows[i]["ESLOYYMM"].ToString().Substring(0, 4), ds.Tables[1].Rows[i]["ESLOYYMM"].ToString().Substring(4, 2));
                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt2.Rows[0]["ECSBBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_3A75E983.GetDataSourceInclude(TSpread.TActionType.Remove, "ESLOYYMM");


            //삭제시 CHECK
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear(); // TY_P_AC_27H64059
                this.DbConnector.Attach("TY_P_AC_3C92V659", dt.Rows[i]["ESLOYYMM"].ToString().Substring(0, 4), dt.Rows[i]["ESLOYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECSBBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
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

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {

            double dESLOJM00 = 0;
            double dESLOREPAY = 0;
            double dESLOCAPIT = 0;
            double dESLOMMJMT = 0;

            double dESLOJMBYL = 0;
            double dESLOMMBYL = 0;


            DataTable Condt = new DataTable();

            DataRow row;

            // ESLOCUST   계열사
            // ESLOYYMM   년월
            // ESLOCDAC   계정과목
            // A1NMAC     계정과목명
            // ESLOVEVL   LEVEL
            // ESLOJM00   전월잔액
            // ESLOJMBYL  전월비율
            // ESLOREPAY  당월상환액
            // ESLOCAPIT  당월차입액
            // ESLOMMJMT  당월잔액
            // ESLOMMBYL  당월비율

            Condt.Columns.Add("ESLOCUST", typeof(System.String));    // 계열사
            Condt.Columns.Add("ESLOYYMM", typeof(System.String));    // 년월
            Condt.Columns.Add("ESLOCDAC", typeof(System.String));    // 계정과목
            Condt.Columns.Add("A1NMAC", typeof(System.String));      // 계정과목명
            Condt.Columns.Add("ESLOVEVL", typeof(System.String));    // LEVEL
            Condt.Columns.Add("ESLOJM00", typeof(System.String));    // 전월잔액
            Condt.Columns.Add("ESLOJMBYL", typeof(System.String));   // 전월비율
            Condt.Columns.Add("ESLOREPAY", typeof(System.String));   // 당월상환액
            Condt.Columns.Add("ESLOCAPIT", typeof(System.String));   // 당월차입액
            Condt.Columns.Add("ESLOMMJMT", typeof(System.String));   // 당월잔액
            Condt.Columns.Add("ESLOMMBYL", typeof(System.String));   // 당월비율

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                row = Condt.NewRow();

                if (dt.Rows[i]["ESLOVEVL"].ToString() == "1")
                {
                    dESLOJM00 = dESLOJM00   + double.Parse(dt.Rows[i]["ESLOJM00"].ToString());    // 전월잔액
                    dESLOREPAY = dESLOREPAY + double.Parse(dt.Rows[i]["ESLOREPAY"].ToString());   // 당월상환액
                    dESLOCAPIT = dESLOCAPIT + double.Parse(dt.Rows[i]["ESLOCAPIT"].ToString());   // 당월차입액
                    dESLOMMJMT = dESLOMMJMT + double.Parse(dt.Rows[i]["ESLOMMJMT"].ToString());   // 당월잔액

                    dESLOJMBYL = dESLOJMBYL + double.Parse(dt.Rows[i]["ESLOJMBYL"].ToString());   // 전월비율
                    dESLOMMBYL = dESLOMMBYL + double.Parse(dt.Rows[i]["ESLOMMBYL"].ToString());   // 당월비율


                }

                row["ESLOCUST"]  = dt.Rows[i]["ESLOCUST"].ToString();// 계열사
                row["ESLOYYMM"]  = dt.Rows[i]["ESLOYYMM"].ToString();// 년월
                row["ESLOCDAC"]  = dt.Rows[i]["ESLOCDAC"].ToString();// 계정과목
                row["A1NMAC"]    = dt.Rows[i]["A1NMAC"].ToString();// 계정과목명
                row["ESLOVEVL"]  = dt.Rows[i]["ESLOVEVL"].ToString();// LEVEL
                row["ESLOJM00"]  = dt.Rows[i]["ESLOJM00"].ToString();// 전월잔액
                row["ESLOJMBYL"] = dt.Rows[i]["ESLOJMBYL"].ToString();// 전월비율
                row["ESLOREPAY"] = dt.Rows[i]["ESLOREPAY"].ToString();// 당월상환액
                row["ESLOCAPIT"] = dt.Rows[i]["ESLOCAPIT"].ToString();// 당월차입액
                row["ESLOMMJMT"] = dt.Rows[i]["ESLOMMJMT"].ToString();// 당월잔액
                row["ESLOMMBYL"] = dt.Rows[i]["ESLOMMBYL"].ToString();// 당월비율

                Condt.Rows.Add(row);
            }

            // 합계 
            row = Condt.NewRow();

            row["ESLOCUST"] = "";
            row["ESLOYYMM"] = "";
            row["ESLOCDAC"] = "";
            row["A1NMAC"] = "합         계";
            row["ESLOVEVL"] = "";
            row["ESLOJM00"] = string.Format("{0:#,##0}", dESLOJM00);
            row["ESLOJMBYL"] = string.Format("{0:#,##0}", dESLOJMBYL);
            row["ESLOREPAY"] = string.Format("{0:#,##0}", dESLOREPAY);
            row["ESLOCAPIT"] = string.Format("{0:#,##0}", dESLOCAPIT);
            row["ESLOMMJMT"] = string.Format("{0:#,##0}", dESLOMMJMT);
            row["ESLOMMBYL"] = string.Format("{0:#,##0}", dESLOMMBYL);

            Condt.Rows.Add(row);

            return Condt;
        }
        #endregion

        #region Description : 스프레드 계정과목 조회
        private void FPS91_TY_S_AC_3A75E983_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 계정과명을 가져오기 위해서 스프레드의 INDEX = 'SC' 파라미터 를 넣음.
            string scode = "SC";

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_3A75E983, "ESLOCDAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = scode;
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.TXT01_ESLOYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.TXT01_ESLOYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}
