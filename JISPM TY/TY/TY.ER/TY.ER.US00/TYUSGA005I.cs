using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.US00
{
    /// <summary>
    /// 실차 일괄삭제 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.10.24 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9AOIZ420 : 실차 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9AOIZ421 : 실차 일괄삭제
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  CHGOKJONG : 곡종
    ///  CHHWAJU : 화주
    ///  CHCHULDAT : 출고일자
    ///  CHTKNO : TICKET번호
    /// </summary>
    public partial class TYUSGA005I : TYBase
    {
        private string fsWKGUBUN = string.Empty;

        private string fsCHCHULDAT = string.Empty;
        private string fsCHTKNO = string.Empty;
        private string fsCHGEGUNGB = string.Empty;
        private string fsCHNUMBER = string.Empty;
        private string fsCHNUMBER1 = string.Empty;
        private string fsCHHWAJU = string.Empty;
        private string fsCHHWAJUNM = string.Empty;
        private string fsCHGOKJONG = string.Empty;
        private string fsCHHANGCHA = string.Empty;
        private string fsCHWONSAN = string.Empty;
        private string fsCHCUSTIL = string.Empty;
        private string fsCHSEQ = string.Empty;
        private string fsCHBLNO = string.Empty;
        private string fsCHBLMSN = string.Empty;
        private string fsCHBLHSN = string.Empty;
        private string fsCHBINNO = string.Empty;
        private string fsCHYNGUBUN = string.Empty;
        private string fsCHYNHWAJU = string.Empty;
        private string fsCHYNILJA = string.Empty;
        private string fsCHYSSEQ = string.Empty;
        private string fsCHYDSEQ = string.Empty;
        private string fsCHYNCHQTY = string.Empty;
        private string fsTRUNSONG = string.Empty;
        private string fsTRHYUNGT = string.Empty;
        private string fsTGJUNGRY = string.Empty;
        private string fsBTCHTIME = string.Empty;
        private string fsBTCLICK = string.Empty;
        private string fsBTCOUNT = string.Empty;
        private string fsCHIPTIME = string.Empty;
        private string fsCHHITIM = string.Empty;
        private string fsCHCHTIME = string.Empty;
        private string fsCHEMPTY = string.Empty;
        private string fsCHTOTAL = string.Empty;
        private string fsCHMTQTY = string.Empty;
        private string fsJBHWAKQTY = string.Empty;
        private string fsJBCHQTY = string.Empty;
        private string fsJBCSJANQTY = string.Empty;
        private string fsJBCSQTY = string.Empty;
        private string fsJBYDQTY = string.Empty;
        private string fsJBYSQTY = string.Empty;
        private string fsJCCSQTY = string.Empty;
        private string fsJCCHQTY = string.Empty;
        private string fsJCJEGOQTY = string.Empty;
        private string fsJCYSQTY = string.Empty;
        private string fsJCYDQTY = string.Empty;
        private string fsJCYSCHQTY = string.Empty;

        private string fsCHANGE_BIN = string.Empty;
        private string fsPREBINNO = string.Empty;
        private string fsRFID = string.Empty;
        private string fsJCIMCHQTY = string.Empty;

        private string fsJBSOSOK = string.Empty;
        private string fsCHWONHWAJU = string.Empty;
        private string fsCHHMNO1 = string.Empty;
        private string fsCHHMNO2 = string.Empty;
        private string fsCHSIKBAEL = string.Empty;

        private string fsCLINKBIN1 = string.Empty;   // 연결 BIN 1
        private string fsCLINKBIN2 = string.Empty;   // 연결 BIN 2

        // 일별 통관재고
        private double fdJCCHQTY = 0;
        private double fdJCYSCHQTY = 0;
        private double fdJCJEGOQTY = 0;

        // 화주별 재고
        private double fdJGCHQTY = 0;
        private double fdJGYSCHQTY = 0;
        private double fdJGJEGOQTY = 0;
        private double fdJGYSJANQTY = 0;
        private double fdJGJANQTY = 0;

        // 통관파일
        private double fdCSCHQTY = 0;
        private double fdCSJGQTY = 0;
        

        // B/L 별 재고
        private double fdJBCHQTY = 0;
        private double fdJBYSCHQTY = 0;
        private double fdJBJEGOQTY = 0;
        private double fdJBYSJANQTY = 0;
        private double fdJBJANQTY = 0;

        // BIN 상태관리
        private double fdSCHULQTY_M = 0;
        private double fdSJEGOQTY_M = 0;
        private double fdSCHULQTY_L1 = 0;
        private double fdSJEGOQTY_L1 = 0;
        private double fdSCHULQTY_L2 = 0;
        private double fdSJEGOQTY_L2 = 0;

        #region Description : 폼 로드
        public TYUSGA005I()
        {
            InitializeComponent();
        }

        private void TYUSGA005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_CHCHULDAT.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_CHCHULDAT);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AVER451",
                                    Get_Date(this.DTP01_CHCHULDAT.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                    this.TXT01_CHTKNO.GetValue().ToString(),
                                    this.TXT01_CHNUMBER.GetValue().ToString(),
                                    this.CBH01_CHHANGCHA.GetValue().ToString(),
                                    this.CBH01_CHHWAJU.GetValue().ToString(),
                                    this.CBH01_CHGOKJONG.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9AOIZ421.SetValue(dt);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sTKNO = string.Empty;

            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sTKNO = dt.Rows[i]["CHTKNO"].ToString();
                    // 변수 초기화
                    UP_Var_Clear();
                    // 데이터 확인 OK
                    UP_run(dt.Rows[i]["CHCHULDAT"].ToString(),
                            dt.Rows[i]["CHTKNO"].ToString(),
                            dt.Rows[i]["CHNUMBER"].ToString());

                    // 재고량 계산 OK
                    UP_USIJEGOF_SEL();

                    UP_USIJEBLF_SEL();

                    UP_USICUSTF_SEL();  

                    UP_USIJECSNF_SEL();

                    // BIN 상태관리 업데이트 체크 OK (프로시저 변경필요 BIN_STATUSMF1 -> BIN_STATUSMF)
                    UP_BIN_STATUSMF_UPTCHECK("M");
                    if (fsCLINKBIN1 != "")
                    {
                        UP_BIN_STATUSMF_UPTCHECK("L1");
                    }
                    if (fsCLINKBIN2 != "")
                    {
                        UP_BIN_STATUSMF_UPTCHECK("L2");
                    }

                    // ---------------------------------

                    this.DbConnector.CommandClear();

                    // 출고 파일 삭제 처리
                    UP_USICHULF_UPDATE();

                    // BIN 상태관리 업데이트 (프로시저 변경필요 BIN_STATUSMF1 -> BIN_STATUSMF)
                    UP_BIN_STATUSMF_UPDATE("M");
                    if (fsCLINKBIN1 != "")
                    {
                        UP_BIN_STATUSMF_UPDATE("L1");
                    }
                    if (fsCLINKBIN2 != "")
                    {
                        UP_BIN_STATUSMF_UPDATE("L2");
                    }

                    // 재고 업데이트
                    // B/L 별 재고파일 업데이트
                    UP_USIJEBLF_UPDATE();
                    // 통관일별 재고파일 업데이트
                    UP_USIJECSNF_UPDATE();
                    // 재고파일 업데이트
                    UP_USIJEGOF_UPDATE();
                    // 통관파일 업데이트
                    UP_USICUSTF_UPDATE();

                    // 양수도 파일 업데이트
                    if (dt.Rows[i]["CHYNGUBUN"].ToString() == "R")
                    {
                        UP_USIYANGNF_UPDATE();
                    }
                    // 출고 누계 파일 업데이트
                    UP_USICHNUF_UPDATE("UPT");

                    // BIN 입고 파일 업데이트
                    UP_USIBNIPF_UPDATE();

                    this.DbConnector.ExecuteNonQueryList();
                }

                this.ShowMessage("TY_M_GB_23NAD874");

                this.BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowCustomMessage("순번-" + sTKNO + "번 삭제 중 오류가 발생하였습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_9AOIZ421.GetDataSourceInclude(TSpread.TActionType.Select, "CHCHULDAT", "CHTKNO", "CHNUMBER", "CHYNGUBUN");

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

        #region Description : 데이터 확인
        private void UP_run(string sCHCHULDAT, string sCHTKNO, string sCHNUMBER)
        {
            string sIHWONSAN = string.Empty;

            DataTable dt = new DataTable();

            UP_Var_Clear();

            fsCHCHULDAT = sCHCHULDAT;
            fsCHTKNO = sCHTKNO;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AU9Q436",
                                    sCHCHULDAT,
                                    sCHTKNO,
                                    sCHNUMBER);
                                    

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 계근대
                fsCHGEGUNGB = dt.Rows[0]["CHGEGUNGB"].ToString();
                // 차량번호
                fsCHNUMBER = dt.Rows[0]["CHNUMBER"].ToString();
                fsCHNUMBER1 = dt.Rows[0]["CHNUMBER1"].ToString();
                // 화주
                fsCHHWAJU = dt.Rows[0]["CHHWAJU"].ToString();
                // 화주명
                fsCHHWAJUNM = dt.Rows[0]["CHHWAJUNM"].ToString();
                // 곡종
                fsCHGOKJONG = dt.Rows[0]["CHGOKJONG"].ToString();
                // 항차
                fsCHHANGCHA = dt.Rows[0]["CHHANGCHA"].ToString();
                // 원산지
                fsCHWONSAN = dt.Rows[0]["CHWONSAN"].ToString();
                // 통관일자
                fsCHCUSTIL = dt.Rows[0]["CHCUSTIL"].ToString();
                // 통관차수
                fsCHSEQ = dt.Rows[0]["CHSEQ"].ToString();
                // B/L번호
                fsCHBLNO = dt.Rows[0]["CHBLNO"].ToString();
                // MSN
                fsCHBLMSN = Set_Fill4(dt.Rows[0]["CHBLMSN"].ToString());
                // HSN
                fsCHBLHSN = Set_Fill4(dt.Rows[0]["CHBLHSN"].ToString());
                // 화물번호1
                fsCHHMNO1 = dt.Rows[0]["CHHMNO1"].ToString();
                // 화물번호2
                fsCHHMNO2 = dt.Rows[0]["CHHMNO2"].ToString();
                // BIN 번호
                fsPREBINNO = dt.Rows[0]["CHBINNO"].ToString();
                fsCHBINNO = dt.Rows[0]["CHBINNO"].ToString();
                // 양수분 출고
                fsCHYNGUBUN = dt.Rows[0]["CHYNGUBUN"].ToString();
                // 식별자
                fsCHSIKBAEL = dt.Rows[0]["CHSIKBAEL"].ToString();
                // 양도자
                fsCHYNHWAJU = dt.Rows[0]["CHYNHWAJU"].ToString();
                // 양수일자
                if (dt.Rows[0]["CHYNILJA"].ToString() == "0")
                {
                    fsCHYNILJA = "0";
                }
                else
                {
                    fsCHYNILJA = dt.Rows[0]["CHYNILJA"].ToString();
                }
                // 양수순번
                fsCHYSSEQ = dt.Rows[0]["CHYSSEQ"].ToString();
                // 양도순번
                fsCHYDSEQ = dt.Rows[0]["CHYDSEQ"].ToString();
                // 양수 출고량
                fsCHYNCHQTY = dt.Rows[0]["CHYNCHQTY"].ToString();
                //fsPRECHYNCHQTY = dt.Rows[0]["CHYNCHQTY"].ToString();
                // 차량 소속
                fsTRUNSONG = dt.Rows[0]["TRUNSONG"].ToString();
                // 차량 형태
                fsTRHYUNGT = dt.Rows[0]["TRHYUNGT"].ToString();
                // 차량 중량
                fsTGJUNGRY = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[0]["TGJUNGRY"].ToString())));

                if (dt.Rows[0]["BTCHTIME"].ToString() != "0" && dt.Rows[0]["BTCLICK"].ToString() != "0" && dt.Rows[0]["BTCOUNT"].ToString() != "0")
                {
                    // 출고 시간(초)
                    fsBTCHTIME = dt.Rows[0]["BTCHTIME"].ToString();
                    // GATE OPEN
                    fsBTCLICK = Set_Fill2(dt.Rows[0]["BTCLICK"].ToString());
                    fsBTCOUNT = Set_Fill2(dt.Rows[0]["BTCOUNT"].ToString());
                }
                else
                {
                    // 출고 시간(초)
                    fsBTCHTIME = "";
                    // GATE OPEN
                    fsBTCLICK = "00";
                    fsBTCOUNT = Set_Fill2(dt.Rows[0]["TRCOUNT"].ToString());
                }
                // 입문 시간
                fsCHIPTIME = Set_Fill4(dt.Rows[0]["CHIPTIME"].ToString());
                // DB 등록/수정 시간
                fsCHHITIM = dt.Rows[0]["CHHITIM"].ToString();
                // 출문 시간
                fsCHCHTIME = Set_Fill4(dt.Rows[0]["CHCHTIME"].ToString());

                // 원화주
                fsCHWONHWAJU = dt.Rows[0]["CHWONHWAJU"].ToString();

                // 재고량

                // 공차
                fsCHEMPTY = dt.Rows[0]["CHEMPTY"].ToString();
                // 실차
                fsCHTOTAL = dt.Rows[0]["CHTOTAL"].ToString();
                // 실중량
                fsCHMTQTY = dt.Rows[0]["CHMTQTY"].ToString();


                // B/L별 재고 조회
                // 확정량
                fsJBHWAKQTY = string.Format("{0:#,##0.000}", Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString()));
                // 출고량 
                fsJBCHQTY = dt.Rows[0]["JBCHQTY"].ToString();
                // 통관잔량 
                fsJBCSJANQTY = string.Format("{0:#,##0.000}", Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString()));
                // 통관량 
                fsJBCSQTY = dt.Rows[0]["JBCSQTY"].ToString();
                // 양도량 
                fsJBYDQTY = dt.Rows[0]["JBYDQTY"].ToString();
                // 양수량 
                fsJBYSQTY = dt.Rows[0]["JBYSQTY"].ToString();
                // 화물번호1
                fsCHHMNO1 = dt.Rows[0]["JBHMNO1"].ToString();
                // 화물번호2
                fsCHHMNO2 = dt.Rows[0]["JBHMNO2"].ToString();
                // 소속
                fsJBSOSOK = dt.Rows[0]["JBSOSOK"].ToString();

                // 통관재고 조회
                // 통관량
                fsJCCSQTY = dt.Rows[0]["JCCSQTY"].ToString();
                // 출고량 
                fsJCCHQTY = dt.Rows[0]["JCCHQTY"].ToString();
                // 재고량 
                fsJCJEGOQTY = dt.Rows[0]["JCJEGOQTY"].ToString();
                // 양수량 
                fsJCYSQTY = dt.Rows[0]["JCYSQTY"].ToString();
                // 양도량 
                fsJCYDQTY = string.Format("{0:#,##0.000}", Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString()));
                // 양수출고량 
                fsJCYSCHQTY = dt.Rows[0]["JCYSCHQTY"].ToString();
                // 가상출고량 
                fsJCIMCHQTY = dt.Rows[0]["JCIMCHQTY"].ToString();
                // 연결빈1
                fsCLINKBIN1 = dt.Rows[0]["CLINKBIN1"].ToString();
                // 연결빈2
                fsCLINKBIN2 = dt.Rows[0]["CLINKBIN2"].ToString();
            }
        }
        #endregion

        #region Description : 재고 파일(USIJEGOF) 데이터 조회
        private void UP_USIJEGOF_SEL()
        {
            double dJGBEJNQTY = 0;  //  1 = 배정량
            double dJGHWAKQTY = 0;  //  2 = 확정량
            double dJGYDQTY = 0;    //  3 = 양도량
            double dJGYSQTY = 0;    //  4 = 양수량
            double dJGYSYDQTY = 0;  //  5 = 양수분양도량
            double dJGCSQTY = 0;    //  6 = 통관수량
            double dJGCHQTY = 0;    //  7 = 출고수량
            double dJGYSCHQTY = 0;  //  8 = 양수출고량
            double dJGJANQTY = 0;   //  9 = 잔량
            double dJGCSJANQTY = 0; // 10 = 통관잔량
            double dJGYSJANQTY = 0; // 11 = 양수출고잔량
            double dJGJEGOQTY = 0;  // 12 = 재고량

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DDH801",
                fsCHHANGCHA, // 항차
                fsCHGOKJONG, // 곡종
                fsCHHWAJU    // 양도화주
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dJGBEJNQTY = Convert.ToDouble(dt.Rows[0]["JGBEJNQTY"].ToString().Trim());    //   1 = 배정량
                dJGHWAKQTY = Convert.ToDouble(dt.Rows[0]["JGHWAKQTY"].ToString().Trim());    //   2 = 확정량 	 
                dJGYDQTY = Convert.ToDouble(dt.Rows[0]["JGYDQTY"].ToString().Trim());        //   3 = 양도량
                dJGYSQTY = Convert.ToDouble(dt.Rows[0]["JGYSQTY"].ToString().Trim());        //   4 = 양수량
                dJGYSYDQTY = Convert.ToDouble(dt.Rows[0]["JGYSYDQTY"].ToString().Trim());    //   5 = 양수분양도량
                dJGCSQTY = Convert.ToDouble(dt.Rows[0]["JGCSQTY"].ToString().Trim());        //   6 = 통관수량
                dJGCHQTY = Convert.ToDouble(dt.Rows[0]["JGCHQTY"].ToString().Trim());        //   7 = 출고수량
                dJGYSCHQTY = Convert.ToDouble(dt.Rows[0]["JGYSCHQTY"].ToString().Trim());    //   8 = 양수출고량
                dJGJANQTY = Convert.ToDouble(dt.Rows[0]["JGJANQTY"].ToString().Trim());      //   9 = 잔량
                dJGCSJANQTY = Convert.ToDouble(dt.Rows[0]["JGCSJANQTY"].ToString().Trim());  //  10 = 통관잔량       
                dJGYSJANQTY = Convert.ToDouble(dt.Rows[0]["JGYSJANQTY"].ToString().Trim());  //  11 = 양수출고잔량
                dJGJEGOQTY = Convert.ToDouble(dt.Rows[0]["JGJEGOQTY"].ToString().Trim());    //  12 = 재고량

                fdJGJANQTY = 0; // 잔량
                fdJGYSJANQTY = 0; // 양수출고잔량
                fdJGJEGOQTY = 0; // 재고량 

                if (fsCHYNGUBUN == "R")
                {
                    // 양수출고량
                    dJGYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSCHQTY - Convert.ToDouble(Get_Numeric(fsCHYNCHQTY))));
                }
                else
                {
                    // 출고수량
                    dJGCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGCHQTY - Convert.ToDouble(Get_Numeric(fsCHMTQTY))));
                }
                // 양수출고량
                fdJGYSCHQTY = dJGYSCHQTY;
                // 출고량
                fdJGCHQTY = dJGCHQTY;
                // 잔량
                fdJGJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGHWAKQTY - (dJGYDQTY + dJGCHQTY)));
                // 양수출고잔량
                fdJGYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJGYSQTY - (dJGYSCHQTY + dJGYSYDQTY)));
                // 재고량
                fdJGJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}", (dJGHWAKQTY + dJGYSQTY) - (dJGYDQTY + dJGYSYDQTY + dJGCHQTY + dJGYSCHQTY)));
            }
        }
        #endregion

        #region Descriptoin : B/L별 재고파일(USIJEBLF) 데이터 조회
        private void UP_USIJEBLF_SEL()
        {
            double dJBBEJNQTY = 0;  //  0 = 배정량
            double dJBHWAKQTY = 0;  //  1 = 확정량
            double dJBYDQTY = 0;    //  2 = 양도량 
            double dJBYSQTY = 0;    //  3 = 양수량 
            double dJBYSYDQTY = 0;  //  4 = 양수분양도량
            double dJBCSQTY = 0;    //  5 = 통관수량
            double dJBCHQTY = 0;    //  6 = 출고수량
            double dJBYSCHQTY = 0;  //  7 = 양수출고량
            double dJBJANQTY = 0;   //  8 = 잔량
            double dJBCSJANQTY = 0; //  9 = 통관잔량
            double dJBYSJANQTY = 0; // 10 = 양수출고잔량
            double dJBJEGOQTY = 0;  // 11 = 재고량

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96CFW789",
                fsCHHANGCHA, // 항차
                fsCHGOKJONG, // 곡종
                fsCHHWAJU,   // 양도화주
                fsCHBLNO,    // B/L번호
                fsCHBLMSN,   // MSN
                fsCHBLHSN,   // HSN
                fsCHHMNO1,   // 화물번호1
                fsCHHMNO2    // 화물번호2
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dJBBEJNQTY = Convert.ToDouble(dt.Rows[0]["JBBEJNQTY"].ToString().Trim());  //  0 =배정량
                dJBHWAKQTY = Convert.ToDouble(dt.Rows[0]["JBHWAKQTY"].ToString().Trim());  //  1 =확정량 
                dJBYDQTY = Convert.ToDouble(dt.Rows[0]["JBYDQTY"].ToString().Trim());      //  2 =양도량
                dJBYSQTY = Convert.ToDouble(dt.Rows[0]["JBYSQTY"].ToString().Trim());      //  3 =양수량
                dJBYSYDQTY = Convert.ToDouble(dt.Rows[0]["JBYSYDQTY"].ToString().Trim());  //  4 =양수분양도량 
                dJBCSQTY = Convert.ToDouble(dt.Rows[0]["JBCSQTY"].ToString().Trim());      //  5 =통관수량
                dJBCHQTY = Convert.ToDouble(dt.Rows[0]["JBCHQTY"].ToString().Trim());      //  6 =출고수량
                dJBYSCHQTY = Convert.ToDouble(dt.Rows[0]["JBYSCHQTY"].ToString().Trim());  //  7 =양수출고량 
                dJBJANQTY = Convert.ToDouble(dt.Rows[0]["JBJANQTY"].ToString().Trim());    //  8 =잔량 
                dJBCSJANQTY = Convert.ToDouble(dt.Rows[0]["JBCSJANQTY"].ToString().Trim());//  9 =통관잔량 
                dJBYSJANQTY = Convert.ToDouble(dt.Rows[0]["JBYSJANQTY"].ToString().Trim());// 10 =양수출고잔량 
                dJBJEGOQTY = Convert.ToDouble(dt.Rows[0]["JBJEGOQTY"].ToString().Trim());  // 11 =재고량 

                fdJBJANQTY  = 0 ; // 양수출고잔량
				fdJBJEGOQTY = 0 ; // 재고량 

                if (fsCHYNGUBUN == "R")
                {
                    //양수출고량
                    dJBYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}",dJBYSCHQTY - Convert.ToDouble(Get_Numeric(fsCHYNCHQTY))));
                }
                else
                {
                    // 출고량
                    dJBCHQTY = Convert.ToDouble(String.Format("{0,9:N3}",dJBCHQTY - Convert.ToDouble(fsCHMTQTY)));
                }
                // 양수출고량
                fdJBYSCHQTY = dJBYSCHQTY;
                // 출고량
                fdJBCHQTY = dJBCHQTY;
                // 잔량 = 확정량 - (양도량 + 출고량)
                fdJBJANQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJBHWAKQTY - (dJBYDQTY + dJBCHQTY)));
                // 양수출고잔량 = 양수량 - (양수출고량 + 양수분양도량)
                fdJBYSJANQTY = Convert.ToDouble(String.Format("{0,9:N3}",dJBYSQTY - (dJBYSCHQTY + dJBYSYDQTY)));
                // 재고량 = (확정량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdJBJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",(dJBHWAKQTY + dJBYSQTY) - (dJBYDQTY + dJBYSYDQTY + dJBCHQTY + dJBYSCHQTY)));
            }
        }
        #endregion

        #region Description : 통관파일(USICUSTF) 데이터 조회
        private void UP_USICUSTF_SEL()
        {
            double dCSCHQTY = 0;
            double dCSJGQTY = 0;
            double dCSQTY = 0;

            string sCHHWAJU = string.Empty;

            DataTable dt = new DataTable();

            if (fsCHYNGUBUN == "R")
            {
                sCHHWAJU = fsCHYNHWAJU;
            }
            else
            {
                sCHHWAJU = fsCHHWAJU;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_9AEDW326",
                fsCHHANGCHA,    // 1 = 항차
                fsCHGOKJONG,  	// 2 = 곡종
                sCHHWAJU,       // 3 = 양도화주
                fsCHBLNO,       // 4 = Ｂ／Ｌ번호
                fsCHBLMSN,  	// 5 = ＭＳＮ
                fsCHBLHSN,      // 6 = ＨＳＮ
                fsCHCUSTIL,     // 7 = 통관일자
                fsCHSEQ  	    // 8 = 통관차수
                );
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 통관량
                dCSQTY = Convert.ToDouble(dt.Rows[0]["CSQTY"].ToString().Trim());
                // 출고량
                dCSCHQTY = Convert.ToDouble(dt.Rows[0]["CSCHQTY"].ToString().Trim());
                // 재고량
                dCSJGQTY = Convert.ToDouble(dt.Rows[0]["CSJGQTY"].ToString().Trim());

                // 재고량
                fdCSJGQTY = 0;

                if (fsCHYNGUBUN == "R")
                {
                    dCSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", dCSCHQTY - Convert.ToDouble(Get_Numeric(fsCHYNCHQTY))));
                }
                else
                {
                    dCSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", dCSCHQTY - Convert.ToDouble(Get_Numeric(fsCHMTQTY))));
                }

                // 출고량
                fdCSCHQTY = dCSCHQTY;
                // 재고량 = 통관량 - 출고량
                fdCSJGQTY = dCSQTY - fdCSCHQTY;
            }
        }
        #endregion

        #region Description : 통관일별 재고파일(USIJECSNF) 데이터 조회
        private void UP_USIJECSNF_SEL()
        {
            string sJCHWAJU = string.Empty;
            string sJCYDHWAJU = string.Empty;
            string sJCYSDATE = string.Empty;
            string sJCYSSEQ = string.Empty;
            string sJCYDSEQ = string.Empty;

            double dJCYDQTY = 0; // 1 = 양도량
            double dJCYSQTY = 0; // 2 = 양수량
            double dJCCSQTY = 0; // 3 = 통관수량
            double dJCCHQTY = 0; // 4 = 출고수량
            double dJCJEGOQTY = 0; // 5 = 재고량
            double dJCYSYDQTY = 0; // 6 = 양수분양도량
            double dJCYSCHQTY = 0; // 7 = 양수출고량
 
            DataTable dt = new DataTable();

            if(fsCHYNGUBUN == "R")
			{
				sJCHWAJU   = fsCHHWAJU;
				sJCYDHWAJU = fsCHYNHWAJU;
				sJCYSDATE   = fsCHYNILJA;
				sJCYSSEQ   = fsCHYSSEQ;
				sJCYDSEQ   = fsCHYDSEQ;
			}
			else
			{
				sJCHWAJU   = fsCHHWAJU;
				sJCYDHWAJU = "";
				sJCYSDATE   = "0";
				sJCYSSEQ   = "0";
				sJCYDSEQ   = "0";
			}

            // 양도화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96DHQ812", 
                fsCHHANGCHA,    // 항차
                fsCHGOKJONG,    // 곡종
                sJCHWAJU,       // 양수화주
                fsCHBLNO,       // B/L번호
                fsCHBLMSN,      // MSN
                fsCHBLHSN,      // HSN
                fsCHCUSTIL,     // 통관일자
                fsCHSEQ,        // 통관차수
                sJCYDHWAJU,     // 양도화주
                sJCYSDATE,      // 양수일자
                sJCYSSEQ,       // 양수순번
                sJCYDSEQ        // 양도차수
                );
                
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dJCYDQTY = Convert.ToDouble(dt.Rows[0]["JCYDQTY"].ToString().Trim());    // 3  = 양도량
                dJCYSQTY = Convert.ToDouble(dt.Rows[0]["JCYSQTY"].ToString().Trim());    // 4  = 양수량
                dJCCSQTY = Convert.ToDouble(dt.Rows[0]["JCCSQTY"].ToString().Trim());    // 5  = 통관수량
                dJCCHQTY = Convert.ToDouble(dt.Rows[0]["JCCHQTY"].ToString().Trim());    // 6  = 출고수량
                dJCJEGOQTY = Convert.ToDouble(dt.Rows[0]["JCJEGOQTY"].ToString().Trim());  // 7  = 재고량
                dJCYSYDQTY = Convert.ToDouble(dt.Rows[0]["JCYSYDQTY"].ToString().Trim());  // 8  = 양수분양도량
                dJCYSCHQTY = Convert.ToDouble(dt.Rows[0]["JCYSCHQTY"].ToString().Trim());  // 9  = 양수출고량

                // 재고량
                fdJCJEGOQTY = 0;

                if (fsCHYNGUBUN == "R")
                {
                    // 양수출고량
                    dJCYSCHQTY = Convert.ToDouble(String.Format("{0,9:N3}", dJCYSCHQTY - Convert.ToDouble(Get_Numeric(fsCHYNCHQTY))));
                }
                else
                {
                    // 출고량
                    dJCCHQTY = Convert.ToDouble(String.Format("{0,9:N3}",dJCCHQTY - Convert.ToDouble(Get_Numeric(fsCHMTQTY))));
                }
                // 양수출고량
                fdJCYSCHQTY = dJCYSCHQTY;
                // 출고량
                fdJCCHQTY = dJCCHQTY;
                // 재고량 = (통관량 + 양수량) - (양도량 + 양수분양도량 + 출고량 + 양수출고량)
                fdJCJEGOQTY = Convert.ToDouble(String.Format("{0,9:N3}",(dJCCSQTY + dJCYSQTY) - (dJCYDQTY + dJCYSYDQTY + dJCCHQTY + dJCYSCHQTY)));
            }
        }
        #endregion

        #region Description : 출고 파일(USICHULF) 업데이트
        private void UP_USICHULF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9AM8Z400",
                                    fsCHCHULDAT,   // 출고일자
                                    fsCHTKNO,      // 순번
                                    fsCHNUMBER     // 차량번호2
                                    );
        }
        #endregion

        #region Description : 재고파일(USIJEGOF) 업데이트
        private void UP_USIJEGOF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALHS390",
                                    fdJGCHQTY,      // 출고량
                                    fdJGYSCHQTY,    // 양수출고량
                                    fdJGJANQTY,     // 잔량
                                    fdJGYSJANQTY,   // 양수출고잔량
                                    fdJGJEGOQTY,    // 재고량
                                    fsCHHANGCHA,    // 항차
                                    fsCHGOKJONG,    // 곡종
                                    fsCHHWAJU       // 화주
                                    );
        }
        #endregion

        #region Description : B/L별 재고파일(USIJEBLF) 업데이트
        private void UP_USIJEBLF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALH2388",
                                    fdJBCHQTY,      // 출고량
                                    fdJBYSCHQTY,    // 양수출고량
                                    fdJBJANQTY,     // 잔량
                                    fdJBYSJANQTY,   // 양수출고잔량
                                    fdJBJEGOQTY,    // 재고량
                                    fsCHHANGCHA,    // 항차
                                    fsCHGOKJONG,    // 곡종
                                    fsCHHWAJU,      // 화주
                                    fsCHBLNO,       // B/L 번호
                                    fsCHBLMSN,      // MSN
                                    fsCHBLHSN,      // HSN
                                    fsCHHMNO1,      // 화물번호1
                                    fsCHHMNO2       // 화물번호2
                                    );
        }
        #endregion

        #region Description : 통관파일(USICUSTF) 업데이트
        private void UP_USICUSTF_UPDATE()
        {
            string sCSHWAJU = string.Empty;

            if (fsCHYNGUBUN == "R")
            {
                sCSHWAJU = fsCHWONHWAJU;
            }
            else
            {
                sCSHWAJU = fsCHHWAJU;
            }

            this.DbConnector.Attach("TY_P_US_9ALIK391",
                                    fdCSCHQTY,      // 출고량
                                    fdCSJGQTY,      // 재고량
                                    fsCHHANGCHA,    // 항차
                                    fsCHGOKJONG,    // 곡종
                                    sCSHWAJU,       // 화주
                                    fsCHBLNO,       // B/L 번호
                                    fsCHBLMSN,      // MSN
                                    fsCHBLHSN,      // HSN
                                    fsCHCUSTIL,     // 통관일자
                                    fsCHSEQ         // 통관차수
                                    );
        }
        #endregion

        #region Description : 통관일별 재고파일(USIJECSNF) 업데이트
        private void UP_USIJECSNF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9ALH5389",
                                    fdJCCHQTY,      // 출고량
                                    fdJCYSCHQTY,    // 양수출고량
                                    fdJCJEGOQTY,    // 재고량
                                    fsCHHANGCHA,    // 항차
                                    fsCHGOKJONG,    // 곡종
                                    fsCHHWAJU,      // 화주
                                    fsCHBLNO,       // B/L 번호
                                    fsCHBLMSN,      // MSN
                                    fsCHBLHSN,      // HSN
                                    fsCHCUSTIL,     // 통관일자
                                    fsCHSEQ,        // 통관차수
                                    fsCHYNHWAJU,    // 양수화주
                                    fsCHYNILJA,     // 양수일자
                                    fsCHYSSEQ,      // 양수순번
                                    fsCHYDSEQ,      // 양도차수
                                    fsCHWONHWAJU    // 원화주
                                    );
        }
        #endregion

        #region Description : 양수도 파일(USIYANGNF) 업데이트
        private void UP_USIYANGNF_UPDATE()
        {
            this.DbConnector.Attach("TY_P_US_9AUEQ447",
                                    Convert.ToDouble(Get_Numeric(fsCHMTQTY)),  // 출고량
                                    fsCHHANGCHA,// 항차
                                    fsCHGOKJONG,// 곡종
                                    fsCHYNHWAJU,// 양도화주
                                    fsCHBLNO,   // B/L 번호
                                    fsCHBLMSN,  // MSN
                                    fsCHBLHSN,  // HSN
                                    fsCHCUSTIL, // 통관일자
                                    fsCHSEQ,    // 통관차수
                                    fsCHHWAJU,  // 양수화주
                                    fsCHYNILJA, // 양수일자
                                    fsCHYSSEQ,  // 양수순번
                                    fsCHYDSEQ,  // 양도차수
                                    fsCHWONHWAJU// 원화주
                                    );
        }
        #endregion

        #region Description : 출고 누계 파일(USICHNUF) 업데이트
        private void UP_USICHNUF_UPDATE(string sGUBUN)
        {   
            this.DbConnector.Attach("TY_P_US_9ATHE432",
                                    Convert.ToDouble(Get_Numeric(fsCHMTQTY)),                      // 출고량
                                    fsCHCHULDAT.Substring(0, 4),    // 년
                                    fsCHCHULDAT.Substring(4, 2),    // 월
                                    fsCHHWAJU,                      // 출고화주
                                    fsCHGOKJONG                     // 곡종
                                    );
        }
        #endregion

        #region Description : BIN 입고파일(USIBNIPF) 업데이트
        private void UP_USIBNIPF_UPDATE()
        {
            //this.DbConnector.Attach("TY_P_US_9ATHS433",
            //                        Convert.ToDouble(Get_Numeric(fsCHMTQTY)),  // 출고량
            //                        fsCHCHULDAT,// 출고일자
            //                        fsCHGOKJONG,// 곡종
            //                        fsCHBINNO   // BIN 번호
            //                        );

            // 2020-03-04 빈 상태관리의 재고량으로 업데이트(오차 최소화)
            this.DbConnector.Attach("TY_P_US_A34E6005",
                                    fsCHCHULDAT,// 출고일자
                                    fsCHGOKJONG,// 곡종
                                    fsCHBINNO   // BIN 번호
                                    );
        }
        #endregion

        #region Description : BIN 상태관리(BIN_STATUSMF) 업데이트 체크
        private void UP_BIN_STATUSMF_UPTCHECK(string sBINGUBUN)
        {
            double dSJUNILQTY = 0;
            double dSIPGOQTY = 0;
            double dSEPGOQTY = 0;
            double dSEPCHQTY = 0;
            double dCHMTQTY = 0;

            string sBINNO = string.Empty;

            DataTable dt = new DataTable();

            if (sBINGUBUN == "M")
            {
                sBINNO = fsCHBINNO;
            }
            else if (sBINGUBUN == "L1")
            {
                sBINNO = fsCLINKBIN1;
            }
            else if (sBINGUBUN == "L2")
            {
                sBINNO = fsCLINKBIN2;
            }

            dCHMTQTY = Convert.ToDouble(Get_Numeric(fsCHMTQTY));

            // BIN 상태관리 재고량 조회 (BIN_STATUSMF1 -> BIN_STATUSMF 변경해야됨)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9AIEM371",
                                    fsCHCHULDAT,   // 출고일자
                                    sBINNO);  // BIN 번호

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dSJUNILQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SJUNILQTY"].ToString()));
                dSIPGOQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SIPGOQTY"].ToString()));
                dSEPGOQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SEPGOQTY"].ToString()));
                dSEPCHQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SEPCHQTY"].ToString()));

                if (sBINGUBUN == "M")
                {
                    fdSCHULQTY_M = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));
                    // 출고량 = 출고량 - 현재출고량
                    fdSCHULQTY_M = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_M - dCHMTQTY));
                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량)
                    fdSJEGOQTY_M = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_M)));
                    
                }
                else if (sBINGUBUN == "L1")
                {
                    fdSCHULQTY_L1 = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));
                    // 출고량 = 출고량 - 현재출고량
                    fdSCHULQTY_L1 = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_L1 - dCHMTQTY));
                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량 + 현재출고량)
                    fdSJEGOQTY_L1 = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_L1)));
                    
                }
                else if (sBINGUBUN == "L2")
                {
                    fdSCHULQTY_L2 = Convert.ToDouble(Get_Numeric(dt.Rows[0]["SCHULQTY"].ToString()));
                    // 출고량 = 출고량 - 현재출고량
                    fdSCHULQTY_L2 = Convert.ToDouble(String.Format("{0,9:N3}", fdSCHULQTY_L2 - dCHMTQTY));
                    // 재고량 = 전일재고 + (입고량 + 이고입고량) - (이고출고량 + 출고량)
                    fdSJEGOQTY_L2 = Convert.ToDouble(String.Format("{0,9:N3}", dSJUNILQTY + (dSIPGOQTY + dSEPGOQTY) - (dSEPCHQTY + fdSCHULQTY_L2)));
                    
                }
            }
        }
        #endregion

        #region Description : BIN 상태관리(BIN_STATUSMF) 업데이트
        private void UP_BIN_STATUSMF_UPDATE(string sBINGUBUN)
        {
            string sBINNO = string.Empty;
            double dSJEGOQTY = 0;
            double dSCHULQTY = 0;

            // 재고 업데이트
            if (sBINGUBUN == "M")
            {
                sBINNO = fsCHBINNO;
                dSJEGOQTY = fdSJEGOQTY_M;
                dSCHULQTY = fdSCHULQTY_M;
            }
            else if (sBINGUBUN == "L1")
            {
                sBINNO = fsCLINKBIN1;
                dSJEGOQTY = fdSJEGOQTY_L1;
                dSCHULQTY = fdSCHULQTY_L1;
            }
            else if (sBINGUBUN == "L2")
            {
                sBINNO = fsCLINKBIN2;
                dSJEGOQTY = fdSJEGOQTY_L2;
                dSCHULQTY = fdSCHULQTY_L2;
            }

            // (BIN_STATUSMF1 -> BIN_STATUSMF 변경해야됨)
            this.DbConnector.Attach("TY_P_US_9AIFC373", 
                                    dSCHULQTY,      // 출고량
                                    dSJEGOQTY,      // 재고량
                                    dSJEGOQTY,      // 마감재고량
                                    fsCHCHULDAT,    // 출고일자
                                    sBINNO);        // BIN 번호
        }
        #endregion

        #region Descriptoin : 변수 초기화
        private void UP_Var_Clear()
        {
            fsWKGUBUN = string.Empty;

            fsCHCHULDAT = string.Empty;
            fsCHTKNO = string.Empty;
            fsCHGEGUNGB = string.Empty;
            fsCHNUMBER = string.Empty;
            fsCHNUMBER1 = string.Empty;
            fsCHHWAJU = string.Empty;
            fsCHHWAJUNM = string.Empty;
            fsCHGOKJONG = string.Empty;
            fsCHHANGCHA = string.Empty;
            fsCHWONSAN = string.Empty;
            fsCHCUSTIL = string.Empty;
            fsCHSEQ = string.Empty;
            fsCHBLNO = string.Empty;
            fsCHBLMSN = string.Empty;
            fsCHBLHSN = string.Empty;
            fsCHBINNO = string.Empty;
            fsCHYNGUBUN = string.Empty;
            fsCHYNHWAJU = string.Empty;
            fsCHYNILJA = string.Empty;
            fsCHYSSEQ = string.Empty;
            fsCHYDSEQ = string.Empty;
            fsCHYNCHQTY = string.Empty;
            fsTRUNSONG = string.Empty;
            fsTRHYUNGT = string.Empty;
            fsTGJUNGRY = string.Empty;
            fsBTCHTIME = string.Empty;
            fsBTCLICK = string.Empty;
            fsBTCOUNT = string.Empty;
            fsCHIPTIME = string.Empty;
            fsCHHITIM = string.Empty;
            fsCHCHTIME = string.Empty;
            fsCHEMPTY = string.Empty;
            fsCHTOTAL = string.Empty;
            fsCHMTQTY = string.Empty;
            fsJBHWAKQTY = string.Empty;
            fsJBCHQTY = string.Empty;
            fsJBCSJANQTY = string.Empty;
            fsJBCSQTY = string.Empty;
            fsJBYDQTY = string.Empty;
            fsJBYSQTY = string.Empty;
            fsJCCSQTY = string.Empty;
            fsJCCHQTY = string.Empty;
            fsJCJEGOQTY = string.Empty;
            fsJCYSQTY = string.Empty;
            fsJCYDQTY = string.Empty;
            fsJCYSCHQTY = string.Empty;

            fsCHANGE_BIN = string.Empty;
            fsPREBINNO = string.Empty;
            fsRFID = string.Empty;
            fsJCIMCHQTY = string.Empty;

            fsJBSOSOK = string.Empty;
            fsCHWONHWAJU = string.Empty;
            fsCHHMNO1 = string.Empty;
            fsCHHMNO2 = string.Empty;
            fsCHSIKBAEL = string.Empty;

            fsCLINKBIN1 = string.Empty;   // 연결 BIN 1
            fsCLINKBIN2 = string.Empty;   // 연결 BIN 2

            // 일별 통관재고
            fdJCCHQTY = 0;
            fdJCYSCHQTY = 0;
            fdJCJEGOQTY = 0;

            // 화주별 재고
            fdJGCHQTY = 0;
            fdJGYSCHQTY = 0;
            fdJGJEGOQTY = 0;
            fdJGYSJANQTY = 0;
            fdJGJANQTY = 0;

            // 통관파일
            fdCSCHQTY = 0;
            fdCSJGQTY = 0;


            // B/L 별 재고
            fdJBCHQTY = 0;
            fdJBYSCHQTY = 0;
            fdJBJEGOQTY = 0;
            fdJBYSJANQTY = 0;
            fdJBJANQTY = 0;

            // BIN 상태관리
            fdSCHULQTY_M = 0;
            fdSJEGOQTY_M = 0;
            fdSCHULQTY_L1 = 0;
            fdSJEGOQTY_L1 = 0;
            fdSCHULQTY_L2 = 0;
            fdSJEGOQTY_L2 = 0;
        }
        #endregion
    }
}
