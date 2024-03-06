using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 당기실적 손익 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.10.11 10:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79TDI716 : 당기실적 손익 생성 SP
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    ///  TY_M_MR_35O21733 : 취소하시겠습니까?
    ///  TY_M_MR_35O22734 : 취소할 데이터가 없습니다.
    ///  TY_M_MR_35O2G735 : 취소하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  BSJYYMM : 실적생성년월
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYBSSJ010B : TYBase
    {
        private object _CBH01_BSJYYMM_Value;
        private object _YEAR_Value;
        private object _CBO01_GOKCR_Value;
        private object _WKSABUN_Value;


        #region  Description : 폼 로드 이벤트
        public TYBSSJ010B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYBSSJ010B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;

            this.CBH01_BSJYYMM.SetValue(UP_Get_LastSJYYMM());

            this.SetStartingFocus(this.CBH01_BSJYYMM);

        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _CBH01_BSJYYMM_Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 6);
            _YEAR_Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4);
            _CBO01_GOKCR_Value = CBO01_GOKCR.GetValue().ToString();
            _WKSABUN_Value = TYUserInfo.EmpNo;        
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            _CBH01_BSJYYMM_Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 6);
            _YEAR_Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4);
            _CBO01_GOKCR_Value = CBO01_GOKCR.GetValue().ToString();
            
            if (_CBO01_GOKCR_Value.ToString() == "A")
            {
                //당기실적 마감관리후 생성이 가능하다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7ANBD869", _CBH01_BSJYYMM_Value.ToString(), _CBH01_BSJYYMM_Value.ToString().Substring(0, 4));
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["BLJCHKMC"].ToString() != "Y" || dt.Rows[0]["BLJCHKCM"].ToString() != "Y" || dt.Rows[0]["BLJCHKPR"].ToString() != "Y" || dt.Rows[0]["BLJCHKIN"].ToString() != "Y")
                    {
                        this.ShowCustomMessage("당기실적마감이 완료되어야 생성이 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                //취소시 실적마감이 완료되면 취소할수 없다.
            }

            if (_CBO01_GOKCR_Value.ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_MR_35O21733"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region  Description : BTN61_BATCH_InvokerStart 이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            try
            {
                e.DbConnector.CommandClear();
                e.DbConnector.Attach("TY_P_AC_79TDI716",
                                        _CBO01_GOKCR_Value,
                                        _CBH01_BSJYYMM_Value,
                                        _YEAR_Value,
                                        _WKSABUN_Value,
                                        ""
                                        );
                e.DbConnector.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
            }
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
                    this.ShowMessage("TY_M_GB_26E30875");
                }
                else
                {
                    this.ShowMessage("TY_M_MR_35O2G735");
                }
            }
        }
        #endregion

        #region Description : 최종 실적년월 가져오기
        private string UP_Get_LastSJYYMM()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AKAW859");
            string sYYMM = this.DbConnector.ExecuteScalar().ToString();

            return sYYMM;
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
