using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 해안단지 입출고 예정사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.14 15:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_95EBV549 : 해안단지 입출고 예정사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_B1IGQ343 : 해안단지 입출고 예정사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPS018S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPS018S()
        {
            InitializeComponent();
        }

        private void TYUTPS018S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.DTP01_SDATE.GetString()) > Convert.ToDouble(this.DTP01_EDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                string sProcedure = string.Empty;

                if (this.CBO01_CHGUBUN.GetValue().ToString() == "A")
                {
                    sProcedure = "TY_P_UT_B1IGP342";
                }
                else if (this.CBO01_CHGUBUN.GetValue().ToString() == "Y")
                {
                    sProcedure = "TY_P_UT_B4K8Y204";
                }
                else if (this.CBO01_CHGUBUN.GetValue().ToString() == "N")
                {
                    sProcedure = "TY_P_UT_B4K8Y205";
                }
                
                this.FPS91_TY_S_UT_B1IGQ343.Initialize();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProcedure.ToString(), this.DTP01_SDATE.GetValue().ToString(),
                                                               this.DTP01_EDATE.GetValue().ToString(),
                                                               this.CBH01_KBBUSEO.GetValue().ToString(),
                                                               this.CBO01_GGUBUN.GetValue().ToString()
                                                               );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_UT_B1IGQ343.SetValue(UP_ConvertDt(dt));
                }
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            string sWKCODE = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt = dt.Clone();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sWKCODE = "";

                row = Retdt.NewRow();

                row["SMWKORAPPDATE"] = dt.Rows[i]["SMWKORAPPDATE"].ToString();

                row["SMWORKTITLE"]   = dt.Rows[i]["SMWORKTITLE"].ToString();
                row["SMAREATEXT"]    = dt.Rows[i]["SMAREATEXT"].ToString();
                row["SMSUBVEND"]     = dt.Rows[i]["SMSUBVEND"].ToString();

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE01"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }


                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE02"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE03"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE04"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE05"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE06"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE07"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE08"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE09"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                sWKCODE = UP_WKCODE(dt.Rows[i]["SWWKCODE10"].ToString());

                if (sWKCODE.ToString() == "01")
                {
                    row["SWWKCODE01"] = "●";
                }
                if (sWKCODE.ToString() == "02")
                {
                    row["SWWKCODE02"] = "●";
                }
                if (sWKCODE.ToString() == "03")
                {
                    row["SWWKCODE03"] = "●";
                }
                if (sWKCODE.ToString() == "04")
                {
                    row["SWWKCODE04"] = "●";
                }
                if (sWKCODE.ToString() == "05")
                {
                    row["SWWKCODE05"] = "●";
                }
                if (sWKCODE.ToString() == "06")
                {
                    row["SWWKCODE06"] = "●";
                }
                if (sWKCODE.ToString() == "07")
                {
                    row["SWWKCODE07"] = "●";
                }
                if (sWKCODE.ToString() == "08")
                {
                    row["SWWKCODE08"] = "●";
                }
                if (sWKCODE.ToString() == "09")
                {
                    row["SWWKCODE09"] = "●";
                }
                if (sWKCODE.ToString() == "10")
                {
                    row["SWWKCODE10"] = "●";
                }

                Retdt.Rows.Add(row);
            }

            return Retdt;
        }
        #endregion

        #region Description : 작업명
        private string UP_WKCODE(string sWKNAME)
        {
            string rtnValue = string.Empty;

            switch (sWKNAME)
            {
                case "일반위험작업":
                    rtnValue = "01";
                    break;
                case "열간(화기)작업":
                    rtnValue = "02";
                    break;
                case "밀폐공간출입작업":
                    rtnValue = "03";
                    break;
                case "Tank Cleaning":
                    rtnValue = "04";
                    break;
                case "Bin Cleaning":
                    rtnValue = "04";
                    break;
                case "전기(정전)작업":
                    rtnValue = "05";
                    break;
                case "방사선사용작업":
                    rtnValue = "06";
                    break;
                case "고소작업":
                    rtnValue = "07";
                    break;
                case "인양(중장비사용)작업":
                    rtnValue = "08";
                    break;
                case "굴착작업":
                    rtnValue = "09";
                    break;
                case "용기출입작업":
                    rtnValue = "10";
                    break;
            }

            return rtnValue;
        }
        #endregion

        private void DTP01_SDATE_ValueChanged(object sender, EventArgs e)
        {
            // 부서
            this.CBH01_KBBUSEO.DummyValue = this.DTP01_SDATE.GetString();
        }

    }
}
