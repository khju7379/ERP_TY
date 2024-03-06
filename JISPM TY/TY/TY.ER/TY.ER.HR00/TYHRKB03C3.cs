using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Windows.Forms;

namespace TY.ER.HR00
{
    /// <summary>
    /// 임원배수관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2021.11.09 11:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_BB9BG718 : 임원퇴직배수관리 수정
    ///  TY_P_HR_BB9BG719 : 임원퇴직배수관리 등록
    ///  TY_P_HR_BB9BG720 : 임원퇴직배수관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  KXJKCD : 직　　급
    ///  KXSABUN : 사　　번
    ///  KXEDATE : 퇴직금계산종료일자
    ///  KXSDATE : 퇴직금계산시작일자
    ///  KXRATENUM : 배수
    /// </summary>
    public partial class TYHRKB03C3 : TYBase
    {
        private string fsKXSABUN = string.Empty;
        private string fsKXSEQ = string.Empty;

       

        #region Descripgion : 페이지 로드
        public TYHRKB03C3(string KXSABUN, string KXSEQ)
        {
          
            fsKXSABUN = KXSABUN;
            fsKXSEQ = KXSEQ;

            InitializeComponent();
          
        }

        private void TYHRKB03C3_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KXSABUN.SetValue(fsKXSABUN);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.SetStartingFocus(DTP01_KXSDATE);

            UP_Run();
        }
        #endregion

        #region Description : UP_Run 이벤트
        private void UP_Run()
        {
            if (fsKXSEQ != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BB9BG720", fsKXSABUN, fsKXSEQ);
                DataSet ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.CBH01_KXSABUN.SetValue(ds.Tables[0].Rows[0]["KXSABUN"].ToString());
                    this.TXT01_KXSEQ.SetValue(ds.Tables[0].Rows[0]["KXSEQ"].ToString());
                    this.DTP01_KXSDATE.SetValue(ds.Tables[0].Rows[0]["KXSDATE"].ToString());
                    this.DTP01_KXEDATE.SetValue(ds.Tables[0].Rows[0]["KXEDATE"].ToString());
                    this.TXT01_KXRATENUM.SetValue(ds.Tables[0].Rows[0]["KXRATENUM"].ToString());
                    this.CBH01_KXJKCD.SetValue(ds.Tables[0].Rows[0]["KXJKCD"].ToString());
                }
            }
            else
            {
                this.UP_FileClear();

                this.UP_Save_Seq();                
            }
        }
        #endregion

        #region Description : UP_FileClear 이벤트
        private void UP_FileClear()
        {
            this.CBH01_KXSABUN.SetValue(this.fsKXSABUN);
            this.TXT01_KXSEQ.SetValue("");

            this.DTP01_KXSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_KXEDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_KXRATENUM.SetValue("0");
            this.CBH01_KXJKCD.SetValue("");
         
        }
        #endregion        

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //현재일자 
            string sNowDate = DateTime.Now.ToString("yyyyMMdd");

            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsKXSEQ))       //저장
            {   
                this.DbConnector.Attach("TY_P_HR_BB9BG719", 
                                        this.CBH01_KXSABUN.GetValue(),
                                        this.TXT01_KXSEQ.GetValue(),
                                        this.DTP01_KXSDATE.GetString().ToString(),                                        
                                        sNowDate == this.DTP01_KXEDATE.GetString().ToString() ? "" : this.DTP01_KXEDATE.GetString().ToString(),
                                        Get_Numeric(this.TXT01_KXRATENUM.GetValue().ToString()),
                                        this.CBH01_KXJKCD.GetValue(),
                                        TYUserInfo.EmpNo
                    );
            }
            else                                    //수정
            {
                this.DbConnector.Attach("TY_P_HR_BB9BG718",
                                        this.DTP01_KXSDATE.GetString().ToString(),
                                        sNowDate == this.DTP01_KXEDATE.GetString().ToString() ? "" : this.DTP01_KXEDATE.GetString().ToString(),
                                        Get_Numeric(this.TXT01_KXRATENUM.GetValue().ToString()),
                                        this.CBH01_KXJKCD.GetValue(),
                                        TYUserInfo.EmpNo,
                                        this.CBH01_KXSABUN.GetValue(),
                                        this.TXT01_KXSEQ.GetValue()
                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion     

        #region Description : BTN61_SAV_ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToDouble(Get_Numeric(TXT01_KXRATENUM.GetValue().ToString())) <= 0)
            {
                this.ShowCustomMessage("적용배수를 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(TXT01_KXRATENUM);
                e.Successed = false;
                return; 
            }

            if (Convert.ToInt32(DTP01_KXSDATE.GetString().ToString().Substring(0,8)) > Convert.ToInt32(DTP01_KXEDATE.GetString().ToString().Substring(0,8)) )
            {
                this.ShowCustomMessage("시작일자를 종료일자보다 작아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(DTP01_KXSDATE);
                e.Successed = false;
                return; 
            }

            if (string.IsNullOrEmpty(fsKXSEQ))       //신규일 경우
            {
                this.UP_Save_Seq();
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : UP_Save_Seq(순번생성) 이벤트
        private void UP_Save_Seq()
        {

            // 순번 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BB9DB723",
                this.CBH01_KXSABUN.GetValue().ToString()                
                );

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_KXSEQ.SetValue(Set_Fill3(iCnt.ToString()));
        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
