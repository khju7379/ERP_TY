using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 일일CASH 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.12.31 14:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_2CV2F429 : EIS 일일CASH 조회
    ///  TY_P_AC_2CV2G430 : EIS 일일CASH 등록
    ///  TY_P_AC_2CV2G431 : EIS 일일CASH 수정
    ///  TY_P_AC_2CV2H432 : EIS 일일CASH 삭제
    ///  TY_P_AC_2CV4B443 : EIS 일일CASH 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2CV2I434 : EIS 일일CASH 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CV43442 : 처리 할 데이터가 없습니다..
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
    ///  EICHYMD : 년월일
    /// </summary>
    public partial class TYACPC012S : TYBase
    {
        public TYACPC012S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2CV2I434, "EICHGBN", "EDDESC1", "EICHGBN"); // 스프레드 CODE HELP (계정과목)
        }

        private void TYACPC012S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2CV2I434, "EICHDAMT");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_EICHYMD.SetValue(DateTime.Now.ToString("yyyy")+"0101");
            this.BTN61_INQ_Click(null, null);
            this.TXT01_EICHYMD.Focus();
        }

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2CV2F429",  this.TXT01_EICHYMD.GetValue().ToString().Trim());
            this.FPS91_TY_S_AC_2CV2I434.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_2CV2I434.CurrentRowCount; i++)
            {
                if (this.FPS91_TY_S_AC_2CV2I434.GetValue(i, "EIPRGUBN").ToString() == "Y")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_2CV2I434.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                    // 특정 ROW 글자색깔 입히기
                    //this.FPS91_TY_S_AC_2CV2I434.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                    // 특정 ROW 색깔 입히기
                    this.FPS91_TY_S_AC_2CV2I434.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }

            UP_SumRowAdd();
        } 
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2CV2H432", dt);
            //this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_2CV2H432", dt.Rows[i]["EICHYMD"].ToString(), dt.Rows[i]["EICHGBN"].ToString(), dt.Rows[i]["EICHHWA"].ToString());
                }
            }
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // this.DbConnector.Attach("TY_P_AC_2CV2G430", ds.Tables[0]); //저장

                    this.DbConnector.Attach("TY_P_AC_2CV2G430", ds.Tables[0].Rows[i][0].ToString(),
                                                                ds.Tables[0].Rows[i][1].ToString(),
                                                                ds.Tables[0].Rows[i][2].ToString(),
                                                                ds.Tables[0].Rows[i][3].ToString(),
                                                                ds.Tables[0].Rows[i][4].ToString(),
                                                                ds.Tables[0].Rows[i][5].ToString(),
                                                                ds.Tables[0].Rows[i][6].ToString()); // 저장
                }

            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_AC_2CV2G431", ds.Tables[1]); //수정
            }

            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); 
        } 
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_SumRowAdd();

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_2CV2I434.GetDataSourceInclude(TSpread.TActionType.New, "EICHYMD", "EICHGBN", "EICHHWA", "EICHJAMT", "EICHIAMT", "EICHOAMT", "EICHDAMT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_2CV2I434.GetDataSourceInclude(TSpread.TActionType.Update, "EICHYMD", "EICHGBN", "EICHHWA", "EICHJAMT", "EICHIAMT", "EICHOAMT", "EICHDAMT"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CV4B443", ds.Tables[0].Rows[i]["EICHYMD"].ToString(), ds.Tables[0].Rows[i]["EICHGBN"].ToString(), ds.Tables[0].Rows[i]["EICHHWA"].ToString());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[0].Rows[i]["EICHYMD"].ToString().Substring(0, 4), ds.Tables[0].Rows[i]["EICHYMD"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
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
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[1].Rows[i]["EICHYMD"].ToString().Substring(0, 4), ds.Tables[1].Rows[i]["EICHYMD"].ToString().Substring(4, 2));
                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt2.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
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
            DataTable dt = this.FPS91_TY_S_AC_2CV2I434.GetDataSourceInclude(TSpread.TActionType.Remove, "EICHYMD", "EICHGBN", "EICHHWA");
            //DataTable dt = this.FPS91_TY_S_AC_2CV2I434.GetDataSourceInclude(TSpread.TActionType.Select, "EICHYMD", "EICHGBN", "EICHHWA");


            //삭제시 CHECK
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", dt.Rows[i]["EICHYMD"].ToString().Substring(0, 4), dt.Rows[i]["EICHYMD"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
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


        #region  Description : 그리드 이벤트 처리(EIS INDEX 코드 처리)
        private void FPS91_TY_S_AC_2CV2I434_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 계정과명을 가져오기 위해서 스프레드의 INDEX = 'CA' 파라미터 를 넣음.
            string scode = "CA";

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2CV2I434, "EICHGBN");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = scode;

        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            int i = 0;

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2CV2I434, "EICHYMD", "합 계", Color.Yellow);

            for (i = 0; i < this.FPS91_TY_S_AC_2CV2I434.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_AC_2CV2I434_Sheet1.SetFormula(
                    i, // row
                    7, // column
                    "R[0]C[-3] + R[0]C[-2] - R[0]C[-1]"); //
            }

            this.FPS91_TY_S_AC_2CV2I434.ActiveSheet.Rows[i - 1].Visible = false;
        }
        #endregion

        #region Description : 값 가져오기 처리
        private void BTN61_GET_Click(object sender, EventArgs e)
        {

            string sLASTDATE = string.Empty;
            string sOUTMSG_SP = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CV6A444");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count != 0)
            {
                sLASTDATE = dt.Rows[0]["LASTDATE"].ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CV6B445", sLASTDATE); // 저장
                this.DbConnector.ExecuteNonQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3122E456", sLASTDATE, sOUTMSG_SP); // SP처리
                this.DbConnector.ExecuteScalar();
                //sOUTMSG_SP = Convert.ToString(this.DbConnector.ExecuteScalar());

            }

            string sOUTMSG = "전일 " + Convert.ToString(sLASTDATE) + " 이월 되었습니다.";
            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            this.BTN61_INQ_Click(null, null);

        } 
        #endregion

        #region Description : 확정 자료 생성
        private void BTN61_SET_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPC012B(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        } 
        #endregion
    }
}
