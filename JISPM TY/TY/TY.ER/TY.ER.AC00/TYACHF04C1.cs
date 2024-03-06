using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 유형자산 자산변경관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.01.03 14:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_A139D648 : 유형자산  자산분류 이력등록
    ///  TY_P_AC_A139X649 : 유형자산  자산분류 이력삭제
    ///  TY_P_AC_A13DS657 : 유형자산 자산분류 이력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_A13DS658 : 유형자산 자산분류 이력 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  FXSCLASS : 자산분류코드
    ///  FXSSEQ : 자산순번
    ///  FXSSUBNUM : 가족코드
    ///  FXSYEAR : 자산년도
    /// </summary>
    public partial class TYACHF04C1 : TYBase
    {
        private string fsFXGYEAR;
        private string fsFXGSEQ;
        private string fsFXGSUBNUM;

        #region  Description : 폼 로드 이벤트
        public TYACHF04C1(string sFXGYEAR, string sFXGSEQ,  string sFXGSUBNUM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsFXGYEAR = sFXGYEAR;
            fsFXGSEQ = sFXGSEQ;
            fsFXGSUBNUM = sFXGSUBNUM;
        }

        private void TYACHF04C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);


            TXT01_FXSYEAR.SetValue(fsFXGYEAR);
            TXT01_FXSSEQ.SetValue(fsFXGSEQ);
            TXT01_FXSSUBNUM.SetValue(fsFXGSUBNUM);

            LBL52_FXSCLASS.Text = "후 자산분류";

            this.DTP01_FXSGETDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT01_FXSYEAR.GetValue().ToString(), this.TXT01_FXSSEQ.GetValue().ToString(), this.TXT01_FXSSUBNUM.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                TXT01_FXSNAME.SetValue(dt.Rows[0]["FXSNAME"].ToString());

                CBH01_FXSCLASS.SetValue(dt.Rows[0]["FXSCLASS"].ToString());
                CBH02_FXSCLASS.SetValue(dt.Rows[0]["FXSCLASS"].ToString());

                CBH01_FXSCLASS.SetReadOnly(true);
            }            

            this.SetStartingFocus(DTP01_FXSGETDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_A13DS658.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_A13DS657", this.TXT01_FXSYEAR.GetValue().ToString(), this.TXT01_FXSSEQ.GetValue().ToString(), this.TXT01_FXSSUBNUM.GetValue());
            this.FPS91_TY_S_AC_A13DS658.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_A13ES659", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_A13DS657", this.TXT01_FXSYEAR.GetValue().ToString(), this.TXT01_FXSSEQ.GetValue().ToString(), this.TXT01_FXSSUBNUM.GetValue());
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_A13FA661", dm.Rows[0]["FXGCLASS"].ToString(), TYUserInfo.EmpNo, this.TXT01_FXSYEAR.GetValue().ToString(), this.TXT01_FXSSEQ.GetValue().ToString(), this.TXT01_FXSSUBNUM.GetValue());
                this.DbConnector.ExecuteTranQuery();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_A13DS658.GetDataSourceInclude(TSpread.TActionType.Remove, "FXGYEAR", "FXGSEQ", "FXGSUBNUM", "FXGSDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_A13F0660", dt.Rows[i]["FXGYEAR"].ToString(), dt.Rows[i]["FXGSEQ"].ToString(), dt.Rows[i]["FXGSUBNUM"].ToString(), dt.Rows[i]["FXGSDATE"].ToString());
                DataTable dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {
                    if (  Convert.ToInt16(dm.Rows[0]["ROWNUM"].ToString()) <= 1 )
                    {
                         this.ShowCustomMessage("삭제 할수 없는 자료입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                         e.Successed = false;
                         return; 
                    }

                    if (Convert.ToInt16(dm.Rows[0]["ROWNUM"].ToString()) > 1 && Convert.ToInt16(dm.Rows[0]["ROWNUM"].ToString()) != Convert.ToInt16(dm.Rows[0]["MAXNUM"].ToString()) )
                    {
                        this.ShowCustomMessage("해당 자료뒤에 자료부터 삭제하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return; 
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_A139D648", this.TXT01_FXSYEAR.GetValue().ToString(), 
                                                        this.TXT01_FXSSEQ.GetValue().ToString(), 
                                                        this.TXT01_FXSSUBNUM.GetValue(),
                                                        DTP01_FXSGETDATE.GetString().ToString(),
                                                        CBH02_FXSCLASS.GetValue().ToString(),
                                                        TXT01_FXSNAME.GetValue().ToString(),
                                                        TYUserInfo.EmpNo 
                                                        );

            this.DbConnector.Attach("TY_P_AC_A13FA661", CBH02_FXSCLASS.GetValue().ToString(), TYUserInfo.EmpNo, this.TXT01_FXSYEAR.GetValue().ToString(), this.TXT01_FXSSEQ.GetValue().ToString(), this.TXT01_FXSSUBNUM.GetValue());
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (CBH01_FXSCLASS.GetValue().ToString() == CBH02_FXSCLASS.GetValue().ToString())
            {
                this.ShowCustomMessage("동일한 자산분류코드 입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
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
