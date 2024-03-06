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
    /// 학자금 기본사항관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.13 11:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73DAQ899 : 학자금기본사항관리 확인
    ///  TY_P_HR_73DAQ900 : 학자금기본사항관리 등록
    ///  TY_P_HR_73DBV906 : 학자금기본사항관리 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HKHAKGA : 학과
    ///  HKHAKKYO : 학교
    ///  HKHLGUBN : 학력구분
    ///  HKJUGUBN : 재학상태
    ///  HKSABUN : 사번
    ///  HKIDATE : 입학일자
    ///  HKJDATE : 졸업일자
    ///  HKCHDJUMIN : 주민번호
    ///  HKCHDNAME : 자녀이름
    ///  HKCHDSEX : 성별
    ///  HKHAKGI : 학기
    ///  HKHAKGITOTAL : 지원총학기
    ///  HKHAKYEAR : 학년
    ///  HKINGHAKGI : 지원된학기
    ///  HKSSEQ : 순번
    ///  HKYEAR : 년도
    /// </summary>
    public partial class TYHRKB019I : TYBase
    {
        private string fsHKSABUN;
        private string fsHKYEAR;
        private string fsHKSSEQ;


        #region  Description : 폼 로드 이벤트
        public TYHRKB019I(string sHKSABUN, string sHKYEAR, string sHKSSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsHKSABUN = sHKSABUN;
            fsHKYEAR = sHKYEAR;
            fsHKSSEQ = sHKSSEQ;
        }

        private void TYHRKB019I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQOPTION.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_INQOPTION.ProcessCheck += new TButton.CheckHandler(BTN61_INQOPTION_ProcessCheck);
            
            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            CBO01_HKCHDSEX.SetReadOnly(true);

            DTP01_HKIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            DTP01_HKJDATE.SetValue("");

            if (string.IsNullOrEmpty(this.fsHKSABUN))
            {
                TXT01_HKYEAR.SetValue(DateTime.Now.ToString("yyyy"));
                this.SetStartingFocus(CBH01_HKSABUN.CodeText);

                //순번 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_73EDP916", TXT01_HKYEAR.GetValue());
                Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iSeq > 0)
                {
                    this.TXT01_HKSSEQ.SetValue(Set_Fill3(iSeq.ToString()));
                }
            }
            else
            {               
                                
                UP_Run();
            }
        }
        #endregion

        #region  Description : 확인 이벤트
        private void UP_Run()
        {
            this.BTN61_INQOPTION.Visible = false;

            CBH01_HKSABUN.SetValue(fsHKSABUN);
            TXT01_HKYEAR.SetValue(fsHKYEAR);
            TXT01_HKSSEQ.SetValue(fsHKSSEQ);

            CBH01_HKSABUN.SetReadOnly(true);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73DAQ899", TYUserInfo.SecureKey, TYUserInfo.PerAuth, this.CBH01_HKSABUN.GetValue().ToString(), TXT01_HKYEAR.GetValue(), TXT01_HKSSEQ.GetValue());
            DataTable dt =  this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CBH01_KBBUSEO.DummyValue = dt.Rows[0]["HKYEAR"].ToString() + "0101";

                this.CurrentDataTableRowMapping(dt, "01"); 
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsHKSABUN))
            {
                this.DbConnector.Attach("TY_P_HR_73DAQ900", this.CBH01_HKSABUN.GetValue().ToString(), 
                                                            TXT01_HKYEAR.GetValue(), 
                                                            TXT01_HKSSEQ.GetValue(),
                                                            TXT01_HKCHDNAME.GetValue(),
                                                            CBO01_HKCHDSEX.GetValue(),
                                                            MTB01_HKCHDJUMIN.GetValue().ToString().Replace("-",""),
                                                            TYUserInfo.SecureKey,
                                                            CBH01_HKHLGUBN.GetValue(),
                                                            CBH01_HKHAKKYO.GetValue(),
                                                            CBH01_HKHAKGA.GetValue(),
                                                            Get_Numeric(TXT01_HKHAKYEAR.GetValue().ToString()),
                                                            Get_Numeric(TXT01_HKHAKGI.GetValue().ToString()),        
                                                            Get_Numeric(TXT01_HKINGHAKGI.GetValue().ToString()),
                                                            Get_Numeric(TXT01_HKHAKGITOTAL.GetValue().ToString()),
                                                            DTP01_HKIDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            DTP01_HKJDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            CBH01_HKJUGUBN.GetValue(),
                                                            TYUserInfo.EmpNo );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_73DBV906", CBH01_HKHLGUBN.GetValue(),
                                                            CBH01_HKHAKKYO.GetValue(),
                                                            CBH01_HKHAKGA.GetValue(),
                                                            Get_Numeric(TXT01_HKHAKYEAR.GetValue().ToString()),
                                                            Get_Numeric(TXT01_HKHAKGI.GetValue().ToString()),
                                                            Get_Numeric(TXT01_HKINGHAKGI.GetValue().ToString()),
                                                            Get_Numeric(TXT01_HKHAKGITOTAL.GetValue().ToString()),
                                                            DTP01_HKIDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            DTP01_HKJDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            CBH01_HKJUGUBN.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_HKSABUN.GetValue().ToString(),
                                                            TXT01_HKYEAR.GetValue(),
                                                            TXT01_HKSSEQ.GetValue());
            }
            this.DbConnector.ExecuteTranQuery();

            fsHKSABUN = this.CBH01_HKSABUN.GetValue().ToString();
            fsHKYEAR  = TXT01_HKYEAR.GetValue().ToString();
            fsHKSSEQ  = TXT01_HKSSEQ.GetValue().ToString();

            UP_Run();

            this.ShowMessage("TY_M_GB_23NAD873");
        }        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            ////학자금 지출 내역이 있으면 수정 불가
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_73EDL915", this.CBH01_HKSABUN.GetValue().ToString(), TXT01_HKYEAR.GetValue(), TXT01_HKSSEQ.GetValue());
            //Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            //if (iCnt > 0)
            //{
            //    this.ShowMessage("TY_M_HR_73EDJ914");
            //    e.Successed = false;
            //    return;
            //}           

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }        
        #endregion

        #region  Description : 자녀 선택 팝업 버튼 이벤트
        private void BTN61_INQOPTION_Click(object sender, EventArgs e)
        {
            TYHRKB19C1 popup = new TYHRKB19C1(CBH01_HKSABUN.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_HKCHDNAME.SetValue(popup.fsGJNAME);
                this.MTB01_HKCHDJUMIN.SetValue(popup.fsGJJUMIN);
                this.CBO01_HKCHDSEX.SetValue(popup.fsGJSEXGB);
            }
        }

        private void BTN61_INQOPTION_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBH01_HKSABUN.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_3AB5A047");
                e.Successed = false;
                return;
            }
        }        
        #endregion

        #region  Description : CBH01_HKSABUN_CodeBoxDataBinded 이벤트
        private void CBH01_HKSABUN_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (this.CBH01_HKSABUN.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.CBH01_HKSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    CBH01_KBBUSEO.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
                    CBH01_KBJKCD.SetValue(dt.Rows[0]["KBJKCD"].ToString());
                }
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

      

      

    }
}
