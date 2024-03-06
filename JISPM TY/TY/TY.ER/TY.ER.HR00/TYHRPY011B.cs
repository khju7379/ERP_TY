using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여계산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.02.16 14:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_52GEE334 : 급여 계산
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_HR_52GEH335 : 급여 생성을 하시겠습니까?
    ///  TY_M_HR_52GEI336 : 급여 생성이 완료되었습니다!
    ///  TY_M_HR_52GEI337 : 급여 생성 작업중 오류가 발생하였습니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  SEL : 선택
    ///  PAYGUBN : 급여구분
    ///  PAYSOPYCODE : 소급급여코드
    ///  PAYAMOUNT : 지급금액
    ///  PAYAPEDATE : 근무일자E
    ///  PAYAPSDATE : 근무일자S
    ///  PAYOTEDATE : OT일자E
    ///  PAYOTSDATE : OT일자S
    ///  PAYSOKP : 소급포함유무
    ///  PAYTAXADJ : 연말정산적용구분
    ///  PAYYYMM : 급여년월
    ///  PTSABUN : 사번
    /// </summary>
    public partial class TYHRPY011B : TYBase
    {

        private object _DTP01_PAYDATE_Value;
        private object _CBO01_PAYGUBN_Value;
        private object _DTP01_PAYJIDATE_Value;
        private object _CBO01_PAYSABUN_Value;
        private object _CBO01_COMSABUN_Value;
        private object _CBO01_GOKCR_Value;
        private object _DTP01_PAYBONUSYYMM_Value;
        private object _CKB01_SOGUB_Value;

        private string fsProceDures;

        #region  Description : 폼로드 이벤트
        public TYHRPY011B()
        {
            InitializeComponent();
        }

        private void TYHRPY011B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;

            this.Initialize_Controls("01");
        }
        #endregion


        #region Description : 급여 계산 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _DTP01_PAYDATE_Value = DTP01_PAYYYMM.GetString().Substring(0, 6);
            _CBO01_PAYGUBN_Value = CBH01_PAYGUBN.GetValue().ToString();
            _DTP01_PAYJIDATE_Value = DTP01_PAYJIDATE.GetString();
            _CBO01_PAYSABUN_Value = CBH02_PTSABUN.GetValue().ToString();
            _CBO01_COMSABUN_Value = TYUserInfo.EmpNo;
            _CBO01_GOKCR_Value = CBO02_GOKCR.GetValue().ToString();
            _DTP01_PAYBONUSYYMM_Value = DTP01_PAYBONUSYYMM.GetString().Substring(0,6);
            _CKB01_SOGUB_Value = CKB01_SOGUB.Checked == true ? "Y" : "N";          

        }

        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach(fsProceDures, _DTP01_PAYDATE_Value, _CBO01_PAYGUBN_Value, _DTP01_PAYJIDATE_Value, _CBO01_PAYSABUN_Value, _CBO01_COMSABUN_Value, _CBO01_GOKCR_Value, _CKB01_SOGUB_Value,"");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowCustomMessage(ds.Tables[0].Rows[0][0].ToString(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                if (_CBO01_GOKCR_Value.ToString() == "A")
                {
                    this.ShowMessage("TY_M_HR_52GEI336");
                }
                else
                {
                    this.ShowMessage("TY_M_HR_52GFV342");
                }
            }
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            _DTP01_PAYDATE_Value = DTP01_PAYYYMM.GetString().Substring(0, 6);
            _CBO01_PAYGUBN_Value = CBH01_PAYGUBN.GetValue().ToString();
            _DTP01_PAYJIDATE_Value = DTP01_PAYJIDATE.GetString();
            _CBO01_PAYSABUN_Value = CBH02_PTSABUN.GetValue().ToString();
            _CBO01_COMSABUN_Value = TYUserInfo.EmpNo;
            _CBO01_GOKCR_Value = CBO02_GOKCR.GetValue().ToString();
            _DTP01_PAYBONUSYYMM_Value = DTP01_PAYBONUSYYMM.GetString().Substring(0,6);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_55RD2343", DTP01_PAYJIDATE.GetString().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                fsProceDures = dt.Rows[0]["PRPROCNO"].ToString();
            }
            else
            {
                this.ShowCustomMessage("등록된 급여 프로시저가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //급여선택 체크
            if (this._CBO01_PAYGUBN_Value.ToString() == "" || this._DTP01_PAYDATE_Value.ToString() == "" || this._DTP01_PAYJIDATE_Value.ToString() == "")
            {
                this.ShowCustomMessage("급여선택을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //급여생성시 상여가 먼저 작업되어야 한다.
            if (this._CBO01_PAYGUBN_Value.ToString() == "M1")
            {
                if (this.CBO02_GOKCR.GetValue().ToString() == "A")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_535G1512", "S1", this._DTP01_PAYBONUSYYMM_Value.ToString(), this._DTP01_PAYJIDATE_Value.ToString());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt <= 0)
                    {
                        this.ShowCustomMessage("급여 작업전 상여가 먼저 생성되어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //급여전표관리 등록시 작업불가
            this.DbConnector.CommandClear();
            if (CBH01_PAYGUBN.GetValue().ToString() == "S1")
            {
                this.DbConnector.Attach("TY_P_HR_5AJFI004", DTP01_PAYYYMM.GetString().Substring(0, 6), "1", "M1", "1", DTP01_PAYJIDATE.GetString().ToString());
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_5AJFI004", DTP01_PAYYYMM.GetString().Substring(0, 6), "1", CBH01_PAYGUBN.GetValue().ToString(), "1", DTP01_PAYJIDATE.GetString().ToString());
            }
            DataTable djp = this.DbConnector.ExecuteDataTable();
            if (djp.Rows.Count > 0)
            {
                this.ShowCustomMessage("급여전표가 완료되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //펌뱅킹 이체시에 처리 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_517BZ067", DTP01_PAYYYMM.GetString().Substring(0, 6), CBH01_PAYGUBN.GetValue().ToString(), DTP01_PAYJIDATE.GetString().ToString(),"00");
            DataTable dp = this.DbConnector.ExecuteDataTable();
            if (dp.Rows.Count > 0)
            {
                this.ShowCustomMessage("급여이체가 완료되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }


            if (CBO01_PAYSOKP.GetValue().ToString() != "Y")
            {
                if (CKB01_SOGUB.Checked == true)
                {
                    this.ShowCustomMessage("소급재반영은 소급반영시만 가능합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }


            if (_CBO01_GOKCR_Value.ToString() == "A")
            {              
                if (!this.ShowMessage("TY_M_HR_52GEH335"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_HR_52GFU341"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 선택버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            TYHRPY006P popup = new TYHRPY006P();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_PAYYYMM.SetValue(popup.fsPAYYYMM);
                this.CBH01_PAYGUBN.SetValue(popup.fsPAYGUBN);
                this.DTP01_PAYJIDATE.SetValue(popup.fsPAYJIDATE);
                                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CIGJ875", popup.fsPAYYYMM, popup.fsPAYGUBN, popup.fsPAYJIDATE);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.DTP01_PAYBONUSYYMM.SetValue(dt.Rows[0]["PAYBONUSYYMM"].ToString());
                    this.DTP01_PAYAPSDATE.SetValue(dt.Rows[0]["PAYAPSDATE"].ToString());
                    this.DTP01_PAYAPEDATE.SetValue(dt.Rows[0]["PAYAPEDATE"].ToString());
                    this.DTP01_PAYOTSDATE.SetValue(dt.Rows[0]["PAYOTSDATE"].ToString());
                    this.DTP01_PAYOTEDATE.SetValue(dt.Rows[0]["PAYOTEDATE"].ToString());
                    this.CBO01_PAYSOKP.SetValue(dt.Rows[0]["PAYSOKP"].ToString());
                    this.CBH01_PAYSOPYCODE.SetValue(dt.Rows[0]["PAYSOPYCODE"].ToString());
                    this.CBO01_PAYTAXADJ.SetValue(dt.Rows[0]["PAYTAXADJ"].ToString());
                    this.TXT01_PAYAMOUNT.SetValue(dt.Rows[0]["PAYAMOUNT"].ToString());
                }

                this.CBH02_PTSABUN.CodeText.Focus();
            }
        }
        #endregion

        

       
    }
}
