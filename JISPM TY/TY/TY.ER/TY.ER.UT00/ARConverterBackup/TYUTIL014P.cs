using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using System.IO;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Html;
using DataDynamics.ActiveReports.Export.Pdf;

namespace TY.ER.UT00
{
    /// <summary>
    /// UTILITY 사용내역 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.15 16:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BIGP785 : UTILITY 사용내역 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  EMHWAJU : 화주
    ///  YYYYMM : 년월
    /// </summary>
    public partial class TYUTIL014P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 페이지 로드
        public TYUTIL014P()
        {
            InitializeComponent();
        }

        private void TYUTIL014P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sHWAJU = string.Empty;
            string sHWAMUL = string.Empty;

            string sOUT_MSG = string.Empty;

            // UTILITY TEMP파일 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71CBE407");

            this.DbConnector.ExecuteNonQuery();

            // UTILITY 사용내역 SP - CALL
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71C92406",  
                                    Get_Date(this.DTP01_YYYYMM.GetValue().ToString()),
                                    this.CBH01_EMHWAJU.GetValue().ToString(),
                                    sOUT_MSG.ToString()
                                    );

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
            {
                this.ShowMessage("TY_M_MR_2BF50354");
            }
            else
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            int stmm = 0;
            int styy = 0;

            int stdd = 0;
            int eddd = 0;

            int styymm = 0;
            int edyymm = 0;

            if (int.Parse(this.DTP01_YYYYMM.GetString()) > 202001)  // 2020-02월 부터 시작일 종료일 변경 (서태호 과장)
            {
                stdd = 21;
                eddd = 20;
            }
            else
            {
                stdd = 26;
                eddd = 25;
            }

            styymm = int.Parse(this.DTP01_YYYYMM.GetString());
            edyymm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString()));

            styy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
            stmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

            stmm = stmm - 1;
            if (stmm == 0)
            {
                styy = styy - 1;
                stmm = 12;
            }

            string wstyymmdd = Convert.ToString(styy) + '-' + Set_Fill2(Convert.ToString(stmm)) + '-' + Convert.ToString(stdd);
            string ssdyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
            string ssdmm = this.DTP01_YYYYMM.GetString().Substring(4, 2);
            string wedyymmdd = ssdyy + '-' + ssdmm + '-' + Convert.ToString(eddd);

            string sqryyymm = this.DTP01_YYYYMM.GetString().Substring(0, 6);

            // --------------- 마지막 일자  ---------------

            int istyy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
            int istmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

            string edstyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
            string edstmm = Set_Fill2(this.DTP01_YYYYMM.GetString().Substring(4, 2));
            string edstdd = "01";

            string wstyymmdd2 = edstyy + edstmm + edstdd; // 해당월 처리 화주 01일~ 
            string ededdd = System.DateTime.DaysInMonth(istyy, istmm).ToString();//해당월의 마지막 일자 구하기
            string wedyymmdd2 = edstyy + edstmm + ededdd; // ~ 마지막일 까지

            wstyymmdd2 = wstyymmdd2.Substring(0, 4) + "-" + wstyymmdd2.Substring(4, 2) + "-" + wstyymmdd2.Substring(6, 2);
            wedyymmdd2 = wedyymmdd2.Substring(0, 4) + "-" + wedyymmdd2.Substring(4, 2) + "-" + wedyymmdd2.Substring(6, 2);

            this.DbConnector.CommandClear();

            //this.DbConnector.Attach("TY_P_UT_6BIGP785", wstyymmdd + "-" + wedyymmdd,
            //                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
            //                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
            //                                            this.CBH01_EMHWAJU.GetValue().ToString(),
            //                                            wstyymmdd2 + "-" + wedyymmdd2,
            //                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
            //                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
            //                                            this.CBH01_EMHWAJU.GetValue().ToString()
            //                                            );

            // 20190523 LPG 추가
            this.DbConnector.Attach("TY_P_UT_95OBL632", wstyymmdd + "-" + wedyymmdd,
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.CBH01_EMHWAJU.GetValue().ToString(),
                                                        wstyymmdd2 + "-" + wedyymmdd2,
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.CBH01_EMHWAJU.GetValue().ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dtJilso = Get_JilsoData();

            if (dt.Rows.Count > 0)
            {
                if (double.Parse(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 202001)
                {
                    ActiveReport rpt = new TYUTIL014R3(dtJilso);
                    // 세로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                    (new TYERGB001P(rpt, Convert_DataTable_201012(dt))).ShowDialog();
                }
                else if (double.Parse(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 201012)
                {
                    ActiveReport rpt = new TYUTIL014R2();
                    // 세로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                    (new TYERGB001P(rpt, Convert_DataTable_201012(dt))).ShowDialog();
                }
                else
                {
                    ActiveReport rpt = new TYUTIL014R1();
                    // 세로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                    (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Descripton : 201012월 이전 데이터 테이블 변경
        private DataTable Convert_DataTable(DataTable dt)
        {
            DataTable dtRtn = new DataTable();
            DataTable dtEL = new DataTable();
            DataTable dtST = new DataTable();
            DataTable dtTK = new DataTable();

            string sNEWYYMM = string.Empty;
            string sNEWHWAJU = string.Empty;

            double WKAMT = 0;
            double wk_DAUBKAMT = 0;
            double wk_DASELAMT = 0;
            double wk_DAPSAMT = 0;
            double wk_DADMAMT = 0;
            double wk_DAELAMT = 0;
            double wk_DAUELAMT = 0;
            double wk_DAELAMT1 = 0;
            double wk_DAUKYAMT = 0;
            double wk_DAUDKAMT = 0;
            double wk_DASBKAMT = 0;
            double wk_DASKYAMT = 0;

            dtRtn.Columns.Add("DAHWAJUNM", typeof(System.String));
            dtRtn.Columns.Add("DAHWAJU", typeof(System.String));
            dtRtn.Columns.Add("DASTYY", typeof(System.Double));
            dtRtn.Columns.Add("DASTMM", typeof(System.Double));
            dtRtn.Columns.Add("DASTDD", typeof(System.Double));
            dtRtn.Columns.Add("DAEDYY", typeof(System.Double));
            dtRtn.Columns.Add("DAEDMM", typeof(System.Double));
            dtRtn.Columns.Add("DAEDDD", typeof(System.Double));
            dtRtn.Columns.Add("DAUELTIME", typeof(System.Double));
            dtRtn.Columns.Add("DAUMOUTER", typeof(System.Double));
            dtRtn.Columns.Add("DAUYUL", typeof(System.Double));
            dtRtn.Columns.Add("DAUELECT", typeof(System.Double));
            dtRtn.Columns.Add("DAUELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAJLQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAJLDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAJLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASELTIME", typeof(System.Double));
            dtRtn.Columns.Add("DASMOTER", typeof(System.Double));
            dtRtn.Columns.Add("DASYUL", typeof(System.Double));
            dtRtn.Columns.Add("DASELECT", typeof(System.Double));
            dtRtn.Columns.Add("DASELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASBKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DASBKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DASBKAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASKYQTY", typeof(System.Double));
            dtRtn.Columns.Add("DASKYDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DASKYAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAGAAMT", typeof(System.Double));
            dtRtn.Columns.Add("DATANK1", typeof(System.String));
            dtRtn.Columns.Add("DACAPA1", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT1", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY1", typeof(System.Double));
            dtRtn.Columns.Add("DATANK2", typeof(System.String));
            dtRtn.Columns.Add("DACAPA2", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT2", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY2", typeof(System.Double));
            dtRtn.Columns.Add("DATANK3", typeof(System.String));
            dtRtn.Columns.Add("DACAPA3", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT3", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY3", typeof(System.Double));
            dtRtn.Columns.Add("DATANK4", typeof(System.String));
            dtRtn.Columns.Add("DACAPA4", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT4", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY4", typeof(System.Double));
            dtRtn.Columns.Add("DATANK5", typeof(System.String));
            dtRtn.Columns.Add("DACAPA5", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT5", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY5", typeof(System.Double));
            dtRtn.Columns.Add("DATOTCLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DATOTPSQTY", typeof(System.Double));
            dtRtn.Columns.Add("DATKCLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAPSDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAPSAMT", typeof(System.Double));
            dtRtn.Columns.Add("DADMQTY", typeof(System.Double));
            dtRtn.Columns.Add("DADMDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DADMAMT", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAAMPRE", typeof(System.Double));
            dtRtn.Columns.Add("DADAY", typeof(System.Double));
            dtRtn.Columns.Add("DAELYUL", typeof(System.Double));
            dtRtn.Columns.Add("DAELDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAAMPRE1", typeof(System.Double));
            dtRtn.Columns.Add("DADAY1", typeof(System.Double));
            dtRtn.Columns.Add("DAELYUL1", typeof(System.Double));
            dtRtn.Columns.Add("DAELDANGA1", typeof(System.Double));
            dtRtn.Columns.Add("DAELAMT1", typeof(System.Double));
            dtRtn.Columns.Add("DATOTAMT", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT1", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT2", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT3", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT4", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT5", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMTHAP", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKAMT", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT1", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT2", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT3", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT4", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT5", typeof(System.Double));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dtRtn.NewRow();

                sNEWYYMM = this.DTP01_YYYYMM.GetString().Substring(0,6);         // 처리년월
                sNEWHWAJU = dt.Rows[i]["TPHWAJU"].ToString();    // 화주

                row["DAHWAJUNM"] = dt.Rows[i]["HWAJUNM"].ToString();               // 화주
                row["DAHWAJU"] = dt.Rows[i]["HWAJUNM"].ToString();               // 화주
                row["DASTYY"] = dt.Rows[i]["PDATE"].ToString().Substring(0, 4);  // 시작 년
                row["DASTMM"] = dt.Rows[i]["PDATE"].ToString().Substring(5, 2);  // 시작 월
                row["DASTDD"] = dt.Rows[i]["PDATE"].ToString().Substring(8, 2);  // 시작 일
                row["DAEDYY"] = dt.Rows[i]["PDATE"].ToString().Substring(11, 4); // 종료 년
                row["DAEDMM"] = dt.Rows[i]["PDATE"].ToString().Substring(16, 2); // 종료 월
                row["DAEDDD"] = dt.Rows[i]["PDATE"].ToString().Substring(19, 2); // 종료 일

                //---------------[ 질소사용료 ]---------------
                row["DAJLQTY"] = double.Parse(dt.Rows[i]["TPJLQTY"].ToString());   // 수량
                row["DAJLDANGA"] = double.Parse(dt.Rows[i]["TPJLDANGA"].ToString()); // 단가
                row["DAJLAMT"] = double.Parse(dt.Rows[i]["TPJLAMT"].ToString());   // 금액
                double wk_DAJLAMT = double.Parse(dt.Rows[i]["TPJLAMT"].ToString());   // 금액

                #region Description : 가열료

                #region Description : 상하단지

                wk_DAUELAMT = 0;

                //---------------[ 전기료 ]---------------
                if (0 != double.Parse(dt.Rows[i]["TPUELTIME"].ToString()))
                {
                    row["DAUELTIME"] = double.Parse(dt.Rows[i]["TPUELTIME"].ToString());
                    row["DAUMOUTER"] = double.Parse(dt.Rows[i]["DNMOTER1"].ToString());
                    row["DAUYUL"] = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                    row["DAUELECT"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    double daueltim = double.Parse(dt.Rows[i]["TPUELTIME"].ToString());
                    double daumoter = double.Parse(dt.Rows[i]["DNMOTER1"].ToString());
                    double dauyul = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                    double dauelect = double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    if (sNEWHWAJU == "TYC")
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6BMDJ824", sNEWYYMM.ToString(),
                                                                    sNEWHWAJU.ToString().ToUpper()
                                                                    );

                        dtEL = this.DbConnector.ExecuteDataTable();

                        WKAMT = 0;

                        if (dtEL.Rows.Count > 0)
                        {
                            wk_DAUELAMT = double.Parse(Get_Numeric(dtEL.Rows[0]["GAELAMT"].ToString()));
                            row["DAUELAMT"] = double.Parse(Get_Numeric(dtEL.Rows[0]["GAELAMT"].ToString()));
                        }
                    }
                    else
                    {
                        wk_DAUELAMT = double.Parse(UP_DotDelete(Convert.ToString(daueltim * daumoter * dauyul * dauelect)));
                        row["DAUELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUELAMT * 0.1))) * 10;
                    }
                }

                //---------------[ 스팀 ]---------------
                // 월집계가 없을때는 계산하여 처리 한다.
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6BMDK825", sNEWYYMM.ToString(),
                                                            sNEWHWAJU.ToString().ToUpper()
                                                            );

                dtST = this.DbConnector.ExecuteDataTable();

                WKAMT = 0;

                if (dtST.Rows.Count > 0)
                {
                    WKAMT = double.Parse(Get_Numeric(dtST.Rows[0]["TTAMT"].ToString()));
                }

                wk_DAUBKAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPUBKQTY"].ToString()))
                {
                    row["DAUBKQTY"] = double.Parse(dt.Rows[i]["TPUBKQTY"].ToString());
                    row["DAUBKDANGA"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());

                    double wkdaubkqty = double.Parse(dt.Rows[i]["TPUBKQTY"].ToString());
                    double wkdaubkdanga = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());

                    if (WKAMT == (wkdaubkqty * wkdaubkdanga))
                    {
                        if (sNEWHWAJU == "TYC")
                        {
                            wk_DAUBKAMT = WKAMT;
                            row["DAUBKAMT"] = wk_DAUBKAMT;
                        }
                        else
                        {
                            wk_DAUBKAMT = double.Parse(UP_DotDelete(Convert.ToString(wkdaubkqty * wkdaubkdanga)));
                            row["DAUBKAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUBKAMT * 0.1))) * 10;
                        }
                    }
                    else
                    {
                        wk_DAUBKAMT = WKAMT;
                        row["DAUBKAMT"] = wk_DAUBKAMT;
                    }
                }

                //---------------[ 경유 ]---------------
                wk_DAUKYAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPUKYQTY"].ToString()))
                {
                    row["DAUKYQTY"] = double.Parse(dt.Rows[i]["TPUKYQTY"].ToString());
                    row["DAUKYDANGA"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());
                    double wkdaukqty = double.Parse(dt.Rows[i]["TPUKYQTY"].ToString());
                    double wkdaukydanga = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());
                    wk_DAUKYAMT = double.Parse(UP_DotDelete(Convert.ToString(wkdaukqty * wkdaukydanga)));
                    row["DAUKYAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10;
                }

                #endregion

                #region Description : 송유단지

                //---------------[ 전기료 ]---------------
                wk_DASELAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSELTIME"].ToString()))
                {
                    row["DASELTIME"] = double.Parse(dt.Rows[i]["TPSELTIME"].ToString());
                    row["DASMOTER"] = double.Parse(dt.Rows[i]["DNMOTER2"].ToString());
                    row["DASYUL"] = double.Parse(dt.Rows[i]["DNYUL"].ToString());       //
                    row["DASELECT"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());     //
                    double daseltim = double.Parse(dt.Rows[i]["TPSELTIME"].ToString());
                    double dasumoter = double.Parse(dt.Rows[i]["DNMOTER2"].ToString());
                    double dasuyul = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                    double dasuelect = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    wk_DASELAMT = double.Parse(UP_DotDelete(Convert.ToString(daseltim * dasumoter * dasuyul * dasuelect)));
                    row["DASELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DASELAMT * 0.1))) * 10;
                }

                //---------------[ 벙커씨유 ]---------------
                wk_DASBKAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSBKQTY"].ToString()))
                {
                    row["DASBKQTY"] = double.Parse(dt.Rows[i]["TPSBKQTY"].ToString());
                    row["DASBKDANGA"] = double.Parse(dt.Rows[i]["DNBKCU"].ToString());
                    wk_DASBKAMT = (double.Parse(dt.Rows[i]["TPSBKQTY"].ToString())) * (double.Parse(dt.Rows[i]["DNBKCU"].ToString()));
                    row["DASBKAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10;
                }

                //---------------[ 경유 ]---------------
                wk_DASKYAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSKYQTY"].ToString()))
                {
                    row["DASKYQTY"] = double.Parse(dt.Rows[i]["TPSKYQTY"].ToString());
                    row["DASKYDANGA"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());
                    wk_DASKYAMT = (double.Parse(dt.Rows[i]["TPSKYQTY"].ToString())) * (double.Parse(dt.Rows[i]["DNKYUNG"].ToString()));
                    row["DASKYAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DASKYAMT * 0.1))) * 10;
                }

                #endregion

                #endregion

                double wk_DAGAAMT = 0;
                // 합계
                if (sNEWHWAJU == "TYC")
                {
                    wk_DAGAAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        wk_DAUELAMT +
                        wk_DAUBKAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASKYAMT * 0.1))) * 10) +
                        wk_DAUDKAMT
                        )));
                }
                else
                {
                    wk_DAGAAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUBKAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASKYAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUDKAMT * 0.1))) * 10)
                        )));
                }

                row["DAGAAMT"] = wk_DAGAAMT;

                //---------------[ 세척료 ]---------------

                row["DATANK1"] = dt.Rows[i]["TPCLTANK1"].ToString().Trim();
                row["DATANK2"] = dt.Rows[i]["TPCLTANK2"].ToString().Trim();
                row["DATANK3"] = dt.Rows[i]["TPCLTANK3"].ToString().Trim();
                row["DATANK4"] = dt.Rows[i]["TPCLTANK4"].ToString().Trim();
                row["DATANK5"] = dt.Rows[i]["TPCLTANK5"].ToString().Trim();

                // (TANK1)
                if ("" != dt.Rows[i]["TPCLTANK1"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK1"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA1"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }
                // (TANK2)
                if ("" != dt.Rows[i]["TPCLTANK2"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK2"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0,6) , sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA2"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK3)
                if ("" != dt.Rows[i]["TPCLTANK3"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK3"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA3"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK4)
                if ("" != dt.Rows[i]["TPCLTANK4"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK4"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA4"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK5)
                if ("" != dt.Rows[i]["TPCLTANK5"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK5"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA5"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                row["DACLAMT1"] = double.Parse(dt.Rows[i]["TPCLAMT1"].ToString());
                row["DACLAMT2"] = double.Parse(dt.Rows[i]["TPCLAMT2"].ToString());
                row["DACLAMT3"] = double.Parse(dt.Rows[i]["TPCLAMT3"].ToString());
                row["DACLAMT4"] = double.Parse(dt.Rows[i]["TPCLAMT4"].ToString());
                row["DACLAMT5"] = double.Parse(dt.Rows[i]["TPCLAMT5"].ToString());

                row["CLSTAMT1"] = double.Parse(dt.Rows[i]["TPSTAMT1"].ToString());
                row["CLSTAMT2"] = double.Parse(dt.Rows[i]["TPSTAMT2"].ToString());
                row["CLSTAMT3"] = double.Parse(dt.Rows[i]["TPSTAMT3"].ToString());
                row["CLSTAMT4"] = double.Parse(dt.Rows[i]["TPSTAMT4"].ToString());
                row["CLSTAMT5"] = double.Parse(dt.Rows[i]["TPSTAMT5"].ToString());

                row["DAPSQTY1"] = double.Parse(dt.Rows[i]["TPPSQTY1"].ToString());
                row["DAPSQTY2"] = double.Parse(dt.Rows[i]["TPPSQTY2"].ToString());
                row["DAPSQTY3"] = double.Parse(dt.Rows[i]["TPPSQTY3"].ToString());
                row["DAPSQTY4"] = double.Parse(dt.Rows[i]["TPPSQTY4"].ToString());
                row["DAPSQTY5"] = double.Parse(dt.Rows[i]["TPPSQTY5"].ToString());

                double wk_DATOTCLAMT = double.Parse(dt.Rows[i]["TPCLAMT1"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPCLAMT2"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPCLAMT3"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPCLAMT4"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPCLAMT5"].ToString());

                row["DATOTCLAMT"] = wk_DATOTCLAMT;
                double dTPSTAMT_TOT = double.Parse(dt.Rows[i]["TPSTAMT1"].ToString()) +
                                      double.Parse(dt.Rows[i]["TPSTAMT2"].ToString()) +
                                      double.Parse(dt.Rows[i]["TPSTAMT3"].ToString()) +
                                      double.Parse(dt.Rows[i]["TPSTAMT4"].ToString()) +
                                      double.Parse(dt.Rows[i]["TPSTAMT5"].ToString());

                row["DATKCLAMT"] = wk_DATOTCLAMT + dTPSTAMT_TOT;
                row["CLSTAMTHAP"] = dTPSTAMT_TOT;
                

                double wk_DATOTPSQTY = double.Parse(dt.Rows[i]["TPPSQTY1"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPPSQTY2"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPPSQTY3"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPPSQTY4"].ToString()) +
                                       double.Parse(dt.Rows[i]["TPPSQTY5"].ToString());

                row["DATOTPSQTY"] = wk_DATOTPSQTY;

                wk_DAPSAMT = 0;
                if (0 != wk_DATOTPSQTY)
                {
                    row["DAPSQTY"] = wk_DATOTPSQTY;
                    row["DAPSDANGA"] = double.Parse(dt.Rows[i]["TPPSDANGA"].ToString());
                    wk_DAPSAMT = wk_DATOTPSQTY * double.Parse(dt.Rows[i]["TPPSDANGA"].ToString());
                    row["DAPSAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAPSAMT * 0.1))) * 10;
                }

                wk_DADMAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDMQTY"].ToString()))
                {
                    row["DADMQTY"] = double.Parse(dt.Rows[i]["TPDMQTY"].ToString());
                    row["DADMDANGA"] = double.Parse(dt.Rows[i]["TPDMDANGA"].ToString());
                    wk_DADMAMT = double.Parse(dt.Rows[i]["TPDMQTY"].ToString()) * double.Parse(dt.Rows[i]["TPDMDANGA"].ToString());
                    row["DADMAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DADMAMT * 0.1))) * 10;
                }
                double wk_DACLAMT = wk_DATOTCLAMT + dTPSTAMT_TOT + wk_DAPSAMT + wk_DADMAMT;
                row["DACLAMT"] = wk_DACLAMT;

                //---------------[ 전기료 ]---------------
                wk_DAELAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDAY"].ToString()))
                {

                    row["DAAMPRE"] = double.Parse(dt.Rows[i]["TPAMPRE"].ToString());
                    row["DADAY"] = double.Parse(dt.Rows[i]["TPDAY"].ToString());
                    row["DAELDANGA"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    double wk_DAELYUL = 0.9;
                    row["DAELYUL"] = wk_DAELYUL;
                    wk_DAELAMT = 1.73 * 0.380 * double.Parse(dt.Rows[i]["TPAMPRE"].ToString()) * 24
                        * double.Parse(dt.Rows[i]["TPDAY"].ToString())
                        * wk_DAELYUL * double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    row["DAELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10;
                }
                wk_DAELAMT1 = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDAY1"].ToString()))
                {
                    row["DAAMPRE1"] = double.Parse(dt.Rows[i]["TPAMPRE1"].ToString());
                    row["DADAY1"] = double.Parse(dt.Rows[i]["TPDAY1"].ToString());
                    row["DAELDANGA1"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    double wk_DAELYUL1 = 0.9;
                    row["DAELYUL1"] = wk_DAELYUL1;
                    wk_DAELAMT1 = 1.73 * 0.380 * double.Parse(dt.Rows[i]["TPAMPRE1"].ToString()) * 24
                        * double.Parse(dt.Rows[i]["TPDAY1"].ToString())
                        * wk_DAELYUL1 * double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    row["DAELAMT1"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10;
                }

                double wk_DATOTAMT = 0;
                if (sNEWHWAJU == "TYC")
                {
                    wk_DATOTAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        wk_DAJLAMT +
                        wk_DAGAAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DACLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10)
                        )));
                }
                else
                {
                    wk_DATOTAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAJLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAGAAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DACLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10)
                        )));
                }

                row["DATOTAMT"] = wk_DATOTAMT;

                dtRtn.Rows.Add(row);
            }

            return dtRtn;
        }
        #endregion

        #region Descripton : 201012월 이후 데이터 테이블 변경
        private DataTable Convert_DataTable_201012(DataTable dt)
        {
            DataTable dtRtn = new DataTable();
            DataTable dtEL = new DataTable();
            DataTable dtST = new DataTable();
            DataTable dtDK = new DataTable();
            DataTable dtTK = new DataTable();

            string sNEWYYMM = string.Empty;
            string sNEWHWAJU = string.Empty;

            double WKAMT = 0;
            double wk_DAUBKAMT = 0;
            double wk_DASELAMT = 0;
            double wk_DAPSAMT = 0;
            double wk_DADMAMT = 0;
            double wk_DAELAMT = 0;
            double wk_DAUELAMT = 0;
            double wk_DAELAMT1 = 0;
            double wk_DAUKYAMT = 0;
            double wk_DAUDKAMT = 0;
            double wk_DASBKAMT = 0;
            double wk_DASKYAMT = 0;

            dtRtn.Columns.Add("DAHWAJUNM", typeof(System.String));
            dtRtn.Columns.Add("DAHWAJU", typeof(System.String));
            dtRtn.Columns.Add("DASTYY", typeof(System.Double));
            dtRtn.Columns.Add("DASTMM", typeof(System.Double));
            dtRtn.Columns.Add("DASTDD", typeof(System.Double));
            dtRtn.Columns.Add("DAEDYY", typeof(System.Double));
            dtRtn.Columns.Add("DAEDMM", typeof(System.Double));
            dtRtn.Columns.Add("DAEDDD", typeof(System.Double));
            dtRtn.Columns.Add("DAUELTIME", typeof(System.Double));
            dtRtn.Columns.Add("DAUMOUTER", typeof(System.Double));
            dtRtn.Columns.Add("DAUYUL", typeof(System.Double));
            dtRtn.Columns.Add("DAUELECT", typeof(System.Double));
            dtRtn.Columns.Add("DAUELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUBKAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUKYAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAJLQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAJLDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAJLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASELTIME", typeof(System.Double));
            dtRtn.Columns.Add("DASMOTER", typeof(System.Double));
            dtRtn.Columns.Add("DASYUL", typeof(System.Double));
            dtRtn.Columns.Add("DASELECT", typeof(System.Double));
            dtRtn.Columns.Add("DASELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASBKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DASBKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DASBKAMT", typeof(System.Double));
            dtRtn.Columns.Add("DASKYQTY", typeof(System.Double));
            dtRtn.Columns.Add("DASKYDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DASKYAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAGAAMT", typeof(System.Double));
            dtRtn.Columns.Add("DATANK1", typeof(System.String));
            dtRtn.Columns.Add("DACAPA1", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT1", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY1", typeof(System.Double));
            dtRtn.Columns.Add("DATANK2", typeof(System.String));
            dtRtn.Columns.Add("DACAPA2", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT2", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY2", typeof(System.Double));
            dtRtn.Columns.Add("DATANK3", typeof(System.String));
            dtRtn.Columns.Add("DACAPA3", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT3", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY3", typeof(System.Double));
            dtRtn.Columns.Add("DATANK4", typeof(System.String));
            dtRtn.Columns.Add("DACAPA4", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT4", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY4", typeof(System.Double));
            dtRtn.Columns.Add("DATANK5", typeof(System.String));
            dtRtn.Columns.Add("DACAPA5", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT5", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY5", typeof(System.Double));
            dtRtn.Columns.Add("DATOTCLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DATOTPSQTY", typeof(System.Double));
            dtRtn.Columns.Add("DATKCLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAPSQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAPSDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAPSAMT", typeof(System.Double));
            dtRtn.Columns.Add("DADMQTY", typeof(System.Double));
            dtRtn.Columns.Add("DADMDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DADMAMT", typeof(System.Double));
            dtRtn.Columns.Add("DACLAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAAMPRE", typeof(System.Double));
            dtRtn.Columns.Add("DADAY", typeof(System.Double));
            dtRtn.Columns.Add("DAELYUL", typeof(System.Double));
            dtRtn.Columns.Add("DAELDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAELAMT", typeof(System.Double));
            dtRtn.Columns.Add("DAAMPRE1", typeof(System.Double));
            dtRtn.Columns.Add("DADAY1", typeof(System.Double));
            dtRtn.Columns.Add("DAELYUL1", typeof(System.Double));
            dtRtn.Columns.Add("DAELDANGA1", typeof(System.Double));
            dtRtn.Columns.Add("DAELAMT1", typeof(System.Double));
            dtRtn.Columns.Add("DATOTAMT", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT1", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT2", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT3", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT4", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMT5", typeof(System.Double));
            dtRtn.Columns.Add("CLSTAMTHAP", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMTHAP", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKQTY", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKDANGA", typeof(System.Double));
            dtRtn.Columns.Add("DAUDKAMT", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT1", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT2", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT3", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT4", typeof(System.Double));
            dtRtn.Columns.Add("CLELAMT5", typeof(System.Double));

            dtRtn.Columns.Add("LPUSAMT", typeof(System.Double));
            dtRtn.Columns.Add("LPELAMT", typeof(System.Double));
            dtRtn.Columns.Add("LPREPAIRAMT", typeof(System.Double));
            dtRtn.Columns.Add("LPGITAAMT", typeof(System.Double));
            dtRtn.Columns.Add("LPTOTAMT", typeof(System.Double));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dtRtn.NewRow();

                sNEWYYMM = this.DTP01_YYYYMM.GetString().Substring(0, 6);        // 처리년월
                sNEWHWAJU = dt.Rows[i]["TPHWAJU"].ToString();    // 화주

                row["DAHWAJUNM"] = dt.Rows[i]["HWAJUNM"].ToString();               // 화주
                row["DAHWAJU"] = dt.Rows[i]["TPHWAJU"].ToString();               // 화주
                row["DASTYY"] = dt.Rows[i]["PDATE"].ToString().Substring(0, 4);  // 시작 년
                row["DASTMM"] = dt.Rows[i]["PDATE"].ToString().Substring(5, 2);  // 시작 월
                row["DASTDD"] = dt.Rows[i]["PDATE"].ToString().Substring(8, 2);  // 시작 일
                row["DAEDYY"] = dt.Rows[i]["PDATE"].ToString().Substring(11, 4); // 종료 년
                row["DAEDMM"] = dt.Rows[i]["PDATE"].ToString().Substring(16, 2); // 종료 월
                row["DAEDDD"] = dt.Rows[i]["PDATE"].ToString().Substring(19, 2); // 종료 일

                //---------------[ 질소사용료 ]---------------
                row["DAJLQTY"] = double.Parse(dt.Rows[i]["TPJLQTY"].ToString());   // 수량
                row["DAJLDANGA"] = double.Parse(dt.Rows[i]["TPJLDANGA"].ToString()); // 단가
                row["DAJLAMT"] = double.Parse(dt.Rows[i]["TPJLAMT"].ToString());   // 금액
                double wk_DAJLAMT = double.Parse(dt.Rows[i]["TPJLAMT"].ToString());   // 금액

                #region Description : 가열료

                #region Description : 상하단지

                wk_DAUELAMT = 0;

                //---------------[ 전기료 ]---------------
                if (0 != double.Parse(dt.Rows[i]["TPUELTIME"].ToString()))
                {
                    double daueltim = 0;
                    double daumoter = 0;
                    double dauyul = 0;
                    double dauelect = 0;

                    row["DAUELTIME"] = double.Parse(dt.Rows[i]["TPUELTIME"].ToString());
                    row["DAUMOUTER"] = double.Parse(dt.Rows[i]["DNMOTER1"].ToString());

                    daueltim = double.Parse(dt.Rows[i]["TPUELTIME"].ToString());
                    daumoter = double.Parse(dt.Rows[i]["DNMOTER1"].ToString());

                    if (double.Parse(sNEWYYMM) >= 201012)
                    {
                        // 전기료 단가 
                        if (double.Parse(dt.Rows[i]["DNSELDANGA"].ToString()) != 0)
                        {
                            row["DAUYUL"] = 0;
                            row["DAUELECT"] = double.Parse(dt.Rows[i]["DNSELECT"].ToString());

                            dauyul = 0;
                            dauelect = double.Parse(dt.Rows[i]["DNSELECT"].ToString());
                        }
                        else
                        {
                            row["DAUYUL"] = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                            row["DAUELECT"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());

                            dauyul = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                            dauelect = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                        }
                    }
                    else
                    {
                        row["DAUYUL"] = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                        row["DAUELECT"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());

                        dauyul = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                        dauelect = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    }

                    if (sNEWHWAJU == "TYC")
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6BMDJ824", sNEWYYMM.ToString(),
                                                                    sNEWHWAJU.ToString().ToUpper()
                                                                    );

                        dtEL = this.DbConnector.ExecuteDataTable();

                        WKAMT = 0;

                        if (dtEL.Rows.Count > 0)
                        {
                            wk_DAUELAMT = double.Parse(Get_Numeric(dtEL.Rows[0]["GAELAMT"].ToString()));
                            row["DAUELAMT"] = double.Parse(Get_Numeric(dtEL.Rows[0]["GAELAMT"].ToString()));
                        }
                    }
                    else
                    {
                        if (double.Parse(dt.Rows[i]["DNSELDANGA"].ToString()) != 0)
                        {
                            // 상단지 전기료 = 전기 사용시간 * UTILITY에 등록된 전기단가
                            wk_DAUELAMT = double.Parse(UP_DotDelete(Convert.ToString(daueltim * double.Parse(dt.Rows[i]["DNSELDANGA"].ToString()))));
                            row["DAUELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUELAMT * 0.1))) * 10;
                        }
                        else
                        {
                            wk_DAUELAMT = double.Parse(UP_DotDelete(Convert.ToString(daueltim * daumoter * dauyul * dauelect)));
                            row["DAUELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUELAMT * 0.1))) * 10;
                        }
                    }
                }

                //---------------[ 스팀 ]---------------
                // 월집계가 없을때는 계산하여 처리 한다.
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6BMDK825", sNEWYYMM.ToString(),
                                                            sNEWHWAJU.ToString().ToUpper()
                                                            );

                dtST = this.DbConnector.ExecuteDataTable();

                WKAMT = 0;

                if (dtST.Rows.Count > 0)
                {
                    WKAMT = double.Parse(Get_Numeric(dtST.Rows[0]["TTAMT"].ToString()));
                }

                wk_DAUBKAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPUBKQTY"].ToString()) || 0 != double.Parse(dt.Rows[i]["TPUSTTIME"].ToString()))
                {
                    double wkdaubkqty = 0;
                    double wkdaubkdanga = 0;

                    if (double.Parse(sNEWYYMM) >= 201012)
                    {
                        row["DAUBKQTY"] = double.Parse(dt.Rows[i]["TPUSTTIME"].ToString());
                        row["DAUBKDANGA"] = double.Parse(dt.Rows[i]["DNSTDANGA"].ToString());

                        wkdaubkqty = double.Parse(dt.Rows[i]["TPUSTTIME"].ToString());
                        wkdaubkdanga = double.Parse(dt.Rows[i]["DNSTDANGA"].ToString());
                    }
                    else
                    {
                        row["DAUBKQTY"] = double.Parse(dt.Rows[i]["TPUBKQTY"].ToString());
                        row["DAUBKDANGA"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());

                        wkdaubkqty = double.Parse(dt.Rows[i]["TPUBKQTY"].ToString());
                        wkdaubkdanga = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());
                    }

                    if (WKAMT == (wkdaubkqty * wkdaubkdanga))
                    {
                        if (sNEWHWAJU == "TYC")
                        {
                            wk_DAUBKAMT = WKAMT;
                            row["DAUBKAMT"] = wk_DAUBKAMT;
                        }
                        else
                        {
                            wk_DAUBKAMT = double.Parse(UP_DotDelete(Convert.ToString(wkdaubkqty * wkdaubkdanga)));
                            row["DAUBKAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUBKAMT * 0.1))) * 10;
                        }
                    }
                    else
                    {
                        wk_DAUBKAMT = WKAMT;
                        row["DAUBKAMT"] = wk_DAUBKAMT;
                    }
                }

                //---------------[ 경유 ]---------------
                wk_DAUKYAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPUKYQTY"].ToString()))
                {
                    row["DAUKYQTY"] = double.Parse(dt.Rows[i]["TPUKYQTY"].ToString());
                    row["DAUKYDANGA"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());
                    double wkdaukqty = double.Parse(dt.Rows[i]["TPUKYQTY"].ToString());
                    double wkdaukydanga = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());
                    wk_DAUKYAMT = double.Parse(UP_DotDelete(Convert.ToString(wkdaukqty * wkdaukydanga)));
                    row["DAUKYAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10;
                }

                //---------------[ 등유 ]---------------
                // 월집계가 없을때는 계산하여 처리 한다.
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6BMHZ833", sNEWYYMM.ToString(),
                                                            sNEWHWAJU.ToString().ToUpper()
                                                            );

                dtDK = this.DbConnector.ExecuteDataTable();

                WKAMT = 0;

                if (dtDK.Rows.Count > 0)
                {
                    WKAMT = double.Parse(Get_Numeric(dtDK.Rows[0]["DKAMT"].ToString()));
                }

                wk_DAUDKAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPUDKQTY"].ToString()))
                {
                    row["DAUDKQTY"] = double.Parse(dt.Rows[i]["TPUDKQTY"].ToString());
                    row["DAUDKDANGA"] = double.Parse(dt.Rows[i]["DNDKCU"].ToString());

                    double wkdaudkqty = double.Parse(dt.Rows[i]["TPUDKQTY"].ToString());
                    double wkdaudkdanga = double.Parse(dt.Rows[i]["DNDKCU"].ToString());

                    if (WKAMT == (wkdaudkqty * wkdaudkdanga))
                    {
                        if (sNEWHWAJU == "TYC")
                        {
                            wk_DAUDKAMT = WKAMT;
                            row["DAUDKAMT"] = wk_DAUDKAMT;
                        }
                        else
                        {
                            wk_DAUDKAMT = double.Parse(UP_DotDelete(Convert.ToString(wkdaudkqty * wkdaudkdanga)));
                            row["DAUDKAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAUDKAMT * 0.1))) * 10;
                        }
                    }
                    else
                    {
                        wk_DAUDKAMT = WKAMT;
                        row["DAUDKAMT"] = wk_DAUDKAMT;
                    }
                }

                #endregion

                #region Description : 송유단지

                //---------------[ 전기료 ]---------------
                wk_DASELAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSELTIME"].ToString()))
                {
                    row["DASELTIME"] = double.Parse(dt.Rows[i]["TPSELTIME"].ToString());
                    row["DASMOTER"] = double.Parse(dt.Rows[i]["DNMOTER2"].ToString());
                    row["DASYUL"] = double.Parse(dt.Rows[i]["DNYUL"].ToString());       //
                    row["DASELECT"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());     //
                    double daseltim = double.Parse(dt.Rows[i]["TPSELTIME"].ToString());
                    double dasumoter = double.Parse(dt.Rows[i]["DNMOTER2"].ToString());
                    double dasuyul = double.Parse(dt.Rows[i]["DNYUL"].ToString());
                    double dasuelect = double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    wk_DASELAMT = double.Parse(Set_Numeric2(dt.Rows[i]["GAELAMT"].ToString(),2));
                    row["DASELAMT"] = wk_DASELAMT;
                }

                //---------------[ 벙커씨유 ]---------------
                wk_DASBKAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSBKQTY"].ToString()))
                {
                    row["DASBKQTY"] = double.Parse(dt.Rows[i]["TPSBKQTY"].ToString());
                    row["DASBKDANGA"] = double.Parse(dt.Rows[i]["DNBKCU"].ToString());
                    wk_DASBKAMT = (double.Parse(dt.Rows[i]["TPSBKQTY"].ToString())) * (double.Parse(dt.Rows[i]["DNBKCU"].ToString()));
                    row["DASBKAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10;
                }

                //---------------[ 경유 ]---------------
                wk_DASKYAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPSKYQTY"].ToString()))
                {
                    row["DASKYQTY"] = double.Parse(dt.Rows[i]["TPSKYQTY"].ToString());
                    row["DASKYDANGA"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());

                    wk_DASKYAMT = double.Parse(Set_Numeric2(dt.Rows[i]["GAKYAMT"].ToString(),2));
                    row["DASKYAMT"] = wk_DASKYAMT;
                }

                #endregion

                #endregion

                double wk_DAGAAMT = 0;
                // 합계
                if (sNEWHWAJU == "TYC")
                {
                    wk_DAGAAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        wk_DAUELAMT +
                        wk_DAUBKAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASKYAMT * 0.1))) * 10) +
                        wk_DAUDKAMT
                        )));
                }
                else
                {
                    wk_DAGAAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUBKAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUKYAMT * 0.1))) * 10) +
                        wk_DASELAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DASBKAMT * 0.1))) * 10) +
                        wk_DASKYAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAUDKAMT * 0.1))) * 10)
                        )));

                }

                row["DAGAAMT"] = wk_DAGAAMT;

                //---------------[ 세척료 ]---------------

                row["DATANK1"] = dt.Rows[i]["TPCLTANK1"].ToString().Trim();
                row["DATANK2"] = dt.Rows[i]["TPCLTANK2"].ToString().Trim();
                row["DATANK3"] = dt.Rows[i]["TPCLTANK3"].ToString().Trim();
                row["DATANK4"] = dt.Rows[i]["TPCLTANK4"].ToString().Trim();
                row["DATANK5"] = dt.Rows[i]["TPCLTANK5"].ToString().Trim();

                // (TANK1)
                if ("" != dt.Rows[i]["TPCLTANK1"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK1"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA1"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }
                // (TANK2)
                if ("" != dt.Rows[i]["TPCLTANK2"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK2"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA2"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK3)
                if ("" != dt.Rows[i]["TPCLTANK3"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK3"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA3"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK4)
                if ("" != dt.Rows[i]["TPCLTANK4"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK4"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    dtTK = this.DbConnector.ExecuteDataTable();

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA4"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                // (TANK5)
                if ("" != dt.Rows[i]["TPCLTANK5"].ToString())
                {
                    string sTANK = dt.Rows[i]["TPCLTANK5"].ToString().Trim();

                    this.DbConnector.CommandClear();

                    //this.DbConnector.Attach("TY_P_UT_6BMDL826", sTANK);
                    this.DbConnector.Attach("TY_P_UT_C2LBN087", this.DTP01_YYYYMM.GetString().Substring(0, 6), sTANK);

                    if (dtTK.Rows.Count > 0)
                    {
                        row["DACAPA5"] = double.Parse(Get_Numeric(dtTK.Rows[0]["YOCAPA"].ToString()));
                    }
                }

                row["DACLAMT1"] = double.Parse(dt.Rows[i]["TPCLAMT1"].ToString());
                row["DACLAMT2"] = double.Parse(dt.Rows[i]["TPCLAMT2"].ToString());
                row["DACLAMT3"] = double.Parse(dt.Rows[i]["TPCLAMT3"].ToString());
                row["DACLAMT4"] = double.Parse(dt.Rows[i]["TPCLAMT4"].ToString());
                row["DACLAMT5"] = double.Parse(dt.Rows[i]["TPCLAMT5"].ToString());

                row["CLSTAMT1"] = double.Parse(dt.Rows[i]["TPSTAMT1"].ToString());
                row["CLSTAMT2"] = double.Parse(dt.Rows[i]["TPSTAMT2"].ToString());
                row["CLSTAMT3"] = double.Parse(dt.Rows[i]["TPSTAMT3"].ToString());
                row["CLSTAMT4"] = double.Parse(dt.Rows[i]["TPSTAMT4"].ToString());
                row["CLSTAMT5"] = double.Parse(dt.Rows[i]["TPSTAMT5"].ToString());

                row["CLELAMT1"] = double.Parse(dt.Rows[i]["TPELAMT1"].ToString());
                row["CLELAMT2"] = double.Parse(dt.Rows[i]["TPELAMT2"].ToString());
                row["CLELAMT3"] = double.Parse(dt.Rows[i]["TPELAMT3"].ToString());
                row["CLELAMT4"] = double.Parse(dt.Rows[i]["TPELAMT4"].ToString());
                row["CLELAMT5"] = double.Parse(dt.Rows[i]["TPELAMT5"].ToString());

                row["DAPSQTY1"] = double.Parse(dt.Rows[i]["TPPSQTY1"].ToString());
                row["DAPSQTY2"] = double.Parse(dt.Rows[i]["TPPSQTY2"].ToString());
                row["DAPSQTY3"] = double.Parse(dt.Rows[i]["TPPSQTY3"].ToString());
                row["DAPSQTY4"] = double.Parse(dt.Rows[i]["TPPSQTY4"].ToString());
                row["DAPSQTY5"] = double.Parse(dt.Rows[i]["TPPSQTY5"].ToString());

                double wk_DATOTCLAMT = double.Parse(dt.Rows[i]["TPCLAMT1"].ToString()) +
                    double.Parse(dt.Rows[i]["TPCLAMT2"].ToString()) +
                    double.Parse(dt.Rows[i]["TPCLAMT3"].ToString()) +
                    double.Parse(dt.Rows[i]["TPCLAMT4"].ToString()) +
                    double.Parse(dt.Rows[i]["TPCLAMT5"].ToString());

                row["DATOTCLAMT"] = wk_DATOTCLAMT;
                double dTPSTAMT_TOT = double.Parse(dt.Rows[i]["TPSTAMT1"].ToString()) +
                    double.Parse(dt.Rows[i]["TPSTAMT2"].ToString()) +
                    double.Parse(dt.Rows[i]["TPSTAMT3"].ToString()) +
                    double.Parse(dt.Rows[i]["TPSTAMT4"].ToString()) +
                    double.Parse(dt.Rows[i]["TPSTAMT5"].ToString());

                double dTPELAMT_TOT = double.Parse(dt.Rows[i]["TPELAMT1"].ToString()) +
                    double.Parse(dt.Rows[i]["TPELAMT2"].ToString()) +
                    double.Parse(dt.Rows[i]["TPELAMT3"].ToString()) +
                    double.Parse(dt.Rows[i]["TPELAMT4"].ToString()) +
                    double.Parse(dt.Rows[i]["TPELAMT5"].ToString());

                row["DATKCLAMT"] = wk_DATOTCLAMT + dTPSTAMT_TOT + dTPELAMT_TOT;
                row["CLSTAMTHAP"] = dTPSTAMT_TOT;
                row["CLELAMTHAP"] = dTPELAMT_TOT;

                double wk_DATOTPSQTY = double.Parse(dt.Rows[i]["TPPSQTY1"].ToString()) +
                    double.Parse(dt.Rows[i]["TPPSQTY2"].ToString()) +
                    double.Parse(dt.Rows[i]["TPPSQTY3"].ToString()) +
                    double.Parse(dt.Rows[i]["TPPSQTY4"].ToString()) +
                    double.Parse(dt.Rows[i]["TPPSQTY5"].ToString());

                row["DATOTPSQTY"] = wk_DATOTPSQTY;

                wk_DAPSAMT = 0;
                if (0 != wk_DATOTPSQTY)
                {
                    row["DAPSQTY"] = wk_DATOTPSQTY;
                    row["DAPSDANGA"] = double.Parse(dt.Rows[i]["TPPSDANGA"].ToString());
                    wk_DAPSAMT = wk_DATOTPSQTY * double.Parse(dt.Rows[i]["TPPSDANGA"].ToString());
                    row["DAPSAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAPSAMT * 0.1))) * 10;
                }

                wk_DADMAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDMQTY"].ToString()))
                {
                    row["DADMQTY"] = double.Parse(dt.Rows[i]["TPDMQTY"].ToString());
                    row["DADMDANGA"] = double.Parse(dt.Rows[i]["TPDMDANGA"].ToString());
                    wk_DADMAMT = double.Parse(dt.Rows[i]["TPDMQTY"].ToString()) * double.Parse(dt.Rows[i]["TPDMDANGA"].ToString());
                    row["DADMAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DADMAMT * 0.1))) * 10;
                }
                double wk_DACLAMT = wk_DATOTCLAMT + dTPSTAMT_TOT + wk_DAPSAMT + wk_DADMAMT;
                row["DACLAMT"] = wk_DACLAMT;

                //---------------[ 전기료 ]---------------
                wk_DAELAMT = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDAY"].ToString()))
                {

                    row["DAAMPRE"] = double.Parse(dt.Rows[i]["TPAMPRE"].ToString());
                    row["DADAY"] = double.Parse(dt.Rows[i]["TPDAY"].ToString());
                    row["DAELDANGA"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    double wk_DAELYUL = 0.9;
                    row["DAELYUL"] = wk_DAELYUL;
                    wk_DAELAMT = 1.73 * 0.380 * double.Parse(dt.Rows[i]["TPAMPRE"].ToString()) * 24
                        * double.Parse(dt.Rows[i]["TPDAY"].ToString())
                        * wk_DAELYUL * double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    row["DAELAMT"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10;
                }
                wk_DAELAMT1 = 0;
                if (0 != double.Parse(dt.Rows[i]["TPDAY1"].ToString()))
                {
                    row["DAAMPRE1"] = double.Parse(dt.Rows[i]["TPAMPRE1"].ToString());
                    row["DADAY1"] = double.Parse(dt.Rows[i]["TPDAY1"].ToString());
                    row["DAELDANGA1"] = double.Parse(dt.Rows[i]["DNELECT"].ToString());
                    double wk_DAELYUL1 = 0.9;
                    row["DAELYUL1"] = wk_DAELYUL1;
                    wk_DAELAMT1 = 1.73 * 0.380 * double.Parse(dt.Rows[i]["TPAMPRE1"].ToString()) * 24
                        * double.Parse(dt.Rows[i]["TPDAY1"].ToString())
                        * wk_DAELYUL1 * double.Parse(dt.Rows[i]["DNELECT"].ToString());

                    row["DAELAMT1"] = double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10;
                }

                // LPG 사용료

                row["LPUSAMT"] = double.Parse(dt.Rows[i]["LPUSAMT"].ToString());
                row["LPELAMT"] = double.Parse(dt.Rows[i]["LPELAMT"].ToString());
                row["LPREPAIRAMT"] = double.Parse(dt.Rows[i]["LPREPAIRAMT"].ToString());
                row["LPGITAAMT"] = double.Parse(dt.Rows[i]["LPGITAAMT"].ToString());
                row["LPTOTAMT"] = double.Parse(dt.Rows[i]["LPTOTAMT"].ToString());

                // 총계
                double wk_DATOTAMT = 0;
                if (sNEWHWAJU == "TYC")
                {
                    wk_DATOTAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        wk_DAJLAMT +
                        wk_DAGAAMT +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DACLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10)
                        ))) + Convert.ToDouble(double.Parse(dt.Rows[i]["LPTOTAMT"].ToString()));
                }
                else
                {
                    wk_DATOTAMT = double.Parse(UP_DotDelete(Convert.ToString
                        (
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAJLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAGAAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DACLAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT * 0.1))) * 10) +
                        (double.Parse(UP_DotDelete(Convert.ToString(wk_DAELAMT1 * 0.1))) * 10)
                        ))) + Convert.ToDouble(double.Parse(dt.Rows[i]["LPTOTAMT"].ToString()));
                }
                

                row["DATOTAMT"] = wk_DATOTAMT;

                dtRtn.Rows.Add(row);
            }

            return dtRtn;
        }
        #endregion

        #region Description : 질소사용량 조회
        private DataTable Get_JilsoData()
        {
            DataTable rtnDt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_A32G6977", this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                        this.CBH01_EMHWAJU.GetValue().ToString()
                                                        );

            rtnDt = this.DbConnector.ExecuteDataTable();

            return rtnDt;
        }
        #endregion

        #region Description : 경로지정 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
        #endregion

        #region Description : 다운로드 버튼
        private void BTN61_DOWN_Click(object sender, EventArgs e)
        {
            if (txtFolder.Text.Trim() == "")
            {
                this.ShowMessage("TY_M_UT_81PBD533");
            }
            else
            {
                string sGUBUN = string.Empty;
                string sFileName = string.Empty;

                int stmm = 0;
                int styy = 0;

                int stdd = 26;
                int eddd = 25;

                int styymm = 0;
                int edyymm = 0;

                int i = 0;

                styymm = int.Parse(this.DTP01_YYYYMM.GetString());
                edyymm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString()));

                styy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
                stmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

                stmm = stmm - 1;
                if (stmm == 0)
                {
                    styy = styy - 1;
                    stmm = 12;
                }

                string wstyymmdd = Convert.ToString(styy) + '-' + Set_Fill2(Convert.ToString(stmm)) + '-' + Convert.ToString(stdd);
                string ssdyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
                string ssdmm = this.DTP01_YYYYMM.GetString().Substring(4, 2);
                string wedyymmdd = ssdyy + '-' + ssdmm + '-' + Convert.ToString(eddd);

                string sqryyymm = this.DTP01_YYYYMM.GetString().Substring(0, 6);

                // --------------- 마지막 일자  ---------------

                int istyy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
                int istmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

                string edstyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
                string edstmm = Set_Fill2(this.DTP01_YYYYMM.GetString().Substring(4, 2));
                string edstdd = "01";

                string wstyymmdd2 = edstyy + edstmm + edstdd; // 해당월 처리 화주 01일~ 
                string ededdd = System.DateTime.DaysInMonth(istyy, istmm).ToString();//해당월의 마지막 일자 구하기
                string wedyymmdd2 = edstyy + edstmm + ededdd; // ~ 마지막일 까지

                wstyymmdd2 = wstyymmdd2.Substring(0, 4) + "-" + wstyymmdd2.Substring(4, 2) + "-" + wstyymmdd2.Substring(6, 2);
                wedyymmdd2 = wedyymmdd2.Substring(0, 4) + "-" + wedyymmdd2.Substring(4, 2) + "-" + wedyymmdd2.Substring(6, 2);

                DataTable dt_hwaju = new DataTable();
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7CCFV251", this.CBH01_EMHWAJU.GetValue().ToString());

                dt_hwaju = this.DbConnector.ExecuteDataTable();

                for (i = 0; i < dt_hwaju.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_95OBL632", wstyymmdd + "-" + wedyymmdd,
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                dt_hwaju.Rows[i]["TPHWAJU"].ToString(),
                                                                wstyymmdd2 + "-" + wedyymmdd2,
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                                dt_hwaju.Rows[i]["TPHWAJU"].ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    DataTable dtJilso = Get_JilsoData();

                    if (dt.Rows.Count > 0)
                    {
                        if (double.Parse(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 202201)
                        {
                            ActiveReport rpt = new TYUTIL014R3(dtJilso);

                            sFileName = dt.Rows[0]["HWAJUNM"].ToString() + "-" + dt_hwaju.Rows[i]["TPHWAJU"].ToString() + "-UTILITY사용내역.pdf";

                            UP_Invoice_PdfFileDown(rpt, Convert_DataTable_201012(dt), this.DTP01_YYYYMM.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                        else if(double.Parse(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 201012)
                        {
                            ActiveReport rpt = new TYUTIL014R2();

                            sFileName = dt.Rows[0]["HWAJUNM"].ToString() + "-" + dt_hwaju.Rows[i]["TPHWAJU"].ToString() + "-UTILITY사용내역.pdf";

                            UP_Invoice_PdfFileDown(rpt, Convert_DataTable_201012(dt), this.DTP01_YYYYMM.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                        else
                        {
                            ActiveReport rpt = new TYUTIL014R1();

                            sFileName = dt.Rows[0]["HWAJUNM"].ToString() + "-" + dt_hwaju.Rows[i]["TPHWAJU"].ToString() + "-UTILITY사용내역.pdf";

                            UP_Invoice_PdfFileDown(rpt, Convert_DataTable(dt), this.DTP01_YYYYMM.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                    }
                }

                if (sGUBUN.ToString() != "")
                {
                    this.ShowMessage("TY_M_UT_7CCDS246");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion

        #region Description : pdf 다운로드
        private void UP_Invoice_PdfFileDown(ActiveReport rpt, DataTable dt, string sYYMM, string sFileName)
        {
            DataDynamics.ActiveReports.Document.Document doc;

            try
            {
                fsFileDownPath = txtFolder.Text + "\\" + sYYMM + "\\";

                if (Directory.Exists(fsFileDownPath) == false)
                {
                    Directory.CreateDirectory(fsFileDownPath);
                }

                rpt.DataSource = dt;
                rpt.Run(false);

                string sfilename = fsFileDownPath + "\\" + sFileName;

                object export = null;

                doc = rpt.Document;

                export = new PdfExport();

                ((PdfExport)export).Export(doc, sfilename);
            }
            catch
            {

            }
            finally
            {

            }
        }
        #endregion
    }
}
