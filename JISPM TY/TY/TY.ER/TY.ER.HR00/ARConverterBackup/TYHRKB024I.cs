using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRKB024I : TYBase
    {
        string fsPSSABUN   = string.Empty;
        string fsPSYDATE   = string.Empty;
        string fsPSGUBN    = string.Empty;
        string fsPSWKSDATE = string.Empty;
        string fsPSWKEDATE = string.Empty;
        string fsPSJPNO    = string.Empty;
        string fsPSTYPE    = string.Empty;

        string fsYEAR    = string.Empty;
        string fsMONTH   = string.Empty;
        string fsDAY     = string.Empty;

        string fsForm    = string.Empty;

        string fsPOPUP   = string.Empty;
        double fdBAESU = 0;

        double fdPSAVGTOTAL = 0;
        double fdPSNATIONPAYAMT = 0;
        double fdPSEXLIMITAMT = 0;
        double fdPSEXOVAMT = 0;

        private DataTable dtMaster = new DataTable();

        #region Description : 페이지 로드 
        public TYHRKB024I(string sPSSABUN, string sPSYDATE, string sPSGUBN, string sPSWKSDATE, string sPSWKEDATE, string sPSJPNO, string sPSTYPE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsPSSABUN   = sPSSABUN.ToString();
            fsPSYDATE   = sPSYDATE.ToString();
            fsPSGUBN    = sPSGUBN.ToString();

            fsPSWKSDATE = sPSWKSDATE.ToString();
            fsPSWKEDATE = sPSWKEDATE.ToString();
            fsPSJPNO    = sPSJPNO.ToString();
            fsPSTYPE = sPSTYPE.ToString();
        }

        private void TYHRKB024I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            FPS91_TY_S_HR_BBUH2838.Visible = true;
            

            CBO01_PSTYPE.SetReadOnly(true);

            UP_Spread_Title();
            UP_Spread_Desc();

            this.CBH01_PSSABUN.SetValue(fsPSSABUN);
            this.DTP01_PSYDATE.SetValue(fsPSYDATE);
            this.CBO01_PSGUBN.SetValue(fsPSGUBN);

            this.DTP01_PSWKSDATE.SetValue(fsPSWKSDATE);
            this.DTP01_PSWKEDATE.SetValue(fsPSWKEDATE);

            UP_SelectOrder();

            fsForm = "Load";

            SetStartingFocus(this.CBH01_PSSABUN.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_SAVE();

            UP_AccSave();  //연금계좌번호 저장

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_SelectOrder();
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            TXT01_PSYEAR.SetValue(Set_Fill2(Get_Numeric(TXT01_PSYEAR.GetValue().ToString())));
            TXT01_PSMONTH.SetValue(Set_Fill2(Get_Numeric(TXT01_PSMONTH.GetValue().ToString())));
            TXT01_PSDAY.SetValue(Set_Fill2(Get_Numeric(TXT01_PSDAY.GetValue().ToString())));

            if (Convert.ToInt16(Get_Numeric(TXT01_PSMONTH.GetValue().ToString())) > 12)
            {
                this.ShowCustomMessage("12개월을 초과할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(Get_Numeric(TXT01_PSDAY.GetValue().ToString())) > 31)
            {
                this.ShowCustomMessage("31일을 초과할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            fsYEAR = Set_Fill2(TXT01_PSYEAR.GetValue().ToString());
            fsMONTH = Set_Fill2(TXT01_PSMONTH.GetValue().ToString());
            fsDAY = Set_Fill2(TXT01_PSDAY.GetValue().ToString());

            DataTable dt = new DataTable();

            //// 계좌번호 체크
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_5B3BR081",
            //    this.CBH01_PSSABUN.GetValue(),
            //    this.DTP01_PSYDATE.GetString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.ShowMessage("TY_M_HR_5B3BS083");
            //    e.Successed = false;
            //    return;
            //}

            for (i = 0; i < 19; i++)
            {
                if (i != 3)
                {
                    if (this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 3].Text.ToString() != "" && double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 4].Text.ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_HR_5B6AF110");
                        e.Successed = false;
                        return;
                    }

                    if (this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 3].Text.ToString() == "" && double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 4].Text.ToString())) != 0)
                    {
                        this.ShowMessage("TY_M_HR_5B6AG112");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 근무년수 계산
            //UP_WORKDATE_Compute();

            // 자동계산
            if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            {
                UP_Auto_ComputeDB();
            }
          

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }        
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_SelectOrder()
        {
            //UP_FieldClear();

            UP_Spread_Title();
            UP_Spread_Desc();

            //퇴직금 산출내역 MASTER
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BBUHJ839",
                this.CBH01_PSSABUN.GetValue().ToString(),
                this.DTP01_PSYDATE.GetValue().ToString(),
                this.CBO01_PSGUBN.GetValue().ToString()
                );

            dtMaster = this.DbConnector.ExecuteDataTable();

            CBO01_PSTYPE.SetValue(dtMaster.Rows[0]["PSTYPE"].ToString());
            CBO01_PSBKACGUBN.SetValue(dtMaster.Rows[0]["PSBKACGUBN"].ToString());

            FPS91_TY_S_HR_BBUH2838.Visible = true;

            TXT01_PSYEAR.SetValue(Set_Fill2(dtMaster.Rows[0]["PSYEAR"].ToString()));
            TXT01_PSMONTH.SetValue(Set_Fill2(dtMaster.Rows[0]["PSMONTH"].ToString()));
            TXT01_PSDAY.SetValue(Set_Fill2(dtMaster.Rows[0]["PSDAY"].ToString()));

            CBH01_PSPENBKCODE.SetValue(dtMaster.Rows[0]["PSPENBKCODE"].ToString());
            MTB01_PSPENSAUPNO.SetValue(dtMaster.Rows[0]["PSPENSAUPNO"].ToString());
            TXT01_PSPENACCNUM.SetValue(dtMaster.Rows[0]["PSPENACCNUM"].ToString());

            fsYEAR = TXT01_PSYEAR.GetValue().ToString();
            fsMONTH = TXT01_PSMONTH.GetValue().ToString();
            fsDAY = TXT01_PSDAY.GetValue().ToString();

            fdPSAVGTOTAL = Convert.ToDouble(dtMaster.Rows[0]["PSAVGTOTAL"].ToString());
            fdPSNATIONPAYAMT = Convert.ToDouble(dtMaster.Rows[0]["PSNATIONPAYAMT"].ToString());

            fdPSEXLIMITAMT = Convert.ToDouble(dtMaster.Rows[0]["PSEXLIMITAMT"].ToString());
            fdPSEXOVAMT = Convert.ToDouble(dtMaster.Rows[0]["PSEXOVAMT"].ToString());
            
            //퇴직금 산출내역 DETAIL
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BBUHK840",
                this.CBH01_PSSABUN.GetValue().ToString(),
                this.DTP01_PSYDATE.GetValue().ToString(),
                this.CBO01_PSGUBN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (CBO01_PSTYPE.GetValue().ToString() != "DC")
                {
                    UP_Spread_Dt1_FillDB(dt);
                }

                if (this.FPS91_TY_S_HR_BBUH2838.CurrentRowCount > 0)
                {
                    TButtonCellType tButtonCellType = new TButtonCellType();
                    tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextRightPictLeft;
                    tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                    tButtonCellType.Text = "한도계산";
                    tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.expand4;
                    this.FPS91_TY_S_HR_BBUH2838.ActiveSheet.Cells[26, 3].CellType = tButtonCellType;   
                }             
            }

            // 근무년수 계산
            //UP_WORKDATE_Compute();

            // 자동계산
            if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            {
                UP_Auto_ComputeDB();

                if (dtMaster.Rows.Count > 0)
                {
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(29, 4, dtMaster.Rows[0]["PSINCOMTAX"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(30, 4, dtMaster.Rows[0]["PSJUMINTAX"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(31, 4, dtMaster.Rows[0]["PSEXINCOMTAX"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(32, 4, dtMaster.Rows[0]["PSEXJUMINTAX"].ToString());

                }
            }

            if (fsPSJPNO == "")
            {
                UP_Field_Lock(false);
            }

        }
        #endregion

        #region Description 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            //출력 팝업
            if (this.OpenModalPopup(new TYHRKB24C2(this.CBH01_PSSABUN.GetValue().ToString(), this.DTP01_PSYDATE.GetValue().ToString(), this.CBO01_PSGUBN.GetValue().ToString(), CBO01_PSTYPE.GetValue().ToString(), "01")) == System.Windows.Forms.DialogResult.OK)
            {

            }

            //// 퇴직금 산출 마스타 조회
            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_BBUHJ839",
            //    this.CBH01_PSSABUN.GetValue().ToString(),
            //    this.DTP01_PSYDATE.GetValue().ToString(),
            //    this.CBO01_PSGUBN.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //// 퇴직금 급여 내역 조회
            //DataTable dtM = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_BC1B3847",
            //    this.CBH01_PSSABUN.GetValue().ToString(),
            //    this.DTP01_PSYDATE.GetValue().ToString(),
            //    this.CBO01_PSGUBN.GetValue().ToString(),
            //    "M","","",""
            //    );

            //dtM = this.DbConnector.ExecuteDataTable();

            //// 퇴직금 상여 내역 조회
            //DataTable dtS = new DataTable();

            //this.DbConnector.CommandClear();

            //if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            //{
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_HR_BC1B3847",
            //        this.CBH01_PSSABUN.GetValue().ToString(),
            //        this.DTP01_PSYDATE.GetValue().ToString(),
            //        this.CBO01_PSGUBN.GetValue().ToString(),
            //        "S", "H", "",""
            //        );
            //}
           

            //dtS = this.DbConnector.ExecuteDataTable();

            //// 퇴직금 연차 내역 조회
            //DataTable dtY = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_BC1B3847",
            //    this.CBH01_PSSABUN.GetValue().ToString(),
            //    this.DTP01_PSYDATE.GetValue().ToString(),
            //    this.CBO01_PSGUBN.GetValue().ToString(),
            //    "Y","","",""
            //    );

            //dtY = this.DbConnector.ExecuteDataTable();

            ////배수별 퇴직금 산출내역 
            //DataTable dtBeSu = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_BC192844",
            //    this.CBH01_PSSABUN.GetValue().ToString(),
            //    this.DTP01_PSYDATE.GetValue().ToString(),
            //    this.CBO01_PSGUBN.GetValue().ToString()                
            //    );

            //dtBeSu = this.DbConnector.ExecuteDataTable();

            //if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            //{
            //    ActiveReport rpt = new TYHRKB017R2(dtM, dtS, dtY, dtBeSu);
            //    (new TYERGB001P(rpt, dt)).ShowDialog();
            //}
           
        }
        #endregion

        #region Description : 연금계좌번호 저장
        private void UP_AccSave()
        {
            this.DbConnector.Attach("TY_P_HR_BC3AF871", CBH01_PSPENBKCODE.GetValue(),
                                                            MTB01_PSPENSAUPNO.GetValue().ToString().Replace("-","").Trim(),
                                                            TXT01_PSPENACCNUM.GetValue().ToString().Replace("-", "").Trim(),
                                                            Employer.EmpNo,
                                                            this.CBH01_PSSABUN.GetValue().ToString(),
                                                            this.DTP01_PSYDATE.GetValue().ToString(),
                                                            this.CBO01_PSGUBN.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 퇴직금 저장
        private void UP_SAVE()
        {
            // 퇴직금 산출 TABLE 수정
            this.DbConnector.CommandClear();
            if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            {
                this.DbConnector.Attach("TY_P_HR_5B6BQ114", fsYEAR.ToString(),
                                                            fsMONTH.ToString(),
                                                            fsDAY.ToString(),
                                                            this.DTP01_PSWKSDATE.GetString(),
                                                            this.DTP01_PSWKEDATE.GetString(),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28, 4].Text.ToString()),
                                                            Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29, 4].Text.ToString()),
                                                            Employer.EmpNo,
                                                            this.CBH01_PSSABUN.GetValue().ToString(),
                                                            this.DTP01_PSYDATE.GetValue().ToString(),
                                                            this.CBO01_PSGUBN.GetValue().ToString());
            }            
            this.DbConnector.ExecuteNonQuery();


            // 퇴직금 산출내역 TABLE 수정
            this.DbConnector.CommandClear();

            if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            {
                for (int i = 0; i < 19; i++)
                {
                    if (i != 3)
                    {
                        this.DbConnector.Attach("TY_P_HR_5B6C3115", Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 4].Text.ToString()),
                                                                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 5].Text.ToString(),
                                                                    Employer.EmpNo,
                                                                    this.CBH01_PSSABUN.GetValue().ToString(),
                                                                    this.DTP01_PSYDATE.GetValue().ToString(),
                                                                    this.CBO01_PSGUBN.GetValue().ToString(),
                                                                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 6].Text.ToString());

                    }                   
                }

                this.DbConnector.Attach("TY_P_HR_5B6C3115", Get_Numeric(this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20, 2].Text.ToString()),
                                                              this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20, 5].Text.ToString(),
                                                              Employer.EmpNo,
                                                              this.CBH01_PSSABUN.GetValue().ToString(),
                                                              this.DTP01_PSYDATE.GetValue().ToString(),
                                                              this.CBO01_PSGUBN.GetValue().ToString(),
                                                              this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20, 6].Text.ToString());
            }            

            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 3);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 3].Value = "지급년월";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 4].Value = "금    액";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 5].Value = "비    고";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            
        }
        #endregion

        #region Description : 스프레드 틀 만들기
        private void UP_Spread_Desc()
        {
            
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.ColumnCount = 7;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.RowCount = 34;

            #region Description : 스프레드 틀 만들기
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(0, 0, 22 , 1); // 평균임금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(0, 1, 3 , 2); // 급여
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(22 , 0, 4, 1); // 법정퇴직금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(26, 0, 2, 1); // 임원한도내역          
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(28 , 0, 1, 1); // 국민연금전환금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(29, 0, 4, 1); // 퇴직소득원천세

            //this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(0, 1, 1, 2);
            //this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(1, 1, 1, 2);
            //this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(2, 1, 1, 2);
            //this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(3, 1, 1, 2);

            // 상여 시작
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(4 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(5 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(6 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(7 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(8 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(9 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(10 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(11 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(12 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(13 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(14 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(15 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(16 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(17 , 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(18 , 1, 1, 2);
            // 상여 끝

            // 평균임금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(21 , 1, 1, 2);

            // 법정퇴직금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(22 , 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(23 , 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(24 , 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(25 , 1, 1, 3);

            // 임원한도내금액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(26, 1, 1, 2);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(27, 1, 1, 2);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(26, 3, 2, 1);

            // 국민연금전환금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(28, 1, 1, 3);

            // 퇴직소득원천세
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(29 , 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(30 , 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(31, 1, 1, 3);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(32, 1, 1, 3);


            // 차인지급액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.AddSpanCell(33 , 1, 1, 2);


            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[0, 0].Value = "평균임금";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[22 , 0].Value = "법정퇴직금";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 0].Value = "임원한도내역";
            
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 0].Value = "국민연금전환금";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 0].Value = "소득원천세";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 0].Value = "차인지급액";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[0, 1].Value = "급    여";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 1].Value = "평균급여";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 1].Value = "평균상여";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 1].Value = "년차수당";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 1].Value = "평균임금";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 1].Value = "임원 한도내";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 1].Value = "임원 한도초과";

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 1].Value = "퇴 직 소 득 세";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 1].Value = "퇴 직 주 민 세";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 1].Value = "근 로 소 득 세";
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 1].Value = "근 로 주 민 세";

            // 평균 임금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[0, 0].Font = new Font("굴림", 9, FontStyle.Bold);

            // 평균급여
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 2].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 3].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 2].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[3, 5].BackColor = Color.LightSkyBlue;

            // 평균상여
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 2].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 3].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 2].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[19 , 5].BackColor = Color.LightSkyBlue;

            // 년차수당
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 2].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 3].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 2].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20 , 5].BackColor = Color.LightSkyBlue;

            // 평균임금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[21 , 5].BackColor = Color.LightSkyBlue;

            // 법정퇴직금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[22 , 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 2].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[25 , 5].BackColor = Color.LightSkyBlue;

            // 임원한도내금액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 5].BackColor = Color.LightSkyBlue;


            // 임원한도초과액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[27, 5].BackColor = Color.LightSkyBlue;


            // 국민연금전환금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[28 , 5].BackColor = Color.LightSkyBlue;

            // 퇴직소득원천세
            // 소득세
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29 , 5].BackColor = Color.LightSkyBlue;

            // 주민세
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30 , 5].BackColor = Color.LightSkyBlue;

            // 근로소득세
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 5].BackColor = Color.LightSkyBlue;

            // 근로주민세
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 1].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 5].BackColor = Color.LightSkyBlue;

            // 차인지급액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 0].Font = new Font("굴림", 9, FontStyle.Bold);
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 4].Font = new Font("굴림", 9, FontStyle.Bold);

            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 1].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 3].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 4].BackColor = Color.LightSkyBlue;
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[33 , 5].BackColor = Color.LightSkyBlue;
            #endregion                                           
        }
        #endregion

        #region Description : 급여 및 상여, 년차 값 가져오기(DB형)
        private void UP_Spread_Dt1_FillDB(DataTable dt)
        {
            DataTable dz = new DataTable();

            int i = 0;
            int iRow = 0;

            // 급여
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PKPAYGUBN"].ToString() == "M1")
                {
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 3].Value = string.Format("{0:###0}", dt.Rows[i]["PKPAYYYMM"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 4].Value = string.Format("{0:###0}", dt.Rows[i]["PKPAYAMOUNT"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 5].Value = string.Format("{0:###0}", dt.Rows[i]["PKMEMO"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 6].Value = string.Format("{0:###0}", dt.Rows[i]["PKSEQ"].ToString());

                    iRow++;
                }
            }

            string sGUBUN = string.Empty;

            iRow = 4;
            
            // 상여
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PKPAYGUBN"].ToString() != "M1" && dt.Rows[i]["PKPAYGUBN"].ToString().Substring(0,1) != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_HR_5B5GG105",
                        dt.Rows[i]["PKPAYGUBN"].ToString()
                        );

                    dz = this.DbConnector.ExecuteDataTable();

                    if (dz.Rows.Count > 0)
                    {
                        sGUBUN = dz.Rows[0]["CDDESC1"].ToString();
                    }

                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 1].Value = sGUBUN.ToString();
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 3].Value = string.Format("{0:###0}", dt.Rows[i]["PKPAYYYMM"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 4].Value = string.Format("{0:###0}", dt.Rows[i]["PKPAYAMOUNT"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 5].Value = string.Format("{0:###0}", dt.Rows[i]["PKMEMO"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 6].Value = string.Format("{0:###0}", dt.Rows[i]["PKSEQ"].ToString());

                    iRow++;
                }
            }

            // 년차
            iRow = 20;
            
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PKPAYGUBN"].ToString().Substring(0, 1) == "Y")
                {
                    string sAMT = "";

                    double dAMT = 0;
                    dAMT = double.Parse(string.Format("{0:###0}", dt.Rows[i]["PKPAYAMOUNT"].ToString())) / 12;
                    dAMT = dAMT / 10;

                    sAMT = UP_DotDelete(Convert.ToString(dAMT));

                    dAMT = double.Parse(sAMT) * 10;

                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 2].Value = string.Format("{0:###0}", dt.Rows[i]["PKPAYAMOUNT"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 3].Value = " / 12";
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 4].Value = string.Format("{0:###0}", dAMT);
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 5].Value = string.Format("{0:###0}", dt.Rows[i]["PKMEMO"].ToString());
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[iRow, 6].Value = string.Format("{0:###0}", dt.Rows[i]["PKSEQ"].ToString());

                    iRow++;
                }
            }
        }
        #endregion

        
        #region Description : 특정 Row와 Column 값 변경(DB형)
        private void UP_Auto_ComputeDB()
        {
            DataTable dt = new DataTable();

            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_BBUH2838, "TITLE1", "합 계", Color.LightSkyBlue);

            double dAMT = 0;

            // 평균급여
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetFormula(
                3,
                2,
                "R[-3]C[2] + R[-2]C[2] + R[-1]C[2]");

            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838.GetValue(3, "GUBUN3").ToString()));
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(3, 3, " / 3");
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(3, 4, UP_COMP(dAMT, 3));

            // 평균상여
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetFormula(
                19,
                2,
                "R[-15]C[2] + R[-14]C[2] + R[-13]C[2] + R[-12]C[2] + R[-11]C[2] + R[-10]C[2] + R[-9]C[2] + R[-8]C[2] + R[-7]C[2] + R[-6]C[2] + R[-5]C[2] + R[-4]C[2] + R[-3]C[2] + R[-2]C[2] + R[-1]C[2]");

            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838.GetValue(19, "GUBUN3").ToString()));
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(19, 3, " / 12");
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(19, 4, UP_COMP(dAMT, 12));

            // 년차
            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838.GetValue(20, "GUBUN3").ToString()));
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(20, 3, " / 12");
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(20, 4, UP_COMP(dAMT, 12));

            // 평균임금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetFormula(
                21,
                4,
                "R[-18]C[0] + R[-2]C[0] + R[-1]C[0]");

            //법정퇴직금
            dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838.GetValue(21, "AMT").ToString()));

            
            string sTextStrY = "";
            string sTextStrM = "";
            string sTextStrD = "";
            string sBAESU = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                  "TY_P_HR_BC192844",
                 this.CBH01_PSSABUN.GetValue(),
                 DTP01_PSYDATE.GetString().ToString(),
                 CBO01_PSGUBN.GetValue()
                );
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sTextStrY = Convert.ToInt16(dt.Rows[i]["PXYEAR"].ToString()) > 0 ? dt.Rows[i]["PXYEAR"].ToString() + "년 " : "";
                    sTextStrM = Convert.ToInt16(dt.Rows[i]["PXMONTH"].ToString()) > 0 ? dt.Rows[i]["PXMONTH"].ToString() + "개월 " : "";
                    sTextStrD = Convert.ToInt16(dt.Rows[i]["PXDAY"].ToString()) > 0 ? dt.Rows[i]["PXDAY"].ToString() + "일 " : "";

                    sBAESU = Convert.ToDouble(dt.Rows[i]["PXRATENUM"].ToString()) > 0 ? dt.Rows[i]["PXRATENUM"].ToString() + " 배수 " : "";

                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22 + i, 1, string.Format("{0:#,##0}", fdPSAVGTOTAL) + " X " + sTextStrY + sTextStrM + sTextStrD + " X " + sBAESU);
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22 + i, 4, string.Format("{0:#,##0}", Convert.ToDouble(dt.Rows[i]["PXREAMOUNT"].ToString())));
                    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22 + i, 5, Set_Date(dt.Rows[i]["PXOVSDATE"].ToString()) + " ~ " + Set_Date(dt.Rows[i]["PXOVEDATE"].ToString()));
                }
            }

            //// 년
            //if (dBAESU == 1)
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsYEAR.ToString());
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsYEAR.ToString() + " X " + Convert.ToString(dBAESU));
            //}

            //if (fsYEAR != "0")
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22, 4, UP_COMP(dAMT * double.Parse(fsYEAR) * dBAESU, 0));
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(22, 4, 0);
            //}

            //// 월
            //if (dBAESU == 1)
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(23, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsMONTH.ToString() + " / 12");
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(23, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsMONTH.ToString() + " X " + Convert.ToString(dBAESU) + " / 12");
            //}

            //if (fsMONTH != "0")
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(23, 4, UP_COMP(dAMT * double.Parse(fsMONTH) * dBAESU, 12));
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(23, 4, 0);
            //}

            //// 일
            //if (dBAESU == 1)
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsDAY.ToString() + " / 365");
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 1, string.Format("{0:#,##0}", dAMT) + " X " + fsDAY.ToString() + " X " + Convert.ToString(dBAESU) + " / 365");
            //}

            ////if (fsDAY!= "0")
            ////{
            ////    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 4, UP_COMP(dAMT * double.Parse(fsDAY) * dBAESU, 4380));
            ////}
            ////else
            ////{
            ////    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 4, 0);
            ////}

            //if (fsDAY != "0")
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 4, UP_COMP(dAMT * double.Parse(fsDAY) * dBAESU, 365));
            //}
            //else
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(24, 4, 0);
            //}

            // 법정퇴직금 계산
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetFormula(
                25,
                4,
                "R[-3]C[0] + R[-2]C[0] + R[-1]C[0]");

            //// 소득세 및 주민세
            //dAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_BBUH2838.GetValue(25, "AMT").ToString()));

            //string sKBIDATE = string.Empty;

            //// 입사일 또는 중간정산일 가져오기
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_5B9FQ133",
            //    this.CBH01_PSSABUN.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    sKBIDATE = dt.Rows[0]["KBIDATE"].ToString();
            //}

            ///*
            //string sSTYYMM = string.Empty;
            //string sEDYYMM = string.Empty;

            //sSTYYMM = "199301";
            //sEDYYMM = "199903";

            //if (int.Parse(sKBIDATE) > 199301)
            //{
            //    sSTYYMM = sKBIDATE;
            //}

            //// 국민연금전환금
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_HR_5B9FK132",
            //    sSTYYMM.ToString().Substring(0,6),
            //    sEDYYMM.ToString().Substring(0,6),
            //    this.CBH01_PSSABUN.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(26, 4, dt.Rows[0]["PNNYUNGUM"].ToString());
            //} */


            // 임원한도내금액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(26, 4, fdPSEXLIMITAMT.ToString());

            // 임원한도초과액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(27, 4, fdPSEXOVAMT.ToString());


            // 국민연금전환금
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(28, 4, fdPSNATIONPAYAMT.ToString());

            //소득세 주민세
            //if (dtMaster.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(27, 4, dtMaster.Rows[0]["PSINCOMTAX"].ToString());
            //    this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(28, 4, dtMaster.Rows[0]["PSJUMINTAX"].ToString());
            //}
            //else
            //{
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_HR_5B5FZ104",
            //        this.DTP01_PSWKSDATE.GetString(),
            //        this.DTP01_PSWKEDATE.GetString(),
            //        Convert.ToString(dAMT)
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    if (dt.Rows.Count > 0)
            //    {
            //        this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(27, 4, dt.Rows[0]["AMT"].ToString());
            //        this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetValue(28, 4, dt.Rows[0]["VAT"].ToString());
            //    }
            //}

            // 차인지급액
            this.FPS91_TY_S_HR_BBUH2838_Sheet1.SetFormula(
                33,
                4,
                "R[-8]C[0] - R[-5]C[0] - R[-4]C[0] - R[-3]C[0]- R[-2]C[0] - R[-1]C[0]");

            this.FPS91_TY_S_HR_BBUH2838.ActiveSheet.Rows[FPS91_TY_S_HR_BBUH2838.CurrentRowCount - 1].Visible = false;
        }
        #endregion

              

        #region Description : 계산 및 원단위 절사
        private double UP_COMP(double dAMOUNT, int iDiv)
        {
            string sAMT = string.Empty;
            double dAMT = 0;

            if (iDiv != 0)
            {
                dAMT = double.Parse(string.Format("{0:###0}", dAMOUNT)) / iDiv;
                dAMT = dAMT / 10;

                sAMT = UP_DotDelete(Convert.ToString(dAMT));

                dAMT = double.Parse(sAMT) * 10;
            }
            else
            {
                dAMT = dAMOUNT;
            }

            return dAMT;
        }
        #endregion

        #region Description : 근무년수 계산
        private void DTP01_PSWKSDATE_ValueChanged(object sender, EventArgs e)
        {
            UP_WORKDATE_Compute();

            if (fsForm != "")
            {
                if (CBO01_PSTYPE.GetValue().ToString() != "DC")
                {
                    UP_Auto_ComputeDB();
                }
               
            }
        }

        private void DTP01_PSWKEDATE_ValueChanged(object sender, EventArgs e)
        {
            UP_WORKDATE_Compute();

            if (fsForm != "")
            {
                if (CBO01_PSTYPE.GetValue().ToString() != "DC")
                {
                    UP_Auto_ComputeDB();
                }
               
            }
        }

        private void UP_WORKDATE_Compute()
        {
            fsYEAR  = "";
            fsMONTH = "";
            fsDAY   = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_5B5F0100",
                this.DTP01_PSWKSDATE.GetString(),
                this.DTP01_PSWKEDATE.GetString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsYEAR  = dt.Rows[0]["YEAR"].ToString();
                fsMONTH = dt.Rows[0]["MONTH"].ToString();
                fsDAY   = dt.Rows[0]["DAY"].ToString();
            }

            LBL51_WORKDATE.Text = "(" + fsYEAR + "년" + fsMONTH + "월" + fsDAY + "일" + ")";
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_BBUH2838_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (fsForm != "")
            {
                if (CBO01_PSTYPE.GetValue().ToString() != "DC")
                {
                    UP_Auto_ComputeDB();
                }
               
            }
        }

        private void FPS91_TY_S_HR_BBUH2838_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (fsForm != "")
            {
                if (CBO01_PSTYPE.GetValue().ToString() != "DC")
                {
                    UP_Auto_ComputeDB();
                }
               
            }
        }

        
        #endregion

        #region Description : 전표번호 존재시 필드 잠금
        private void UP_Field_Lock(bool fLock)
        {
            int i = 0;

            if (CBO01_PSTYPE.GetValue().ToString() != "DC")
            {
                for (i = 0; i < 19; i++)
                {
                    if (i != 3)
                    {
                        if (FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 3].Text != "")
                        {
                            //this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 3].Locked = fLock;
                            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 4].Locked = fLock;
                            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 5].Locked = fLock;

                            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 4].BackColor = Color.LightBlue;
                            this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[i, 5].BackColor = Color.LightBlue;
                        }
                    }
                }

                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[20, 2].Locked = fLock;

                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[26, 3].Locked = fLock;                

                //소득세, 주민세
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29, 4].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[29, 5].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30, 4].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[30, 5].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 4].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[31, 5].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 4].Locked = fLock;
                this.FPS91_TY_S_HR_BBUH2838_Sheet1.Cells[32, 5].Locked = fLock;

            }
           

        }
        #endregion

        #region Description : FPS91_TY_S_HR_BBUH2838_ButtonClicked 이벤트
        private void FPS91_TY_S_HR_BBUH2838_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "3")
            {
                this.OpenModalPopup(new TYHRKB24C1( CBH01_PSSABUN.GetValue().ToString(),
                                                    DTP01_PSYDATE.GetString().ToString(),
                                                    CBO01_PSGUBN.GetValue().ToString()
                                                    ));
                    
            }
        }
        #endregion

    }
}