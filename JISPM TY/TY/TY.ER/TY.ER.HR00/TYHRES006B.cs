using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 직급별 인건비 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.19 11:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BJBZ457 : EIS 직급별 인건비 조회
    ///  TY_P_HR_2BJC2458 : EIS 직급별 인건비 등록
    ///  TY_P_HR_2BJC3459 : EIS 직급별 인건비 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRES006B : TYBase
    {
        private TYData DAT02_ELRYYMM;
        private TYData DAT02_ELRSAUP;
        private TYData DAT02_ELRJKCD;
        private TYData DAT02_ELRJKCDNM;
        private TYData DAT02_ELRJKGN;
        private TYData DAT02_ELRPAYTOTAL;
        private TYData DAT02_ELRPEOPLE;
        private TYData DAT02_ELRHIDAT;
        private TYData DAT02_ELRHITIM;
        private TYData DAT02_ELRHISAB;       


        #region  Description : 폼 로드 이벤트
        public TYHRES006B()
        {
            InitializeComponent();

            this.DAT02_ELRYYMM = new TYData("DAT02_ELRYYMM", null);
            this.DAT02_ELRSAUP = new TYData("DAT02_ELRSAUP", null);
            this.DAT02_ELRJKCD = new TYData("DAT02_ELRJKCD", null);
            this.DAT02_ELRJKCDNM = new TYData("DAT02_ELRJKCDNM", null);
            this.DAT02_ELRJKGN = new TYData("DAT02_ELRJKGN", null);
            this.DAT02_ELRPAYTOTAL = new TYData("DAT02_ELRPAYTOTAL", null);
            this.DAT02_ELRPEOPLE = new TYData("DAT02_ELRPEOPLE", null);
            this.DAT02_ELRHIDAT = new TYData("DAT02_ELRHIDAT", null);
            this.DAT02_ELRHITIM = new TYData("DAT02_ELRHITIM", null);
            this.DAT02_ELRHISAB = new TYData("DAT02_ELRHISAB", null);       

        }

        private void TYHRES006B_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(DAT02_ELRYYMM);
            this.ControlFactory.Add(DAT02_ELRSAUP);
            this.ControlFactory.Add(DAT02_ELRJKCD);
            this.ControlFactory.Add(DAT02_ELRJKCDNM);
            this.ControlFactory.Add(DAT02_ELRJKGN);
            this.ControlFactory.Add(DAT02_ELRPAYTOTAL);
            this.ControlFactory.Add(DAT02_ELRPEOPLE);
            this.ControlFactory.Add(DAT02_ELRHIDAT);
            this.ControlFactory.Add(DAT02_ELRHITIM);
            this.ControlFactory.Add(DAT02_ELRHISAB);

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.MTB01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.SetStartingFocus(this.MTB01_GSTYYMM);
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }           

        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            //신인사 적용후 사용함
            UP_PAY_Create();

            /*
            string sSDATE = "";
            string sEDATE = "";
            string sJKCD = "";
            string sYYMM = "";
            string sMaxValue = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            sSDATE = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "01";
            sEDATE = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "12";

            sYYMM = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4);


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BJC3459", MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQuery(); 
            
            this.DbConnector.CommandClear();
            //직급코드
            this.DbConnector.Attach("TY_P_HR_28TBA623", "JK", "", "");
            DataTable dtjk = this.DbConnector.ExecuteDataTable();

            if (dtjk.Rows.Count > 0)
            {
                for (int i = 0; i < dtjk.Rows.Count; i++)
                {
                    sJKCD = dtjk.Rows[i]["CODE"].ToString();

                    this.DbConnector.Attach("TY_P_HR_2BJBZ457", sSDATE, sEDATE, sJKCD, sSDATE, sEDATE, sJKCD, sSDATE, sEDATE, sJKCD);
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        sYYMM = dt.Rows[j]["PYDATE"].ToString();

                        this.DAT02_ELRYYMM.SetValue(sYYMM);
                        this.DAT02_ELRSAUP.SetValue(dt.Rows[j]["PYDEPT"].ToString());
                        this.DAT02_ELRJKCD.SetValue(dt.Rows[j]["PYJIKGUB"].ToString());
                        this.DAT02_ELRJKCDNM.SetValue(UP_SET_JKNAME(dt.Rows[j]["PYJIKGUB"].ToString()));
                        this.DAT02_ELRPAYTOTAL.SetValue(dt.Rows[j]["PAYTOTAL"].ToString());
                        this.DAT02_ELRPEOPLE.SetValue(dt.Rows[j]["CNT"].ToString());
                        this.DAT02_ELRHISAB.SetValue(TYUserInfo.EmpNo);
                        if (sJKCD == "01" || sJKCD == "1A" || sJKCD == "1B" || sJKCD == "2A" || sJKCD == "2B")
                        {
                            this.DAT02_ELRJKGN.SetValue("1");
                        }
                        else if (sJKCD == "2C" || sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")
                        {
                            this.DAT02_ELRJKGN.SetValue("2");
                        }
                        else
                        {
                            //3c, 3d, 6c(현장)
                            this.DAT02_ELRJKGN.SetValue("3");
                        }

                        datas.Add(new object[] {  this.DAT02_ELRYYMM.GetValue(), 
                                                  this.DAT02_ELRSAUP.GetValue(),
                                                  this.DAT02_ELRJKCD.GetValue(),
                                                  this.DAT02_ELRJKCDNM.GetValue(),
                                                  this.DAT02_ELRJKGN.GetValue(), 
                                                  this.DAT02_ELRPAYTOTAL.GetValue(),
                                                  this.DAT02_ELRPEOPLE.GetValue(),
                                                  this.DAT02_ELRHISAB.GetValue()
                                                });
                    } //for (int j = 0; j < dt.Rows.Count; j++)...end

                    sMaxValue = dt.Compute("MAX(PYDATE)", "").ToString();

                    if (sMaxValue != sEDATE)
                    {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_2BR5A684", sMaxValue, sJKCD);
                            DataTable dtpl = this.DbConnector.ExecuteDataTable();

                            for (int k = 0; k < dtpl.Rows.Count; k++)
                            {
                                this.DAT02_ELRYYMM.SetValue(dtpl.Rows[k]["ELFYYMM"].ToString());
                                this.DAT02_ELRSAUP.SetValue(dtpl.Rows[k]["ELFSAUP"].ToString());
                                this.DAT02_ELRJKCD.SetValue(dtpl.Rows[k]["ELFJKCD"].ToString());
                                this.DAT02_ELRJKCDNM.SetValue(UP_SET_JKNAME(dtpl.Rows[k]["ELFJKCD"].ToString()));
                                this.DAT02_ELRPAYTOTAL.SetValue(dtpl.Rows[k]["ELFPAYTOTAL"].ToString());
                                this.DAT02_ELRPEOPLE.SetValue(dtpl.Rows[k]["ELFPEOPLE"].ToString());
                                this.DAT02_ELRHISAB.SetValue(TYUserInfo.EmpNo);
                                if (sJKCD == "01" || sJKCD == "1A" || sJKCD == "1B" || sJKCD == "2A" || sJKCD == "2B")
                                {
                                    this.DAT02_ELRJKGN.SetValue("1");
                                }
                                else if (sJKCD == "2C" || sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")
                                {
                                    this.DAT02_ELRJKGN.SetValue("2");
                                }
                                else
                                {
                                    //3c, 3d, 6c(현장)
                                    this.DAT02_ELRJKGN.SetValue("3");
                                }

                                datas.Add(new object[] {  this.DAT02_ELRYYMM.GetValue(), 
                                                  this.DAT02_ELRSAUP.GetValue(),
                                                  this.DAT02_ELRJKCD.GetValue(),
                                                  this.DAT02_ELRJKCDNM.GetValue(),
                                                  this.DAT02_ELRJKGN.GetValue(), 
                                                  this.DAT02_ELRPAYTOTAL.GetValue(),
                                                  this.DAT02_ELRPEOPLE.GetValue(),
                                                  this.DAT02_ELRHISAB.GetValue()
                                                });
                            } //for (int k = 0; k < dtpl.Rows.Count; k++)...end
                    } //if (sMaxValue != sEDATE)...end

                } //for (int i = 0; i < dtjk.Rows.Count; i++)..end

            } //if (dtjk.Rows.Count > 0)...end

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_HR_2BJC2458", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }
             

            this.ShowMessage("TY_M_GB_26E30875");
              */

        }
        #endregion


        #region  Description : 신 인사용 직급별 급여계산 함수
        private void UP_PAY_Create()
        {
            string sSDATE = "";
            string sEDATE = "";
            string sJKCD = "";
            string sYYMM = "";
            string sMaxValue = "";

            int icnt = 0;

            try
            {

                System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

                sSDATE = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "01";
                sEDATE = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "12";

                sYYMM = MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4);


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_2BJC3459", MTB01_GSTYYMM.GetValue().ToString().Substring(0, 4));
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                //직급코드
                this.DbConnector.Attach("TY_P_HR_4B5GY325", "JK", "", "");
                DataTable dtjk = this.DbConnector.ExecuteDataTable();

                if (dtjk.Rows.Count > 0)
                {
                    for (int i = 0; i < dtjk.Rows.Count; i++)
                    {
                        sJKCD = dtjk.Rows[i]["CODE"].ToString();

                        this.DbConnector.Attach("TY_P_HR_5C49I263", sSDATE, sEDATE, sJKCD, sSDATE, sEDATE, sJKCD, sSDATE, sEDATE, sJKCD);
                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            sYYMM = dt.Rows[j]["PYDATE"].ToString();

                            this.DAT02_ELRYYMM.SetValue(sYYMM);
                            this.DAT02_ELRSAUP.SetValue(dt.Rows[j]["PYDEPT"].ToString());
                            this.DAT02_ELRJKCD.SetValue(dt.Rows[j]["PYJIKGUB"].ToString());
                            this.DAT02_ELRJKCDNM.SetValue(UP_SET_JKNAME(dt.Rows[j]["PYJIKGUB"].ToString()));
                            this.DAT02_ELRPAYTOTAL.SetValue(dt.Rows[j]["PAYTOTAL"].ToString());
                            this.DAT02_ELRPEOPLE.SetValue(dt.Rows[j]["CNT"].ToString());
                            this.DAT02_ELRHISAB.SetValue(TYUserInfo.EmpNo);
                            if (sJKCD == "01" || sJKCD == "1A" || sJKCD == "1B" || sJKCD == "2A" || sJKCD == "2B")
                            {
                                this.DAT02_ELRJKGN.SetValue("1");
                            }
                            else if (sJKCD == "2C" || sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")
                            {
                                this.DAT02_ELRJKGN.SetValue("2");
                            }
                            else
                            {
                                //3c, 3d, 6c(현장)
                                this.DAT02_ELRJKGN.SetValue("3");
                            }

                            datas.Add(new object[] {  this.DAT02_ELRYYMM.GetValue(), 
                                                  this.DAT02_ELRSAUP.GetValue(),
                                                  this.DAT02_ELRJKCD.GetValue(),
                                                  this.DAT02_ELRJKCDNM.GetValue(),
                                                  this.DAT02_ELRJKGN.GetValue(), 
                                                  this.DAT02_ELRPAYTOTAL.GetValue(),
                                                  this.DAT02_ELRPEOPLE.GetValue(),
                                                  this.DAT02_ELRHISAB.GetValue()
                                                });
                        } //for (int j = 0; j < dt.Rows.Count; j++)...end

                        sMaxValue = dt.Compute("MAX(PYDATE)", "").ToString();

                        if (sMaxValue != sEDATE)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_2BR5A684", sMaxValue, sJKCD);
                            DataTable dtpl = this.DbConnector.ExecuteDataTable();

                            for (int k = 0; k < dtpl.Rows.Count; k++)
                            {
                                this.DAT02_ELRYYMM.SetValue(dtpl.Rows[k]["ELFYYMM"].ToString());
                                this.DAT02_ELRSAUP.SetValue(dtpl.Rows[k]["ELFSAUP"].ToString());
                                this.DAT02_ELRJKCD.SetValue(dtpl.Rows[k]["ELFJKCD"].ToString());
                                this.DAT02_ELRJKCDNM.SetValue(UP_SET_JKNAME(dtpl.Rows[k]["ELFJKCD"].ToString()));
                                this.DAT02_ELRPAYTOTAL.SetValue(dtpl.Rows[k]["ELFPAYTOTAL"].ToString());
                                this.DAT02_ELRPEOPLE.SetValue(dtpl.Rows[k]["ELFPEOPLE"].ToString());
                                this.DAT02_ELRHISAB.SetValue(TYUserInfo.EmpNo);
                                if (sJKCD == "01" || sJKCD == "1A" || sJKCD == "1B" || sJKCD == "2A" || sJKCD == "2B")
                                {
                                    this.DAT02_ELRJKGN.SetValue("1");
                                }
                                else if (sJKCD == "2C" || sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")
                                {
                                    this.DAT02_ELRJKGN.SetValue("2");
                                }
                                else
                                {
                                    //3c, 3d, 6c(현장)
                                    this.DAT02_ELRJKGN.SetValue("3");
                                }

                                datas.Add(new object[] {  this.DAT02_ELRYYMM.GetValue(), 
                                                  this.DAT02_ELRSAUP.GetValue(),
                                                  this.DAT02_ELRJKCD.GetValue(),
                                                  this.DAT02_ELRJKCDNM.GetValue(),
                                                  this.DAT02_ELRJKGN.GetValue(), 
                                                  this.DAT02_ELRPAYTOTAL.GetValue(),
                                                  this.DAT02_ELRPEOPLE.GetValue(),
                                                  this.DAT02_ELRHISAB.GetValue()
                                                });
                            } //for (int k = 0; k < dtpl.Rows.Count; k++)...end
                        } //if (sMaxValue != sEDATE)...end

                    } //for (int i = 0; i < dtjk.Rows.Count; i++)..end

                } //if (dtjk.Rows.Count > 0)...end

                if (datas.Count > 0)
                {                    
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_2BJC2458", data);
                        this.DbConnector.ExecuteTranQuery();
                    }                  
                }
            }
            catch
            {
                string ddd = icnt.ToString();

                string ss = ddd;
            }

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion


        #region  Description : 직급 이름 반환 함수
        private string UP_SET_JKNAME(string sJKCD)
        {
            string sValue = "";

            switch (sJKCD)
            {
                case "01":
                   sValue = "임원";
                   break;
                case "1A":
                   sValue = "부장";
                   break;
                case "1B":
                   sValue = "차장";
                   break;
                case "2A":
                   sValue = "과장";
                   break;
                case "2B":
                   sValue = "대리";
                   break;
                case "2C":
                   sValue = "주임";
                   break;
                case "3A":
                   sValue = "사원-A";
                   break;
                case "3B":
                   sValue = "사원-B";
                   break;
                case "4A":
                   sValue = "운전";
                   break;
                case "3C":
                   sValue = "운영주임";
                   break;
                case "3D":
                   sValue = "운영사원";
                   break;
                case "6C":
                   sValue = "계약";
                   break;
                default:
                    sValue = "";
                    break;
            }


            return sValue;

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
