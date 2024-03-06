using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 4대 보험 요율관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.19 16:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CJGE905 : 4대 보험 요율관리 조회
    ///  TY_P_HR_4CJGY908 : 4대 보험 요율관리 등록
    ///  TY_P_HR_4CJGZ909 : 4대 보험 요율관리 수정
    ///  TY_P_HR_4CJH1910 : 4대 보험 요율관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CJGV907 : 4대 보험 요율관리
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
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PIPAYCODE : 급여코드
    /// </summary>
    public partial class TYHRPY007P : TYBase
    {
        #region Description : 페이지 로드
        public TYHRPY007P()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CJGV907, "TRPAYCODE", "TRPAYCODENM", "TRPAYCODE");
        }

        private void TYHRPY007P_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CJGV907, "TRPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CJGV907, "TRRSDATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            BTN61_INQ_Click(null,null);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4CJGV907.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJGE905", CBH01_TRPAYCODE.GetValue().ToString()
                                                      , "");
            this.FPS91_TY_S_HR_4CJGV907.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            dt.Columns.Remove("TRREDATE");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJH1910", dt);  //삭제
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;
            string sEDDATE = string.Empty;

            DataTable dt = this.FPS91_TY_S_HR_4CJGV907.GetDataSourceInclude(TSpread.TActionType.Remove, "TRPAYCODE", "TRRSDATE", "TRREDATE");

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
                    if (dt.Rows[i]["TRREDATE"].ToString() == "19000101")
                    {
                        sEDDATE = "99991231";
                    }
                    else{
                        sEDDATE = dt.Rows[i]["TRREDATE"].ToString();
                    }
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CJHJ913", dt.Rows[i]["TRPAYCODE"].ToString(), dt.Rows[i]["TRRSDATE"].ToString(), sEDDATE);
                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowCustomMessage("급여결과내역이 존재합니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            string sDATE = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CJGY908", ds.Tables[0].Rows[i]["TRPAYCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["TRRSDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["TRRATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["TRTOPAMOUNT"].ToString(),
                                                                UP_ChangeDate(ds.Tables[0].Rows[i]["TRREDATE"].ToString()),
                                                                ds.Tables[0].Rows[i]["TRBIGO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );  //등록
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CJGZ909", ds.Tables[1].Rows[i]["TRRATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["TRTOPAMOUNT"].ToString(),
                                                                UP_ChangeDate(ds.Tables[1].Rows[i]["TRREDATE"].ToString()),
                                                                ds.Tables[1].Rows[i]["TRBIGO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["TRPAYCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["TRRSDATE"].ToString()
                                                                );   //수정
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");   
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CJGV907.GetDataSourceInclude(TSpread.TActionType.New, "TRPAYCODE", "TRRSDATE", "TRRATE", "TRTOPAMOUNT", "TRREDATE","TRBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CJGV907.GetDataSourceInclude(TSpread.TActionType.Update, "TRPAYCODE", "TRRSDATE", "TRRATE", "TRTOPAMOUNT", "TRREDATE", "TRBIGO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CJGE905", "", "");
                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {   
                    //동일자료 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CJGE905", ds.Tables[0].Rows[i]["TRPAYCODE"].ToString(), ds.Tables[0].Rows[i]["TRRSDATE"].ToString());
                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_3219C986");
                        e.Successed = false;
                        return;
                    }

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (i != j)
                        {
                            if (ds.Tables[0].Rows[i]["TRPAYCODE"].ToString() == ds.Tables[0].Rows[j]["TRPAYCODE"].ToString() && ds.Tables[0].Rows[i]["TRRSDATE"].ToString() == ds.Tables[0].Rows[j]["TRRSDATE"].ToString())
                            {
                                this.ShowMessage("TY_M_AC_3219C986");
                                e.Successed = false;
                                return;
                            }
                        }
                    }

                    //동일급여코드에 종료안된 자료 체크
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (ds.Tables[0].Rows[i]["TRPAYCODE"].ToString() == dt.Rows[k]["TRPAYCODE"].ToString() &&
                            Convert.ToInt32(ds.Tables[0].Rows[i]["TRRSDATE"].ToString()) >= Convert.ToInt32(dt.Rows[k]["TRRSDATE"].ToString())
                           )
                        {
                            if (dt.Rows[k]["TRREDATE"].ToString() == "")
                            {
                                this.ShowCustomMessage("동일급여코드에 종료되지 않은 자료가 존재합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        #region Description : 종료일자 미입력시 공백처리
        private void FPS91_TY_S_HR_4CJGV907_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (this.FPS91_TY_S_HR_4CJGV907.GetValue("TRREDATE").ToString() == "19000101")
            {
                this.FPS91_TY_S_HR_4CJGV907.SetValue("TRREDATE", "");
            }
        }
        #endregion

        #region Description : 종료일자 체크
        private string UP_ChangeDate(string sDATE)
        {
            if (sDATE == "19000101")
            {
                sDATE = "";
            }
            return sDATE;
        }
        #endregion
    }
}
