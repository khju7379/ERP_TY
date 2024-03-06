using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반입보고서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.27 11:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_72RDC787 : 반입보고서 조회
    ///  TY_P_UT_72RDE788 : 내국화물 반입보고서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_72RDH792 : 반입보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  GOKCR : 생성구분
    ///  EDIDATE : 반입일자
    ///  EDIJUKHA : 적하목록
    /// </summary>
    public partial class TYEDFB004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDFB004S()
        {
            InitializeComponent();
        }

        private void TYEDFB004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);            

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-26"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-25"));


            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_75FFM490.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75FFM489", DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString(), TXT01_EDIJUKHA.GetValue().ToString());
            this.FPS91_TY_S_UT_75FFM490.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_75FFM490.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_75FFM490.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_75FFM490.GetValue(i, "EDIRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_75FFM490_Sheet1.Rows[i].ForeColor = Color.Blue; 
                    }
                    else if (this.FPS91_TY_S_UT_75FFM490.GetValue(i, "EDIRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_75FFM490_Sheet1.Rows[i].ForeColor = Color.Red; 
                    }
                }
            }

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_75FG4492", dt.Rows[i]["EDIGJ"].ToString(),
                                                                    dt.Rows[i]["EDICHULIL"].ToString(),
                                                                    dt.Rows[i]["EDIIPHANG"].ToString(),
                                                                    dt.Rows[i]["EDIBONSUN"].ToString(),
                                                                    dt.Rows[i]["EDIHWAMUL"].ToString(),
                                                                    dt.Rows[i]["EDIHWAJU"].ToString(),
                                                                    dt.Rows[i]["EDIBLNO"].ToString(),
                                                                    dt.Rows[i]["EDIJUKHA"].ToString(),
                                                                    dt.Rows[i]["EDIBLMSN"].ToString(),
                                                                    dt.Rows[i]["EDIBLHSN"].ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteNonQueryList();                               
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_75FFM490.GetDataSourceInclude(TSpread.TActionType.Remove, "EDIGJ", "EDICHULIL", "EDIIPHANG", "EDIBONSUN", "EDIHWAMUL", "EDIHWAJU", "EDIBLNO", "EDIJUKHA", "EDIBLMSN", "EDIBLHSN", "EDIRCVGB");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDIRCVGB"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_UT_72SA1811");
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

        #region  Description : FPS91_TY_S_UT_75FFM490_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_75FFM490_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            TYEDKB03C1 popup = new TYEDKB03C1(this.FPS91_TY_S_UT_75FFM490.GetValue("EDIGJ").ToString(), this.FPS91_TY_S_UT_75FFM490.GetValue("EDIDATE").ToString(),this.FPS91_TY_S_UT_75FFM490.GetValue("EDISINNO").ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }

        }
        #endregion


    }
}
