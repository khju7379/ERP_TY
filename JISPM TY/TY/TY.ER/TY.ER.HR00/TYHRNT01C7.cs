using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 월세액 세액명세 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.11.30 16:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_7BUG0144 : 연말정산 월세액 명세관리 등록
    ///  TY_P_HR_7BUG3145 : 연말정산 월세액 명세관리 수정
    ///  TY_P_HR_7BUG3146 : 연말정산 월세액 명세관리 삭제
    ///  TY_P_HR_7BUGB147 : 연말정산 월세액 명세관리 조회
    /// 
    ///  # 스프레드 정보 ####
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
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT01C7 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C7(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C7_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);            

            TXT01_SDATE.SetValue(fsWKYEAR);
            CBH01_KBSABUN.SetValue(fsWKSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
            }

            this.UP_Grid_DataBinding();
        }
        #endregion


        #region  Description : 그리드 행추가 이벤트
        private void UP_Grid_RowAddEevent()
        {
            //빈칸 Add
            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.AddRows(FPS91_TY_S_HR_7BUGB148.CurrentRowCount, 1);

            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.RowHeader.Cells[FPS91_TY_S_HR_7BUGB148.CurrentRowCount-1, 0].Text = "N";

            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.Cells[FPS91_TY_S_HR_7BUGB148.CurrentRowCount - 1, 0].Text = fsWKCOMPANY;
            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.Cells[FPS91_TY_S_HR_7BUGB148.CurrentRowCount - 1, 1].Text = fsWKYEAR;
            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.Cells[FPS91_TY_S_HR_7BUGB148.CurrentRowCount - 1, 2].Text = fsWKSABUN;
            this.FPS91_TY_S_HR_7BUGB148.ActiveSheet.Cells[FPS91_TY_S_HR_7BUGB148.CurrentRowCount - 1, 3].Text = UP_Get_SeqNumber();
                        
        }
        #endregion       

        #region  Description : 그리드 데이타 바인딩 이벤트
        private void UP_Grid_DataBinding()
        {
            this.FPS91_TY_S_HR_7BUGB148.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7BUGB147", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", fsWKCOMPANY, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue());
            this.FPS91_TY_S_HR_7BUGB148.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7BUGB148.CurrentRowCount <= 0)
            {
                UP_Grid_RowAddEevent();
            }
        }
        #endregion       

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                this.DbConnector.Attach("TY_P_HR_7BUG0144", ds.Tables[0].Rows[i]["MRCOMPANY"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRLESSOR_NAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRLESSOR_JUMIN"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[0].Rows[i]["MRHUSETYPE"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRHUSESIZE"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRLESSOR_JUSO"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRCONSDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRCONEDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["MRRENTAMOUNT"].ToString(),
                                                            "0",
                                                            "N",
                                                            TYUserInfo.EmpNo
                                                           );
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

                this.DbConnector.Attach("TY_P_HR_7BUG3145", ds.Tables[1].Rows[i]["MRLESSOR_NAME"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRLESSOR_JUMIN"].ToString(),
                                                            TYUserInfo.SecureKey,
                                                            ds.Tables[1].Rows[i]["MRHUSETYPE"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRHUSESIZE"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRLESSOR_JUSO"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRCONSDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRCONEDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRRENTAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["MRCOMPANY"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRSABUN"].ToString(),
                                                            ds.Tables[1].Rows[i]["MRSEQ"].ToString()
                                                           );
            }


            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.UP_ProCedure_FixCall();
            
            this.UP_Grid_DataBinding();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_7BUGB148.GetDataSourceInclude(TSpread.TActionType.New, "MRCOMPANY", "MRYEAR", "MRSABUN", "MRSEQ", "MRLESSOR_NAME", "MRLESSOR_JUMIN", "MRHUSETYPE", "MRHUSESIZE",
                                                                                                       "MRLESSOR_JUSO", "MRCONSDATE", "MRCONEDATE", "MRRENTAMOUNT", "MRNTSGN"));
            ds.Tables.Add(this.FPS91_TY_S_HR_7BUGB148.GetDataSourceInclude(TSpread.TActionType.Update, "MRCOMPANY", "MRYEAR", "MRSABUN", "MRSEQ", "MRLESSOR_NAME", "MRLESSOR_JUMIN", "MRHUSETYPE", "MRHUSESIZE",
                                                                                                       "MRLESSOR_JUSO", "MRCONSDATE", "MRCONEDATE", "MRRENTAMOUNT", "MRNTSGN"));
            if( ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (ds.Tables[1].Rows[i]["MRNTSGN"].ToString().Trim() == "Y")
                    {                        
                        this.ShowCustomMessage("국세청자료는 수정이 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BUG3146", dt.Rows[i]["MRCOMPANY"].ToString().Trim(),
                                                                dt.Rows[i]["MRYEAR"].ToString().Trim(),
                                                                dt.Rows[i]["MRSABUN"].ToString().Trim(),
                                                                dt.Rows[i]["MRSEQ"].ToString().Trim());
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            this.UP_Grid_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
            
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_7BUGB148.GetDataSourceInclude(TSpread.TActionType.Remove, "MRCOMPANY", "MRYEAR", "MRSABUN", "MRSEQ","MRNTSGN");

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
                    if (dt.Rows[i]["MRNTSGN"].ToString().Trim() == "Y")
                    {
                        this.ShowCustomMessage("국세청자료는 삭제가 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y", "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion    
   
        #region  Description : FPS91_TY_S_HR_7BUGB148_RowInserted 이벤트
        private void FPS91_TY_S_HR_7BUGB148_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_7BUGB148.SetValue(e.RowIndex, "MRCOMPANY", fsWKCOMPANY);
            this.FPS91_TY_S_HR_7BUGB148.SetValue(e.RowIndex, "MRYEAR", fsWKYEAR);
            this.FPS91_TY_S_HR_7BUGB148.SetValue(e.RowIndex, "MRSABUN", fsWKSABUN);

            this.FPS91_TY_S_HR_7BUGB148.SetValue(e.RowIndex, "MRSEQ", UP_Get_SeqNumber());

            this.FPS91_TY_S_HR_7BUGB148.SetValue(e.RowIndex, "MRNTSGN", "N");
        }
        #endregion    
   
        #region  Description : FPS91_TY_S_HR_7BUGB148_RowInserted 이벤트
        private string  UP_Get_SeqNumber()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C190150", fsWKCOMPANY, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue());
            string sSeq = this.DbConnector.ExecuteScalar().ToString();

            return sSeq;
        }
        #endregion      

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion      

       


    }
}
