using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 계정과목 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.09.03 10:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_393AZ456 : EIS 계정과목 코드 확인
    ///  TY_P_AC_393AZ458 : EIS 계정과목 코드 저장
    ///  TY_P_AC_393B0459 : EIS 계정과목 코드 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  EPCACHL1 : 상위계정코드1
    ///  EPCACHL2 : 상위계정코드2
    ///  EPCACHL3 : 상위계정코드3
    ///  EPCACHL4 : 상위계정코드4
    ///  EPCACHL5 : 상위계정코드5
    ///  EPCIDAC : 계정구분
    ///  EPCLVAC : LEVEL
    ///  EPCTAG01 : 차/대(D/C)
    ///  EPCTAG02 : 전표계정
    ///  EPCYNBS : B/S계정
    ///  EPCYNIS : I/S계정여부
    ///  EPCYNTB : T/B계정여부
    ///  EPCABAC : 계정과목약명
    ///  EPCCDAC : 계정코드
    ///  EPCNMAC : 계정과목명
    /// </summary>
    public partial class TYACPO020I : TYBase
    {
        private string _EPCCDAC;

        public TYACPO020I(string EPCCDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this._EPCCDAC = EPCCDAC;
        }

        #region Description : TYACPO020I_Load
        private void TYACPO020I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(_EPCCDAC))
            {
                this.TXT01_EPCCDAC.SetReadOnly(false);
                this.SetStartingFocus(TXT01_EPCCDAC);
            }
            else
            {
                this.TXT01_EPCCDAC.SetReadOnly(true);
                this.SetStartingFocus(TXT01_EPCNMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_393AZ456", _EPCCDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    //this.CBH01_EPCACHL1.SetValue(dt.Rows[0]["EPCACHL1"]);
                    //this.CBH01_EPCACHL2.SetValue(dt.Rows[0]["EPCACHL2"]);
                    //this.CBH01_EPCACHL3.SetValue(dt.Rows[0]["EPCACHL3"]);
                    //this.CBH01_EPCACHL4.SetValue(dt.Rows[0]["EPCACHL4"]);
                    //this.CBH01_EPCACHL5.SetValue(dt.Rows[0]["EPCACHL5"]);
                    //this.CBO01_EPCIDAC.SetValue(dt.Rows[0]["EPCIDAC"]);
                    //this.CBO01_EPCLVAC.SetValue(dt.Rows[0]["EPCLVAC"]);
                    //this.CKB01_EPCYNBS.SetValue(dt.Rows[0]["EPCYNBS"]);
                    //this.CKB01_EPCYNIS.SetValue(dt.Rows[0]["EPCYNIS"]);
                    //this.CKB01_EPCYNTB.SetValue(dt.Rows[0]["EPCYNTB"]);
                    //this.TXT01_EPCABAC.SetValue(dt.Rows[0]["EPCABAC"]);
                    //this.TXT01_EPCCDAC.SetValue(dt.Rows[0]["EPCCDAC"]);
                    //this.TXT01_EPCNMAC.SetValue(dt.Rows[0]["EPCNMAC"]);
                    //this.TXT01_EPCTAG01.SetValue(dt.Rows[0]["EPCTAG01"]);
                    //this.CKB01_EPCTAG02.SetValue(dt.Rows[0]["EPCTAG02"]);

                }
            }
        } 
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            string sEPCYNTB = "Y";
            string sEPCYNBS = "Y";
            string sEPCYNIS = "Y";
            string sEPCTAG02 = "Y";

            if (this.CKB01_EPCYNTB.GetValue().ToString() != "Y")
            {
                sEPCYNTB = "";
            };
            if (this.CKB01_EPCYNBS.GetValue().ToString() != "Y")
            {
                sEPCYNBS = "";
            };
            if (this.CKB01_EPCYNIS.GetValue().ToString() != "Y")
            {
                sEPCYNIS = "";
            };

            if (this.CKB01_EPCTAG02.GetValue().ToString() != "Y")
            {
                sEPCTAG02 = "";
            };

            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this._EPCCDAC))
            {
                this.DbConnector.Attach("TY_P_AC_393AZ458",
                                        this.TXT01_EPCCDAC.GetValue(),
                                        this.TXT01_EPCNMAC.GetValue(),
                                        this.TXT01_EPCABAC.GetValue(),
                                        this.CBO01_EPCLVAC.GetValue(),
                                        this.CBO01_EPCIDAC.GetValue(),
                                        sEPCYNTB.ToString(),
                                        sEPCYNBS.ToString(),
                                        sEPCYNIS.ToString(),
                                        this.CBH01_EPCACHL1.GetValue(),
                                        this.CBH01_EPCACHL2.GetValue(),
                                        this.CBH01_EPCACHL3.GetValue(),
                                        this.CBH01_EPCACHL4.GetValue(),
                                        this.CBH01_EPCACHL5.GetValue(),
                                        this.TXT01_EPCTAG01.GetValue(),
                                        sEPCTAG02.ToString(),
                                        Employer.UserID); // 저장
                // 태영그레인터미널
                this.DbConnector.Attach("TY_P_AC_3C45E548",
                                        this.TXT01_EPCCDAC.GetValue(),
                                        this.TXT01_EPCNMAC.GetValue(),
                                        this.TXT01_EPCABAC.GetValue(),
                                        this.CBO01_EPCLVAC.GetValue(),
                                        this.CBO01_EPCIDAC.GetValue(),
                                        sEPCYNTB.ToString(),
                                        sEPCYNBS.ToString(),
                                        sEPCYNIS.ToString(),
                                        this.CBH01_EPCACHL1.GetValue(),
                                        this.CBH01_EPCACHL2.GetValue(),
                                        this.CBH01_EPCACHL3.GetValue(),
                                        this.CBH01_EPCACHL4.GetValue(),
                                        this.CBH01_EPCACHL5.GetValue(),
                                        this.TXT01_EPCTAG01.GetValue(),
                                        sEPCTAG02.ToString(),
                                        Employer.UserID); // 태영그레인터미널 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_393B0459",
                                        this.TXT01_EPCNMAC.GetValue(),
                                        this.TXT01_EPCABAC.GetValue(),
                                        this.CBO01_EPCLVAC.GetValue(),
                                        this.CBO01_EPCIDAC.GetValue(),
                                        sEPCYNTB.ToString(),
                                        sEPCYNBS.ToString(),
                                        sEPCYNIS.ToString(),
                                        this.CBH01_EPCACHL1.GetValue(),
                                        this.CBH01_EPCACHL2.GetValue(),
                                        this.CBH01_EPCACHL3.GetValue(),
                                        this.CBH01_EPCACHL4.GetValue(),
                                        this.CBH01_EPCACHL5.GetValue(),
                                        this.TXT01_EPCTAG01.GetValue(),
                                        sEPCTAG02.ToString(),
                                        Employer.UserID,
                                        this.TXT01_EPCCDAC.GetValue()); // 수정
                // 태영그레인터미널
                this.DbConnector.Attach("TY_P_AC_3C45E549",
                                        this.TXT01_EPCNMAC.GetValue(),
                                        this.TXT01_EPCABAC.GetValue(),
                                        this.CBO01_EPCLVAC.GetValue(),
                                        this.CBO01_EPCIDAC.GetValue(),
                                        sEPCYNTB.ToString(),
                                        sEPCYNBS.ToString(),
                                        sEPCYNIS.ToString(),
                                        this.CBH01_EPCACHL1.GetValue(),
                                        this.CBH01_EPCACHL2.GetValue(),
                                        this.CBH01_EPCACHL3.GetValue(),
                                        this.CBH01_EPCACHL4.GetValue(),
                                        this.CBH01_EPCACHL5.GetValue(),
                                        this.TXT01_EPCTAG01.GetValue(),
                                        sEPCTAG02.ToString(),
                                        Employer.UserID,
                                        this.TXT01_EPCCDAC.GetValue()); // 태영그레인터미널 수정
            }

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
