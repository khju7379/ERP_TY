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
    /// 외부식수관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2016.02.19 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_62MBD551 : 외부업체 식수관리 등록
    ///  TY_P_HR_62MBF552 : 외부업체 식수관리 수정
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
    ///  FDDEPT : 제공부서
    ///  FDSABUN : 제공사번
    ///  FDVNCODE : 거래처코드
    ///  FDGUBUN : 식사구분
    ///  FDCNT : 순　서
    ///  FDCOUNT : 식수인원
    ///  FDDATE : 일　자
    ///  FDGATEGN : FDGATEGN
    ///  FDGEORE : 업체명
    ///  FDSAYU : 사　유
    ///  FDSDGUBN : 식대지급
    /// </summary>
    public partial class TYHRCS005I : TYBase
    {
        private string fsFDDATE = string.Empty;
        private string fsFDGUBUN = string.Empty;
        private string fsFDCNT = string.Empty;
        private string fsFDGATEGN = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRCS005I(string sFDDATE, string sFDGUBUN, string sFDCNT, string sFDGATEGN)
        {
            InitializeComponent();

            fsFDDATE = sFDDATE;
            fsFDGUBUN = sFDGUBUN;
            fsFDCNT = sFDCNT;
            fsFDGATEGN = sFDGATEGN;
        }

        private void TYHRCS005I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_FDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_FDDEPT.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            
            Initialize_Controls("01");

            if (string.IsNullOrEmpty(this.fsFDDATE))
            {
                this.TXT01_FDCNT.SetReadOnly(true);

                this.DTP01_FDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                CBO01_FDGUBUN.SetValue("01");
                CBO01_FDGATEGN.SetValue("1");

                this.SetStartingFocus(this.DTP01_FDDATE);
            }
            else
            {
                this.UP_DataBinding();
                this.UP_FieldLock();
                this.SetStartingFocus(this.CBH01_FDVNCODE.CodeText);
            }

        }
        #endregion

        #region Description : 필드 LOCK 이벤트
        private void UP_FieldLock()
        {
            this.DTP01_FDDATE.SetReadOnly(true);
            this.CBO01_FDGUBUN.SetReadOnly(true);
            this.CBO01_FDGATEGN.SetReadOnly(true);
            this.TXT01_FDCNT.SetReadOnly(true);
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
             this.DbConnector.CommandClear();
             this.DbConnector.Attach("TY_P_HR_62MDD554", fsFDDATE, fsFDGUBUN, fsFDCNT, fsFDGATEGN);
             DataTable dt = this.DbConnector.ExecuteDataTable();

             if (dt.Rows.Count > 0)
             {
                this.CBH01_FDDEPT.DummyValue = dt.Rows[0]["FDDATE"].ToString();
                this.DTP01_FDDATE.SetValue(dt.Rows[0]["FDDATE"].ToString());
                CBO01_FDGUBUN.SetValue(dt.Rows[0]["FDGUBUN"].ToString());
                TXT01_FDCNT.SetValue(dt.Rows[0]["FDCNT"].ToString());
                CBO01_FDGATEGN.SetValue(dt.Rows[0]["FDGATEGN"].ToString());
                this.CBH01_FDVNCODE.SetValue(dt.Rows[0]["FDVNCODE"].ToString());
                this.TXT01_FDGEORE.SetValue(dt.Rows[0]["FDGEORE"].ToString());
                this.CBH01_FDDEPT.SetValue(dt.Rows[0]["FDDEPT"].ToString());
                this.CBH01_FDSABUN.SetValue(dt.Rows[0]["FDSABUN"].ToString());
                this.TXT01_FDCOUNT.SetValue(dt.Rows[0]["FDCOUNT"].ToString());
                this.CBO01_FDSDGUBN.SetValue(dt.Rows[0]["FDSDGUBN"].ToString());
                this.TXT01_FDSAYU.SetValue(dt.Rows[0]["FDSAYU"].ToString());
             }           

        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsFDDATE))
            {
                this.DbConnector.Attach("TY_P_HR_62MBD551", this.DTP01_FDDATE.GetString(), 
                                                            CBO01_FDGUBUN.GetValue(), 
                                                            TXT01_FDCNT.GetValue(), 
                                                            CBO01_FDGATEGN.GetValue(),
                                                            this.CBH01_FDVNCODE.GetValue(),
                                                            this.TXT01_FDGEORE.GetValue(),
                                                            this.CBH01_FDDEPT.GetValue(),
                                                            this.CBH01_FDSABUN.GetValue(),
                                                            this.TXT01_FDCOUNT.GetValue(),
                                                            this.CBO01_FDSDGUBN.GetValue(),
                                                            this.TXT01_FDSAYU.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_62MBF552",       this.CBH01_FDVNCODE.GetValue(),
                                                                  this.TXT01_FDGEORE.GetValue(),
                                                                  this.CBH01_FDDEPT.GetValue(),
                                                                  this.CBH01_FDSABUN.GetValue(),
                                                                  this.TXT01_FDCOUNT.GetValue(),
                                                                  this.CBO01_FDSDGUBN.GetValue(),
                                                                  this.TXT01_FDSAYU.GetValue(),
                                                                  TYUserInfo.EmpNo,
                                                                  this.DTP01_FDDATE.GetString(),
                                                                  CBO01_FDGUBUN.GetValue(),
                                                                  TXT01_FDCNT.GetValue(),
                                                                  CBO01_FDGATEGN.GetValue()
                                                                  );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //신규일경우 순번생성
            if (string.IsNullOrEmpty(this.fsFDDATE))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_62MDT557", this.DTP01_FDDATE.GetString(), CBO01_FDGUBUN.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.TXT01_FDCNT.SetValue(Set_Fill3(dt.Rows[0][0].ToString()));
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", CBH01_FDSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                this.CBH01_FDDEPT.SetValue(dk.Rows[0]["KBBSTEAM"].ToString());
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

        #region  Description : CBH01_FDSABUN_Leave 이벤트
        private void CBH01_FDSABUN_Leave(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", CBH01_FDSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CBH01_FDDEPT.SetValue(dt.Rows[0]["KBBSTEAM"].ToString());
            }
        }
        #endregion



    }
}
