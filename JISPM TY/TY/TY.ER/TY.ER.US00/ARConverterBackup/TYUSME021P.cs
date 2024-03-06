using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_92CE5728 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUSME021P : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME021P()
        {
            InitializeComponent();
        }

        private void TYUSME021P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GDATE.SetValue("");

            this.SetFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_959DX526",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = UP_Get_Convert(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_US_9BQAJ547.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_959DX526",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = UP_Get_Convert(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUSME021R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_GB_9BIG4521");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable UP_Get_Convert(DataTable Orgdt)
        {
            string sSql = string.Empty;
            string sGUBUN = string.Empty;
            string sHANGCHA = string.Empty;
            string sGOKJONG = string.Empty;
            string sSOSOK   = string.Empty;
            string sIHIPHANG = string.Empty;
            string sIHJAKENDAT = string.Empty;
            string sNEWIHIPHANG = string.Empty;
            string sNEWIHJAKENDAT = string.Empty;
            double dJGHWAKQTY = 0;
            double dJGBEJNQTY = 0;

            sGUBUN = "0";

            DataTable dt = new DataTable();
            DataTable dz = new DataTable();
            DataTable dy = new DataTable();

            DataTable retDt = new DataTable();

            DataRow row;

            retDt.Columns.Add("GUBUN",      typeof(System.String));
            retDt.Columns.Add("USHANGCHA",  typeof(System.String));
            retDt.Columns.Add("VSDESC1",    typeof(System.String));
            retDt.Columns.Add("IHIPHANG",   typeof(System.String));
            retDt.Columns.Add("USGOKJONG",  typeof(System.String));
            retDt.Columns.Add("GKDESC1",    typeof(System.String));
            retDt.Columns.Add("JGHWAKQTY",  typeof(System.Decimal));
            retDt.Columns.Add("JGBEJNQTY",  typeof(System.Decimal));
            retDt.Columns.Add("IHJAKENDAT", typeof(System.String));
            retDt.Columns.Add("HJDESC1",    typeof(System.String));
            retDt.Columns.Add("JGSOSOK",    typeof(System.String));
            retDt.Columns.Add("QTY",        typeof(System.Decimal));
            retDt.Columns.Add("CNSISULAMT", typeof(System.Decimal));
            retDt.Columns.Add("USUSEDAMT",  typeof(System.Decimal));
            retDt.Columns.Add("USUSEDVAT",  typeof(System.Decimal));
            retDt.Columns.Add("HAP",        typeof(System.Decimal));
            

            if (Orgdt.Rows.Count > 0)
            {
                // 선내톤수 월계
                for (int i = 0; i < Orgdt.Rows.Count; i++)
                {
                    dJGHWAKQTY = 0;
                    dJGBEJNQTY = 0;

                    if (i == 0)
                    {
                        //sGUBUN   = Orgdt.Rows[i]["JGSOSOK"].ToString();
                        sGUBUN = "0";
                        sHANGCHA = Orgdt.Rows[i]["USHANGCHA"].ToString();
                        sGOKJONG = Orgdt.Rows[i]["USGOKJONG"].ToString();
                        sSOSOK   = Orgdt.Rows[i]["JGSOSOK"].ToString();
                    }
                    else
                    {
                        if ((sHANGCHA != Orgdt.Rows[i]["USHANGCHA"].ToString()) ||
                            (sGOKJONG != Orgdt.Rows[i]["USGOKJONG"].ToString()) ||
                            (sSOSOK   != Orgdt.Rows[i]["JGSOSOK"].ToString()))
                        {
                            sHANGCHA = Orgdt.Rows[i]["USHANGCHA"].ToString();
                            sGOKJONG = Orgdt.Rows[i]["USGOKJONG"].ToString();
                            sSOSOK   = Orgdt.Rows[i]["JGSOSOK"].ToString();

                            sGUBUN = Convert.ToString(double.Parse(sGUBUN) + 1);
                        }
                    }

                    // 입항파일 READ
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_938DQ030",
                        sHANGCHA.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sIHIPHANG = dt.Rows[0]["IHIPHANG"].ToString();
                        sIHJAKENDAT = dt.Rows[0]["IHJAKENDAT"].ToString();
                        sNEWIHIPHANG = sIHIPHANG.Substring(0, 4) + "/" + sIHIPHANG.Substring(4, 2) + "/" + sIHIPHANG.Substring(6, 2);
                        sNEWIHJAKENDAT = sIHJAKENDAT.Substring(0, 4) + "/" + sIHJAKENDAT.Substring(4, 2) + "/" + sIHJAKENDAT.Substring(6, 2);
                    }

                    // 재고파일 READ
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_959FP528",
                        sHANGCHA.ToString(),
                        sGOKJONG.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dJGHWAKQTY = double.Parse(Get_Numeric(dt.Rows[0]["JGHWAKQTY"].ToString()));
                        dJGBEJNQTY = double.Parse(Get_Numeric(dt.Rows[0]["JGBEJNQTY"].ToString()));
                    }

                    row = retDt.NewRow();

                    row["GUBUN"] = sGUBUN.ToString();
                    row["USHANGCHA"] = Orgdt.Rows[i]["USHANGCHA"].ToString();
                    row["VSDESC1"] = Orgdt.Rows[i]["VSDESC1"].ToString();
                    row["IHIPHANG"] = sNEWIHIPHANG.ToString();
                    row["USGOKJONG"] = Orgdt.Rows[i]["USGOKJONG"].ToString();
                    row["GKDESC1"] = Orgdt.Rows[i]["GKDESC1"].ToString();
                    row["JGHWAKQTY"] = dJGHWAKQTY;
                    row["JGBEJNQTY"] = dJGBEJNQTY;
                    row["IHJAKENDAT"] = sNEWIHJAKENDAT.ToString();
                    row["HJDESC1"] = Orgdt.Rows[i]["HJDESC1"].ToString();
                    row["JGSOSOK"] = Orgdt.Rows[i]["JGSOSOK"].ToString();
                    row["QTY"] = double.Parse(Orgdt.Rows[i]["QTY"].ToString());
                    row["CNSISULAMT"] = double.Parse(Orgdt.Rows[i]["CNSISULAMT"].ToString());
                    row["USUSEDAMT"] = double.Parse(Orgdt.Rows[i]["USUSEDAMT"].ToString());
                    row["USUSEDVAT"] = double.Parse(Orgdt.Rows[i]["USUSEDVAT"].ToString());
                    row["HAP"] = double.Parse(Orgdt.Rows[i]["HAP"].ToString());

                    retDt.Rows.Add(row);
                }
            }

            // 수정세금계산서(USIUPTXF)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9BKBG529", Get_Date(this.DTP01_GDATE.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dJGHWAKQTY = 0;
                    dJGBEJNQTY = 0;

                    // 시설사용료 매출 READ
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_959G7530",
                        dt.Rows[j]["UTWNJPNO"].ToString()
                        );

                    dz = this.DbConnector.ExecuteDataTable();

                    if (dz.Rows.Count > 0)
                    {
                        // 재고파일 READ
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_US_959FP528",
                            dz.Rows[0]["USHANGCHA"].ToString(),
                            dz.Rows[0]["USGOKJONG"].ToString()
                            );

                        dy = this.DbConnector.ExecuteDataTable();

                        if (dy.Rows.Count > 0)
                        {
                            dJGHWAKQTY = double.Parse(Get_Numeric(dy.Rows[0]["JGHWAKQTY"].ToString()));
                            dJGBEJNQTY = double.Parse(Get_Numeric(dy.Rows[0]["JGBEJNQTY"].ToString()));
                        }

                        // 입항파일 READ
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_US_938DQ030",
                            dz.Rows[0]["USHANGCHA"].ToString()
                            );

                        dy = this.DbConnector.ExecuteDataTable();

                        if (dy.Rows.Count > 0)
                        {
                            sIHIPHANG      = dy.Rows[0]["IHIPHANG"].ToString();
                            sIHJAKENDAT    = dy.Rows[0]["IHJAKENDAT"].ToString();
                            sNEWIHIPHANG   = sIHIPHANG.Substring(0, 4) + "/" + sIHIPHANG.Substring(4, 2) + "/" + sIHIPHANG.Substring(6, 2);
                            sNEWIHJAKENDAT = sIHJAKENDAT.Substring(0, 4) + "/" + sIHJAKENDAT.Substring(4, 2) + "/" + sIHJAKENDAT.Substring(6, 2);
                        }

                        sGUBUN = Convert.ToString(double.Parse(sGUBUN) + 1);

                        row = retDt.NewRow();

                        row["GUBUN"]      = sGUBUN.ToString();
                        row["USHANGCHA"]  = dz.Rows[0]["USHANGCHA"].ToString();
                        row["VSDESC1"]    = dz.Rows[0]["VSDESC1"].ToString();
                        row["IHIPHANG"]   = sNEWIHIPHANG.ToString();
                        row["USGOKJONG"]  = dz.Rows[0]["USGOKJONG"].ToString();
                        row["GKDESC1"]    = dz.Rows[0]["GKDESC1"].ToString();
                        row["JGHWAKQTY"]  = 0;
                        row["JGBEJNQTY"]  = 0;
                        row["IHJAKENDAT"] = sNEWIHJAKENDAT.ToString();
                        row["HJDESC1"]    = dz.Rows[0]["HJDESC1"].ToString().Trim() + "-수정 전자세금";
                        row["JGSOSOK"]    = dz.Rows[0]["JGSOSOK"].ToString();
                        row["QTY"]        = 0;
                        row["CNSISULAMT"] = 0;
                        row["USUSEDAMT"]  = double.Parse(dt.Rows[j]["AMT"].ToString());
                        row["USUSEDVAT"]  = double.Parse(dt.Rows[j]["VAT"].ToString());
                        row["HAP"]        = double.Parse(dt.Rows[j]["TOTAMT"].ToString());

                        retDt.Rows.Add(row);
                    }
                }
            }

            return retDt;
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 항차 이벤트
        private void CBH01_STHANGCHA_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                this.CBH01_EDHANGCHA.SetValue(this.CBH01_STHANGCHA.GetValue().ToString());
            }
        }
        #endregion
    }
}