using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

using System.Data.OleDb;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 엑셀자료 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.11.12 15:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3962F585 : EIS 계열사 엑셀자료 처리(계획_경영실적-SP)
    ///  TY_P_AC_3A12K936 : EIS 계열사 자금수지 등록
    ///  TY_P_AC_3A13T941 : EIS 계열사 자금수지 삭제
    ///  TY_P_AC_3A15M944 : EIS 계열사 엑셀자료 처리(티와이스틸 매출-2차)
    ///  TY_P_AC_3A15N945 : EIS 계열사 엑셀자료 삭제처리(티와이스틸 매출-2차)
    ///  TY_P_AC_3AH6D076 : EIS 계열사 엑셀자료 삭제처리(차입금세부)
    ///  TY_P_AC_3AH6E077 : EIS 계열사 엑셀자료 등록처리(차입금세부)
    ///  TY_P_AC_3AUAN135 : EIS 계열사 엑셀자료 삭제처리(GLS 매출)
    ///  TY_P_AC_3AUAP136 : EIS 계열사 엑셀자료 등록처리(GLS매출)
    ///  TY_P_AC_3B4BT185 : EIS 계열사 엑셀자료 처리(계획_경영실적-SP-TYGT)
    ///  TY_P_AC_3BB31246 : EIS 계열사 엑셀자료 등록처리(자금수지계획)
    ///  TY_P_AC_3BB36245 : EIS 계열사 엑셀자료 삭제처리(자금수지계획)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39633586 : EIS 계열사 엑셀 손익자료-업데이트
    ///  TY_S_AC_39635587 : EIS 계열사 엑셀 재무상태표자료-업데이트
    ///  TY_S_AC_3A159942 : EIS 계열사 엑셀 티와이스틸-업데이트
    ///  TY_S_AC_3AH5T074 : EIS 계열사 차입금 세부-업데이트
    ///  TY_S_AC_3AUA9134 : EIS 계열사 엑셀 GLS매출자료-업데이트
    ///  TY_S_AC_3BB34247 : EIS 계열사 자금수지 계획 -업데이트
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_31B1C623 : EXCEL 업데이트 할 파일을 선택하세요.
    ///  TY_M_AC_31BAP617 : EXCEL 업데이트가 완료 되었습니다.
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  EXCEL : 엑셀 업데이트
    ///  INQ : 조회
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    ///  ESPLCMPY : 계열사구분
    ///  EPLCGB : 손익생성
    /// </summary>
    public partial class TYAFMA012S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYAFMA012S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYAFMA012S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_EXCEL.ProcessCheck += new TButton.CheckHandler(BTN61_EXCEL_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_GSTYYMM.Visible = false;
            this.LBL51_GSTYYMM.Visible = false;

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "TL";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESPLCMPY.SetValue(fsCompanyCode);
                this.CBH01_ESPLCMPY.SetReadOnly(true);

                this.SetStartingFocus(this.CBO01_EPLCGB);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESPLCMPY.CodeText);
            }

            this.FPS91_TY_S_AC_39633586.Visible = true;  // 손익자료 (P)
            this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표 (B)
            this.FPS91_TY_S_AC_3BB34247.Visible = false; // 자금계획 (R)
            this.FPS91_TY_S_AC_3A159942.Visible = false; // 티와이스틸 매출자료 (M) --사용안함
            this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 차입금 세부 (C)
            this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 티와이스틸 매출 (G)

        }
        #endregion

        #region Description : 엑셀 올리기
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            if (this.CBO01_EPLCGB.GetValue().ToString() == "P")  //계획_경영실적
            {
                if (this.FPS91_TY_S_AC_39633586.ActiveSheet.RowCount != 0)
                {
                    this.DbConnector.CommandClear();
                    if (CBH01_ESPLCMPY.GetValue().ToString() != "TG")
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_39633586.ActiveSheet.RowCount; i++)
                        {
                            this.DbConnector.Attach("TY_P_AC_3962F585", "P",
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCUST").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMYYHD").ToString().Trim().Substring(0, 4),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMYYHD").ToString().Trim().Substring(4, 2),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCDAC").ToString(),
                                //  this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCDNM").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMPL").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMUS").ToString(),
                                                    sOUTMSG.ToString()
                                                    );

                            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        }
                    }
                    else  // 그레인
                    {
                        this.DbConnector.CommandClear();
                        for (int i = 0; i < this.FPS91_TY_S_AC_39633586.ActiveSheet.RowCount; i++)
                        {
                            this.DbConnector.Attach("TY_P_AC_3B4BT185", "P",
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCUST").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMYYHD").ToString().Trim().Substring(0, 4),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMYYHD").ToString().Trim().Substring(4, 2),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCDAC").ToString(),
                                //  this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMCDNM").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMPL").ToString(),
                                                    this.FPS91_TY_S_AC_39633586.GetValue(i, "ESMUS").ToString(),
                                                    sOUTMSG.ToString()
                                                    );

                            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        }
                    }

                    if (sOUTMSG.Substring(0, 2) != "OK")
                    {
                        return;
                    }

                    this.ShowMessage("TY_M_AC_31BAP617");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_31B1C623");
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "B") // 재무상태표
            {
                if (this.FPS91_TY_S_AC_39635587.ActiveSheet.RowCount != 0)
                {
                    this.DbConnector.CommandClear();

                    if (CBH01_ESPLCMPY.GetValue().ToString() != "TG")
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_39635587.ActiveSheet.RowCount; i++)
                        {
                            this.DbConnector.Attach("TY_P_AC_3962F585", "B",
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCUST").ToString(),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMYYHD").ToString().Trim().Substring(0, 4),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMYYHD").ToString().Trim().Substring(4, 2),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCDAC").ToString(),
                                //  this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCDNM").ToString(),
                                                    "0",
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMUS").ToString(),
                                                    sOUTMSG.ToString()
                                                    );

                            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        }
                    }
                    else // 그레인
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_39635587.ActiveSheet.RowCount; i++)
                        {
                            this.DbConnector.Attach("TY_P_AC_3B4BT185", "B",
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCUST").ToString(),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMYYHD").ToString().Trim().Substring(0, 4),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMYYHD").ToString().Trim().Substring(4, 2),
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCDAC").ToString(),
                                //  this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMCDNM").ToString(),
                                                    "0",
                                                    this.FPS91_TY_S_AC_39635587.GetValue(i, "ESMUS").ToString(),
                                                    sOUTMSG.ToString()
                                                    );

                            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        }
                    }

                    if (sOUTMSG.Substring(0, 2) != "OK")
                    {
                        return;
                    }

                    this.ShowMessage("TY_M_AC_31BAP617");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_31B1C623");
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "R") // 자금수지 계획
            {
                // -------------------------------- 자금 계획 등록 ---------------------------------------
                // 삭제
                if (this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount != 0)
                {
                    string sCUST = string.Empty;
                    string sYYMM = string.Empty;
                    for (int i = 0; i < this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString())
                        {
                            sCUST = this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString();
                            sYYMM = this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString();
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BB36245", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount; i++)
                {
                    if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString() &&
                        this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString())
                    {
                        this.DbConnector.Attach("TY_P_AC_3BB31246",
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString(),  // 계열사
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString(),   // 년월
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSEQN").ToString(),   // 자금코드
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFTINM").ToString(),   // 자금명
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFLEVE").ToString(),   // LEVEL
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSAMM").ToString(),   // 당월계획액
                                                TYUserInfo.EmpNo.ToString()
                                                );
                    }
                }
                this.DbConnector.ExecuteTranQueryList();

                // -------------------------------- 자금 실적 등록 ---------------------------------------

                // 삭제
                if (this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount != 0)
                {
                    string sCUST = string.Empty;
                    string sYYMM = string.Empty;

                    for (int i = 0; i < this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString())
                        {
                            sCUST = this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString();
                            sYYMM = this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString();
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A13T941", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();

                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount; i++)
                {
                    if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString() &&
                        this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString())
                    {
                        this.DbConnector.Attach("TY_P_AC_3A12K936",
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString(),  // 계열사
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString(),   // 년월
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSEQN").ToString(),   // 자금코드
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFTINM").ToString(),   // 자금명
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFLEVE").ToString(),   // LEVEL
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EAFSAMM").ToString(),   // 당월실적액
                                                this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EAFNEMM").ToString(),   // 익월예상액
                                                TYUserInfo.EmpNo.ToString()
                                                );
                    }
                }
                this.DbConnector.ExecuteTranQueryList();


                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMM") // 티와이스틸 매출자료(사용안함)
            {
                // 삭제
                if (this.FPS91_TY_S_AC_3A159942.ActiveSheet.RowCount != 0)
                {
                    string sCUST = this.FPS91_TY_S_AC_3A159942.GetValue(0, "ESMCUST").ToString();
                    string sYYMM = this.FPS91_TY_S_AC_3A159942.GetValue(0, "ESMYYHD").ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A15N945", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3A159942.ActiveSheet.RowCount; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_3A15M944",
                                            this.FPS91_TY_S_AC_3A159942.GetValue(i, "ESMCUST").ToString(),
                                            this.FPS91_TY_S_AC_3A159942.GetValue(i, "ESMYYHD").ToString(),
                                            this.FPS91_TY_S_AC_3A159942.GetValue(i, "ESMCDAC").ToString(),
                                            this.FPS91_TY_S_AC_3A159942.GetValue(i, "ESMCDNM").ToString(),
                                            this.FPS91_TY_S_AC_3A159942.GetValue(i, "ESMUS").ToString()
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "C") // 차입금 세부
            {
                // 삭제
                if (this.FPS91_TY_S_AC_3AH5T074.ActiveSheet.RowCount != 0)
                {
                    string sCUST = string.Empty;
                    string sYYMM = string.Empty;
                    for (int i = 0; i < this.FPS91_TY_S_AC_3AH5T074.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORYYHD").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCUST").ToString())
                        {
                            sCUST = this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCUST").ToString();
                            sYYMM = this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORYYHD").ToString();
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3AH6D076", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3AH5T074.ActiveSheet.RowCount; i++)
                {
                    if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORYYHD").ToString() &&
                        this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCUST").ToString())
                    {
                        this.DbConnector.Attach("TY_P_AC_3AH6E077",
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCUST").ToString(),   // 계열사
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORYYHD").ToString(),   // 년월
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCDAC").ToString(),   // 대출과목
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORNOSQ").ToString(),   // 차입금번호
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BROBKNM").ToString(),   // 금융기관명
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORWONAMT").ToString(), // 원화
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORBODATE").ToString(), // 차입일
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BOREXDATE").ToString(), // 만기일
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORIEYEL").ToString(),  // 이율
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORJMAJN").ToString(),  // 전월잔액
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORREPAY").ToString(),  // 당월상환액
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCAPIT").ToString(),  // 당윌차입액
                                                this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORMMJMT").ToString()   // 잔액
                                                );
                    }
                }

                this.DbConnector.ExecuteTranQueryList();

                // 차입금 세부 내역기준으로 MASTER 생성후 상위계정 계산 및 백만단위 정리
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3BF40325","A",this.CBH01_ESPLCMPY.GetValue(), this.DTP01_GSTYYMM.GetValue(), sOUTMSG.ToString() );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    this.ShowMessage("TY_M_AC_31BAP617");
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "M" ) // GLS 매출
            {
                // 삭제
                if (this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount != 0)
                {
                    string sCUST = this.FPS91_TY_S_AC_3AUA9134.GetValue(0, "ESSUBGB").ToString();
                    string sYYMM = this.FPS91_TY_S_AC_3AUA9134.GetValue(0, "ESSYYMM").ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3AUAN135", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_3AUAP136",
                                            this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString(),   // 계열사
                                            this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString(),   // 년월
                                            this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSPUMM").ToString(),   // 품목
                                            this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSMAEAMT").ToString()   // 매출액
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "G") // 티와이스틸 매출
            {
                // 삭제
                if (this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount != 0)
                {
                    string sCUST = string.Empty;
                    string sYYMM = string.Empty;
                    for (int i = 0; i < this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString())
                        {
                            sCUST = this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString();
                            sYYMM = this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString();
                        }
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A15N945", sCUST, sYYMM);
                    this.DbConnector.ExecuteNonQueryList();
                }

                // 등록
                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount; i++)
                {
                    if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString() &&
                        this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString())
                    {
                        this.DbConnector.Attach("TY_P_AC_3A15M944",
                                                this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString(),   // 계열사
                                                this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString(),   // 년월
                                                this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSPUMM").ToString(),   // 품목
                                                this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSMAEAMT").ToString()   // 매출액
                                                );
                    }
                }
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_31BAP617");
            }
        }
        #endregion

        #region Description : 조회 체크
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.txtFile.Text.ToString().Trim() == "")
            {
                string sOUTMSG = "EXCEL 화일을 선택 하세요";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 엑셀 올리기 체크
        private void BTN61_EXCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
            {
                if (this.FPS91_TY_S_AC_39633586.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
            {
                if (this.FPS91_TY_S_AC_39635587.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "R")  // 자금수지 
            {
                if (this.DTP01_GSTYYMM.GetValue().ToString() == "")
                {
                    string sOUTMSG = "년월을 입력 하세요";
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //this.SetStartingFocus(this.DTP01_GSTYYMM);
                    this.SetFocus(this.DTP01_GSTYYMM);
                    e.Successed = false;
                    return;
                }

                string sSW = string.Empty;

                if (this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_3BB34247.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFYYMM").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3BB34247.GetValue(i, "EPFSUBGN").ToString())
                        {
                            sSW = "*";
                        }
                    }

                    if (sSW != "*")
                    {
                        string sOUTMSG = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + "월 자료가 존재하지 않습니다. 아래 내역에서 확인하세요";
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }

            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMMM") // 사용안함
            {
                if (this.FPS91_TY_S_AC_3A159942.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "C")  // 차입금
            {
                if (this.DTP01_GSTYYMM.GetValue().ToString() == "")
                {
                    string sOUTMSG = "년월을 입력 하세요";
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //this.SetStartingFocus(this.DTP01_GSTYYMM);
                    this.SetFocus(this.DTP01_GSTYYMM);
                    e.Successed = false;
                    return;
                }

                string sSW = string.Empty;

                if (this.FPS91_TY_S_AC_3AH5T074.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_3AH5T074.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORYYHD").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AH5T074.GetValue(i, "BORCUST").ToString())
                        {
                            sSW = "*";
                        }
                    }

                    if (sSW != "*")
                    {
                        string sOUTMSG = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + "월 자료가 존재하지 않습니다. 아래 내역에서 확인하세요";
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "M" ) // GLS 매출
            {
                if (this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "G") // 티와이스틸  매출
            {

                if (this.DTP01_GSTYYMM.GetValue().ToString() == "")
                {
                    string sOUTMSG = "년월을 입력 하세요";
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //this.SetStartingFocus(this.DTP01_GSTYYMM);
                    this.SetFocus(this.DTP01_GSTYYMM);
                    e.Successed = false;
                    return;
                }

                string sSW = string.Empty;

                if (this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount == 0)
                {
                    this.ShowMessage("TY_M_GB_2452W459");
                    e.Successed = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_3AUA9134.ActiveSheet.RowCount; i++)
                    {
                        if (this.DTP01_GSTYYMM.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSYYMM").ToString() &&
                            this.CBH01_ESPLCMPY.GetValue().ToString() == this.FPS91_TY_S_AC_3AUA9134.GetValue(i, "ESSUBGB").ToString())
                        {
                            sSW = "*";
                        }
                    }

                    if (sSW != "*")
                    {
                        string sOUTMSG = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "년" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + "월 자료가 존재하지 않습니다. 아래 내역에서 확인하세요";
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

        }
        #endregion


        #region Description : 조회(엑셀 자료 조회)
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = true; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = false;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = false;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = false;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = false;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = false;  // 티와이스틸 , GLS 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = true;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = false;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = false;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = false;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = false;  // 티와이스틸 , GLS 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "R")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = false;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = true;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = false;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = false;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = false;  // 티와이스틸 , GLS 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMM")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = false;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = false;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = true;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = false;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = false;  // 티와이스틸 , GLS 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "C")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = false;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = false;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = false;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = true;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = false;  // 티와이스틸 , GLS 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "G" || this.CBO01_EPLCGB.GetValue().ToString() == "M")
                {
                    this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                    this.FPS91_TY_S_AC_39635587.Visible = false;  // 재무상태표
                    this.FPS91_TY_S_AC_3BB34247.Visible = false;  // 자금계획
                    this.FPS91_TY_S_AC_3A159942.Visible = false;  // 티와이스틸 매출
                    this.FPS91_TY_S_AC_3AH5T074.Visible = false;  // 차입금 세부
                    this.FPS91_TY_S_AC_3AUA9134.Visible = true;  // 티와이스틸 , GLS 매출
                }


                string strProvider = string.Empty;
                string strQuery = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
                {
                    strQuery = "SELECT * FROM [경영실적$A06:F10000]"; //  , Sheet1$
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
                {
                    strQuery = "SELECT * FROM [재무상태표$A05:E10000]"; //  , Sheet1$
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "R")
                {
                    strQuery = "SELECT * FROM [자금수지$A05:H10000]"; //  , Sheet1$
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMM")
                {
                    strQuery = "SELECT * FROM [매출$A05:E10000]"; //  , Sheet1$
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "C")
                {
                    strQuery = "SELECT * FROM [차입금현황$A04:N10000]"; //  , Sheet1$
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "G" || this.CBO01_EPLCGB.GetValue().ToString() == "M")  // 티와이스틸 , GLS 매출
                {
                    strQuery = "SELECT * FROM [매출$A05:E10000]"; //  , Sheet1$
                }


                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                // 데이터테이블로 가져옴
                DataSet ExcelDs = new DataSet();

                if (this.CBO01_EPLCGB.GetValue().ToString() == "C") // 차입금 세부
                {
                    ExcelDs = Convert_DataSet_borr(ds);
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "G" || this.CBO01_EPLCGB.GetValue().ToString() == "M") // 티와이스틸 ,GLS 매출
                {
                    ExcelDs = Convert_DataSet_Gls(ds);
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "R") // 자금계획
                {
                    ExcelDs = Convert_DataSet_FUND(ds);
                }
                else
                {
                    ExcelDs = Convert_DataSet(ds);
                }


                if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
                {
                    this.FPS91_TY_S_AC_39633586.SetValue(ExcelDs); // 손익자료
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
                {
                    this.FPS91_TY_S_AC_39635587.SetValue(ExcelDs); // 재무상태표
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "R")
                {
                    this.FPS91_TY_S_AC_3BB34247.SetValue(ExcelDs); // 자금수지계획
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMM")
                {
                    this.FPS91_TY_S_AC_3A159942.SetValue(ExcelDs); // 티와이스틸 매출
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "C")
                {
                    this.FPS91_TY_S_AC_3AH5T074.SetValue(ExcelDs); // 차입금 세부
                }
                else if (this.CBO01_EPLCGB.GetValue().ToString() == "G" || this.CBO01_EPLCGB.GetValue().ToString() == "M")
                {
                    this.FPS91_TY_S_AC_3AUA9134.SetValue(ExcelDs); // 티와이스틸 , GLS 매출
                }

            }
        }
        #endregion

        #region Description : 화일 찾기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            //OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 워크시트 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region Description : CBO01_EPLCGB_SelectedIndexChanged
        private void CBO01_EPLCGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
            {
                this.FPS91_TY_S_AC_39633586.Visible = true;  // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = false;
                this.LBL51_GSTYYMM.Visible = false;
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = true;  // 재무상태표
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = false;
                this.LBL51_GSTYYMM.Visible = false;
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "R")
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3BB34247.Visible = true;  // 자금수지계획
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = true;
                this.LBL51_GSTYYMM.Visible = true;
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "MMM")
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3A159942.Visible = true;  // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = false;
                this.LBL51_GSTYYMM.Visible = false;
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "C")
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AH5T074.Visible = true;  // 차입금 세부
                this.FPS91_TY_S_AC_3AUA9134.Visible = false; // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = true;
                this.LBL51_GSTYYMM.Visible = true;
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "G" )
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3AUA9134.Visible = true;  // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = true;
                this.LBL51_GSTYYMM.Visible = true;
            }
            else if ( this.CBO01_EPLCGB.GetValue().ToString() == "M")
            {
                this.FPS91_TY_S_AC_39633586.Visible = false; // 손익자료
                this.FPS91_TY_S_AC_39635587.Visible = false; // 재무상태표
                this.FPS91_TY_S_AC_3BB34247.Visible = false; // 자금수지계획
                this.FPS91_TY_S_AC_3A159942.Visible = false; // 티와이스틸 주요매출
                this.FPS91_TY_S_AC_3AH5T074.Visible = false; // 차입금 세부
                this.FPS91_TY_S_AC_3AUA9134.Visible = true;  // 티와이스틸 ,GLS 주요매출

                this.DTP01_GSTYYMM.Visible = false;
                this.LBL51_GSTYYMM.Visible = false;
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataSet Convert_DataSet(DataSet ds)
        {

            DataSet RetDs = new DataSet();

            // 마스터테이블 생성
            DataTable ExcelTable = new DataTable();

            // 빈 ROW 생성
            DataRow ExcelRow;

            string sTMCUST = string.Empty;
            string sTMYYHD = string.Empty;
            string sTMCDAC = string.Empty;
            string sTMCDNM = string.Empty;
            string sTMPLAMT = string.Empty;
            string sTMUSAMT = string.Empty;

            ExcelTable.Columns.Add("ESMCUST", typeof(System.String));
            ExcelTable.Columns.Add("ESMYYHD", typeof(System.String));
            ExcelTable.Columns.Add("ESMCDAC", typeof(System.String));
            ExcelTable.Columns.Add("ESMCDNM", typeof(System.String));
            if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
            {
                ExcelTable.Columns.Add("ESMPL", typeof(System.String));
                ExcelTable.Columns.Add("ESMUS", typeof(System.Double));
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
            {
                ExcelTable.Columns.Add("ESMUS", typeof(System.Double));
            }
            else if (this.CBO01_EPLCGB.GetValue().ToString() == "M")
            {
                ExcelTable.Columns.Add("ESMUS", typeof(System.Double));
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim()) != "")
                {
                    ExcelRow = ExcelTable.NewRow();

                    if (this.CBO01_EPLCGB.GetValue().ToString() == "P")
                    {
                        sTMCUST = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());
                        sTMYYHD = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());
                        sTMCDAC = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());
                        sTMCDNM = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());
                        sTMPLAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()));
                        sTMUSAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][5].ToString().Trim()));

                        ExcelRow["ESMCUST"] = sTMCUST;
                        ExcelRow["ESMYYHD"] = sTMYYHD;
                        ExcelRow["ESMCDAC"] = sTMCDAC;
                        ExcelRow["ESMCDNM"] = sTMCDNM;
                        ExcelRow["ESMPL"] = double.Parse(sTMPLAMT);
                        ExcelRow["ESMUS"] = double.Parse(sTMUSAMT);
                    }
                    else if (this.CBO01_EPLCGB.GetValue().ToString() == "B")
                    {
                        sTMCUST = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());
                        sTMYYHD = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());
                        sTMCDAC = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());
                        sTMCDNM = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());
                        sTMUSAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()));

                        ExcelRow["ESMCUST"] = sTMCUST;
                        ExcelRow["ESMYYHD"] = sTMYYHD;
                        ExcelRow["ESMCDAC"] = sTMCDAC;
                        ExcelRow["ESMCDNM"] = sTMCDNM;
                        ExcelRow["ESMUS"] = double.Parse(sTMUSAMT);
                    }
                    else if (this.CBO01_EPLCGB.GetValue().ToString() == "M")
                    {
                        sTMCUST = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());
                        sTMYYHD = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());
                        sTMCDAC = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());
                        sTMCDNM = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());
                        sTMUSAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()));

                        ExcelRow["ESMCUST"] = sTMCUST;
                        ExcelRow["ESMYYHD"] = sTMYYHD;
                        ExcelRow["ESMCDAC"] = sTMCDAC;
                        ExcelRow["ESMCDNM"] = sTMCDNM;
                        ExcelRow["ESMUS"] = double.Parse(sTMUSAMT);
                    }

                    ExcelTable.Rows.Add(ExcelRow);
                }
            }

            ExcelTable.TableName = "EXCEL1";

            RetDs.Tables.Add(ExcelTable);

            return RetDs;
        }
        #endregion

        #region Description : 데이터셋 변경(GLS 매출)
        private DataSet Convert_DataSet_Gls(DataSet ds)
        {
            DataSet RetDs = new DataSet();

            // 마스터테이블 생성
            DataTable ExcelTable = new DataTable();

            // 빈 ROW 생성
            DataRow ExcelRow;

            string sESSUBGB = string.Empty;    // 계열사
            string sESSYYMM = string.Empty;    // 년월
            string sESSPUMM = string.Empty;    // 품목
            string sESSPUMMNM = string.Empty;    // 품목명
            string sESSMAEAMT = string.Empty;    // 매출액 

            ExcelTable.Columns.Add("ESSUBGB", typeof(System.String));    // 계열사
            ExcelTable.Columns.Add("ESSYYMM", typeof(System.String));    // 년월
            ExcelTable.Columns.Add("ESSPUMM", typeof(System.String));    // 품목
            ExcelTable.Columns.Add("ESSPUMMNM", typeof(System.String));  // 품목명
            ExcelTable.Columns.Add("ESSMAEAMT", typeof(System.Double));  // 매출액

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim()) != "")
                {
                    ExcelRow = ExcelTable.NewRow();

                    sESSUBGB = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());                    // 계열사
                    sESSYYMM = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());                    // 년월
                    sESSPUMM = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());                    // 품목
                    sESSPUMMNM = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());                  // 품목명
                    sESSMAEAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()));    // 매출액 

                    ExcelRow["ESSUBGB"] = sESSUBGB;                      // 계열사
                    ExcelRow["ESSYYMM"] = sESSYYMM;                      // 년월
                    ExcelRow["ESSPUMM"] = sESSPUMM;                      // 품목
                    ExcelRow["ESSPUMMNM"] = sESSPUMMNM;                  // 품목명
                    ExcelRow["ESSMAEAMT"] = double.Parse(sESSMAEAMT);    // 매출액

                    ExcelTable.Rows.Add(ExcelRow);
                }
            }

            ExcelTable.TableName = "EXCEL1";

            RetDs.Tables.Add(ExcelTable);

            return RetDs;
        }
        #endregion

        #region Description : 데이터셋 변경(자금수지 계획)
        private DataSet Convert_DataSet_FUND(DataSet ds)
        {
            DataSet RetDs = new DataSet();

            // 마스터테이블 생성
            DataTable ExcelTable = new DataTable();

            // 빈 ROW 생성
            DataRow ExcelRow;

            string sESSUBGB = string.Empty;   // 계열사
            string sESSYYMM = string.Empty;   // 년월
            string sESSPUMM = string.Empty;   // 코드
            string sEPFTINM = string.Empty;   // 자금명
            string sESSPUMMNM = string.Empty; // LEVEL
            string sESSMAEAMT = string.Empty; // 당월계획액
            string sEAFSAMM = string.Empty; // 당월실적액
            string sEAFNEMM = string.Empty; // 익월예상액

            ExcelTable.Columns.Add("EPFSUBGN", typeof(System.String));   // 계열사
            ExcelTable.Columns.Add("EPFYYMM", typeof(System.String));    // 년월
            ExcelTable.Columns.Add("EPFSEQN", typeof(System.String));    // 코드
            ExcelTable.Columns.Add("EPFTINM", typeof(System.String));    // 자금명
            ExcelTable.Columns.Add("EPFLEVE", typeof(System.String));    // LEVEL
            ExcelTable.Columns.Add("EPFSAMM", typeof(System.Double));    // 당월계획액
            ExcelTable.Columns.Add("EAFSAMM", typeof(System.Double));    // 당월실적액
            ExcelTable.Columns.Add("EAFNEMM", typeof(System.Double));    // 익월예상액

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim()) != "")
                {
                    ExcelRow = ExcelTable.NewRow();

                    sESSUBGB = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());                   // 계열사
                    sESSYYMM = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());                   // 년월
                    sESSPUMM = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());                   // 코드
                    sEPFTINM = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());                   // 자금명
                    sESSPUMMNM = SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim());                 // LEVEL
                    sESSMAEAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][5].ToString().Trim()));    // 당월계획액
                    sEAFSAMM = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][6].ToString().Trim()));      // 당월실적액
                    sEAFNEMM = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][7].ToString().Trim()));      // 익월예상액

                    ExcelRow["EPFSUBGN"] = sESSUBGB;                       // 계열사
                    ExcelRow["EPFYYMM"] = sESSYYMM;                       // 년월
                    ExcelRow["EPFSEQN"] = sESSPUMM;                       // 코드
                    ExcelRow["EPFTINM"] = sEPFTINM;                       // 자금명
                    ExcelRow["EPFLEVE"] = sESSPUMMNM;                   // LEVEL
                    ExcelRow["EPFSAMM"] = double.Parse(sESSMAEAMT);     // 당월계획액
                    ExcelRow["EAFSAMM"] = double.Parse(sEAFSAMM);       // 당월실적액
                    ExcelRow["EAFNEMM"] = double.Parse(sEAFNEMM);       // 익월예상액

                    ExcelTable.Rows.Add(ExcelRow);
                }
            }

            ExcelTable.TableName = "EXCEL1";

            RetDs.Tables.Add(ExcelTable);

            return RetDs;
        }
        #endregion

        #region Description : 데이터셋 변경(차입금)
        private DataSet Convert_DataSet_borr(DataSet ds)
        {

            DataSet RetDs = new DataSet();

            // 마스터테이블 생성
            DataTable ExcelTable = new DataTable();

            // 빈 ROW 생성
            DataRow ExcelRow;

            string sBORCUST = string.Empty;    // 계열사
            string sBORYYHD = string.Empty;    // 년월

            string sBORCDNM = string.Empty;    // 대출과목명
            string sBORCDAC = string.Empty;    // 대출과목
            string sBORNOSQ = string.Empty;    // 차입금번호
            string sBROBKNM = string.Empty;    // 금융기관명
            string sBORWONAMT = string.Empty;  // 원화
            string sBORBODATE = string.Empty;  // 차입일
            string sBOREXDATE = string.Empty;  // 만기일
            string sBORIEYEL = string.Empty;   // 이율
            string sBORJMAJN = string.Empty;   // 전월잔액 
            string sBORREPAY = string.Empty;   // 당월상환액 
            string sBORCAPIT = string.Empty;   // 당윌차입액 
            string sBORMMJMT = string.Empty;   // 잔액 

            ExcelTable.Columns.Add("BORCUST", typeof(System.String));    // 계열사
            ExcelTable.Columns.Add("BORYYHD", typeof(System.String));    // 년월
            ExcelTable.Columns.Add("BORCDNM", typeof(System.String));    // 대출과목명
            ExcelTable.Columns.Add("BORCDAC", typeof(System.String));    // 대출과목
            ExcelTable.Columns.Add("BORNOSQ", typeof(System.String));    // 차입금번호
            ExcelTable.Columns.Add("BROBKNM", typeof(System.String));    // 금융기관명
            ExcelTable.Columns.Add("BORWONAMT", typeof(System.Double));  // 원화
            ExcelTable.Columns.Add("BORBODATE", typeof(System.String));  // 차입일
            ExcelTable.Columns.Add("BOREXDATE", typeof(System.String));  // 만기일
            ExcelTable.Columns.Add("BORIEYEL", typeof(System.Double));   // 이율
            ExcelTable.Columns.Add("BORJMAJN", typeof(System.Double));   // 전월잔액 
            ExcelTable.Columns.Add("BORREPAY", typeof(System.Double));   // 당월상환액
            ExcelTable.Columns.Add("BORCAPIT", typeof(System.Double));   // 당윌차입액
            ExcelTable.Columns.Add("BORMMJMT", typeof(System.Double));   // 잔액 

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim()) != "")
                {
                    ExcelRow = ExcelTable.NewRow();

                    sBORCUST = SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim());                    // 계열사
                    sBORYYHD = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim());                    // 년월
                    sBORCDNM = SetDefaultValue(ds.Tables[0].Rows[i][2].ToString().Trim());                    // 대출과목명
                    sBORCDAC = SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim());                    // 대출과목
                    if (SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()) == "")
                    {
                        sBORNOSQ = Get_Numeric(Convert.ToString(i));                    // 차입금번호
                    }
                    else
                    {
                        sBORNOSQ = SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim());                    // 차입금번호
                    }
                    sBROBKNM = SetDefaultValue(ds.Tables[0].Rows[i][5].ToString().Trim());                    // 금융기관명
                    sBORWONAMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][6].ToString().Trim()));     // 원화
                    if (SetDefaultValue(ds.Tables[0].Rows[i][7].ToString()) != "")
                    {
                        sBORBODATE = SetDefaultValue(ds.Tables[0].Rows[i][7].ToString().Replace("-", "").Trim().Substring(0, 8));                  // 차입일
                    }else
                    {
                        sBORBODATE = "";
                    }
                    ;
                    if (SetDefaultValue(ds.Tables[0].Rows[i][8].ToString()) != "")
                    {
                        sBOREXDATE = SetDefaultValue(ds.Tables[0].Rows[i][8].ToString().Replace("-", "").Trim().Substring(0, 8));                  // 만기일
                    }
                    else
                    {
                        sBOREXDATE = "";
                    }
                    sBORIEYEL = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][9].ToString().Trim()));      // 이율
                    sBORJMAJN = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][10].ToString().Trim()));     // 전월잔액
                    sBORREPAY = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][11].ToString().Trim()));     // 당월상환액
                    sBORCAPIT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][12].ToString().Trim()));     // 당윌차입액
                    sBORMMJMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][13].ToString().Trim()));     // 잔액

                    ExcelRow["BORCUST"] = sBORCUST;                      // 계열사
                    ExcelRow["BORYYHD"] = sBORYYHD;                      // 년월
                    ExcelRow["BORCDNM"] = sBORCDNM;                      // 대출과목명
                    ExcelRow["BORCDAC"] = sBORCDAC;                      // 대출과목
                    ExcelRow["BORNOSQ"] = sBORNOSQ;                      // 차입금번호
                    ExcelRow["BROBKNM"] = sBROBKNM;                      // 금융기관명
                    ExcelRow["BORWONAMT"] = double.Parse(sBORWONAMT);    // 원화
                    ExcelRow["BORBODATE"] = sBORBODATE;                  // 차입일
                    ExcelRow["BOREXDATE"] = sBOREXDATE;                  // 만기일
                    ExcelRow["BORIEYEL"] = double.Parse(sBORIEYEL);      // 이율
                    ExcelRow["BORJMAJN"] = double.Parse(sBORJMAJN);      // 전월잔액
                    ExcelRow["BORREPAY"] = double.Parse(sBORREPAY);      // 당월상환액
                    ExcelRow["BORCAPIT"] = double.Parse(sBORCAPIT);      // 당윌차입액
                    ExcelRow["BORMMJMT"] = double.Parse(sBORMMJMT);      // 잔액

                    ExcelTable.Rows.Add(ExcelRow);
                }
            }

            ExcelTable.TableName = "EXCEL1";

            RetDs.Tables.Add(ExcelTable);

            return RetDs;
        }
        #endregion
    }
}
