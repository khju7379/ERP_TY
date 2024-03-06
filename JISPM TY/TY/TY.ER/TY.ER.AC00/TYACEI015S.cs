using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 지급어음 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.25 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25P1V652 : 지급어음 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25P64673 : 지급어음 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  F3CLNY : 거래처
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  F3NONY : 어음번호
    /// </summary>
    public partial class TYACEI015S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI015S()
        {
            InitializeComponent();
        }

        private void TYACEI015S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);            

            this.DTP01_STDATE.SetValue(DateTime.Now.AddDays(-30).ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));                      

            this.SetStartingFocus(DTP01_STDATE);  

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25P1V652", TXT01_F3NONY.GetValue(),CBH01_F3CLNY.GetValue(), DTP01_STDATE.GetString(),DTP01_EDDATE.GetString());
            this.FPS91_TY_S_AC_25P64673.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACEI015I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //지급어음 마스타 삭제
                this.DbConnector.Attach("TY_P_AC_25T6A700", dt.Rows[i]["F3NONY"].ToString() );
                //지급어음 내역 삭제
                this.DbConnector.Attach("TY_P_AC_25T6D703", dt.Rows[i]["F3NONY"].ToString(), dt.Rows[i]["F3SSYN"].ToString());
                //어음 수표 용지 수정
                this.DbConnector.Attach("TY_P_AC_25T6F704", "", "", "", "0", dt.Rows[i]["F3CDAC"].ToString(), dt.Rows[i]["F3CDAC"].ToString(), dt.Rows[i]["F3NONY"].ToString());

                if (dt.Rows[i]["M1NOSQ"].ToString() != "")
                {
                    //TY_P_AC_3344M225
                    this.DbConnector.Attach("TY_P_AC_3344M225", "",
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[i]["F3DTIS"].ToString(),
                                                                dt.Rows[i]["F3CLNY"].ToString(),
                                                                dt.Rows[i]["M1NOSQ"].ToString());
                }
            }
            
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");            
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            if (this.FPS91_TY_S_AC_25P64673.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_AC_25P64673.GetDataSourceInclude(TSpread.TActionType.Remove, "F3NONY", "F3SSYN", "F3DTIS", "F3CLNY", "M1NOSQ", "F3CDAC");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //지급어음에 현상태의 이외 경우가 존재하는지 찾기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25U1J725", dt.Rows[i]["F3NONY"].ToString(), dt.Rows[i]["F3SSYN"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_25O4H641");
                    e.Successed = false;
                    return;
                }
                //지급어음 내역 파일의 발행일자가 더 큰지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25U1R726", dt.Rows[i]["F3NONY"].ToString(), dt.Rows[i]["F3SSYN"].ToString(), dt.Rows[i]["F3DTIS"].ToString().Replace("-",""));
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_25U1T727");
                    e.Successed = false;
                    return;
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

        #region Description : 스프레드 CellDoubleClick ProcessCheck 이벤트
        private void FPS91_TY_S_AC_25P64673_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACEI015I(this.FPS91_TY_S_AC_25P64673.GetValue("F3NONY").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
