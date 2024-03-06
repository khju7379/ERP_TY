using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 퇴직금 중도정산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.29 11:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73TD1143 : 퇴직금 중도정산 등록
    ///  TY_P_HR_73TD4144 : 퇴직금 중도정산 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  MDBUSEO : 부서
    ///  MDSABUN : 사번
    ///  MDACCEDATE : 정산일자E
    ///  MDACCSDATE : 정산일자S
    ///  MDDATE : 중도퇴직일자
    ///  MDAMOUNT : 정산금액
    ///  MDCHAIN : 차인지급액
    ///  MDCHASU : 정산차수
    ///  MDINCOMETAX : 소득세
    ///  MDJKCD : 직급
    ///  MDRESTAX : 주민세
    ///  MDWKDAY : 근무일
    ///  MDWKMONTH : 근무월
    ///  MDWKYEAR : 근무년수
    /// </summary>
    public partial class TYHRKB023I : TYBase
    {
        private string fsMDDATE;
        private string fsMDSABUN;


        #region  Description : 폼 로드 이벤트
        public TYHRKB023I(string sMDDATE, string sMDSABUN)
        {
            InitializeComponent();

            fsMDDATE = sMDDATE;
            fsMDSABUN = sMDSABUN;

        }

        private void TYHRKB023I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_MDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_MDACCSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_MDACCEDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_MDBUSEO.DummyValue = this.DTP01_MDDATE.GetString();

            if (string.IsNullOrEmpty(this.fsMDDATE))
            {
                this.SetStartingFocus(this.DTP01_MDDATE);
            }
            else
            {
                DTP01_MDDATE.SetValue(fsMDDATE);
                CBH01_MDSABUN.SetValue(fsMDSABUN);

                DTP01_MDDATE.SetReadOnly(true);
                CBH01_MDSABUN.SetReadOnly(true);
                
                UP_DataBinding();

                this.SetStartingFocus(this.DTP01_MDACCSDATE);
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73TDC145", this.DTP01_MDDATE.GetString(), this.CBH01_MDSABUN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsMDDATE))
            {

                this.DbConnector.Attach("TY_P_HR_73TD1143", this.DTP01_MDDATE.GetString(), 
                                                            this.CBH01_MDSABUN.GetValue(),
                                                            this.CBH01_MDJKCD.GetValue(), 
                                                            this.CBH01_MDBUSEO.GetValue(), 
                                                            DTP01_MDACCSDATE.GetString().ToString().Replace("19000101",""),
                                                            DTP01_MDACCEDATE.GetString().ToString().Replace("19000101", ""), 
                                                            TXT01_MDWKYEAR.GetValue(), 
                                                            TXT01_MDWKMONTH.GetValue(),  
                                                            TXT01_MDWKDAY.GetValue(),  
                                                            TXT01_MDCHASU.GetValue(),  
                                                            Get_Numeric(TXT01_MDAMOUNT.GetValue().ToString()), 
                                                            Get_Numeric(TXT01_MDINCOMETAX.GetValue().ToString()), 
                                                            Get_Numeric(TXT01_MDRESTAX.GetValue().ToString()),  
                                                            Get_Numeric(TXT01_MDCHAIN.GetValue().ToString()), 
                                                            CBO01_MDINKIYN.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_73TD4144", DTP01_MDACCSDATE.GetString().ToString().Replace("19000101", ""),
                                                            DTP01_MDACCEDATE.GetString().ToString().Replace("19000101", ""), 
                                                            TXT01_MDWKYEAR.GetValue(),
                                                            TXT01_MDWKMONTH.GetValue(),
                                                            TXT01_MDWKDAY.GetValue(),
                                                            TXT01_MDCHASU.GetValue(),
                                                            Get_Numeric(TXT01_MDAMOUNT.GetValue().ToString()),
                                                            Get_Numeric(TXT01_MDINCOMETAX.GetValue().ToString()),
                                                            Get_Numeric(TXT01_MDRESTAX.GetValue().ToString()),
                                                            Get_Numeric(TXT01_MDCHAIN.GetValue().ToString()),
                                                            CBO01_MDINKIYN.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_MDDATE.GetString(),
                                                            this.CBH01_MDSABUN.GetValue()
                                                            );
            }

            if (CBO01_MDINKIYN.GetValue().ToString() == "Y")
            {
                //정산마지막일자에 하루 더하기
                DateTime dDate = Convert.ToDateTime(DTP01_MDACCEDATE.GetString().ToString().Substring(0, 4) + "-" + DTP01_MDACCEDATE.GetString().ToString().Substring(4, 2) + "-" + DTP01_MDACCEDATE.GetString().ToString().Substring(6,2)).AddDays(1);  

                string sDate = dDate.Year.ToString() + Set_Fill2(dDate.Month.ToString()) + Set_Fill2(dDate.Day.ToString());

                this.DbConnector.Attach("TY_P_HR_73MB3075", sDate,
                                                            TYUserInfo.EmpNo,
                                                            "2",
                                                            this.CBH01_MDSABUN.GetValue()
                                                           );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_DataBinding();
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
           
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }

        #endregion

        #region  Description : CBH01_MDSABUN_CodeBoxDataBinded 이벤트
        private void CBH01_MDSABUN_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_MDSABUN.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.CBH01_MDSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    CBH01_MDBUSEO.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
                    CBH01_MDJKCD.SetValue(dt.Rows[0]["KBJKCD"].ToString());
                }
            }
        }
        #endregion

        #region  Description :  DTP01_MDDATE_ValueChanged 이벤트
        private void DTP01_MDDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_MDBUSEO.DummyValue = this.DTP01_MDDATE.GetString();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

       
      

    }
}
