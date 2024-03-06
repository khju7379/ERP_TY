using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인사기록부 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.09.25 09:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_HR_4BCH4388 : 발령사항 조회(사번별)
    ///  TY_P_HR_4BD8V393 : 학력사항 조회(사번별)
    ///  TY_P_HR_4BD9J398 : 경력사항 조회(사번별)
    ///  TY_P_HR_4BD9R402 : 자격면허 조회(사번별)
    ///  TY_P_HR_4BDAR406 : 가족사항 조회(사번별)
    ///  TY_P_HR_4BDB1409 : 포상사항 조회(사번별)
    ///  TY_P_HR_4BDB6410 : 징계사항 조회(사번별)
    ///  TY_P_HR_4BDBM414 : 병력사항 조회(사번별)
    ///  TY_P_HR_4BPIW524 : 교육사항 조회(사번별)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_59P9H901 : 가족사항 조회
    ///  TY_S_HR_59P9I902 : 학력사항 조회
    ///  TY_S_HR_59P9J903 : 경력사항 조회
    ///  TY_S_HR_59P9J904 : 교육사항 조회
    ///  TY_S_HR_59P9K906 : 포상사항 조회
    ///  TY_S_HR_59P9K907 : 징계사항 조회
    ///  TY_S_HR_59P9K908 : 발령사항 조회
    ///  TY_S_HR_59P9M909 : 발령사항 조회
    ///  TY_S_HR_59P9N910 : 병력사항 조회
    ///  TY_S_HR_59P9N911 : 자격면허 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  KBJCCD : 직책
    ///  KBJJCD : 직위
    ///  KBSABUN : 사번
    ///  KBUNION : 노조유무
    ///  KBBDATE : 발령일자
    ///  KBIDATE : 입사일자
    ///  KBBONJK : 본 적
    ///  KBBSTEAM : 부서(반)
    ///  KBBUSEO : 부서
    ///  KBHANGL : 한글이름
    ///  KBHANJA : 한자이름
    ///  KBJUMIN : 주민번호
    ///  KBMOBILE : 핸드폰
    /// </summary>
    public partial class TYHRKB016P : TYBase
    {
        private string fsKBSABUN;
        private string fsAge;

        #region  Description : 폼 로드 이벤트
        public TYHRKB016P(string KBSABUN)
        {
            InitializeComponent();

            this.fsKBSABUN = KBSABUN;
        }

        private void TYHRKB016P_Load(object sender, System.EventArgs e)
        {
            this.UP_DataBinding(fsKBSABUN);
        }
        #endregion

        #region  Description : DataBind 이벤트
        private void UP_DataBinding(string sKBSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.fsKBSABUN, TYUserInfo.SecureKey, "Y");
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CBH01_KBBUSEO.DummyValue = ds.Tables[0].Rows[0]["KBBDATE"].ToString();
                this.CBH01_KBBSTEAM.DummyValue = ds.Tables[0].Rows[0]["KBBDATE"].ToString();


                this.TXT01_KBHANGL.SetValue(ds.Tables[0].Rows[0]["KBHANGL"].ToString());
                this.TXT01_KBSABUN.SetValue(ds.Tables[0].Rows[0]["KBSABUN"].ToString());
                this.TXT01_KBHANJA.SetValue(ds.Tables[0].Rows[0]["KBHANJA"].ToString());
                this.MTB01_KBJUMIN.SetValue(ds.Tables[0].Rows[0]["KBJUMIN"].ToString());
                this.CBH01_KBBUSEO.SetValue(ds.Tables[0].Rows[0]["KBBUSEO"].ToString());
                this.CBH01_KBBSTEAM.SetValue(ds.Tables[0].Rows[0]["KBBSTEAM"].ToString());
                this.CBH01_KBJJCD.SetValue(ds.Tables[0].Rows[0]["KBJJCD"].ToString());
                this.CBH01_KBJCCD.SetValue(ds.Tables[0].Rows[0]["KBJCCD"].ToString());
                this.DTP01_KBIDATE.SetValue(ds.Tables[0].Rows[0]["KBIDATE"].ToString());
                this.DTP01_KBBDATE.SetValue(ds.Tables[0].Rows[0]["KBJKBDATE"].ToString());
                this.DTP01_KBEDATE.SetValue(ds.Tables[0].Rows[0]["KBREBDATE"].ToString());
                this.CBO01_KBUNION.SetValue(ds.Tables[0].Rows[0]["KBUNION"].ToString());
                this.TXT01_KBMOBILE.SetValue(ds.Tables[0].Rows[0]["KBMOBILE"].ToString());

                this.TXT01_KBBONJK.SetValue(ds.Tables[0].Rows[0]["KBBONJK"].ToString());
                this.TXT01_KBJUSO.SetValue(ds.Tables[0].Rows[0]["KBJUSO"].ToString());

                this.TXT01_PAYTOTAL.SetValue(ds.Tables[0].Rows[0]["LASTYEARPYTOTAL"].ToString());

                fsAge = this.UP_GetAge(ds.Tables[0].Rows[0]["KBJUMIN"].ToString()).ToString();
                this.lblAge.Text = "(당 " + fsAge + "세 )";
                
                //사진
                UP_Get_ImgScreen(this.fsKBSABUN);

                //가족사항
                this.FPS91_TY_S_HR_59P9H901.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDAR406", TYUserInfo.SecureKey, TYUserInfo.PerAuth, this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9H901.SetValue(this.DbConnector.ExecuteDataTable());

                //학력사항
                this.FPS91_TY_S_HR_59P9I902.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD8V393", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9I902.SetValue(this.DbConnector.ExecuteDataTable());

                //병역사항
                this.FPS91_TY_S_HR_59P9N910.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDBM414", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9N910.SetValue(this.DbConnector.ExecuteDataTable());
                
                //경력사항
                this.FPS91_TY_S_HR_59P9J903.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD9J398", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9J903.SetValue(this.DbConnector.ExecuteDataTable());

                //자격사항
                this.FPS91_TY_S_HR_59P9N911.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD9R402", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9N911.SetValue(this.DbConnector.ExecuteDataTable());
                
                //포상
                this.FPS91_TY_S_HR_59P9K906.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDB1409", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9K906.SetValue(this.DbConnector.ExecuteDataTable());

                //징계
                this.FPS91_TY_S_HR_59P9K907.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDB6410", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9K907.SetValue(this.DbConnector.ExecuteDataTable());

                //교육사항
                this.FPS91_TY_S_HR_59P9J904.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BPIW524", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9J904.SetValue(this.DbConnector.ExecuteDataTable());

                //발령사항
                this.FPS91_TY_S_HR_59P9K908.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_59UBJ928", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9K908.SetValue(this.DbConnector.ExecuteDataTable());

                

                //승진사항
                this.FPS91_TY_S_HR_59P9M909.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_59UBJ927", this.fsKBSABUN);
                this.FPS91_TY_S_HR_59P9M909.SetValue(this.DbConnector.ExecuteDataTable());

            }
        }
        #endregion

        #region Description : 사진 조회 함수
        private void UP_Get_ImgScreen(string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFV434", sSABUN, "1", "");
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                FileStream stream = null;
                byte[] _AttachFile = null;

                try
                {
                    string fileName = "c:\\" + ds.Tables[0].Rows[0]["AFFILENAME"].ToString();

                    _AttachFile = ds.Tables[0].Rows[0]["AFFILEBYTE"] as byte[];

                    int ArraySize = _AttachFile.GetUpperBound(0);

                    PBX01_IMG.SetValue(_AttachFile);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }
            else
            {
                this.PBX01_IMG.Image = TY.ER.HR00.Properties.Resources.nophoto;
            }
        }
        #endregion

        #region  Description : 나이 구하는 함수 이벤트
        private int UP_GetAge(string sJumin)
        {
            int iYear;

            if (sJumin.Substring(6, 1) == "1" || sJumin.Substring(6, 1) == "2")
            {
                iYear = Convert.ToInt16("19"+sJumin.Substring(0, 2));
            }
            else
            {
                iYear = Convert.ToInt16("20" + sJumin.Substring(0, 2));
            }

            DateTime _toDay = DateTime.Today;
            return (_toDay.Year - iYear) + 1;
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.fsKBSABUN, TYUserInfo.SecureKey, "Y");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //가족사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDAR406", TYUserInfo.SecureKey, TYUserInfo.PerAuth, this.fsKBSABUN);
                DataTable dtGJ = this.DbConnector.ExecuteDataTable();

                //학력사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD8V393", this.fsKBSABUN);
                DataTable dtHL = this.DbConnector.ExecuteDataTable();

                //병역사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDBM414", this.fsKBSABUN);
                DataTable dtGB = this.DbConnector.ExecuteDataTable();

                //경력사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD9J398", this.fsKBSABUN);
                DataTable dtKL = this.DbConnector.ExecuteDataTable();

                //자격사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BD9R402", this.fsKBSABUN);
                DataTable dtJK = this.DbConnector.ExecuteDataTable();

                //포상
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDB1409", this.fsKBSABUN);
                DataTable dtPR = this.DbConnector.ExecuteDataTable();

                //징계
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BDB6410", this.fsKBSABUN);
                DataTable dtSB = this.DbConnector.ExecuteDataTable();

                //교육사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BPIW524", this.fsKBSABUN);
                DataTable dtGY = this.DbConnector.ExecuteDataTable();

                //발령사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_A94AJ970", this.fsKBSABUN);
                DataTable dtBL = this.DbConnector.ExecuteDataTable();

                //승진사항
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_59UBJ927", this.fsKBSABUN);
                DataTable dtSJ = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BHFV434", this.fsKBSABUN, "1", "");
                DataTable dtIMG = this.DbConnector.ExecuteDataTable();

                byte[] bIMG = null;

                if (dtIMG != null && dtIMG.Rows.Count > 0)
                {
                    bIMG = dtIMG.Rows[0]["AFFILEBYTE"] as byte[];
                }

                ActiveReport rpt = new TYHRKB016R(dtGJ, dtHL, dtGB, dtKL, dtJK, dtPR, dtSB, dtGY, dtBL, dtSJ, fsAge, bIMG);

                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
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
