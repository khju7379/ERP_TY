using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음결재관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.14 11:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29E5M060 : 받을어음결재관리 결재예정 조회
    ///  TY_P_AC_29E68078 : 받을어음결재관리 결제 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29E5O064 : 받을어음결재관리 결재예정 조회
    ///  TY_S_AC_29E69080 : 받을어음결재관리 결재 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  E6CDGL : 금융기관
    ///  E6DTAP : 결재일자
    ///  E6DTED : 만기일자
    /// </summary>
    public partial class TYACEI005S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI005S()
        {
            InitializeComponent();
        }

        private void TYACEI005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_NEW.ProcessCheck += new TButton.CheckHandler(BTN61_NEW_ProcessCheck);

            this.FPS91_TY_S_AC_29E5O064.Visible = true;
            this.FPS91_TY_S_AC_29E69080.Visible = false;

            this.DTP01_E6DTED.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP02_E6DTED.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_E6DTAP.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP02_E6DTAP.SetValue(DateTime.Now.ToString("yyyy-MM-dd")); 


        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.CBO01_GOKCR.GetValue().ToString() == "1")  //결재예정
            {
                this.FPS91_TY_S_AC_29E5O064.Visible = true;
                this.FPS91_TY_S_AC_29E69080.Visible = false;

                this.FPS91_TY_S_AC_29E5O064.Initialize();
 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29E5M060", this.DTP01_E6DTED.GetValue(), this.DTP02_E6DTED.GetValue(),this.DTP01_E6DTAP.GetValue(), this.DTP02_E6DTAP.GetValue(),
                                                            this.CBH01_E6CDGL.GetValue(), "1" );
                this.FPS91_TY_S_AC_29E5O064.SetValue(this.DbConnector.ExecuteDataTable());                

            }
            else  //결재
            {
                this.FPS91_TY_S_AC_29E5O064.Visible = false;
                this.FPS91_TY_S_AC_29E69080.Visible = true;

                this.FPS91_TY_S_AC_29E69080.Initialize(); 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29E68078", this.DTP01_E6DTED.GetValue(), this.DTP02_E6DTED.GetValue(), this.DTP01_E6DTAP.GetValue(), this.DTP02_E6DTAP.GetValue(),
                                                            this.CBH01_E6CDGL.GetValue(), "2");
                this.FPS91_TY_S_AC_29E69080.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_29E69080.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_29E69080, "E6NONR", "전표합계", SumRowType.Total);
                }

            }
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if ((new TYACEI005I(ds,"", "", "A")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 신규 ProcessCheck 이벤트
        private void BTN61_NEW_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_29E5O064.GetDataSourceInclude(TSpread.TActionType.Select, "E6NONR", "E6DTED", "E6CDGL", "E6CDGLNM", "E6AMNR", "E6CDCL", "E6CDCLNM", "E6IDBG", "E6IDBGNM","E6CDCM","E6DTAP","E6DTBG"));

            if (ds.Tables[0].Rows.Count > 49)
            {
                this.ShowMessage("TY_M_AC_29I91156");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                string sE6DTED = "";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (sE6DTED != "" && sE6DTED != ds.Tables[0].Rows[i]["E6DTAP"].ToString())
                    {
                        this.ShowMessage("TY_M_AC_31H7A797");
                        e.Successed = false;
                        return;
                    }
                    sE6DTED = ds.Tables[0].Rows[i]["E6DTAP"].ToString();
                }   
            }
            

            if (!this.ShowMessage("TY_M_AC_29H13142"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29E69080_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_29E69080_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataSet ds = new DataSet();

            if ((new TYACEI005I(ds, this.FPS91_TY_S_AC_29E69080.GetValue("E6DTED").ToString(), this.FPS91_TY_S_AC_29E69080.GetValue("E7HDAC").ToString(), "D")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
