using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Internal;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// BIN 출고관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.08 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_948BD270 : BIN 출고관리 조회
    ///  TY_P_US_948BI273 : BIN 출고관리 등록
    ///  TY_P_US_948BI274 : BIN 출고관리 수정
    ///  TY_P_US_948BJ275 : BIN 출고관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_96IAG891 : BIN 출고관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BINO : BIN
    /// </summary>
    public partial class TYUSAU003I : TYBase
    {
        string fsGUBUN = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYUSAU003I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNGOKJONG", "GKDESC1", "BNGOKJONG");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNWONSAN",  "WNDESC1", "BNWONSAN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNTJHJ",    "HJDESC1", "BNTJHJ");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNSOSOK",   "SKDESC1", "BNSOSOK");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNHANGCHA", "VSDESC1", "BNHANGCHA");
        }

        public TYUSAU003I(string sGUBUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNGOKJONG", "GKDESC1", "BNGOKJONG");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNWONSAN", "WNDESC1", "BNWONSAN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNTJHJ", "HJDESC1", "BNTJHJ");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNSOSOK", "SKDESC1", "BNSOSOK");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96IAG891, "BNHANGCHA", "VSDESC1", "BNHANGCHA");

            fsGUBUN = sGUBUN;
        }

        private void TYUSAU003I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96IAG891, "BNBINNO");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_BNCHULIL.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GDATE.SetValue(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

            UP_DataBinding();

            this.SetStartingFocus(this.DTP01_BNCHULIL);

            if (fsGUBUN != "")
            {
                this.BTN61_CLO.Visible = true;
            }
            else
            {
                this.BTN61_CLO.Visible = false;

                this.BTN61_BATCH.Location = new System.Drawing.Point(1095, 12);
                this.BTN61_INQ.Location = new System.Drawing.Point(1014, 12);
            }
        }
        #endregion

        #region  Description : 조회 메소드
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_US_96IAG891.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96IAC890", Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()));

            this.FPS91_TY_S_US_96IAG891.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_US_96IAG891.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_96IAG891_Sheet1.Cells[i, 0].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_96IAG891_Sheet1.Cells[i, 6].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_96IAG891_Sheet1.Cells[i, 7].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_96IAG891_Sheet1.Cells[i, 8].Font = new Font("굴림", 9, FontStyle.Bold);
            }

            this.SetFocus(this.DTP01_BNCHULIL);
        }
        #endregion

        #region  Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBinding();
        }
        #endregion

        #region  Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                this.DbConnector.Attach("TY_P_US_96IB3892", Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[0].Rows[i]["BNBINNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNGOKJONG"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNWONSAN"].ToString(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["BNHJEGO"].ToString()),
                                                            ds.Tables[0].Rows[i]["BNCHGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNCHBB"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNTJHJ"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNSOSOK"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNHANGCHA"].ToString()
                                                            );

                // BIN 상태관리 업데이트
                this.DbConnector.Attach("TY_P_US_96IB8895", ds.Tables[0].Rows[i]["BNGOKJONG"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNWONSAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNCHGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNTJHJ"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNSOSOK"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BNHANGCHA"].ToString(),
                                                            Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[0].Rows[i]["BNBINNO"].ToString()
                                                            );
            }                

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                this.DbConnector.Attach("TY_P_US_96IB4893", ds.Tables[1].Rows[i]["BNGOKJONG"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNWONSAN"].ToString(),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["BNHJEGO"].ToString()),
                                                            ds.Tables[1].Rows[i]["BNCHGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNCHBB"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNGUBN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNTJHJ"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNSOSOK"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNHANGCHA"].ToString(),
                                                            Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[1].Rows[i]["BNBINNO"].ToString()
                                                            );

                // BIN 상태관리 업데이트
                this.DbConnector.Attach("TY_P_US_96IB8895", ds.Tables[1].Rows[i]["BNGOKJONG"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNWONSAN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNCHGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNTJHJ"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNSOSOK"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNGUBN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BNHANGCHA"].ToString(),
                                                            Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[1].Rows[i]["BNBINNO"].ToString()
                                                            );
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                this.DbConnector.Attach("TY_P_US_96IB5894", Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[2].Rows[i]["BNBINNO"].ToString());

                // BIN 상태관리 업데이트
                this.DbConnector.Attach("TY_P_US_96IB8895", ds.Tables[2].Rows[i]["BNGOKJONG"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNWONSAN"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNCHGN"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNTJHJ"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNSOSOK"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNGUBN"].ToString(),
                                                            ds.Tables[2].Rows[i]["BNHANGCHA"].ToString(),
                                                            Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[2].Rows[i]["BNBINNO"].ToString()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            UP_DataBinding();

            this.ShowMessage("TY_M_MR_2BF50354");
        }

        protected void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_96IAG891.GetDataSourceInclude(TSpread.TActionType.New,    "BNBINNO", "BNGOKJONG", "BNWONSAN", "BNHJEGO", "BNCHGN", "BNCHBB", "BNGUBN", "BNTJHJ", "BNSOSOK", "BNHANGCHA"));
            ds.Tables.Add(this.FPS91_TY_S_US_96IAG891.GetDataSourceInclude(TSpread.TActionType.Update, "BNBINNO", "BNGOKJONG", "BNWONSAN", "BNHJEGO", "BNCHGN", "BNCHBB", "BNGUBN", "BNTJHJ", "BNSOSOK", "BNHANGCHA"));
            ds.Tables.Add(this.FPS91_TY_S_US_96IAG891.GetDataSourceInclude(TSpread.TActionType.Remove, "BNBINNO", "BNGOKJONG", "BNWONSAN", "BNHJEGO", "BNCHGN", "BNCHBB", "BNGUBN", "BNTJHJ", "BNSOSOK", "BNHANGCHA"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            // 등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //동일코드 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96ICT896", Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                            ds.Tables[0].Rows[i]["BNBINNO"].ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();
                if ( dt.Rows.Count > 0 )
                {
                    this.ShowCustomMessage("동일 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[0].Rows[i]["BNCHGN"].ToString() != "" && ds.Tables[0].Rows[i]["BNCHGN"].ToString() != "Y")
                {
                    this.ShowCustomMessage("출고 가능을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["BNCHGN"].ToString() != "" && ds.Tables[1].Rows[i]["BNCHGN"].ToString() != "Y")
                {
                    this.ShowCustomMessage("출고 가능을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
        
        #region  Description : 일자복사 버튼
        private void BTN61_SILOCODEHELP01_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96IDA898", Get_Date(this.DTP01_BNCHULIL.GetValue().ToString()),
                                                        Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                        Employer.EmpNo,
                                                        sOUT_MSG.ToString()
                                                        );

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 1).ToString() == "I")
            {
                this.ShowCustomMessage(Set_Date(this.DTP01_GDATE.GetValue().ToString()) + "복사가 완료 되었습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.ShowCustomMessage(Set_Date(this.DTP01_GDATE.GetValue().ToString()) + "복사 실패", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_US_96IAG891_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                TButton.ClickEventCheckArgs args = new TButton.ClickEventCheckArgs(true);

                this.BTN61_BATCH_ProcessCheck(this.BTN61_BATCH, args);

                if (args.Successed == true)
                {
                    this.BTN61_BATCH_Click(this.BTN61_BATCH, args);
                }
            }
        }
        #endregion
    }
}