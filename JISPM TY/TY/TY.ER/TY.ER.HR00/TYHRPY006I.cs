using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여지급관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.18 11:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CIA9857 : 급여지급관리 조회
    ///  TY_P_HR_4CIBJ862 : 급여지급관리 등록
    ///  TY_P_HR_4CIBK863 : 급여지급관리 수정
    ///  TY_P_HR_4CIBK864 : 급여지급관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CIBM865 : 급여지급관리 조회
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
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PAYGUBN : 급여구분
    ///  PAYSOPYCODE : 소급급여코드
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY006I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY006I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CIBM865, "PAYGUBN", "PAYGUBNNM", "PAYGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CIBM865, "PAYSOPYCODE", "PAYSOPYCODENM", "PAYSOPYCODE");

        }

        private void TYHRPY006I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYGUBNNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYJIDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYYYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYGUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "PAYJIDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "BTN1");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "BTN2");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "BTN3");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CIBM865, "BTN4");

            (this.FPS91_TY_S_HR_4CIBM865.Sheets[0].Columns[17].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            (this.FPS91_TY_S_HR_4CIBM865.Sheets[0].Columns[18].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            (this.FPS91_TY_S_HR_4CIBM865.Sheets[0].Columns[19].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            (this.FPS91_TY_S_HR_4CIBM865.Sheets[0].Columns[20].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);


            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-10).ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4CIBM865.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CIA9857", this.DTP01_SDATE.GetString().Substring(0,6), this.DTP01_EDATE.GetString().Substring(0,6), this.CBH01_PAYGUBN.GetValue());
            this.FPS91_TY_S_HR_4CIBM865.SetValue(this.DbConnector.ExecuteDataTable());
            
            for (int i = 0; i < this.FPS91_TY_S_HR_4CIBM865.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_4CIBM865.GetValue(i, "PAYSTATUS").ToString() == "Y")
                {
                    for (int j = 0; j < 13; j++)
                    {
                        this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[i, j].Locked = true;
                        this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[i, j].ForeColor = Color.Red;
                    }                    
                }

                if ( this.FPS91_TY_S_HR_4CIBM865.GetValue(i, "PAYGUBN").ToString().Substring(0,1) != "M")
                {
                    this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[i, 20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CIBK864", dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_5BOBG208", dt.Rows[i]["PAYGUBN"].ToString(), dt.Rows[i]["PAYYYMM"].ToString(), dt.Rows[i]["PAYJIDATE"].ToString());

                this.DbConnector.Attach("TY_P_HR_5BP8O213", dt.Rows[i]["PAYGUBN"].ToString(), dt.Rows[i]["PAYYYMM"].ToString(), dt.Rows[i]["PAYJIDATE"].ToString());

                this.DbConnector.Attach("TY_P_HR_5BP8P214", dt.Rows[i]["PAYGUBN"].ToString(), dt.Rows[i]["PAYYYMM"].ToString(), dt.Rows[i]["PAYJIDATE"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);            
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CIBM865.GetDataSourceInclude(TSpread.TActionType.Remove, "PAYYYMM", "PAYGUBN", "PAYJIDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //급여생성이 되었는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_535G1512", dt.Rows[i]["PAYGUBN"].ToString(), dt.Rows[i]["PAYYYMM"].ToString(), dt.Rows[i]["PAYJIDATE"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());                

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여생성이 되었습니다! 삭제 할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
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

            this.DataTableColumnAdd(ds.Tables[0], "PAHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PAHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CIBJ862", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_HR_4CIBK863", ds.Tables[1]);

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);            
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            
            ds.Tables.Add(this.FPS91_TY_S_HR_4CIBM865.GetDataSourceInclude(TSpread.TActionType.New, "PAYYYMM", "PAYGUBN", "PAYJIDATE", "PAYAPSDATE", "PAYAPEDATE", "PAYOTSDATE", "PAYOTEDATE", "PAYBONUSYYMM","PAYTAXADJ", "PAYSOKP", "PAYSOPYCODE", "PAYAMOUNT", "PAYTAXRATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CIBM865.GetDataSourceInclude(TSpread.TActionType.Update, "PAYYYMM", "PAYGUBN", "PAYJIDATE", "PAYAPSDATE", "PAYAPEDATE", "PAYOTSDATE", "PAYOTEDATE", "PAYBONUSYYMM", "PAYTAXADJ", "PAYSOKP", "PAYSOPYCODE", "PAYAMOUNT", "PAYTAXRATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["PAYGUBN"].ToString().Trim().Substring(0, 1) == "M" && ds.Tables[0].Rows[i]["PAYBONUSYYMM"].ToString().Trim() == "")
                    {
                        this.ShowCustomMessage("상여년월을 선택하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }                   

                    if (ds.Tables[0].Rows[i]["PAYGUBN"].ToString().Trim().Substring(0, 1) == "S" && ds.Tables[0].Rows[i]["PAYBONUSYYMM"].ToString().Trim() != "190001")
                    {
                        this.ShowCustomMessage("상여작업시에는 선택할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["PAYGUBN"].ToString().Trim().Substring(0, 1) == "S" && ds.Tables[0].Rows[i]["PAYSOKP"].ToString().Trim() == "Y")
                    {
                        this.ShowCustomMessage("상여작업시에는 소급급여를 선택할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["PAYSOKP"].ToString().Trim().Substring(0, 1) == "Y" && ds.Tables[0].Rows[i]["PAYSOPYCODE"].ToString().Trim() == "")
                    {
                        this.ShowCustomMessage("소급급여코드를 선택하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_535G1512", ds.Tables[1].Rows[i]["PAYGUBN"].ToString(),ds.Tables[1].Rows[i]["PAYYYMM"].ToString(), ds.Tables[1].Rows[i]["PAYJIDATE"].ToString());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                    if ( iCnt > 0)
                    {
                         this.ShowCustomMessage("급여생성이 되었습니다! 수정 할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                         e.Successed = false;
                         return;
                    }
                    if (ds.Tables[1].Rows[i]["PAYGUBN"].ToString().Trim().Substring(0, 1) == "S" && ds.Tables[1].Rows[i]["PAYSOKP"].ToString().Trim() == "Y")
                    {
                        this.ShowCustomMessage("상여작업시에는 소급급여를 선택할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[1].Rows[i]["PAYSOKP"].ToString().Trim().Substring(0, 1) == "Y" && ds.Tables[1].Rows[i]["PAYSOPYCODE"].ToString().Trim() == "")
                    {
                        this.ShowCustomMessage("소급급여코드를 선택하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_4CIBM865_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CIBM865.SetValue(e.RowIndex, "PAYTAXADJ", "N");
            this.FPS91_TY_S_HR_4CIBM865.SetValue(e.RowIndex, "PAYSOKP", "N");

            this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.RowIndex, 17].Locked = true;
            this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.RowIndex, 18].Locked = true;
            this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.RowIndex, 19].Locked = true;
            
        }               
        private void FPS91_TY_S_HR_4CIBM865_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            string sPAYAPSDATE = string.Empty;
            string sPAYAPEDATE = string.Empty;
            string sPAYOTSDATE = string.Empty;
            string sPAYOTEDATE = string.Empty;
            string sPAYBONUSYYMM = string.Empty;

            if (e.Column == 3)
            {
                //지급일자 입력시 근무일자, 연장일자 계산
                string sPAYGUBN = this.FPS91_TY_S_HR_4CIBM865.GetValue(e.Row, "PAYGUBN").ToString().Trim();
                string sPAYJIDATE =  this.FPS91_TY_S_HR_4CIBM865.GetValue(e.Row, "PAYJIDATE").ToString().Trim();
                
                if (sPAYGUBN == "" || sPAYJIDATE == "") return;

                DateTime dTime = Convert.ToDateTime(sPAYJIDATE.Substring(0, 4) + "-" + sPAYJIDATE.Substring(4, 2) + "-"+ sPAYJIDATE.Substring(6,2));

                if (sPAYGUBN.Substring(0, 1) == "M" || sPAYGUBN.Substring(0, 2) == "S1")
                {
                    sPAYAPSDATE = sPAYJIDATE.Substring(0, 6) + "01";
                    sPAYAPEDATE = sPAYJIDATE.Substring(0, 6) + Set_Fill2(Convert.ToString(DateTime.DaysInMonth(int.Parse(dTime.Year.ToString()), int.Parse(dTime.Month.ToString()))));

                    sPAYOTSDATE = dTime.AddMonths(-1).Year.ToString() + Set_Fill2( dTime.AddMonths(-1).Month.ToString()) + "21";
                    sPAYOTEDATE = dTime.Year.ToString() + Set_Fill2(dTime.Month.ToString()) + "20";

                    sPAYBONUSYYMM = this.FPS91_TY_S_HR_4CIBM865.GetValue(e.Row, "PAYYYMM").ToString().Trim(); 
                }

                if (sPAYGUBN.Substring(0, 1) == "S")
                {
                    sPAYOTSDATE = "";
                    sPAYOTEDATE = "";

                    sPAYBONUSYYMM = "";

                    this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.Row, 6].Locked = true;
                    this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.Row, 7].Locked = true;
                    this.FPS91_TY_S_HR_4CIBM865_Sheet1.Cells[e.Row, 8].Locked = true;
                }
                //else if (sPAYGUBN.Substring(0, 2) == "S1")
                //{
                //    sPAYAPSDATE = dTime.AddMonths(-1).Year.ToString() + Set_Fill2(dTime.AddMonths(-1).Month.ToString()) + "01";
                //    sPAYAPEDATE = sPAYJIDATE.Substring(0, 6) + Set_Fill2(Convert.ToString(DateTime.DaysInMonth(int.Parse(dTime.Year.ToString()), int.Parse(dTime.Month.ToString()))));

                //    sPAYOTSDATE = "";
                //    sPAYOTEDATE = "";
                //}

                this.FPS91_TY_S_HR_4CIBM865.SetValue(e.Row, "PAYAPSDATE", sPAYAPSDATE);
                this.FPS91_TY_S_HR_4CIBM865.SetValue(e.Row, "PAYAPEDATE", sPAYAPEDATE);
                this.FPS91_TY_S_HR_4CIBM865.SetValue(e.Row, "PAYOTSDATE", sPAYOTSDATE);
                this.FPS91_TY_S_HR_4CIBM865.SetValue(e.Row, "PAYOTEDATE", sPAYOTEDATE);
                this.FPS91_TY_S_HR_4CIBM865.SetValue(e.Row, "PAYBONUSYYMM", sPAYBONUSYYMM);
            }
        }
        #endregion

        #region Description : 스프레드 버튼 클릭 이벤트
        private void FPS91_TY_S_HR_4CIBM865_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "17")
            {
                if ((new TYHRPY06C1(this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYGUBN").ToString(),
                                    this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYYYMM").ToString(),
                                    this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYJIDATE").ToString()
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

            if (e.Column.ToString() == "18")
            {
                if ((new TYHRPY06C2(this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYGUBN").ToString(),
                                    this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYYYMM").ToString(),
                                    this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYJIDATE").ToString()
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

            if (e.Column.ToString() == "19")
            {
                if ((new TYHRPY06C3(this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYGUBN").ToString(),
                                   this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYYYMM").ToString(),
                                   this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYJIDATE").ToString()
                                   )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

            if (e.Column.ToString() == "20")
            {
                if ((new TYHRPY06C5(this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYGUBN").ToString(),
                                   this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYYYMM").ToString(),
                                   this.FPS91_TY_S_HR_4CIBM865.GetValue("PAYJIDATE").ToString()
                                   )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

    }
}
