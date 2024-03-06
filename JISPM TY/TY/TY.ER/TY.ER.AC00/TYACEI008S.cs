using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부도어음 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.24 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25I5Z550 : 할인어음 조회
    ///  TY_P_AC_25L3Z580 : 할인어음 삭제
    ///  TY_P_AC_25L45582 : 받을어음 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25O13633 :  부도어음 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25N1M601 : 이미 승인된 전표입니다.
    ///  TY_M_AC_25N1M602 : 어음상태가 할인인 경우만 삭제 가능합니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_25F8V482 : 전표번호가 존재합니다!
    /// 
    ///  # 필드사전 정보 ####
    
    ///  INQ : 조회
    ///  REM : 삭제
    ///  E6CDCL : 거래처코드
    ///  E7IDBG : 상태구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  E7NONR : 어음번호
    /// </summary>
    public partial class TYACEI008S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI008S()
        {
            InitializeComponent();
        }

        private void TYACEI008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBO01_E7IDBG.SetValue("14"); 
            
            this.SetStartingFocus(DTP01_STDATE);  
        }
        #endregion

        #region Description : 조회  이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25I5Z550", CBO01_E7IDBG.GetValue(), DTP01_STDATE.GetString(), DTP01_EDDATE.GetString(), TXT01_E7NONR.GetValue(), CBH01_E6CDCL.GetValue());
            this.FPS91_TY_S_AC_25O13633.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제  이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //어음내역삭제
                this.DbConnector.Attach("TY_P_AC_25L3Z580", dt.Rows[i]["E7NONR"].ToString(), dt.Rows[i]["E7IDBG"].ToString(),
                                                            dt.Rows[i]["E7DTBG"].ToString());
                //어음마스타수정
                this.DbConnector.Attach("TY_P_AC_25L45582", dt.Rows[i]["E7HIDBG"].ToString(), dt.Rows[i]["E7HDTBG"].ToString(),
                                                            dt.Rows[i]["E7HCDGL"].ToString(), dt.Rows[i]["E7NONR"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_25O13633.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_AC_25O13633.GetDataSourceInclude(TSpread.TActionType.Remove, "E7NONR", "E7IDBG", "E7DTBG", "E7HDAC", "E7HIDBG", "E7HDTBG", "E7HCDGL");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["E7IDBG"].ToString() != "14")
                {
                    this.ShowMessage("TY_M_AC_25N1M602");
                    e.Successed = false;
                    return;
                }
                if (dt.Rows[i]["E7HDAC"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25O4E640", dt.Rows[i]["E7NONR"].ToString(), dt.Rows[i]["E7DTBG"].ToString());
                int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_25O4H641");
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

        #region Description : Spread CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_25O13633_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYACEI008I(this.FPS91_TY_S_AC_25O13633.GetValue("E7NONR").ToString(),
                                                   this.FPS91_TY_S_AC_25O13633.GetValue("E6IDBG").ToString(),
                                                   this.FPS91_TY_S_AC_25O13633.GetValue("E6DTBG").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
