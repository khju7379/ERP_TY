using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 고객정보관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.27 09:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92R9P917 : 고객정보 등록(SILO)
    ///  TY_P_US_92RA2918 : 고객정보 수정(SILO)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  EMHWAJU : 화주
    ///  EMSABUN : EMSABUN
    ///  EMADDRS : 주소
    ///  EMDELIVERY : 운송사구분
    ///  EMDYTEL : 담당자번화번호
    ///  EMEMAIL : E-MAIL
    ///  EMGUBUN : 작업구분
    ///  EMIRUM : 대표자명
    ///  EMISANGHO : 거래처상호
    ///  EMNAME : 담당자
    ///  EMORDNAME : 오더입력자
    ///  EMPASGB : 사용유무
    ///  EMPASSWD : 비밀번호
    ///  EMPHONE : 담당자휴대폰
    ///  EMSAUPNO : 사업자등록
    ///  EMTELNUM : 전화번호
    ///  EMUSERID : 아이디
    ///  EMUSNAME : 이름
    /// </summary>
    public partial class TYUSKB014I : TYBase
    {
        private string fsEMUSERID;
        private string fsEMWHAJU;

        #region  Description : 폼 로드 이벤트
        public TYUSKB014I(string sEMUSERID)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsEMUSERID = sEMUSERID;
        }

        private void TYUSKB014I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsEMUSERID))
            {

                this.SetStartingFocus(this.TXT01_EMUSERID);
            }
            else
            {
                UP_DataBinding(fsEMUSERID);                

                this.SetStartingFocus(this.TXT01_EMUSNAME);
            }

        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding(string sEMUSERID)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66TFK461", sEMUSERID);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_EMUSERID.SetValue(dt.Rows[0]["EMUSERID"].ToString());
                TXT01_EMPASSWD.SetValue(dt.Rows[0]["EMPASSWD"].ToString());
                TXT01_EMUSNAME.SetValue(dt.Rows[0]["EMUSNAME"].ToString());
                TXT01_EMTELNUM.SetValue(dt.Rows[0]["EMTELNUM"].ToString());
                TXT01_EMEMAIL.SetValue(dt.Rows[0]["EMEMAIL"].ToString());
                TXT01_EMIRUM.SetValue(dt.Rows[0]["EMIRUM"].ToString());
                TXT01_EMISANGHO.SetValue(dt.Rows[0]["EMISANGHO"].ToString());
                MTB01_EMSAUPNO.SetValue(dt.Rows[0]["EMSAUPNO"].ToString());
                TXT01_EMADDRS.SetValue(dt.Rows[0]["EMADDRS"].ToString());
                CBH01_EMHWAJU.SetValue(dt.Rows[0]["EMHWAJU"].ToString());
                TXT01_EMNAME.SetValue(dt.Rows[0]["EMNAME"].ToString());
                TXT01_EMPHONE.SetValue(dt.Rows[0]["EMPHONE"].ToString());

                fsEMWHAJU = dt.Rows[0]["EMHWAJU"].ToString();

                TXT01_EMUSERID.SetReadOnly(true);
            }

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsEMUSERID))
            {
                this.DbConnector.Attach("TY_P_US_92R9P917", TXT01_EMUSERID.GetValue(),
                                                            TXT01_EMPASSWD.GetValue(),
                                                            TXT01_EMUSNAME.GetValue(),
                                                             TXT01_EMTELNUM.GetValue(),
                                                             TXT01_EMEMAIL.GetValue(),
                                                             TXT01_EMIRUM.GetValue(),
                                                             TXT01_EMISANGHO.GetValue(),
                                                             MTB01_EMSAUPNO.GetValue().ToString().Replace("-",""),
                                                             TXT01_EMADDRS.GetValue(),
                                                             CBH01_EMHWAJU.GetValue(),
                                                             TXT01_EMNAME.GetValue(),
                                                             TXT01_EMPHONE.GetValue(),
                                                             "Y",
                                                             "S",
                                                             TYUserInfo.EmpNo
                    );
            }
            else
            {
                this.DbConnector.Attach("TY_P_US_92RA2918", 
                                                            TXT01_EMPASSWD.GetValue(),
                                                            TXT01_EMUSNAME.GetValue(),
                                                             TXT01_EMTELNUM.GetValue(),
                                                             TXT01_EMEMAIL.GetValue(),
                                                             TXT01_EMIRUM.GetValue(),
                                                             TXT01_EMISANGHO.GetValue(),
                                                             MTB01_EMSAUPNO.GetValue().ToString().Replace("-", ""),
                                                             TXT01_EMADDRS.GetValue(),
                                                             CBH01_EMHWAJU.GetValue(),
                                                             TXT01_EMNAME.GetValue(),
                                                             TXT01_EMPHONE.GetValue(),
                                                             "Y",
                                                             "S",
                                                             TYUserInfo.EmpNo,
                                                             TXT01_EMUSERID.GetValue()
                    );
            }
            this.DbConnector.ExecuteTranQuery();

            fsEMUSERID = TXT01_EMUSERID.GetValue().ToString();
            fsEMWHAJU = CBH01_EMHWAJU.GetValue().ToString();

            UP_DataBinding(fsEMUSERID);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iVNPERCNT = 0;
            int iCNT = 0;

            //id 중복 체크
            if (string.IsNullOrEmpty(this.fsEMUSERID))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66TFK461", TXT01_EMUSERID.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.SetFocus(TXT01_EMUSERID);
                    this.ShowCustomMessage("동일한 ID가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92PHS899", "", MTB01_EMSAUPNO.GetValue().ToString().Replace("-", ""), "", "");
            DataTable dsm = this.DbConnector.ExecuteDataTable();
            if (dsm.Rows.Count <= 0)
            {
                this.SetFocus(MTB01_EMSAUPNO);
                this.ShowCustomMessage("거래처관리에 사업자번호가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            
            // ID 발급제한 체크
            if (CBH01_EMHWAJU.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_BA7EV603", CBH01_EMHWAJU.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    iVNPERCNT = Convert.ToInt32(Get_Numeric(dt.Rows[0]["VNPERCNT"].ToString()));
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_BA7EY604", "S", CBH01_EMHWAJU.GetValue());
                dt = this.DbConnector.ExecuteDataTable();

                iCNT = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                if (string.IsNullOrEmpty(this.fsEMUSERID) || fsEMWHAJU != CBH01_EMHWAJU.GetValue().ToString())
                {
                    iCNT ++;
                }

                if (iCNT > iVNPERCNT)
                {
                    this.SetFocus(CBH01_EMHWAJU.CodeText);
                    this.ShowMessage("TY_M_US_BA7F5606");
                    e.Successed = false;
                    return; 
                }
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

        #region Description : 사업자 번호 이벤트
        private void MTB01_EMSAUPNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92PHS899", "", MTB01_EMSAUPNO.GetValue().ToString().Replace("-", ""), "", "");

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_EMHWAJU.SetValue(dt.Rows[0]["VNCODE"].ToString());
                }
            }
        }
        #endregion
    }
}
