using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing; 

namespace TY.ER.AC00
{
    /// <summary>
    /// 신고서 마감 체크리스트 조회 팝업(신고서 마감) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.02.17 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_42H58420 : 신고서 마감 체크리스트 조회 (SP)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_42H5Y426 : 신고서 마감 체크리스트 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  SAV : 저장
    ///  INQOPTION : 조회구분
    ///  VNGUBUN : 구분
    ///  ELXYYMM : 기준년도
    /// </summary>
    public partial class TYACTX024S : TYBase
    {
        //bool fbRtnFlag = true;

        string _YEAR = string.Empty;
        string _BRANCH = string.Empty;
        string _RPTGUBN = string.Empty;
            
        public TYACTX024S(string sYEAR, string sBRANCH, string sRPTGUBN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            _YEAR = sYEAR;
            _BRANCH = sBRANCH;
            _RPTGUBN = sRPTGUBN;
        }

        #region Description : Page_Load
        private void TYACTX024S_Load(object sender, System.EventArgs e)
        {
            this.RB_ATTAXGUBN1.Checked = true;

            this.DTP01_ELXYYMM.SetValue(_YEAR);
            this.CBO01_VNGUBUN.SetValue(_BRANCH);
            this.CBO01_INQOPTION.SetValue(_RPTGUBN);

            this.BTN61_INQ_Click(null, null);

            this.BTN61_INQ.Visible = false;

        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            };

            this.Close();
        }
        #endregion
        
        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();  // 체크리스트 (SP)
            this.DbConnector.Attach("TY_P_AC_42H58420",
                                    this.DTP01_ELXYYMM.GetString().ToString().Trim(),
                                    this.CBO01_VNGUBUN.GetValue().ToString().Trim(),  // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                   );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_42H5Y426.SetValue(UP_ConvertDt(dt));

                for (int i = 0; i < this.FPS91_TY_S_AC_42H5Y426.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_42H5Y426.GetValue(i, "CHK01_AMT03").ToString() != "0")
                    {
                        this.FPS91_TY_S_AC_42H5Y426_Sheet1.Cells[i, 3].ForeColor = Color.Red;
                        this.FPS91_TY_S_AC_42H5Y426_Sheet1.Cells[i, 4].ForeColor = Color.Red;
                    }
                }

                for (int i = 0; i < this.FPS91_TY_S_AC_42H5Y426.ActiveSheet.RowCount; i++)
                {
                     if (this.FPS91_TY_S_AC_42H5Y426.GetValue(i, "CHK01_AMT01").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_42H5Y426_Sheet1.Cells[i, 1].Value = "";
                    }

                    if (this.FPS91_TY_S_AC_42H5Y426.GetValue(i, "CHK01_AMT02").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_42H5Y426_Sheet1.Cells[i, 2].Value = "";
                    }

                    if (this.FPS91_TY_S_AC_42H5Y426.GetValue(i, "CHK01_AMT03").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_42H5Y426_Sheet1.Cells[i, 3].Value = "";
                    }
                }
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            DataTable Retdt = new DataTable();

            DataTable ConDt = new DataTable();
            ConDt = dt;
            DataRow row;

            string sCHKGB = string.Empty;

            Retdt.Columns.Add("CHKGB", typeof(System.String));
            Retdt.Columns.Add("CHK01_AMT01", typeof(System.String));
            Retdt.Columns.Add("CHK01_AMT02", typeof(System.String));
            Retdt.Columns.Add("CHK01_AMT03", typeof(System.String));
            Retdt.Columns.Add("CHK01_MEASS", typeof(System.String));

