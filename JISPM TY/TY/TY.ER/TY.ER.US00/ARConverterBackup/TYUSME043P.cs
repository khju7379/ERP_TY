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
    public partial class TYUSME043P : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME043P()
        {
            InitializeComponent();
        }

        private void TYUSME043P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_94M9E412",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = UP_Get_Convert(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUSME043R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable UP_Get_Convert(DataTable Orgdt)
        {
            string sJPNO     = string.Empty;
            string sVNSANGHO = string.Empty;
            string sVNSAUPNO = string.Empty;
            string sTXGUBUN  = string.Empty;

            string sGUBUN    = string.Empty;

            string sDATE     = string.Empty;
            string sSTDATE   = string.Empty;
            string sEDDATE   = string.Empty;
            string sTXGUBUN1 = string.Empty;

            double dAMOUNT   = 0;
            double dVAT      = 0;

            int i = 0;

            sSTDATE = Get_Date(this.DTP01_STDATE.GetValue().ToString());
            sEDDATE = Get_Date(this.DTP01_EDDATE.GetValue().ToString());

            sDATE = sSTDATE.Substring(0, 4) + "년" + sSTDATE.Substring(4, 2) + "월" + sSTDATE.Substring(6, 2) + "일 ~ " + sEDDATE.Substring(0, 4) + "년" + sEDDATE.Substring(4, 2) + "월" + sEDDATE.Substring(6, 2);

            DataTable dt = new DataTable();
            DataTable retDt = new DataTable();

            DataRow dtRow;

            retDt.Columns.Add("GUBUN1",   typeof(System.String));
            retDt.Columns.Add("SEQ",      typeof(System.Decimal));
            retDt.Columns.Add("JPNO",     typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("VNSAUPNO", typeof(System.String));
            retDt.Columns.Add("TXDESC1",  typeof(System.String));
            retDt.Columns.Add("AMOUNT",   typeof(System.Decimal));
            retDt.Columns.Add("VAT",      typeof(System.Decimal));
            retDt.Columns.Add("TOT",      typeof(System.Decimal));
            retDt.Columns.Add("SAUPBU",   typeof(System.String));
            retDt.Columns.Add("GUBUN",    typeof(System.String));
            retDt.Columns.Add("DATE",     typeof(System.String));

            for (i = 0; i < Orgdt.Rows.Count; i++)
            {
                sJPNO     = "";
                sVNSANGHO = "";
                sVNSAUPNO = "";
                sTXGUBUN  = "";

                dAMOUNT   = 0;
                dVAT      = 0;

                sTXGUBUN1 = "";

                // 확인
                sGUBUN = "";

                // 전표번호
                sJPNO = Orgdt.Rows[i]["JPNO"].ToString().Substring(0, 17);

                // 전표 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_94MBL417",
                    sJPNO.ToString().Substring(0, 6),
                    sJPNO.ToString().Substring(6, 8),
                    sJPNO.ToString().Substring(14, 3)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    // 확인
                    sGUBUN = "미승인전표 없음";
                }

                if (sGUBUN == "")
                {
                    // 전표 공급가
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_94MBO418",
                        sJPNO.ToString().Substring(0, 6),
                        sJPNO.ToString().Substring(6, 8),
                        sJPNO.ToString().Substring(14, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 공급가
                        dAMOUNT = double.Parse(dt.Rows[0]["B2AMCR"].ToString());
                    }

                    // 수정세금계산서 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_94MBP419", sJPNO.ToString().Substring(0, 17));

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sTXGUBUN1 = "(수정)";
                    }

                    // 전표 부가세
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_94MBQ420",
                        sJPNO.ToString().Substring(0, 6),
                        sJPNO.ToString().Substring(6, 8),
                        sJPNO.ToString().Substring(14, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 거래처명
                        sVNSANGHO = dt.Rows[0]["VNSANGHO"].ToString();
                        // 사업자번호
                        sVNSAUPNO = dt.Rows[0]["VNSAUPNO"].ToString();
                        // 과세 종류
                        sTXGUBUN  = dt.Rows[0]["TXDESC1"].ToString();
                        // 부가세
                        dVAT      = double.Parse(dt.Rows[0]["B2AMCR"].ToString());
                    }
                }

                dtRow = retDt.NewRow();

                dtRow["GUBUN1"]   = "HAP";
                dtRow["SEQ"]      = (i + 1);
                dtRow["JPNO"]     = sJPNO.ToString();
                dtRow["VNSANGHO"] = sVNSANGHO.ToString();
                dtRow["VNSAUPNO"] = sVNSAUPNO.ToString().Substring(0, 3) + "-" + sVNSAUPNO.Substring(3, 2) + "-" + sVNSAUPNO.Substring(5, 5);
                dtRow["TXDESC1"]  = sTXGUBUN.ToString() + sTXGUBUN1.ToString();
                dtRow["AMOUNT"]   = dAMOUNT;
                dtRow["VAT"]      = dVAT;
                dtRow["TOT"]      = dAMOUNT + dVAT;
                dtRow["SAUPBU"]   = "SILO사업본부";
                dtRow["GUBUN"]    = sGUBUN.ToString();
                dtRow["DATE"]     = sDATE.ToString();

                retDt.Rows.Add(dtRow);
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
    }
}