using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 일괄 승인전표 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.05.15 13:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24IAO743 : 승인처리 SP CALL
    ///  TY_P_AC_25FAE441 : 일괄승인(미승인전표 조회)
    ///  TY_P_AC_25FAF442 : 일괄승인(승인전표 조회)
    ///  TY_P_AC_25F3S461 : 일괄승인 (처리전 체크 처리)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25FA8438 : 일괄 승인 (미승인전표 ,승인전표 조회)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2584X095 : 승인자를 확인하세요
    ///  TY_M_AC_2584Y096 : 승인일자를 확인하세요
    ///  TY_M_AC_2585M104 : 작성부서를 확인하세요
    ///  TY_M_AC_2585M105 : 작성일자를 확인하세요
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CONFIRM : 확인
    ///  B1NOEM : 승인자
    ///  CNDB2DPMK : 전표번호_부서
    ///  GAPPGB : 작업구분
    ///  CNDB2DTMK : 전표번호_일자
    /// </summary>
    public partial class TYACBJ003B : TYBase
    {
        private string sSABUN = string.Empty;

        
        public TYACBJ003B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACBJ003B_Load(object sender, System.EventArgs e)
        {
            // 로그인 사번 가져오기
            this.sSABUN = TYUserInfo.EmpNo.Trim().ToUpper();

            this.CBH01_B1NOEM.SetValue(TYUserInfo.EmpNo.Trim().ToUpper());

            this.DTP01_CNDB2DTMK.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH01_CNDB2DPMK.DummyValue = this.DTP01_CNDB2DTMK.GetString();

            this.BTN61_BATCH.Visible = false;
        } 
        #endregion


        #region Description : 처리
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            string sPRDATE = string.Empty;
            string sPRSABUN = string.Empty;
            string sPRGUBUN = string.Empty;
            string sOUTMSG = string.Empty;

            double dB2TOTAL = 0;

            int iTotalCnt = 0;
            int iRowCnt = 0;

            sPRSABUN = this.CBH01_B1NOEM.GetValue().ToString().Trim();

            if (this.CKB01_GAPPGB.GetValue().ToString() == "Y")
            {
                sPRGUBUN = "INS"; // 승인처리
            }
            else
            {
                sPRGUBUN = "DEL"; // 승인취소
            }

            DataTable dt = FPS91_TY_S_AC_25FA8438.GetDataSourceInclude(TSpread.TActionType.Select, "B1DPMK", "B1DTMK", "B1NOSQ", "B1DTLI","B1TTJP");

            if (dt.Rows.Count != 0)
            {
                iTotalCnt = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sB2DPMK = dt.Rows[i]["B1DPMK"].ToString(); // 부서
                    sB2DTMK = dt.Rows[i]["B1DTMK"].ToString(); // 일자
                    sB2NOSQ = dt.Rows[i]["B1NOSQ"].ToString(); // 번호
                    sPRDATE = dt.Rows[i]["B1DTLI"].ToString(); // 승인일자

                    if (sB2NOSQ != "")
                    {
                        // 승인처리(ADSLACF,AHSLACF 생성)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_24IAO743", sB2DPMK, sB2DTMK, sB2NOSQ, sPRDATE, sPRSABUN, sPRGUBUN, sOUTMSG); // SP CALL
                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

                        if (sOUTMSG.Substring(0, 2) != "ER")
                        {
                            iRowCnt += 1;

                            dB2TOTAL += Convert.ToDouble(dt.Rows[i]["B1TTJP"].ToString());
                        }
                    }
                }

                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    if (this.CKB01_GAPPGB.GetValue().ToString() == "Y")
                    {
                        sOUTMSG = "승인 처리되었습니다.";
                    }
                    else
                    {
                        sOUTMSG = "승인취소 처리되었습니다.";
                    }

                    this.ShowCustomMessage("총 금액:" + Set_Numeric2(dB2TOTAL.ToString(), 0) + " - 총 건수:" + iTotalCnt.ToString() + "건 - 완료건수:" + iRowCnt + "건이 " + sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);

                    this.BTN61_CONFIRM_Click(null, null);
                }

            }

        } 
        #endregion

        #region Description : 처리시 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            string sMaesgRt = string.Empty;
            string sJPNO = string.Empty;

            if (this.CKB01_GAPPGB.GetValue().ToString() == "Y")
            {
                DataTable dt = FPS91_TY_S_AC_25FA8438.GetDataSourceInclude(TSpread.TActionType.Select, "B1DPMK", "B1DTMK", "B1NOSQ");

                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sB2DPMK = dt.Rows[i]["B1DPMK"].ToString(); // 부서
                        sB2DTMK = dt.Rows[i]["B1DTMK"].ToString(); // 일자
                        sB2NOSQ = dt.Rows[i]["B1NOSQ"].ToString(); // 번호

                        if (sB2NOSQ != "")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_25F3S461", sB2DPMK, sB2DTMK, sB2NOSQ, sB2DPMK, sB2DTMK, sB2NOSQ);
                            DataTable dt1 = this.DbConnector.ExecuteDataTable();

                            if (dt1.Rows.Count != 0)
                            {
                                string sB1CNLN = dt1.Rows[0]["B1CNLN"].ToString();
                                string sB1IDJP = dt1.Rows[0]["B1IDJP"].ToString();
                                string sB1TTJP = dt1.Rows[0]["B1TTJP"].ToString();
                                string sB2CNT = dt1.Rows[0]["CNT"].ToString();
                                string sB2AMDR = dt1.Rows[0]["B2AMDR"].ToString();
                                string sB2AMCR = dt1.Rows[0]["B2AMCR"].ToString();

                                sJPNO = sB2DPMK + " - " + sB2DTMK + " - " + sB2NOSQ;

                                //헤더파일과 내역파일이 라인수가 틀린경우
                                if (Int32.Parse(sB1CNLN) != Int32.Parse(sB2CNT))
                                {
                                    sMaesgRt = "미승인전표 헤드와 내역 자료가 일치하지 않습니다 " + sJPNO;
                                    this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    this.SetFocus(this.CBH01_CNDB2DPMK);

                                    e.Successed = false;
                                    return;
                                }
                                //대체전표
                                if (sB1IDJP.Trim() == "3")
                                {
                                    //헤더파일합계금액과 내역파일 합계금액이 틀린경우(차변)
                                    if (Convert.ToDouble(sB1TTJP) != Convert.ToDouble(sB2AMDR))
                                    {
                                        sMaesgRt = "미승인전표 헤드와 내역 자료가 일치하지 않습니다(B2AMDR) " + sJPNO;
                                        this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        this.SetFocus(this.CBH01_CNDB2DPMK);

                                        e.Successed = false;
                                        return;
                                    }
                                    //헤더파일합계금액과 내역파일 합계금액이 틀린경우 (대변)
                                    if (Convert.ToDouble(sB1TTJP) != Convert.ToDouble(sB2AMCR))
                                    {
                                        sMaesgRt = "미승인전표 헤드와 내역 자료가 일치하지 않습니다(sB2AMCR) " + sJPNO;
                                        this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        this.SetFocus(this.CBH01_CNDB2DPMK);

                                        e.Successed = false;
                                        return;
                                    }
                                    //미승인 차대변 내역 자료 합계금액이 틀린경우
                                    if (Convert.ToDouble(sB2AMDR) != Convert.ToDouble(sB2AMCR))
                                    {
                                        sMaesgRt = "미승인 차대변 내역 자료가 일치하지 않습니다(sB2AMCR<>B2AMDR) " + sJPNO;
                                        this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        this.SetFocus(this.CBH01_CNDB2DPMK);

                                        e.Successed = false;
                                        return;
                                    }

                                }
                                else  //1,2 입금,출금전표
                                {
                                    if (Convert.ToDouble(sB1TTJP) != Convert.ToDouble(sB2AMDR) &&
                                        Convert.ToDouble(sB1TTJP) != Convert.ToDouble(sB2AMCR))
                                    {
                                        sMaesgRt = "미승인 차대변 내역 자료가 일치하지 않습니다(sB2AMCR<>B2AMDR) " + sJPNO;
                                        this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        this.SetFocus(this.CBH01_CNDB2DPMK);

                                        e.Successed = false;
                                        return;
                                    }
                                }
                            }
                        }

                    } // End .. From

                    if (!this.ShowMessage("TY_M_AC_59H90849"))
                    {
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_AC_25F59464");
                    this.SetFocus(this.CBH01_CNDB2DPMK);

                    e.Successed = false;
                    return;
                }   // (dt != null)

            }
            else // 취소 처리
            {
                DataTable dt_cn = FPS91_TY_S_AC_25FA8438.GetDataSourceInclude(TSpread.TActionType.Select, "B1DPMK", "B1DTMK", "B1NOSQ");

                if (dt_cn.Rows.Count != 0)
                {
                    for (int i = 0; i < dt_cn.Rows.Count; i++)
                    {
                        sB2DPMK = dt_cn.Rows[i]["B1DPMK"].ToString(); // 부서
                        sB2DTMK = dt_cn.Rows[i]["B1DTMK"].ToString(); // 일자
                        sB2NOSQ = dt_cn.Rows[i]["B1NOSQ"].ToString(); // 번호

                        sJPNO = sB2DPMK + sB2DTMK + Set_Fill3(sB2NOSQ) + "00";

                        if (sB2NOSQ != "")
                        {
                            this.DbConnector.CommandClear();  // AHSLACF TY_P_AC_3253S019
                            this.DbConnector.Attach("TY_P_AC_3253S019", sJPNO);
                            DataTable dt_d = this.DbConnector.ExecuteDataTable();
                            if (dt_d.Rows.Count != 0)
                            {
                                string sB3DTCE = dt_d.Rows[0]["B3DTCE"].ToString().Trim();
                                if (Int32.Parse(sB3DTCE) > 0)
                                {
                                    sJPNO = sB2DPMK + " - " + sB2DTMK + " - " + sB2NOSQ;
                                    sMaesgRt = "POSTING 완료 자료이므로 취소할 수 없습니다" + sJPNO;

                                    this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    this.SetFocus(this.CBH01_CNDB2DPMK);
                                    e.Successed = false;
                                }
                            }
                            else
                            {
                                sJPNO = sB2DPMK + " - " + sB2DTMK + " - " + sB2NOSQ;
                                sMaesgRt = "승인전표 헤드파일 자료가 존재하지 않습니다" + sJPNO;

                                this.ShowCustomMessage(sMaesgRt, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                this.SetFocus(this.CBH01_CNDB2DPMK);
                                e.Successed = false;
                            }
                        }
                    } // End .. From

                    if (!this.ShowMessage("TY_M_AC_34UC7579"))
                    {
                        e.Successed = false;
                        return;
                    }

                } // End ..dt_cn.Rows.Count != 0
                else
                {
                    this.ShowMessage("TY_M_AC_25F59464");
                    this.SetFocus(this.CBH01_CNDB2DPMK);

                    e.Successed = false;
                    return;
                }   // (dt != null)

            } // End..... CKB01_GAPPGB.GetValue().ToString() == "Y"

        }
        #endregion

        #region Description : 확인 버튼 처리
        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sDTLI = string.Empty;

            sB2DPMK = this.CBH01_CNDB2DPMK.GetValue().ToString().Trim();
            sB2DTMK = this.DTP01_CNDB2DTMK.GetString().ToString().Trim();
            sDTLI = this.DTP01_CNDB2DTMK.GetString().ToString().Trim();
 
            this.DbConnector.CommandClear();

            if (CKB01_GAPPGB.GetValue().ToString() == "Y")
            {
                this.DbConnector.Attach("TY_P_AC_25FAE441", sDTLI, sB2DPMK, sB2DTMK); // 미승인전표 조회 (승인할 내용)
                this.CBH01_B1NOEM.SetValue(sSABUN);
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_25FAF442", sB2DPMK, sB2DTMK); // 승인전표 조회 (승인취소할 내용)
            }

            this.FPS91_TY_S_AC_25FA8438.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_25FA8438.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                this.BTN61_BATCH.Visible = false;
            }
            else
            {
                this.BTN61_BATCH.Visible = true;

                if (this.FPS91_TY_S_AC_25FA8438.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_25FA8438, "B1DPMK", "합   계", SumRowType.Sum, "B1TTJP");              
                }

            }
        } 
        #endregion

        private void CKB01_GAPPGB_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.CKB01_GAPPGB.GetValue().ToString().Trim() == "Y")
            {
                this.CKB01_GAPPGB.Text = "승인처리";
            }
            else
            {
                this.CKB01_GAPPGB.Text = "승인취소";
            }
        }

        private void DTP01_CNDB2DTMK_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_CNDB2DPMK.DummyValue = this.DTP01_CNDB2DTMK.GetString();
        }


    }
}
