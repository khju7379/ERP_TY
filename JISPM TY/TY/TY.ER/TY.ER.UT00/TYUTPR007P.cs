using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 유조용량표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.29 10:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73TBY140 : 유조용량표 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GATANKNO : TANK번호
    /// </summary>
    public partial class TYUTPR007P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR007P()
        {
            InitializeComponent();
        }

        private void TYUTPR007P_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GATANKNO);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73TBY140", this.TXT01_GATANKNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR007R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, QueryDataSetReport(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("GATANKNO", typeof(System.String));
            retDt.Columns.Add("GAMAJOR", typeof(System.String));
            retDt.Columns.Add("TNCAPA", typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("GAUGE001", typeof(System.String));
            retDt.Columns.Add("GAUGE002", typeof(System.String));
            retDt.Columns.Add("GAUGE003", typeof(System.String));
            retDt.Columns.Add("GAUGE004", typeof(System.String));
            retDt.Columns.Add("GAUGE005", typeof(System.String));
            retDt.Columns.Add("GAUGE006", typeof(System.String));
            retDt.Columns.Add("GAUGE007", typeof(System.String));
            retDt.Columns.Add("GAUGE008", typeof(System.String));
            retDt.Columns.Add("GAUGE009", typeof(System.String));
            retDt.Columns.Add("GAUGE010", typeof(System.String));
            retDt.Columns.Add("GAUGE011", typeof(System.String));
            retDt.Columns.Add("GAUGE012", typeof(System.String));
            retDt.Columns.Add("GAUGE013", typeof(System.String));
            retDt.Columns.Add("GAUGE014", typeof(System.String));
            retDt.Columns.Add("GAUGE015", typeof(System.String));
            retDt.Columns.Add("GAUGE016", typeof(System.String));
            retDt.Columns.Add("GAUGE017", typeof(System.String));
            retDt.Columns.Add("GAUGE018", typeof(System.String));
            retDt.Columns.Add("GAUGE019", typeof(System.String));
            retDt.Columns.Add("GAUGE020", typeof(System.String));
            retDt.Columns.Add("GAUGE021", typeof(System.String));
            retDt.Columns.Add("GAUGE022", typeof(System.String));
            retDt.Columns.Add("GAUGE023", typeof(System.String));
            retDt.Columns.Add("GAUGE024", typeof(System.String));
            retDt.Columns.Add("GAUGE025", typeof(System.String));
            retDt.Columns.Add("GAUGE026", typeof(System.String));
            retDt.Columns.Add("GAUGE027", typeof(System.String));
            retDt.Columns.Add("GAUGE028", typeof(System.String));
            retDt.Columns.Add("GAUGE029", typeof(System.String));
            retDt.Columns.Add("GAUGE030", typeof(System.String));
            retDt.Columns.Add("GAUGE031", typeof(System.String));
            retDt.Columns.Add("GAUGE032", typeof(System.String));
            retDt.Columns.Add("GAUGE033", typeof(System.String));
            retDt.Columns.Add("GAUGE034", typeof(System.String));
            retDt.Columns.Add("GAUGE035", typeof(System.String));
            retDt.Columns.Add("GAUGE036", typeof(System.String));
            retDt.Columns.Add("GAUGE037", typeof(System.String));
            retDt.Columns.Add("GAUGE038", typeof(System.String));
            retDt.Columns.Add("GAUGE039", typeof(System.String));
            retDt.Columns.Add("GAUGE040", typeof(System.String));
            retDt.Columns.Add("GAUGE041", typeof(System.String));
            retDt.Columns.Add("GAUGE042", typeof(System.String));
            retDt.Columns.Add("GAUGE043", typeof(System.String));
            retDt.Columns.Add("GAUGE044", typeof(System.String));
            retDt.Columns.Add("GAUGE045", typeof(System.String));
            retDt.Columns.Add("GAUGE046", typeof(System.String));
            retDt.Columns.Add("GAUGE047", typeof(System.String));
            retDt.Columns.Add("GAUGE048", typeof(System.String));
            retDt.Columns.Add("GAUGE049", typeof(System.String));
            retDt.Columns.Add("GAUGE050", typeof(System.String));
            retDt.Columns.Add("GAUGE051", typeof(System.String));
            retDt.Columns.Add("GAUGE052", typeof(System.String));
            retDt.Columns.Add("GAUGE053", typeof(System.String));
            retDt.Columns.Add("GAUGE054", typeof(System.String));
            retDt.Columns.Add("GAUGE055", typeof(System.String));
            retDt.Columns.Add("GAUGE056", typeof(System.String));
            retDt.Columns.Add("GAUGE057", typeof(System.String));
            retDt.Columns.Add("GAUGE058", typeof(System.String));
            retDt.Columns.Add("GAUGE059", typeof(System.String));
            retDt.Columns.Add("GAUGE060", typeof(System.String));
            retDt.Columns.Add("GAUGE061", typeof(System.String));
            retDt.Columns.Add("GAUGE062", typeof(System.String));
            retDt.Columns.Add("GAUGE063", typeof(System.String));
            retDt.Columns.Add("GAUGE064", typeof(System.String));
            retDt.Columns.Add("GAUGE065", typeof(System.String));
            retDt.Columns.Add("GAUGE066", typeof(System.String));
            retDt.Columns.Add("GAUGE067", typeof(System.String));
            retDt.Columns.Add("GAUGE068", typeof(System.String));
            retDt.Columns.Add("GAUGE069", typeof(System.String));
            retDt.Columns.Add("GAUGE070", typeof(System.String));
            retDt.Columns.Add("GAUGE071", typeof(System.String));
            retDt.Columns.Add("GAUGE072", typeof(System.String));
            retDt.Columns.Add("GAUGE073", typeof(System.String));
            retDt.Columns.Add("GAUGE074", typeof(System.String));
            retDt.Columns.Add("GAUGE075", typeof(System.String));
            retDt.Columns.Add("GAUGE076", typeof(System.String));
            retDt.Columns.Add("GAUGE077", typeof(System.String));
            retDt.Columns.Add("GAUGE078", typeof(System.String));
            retDt.Columns.Add("GAUGE079", typeof(System.String));
            retDt.Columns.Add("GAUGE080", typeof(System.String));
            retDt.Columns.Add("GAUGE081", typeof(System.String));
            retDt.Columns.Add("GAUGE082", typeof(System.String));
            retDt.Columns.Add("GAUGE083", typeof(System.String));
            retDt.Columns.Add("GAUGE084", typeof(System.String));
            retDt.Columns.Add("GAUGE085", typeof(System.String));
            retDt.Columns.Add("GAUGE086", typeof(System.String));
            retDt.Columns.Add("GAUGE087", typeof(System.String));
            retDt.Columns.Add("GAUGE088", typeof(System.String));
            retDt.Columns.Add("GAUGE089", typeof(System.String));
            retDt.Columns.Add("GAUGE090", typeof(System.String));
            retDt.Columns.Add("GAUGE091", typeof(System.String));
            retDt.Columns.Add("GAUGE092", typeof(System.String));
            retDt.Columns.Add("GAUGE093", typeof(System.String));
            retDt.Columns.Add("GAUGE094", typeof(System.String));
            retDt.Columns.Add("GAUGE095", typeof(System.String));
            retDt.Columns.Add("GAUGE096", typeof(System.String));
            retDt.Columns.Add("GAUGE097", typeof(System.String));
            retDt.Columns.Add("GAUGE098", typeof(System.String));
            retDt.Columns.Add("GAUGE099", typeof(System.String));
            retDt.Columns.Add("GAUGE100", typeof(System.String));
            retDt.Columns.Add("VOLUME001", typeof(System.String));
            retDt.Columns.Add("VOLUME002", typeof(System.String));
            retDt.Columns.Add("VOLUME003", typeof(System.String));
            retDt.Columns.Add("VOLUME004", typeof(System.String));
            retDt.Columns.Add("VOLUME005", typeof(System.String));
            retDt.Columns.Add("VOLUME006", typeof(System.String));
            retDt.Columns.Add("VOLUME007", typeof(System.String));
            retDt.Columns.Add("VOLUME008", typeof(System.String));
            retDt.Columns.Add("VOLUME009", typeof(System.String));
            retDt.Columns.Add("VOLUME010", typeof(System.String));
            retDt.Columns.Add("VOLUME011", typeof(System.String));
            retDt.Columns.Add("VOLUME012", typeof(System.String));
            retDt.Columns.Add("VOLUME013", typeof(System.String));
            retDt.Columns.Add("VOLUME014", typeof(System.String));
            retDt.Columns.Add("VOLUME015", typeof(System.String));
            retDt.Columns.Add("VOLUME016", typeof(System.String));
            retDt.Columns.Add("VOLUME017", typeof(System.String));
            retDt.Columns.Add("VOLUME018", typeof(System.String));
            retDt.Columns.Add("VOLUME019", typeof(System.String));
            retDt.Columns.Add("VOLUME020", typeof(System.String));
            retDt.Columns.Add("VOLUME021", typeof(System.String));
            retDt.Columns.Add("VOLUME022", typeof(System.String));
            retDt.Columns.Add("VOLUME023", typeof(System.String));
            retDt.Columns.Add("VOLUME024", typeof(System.String));
            retDt.Columns.Add("VOLUME025", typeof(System.String));
            retDt.Columns.Add("VOLUME026", typeof(System.String));
            retDt.Columns.Add("VOLUME027", typeof(System.String));
            retDt.Columns.Add("VOLUME028", typeof(System.String));
            retDt.Columns.Add("VOLUME029", typeof(System.String));
            retDt.Columns.Add("VOLUME030", typeof(System.String));
            retDt.Columns.Add("VOLUME031", typeof(System.String));
            retDt.Columns.Add("VOLUME032", typeof(System.String));
            retDt.Columns.Add("VOLUME033", typeof(System.String));
            retDt.Columns.Add("VOLUME034", typeof(System.String));
            retDt.Columns.Add("VOLUME035", typeof(System.String));
            retDt.Columns.Add("VOLUME036", typeof(System.String));
            retDt.Columns.Add("VOLUME037", typeof(System.String));
            retDt.Columns.Add("VOLUME038", typeof(System.String));
            retDt.Columns.Add("VOLUME039", typeof(System.String));
            retDt.Columns.Add("VOLUME040", typeof(System.String));
            retDt.Columns.Add("VOLUME041", typeof(System.String));
            retDt.Columns.Add("VOLUME042", typeof(System.String));
            retDt.Columns.Add("VOLUME043", typeof(System.String));
            retDt.Columns.Add("VOLUME044", typeof(System.String));
            retDt.Columns.Add("VOLUME045", typeof(System.String));
            retDt.Columns.Add("VOLUME046", typeof(System.String));
            retDt.Columns.Add("VOLUME047", typeof(System.String));
            retDt.Columns.Add("VOLUME048", typeof(System.String));
            retDt.Columns.Add("VOLUME049", typeof(System.String));
            retDt.Columns.Add("VOLUME050", typeof(System.String));
            retDt.Columns.Add("VOLUME051", typeof(System.String));
            retDt.Columns.Add("VOLUME052", typeof(System.String));
            retDt.Columns.Add("VOLUME053", typeof(System.String));
            retDt.Columns.Add("VOLUME054", typeof(System.String));
            retDt.Columns.Add("VOLUME055", typeof(System.String));
            retDt.Columns.Add("VOLUME056", typeof(System.String));
            retDt.Columns.Add("VOLUME057", typeof(System.String));
            retDt.Columns.Add("VOLUME058", typeof(System.String));
            retDt.Columns.Add("VOLUME059", typeof(System.String));
            retDt.Columns.Add("VOLUME060", typeof(System.String));
            retDt.Columns.Add("VOLUME061", typeof(System.String));
            retDt.Columns.Add("VOLUME062", typeof(System.String));
            retDt.Columns.Add("VOLUME063", typeof(System.String));
            retDt.Columns.Add("VOLUME064", typeof(System.String));
            retDt.Columns.Add("VOLUME065", typeof(System.String));
            retDt.Columns.Add("VOLUME066", typeof(System.String));
            retDt.Columns.Add("VOLUME067", typeof(System.String));
            retDt.Columns.Add("VOLUME068", typeof(System.String));
            retDt.Columns.Add("VOLUME069", typeof(System.String));
            retDt.Columns.Add("VOLUME070", typeof(System.String));
            retDt.Columns.Add("VOLUME071", typeof(System.String));
            retDt.Columns.Add("VOLUME072", typeof(System.String));
            retDt.Columns.Add("VOLUME073", typeof(System.String));
            retDt.Columns.Add("VOLUME074", typeof(System.String));
            retDt.Columns.Add("VOLUME075", typeof(System.String));
            retDt.Columns.Add("VOLUME076", typeof(System.String));
            retDt.Columns.Add("VOLUME077", typeof(System.String));
            retDt.Columns.Add("VOLUME078", typeof(System.String));
            retDt.Columns.Add("VOLUME079", typeof(System.String));
            retDt.Columns.Add("VOLUME080", typeof(System.String));
            retDt.Columns.Add("VOLUME081", typeof(System.String));
            retDt.Columns.Add("VOLUME082", typeof(System.String));
            retDt.Columns.Add("VOLUME083", typeof(System.String));
            retDt.Columns.Add("VOLUME084", typeof(System.String));
            retDt.Columns.Add("VOLUME085", typeof(System.String));
            retDt.Columns.Add("VOLUME086", typeof(System.String));
            retDt.Columns.Add("VOLUME087", typeof(System.String));
            retDt.Columns.Add("VOLUME088", typeof(System.String));
            retDt.Columns.Add("VOLUME089", typeof(System.String));
            retDt.Columns.Add("VOLUME090", typeof(System.String));
            retDt.Columns.Add("VOLUME091", typeof(System.String));
            retDt.Columns.Add("VOLUME092", typeof(System.String));
            retDt.Columns.Add("VOLUME093", typeof(System.String));
            retDt.Columns.Add("VOLUME094", typeof(System.String));
            retDt.Columns.Add("VOLUME095", typeof(System.String));
            retDt.Columns.Add("VOLUME096", typeof(System.String));
            retDt.Columns.Add("VOLUME097", typeof(System.String));
            retDt.Columns.Add("VOLUME098", typeof(System.String));
            retDt.Columns.Add("VOLUME099", typeof(System.String));
            retDt.Columns.Add("VOLUME100", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                row["GAMAJOR"] = SetDefaultValue(dt.Rows[i]["GAMAJOR"].ToString()).Trim();
                row["GATANKNO"] = SetDefaultValue(dt.Rows[i]["GATANKNO"].ToString()).Trim();
                if (row["GAMAJOR"].ToString() == "00")
                {
                    //row["GATANKNO"] = SetDefaultValue(dt.Rows[i][0].ToString()).Trim();
                    row["TNCAPA"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i]["TNCAPA"].ToString()))).Trim();
                    row["VNSANGHO"] = SetDefaultValue(dt.Rows[i]["VNSANGHO"].ToString()).Trim();
                }
                else
                {
                    //row["GATANKNO"] = "";
                    row["TNCAPA"] = 0;
                    row["VNSANGHO"] = "";
                }
                row["GAUGE001"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 0);
                row["GAUGE002"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 1);
                row["GAUGE003"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 2);
                row["GAUGE004"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 3);
                row["GAUGE005"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 4);
                row["GAUGE006"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 5);
                row["GAUGE007"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 6);
                row["GAUGE008"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 7);
                row["GAUGE009"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 8);
                row["GAUGE010"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 9);
                row["GAUGE011"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 10);
                row["GAUGE012"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 11);
                row["GAUGE013"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 12);
                row["GAUGE014"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 13);
                row["GAUGE015"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 14);
                row["GAUGE016"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 15);
                row["GAUGE017"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 16);
                row["GAUGE018"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 17);
                row["GAUGE019"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 18);
                row["GAUGE020"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 19);
                row["GAUGE021"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 20);
                row["GAUGE022"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 21);
                row["GAUGE023"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 22);
                row["GAUGE024"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 23);
                row["GAUGE025"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 24);
                row["GAUGE026"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 25);
                row["GAUGE027"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 26);
                row["GAUGE028"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 27);
                row["GAUGE029"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 28);
                row["GAUGE030"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 29);
                row["GAUGE031"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 30);
                row["GAUGE032"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 31);
                row["GAUGE033"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 32);
                row["GAUGE034"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 33);
                row["GAUGE035"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 34);
                row["GAUGE036"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 35);
                row["GAUGE037"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 36);
                row["GAUGE038"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 37);
                row["GAUGE039"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 38);
                row["GAUGE040"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 39);
                row["GAUGE041"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 40);
                row["GAUGE042"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 41);
                row["GAUGE043"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 42);
                row["GAUGE044"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 43);
                row["GAUGE045"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 44);
                row["GAUGE046"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 45);
                row["GAUGE047"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 46);
                row["GAUGE048"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 47);
                row["GAUGE049"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 48);
                row["GAUGE050"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 49);
                row["GAUGE051"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 50);
                row["GAUGE052"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 51);
                row["GAUGE053"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 52);
                row["GAUGE054"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 53);
                row["GAUGE055"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 54);
                row["GAUGE056"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 55);
                row["GAUGE057"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 56);
                row["GAUGE058"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 57);
                row["GAUGE059"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 58);
                row["GAUGE060"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 59);
                row["GAUGE061"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 60);
                row["GAUGE062"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 61);
                row["GAUGE063"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 62);
                row["GAUGE064"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 63);
                row["GAUGE065"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 64);
                row["GAUGE066"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 65);
                row["GAUGE067"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 66);
                row["GAUGE068"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 67);
                row["GAUGE069"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 68);
                row["GAUGE070"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 69);
                row["GAUGE071"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 70);
                row["GAUGE072"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 71);
                row["GAUGE073"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 72);
                row["GAUGE074"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 73);
                row["GAUGE075"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 74);
                row["GAUGE076"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 75);
                row["GAUGE077"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 76);
                row["GAUGE078"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 77);
                row["GAUGE079"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 78);
                row["GAUGE080"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 79);
                row["GAUGE081"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 80);
                row["GAUGE082"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 81);
                row["GAUGE083"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 82);
                row["GAUGE084"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 83);
                row["GAUGE085"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 84);
                row["GAUGE086"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 85);
                row["GAUGE087"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 86);
                row["GAUGE088"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 87);
                row["GAUGE089"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 88);
                row["GAUGE090"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 89);
                row["GAUGE091"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 90);
                row["GAUGE092"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 91);
                row["GAUGE093"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 92);
                row["GAUGE094"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 93);
                row["GAUGE095"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 94);
                row["GAUGE096"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 95);
                row["GAUGE097"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 96);
                row["GAUGE098"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 97);
                row["GAUGE099"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 98);
                row["GAUGE100"] = Convert.ToString(double.Parse(row["GAMAJOR"].ToString()) * 100 + 99);

                row["VOLUME001"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][4].ToString()))).Trim();
                row["VOLUME002"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][5].ToString()))).Trim();
                row["VOLUME003"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][6].ToString()))).Trim();
                row["VOLUME004"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][7].ToString()))).Trim();
                row["VOLUME005"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][8].ToString()))).Trim();
                row["VOLUME006"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][9].ToString()))).Trim();
                row["VOLUME007"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][10].ToString()))).Trim();
                row["VOLUME008"] = String.Format("{0:N1}", double.Parse(SetDefaultValue(dt.Rows[i][11].ToString()))).Trim();
                row["VOLUME009"] = String.Format("{0:N1}", double.Parse(SetDefaultValue(dt.Rows[i][12].ToString()))).Trim();
                row["VOLUME010"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][13].ToString()))).Trim();
                row["VOLUME011"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][14].ToString()))).Trim();
                row["VOLUME012"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][15].ToString()))).Trim();
                row["VOLUME013"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][16].ToString()))).Trim();
                row["VOLUME014"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][17].ToString()))).Trim();
                row["VOLUME015"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][18].ToString()))).Trim();
                row["VOLUME016"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][19].ToString()))).Trim();
                row["VOLUME017"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][20].ToString()))).Trim();
                row["VOLUME018"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][21].ToString()))).Trim();
                row["VOLUME019"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][22].ToString()))).Trim();
                row["VOLUME020"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][23].ToString()))).Trim();
                row["VOLUME021"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][24].ToString()))).Trim();
                row["VOLUME022"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][25].ToString()))).Trim();
                row["VOLUME023"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][26].ToString()))).Trim();
                row["VOLUME024"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][27].ToString()))).Trim();
                row["VOLUME025"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][28].ToString()))).Trim();
                row["VOLUME026"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][29].ToString()))).Trim();
                row["VOLUME027"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][30].ToString()))).Trim();
                row["VOLUME028"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][31].ToString()))).Trim();
                row["VOLUME029"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][32].ToString()))).Trim();
                row["VOLUME030"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][33].ToString()))).Trim();
                row["VOLUME031"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][34].ToString()))).Trim();
                row["VOLUME032"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][35].ToString()))).Trim();
                row["VOLUME033"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][36].ToString()))).Trim();
                row["VOLUME034"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][37].ToString()))).Trim();
                row["VOLUME035"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][38].ToString()))).Trim();
                row["VOLUME036"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][39].ToString()))).Trim();
                row["VOLUME037"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][40].ToString()))).Trim();
                row["VOLUME038"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][41].ToString()))).Trim();
                row["VOLUME039"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][42].ToString()))).Trim();
                row["VOLUME040"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][43].ToString()))).Trim();
                row["VOLUME041"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][44].ToString()))).Trim();
                row["VOLUME042"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][45].ToString()))).Trim();
                row["VOLUME043"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][46].ToString()))).Trim();
                row["VOLUME044"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][47].ToString()))).Trim();
                row["VOLUME045"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][48].ToString()))).Trim();
                row["VOLUME046"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][49].ToString()))).Trim();
                row["VOLUME047"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][50].ToString()))).Trim();
                row["VOLUME048"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][51].ToString()))).Trim();
                row["VOLUME049"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][52].ToString()))).Trim();
                row["VOLUME050"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][53].ToString()))).Trim();
                row["VOLUME051"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][54].ToString()))).Trim();
                row["VOLUME052"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][55].ToString()))).Trim();
                row["VOLUME053"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][56].ToString()))).Trim();
                row["VOLUME054"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][57].ToString()))).Trim();
                row["VOLUME055"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][58].ToString()))).Trim();
                row["VOLUME056"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][59].ToString()))).Trim();
                row["VOLUME057"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][60].ToString()))).Trim();
                row["VOLUME058"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][61].ToString()))).Trim();
                row["VOLUME059"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][62].ToString()))).Trim();
                row["VOLUME060"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][63].ToString()))).Trim();
                row["VOLUME061"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][64].ToString()))).Trim();
                row["VOLUME062"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][65].ToString()))).Trim();
                row["VOLUME063"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][66].ToString()))).Trim();
                row["VOLUME064"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][67].ToString()))).Trim();
                row["VOLUME065"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][68].ToString()))).Trim();
                row["VOLUME066"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][69].ToString()))).Trim();
                row["VOLUME067"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][70].ToString()))).Trim();
                row["VOLUME068"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][71].ToString()))).Trim();
                row["VOLUME069"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][72].ToString()))).Trim();
                row["VOLUME070"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][73].ToString()))).Trim();
                row["VOLUME071"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][74].ToString()))).Trim();
                row["VOLUME072"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][75].ToString()))).Trim();
                row["VOLUME073"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][76].ToString()))).Trim();
                row["VOLUME074"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][77].ToString()))).Trim();
                row["VOLUME075"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][78].ToString()))).Trim();
                row["VOLUME076"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][79].ToString()))).Trim();
                row["VOLUME077"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][80].ToString()))).Trim();
                row["VOLUME078"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][81].ToString()))).Trim();
                row["VOLUME079"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][82].ToString()))).Trim();
                row["VOLUME080"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][83].ToString()))).Trim();
                row["VOLUME081"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][84].ToString()))).Trim();
                row["VOLUME082"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][85].ToString()))).Trim();
                row["VOLUME083"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][86].ToString()))).Trim();
                row["VOLUME084"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][87].ToString()))).Trim();
                row["VOLUME085"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][88].ToString()))).Trim();
                row["VOLUME086"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][89].ToString()))).Trim();
                row["VOLUME087"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][90].ToString()))).Trim();
                row["VOLUME088"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][91].ToString()))).Trim();
                row["VOLUME089"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][92].ToString()))).Trim();
                row["VOLUME090"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][93].ToString()))).Trim();
                row["VOLUME091"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][94].ToString()))).Trim();
                row["VOLUME092"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][95].ToString()))).Trim();
                row["VOLUME093"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][96].ToString()))).Trim();
                row["VOLUME094"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][97].ToString()))).Trim();
                row["VOLUME095"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][98].ToString()))).Trim();
                row["VOLUME096"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][99].ToString()))).Trim();
                row["VOLUME097"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][100].ToString()))).Trim();
                row["VOLUME098"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][101].ToString()))).Trim();
                row["VOLUME099"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][102].ToString()))).Trim();
                row["VOLUME100"] = String.Format("{0:N3}", double.Parse(SetDefaultValue(dt.Rows[i][103].ToString()))).Trim();

                retDt.Rows.Add(row);
            }
            return retDt;
        }
        #endregion		
    }
}
