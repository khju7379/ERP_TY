using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 투하자금이자 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.12.14 16:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_8CH9D320 : 투하자금 이자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_8CH9E321 : 투하자금 이자관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACNC037S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC037S()
        {
            InitializeComponent();
        }

        private void TYACNC037S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            TXT01_AJNDATE.SetValue(DateTime.Now.ToString("yyyy"));
            
            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;
            int iRowCnt = 0;

            this.FPS91_TY_S_AC_8CH9E321.Initialize();
            this.DbConnector.CommandClear();

            //this.DbConnector.Attach("TY_P_AC_8CH9D320", this.DTP01_SDATE.GetString().ToString().Substring(0, 6), this.DTP01_EDATE.GetString().ToString().Substring(0, 6));
            this.DbConnector.Attach("TY_P_AC_AB5GS139", this.DTP01_SDATE.GetString().ToString().Substring(0, 6), this.DTP01_EDATE.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_8CH9E321.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_8CH9E321.CurrentRowCount > 0)
            {
                iRowCnt = 0;
                iRowIndex = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_8CH9E321.CurrentRowCount; i++)
                {
                    iRowCnt += 1;
                    if (this.FPS91_TY_S_AC_8CH9E321.GetValue(i, "AJMDPAC").ToString() == "Z")
                    {
                        this.FPS91_TY_S_AC_8CH9E321_Sheet1.AddSpanCell(iRowIndex, 0, iRowCnt, 1); //년월 rowcell 합치기
                        this.FPS91_TY_S_AC_8CH9E321_Sheet1.AddSpanCell(iRowIndex, 1, iRowCnt, 1); //전체이자 rowcell 합치기
                        iRowIndex = i+1;
                        iRowCnt = 0;
                    }
                }

                this.SetSpreadSumRow(this.FPS91_TY_S_AC_8CH9E321, "AJMDPACNM", "계", SumRowType.SubTotal);
            }
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYACNC037B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 프로젝트 이자 월별 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_AB3H4109.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB3H3108", this.TXT01_AJNDATE.GetValue().ToString().Substring(0,4) );
            this.FPS91_TY_S_AC_AB3H4109.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 프로젝트 이자 월별 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            Int16 iCnt = 0;
            string sFieldName = string.Empty;


            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    sFieldName = "AJMIJAMT" + Set_Fill2(j.ToString());

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_AB6DU141", ds.Tables[0].Rows[i]["AJMPJGB"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJMDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJMDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJMYEAR"].ToString()+ Set_Fill2(j.ToString()));
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        
                        //투자금, 이자율 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_AB49S114", ds.Tables[0].Rows[i]["AJMPJGB"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMDATE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMDPAC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMYEAR"].ToString() + Set_Fill2(j.ToString()));
                        DataTable dr = this.DbConnector.ExecuteDataTable();

                        //수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_AB6DW142", dr.Rows[0]["AJMINAMT"].ToString(),
                                                                    dr.Rows[0]["AJMIJRATE"].ToString(),
                                                                    ds.Tables[0].Rows[i][sFieldName].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["AJMPJGB"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMDATE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMDPAC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["AJMYEAR"].ToString() + Set_Fill2(j.ToString())
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    else
                    {
                        //등록
                        if (Convert.ToDouble(ds.Tables[0].Rows[i][sFieldName].ToString()) > 0)
                        {

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_AB6EN144", ds.Tables[0].Rows[i]["AJMPJGB"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMDATE"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMDPAC"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMYEAR"].ToString() + Set_Fill2(j.ToString()));
                            DataTable dr = this.DbConnector.ExecuteDataTable();

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_AB6DY143", ds.Tables[0].Rows[i]["AJMPJGB"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMDATE"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMDPAC"].ToString(),
                                                                        ds.Tables[0].Rows[i]["AJMYEAR"].ToString() + Set_Fill2(j.ToString()),
                                                                        dr.Rows[0]["AJNINAMT"].ToString(),
                                                                        dr.Rows[0]["AJRRATE"].ToString(),
                                                                        ds.Tables[0].Rows[i][sFieldName].ToString(),
                                                                        TYUserInfo.EmpNo
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }
                    }

                }
            }

            BTN62_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_AB3H4109.GetDataSourceInclude(TSpread.TActionType.Update, "AJMPJGB", "AJMDATE", "AJMDPAC", "AJMYEAR", "AJMIJAMT01", "AJMIJAMT02", "AJMIJAMT03", "AJMIJAMT04", "AJMIJAMT05", "AJMIJAMT06", "AJMIJAMT07", "AJMIJAMT08", "AJMIJAMT09", "AJMIJAMT10", "AJMIJAMT11", "AJMIJAMT12"));

            if (ds.Tables[0].Rows.Count == 0)
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
    }
}
