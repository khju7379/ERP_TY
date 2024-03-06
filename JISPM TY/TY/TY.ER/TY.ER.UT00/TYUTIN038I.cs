using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Data.OleDb;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 가성소다 지시 UPLOAD 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.05.31 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6ABKU341 : 이전 HSN에 대한 데이터 가져오기
    ///  TY_P_UT_6BAF3716 : 출고지시 - 입고탱크 조회
    ///  TY_P_UT_6BAF5721 : 탱크 - 위치 가져오기
    ///  TY_P_UT_6BBD1736 : 출고지시번호 가져오기
    ///  TY_P_UT_6BBD4737 : 출고지시번호 등록
    ///  TY_P_UT_6BBD4738 : 출고지시번호 수정
    ///  TY_P_UT_6BBD7735 : 출고지시번호 - 순번체크
    ///  TY_P_UT_6BBEP748 : 대기차량관리 테이블 업데이트
    ///  TY_P_UT_A7OGE818 : 출고지시 등록(20200724)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A859I883 : 가성소다 지시 UPLOAD
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_31B1C623 : EXCEL 업데이트 할 파일을 선택하세요.
    ///  TY_M_AC_31BAP617 : EXCEL 업데이트가 완료 되었습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_UT_6APJV532 : EXCEL 파일 열기가 완료 되었습니다.
    ///  TY_M_UT_6APKH541 : EXCEL 파일을 업데이트 하시겠습니까?
    /// 
    ///  # 필드사전 정보 ####
    ///  EXCEL : 엑셀 업데이트
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    /// </summary>
    public partial class TYUTIN038I : TYBase
    {
        private double fdJISIMTQTY_TOT = 0;

        #region Description : 폼 로드
        public TYUTIN038I()
        {
            InitializeComponent();

            // 출고화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_A859I883, "JICHHJ", "JICHHJNM", "JICHHJ");
        }

        private void TYUTIN038I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_UT_A859I883.Sheets[0].Columns[51].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.leftmenu_search_icon_02;
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_A859I883, "BTNJEGO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_INQ.Visible = false;
            this.BTN61_SAV.Visible = false;
        }
        #endregion

        #region Description : 파일 찾기 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region Description : 엑셀 열기 버튼
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_UT_A859I883.Initialize();
                
                string strProvider = string.Empty;
                string strQuery = string.Empty;

                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                strQuery = "SELECT * FROM [Sheet1$] "; //  , Sheet1$

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                this.FPS91_TY_S_UT_A859I883_Sheet1.RowCount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 0].Value = ds.Tables[0].Rows[i][5].ToString().Replace("-", "").Trim().Substring(0, 8);  // 입차요청일 (지시일자 : JIYYMM)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 2].Value = "HHK";   // 재고화주 (JIJGHWAJU)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 3].Value = "C02";   // 화물 (JIHWAMUL)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 5].Value = UP_GetHWAJU(ds.Tables[0].Rows[i][1].ToString().Trim());  // 출고화주 (JICHHJ)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 6].Value = ds.Tables[0].Rows[i][1].ToString().Trim();  // 인도처 (출고화주명 : JICHHJNM)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 10].Value = "01";   // 출고유형(JIWKTYPE)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 11].Value = UP_ChangeCARNO(ds.Tables[0].Rows[i][2].ToString());  // 차량번호 (JICARNO2)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 15].Value = Convert.ToString((double.Parse(ds.Tables[0].Rows[i][3].ToString().Trim())));    //의뢰수량 (지시수량 : JISTMTQTY)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 16].Value = "0";    // JIEDMTQTY
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 17].Value = "0";    // JISTLTQTY
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 18].Value = "0";    // JIEDLTQTY
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 19].Value = UP_GetJITMGUBN(ds.Tables[0].Rows[i][4].ToString().Trim(), ds.Tables[0].Rows[i][5].ToString().Replace("-", "").Trim().Substring(0, 8)); // 특허구분 (JITMGUBN)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 20].Value = ds.Tables[0].Rows[i][4].ToString().Trim();  // 배차주의사항 (지시시간 : JITIMEHH)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 24].Value = "1";    // 지시대수 (JIJICNT)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 40].Value = "1";    // 출하방법 (JICHTYPE)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 41].Value = "1";    // 출고단위 (JIUNIT)
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 42].Value = "1";    // JIRACK
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 43].Value = "1";    // JIPUMP
                }
                this.BTN61_INQ.Visible = true;
                this.BTN61_SAV.Visible = true;

                ExcelCon.Close();

                this.ShowMessage("TY_M_UT_6APJV532");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
        }
        #endregion

        #region Description : 재고조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_CALL_JEGO();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.FPS91_TY_S_UT_A859I883.ActiveSheet.RowCount; i++)
            {
                // 출고지시 번호 등록
                this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 1].Value = UP_UTIJICNF_INS(Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYYMM").ToString()));

                // 출고지시 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A7OGE818", Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYYMM").ToString()),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JISEQ").ToString(),
                                                            Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIACTHJ").ToString(),      // 10
                                                            Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICUSTIL").ToString()),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHASU").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIJGHWAJU").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSHWAJU").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDHWAJU").ToString(),
                                                            Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSDATE").ToString()),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDSEQ").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSSEQ").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYNGUBUN").ToString().Replace("N", ""),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHHJ").ToString(),       // 20
                                                            Set_TankNo(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString()),
                                                            Set_TankNo(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPTANK").ToString()),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIJANQTY").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JISTMTQTY").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIEDMTQTY").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JISTLTQTY").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIEDLTQTY").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITMGUBN").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITIMEHH").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITIMEMM").ToString(),     // 30
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIDNST").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIJICNT").ToString(),  // 차량대수
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO1").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO2").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHJANG").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHTYPE").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIUNIT").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIWKTYPE").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIRACK").ToString(),   // RACK
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIPUMP").ToString(),   // PUMP 40
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICONTNUM").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JISILNUM").ToString(),
                                                            this.FPS91_TY_S_UT_A859I883.GetValue(i, "JISILNUMCK").ToString(),
                                                            "",
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                            );

                this.DbConnector.ExecuteNonQuery();

                // 대기차량관리 업데이트
                UP_UTICARSTBF_UPDATE(Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYYMM").ToString()), this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO2").ToString());
            }
            this.BTN61_SAV.Visible = false;
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 출고지시번호 등록
        private string UP_UTIJICNF_INS(string sJIYYMM)
        {
            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_UT_6BBD7735",
                sJIYYMM
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                sJCSEQ = "1";

                // 출고지시번호 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BBD4737", sJIYYMM,
                                                            sJCSEQ.ToString()
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_6BBD1736",
                   sJIYYMM
                   );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    sJCSEQ = dt1.Rows[0]["JCSEQ"].ToString();
                }

                // 출고지시번호 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BBD4738", sJCSEQ.ToString(),
                                                            sJIYYMM
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }

            // 지시 순번
            return sJCSEQ;
        }
        #endregion

        #region Description : 대기차량관리 업데이트
        private void UP_UTICARSTBF_UPDATE(string sJIYYMM, string sJICARNO2)
        {
            // 대기차량관리 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BBEP748", sJIYYMM,
                                                        DateTime.Now.ToString("HHmmss").ToString(),
                                                        sJIYYMM,
                                                        sJICARNO2
                                                        );

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < this.FPS91_TY_S_UT_A859I883.ActiveSheet.RowCount; i++)
            {

                // 날짜 체크
                if (!dateValidateCheck(Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYYMM").ToString())))
                {
                    this.ShowCustomMessage("지시일자를 확인 하세요![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                // BL별 입고값 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBK434",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 39].Value = dt.Rows[0]["IPJANQTY"].ToString();   // 미통관재고 
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 44].Value = dt.Rows[0]["IPMTQTY"].ToString();    // 입고수량
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 45].Value = dt.Rows[0]["IPPAQTY"].ToString();    // 통관누계
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 46].Value = dt.Rows[0]["IPCHQTY"].ToString();    // 출고누계
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 47].Value = dt.Rows[0]["IPJEQTY"].ToString();    // 통관재고
                }

                // 이전 HSN에 대한 데이터 가져오기
                this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 48].Value = UP_GET_PreIPMTQTY(Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                                                                                          this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString()
                                                                                          );

                // 미통관잔량 = MT입고량 - 통관량 - BL분할 수량	    
                this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 39].Value = String.Format("{0:#,##0.000}", double.Parse(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPQTY").ToString()) - double.Parse(this.FPS91_TY_S_UT_A859I883.GetValue(i, "IPPAQTY").ToString()) - double.Parse(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JUNIPMTQTY").ToString()));

                // 출고탱크 체크
                if (this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6AKBS435",
                        Set_TankNo(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString()).Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("출고 탱크번호를 확인 하세요![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }

                // 입고탱크 체크
                if (this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPTANK").ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6AKBS435",
                        Set_TankNo(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPTANK").ToString()).Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {   
                        this.ShowCustomMessage("입고 탱크번호를 확인하세요![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }

                // 입고화물 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBY438",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {   
                    this.ShowCustomMessage("입고 화물이 없습니다![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }


                // B/L별 입고 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBZ440",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {   
                    this.ShowCustomMessage("B/L별 입고 자료가 없습니다![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                // 이고가 없을 경우 출고탱크와 입고탱크 번호가 일치해야 함
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_752FQ401",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString().Trim() != this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPTANK").ToString().Trim())
                    {   
                        this.ShowCustomMessage("출고 탱크 번호와 입고탱크 번호를 확인하세요.[" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }

                // SURVEY파일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKCN442",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JITANKNO").ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {   
                    this.ShowCustomMessage("SURVEY 화일이 없습니다![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                // 매출입고 할증 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKCN444",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPTANK").ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("매출 입고 할증이 없습니다![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }


                // 통관화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKDD446",
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIACTHJ").ToString().ToUpper(),
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString(),
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICUSTIL").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHASU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIJGHWAJU").ToString().ToUpper(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSHWAJU").ToString().ToUpper(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDHWAJU").ToString().ToUpper(),
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSDATE").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDSEQ").ToString().ToUpper(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSSEQ").ToString().ToUpper()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("통관화주 자료가 없습니다![" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                // 양수도일 경우 체크
                if (this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYNGUBUN").ToString() == "R")
                {
                    // 양수도 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_699AD123",
                        Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString(),
                        Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICUSTIL").ToString()),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHASU").ToString(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIACTHJ").ToString().ToUpper(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDHWAJU").ToString().ToUpper(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSHWAJU").ToString().ToUpper(),
                        Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSDATE").ToString()),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYDSEQ").ToString().ToUpper(),
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIYSSEQ").ToString().ToUpper()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("양수도 자료가 존재하지 않습니다.[" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }


                // 통관화일 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BAGW725",
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIIPHANG").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBONSUN").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAJU").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHWAMUL").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIBLNO").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIMSNSEQ").ToString(),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JIHSNSEQ").ToString(),
                    Get_Date(this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICUSTIL").ToString()),
                    this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICHASU").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {   
                    this.ShowCustomMessage("통관화일이 없습니다.[" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt.Rows[0]["CSBANGB"].ToString() == "70" || dt.Rows[0]["CSBANGB"].ToString() == "71")
                    {   
                        this.ShowCustomMessage("반송화물 입니다.[" + (i + 1) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }

                // 차량번호 체크
                if (this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO2").ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_6BAH0732",
                        this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO2").ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("미등록 차량번호입니다.[" + (i + 1) + "/" + this.FPS91_TY_S_UT_A859I883.GetValue(i, "JICARNO2").ToString() + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 재고 조회(통관 및 양수도)
        private void UP_CALL_JEGO()
        {
            double dIPJEQTY = 0;
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A91E3948");

            dt = this.DbConnector.ExecuteDataTable();

            TYUTGB008S popup = new TYUTGB008S("HHK");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dIPJEQTY = Convert.ToDouble(popup.fsIPJEQTY);   // 재고량

                for (int i = 0; i < this.FPS91_TY_S_UT_A859I883.ActiveSheet.RowCount; i++)
                {
                    if (dIPJEQTY > Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 15].Value.ToString())))
                    {
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 3].Value = popup.fsHWAMUL;      // 화물       
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 4].Value = popup.fsYNGUBUN;     // 양수구분   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 7].CellType = new TComboBoxCellType(UP_GET_CHTANKLIST(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL));
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 7].Value = popup.fsTANKNO;      // 출고탱크
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 8].CellType = new TComboBoxCellType(UP_GET_IPTANKLIST(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL));
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 8].Value = UP_GET_JIIPTANK(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL);      // 입고탱크
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 9].CellType = new TComboBoxCellType(dt);
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 9].Value = UP_GET_JICHJANG(popup.fsTANKNO);      // 출하장
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 25].Value = popup.fsIPHANG;     // 입항일자   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 26].Value = popup.fsBONSUN;     // 본선       
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 27].Value = popup.fsHWAJU;      // 화주       
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 28].Value = popup.fsBLNO;       // BL번호     
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 29].Value = popup.fsMSNSEQ;     // MSN번호    
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 30].Value = popup.fsHSNSEQ;     // HSN번호    
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 31].Value = popup.fsACTHJ;      // 통관화주   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 32].Value = popup.fsCUSTIL;     // 통관일자   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 33].Value = popup.fsCHASU;      // 통관차수   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 34].Value = popup.fsYSHWAJU;    // 양수화주   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 35].Value = popup.fsYDHWAJU;    // 양도화주   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 36].Value = popup.fsYSDATE;     // 양수일자   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 37].Value = popup.fsYDSEQ;      // 양도차수   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 38].Value = popup.fsYSSEQ;      // 양수순번   
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 39].Value = popup.fsIPJANQTY;   // 미통관재고 
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 44].Value = popup.fsIPMTQTY;    // 입고수량
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 45].Value = popup.fsIPPAQTY;    // 통관누계
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 46].Value = popup.fsIPCHQTY;    // 출고누계
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 47].Value = popup.fsIPJEQTY;    // 통관재고
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 49].Value = popup.fsCJJEQTY;    // 재고수량
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 50].Value = dIPJEQTY;    // 예상재고수량

                        // 이전 HSN에 대한 데이터 가져오기
                        this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 48].Value = UP_GET_PreIPMTQTY(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL, popup.fsBLNO, popup.fsMSNSEQ, popup.fsHSNSEQ);

                        dIPJEQTY = dIPJEQTY - Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[i, 15].Value.ToString()));
                    }
                }
            }
        }
        #endregion

        #region Description : 출고탱크 리스트 조회
        private DataTable UP_GET_CHTANKLIST(string sJIIPHANG, string sJIBONSUN, string sJIHWAJU, string sJIHWAMUL)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
               (
               "TY_P_UT_A92BP953",
               sJIIPHANG,       // 입항일자
               sJIBONSUN,       // 본선
               sJIHWAJU,        // 화주
               sJIHWAMUL        // 화물
               );

            dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region Description : 입고탱크 리스트 조회
        private DataTable UP_GET_IPTANKLIST(string sJIIPHANG, string sJIBONSUN, string sJIHWAJU, string sJIHWAMUL)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
               (
               "TY_P_UT_A92BP954",
               sJIIPHANG,       // 입항일자
               sJIBONSUN,       // 본선
               sJIHWAJU,        // 화주
               sJIHWAMUL        // 화물
               );

            dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region Description : 입고탱크 가져오기
        private string UP_GET_JIIPTANK(string sJIIPHANG, string sJIBONSUN, string sJIHWAJU, string sJIHWAMUL)
        {
            string sJIIPTANK = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3716",
                sJIIPHANG,       // 입항일자
                sJIBONSUN,       // 본선
                sJIHWAJU,        // 화주
                sJIHWAMUL        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    sJIIPTANK = dt.Rows[0]["COTANKNO"].ToString();
                }
            }

            return sJIIPTANK;
        }
        #endregion

        #region Description : 출하장 가져오기
        private string UP_GET_JICHJANG(string JITANKNO)
        {
            string sJICHJANG = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF5721",
                Set_TankNo(JITANKNO).Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    sJICHJANG = dt.Rows[0]["TNLOCATE"].ToString();
                }
            }
            return sJICHJANG;
        }
        #endregion

        #region Description : 이전 HSN에 대한 데이터 가져오기
        private string UP_GET_PreIPMTQTY(string sJIIPHANG, string sJIBONSUN, string sJIHWAJU, string sJIHWAMUL,
                                       string sJIBLNO, string sJIMSNSEQ, string sJIHSNSEQ)
        {
            string sJUNIPMTQTY = "";
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6ABKU341",
                sJIIPHANG,       // 입항일자
                sJIBONSUN,       // 본선
                sJIHWAJU,        // 화주
                sJIHWAMUL,       // 화물
                sJIBLNO,         // BL번호
                sJIMSNSEQ,       // MSN번호
                sJIHSNSEQ        // HSN번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sJUNIPMTQTY = dt.Rows[0]["IPMTQTY"].ToString();
            }

            return sJUNIPMTQTY;
        }
        #endregion

        #region Description : 차량번호 변환
        private string UP_ChangeCARNO(string sCARNO)
        {
            string[] sTemp = sCARNO.Split('-');
            string sRtnCarno = string.Empty;

            try
            {
                if (sTemp.Length == 2)
                {   
                    sRtnCarno = sTemp[1].Replace('1', '１').Replace('2', '２').Replace('3', '３').Replace('4', '４').Replace('5', '５').Replace('6', '６').Replace('7', '７').Replace('8', '８').Replace('9', '９').Replace('0', '０');
                    sRtnCarno += sTemp[0];
                }

                return sRtnCarno;
            }
            catch
            {
                return sRtnCarno;
            }
        }
        #endregion

        #region Description : 출고화주코드 조회
        private string UP_GetHWAJU(string sHWAJUNM)
        {
            string sHWAJU = string.Empty;
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A8RGK932", sHWAJUNM);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 1)
            {
                sHWAJU = dt.Rows[0]["CODE"].ToString();
            }

            return sHWAJU;
        }
        #endregion

        #region Description : 특허구분 자동 선택
        private string UP_GetJITMGUBN(string sSTTIME, string sJIYYMM)
        {
            string sJITMGUBN = "1";
            string sGUBN = string.Empty;
            double dMMDD;
            double dDay;

            if (sSTTIME.Length == 2)
            {
                // 근태월력 조회 (1 : 토,일,공휴일 , 2 : 평일) 
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_UT_A7OF1817",
                    Get_Date(sJIYYMM.ToString())
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sGUBN = dt.Rows[0]["WEEK"].ToString();
                    dMMDD = Convert.ToDouble(Get_Date(sJIYYMM.ToString()).Substring(4, 4));
                    dDay = Convert.ToDouble(Get_Numeric(sSTTIME.ToString()));

                    if (dDay == 00)
                    {
                        dDay = 24;
                    }

                    if (sGUBN == "1")
                    {
                        if (dDay >= 09 && dDay < 24)   // 특허
                        {
                            sJITMGUBN = "3";
                        }
                        else // 조출
                        {
                            sJITMGUBN = "2";
                        }
                    }
                    else
                    {
                        if (dMMDD >= 0401 && dMMDD <= 1031) // 하절기
                        {
                            if (dDay >= 09 && dDay < 18)    // 일반
                            {
                                sJITMGUBN = "1";
                            }
                            else if (dDay >= 18 && dDay < 24)   // 특허
                            {
                                sJITMGUBN = "3";
                            }
                            else // 조출
                            {
                                sJITMGUBN = "2";
                            }
                        }
                        else // 동절기
                        {
                            if (dDay >= 09 && dDay < 17)    // 일반
                            {
                                sJITMGUBN = "1";
                            }
                            else if (dDay >= 17 && dDay < 24)   // 특허
                            {
                                sJITMGUBN = "3";
                            }
                            else // 조출
                            {
                                sJITMGUBN = "2";
                            }
                        }
                    }
                }
            }

            return sJITMGUBN;
        }
        #endregion

        #region Descriptin : 스프레드 재고선택버튼
        private void FPS91_TY_S_UT_A859I883_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {   
            TYUTGB008S popup = new TYUTGB008S("HHK");

            double dIPJEQTY = 0;

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dIPJEQTY = Convert.ToDouble(popup.fsIPJEQTY);   // 재고량

                if (dIPJEQTY > Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 15].Value.ToString())))
                {
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 3].Value = popup.fsHWAMUL;      // 화물       
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 4].Value = popup.fsYNGUBUN;     // 양수구분   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 7].CellType = new TComboBoxCellType(UP_GET_CHTANKLIST(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL));
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 7].Value = popup.fsTANKNO;      // 출고탱크
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 8].CellType = new TComboBoxCellType(UP_GET_IPTANKLIST(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL));
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 8].Value = UP_GET_JIIPTANK(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL);      // 입고탱크
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 9].Value = UP_GET_JICHJANG(popup.fsTANKNO);      // 출하장
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 25].Value = popup.fsIPHANG;     // 입항일자   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 26].Value = popup.fsBONSUN;     // 본선       
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 27].Value = popup.fsHWAJU;      // 화주       
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 28].Value = popup.fsBLNO;       // BL번호     
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 29].Value = popup.fsMSNSEQ;     // MSN번호    
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 30].Value = popup.fsHSNSEQ;     // HSN번호    
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 31].Value = popup.fsACTHJ;      // 통관화주   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 32].Value = popup.fsCUSTIL;     // 통관일자   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 33].Value = popup.fsCHASU;      // 통관차수   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 34].Value = popup.fsYSHWAJU;    // 양수화주   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 35].Value = popup.fsYDHWAJU;    // 양도화주   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 36].Value = popup.fsYSDATE;     // 양수일자   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 37].Value = popup.fsYDSEQ;      // 양도차수   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 38].Value = popup.fsYSSEQ;      // 양수순번   
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 39].Value = popup.fsIPJANQTY;   // 미통관재고 
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 44].Value = popup.fsIPMTQTY;    // 입고수량
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 45].Value = popup.fsIPPAQTY;    // 통관누계
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 46].Value = popup.fsIPCHQTY;    // 출고누계
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 47].Value = popup.fsIPJEQTY;    // 통관재고
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 49].Value = popup.fsCJJEQTY;    // 재고수량
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 50].Value = dIPJEQTY;    // 예상재고수량

                    // 이전 HSN에 대한 데이터 가져오기
                    this.FPS91_TY_S_UT_A859I883_Sheet1.Cells[e.Row, 48].Value = UP_GET_PreIPMTQTY(popup.fsIPHANG, popup.fsBONSUN, popup.fsHWAJU, popup.fsHWAMUL, popup.fsBLNO, popup.fsMSNSEQ, popup.fsHSNSEQ);
                }
            }
        }
        #endregion
    }
}
