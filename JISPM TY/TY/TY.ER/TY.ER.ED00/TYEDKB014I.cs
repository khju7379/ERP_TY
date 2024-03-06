using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 내국반출수기관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.14 13:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73VGC181 : EDI 환경설정 조회
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
    ///  INQ : 조회
    ///  SAV : 저장
    ///  EDHANGCHA : 항 차
    ///  EDHBONSUN : 본선
    ///  EDHHWAJU : 화주
    ///  EDHHWAMUL : 화물
    ///  EDHGJ : 공장
    ///  EDHACDSAU : 반출사유
    ///  EDHCHQTY : 반출중량
    ///  EDHDATE : 등록일자
    ///  EDHHMNO1 : 입고번호1
    ///  EDHHMNO2 : 입고번호2
    ///  EDHIPHANG : 입항일
    ///  EDHIPSINNO : 반입신고
    ///  EDHJSGB : 전송구분
    ///  EDHJUKHA : 적하목록
    ///  EDHMULGB : 반출물품
    ///  EDHNO1 : NO1
    ///  EDHNO2 : NO2
    ///  EDHNO3 : NO3
    ///  EDHPUMNM : 품명
    ///  EDHSEQ : 순번
    /// </summary>
    public partial class TYEDKB014I : TYBase
    {
        private string fsEDHDATE;
        private string fsEDHSEQ;

        #region  Description : 폼 로드 이벤트
        public TYEDKB014I(string sEDHDATE, string sEDHSEQ)
        {
            InitializeComponent();

            fsEDHDATE = sEDHDATE;
            fsEDHSEQ  = sEDHSEQ;
        }

        private void TYEDKB014I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            CBH01_EDHHWAMUL.SetReadOnly(true);

            DTP01_EDHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            UP_Set_FormLoad();
        }
        #endregion

        #region  Description : UP_Set_FormLoad 이벤트
        private void UP_Set_FormLoad()
        {
            if (string.IsNullOrEmpty(this.fsEDHDATE))
            {
                DTP01_EDHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                TXT01_EDHSEQ.SetValue("");
                TXT01_EDHACDSAU.SetValue("송유관이송");

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73VGC181");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    TXT01_EDHNO1.SetValue(dt.Rows[0]["EDNIMPSIGN"].ToString());
                    TXT01_EDHNO2.SetValue(DateTime.Now.ToString("yyyy"));
                    //순번 생성
                    TXT01_EDHNO3.SetValue(String.Format("{0:D8}", Convert.ToInt64(UP_Get_ChulSeq(DateTime.Now.ToString("yyyy")))));
                }
            }
            else
            {
                DTP01_EDHDATE.SetValue(fsEDHDATE);
                TXT01_EDHSEQ.SetValue(fsEDHSEQ);

                BTN61_INQ.Visible = false;

                UP_DataBinding();

                if (TXT01_EDHRCVGB.GetValue().ToString() == "Y")
                {
                    this.BTN61_SAV.Visible = false;
                    this.BTN61_INQ.Visible = false;
                }
            }
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4GE3279", DTP01_EDHDATE.GetString().ToString(), TXT01_EDHSEQ.GetValue());
            this.CurrentDataTableRowMapping(this.DbConnector.ExecuteDataTable(), "01");
        }
        #endregion

        #region  Description : 반입재고 선택 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            TYEDKB14C1 popup = new TYEDKB14C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CBO01_EDHGJ.SetValue(popup.fsEDIGJ);
                TXT01_EDHIPSINNO.SetValue(popup.fsCSSINNO);                
                TXT01_EDHJUKHA.SetValue(popup.fsVSJUKHA);
                TXT01_EDHBLMSN.SetValue(popup.fsCSMSNSEQ);
                TXT01_EDHBLHSN.SetValue(popup.fsCSHSNSEQ);
                TXT01_EDHHMNO1.SetValue(popup.fsIPSINOYY);
                TXT01_EDHHMNO2.SetValue(popup.fsIPSINO);
                
                TXT01_EDHIPHANG.SetValue(popup.fsCSIPHANG);
                CBH01_EDHBONSUN.SetValue(popup.fsCSBONSUN);
                CBH01_EDHHWAJU.SetValue(popup.fsCSHWAJU);
                CBH01_EDHHWAMUL.SetValue(popup.fsCSHWAMUL);
                TXT01_EDHPUMNM.SetValue(popup.fsCSHWAMULNM);
                TXT01_EDHBLNO.SetValue(popup.fsCSBLNO);

                TXT01_EDHCHQTY.SetValue(popup.fsCSIPQTY);
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsEDHDATE))
            {

                this.DbConnector.Attach("TY_P_UT_A4HG9295", DTP01_EDHDATE.GetString().ToString(),
                                                            TXT01_EDHSEQ.GetValue().ToString(),
                                                            CBO01_EDHGJ.GetValue(),
                                                            TXT01_EDHJUKHA.GetValue(),
                                                            TXT01_EDHBLMSN.GetValue(),
                                                            TXT01_EDHBLHSN.GetValue(),
                                                            TXT01_EDHHMNO1.GetValue().ToString()+TXT01_EDHHMNO2.GetValue().ToString(),
                                                            TXT01_EDHIPHANG.GetValue(),
                                                            CBH01_EDHBONSUN.GetValue(),
                                                            CBH01_EDHHWAJU.GetValue(),
                                                            CBH01_EDHHWAMUL.GetValue(),
                                                            TXT01_EDHBLNO.GetValue(),
                                                            "9",
                                                            "Y",
                                                            "1",
                                                            TXT01_EDHPUMNM.GetValue(),
                                                            TXT01_EDHACDSAU.GetValue(),
                                                            DTP01_EDHDATE.GetString().ToString(),
                                                            "0",
                                                            TXT01_EDHCHQTY.GetValue().ToString(),
                                                            "A",
                                                            TXT01_EDHNO1.GetValue(),
                                                            TXT01_EDHNO2.GetValue(),
                                                            TXT01_EDHNO3.GetValue(),
                                                            TXT01_EDHIPSINNO.GetValue(),
                                                            "",
                                                            "",
                                                            TXT01_EDHHMNO1.GetValue(),
                                                            TXT01_EDHHMNO2.GetValue(),
                                                            TYUserInfo.EmpNo
                    );

            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_A4HG3296", CBO01_EDHGJ.GetValue(),
                                                            TXT01_EDHJUKHA.GetValue(),
                                                            TXT01_EDHBLMSN.GetValue(),
                                                            TXT01_EDHBLHSN.GetValue(),
                                                            TXT01_EDHHMNO1.GetValue().ToString() + TXT01_EDHHMNO2.GetValue().ToString(),
                                                            TXT01_EDHIPHANG.GetValue(),
                                                            CBH01_EDHBONSUN.GetValue(),
                                                            CBH01_EDHHWAJU.GetValue(),
                                                            CBH01_EDHHWAMUL.GetValue(),
                                                            TXT01_EDHBLNO.GetValue(),
                                                            "9",
                                                            "Y",
                                                            "1",
                                                            TXT01_EDHPUMNM.GetValue(),
                                                            TXT01_EDHACDSAU.GetValue(),
                                                            DTP01_EDHDATE.GetString().ToString(),
                                                            "0",
                                                            TXT01_EDHCHQTY.GetValue().ToString(),
                                                            "A",
                                                            TXT01_EDHNO1.GetValue(),
                                                            TXT01_EDHNO2.GetValue(),
                                                            TXT01_EDHNO3.GetValue(),
                                                            TXT01_EDHIPSINNO.GetValue(),
                                                            "",
                                                            "",
                                                            TXT01_EDHHMNO1.GetValue(),
                                                            TXT01_EDHHMNO2.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            DTP01_EDHDATE.GetString().ToString(),
                                                            TXT01_EDHSEQ.GetValue().ToString()

                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (string.IsNullOrEmpty(this.fsEDHDATE))
            {
                //순번생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4HF2293", DTP01_EDHDATE.GetString().ToString());
                TXT01_EDHSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }
           
            //재고량체크
            if (Convert.ToDouble(Get_Numeric(TXT01_EDHCHQTY.GetValue().ToString())) > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A4HFZ294", CBO01_EDHGJ.GetValue(), this.TXT01_EDHIPSINNO.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(TXT01_EDHCHQTY.GetValue().ToString()) > Convert.ToDouble(dt.Rows[0]["JEGOQTY"].ToString()))
                    {
                        this.SetFocus(TXT01_EDHCHQTY);
                        e.Successed = false;
                        this.ShowCustomMessage("반출재고량이 초과되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                this.SetFocus(TXT01_EDHCHQTY);
                e.Successed = false;
                this.ShowCustomMessage("출고량은 0 보다 커야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
