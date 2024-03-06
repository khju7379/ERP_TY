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
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 입고지시 보류 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.07.20 13:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_87KDE440 : 입고지시 보류 조회 - 오더조회(화주)
    ///  TY_P_UT_87KDE441 : 입고지시 보류 조회 - 오더조회(화주 없이)
    ///  TY_P_UT_87KDG442 : 입고지시 보류 - 보류 구분 업데이트
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_87KDJ443 : 입고지시 보류 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_UT_77KD2242 : 보류 할 자료가 없습니다.
    ///  TY_M_UT_77KD3245 : 해당 데이터를 보류 하시겠습니까?
    ///  TY_M_UT_77KD4246 : 보류 처리 되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTGB025S : TYBase
    {
        #region Description : 폼 로드
        public TYUTGB025S()
        {
            InitializeComponent();
        }

        private void TYUTGB025S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 보류 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sIOHOLD = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["IOHOLD"].ToString() == "보류")
                {
                    sIOHOLD = "";
                }
                else
                {
                    sIOHOLD = "H";
                }
                this.DbConnector.Attach("TY_P_UT_87KDG442", sIOHOLD.ToString(),
                                                            ds.Tables[0].Rows[i]["IOIPDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["IOTKNO"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BF50354");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_87KDJ443.GetDataSourceInclude(TSpread.TActionType.Select, "IOIPDATE", "IOTKNO", "IOHOLD"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sVNRPCODE = string.Empty;

            if (this.CBH01_JIJGHWAJU.GetValue().ToString() != "")
            {
                // 대표거래처 코드 가져오기
                sVNRPCODE = Get_VNCODE(this.CBH01_JIJGHWAJU.GetValue().ToString());
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_JIJGHWAJU.GetValue().ToString() == "")
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_87KDE441",
                   Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                   Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                   );
            }
            else
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_87KDE440",
                   Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                   Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                   sVNRPCODE.ToString()
                   );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_87KDJ443.SetValue(dt);

            if (this.FPS91_TY_S_UT_87KDJ443.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_87KDJ443.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_87KDJ443.GetValue(i, "IOHOLD").ToString() == "보류")
                    {
                        this.FPS91_TY_S_UT_87KDJ443_Sheet1.Cells[i, 16].Font = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_UT_87KDJ443_Sheet1.Cells[i, 16].ForeColor = Color.Blue;
                    }
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
