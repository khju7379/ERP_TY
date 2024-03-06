using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 지급어음만기등록 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.24 16:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29O5G251 : 지급어음만기등록 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29O5H254 : 지급어음만기등록 조회
    ///  TY_S_AC_29O68256 : 지급어음만기등록 전표발행 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYACEI016S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI016S()
        {
            InitializeComponent();
        }

        private void TYACEI016S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_GSTDATE); 
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sJpno = "";
            int iRowCnt = 0;
            int ispoint = 0;

            this.FPS91_TY_S_AC_29O5H254.Initialize();
            this.FPS91_TY_S_AC_29O68256.Initialize(); 

            if (this.CBO01_GOKCR.GetValue().ToString() == "1")  //결재예정
            {
                this.FPS91_TY_S_AC_29O5H254.Visible = false;
                this.FPS91_TY_S_AC_29O68256.Visible = true;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29O5G251", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBO01_GOKCR.GetValue());
                this.FPS91_TY_S_AC_29O68256.SetValue(this.DbConnector.ExecuteDataTable());

            }
            else  //결재
            {
                this.FPS91_TY_S_AC_29O5H254.Visible = true;
                this.FPS91_TY_S_AC_29O68256.Visible = false;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29P94268", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBO01_GOKCR.GetValue());
                this.FPS91_TY_S_AC_29O5H254.SetValue(this.DbConnector.ExecuteDataTable());

                ispoint = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_29O5H254.CurrentRowCount; i++)
                {

                    if ( sJpno != "" && sJpno != this.FPS91_TY_S_AC_29O5H254.GetValue(i, "F3JPNO").ToString())
                    {
                        FPS91_TY_S_AC_29O5H254.ActiveSheet.AddSpanCell(ispoint, 11, iRowCnt, 1);
                        ispoint = iRowCnt;
                    }

                    iRowCnt = iRowCnt + 1;
                    sJpno = this.FPS91_TY_S_AC_29O5H254.GetValue(i, "F3JPNO").ToString();

                    if (i <= this.FPS91_TY_S_AC_29O5H254.CurrentRowCount)
                    {
                        FPS91_TY_S_AC_29O5H254.ActiveSheet.AddSpanCell(ispoint, 11, iRowCnt, 1);
                    }
                    
                }
                if (this.FPS91_TY_S_AC_29O5H254.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_29O5H254, "F3NONY", "전표합계", SumRowType.Total);
                }

            }
        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if ((new TYACEI016I(ds, "", "A")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_29O68256.GetDataSourceInclude(TSpread.TActionType.Select, "F3NONY", "F3DTIS", "F3DTED", "F3CLNY", "F3CLNYNM", "SANGTAE", "F3AMNY", "F3DPAC", "F3RPYY", "F3BKPY", "F3SSYN", "F3JPNO"));
           
            if (ds.Tables[0].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 49)
            {
                this.ShowMessage("TY_M_AC_29I91156");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_29H13142"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29O5H254_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_29O5H254_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataSet ds = new DataSet();

            if ((new TYACEI016I(ds, this.FPS91_TY_S_AC_29O5H254.GetValue("F3JPNO").ToString(), "D")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion
    }
}
