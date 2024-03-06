using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 출입자 관리(공사) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.05.02 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_852HP910 : 출입자 관리(공사) 상세조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CIINWON : 작업인원
    ///  CIWORKBTN : 공사진행조회
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CICSABUN : 사번
    ///  CIVEND : 거래처
    ///  CISIKDEGN : 식대비용
    ///  CISIKSAGN1 : 조식
    ///  CISIKSAGN2 : 중식
    ///  CISIKSAGN3 : 석식
    ///  CISIKSAGN4 : 야식
    ///  CISIKSAGN5 : 간식
    ///  CIDATE : 작업일자
    ///  CIYSEDATE : 종료예정일자
    ///  CIYSSDATE : 시작예정일자
    ///  CIPLACE : 작업장소
    ///  CISEQ : 순번
    ///  CISJIKCHK : 직책
    ///  CISNAME : 신청자
    ///  CIWKLIST : 작업내용
    ///  CIWORK : 작업명
    ///  CIYSETIME : 종료예정시간
    ///  CIYSSTIME : 시작예정시간
    /// </summary>
    public partial class TYHRGB001I : TYBase
    {
        string fsCIDATE = string.Empty;
        string fsCISEQ = string.Empty;

        #region Description : 폼 로드
        public TYHRGB001I(string sCIDATE, string sCISEQ)
        {
            fsCIDATE = sCIDATE;
            fsCISEQ = sCISEQ;

            InitializeComponent();
        }

        private void TYHRGB001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.TXT01_CISEQ.SetReadOnly(true);
            this.CBH01_CICSABUN.SetReadOnly(true);
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85BH5014, "CLNAME");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85BH5014, "CLJUMIN");

            if (TYUserInfo.DeptCode.ToString().Substring(0, 2) != "D1" && TYUserInfo.DeptCode.ToString().Substring(0, 2) != "E1" && TYUserInfo.DeptCode != "A10300" &&
               TYUserInfo.EmpNo != "0372-M" && TYUserInfo.EmpNo != "0411-M" && TYUserInfo.EmpNo != "0431-M" &&
               TYUserInfo.EmpNo != "0064-M" && TYUserInfo.EmpNo != "0082-M" && TYUserInfo.EmpNo != "0118-M" &&
               TYUserInfo.EmpNo != "0039-M" && TYUserInfo.EmpNo != "0040-M" && TYUserInfo.EmpNo != "0127-M" &&
               TYUserInfo.EmpNo != "0422-M" && TYUserInfo.EmpNo != "0414-M" )
            {
                UP_SetColumnsLocked();
            }

            if (string.IsNullOrEmpty(fsCISEQ))
            {   
                UP_Field_Init();
                this.DTP01_CIDATE.SetReadOnly(false);
            }
            else
            {
                UP_Select();
                this.DTP01_CIDATE.SetReadOnly(true);
            }

            SetStartingFocus(this.CBH01_CIVEND.CodeText);
        }
        #endregion

        #region Description : 상세내역 조회
        private void UP_Select()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_842GS789",
                fsCIDATE,
                fsCISEQ
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_CIDATE.SetValue(dt.Rows[0]["CIDATE"].ToString());
                TXT01_CISEQ.SetValue(dt.Rows[0]["CISEQ"].ToString());
                CBO01_CIGUBUN.SetValue(dt.Rows[0]["CIGUBUN"].ToString());
                CBO01_CICGUBUN.SetValue(dt.Rows[0]["CICGUBUN"].ToString());
                CBH01_CIVEND.SetValue(dt.Rows[0]["CIVEND"].ToString());
                TXT01_CIWORK.SetValue(dt.Rows[0]["CIWORK"].ToString());
                DTP01_CIYSSDATE.SetValue(dt.Rows[0]["CIYSSDATE"].ToString());
                TXT01_CIYSSHH.SetValue(Set_Fill4(dt.Rows[0]["CIYSSTIME"].ToString()).Substring(0, 2));
                TXT01_CIYSSMM.SetValue(Set_Fill4(dt.Rows[0]["CIYSSTIME"].ToString()).Substring(2, 2));
                DTP01_CIYSEDATE.SetValue(dt.Rows[0]["CIYSEDATE"].ToString());
                TXT01_CIYSEHH.SetValue(Set_Fill4(dt.Rows[0]["CIYSETIME"].ToString()).Substring(0, 2));
                TXT01_CIYSEMM.SetValue(Set_Fill4(dt.Rows[0]["CIYSETIME"].ToString()).Substring(2, 2));
                TXT01_CIPLACE.SetValue(dt.Rows[0]["CIPLACE"].ToString());
                TXT01_CIWKLIST.SetValue(dt.Rows[0]["CIWKLIST"].ToString());
                TXT01_CISNAME.SetValue(dt.Rows[0]["CISNAME"].ToString());
                TXT01_CISJIKCHK.SetValue(dt.Rows[0]["CISJIKCHK"].ToString());
                CBH01_CICSABUN.SetValue(dt.Rows[0]["CICSABUN"].ToString());
                if (dt.Rows[0]["CISIKSAGN1"].ToString() == "Y")
                {
                    CKB01_CISIKSAGN1.Checked = true;
                }
                else{
                    CKB01_CISIKSAGN1.Checked = false;
                }
                if (dt.Rows[0]["CISIKSAGN2"].ToString() == "Y")
                {
                    CKB01_CISIKSAGN2.Checked = true;
                }
                else
                {
                    CKB01_CISIKSAGN2.Checked = false;
                }
                if (dt.Rows[0]["CISIKSAGN3"].ToString() == "Y")
                {
                    CKB01_CISIKSAGN3.Checked = true;
                }
                else
                {
                    CKB01_CISIKSAGN3.Checked = false;
                }
                if (dt.Rows[0]["CISIKSAGN4"].ToString() == "Y")
                {
                    CKB01_CISIKSAGN4.Checked = true;
                }
                else
                {
                    CKB01_CISIKSAGN4.Checked = false;
                }
                if (dt.Rows[0]["CISIKSAGN5"].ToString() == "Y")
                {
                    CKB01_CISIKSAGN5.Checked = true;
                }
                else
                {
                    CKB01_CISIKSAGN5.Checked = false;
                }
                CBO01_CISIKDEGN.SetValue(dt.Rows[0]["CISIKDEGN"].ToString());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_852HN908",
                    fsCIDATE,
                    fsCISEQ
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_HR_85BH5014.SetValue(dt);
            }
        }
        #endregion

        #region Description : 인원선택 버튼
        private void BTN61_CIINWON_Click(object sender, EventArgs e)
        {
            TYHRGB01C1 popup = new TYHRGB01C1(this.DTP01_CIDATE.GetValue().ToString(),this.TXT01_CISEQ.GetValue().ToString(), "공사", this.CBH01_CIVEND.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.ds.Tables[0].Rows.Count > 0)
                {
                    int iRowIndex = 0;

                    iRowIndex = this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Rows.Count;

                    for (int i = 0; i < popup.ds.Tables[0].Rows.Count; i++)
                    {
                        if (popup.ds.Tables[0].Rows[i]["GUBUN"].ToString() == "ADD")
                        {
                            iRowIndex = iRowIndex + 1;

                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.AddRows(iRowIndex - 1, 1);
                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Cells[iRowIndex - 1, 2].Text = popup.ds.Tables[0].Rows[i]["CLNAME"].ToString();
                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Cells[iRowIndex - 1, 3].Text = popup.ds.Tables[0].Rows[i]["CLJUMIN"].ToString().Substring(0, 7);
                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Cells[iRowIndex - 1, 6].Text = popup.ds.Tables[0].Rows[i]["CLTEL"].ToString();
                            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Cells[iRowIndex - 1, 4].Text = popup.ds.Tables[0].Rows[i]["CLJUSO"].ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 공사조회 버튼
        private void BTN61_CIWORKBTN_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsCIDATE = "";
            fsCIDATE = "";
            UP_Field_Init();
            this.DTP01_CIDATE.SetReadOnly(false);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                string sCISIKSAGN1 = "N";
                string sCISIKSAGN2 = "N";
                string sCISIKSAGN3 = "N";
                string sCISIKSAGN4 = "N";
                string sCISIKSAGN5 = "N";
                string sCIYSSTIME = string.Empty;
                string sCIYSETIME = string.Empty;
                string sCISEQ = string.Empty;

                if (CKB01_CISIKSAGN1.Checked == true)
                {
                    sCISIKSAGN1 = "Y";
                }
                if (CKB01_CISIKSAGN2.Checked == true)
                {
                    sCISIKSAGN2 = "Y";
                }
                if (CKB01_CISIKSAGN3.Checked == true)
                {
                    sCISIKSAGN3 = "Y";
                }
                if (CKB01_CISIKSAGN4.Checked == true)
                {
                    sCISIKSAGN4 = "Y";
                }
                if (CKB01_CISIKSAGN5.Checked == true)
                {
                    sCISIKSAGN5 = "Y";
                }

                if (TXT01_CIYSSHH.GetValue().ToString() != "" && TXT01_CIYSSMM.GetValue().ToString() != "")
                {
                    sCIYSSTIME = Set_Fill2(TXT01_CIYSSHH.GetValue().ToString()) + Set_Fill2(TXT01_CIYSSMM.GetValue().ToString());
                }
                if (TXT01_CIYSEHH.GetValue().ToString() != "" && TXT01_CIYSEMM.GetValue().ToString() != "")
                {
                    sCIYSETIME = Set_Fill2(TXT01_CIYSEHH.GetValue().ToString()) + Set_Fill2(TXT01_CIYSEMM.GetValue().ToString());
                }

                this.DbConnector.CommandClear();
                if (string.IsNullOrEmpty(fsCISEQ))
                {
                    sCISEQ = UP_getSEQ(DTP01_CIDATE.GetString());
                    //신규(마스타)
                    this.DbConnector.Attach("TY_P_HR_853DC927",
                                            DTP01_CIDATE.GetString(),
                                            sCISEQ,
                                            CBO01_CIGUBUN.GetValue().ToString(),
                                            CBO01_CICGUBUN.GetValue().ToString(),
                                            CBH01_CIVEND.GetValue().ToString(),
                                            TXT01_CIWORK.GetValue().ToString(),
                                            DTP01_CIYSSDATE.GetString(),
                                            sCIYSSTIME,
                                            DTP01_CIYSEDATE.GetString(),
                                            sCIYSETIME,
                                            TXT01_CIPLACE.GetValue().ToString(),
                                            TXT01_CIWKLIST.GetValue().ToString(),
                                            CBH01_CICSABUN.GetValue().ToString(),
                                            TXT01_CISNAME.GetValue().ToString(),
                                            TXT01_CISJIKCHK.GetValue().ToString(),
                                            sCISIKSAGN1,
                                            sCISIKSAGN2,
                                            sCISIKSAGN3,
                                            sCISIKSAGN4,
                                            sCISIKSAGN5,
                                            CBO01_CISIKDEGN.GetValue().ToString());
                }
                else
                {
                    sCISEQ = TXT01_CISEQ.GetValue().ToString();
                    //수정(마스타)
                    this.DbConnector.Attach("TY_P_HR_853DH929",
                                            CBO01_CIGUBUN.GetValue().ToString(),
                                            CBO01_CICGUBUN.GetValue().ToString(),
                                            CBH01_CIVEND.GetValue().ToString(),
                                            TXT01_CIWORK.GetValue().ToString(),
                                            DTP01_CIYSSDATE.GetString(),
                                            sCIYSSTIME,
                                            DTP01_CIYSEDATE.GetString(),
                                            sCIYSETIME,
                                            TXT01_CIPLACE.GetValue().ToString(),
                                            TXT01_CIWKLIST.GetValue().ToString(),
                                            CBH01_CICSABUN.GetValue().ToString(),
                                            TXT01_CISNAME.GetValue().ToString(),
                                            TXT01_CISJIKCHK.GetValue().ToString(),
                                            sCISIKSAGN1,
                                            sCISIKSAGN2,
                                            sCISIKSAGN3,
                                            sCISIKSAGN4,
                                            sCISIKSAGN5,
                                            CBO01_CISIKDEGN.GetValue().ToString(),
                                            DTP01_CIDATE.GetString(),
                                            sCISEQ);
                }
                this.DbConnector.ExecuteTranQuery();

                if (UP_Save_Detail(ds, sCISEQ))
                {
                    fsCIDATE = DTP01_CIDATE.GetString();
                    fsCISEQ = sCISEQ;
                    UP_Select();
                    this.ShowMessage("TY_M_GB_23NAD873");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_246A2488");
                }
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_85BH5014.GetDataSourceInclude(TSpread.TActionType.New, "CLDATE", "CLSEQ", "CLNAME", "CLJUMIN", "CLJUSO", "CLRFID", "CLTEL", "CLIPDATE", "CLIPTIME", "CLCHDATE", "CLCHTIME", "CLSUGN", "CLSUDATE", "CLBLOOD", "CLSUGN2", "CLSUDATE2", "CLBLOOD2"));
            ds.Tables.Add(this.FPS91_TY_S_HR_85BH5014.GetDataSourceInclude(TSpread.TActionType.Update, "CLDATE", "CLSEQ", "CLNAME", "CLJUMIN", "CLJUSO", "CLRFID", "CLTEL", "CLIPDATE", "CLIPTIME", "CLCHDATE", "CLCHTIME", "CLSUGN", "CLSUDATE", "CLBLOOD", "CLSUGN2", "CLSUDATE2", "CLBLOOD2"));
            ds.Tables.Add(this.FPS91_TY_S_HR_85BH5014.GetDataSourceInclude(TSpread.TActionType.Remove, "CLDATE", "CLSEQ", "CLNAME", "CLJUMIN"));

            //if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_GB_2452W459");
            //    e.Successed = false;
            //    return;
            //}

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CLSUGN"].ToString() == "True" || ds.Tables[0].Rows[i]["CLSUGN"].ToString() == "Y")
                {
                    if (ds.Tables[0].Rows[i]["CLSUDATE"].ToString() == "19000101" || ds.Tables[0].Rows[i]["CLSUDATE"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전1차 체크시 교육일자는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["CLBLOOD"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전1차 체크시 혈압은 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["CLSUDATE"].ToString() != "19000101" && ds.Tables[0].Rows[i]["CLSUDATE"].ToString() != "")
                    {
                        this.ShowCustomMessage("교육일자1차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["CLBLOOD"].ToString() != "")
                    {
                        this.ShowCustomMessage("혈압1차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                if (ds.Tables[0].Rows[i]["CLSUGN2"].ToString() == "True" || ds.Tables[0].Rows[i]["CLSUGN2"].ToString() == "Y")
                {
                    if (ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() == "19000101" || ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전2차 체크시 교육일자는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["CLBLOOD2"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전2차 체크시 혈압은 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() != "19000101" && ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() != "")
                    {
                        this.ShowCustomMessage("교육일자2차입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["CLBLOOD2"].ToString() != "")
                    {
                        this.ShowCustomMessage("혈압2차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["CLSUGN"].ToString() == "True" || ds.Tables[1].Rows[i]["CLSUGN"].ToString() == "Y")
                {
                    if (ds.Tables[1].Rows[i]["CLSUDATE"].ToString() == "19000101" || ds.Tables[1].Rows[i]["CLSUDATE"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전1차 체크시 교육일자는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[1].Rows[i]["CLBLOOD"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전1차 체크시 혈압은 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (ds.Tables[1].Rows[i]["CLSUDATE"].ToString() != "19000101" && ds.Tables[1].Rows[i]["CLSUDATE"].ToString() != "")
                    {
                        this.ShowCustomMessage("교육일자1차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[1].Rows[i]["CLBLOOD"].ToString() != "")
                    {
                        this.ShowCustomMessage("혈압1차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                if (ds.Tables[1].Rows[i]["CLSUGN2"].ToString() == "True" || ds.Tables[1].Rows[i]["CLSUGN2"].ToString() == "Y")
                {
                    if (ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() == "19000101" || ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전2차 체크시 교육일자는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[1].Rows[i]["CLBLOOD2"].ToString() == "")
                    {
                        this.ShowCustomMessage("안전2차 체크시 혈압은 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() != "19000101" && ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() != "")
                    {
                        this.ShowCustomMessage("교육일자2차입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[1].Rows[i]["CLBLOOD2"].ToString() != "")
                    {
                        this.ShowCustomMessage("혈압2차 입력시 안전 체크는 필수항목 입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 순번 가져오기
        private string UP_getSEQ(string sCIDATE)
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_85AGE999",
                sCIDATE
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSEQ = dt.Rows[0]["CNT"].ToString();
            }

            return sSEQ;
        }
        private string UP_getBASEQ()
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85EHN037");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSEQ = dt.Rows[0]["CNT"].ToString();
            }

            return sSEQ;
            
        }
        #endregion

        #region Description : 내역 저장
        private bool UP_Save_Detail(DataSet ds, string sCISEQ)
        {
            try
            {   
                DataTable dt = new DataTable();
                // 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string sCLSUGN = "N";
                        string sCLSUGN2 = "N";
                        string sCLSUDATE = "";
                        string sCLSUDATE2 = "";

                        if (ds.Tables[0].Rows[i]["CLSUGN"].ToString() == "True" || ds.Tables[0].Rows[i]["CLSUGN"].ToString() == "Y")
                        {
                            sCLSUGN = "Y";
                        }
                        if (ds.Tables[0].Rows[i]["CLSUGN2"].ToString() == "True" || ds.Tables[0].Rows[i]["CLSUGN2"].ToString() == "Y")
                        {
                            sCLSUGN2 = "Y";
                        }

                        if (ds.Tables[0].Rows[i]["CLSUDATE"].ToString() != "19000101" && ds.Tables[0].Rows[i]["CLSUDATE"].ToString() != "")
                        {
                            sCLSUDATE = ds.Tables[0].Rows[i]["CLSUDATE"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() != "19000101" && ds.Tables[0].Rows[i]["CLSUDATE2"].ToString() != "")
                        {
                            sCLSUDATE2 = ds.Tables[0].Rows[i]["CLSUDATE2"].ToString();
                        }

                        //SelectOrder_GHCLREF; TY_P_HR_853GV938
                        //GHCLREF 에 DATA가 있는지 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853GV938",
                            DTP01_CIDATE.GetString(),
                            sCISEQ,
                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if(dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHCLREF; TY_P_HR_853GW939
                            this.DbConnector.CommandClear();

                            

                            this.DbConnector.Attach("TY_P_HR_853GW939",
                                            DTP01_CIDATE.GetString(),
                                            sCISEQ,
                                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString(),
                                            ds.Tables[0].Rows[i]["CLJUSO"].ToString(),
                                            ds.Tables[0].Rows[i]["CLTEL"].ToString(),
                                            sCLSUGN,
                                            sCLSUDATE,
                                            ds.Tables[0].Rows[i]["CLBLOOD"].ToString(),
                                            sCLSUGN2,
                                            sCLSUDATE2,
                                            ds.Tables[0].Rows[i]["CLBLOOD2"].ToString());

                            this.DbConnector.ExecuteTranQuery();
                        }

                        //SelectOrder_GHCPREF; TY_P_HR_853GZ940
                        //GHCPREF 에 DATA가 있는지 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853GZ940",
                            CBO01_CIGUBUN.GetValue().ToString(),
                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if(dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHCPREF; TY_P_HR_853H0941
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H0941",
                                            CBO01_CIGUBUN.GetValue().ToString(),
                                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString(),
                                            StringTransfer(ds.Tables[0].Rows[i]["CLJUSO"].ToString(),46),
                                            ds.Tables[0].Rows[i]["CLTEL"].ToString(),
                                            CBH01_CIVEND.GetValue().ToString().Trim(),
                                            CBH01_CIVEND.GetText().ToString().Trim()
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }

                        //외부방문자 이력TABLE 등록

                        string sBASEQ = UP_getBASEQ();

                        //SelectOrder_GHBMASTER; TY_P_HR_853H7942 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853H7942",
                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if(dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHBMASTER; TY_P_HR_853H1944
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H1944",
                                            sBASEQ,
                                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString().Replace("-",""),
                                            StringTransfer(ds.Tables[0].Rows[i]["CLJUSO"].ToString(), 46),
                                            CBH01_CIVEND.GetText().ToString().Trim(),
                                            ds.Tables[0].Rows[i]["CLTEL"].ToString(),
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss"),
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            "A",
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss")
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }
                        else{
                            //Ora_UpdateOrder_GHBMASTER; TY_P_HR_853H0943
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H0943",
                                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString().Replace("-", ""),
                                            StringTransfer(ds.Tables[0].Rows[i]["CLJUSO"].ToString(), 46),
                                            CBH01_CIVEND.GetText().ToString().Trim(),
                                            ds.Tables[0].Rows[i]["CLTEL"].ToString(),
                                            "C",
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss"),
                                            ds.Tables[0].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[0].Rows[i]["CLJUMIN"].ToString().Replace("-", "")
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }
                    }
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string sCLSUGN = "N";
                        string sCLSUGN2 = "N";
                        string sCLSUDATE = "";
                        string sCLSUDATE2 = "";

                        if (ds.Tables[1].Rows[i]["CLSUGN"].ToString() == "True" || ds.Tables[1].Rows[i]["CLSUGN"].ToString() == "Y")
                        {
                            sCLSUGN = "Y";
                        }
                        if (ds.Tables[1].Rows[i]["CLSUGN2"].ToString() == "True" || ds.Tables[1].Rows[i]["CLSUGN2"].ToString() == "Y")
                        {
                            sCLSUGN2 = "Y";
                        }
                        if (ds.Tables[1].Rows[i]["CLSUDATE"].ToString() != "19000101" && ds.Tables[1].Rows[i]["CLSUDATE"].ToString() != "")
                        {
                            sCLSUDATE = ds.Tables[1].Rows[i]["CLSUDATE"].ToString();
                        }
                        if (ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() != "19000101" && ds.Tables[1].Rows[i]["CLSUDATE2"].ToString() != "")
                        {
                            sCLSUDATE2 = ds.Tables[1].Rows[i]["CLSUDATE2"].ToString();
                        }

                        //SelectOrder_GHCLREF; TY_P_HR_853GV938
                        //GHCLREF 에 DATA가 있는지 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853GV938",
                            DTP01_CIDATE.GetString(),
                            sCISEQ,
                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHCLREF; TY_P_HR_853GW939
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853GW939",
                                            DTP01_CIDATE.GetString(),
                                            sCISEQ,
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUSO"].ToString(),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            sCLSUGN,
                                            sCLSUDATE,
                                            ds.Tables[1].Rows[i]["CLBLOOD"].ToString(),
                                            sCLSUGN2,
                                            sCLSUDATE2,
                                            ds.Tables[1].Rows[i]["CLBLOOD2"].ToString());

                            this.DbConnector.ExecuteTranQuery();
                        }
                        else
                        {
                            //Ora_UpdateOrder_GHCLREF; TY_P_HR_853HN945
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853HN945",
                                            ds.Tables[1].Rows[i]["CLJUSO"].ToString(),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            sCLSUGN,
                                            sCLSUDATE,
                                            ds.Tables[1].Rows[i]["CLBLOOD"].ToString(),
                                            sCLSUGN2,
                                            sCLSUDATE2,
                                            ds.Tables[1].Rows[i]["CLBLOOD2"].ToString(),
                                            ds.Tables[1].Rows[i]["CLDATE"].ToString(),
                                            sCISEQ,
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString());

                            this.DbConnector.ExecuteTranQuery();
                        }

                        //SelectOrder_GHCPREF; TY_P_HR_853GZ940
                        //GHCPREF 에 DATA가 있는지 조회
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853GZ940",
                            CBO01_CIGUBUN.GetValue().ToString(),
                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHCPREF; TY_P_HR_853H0941
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H0941",
                                            CBO01_CIGUBUN.GetValue().ToString(),
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString(),
                                            StringTransfer(ds.Tables[1].Rows[i]["CLJUSO"].ToString(), 46),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            CBH01_CIVEND.GetValue().ToString().Trim(),
                                            CBH01_CIVEND.GetText().ToString().Trim()
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }
                        else
                        {
                            //Ora_UpdateOrder_GHCPREF; TY_P_HR_853HQ948
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853HQ948",
                                            StringTransfer(ds.Tables[1].Rows[i]["CLJUSO"].ToString(), 46),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            CBH01_CIVEND.GetValue().ToString().Trim(),
                                            CBH01_CIVEND.GetText().ToString().Trim(),
                                            CBO01_CIGUBUN.GetValue().ToString(),
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString()
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }

                        //외부방문자 이력TABLE 등록

                        string sBASEQ = UP_getBASEQ();

                        //SelectOrder_GHBMASTER; TY_P_HR_853H7942 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_HR_853H7942",
                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            //Ora_InsertOrder_GHBMASTER; TY_P_HR_853H1944
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H1944",
                                            sBASEQ,
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString().Replace("-", ""),
                                            StringTransfer(ds.Tables[1].Rows[i]["CLJUSO"].ToString(), 46),
                                            CBH01_CIVEND.GetText().ToString().Trim(),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss"),
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            "A",
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss")
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }
                        else
                        {
                            //Ora_UpdateOrder_GHBMASTER; TY_P_HR_853H0943
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_HR_853H0943",
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString().Replace("-", ""),
                                            StringTransfer(ds.Tables[1].Rows[i]["CLJUSO"].ToString(), 46),
                                            CBH01_CIVEND.GetText().ToString().Trim(),
                                            ds.Tables[1].Rows[i]["CLTEL"].ToString(),
                                            "C",
                                            System.DateTime.Now.ToString("yyyyMMdd"),
                                            System.DateTime.Now.ToString("HHmmss"),
                                            ds.Tables[1].Rows[i]["CLNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["CLJUMIN"].ToString().Replace("-", "")
                                            );

                            this.DbConnector.ExecuteTranQuery();
                        }
                    }
                }
                // 삭제
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_859GJ977",
                                                DTP01_CIDATE.GetString(),
                                                sCISEQ,
                                                ds.Tables[2].Rows[i]["CLNAME"].ToString(),
                                                ds.Tables[2].Rows[i]["CLJUMIN"].ToString()
                                                );
                        
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Description : 화면 초기화
        private void UP_Field_Init()
        {
            DTP01_CIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            TXT01_CISEQ.SetValue("");
            CBO01_CIGUBUN.SetValue("1");
            CBO01_CICGUBUN.SetValue("3");
            CBH01_CIVEND.SetValue("");
            TXT01_CIWORK.SetValue("");
            DTP01_CIYSSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            TXT01_CIYSSHH.SetValue("");
            TXT01_CIYSSMM.SetValue("");
            DTP01_CIYSEDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            TXT01_CIYSEHH.SetValue("");
            TXT01_CIYSEMM.SetValue("");
            TXT01_CIPLACE.SetValue("");
            TXT01_CIWKLIST.SetValue("");
            TXT01_CISNAME.SetValue("");
            TXT01_CISJIKCHK.SetValue("");
            CBH01_CICSABUN.SetValue(TYUserInfo.EmpNo);
            CKB01_CISIKSAGN1.Checked = false;
            CKB01_CISIKSAGN2.Checked = false;
            CKB01_CISIKSAGN3.Checked = false;
            CKB01_CISIKSAGN4.Checked = false;
            CKB01_CISIKSAGN5.Checked = false;
            CBO01_CISIKDEGN.SetValue("Y");

            this.FPS91_TY_S_HR_85BH5014.Initialize();
        }
        #endregion

        #region Description : 스프레드 컬럼 잠그기
        private void UP_SetColumnsLocked()
        {
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLSUGN"].Locked = true;
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLSUDATE"].Locked = true;
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLBLOOD"].Locked = true;
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLSUGN2"].Locked = true;
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLSUDATE2"].Locked = true;
            this.FPS91_TY_S_HR_85BH5014.ActiveSheet.Columns["CLBLOOD2"].Locked = true;
        }
        #endregion

        #region Description : 안전교육 이수 수정 체크
        private void FPS91_TY_S_HR_85BH5014_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 12)
            {
                if (this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 12].Text.ToString() == "" || this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 12].Text.ToString() == "1900-01-01")
                {
                    this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 12].Value = "";
                }
            }
            if (e.Column == 15)
            {
                if (this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 15].Text.ToString() == "" || this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 15].Text.ToString() == "1900-01-01")
                {
                    this.FPS91_TY_S_HR_85BH5014_Sheet1.Cells[e.Row, 15].Value = "";
                }
            }
        }
        #endregion
    }
}
