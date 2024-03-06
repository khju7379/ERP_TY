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
    /// 탱크세척 요율표 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.01 15:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_671AL500 : 탱크세척 요율표 조회
    ///  TY_P_UT_671AX503 : 탱크세척 요율표 등록
    ///  TY_P_UT_671E0510 : 탱크세척 요율표 수정
    ///  TY_P_UT_671E1511 : 탱크세척 요율표 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_671AN501 : 탱크세척 요율표 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CNTANKNO : 탱크번호
    /// </summary>
    public partial class TYUTIL001I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL001I()
        {
            InitializeComponent();
        }
        
        private void TYUTIL001I_Load(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();

            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_671AN501, "YOTANK");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            // 최근 등록 자료 조회

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_C3AG5142");

            dt =  this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DTP01_YOYYMM.SetValue(dt.Rows[0]["YOYYMM"].ToString());
            }
            //this.DTP01_YOYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_CNTANKNO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sTANK = this.TXT01_CNTANKNO.GetValue().ToString();

            if(sTANK == "")
            {
                sTANK = "0";
            }

            this.FPS91_TY_S_UT_671AN501.Initialize();

            this.DbConnector.CommandClear();

            //this.DbConnector.Attach("TY_P_UT_671AL500", sTANK);
            this.DbConnector.Attach("TY_P_UT_C2HBC065", DTP01_YOYYMM.GetString().Substring(0,6),  sTANK);

            this.FPS91_TY_S_UT_671AN501.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_671E1511", dt);
            this.DbConnector.Attach("TY_P_UT_C2HBO072", dt);
            
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            double dYOGUMAIK = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //dYOGUMAIK = double.Parse(ds.Tables[0].Rows[i]["YOMENJUK"].ToString()) * double.Parse(ds.Tables[0].Rows[i]["YODANGA"].ToString());

                    dYOGUMAIK = double.Parse(ds.Tables[0].Rows[i]["YOGUMAIK"].ToString());

                    //this.DbConnector.Attach("TY_P_UT_671AX503", ds.Tables[0].Rows[i]["YOTANK"].ToString(),
                    //                                            ds.Tables[0].Rows[i]["YOCAPA"].ToString(),
                    //                                            ds.Tables[0].Rows[i]["YOMENJUK"].ToString(),
                    //                                            ds.Tables[0].Rows[i]["YODANGA"].ToString(),
                    //                                            Convert.ToString(dYOGUMAIK),
                    //                                            TYUserInfo.EmpNo
                    //                                            );
                    this.DbConnector.Attach("TY_P_UT_C2HBS073", ds.Tables[0].Rows[i]["YOYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["YOTANK"].ToString(),
                                                                ds.Tables[0].Rows[i]["YOCAPA"].ToString(),
                                                                ds.Tables[0].Rows[i]["YOMENJUK"].ToString(),
                                                                ds.Tables[0].Rows[i]["YODANGA"].ToString(),
                                                                Convert.ToString(dYOGUMAIK),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    //dYOGUMAIK = double.Parse(ds.Tables[1].Rows[i]["YOMENJUK"].ToString()) * double.Parse(ds.Tables[1].Rows[i]["YODANGA"].ToString());

                    dYOGUMAIK = double.Parse(ds.Tables[1].Rows[i]["YOGUMAIK"].ToString());

                    //this.DbConnector.Attach("TY_P_UT_671E0510", ds.Tables[1].Rows[i]["YOCAPA"].ToString(),
                    //                                            ds.Tables[1].Rows[i]["YOMENJUK"].ToString(),
                    //                                            ds.Tables[1].Rows[i]["YODANGA"].ToString(),
                    //                                            Convert.ToString(dYOGUMAIK),
                    //                                            TYUserInfo.EmpNo,
                    //                                            ds.Tables[1].Rows[i]["YOTANK"].ToString().Trim()
                    //                                            ); 
                    this.DbConnector.Attach("TY_P_UT_C2HBS074", ds.Tables[1].Rows[i]["YOCAPA"].ToString(),
                                                                ds.Tables[1].Rows[i]["YOMENJUK"].ToString(),
                                                                ds.Tables[1].Rows[i]["YODANGA"].ToString(),
                                                                Convert.ToString(dYOGUMAIK),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["YOYYMM"].ToString().Trim(),
                                                                ds.Tables[1].Rows[i]["YOTANK"].ToString().Trim()
                                                                ); 
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_671AN501.GetDataSourceInclude(TSpread.TActionType.New, "YOYYMM", "YOTANK", "YOCAPA", "YOMENJUK", "YODANGA", "YOGUMAIK"));

            ds.Tables.Add(this.FPS91_TY_S_UT_671AN501.GetDataSourceInclude(TSpread.TActionType.Update, "YOYYMM", "YOTANK", "YOCAPA", "YOMENJUK", "YODANGA", "YOGUMAIK"));


            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                //this.DbConnector.Attach("TY_P_UT_671FA519", ds.Tables[0].Rows[i]["YOTANK"].ToString());
                this.DbConnector.Attach("TY_P_UT_C2HBK069", ds.Tables[0].Rows[i]["YOYYMM"].ToString(), 
                                                            ds.Tables[0].Rows[i]["YOTANK"].ToString());
                
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["YOTANK"].ToString() + "] 탱크번호가 이미 등록되어 있습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_671AN501.GetDataSourceInclude(TSpread.TActionType.Remove, "YOYYMM", "YOTANK");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : 로우 추가 이벤트
        private void FPS91_TY_S_UT_671AN501_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_671AN501.SetValue(e.RowIndex, "YOYYMM", this.DTP01_YOYYMM.GetString().Substring(0,6));
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYUTIL001B(this.DTP01_YOYYMM.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
