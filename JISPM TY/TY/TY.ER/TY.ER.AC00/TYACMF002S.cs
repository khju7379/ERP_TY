using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 통장거래내역관리(외화) 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.01 13:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B11F980 : 통장거래내역(외화) 조회
    ///  TY_P_AC_2B11J983 : 통장거래내역(외화) 삭제
    ///  TY_P_AC_2B11S987 : 통장거래내역(외화) 일,월 내역 정리 SP
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B11K984 : 통장거래내역(외화) 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  H1CDBK : 은 행
    ///  H1NOAC : 계좌번호
    ///  H1DATE : 거래일자
    /// </summary>
    public partial class TYACMF002S : TYBase
    {
        private bool _Isloaded = false;

        #region  Description : 폼로드 버튼 이벤트
        public TYACMF002S()
        {
            InitializeComponent();
        }

        private void TYACMF002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_H1CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_H1CDBK_CodeBoxDataBinded);

            this.DTP01_H1DATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP02_H1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_H1CDBK.OnCodeBoxDataBinded(null, null);

            this.SetStartingFocus(this.DTP01_H1DATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2B11K984.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2B11F980", this.CBH01_H1CDBK.GetValue().ToString(), this.CBH01_H1NOAC.GetValue(), this.DTP01_H1DATE.GetString(), this.DTP02_H1DATE.GetString());
            this.FPS91_TY_S_AC_2B11K984.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2B11K984.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2B11K984, "H1CDBKNM", "합   계", SumRowType.Sum, "H1AMIO","H1AMWON");
            }

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B11J983", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_2B11S987", dt.Rows[i]["H1CDBK"].ToString(), dt.Rows[i]["H1NOAC"].ToString(), dt.Rows[i]["H1DATE"].ToString(),
                                                            dt.Rows[i]["H1NOSQ"].ToString(), dt.Rows[i]["H1EXGB"].ToString(), TYUserInfo.EmpNo, "D", sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_2B11K984.GetDataSourceInclude(TSpread.TActionType.Remove, "H1CDBK", "H1NOAC", "H1DATE", "H1NOSQ", "H1EXGB");

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

        #region  Description : 계좌번호 CBH01_H1CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_H1CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_H1CDBK.GetValue().ToString();
            this.CBH01_H1NOAC.DummyValue = groupCode;
            this.CBH01_H1NOAC.SetValue("");
            this.CBH01_H1NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_H1NOAC.Initialize();
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACMF002I(string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2B11K984_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2B11K984_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACMF002I(this.FPS91_TY_S_AC_2B11K984.GetValue("H1CDBK").ToString(),
                                this.FPS91_TY_S_AC_2B11K984.GetValue("H1NOAC").ToString(),
                                this.FPS91_TY_S_AC_2B11K984.GetValue("H1DATE").ToString(),
                                this.FPS91_TY_S_AC_2B11K984.GetValue("H1NOSQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
