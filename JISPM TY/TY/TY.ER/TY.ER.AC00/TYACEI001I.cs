using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.11 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25B6E344 : 받을어음 등록
    ///  TY_P_AC_25B6N350 : 받을어음 수정
    ///  TY_P_AC_25B6N351 : 받을어음 삭제
    ///  TY_P_AC_25E4M427 : 받을어음 확인
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
    ///  E6CDCL : 거래처코드
    ///  E6CDCM : 귀속사업장
    ///  E6CDGL : 금융기관
    ///  E6CDSO : 발생부서
    ///  E6GUBUN : 어음구분
    ///  E6IDBG : 상태구분
    ///  E6IDBS : 발생구분
    ///  E6IDJT : 자．타수구분
    ///  E6IDNR : 어음종류
    ///  E6TAGB : 타수구분
    ///  E6DTAP : 결재일자
    ///  E6DTBG : 상태변경일
    ///  E6DTCO : 입금일자
    ///  E6DTED : 만기일자
    ///  E6DTIS : 발행일자
    ///  E6AMNR : 어음금액
    ///  E6JPNO : 발생전표
    ///  E6NMBK : 결제은행명
    ///  E6NMBS : 어음배서인
    ///  E6NMIS : 어음발행자
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI001I : TYBase
    {
        private string fsE6NONR;

        #region Description : Page Load()
        public TYACEI001I(string sE6NONR)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this.fsE6NONR = sE6NONR;
        }

        private void TYACEI001I_Load(object sender, System.EventArgs e)
        {
            this.CBH01_E6CDCL.SetIPopupHelper(new TY.ER.GB00.TYERGB011P());

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_E6NMBK.SetIPopupHelper(new TY.ER.GB00.TYERGB007P());
            this.CBH01_E6CDGL.SetIPopupHelper(new TY.ER.GB00.TYERGB007P());

            if (string.IsNullOrEmpty(this.fsE6NONR))
            {               
                this.TXT01_E6NONR.SetReadOnly(false);
                UP_Set_FiledLock();
                //this.SetStartingFocus(TXT01_E6NONR);

                this.DTP01_E6DTAP.SetValue(""); 

                this.DTP01_E6DTCO.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                CBH01_E6CDSO.DummyValue = this.DTP01_E6DTCO.GetValue();
                CBH01_E6CDCM.DummyValue = this.DTP01_E6DTCO.GetValue();
            }
            else
            {
                this.TXT01_E6NONR.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25E4M427", this.fsE6NONR);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                CBH01_E6CDSO.DummyValue = dt.Rows[0]["E6DTCO"].ToString();
                CBH01_E6CDCM.DummyValue = dt.Rows[0]["E6DTCO"].ToString();
                
                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                this.DTP01_E6DTAP.SetValue(UP_Get_ApprovalDate(this.CBO01_E6GUBUN.GetValue().ToString(), this.DTP01_E6DTED.GetString()));

                UP_Set_FiledLock();

                //this.SetStartingFocus(CBO01_E6IDJT);
            }

        }
        #endregion      

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsE6NONR))
            {
                //어음마스타 등록
                this.DbConnector.Attach("TY_P_AC_25B6E344", TXT01_E6NONR.GetValue(), CBO01_E6IDJT.GetValue(),
                                                            CBO01_E6TAGB.GetValue(), CBO01_E6IDBS.GetValue(),
                                                            CBO01_E6GUBUN.GetValue(), CBH01_E6CDCL.GetValue(),
                                                            CBH01_E6CDSO.GetValue(), DTP01_E6DTCO.GetValue(),
                                                            CBH01_E6CDCM.GetValue(), TXT01_E6AMNR.GetValue(),
                                                            CBH01_E6NMBK.GetValue(), DTP01_E6DTIS.GetValue(),
                                                            TXT01_E6NMIS.GetValue(), DTP01_E6DTED.GetValue(),
                                                            DTP01_E6DTAP.GetValue(), 
                                                            CBO01_E6IDNR.GetValue(), TXT01_E6NMBS.GetValue(),
                                                            CBO01_E6IDBG.GetValue(), DTP01_E6DTBG.GetValue(),
                                                            CBH01_E6CDGL.GetValue());
                //어음내역 등록
                this.DbConnector.Attach("TY_P_AC_25F8N480", TXT01_E6NONR.GetValue(), CBO01_E6IDBG.GetValue(),
                                                            DTP01_E6DTCO.GetValue(), CBH01_E6NMBK.GetValue(),
                                                            CBH01_E6CDCM.GetValue(), "",
                                                            CBH01_E6CDCL.GetValue(), CBO01_E6IDBG.GetValue(), 
                                                            DTP01_E6DTCO.GetValue(), CBH01_E6NMBK.GetValue(),"","");
            }
            else
            {
                // 수정
                this.DbConnector.Attach("TY_P_AC_25B6N350", CBO01_E6IDJT.GetValue(), CBO01_E6TAGB.GetValue(),
                                                            CBO01_E6IDBS.GetValue(), CBO01_E6GUBUN.GetValue(),
                                                            CBH01_E6CDCL.GetValue(), CBH01_E6CDSO.GetValue(),
                                                            DTP01_E6DTCO.GetValue(), CBH01_E6CDCM.GetValue(),
                                                            TXT01_E6AMNR.GetValue(), CBH01_E6NMBK.GetValue(),
                                                            DTP01_E6DTIS.GetValue(), TXT01_E6NMIS.GetValue(),
                                                            DTP01_E6DTED.GetValue(), DTP01_E6DTAP.GetValue(), 
                                                            CBO01_E6IDNR.GetValue(),
                                                            TXT01_E6NMBS.GetValue(), CBO01_E6IDBG.GetValue(),
                                                            DTP01_E6DTBG.GetValue(), CBH01_E6CDGL.GetValue(),
                                                            TXT01_E6NONR.GetValue());

                this.DbConnector.Attach("TY_P_AC_25F8M478", CBO01_E6IDBG.GetValue(), CBH01_E6NMBK.GetValue(),
                                                            CBH01_E6CDCM.GetValue(), CBH01_E6CDCL.GetValue(), 
                                                            TXT01_E6NONR.GetValue(), CBO01_E6IDBG.GetValue(),
                                                            DTP01_E6DTBG.GetValue()); 

            }

            this.DbConnector.ExecuteTranQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;

            if (string.IsNullOrEmpty(this.fsE6NONR))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25E4M427", TXT01_E6NONR.GetValue());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            //무역 여신한도 체크
            if (CBH01_E6CDCM.GetValue().ToString().Substring(0, 1) == "B")
            {
                if (CBO01_E6IDJT.GetValue().ToString() == "2" && CBO01_E6TAGB.GetValue().ToString() == "4" ) 
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_25G10490", CBH01_E6CDCL.GetValue());                    
                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        //신용한도 금액
                        double dCUHANDOJ = Convert.ToDouble(this.DbConnector.ExecuteDataTable().Rows[0]["CUHANDOJ"].ToString());

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_25G2K496", TXT01_E6NONR.GetValue(),
                                                                    CBH01_E6CDCL.GetValue(),
                                                                    CBO01_E6IDJT.GetValue(),
                                                                    CBO01_E6TAGB.GetValue(),
                                                                    dCUHANDOJ );

                        double dE6AMNRTotal = Convert.ToDouble(this.DbConnector.ExecuteDataTable().Rows[0]["E6AMNR"].ToString());

                        dE6AMNRTotal = dE6AMNRTotal + Convert.ToDouble(TXT01_E6AMNR.GetValue().ToString());
                        if (dE6AMNRTotal > 20000000)
                        {
                            this.ShowMessage("TY_M_AC_25G2S497");
                            //e.Successed = false;
                            //return;
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_25G14492");
                        e.Successed = false;
                        return;
                    }
                }

                //무역 마감 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25G2U498", DTP01_E6DTCO.GetValue().ToString().Substring(0, 6));
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_25G2W499");
                    e.Successed = false;
                    return;
                }
            }

            //등록일 경우만
            if (string.IsNullOrEmpty(this.fsE6NONR))
            {

                //상태변경일자 <- 입금일자
                DTP01_E6DTBG.SetValue(DTP01_E6DTCO.GetValue());

                //어음내역파일이 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25G2Z500", TXT01_E6NONR.GetValue(), CBO01_E6IDBG.GetValue(), DTP01_E6DTBG.GetValue());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_25G34501");
                    e.Successed = false;
                    return;
                }
            }
            else
            {

                //전표번호 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25E4M427", TXT01_E6NONR.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows[0]["E6JPNO"].ToString().Trim() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                };

                if (CBO01_E6IDBG.GetValue().ToString().Trim() != "10")
                {
                    this.ShowMessage("TY_M_AC_25G3B502");
                    e.Successed = false;
                    return;
                }
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25G2Z500", TXT01_E6NONR.GetValue(), CBO01_E6IDBG.GetValue(), DTP01_E6DTBG.GetValue());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt <= 0)
                {
                    this.ShowMessage("TY_M_AC_25G3D503");
                    e.Successed = false;
                    return;
                }
                //입금일자와 상태변경일자가 다르면 수정 불가
                if (DTP01_E6DTCO.GetValue().ToString() != DTP01_E6DTBG.GetValue().ToString())
                {
                    this.ShowMessage("TY_M_AC_25G3E504");
                    e.Successed = false;
                    return;
                }
            }

            // 결재일자가 공백일경우 결재일자 자동계산
            if (this.DTP01_E6DTAP.GetValue().ToString().Trim() == "" && this.CBO01_E6GUBUN.GetValue().ToString().Trim() != "" && this.DTP01_E6DTED.GetString().Trim() != "")
            {
                this.DTP01_E6DTAP.SetValue(UP_Get_ApprovalDate(this.CBO01_E6GUBUN.GetValue().ToString(), this.DTP01_E6DTED.GetString()));
            }

            //입금일자는 만기일자보다 늦을수 없다.
            DateTime dt_E6DTCO = Convert.ToDateTime(DTP01_E6DTCO.GetString().ToString().Substring(0, 4) + "-" + DTP01_E6DTCO.GetString().ToString().Substring(4, 2) + "-" + DTP01_E6DTCO.GetString().ToString().Substring(6,2));
            DateTime dt_E6DTED = Convert.ToDateTime(DTP01_E6DTED.GetString().ToString().Substring(0, 4) + "-" + DTP01_E6DTED.GetString().ToString().Substring(4, 2) + "-" + DTP01_E6DTED.GetString().ToString().Substring(6, 2));

            if ((dt_E6DTED - dt_E6DTCO).Days < 0)
            {
                this.ShowCustomMessage("만기일자는 입금일자보다 빠를수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }


            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
            

        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_Set_FiledLock()
        {
            CBO01_E6IDBG.SetReadOnly(true);
            DTP01_E6DTBG.SetReadOnly(true);
            CBH01_E6CDGL.SetReadOnly(true);
            DTP01_E6DTAP.SetReadOnly(true);
            TXT01_E6JPNO.SetReadOnly(true);
        }
                
        private string UP_Get_ApprovalDate(string sBillGN, string sDTED)
        {
           //sBillGN = 1 -> 일반어음  2 -> 전자어음         

            int iWeek = 0;
            bool nReturn = true;
            string sWK_Date = "";
            string sYOIL = "";
            string sHUMU = "";
            string sSW = "";
            string sRetrun_Date = "";

            //결재일자 계산 
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            sDTED = sDTED.Substring(0, 4) + "-" + sDTED.Substring(4, 2) + "-" + sDTED.Substring(6, 2);

            t1 = Convert.ToDateTime(sDTED);
            if (sBillGN == "1")  //일반어음
            {
                t2 = t1.AddDays(-1);
                sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());
                while (nReturn)
                {
                    //요일
                    iWeek = Convert.ToInt16(t2.DayOfWeek);
 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_29EAS015", sWK_Date.Substring(0, 4), sWK_Date.Substring(5, 2), sWK_Date.Substring(8, 2));
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sYOIL = dt.Rows[0]["UYYOILCD"].ToString();
                        sHUMU = dt.Rows[0]["UYHUMUCD"].ToString();
                    }
                    else
                    {
                        sYOIL = "";
                        sHUMU = "";
                    }
                    
                    // 10 = 창립기념일 , 20=회사휴뮤 , 21=체육대회
                    if (sHUMU.Trim() == "10" || sHUMU.Trim() == "20" || sHUMU.Trim() == "21" || sHUMU.Trim() == "")
                    {
                        sSW = "";
                    }
                    else
                    {
                        sSW = "*";
                    }

                    if ((iWeek == 6 || iWeek == 0) || (sSW == "*"))
                    {
                        t1 = Convert.ToDateTime(sWK_Date);
                        t2 = t1.AddDays(-1);
                        sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());
                    }
                    else
                    {
                        sRetrun_Date = sWK_Date;
                        nReturn = false;
                    }
                } // End...While
            }
            else  //전자어음
            {
                t2 = t1.AddDays(+1);
                sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());
                while (nReturn)
                {
                    //요일
                    iWeek = Convert.ToInt16(t2.DayOfWeek);

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_29EAS015", sWK_Date.Substring(0, 4), sWK_Date.Substring(5, 2), sWK_Date.Substring(8, 2));
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        sYOIL = dt.Rows[0]["UYYOILCD"].ToString();
                        sHUMU = dt.Rows[0]["UYHUMUCD"].ToString();
                    }
                    else
                    {
                        sYOIL = "";
                        sHUMU = "";
                    }

                    // 10 = 창립기념일 , 20=회사휴뮤 , 21=체육대회
                    if (sHUMU.Trim() == "10" || sHUMU.Trim() == "20" || sHUMU.Trim() == "21" || sHUMU.Trim() == "")
                    {
                        sSW = "";
                    }
                    else
                    {
                        sSW = "*";
                    }

                    if ((iWeek == 6 || iWeek == 0) || (sSW == "*"))
                    {
                        t1 = Convert.ToDateTime(sWK_Date);
                        t2 = t1.AddDays(+1);
                        sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());
                    }
                    else
                    {
                        sRetrun_Date = sWK_Date;
                        nReturn = false;
                    }
                } // End...While
            }

            return sRetrun_Date;
        }
        #endregion

        #region Description : DTP01_E6DTCO_ValueChanged 함수
        private void DTP01_E6DTCO_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E6CDCM.DummyValue = this.DTP01_E6DTCO.GetValue(); 
        }
        #endregion

        #region Description : DTP01_E6DTED_ValueChanged 함수
        private void DTP01_E6DTED_ValueChanged(object sender, EventArgs e)
        {            
            this.DTP01_E6DTAP.SetValue(UP_Get_ApprovalDate(this.CBO01_E6GUBUN.GetValue().ToString(),this.DTP01_E6DTED.GetString()));
        }
        #endregion


    }
}
