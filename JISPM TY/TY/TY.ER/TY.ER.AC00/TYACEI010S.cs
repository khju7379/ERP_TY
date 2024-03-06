using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별어음현황조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.23 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28N6Z529 : 개별어음현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28N70530 : 개별어음현황 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI010S : TYBase
    {
        #region Description : 페이지 로드
        public TYACEI010S()
        {
            InitializeComponent();
        }

        private void TYACEI010S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_E6NONR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28N6Z529",
                this.TXT01_E6NONR.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28N70530.SetValue(UP_ConvertDt(dt));
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            string sNEWE6NONR = string.Empty;
            string sOLDE6NONR = string.Empty;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("E6NONR",   typeof(System.String));
            Condt.Columns.Add("E6AMNR",   typeof(System.String));
            Condt.Columns.Add("VNSANGHO", typeof(System.String));
            Condt.Columns.Add("E6DTIS",   typeof(System.String));
            Condt.Columns.Add("E6DTED",   typeof(System.String));
            Condt.Columns.Add("E6NMIS",   typeof(System.String));
            Condt.Columns.Add("E6NMBKNM", typeof(System.String));
            Condt.Columns.Add("E6IDNRNM", typeof(System.String));
            Condt.Columns.Add("E6CDCMNM", typeof(System.String));
            Condt.Columns.Add("E7DTBG",   typeof(System.String));
            Condt.Columns.Add("BBDESC1",  typeof(System.String));
            Condt.Columns.Add("E7HDAC",   typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sNEWE6NONR = dt.Rows[i]["E6NONR"].ToString();

                row = Condt.NewRow();

                if (sNEWE6NONR != sOLDE6NONR)
                {
                    row["E6NONR"]   = dt.Rows[i]["E6NONR"].ToString();
                    row["E6AMNR"]   = dt.Rows[i]["E6AMNR"].ToString();
                    row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                    row["E6DTIS"]   = dt.Rows[i]["E6DTIS"].ToString();
                    row["E6DTED"]   = dt.Rows[i]["E6DTED"].ToString();
                    row["E6NMIS"]   = dt.Rows[i]["E6NMIS"].ToString();
                    row["E6NMBKNM"] = dt.Rows[i]["E6NMBKNM"].ToString();
                    row["E6IDNRNM"] = dt.Rows[i]["E6IDNRNM"].ToString();
                    row["E6CDCMNM"] = dt.Rows[i]["E6CDCMNM"].ToString();

                    sOLDE6NONR = sNEWE6NONR;
                }
                else
                {
                    row["E6NONR"]   = "";
                    row["E6AMNR"]   = "";
                    row["VNSANGHO"] = "";
                    row["E6DTIS"]   = "";
                    row["E6DTED"]   = "";
                    row["E6NMIS"]   = "";
                    row["E6NMBKNM"] = "";
                    row["E6IDNRNM"] = "";
                    row["E6CDCMNM"] = "";
                }

                row["E7DTBG"]  = dt.Rows[i]["E7DTBG"].ToString();
                row["BBDESC1"] = dt.Rows[i]["BBDESC1"].ToString();
                row["E7HDAC"]  = dt.Rows[i]["E7HDAC"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion
    }
}