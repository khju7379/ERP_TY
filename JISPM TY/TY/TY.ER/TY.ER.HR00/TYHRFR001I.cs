using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 재직증명서 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.25 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPGX507 : 재직증명서 조회
    ///  TY_P_HR_4BPIL522 : 재직증명서 등록
    ///  TY_P_HR_4BPIN523 : 재직증명서 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3219C986 : 동일 자료가 존재합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  CEBALYN : 발급여부
    ///  CEBIGO : 신청인
    ///  CEDATE : 발급일자
    ///  CEGUBUN : 발급구분
    ///  CEPOST : 직위
    ///  CEWORK1 : 담당업무내용1
    ///  CEWORK2 : 담당업무내용2
    ///  CEYAER : 발급년도
    ///  CEYONGDO : 용도
    ///  SEQ : 순서
    /// </summary>
    public partial class TYHRFR001I : TYBase
    {

        #region Description : 페이지 로드
        public TYHRFR001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53HCL688, "CTGUBN", "CTGUBNNM", "CTGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53HCL688, "CTSABUN", "CTSABUNNM", "CTSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_53HCL688, "CTBKCODE", "CTBKCODENM", "CTBKCODE");
        }

        private void TYHRFR001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_53HCL688, "CTGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_53HCL688, "CTSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53HCL688, "CTGUBN");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.CBH01_CTGUBN);

        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion       

        #region Description : 저장버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "CTHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "CTHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_53HCZ689", ds.Tables[0].Rows[i]["CTGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTSDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["CTSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTBKCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTBKACCOUNT"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTBIGO"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTHISAB"].ToString()
                                                                );
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_53HD2690", ds.Tables[1].Rows[i]["CTEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["CTSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTBKCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTBKACCOUNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTBIGO"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTSDATE"].ToString()
                                                                );
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_53HD9691", ds.Tables[0].Rows[i]["CTGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["CTSDATE"].ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_53HCL688.GetDataSourceInclude(TSpread.TActionType.New, "CTGUBN", "CTSDATE", "CTEDATE", "CTSABUN", "CTBKCODE", "CTBKACCOUNT", "CTBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_53HCL688.GetDataSourceInclude(TSpread.TActionType.Update, "CTGUBN", "CTSDATE", "CTEDATE", "CTSABUN", "CTBKCODE", "CTBKACCOUNT", "CTBIGO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                //동일한 이체코드, 시작일자가 있는지 체크
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53IAB696", ds.Tables[0].Rows[i]["CTGUBN"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            //이체코드, 시작일자가 같은거
                            if (dt.Rows[j]["CTSDATE"].ToString() == ds.Tables[0].Rows[i]["CTSDATE"].ToString())
                            {
                                this.ShowMessage("TY_M_GB_23S40973");
                                e.Successed = false;
                                return;
                            }
                            //이체코드에 종료일자가 없는거
                            if (dt.Rows[j]["CTEDATE"].ToString() == "")
                            {
                                this.ShowCustomMessage("동일한 이체코드에 종료일자 없는 자료가 있습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }

                            //시작일자 종료일자 사이에 있는지 체크
                            if (Convert.ToInt32(Get_Numeric(dt.Rows[j]["CTEDATE"].ToString())) >= Convert.ToInt32(ds.Tables[0].Rows[i]["CTSDATE"].ToString()))
                            {
                                this.ShowCustomMessage("동일한 이체코드에 시작일이 종료일보다 작을수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                //이체자료가 있으면 자료를 수정할수 없다
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53IAG697", ds.Tables[1].Rows[i]["CTGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTBKCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["CTBKACCOUNT"].ToString().Trim()
                                                                 );
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_HR_51CIB113");
                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53IAB696", ds.Tables[1].Rows[i]["CTGUBN"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            //시작일자 종료일자 사이에 있는지 체크
                            if (Convert.ToInt32(Get_Numeric(dt.Rows[j]["CTSDATE"].ToString())) <= Convert.ToInt32(ds.Tables[1].Rows[i]["CTEDATE"].ToString()))
                            {
                                this.ShowCustomMessage("동일한 이체코드에 종료일이 시작일보다 클수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
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

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_53HCL688.GetDataSourceInclude(TSpread.TActionType.Remove, "CTGUBN", "CTSDATE"));

            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion 

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_53HCL688.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53GKN683", this.CBH01_CTGUBN.GetValue());
            this.FPS91_TY_S_HR_53HCL688.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion






    }
}