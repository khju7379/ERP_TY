using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 지급어음 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.29 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25T63698 : 지급어음 등록
    ///  TY_P_AC_25T68697 : 지급어음 확인
    ///  TY_P_AC_25T6A699 : 지급어음 수정
    ///  TY_P_AC_25T6A700 : 지급어음 삭제
    ///  TY_P_AC_25T6B701 : 지급어음 내역 등록
    ///  TY_P_AC_25T6D702 : 지급어음 내역 수정
    ///  TY_P_AC_25T6D703 : 지급어음 내역 삭제
    ///  TY_P_AC_25T6F704 : 어음수표용지 수정
    ///  TY_P_AC_25T6H705 : 어음수표용지 존재유무 체크
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  F3BKPY : 지급은행
    ///  F3CDDP : 부서코드
    ///  F3CDFD : 자금항목코드
    ///  F3CLNY : 거래처
    ///  F3SADP : 사업장
    ///  F3CDAC : 계정코드
    ///  F3GUBUN : 어음구분
    ///  F3SSYN : 발생상태
    ///  F3DTBG : 상태변경일
    ///  F3DTED : 만기일자
    ///  F3DTIS : 발행일자
    ///  F3DTPY : 지급일자
    ///  F3ACCR : 상대계정
    ///  F3AMNY : 금액
    ///  F3HSYN : 현상태
    ///  F3NOAC : 계좌번호
    ///  F3NONY : 어음번호
    ///  F3RPYY : 적요
    /// </summary>
    public partial class TYACEI015I : TYBase
    {
        private string fsF3NONY;

        private TYData DAT03_F3NONY;
        private TYData DAT03_F3SSYN;
        private TYData DAT03_F3CDAC;
        private TYData DAT03_F3DTIS;
        private TYData DAT03_F3DTBG;
        private TYData DAT03_F3HSYN;
        private TYData DAT03_F3DTED;
        private TYData DAT03_F3CDDP;
        private TYData DAT03_F3SADP;
        private TYData DAT03_F3CLNY;
        private TYData DAT03_F3AMNY;
        private TYData DAT03_F3CDFD;
        private TYData DAT03_F3RPYY;
        private TYData DAT03_F3BKPY;
        private TYData DAT03_F3NOAC;
        private TYData DAT03_F3ACCR;
        private TYData DAT03_F3GUBUN;
        private TYData DAT03_F3HISAB;

        private TYData DAT04_F4NONY;
        private TYData DAT04_F4IDBG;
        private TYData DAT04_F4CDDP;
        private TYData DAT04_F4BKPY;
        private TYData DAT04_F4DTIS;
        private TYData DAT04_F4DTED;
        private TYData DAT04_F4CLNY;
        private TYData DAT04_F4AMNY;
        private TYData DAT04_F4HISAB;

        private TYData DAT05_F5DTIS;
        private TYData DAT05_F5DTED;
        private TYData DAT05_F5CLNC;
        private TYData DAT05_F5AMIS;
        private TYData DAT05_F5IDUS1;
        private TYData DAT05_F5IDUS2;
        private TYData DAT05_F5NONC;


        #region Description : Page_Load
        public TYACEI015I(string sF3NONY)
        {
            InitializeComponent();
            this.SetPopupStyle();

            this.BTN61_F3NONY.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_F3CLNY.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.fsF3NONY = sF3NONY;

            this.DAT03_F3NONY = new TYData("DAT03_F3NONY", null);
            this.DAT03_F3SSYN = new TYData("DAT03_F3SSYN", null);
            this.DAT03_F3CDAC = new TYData("DAT03_F3CDAC", null);
            this.DAT03_F3DTIS = new TYData("DAT03_F3DTIS", null);
            this.DAT03_F3DTBG = new TYData("DAT03_F3DTBG", null);
            this.DAT03_F3HSYN = new TYData("DAT03_F3HSYN", null);
            this.DAT03_F3DTED = new TYData("DAT03_F3DTED", null);
            this.DAT03_F3CDDP = new TYData("DAT03_F3CDDP", null);
            this.DAT03_F3SADP = new TYData("DAT03_F3SADP", null);
            this.DAT03_F3CLNY = new TYData("DAT03_F3CLNY", null);
            this.DAT03_F3AMNY = new TYData("DAT03_F3AMNY", null);
            this.DAT03_F3CDFD = new TYData("DAT03_F3CDFD", null);
            this.DAT03_F3RPYY = new TYData("DAT03_F3RPYY", null);
            this.DAT03_F3BKPY = new TYData("DAT03_F3BKPY", null);
            this.DAT03_F3NOAC = new TYData("DAT03_F3NOAC", null);
            this.DAT03_F3ACCR = new TYData("DAT03_F3ACCR", null);
            this.DAT03_F3GUBUN = new TYData("DAT03_F3GUBUN", null);
            this.DAT03_F3HISAB = new TYData("DAT03_F3HISAB", null);

            this.DAT04_F4NONY = new TYData("DAT04_F4NONY", null);
            this.DAT04_F4IDBG = new TYData("DAT04_F4IDBG", null);
            this.DAT04_F4CDDP = new TYData("DAT04_F4CDDP", null);
            this.DAT04_F4BKPY = new TYData("DAT04_F4BKPY", null);
            this.DAT04_F4DTIS = new TYData("DAT04_F4DTIS", null);
            this.DAT04_F4DTED = new TYData("DAT04_F4DTED", null);
            this.DAT04_F4CLNY = new TYData("DAT04_F4CLNY", null);
            this.DAT04_F4AMNY = new TYData("DAT04_F4AMNY", null);
            this.DAT04_F4HISAB = new TYData("DAT04_F4HISAB", null);

            this.DAT05_F5DTIS = new TYData("DAT05_F5DTIS", null);
            this.DAT05_F5DTED = new TYData("DAT05_F5DTED", null);
            this.DAT05_F5CLNC = new TYData("DAT05_F5CLNC", null);
            this.DAT05_F5AMIS = new TYData("DAT05_F5AMIS", null);
            this.DAT05_F5IDUS1 = new TYData("DAT05_F5IDUS1", null);
            this.DAT05_F5IDUS2 = new TYData("DAT05_F5IDUS2", null);
            this.DAT05_F5NONC = new TYData("DAT05_F5NONC", null);


        }

        private void TYACEI015I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT03_F3NONY);
            this.ControlFactory.Add(this.DAT03_F3SSYN);
            this.ControlFactory.Add(this.DAT03_F3CDAC);
            this.ControlFactory.Add(this.DAT03_F3DTIS);
            this.ControlFactory.Add(this.DAT03_F3DTBG);
            this.ControlFactory.Add(this.DAT03_F3HSYN);
            this.ControlFactory.Add(this.DAT03_F3DTED);
            this.ControlFactory.Add(this.DAT03_F3CDDP);
            this.ControlFactory.Add(this.DAT03_F3SADP);
            this.ControlFactory.Add(this.DAT03_F3CLNY);
            this.ControlFactory.Add(this.DAT03_F3AMNY);
            this.ControlFactory.Add(this.DAT03_F3CDFD);
            this.ControlFactory.Add(this.DAT03_F3RPYY);
            this.ControlFactory.Add(this.DAT03_F3BKPY);
            this.ControlFactory.Add(this.DAT03_F3NOAC);
            this.ControlFactory.Add(this.DAT03_F3ACCR);
            this.ControlFactory.Add(this.DAT03_F3GUBUN);
            this.ControlFactory.Add(this.DAT03_F3HISAB);

            this.ControlFactory.Add(this.DAT04_F4NONY);
            this.ControlFactory.Add(this.DAT04_F4IDBG);
            this.ControlFactory.Add(this.DAT04_F4CDDP);
            this.ControlFactory.Add(this.DAT04_F4BKPY);
            this.ControlFactory.Add(this.DAT04_F4DTIS);
            this.ControlFactory.Add(this.DAT04_F4DTED);
            this.ControlFactory.Add(this.DAT04_F4CLNY);
            this.ControlFactory.Add(this.DAT04_F4AMNY);
            this.ControlFactory.Add(this.DAT04_F4HISAB);

            this.ControlFactory.Add(this.DAT05_F5DTIS);
            this.ControlFactory.Add(this.DAT05_F5DTED);
            this.ControlFactory.Add(this.DAT05_F5CLNC);
            this.ControlFactory.Add(this.DAT05_F5AMIS);
            this.ControlFactory.Add(this.DAT05_F5IDUS1);
            this.ControlFactory.Add(this.DAT05_F5IDUS2);
            this.ControlFactory.Add(this.DAT05_F5NONC);


            this.CBH01_F3BKPY.SetReadOnly(true);
            this.TXT01_F3NOAC.SetReadOnly(true);
            this.TXT01_F3HSYN.SetReadOnly(true);
            this.DTP01_F3DTBG.SetReadOnly(true);
            this.DTP01_F3DTPY.SetReadOnly(true);
  
            if (string.IsNullOrEmpty(this.fsF3NONY))
            {
                this.CBO01_F3SSYN.SetValue("10");
                this.CBO01_F3CDAC.SetValue("21100201");
                this.DTP01_F3DTIS.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.DTP01_F3DTED.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                this.CBO01_F3GUBUN.SetValue("1");
                this.CBH01_F3CDDP.SetValue("A50300");
                this.CBH01_F3CDFD.SetValue("21700");
                this.CBH01_F3ACCR.SetValue("11100301");
                this.DTP01_F3DTPY.SetValue("");
                this.DTP01_F3DTBG.SetValue("");
                this.CBH01_F3CDDP.DummyValue = this.DTP01_F3DTIS.GetString(); 

                this.SetStartingFocus(TXT01_F3NONY); 
            }
            else
            {
                this.TXT01_F3NONY.SetReadOnly(true);
                this.CBO01_F3SSYN.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25T68697", this.fsF3NONY);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_F3CDDP.DummyValue = dt.Rows[0]["F3DTIS"].ToString().Replace("-","");
                    this.CBH01_F3SADP.DummyValue = dt.Rows[0]["F3DTIS"].ToString().Replace("-", ""); 

                    this.CurrentDataTableRowMapping(dt, "01");                  
                }

                this.SetStartingFocus(this.DTP01_F3DTIS); 
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
            if (string.IsNullOrEmpty(this.fsF3NONY))
            {
                //지급어음 마스타 등록               
                this.DbConnector.Attach("TY_P_AC_25T63698", this.ControlFactory, "03"); 
                //지급어음 내역 등록
                this.DbConnector.Attach("TY_P_AC_25T6B701", this.ControlFactory, "04"); 
            }
            else
            {
                this.ControlFactory.Remove(this.DAT03_F3HSYN);
                //지급어음 마스타 수정
                this.DbConnector.Attach("TY_P_AC_25T6A699", this.ControlFactory, "03"); 
                //지급어음 내역 수정
                this.DbConnector.Attach("TY_P_AC_25T6D702", this.ControlFactory, "04"); 
            }
            //어음 수표 용지 수정
            this.DbConnector.Attach("TY_P_AC_25T6F704", this.ControlFactory, "05"); 

            //미지급금관리 어음번호 표기
            this.DbConnector.Attach("TY_P_AC_3344M225", this.DAT03_F3NONY.GetValue(), TYUserInfo.EmpNo, this.DTP01_F3DTIS.GetString(), this.DAT03_F3CLNY.GetValue(), this.TXT01_M1NOSQ.GetValue()); 

            this.DbConnector.ExecuteTranQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            if (string.IsNullOrEmpty(this.fsF3NONY))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25T68697", this.TXT01_F3NONY.GetValue());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            //어음수표용지 존재유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25T6H705", this.TXT01_F3NONY.GetValue());
            if (this.DbConnector.ExecuteDataTable().Rows.Count  <= 0)
            {
                this.ShowMessage("TY_M_AC_25U1E722");
                e.Successed = false;
                return;
            }
            //지급어음에 현상태의 이외 경우가 존재하는지 찾기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25U1J725", this.TXT01_F3NONY.GetValue(), this.CBO01_F3SSYN.GetValue());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.ShowMessage("TY_M_AC_25O4H641");
                e.Successed = false;
                return;
            }          
            //지급어음 내역 파일의 발행일자가 더 큰지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25U1R726", this.TXT01_F3NONY.GetValue(), this.CBO01_F3SSYN.GetValue(), this.DTP01_F3DTIS.GetString());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.ShowMessage("TY_M_AC_25U1T727");
                e.Successed = false;
                return;
            }

            //등록일경우
            if (string.IsNullOrEmpty(this.fsF3NONY))
            {
                this.TXT01_F3HSYN.SetValue(this.CBO01_F3SSYN.GetValue());
                this.DTP01_F3DTBG.SetValue(""); 
            }

            this.DAT03_F3NONY.SetValue(this.TXT01_F3NONY.GetValue());
            this.DAT03_F3SSYN.SetValue(this.CBO01_F3SSYN.GetValue());
            this.DAT03_F3CDAC.SetValue(this.CBO01_F3CDAC.GetValue());
            this.DAT03_F3DTIS.SetValue(this.DTP01_F3DTIS.GetString());
            this.DAT03_F3DTBG.SetValue("");
            this.DAT03_F3HSYN.SetValue(this.TXT01_F3HSYN.GetValue());
            this.DAT03_F3DTED.SetValue(this.DTP01_F3DTED.GetString());
            this.DAT03_F3CDDP.SetValue(this.CBH01_F3CDDP.GetValue());
            this.DAT03_F3SADP.SetValue(this.CBH01_F3SADP.GetValue());
            this.DAT03_F3CLNY.SetValue(this.CBH01_F3CLNY.GetValue());
            this.DAT03_F3AMNY.SetValue(Get_Numeric(this.TXT01_F3AMNY.GetValue().ToString()));
            this.DAT03_F3CDFD.SetValue(this.CBH01_F3CDFD.GetValue());
            this.DAT03_F3RPYY.SetValue(this.TXT01_F3RPYY.GetValue());
            this.DAT03_F3BKPY.SetValue(this.CBH01_F3BKPY.GetValue());
            this.DAT03_F3NOAC.SetValue(this.TXT01_F3NOAC.GetValue());
            this.DAT03_F3ACCR.SetValue(this.CBH01_F3ACCR.GetValue());
            this.DAT03_F3GUBUN.SetValue(this.CBO01_F3GUBUN.GetValue());
            this.DAT03_F3HISAB.SetValue(Employer.UserID);

            this.DAT04_F4NONY.SetValue(this.TXT01_F3NONY.GetValue());
            this.DAT04_F4IDBG.SetValue(this.CBO01_F3SSYN.GetValue());
            this.DAT04_F4CDDP.SetValue(this.CBH01_F3CDDP.GetValue());
            this.DAT04_F4BKPY.SetValue(this.CBH01_F3BKPY.GetValue());
            this.DAT04_F4DTIS.SetValue(this.DTP01_F3DTIS.GetString());
            this.DAT04_F4DTED.SetValue(this.DTP01_F3DTED.GetString());
            this.DAT04_F4CLNY.SetValue(this.CBH01_F3CLNY.GetValue());
            this.DAT04_F4AMNY.SetValue(Get_Numeric(this.TXT01_F3AMNY.GetValue().ToString()));

            this.DAT05_F5DTIS.SetValue(this.DTP01_F3DTIS.GetString());
            this.DAT05_F5DTED.SetValue(this.DTP01_F3DTED.GetString());
            this.DAT05_F5CLNC.SetValue(this.CBH01_F3CLNY.GetValue());
            this.DAT05_F5AMIS.SetValue(Get_Numeric(this.TXT01_F3AMNY.GetValue().ToString()));
            this.DAT05_F5IDUS1.SetValue(this.CBO01_F3CDAC.GetValue());
            this.DAT05_F5IDUS2.SetValue(this.CBO01_F3CDAC.GetValue());
            this.DAT05_F5NONC.SetValue(this.TXT01_F3NONY.GetValue()); 
            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : DTP01_F3DTIS_ValueChanged 이벤트
        private void DTP01_F3DTIS_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_F3CDDP.DummyValue = this.DTP01_F3DTIS.GetString();
            this.CBH01_F3SADP.DummyValue = this.DTP01_F3DTIS.GetString();
        }
        #endregion

        #region Description : TXT01_F3NONY_TextChanged 이벤트
        private void TXT01_F3NONY_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsF3NONY))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25T6H705", this.TXT01_F3NONY.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.CBH01_F3BKPY.SetValue(dt.Rows[0]["F5CDBK"].ToString());
                    this.TXT01_F3NOAC.SetValue(dt.Rows[0]["F5NOAC"].ToString());
                }
            }
        }
        #endregion

        #region Description : BTN61_F3NONY_Click 이벤트
        private void BTN61_F3NONY_Click(object sender, EventArgs e)
        {
            UP_PopHelp_NONC();
        }
        #endregion

        #region Description : TXT01_F3NONY_KeyDown 이벤트
        private void TXT01_F3NONY_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                UP_PopHelp_NONC();
            }
        }
        #endregion

        #region Description : 지급어음 팝업 조회 이벤트
        private void UP_PopHelp_NONC()
        {
            TYAZEI15C1 popup = new TYAZEI15C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TXT01_F3NONY.SetValue(popup.fsF5NONC);
                CBH01_F3CLNY.SetValue(popup.fsF5CLNC);

                this.SetFocus(this.CBH01_F3CLNY);
            }
        }
        #endregion

        #region Description : BTN61_F3CLNY_Click 이벤트
        private void BTN61_F3CLNY_Click(object sender, EventArgs e)
        {
            UP_PopHelp_VEND();
        }
        #endregion

        #region Description : 지급어음 팝업 조회 이벤트
        private void UP_PopHelp_VEND()
        {
            TYAZEI15C2 popup = new TYAZEI15C2(CBH01_F3CLNY.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DTP01_F3DTIS.SetValue(popup.fsM1DTED);
                TXT01_F3AMNY.SetValue(popup.fsM1AMT);
                CBH01_F3CLNY.SetValue(popup.fsM1VNCD);
                TXT01_F3RPYY.SetValue(popup.fsM1RKAC);
                CBH01_F3SADP.SetValue(popup.fsM1WNJP.ToString().Substring(0, 6));
                TXT01_M1NOSQ.SetValue(popup.fsM1NOSQ); 

                this.SetFocus(this.CBH01_F3CLNY);
            }
        }
        #endregion

    }
}
