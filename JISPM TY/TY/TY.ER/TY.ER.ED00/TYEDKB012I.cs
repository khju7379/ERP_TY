using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출기간연장신청서관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.06.05 13:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_865E9175 : 반출기간연장신청서 등록
    ///  TY_P_UT_865G2177 : 반출기간연장신청서 수정
    ///  TY_P_UT_865G3178 : 반출기간연장신청서 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  EDIAPPGUBN : EDIAPPGUBN
    ///  EDIEXTGUBN : EDIEXTGUBN
    ///  EDIGJ : 공장
    ///  EDIJSGB : 전송구분
    ///  EDIDATE : 반입일자
    ///  EDIEXTEDATE : EDIEXTEDATE
    ///  EDIEXTSDATE : EDIEXTSDATE
    ///  EDIAPPNAME : EDIAPPNAME
    ///  EDIBLHSN : HSN
    ///  EDIBLMSN : MSN
    ///  EDICSSINNO : EDICSSINNO
    ///  EDICUSTKWA : EDICUSTKWA
    ///  EDICUSTLOC : EDICUSTLOC
    ///  EDIJUKHA : 적하목록
    ///  EDIMSG : MSG
    ///  EDINO1 : NO1
    ///  EDINO2 : NO2
    ///  EDINO3 : NO3
    ///  EDIRCVGB : 접수구분
    ///  EDIREASON : EDIREASON
    ///  EDIWHNUMBER : EDIWHNUMBER
    /// </summary>
    public partial class TYEDKB012I : TYBase
    {
        private string fsEDIGJ;
        private string fsEDIDATE;
        private string fsEDINO1;
        private string fsEDINO2;
        private string fsEDINO3;

        #region  Description : 폼 로드 이벤트
        public TYEDKB012I(string sEDIGJ, string sEDIDATE, string sEDINO1, string sEDINO2, string sEDINO3)
        {
            InitializeComponent();

            this.SetPopupStyle();

             fsEDIGJ   = sEDIGJ;
             fsEDIDATE = sEDIDATE;
             fsEDINO1 = sEDINO1;
             fsEDINO2 = sEDINO2;
             fsEDINO3 =  sEDINO3;
        }       

        private void TYEDKB012I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            BTN61_INQOPTION.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            UP_SetLockCheck();

            if (string.IsNullOrEmpty(this.fsEDIGJ))
            {
                //신규
                BTN61_INQOPTION.Visible = true;


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73VGC181");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                TXT01_EDINO1.SetValue(dt.Rows[0]["EDNIMPSIGN"].ToString());
                TXT01_EDINO2.SetValue(DateTime.Now.ToString("yyyy"));
                TXT01_EDINO3.SetValue("");

                TXT01_EDICUSTLOC.SetValue(dt.Rows[0]["EDNCUSTLOC"].ToString()+":울산세관");
                TXT01_EDICUSTKWA.SetValue(dt.Rows[0]["EDNCUSTKWA"].ToString() + ":통관지원(1)과");
                TXT01_EDIWHNUMBER.SetValue(dt.Rows[0]["EDNIMPSIGN"].ToString());
                
                DTP01_EDIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                CBO01_EDIEXTGUBN.SetValue("01");                

                DTP01_EDIEXTSDATE.SetValue("");
                DTP01_EDIEXTEDATE.SetValue("");

                TXT01_EDISAUPNO.SetValue("6108110449");
                TXT01_EDIYEAR.SetValue(DateTime.Now.ToString("yyyy"));
                TXT01_EDISEQ.SetValue("");

            }
            else
            {
                BTN61_INQOPTION.Visible = false;

                CBO01_EDIGJ.SetValue(fsEDIGJ);
                DTP01_EDIDATE.SetValue(fsEDIDATE);
                TXT01_EDINO1.SetValue(fsEDINO1);
                TXT01_EDINO2.SetValue(fsEDINO2);
                TXT01_EDINO3.SetValue(fsEDINO3);

                //수정
                UP_DataBinding();
            }

            UP_Set_EXTDATEDisPlay(CBO01_EDIEXTGUBN.GetValue().ToString());

        }
        #endregion

        #region  Description : UP_DataBinding() 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_865G3178", this.CBO01_EDIGJ.GetValue().ToString(), DTP01_EDIDATE.GetString().ToString(), TXT01_EDINO1.GetValue().ToString(), TXT01_EDINO2.GetValue().ToString(), TXT01_EDINO3.GetValue().ToString() );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                CBO01_EDIGJ.SetValue(dt.Rows[0]["EDIGJ"].ToString());
                DTP01_EDIDATE.SetValue(dt.Rows[0]["EDIDATE"].ToString());
                TXT01_EDINO1.SetValue(dt.Rows[0]["EDINO1"].ToString());
                TXT01_EDINO2.SetValue(dt.Rows[0]["EDINO2"].ToString());
                TXT01_EDINO3.SetValue(dt.Rows[0]["EDINO3"].ToString());

                TXT01_EDISAUPNO.SetValue(dt.Rows[0]["EDISAUPNO"].ToString());
                TXT01_EDIYEAR.SetValue(dt.Rows[0]["EDIYEAR"].ToString());
                TXT01_EDISEQ.SetValue(dt.Rows[0]["EDISEQ"].ToString());

                TXT01_EDIJUKHA.SetValue(dt.Rows[0]["EDIJUKHA"].ToString());
                TXT01_EDIBLMSN.SetValue(dt.Rows[0]["EDIBLMSN"].ToString());
                TXT01_EDIBLHSN.SetValue(dt.Rows[0]["EDIBLHSN"].ToString());
                TXT01_EDICSSINNO.SetValue(dt.Rows[0]["EDICSSINNO"].ToString());

                CBO01_EDIJSGB.SetValue(dt.Rows[0]["EDIJSGB"].ToString());

                TXT01_EDICUSTLOC.SetValue(dt.Rows[0]["EDICUSTLOC"].ToString() + ":울산세관");
                TXT01_EDICUSTKWA.SetValue(dt.Rows[0]["EDICUSTKWA"].ToString() + ":통관지원(1)과");
                CBO01_EDIAPPGUBN.SetValue(dt.Rows[0]["EDIAPPGUBN"].ToString());
                DTP01_EDIDATE.SetValue(dt.Rows[0]["EDIDATE"].ToString());
                TXT01_EDIWHNUMBER.SetValue(dt.Rows[0]["EDIWHNUMBER"].ToString());
                TXT01_EDIAPPNAME.SetValue(dt.Rows[0]["EDIAPPNAME"].ToString());
                CBO01_EDIEXTGUBN.SetValue(dt.Rows[0]["EDIEXTGUBN"].ToString());
                DTP01_EDIEXTSDATE.SetValue(dt.Rows[0]["EDIEXTSDATE"].ToString());
                DTP01_EDIEXTEDATE.SetValue(dt.Rows[0]["EDIEXTEDATE"].ToString());
                TXT01_EDIREASON.SetValue(dt.Rows[0]["EDIREASON"].ToString());
                TXT01_EDIRCVGB.SetValue(dt.Rows[0]["EDIRCVGB"].ToString());
                TXT01_EDIMSG.SetValue(dt.Rows[0]["EDIMSG"].ToString());

                //연장승인번호
                TXT01_EDAAPVALNO.SetValue(dt.Rows[0]["EDAAPVALNO"].ToString());

                this.DTP01_EDIDATE.SetReadOnly(true);

                if (dt.Rows[0]["EDIRCVGB"].ToString() == "Y")
                {
                    BTN61_SAV.Visible = false;
                }
            }

        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsEDIGJ))
            {
                //신규                
                this.DbConnector.Attach("TY_P_UT_865E9175", this.CBO01_EDIGJ.GetValue().ToString(), 
                                                            DTP01_EDIDATE.GetString().ToString(), 
                                                            TXT01_EDINO1.GetValue().ToString(), 
                                                            TXT01_EDINO2.GetValue().ToString(), 
                                                            TXT01_EDINO3.GetValue().ToString(),
                                                            TXT01_EDISAUPNO.GetValue().ToString(),
                                                            TXT01_EDIYEAR.GetValue().ToString(),
                                                            TXT01_EDISEQ.GetValue().ToString(),
                                                            TXT01_EDIJUKHA.GetValue().ToString(),
                                                            TXT01_EDIBLMSN.GetValue().ToString(),
                                                            TXT01_EDIBLHSN.GetValue().ToString(),
                                                            CBO01_EDIJSGB.GetValue().ToString(),
                                                            TXT01_EDICUSTLOC.GetValue().ToString().Substring(0,3),
                                                            TXT01_EDICUSTKWA.GetValue().ToString().Substring(0,2),
                                                            CBO01_EDIAPPGUBN.GetValue().ToString(),
                                                            TXT01_EDIWHNUMBER.GetValue().ToString(),
                                                            TXT01_EDIAPPNAME.GetValue().ToString(),
                                                            TXT01_EDICSSINNO.GetValue().ToString(),
                                                            CBO01_EDIEXTGUBN.GetValue().ToString(),                                                            
                                                            CBO01_EDIEXTGUBN.GetValue().ToString() != "99" ? "" : DTP01_EDIEXTSDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            CBO01_EDIEXTGUBN.GetValue().ToString() != "99" ? "" : DTP01_EDIEXTEDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                            TXT01_EDIREASON.GetValue().ToString(),
                                                            TXT01_EDIRCVGB.GetValue().ToString(),
                                                            TXT01_EDIMSG.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                //수정
                this.DbConnector.Attach("TY_P_UT_865G2177",
                                                           TXT01_EDISAUPNO.GetValue().ToString(),
                                                           TXT01_EDIYEAR.GetValue().ToString(),
                                                           TXT01_EDISEQ.GetValue().ToString(),
                                                           TXT01_EDIJUKHA.GetValue().ToString(),
                                                           TXT01_EDIBLMSN.GetValue().ToString(),
                                                           TXT01_EDIBLHSN.GetValue().ToString(),
                                                           CBO01_EDIJSGB.GetValue().ToString(),
                                                           TXT01_EDICUSTLOC.GetValue().ToString().Substring(0, 3),
                                                           TXT01_EDICUSTKWA.GetValue().ToString().Substring(0, 2),
                                                           CBO01_EDIAPPGUBN.GetValue().ToString(),
                                                           TXT01_EDIWHNUMBER.GetValue().ToString(),
                                                           TXT01_EDIAPPNAME.GetValue().ToString(),
                                                           TXT01_EDICSSINNO.GetValue().ToString(),
                                                           CBO01_EDIEXTGUBN.GetValue().ToString(),
                                                           CBO01_EDIEXTGUBN.GetValue().ToString() != "99" ? "" : DTP01_EDIEXTSDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                           CBO01_EDIEXTGUBN.GetValue().ToString() != "99" ? "" : DTP01_EDIEXTEDATE.GetString().ToString().Replace("19000101", "").ToString(),
                                                           TXT01_EDIREASON.GetValue().ToString(),
                                                           TXT01_EDIRCVGB.GetValue().ToString(),
                                                           TXT01_EDIMSG.GetValue().ToString(),
                                                           TYUserInfo.EmpNo,
                                                           this.CBO01_EDIGJ.GetValue().ToString(),
                                                           DTP01_EDIDATE.GetString().ToString(),
                                                           TXT01_EDINO1.GetValue().ToString(),
                                                           TXT01_EDINO2.GetValue().ToString(),
                                                           TXT01_EDINO3.GetValue().ToString()
                                                           );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //순번 생성
            if (string.IsNullOrEmpty(this.fsEDIGJ))
            {
               this.TXT01_EDINO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(TXT01_EDINO2.GetValue().ToString()))));

               this.TXT01_EDISEQ.SetValue(String.Format("{0:D5}", Convert.ToInt64(UP_Get_SubMitSeq(TXT01_EDIYEAR.GetValue().ToString()))));
            } 
          
            //연장기간입력일 경우 날짜 체크
            if (CBO01_EDIEXTGUBN.GetValue().ToString() == "99")
            {
                if (DTP01_EDIEXTSDATE.GetString().ToString().Replace("19000101", "").ToString() == "" || DTP01_EDIEXTEDATE.GetString().ToString().Replace("19000101", "").ToString() == "")
                {
                    this.ShowCustomMessage("연장기간일자를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 반출기간연장 순번 생성
        private string UP_Get_ChulSeq(string sYear)
        {
            string sSEQCH = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_867GH188", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sSEQCH = Convert.ToString(Convert.ToInt16(dt.Rows[0]["SEQCH"].ToString()) + 1);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_867GJ189", sSEQCH, sYear);
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                sSEQCH = "1";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_867GJ190", sYear, sSEQCH);
                this.DbConnector.ExecuteNonQuery();
            }

            return sSEQCH;
        }
        #endregion

        #region  Description : 반출기간연장 제출번호 생성
        private string UP_Get_SubMitSeq(string sYear)
        {
            string sSEQCH = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_868DH193", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sSEQCH = Convert.ToString(Convert.ToInt16(dt.Rows[0]["SEQ"].ToString()));
            }

            return sSEQCH;
        }
        #endregion

        #region  Description : CBO01_EDIEXTGUBN_SelectedIndexChanged 이벤트
        private void CBO01_EDIEXTGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Set_EXTDATEDisPlay(CBO01_EDIEXTGUBN.GetValue().ToString());
        }
        #endregion

        #region  Description : 연장기간 시작일종료일 표시
        private void UP_Set_EXTDATEDisPlay(string sCode)
        {
            if (sCode == "99")
            {
                DTP01_EDIEXTSDATE.Visible = true;
                DTP01_EDIEXTEDATE.Visible = true;
                LBL51_EDIEXTSDATE.Visible = true;
                label1.Visible = true;
            }
            else
            {
                DTP01_EDIEXTSDATE.Visible = false;
                DTP01_EDIEXTEDATE.Visible = false;
                LBL51_EDIEXTSDATE.Visible = false;
                label1.Visible = false;
            }
        }
        #endregion

        #region  Description : 통관일 5개월이상 자료 조회 팝업 이벤트
        private void BTN61_INQOPTION_Click(object sender, EventArgs e)
        {
            TYEDKB12C1 popup = new TYEDKB12C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TXT01_EDIJUKHA.SetValue(popup.fsJUKHA);
                TXT01_EDIBLMSN.SetValue(popup.fsMSN);
                TXT01_EDIBLHSN.SetValue(popup.fsHSN);
                TXT01_EDICSSINNO.SetValue(popup.fsCSSINNO);
            }
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
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
