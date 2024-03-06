using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 폐기물 탱크 반출입 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.25 15:31
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BPFJ880 : 폐기물 탱크 반출입 현황
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTDI003S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTDI003S()
        {
            InitializeComponent();
        }

        private void TYUTDI003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
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
            string sDATE = "(" + this.DTP01_STDATE.GetString().Substring(0, 4) + "/" + this.DTP01_STDATE.GetString().Substring(4, 2) + "/" + this.DTP01_STDATE.GetString().Substring(6, 2) + "~"
                                + this.DTP01_EDDATE.GetString().Substring(0, 4) + "/" + this.DTP01_EDDATE.GetString().Substring(4, 2) + "/" + this.DTP01_EDDATE.GetString().Substring(6, 2) + ")";

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6BPFJ880", sDATE,
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTDI003R();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 출력 버튼 체크
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if ( Convert.ToInt32(this.DTP01_STDATE.GetString().ToString().Substring(0, 8)) > Convert.ToInt32((this.DTP01_EDDATE.GetString().ToString().Substring(0, 8))) )
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                this.DTP01_STDATE.Focus(); 
                e.Successed = false;
                return;
            }
        }        
        #endregion

        #region Description : 데이터테이블 변경
        private DataTable Convert_DataTable(DataTable dt)
        {
            DataTable retDt = new DataTable();

            double dSJJEGOQTY = 0;
            double dSFIPGOQTY = 0;
            double dSFCHULQTY = 0;

            retDt.Columns.Add("SFILJA", typeof(System.String));
            retDt.Columns.Add("SFTICKNO", typeof(System.String));
            retDt.Columns.Add("SFTANKNO", typeof(System.String));
            retDt.Columns.Add("HMDESC1", typeof(System.String));
            retDt.Columns.Add("SFIPGOQTY", typeof(System.Double));
            retDt.Columns.Add("SFCHULQTY", typeof(System.Double));
            retDt.Columns.Add("SJJEGOQTY", typeof(System.Double));
            retDt.Columns.Add("ACDESC1", typeof(System.String));
            retDt.Columns.Add("CHDESC1", typeof(System.String));
            retDt.Columns.Add("DATE", typeof(System.String));

            // 폐수 집수조 재고 마지막 재고 체크
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6BPHZ882", DTP01_STDATE.GetString());

            DataTable dtjg = this.DbConnector.ExecuteDataTable();

            if (dtjg.Rows.Count > 0)
            {
                dSJJEGOQTY = double.Parse(dtjg.Rows[0]["SJJEGOQTY"].ToString());
            }

            // 폐수 집수조 반출입 현황
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                row["SFILJA"] = dt.Rows[i]["SFILJA"].ToString();
                // 번호
                row["SFTICKNO"] = dt.Rows[i]["SFTICKNO"].ToString();
                // 탱크번호
                row["SFTANKNO"] = dt.Rows[i]["SFTANKNO"].ToString();
                // 화물명
                row["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                // 입고량
                dSFIPGOQTY = double.Parse(dt.Rows[i]["SFIPGOQTY"].ToString());
                // 출고량
                dSFCHULQTY = double.Parse(dt.Rows[i]["SFCHULQTY"].ToString());
                // 입고량
                row["SFIPGOQTY"] = dSFIPGOQTY;
                // 출고량
                row["SFCHULQTY"] = dSFCHULQTY;
                // 재고량  = 전일재고량 + 입고량 - 출고량
                dSJJEGOQTY = dSJJEGOQTY + dSFIPGOQTY - dSFCHULQTY;
                // 재고량
                row["SJJEGOQTY"] = dSJJEGOQTY;
                // 입고자
                row["ACDESC1"] = dt.Rows[i]["ACDESC1"].ToString();
                // 출고자
                row["CHDESC1"] = dt.Rows[i]["CHDESC1"].ToString();
                // 일자
                row["DATE"] = dt.Rows[i]["DATE"].ToString();
                
                retDt.Rows.Add(row);
            }

            return retDt;
        }
        #endregion
    }
}
