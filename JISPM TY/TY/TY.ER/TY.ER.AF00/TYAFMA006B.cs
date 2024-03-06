using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 자금수지 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.11.14 18:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_GB_3B725225 : EIS 계열사 자금생성SP(TH)
    ///  TY_P_GB_3BD91269 : EIS 계열사 자금생성SP(TS)
    ///  TY_P_GB_3BE5Z303 : EIS 계열사 자금생성SP(TG)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    ///  EFSUBGN : 계열사구분
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYAFMA006B : TYBase
    {
        private string fsCompany = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA006B(string sCompany)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsCompany = sCompany;
        }

        private void TYAFMA006B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            CBH01_EFSUBGN.SetValue(fsCompany);
            CBH01_EFSUBGN.SetReadOnly(true);   

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(this.DTP01_GSTYYMM);

        }
        #endregion

        #region  Description : 자금생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            try
            {
                //삭제                
                if (fsCompany == "TG")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BFAN309", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3BFAN310", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();
                }
                //미확정분 삭제                    
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A13T941", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.ExecuteNonQuery();


                if (this.CBO01_GOKCR.GetValue().ToString() == "A")
                {

                    this.DbConnector.CommandClear();
                    if (fsCompany == "TH") //태영호라이즌
                    {
                        this.DbConnector.Attach("TY_P_GB_3B725225", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sOUTMSG.ToString());
                    }
                    if (fsCompany == "TS") //태영gls
                    {
                        this.DbConnector.Attach("TY_P_GB_3BD91269", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sOUTMSG.ToString());
                    }
                    if (fsCompany == "TG") //태영그레인
                    {
                        this.DbConnector.Attach("TY_P_GB_3BE5Z303", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sOUTMSG.ToString());
                    }

                    if (fsCompany == "TG")
                    {
                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        if (sOUTMSG.Trim().Substring(0, 1) == "I")
                        {
                            UP_TG_FoundFixData( this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6) );

                            this.ShowMessage("TY_M_GB_26E30875");
                        }
                        else
                        {
                            this.ShowMessage("TY_M_GB_26E31876");
                        }
                    }
                    else
                    {
                        sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                        this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }

                }
                else
                {
                    this.ShowMessage("TY_M_AC_2CDB1167");
                }
            }
            catch(Exception ex)
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
        }
        #endregion

        #region  Description : 태영그레인 자금생성 확정분 생성
        private void UP_TG_FoundFixData(string sYYMM)
        {
            string sGubun = string.Empty;

            string sEAFSUBGN = "TG";
            string sEAFYYMM = sYYMM;
            string sEAFSEQN = string.Empty;
            string sEAFTINM = string.Empty;
            string sEAFLEVE = string.Empty;
            double dEAFSAMM = 0;
            double dEAFNEMM = 0;            

            double dJunWolTotal = 0;

            double dIPTotal  = 0;
            double dCHTotal  = 0;

            double dChaOutTotal = 0;
            double dChaUpTotal  = 0;
            double dUpTotal = 0;

            DateTime dTime = new DateTime();
            dTime = Convert.ToDateTime(sYYMM.ToString().Substring(0, 4) + "-" + sYYMM.ToString().Substring(4, 2) + "-01");
            dTime = dTime.AddMonths(-1);
            string sDate = dTime.Year.ToString() + Set_Fill2(dTime.Month.ToString()) + Set_Fill2(dTime.Day.ToString());

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
 
            //192.168.100.8 -> 192.168.100.2 이관
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BF90305");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                dEAFNEMM = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sGubun = dt.Rows[i]["EDCODE"].ToString().Substring(0,1);

                    sEAFSEQN = dt.Rows[i]["EDCODE"].ToString();
                    sEAFTINM = dt.Rows[i]["EDDESC1"].ToString();
                    

                    if (sGubun == "1") //전월이월
                    {
                        sEAFLEVE = "1";
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9E306", sEAFSUBGN, sYYMM);
                        dJunWolTotal = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        dEAFSAMM = dJunWolTotal;                        
                    }

                    if (sGubun == "2") //수입
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "1");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //수입 누적
                        dIPTotal = dIPTotal + dEAFSAMM;
                    }

                    if (sGubun == "3") //지출
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "2");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //지출 누적
                        dCHTotal = dCHTotal + dEAFSAMM;
                    }

                    if (sGubun == "4") //차입금상환
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "3");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //차입금상환 누적
                        dChaOutTotal = dChaOutTotal + dEAFSAMM;
                    }

                    if (sGubun == "5") //차입금증가
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "4");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));

                        //차입금상환 누적
                        dChaUpTotal = dChaUpTotal + dEAFSAMM;
                    }

                    if (sGubun == "6") //증자
                    {
                        sEAFLEVE = "3";

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_3BF9N308", sEAFSUBGN, sYYMM, sEAFSEQN, "5");
                        dEAFSAMM = Convert.ToDouble(Get_Numeric(this.DbConnector.ExecuteScalar().ToString()));
                        
                        dUpTotal = dUpTotal + dEAFSAMM;
                    }

                    datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });

                }//for..end

                //수입누계
                sEAFSEQN = "2999";
                sEAFTINM = "수입계";
                sEAFLEVE = "2";
                dEAFSAMM = dIPTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //지출누계
                sEAFSEQN = "3999";
                sEAFTINM = "지출계";
                sEAFLEVE = "2";
                dEAFSAMM = dCHTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //자금과부족
                sEAFSEQN = "4000";
                sEAFTINM = "자금과부족";
                sEAFLEVE = "1";
                dEAFSAMM = dJunWolTotal + dIPTotal - dCHTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                //차월이월금 = 자금과부족 - 차입금상환 + 차입금증가 + 증자
                sEAFSEQN = "9999";
                sEAFTINM = "차월이월금";
                sEAFLEVE = "1";
                dEAFSAMM = (dJunWolTotal + dIPTotal - dCHTotal) - dChaOutTotal + dChaUpTotal + dUpTotal;
                datas.Add(new object[] {sEAFSUBGN,  //1
                                            sEAFYYMM,  //2
                                            sEAFSEQN,  //3
                                            sEAFTINM,  //4
                                            sEAFLEVE,  //5
                                            dEAFSAMM.ToString(),  //6
                                            dEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_3A12K936", data);
                }
            }
            this.DbConnector.ExecuteTranQueryList();

        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 확정 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //마감월이후 작업금지   
            /*  
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C92V659",  // TY_P_AC_27H64059
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4),
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            //해당월 확정시 작업금지
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A13B938", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));

            Int32 iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

            if (iCnt > 0)
            {
                this.ShowMessage("TY_M_GB_3A82W005");
                e.Successed = false;
                return;
            }
            */


            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {              

                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }

            }
            else
            {
                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion
    }
}
