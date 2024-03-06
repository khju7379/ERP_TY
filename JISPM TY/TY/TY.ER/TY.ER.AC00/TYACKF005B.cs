using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금계획생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.27 17:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CR5E382 : 자금계획생성  받을어음 조회
    ///  TY_P_AC_2CR6J387 : 자금계획생성 지급어음 조회
    ///  TY_P_AC_33S4T388 : 자금계획 등록(자동욜)
    ///  TY_P_AC_2CR6Q390 : 자금계획 삭제
    ///  TY_P_AC_2CR70392 : 자금계획생성 외화단기차입금 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  INQOPTION : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF005B : TYBase
    {

        private TYData DAT02_PHDPAC;
        private TYData DAT02_PHSABUN;
        private TYData DAT02_PHIPDATE;
        private TYData DAT02_PHNOSQ;
        private TYData DAT02_PHCDAC;
        private TYData DAT02_PHCDFD;
        private TYData DAT02_PHFDGUBN;
        private TYData DAT02_PHVEND;
        private TYData DAT02_PHBANK;
        private TYData DAT02_PHYUL;
        private TYData DAT02_PHAMTGN;
        private TYData DAT02_PHAWAMT;
        private TYData DAT02_PHAIAMT;
        private TYData DAT02_PHRKAC;
        private TYData DAT02_PHSTGBN;
        private TYData DAT02_PHBLGUBN;
        private TYData DAT02_PHBLSABN;
        private TYData DAT02_PHBLTEAM;
        private TYData DAT02_PHBLYYNO;
        private TYData DAT02_PHBLSQNO;
        private TYData DAT02_PHBLNO;
        private TYData DAT02_PHLCBLNO;
        private TYData DAT02_PHPUMMOK;
        private TYData DAT02_PHDATE;
        private TYData DAT02_PHHISAB;     

        #region  Description : 폼 로드 이벤트
        public TYACKF005B()
        {
            InitializeComponent();

            this.SetPopupStyle();
            
            this.DAT02_PHDPAC= new TYData("DAT02_PHDPAC", null);       
            this.DAT02_PHSABUN= new TYData("DAT02_PHSABUN", null);      
            this.DAT02_PHIPDATE= new TYData("DAT02_PHIPDATE", null);     
            this.DAT02_PHNOSQ= new TYData("DAT02_PHNOSQ", null);       
            this.DAT02_PHCDAC= new TYData("DAT02_PHCDAC", null);       
            this.DAT02_PHCDFD= new TYData("DAT02_PHCDFD", null);       
            this.DAT02_PHFDGUBN= new TYData("DAT02_PHFDGUBN", null);     
            this.DAT02_PHVEND= new TYData("DAT02_PHVEND", null);       
            this.DAT02_PHBANK= new TYData("DAT02_PHBANK", null);       
            this.DAT02_PHYUL= new TYData("DAT02_PHYUL", null);        
            this.DAT02_PHAMTGN= new TYData("DAT02_PHAMTGN", null);      
            this.DAT02_PHAWAMT= new TYData("DAT02_PHAWAMT", null);      
            this.DAT02_PHAIAMT= new TYData("DAT02_PHAIAMT", null);      
            this.DAT02_PHRKAC= new TYData("DAT02_PHRKAC", null);       
            this.DAT02_PHSTGBN= new TYData("DAT02_PHSTGBN", null);      
            this.DAT02_PHBLGUBN= new TYData("DAT02_PHBLGUBN", null);     
            this.DAT02_PHBLSABN= new TYData("DAT02_PHBLSABN", null);     
            this.DAT02_PHBLTEAM= new TYData("DAT02_PHBLTEAM", null);     
            this.DAT02_PHBLYYNO= new TYData("DAT02_PHBLYYNO", null);     
            this.DAT02_PHBLSQNO= new TYData("DAT02_PHBLSQNO", null);     
            this.DAT02_PHBLNO= new TYData("DAT02_PHBLNO", null);       
            this.DAT02_PHLCBLNO= new TYData("DAT02_PHLCBLNO", null);     
            this.DAT02_PHPUMMOK= new TYData("DAT02_PHPUMMOK", null);     
            this.DAT02_PHDATE= new TYData("DAT02_PHDATE", null);
            this.DAT02_PHHISAB = new TYData("DAT02_PHHISAB", null);        
				                    
        }

        private void TYACKF005B_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_PHDPAC);
            this.ControlFactory.Add(this.DAT02_PHSABUN);
            this.ControlFactory.Add(this.DAT02_PHIPDATE);
            this.ControlFactory.Add(this.DAT02_PHNOSQ);
            this.ControlFactory.Add(this.DAT02_PHCDAC);
            this.ControlFactory.Add(this.DAT02_PHCDFD);
            this.ControlFactory.Add(this.DAT02_PHFDGUBN);
            this.ControlFactory.Add(this.DAT02_PHVEND);
            this.ControlFactory.Add(this.DAT02_PHBANK);
            this.ControlFactory.Add(this.DAT02_PHYUL);
            this.ControlFactory.Add(this.DAT02_PHAMTGN);
            this.ControlFactory.Add(this.DAT02_PHAWAMT);
            this.ControlFactory.Add(this.DAT02_PHAIAMT);
            this.ControlFactory.Add(this.DAT02_PHRKAC);
            this.ControlFactory.Add(this.DAT02_PHSTGBN);
            this.ControlFactory.Add(this.DAT02_PHBLGUBN);
            this.ControlFactory.Add(this.DAT02_PHBLSABN);
            this.ControlFactory.Add(this.DAT02_PHBLTEAM);
            this.ControlFactory.Add(this.DAT02_PHBLYYNO);
            this.ControlFactory.Add(this.DAT02_PHBLSQNO);
            this.ControlFactory.Add(this.DAT02_PHBLNO);
            this.ControlFactory.Add(this.DAT02_PHLCBLNO);
            this.ControlFactory.Add(this.DAT02_PHPUMMOK);
            this.ControlFactory.Add(this.DAT02_PHDATE);
            this.ControlFactory.Add(this.DAT02_PHHISAB);     

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            Int32 iCnt = 0;
            Int32 iCHK = 0;

            DataTable dt = new DataTable(); 

            if (CBO01_GOKCR.GetValue().ToString() == "A") //생성
            {
                this.DbConnector.CommandClear();
                if (CBO01_INQOPTION.GetValue().ToString() == "11100501")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "11200");
                }
                else if (CBO01_INQOPTION.GetValue().ToString() == "21100202")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "22500");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "21610");
                }
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                if (CBO01_INQOPTION.GetValue().ToString() == "11100501")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR5E382", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString());
                }
                else if (CBO01_INQOPTION.GetValue().ToString() == "21100202")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6J387", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString());
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_2CR70392", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString());
                }

                dt = this.DbConnector.ExecuteDataTable();

                iCHK = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (CBO01_INQOPTION.GetValue().ToString() == "11100501")
                        {
                            this.DAT02_PHDPAC.SetValue(dt.Rows[i]["E6CDSO"].ToString());
                            this.DAT02_PHSABUN.SetValue(TYUserInfo.EmpNo);
                            this.DAT02_PHIPDATE.SetValue(dt.Rows[i]["E6DTED"].ToString());

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_2CR7M393", this.DAT02_PHDPAC.GetValue(), this.DAT02_PHSABUN.GetValue());
                            iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                            this.DAT02_PHNOSQ.SetValue(iCnt.ToString());
                            this.DAT02_PHCDAC.SetValue(CBO01_INQOPTION.GetValue().ToString());
                            this.DAT02_PHCDFD.SetValue("11200");
                            this.DAT02_PHFDGUBN.SetValue("1");
                            this.DAT02_PHVEND.SetValue("");
                            this.DAT02_PHBANK.SetValue("");
                            this.DAT02_PHYUL.SetValue("0");
                            this.DAT02_PHAMTGN.SetValue("");
                            this.DAT02_PHAWAMT.SetValue(dt.Rows[i]["E6AMNR"].ToString());
                            this.DAT02_PHAIAMT.SetValue("0");
                            this.DAT02_PHRKAC.SetValue("받을어음 입금");
                            this.DAT02_PHSTGBN.SetValue("1");
                            this.DAT02_PHBLGUBN.SetValue("");
                            this.DAT02_PHBLSABN.SetValue("");
                            this.DAT02_PHBLTEAM.SetValue("");
                            this.DAT02_PHBLYYNO.SetValue("");
                            this.DAT02_PHBLSQNO.SetValue("0");
                            this.DAT02_PHBLNO.SetValue("");
                            this.DAT02_PHLCBLNO.SetValue("");
                            this.DAT02_PHPUMMOK.SetValue("");
                            this.DAT02_PHDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                            this.DAT02_PHHISAB.SetValue(TYUserInfo.EmpNo);
                        }
                        else if (CBO01_INQOPTION.GetValue().ToString() == "21100202")
                        {
                            this.DAT02_PHDPAC.SetValue(dt.Rows[i]["B2DPAC"].ToString());
                            this.DAT02_PHSABUN.SetValue(TYUserInfo.EmpNo);
                            this.DAT02_PHIPDATE.SetValue(dt.Rows[i]["F3DTED"].ToString());

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_2CR7M393", this.DAT02_PHDPAC.GetValue(), this.DAT02_PHSABUN.GetValue());
                            iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                            this.DAT02_PHNOSQ.SetValue(iCnt.ToString());
                            this.DAT02_PHCDAC.SetValue(CBO01_INQOPTION.GetValue().ToString());
                            this.DAT02_PHCDFD.SetValue("22500");
                            this.DAT02_PHFDGUBN.SetValue("2");
                            this.DAT02_PHVEND.SetValue("");
                            this.DAT02_PHBANK.SetValue(dt.Rows[i]["F3BKPY"].ToString());
                            this.DAT02_PHYUL.SetValue("0");
                            this.DAT02_PHAMTGN.SetValue("");
                            this.DAT02_PHAWAMT.SetValue(dt.Rows[i]["F3AMNY"].ToString());
                            this.DAT02_PHAIAMT.SetValue("0");
                            this.DAT02_PHRKAC.SetValue("지급어음 결제");
                            this.DAT02_PHSTGBN.SetValue("1");
                            this.DAT02_PHBLGUBN.SetValue("");
                            this.DAT02_PHBLSABN.SetValue("");
                            this.DAT02_PHBLTEAM.SetValue("");
                            this.DAT02_PHBLYYNO.SetValue("");
                            this.DAT02_PHBLSQNO.SetValue("0");
                            this.DAT02_PHBLNO.SetValue("");
                            this.DAT02_PHLCBLNO.SetValue("");
                            this.DAT02_PHPUMMOK.SetValue("");
                            this.DAT02_PHDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                            this.DAT02_PHHISAB.SetValue(TYUserInfo.EmpNo);
                        }
                        else
                        {
                            this.DAT02_PHDPAC.SetValue(dt.Rows[i]["DPAC"].ToString());
                            this.DAT02_PHSABUN.SetValue(dt.Rows[i]["IPBLSABN"].ToString());
                            this.DAT02_PHIPDATE.SetValue(dt.Rows[i]["MGILJA"].ToString());

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_2CR7M393", this.DAT02_PHDPAC.GetValue(), this.DAT02_PHSABUN.GetValue());
                            iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                            this.DAT02_PHNOSQ.SetValue(iCnt.ToString());
                            this.DAT02_PHCDAC.SetValue(CBO01_INQOPTION.GetValue().ToString());
                            this.DAT02_PHCDFD.SetValue("21610");
                            this.DAT02_PHFDGUBN.SetValue("2");
                            this.DAT02_PHVEND.SetValue("");
                            this.DAT02_PHBANK.SetValue(dt.Rows[i]["USSJBK"].ToString());
                            this.DAT02_PHYUL.SetValue(dt.Rows[i]["EXCHANGE"].ToString());
                            this.DAT02_PHAMTGN.SetValue(dt.Rows[i]["HWAPE"].ToString());
                            this.DAT02_PHAWAMT.SetValue(dt.Rows[i]["AMOUNT_KRW"].ToString());
                            this.DAT02_PHAIAMT.SetValue(dt.Rows[i]["AMOUNT"].ToString());
                            this.DAT02_PHRKAC.SetValue(dt.Rows[i]["RKAC"].ToString());
                            this.DAT02_PHSTGBN.SetValue("1");
                            this.DAT02_PHBLGUBN.SetValue(dt.Rows[i]["IPBLGUBN"].ToString());
                            this.DAT02_PHBLSABN.SetValue(dt.Rows[i]["IPBLSABN"].ToString());
                            this.DAT02_PHBLTEAM.SetValue(dt.Rows[i]["IPBLTEAM"].ToString());
                            this.DAT02_PHBLYYNO.SetValue(dt.Rows[i]["IPBLYYNO"].ToString());
                            this.DAT02_PHBLSQNO.SetValue(dt.Rows[i]["IPBLSQNO"].ToString());
                            this.DAT02_PHBLNO.SetValue(dt.Rows[i]["BL_NO"].ToString());
                            this.DAT02_PHLCBLNO.SetValue(dt.Rows[i]["LC_NO"].ToString());
                            this.DAT02_PHPUMMOK.SetValue(dt.Rows[i]["PUMMOK"].ToString());
                            this.DAT02_PHDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
                            this.DAT02_PHHISAB.SetValue(TYUserInfo.EmpNo);
                        }

                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_33S4T388",
                                            this.DAT02_PHDPAC.GetValue().ToString(),
                                            this.DAT02_PHSABUN.GetValue().ToString(),
                                            this.DAT02_PHIPDATE.GetValue().ToString(),
                                            this.DAT02_PHNOSQ.GetValue().ToString(),
                                            this.DAT02_PHCDAC.GetValue().ToString(),
                                            this.DAT02_PHCDFD.GetValue().ToString(),
                                            this.DAT02_PHFDGUBN.GetValue().ToString(),
                                            this.DAT02_PHVEND.GetValue().ToString(),
                                            this.DAT02_PHBANK.GetValue().ToString(),
                                            this.DAT02_PHYUL.GetValue().ToString(),
                                            this.DAT02_PHAMTGN.GetValue().ToString(),
                                            this.DAT02_PHAWAMT.GetValue().ToString(),
                                            this.DAT02_PHAIAMT.GetValue().ToString(),
                                            this.DAT02_PHRKAC.GetValue().ToString(),
                                            this.DAT02_PHSTGBN.GetValue().ToString(),
                                            this.DAT02_PHBLGUBN.GetValue().ToString(),
                                            this.DAT02_PHBLSABN.GetValue().ToString(),
                                            this.DAT02_PHBLTEAM.GetValue().ToString(),
                                            this.DAT02_PHBLYYNO.GetValue().ToString(),
                                            this.DAT02_PHBLSQNO.GetValue().ToString(),
                                            this.DAT02_PHBLNO.GetValue().ToString(),
                                            this.DAT02_PHLCBLNO.GetValue().ToString(),
                                            this.DAT02_PHPUMMOK.GetValue().ToString(),
                                            this.DAT02_PHDATE.GetValue().ToString(),
                                            this.DAT02_PHHISAB.GetValue().ToString()
                                            );

                        this.DbConnector.ExecuteNonQuery();

                    } // End .. for

                    this.ShowMessage("TY_M_GB_26E30875");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2CV43442");
                }
            }
            else
            {
                //취소
                this.DbConnector.CommandClear();
                if (CBO01_INQOPTION.GetValue().ToString() == "11100501")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "11200");
                }
                else if (CBO01_INQOPTION.GetValue().ToString() == "21100202")
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "22500");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_2CR6Q390", DTP01_GSTDATE.GetString(), DTP01_GEDDATE.GetString(), CBO01_INQOPTION.GetValue().ToString(), "21610");
                }
                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_AC_2CDB1167");
            }
           
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    e.Successed = false;
                    return;
                }
            }

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
