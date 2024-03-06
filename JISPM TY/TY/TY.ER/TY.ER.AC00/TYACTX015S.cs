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
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
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
    public partial class TYACTX015S : TYBase
    {
        string fsS5YEAR    = string.Empty;
        string fsS5BRANCH  = string.Empty;
        string fsS5CONFGB  = string.Empty;

        string fsPOPUP = string.Empty;

        #region Description : 페이지 로드
        public TYACTX015S()
        {
            InitializeComponent();
        }

        public TYACTX015S(string sS5YEAR, string sS5BRANCH, string sS5CONFGB, string sPOPUP)
        {
            InitializeComponent();

            fsS5YEAR    = sS5YEAR.ToString();
            fsS5BRANCH  = sS5BRANCH.ToString();
            fsS5CONFGB  = sS5CONFGB.ToString();
            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX015S_Load(object sender, System.EventArgs e)
        {
            //UP_Spread_Title();

            this.FPS91_TY_S_AC_3CJ2H822.Initialize();
            this.FPS91_TY_S_AC_3CJ2A820.Initialize();

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S5YEAR.SetValue(fsS5YEAR.ToString());
                this.CBO01_S5BRANCH.SetValue(fsS5BRANCH.ToString());
                this.CBO01_S5CONFGB.SetValue(fsS5CONFGB.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            SetStartingFocus(this.TXT01_S5YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_3CJ2H822.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CJ23819",
                this.TXT01_S5YEAR.GetValue().ToString(),
                this.CBO01_S5BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 2)
                );

            dt = this.DbConnector.ExecuteDataTable();

            fsS5YEAR    = this.TXT01_S5YEAR.GetValue().ToString();
            fsS5BRANCH  = this.CBO01_S5BRANCH.GetValue().ToString();
            fsS5CONFGB  = this.CBO01_S5CONFGB.GetValue().ToString();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3CJ2A820.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CJ2A820.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CJ2A820.GetValue(i, "SAYU").ToString() == "합       계")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3CJ2A820.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3CJ2A820.GetValue(i, "SAYU").ToString() == "합       계")
                {
                    this.FPS91_TY_S_AC_3CJ2A820.ActiveSheet.Rows[i].ForeColor = Color.Red;

                    this.FPS91_TY_S_AC_3CJ2A820.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 내역 조회
        private void UP_JPNO_List(string sS5PRIVE)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CJ2G821",
                this.TXT01_S5YEAR.GetValue().ToString(),
                this.CBO01_S5BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 2),
                sS5PRIVE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CJ2H822.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CJ2H822.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CJ2H822.GetValue(i, "NUMBER").ToString() == "합       계")
                {
                    this.FPS91_TY_S_AC_3CJ2H822.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CJ2H822.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처수";
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 5].Value = "매수";
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 6].Value = "공급가액";
            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 7].Value = "세    액";

            this.FPS91_TY_S_AC_3CJ2A820_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CK2B844",
                this.TXT01_S5YEAR.GetValue().ToString(),
                this.CBO01_S5BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S5YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 2),
                this.CBO01_S5BRANCH.GetValue().ToString()
                );

            DataTable dt2 = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYACTX015R(dt2, this.TXT01_S5YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S5CONFGB.GetValue().ToString(), 2));
            (new TYERGB001P(rpt, dt)).ShowDialog();

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 세금계산서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CJ2A820_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CJ2A820.GetValue("SAYU").ToString() == "합       계")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CJ2H822.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CJ2A820.GetValue("AMT").ToString() == "0" && this.FPS91_TY_S_AC_3CJ2A820.GetValue("CNT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3CJ2H822.Initialize();
            }
            else
            {
                UP_JPNO_List(this.FPS91_TY_S_AC_3CJ2A820.GetValue("PRIVE").ToString());
            }
        }
        #endregion

        #region Description : 내역 스프레드 이벤트
        private void FPS91_TY_S_AC_3CJ2H822_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if (this.FPS91_TY_S_AC_3CJ2H822.GetValue("NUMBER").ToString() == "합       계")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_3CJ2H822.GetValue("S5JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_3CJ2H822.GetValue("S5JPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_3CJ2H822.GetValue("S5JPNO").ToString().Substring(14, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
            }
        }
        #endregion

        #region Description : 세금계산서 합계표 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt, string sGubn)
        {
            string sNUMBER = string.Empty;
            string sS5SAUPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sS5TAXCDGN = string.Empty;
            string sMAESU_CNT = string.Empty;
            string sHAP_AMT = string.Empty;
            string sHAP_VAT = string.Empty;
            int iBLANK = 0;

            if (sGubn == "1")
            {
                iBLANK = 14 - ((dt.Rows.Count - 5) % 14);
            }
            else
            {
                iBLANK = 15 - ((dt.Rows.Count - 5) % 15);
            }

            int i = 0;

            sNUMBER = "";
            sS5SAUPNO = "";
            sVNSANGHO = "";
            sS5TAXCDGN = "";
            sMAESU_CNT = "";
            sHAP_AMT = "";
            sHAP_VAT = "";

            DataTable Retdt = dt;



            if (dt != null && dt.Rows.Count > 5)
            {
                DataRow row;

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S1SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S1TAXCDGN"] = "";
                    row["MAESU_CNT"] = DBNull.Value;
                    row["HAP_AMT"] = DBNull.Value;
                    row["HAP_VAT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            else if (dt.Rows.Count < 5)
            {
                DataRow row;

                iBLANK = 5 - (dt.Rows.Count);

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S1SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S1TAXCDGN"] = "";
                    row["MAESU_CNT"] = DBNull.Value;
                    row["HAP_AMT"] = DBNull.Value;
                    row["HAP_VAT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.TXT01_S5YEAR.SetValue(TYCookie.Year);
                this.CBO01_S5BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S5CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S5YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S5YEAR.GetValue().ToString(), this.CBO01_S5BRANCH.GetValue().ToString(), this.CBO01_S5CONFGB.GetValue().ToString());
        }
        #endregion
    }
}