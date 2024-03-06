using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금수지 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.19 09:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27J12118 : 자금수지관리 삭제
    ///  TY_P_AC_27J16115 : 자금수지관리 조회
    ///  TY_P_AC_27J17116 : 자금수지관리 등록
    ///  TY_P_AC_27J18117 : 자금수지관리 수정
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27J14119 : 자금수지관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_AC_27J4T125 : 항목명을 입력하세요.
    ///  TY_M_AC_27J4T124 : LEVEL을 입력하세요.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EIFYYMM : 년월
    /// </summary>
    public partial class TYACPB001I : TYBase
    {
        #region Description : 페이지 로드
        public TYACPB001I()
        {
            InitializeComponent();
        }

        private void TYACPB001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.DTP01_EIFYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27J16115",
                this.DTP01_EIFYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_27J14119.SetValue(dt);
                }
                else
                {
                    this.FPS91_TY_S_AC_27J14119.SetValue(UP_NewRowAdd(dt));
                }
            }
            else
            {
                this.FPS91_TY_S_AC_27J14119.SetValue(UP_NewRowAdd(dt));
            }

            // 특정 ROW 잠금
            for(int i = 0; i < 14; i++)
            {
                if (i == 1 || i == 5 || i == 11 || i == 13) // 전월이월 등록 가능하게 수정 i == 0 ||
                {
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_AC_27J14119.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;

                    this.FPS91_TY_S_AC_27J14119.ActiveSheet.Rows[i].Locked = true;
                }
            }

            UP_SumRowAdd();

            this.FPS91_TY_S_AC_27J14119.Focus();
        }
        #endregion

        #region Description : 조회 메소드
        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            string sEIFSEQN = string.Empty;
            string sEIFTINM = string.Empty;
            string sEIFLEVE = string.Empty;

            for (int i = 0; i < 14; i++)
            {
                rw = Rowdt.NewRow();

                if (i == 0)
                {
                    sEIFSEQN = "00";
                    sEIFTINM = "전월이월";
                    sEIFLEVE = "1";
                }
                else if (i == 1)
                {
                    sEIFSEQN = "10";
                    sEIFTINM = "수입합계";
                    sEIFLEVE = "2";
                }
                else if (i == 2)
                {
                    sEIFSEQN = "11";
                    sEIFTINM = "매출수입";
                    sEIFLEVE = "3";
                }
                else if (i == 3)
                {
                    sEIFSEQN = "12";
                    sEIFTINM = "구매자금대출";
                    sEIFLEVE = "3";
                }
                else if (i == 4)
                {
                    sEIFSEQN = "13";
                    sEIFTINM = "기타수입";
                    sEIFLEVE = "3";
                }
                else if (i == 5)
                {
                    sEIFSEQN = "50";
                    sEIFTINM = "지출합계";
                    sEIFLEVE = "2";
                }
                else if (i == 6)
                {
                    sEIFSEQN = "51";
                    sEIFTINM = "상품구매";
                    sEIFLEVE = "3";
                }
                else if (i == 7)
                {
                    sEIFSEQN = "52";
                    sEIFTINM = "운영비용";
                    sEIFLEVE = "3";
                }
                else if (i == 8)
                {
                    sEIFSEQN = "53";
                    sEIFTINM = "차입금상환";
                    sEIFLEVE = "3";
                }
                else if (i == 9)
                {
                    sEIFSEQN = "54";
                    sEIFTINM = "시설투자";
                    sEIFLEVE = "3";
                }
                else if (i == 10)
                {
                    sEIFSEQN = "55";
                    sEIFTINM = "기타지출";
                    sEIFLEVE = "3";
                }
                else if (i == 11)
                {
                    sEIFSEQN = "90";
                    sEIFTINM = "과부족";
                    sEIFLEVE = "2";
                }
                else if (i == 12)
                {
                    sEIFSEQN = "95";
                    sEIFTINM = "자금조달";
                    sEIFLEVE = "2";
                }
                else if (i == 13)
                {
                    sEIFSEQN = "99";
                    sEIFTINM = "차월이월";
                    sEIFLEVE = "1";
                }

                rw["EIFSEQN"] = sEIFSEQN.ToString();
                rw["EIFTINM"] = sEIFTINM.ToString();
                rw["EIFLEVE"] = sEIFLEVE.ToString();
                rw["EIFSEMM"] = 0;
                rw["EIFSAMM"] = 0;
                rw["EIFNEMM"] = 0;
                
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 삭제
            this.DbConnector.Attach
                (
                "TY_P_AC_27J12118",
                this.DTP01_EIFYYMM.GetValue()
                );

            for (int i = 0; i < 14; i++)
            {
                this.DbConnector.Attach("TY_P_AC_27J17116", this.DTP01_EIFYYMM.GetValue(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFSEQN").ToString(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFTINM").ToString(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFLEVE").ToString(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFSEMM").ToString(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFSAMM").ToString(),
                                                            this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFNEMM").ToString(),
                                                            TYUserInfo.EmpNo); // 저장
            }

            this.DbConnector.ExecuteNonQueryList();

            // 저장 메세지
            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27J12118",
                this.DTP01_EIFYYMM.GetValue()
                );

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_27H64059",
                this.DTP01_EIFYYMM.GetValue().ToString().Substring(0, 4),
                this.DTP01_EIFYYMM.GetValue().ToString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["ECGUBUN"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFTINM").ToString() != "")
                {
                    if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFLEVE").ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27J4T124");
                        e.Successed = false;
                        return;
                    }
                }

                if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFLEVE").ToString() != "")
                {
                    if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFTINM").ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27J4T125");
                        e.Successed = false;
                        return;
                    }
                }

                if (double.Parse(Get_Numeric(this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFSEMM").ToString())) != 0 || 
                    double.Parse(Get_Numeric(this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFSAMM").ToString())) != 0 ||
                    double.Parse(Get_Numeric(this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFNEMM").ToString())) != 0)
                {
                    if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFTINM").ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27J4T125");
                        e.Successed = false;
                        return;
                    }

                    if (this.FPS91_TY_S_AC_27J14119.GetValue(i, "EIFLEVE").ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27J4T124");
                        e.Successed = false;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_27H64059",
                this.DTP01_EIFYYMM.GetValue().ToString().Substring(0, 4),
                this.DTP01_EIFYYMM.GetValue().ToString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["ECGUBUN"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_27J14119, "EIFTINM", "합 계", Color.Yellow);

            #region Description : 당월 예상액

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                1,
                3,
                "R[1]C[0] + R[2]C[0] + R[3]C[0]"); // 수입

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                5,
                3,
                "R[1]C[0] + R[2]C[0] + R[3]C[0] + R[4]C[0] + R[5]C[0]"); // 지출

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                11,
                3,
                "R[-10]C[0] - R[-6]C[0]"); // 과부족

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                13,
                3,
                "R[-13]C[0] + R[-12]C[0] - R[-8]C[0] + R[-1]C[0]"); // 차월이월

            #endregion

            #region Description : 당월 실적액

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                1,
                4,
                "R[1]C[0] + R[2]C[0] + R[3]C[0]"); // 수입

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                5,
                4,
                "R[1]C[0] + R[2]C[0] + R[3]C[0] + R[4]C[0] + R[5]C[0]"); // 지출

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                11,
                4,
                "R[-10]C[0] - R[-6]C[0]"); // 과부족

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                13,
                4,
                "R[-13]C[0] + R[-12]C[0] - R[-8]C[0] + R[-1]C[0]"); // 차월이월

            #endregion

            #region Description : 익월 실적액

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                1,
                5,
                "R[1]C[0] + R[2]C[0] + R[3]C[0]"); // 수입

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                5,
                5,
                "R[1]C[0] + R[2]C[0] + R[3]C[0] + R[4]C[0] + R[5]C[0]"); // 지출

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                11,
                5,
                "R[-10]C[0] - R[-6]C[0]"); // 과부족

            this.FPS91_TY_S_AC_27J14119_Sheet1.SetFormula(
                13,
                5,
                "R[-13]C[0] + R[-12]C[0] - R[-8]C[0] + R[-1]C[0]"); // 차월이월

            this.FPS91_TY_S_AC_27J14119.ActiveSheet.Rows[14].Visible = false;

            #endregion
        }
        #endregion
    }
}