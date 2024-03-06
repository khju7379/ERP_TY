using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 토지 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.01.30 21:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31U2Y956 : 고정자산 토지 공시지가 이력 조회
    ///  TY_P_AC_31U2Y957 : 고정자산 토지 공시지가 이력 등록
    ///  TY_P_AC_31U2Z958 : 고정자산 토지 공시지가 이력 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31U5B968 : 고정자산 토지 공시지가 이력 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_2584Y096 : 승인일자를 확인하세요
    ///  TY_M_AC_2C65V970 : 승인사번을 확인하세요!
    ///  TY_M_AC_2CV43442 : 처리 할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  FLAPPSABUN : 승인사번
    ///  FLGUBN : 자산분류코드
    ///  FLITEMCODE : 품목코드
    ///  FLSAUP : 사업부
    ///  FLAPPGUBN : 승인유무
    ///  FLCAUSECODE : 취득원인
    ///  FLREPAYWAY : 상각방식
    ///  FLAPPDATE : 승인일자
    ///  FLAREA : 면적
    ///  FLBIGO : 비고
    ///  FLCHGCODE : 변경코드
    ///  FLCHGDATE : 변경일자
    ///  FLCHGTEXT : 변경사유
    ///  FLGETAMOUNT : 취득금액
    ///  FLGETDATE : 취득일
    ///  FLGRURL : 결재번호
    ///  FLJIMOK : 지목
    ///  FLJPNO : 자산생성전표번호
    ///  FLLANDAMOUNT : 공시지가
    ///  FLLOTNUM1 : 지번(본)
    ///  FLLOTNUM2 : 지번(부)
    ///  FLNAME : 자산명
    ///  FLNOMNAME : 등기명의인
    ///  FLSEQ : 자산순번
    ///  FLSITE : 소재지
    ///  FLSTOCKNUM : 입고번호
    ///  FLYEAR : 자산년도
    /// </summary>
    public partial class TYACHF006I : TYBase
    {
        private TYData DAT11_FLYEAR;
        private TYData DAT11_FLSEQ;
        private TYData DAT11_FLGUBN;
        private TYData DAT11_FLNAME;
        private TYData DAT11_FLREPAYWAY;
        private TYData DAT11_FLSAUP;
        private TYData DAT11_FLGETDATE;
        private TYData DAT11_FLGETAMOUNT;
        private TYData DAT11_FLAREA;
        private TYData DAT11_FLLANDAMOUNT;
        private TYData DAT11_FLCAUSECODE;
        private TYData DAT11_FLSITE;
        private TYData DAT11_FLLOTNUM1;
        private TYData DAT11_FLLOTNUM2;
        private TYData DAT11_FLJIMOK;
        private TYData DAT11_FLNOMNAME;
        private TYData DAT11_FLSTOCKNUM;
        private TYData DAT11_FLGRURL;
        private TYData DAT11_FLJPNO;
        private TYData DAT11_FLITEMCODE;
        private TYData DAT11_FLAPPGUBN;
        private TYData DAT11_FLAPPSABUN;
        private TYData DAT11_FLAPPDATE;
        private TYData DAT11_FLBIGO;
        private TYData DAT11_FLHISAB;

        private TYData DAT21_FSYEAR;
        private TYData DAT21_FSSEQ;
        private TYData DAT21_FSGETYEAR;
        private TYData DAT21_FSLANDAMOUNT;
        private TYData DAT21_FSHISAB;

        private string fsFLYEAR;
        private string fsFLSEQ;
        private string fsTabCtl;

        #region Description : 페이지 로드
        public TYACHF006I(string sYEAR, string sSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsFLYEAR = sYEAR;
            this.fsFLSEQ  = sSEQ;

            this.DAT11_FLYEAR       = new TYData("DAT11_FLYEAR", null);
            this.DAT11_FLSEQ        = new TYData("DAT11_FLSEQ", null);
            this.DAT11_FLGUBN       = new TYData("DAT11_FLGUBN", null);
            this.DAT11_FLNAME       = new TYData("DAT11_FLNAME", null);
            this.DAT11_FLREPAYWAY   = new TYData("DAT11_FLREPAYWAY", null);
            this.DAT11_FLSAUP       = new TYData("DAT11_FLSAUP", null);
            this.DAT11_FLGETDATE    = new TYData("DAT11_FLGETDATE", null);
            this.DAT11_FLGETAMOUNT  = new TYData("DAT11_FLGETAMOUNT", null);
            this.DAT11_FLAREA       = new TYData("DAT11_FLAREA", null);
            this.DAT11_FLLANDAMOUNT = new TYData("DAT11_FLLANDAMOUNT", null);
            this.DAT11_FLCAUSECODE  = new TYData("DAT11_FLCAUSECODE", null);
            this.DAT11_FLSITE       = new TYData("DAT11_FLSITE", null);
            this.DAT11_FLLOTNUM1    = new TYData("DAT11_FLLOTNUM1", null);
            this.DAT11_FLLOTNUM2    = new TYData("DAT11_FLLOTNUM2", null);
            this.DAT11_FLJIMOK      = new TYData("DAT11_FLJIMOK", null);
            this.DAT11_FLNOMNAME    = new TYData("DAT11_FLNOMNAME", null);
            this.DAT11_FLSTOCKNUM   = new TYData("DAT11_FLSTOCKNUM", null);
            this.DAT11_FLGRURL      = new TYData("DAT11_FLGRURL", null);
            this.DAT11_FLJPNO       = new TYData("DAT11_FLJPNO", null);
            this.DAT11_FLITEMCODE   = new TYData("DAT11_FLITEMCODE", null);
            this.DAT11_FLAPPGUBN    = new TYData("DAT11_FLAPPGUBN", null);
            this.DAT11_FLAPPSABUN   = new TYData("DAT11_FLAPPSABUN", null);
            this.DAT11_FLAPPDATE    = new TYData("DAT11_FLAPPDATE", null);
            this.DAT11_FLBIGO       = new TYData("DAT11_FLBIGO", null);
            this.DAT11_FLHISAB      = new TYData("DAT11_FLHISAB", null);

            this.DAT21_FSYEAR       = new TYData("DAT21_FSYEAR", null);
            this.DAT21_FSSEQ        = new TYData("DAT21_FSSEQ", null);
            this.DAT21_FSGETYEAR    = new TYData("DAT21_FSGETYEAR", null);
            this.DAT21_FSLANDAMOUNT = new TYData("DAT21_FSLANDAMOUNT", null);
            this.DAT21_FSHISAB      = new TYData("DAT21_FSHISAB", null);
        }

        private void TYACHF006I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_31U5B968, "FSGETYEAR");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT11_FLYEAR);
            this.ControlFactory.Add(this.DAT11_FLSEQ);
            this.ControlFactory.Add(this.DAT11_FLGUBN);
            this.ControlFactory.Add(this.DAT11_FLNAME);
            this.ControlFactory.Add(this.DAT11_FLREPAYWAY);
            this.ControlFactory.Add(this.DAT11_FLSAUP);
            this.ControlFactory.Add(this.DAT11_FLGETDATE);
            this.ControlFactory.Add(this.DAT11_FLGETAMOUNT);
            this.ControlFactory.Add(this.DAT11_FLAREA);
            this.ControlFactory.Add(this.DAT11_FLLANDAMOUNT);
            this.ControlFactory.Add(this.DAT11_FLCAUSECODE);
            this.ControlFactory.Add(this.DAT11_FLSITE);
            this.ControlFactory.Add(this.DAT11_FLLOTNUM1);
            this.ControlFactory.Add(this.DAT11_FLLOTNUM2);
            this.ControlFactory.Add(this.DAT11_FLJIMOK);
            this.ControlFactory.Add(this.DAT11_FLNOMNAME);
            this.ControlFactory.Add(this.DAT11_FLSTOCKNUM);
            this.ControlFactory.Add(this.DAT11_FLGRURL);
            this.ControlFactory.Add(this.DAT11_FLJPNO);
            this.ControlFactory.Add(this.DAT11_FLITEMCODE);
            this.ControlFactory.Add(this.DAT11_FLAPPGUBN);
            this.ControlFactory.Add(this.DAT11_FLAPPSABUN);
            this.ControlFactory.Add(this.DAT11_FLAPPDATE);
            this.ControlFactory.Add(this.DAT11_FLBIGO);
            this.ControlFactory.Add(this.DAT11_FLHISAB);

            this.ControlFactory.Add(this.DAT21_FSYEAR);
            this.ControlFactory.Add(this.DAT21_FSSEQ);
            this.ControlFactory.Add(this.DAT21_FSGETYEAR);
            this.ControlFactory.Add(this.DAT21_FSLANDAMOUNT);
            this.ControlFactory.Add(this.DAT21_FSHISAB);

            this.TXT01_FLYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            this.TXT01_FLSEQ.SetValue(Set_Fill4(fsFLSEQ));

            this.FPS91_TY_S_AC_31U5B968.Initialize();

            // 신규
            if (string.IsNullOrEmpty(this.fsFLYEAR))
            {
                this.TXT01_FLYEAR.SetReadOnly(false);
                this.TXT01_FLSEQ.SetReadOnly(true);

                UP_SET_NewProcess();
            }
            else
            {
                this.Initialize_Controls("01");

                this.TXT01_FLYEAR.SetValue(fsFLYEAR);
                this.TXT01_FLSEQ.SetValue(Set_Fill4(fsFLSEQ));

                this.TXT01_FLYEAR.SetReadOnly(true);
                this.TXT01_FLSEQ.SetReadOnly(true);

                // 수정
                UP_SET_UpdateProcess();
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsFLYEAR = "";

            this.Initialize_Controls("01");

            this.TXT01_FLYEAR.SetReadOnly(false);

            this.SetFocus(this.TXT01_FLYEAR);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //고정자산 마스타 저장
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsFLYEAR))
            {
                this.DbConnector.Attach("TY_P_AC_31V4M977", this.ControlFactory, "11"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_31V4N978", this.ControlFactory, "11"); // 수정
            }

            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            // 마스타 
            UP_Set_MasterScreen();
            // 디테일
            UP_Set_DetailScreen();

            fsTabCtl = "2";

            this.UP_Set_TabControl();

            if (this.TXT01_FLJPNO.GetValue().ToString() == "")
            {
                this.UP_SET_BtnProCess("1");
            }
            else
            {
                this.UP_SET_BtnProCess("3");
            }
        }
        #endregion

        #region Description : 저장 BTN62_SAV 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_31U2Y957", ds.Tables[0].Rows[i]["FSYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["FSSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["FSGETYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["FSLANDAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo);
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_31U2Z958", ds.Tables[1].Rows[i]["FSLANDAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["FSYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["FSSEQ"].ToString(),
                                                            ds.Tables[1].Rows[i]["FSGETYEAR"].ToString());
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_31U2Z959", ds.Tables[2].Rows[i]["FSYEAR"].ToString(),
                                                            ds.Tables[2].Rows[i]["FSSEQ"].ToString(),
                                                            ds.Tables[2].Rows[i]["FSGETYEAR"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            UP_Set_DetailScreen();

            string sFSLANDAMOUNT = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3212D989", this.TXT01_FLYEAR.GetValue(), this.TXT01_FLSEQ.GetValue()); // 공시지가 확인
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sFSLANDAMOUNT = dt.Rows[0]["FSLANDAMOUNT"].ToString();
            }

            // 공시지가 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3212G991", sFSLANDAMOUNT.ToString(),
                                                        this.TXT01_FLYEAR.GetValue(),
                                                        this.TXT01_FLSEQ.GetValue());

            this.DbConnector.ExecuteNonQuery();

            if (this.TXT01_FLJPNO.GetValue().ToString() == "")
            {
                this.UP_SET_BtnProCess("2");
            }
            else
            {
                this.UP_SET_BtnProCess("3");
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : 신규 상태 처리 이벤트
        private void UP_SET_NewProcess()
        {
            fsTabCtl = "1";

            this.UP_Set_TabControl();

            this.tabCtl.SelectedIndex = 0;

            this.UP_SET_BtnProCess("1");

            this.Initialize_Controls("01");

            this.CBO01_FLREPAYWAY.SetValue("1");
            this.CBO01_FLAPPGUBN.SetValue("N");

            //this.CBH01_FLSAUP.DummyValue = DateTime.Now.ToString("yyyy") + "0101";
            this.CBH01_FLSAUP.DummyValue = "19900101";

            this.UP_Set_ReadOnly(true);

            this.SetStartingFocus(this.TXT01_FLYEAR);
        }
        #endregion

        #region  Description : 수정 상태 처리 이벤트
        private void UP_SET_UpdateProcess()
        {
            fsTabCtl = "2";

            this.tabCtl.SelectedIndex = 0;

           //this.CBH01_FLSAUP.DummyValue = this.TXT01_FLYEAR.GetValue().ToString() + "0101";
            this.CBH01_FLSAUP.DummyValue = "19900101";

            this.UP_Set_ReadOnly(true);

            // 마스타 
            UP_Set_MasterScreen();
            // 디테일
            UP_Set_DetailScreen();

            this.UP_Set_TabControl();

            if (this.TXT01_FLJPNO.GetValue().ToString() == "")
            {
                this.UP_SET_BtnProCess("1");
            }
            else
            {
                this.UP_SET_BtnProCess("3");
            }

            this.SetStartingFocus(this.CBH01_FLGUBN.CodeText);
        }
        #endregion

        #region Description : 고정자산 마스타 확인
        private void UP_Set_MasterScreen()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31V4J976", this.TXT01_FLYEAR.GetValue(), this.TXT01_FLSEQ.GetValue()); // 토지 확인
            DataTable dtm = this.DbConnector.ExecuteDataTable();
            this.CurrentDataTableRowMapping(dtm, "01");
        }
        #endregion

        #region Description : 고정자산 디테일 확인
        private void UP_Set_DetailScreen()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31U2Y956", this.TXT01_FLYEAR.GetValue(), this.TXT01_FLSEQ.GetValue()); // 디테일 조회
            DataTable dts = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_31U5B968.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 BTN61_SAV ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //신규이면 자산순번 생성
            if (string.IsNullOrEmpty(this.fsFLYEAR))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31V6F979", this.TXT01_FLYEAR.GetValue());
                this.TXT01_FLSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }

            // 전표번호 존재하면 작업 불가.
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31V4J976", this.TXT01_FLYEAR.GetValue(), this.TXT01_FLSEQ.GetValue());
            DataTable dtm = this.DbConnector.ExecuteDataTable();

            if (dtm.Rows.Count > 0)
            {
                if (dtm.Rows[0]["FLJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }
            }

            if (this.MTB01_FLGETDATE.GetValue().ToString().Replace("-","") == "")
            {
                this.ShowMessage("TY_M_AC_321CV988");
                this.SetFocus(this.MTB01_FLGETDATE);
                e.Successed = false;
                return;
            }

            //승인이면 승인일자, 승인사번 받는다
            if (this.CBO01_FLAPPGUBN.GetValue().ToString() == "Y")
            {
                if (MTB01_FLAPPDATE.GetValue().ToString().Replace("-", "") == "")
                {
                    this.ShowMessage("TY_M_AC_2584Y096");
                    this.SetFocus(this.MTB01_FLAPPDATE);
                    e.Successed = false;
                    return;
                }

                if (CBH01_FLAPPSABUN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2C65V970");
                    this.SetFocus(this.CBH01_FLAPPSABUN);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.MTB01_FLAPPDATE.SetValue("");
                this.CBH01_FLAPPSABUN.SetValue("");
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            this.DAT11_FLYEAR.SetValue(TXT01_FLYEAR.GetValue());
            this.DAT11_FLSEQ.SetValue(TXT01_FLSEQ.GetValue());
            this.DAT11_FLGUBN.SetValue(CBH01_FLGUBN.GetValue());
            this.DAT11_FLNAME.SetValue(TXT01_FLNAME.GetValue());
            this.DAT11_FLREPAYWAY.SetValue(CBO01_FLREPAYWAY.GetValue());
            this.DAT11_FLSAUP.SetValue(CBH01_FLSAUP.GetValue());
            this.DAT11_FLGETDATE.SetValue(MTB01_FLGETDATE.GetValue().ToString().Replace("-", ""));
            this.DAT11_FLGETAMOUNT.SetValue(TXT01_FLGETAMOUNT.GetValue());
            this.DAT11_FLAREA.SetValue(TXT01_FLAREA.GetValue());
            this.DAT11_FLLANDAMOUNT.SetValue(TXT01_FLLANDAMOUNT.GetValue());
            this.DAT11_FLCAUSECODE.SetValue(CBO01_FLCAUSECODE.GetValue());
            this.DAT11_FLSITE.SetValue(TXT01_FLSITE.GetValue());
            this.DAT11_FLLOTNUM1.SetValue(TXT01_FLLOTNUM1.GetValue());
            this.DAT11_FLLOTNUM2.SetValue(TXT01_FLLOTNUM2.GetValue());
            this.DAT11_FLJIMOK.SetValue(TXT01_FLJIMOK.GetValue());
            this.DAT11_FLNOMNAME.SetValue(TXT01_FLNOMNAME.GetValue());
            this.DAT11_FLSTOCKNUM.SetValue(TXT01_FLSTOCKNUM.GetValue());
            this.DAT11_FLGRURL.SetValue(TXT01_FLGRURL.GetValue());
            this.DAT11_FLJPNO.SetValue(TXT01_FLJPNO.GetValue());
            this.DAT11_FLITEMCODE.SetValue(CBH01_FLITEMCODE.GetValue());
            this.DAT11_FLAPPGUBN.SetValue(CBO01_FLAPPGUBN.GetValue());
            this.DAT11_FLAPPSABUN.SetValue(CBH01_FLAPPSABUN.GetValue());
            this.DAT11_FLAPPDATE.SetValue(MTB01_FLAPPDATE.GetValue().ToString().Replace("-", ""));
            this.DAT11_FLBIGO.SetValue(TXT01_FLBIGO.GetValue());
            this.DAT11_FLHISAB.SetValue(TYUserInfo.EmpNo);
        }
        #endregion

        #region Description : BTN62_SAV ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 전표번호 존재하면 작업 불가.
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31V4J976", this.TXT01_FLYEAR.GetValue(), this.TXT01_FLSEQ.GetValue());
            DataTable dtm = this.DbConnector.ExecuteDataTable();

            if (dtm.Rows.Count > 0)
            {
                if (dtm.Rows[0]["FLJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }
            }

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_31U5B968.GetDataSourceInclude(TSpread.TActionType.New,    "FSYEAR", "FSSEQ", "FSGETYEAR", "FSLANDAMOUNT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_31U5B968.GetDataSourceInclude(TSpread.TActionType.Update, "FSYEAR", "FSSEQ", "FSGETYEAR", "FSLANDAMOUNT"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_31U5B968.GetDataSourceInclude(TSpread.TActionType.Remove, "FSYEAR", "FSSEQ", "FSGETYEAR"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_3219B985",
                                       ds.Tables[0].Rows[i]["FSYEAR"].ToString(),
                                       ds.Tables[0].Rows[i]["FSSEQ"].ToString(),
                                       ds.Tables[0].Rows[i]["FSGETYEAR"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : tabCtl_SelectedIndexChanged 이벤트
        private void UP_Set_ReadOnly(bool bTrueAndFalse)
        {
            // 마스타 tab
            this.CBH01_FLCHGCODE.SetReadOnly(bTrueAndFalse);
            this.MTB01_FLCHGDATE.SetReadOnly(bTrueAndFalse);
            this.TXT01_FLCHGTEXT.SetReadOnly(bTrueAndFalse);
            this.TXT01_FLSTOCKNUM.SetReadOnly(bTrueAndFalse);
            this.TXT01_FLJPNO.SetReadOnly(bTrueAndFalse);
            this.TXT01_FLGRURL.SetReadOnly(bTrueAndFalse);
            this.TXT01_FLLANDAMOUNT.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : UP_Set_TabControl Tab Display 이벤트
        private void UP_Set_TabControl()
        {
            if (this.tabCtl.TabPages.Contains(this.Master))
                this.tabCtl.TabPages.Remove(this.Master);
            if (this.tabCtl.TabPages.Contains(this.Detail))
                this.tabCtl.TabPages.Remove(this.Detail);

            if (fsTabCtl == "1")
            {
                this.tabCtl.TabPages.Add(this.Master);
            }
            else if (fsTabCtl == "2")
            {
                this.tabCtl.TabPages.Add(this.Master);
                this.tabCtl.TabPages.Add(this.Detail);
            }
        }
        #endregion

        #region  Description : 버튼 상태 처리 이벤트
        private void UP_SET_BtnProCess(string sGubn)
        {
            // 마스타
            if (sGubn == "1")
            {
                this.BTN61_NEW.Visible = true;
                this.BTN61_SAV.Visible = true;

                this.BTN62_SAV.Visible = false;
            }
            // 디테일
            if (sGubn == "2")
            {
                this.BTN61_NEW.Visible = false;
                this.BTN61_SAV.Visible = false;

                this.BTN62_SAV.Visible = true;
            }

            if (sGubn == "3")
            {
                this.BTN61_NEW.Visible = false;
                this.BTN61_SAV.Visible = false;

                this.BTN62_SAV.Visible = false;
            }
        }
        #endregion

        #region Description : 스프레드 추가 이벤트
        private void FPS91_TY_S_AC_31U5B968_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_31U5B968.SetValue(e.RowIndex, "FSYEAR", this.TXT01_FLYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_31U5B968.SetValue(e.RowIndex, "FSSEQ",  this.TXT01_FLSEQ.GetValue().ToString());
        }
        #endregion

        #region  Description : tabCtl_SelectedIndexChanged 이벤트
        private void tabCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtl.SelectedIndex == 0)  // 마스타
            {
                UP_Set_MasterScreen();

                if (this.TXT01_FLJPNO.GetValue().ToString() == "")
                {
                    this.UP_SET_BtnProCess("1");
                }
                else
                {
                    this.UP_SET_BtnProCess("3");
                }

                this.SetFocus(this.CBH01_FLGUBN.CodeText);
            }
            else if (tabCtl.SelectedIndex == 1) // 디테일
            {
                UP_Set_DetailScreen();

                if (this.TXT01_FLJPNO.GetValue().ToString() == "")
                {
                    this.UP_SET_BtnProCess("2");
                }
                else
                {
                    this.UP_SET_BtnProCess("3");
                }
            }
        }
        #endregion

        #region Description : TXT01_FLYEAR_TextChanged 이벤트
        private void TXT01_FLYEAR_TextChanged(object sender, EventArgs e)
        {
            //this.CBH01_FLSAUP.DummyValue = this.TXT01_FLYEAR.GetValue().ToString() + "0101";
            this.CBH01_FLSAUP.DummyValue = "19900101";
        }
        #endregion
    }
}