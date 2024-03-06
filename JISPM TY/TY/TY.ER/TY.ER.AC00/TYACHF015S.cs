using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 유형자산 일괄폐기관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.07.17 15:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_87HGA413 : 유형자산  폐기자산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_87HGA414 : 유형자산 폐기자산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  FXLMASCODE : 자산구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACHF015S: TYBase
    {
        #region Description : 페이지 로드
        public TYACHF015S()
        {
            InitializeComponent();
        }

        private void TYACHF015S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            this.BTN61_JPNO_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_DEL_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            CBO01_INQOPTION.SetValue("2");

            this.SetStartingFocus(this.DTP01_SDATE);            
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {          
            
            this.FPS91_TY_S_AC_87HGA414.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87HGA413", DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString(), CBH01_FXLMASCODE.GetValue(), CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_AC_87HGA414.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 전표발행 처리
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if ((new TYACHF015I(ds, "A")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bCheck = false;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_87HGA414.GetDataSourceInclude(TSpread.TActionType.Select, "FXLYEAR", "FXLSEQ", "FXLSUBNUM", "FXLNUM", "FXSCLASS", "FXLSETDATE", "FXLSETGN", "FXLSETGNNM", "FXSNAME", "FXLSETTEXT", "FXLAMOUNT", "FXLDWAMOUNT", "FXSGETAMOUNT", "FXSAUP", "LASTDATE", "REPAMOUNTSUM", "FXMREPJANAMOUNT","FXLJPNODATE", "FXLUWJPNO","B1HISAB"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 30)
            {
                this.ShowCustomMessage("선택항목이 30개를 초과 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //설정일자가 같아야 한다.
            for( int i = 0; i < ds.Tables[0].Rows.Count; i++ )
            {
                if (ds.Tables[0].Rows[i]["FXLUWJPNO"].ToString() != "")
                {
                    this.ShowCustomMessage("이미 전표발행이 완료된 자산입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                bCheck = UP_SameDay_Check(ds.Tables[0], ds.Tables[0].Rows[i]["FXLSETDATE"].ToString());

                if (!bCheck)
                {
                    this.ShowCustomMessage("동일한 설정년도만 전표발행이 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        } 
        #endregion

        #region Description : 서로 다른일자가 있는지 체크
        private bool UP_SameDay_Check(DataTable dt, string sCheckDate)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FXLSETDATE"].ToString().Substring(0,4) != sCheckDate.Substring(0,4))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Description : 동일 전표번호 체크
        private bool UP_JunPyoNum_Check(DataTable dt, string sCheckData)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FXLUWJPNO"].ToString().Substring(0, 17) != sCheckData.Substring(0, 17))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Description : 전표발행 처리
        private void BTN61_JPNO_DEL_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //전표번호 동일한 자산이력 일괄 조회

            DataSet dsLog = new DataSet();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87JE3432", ds.Tables[0].Rows[0]["FXLUWJPNO"].ToString().Replace("-", "").Substring(0, 17));
            DataSet dLog = this.DbConnector.ExecuteDataSet();

            if ((new TYACHF015I(dLog, "D")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        private void BTN61_JPNO_DEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bCheck = false;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_87HGA414.GetDataSourceInclude(TSpread.TActionType.Select, "FXLYEAR", "FXLSEQ", "FXLSUBNUM", "FXLNUM", "FXSCLASS", "FXLSETDATE", "FXLSETGN", "FXLSETGNNM", "FXSNAME", "FXLSETTEXT", "FXLAMOUNT", "FXLDWAMOUNT", "FXSGETAMOUNT", "FXSAUP", "LASTDATE", "REPAMOUNTSUM", "FXMREPJANAMOUNT", "FXLJPNODATE", "FXLUWJPNO","B1HISAB"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //전표번호가 같아야 한다.
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["FXLUWJPNO"].ToString() == "")
                {
                    this.ShowCustomMessage("전표발행후 취소 할수있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                bCheck = UP_JunPyoNum_Check(ds.Tables[0], ds.Tables[0].Rows[i]["FXLUWJPNO"].ToString());

                if (!bCheck)
                {
                    this.ShowCustomMessage("동일한 전표번호만 전표취소이 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }          

            e.ArgData = ds;

        } 
        #endregion








    }
}