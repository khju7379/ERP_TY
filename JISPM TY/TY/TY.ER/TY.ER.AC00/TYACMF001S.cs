using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 통장거래내역관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.10.30 15:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2AU3H919 : 통장거래내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2AU3I922 : 통장거래내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  SAV : 저장
    ///  B1CDBK : 은  행
    ///  B1NOAC : 계좌번호
    ///  B1DATE : 거래일자
    /// </summary>
    public partial class TYACMF001S : TYBase
    {
        private bool _Isloaded = false;

        #region  Description : 폼 로드 이벤트
        public TYACMF001S()
        {
            InitializeComponent();
        }

        private void TYACMF001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_B1CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_B1CDBK_CodeBoxDataBinded);

            this.DTP01_B1DATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP02_B1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B1CDBK.OnCodeBoxDataBinded(null, null); 

            this.SetStartingFocus(this.DTP01_B1DATE);
        }
        #endregion

        #region  Description : 계좌번호 CBH01_B1CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_B1CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_B1CDBK.GetValue().ToString();
            this.CBH01_B1NOAC.DummyValue = groupCode;
            this.CBH01_B1NOAC.SetValue(""); 
            this.CBH01_B1NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_B1NOAC.Initialize();
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2AU3H919", this.CBH01_B1CDBK.GetValue().ToString(), this.CBH01_B1NOAC.GetValue(), this.DTP01_B1DATE.GetString(), this.DTP02_B1DATE.GetString());
            this.FPS91_TY_S_AC_2AU3I922.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2AU3I922.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2AU3I922, "B1CDBKNM", "합   계", SumRowType.Sum, "B1AMIO");

                //this.SetFocus(this.FPS91_TY_S_AC_2AU3I922); 

            }

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACMF001I(string.Empty,string.Empty,string.Empty,string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if ((new TYACMF001B(ds)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2AU3I922_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2AU3I922_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACMF001I(this.FPS91_TY_S_AC_2AU3I922.GetValue("B1CDBK").ToString(),
                                 this.FPS91_TY_S_AC_2AU3I922.GetValue("B1NOAC").ToString(),
                                 this.FPS91_TY_S_AC_2AU3I922.GetValue("B1DATE").ToString(),
                                 this.FPS91_TY_S_AC_2AU3I922.GetValue("B1NOSQ").ToString()
                                 )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_2AU3I922.GetDataSourceInclude(TSpread.TActionType.Select, "B1CDBK","B1CDBKNM", "B1NOAC", "B1DATE", "B1NOSQ", "B1IOGB", "B1YNGB", "B1AMIO", "B1NAME","E3CDAC","E3CDACNM", "B1CDBKNOAC", "B1JPNO" ));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //같은일자 체크 & 차대합계 체크
            string sCkDate = "";
            double dDRAMT = 0;
            double dCRAMT = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["B1IOGB"].ToString().Substring(0, 1) == "1")
                {
                    dDRAMT = dDRAMT + Convert.ToDouble(ds.Tables[0].Rows[i]["B1AMIO"].ToString());
                }
                else
                {
                    dCRAMT = dCRAMT + Convert.ToDouble(ds.Tables[0].Rows[i]["B1AMIO"].ToString());
                }

                if (i == 0)
                {
                    sCkDate = ds.Tables[0].Rows[i]["B1DATE"].ToString();
                }
                else
                {
                    if (sCkDate != ds.Tables[0].Rows[i]["B1DATE"].ToString())
                    {
                        this.ShowMessage("TY_M_GB_26I24916");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (dCRAMT != dDRAMT)
            {
                this.ShowMessage("TY_M_AC_29D5Z005");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AU5A931", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_2AV9K940", dt.Rows[i]["B1CDBK"].ToString(), dt.Rows[i]["B1NOAC"].ToString(), dt.Rows[i]["B1DATE"].ToString(),
                                                            dt.Rows[i]["B1NOSQ"].ToString(), TYUserInfo.EmpNo, "D", sOUTMSG.ToString());

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
            //DataTable dt = this.FPS91_TY_S_AC_2AU3I922.GetDataSourceInclude(TSpread.TActionType.Remove, "B1CDBK", "B1NOAC", "B1DATE", "B1NOSQ");
            DataTable dt = this.FPS91_TY_S_AC_2AU3I922.GetDataSourceInclude(TSpread.TActionType.Select, "B1CDBK", "B1NOAC", "B1DATE", "B1NOSQ");
           

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //전표발행 유무 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B1AV975", dt.Rows[i]["B1CDBK"].ToString(),
                                                            dt.Rows[i]["B1NOAC"].ToString(),
                                                            dt.Rows[i]["B1DATE"].ToString(),
                                                            dt.Rows[i]["B1NOSQ"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
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

        #region Description : FPS91_TY_S_AC_2AU3I922_KeyPress 이벤트
        //private void FPS91_TY_S_AC_2AU3I922_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 13)
        //    {
        //        this.FPS91_TY_S_AC_2AU3I922_CellDoubleClick(null, null);
        //    }
        //}
        #endregion
    }
}
