using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 인사기본사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.19 17:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51GAN168 : 용역직 인사기본사항 조회(팝업)
    ///  TY_P_HR_51JHW190 : 용역직 인사기본사항 등록
    ///  TY_P_HR_51KDN197 : 용역직 인사기본사항 수정
    ///  TY_P_HR_51KDO198 : 용역직 인사기본사항 순번 가져오기
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
    ///  KYJJCD : 직종
    ///  KBSEXGB : 성별
    ///  KYBIRGB : 음력구분
    ///  KBGDATE : 그룹입사일
    ///  KBIDATE : 입사일자
    ///  KBBIRTH : 생년월일
    ///  KBHANGL : 한글이름
    ///  KBHANJA : 한자이름
    ///  KBJUMIN : 주민번호
    ///  KBRFID : RF카드번호
    ///  KBTELNO : 전화번호
    ///  KBUPCD : 우편번호
    ///  KYSEQ : 순번
    ///  KYYEAR : 년도
    ///  VNJUSO : 주소
    /// </summary>
    public partial class TYHRGB003I : TYBase
    {
        public DataSet ds = new DataSet();
        string fsBMDATE = string.Empty;
        string fsBMSEQ  = string.Empty;

        #region Description : 폼 로드
        public TYHRGB003I(string sBMDATE, string sBMSEQ)
        {
            InitializeComponent();

            // RF-CARD
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_85ADZ980, "BLRFID", "RFNAME", "BLRFID");

            fsBMDATE = sBMDATE;
            fsBMSEQ = sBMSEQ;
        }

        private void TYHRGB003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 확인
            UP_Run(fsBMDATE, fsBMSEQ);

            if (string.IsNullOrEmpty(this.fsBMDATE) && string.IsNullOrEmpty(this.fsBMSEQ))
            {
                this.DTP01_BMDATE.SetReadOnly(false);
                this.TXT01_BMSEQ.SetReadOnly(false);

                SetStartingFocus(this.DTP01_BMDATE);
            }
            else
            {
                this.DTP01_BMDATE.SetReadOnly(true);
                this.TXT01_BMSEQ.SetReadOnly(true);

                SetStartingFocus(this.CBO01_BMGUBUN);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsBMDATE) && string.IsNullOrEmpty(this.fsBMSEQ))
            {
                string sBMSEQ = string.Empty;

                this.TXT01_BMSEQ.SetValue(UP_getSEQ());
                this.DbConnector.Attach("TY_P_HR_85AFI992",
                                        Get_Date(this.DTP01_BMDATE.GetValue().ToString()),
                                        this.TXT01_BMSEQ.GetValue().ToString(),
                                        this.CBO01_BMGUBUN.GetValue().ToString(),
                                        this.TXT01_BMSSKNM.GetValue().ToString(),
                                        this.TXT01_BMMJNM1.GetValue().ToString(),
                                        this.TXT01_BMCARNO.GetValue().ToString(),
                                        this.TXT01_BMTEL.GetValue().ToString(),
                                        this.CBH01_BMSABUN.GetValue().ToString(),
                                        this.CBH01_BMSABUN.GetText().ToString(),
                                        this.CKB01_BMSIKSAGN1.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN2.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN3.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN4.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN5.GetValue().ToString(),
                                        this.CBO01_BMSIKDEGN.GetValue().ToString(),
                                        this.CBH01_BMVISABUN.GetValue().ToString(),
                                        this.CBH01_BMVISABUN.GetText().ToString()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_85AFN995",
                                        this.CBO01_BMGUBUN.GetValue().ToString(),
                                        this.TXT01_BMSSKNM.GetValue().ToString(),
                                        this.TXT01_BMMJNM1.GetValue().ToString(),
                                        this.TXT01_BMCARNO.GetValue().ToString(),
                                        this.TXT01_BMTEL.GetValue().ToString(),
                                        this.CBH01_BMSABUN.GetValue().ToString(),
                                        this.CBH01_BMSABUN.GetText().ToString(),
                                        this.CKB01_BMSIKSAGN1.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN2.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN3.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN4.GetValue().ToString(),
                                        this.CKB01_BMSIKSAGN5.GetValue().ToString(),
                                        this.CBO01_BMSIKDEGN.GetValue().ToString(),
                                        this.CBH01_BMVISABUN.GetValue().ToString(),
                                        this.CBH01_BMVISABUN.GetText().ToString(),
                                        Get_Date(this.DTP01_BMDATE.GetValue().ToString()),
                                        this.TXT01_BMSEQ.GetValue().ToString()
                                        );
            }
            this.DbConnector.ExecuteTranQuery();


            #region Description : 디테일 등록 및 수정

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85AG9996", Get_Date(this.DTP01_BMDATE.GetValue().ToString()),
                                                                this.TXT01_BMSEQ.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["BLNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["BLJUMIN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BLJUSO"].ToString(),
                                                                ds.Tables[0].Rows[i]["BLRFID"].ToString(),
                                                                ds.Tables[0].Rows[i]["BLTEL"].ToString(),
                                                                DateTime.Now.ToString("yyyyMMdd"),
                                                                DateTime.Now.ToString("HHmmss").ToString().Substring(0,4).ToString(),
                                                                "0",
                                                                "0"
                                                                ); // 저장
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85AG0997", ds.Tables[1].Rows[i]["BLNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["BLJUMIN"].ToString(),
                                                                ds.Tables[1].Rows[i]["BLJUSO"].ToString(),
                                                                ds.Tables[1].Rows[i]["BLRFID"].ToString(),
                                                                ds.Tables[1].Rows[i]["BLTEL"].ToString(),
                                                                Get_Date(this.DTP01_BMDATE.GetValue().ToString()),
                                                                this.TXT01_BMSEQ.GetValue().ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            #endregion


            this.ShowMessage("TY_M_GB_23NAD873");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_85ADZ980.GetDataSourceInclude(TSpread.TActionType.New, "BLNAME", "BLJUMIN", "BLRFID", "RFNAME", "BLTEL", "BLIPTIME", "BLTATIME", "BLJUSO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_85ADZ980.GetDataSourceInclude(TSpread.TActionType.Update, "BLNAME", "BLJUMIN", "BLRFID", "RFNAME", "BLTEL", "BLIPTIME", "BLTATIME", "BLJUSO"));

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion
        #region Description : 데이터 확인
        private void UP_Run(string sBMDATE, string sBMSEQ)
        {
            this.FPS91_TY_S_HR_85ADZ980.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85ADA979", Get_Date(sBMDATE), sBMSEQ);
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85AGJ002", Get_Date(sBMDATE), sBMSEQ);
            
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_85ADZ980.SetValue(dt);
        }
        #endregion

        #region Description : 순번 가져오기
        private string UP_getSEQ()
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85AFK993", Get_Date(this.DTP01_BMDATE.GetValue().ToString()));

            sSEQ = this.DbConnector.ExecuteScalar().ToString();

            return sSEQ;
        }
        #endregion

        #region Description : 작업인원 버튼
        private void BTN61_CIINWON_Click(object sender, EventArgs e)
        {
            TYHRGB01C1 popup = new TYHRGB01C1(this.DTP01_BMDATE.GetValue().ToString(), this.TXT01_BMSEQ.GetValue().ToString(), "방문", "");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.ds.Tables[0].Rows.Count > 0)
                {
                    int iRowIndex = 0;

                    iRowIndex = this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Rows.Count;

                    for (int i = 0; i < popup.ds.Tables[0].Rows.Count; i++)
                    {
                        if (popup.ds.Tables[0].Rows[i]["GUBUN"].ToString() == "ADD")
                        {
                            iRowIndex = iRowIndex + 1;

                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.AddRows(iRowIndex - 1, 1);
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";


                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 0].Text = popup.ds.Tables[0].Rows[i]["CLNAME"].ToString();
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 1].Text = popup.ds.Tables[0].Rows[i]["CLJUMIN"].ToString().Substring(0,7);
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 2].Text = "";
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 3].Text = "";
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 4].Text = popup.ds.Tables[0].Rows[i]["CLTEL"].ToString();
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 5].Text = "";
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 6].Text = "";
                            this.FPS91_TY_S_HR_85ADZ980.ActiveSheet.Cells[iRowIndex - 1, 7].Text = popup.ds.Tables[0].Rows[i]["CLJUSO"].ToString();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
