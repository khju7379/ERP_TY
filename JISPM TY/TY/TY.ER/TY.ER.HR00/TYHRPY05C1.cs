using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인급여예외내역관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.16 11:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CCDU797 : 개인급여예외내역관리 등록
    ///  TY_P_HR_4CCDU798 : 개인급여예외내역관리 수정
    ///  TY_P_HR_4CCDW799 : 개인급여예외내역관리 삭제
    ///  TY_P_HR_4CGB9820 : 개인급여예외내역관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CGBA831 : 개인급여예외지급내역관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  PXSEXCODE : 예외코드
    ///  PXSGUBN : 급여구분
    ///  PXSPAYCODE : 급여코드
    ///  PXSSABUN : 사번
    /// </summary>
    public partial class TYHRPY05C1 : TYBase
    {
        private string fsPXSSABUN = string.Empty;
        private string fsPXSGUBN = string.Empty;
        private string fsPXSEXCODE = string.Empty;
        private string fsPXSDATE = string.Empty;
        private string fsPXEDATE = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRPY05C1(string sPXSSABUN, string sPXSGUBN, string sPXSEXCODE, string sPXSDATE, string sPXEDATE)
        {
            InitializeComponent();

            fsPXSSABUN = sPXSSABUN;
            fsPXSGUBN = sPXSGUBN;
            fsPXSEXCODE = sPXSEXCODE;
            fsPXSDATE = sPXSDATE;
            fsPXEDATE = sPXEDATE;

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CGBA831, "PXSPAYCODE", "PXSPAYCODENM", "PXSPAYCODE");
        }

        private void TYHRPY05C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSEXCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CGBA831, "PXSPAYCODE");

            this.CBH01_PXSSABUN.SetValue(fsPXSSABUN);
            this.CBH01_PXSGUBN.SetValue(fsPXSGUBN);
            this.CBH01_PXSEXCODE.SetValue(fsPXSEXCODE);

            UP_Run();
        }
        #endregion

        #region  Description : 확인 이벤트
        private void UP_Run()
        {
            this.FPS91_TY_S_HR_4CGBA831.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CGB9820", this.fsPXSSABUN, this.fsPXSGUBN, this.fsPXSEXCODE, this.fsPXSDATE);
            this.FPS91_TY_S_HR_4CGBA831.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_4CGBA831_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CGBA831.SetValue(e.RowIndex, "PXSSABUN", this.fsPXSSABUN);
            this.FPS91_TY_S_HR_4CGBA831.SetValue(e.RowIndex, "PXSGUBN", this.fsPXSGUBN);
            this.FPS91_TY_S_HR_4CGBA831.SetValue(e.RowIndex, "PXSEXCODE", this.fsPXSEXCODE);
            this.FPS91_TY_S_HR_4CGBA831.SetValue(e.RowIndex, "PXSDATE", this.fsPXSDATE);
            this.FPS91_TY_S_HR_4CGBA831.SetValue(e.RowIndex, "PXSEDATE", this.fsPXEDATE);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "PXSHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PXSHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDU797", ds.Tables[0].Rows[i]["PXSSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSEXCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSPAYCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSSTAMOUNT"].ToString(),                                                                
                                                                ds.Tables[0].Rows[i]["PXSEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["PXSHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDU798", ds.Tables[1].Rows[i]["PXSSTAMOUNT"].ToString(),                                                                
                                                                ds.Tables[1].Rows[i]["PXSEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["PXSHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["PXSSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["PXSGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["PXSEXCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["PXSDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["PXSPAYCODE"].ToString()                                                                
                                                                );
                }
            }                           
           
            this.DbConnector.ExecuteTranQueryList();

            UP_Run();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();
            //개인급여예외지급관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBA831.GetDataSourceInclude(TSpread.TActionType.New, "PXSSABUN", "PXSGUBN", "PXSEXCODE", "PXSPAYCODE", "PXSSTAMOUNT", "PXSDATE", "PXSEDATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBA831.GetDataSourceInclude(TSpread.TActionType.Update, "PXSSABUN", "PXSGUBN", "PXSEXCODE", "PXSPAYCODE", "PXSSTAMOUNT", "PXSDATE", "PXSEDATE"));
          
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CGB9820", ds.Tables[0].Rows[i]["PXSSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSEXCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSDATE"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["PXSPAYCODE"].ToString() == ds.Tables[0].Rows[i]["PXSPAYCODE"].ToString())
                            {
                                this.ShowMessage("TY_M_HR_4CBDI704");
                                e.Successed = false;
                                return;
                            }
                        }                        
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CGGY847", ds.Tables[0].Rows[i]["PXSSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSEXCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PXSDATE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt <= 0)
                    {
                        this.ShowCustomMessage("개인급여예외관리에 자료가 존재하지 않습니다. 시작일자를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CCDW799", dt);  //삭제
            this.DbConnector.ExecuteTranQueryList();

            this.UP_Run();

            this.ShowMessage("TY_M_GB_23NAD874");

            

        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataTable dt = this.FPS91_TY_S_HR_4CGBA831.GetDataSourceInclude(TSpread.TActionType.Remove, "PXSSABUN", "PXSGUBN", "PXSEXCODE", "PXSPAYCODE", "PXSDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CGGH845", dt.Rows[i]["PXSGUBN"].ToString(), dt.Rows[i]["PXSDATE"].ToString().Substring(0, 6), dt.Rows[i]["PXSSABUN"].ToString(), dt.Rows[i]["PXSPAYCODE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("급여지급내역이 존재합니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
