using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선박사양조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.24 15:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BOF3847 : 선박사양관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6BOFS853 : 선박사양관리
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  VESLCODE : 선박코드
    /// </summary>
    public partial class TYUTVS002S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTVS002S()
        {
            InitializeComponent();
        }

        private void TYUTVS002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_VESLCODE.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_6BOFS853.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6BOF3847", this.CBH01_VESLCODE.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6BOFS853.SetValue(UP_ConvertDt(dt));
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            string sVEWHARF = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt = dt.Clone();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sVEWHARF = "";

                row = Retdt.NewRow();

                row["VESLCODE"] = dt.Rows[i]["VESLCODE"].ToString();

                row["VSDESC1"]  = dt.Rows[i]["VSDESC1"].ToString();
                row["VESLAJET"] = dt.Rows[i]["VESLAJET"].ToString();
                row["BRDESC1"]  = dt.Rows[i]["BRDESC1"].ToString();
                row["VESLGLOS"] = dt.Rows[i]["VESLGLOS"].ToString();
                row["VESLLOGN"] = dt.Rows[i]["VESLLOGN"].ToString();
                row["VESLFLAG"] = dt.Rows[i]["VESLFLAG"].ToString();
                row["KJDESC1"]  = dt.Rows[i]["KJDESC1"].ToString();
                row["VESLCALL"] = dt.Rows[i]["VESLCALL"].ToString();
                row["VEMANIFD"] = dt.Rows[i]["VEMANIFD"].ToString();
                row["VEHOSENO"] = dt.Rows[i]["VEHOSENO"].ToString();
                row["VEPMTYPE"] = dt.Rows[i]["VEPMTYPE"].ToString();
                row["VEPMCAPA"] = dt.Rows[i]["VEPMCAPA"].ToString();

                row["VERATE"]   = dt.Rows[i]["VERATE"].ToString();

                if(dt.Rows[i]["VEWHARF1"].ToString() == "Y")
                {
                    sVEWHARF = "1";
                }

                if (dt.Rows[i]["VEWHARF2"].ToString() == "Y")
                {
                    if (sVEWHARF.ToString() != "")
                    {
                        sVEWHARF = sVEWHARF + "," + "UTT";
                    }
                    else
                    {
                        sVEWHARF = "UTT";
                    }
                }

                if (dt.Rows[i]["VEWHARF3"].ToString() == "Y")
                {
                    if (sVEWHARF.ToString() != "")
                    {
                        sVEWHARF = sVEWHARF + "," + "2";
                    }
                    else
                    {
                        sVEWHARF = "2";
                    }
                }

                if (dt.Rows[i]["VEWHARF4"].ToString() == "Y")
                {
                    if (sVEWHARF.ToString() != "")
                    {
                        sVEWHARF = sVEWHARF + "," + "양곡";
                    }
                    else
                    {
                        sVEWHARF = "양곡";
                    }
                }

                if (sVEWHARF.ToString() != "")
                {
                    sVEWHARF = sVEWHARF + " 부두";
                }

                row["VEWHARF1"]  = sVEWHARF.ToString();
                row["VEBONSON"] = dt.Rows[i]["VEBONSON"].ToString();

                
                Retdt.Rows.Add(row);
            }

            return Retdt;
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTVS002I(string.Empty
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_UT_6BOFI851", dt);
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {   
            DataTable dt = this.FPS91_TY_S_UT_6BOFS853.GetDataSourceInclude(TSpread.TActionType.Remove, "VESLCODE");

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 그리드 더블 클릭
        private void FPS91_TY_S_UT_6BOFS853_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUTVS002I(this.FPS91_TY_S_UT_6BOFS853.GetValue("VESLCODE").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
