using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
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
    ///  TY_S_US_96I8P881 : BIN 출고관리 조회
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
    public partial class TYUSAU02C1 : TYBase
    {
        private string fsTGNUMNO1 = string.Empty;
        private string fsTGNUMNO2 = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYUSAU02C1(string sTGNUMNO1, string sTGNUMNO2)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96I8P881, "TGGOKJG", "GKDESC1", "TGGOKJG");

            fsTGNUMNO1 = sTGNUMNO1.ToString();
            fsTGNUMNO2 = sTGNUMNO2.ToString();
        }

        private void TYUSAU02C1_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96I8P881, "TGGOKJG");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_DataBinding();
        }
        #endregion

        #region  Description : 데이터 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.TXT01_TGNUMNO1.SetValue(fsTGNUMNO1.ToString());
            this.TXT01_TGNUMNO2.SetValue(fsTGNUMNO2.ToString());

            this.FPS91_TY_S_US_96I8P881.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96I8O880", this.TXT01_TGNUMNO1.GetValue().ToString(), this.TXT01_TGNUMNO2.GetValue().ToString());

            this.FPS91_TY_S_US_96I8P881.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBinding();
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                this.DbConnector.Attach("TY_P_US_96I8W883", this.TXT01_TGNUMNO1.GetValue().ToString(),
                                                            this.TXT01_TGNUMNO2.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["TGGOKJG"].ToString(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["TGJUNGRY"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["TGEMPTY"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["TGMTQTY"].ToString()),
                                                            Employer.EmpNo.ToString()
                                                            );
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                this.DbConnector.Attach("TY_P_US_96I8X884", Get_Numeric(ds.Tables[1].Rows[i]["TGJUNGRY"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["TGEMPTY"].ToString()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["TGMTQTY"].ToString()),
                                                            Employer.EmpNo.ToString(),
                                                            this.TXT01_TGNUMNO1.GetValue().ToString(),
                                                            this.TXT01_TGNUMNO2.GetValue().ToString(),
                                                            ds.Tables[1].Rows[i]["TGGOKJG"].ToString()
                                                            );
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                this.DbConnector.Attach("TY_P_US_96I8Y885", this.TXT01_TGNUMNO1.GetValue().ToString(),
                                                            this.TXT01_TGNUMNO2.GetValue().ToString(),
                                                            ds.Tables[2].Rows[i]["TGGOKJG"].ToString()
                                                            );
            }
            this.DbConnector.ExecuteNonQueryList();
            // TYSCMLIB 로 변경 후 TransQueryList 로 변경
            

            UP_DataBinding();

            this.ShowMessage("TY_M_MR_2BF50354");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_96I8P881.GetDataSourceInclude(TSpread.TActionType.New,    "TGGOKJG", "TGJUNGRY", "TGEMPTY", "TGMTQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_96I8P881.GetDataSourceInclude(TSpread.TActionType.Update, "TGGOKJG", "TGJUNGRY", "TGEMPTY", "TGMTQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_96I8P881.GetDataSourceInclude(TSpread.TActionType.Remove, "TGGOKJG", "TGJUNGRY", "TGEMPTY", "TGMTQTY"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            //동일코드 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96I8U882", this.TXT01_TGNUMNO1.GetValue().ToString(),
                                                            this.TXT01_TGNUMNO2.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["TGGOKJG"].ToString()
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if ( dt.Rows.Count > 0 )
                {
                    this.ShowCustomMessage("동일 곡종코드가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
               

       
    }
}
