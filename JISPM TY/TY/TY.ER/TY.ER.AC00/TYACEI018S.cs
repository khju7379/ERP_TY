using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 만기일별 지급어음 명세서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.31 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25V8A760 : 만기일별 지급어음 명세서
    ///  TY_P_AC_29O6G257 : 만기일별 지급어음 명세서1
    ///  TY_P_AC_29O6H258 : 만기일별 지급어음 명세서2
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2619E768 : 만기일자별 지급어음 명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  F3CDDP : 부서코드
    ///  F3SSYN : 발생상태
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACEI018S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI018S()
        {
            InitializeComponent();
        }

        private void TYACEI018S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.AddDays(-30).ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_F3CDDP.DummyValue = this.DTP01_STDATE.GetValue();
            
            this.SetStartingFocus(DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sF3HSYN = this.CBO01_F3HSYN.GetValue().ToString();
            sF3HSYN = sF3HSYN.Replace("'","");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25V8A760", this.CBH01_F3CDDP.GetValue(), this.DTP01_STDATE.GetValue(), this.DTP01_EDDATE.GetValue(), sF3HSYN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_2619E768.SetValue(UP_ConvertDt(dt));
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sF3HSYN = this.CBO01_F3HSYN.GetValue().ToString();
            sF3HSYN = sF3HSYN.Replace("'", "");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25V8A760", this.CBH01_F3CDDP.GetValue(), this.DTP01_STDATE.GetValue(), this.DTP01_EDDATE.GetValue(), sF3HSYN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACEI018R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt))).ShowDialog();
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            //this.DbConnector.CommandClear();

            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_29O6G257",
            //    this.DTP01_STDATE.GetValue()
            //    );

            //DataTable dtNony = this.DbConnector.ExecuteDataTable();

            //this.DbConnector.CommandClear();

            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_29O6H258",
            //    this.DTP01_STDATE.GetValue(),
            //    this.DTP01_STDATE.GetValue()
            //    );

            //DataTable dtADSL = this.DbConnector.ExecuteDataTable();

            string sDPAC = string.Empty;

            int i = 0;

            DataTable table = new DataTable();

            DataRow Row;

            table.Columns.Add("ROWNUM",      typeof(System.String));
            table.Columns.Add("DATE",        typeof(System.String));
            table.Columns.Add("F3DTED",      typeof(System.String));
            table.Columns.Add("F3DTIS",      typeof(System.String));
            table.Columns.Add("F3HSYN",      typeof(System.String));
            table.Columns.Add("F3HSYNNM",    typeof(System.String));
            table.Columns.Add("F3NONY",      typeof(System.String));
            table.Columns.Add("F3AMNY",      typeof(System.String));
            table.Columns.Add("F3AMNYTOTAL", typeof(System.String));
            table.Columns.Add("F3BKPY",      typeof(System.String));
            table.Columns.Add("F3BKPYNM",    typeof(System.String));
            table.Columns.Add("F3RPYY",      typeof(System.String));
            table.Columns.Add("F3CLNY",      typeof(System.String));
            table.Columns.Add("F3CLNYNM",    typeof(System.String));
            table.Columns.Add("F3CDDP",      typeof(System.String));

            for (i = 0; i < dt.Rows.Count; i++)
            {
                Row = table.NewRow();

                Row["ROWNUM"]      = dt.Rows[i]["ROWNUM"].ToString();
                Row["DATE"]        = dt.Rows[i]["DATE"].ToString();
                Row["F3DTED"]      = dt.Rows[i]["F3DTED"].ToString();
                Row["F3DTIS"]      = dt.Rows[i]["F3DTIS"].ToString();
                Row["F3HSYN"]      = dt.Rows[i]["F3HSYN"].ToString();
                Row["F3HSYNNM"]    = dt.Rows[i]["F3HSYNNM"].ToString();
                Row["F3NONY"]      = dt.Rows[i]["F3NONY"].ToString();
                Row["F3AMNY"]      = dt.Rows[i]["F3AMNY"].ToString();
                Row["F3AMNYTOTAL"] = dt.Rows[i]["F3AMNYTOTAL"].ToString();
                Row["F3RPYY"]      = dt.Rows[i]["F3RPYY"].ToString();
                Row["F3BKPY"]      = dt.Rows[i]["F3BKPY"].ToString();
                Row["F3BKPYNM"]    = dt.Rows[i]["F3BKPYNM"].ToString();
                Row["F3CLNY"]      = dt.Rows[i]["F3CLNY"].ToString();
                Row["F3CLNYNM"]    = dt.Rows[i]["F3CLNYNM"].ToString();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_29O6G257",
                    this.DTP01_STDATE.GetValue(),
                    dt.Rows[i]["F3NONY"].ToString()
                    );

                DataTable dtNony = this.DbConnector.ExecuteDataTable();

                if (dtNony.Rows.Count > 0)
                {
                    sDPAC = dtNony.Rows[0][0].ToString();

                    //sB2WCJP = DataRowNony[0][2].ToString();


                    //sParam = "";
                    //sParam = "B2DPMK = '" + sB2WCJP.Substring(0, 6) + "' AND ";
                    //sParam = sParam + "B2DTMK = '" + sB2WCJP.Substring(6, 8) + "' AND ";
                    //sParam = sParam + "B2NOSQ = " + sB2WCJP.Substring(14, 3) + " ";

                    //sSort = "B2DPMK, B2DTMK, B2NOSQ, B2NOLN ASC";

                    //DataRowAdsl = dtADSL.Select(sParam, sSort);

                    //if (DataRowAdsl.Length > 0)
                    //{
                    //    for (int j = 0; j < DataRowAdsl.Length; j++)
                    //    {
                    //        if (DataRowAdsl[j][5].ToString() == "01" &&
                    //            DataRowAdsl[j][6].ToString() == sVEND)
                    //        {
                    //            sDPAC = DataRowAdsl[j][4].ToString();
                    //        }
                    //        if (DataRowAdsl[j][7].ToString() == "01" &&
                    //            DataRowAdsl[j][8].ToString() == sVEND)
                    //        {
                    //            sDPAC = DataRowAdsl[j][4].ToString();
                    //        }
                    //        if (DataRowAdsl[j][9].ToString() == "01" &&
                    //            DataRowAdsl[j][10].ToString() == sVEND)
                    //        {
                    //            sDPAC = DataRowAdsl[j][4].ToString();
                    //        }
                    //        if (DataRowAdsl[j][11].ToString() == "01" &&
                    //            DataRowAdsl[j][12].ToString() == sVEND)
                    //        {
                    //            sDPAC = DataRowAdsl[j][4].ToString();
                    //        }
                    //        if (DataRowAdsl[j][13].ToString() == "01" &&
                    //            DataRowAdsl[j][14].ToString() == sVEND)
                    //        {
                    //            sDPAC = DataRowAdsl[j][4].ToString();
                    //        }

                    //        if (sDPAC.Trim() != "")
                    //        {
                    //            j = DataRowAdsl.Length;
                    //        }
                    //    }
                    //}

                    Row["F3CDDP"] = sDPAC;
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_33428224",
                        dt.Rows[i]["F3NONY"].ToString()
                        );

                    DataTable dtSAUP = this.DbConnector.ExecuteDataTable();

                    Row["F3CDDP"] = dtSAUP.Rows[0][0].ToString();
                }

                table.Rows.Add(Row);
            }

            return table;
        }
        #endregion

        #region Description : DTP01_STDATE_ValueChanged 이벤트
        private void DTP01_STDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_F3CDDP.DummyValue = this.DTP01_STDATE.GetValue();
        }
        #endregion
    }
}
