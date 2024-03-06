using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 조직도 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.19 11:34
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28N3N526 : 조직도TreeView 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  BLDATE : 발령일자
    /// </summary>
    public partial class TYHRKB02P1 : TYBase
    {
        private string fsKIJUNDATE;

        private string fsSTARTDATE;

        public string fsSOSOK;
        public string fsBUSEO;
        public string fsTEAM;

        #region Description : 폼 로드 이벤트
        public TYHRKB02P1(string sKIJUNDATE)
        {
            InitializeComponent();

             fsSOSOK = "";
             fsBUSEO = "";
             fsTEAM = "";

            this.fsKIJUNDATE = sKIJUNDATE;
        }

        private void TYHRKB02P1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BLDATE.SetValue(this.fsKIJUNDATE);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BJF0458", this.fsKIJUNDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.TRV01_ORG.Initialize();
            if (dt.Rows.Count > 0)
            {
                fsSTARTDATE = dt.Rows[0]["SDATE"].ToString();

                UP_TreeView_ORG( dt.Rows[0]["ENTER_CD"].ToString(),
                                 dt.Rows[0]["ORG_CHART_NM"].ToString(),
                                 dt.Rows[0]["SDATE"].ToString());
            }
        }
        #endregion

        #region Description : TreeView 조직도 표현 이벤트
        private void UP_TreeView_ORG(string sENTER_CD, string sORG_CHART_NM, string sSDATE)
        {
            string sCODE = "";
            string sCODE_NAME = "";
            string sPARENT_CODE = "";

            this.TRV01_ORG.Initialize();

            DataTable _dataSource = new DataTable();
            _dataSource.Columns.Add("CODE");
            _dataSource.Columns.Add("CODE_NAME");
            _dataSource.Columns.Add("PARENT_CODE");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N3N526", sENTER_CD, sORG_CHART_NM, sSDATE, sENTER_CD, sORG_CHART_NM, sSDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sCODE = dt.Rows[i]["ORG_CD"].ToString();
                    sCODE_NAME = dt.Rows[i]["ORG_CDNM"].ToString() + "[" + dt.Rows[i]["ORG_CD"].ToString() + "]";
                    sPARENT_CODE = dt.Rows[i]["PRIOR_ORG_CD"].ToString();

                    _dataSource.Rows.Add(sCODE, sCODE_NAME, sPARENT_CODE);
                }

                this.TRV01_ORG.SetValue(new object[] { "태영인더스트리", "0", _dataSource });
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 트리 더블클릭 이벤트
        private void TRV01_ORG_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            string sENTER_CD = "TY";
            string sORG_CHART_NM = "조직도";
            string sSDATE = fsSTARTDATE;

            Int16  iLevel = 0;
            string sPRIOR_ORG_CD = string.Empty;

            string sCODE = this.TRV01_ORG.SelectedNodeName;
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28N3N526", sENTER_CD, sORG_CHART_NM, sSDATE, sENTER_CD, sORG_CHART_NM, sSDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {                
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        iLevel = 0;
                        sPRIOR_ORG_CD = "";
                        if (sCODE == dt.Rows[i]["ORG_CD"].ToString().Trim())
                        {
                            iLevel = Convert.ToInt16(dt.Rows[i]["LEVEL"].ToString());

                            switch (iLevel)
                            {
                                case 1: fsSOSOK = dt.Rows[i]["ORG_CD"].ToString().Trim();
                                    break;
                                case 2: fsBUSEO = dt.Rows[i]["ORG_CD"].ToString().Trim();
                                    break;
                                case 3: fsTEAM = dt.Rows[i]["ORG_CD"].ToString().Trim();
                                    break;
                            }

                            sPRIOR_ORG_CD = dt.Rows[i]["PRIOR_ORG_CD"].ToString().Trim();
                            if (sPRIOR_ORG_CD != "")
                            {
                                UP_NodeTree_Check(dt, iLevel - 1, sPRIOR_ORG_CD);

                                if (fsBUSEO == "")
                                {
                                    fsBUSEO = fsTEAM;
                                }
                                if (fsTEAM == "")
                                {
                                    fsTEAM = fsBUSEO;
                                }

                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                        }
                    }                
            }

        }
        #endregion

        #region Description : UP_NodeTree_Check 이벤트
        private void UP_NodeTree_Check(DataTable dt, int ilevel, string sORG_CD)
        {
            foreach (DataRow dr in dt.Select(string.Format("[LEVEL] = '{0}'", ilevel)))
            {
                if (sORG_CD == Convert.ToString(dr["ORG_CD"]))
                {
                    switch (Convert.ToInt16(dr["LEVEL"]))
                    {
                        case 1: fsSOSOK = Convert.ToString(dr["ORG_CD"]);
                                break;
                        case 2: fsBUSEO = Convert.ToString(dr["ORG_CD"]);
                                break;
                        case 3: fsTEAM = Convert.ToString(dr["ORG_CD"]);
                                break;
                    }

                    if (Convert.ToString(dr["PRIOR_ORG_CD"]) != "" && Convert.ToString(dr["PRIOR_ORG_CD"]) != "0" && Convert.ToInt16(dr["LEVEL"]) > 0)
                    {
                        UP_NodeTree_Check(dt, Convert.ToInt16(dr["LEVEL"]) - 1, Convert.ToString(dr["PRIOR_ORG_CD"]));
                    }
                }
            }
        }
        #endregion

    }
}
