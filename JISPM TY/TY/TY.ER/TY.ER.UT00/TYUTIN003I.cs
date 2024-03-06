using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 계약관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FFQ222 : 회계 거래처 코드의 거래처구분 체크
    ///  TY_P_UT_66FFV223 : 계약관리 확인
    ///  TY_P_UT_66FFW224 : 계약관리 수정
    ///  TY_P_UT_66FFY225 : 계약관리 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  CNCONTGB : 계약구분
    ///  CNCURRCD : 화폐구분
    ///  CNHWAJU : 화주
    ///  CNHWAMUL : 화물
    ///  CNBOHP : 보관료화폐
    ///  CNBOJDA : 보장물동단위
    ///  CNBOJHP : 보장물동화폐
    ///  CNBUDUHP : 부두사용료화폐
    ///  CNCANHP : CAN 화폐
    ///  CNCHDA : 출고단위
    ///  CNCHHP : 출고화폐
    ///  CNDRHP : DRUM 화폐
    ///  CNHANDDA : 취급료단위
    ///  CNHANDHP : 취급료화폐
    ///  CNIPDA : 입고단위
    ///  CNIPHP : 입고화폐
    ///  CNISDA : 이송단위
    ///  CNISHP : 이송화폐
    ///  CNJILHP : 질소충전비화폐
    ///  CNTOJIHP : 토지사용료화폐
    ///  CNCONTEN : 계약종료일자
    ///  CNCONTIL : 계약일자
    ///  CNCONTST : 계약시작일자
    ///  CNBOAM : 보관료
    ///  CNBOJAM : 보장물동금액
    ///  CNBOJMON : 보장물동월수
    ///  CNBOJPER : 보장물동퍼센트
    ///  CNBOJQTY : 보장물동수량
    ///  CNBUDU : 부두사용료
    ///  CNCANAM : CAN 금액
    ///  CNCAPA : 용량
    ///  CNCHAM : 출고금액
    ///  CNCONTNO : 계약번호
    ///  CNDRAM : DRUM 금액
    ///  CNHANDAM : 취급료
    ///  CNHANDOV : 취급료할증율
    ///  CNHAYKOV : 하역료할증율
    ///  CNIPAM : 입고금액
    ///  CNISAM : 이송금액
    ///  CNJILSO : 질소충전비
    ///  CNRATE : 환율
    ///  CNREQGB : 청구구분
    ///  CNSHOTA : 자연감모
    ///  CNTANKNO : 탱크번호
    ///  CNTOJI : 토지사용료
    /// </summary>
    public partial class TYUTIN003I : TYBase
    {
        private string fsCNCONTNO;

        #region Description : 페이지 로드
        public TYUTIN003I(string sCNCONTNO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsCNCONTNO = sCNCONTNO;
        }

        private void TYUTIN003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsCNCONTNO))
            {
                this.TXT01_CNYEAR.SetValue(DateTime.Now.ToString("yyyy"));

                this.TXT01_CNYEAR.SetReadOnly(false);
                this.TXT01_CNSEQ.SetReadOnly(true);

                SetStartingFocus(this.TXT01_CNYEAR);
            }
            else
            {
                this.TXT01_CNYEAR.SetReadOnly(true);
                this.TXT01_CNSEQ.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66FFV223", this.fsCNCONTNO);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                SetStartingFocus(this.CBH01_CNHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.fsCNCONTNO = "";

            this.TXT01_CNYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.TXT01_CNSEQ.SetValue("");

            this.TXT01_CNYEAR.SetReadOnly(false);
            this.TXT01_CNSEQ.SetReadOnly(true);

            this.TXT01_CNYEAR.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sCNCONTNO = string.Empty;

            sCNCONTNO = this.TXT01_CNYEAR.GetValue().ToString().Trim() + "-" + Set_Fill3(this.TXT01_CNSEQ.GetValue().ToString().Trim());

            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsCNCONTNO))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66FFY225",
                                        sCNCONTNO.ToString(),                                   // 1=계약번호        
                                        this.CBH01_CNHWAJU.GetValue().ToString(),               // 2=화주            
                                        this.DTP01_CNCONTIL.GetValue(),                         // 3=계약일자        
                                        this.TXT01_CNCAPA.GetValue().ToString(),                // 4=용량            
                                        Set_TankNo(this.TXT01_CNTANKNO.GetValue().ToString()),  // 5=탱크번호        
                                        this.CBH01_CNHWAMUL.GetValue().ToString(),              // 6=화물            
                                        this.CBO01_CNREQGB.GetValue().ToString(),               // 7=매출청구구분    
                                        this.CBH01_CNCONTGB.GetValue().ToString(),              // 8=계약구분        
                                        this.DTP01_CNCONTST.GetValue(),                         // 9=계약시작일자    
                                        this.DTP01_CNCONTEN.GetValue(),                         // 10=계약종료일자   
                                        Get_Numeric(this.TXT01_CNRATE.GetValue().ToString()),   // 11=환율           
                                        Get_Numeric(this.TXT01_CNHAYKOV.GetValue().ToString()), // 12=하역료할증율   
                                        Get_Numeric(this.TXT01_CNBOAM.GetValue().ToString()),   // 13=보관료         
                                        this.CBO01_CNBOHP.GetValue().ToString(),                // 14=보관료화폐     
                                        Get_Numeric(this.TXT01_CNIPAM.GetValue().ToString()),   // 15=입고금액       
                                        this.CBO01_CNIPDA.GetValue().ToString(),                // 16=입고단위       
                                        this.CBO01_CNIPHP.GetValue().ToString(),                // 17=입고화폐       
                                        Get_Numeric(this.TXT01_CNCHAM.GetValue().ToString()),   // 18=출고금액       
                                        this.CBO01_CNCHDA.GetValue().ToString(),                // 19=출고단위       
                                        this.CBO01_CNCHHP.GetValue().ToString(),                // 20=출고화폐       
                                        Get_Numeric(this.TXT01_CNISAM.GetValue().ToString()),   // 21=이송금액       
                                        this.CBO01_CNISDA.GetValue().ToString(),                // 22=이송단위       
                                        this.CBO01_CNISHP.GetValue().ToString(),                // 23=이송화폐       
                                        Get_Numeric(this.TXT01_CNCANAM.GetValue().ToString()),  // 24=CAN금액        
                                        this.CBO01_CNCANHP.GetValue().ToString(),               // 25=CAN화폐        
                                        Get_Numeric(this.TXT01_CNDRAM.GetValue().ToString()),   // 26=DRUM금액       
                                        this.CBO01_CNDRHP.GetValue().ToString(),                // 27=DRUM화폐       
                                        Get_Numeric(this.TXT01_CNHANDOV.GetValue().ToString()), // 28=취급료할증율   
                                        Get_Numeric(this.TXT01_CNHANDAM.GetValue().ToString()), // 29=취급료         
                                        this.CBO01_CNHANDDA.GetValue().ToString(),              // 30=취급료단위     
                                        this.CBO01_CNHANDHP.GetValue().ToString(),              // 31=취급료화폐     
                                        Get_Numeric(this.TXT01_CNSHOTA.GetValue().ToString()),  // 32=자연감모       
                                        Get_Numeric(this.TXT01_CNBOJMON.GetValue().ToString()), // 33=보장물동월수   
                                        Get_Numeric(this.TXT01_CNBOJPER.GetValue().ToString()), // 34=보장물동퍼센트 
                                        Get_Numeric(this.TXT01_CNBOJQTY.GetValue().ToString()), // 35=보장물동량     
                                        this.CBO01_CNBOJDA.GetValue().ToString(),               // 36=보장물동단위   
                                        Get_Numeric(this.TXT01_CNBOJAM.GetValue().ToString()),  // 37=보장물동금액   
                                        this.CBO01_CNBOJHP.GetValue().ToString(),               // 38=보장물동화폐   
                                        Get_Numeric(this.TXT01_CNBUDU.GetValue().ToString()),   // 39=부두사용료     
                                        this.CBO01_CNBUDUHP.GetValue().ToString(),              // 40=부두사용료화폐 
                                        Get_Numeric(this.TXT01_CNTOJI.GetValue().ToString()),   // 41=토지사용료     
                                        this.CBO01_CNTOJIHP.GetValue().ToString(),              // 42=토지사용료화폐 
                                        Get_Numeric(this.TXT01_CNJILSO.GetValue().ToString()),  // 43=질소충전비     
                                        this.CBO01_CNJILHP.GetValue().ToString(),               // 44=질소충전비화폐 
                                        this.CBH01_CNCURRCD.GetValue().ToString(),              // 45=외화화폐,
                                        this.TXT01_CNVOCAMT.GetValue().ToString(),              // 46=운영비
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66FFW224",
                                        this.CBH01_CNHWAJU.GetValue().ToString(),               // 1=화주            
                                        this.DTP01_CNCONTIL.GetValue(),                         // 2=계약일자        
                                        this.TXT01_CNCAPA.GetValue().ToString(),                // 3=용량            
                                        Set_TankNo(this.TXT01_CNTANKNO.GetValue().ToString()),  // 4=탱크번호        
                                        this.CBH01_CNHWAMUL.GetValue().ToString(),              // 5=화물            
                                        this.CBO01_CNREQGB.GetValue().ToString(),               // 6=매출청구구분    
                                        this.CBH01_CNCONTGB.GetValue().ToString(),              // 7=계약구분        
                                        this.DTP01_CNCONTST.GetValue(),                         // 8=계약시작일자    
                                        this.DTP01_CNCONTEN.GetValue(),                         // 9=계약종료일자   
                                        Get_Numeric(this.TXT01_CNRATE.GetValue().ToString()),   // 10=환율           
                                        Get_Numeric(this.TXT01_CNHAYKOV.GetValue().ToString()), // 11=하역료할증율   
                                        Get_Numeric(this.TXT01_CNBOAM.GetValue().ToString()),   // 12=보관료         
                                        this.CBO01_CNBOHP.GetValue().ToString(),                // 13=보관료화폐     
                                        Get_Numeric(this.TXT01_CNIPAM.GetValue().ToString()),   // 14=입고금액       
                                        this.CBO01_CNIPDA.GetValue().ToString(),                // 15=입고단위       
                                        this.CBO01_CNIPHP.GetValue().ToString(),                // 16=입고화폐       
                                        Get_Numeric(this.TXT01_CNCHAM.GetValue().ToString()),   // 17=출고금액       
                                        this.CBO01_CNCHDA.GetValue().ToString(),                // 18=출고단위       
                                        this.CBO01_CNCHHP.GetValue().ToString(),                // 19=출고화폐       
                                        Get_Numeric(this.TXT01_CNISAM.GetValue().ToString()),   // 20=이송금액       
                                        this.CBO01_CNISDA.GetValue().ToString(),                // 21=이송단위       
                                        this.CBO01_CNISHP.GetValue().ToString(),                // 22=이송화폐       
                                        Get_Numeric(this.TXT01_CNCANAM.GetValue().ToString()),  // 23=CAN금액        
                                        this.CBO01_CNCANHP.GetValue().ToString(),               // 24=CAN화폐        
                                        Get_Numeric(this.TXT01_CNDRAM.GetValue().ToString()),   // 25=DRUM금액       
                                        this.CBO01_CNDRHP.GetValue().ToString(),                // 26=DRUM화폐       
                                        Get_Numeric(this.TXT01_CNHANDOV.GetValue().ToString()), // 27=취급료할증율   
                                        Get_Numeric(this.TXT01_CNHANDAM.GetValue().ToString()), // 28=취급료         
                                        this.CBO01_CNHANDDA.GetValue().ToString(),              // 29=취급료단위     
                                        this.CBO01_CNHANDHP.GetValue().ToString(),              // 30=취급료화폐     
                                        Get_Numeric(this.TXT01_CNSHOTA.GetValue().ToString()),  // 31=자연감모       
                                        Get_Numeric(this.TXT01_CNBOJMON.GetValue().ToString()), // 32=보장물동월수   
                                        Get_Numeric(this.TXT01_CNBOJPER.GetValue().ToString()), // 33=보장물동퍼센트 
                                        Get_Numeric(this.TXT01_CNBOJQTY.GetValue().ToString()), // 34=보장물동량     
                                        this.CBO01_CNBOJDA.GetValue().ToString(),               // 35=보장물동단위   
                                        Get_Numeric(this.TXT01_CNBOJAM.GetValue().ToString()),  // 36=보장물동금액   
                                        this.CBO01_CNBOJHP.GetValue().ToString(),               // 37=보장물동화폐   
                                        Get_Numeric(this.TXT01_CNBUDU.GetValue().ToString()),   // 38=부두사용료     
                                        this.CBO01_CNBUDUHP.GetValue().ToString(),              // 39=부두사용료화폐 
                                        Get_Numeric(this.TXT01_CNTOJI.GetValue().ToString()),   // 40=토지사용료     
                                        this.CBO01_CNTOJIHP.GetValue().ToString(),              // 41=토지사용료화폐 
                                        Get_Numeric(this.TXT01_CNJILSO.GetValue().ToString()),  // 42=질소충전비     
                                        this.CBO01_CNJILHP.GetValue().ToString(),               // 43=질소충전비화폐 
                                        this.CBH01_CNCURRCD.GetValue().ToString(),              // 44=외화화폐
                                        this.TXT01_CNVOCAMT.GetValue().ToString(),              // 45=운영비
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),           // 46=사용자
                                        sCNCONTNO.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();                        
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 사업자 번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66FFQ222", this.CBH01_CNHWAJU.GetValue());
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "6")
                {
                    if (this.CBO01_CNREQGB.GetValue().ToString() != "2")
                    {
                        this.ShowMessage("TY_M_UT_66FHW237");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (int.Parse(this.DTP01_CNCONTST.GetValue().ToString()) > int.Parse(this.DTP01_CNCONTEN.GetValue().ToString()))
            {
                this.ShowMessage("TY_M_UT_66G9B253");
                e.Successed = false;
                return;
            }

            if (this.CBO01_CNREQGB.GetValue().ToString() == "1") // 원화
            {
                this.CBO01_CNBOHP.SelectedIndex = 1;
                this.CBO01_CNCHHP.SelectedIndex = 1;
                this.CBO01_CNCANHP.SelectedIndex = 1;
                this.CBO01_CNHANDHP.SelectedIndex = 1;
                this.CBO01_CNIPHP.SelectedIndex = 1;
                this.CBO01_CNDRHP.SelectedIndex = 1;
                this.CBO01_CNISHP.SelectedIndex = 1;
                this.CBO01_CNBOJHP.SelectedIndex = 1;
                this.CBO01_CNBUDUHP.SelectedIndex = 1;
                this.CBO01_CNTOJIHP.SelectedIndex = 1;
                this.CBO01_CNJILHP.SelectedIndex = 1;

                this.CBH01_CNCURRCD.SetValue("");
            }
            else if (this.CBO01_CNREQGB.GetValue().ToString() == "1") // 원화
            {
                this.CBO01_CNBOHP.SelectedIndex = 2;
                this.CBO01_CNCHHP.SelectedIndex = 2;
                this.CBO01_CNCANHP.SelectedIndex = 2;
                this.CBO01_CNHANDHP.SelectedIndex = 2;
                this.CBO01_CNIPHP.SelectedIndex = 2;
                this.CBO01_CNDRHP.SelectedIndex = 2;
                this.CBO01_CNISHP.SelectedIndex = 2;
                this.CBO01_CNBOJHP.SelectedIndex = 2;
                this.CBO01_CNBUDUHP.SelectedIndex = 2;
                this.CBO01_CNTOJIHP.SelectedIndex = 2;
                this.CBO01_CNJILHP.SelectedIndex = 2;

                if (this.CBH01_CNCURRCD.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66FHY238");
                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNBOAM.GetValue().ToString())) == 0)
            {
                this.CBO01_CNBOHP.SelectedIndex = 0;
            }

            // 입고 취급료
            if (double.Parse(Get_Numeric(this.TXT01_CNIPAM.GetValue().ToString())) == 0)
            {
                if (this.CBO01_CNIPDA.GetValue().ToString() == "MT" || this.CBO01_CNIPDA.GetValue().ToString() == "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI1239");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.CBO01_CNIPHP.SelectedIndex = 0;
                }
            }
            else
            {
                if (this.CBO01_CNIPDA.GetValue().ToString() != "MT" && this.CBO01_CNIPDA.GetValue().ToString() != "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI1240");
                    e.Successed = false;
                    return;
                }
            }

            // 출고 취급료
            if (double.Parse(Get_Numeric(this.TXT01_CNCHAM.GetValue().ToString())) == 0)
            {
                if (this.CBO01_CNCHDA.GetValue().ToString() == "MT" || this.CBO01_CNCHDA.GetValue().ToString() == "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI4241");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.CBO01_CNCHHP.SelectedIndex = 0;
                }
            }
            else
            {
                if (this.CBO01_CNCHDA.GetValue().ToString() != "MT" && this.CBO01_CNCHDA.GetValue().ToString() != "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI5242");
                    e.Successed = false;
                    return;
                }
            }

            // 이송료
            if (double.Parse(Get_Numeric(this.TXT01_CNISAM.GetValue().ToString())) == 0)
            {
                if (this.CBO01_CNISDA.GetValue().ToString() == "MT" || this.CBO01_CNISDA.GetValue().ToString() == "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI7243");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.CBO01_CNISHP.SelectedIndex = 0;
                }
            }
            else
            {
                if (this.CBO01_CNISDA.GetValue().ToString() != "MT" && this.CBO01_CNISDA.GetValue().ToString() != "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI7244");
                    e.Successed = false;
                    return;
                }
            }


            if (double.Parse(Get_Numeric(this.TXT01_CNCANAM.GetValue().ToString())) == 0)
            {
                this.CBO01_CNCANHP.SelectedIndex = 0;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNDRAM.GetValue().ToString())) == 0)
            {
                this.CBO01_CNDRHP.SelectedIndex = 0;
            }

            // 취급료할증
            if (double.Parse(Get_Numeric(this.TXT01_CNHANDAM.GetValue().ToString())) == 0)
            {
                if (this.CBO01_CNHANDDA.GetValue().ToString() == "MT" || this.CBO01_CNHANDDA.GetValue().ToString() == "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI0245");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.CBO01_CNHANDHP.SelectedIndex = 0;
                }
            }
            else
            {
                if (this.CBO01_CNHANDDA.GetValue().ToString() != "MT" && this.CBO01_CNHANDDA.GetValue().ToString() != "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI1246");
                    e.Successed = false;
                    return;
                }
            }

            // 보장물동수량
            if (double.Parse(Get_Numeric(this.TXT01_CNBOJQTY.GetValue().ToString())) == 0)
            {
                if (this.CBO01_CNBOJDA.GetValue().ToString() == "MT" || this.CBO01_CNBOJDA.GetValue().ToString() == "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI2247");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.CBO01_CNBOJDA.GetValue().ToString() != "MT" && this.CBO01_CNBOJDA.GetValue().ToString() != "KL")
                {
                    this.ShowMessage("TY_M_UT_66FI3248");
                    e.Successed = false;
                    return;
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNBOJAM.GetValue().ToString())) == 0)
            {
                this.CBO01_CNBOJHP.SelectedIndex = 0;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNBUDU.GetValue().ToString())) == 0)
            {
                this.CBO01_CNBUDUHP.SelectedIndex = 0;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNTOJI.GetValue().ToString())) == 0)
            {
                this.CBO01_CNTOJIHP.SelectedIndex = 0;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CNJILSO.GetValue().ToString())) == 0)
            {
                this.CBO01_CNJILHP.SelectedIndex = 0;
            }

            if ((double.Parse(Get_Numeric(this.TXT01_CNBOAM.GetValue().ToString())) > 0) && (this.CBO01_CNBOHP.GetValue().ToString() == ""))
            {
                this.ShowMessage("TY_M_UT_66FIA249");
                e.Successed = false;
                return;
            }

            
            if (string.IsNullOrEmpty(this.fsCNCONTNO))
            {
                // 등록 - 순번 자동으로 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66G93252", this.TXT01_CNYEAR.GetValue().ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CNSEQ.SetValue(dt.Rows[0][0].ToString());
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 텍스트 박스 엔터키
        private void TXT01_CNCHAM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_CNCHDA);
            }
        }

        private void CBO01_CNCHDA_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_CNCHHP);
            }
        }

        private void CBO01_CNCHHP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_CNIPAM);
            }
        }

        private void TXT01_CNIPAM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_CNIPDA);
            }
        }

        private void CBO01_CNBOJDA_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_CNBOJHP);
            }
        }

        private void CBO01_CNBOJHP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_CNBUDU);
            }
        }

        private void TXT01_CNBUDU_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_CNBUDUHP);
            }
        }
        #endregion
    }
}
