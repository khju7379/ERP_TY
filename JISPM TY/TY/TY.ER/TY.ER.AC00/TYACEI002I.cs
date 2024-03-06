using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음 보관관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.21 17:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25F8N480 : 받을어음 내역 등록
    ///  TY_P_AC_25L45582 : 받을어음 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28L7C480 : 보관어음 등록자료 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  E7CDCM : 관리부서
    ///  E7CDGL : 금융기관
    ///  E7CDSB : 관리사번
    ///  E7DTBG : 상태변경일
    /// </summary>
    public partial class TYACEI002I : TYBase
    {
        private DataSet fsds;

        #region Description : 폼 로드 이벤트
        public TYACEI002I(DataSet ds )
        {
            InitializeComponent();

            fsds = ds;           
        }

        private void TYACEI002I_Load(object sender, System.EventArgs e)
        {           

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);                       

            this.FPS91_TY_S_AC_28L7C480.SetValue(UP_Set_RowAddTotal(fsds));


            if (this.FPS91_TY_S_AC_28L7C480.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_28L7C480, "E6NONR", "합 계", SumRowType.SubTotal);

                for (int i = 0; i < this.FPS91_TY_S_AC_28L7C480.ActiveSheet.RowCount-1; i++)
                {
                    this.FPS91_TY_S_AC_28L7C480.ActiveSheet.Cells[i, 4].BackColor = Color.FromArgb(218, 239, 244);
                }
            }


            this.DTP01_E7DTBG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_E7CDCM.DummyValue = this.DTP01_E7DTBG.GetString(); 

            this.SetStartingFocus(this.CBH01_E7CDGL);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int iRowCnt = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //합계 라인은 빼고 한다.
                if (ds.Tables[0].Rows[i]["E6CDCL"].ToString().Trim() != "")
                {
                    this.DbConnector.Attach("TY_P_AC_25F8N480", ds.Tables[0].Rows[i]["E6NONR"].ToString(),
                                                                "15",
                                                                ds.Tables[0].Rows[i]["E6BODATE"].ToString(),
                                                                this.CBH01_E7CDGL.GetValue(),
                                                                this.CBH01_E7CDCM.GetValue(),
                                                                this.CBH01_E7CDSB.GetValue(),
                                                                ds.Tables[0].Rows[i]["E6CDCL"].ToString(),
                                                                ds.Tables[0].Rows[i]["E7HIDBG"].ToString(),
                                                                ds.Tables[0].Rows[i]["E7HDTBG"].ToString(),
                                                                ds.Tables[0].Rows[i]["E7HCDGL"].ToString(), "", ""
                                                                ); // 받을어음 내역 등록
                    this.DbConnector.Attach("TY_P_AC_25L45582", "15", ds.Tables[0].Rows[i]["E6BODATE"].ToString(), this.CBH01_E7CDGL.GetValue(), ds.Tables[0].Rows[i]["E6NONR"].ToString()); // 받을어음 수정

                    iRowCnt += 1;
                }

                
            }
            if (iRowCnt > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();            
        }
        #endregion

        #region Description : DTP01_E7DTBG_ValueChanged 이벤트
        private void DTP01_E7DTBG_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.FPS91_TY_S_AC_28L7C480.ActiveSheet.RowCount-1; i++)
            {
                this.FPS91_TY_S_AC_28L7C480.ActiveSheet.Cells[i, 4].Value = this.DTP01_E7DTBG.GetValue().ToString();
            }

            this.CBH01_E7CDCM.DummyValue = this.DTP01_E7DTBG.GetString(); 
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_28L7C480.GetDataSourceInclude(TSpread.TActionType.Select, "E6NONR", "E6DTCO", "E6IDBG", "E6IDBGNM", "E6BODATE", "E6CDCL", "E6CDCLNM", "E6AMNR", "E6DTED", "E7HIDBG", "E7HDTBG", "E7HCDGL"));

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

        #region Description : 합계, 매수 넣기
        private DataTable UP_Set_RowAddTotal(DataSet ds)
        {
            string sFilter = "";

            double dE6AMNR = 0;

            Int32  iRowCnt = 0;

            DataRow row;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0].Clone();

            int nNum = ds.Tables[0].Rows.Count;

            if (nNum > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Select("E6NONR <> ''", "E6NONR ASC"))
                    table.Rows.Add(dr.ItemArray);

                sFilter = "";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dE6AMNR += Convert.ToDouble(ds.Tables[0].Rows[i]["E6AMNR"].ToString());
                    iRowCnt += 1;
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);

                //"E6NONR", "E6DTCO", "E6IDBG", "E6IDBGNM", "E6BODATE", "E6CDCL", "E6CDCLNM", "E6AMNR", "E6DTED", "E7HIDBG", "E7HDTBG", "E7HCDGL"));           

                table.Rows[nNum]["E6NONR"] = "합 계";
                table.Rows[nNum]["E6DTCO"] = "";
                table.Rows[nNum]["E6IDBG"] = "";
                table.Rows[nNum]["E6IDBGNM"] = "";
                table.Rows[nNum]["E6BODATE"] = "";
                table.Rows[nNum]["E6CDCL"] = "";
                table.Rows[nNum]["E6CDCLNM"] = iRowCnt.ToString()+" 매";
                table.Rows[nNum]["E6AMNR"] = dE6AMNR;
                table.Rows[nNum]["E6DTED"] = "";
                table.Rows[nNum]["E7HIDBG"] = "";
                table.Rows[nNum]["E7HDTBG"] = "";
                table.Rows[nNum]["E7HCDGL"] = "";                

            }

            return table;
        }
        #endregion

    }
}
