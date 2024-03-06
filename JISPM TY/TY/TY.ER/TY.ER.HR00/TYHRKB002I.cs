using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인사기본사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.12 09:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_HR_4BBGY368 : 인사기본사항 등록
    ///  TY_P_HR_4BBH0369 : 인사기본사항 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BTNIMG : 사진저장
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  KBBALCD : 발령코드
    ///  KBIGUBN : 입사구분
    ///  KBJCCD : 직책
    ///  KBJJCD : 직위
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  KBBIRGB : 음양구분
    ///  KBCODE : 지급구분
    ///  KBEISGN : EIS연동
    ///  KBGUNMU : 근무처
    ///  KBPAYGN : 급여지급유무
    ///  KBSEXGB : 성별
    ///  KBUNION : 노조유무
    ///  KBBDATE : 발령일자
    ///  KBGDATE : 그룹입사일
    ///  KBIDATE : 입사일자
    ///  KBJDATE : 중간정산일자
    ///  KBBIRTH : 생년월일
    ///  KBBONJK : 본 적
    ///  KBBSTEAM : 부서(반)
    ///  KBBUSEO : 부서
    ///  KBCOMFAX : 팩스
    ///  KBCOMTEL : 사내번호
    ///  KBENGNM : 영어이름
    ///  KBHANGL : 한글이름
    ///  KBHANJA : 한자이름
    ///  KBHOBN : 호봉
    ///  KBINTRO : 자기소개
    ///  KBJUMIN : 주민번호
    ///  KBJUSO : 회사주소
    ///  KBMAILID : 메일
    ///  KBMOBILE : 핸드폰
    ///  KBRFID : RF카드번호
    ///  KBSOSOK : 소속
    ///  KBTELNO : 전화번호
    ///  KBUPCD : 우편번호
    /// </summary>
    public partial class TYHRKB002I : TYBase
    {
        private string fsKBSABUN;

        private TYData DAT10_KBCOMPANY;	
        private TYData DAT10_KBSABUN;	        
        private TYData DAT10_KBHANGL;	        
        private TYData DAT10_KBHANJA;	        
        private TYData DAT10_KBENGNM;	        
        private TYData DAT10_KBSEXGB;   	
        private TYData DAT10_KBIDATE;  	
        private TYData DAT10_KBJDATE; 	
        private TYData DAT10_KBGDATE;         
        private TYData DAT10_KBIGUBN; 	
        private TYData DAT10_KBJJCD;  	
        private TYData DAT10_KBJCCD;  	
        private TYData DAT10_KBJKCD;  	
        private TYData DAT10_KBHOBN;  	
        private TYData DAT10_KBSOSOK; 	
        private TYData DAT10_KBBUSEO;	        
        private TYData DAT10_KBBSTEAM;	
        private TYData DAT10_KBBALCD; 	
        private TYData DAT10_KBBDATE; 	
        private TYData DAT10_KBCODE;  	
        private TYData DAT10_KBRFID;  	
        private TYData DAT10_KBGUNMU; 	
        private TYData DAT10_KBBONJK; 	
        private TYData DAT10_KBJUSO;  	
        private TYData DAT10_KBUPCD;  	
        private TYData DAT10_KBJUMIN; 	
        private TYData DAT10_KBUNION; 	
        private TYData DAT10_KBBIRTH; 	
        private TYData DAT10_KBBIRGB; 	
        private TYData DAT10_KBMAILID;	
        private TYData DAT10_KBTELNO; 	
        private TYData DAT10_KBMOBILE;	
        private TYData DAT10_KBCOMTEL;	
        private TYData DAT10_KBCOMFAX;	
        private TYData DAT10_KBINTRO; 	
        private TYData DAT10_KBPAYGN; 	
        private TYData DAT10_KBEISGN; 	
        private TYData DAT10_KBMEDNUM;
        private TYData DAT10_KBLEVEL;
        private TYData DAT10_KBBOHUN;
        private TYData DAT10_KBHISAB;

        private TYData DAT10_KBPYPEAK;
        private TYData DAT10_KBPKSDATE;
        private TYData DAT10_KBREDATE;
        private TYData DAT10_KBPENSGUBN;
        private TYData DAT10_KBPERAUTH;
        private TYData DAT10_KEY;



        #region Description : 폼 로드 이벤트
        public TYHRKB002I(string KBSABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsKBSABUN = KBSABUN;

            this.DAT10_KBCOMPANY = new TYData("DAT10_KBCOMPANY", null);
            this.DAT10_KBSABUN = new TYData("DAT10_KBSABUN", null);	        
            this.DAT10_KBHANGL = new TYData("DAT10_KBHANGL", null);	        
            this.DAT10_KBHANJA = new TYData("DAT10_KBHANJA", null);	        
            this.DAT10_KBENGNM = new TYData("DAT10_KBENGNM", null);	        
            this.DAT10_KBSEXGB = new TYData("DAT10_KBSEXGB", null);   	
            this.DAT10_KBIDATE = new TYData("DAT10_KBIDATE", null); 	
            this.DAT10_KBJDATE = new TYData("DAT10_KBJDATE", null);
            this.DAT10_KBGDATE = new TYData("DAT10_KBGDATE", null);
            this.DAT10_KBIGUBN = new TYData("DAT10_KBIGUBN", null);
            this.DAT10_KBJJCD = new TYData("DAT10_KBJJCD", null);  	
            this.DAT10_KBJCCD = new TYData("DAT10_KBJCCD", null);  	
            this.DAT10_KBJKCD = new TYData("DAT10_KBJKCD", null);  	
            this.DAT10_KBHOBN = new TYData("DAT10_KBHOBN", null);  	
            this.DAT10_KBSOSOK = new TYData("DAT10_KBSOSOK", null); 	
            this.DAT10_KBBUSEO = new TYData("DAT10_KBBUSEO", null);	        
            this.DAT10_KBBSTEAM = new TYData("DAT10_KBBSTEAM", null);	
            this.DAT10_KBBALCD = new TYData("DAT10_KBBALCD", null); 	
            this.DAT10_KBBDATE = new TYData("DAT10_KBBDATE", null); 	
            this.DAT10_KBCODE = new TYData("DAT10_KBCODE", null);  	
            this.DAT10_KBRFID = new TYData("DAT10_KBRFID", null);  	
            this.DAT10_KBGUNMU = new TYData("DAT10_KBGUNMU", null); 	
            this.DAT10_KBBONJK = new TYData("DAT10_KBBONJK", null); 	
            this.DAT10_KBJUSO = new TYData("DAT10_KBJUSO", null);  	
            this.DAT10_KBUPCD = new TYData("DAT10_KBUPCD", null);  	
            this.DAT10_KBJUMIN = new TYData("DAT10_KBJUMIN", null); 	
            this.DAT10_KBUNION = new TYData("DAT10_KBUNION", null); 	
            this.DAT10_KBBIRTH = new TYData("DAT10_KBBIRTH", null); 	
            this.DAT10_KBBIRGB = new TYData("DAT10_KBBIRGB", null); 	
            this.DAT10_KBMAILID = new TYData("DAT10_KBMAILID", null);	
            this.DAT10_KBTELNO = new TYData("DAT10_KBTELNO", null); 	
            this.DAT10_KBMOBILE = new TYData("DAT10_KBMOBILE", null);	
            this.DAT10_KBCOMTEL = new TYData("DAT10_KBCOMTEL", null);	
            this.DAT10_KBCOMFAX = new TYData("DAT10_KBCOMFAX", null);	
            this.DAT10_KBINTRO = new TYData("DAT10_KBINTRO", null); 	
            this.DAT10_KBPAYGN = new TYData("DAT10_KBPAYGN", null); 	
            this.DAT10_KBEISGN = new TYData("DAT10_KBEISGN", null); 	
            this.DAT10_KBMEDNUM = new TYData("DAT10_KBMEDNUM", null);
            this.DAT10_KBLEVEL = new TYData("DAT10_KBLEVEL", null);
            this.DAT10_KBBOHUN = new TYData("DAT10_KBBOHUN", null);        
            this.DAT10_KBHISAB = new TYData("DAT10_KBHISAB", null);
            this.DAT10_KBPYPEAK = new TYData("DAT10_KBPYPEAK", null);
            this.DAT10_KBPKSDATE = new TYData("DAT10_KBPKSDATE", null);
            this.DAT10_KBREDATE = new TYData("DAT10_KBREDATE", null);
            this.DAT10_KBPENSGUBN = new TYData("DAT10_KBPENSGUBN", null);
            this.DAT10_KBPERAUTH = new TYData("DAT10_KBPERAUTH", null);
            this.DAT10_KEY = new TYData("DAT10_KEY", null);

        }

        private void TYHRKB002I_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT10_KBCOMPANY);
            this.ControlFactory.Add(this.DAT10_KBSABUN);
            this.ControlFactory.Add(this.DAT10_KBHANGL);
            this.ControlFactory.Add(this.DAT10_KBHANJA);
            this.ControlFactory.Add(this.DAT10_KBENGNM);
            this.ControlFactory.Add(this.DAT10_KBSEXGB);
            this.ControlFactory.Add(this.DAT10_KBIDATE);
            this.ControlFactory.Add(this.DAT10_KBJDATE);
            this.ControlFactory.Add(this.DAT10_KBGDATE);
            this.ControlFactory.Add(this.DAT10_KBIGUBN);
            this.ControlFactory.Add(this.DAT10_KBJJCD);
            this.ControlFactory.Add(this.DAT10_KBJCCD);
            this.ControlFactory.Add(this.DAT10_KBJKCD);
            this.ControlFactory.Add(this.DAT10_KBHOBN);
            this.ControlFactory.Add(this.DAT10_KBSOSOK);
            this.ControlFactory.Add(this.DAT10_KBBUSEO);
            this.ControlFactory.Add(this.DAT10_KBBSTEAM);
            this.ControlFactory.Add(this.DAT10_KBBALCD);
            this.ControlFactory.Add(this.DAT10_KBBDATE);
            this.ControlFactory.Add(this.DAT10_KBCODE);
            this.ControlFactory.Add(this.DAT10_KBRFID);
            this.ControlFactory.Add(this.DAT10_KBGUNMU);
            this.ControlFactory.Add(this.DAT10_KBBONJK);
            this.ControlFactory.Add(this.DAT10_KBJUSO);
            this.ControlFactory.Add(this.DAT10_KBUPCD);
            this.ControlFactory.Add(this.DAT10_KBJUMIN);
            this.ControlFactory.Add(this.DAT10_KBUNION);
            this.ControlFactory.Add(this.DAT10_KBBIRTH);
            this.ControlFactory.Add(this.DAT10_KBBIRGB);
            this.ControlFactory.Add(this.DAT10_KBMAILID);
            this.ControlFactory.Add(this.DAT10_KBTELNO);
            this.ControlFactory.Add(this.DAT10_KBMOBILE);
            this.ControlFactory.Add(this.DAT10_KBCOMTEL);
            this.ControlFactory.Add(this.DAT10_KBCOMFAX);
            this.ControlFactory.Add(this.DAT10_KBINTRO);
            this.ControlFactory.Add(this.DAT10_KBPAYGN);
            this.ControlFactory.Add(this.DAT10_KBEISGN);
            this.ControlFactory.Add(this.DAT10_KBMEDNUM);
            this.ControlFactory.Add(this.DAT10_KBLEVEL);
            this.ControlFactory.Add(this.DAT10_KBBOHUN);
            this.ControlFactory.Add(this.DAT10_KBHISAB);

            this.ControlFactory.Add(this.DAT10_KBPYPEAK);
            this.ControlFactory.Add(this.DAT10_KBPKSDATE);
            this.ControlFactory.Add(this.DAT10_KBREDATE);
            this.ControlFactory.Add(this.DAT10_KBPENSGUBN);
            this.ControlFactory.Add(this.DAT10_KBPERAUTH);
            this.ControlFactory.Add(this.DAT10_KEY);	


            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);
            this.BTN64_REM.ProcessCheck += new TButton.CheckHandler(BTN64_REM_ProcessCheck);
            this.BTN65_REM.ProcessCheck += new TButton.CheckHandler(BTN65_REM_ProcessCheck);
            this.BTN66_REM.ProcessCheck += new TButton.CheckHandler(BTN66_REM_ProcessCheck);
            this.BTN67_REM.ProcessCheck += new TButton.CheckHandler(BTN67_REM_ProcessCheck);
            this.BTN68_REM.ProcessCheck += new TButton.CheckHandler(BTN68_REM_ProcessCheck);
            this.BTN69_SAV.ProcessCheck += new TButton.CheckHandler(BTN69_SAV_ProcessCheck);
            this.BTN69_REM.ProcessCheck += new TButton.CheckHandler(BTN69_REM_ProcessCheck);
            this.BTN70_REM.ProcessCheck += new TButton.CheckHandler(BTN70_REM_ProcessCheck);
            this.BTN71_SAV.ProcessCheck += new TButton.CheckHandler(BTN71_SAV_ProcessCheck);
            this.BTN71_REM.ProcessCheck += new TButton.CheckHandler(BTN71_REM_ProcessCheck);
            this.BTN72_REM.ProcessCheck += new TButton.CheckHandler(BTN72_REM_ProcessCheck);
            this.BTN73_REM.ProcessCheck += new TButton.CheckHandler(BTN73_REM_ProcessCheck);
            this.BTN74_REM.ProcessCheck += new TButton.CheckHandler(BTN74_REM_ProcessCheck);

            (this.FPS91_TY_S_HR_4BLBZ470.Sheets[0].Columns[8].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.Download;

            this.UP_FieldLock();
                      
            if (string.IsNullOrEmpty(this.fsKBSABUN))
            {
                this.CBO01_KBPAYGN.SetValue("Y");

                this.UP_BtnScreen(true);
                SetStartingFocus(this.TXT01_KBSABUN);
            }
            else
            {
                this.TXT01_KBSABUN.SetReadOnly(true);

                this.UP_Run();
            }

            this.SetStartingFocus(this.TXT01_KBSABUN);
        }
        #endregion

        #region Description : tabControl1_SelectedIndexChanged 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (tabControl1.SelectedIndex == 1) //학력
            {
                UP_Search_학력사항();
            }
            if (tabControl1.SelectedIndex == 2) //경력
            {
                UP_Search_경력사항();
            }
            if (tabControl1.SelectedIndex == 3) //교육
            {
                UP_Search_교육사항();
            }
            if (tabControl1.SelectedIndex == 4) //포상
            {
                UP_Search_포상사항();
            }
            if (tabControl1.SelectedIndex == 5) //징계
            {
                UP_Search_징계사항();
            }
            if (tabControl1.SelectedIndex == 6) //자격면허
            {
                UP_Search_자격면허();
            }
            if (tabControl1.SelectedIndex == 7) //병력사항
            {
                UP_Search_병력사항();
            }
            if (tabControl1.SelectedIndex == 8) //가족사항
            {
                UP_Search_가족사항();
            }
            if (tabControl1.SelectedIndex == 9) //보증사항
            {
                UP_Search_보증사항();
            }
            if (tabControl1.SelectedIndex == 10) //특기사항
            {
                UP_Search_특기사항();
            }
            if (tabControl1.SelectedIndex == 11) // 첨부관리
            {
                UP_Search_첨부관리();
            }
            if (tabControl1.SelectedIndex == 12) // 임원배수관리
            {
                UP_Search_임원배수관리();
            }
        }
        #endregion

        #region Description : UP_BtnScreen 이벤트
        private void UP_BtnScreen(bool bvalue)
        {
            //사진등록
            this.BTN61_BTNIMG.SetReadOnly(bvalue);

            //발령
            this.BTN62_NEW.SetReadOnly(bvalue);
            this.BTN62_REM.SetReadOnly(bvalue);
            //학력
            this.BTN63_NEW.SetReadOnly(bvalue);
            this.BTN63_REM.SetReadOnly(bvalue);
            //경력
            this.BTN64_NEW.SetReadOnly(bvalue);
            this.BTN64_REM.SetReadOnly(bvalue);
            //교육
            //this.BTN65_NEW.SetReadOnly(bvalue);
            //this.BTN65_REM.SetReadOnly(bvalue);
            //포상
            this.BTN66_NEW.SetReadOnly(bvalue);
            this.BTN66_REM.SetReadOnly(bvalue);
            //징계
            this.BTN67_NEW.SetReadOnly(bvalue);
            this.BTN67_REM.SetReadOnly(bvalue);
            //자격
            this.BTN68_NEW.SetReadOnly(bvalue);
            this.BTN68_REM.SetReadOnly(bvalue);
            //병력
            this.BTN69_SAV.SetReadOnly(bvalue);
            this.BTN69_REM.SetReadOnly(bvalue);
            //가족
            this.BTN70_NEW.SetReadOnly(bvalue);
            this.BTN70_REM.SetReadOnly(bvalue);
            //보증
            this.BTN71_SAV.SetReadOnly(bvalue);
            this.BTN71_REM.SetReadOnly(bvalue);
            //특기사항
            //첨부
            this.BTN73_NEW.SetReadOnly(bvalue);
            this.BTN73_REM.SetReadOnly(bvalue);

            //임원배수관리
            this.BTN74_NEW.SetReadOnly(bvalue);
            this.BTN74_REM.SetReadOnly(bvalue);
            
        }
        #endregion

        #region Description : UP_RUN 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.fsKBSABUN, TYUserInfo.SecureKey, TYUserInfo.PerAuth);
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CBH01_KBSOSOK.DummyValue = ds.Tables[0].Rows[0]["KBBDATE"].ToString();
                this.CBH01_KBBUSEO.DummyValue = ds.Tables[0].Rows[0]["KBBDATE"].ToString();
                this.CBH01_KBBSTEAM.DummyValue = ds.Tables[0].Rows[0]["KBBDATE"].ToString();
                
                this.CurrentDataTableRowMapping(ds.Tables[0], "01");

                //사진
                UP_Get_ImgScreen(this.fsKBSABUN);
            }

            this.UP_Search_발령사항();           
        }
        #endregion

        #region Description : 사진 조회 함수
        private void UP_Get_ImgScreen(string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFV434", sSABUN, "1","");
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

        #region Description : 필드 LOCK 이벤트
        private void UP_FieldLock()
        {
            this.CBH01_KBBALCD.SetReadOnly(true);
            this.DTP01_KBBDATE.SetReadOnly(true);
            //this.CBH01_KBJJCD.SetReadOnly(true);
            this.CBH01_KBJCCD.SetReadOnly(true);
            this.CBH01_KBJKCD.SetReadOnly(true);
            this.TXT01_KBHOBN.SetReadOnly(true);
            this.CBH01_KBSOSOK.SetReadOnly(true);
            this.CBH01_KBBUSEO.SetReadOnly(true);
            //this.CBH01_KBBSTEAM.SetReadOnly(true);

            this.DTP01_KBPKSDATE.SetReadOnly(true);
            this.DTP01_KBREDATE.SetReadOnly(true);

            if (TYUserInfo.PerAuth != "Y")
            {
                MTB01_KBJUMIN.SetReadOnly(true);
            }
            
        }
        #endregion

        #region Description : 사진 등록 팝업 이벤트
        private void BTN61_BTNIMG_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB002P(this.TXT01_KBSABUN.GetValue().ToString()
                               )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Run();
        }
        #endregion

        #region Description : 닫기 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsKBSABUN))
            {
                this.DbConnector.Attach("TY_P_HR_4BBGY368", this.ControlFactory, "10");
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BBH0369", this.ControlFactory, "10");
            }
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsKBSABUN))
            {
                //구 인사 등록
                this.DbConnector.Attach("TY_P_HR_5C8FK289", "2",
                                                            this.TXT01_KBSABUN.GetValue(),
                                                            this.TXT01_KBHANGL.GetValue(),
                                                            this.TXT01_KBHANJA.GetValue(),
                                                            this.CBO01_KBSEXGB.GetValue(),
                                                            UP_Get_OldInsaCode("2",this.CBH01_KBJJCD.GetValue().ToString()),
                                                            this.DTP01_KBIDATE.GetString(),
                                                            "",
                                                            this.DTP01_KBGDATE.GetString(),
                                                            UP_Get_OldInsaCode("1",this.CBH01_KBIGUBN.GetValue().ToString()),
                                                            "",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            "0",
                                                            this.TXT01_KBBONJK.GetValue(),
                                                            "",
                                                            this.MTB01_KBUPCD.GetValue().ToString().Replace("-", ""),
                                                            this.MTB01_KBJUMIN.GetValue().ToString().Replace("-", ""),
                                                            this.TXT01_KBTELNO.GetValue().ToString().Replace("-", ""),
                                                            this.CBO01_KBUNION.GetValue(),
                                                            "",
                                                            this.MTB01_KBBIRTH.GetValue().ToString().Replace("-", ""),
                                                            this.CBO01_KBBIRGB.GetValue(),
                                                            this.TXT01_KBMOBILE.GetValue()
                                                            );
            }
            else
            {

                //구 인사 수정
                this.DbConnector.Attach("TY_P_HR_5C8FS290", this.TXT01_KBHANGL.GetValue(),
                                                            this.TXT01_KBHANJA.GetValue(),
                                                            this.CBO01_KBSEXGB.GetValue(),
                                                            UP_Get_OldInsaCode("2", this.CBH01_KBJJCD.GetValue().ToString()),
                                                            this.DTP01_KBIDATE.GetString(),
                                                            this.DTP01_KBJDATE.GetString(),
                                                            this.DTP01_KBGDATE.GetString(),
                                                            UP_Get_OldInsaCode("1",this.CBH01_KBIGUBN.GetValue().ToString()),
                                                            this.TXT01_KBBONJK.GetValue(),
                                                            "",
                                                            this.MTB01_KBUPCD.GetValue().ToString().Replace("-", ""),
                                                            this.MTB01_KBJUMIN.GetValue().ToString().Replace("-", ""),
                                                            this.TXT01_KBTELNO.GetValue().ToString().Replace("-", ""),
                                                            this.CBO01_KBUNION.GetValue(),
                                                            this.MTB01_KBBIRTH.GetValue().ToString().Replace("-", ""),
                                                            this.CBO01_KBBIRGB.GetValue(),
                                                            this.TXT01_KBMOBILE.GetValue(),
                                                             "2",
                                                            this.TXT01_KBSABUN.GetValue()
                                                            );
            }
            this.DbConnector.ExecuteNonQuery();

            //오라클 기본사항 등록
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsKBSABUN))
            {
                // 인사 등록
                this.DbConnector.Attach("TY_P_HR_9B4E1469", "2",
                                                            this.TXT01_KBSABUN.GetValue(),
                                                            this.TXT01_KBHANGL.GetValue(),
                                                            this.TXT01_KBHANJA.GetValue(),
                                                            this.CBO01_KBSEXGB.GetValue(),
                                                            UP_Get_OldInsaCode("2", this.CBH01_KBJJCD.GetValue().ToString()),
                                                            this.DTP01_KBIDATE.GetString(),
                                                            "",
                                                            this.DTP01_KBGDATE.GetString(),
                                                            UP_Get_OldInsaCode("1", this.CBH01_KBIGUBN.GetValue().ToString()),
                                                            "",
                                                            "",
                                                            CBH01_KBJKCD.GetValue().ToString(),
                                                            TXT01_KBHOBN.GetValue().ToString(),
                                                            CBH01_KBBUSEO.GetValue().ToString(),
                                                            CBH01_KBBSTEAM.GetValue().ToString(),
                                                            CBH01_KBSOSOK.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            CBO01_KBGUNMU.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            this.MTB01_KBJUMIN.GetValue().ToString().Replace("-", ""),
                                                            "",
                                                            CBO01_KBUNION.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            TXT01_KBMAILID.GetValue().ToString(),
                                                            "0",
                                                            TXT01_KBMOBILE.GetValue().ToString(),
                                                            TXT01_KBCOMTEL.GetValue().ToString(),
                                                            "",
                                                            TXT01_KBINTRO.GetValue().ToString()                                                          
                                                            );
            }
            else
            {

                // 인사 수정
                this.DbConnector.Attach("TY_P_HR_9B4EG470",  this.TXT01_KBHANGL.GetValue(),
                                                            this.TXT01_KBHANJA.GetValue(),
                                                            this.CBO01_KBSEXGB.GetValue(),
                                                            UP_Get_OldInsaCode("2", this.CBH01_KBJJCD.GetValue().ToString()),
                                                            this.DTP01_KBIDATE.GetString(),
                                                            "",
                                                            this.DTP01_KBGDATE.GetString(),
                                                            UP_Get_OldInsaCode("1", this.CBH01_KBIGUBN.GetValue().ToString()),
                                                            "",
                                                            "",
                                                            CBH01_KBJKCD.GetValue().ToString(),
                                                            TXT01_KBHOBN.GetValue().ToString(),
                                                            CBH01_KBBUSEO.GetValue().ToString(),
                                                            CBH01_KBBSTEAM.GetValue().ToString(),
                                                            CBH01_KBSOSOK.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            CBO01_KBGUNMU.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            this.MTB01_KBJUMIN.GetValue().ToString().Replace("-", ""),
                                                            "",
                                                            CBO01_KBUNION.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            TXT01_KBMAILID.GetValue().ToString(),
                                                            "0",
                                                            TXT01_KBMOBILE.GetValue().ToString(),
                                                            TXT01_KBCOMTEL.GetValue().ToString(),
                                                            "",
                                                            TXT01_KBINTRO.GetValue().ToString(),   
                                                            "2",
                                                            this.TXT01_KBSABUN.GetValue()
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();



            this.ShowMessage("TY_M_GB_23NAD873");

            this.fsKBSABUN = this.TXT01_KBSABUN.GetValue().ToString();

            this.UP_BtnScreen(false);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //개인정보취급자가 아니면
            if (TYUserInfo.PerAuth != "Y")
            {
                this.ShowCustomMessage("개인정보취급자이외는 저장 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //사번 존재 체크
            if (string.IsNullOrEmpty(this.fsKBSABUN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.TXT01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_6CDAB088");
                    e.Successed = false;
                    return;
                }
                
            }

            //최초등록시 임금피크일자, 정년일자 자동 계산
            if (MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Length > 0 && CBH01_KBJKCD.GetValue().ToString() != "01")
            {
                string sBirthDate = string.Empty;

                if (MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) == "1" ||
                    MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) == "2")
                {
                    sBirthDate = "19" + MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Substring(0, 2);
                }
                else
                {
                    sBirthDate = "20" + MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Substring(0, 2);
                }

                DTP01_KBPKSDATE.SetValue(Convert.ToString(Convert.ToInt32(sBirthDate) + 56) + "0101");
                DTP01_KBREDATE.SetValue(Convert.ToString(Convert.ToInt32(sBirthDate) + 60) + "1231");

                if (CKB01_KBPYPEAK.Checked == true)
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) < Convert.ToInt32(DTP01_KBPKSDATE.GetString().ToString().Substring(0, 4)))
                    {
                        this.ShowCustomMessage("임금피크제구분에 체크 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                //임원
                CKB01_KBPYPEAK.Checked = false;
                DTP01_KBPKSDATE.SetValue("");
                DTP01_KBREDATE.SetValue("");
            }

            this.DAT10_KBCOMPANY.SetValue("2");
            this.DAT10_KBSABUN.SetValue(this.TXT01_KBSABUN.GetValue());
            this.DAT10_KBHANGL.SetValue(this.TXT01_KBHANGL.GetValue());
            this.DAT10_KBHANJA.SetValue(this.TXT01_KBHANJA.GetValue());
            this.DAT10_KBENGNM.SetValue(this.TXT01_KBENGNM.GetValue());
            this.DAT10_KBSEXGB.SetValue(this.CBO01_KBSEXGB.GetValue());
            this.DAT10_KBIDATE.SetValue(DTP01_KBIDATE.GetValue().ToString());
            this.DAT10_KBJDATE.SetValue(DTP01_KBJDATE.GetValue().ToString());
            this.DAT10_KBGDATE.SetValue(DTP01_KBGDATE.GetValue().ToString());
            this.DAT10_KBIGUBN.SetValue(CBH01_KBIGUBN.GetValue().ToString());
            this.DAT10_KBJJCD.SetValue(CBH01_KBJJCD.GetValue().ToString());
            this.DAT10_KBJCCD.SetValue(CBH01_KBJCCD.GetValue().ToString());
            this.DAT10_KBJKCD.SetValue(CBH01_KBJKCD.GetValue().ToString());
            this.DAT10_KBHOBN.SetValue(TXT01_KBHOBN.GetValue());
            this.DAT10_KBSOSOK.SetValue(CBH01_KBSOSOK.GetValue().ToString());
            this.DAT10_KBBUSEO.SetValue(CBH01_KBBUSEO.GetValue().ToString());
            this.DAT10_KBBSTEAM.SetValue(CBH01_KBBSTEAM.GetValue().ToString());
            this.DAT10_KBBALCD.SetValue(CBH01_KBBALCD.GetValue().ToString());
            this.DAT10_KBBDATE.SetValue(DTP01_KBBDATE.GetString().ToString());
            this.DAT10_KBCODE.SetValue(CBO01_KBCODE.GetValue());
            this.DAT10_KBRFID.SetValue(TXT01_KBRFID.GetValue());
            this.DAT10_KBGUNMU.SetValue(CBO01_KBGUNMU.GetValue());
            this.DAT10_KBBONJK.SetValue(TXT01_KBBONJK.GetValue());
            this.DAT10_KBJUSO.SetValue(TXT01_KBJUSO.GetValue());
            this.DAT10_KBUPCD.SetValue(MTB01_KBUPCD.GetValue().ToString().Replace("-", "").Trim());
            this.DAT10_KBJUMIN.SetValue(MTB01_KBJUMIN.GetValue().ToString().Replace("-", "").Trim());
            this.DAT10_KBUNION.SetValue(CBO01_KBUNION.GetValue());
            this.DAT10_KBBIRTH.SetValue(MTB01_KBBIRTH.GetValue().ToString().Replace("-", "").Trim());
            this.DAT10_KBBIRGB.SetValue(CBO01_KBBIRGB.GetValue());
            this.DAT10_KBMAILID.SetValue(TXT01_KBMAILID.GetValue());
            this.DAT10_KBTELNO.SetValue(TXT01_KBTELNO.GetValue());
            this.DAT10_KBMOBILE.SetValue(TXT01_KBMOBILE.GetValue());
            this.DAT10_KBCOMTEL.SetValue(TXT01_KBCOMTEL.GetValue());
            this.DAT10_KBCOMFAX.SetValue(TXT01_KBCOMFAX.GetValue());
            this.DAT10_KBINTRO.SetValue(TXT01_KBINTRO.GetValue());
            this.DAT10_KBPAYGN.SetValue(CBO01_KBPAYGN.GetValue());
            this.DAT10_KBEISGN.SetValue(CBO01_KBEISGN.GetValue());
            this.DAT10_KBMEDNUM.SetValue(TXT01_KBMEDNUM.GetValue());
            this.DAT10_KBLEVEL.SetValue(TXT01_KBLEVEL.GetValue());
            this.DAT10_KBBOHUN.SetValue(CBO01_KBBOHUN.GetValue());
            this.DAT10_KBHISAB.SetValue(TYUserInfo.EmpNo);
            this.DAT10_KBPERAUTH.SetValue(CKB01_KBPERAUTH.GetValue());
            this.DAT10_KEY.SetValue(TYUserInfo.SecureKey);

            if (CBH01_KBJKCD.GetValue().ToString() != "01")
            {
                this.DAT10_KBPYPEAK.SetValue(CKB01_KBPYPEAK.GetValue());
                this.DAT10_KBPKSDATE.SetValue(DTP01_KBPKSDATE.GetString().ToString());
                this.DAT10_KBREDATE.SetValue(DTP01_KBREDATE.GetString().ToString());
                this.DAT10_KBPENSGUBN.SetValue(CBO01_KBPENSGUBN.GetValue());
            }
            else
            {
                this.DAT10_KBPYPEAK.SetValue("N");
                this.DAT10_KBPKSDATE.SetValue("");
                this.DAT10_KBREDATE.SetValue("");
                this.DAT10_KBPENSGUBN.SetValue("DB");
            }


            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 신규 발령사항 이벤트
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C1(this.TXT01_KBSABUN.GetValue().ToString(), string.Empty, string.Empty, "", "",  "I")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_Search_발령사항();
                this.UP_Run();
            }
        }
        #endregion

        #region Description : 발령사항 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BCH5389_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C1(this.FPS91_TY_S_HR_4BCH5389.GetValue("BLSABUN").ToString(), 
                                this.FPS91_TY_S_HR_4BCH5389.GetValue("BLBUNOYY").ToString(), 
                                this.FPS91_TY_S_HR_4BCH5389.GetValue("BLBUNOSEQ").ToString(),
                                this.FPS91_TY_S_HR_4BCH5389.GetValue("BLDATE").ToString(),
                                this.FPS91_TY_S_HR_4BCH5389.GetValue("BLCODE").ToString(),
                                "I")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_발령사항();
        }
        #endregion

        #region Description : 삭제 발령사항 이벤트
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BJ8L451", dt);
            this.DbConnector.ExecuteTranQueryList();

            //삭제후 제일 마지막 발령기준으로 인사기본사항 UPDATE한다.

            int iRowIndex = 0;
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BCH4388", this.fsKBSABUN);
            DataTable db = this.DbConnector.ExecuteDataTable();
            if (db.Rows.Count > 0)
            {
                iRowIndex = db.Rows.Count - 1;

                string sBLDATE = db.Rows[iRowIndex]["BLDATE"].ToString();
                string sBLCODE = db.Rows[iRowIndex]["BLCODE"].ToString();
                string sBLSOSOK = db.Rows[iRowIndex]["BLSOSOK"].ToString();
                string sBLBUSEO = db.Rows[iRowIndex]["BLBUSEO"].ToString();
                string sBLBSTEAM = db.Rows[iRowIndex]["BLBSTEAM"].ToString();
                string sBLJCCD = db.Rows[iRowIndex]["BLJCCD"].ToString();
                string sBLJKCD = db.Rows[iRowIndex]["BLJKCD"].ToString();
                string sBLJJCD = db.Rows[iRowIndex]["BLJJCD"].ToString();
                string sBLHOBN = db.Rows[iRowIndex]["BLHOBN"].ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BJB5457", sBLJJCD,
                                                         sBLJCCD,
                                                         sBLJKCD,
                                                         sBLHOBN,
                                                         sBLSOSOK,
                                                         sBLBUSEO,
                                                         sBLBSTEAM,
                                                         sBLCODE,
                                                         sBLDATE,
                                                         TYUserInfo.EmpNo,
                                                         this.fsKBSABUN
                                                        );
                this.DbConnector.ExecuteTranQuery();
            }
            this.UP_Run();
            this.ShowMessage("TY_M_GB_23NAD874");            
        }

        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BCH5389.GetDataSourceInclude(TSpread.TActionType.Remove, "BLBUNOYY", "BLBUNOSEQ", "BLSABUN", "BLDATE","BLCODE" );

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            
            /*  2018.09.04 손호진 요청 임시로 막는다 
            for( int i = 0; i < dt.Rows.Count; i++)
            {
                //발령일자 이후 등록된 자료가 있는지 체크한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BJAX455", dt.Rows[i]["BLSABUN"].ToString(), dt.Rows[i]["BLDATE"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_HR_4BJB0456");
                    e.Successed = false;

                    this.UP_Search_발령사항();

                    return;
                }
            }*/


            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 신규 학력사항 이벤트
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C2(this.fsKBSABUN, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_학력사항();
        }
        #endregion

        #region Description : 삭제 학력사항 이벤트
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BID8444", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();
            
            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_학력사항();
        }
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BD8V394.GetDataSourceInclude(TSpread.TActionType.Remove, "HLSABUN", "HLHLGUBN", "HLJUGUBN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }          

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : 학력사항 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BD8V394_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C2(this.FPS91_TY_S_HR_4BD8V394.GetValue("HLSABUN").ToString(), this.FPS91_TY_S_HR_4BD8V394.GetValue("HLHLGUBN").ToString(), this.FPS91_TY_S_HR_4BD8V394.GetValue("HLJUGUBN").ToString()
                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_학력사항();
        }
        #endregion       

        #region Description :  발령사항 조회 함수
        private void UP_Search_발령사항()
        {
            //발령사항 조회
            this.FPS91_TY_S_HR_4BCH5389.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BCH4388", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BCH5389.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description :  학력사항 조회 함수
        private void UP_Search_학력사항()
        {
            this.FPS91_TY_S_HR_4BD8V394.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BD8V393", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BD8V394.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description :  교육사항 조회 함수
        private void UP_Search_교육사항()
        {
            this.FPS91_TY_S_HR_4BPIW526.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPIW524", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BPIW526.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description :  경력사항 조회 함수
        private void UP_Search_경력사항()
        {
            this.FPS91_TY_S_HR_4BD9J399.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BD9J398", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BD9J399.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description :  자격면허 조회 함수
        private void UP_Search_자격면허()
        {
            this.FPS91_TY_S_HR_4BD9R403.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BD9R402", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BD9R403.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description :  가족사항 조회 함수
        private void UP_Search_가족사항()
        {
            this.FPS91_TY_S_HR_4BDAR407.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDAR406", TYUserInfo.SecureKey, TYUserInfo.PerAuth, this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BDAR407.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 포상사항 조회 함수
        private void UP_Search_포상사항()
        {
            this.FPS91_TY_S_HR_4BDB6411.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDB1409", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BDB6411.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 징계사항 조회 함수
        private void UP_Search_징계사항()
        {
            this.FPS91_TY_S_HR_4BDB9412.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDB6410", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BDB9412.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        #region Description :  병력사항 조회 함수
        private void UP_Search_병력사항()
        {
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDBM414", this.fsKBSABUN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "59");
            }

        }
        #endregion

        #region Description :  특기사항 조회 함수
        private void UP_Search_특기사항()
        {
            this.FPS91_TY_S_HR_4BLFI478.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BLFH476", this.fsKBSABUN);
            this.FPS91_TY_S_HR_4BLFI478.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description :  보증사항 조회 함수
        private void UP_Search_보증사항()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BD9Y404", this.fsKBSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "41");
            }
        }
        #endregion

        #region Description :  첨부관리 조회 함수
        private void UP_Search_첨부관리()
        {
            this.FPS91_TY_S_HR_4BLBZ470.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFV434", this.fsKBSABUN, "2","");
            this.FPS91_TY_S_HR_4BLBZ470.SetValue(this.DbConnector.ExecuteDataTable());

            //for (int i = 0; i < this.FPS91_TY_S_HR_4BLBZ470.ActiveSheet.RowCount; i++)
            //{
            //    this.FPS91_TY_S_HR_4BLBZ470_Sheet1.Cells[i, 8].CellType = new FarPoint.Win.Spread.CellType.ButtonCellType();
            //    this.FPS91_TY_S_HR_4BLBZ470_Sheet1.Cells[i, 8].Text = "download";

            //    this.FPS91_TY_S_HR_4BLBZ470_Sheet1.Cells[i, 8].CellType = global::TY.Service.Library.Properties.Resources.magnifier;
            //}
        }
        #endregion

        #region Description :  임원배수관리 조회 함수
        private void UP_Search_임원배수관리()
        {
            this.FPS91_TY_S_HR_BB9BI722.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_BB9BH721", this.fsKBSABUN);
            this.FPS91_TY_S_HR_BB9BI722.SetValue(this.DbConnector.ExecuteDataTable());          
        }
        #endregion

        #region Description : 신규 경력사항 이벤트
        private void BTN64_NEW_Click(object sender, EventArgs e)
        {
             if ((new TYHRKB02C3(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 this.UP_Search_경력사항();
        }
        #endregion

        #region Description : 경력사항 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BD9J399_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C3(this.FPS91_TY_S_HR_4BD9J399.GetValue("KLSABUN").ToString(), this.FPS91_TY_S_HR_4BD9J399.GetValue("KLSUNBUN").ToString() 
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_경력사항();
        }
        #endregion

        #region Description : 신규 가족사항 이벤트
        private void BTN70_NEW_Click(object sender, EventArgs e)
        {
            //개인정보취급자가 아니면
            if (TYUserInfo.PerAuth != "Y")
            {
                this.ShowCustomMessage("개인정보취급자이외는 등록 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);                
                return;
            }

            if ((new TYHRKB02C5(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_Search_가족사항();  
            }              
        }
        #endregion

        #region Description : 삭제 가족사항 이벤트
        private void BTN70_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BKKG467", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_가족사항();
        }

        private void BTN70_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BDAR407.GetDataSourceInclude(TSpread.TActionType.Remove, "GJSABUN", "GJSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //개인정보취급자가 아니면
            if (TYUserInfo.PerAuth != "Y")
            {
                this.ShowCustomMessage("개인정보취급자이외는 삭제 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 가족사항 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BDAR407_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C5(this.FPS91_TY_S_HR_4BDAR407.GetValue("GJSABUN").ToString(), this.FPS91_TY_S_HR_4BDAR407.GetValue("GJSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_가족사항();
        }
        #endregion

        #region Description : 삭제 경력사항 이벤트
        private void BTN64_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BIDD445", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_경력사항();
        }

        private void BTN64_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BD9J399.GetDataSourceInclude(TSpread.TActionType.Remove, "KLSABUN", "KLSUNBUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

       


        #region Description : 신규 자격면허 이벤트
        private void BTN68_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C6(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_Search_자격면허();
        }
        #endregion

        #region Description : 삭제 자격면허 이벤트
        private void BTN68_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BKK3466", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_자격면허();
        }

        private void BTN68_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_HR_4BD9R403.GetDataSourceInclude(TSpread.TActionType.Remove, "JKSABUN", "JKCODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 자격면허 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BD9R403_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C6(this.FPS91_TY_S_HR_4BD9R403.GetValue("JKSABUN").ToString(), this.FPS91_TY_S_HR_4BD9R403.GetValue("JKCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_자격면허();
        }
        #endregion

        #region Description : 신규 포상사항 이벤트
        private void BTN66_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C7(this.fsKBSABUN, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_Search_포상사항();
            }            
        }
        #endregion

        #region Description : 삭제 포상사항 이벤트
        private void BTN66_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BLG2480", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_포상사항();

        }

        private void BTN66_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BDB6411.GetDataSourceInclude(TSpread.TActionType.Remove, "PRSABUN", "PRGUBUN", "PRDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 포상사항 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4BDB6411_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C7(this.FPS91_TY_S_HR_4BDB6411.GetValue("PRSABUN").ToString(), this.FPS91_TY_S_HR_4BDB6411.GetValue("PRGUBUN").ToString(), this.FPS91_TY_S_HR_4BDB6411.GetValue("PRDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_포상사항();
        }
        #endregion

        #region Description : 신규 징계사항 이벤트
        private void BTN67_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C8(this.fsKBSABUN, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_Search_징계사항();
            }
        }
        #endregion

        #region Description : 징계사항 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4BDB9412_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C8(this.FPS91_TY_S_HR_4BDB9412.GetValue("SBSABUN").ToString(), this.FPS91_TY_S_HR_4BDB9412.GetValue("SBGUBUN").ToString(), this.FPS91_TY_S_HR_4BDB9412.GetValue("SBDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_징계사항();
        }
        #endregion

        #region Description : 삭제 징계사항 이벤트
        private void BTN67_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BLG6481", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_징계사항();
        }

        private void BTN67_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BDB9412.GetDataSourceInclude(TSpread.TActionType.Remove, "SBSABUN", "SBGUBUN", "SBDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 병력사항 저장 버튼 이벤트
        private void BTN69_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDBM414", this.fsKBSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_HR_4BKHQ461", this.CBH59_GBGBCODE.GetValue(),
                                                            this.CBH59_GBGKCODE.GetValue(),
                                                            this.CBH59_GBYJCODE.GetValue(),
                                                            this.CBH59_GBJDGUBN.GetValue(),
                                                            this.CBH59_GBSOSOK.GetValue(),
                                                            this.TXT59_GBNAME.GetValue(),                                                            
                                                            Get_Date(DTP59_GBIDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                                            Get_Date(DTP59_GBJDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),                                                             
                                                            this.CBH59_GBBKCODE.GetValue(),
                                                            this.TXT59_GBJUTEGI.GetValue(),
                                                            this.TXT59_GBGUNBUN.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.fsKBSABUN
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BKHP460", this.fsKBSABUN,
                                                            this.CBH59_GBGBCODE.GetValue(),
                                                            this.CBH59_GBGKCODE.GetValue(),
                                                            this.CBH59_GBYJCODE.GetValue(),
                                                            this.CBH59_GBJDGUBN.GetValue(),
                                                            this.CBH59_GBSOSOK.GetValue(),
                                                            this.TXT59_GBNAME.GetValue(),
                                                            Get_Date(DTP59_GBIDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                                            Get_Date(DTP59_GBJDATE.GetValue().ToString()).Replace("19000101", "").Replace("44441231", ""),
                                                            this.CBH59_GBBKCODE.GetValue(),
                                                            this.TXT59_GBJUTEGI.GetValue(),
                                                            this.TXT59_GBGUNBUN.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();
            this.ShowMessage("TY_M_GB_23NAD873");

        }

        private void BTN69_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion


        #region Description : 병력사항 삭제 버튼 이벤트
        private void BTN69_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BKHR462",this.fsKBSABUN);           
            this.DbConnector.ExecuteTranQuery();

            this.Initialize_Controls("59");

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN69_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDBM414", this.fsKBSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 보증사항 저장 버튼 이벤트
        private void BTN71_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BD9Y404", this.fsKBSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_HR_4BKIR464", this.DTP41_BJIDATE1.GetString().ToString(),
                                                            this.DTP41_BJJDATE1.GetString().ToString(),
                                                            this.TXT41_BJGIGANG.GetValue(),
                                                            this.TXT41_BJBUNNO.GetValue(),
                                                            Get_Numeric(this.TXT41_BJAMT.GetValue().ToString()),
                                                            this.MTB41_BJIDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.MTB41_BJJDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.TXT41_BJNAME1.GetValue(),
                                                            this.TXT41_BJGANG1.GetValue(),
                                                            this.TXT41_BJBONJUK1.GetValue(),
                                                            this.TXT41_BJJUSO1.GetValue(),
                                                            this.TXT41_BJNAME2.GetValue(),
                                                            this.TXT41_BJGANG2.GetValue(),
                                                            this.TXT41_BJBONJUK2.GetValue(),
                                                            this.TXT41_BJJUSO2.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.fsKBSABUN
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BKIQ463", this.fsKBSABUN,                                                           
                                                            this.DTP41_BJIDATE1.GetString().ToString(),
                                                            this.DTP41_BJJDATE1.GetString().ToString(),
                                                            this.TXT41_BJGIGANG.GetValue(),
                                                            this.TXT41_BJBUNNO.GetValue(),
                                                            Get_Numeric(this.TXT41_BJAMT.GetValue().ToString()),
                                                            this.MTB41_BJIDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.MTB41_BJJDATE2.GetValue().ToString().Replace("-", "").Trim(),
                                                            this.TXT41_BJNAME1.GetValue(),
                                                            this.TXT41_BJGANG1.GetValue(),
                                                            this.TXT41_BJBONJUK1.GetValue(),
                                                            this.TXT41_BJJUSO1.GetValue(),
                                                            this.TXT41_BJNAME2.GetValue(),
                                                            this.TXT41_BJGANG2.GetValue(),
                                                            this.TXT41_BJBONJUK2.GetValue(),
                                                            this.TXT41_BJJUSO2.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN71_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 보증사항 삭제 버튼 이벤트
        private void BTN71_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BKIS465", this.fsKBSABUN);
            this.DbConnector.ExecuteTranQuery();

            this.Initialize_Controls("41");

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN71_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 첨부관리 신규 버튼 이벤트
        private void BTN73_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB03C2(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_첨부관리();
        }
        #endregion

        #region Description : 첨부관리 삭제 버튼 이벤트
        private void BTN73_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BLEZ475", dt);
            this.DbConnector.ExecuteNonQueryList();
                        
            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_첨부관리();
        }

        private void BTN73_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BLBZ470.GetDataSourceInclude(TSpread.TActionType.Remove, "AFSABUN", "AFSEQ", "AFFILEGUBN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 첨부관리 그리드 버튼 클릭 이벤트
        private void FPS91_TY_S_HR_4BLBZ470_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "8")
            {
                this.UP_AttachFileDown(this.FPS91_TY_S_HR_4BLBZ470.GetValue("AFSABUN").ToString(),
                                       this.FPS91_TY_S_HR_4BLBZ470.GetValue("AFSEQ").ToString(),
                                       this.FPS91_TY_S_HR_4BLBZ470.GetValue("AFFILEGUBN").ToString());
            }
        }
        #endregion

        #region Description : 첨부관리 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4BLBZ470_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if ((new TYHRKB02C6(this.FPS91_TY_S_HR_4BD9R403.GetValue("JKSABUN").ToString(), this.FPS91_TY_S_HR_4BD9R403.GetValue("JKCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    this.UP_Search_자격면허();
        }
        #endregion

        #region Description : 임원배수관리 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_BB9BI722_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB03C3(this.FPS91_TY_S_HR_BB9BI722.GetValue("KXSABUN").ToString(), this.FPS91_TY_S_HR_BB9BI722.GetValue("KXSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_임원배수관리();
        }
        #endregion

        #region Description : 임원배수관리 신규 버튼 이벤트
        private void BTN74_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB03C3(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_임원배수관리();
        }
        #endregion

        #region Description : 임원배수관리 삭제 버튼 이벤트
        private void BTN74_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_BB9BG717", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_임원배수관리();
        }

        private void BTN74_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_BB9BI722.GetDataSourceInclude(TSpread.TActionType.Remove, "KXSABUN", "KXSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 첨부관리 다운로드 이벤트
        private void UP_AttachFileDown(string sAFSABUN, string sAFSEQ, string sAFFILEGUBN)
        {
            FileStream stream = null;
            int iArraySize = 0;
            byte[] _AttachFile = null;
            string sAFFILENAME = string.Empty;

            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BLEU474", sAFSABUN, sAFSEQ, sAFFILEGUBN);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAFFILENAME = dt.Rows[0]["AFFILENAME"].ToString();
                    _AttachFile = dt.Rows[0]["AFFILEBYTE"] as byte[];                    
                    iArraySize = _AttachFile.GetUpperBound(0);
                }

                this.saveFileDialog.FileName = sAFFILENAME;
                if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                string fileName = this.saveFileDialog.FileName;                

                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_AttachFile, 0, iArraySize + 1);

                this.ShowMessage("TY_M_GB_25UAA711");

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
        #endregion

        #region Description : 특기사항 신규 버튼 이벤트
        private void BTN72_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB03C1(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_특기사항();
        }
        #endregion

        #region Description : 특기사항 삭제 버튼 이벤트
        private void BTN72_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BLFT479", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_특기사항();

        }
        private void BTN72_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BLFI478.GetDataSourceInclude(TSpread.TActionType.Remove, "SPSABUN", "SPNUM");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 특기사항 그리드 더블 클릭 버튼 이벤트
        private void FPS91_TY_S_HR_4BLFI478_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB03C1(this.FPS91_TY_S_HR_4BLFI478.GetValue("SPSABUN").ToString(), this.FPS91_TY_S_HR_4BLFI478.GetValue("SPNUM").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_특기사항();
        }
        #endregion

        #region Description : 교육사항 신규 버튼 이벤트
        private void BTN65_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C4(this.fsKBSABUN, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_교육사항();
        }
        #endregion

        #region Description : 교육사항 삭제 버튼 이벤트
        private void BTN65_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPIW525", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_Search_교육사항();
        }

        private void BTN65_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BPIW526.GetDataSourceInclude(TSpread.TActionType.Remove, "GYSABUN", "GYIDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 교육사항 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BPIW526_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C4(this.FPS91_TY_S_HR_4BPIW526.GetValue("GYSABUN").ToString(), this.FPS91_TY_S_HR_4BPIW526.GetValue("GYIDATE").ToString()
                               )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.UP_Search_교육사항();
        }
        #endregion

       

       

       

       
        

    }
}