            for (int i = 0; i <= ConDt.Rows.Count - 1; i++)
            {
                row = Retdt.NewRow();

                if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "01")
                {
                    sCHKGB = "1. 신고서의 과세표준및매출세액 합계 세액 (9) = 장부금액 매출부가세";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "신고서상 매출세액과 장부금액 매출세액이 일치하지않습니다 "; //[ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }
                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "02")
                {
                    sCHKGB = "2. 신고서의 매입세액 차감계 (17) = 장부금액 매입부가세";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "신고서상 매입세액과 장부금액 매입세액이 일치하지않습니다 "; // [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }
                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "03")
                {
                    sCHKGB = "3. 손익계산서상 매출금액 = 신고서 과세표준명세 (26) (수입금액 제외)";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "신고서 과세표준명세 수입금액과 장부상 매출금액이 일치하지 않습니다 "; // [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }

                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "04")
                {
                    sCHKGB = "4. 신고서의 과세표준명세(30)항목의 합계금액 = 과세표준및매출세액 합계 (9)항목 금액";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "신고서 매출합계액과 과세표준명세 합계액이 일치하지 않습니다 "; // [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] 건 ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }
                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "05")
                {
                    sCHKGB = "5. 건물등감가상각자산취득명세서";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "유형자산취득명세서의 신고계정을 확인하세요."; // [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] 건 ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }
                }

                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "06")
                {
                    sCHKGB = "6. 총괄납부명세서 지점 납부세액 = 지점 신고서의 (25)";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "지점 신고서와 총괄납부명세서 지점 납부세액이 일치하지 않습니다.";//  [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }

                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "07")
                {
                    sCHKGB = "7. 총괄납부명세서 납부세액 = 본점 신고서의 총괄납부사업자가 납부할 세액";

                    if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                    {
                        row["CHK01_MEASS"] = "신고서와 총괄납부명세서 납부세액이 일치하지 않습니다.";//  [ " + string.Format("{0:#,###}", double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString())) + " ] ";
                    }
                    else
                    {
                        row["CHK01_MEASS"] = " ";
                    }

                }
                else if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "08")
                {
                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") // 본점일경우
                    {
                        sCHKGB = "8. 지점 신고서 마감여부";

                        if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                        {
                            row["CHK01_MEASS"] = "지점 신고서 마감이 되지않았습니다.";
                        }
                        else
                        {
                            row["CHK01_MEASS"] = " ";
                        }
                    }
                    else   // 지점일경우
                    {
                        sCHKGB = "8. 본점 신고서 마감여부 ";

                        if (double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString()) != 0)
                        {
                            row["CHK01_MEASS"] = "본점 신고서 마감완료 되었습니다. ";
                        }
                        else
                        {
                            row["CHK01_MEASS"] = " ";
                        }
                    }
                }

                row["CHKGB"] = sCHKGB;
                
                if (ConDt.Rows[i]["CHKGB"].ToString().Trim() == "07")
                {
                    row["CHK01_AMT01"] = double.Parse(ConDt.Rows[i]["CHK01_AMT01"].ToString()) + double.Parse(ConDt.Rows[i]["CHK01_AMT02"].ToString());
                    row["CHK01_AMT02"] = double.Parse(ConDt.Rows[i]["CHK01_AMT01"].ToString()) + double.Parse(ConDt.Rows[i]["CHK01_AMT02"].ToString());
                    row["CHK01_AMT03"] = double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString());
                }
                else
                {
                    row["CHK01_AMT01"] = double.Parse(ConDt.Rows[i]["CHK01_AMT01"].ToString());
                    row["CHK01_AMT02"] = double.Parse(ConDt.Rows[i]["CHK01_AMT02"].ToString());
                    row["CHK01_AMT03"] = double.Parse(ConDt.Rows[i]["CHK01_AMT03"].ToString());
                }

                Retdt.Rows.Add(row);

                sCHKGB = "";
            }

            return Retdt;
        }
        #endregion

        #region Description : 라디오버튼 이벤트
        private void RB_ATTAXGUBN1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.RB_ATTAXGUBN2.Checked = false;
            }
        }

        private void RB_ATTAXGUBN2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN2.Checked == true)
            {
                this.RB_ATTAXGUBN1.Checked = false;
            }
        }
        #endregion

        #region Description : FormClosing
        private void TYACTX024S_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            };

        } 
        #endregion
    }
}
