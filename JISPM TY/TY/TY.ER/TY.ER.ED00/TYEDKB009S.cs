using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// BL별 반출신고내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.24 15:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73TGG148 : BL별 반출신고내역 조회
    ///  TY_P_UT_73THA152 : BL별 반출신고상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73TGH149 : BL별 반출신고내역 조회
    ///  TY_S_UT_73THA153 : BL별 반출신고상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDIGJ : 공장
    ///  CHBLNO : B/L번호
    ///  EDATE : 종료일자
    ///  EDIHWAJU : 화주
    ///  EDIHWAMUL : 화물
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB009S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB009S()
        {
            InitializeComponent();
        }

        private void TYEDKB009S_Load(object sender, System.EventArgs e)
        {
            UP_SetLockCheck();

            this.CBO01_EDIGJ_SelectedIndexChanged(null, null);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.CBO01_EDIGJ.GetValue().ToString() != "T")
            {
                this.FPS91_TY_S_US_A16DD675.Visible = true;
                this.FPS91_TY_S_UT_73TGH149.Visible = false;
                this.FPS91_TY_S_UT_73THA153.Initialize();

                string sProCedureId = CBO01_INQOPTION.GetValue().ToString() == "1" ? "TY_P_US_A16DC674" : "TY_P_UT_73U9J159";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProCedureId, this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_CSHWAJU.GetValue(), this.CBH01_CSGOKJONG.GetValue(), this.TXT01_CHBLNO.GetValue());
                this.FPS91_TY_S_US_A16DD675.SetValue(this.DbConnector.ExecuteDataTable());

            }
            else
            {
                this.FPS91_TY_S_US_A16DD675.Visible = false;
                this.FPS91_TY_S_UT_73TGH149.Visible = true;
                this.FPS91_TY_S_UT_73THA153.Initialize();

                string sProCedureId = CBO01_INQOPTION.GetValue().ToString() == "1" ? "TY_P_UT_73TGG148" : "TY_P_UT_73U9J159";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProCedureId, this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_EDIHWAJU.GetValue(), this.CBH01_EDIHWAMUL.GetValue(), this.TXT01_CHBLNO.GetValue());
                this.FPS91_TY_S_UT_73TGH149.SetValue(this.DbConnector.ExecuteDataTable());
            }

            

        }
        #endregion

        #region  Description : FPS91_TY_S_UT_73TGH149_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_73TGH149_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            double dSUMEDICHQTY = 0;
            double dCSSINQTY = 0;

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                UP_Detail_DataBinding_Import(this.FPS91_TY_S_UT_73TGH149.GetValue("CSSINNO").ToString());
            }
            else
            {
                UP_Detail_DataBinding_Local(this.FPS91_TY_S_UT_73TGH149.GetValue("CSIPHANG").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSBONSUN").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSHWAJU").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSHWAMUL").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSBLNO").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSMSNSEQ").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSHSNSEQ").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSCUSTIL").ToString(),
                                            this.FPS91_TY_S_UT_73TGH149.GetValue("CSCHASU").ToString()
                    );
            }

            if (this.FPS91_TY_S_UT_73THA153.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_UT_73THA153, "EDISINGONUM", "합   계", SumRowType.Total, "CHMTQTY");

                for (int i = 0; i < this.FPS91_TY_S_UT_73THA153.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDIRCVGBNM").ToString() == "접수")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i,18].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDIRCVGBNM").ToString() == "오류")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i, 18].ForeColor = Color.Red;
                    }

                    if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDISINGONUM").ToString() == "합   계")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i, 2].Text = this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i-1, 20].Text;
                        dSUMEDICHQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i - 1, 20].Text));
                        dCSSINQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i - 1, 21].Text));
                    }
                }

                int iRow = this.FPS91_TY_S_UT_73THA153.CurrentRowCount;

                this.FPS91_TY_S_UT_73THA153_Sheet1.AddRows(iRow, 1);
                
                this.FPS91_TY_S_UT_73THA153.SetValue(iRow, "EDISINGONUM", "신고잔량");
                this.FPS91_TY_S_UT_73THA153.SetValue(iRow, "EDICHQTY", string.Format("{0:#,###.000}", dCSSINQTY - dSUMEDICHQTY));
                this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[iRow, 2].ForeColor = Color.Red;
                this.FPS91_TY_S_UT_73THA153_Sheet1.Rows[iRow].BackColor = Color.FromArgb(228, 242, 194);

            }
        }
        #endregion

        #region  Description : UP_Detail_DataBinding_Import(수입)
        private void UP_Detail_DataBinding_Import(string sCSSINNO)
        {
            string sProId = (this.CBO01_EDIGJ.GetValue().ToString() != "T") ? "TY_P_US_A16EH676" : "TY_P_UT_73THA152";

            this.FPS91_TY_S_UT_73THA153.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProId, sCSSINNO, CBO01_EDIGJ.GetValue(), sCSSINNO);
            this.FPS91_TY_S_UT_73THA153.SetValue(this.DbConnector.ExecuteDataTable());
           
        }
        #endregion

        #region  Description : UP_Detail_DataBinding_Local(내국)
        private void UP_Detail_DataBinding_Local(string sCSIPHANG, string sCSBONSUN, string sCSHWAJU, string sCSHWAMUL, string sCSBLNO, string sCSMSNSEQ, string sCSHSNSEQ, string sCSCUSTIL, string sCSCHASU)
        {
            this.FPS91_TY_S_UT_73THA153.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73UA0160", sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSBLNO, sCSMSNSEQ, sCSHSNSEQ, sCSCUSTIL, sCSCHASU, CBO01_EDIGJ.GetValue(), 
                                                        sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSMSNSEQ, sCSHSNSEQ);
            this.FPS91_TY_S_UT_73THA153.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : CBO01_EDIGJ_SelectedIndexChanged 이벤트
        private void CBO01_EDIGJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_EDIGJ.GetValue().ToString() != "T")
            {
                this.FPS91_TY_S_US_A16DD675.Visible = true;
                this.FPS91_TY_S_UT_73TGH149.Visible = false;
                this.FPS91_TY_S_UT_73THA153.Initialize();

                this.CBH01_CSGOKJONG.Visible = true;
                this.CBH01_CSHWAJU.Visible = true;
                this.CBH01_EDIHWAMUL.Visible = false;
                this.CBH01_EDIHWAJU.Visible = false;
            }
            else
            {
                this.FPS91_TY_S_US_A16DD675.Visible = false;
                this.FPS91_TY_S_UT_73TGH149.Visible = true;
                this.FPS91_TY_S_UT_73THA153.Initialize();
                this.CBH01_CSGOKJONG.Visible = false;
                this.CBH01_CSHWAJU.Visible = false;
                this.CBH01_EDIHWAMUL.Visible = true;
                this.CBH01_EDIHWAJU.Visible = true;
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_US_A16DD675_CellDoubleClick 이벤트
        private void FPS91_TY_S_US_A16DD675_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            double dSUMEDICHQTY = 0;
            double dCSSINQTY = 0;

            UP_Detail_DataBinding_Import(this.FPS91_TY_S_US_A16DD675.GetValue("CSSINNO").ToString());           

            if (this.FPS91_TY_S_UT_73THA153.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_UT_73THA153, "EDISINGONUM", "합   계", SumRowType.Total, "CHMTQTY");

                for (int i = 0; i < this.FPS91_TY_S_UT_73THA153.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDIRCVGBNM").ToString() == "접수")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i, 18].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDIRCVGBNM").ToString() == "오류")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i, 18].ForeColor = Color.Red;
                    }

                    if (this.FPS91_TY_S_UT_73THA153.GetValue(i, "EDISINGONUM").ToString() == "합   계")
                    {
                        this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i, 2].Text = this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i - 1, 20].Text;
                        dSUMEDICHQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i - 1, 20].Text));
                        dCSSINQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[i - 1, 21].Text));
                    }
                }

                int iRow = this.FPS91_TY_S_UT_73THA153.CurrentRowCount;

                this.FPS91_TY_S_UT_73THA153_Sheet1.AddRows(iRow, 1);

                this.FPS91_TY_S_UT_73THA153.SetValue(iRow, "EDISINGONUM", "신고잔량");
                this.FPS91_TY_S_UT_73THA153.SetValue(iRow, "EDICHQTY", string.Format("{0:#,###.000}", dCSSINQTY - dSUMEDICHQTY));
                this.FPS91_TY_S_UT_73THA153_Sheet1.Cells[iRow, 2].ForeColor = Color.Red;
                this.FPS91_TY_S_UT_73THA153_Sheet1.Rows[iRow].BackColor = Color.FromArgb(228, 242, 194);

            }
        }
        #endregion
    }
}
