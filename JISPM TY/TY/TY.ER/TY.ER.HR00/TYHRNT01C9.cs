using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 신용카드등 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.12.24 13:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77JC6219 : 연말정산 소득자공제명세서 조회
    ///  TY_P_HR_77JDB223 : 연말정산 소득공제명세 국세청자료 확정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_BCT9Z963 : 연말정산 소득공제명세 신용카드 전용 조회
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
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  WFSABUN : 귀속사번
    ///  WFYEAR : 년    도
    /// </summary>
    public partial class TYHRNT01C9 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C9(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C9_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Spread_PayItemTitle();

            TXT01_WFYEAR.SetValue(fsWKYEAR);
            CBH01_WFSABUN.SetValue(fsWKSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_SAV.Visible = false;
            }

            this.UP_Grid_DataBinding_Card();
            
        }
        #endregion

        #region  Description : 그리드 데이타 바인딩 이벤트(카드사용분)
        private void UP_Grid_DataBinding_Card()
        {
            this.FPS91_TY_S_HR_BCT9Z963.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JC6219", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.SecureKey, "Y");

            DataTable dt = this.DbConnector.ExecuteDataTable();


            this.FPS91_TY_S_HR_BCT9Z963.SetValue(dt);

            if (this.FPS91_TY_S_HR_BCT9Z963.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_BCT9Z963.CurrentRowCount; i++)
                {
                    //국세청 자료는 필드 잠금
                    if (this.FPS91_TY_S_HR_BCT9Z963.GetValue(i, "NTSGUBN").ToString() == "1")
                    {
                        for (int j = 19; j < 29; j++)
                        {
                            this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].Locked = true;
                            this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].BackColor = Color.WhiteSmoke;
                        }
                    }
                    else
                    {
                        for (int j = 19; j < 29; j++)
                        {                           
                            //현금영수증은 잠금
                            if (j == 21 || j == 26 || j == 27 || j == 28)
                            {
                                this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].Locked = true;
                                this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].Text = "";
                                this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].BackColor = Color.WhiteSmoke;
                            }

                            if (j != 21 || j != 26 || j != 27 || j != 28 )
                            {
                                this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].ForeColor = Color.DarkRed;
                                this.FPS91_TY_S_HR_BCT9Z963_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Underline);
                            }
                        }
                    }
                }
            }

        }
        #endregion               

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //신용카드 사용분
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.DbConnector.Attach("TY_P_HR_BCTDS967",
                                                                ds.Tables[0].Rows[i]["WFTAXCARD"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFTAXDEBCARD"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFTAXMARKET"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFTAXPUBTRANS"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFTAXCARDBOOK"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFTAXDEBBOOK"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["WFCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["WFSABUN"].ToString(),
                                                                TYUserInfo.SecureKey,
                                                                "Y",
                                                                ds.Tables[0].Rows[i]["WFJUMIN"].ToString()
                                                               );
                
                }
            }          


            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.UP_ProCedure_FixCall();
            
            this.UP_Grid_DataBinding_Card();
            

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_BCT9Z963.GetDataSourceInclude(TSpread.TActionType.Update, "WFCOMPANY", "WFYEAR", "WFSABUN", "WFJUMIN", "WFCODE", "WFGUBUN", "WFEDUGN", "WFJANG",
                                                                                                       "WFTAXCARD", "WFTAXDEBCARD",  "WFTAXMARKET", "WFTAXPUBTRANS",
                                                                                                       "WFTAXCARDBOOK", "WFTAXDEBBOOK", "WFTAXINCOME20", "WFTAXINCOME21"
                                                                                                       ));

          
            if (ds.Tables[0].Rows.Count == 0 )
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

            e.ArgData = ds;
        }
        #endregion      

        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y", "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion     
        
        #region Description : 소득명세 스프레드 타이틀 변경
        private void UP_Spread_PayItemTitle()
        {
            Int32 iyear = Convert.ToInt16(fsWKYEAR) - 1;
            
            this.FPS91_TY_S_HR_BCT9Z963_Sheet1.ColumnHeader.Cells[0, 27].Value = iyear.ToString() +"년 사용액";
            this.FPS91_TY_S_HR_BCT9Z963_Sheet1.ColumnHeader.Cells[0, 28].Value = fsWKYEAR + "년 사용액";                             
        }
        #endregion


        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion      
       

    }
}
