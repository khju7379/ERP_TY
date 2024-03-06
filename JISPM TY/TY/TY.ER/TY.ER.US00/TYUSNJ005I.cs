using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 일별 작업현황관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.10 08:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_949GF299 : 항운노조 일별작업현황 등록
    ///  TY_P_US_949GG300 : 항운노조 일별작업현황 수정
    ///  TY_P_US_949GL303 : 항운노조 일별작업현황 확인
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
    ///  HIGOKJONG : 곡　　종
    ///  HIHANGCHA : 항　　차
    ///  HIWKDATE : 작업일
    ///  HIAMT : 총금액
    ///  HIBIGO : 비고
    ///  HIDANGA : 단가
    ///  HIEDTIME : 종료시간
    ///  HIJYTIME : 적용시간
    ///  HIJYYYMM : 적용년월
    ///  HISEQ : 번호
    ///  HISTTIME : 시작시간
    ///  HIWKGUBUN : 작업구분
    ///  HIWKMAN : 작업인원
    ///  HIWKQTY : 작업량
    /// </summary>
    public partial class TYUSNJ005I : TYBase
    {
        private string  fsHIHANGCHA;
	    private string  fsHIGOKJONG;
	    private string  fsHIWKDATE;
        private string  fsHISEQ;    

        #region  Description : 폼 로드 이벤트
        public TYUSNJ005I( string sHIHANGCHA, string sHIGOKJONG, string sHIWKDATE, string sHISEQ )
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsHIHANGCHA = sHIHANGCHA;
            fsHIGOKJONG = sHIGOKJONG;
            fsHIWKDATE  = sHIWKDATE;
            fsHISEQ = sHISEQ;        
        }

        private void TYUSNJ005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsHIHANGCHA))
            {
                this.DTP01_HIWKDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                this.DTP01_HIJYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

                this.SetStartingFocus(CBH01_HIHANGCHA.CodeText);
            }
            else
            {
                CBH01_HIHANGCHA.SetReadOnly(true);
                CBH01_HIGOKJONG.SetReadOnly(true);
                DTP01_HIWKDATE.SetReadOnly(true);

                UP_DataBinding();

                this.SetStartingFocus(MTB01_HISTTIME);
            }
        }
        #endregion

        #region  Description : 데이타 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949GL303", this.fsHIHANGCHA, fsHIGOKJONG, fsHIWKDATE, fsHISEQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
                this.CurrentDataTableRowMapping(dt, "01");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsHIHANGCHA = "";

            this.TXT01_HISEQ.SetValue("");

            UP_FieldClear();

            CBH01_HIHANGCHA.SetReadOnly(false);
            CBH01_HIGOKJONG.SetReadOnly(false);
            DTP01_HIWKDATE.SetReadOnly(false);

            this.SetFocus(this.CBH01_HIHANGCHA.CodeText);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsHIHANGCHA))
            {
                this.DbConnector.Attach("TY_P_US_949GF299",
                 CBH01_HIHANGCHA.GetValue(),
                 CBH01_HIGOKJONG.GetValue(),                 
                 DTP01_HIWKDATE.GetString().ToString(),
                 TXT01_HISEQ.GetValue(),
                 MTB01_HISTTIME.GetValue().ToString().Replace(":", ""),
                 MTB01_HIEDTIME.GetValue().ToString().Replace(":",""),                 
                 TXT01_HIJYTIME.GetValue(),
                 TXT01_HIWKQTY.GetValue(),
                 TXT01_HIWKMAN.GetValue(),
                 CBO01_HIWKGUBUN.GetValue(),
                 TXT01_HIADD.GetValue(),
                 TXT01_HIBIGO.GetValue(),
                 DTP01_HIJYYYMM.GetString().ToString().Substring(0,6),
                 TXT01_HIDANGA.GetValue(),
                 TXT01_HIAMT.GetValue()                 
                );
            }
            else
            {
                this.DbConnector.Attach("TY_P_US_949GG300",
                                                             MTB01_HISTTIME.GetValue().ToString().Replace(":", ""),                                         
                                                             MTB01_HIEDTIME.GetValue().ToString().Replace(":",""),                                                             
                                                             TXT01_HIJYTIME.GetValue(),
                                                             TXT01_HIWKQTY.GetValue(),
                                                             TXT01_HIWKMAN.GetValue(),
                                                             CBO01_HIWKGUBUN.GetValue(),
                                                             TXT01_HIADD.GetValue(),
                                                             TXT01_HIBIGO.GetValue(),
                                                             DTP01_HIJYYYMM.GetString().ToString().Substring(0, 6),
                                                             TXT01_HIDANGA.GetValue(),
                                                             TXT01_HIAMT.GetValue(),
                                                             CBH01_HIHANGCHA.GetValue(),
                                                            CBH01_HIGOKJONG.GetValue(),                                                            
                                                            DTP01_HIWKDATE.GetString().ToString(),
                                                            TXT01_HISEQ.GetValue()
                                                            );                
            }
            this.DbConnector.ExecuteTranQuery();

            fsHIHANGCHA = CBH01_HIHANGCHA.GetValue().ToString();
            fsHIGOKJONG = CBH01_HIGOKJONG.GetValue().ToString();
            fsHIWKDATE = DTP01_HIWKDATE.GetString().ToString();
            fsHISEQ = TXT01_HISEQ.GetValue().ToString();        

            UP_DataBinding();

            this.SetFocus(this.BTN61_NEW);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_A41A0173", this.CBH01_HIHANGCHA.GetValue());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_A41A2176");
                e.Successed = false;
                return; 
            }

            if (string.IsNullOrEmpty(this.fsHIHANGCHA))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_94AAE321",  CBH01_HIHANGCHA.GetValue(),  CBH01_HIGOKJONG.GetValue(),    DTP01_HIWKDATE.GetString().ToString() );
                TXT01_HISEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }

            if (Convert.ToInt16(Set_Fill4(MTB01_HISTTIME.GetValue().ToString().Replace(":", "")).Substring(0, 2)) > 24)
            {
                this.SetFocus(MTB01_HISTTIME);
                this.ShowCustomMessage("작업시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            if (Convert.ToInt16(Set_Fill4(MTB01_HISTTIME.GetValue().ToString().Replace(":", "")).Substring(2, 2)) > 59)
            {
                this.SetFocus(MTB01_HISTTIME);
                this.ShowCustomMessage("작업시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(Set_Fill4(MTB01_HIEDTIME.GetValue().ToString().Replace(":", "")).Substring(0, 2)) > 24)
            {
                this.SetFocus(MTB01_HIEDTIME);
                this.ShowCustomMessage("작업시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(Set_Fill4(MTB01_HIEDTIME.GetValue().ToString().Replace(":", "")).Substring(2, 2)) > 59)
            {
                this.SetFocus(MTB01_HIEDTIME);
                this.ShowCustomMessage("작업시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (CBO01_HIWKGUBUN.Text.Trim() == "2" &&   Convert.ToInt16(Get_Numeric(TXT01_HIWKMAN.GetValue().ToString())) <= 0 )
            {
                this.SetFocus(TXT01_HIWKMAN);
                this.ShowCustomMessage("작업인원을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            
            string sHDJJAKUP = string.Empty;
            string sHDBJAKUP = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949A2295", DTP01_HIWKDATE.GetString().ToString().Substring(0,6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sHDJJAKUP = dt.Rows[0]["HDJJAKUP"].ToString();
                sHDBJAKUP = dt.Rows[0]["HDBJAKUP"].ToString();
            }
            else
            {
                this.ShowCustomMessage("단가파일에 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (TXT01_HIADD.GetValue().ToString() != "*" && TXT01_HIADD.GetValue().ToString() != "")
            {
                this.ShowCustomMessage("휴일연장 구분을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            string sIHHUVSGB = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_94BDG346", CBH01_HIHANGCHA.GetValue() );
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                sIHHUVSGB = dk.Rows[0]["IHHUVSGB"].ToString();
            }

            //요일확인
            string sDYGUBUN = string.Empty;
            DateTime dWeek = Convert.ToDateTime(DTP01_HIWKDATE.GetString().ToString().Substring(0, 4) + "-" + DTP01_HIWKDATE.GetString().ToString().Substring(4, 2) + "-" + DTP01_HIWKDATE.GetString().ToString().Substring(6, 2));
            int iYoilGn = Convert.ToInt16(dWeek.DayOfWeek);

            if (iYoilGn != 1)
            {
                //월 ~ 금
                sDYGUBUN = "1";
            }
            else
            {
                //일요일
                sDYGUBUN = "2";
            }

            string sJYGUBUN = string.Empty;

            Int32 iTIME = Convert.ToInt16(Set_Fill4(MTB01_HISTTIME.GetValue().ToString().Replace(":", "")).Substring(0, 2)) +
                          Convert.ToInt16(Set_Fill4(MTB01_HIEDTIME.GetValue().ToString().Replace(":", "")).Substring(0, 2));
            Int32 iMM = Convert.ToInt16(DTP01_HIWKDATE.GetString().ToString().Substring(4, 2));

            iTIME = iTIME / 2;

            if (((0 < iMM && iMM < 4) && (6 < iTIME && iTIME < 18)) ||
                ((3 < iMM && iMM < 7) && (4 < iTIME && iTIME < 19)) ||
                ((6 < iMM && iMM < 10) && (5 < iTIME && iTIME < 19)) ||
                ((9 < iMM && iMM < 13) && (6 < iTIME && iTIME < 17)))
            {
                sJYGUBUN = "1";
            }
            else
            {
                sJYGUBUN = "2";
            }

            if (double.Parse(Get_Numeric(this.TXT01_HIDANGA.GetValue().ToString())) == 0)
            {
                string sDANGA = string.Empty;
                if (sIHHUVSGB == "1")
                {
                    if (sDYGUBUN == "1" && sJYGUBUN == "1")
                    {
                        sDANGA = sHDJJAKUP.ToString();
                    }
                    else if (sDYGUBUN == "1" && sJYGUBUN == "2")
                    {
                        sDANGA = Convert.ToString(Decimal.Parse(sHDJJAKUP) * Decimal.Parse("0.5"));
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDJJAKUP) + Decimal.Parse(sDANGA)));
                    }
                    else if (sDYGUBUN == "2" && sJYGUBUN == "1")
                    {
                        sDANGA = Convert.ToString(Decimal.Parse(sHDJJAKUP) * Decimal.Parse("0.5"));
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDJJAKUP) + Decimal.Parse(sDANGA)));
                    }
                    else
                    {
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDJJAKUP) * 2));
                    }
                }
                else
                {
                    if (sDYGUBUN == "1" && sJYGUBUN == "1")
                    {
                        sDANGA = sHDBJAKUP.ToString();
                    }
                    else if (sDYGUBUN == "1" && sJYGUBUN == "2")
                    {
                        sDANGA = Convert.ToString(Decimal.Parse(sHDBJAKUP) * Decimal.Parse("0.5"));
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDBJAKUP) + Decimal.Parse(sDANGA)));
                    }
                    else if (sDYGUBUN == "2" && sJYGUBUN == "1")
                    {
                        sDANGA = Convert.ToString(Decimal.Parse(sHDBJAKUP) * Decimal.Parse("0.5"));
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDBJAKUP) + Decimal.Parse(sDANGA)));
                    }
                    else
                    {
                        sDANGA = Convert.ToString(Math.Floor(Decimal.Parse(sHDBJAKUP) * 2));
                    }
                }

                TXT01_HIDANGA.SetValue(Get_Numeric(sDANGA));
            }

            if (CBO01_HIWKGUBUN.GetValue().ToString() == "1") // 전체
            {
                TXT01_HIAMT.SetValue(Convert.ToString(Math.Floor(Decimal.Parse(TXT01_HIWKQTY.GetValue().ToString().Trim()) * Decimal.Parse(TXT01_HIDANGA.GetValue().ToString().Trim()))));
            }
            else if (CBO01_HIWKGUBUN.GetValue().ToString() == "2") // 일용직
            {
                TXT01_HIAMT.SetValue(Convert.ToString(Math.Floor(Decimal.Parse(TXT01_HIWKMAN.GetValue().ToString().Trim()) * Decimal.Parse(TXT01_HIDANGA.GetValue().ToString().Trim()))));                
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

        #region Description : 단가 텍스트 이벤트
        private void TXT01_HIDANGA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.MTB01_HISTTIME.SetValue("");
            this.MTB01_HIEDTIME.SetValue("");
            this.TXT01_HIJYTIME.SetValue("");
            this.TXT01_HIWKQTY.SetValue("");
            this.TXT01_HIWKMAN.SetValue("");
            this.CBO01_HIWKGUBUN.SetValue("1");
            this.TXT01_HIADD.SetValue("");
            this.TXT01_HIBIGO.SetValue("");
            this.DTP01_HIJYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.TXT01_HIDANGA.SetValue("");
            this.TXT01_HIAMT.SetValue("");
        }
        #endregion
    }
}
