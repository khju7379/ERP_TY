using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대차대조표 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.24 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25M2N592 : 대차대조표 조회 및 출력
    ///  TY_P_GB_2423M259 : 코드박스-공통코드
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25O3P637 : 대차대조표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  GBENO : 비교년월-기
    ///  GCUNO : 기준년월-기
    /// </summary>
    public partial class TYACBJ010S : TYBase
    {
        private string _sA1LVAC = string.Empty;

        private int _iA1LVAC2 = 0;
        private int _iA1LVAC3 = 0;
        private int _iA1LVAC4 = 0;
        private int _iA1LVAC5 = 0;
        private int _iA1LVAC6 = 0;

        private double _fd33070200_HC = 0;
        private double _fd33070200_OC = 0;

        private double _fd당기대변금액 = 0;
        private double _fd전기대변금액 = 0;

        private double _fd당기부채자본금액 = 0;
        private double _fd전기부채자본금액 = 0;

        private double _d당기부채자본금액 = 0;
        private double _d전기부채자본금액 = 0;

        // 당기
        private double _fdHRDRAMT = 0;

        // 전기
        private double _fdORDRAMT = 0;

        // 당기
        private double _fdHRCRAMT = 0;

        // 전기
        private double _fdORCRAMT = 0;

        private string fsGSTDATE = string.Empty;
        private string fsGEDDATE = string.Empty;

        #region Description : 페이지 로드
        public TYACBJ010S()
        {
            InitializeComponent();
        }

        private void TYACBJ010S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GCUNO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            int iDANGGISU = 0;
            string sDATE  = string.Empty;

            string sYear  = string.Empty;
            string sMonth = string.Empty;
            int iDD = 0;

            sYear  = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD    = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            // 기준년월
            fsGSTDATE = this.DTP01_GSTDATE.GetValue().ToString() + Convert.ToString(iDD);



            sYear  = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD    = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            // 비교년월
            fsGEDDATE = this.DTP01_GEDDATE.GetValue().ToString() + Convert.ToString(iDD);

            DataTable dz = new DataTable();

            // 당기기수 가져오기
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_GB_2423M259",
                "FS",
                "",
                ""
                );

            dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count > 0)
            {
                iDANGGISU = int.Parse(dz.Rows[0][1].ToString()) - 1;

                this.TXT01_GCUNO.SetValue(Convert.ToString(int.Parse(this.fsGSTDATE.ToString().Substring(0, 4)) - iDANGGISU));
            }

            if (this.TXT01_GBENO.GetValue().ToString() == "")
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.TXT01_GCUNO.GetValue().ToString()) - 1));

                sDATE = Convert.ToString(int.Parse(this.fsGSTDATE.ToString().Substring(0, 4)) - 1) + "1231";

                this.DTP01_GEDDATE.SetValue(sDATE.ToString());

                // 비교년월
                fsGEDDATE = sDATE.ToString();
            }
            else
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.fsGEDDATE.ToString().Substring(0, 4)) - iDANGGISU));
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25M2N592",
                this.TXT01_GCUNO.GetValue(),
                fsGSTDATE.ToString(),
                this.TXT01_GBENO.GetValue(),
                fsGEDDATE.ToString(),
                ""
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_25O3P637.SetValue(UP_ConvertDt(dt));
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            DataTable Retdt   = new DataTable();
            
            string _sA1TAG10  = string.Empty;

            string sNEWA1CDAC = string.Empty;
            string sOLDA1CDAC = string.Empty;

            string sNEWA1ABAC = string.Empty;
            string sOLDA1ABAC = string.Empty;

            double dOUT_HCRAMT = 0;
            double dOUT_OCRAMT = 0;

            double dHCRAMT    = 0;
            double dOCRAMT    = 0;

            _fd당기대변금액     = 0;
            _fd전기대변금액     = 0;

            _d당기부채자본금액  = 0;
            _d전기부채자본금액  = 0;

            _fd당기부채자본금액 = 0;
            _fd전기부채자본금액 = 0;

            DataTable Finaldt = new DataTable();

            string sA1CDAC  = string.Empty;
            string sA1ABAC  = string.Empty;
            string sHRDRAMT = string.Empty;
            string sHRCRAMT = string.Empty;
            string sORDRAMT = string.Empty;
            string sORCRAMT = string.Empty;

            DataTable ConDt = new DataTable();

            ConDt = dt;

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["A1CDAC"]) == "20000000" || Convert.ToString(dr["A1CDAC"]) == "30000000")
                {
                    this._d당기부채자본금액 = this._d당기부채자본금액 + double.Parse(dr["HCRAMT"].ToString());
                    this._d전기부채자본금액 = this._d전기부채자본금액 + double.Parse(dr["OCRAMT"].ToString());
                }

                if (Convert.ToString(dr["A1CDAC"]) == "36200200") // 33070200 (당기순이익)
                {
                    this._fd33070200_HC = this._fd33070200_HC + double.Parse(dr["HCRAMT"].ToString());
                    this._fd33070200_OC = this._fd33070200_OC + double.Parse(dr["OCRAMT"].ToString());
                }
            }

            this._fd당기부채자본금액 = this._d당기부채자본금액 + this._fd33070200_HC;

            this._fd전기부채자본금액 = this._d전기부채자본금액 + this._fd33070200_OC;

            DataRow row;

            Retdt.Columns.Add("A1CDAC",  typeof(System.String));
            Retdt.Columns.Add("A1LVAC",  typeof(System.String));
            Retdt.Columns.Add("A1ABAC",  typeof(System.String));
            Retdt.Columns.Add("A1TAG10", typeof(System.String));
            Retdt.Columns.Add("A1YNBS",  typeof(System.String));
            Retdt.Columns.Add("A1YNTB",  typeof(System.String));
            Retdt.Columns.Add("HDRAMT",  typeof(System.String));
            Retdt.Columns.Add("HCRAMT",  typeof(System.String));
            Retdt.Columns.Add("ODRAMT",  typeof(System.String));
            Retdt.Columns.Add("OCRAMT",  typeof(System.String));
            Retdt.Columns.Add("DSP",     typeof(System.String)); // A1TAG10 = 1이면서 충당금계정이 0일 경우 대변에 DISPLAY하기위한 필드

            for (int i = 0; i <= ConDt.Rows.Count - 1; i++)
            {
                sNEWA1CDAC = Convert.ToString(ConDt.Rows[i]["A1CDAC"]);
                sNEWA1ABAC = ConDt.Rows[i]["A1ABAC"].ToString();

                // 반제 계정일 경우
                //if (Convert.ToString(ConDt.Rows[i]["A1TAG10"]) == "2")
                //{
                //    // 당기
                //    _fd당기대변금액 = _fdHRDRAMT - double.Parse(ConDt.Rows[i]["HDRAMT"].ToString());
                //    // 전기
                //    _fd전기대변금액 = _fdORDRAMT - double.Parse(ConDt.Rows[i]["ODRAMT"].ToString());
                //}
                //else
                if (sNEWA1CDAC.ToString() == "36200200") // 33070200 (당기순이익)
                {
                    _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                    _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }
                else if (sNEWA1CDAC.ToString() == "30000000" || sNEWA1CDAC.ToString() == "33000000") // || sNEWA1CDAC.ToString() == "33070000")
                {
                    _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString()) + _fd33070200_HC;

                    _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString()) + _fd33070200_OC;
                }
                else
                {
                    _fd당기대변금액 = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                    _fd전기대변금액 = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());
                }

                if (
                      (ConDt.Rows[i]["A1YNBS"].ToString() != "Y") ||
                      (
                         //dr["A1TAG10"].ToString() != "2" &&
                         double.Parse(ConDt.Rows[i]["HDRAMT"].ToString()) == 0 && this._fd당기대변금액 == 0 &&
                         double.Parse(ConDt.Rows[i]["ODRAMT"].ToString()) == 0 && this._fd전기대변금액 == 0
                      )
                   )
                {
                }
                else
                {
                    dOUT_HCRAMT = double.Parse(ConDt.Rows[i]["OUT_HCRAMT"].ToString());
                    dOUT_OCRAMT = double.Parse(ConDt.Rows[i]["OUT_OCRAMT"].ToString());

                    if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                    {
                        if (sOLDA1CDAC == "")
                        {
                            sOLDA1CDAC = sNEWA1CDAC.ToString();
                            sOLDA1ABAC = sNEWA1ABAC.ToString();
                        }

                        if (sOLDA1CDAC != "" && (sNEWA1CDAC != sOLDA1CDAC))
                        {
                            // 레벨 = 1, A1TAG10 = 10, A1YNBS = Y

                            row = Retdt.NewRow();

                            row["A1CDAC"]  = sOLDA1CDAC.ToString();
                            row["A1LVAC"]  = "1";
                            row["A1ABAC"]  = sOLDA1ABAC.ToString() + "총계";
                            row["A1TAG10"] = "10";
                            row["A1YNBS"]  = "Y";
                            row["A1YNTB"]  = "";
                            row["HDRAMT"]  = "0";
                            row["HCRAMT"]  = string.Format("{0:#,###}", Convert.ToString(dHCRAMT));
                            row["ODRAMT"]  = "0";
                            row["OCRAMT"]  = string.Format("{0:#,###}", Convert.ToString(dOCRAMT));
                            row["DSP"]     = "";

                            Retdt.Rows.Add(row);

                            sOLDA1CDAC = sNEWA1CDAC.ToString();
                            sOLDA1ABAC = sNEWA1ABAC.ToString();
                        }
                    }

                    row = Retdt.NewRow();

                    row["A1CDAC"]  = ConDt.Rows[i]["A1CDAC"].ToString();
                    row["A1LVAC"]  = ConDt.Rows[i]["A1LVAC"].ToString();
                    row["A1ABAC"]  = ConDt.Rows[i]["A1ABAC"].ToString();
                    row["A1TAG10"] = ConDt.Rows[i]["A1TAG10"].ToString();
                    row["A1YNBS"]  = ConDt.Rows[i]["A1YNBS"].ToString();
                    row["A1YNTB"]  = ConDt.Rows[i]["A1YNTB"].ToString();
                    row["HDRAMT"]  = string.Format("{0:#,###}", ConDt.Rows[i]["HDRAMT"].ToString());

                    if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                    {
                        dHCRAMT       = double.Parse(ConDt.Rows[i]["HCRAMT"].ToString());

                        row["HCRAMT"] = "0";
                    }
                    else
                    {
                        row["HCRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["HCRAMT"].ToString());
                    }

                    row["ODRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["ODRAMT"].ToString());

                    if (ConDt.Rows[i]["A1CDAC"].ToString().Substring(1, 7) == "0000000")
                    {
                        dOCRAMT       = double.Parse(ConDt.Rows[i]["OCRAMT"].ToString());

                        row["OCRAMT"] = "0";
                    }
                    else
                    {
                        row["OCRAMT"] = string.Format("{0:#,###}", ConDt.Rows[i]["OCRAMT"].ToString());
                    }

                    if (ConDt.Rows[i]["A1TAG10"].ToString() == "1")
                    {
                        if (ConDt.Rows[i + 1]["A1TAG10"].ToString() == "2")
                        {
                            if (double.Parse(ConDt.Rows[i + 1]["HDRAMT"].ToString()) == 0 &&
                                double.Parse(ConDt.Rows[i + 1]["ODRAMT"].ToString()) == 0)
                            {
                                row["DSP"] = "C";
                            }
                            else
                            {
                                row["DSP"] = "";
                            }
                        }
                        else
                        {
                            row["DSP"] = "";
                        }
                    }
                    else
                    {
                        row["DSP"] = "";
                    }

                    Retdt.Rows.Add(row);
                }
            }

            #region Description : 당기 순이익

            row = Retdt.NewRow();

            row["A1CDAC"]  = "";
            row["A1LVAC"]  = "9";
            row["A1ABAC"]  = "(당기순이익 : ";
            row["A1TAG10"] = "9";
            row["A1YNBS"]  = "Y";
            row["A1YNTB"]  = "";
            row["HDRAMT"]  = "0";
            row["HCRAMT"]  = "0";
            row["ODRAMT"]  = "0";
            row["OCRAMT"]  = "0";
            row["DSP"]     = "";

            Retdt.Rows.Add(row);

            #endregion

            #region Description : 당기

            row = Retdt.NewRow();

            row["A1CDAC"]  = "";
            row["A1LVAC"]  = "9";
            row["A1ABAC"]  = "당기 : " + string.Format("{0:#,###}", dOUT_HCRAMT) + "원";
            row["A1TAG10"] = "9";
            row["A1YNBS"]  = "Y";
            row["A1YNTB"]  = "";
            row["HDRAMT"]  = "0";
            row["HCRAMT"]  = "0";
            row["ODRAMT"]  = "0";
            row["OCRAMT"]  = "0";
            row["DSP"]     = "";

            Retdt.Rows.Add(row);

            #endregion

            #region Description : 전기

            row = Retdt.NewRow();

            row["A1CDAC"]  = "";
            row["A1LVAC"]  = "9";
            row["A1ABAC"]  = "전기 : " + string.Format("{0:#,###}", dOUT_OCRAMT) + "원)";
            row["A1TAG10"] = "9";
            row["A1YNBS"]  = "Y";
            row["A1YNTB"]  = "";
            row["HDRAMT"]  = "0";
            row["HCRAMT"]  = "0";
            row["ODRAMT"]  = "0";
            row["OCRAMT"]  = "0";
            row["DSP"]     = "";

            Retdt.Rows.Add(row);

            #endregion

            // 레벨 = 1, A1TAG10 = 10

            row = Retdt.NewRow();

            row["A1CDAC"]  = sOLDA1CDAC.ToString();
            row["A1LVAC"]  = "1";
            row["A1ABAC"]  = sOLDA1ABAC.ToString() + "총계";
            row["A1TAG10"] = "10";
            row["A1YNBS"]  = "Y";
            row["A1YNTB"]  = "";
            row["HDRAMT"]  = "0";
            row["HCRAMT"]  = Convert.ToString(dHCRAMT);
            row["ODRAMT"]  = "0";
            row["OCRAMT"]  = Convert.ToString(dOCRAMT);
            row["DSP"]     = "";

            Retdt.Rows.Add(row);

            row = Retdt.NewRow();

            row["A1CDAC"]  = sOLDA1CDAC.ToString();
            row["A1LVAC"]  = "1";
            row["A1ABAC"]  = "부채와 자본 총계";
            row["A1TAG10"] = "10";
            row["A1YNBS"]  = "Y";
            row["A1YNTB"]  = "";
            row["HDRAMT"]  = "0";
            row["HCRAMT"]  = Convert.ToString(_fd당기부채자본금액);
            row["ODRAMT"]  = "0";
            row["OCRAMT"]  = Convert.ToString(_fd전기부채자본금액);
            row["DSP"]     = "";

            Retdt.Rows.Add(row);

            _fd당기대변금액 = 0;
            _fd전기대변금액 = 0;

            Finaldt.Columns.Add("A1ABAC", typeof(System.String));
            Finaldt.Columns.Add("HDRAMT", typeof(System.String));
            Finaldt.Columns.Add("HCRAMT", typeof(System.String));
            Finaldt.Columns.Add("ODRAMT", typeof(System.String));
            Finaldt.Columns.Add("OCRAMT", typeof(System.String));

            foreach (DataRow dr in Retdt.Rows)
            {
                sA1CDAC = Convert.ToString(dr["A1CDAC"].ToString());

                // 계정코드
                if (dr["A1LVAC"].ToString() == "2")
                {
                    this._iA1LVAC2++;

                    this._iA1LVAC3 = 0;

                    this._iA1LVAC5 = 0;

                    if (this._iA1LVAC2 == 1)
                    {
                        this._sA1LVAC = "Ⅰ";
                    }
                    else if (this._iA1LVAC2 == 2)
                    {
                        this._sA1LVAC = "Ⅱ";
                    }
                    else if (this._iA1LVAC2 == 3)
                    {
                        this._sA1LVAC = "Ⅲ";
                    }
                    else if (this._iA1LVAC2 == 4)
                    {
                        this._sA1LVAC = "Ⅳ";
                    }
                    else if (this._iA1LVAC2 == 5)
                    {
                        this._sA1LVAC = "Ⅴ";
                    }
                    else if (this._iA1LVAC2 == 6)
                    {
                        this._sA1LVAC = "Ⅵ";
                    }

                    sA1ABAC = this._sA1LVAC.ToString() + ".     " + dr["A1ABAC"].ToString();

                }
                else if (dr["A1LVAC"].ToString() == "3")
                {
                    this._iA1LVAC3++;

                    this._iA1LVAC5 = 0;

                    this._sA1LVAC = "  (" + Convert.ToString(this._iA1LVAC3) + ")";
                    sA1ABAC = this._sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                }
                else if (dr["A1LVAC"].ToString() == "5")
                {
                    if (Convert.ToString(dr["A1TAG10"]) != "2")
                    {
                        this._iA1LVAC5++;

                        this._sA1LVAC = "        " + Convert.ToString(this._iA1LVAC5) + ".";

                        sA1ABAC = this._sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                    }
                    else
                    {
                        sA1ABAC = "              " + dr["A1ABAC"].ToString();
                    }
                }
                else if (dr["A1LVAC"].ToString() == "6")
                {
                    this._iA1LVAC6++;

                    this._sA1LVAC = "           " + Convert.ToString(this._iA1LVAC6) + ".";

                    sA1ABAC = this._sA1LVAC.ToString() + "   " + dr["A1ABAC"].ToString();
                }
                else
                {
                    this._sA1LVAC = "";

                    this._iA1LVAC2 = 0;
                    this._iA1LVAC3 = 0;
                    this._iA1LVAC4 = 0;
                    this._iA1LVAC5 = 0;
                    this._iA1LVAC6 = 0;

                    // 계정과목
                    sA1ABAC = dr["A1ABAC"].ToString();
                }

                if (dr["A1TAG10"].ToString() == "1" && dr["DSP"].ToString() == "C")
                {
                    sHRDRAMT = "";
                    sORDRAMT = "";
                }
                else
                {
                    sHRDRAMT = string.Format("{0:#,###}", double.Parse(dr["HDRAMT"].ToString()));
                    sORDRAMT = string.Format("{0:#,###}", double.Parse(dr["ODRAMT"].ToString()));
                }

                // 충당금 계정일 경우
                if (_sA1TAG10 == "1" && Convert.ToString(dr["A1TAG10"]) == "2")
                {
                    _sA1TAG10 = dr["A1TAG10"].ToString();
                    // 당기
                    sHRCRAMT = string.Format("{0:#,###}", _fdHRDRAMT - double.Parse(dr["HDRAMT"].ToString()));
                    // 전기
                    sORCRAMT = string.Format("{0:#,###}", _fdORDRAMT - double.Parse(dr["ODRAMT"].ToString()));

                    this._fdHRDRAMT = 0;

                    this._fdORDRAMT = 0;

                    this._fdHRCRAMT = double.Parse(Get_Numeric(sHRCRAMT.ToString()));

                    this._fdORCRAMT = double.Parse(Get_Numeric(sORCRAMT.ToString()));
                }
                // 충당금 계정일 경우
                else if (_sA1TAG10 == "2" && Convert.ToString(dr["A1TAG10"]) == "2")
                {
                    _sA1TAG10 = dr["A1TAG10"].ToString();

                    // 당기 = (A1TAG10 = '2'인 대변값) - (A1TAG10 = '2'인 차변값)
                    sHRCRAMT = string.Format("{0:#,###}", _fdHRCRAMT - double.Parse(dr["HDRAMT"].ToString()));
                    // 전기 = (A1TAG10 = '2'인 대변값) - (A1TAG10 = '2'인 차변값)
                    sORCRAMT = string.Format("{0:#,###}", _fdORCRAMT - double.Parse(dr["ODRAMT"].ToString()));
                }
                else
                {
                    this._fdHRDRAMT = 0;

                    this._fdORDRAMT = 0;

                    this._fdHRCRAMT = 0;

                    this._fdORCRAMT = 0;

                    _sA1TAG10 = dr["A1TAG10"].ToString();

                    if (dr["A1TAG10"].ToString() == "1" && dr["DSP"].ToString() == "C")
                    {
                        // 당기
                        sHRCRAMT = string.Format("{0:#,###}", double.Parse(dr["HDRAMT"].ToString()));
                        // 전기
                        sORCRAMT = string.Format("{0:#,###}", double.Parse(dr["ODRAMT"].ToString()));
                    }
                    else
                    {
                        // 당기
                        sHRCRAMT = string.Format("{0:#,###}", double.Parse(dr["HCRAMT"].ToString()));
                        // 전기
                        sORCRAMT = string.Format("{0:#,###}", double.Parse(dr["OCRAMT"].ToString()));
                    }

                    // 당기
                    this._fdHRDRAMT = double.Parse(dr["HDRAMT"].ToString());

                    // 전기
                    this._fdORDRAMT = double.Parse(dr["ODRAMT"].ToString());

                    // 당기
                    this._fdHRCRAMT = double.Parse(dr["HCRAMT"].ToString());

                    // 전기
                    this._fdORCRAMT = double.Parse(dr["OCRAMT"].ToString());
                }

                row = Finaldt.NewRow();

                row["A1ABAC"] = sA1ABAC.ToString();
                row["HDRAMT"] = Get_Numeric(sHRDRAMT.ToString());
                row["HCRAMT"] = Get_Numeric(sHRCRAMT.ToString());
                row["ODRAMT"] = Get_Numeric(sORDRAMT.ToString());
                row["OCRAMT"] = Get_Numeric(sORCRAMT.ToString());

                Finaldt.Rows.Add(row);
            }

            //// 구조만 복사
            //DataTable aa = Finaldt.Clone();

            //// 정렬후 aa데이터 테이블에 넣음.
            //foreach (DataRow dr in Finaldt.Select("", "A1ABAC ASC"))
            //{
            //    aa.Rows.Add(dr.ItemArray);
            //}

            return Finaldt;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            int iDANGGISU = 0;
            string sDATE = string.Empty;

            string sYear  = string.Empty;
            string sMonth = string.Empty;
            int iDD = 0;

            sYear  = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD    = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            // 기준년월
            fsGSTDATE = this.DTP01_GSTDATE.GetValue().ToString() + Convert.ToString(iDD);



            sYear  = this.DTP01_GEDDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GEDDATE.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD    = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            // 비교년월
            fsGEDDATE = this.DTP01_GEDDATE.GetValue().ToString() + Convert.ToString(iDD);

            DataTable dz = new DataTable();

            // 당기기수 가져오기
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_GB_2423M259",
                "FS",
                "",
                ""
                );

            dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count > 0)
            {
                iDANGGISU = int.Parse(dz.Rows[0][1].ToString()) - 1;

                this.TXT01_GCUNO.SetValue(Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - iDANGGISU));
            }

            if (this.TXT01_GBENO.GetValue().ToString() == "")
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.TXT01_GCUNO.GetValue().ToString()) - 1));

                sDATE = Convert.ToString(int.Parse(this.fsGSTDATE.ToString().Substring(0, 4)) - 1) + "1231";

                this.DTP01_GEDDATE.SetValue(sDATE.ToString());

                // 비교년월
                fsGEDDATE = sDATE.ToString();
            }
            else
            {
                this.TXT01_GBENO.SetValue(Convert.ToString(int.Parse(this.fsGEDDATE.ToString().Substring(0, 4)) - iDANGGISU));
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25M2N592",
                this.TXT01_GCUNO.GetValue(),
                fsGSTDATE.ToString(),
                this.TXT01_GBENO.GetValue(),
                fsGEDDATE.ToString(),
                ""
                );

            SectionReport rpt = new TYACBJ010R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}
