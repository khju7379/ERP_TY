using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금항목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.30 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23U3Z210 : 자금항목코드 등록
    ///  TY_P_AC_23U40211 : 자금항목코드 수정
    ///  TY_P_AC_23U43214 : 자금항목코드 확인
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
    ///  A3FDHL1 : 상위항목코드１
    ///  A3FDHL2 : 상위항목코드２
    ///  A3FDHL3 : 상위항목코드３
    ///  A3IDPL  : LEVEL
    ///  A3YNSL  : 전표발생단위Y/N
    ///  A3ABFD  : 자금항목약명
    ///  A3CDFD  : 자금항목코드
    ///  A3HISAB : 작성사번
    ///  A3NMFD  : 자금항목명
    /// </summary>
    public partial class TYACTX003I : TYBase
    {
        string fsAMIYEAR    = string.Empty;
        string fsAMIREPGB   = string.Empty;
        string fsAMIRPTGUBN = string.Empty;
        string fsAMIBRANCH  = string.Empty;
        string fsAMITAXGUBN = string.Empty;
        string fsAMIDEALDT  = string.Empty;
        string fsAMIVNEDCD  = string.Empty;
        string fsAMISAUPNO  = string.Empty;
        string fsGUBUN      = string.Empty;

        #region Description : 페이지 로드
        public TYACTX003I(string sAMIYEAR,   string sAMIREPGB,   string sAMIRPTGUBN,
                          string sAMIBRANCH, string sAMITAXGUBN, string sAMIDEALDT,
                          string sAMIVNEDCD, string sAMISAUPNO,  string sGUBUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsAMIYEAR    = sAMIYEAR.ToString();
            fsAMIREPGB   = sAMIREPGB.ToString();
            fsAMIRPTGUBN = sAMIRPTGUBN.ToString();
            fsAMIBRANCH  = sAMIBRANCH.ToString();
            fsAMITAXGUBN = sAMITAXGUBN.ToString();
            fsAMIDEALDT  = sAMIDEALDT.ToString();
            fsAMIVNEDCD  = sAMIVNEDCD.ToString();
            fsAMISAUPNO  = sAMISAUPNO.ToString();
            fsGUBUN      = sGUBUN.ToString();
        }

        private void TYACTX003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (fsAMIRPTGUBN.ToString() != "")
            {
                this.CBH01_AMITAXGUBN.DummyValue = fsAMIRPTGUBN.ToString();
            }
            else
            {
                this.CBH01_AMITAXGUBN.DummyValue = this.CBO01_AMIRPTGUBN.GetValue().ToString();
            }

            this.TXT01_AMIDELADD.SetReadOnly(true);
            this.TXT01_AMIPENADD.SetReadOnly(true);
            this.TXT01_AMIPAYADD.SetReadOnly(true);

            this.TXT01_AMIVALAMT.SetReadOnly(false);
            this.TXT01_AMITAXAMT.SetReadOnly(false);

            if (string.IsNullOrEmpty(fsGUBUN))
            {
                UP_ReadOnly(false);

                this.TXT01_AMIYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

                SetStartingFocus(this.TXT01_AMIYEAR);
            }
            else
            {
                UP_ReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BM55423",
                    fsAMIYEAR.ToString(),
                    fsAMIREPGB.ToString(),
                    fsAMIRPTGUBN.ToString(),
                    fsAMIBRANCH.ToString(),
                    fsAMITAXGUBN.ToString(),
                    fsAMIDEALDT.ToString(),
                    fsAMIVNEDCD.ToString(),
                    fsAMISAUPNO.ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                // 마감체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42B8L317",
                    this.TXT01_AMIYEAR.GetValue().ToString(),
                    this.CBO01_AMIBRANCH.GetValue().ToString(),
                    this.CBO01_AMIREPGB.GetValue().ToString(),
                    "2"
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.TXT01_AMIVALAMT.SetReadOnly(true);
                    this.TXT01_AMITAXAMT.SetReadOnly(true);
                }
                else
                {
                    SetStartingFocus(this.TXT01_AMIVALAMT);
                }
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsGUBUN))
                // 등록
                this.DbConnector.Attach("TY_P_AC_3BM55424", this.ControlFactory, "01");
            else
                // 수정
                this.DbConnector.Attach("TY_P_AC_3BM56425", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            if ((new TYACTX019S(this.TXT01_AMIYEAR.GetValue().ToString(),        this.CBO01_AMIBRANCH.GetValue().ToString(),
                                this.CBO01_AMIREPGB.GetValue().ToString() + "2", "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ShowMessage("TY_M_GB_23NAD873");
                this.Close();
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sAMITAXGUBN = string.Empty;

            if (string.IsNullOrEmpty(fsGUBUN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BM55423",
                    this.TXT01_AMIYEAR.GetValue().ToString(),
                    this.CBO01_AMIREPGB.GetValue().ToString(),
                    this.CBO01_AMIRPTGUBN.GetValue().ToString(),
                    this.CBO01_AMIBRANCH.GetValue().ToString(),
                    this.CBH01_AMITAXGUBN.GetValue().ToString(),
                    this.DTP01_AMIDEALDT.GetValue().ToString(),
                    this.CBH01_AMIVNEDCD.GetValue().ToString(),
                    this.TXT01_AMISAUPNO.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_AMIYEAR.GetValue().ToString(),
                this.CBO01_AMIBRANCH.GetValue().ToString(),
                this.CBO01_AMIREPGB.GetValue().ToString(),
                "2"
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            // 기준년도와 거래일자 년도는 같아야 한다.
            if (this.TXT01_AMIYEAR.GetValue().ToString() != this.DTP01_AMIDEALDT.GetValue().ToString().Substring(0, 4))
            {
                this.ShowMessage("TY_M_AC_3BP44447");
                e.Successed = false;
                return;
            }

            // 1기일경우 = 1~3월까지, 2기일 경우 = 7~9월까지
            //if (this.CBO01_AMIREPGB.GetValue().ToString() == "1")
            //{
            //    if(int.Parse(this.DTP01_AMIDEALDT.GetValue().ToString().Substring(4,2)) >= 4)
            //    {
            //        this.ShowMessage("TY_M_AC_3BP45448");
            //        e.Successed = false;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (int.Parse(this.DTP01_AMIDEALDT.GetValue().ToString().Substring(4, 2)) <= 6)
            //    {
            //        this.ShowMessage("TY_M_AC_3BP4A449");
            //        e.Successed = false;
            //        return;
            //    }

            //    if (int.Parse(this.DTP01_AMIDEALDT.GetValue().ToString().Substring(4, 2)) >= 10)
            //    {
            //        this.ShowMessage("TY_M_AC_3BP4A449");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            sAMITAXGUBN = this.CBH01_AMITAXGUBN.GetValue().ToString();

            DataTable dt = new DataTable();

            double dAMT    = 0;
            double dBUNJA  = 0;
            double dBUNMO  = 0;
            double dSEYUL  = 0;
            double dILSU   = 0;

            double dAMIDELADD = 0;
            double dAMIPENADD = 0;
            double dAMIPAYADD = 0;

            string sAMIPENADD = string.Empty;

            sAMIPENADD = "0";

            //지연제출(가)         = 11,19,61,68,69,12,62의 공급가액 x 지연제출세율
            //신고불성실(나)       = 11,19,61,68,69,20의 세액 x 신고불성실 세율
            //납부불성실(다)       = 부가세구분(11,19,61,68,69,20)의 세액 x 경과일수 x 납부불성실 세율
            //영세율신고불성실(라) = 12,13,62의 공급가액 x 영세율신고부성실 세율
            //신고불성실           = (나) + (라)

            // 지연제출
            if (sAMITAXGUBN.ToString() == "11" ||
                sAMITAXGUBN.ToString() == "12" ||
                sAMITAXGUBN.ToString() == "19" ||
                sAMITAXGUBN.ToString() == "61" ||
                sAMITAXGUBN.ToString() == "62" ||
                sAMITAXGUBN.ToString() == "68" ||
                sAMITAXGUBN.ToString() == "69")
            {
                dAMIDELADD = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CVAU905",
                    "1",
                    this.TXT01_AMIYEAR.GetValue().ToString(),
                    this.CBO01_AMIBRANCH.GetValue().ToString(),
                    this.CBO01_AMIREPGB.GetValue().ToString(),
                    this.CBO01_AMIRPTGUBN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dAMT    = double.Parse(Get_Numeric(this.TXT01_AMIVALAMT.GetValue().ToString()));

                    dBUNJA = double.Parse(dt.Rows[0]["BUNJA"].ToString());
                    dBUNMO = double.Parse(dt.Rows[0]["BUNMO"].ToString());

                    dSEYUL = double.Parse(string.Format("{0:###.00000}", dBUNJA / dBUNMO));

                    dAMIDELADD = double.Parse(string.Format("{0:###0}", dAMT * dSEYUL));

                    this.TXT01_AMIDELADD.SetValue(Convert.ToString(string.Format("{0:###0}", dAMIDELADD)));
                }
            }

            // 신고불성실
            if (sAMITAXGUBN.ToString() == "11" ||
                sAMITAXGUBN.ToString() == "19" ||
                sAMITAXGUBN.ToString() == "20" ||
                sAMITAXGUBN.ToString() == "61" ||
                sAMITAXGUBN.ToString() == "68" ||
                sAMITAXGUBN.ToString() == "69")
            {
                if (double.Parse(Get_Numeric(this.TXT01_AMITAXAMT.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_3CVB8907");
                    e.Successed = false;
                    return;
                }
                else
                {
                    dAMIPENADD = 0;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CVAU905",
                        "2",
                        this.TXT01_AMIYEAR.GetValue().ToString(),
                        this.CBO01_AMIBRANCH.GetValue().ToString(),
                        this.CBO01_AMIREPGB.GetValue().ToString(),
                        this.CBO01_AMIRPTGUBN.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dAMT   = double.Parse(Get_Numeric(this.TXT01_AMITAXAMT.GetValue().ToString()));

                        dBUNJA = double.Parse(dt.Rows[0]["BUNJA"].ToString());
                        dBUNMO = double.Parse(dt.Rows[0]["BUNMO"].ToString());

                        dSEYUL = double.Parse(string.Format("{0:###.00000}", dBUNJA / dBUNMO));

                        dAMIPENADD = double.Parse(string.Format("{0:###0}", dAMT * dSEYUL));

                        sAMIPENADD = Convert.ToString(string.Format("{0:###0}", dAMIPENADD));

                        //this.TXT01_AMIPENADD.SetValue(Convert.ToString(string.Format("{0:###0}", dAMIPENADD)));
                    }
                }
            }

            // 납부불성실
            if (sAMITAXGUBN.ToString() == "11" ||
                sAMITAXGUBN.ToString() == "19" ||
                sAMITAXGUBN.ToString() == "20" ||
                sAMITAXGUBN.ToString() == "61" ||
                sAMITAXGUBN.ToString() == "68" ||
                sAMITAXGUBN.ToString() == "69")
            {
                if (double.Parse(Get_Numeric(this.TXT01_AMITAXAMT.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_3CVB8907");
                    e.Successed = false;
                    return;
                }
                else
                {
                    string sSTDATE = string.Empty;
                    string sEDDATE = string.Empty;

                    // 1기 - 4/26~7/25
                    // 2기 - 10/26~다음해1/25

                    if (this.CBO01_AMIREPGB.GetValue().ToString() == "1")
                    {
                        sSTDATE = this.TXT01_AMIYEAR.GetValue().ToString() + "0426";
                        sEDDATE = this.TXT01_AMIYEAR.GetValue().ToString() + "0725";
                    }
                    else
                    {
                        sSTDATE = this.TXT01_AMIYEAR.GetValue().ToString() + "1026";
                        sEDDATE = Convert.ToString(double.Parse(this.TXT01_AMIYEAR.GetValue().ToString()) + 1) + "0126";
                    }

                    // 경과일수
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CV9Y904",
                        sEDDATE.ToString(),
                        sSTDATE.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dILSU = double.Parse(dt.Rows[0]["ILSU"].ToString());
                    }

                    dAMIPAYADD = 0;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CVAU905",
                        "3",
                        this.TXT01_AMIYEAR.GetValue().ToString(),
                        this.CBO01_AMIBRANCH.GetValue().ToString(),
                        this.CBO01_AMIREPGB.GetValue().ToString(),
                        this.CBO01_AMIRPTGUBN.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dAMT   = double.Parse(Get_Numeric(this.TXT01_AMITAXAMT.GetValue().ToString()));

                        dBUNJA = double.Parse(dt.Rows[0]["BUNJA"].ToString());
                        dBUNMO = double.Parse(dt.Rows[0]["BUNMO"].ToString());

                        dSEYUL = double.Parse(string.Format("{0:###.00000}", dBUNJA / dBUNMO));

                        dAMIPAYADD = double.Parse(string.Format("{0:###0}", dAMT * dILSU * dSEYUL));

                        this.TXT01_AMIPAYADD.SetValue(Convert.ToString(string.Format("{0:###0}", dAMIPAYADD)));
                    }
                }
            }

            // 영세율신고불성실
            if (sAMITAXGUBN.ToString() == "12" ||
                sAMITAXGUBN.ToString() == "13" ||
                sAMITAXGUBN.ToString() == "62")
            {
                dAMIPENADD = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CVAU905",
                    "4",
                    this.TXT01_AMIYEAR.GetValue().ToString(),
                    this.CBO01_AMIBRANCH.GetValue().ToString(),
                    this.CBO01_AMIREPGB.GetValue().ToString(),
                    this.CBO01_AMIRPTGUBN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dAMT   = double.Parse(Get_Numeric(this.TXT01_AMIVALAMT.GetValue().ToString()));

                    dBUNJA = double.Parse(dt.Rows[0]["BUNJA"].ToString());
                    dBUNMO = double.Parse(dt.Rows[0]["BUNMO"].ToString());

                    dSEYUL = double.Parse(string.Format("{0:###.00000}", dBUNJA / dBUNMO));

                    dAMIPENADD = double.Parse(string.Format("{0:###0}", dAMT * dSEYUL));

                    sAMIPENADD = Convert.ToString(string.Format("{0:###0}", double.Parse(Get_Numeric(sAMIPENADD.ToString())) + dAMIPENADD));
                }
            }

            this.TXT01_AMIPENADD.SetValue(Convert.ToString(string.Format("{0:###0}", double.Parse(Get_Numeric(sAMIPENADD.ToString())))));

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 텍스트박스 읽기모드
        private void UP_ReadOnly(bool bResult)
        {
            this.TXT01_AMIYEAR.SetReadOnly(bResult);
            this.CBO01_AMIREPGB.SetReadOnly(bResult);
            this.CBO01_AMIRPTGUBN.SetReadOnly(bResult);
            this.CBO01_AMIBRANCH.SetReadOnly(bResult);
            this.CBH01_AMITAXGUBN.SetReadOnly(bResult);
            this.CBH01_AMITAXGUBN.SetReadOnlyText(bResult);
            this.DTP01_AMIDEALDT.SetReadOnly(bResult);
            this.CBH01_AMIVNEDCD.SetReadOnly(bResult);
            this.CBH01_AMIVNEDCD.SetReadOnlyText(bResult);
            this.TXT01_AMISAUPNO.SetReadOnly(bResult);

            this.TXT01_AMISAUPNO.SetReadOnly(true);
        }
        #endregion

        #region Description : 세무구분 인덱스값 변경
        private void CBO01_AMIRPTGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CBH01_AMITAXGUBN.DummyValue = this.CBO01_AMIRPTGUBN.GetValue().ToString();
        }
        #endregion

        #region Description : 사업자번호 가져오기
        private void CBH01_AMIVNEDCD_CodeBoxDataBinded(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_31I9L799",
                this.CBH01_AMIVNEDCD.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_AMISAUPNO.SetValue(dt.Rows[0]["VNSAUPNO"].ToString());
            }
        }
        #endregion
    }
}