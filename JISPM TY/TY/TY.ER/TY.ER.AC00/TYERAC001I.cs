using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 계정 과목 코드 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.20 15:43
    /// </summary>
    public partial class TYERAC001I : TYBase
    {
        private string _A1CDAC;
        private TYData DAT01_A1HISAB;

        public TYERAC001I(string A1CDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this._A1CDAC = A1CDAC;
            this.DAT01_A1HISAB = new TYData("DAT01_A1HISAB", Employer.UserID);
        }

        #region Description : Page_Load
        private void TYERAC001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_A1HISAB);

            if (string.IsNullOrEmpty(this._A1CDAC))
            {
                this.TXT01_A1CDAC.SetReadOnly(false);
                this.SetStartingFocus(this.TXT01_A1CDAC);
            }
            else
            {
                this.TXT01_A1CDAC.SetReadOnly(true);
                this.SetStartingFocus(this.TXT01_A1NMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3L884", this._A1CDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
        } 
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CKB01_A1TAG02.GetValue().ToString() == "Y")
            {
                //if (this.CBH01_A1ODAC.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_AC_25H6L538");
                //    this.CBH01_A1ODAC.Focus();
                //    e.Successed = false;
                //    return;
                //}

                if (this.CBH01_A1GAAP.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_25H6M539");
                    this.CBH01_A1GAAP.Focus();
                    e.Successed = false;
                    return;
                }

                if (this.CBH01_A1IFRS_.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_25H6M540");
                    this.CBH01_A1IFRS_.Focus();
                    e.Successed = false;
                    return;
                }

                // 재무상태표 관련 EIS 처리 안함 (향후 추가 해야 할 사항)
                // 전표계정이면서 = 자산,부채,자본 이면 EIS 계정과목에 전표계정 "Y" 가 있어야 한다. (구분손익 대차대조표 용)
                //if (this.CBH01_A1EISC.GetValue().ToString() == "")
                //{
                //    if (this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "1" ||
                //        this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "2" ||
                //        this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "3")
                //    {
                //        this.ShowMessage("TY_M_AC_2671K818");
                //        this.CBH01_A1EISC.Focus();
                //        e.Successed = false;
                //        return;
                //    }
                //}
                //else
                //{
                //    if (this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "1" ||
                //        this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "2" ||
                //        this.TXT01_A1CDAC.GetValue().ToString().Substring(0, 1) == "3")
                //    {
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_2672T819", this.CBH01_A1EISC.GetValue());
                //        DataTable dt = this.DbConnector.ExecuteDataTable();

                //        if (dt.Rows.Count == 0)
                //        {
                //            this.ShowMessage("TY_M_AC_2671K818");
                //            this.CBH01_A1EISC.Focus();
                //            e.Successed = false;
                //            return;
                //        }
                //    }
                //}
                
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {


            string sA1YNTBD =  string.Empty; // 일계표
            string sA1YNTB_ = string.Empty; // T/B
            string sA1YNBS = string.Empty; // B/S
            string sA1YNIS = string.Empty;  // I/S
            string sA1YNCM = string.Empty;  // 제조원가

            string sA1TAG02 = string.Empty;
            string sA1TAG06 = string.Empty;
            string sA1TAG07 = string.Empty;
            string sA1TAG09 = string.Empty;


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3L884", this.TXT01_A1CDAC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            if (dt.Rows.Count <= 0)
            {
                if (string.IsNullOrEmpty(this._A1CDAC))
                {

                    // 일계표
                    if (this.CKB01_A1YNTBD.GetValue().ToString() == "Y") { sA1YNTBD = "Y"; } else { sA1YNTBD = " "; };
                    // T/B
                    if (this.CKB01_A1YNTB_.GetValue().ToString() == "Y") { sA1YNTB_ = "Y"; } else { sA1YNTB_ = ""; };
                    // B/S
                    if (this.CKB01_A1YNBS.GetValue().ToString() == "Y") { sA1YNBS = "Y"; } else { sA1YNBS = ""; };
                    // I/S
                    if (this.CKB01_A1YNIS.GetValue().ToString() == "Y") { sA1YNIS = "Y"; } else { sA1YNIS = ""; };
                    // 제조원가
                    if (this.CKB01_A1YNCM.GetValue().ToString() == "Y") { sA1YNCM = "Y"; } else { sA1YNCM = ""; };


                    // 전표계정
                    if (this.CKB01_A1TAG02.GetValue().ToString() == "Y") { sA1TAG02 = "Y"; } else { sA1TAG02 = ""; };
                    // 예산통제
                    if (this.CKB01_A1TAG06.GetValue().ToString() == "Y") { sA1TAG06 = "Y"; } else { sA1TAG06 = ""; };
                    // 반제관리
                    if (this.CKB01_A1TAG07.GetValue().ToString() == "Y") { sA1TAG07 = "Y"; } else { sA1TAG07 = ""; };
                    // 접대비
                    if (this.CKB01_A1TAG09.GetValue().ToString() == "Y") { sA1TAG09 = "Y"; } else { sA1TAG09 = ""; };

                    //this.DbConnector.Attach("TY_P_AC_23N3M886", this.ControlFactory, "01");

                    this.DbConnector.Attach("TY_P_AC_23N3M886",
                        this.TXT01_A1CDAC.GetValue().ToString().Trim(),
                        this.TXT01_A1NMAC.GetValue().ToString().Trim(),
                        this.TXT01_A1ABAC.GetValue().ToString().Trim(),
                        this.TXT01_A1NMHNAC.GetValue().ToString().Trim(),
                        this.TXT01_A1NMENAC.GetValue().ToString().Trim(),
                        this.CBO01_A1LVAC.GetValue().ToString().Trim(),
                        this.CBO01_A1IDAC.GetValue().ToString().Trim(),
                        sA1YNTBD, // 일계표
                        sA1YNTB_, // T/B
                        sA1YNBS,  // B/S
                        sA1YNIS,  // I/S
                        sA1YNCM,  // 제조원가
                        this.CBH01_A1ACHL1.GetValue().ToString().Trim(),
                        this.CBH01_A1ACHL2.GetValue().ToString().Trim(),
                        this.CBH01_A1ACHL3.GetValue().ToString().Trim(),
                        this.CBH01_A1ACHL4.GetValue().ToString().Trim(),
                        this.CBH01_A1ACHL5.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI1.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI2.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI3.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI4.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI5.GetValue().ToString().Trim(),
                        this.CBH01_A1CDMI6.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI1.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI2.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI3.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI4.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI5.GetValue().ToString().Trim(),
                        this.CBO01_A1OTMI6.GetValue().ToString().Trim(),
                        this.CBO01_A1TAG01.GetValue().ToString().Trim(),
                        sA1TAG02,
                        this.CBO01_A1TAG03.GetValue().ToString().Trim(),
                        this.CBO01_A1TAG04.GetValue().ToString().Trim(),
                        this.TXT01_A1TAG05.GetValue().ToString().Trim(),
                        sA1TAG06,
                        sA1TAG07,
                        sA1TAG09,
                        this.CBO01_A1TAG10.GetValue().ToString().Trim(),
                        this.CBO01_A1TAG11.GetValue().ToString().Trim(),
                        this.CBH01_A1DRFD.GetValue().ToString().Trim(),
                        this.CBH01_A1CRFD.GetValue().ToString().Trim(),
                        this.CBH01_A1ODAC.GetValue().ToString().Trim(),
                        this.CBH01_A1GAAP.GetValue().ToString().Trim(),
                        this.CBH01_A1IFRS_.GetValue().ToString().Trim(),
                        this.CBH01_A1IFRS1.GetValue().ToString().Trim(),
                        this.CBH01_A1EISC.GetValue().ToString().Trim(),
                        Employer.UserID
                 );

                }
            }
            else
            {

                // 일계표
                if (this.CKB01_A1YNTBD.GetValue().ToString() == "Y") { sA1YNTBD = "Y" ;} else {sA1YNTBD = " ";};
                // T/B
                if (this.CKB01_A1YNTB_.GetValue().ToString() == "Y") { sA1YNTB_ = "Y" ; } else {sA1YNTB_ ="";};
                // B/S
                if (this.CKB01_A1YNBS.GetValue().ToString() == "Y") { sA1YNBS = "Y" ; } else {sA1YNBS ="";};
                // I/S
                if (this.CKB01_A1YNIS.GetValue().ToString() == "Y") { sA1YNIS = "Y"; } else { sA1YNIS = ""; };
                // 제조원가
                if (this.CKB01_A1YNCM.GetValue().ToString() == "Y") { sA1YNCM = "Y"; } else { sA1YNCM = ""; };


                // 전표계정
                if (this.CKB01_A1TAG02.GetValue().ToString() == "Y") { sA1TAG02 = "Y"; } else { sA1TAG02 = ""; };
                // 예산통제
                if (this.CKB01_A1TAG06.GetValue().ToString() == "Y") { sA1TAG06 = "Y"; } else { sA1TAG06 = ""; };
                // 반제관리
                if (this.CKB01_A1TAG07.GetValue().ToString() == "Y") { sA1TAG07 = "Y"; } else { sA1TAG07 = ""; };
                // 접대비
                if (this.CKB01_A1TAG09.GetValue().ToString() == "Y") { sA1TAG09 = "Y"; } else { sA1TAG09 = ""; };


                //this.DbConnector.Attach("TY_P_AC_23N3M885", this.ControlFactory, "01");

                this.DbConnector.Attach("TY_P_AC_23N3M885",
                                        this.TXT01_A1NMAC.GetValue().ToString().Trim(),
                                        this.TXT01_A1ABAC.GetValue().ToString().Trim(),
                                        this.TXT01_A1NMHNAC.GetValue().ToString().Trim(),
                                        this.TXT01_A1NMENAC.GetValue().ToString().Trim(),
                                        this.CBO01_A1LVAC.GetValue().ToString().Trim(),
                                        this.CBO01_A1IDAC.GetValue().ToString().Trim(),
                                        sA1YNTBD, // 일계표
                                        sA1YNTB_, // T/B
                                        sA1YNBS,  // B/S
                                        sA1YNIS,  // I/S
                                        sA1YNCM,  // 제조원가
                                        this.CBH01_A1ACHL1.GetValue().ToString().Trim(),
                                        this.CBH01_A1ACHL2.GetValue().ToString().Trim(),
                                        this.CBH01_A1ACHL3.GetValue().ToString().Trim(),
                                        this.CBH01_A1ACHL4.GetValue().ToString().Trim(),
                                        this.CBH01_A1ACHL5.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI1.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI2.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI3.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI4.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI5.GetValue().ToString().Trim(),
                                        this.CBH01_A1CDMI6.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI1.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI2.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI3.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI4.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI5.GetValue().ToString().Trim(),
                                        this.CBO01_A1OTMI6.GetValue().ToString().Trim(),
                                        this.CBO01_A1TAG01.GetValue().ToString().Trim(),
                                        sA1TAG02,
                                        this.CBO01_A1TAG03.GetValue().ToString().Trim(),
                                        this.CBO01_A1TAG04.GetValue().ToString().Trim(),
                                        this.TXT01_A1TAG05.GetValue().ToString().Trim(),
                                        sA1TAG06,
                                        sA1TAG07,
                                        sA1TAG09,
                                        this.CBO01_A1TAG10.GetValue().ToString().Trim(),
                                        this.CBO01_A1TAG11.GetValue().ToString().Trim(),
                                        this.CBH01_A1DRFD.GetValue().ToString().Trim(),
                                        this.CBH01_A1CRFD.GetValue().ToString().Trim(),
                                        this.CBH01_A1ODAC.GetValue().ToString().Trim(),
                                        this.CBH01_A1GAAP.GetValue().ToString().Trim(),
                                        this.CBH01_A1IFRS_.GetValue().ToString().Trim(),
                                        this.CBH01_A1IFRS1.GetValue().ToString().Trim(),
                                        this.CBH01_A1EISC.GetValue().ToString().Trim(),
                                        Employer.UserID,
                                        this.TXT01_A1CDAC.GetValue().ToString().Trim()
                                 );
            }

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        } 
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}
