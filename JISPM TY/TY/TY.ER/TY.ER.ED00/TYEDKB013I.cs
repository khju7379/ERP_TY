using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출수기관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.10 13:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A4AE1244 : 반출수기관리 등록
    ///  TY_P_UT_A4AE2245 : 반출수기관리 수정
    ///  TY_P_UT_A4AE4246 : 반출수기관리 확인
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
    ///  EDMHWAJU : 화주
    ///  EDMHWAMUL : 화물
    ///  EDMBANGB : 반출유형
    ///  EDMBLHSN : HSN
    ///  EDMBLMSN : MSN
    ///  EDMBUNHAL : 분할반출구분
    ///  EDMCHASU : 반출차수
    ///  EDMCHCNT : 반출개수
    ///  EDMCHQTY : 반출중량
    ///  EDMDATE : 등록일자
    ///  EDMGJ : 공장
    ///  EDMHANGCHA : 모선
    ///  EDMHMNO1 : 입고번호1
    ///  EDMHMNO2 : 입고번호2
    ///  EDMIPHANG : 입항일
    ///  EDMJSGB : 전송구분
    ///  EDMJUKHA : 적하목록
    ///  EDMNO1 : NO1
    ///  EDMNO2 : NO2
    ///  EDMNO3 : NO3
    ///  EDMSEQ : 순번
    ///  EDMSINNO : 반출근거번호
    ///  EDMTIME : 반출시간
    /// </summary>
    public partial class TYEDKB013I : TYBase
    {
        private string fsEDMDATE;
        private string fsEDMSEQ;

        #region  Description : 폼 로드 이벤트
        public TYEDKB013I(string sEDMDATE, string sEDMSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsEDMDATE = sEDMDATE;
            fsEDMSEQ  = sEDMSEQ;
        }

        private void TYEDKB013I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            BTN62_INQ.Text = "신고이력";
            
            this.UP_SetLockCheck();

            if (string.IsNullOrEmpty(this.fsEDMDATE))
            {
                DTP01_EDMDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                MTB01_EDMTIME.SetValue(DateTime.Now.ToString("HHmmss"));
                TXT01_EDMSEQ.SetValue("");

                this.BTN62_INQ.Visible = false;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73VGC181");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    TXT01_EDMNO1.SetValue(dt.Rows[0]["EDNIMPSIGN"].ToString());
                    TXT01_EDMNO2.SetValue(DateTime.Now.ToString("yyyy"));
                    //순번 생성
                    TXT01_EDMNO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(DateTime.Now.ToString("yyyy")))));                    
                }

                CBH01_EDMBANGB.SetValue("50");
                TXT01_EDMCHCNT.SetValue("0");
                TXT01_EDMCHASU.SetValue("0");     
            }
            else
            {
                DTP01_EDMDATE.SetValue(fsEDMDATE);
                TXT01_EDMSEQ.SetValue(fsEDMSEQ);

                BTN61_INQ.Visible = false;

                UP_DataBinding();

                if (TXT01_EDMRCVGB.GetValue().ToString() == "Y")
                {
                    this.BTN61_SAV.Visible = false;
                    this.BTN62_INQ.Visible = false;
                }
            }            
        }
        #endregion   
   
        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4AE4246", DTP01_EDMDATE.GetString().ToString(), TXT01_EDMSEQ.GetValue());
            this.CurrentDataTableRowMapping(this.DbConnector.ExecuteDataTable(), "01");            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsEDMDATE))
            {
                
                this.DbConnector.Attach("TY_P_UT_A4AE1244", DTP01_EDMDATE.GetString().ToString(),
                                                            TXT01_EDMSEQ.GetValue().ToString(),
                                                            CBO01_EDMGJ.GetValue(),
                                                            TXT01_EDMJUKHA.GetValue(),
                                                            TXT01_EDMBLMSN.GetValue(),
                                                            TXT01_EDMBLHSN.GetValue(),
                                                            TXT01_EDMSINNO.GetValue(),
                                                            TXT01_EDMNO1.GetValue(),
                                                            TXT01_EDMNO2.GetValue(),
                                                            TXT01_EDMNO3.GetValue(),
                                                            MTB01_EDMTIME.GetValue().ToString().Replace(":",""),
                                                            "9",
                                                            CBH01_EDMBANGB.GetValue(),
                                                            CBO01_EDMBUNHAL.GetValue(),
                                                            TXT01_EDMCHQTY.GetValue().ToString(),
                                                            TXT01_EDMCHCNT.GetValue(),
                                                            "0",
                                                            "0",
                                                            TXT01_EDMCHASU.GetValue(),
                                                            TXT01_EDMIPHANG.GetValue(),
                                                            CBH01_EDMHANGCHA.GetValue(),
                                                            CBH01_EDMHWAMUL.GetValue(),
                                                            CBH01_EDMHWAJU.GetValue(),
                                                            "",
                                                            "",
                                                            TXT01_EDMHMNO1.GetValue(),
                                                            TXT01_EDMHMNO2.GetValue(),
                                                            TYUserInfo.EmpNo
                    );

            }
            else
            {                
                this.DbConnector.Attach("TY_P_UT_A4AE2245", CBO01_EDMGJ.GetValue(),
                                                            TXT01_EDMJUKHA.GetValue(),
                                                            TXT01_EDMBLMSN.GetValue(),
                                                            TXT01_EDMBLHSN.GetValue(),
                                                            TXT01_EDMSINNO.GetValue(),
                                                            TXT01_EDMNO1.GetValue(),
                                                            TXT01_EDMNO2.GetValue(),
                                                            TXT01_EDMNO3.GetValue(),
                                                            MTB01_EDMTIME.GetValue().ToString().Replace(":",""),
                                                            "9",
                                                            CBH01_EDMBANGB.GetValue(),
                                                            CBO01_EDMBUNHAL.GetValue(),
                                                            TXT01_EDMCHQTY.GetValue().ToString(),
                                                            TXT01_EDMCHCNT.GetValue(),
                                                            "0",
                                                            "0",
                                                            TXT01_EDMCHASU.GetValue(),
                                                            TXT01_EDMIPHANG.GetValue(),
                                                            CBH01_EDMHANGCHA.GetValue(),
                                                            CBH01_EDMHWAMUL.GetValue(),
                                                            CBH01_EDMHWAJU.GetValue(),
                                                            TXT01_EDMHMNO1.GetValue(),
                                                            TXT01_EDMHMNO2.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            DTP01_EDMDATE.GetString().ToString(),
                                                            TXT01_EDMSEQ.GetValue().ToString()

                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_CLO_Click(null, null);

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsEDMDATE))
            {
                //순번생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4DEM256", DTP01_EDMDATE.GetString().ToString());
                TXT01_EDMSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }

            if (CBO01_EDMBUNHAL.GetValue().ToString() == "A")
            {
                TXT01_EDMCHASU.SetValue("0");
            }

            if (Convert.ToDouble(TXT01_EDMCHQTY.GetValue().ToString()) <= 0)
            {
                this.SetFocus(TXT01_EDMCHQTY);
                this.ShowCustomMessage("반출중량을 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return; 
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4DG5260", CBO01_EDMGJ.GetValue(), TXT01_EDMSINNO.GetValue(), TXT01_EDMJUKHA.GetValue(), TXT01_EDMBLMSN.GetValue(), TXT01_EDMBLHSN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            //분할구분에 따른 판단
            if (CBO01_EDMBUNHAL.GetValue().ToString() == "A") //전량
            {
                //전량일 경우 신고 이력이 있으면 안된다.
                if (dt.Rows.Count > 0)
                {
                    this.SetFocus(CBO01_EDMBUNHAL);
                    this.ShowCustomMessage("반출신고 이력이 있으면 전량반출을 할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return; 
                }

                //전량신고의 경우 신고수량과 출고중량이 같아야 한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4KHG301", CBO01_EDMGJ.GetValue(), TXT01_EDMSINNO.GetValue(), TXT01_EDMJUKHA.GetValue(), TXT01_EDMBLMSN.GetValue(), TXT01_EDMBLHSN.GetValue());
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dk.Rows[0]["EDICHQTYGAP"].ToString()) - Convert.ToDouble(TXT01_EDMCHQTY.GetValue().ToString()) != 0)
                    {
                        this.SetFocus(TXT01_EDMCHQTY);
                        this.ShowCustomMessage("전량신고경우 출고중량이 신고량과 같아야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (CBO01_EDMBUNHAL.GetValue().ToString() == "P") //분할
            {
                if (Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()) <= 0 )
                {
                    this.SetFocus(TXT01_EDMCHASU);
                    this.ShowCustomMessage("분할반출인경우 분할차수는 0보다 커야 합니다 ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                //전량, 최종으로 신고 된적이 있으면 안된다.
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["EDIBUNHAL"].ToString() == "A" || dt.Rows[0]["EDIBUNHAL"].ToString() == "L")
                    {
                        this.SetFocus(CBO01_EDMBUNHAL);
                        this.ShowCustomMessage("분할반출구분을 확인하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                //분할이면 이전신고 반출중량 + 현재 반출중량이 신고잔량보다 작아야 한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4KHG301", CBO01_EDMGJ.GetValue(), TXT01_EDMSINNO.GetValue(), TXT01_EDMJUKHA.GetValue(), TXT01_EDMBLMSN.GetValue(), TXT01_EDMBLHSN.GetValue());
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dk.Rows[0]["EDICHQTYGAP"].ToString()) - Convert.ToDouble(TXT01_EDMCHQTY.GetValue().ToString()) <= 0)
                    {
                        this.SetFocus(TXT01_EDMCHQTY);
                        this.ShowCustomMessage("분할반출경우 출고중량이 신고잔량보다 작아야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (CBO01_EDMBUNHAL.GetValue().ToString() == "L") //최종
            {
                if (Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()) <= 0)
                {
                    this.SetFocus(TXT01_EDMCHASU);
                    this.ShowCustomMessage("최종반출인경우 분할차수는 0보다 커야 합니다 ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                //전량으로 신고 된적이 있으면 안된다.
                //처음 신고는 L 로 할수 없다.
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["EDIBUNHAL"].ToString() == "A" || dt.Rows[0]["EDIBUNHAL"].ToString() == "L")
                    {
                        this.SetFocus(CBO01_EDMBUNHAL);
                        this.ShowCustomMessage("분할반출구분을 확인하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.SetFocus(CBO01_EDMBUNHAL);
                    this.ShowCustomMessage("최종반출을 선택할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return; 
                }

                //최종이면 이전신고 반출중량 + 현재 반출중량이 신고수량과 같아야 한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4KHG301", CBO01_EDMGJ.GetValue(), TXT01_EDMSINNO.GetValue(), TXT01_EDMJUKHA.GetValue(), TXT01_EDMBLMSN.GetValue(), TXT01_EDMBLHSN.GetValue());
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dk.Rows[0]["EDICHQTYGAP"].ToString()) - Convert.ToDouble(TXT01_EDMCHQTY.GetValue().ToString()) != 0)
                    {
                        this.SetFocus(TXT01_EDMCHQTY);
                        this.ShowCustomMessage("최종분할반출은 신고잔량과 일치해야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if ( Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()) > 0 && (CBO01_EDMBUNHAL.GetValue().ToString() == "P" || CBO01_EDMBUNHAL.GetValue().ToString() == "L"))
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt16(dt.Rows[0]["EDICHASU"].ToString()) >= Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()))
                    {
                        this.SetFocus(TXT01_EDMCHASU);
                        this.ShowCustomMessage("반출차수가 최종분할차수보다 커야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }

                    if (Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()) - Convert.ToInt16(dt.Rows[0]["EDICHASU"].ToString()) > 1 )
                    {
                        this.SetFocus(TXT01_EDMCHASU);
                        this.ShowCustomMessage("반출차수가 최종분할차수보다 1 이상 차이가 날수 없습니다! ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (Convert.ToInt16(TXT01_EDMCHASU.GetValue().ToString()) > 1)
                    {
                        this.SetFocus(TXT01_EDMCHASU);
                        this.ShowCustomMessage("첫 반출일 경우 반출차수가  1 이여야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

            }

            if (Convert.ToDouble(TXT01_EDMCHQTY.GetValue().ToString()) <= 0)
            {
                this.SetFocus(TXT01_EDMCHQTY);
                this.ShowCustomMessage("반출중량을 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 재고선택 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            TYEDKB13C1 popup = new TYEDKB13C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TXT01_EDMJUKHA.SetValue(popup.fsVSJUKHA);
                TXT01_EDMBLMSN.SetValue(popup.fsCSMSNSEQ);
                TXT01_EDMBLHSN.SetValue(popup.fsCSHSNSEQ);                
                TXT01_EDMHMNO1.SetValue(popup.fsIPSINOYY);
                TXT01_EDMHMNO2.SetValue(popup.fsIPSINO);
                TXT01_EDMSINNO.SetValue(popup.fsCSSINNO);

                TXT01_EDMIPHANG.SetValue(popup.fsCSIPHANG);
                CBH01_EDMHANGCHA.SetValue(popup.fsCSBONSUN);
                CBH01_EDMHWAJU.SetValue(popup.fsCSHWAJU);
                CBH01_EDMHWAMUL.SetValue(popup.fsCSHWAMUL);

                TXT01_EDMCHASU.SetValue(popup.fsCHASU);

                this.BTN62_INQ.Visible = true;
            }
        }
        #endregion

        #region  Description : 반출보고서 순번 생성
        private string UP_Get_ChulSeq(string sYear)
        {
            string sSEQCH = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_758E2416", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sSEQCH = Convert.ToString(Convert.ToInt16(dt.Rows[0]["SEQCH"].ToString()) + 1);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_758E1419", sSEQCH, sYear);
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                sSEQCH = "1";
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_758E4420", sYear, sSEQCH);
                this.DbConnector.ExecuteNonQuery();
            }

            return sSEQCH;
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDMGJ.SetValue("S");
            }
            else
            {
                CBO01_EDMGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDMGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : CBO01_EDMBUNHAL_SelectedIndexChanged 이벤트
        private void CBO01_EDMBUNHAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_EDMBUNHAL.GetValue().ToString() == "A")
            {
                TXT01_EDMCHASU.SetValue("0");
            }
        }
        #endregion

        #region  Description : 신고이력 조회 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            (new TYEDKB13C2(CBO01_EDMGJ.GetValue().ToString(),
                                   TXT01_EDMSINNO.GetValue().ToString(),
                                   TXT01_EDMJUKHA.GetValue().ToString(),
                                   TXT01_EDMBLMSN.GetValue().ToString(),
                                   TXT01_EDMBLHSN.GetValue().ToString()
                                   )).ShowDialog();            
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
