using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 출입자 인원관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.05.08 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_852HN908 : 출입자 관리(공사) 상세 조회
    ///  TY_P_HR_853GV938 : 출입자 관리(공사) 상세 존재 유무
    ///  TY_P_HR_853GW939 : 출입자 관리(공사) 상세 등록
    ///  TY_P_HR_853HN945 : 출입자 관리(공사) 상세 수정
    ///  TY_P_HR_853HS949 : 출입자 관리(공사) 상세 삭제
    ///  TY_P_HR_859EG973 : 출입자 인원 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_852HP910 : 출입자 관리(공사) 상세조회
    ///  TY_S_HR_859EG975 : 출입자 인원 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OK : 확인
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BLNAME : 성명
    ///  CIVEND : 거래처
    ///  CIDATE : 작업일자
    ///  BLJUMIN : 주민번호
    ///  CISEQ : 순번
    /// </summary>
    public partial class TYHRGB01C1 : TYBase
    {
        string fsCIDATE = string.Empty;
        string fsCISEQ  = string.Empty;
        string fsGUBUN  = string.Empty;
        string fsCIVEND = string.Empty;

        public DataSet ds = new DataSet();

        #region Description : 폼 로드
        public TYHRGB01C1(string sCIDATE, string sCISEQ, string sGUBUN, string sCIVEND)
        {
            InitializeComponent();
            fsCIDATE = sCIDATE;
            fsCISEQ = sCISEQ;
            fsGUBUN = sGUBUN;
            fsCIVEND = sCIVEND;
        }

        private void TYHRGB01C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_BATCH.Visible = false;

            this.BTN61_INQ_Click(null, null);

            UP_Select();

            SetStartingFocus(this.TXT01_BLNAME);            
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_859EG975.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_859EG973", this.TXT01_BLNAME.GetValue().ToString(),
                                                        this.TXT01_BLJUMIN.GetValue().ToString(),
                                                        this.TXT01_CIVEND.GetValue().ToString());
            this.FPS91_TY_S_HR_859EG975.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 확인 버튼
        private void BTN61_OK_Click(object sender, EventArgs e)
        {
            ds.Tables.Add(FPS91_TY_S_HR_852HP910.GetDataSource(TSpread.TActionType.All));

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region Description : << 선택 버튼(삭제)
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_852HP910.GetDataSourceInclude(TSpread.TActionType.Select, "CLDATE", "CLSEQ", "CLNAME", "CLJUMIN");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for(int j = 0; j < this.FPS91_TY_S_HR_852HP910.ActiveSheet.Rows.Count ; j ++)
                {
                    if (this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 2].Text == dt.Rows[i]["CLNAME"].ToString() &&
                        this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 3].Text == dt.Rows[i]["CLJUMIN"].ToString())
                    {
                        this.FPS91_TY_S_HR_852HP910.ActiveSheet.RemoveRows(j,1);
                    }
                }
            }
        }
        #endregion

        #region Description : >> 선택 버튼(등록)
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_852HP910.ActiveSheet.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iRowIndex = iRowIndex + 1;

                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.AddRows(iRowIndex - 1, 1);
                    //this.FPS91_TY_S_HR_852HP910.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 0].Text = this.fsCIDATE.ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 1].Text = this.fsCISEQ.ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 2].Text = dt.Rows[i]["BANAME"].ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["BAJUMINFULL"].ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 4].Text = dt.Rows[i]["BAJUSO"].ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 5].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 6].Text = dt.Rows[i]["BATEL"].ToString();
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 7].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 8].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 9].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 10].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 11].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 12].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 13].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 14].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 15].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 16].Text = "";
                    this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 17].Text = "ADD";
                }
            }
            BTN61_INQ_Click(null,null);
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_859EG975.GetDataSourceInclude(TSpread.TActionType.Select, "BASEQ", "BANAME", "BAJUMIN", "BAJUSO", "BAVEND", "BATEL", "BAJUMINFULL");

            if (dt.Rows.Count == 0)
            {   
                e.Successed = false;
                return;
            }

            //출입자 명단에 등록되어 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.FPS91_TY_S_HR_852HP910.ActiveSheet.Rows.Count; j++)
                {
                    if (dt.Rows[i]["BANAME"].ToString().Trim() == this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 2].Text.Trim() &&
                        dt.Rows[i]["BAJUMINFULL"].ToString().Trim() == this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 3].Text.Trim())
                    {
                        this.ShowCustomMessage("출입자 명단에 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (dt.Rows[i]["BAJUMINFULL"].ToString().Trim().Length < 7)
                    {
                        this.ShowCustomMessage("주민등록번호가 올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 출입자 인원내역 조회
        private void UP_Select()
        {
            string sProcedure = string.Empty;

            try
            {
                this.FPS91_TY_S_HR_852HP910.Initialize();

                if (fsGUBUN == "공사")
                {
                    sProcedure = "TY_P_HR_852HN908";
                }
                else
                {
                    sProcedure = "TY_P_HR_85AI0007";
                }

                this.DbConnector.CommandClear();



                this.DbConnector.Attach(sProcedure, fsCIDATE.ToString(),
                                                    fsCISEQ.ToString());

                this.FPS91_TY_S_HR_852HP910.SetValue(this.DbConnector.ExecuteDataTable());
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            UP_FieldClear();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN62_BATCH_Click(object sender, EventArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {   
                return;
            }

            this.DbConnector.CommandClear();
            if (this.TXT01_BASEQ.GetValue().ToString() == "")
            {
                this.DbConnector.Attach("TY_P_HR_853BO924",
                                        this.TXT01_BANAME.GetValue().ToString(),
                                        this.TXT01_BAJUMIN.GetValue().ToString(),
                                        this.CBH01_BACODE.GetValue().ToString(),
                                        this.CBH01_BACODE.GetText().ToString(),
                                        this.TXT01_BAJUSO.GetValue().ToString(),
                                        this.TXT01_BACARNO.GetValue().ToString(),
                                        this.TXT01_BATEL.GetValue().ToString(),
                                        this.CBO01_BASTOPGN.GetValue().ToString(),
                                        this.TXT01_BASTOPSU.GetValue().ToString(),
                                        this.CBO01_BASAFEGN.GetValue().ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_853BP925",
                                        this.CBH01_BACODE.GetValue().ToString(),
                                        this.CBH01_BACODE.GetText().ToString(),
                                        this.TXT01_BAJUSO.GetValue().ToString(),
                                        this.TXT01_BACARNO.GetValue().ToString(),
                                        this.TXT01_BATEL.GetValue().ToString(),
                                        this.CBO01_BASTOPGN.GetValue().ToString(),
                                        this.TXT01_BASTOPSU.GetValue().ToString(),
                                        this.CBO01_BASAFEGN.GetValue().ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString(),
                                        this.TXT01_BASEQ.GetValue().ToString(),
                                        this.TXT01_BANAME.GetValue().ToString(),
                                        this.TXT01_BAJUMIN.GetValue().ToString()
                                        );
            }
            this.DbConnector.ExecuteTranQuery();

            fsCIVEND = this.CBH01_BACODE.GetValue().ToString();

            UP_SAVE();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN62_NEW_Click(null, null);
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_HR_859EG975_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Run(this.FPS91_TY_S_HR_859EG975.GetValue("BASEQ").ToString(),
                   this.FPS91_TY_S_HR_859EG975.GetValue("BANAME").ToString(),
                   this.FPS91_TY_S_HR_859EG975.GetValue("BAJUMINFULL").ToString());
        }
        #endregion

        #region Description : 데이터 확인
        private void UP_Run(string sBASEQ, string sBANAME, string sBAJUMIN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_853BA923", sBASEQ, sBANAME, sBAJUMIN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
                UP_FieldLock("LOCK");
                this.BTN62_BATCH.Visible = true;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.TXT01_BASEQ.SetValue("");
            this.TXT01_BANAME.SetValue("");
            this.TXT01_BAJUMIN.SetValue("");
            this.CBH01_BACODE.SetValue(fsCIVEND);
            this.TXT01_BAJUSO.SetValue("");
            this.TXT01_BACARNO.SetValue("");
            this.TXT01_BATEL.SetValue("");
            this.CBO01_BASTOPGN.SetValue("");
            this.TXT01_BASTOPSU.SetValue("");
            this.CBO01_BASAFEGN.SetValue("");

            UP_FieldLock("UNLOCK");
            this.BTN62_BATCH.Visible = true;
            this.TXT01_BANAME.Focus();
        }
        #endregion

        #region Description : 필드 잠금
        private void UP_FieldLock(string sGUBUN)
        {
            if (sGUBUN == "LOCK")
            {
                this.TXT01_BANAME.SetReadOnly(true);
                this.TXT01_BAJUMIN.SetReadOnly(true);
            }
            else
            {
                this.TXT01_BANAME.SetReadOnly(false);
                this.TXT01_BAJUMIN.SetReadOnly(false);
            }
        }
        #endregion

        #region Description : 출입자 인원 저장 후 명부에 등록
        private void UP_SAVE()
        {
            int iRowIndex = 0;
            bool b = true;

            for (int j = 0; j < this.FPS91_TY_S_HR_852HP910.ActiveSheet.Rows.Count; j++)
            {
                if (this.TXT01_BANAME.GetValue().ToString() == this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 2].Text.Trim() &&
                    this.TXT01_BAJUMIN.GetValue().ToString() == this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[j, 3].Text.Trim())
                {
                    b = false;
                }
            }

            if (b == true)
            {
                iRowIndex = this.FPS91_TY_S_HR_852HP910.ActiveSheet.Rows.Count;
                iRowIndex = iRowIndex + 1;

                this.FPS91_TY_S_HR_852HP910.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 0].Text = this.fsCIDATE.ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 1].Text = this.fsCISEQ.ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 2].Text = this.TXT01_BANAME.GetValue().ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 3].Text = this.TXT01_BAJUMIN.GetValue().ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 4].Text = this.TXT01_BAJUSO.GetValue().ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 5].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 6].Text = this.TXT01_BATEL.GetValue().ToString();
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 7].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 8].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 9].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 10].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 11].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 12].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 13].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 14].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 15].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 16].Text = "";
                this.FPS91_TY_S_HR_852HP910.ActiveSheet.Cells[iRowIndex - 1, 17].Text = "ADD";
            }
        }
        #endregion

        #region Description : 엔터 키 이벤트
        private void TXT01_CIVEND_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }

        private void CBO01_BASAFEGN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN62_BATCH);
            }
        }
        #endregion
    }
}
