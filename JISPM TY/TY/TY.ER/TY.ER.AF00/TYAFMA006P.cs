using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 자금수지 확정 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.10.08 13:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3A81I998 : EIS 계열사 자금수지 조회[미확정분]
    ///  TY_P_AC_3A81K001 : EIS 계열사 자금수지 삭제[확정분]
    ///  TY_P_AC_3A81K999 : EIS 계열사 자금수지 등록[확정분]
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_3A81A996 : 확정 하시겠습니까?
    ///  TY_M_GB_3A81B997 : 확정하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYAFMA006P : TYBase
    {
        private string fsCompany = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA006P(string sCompany)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsCompany = sCompany;
        }

        private void TYAFMA006P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 확정 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            double dEAFSAMMWon = 0;
            double dEAFNEMMWon = 0;

            double dEAFSAMM = 0;
            double dEAFNEMM = 0;


            double dCHAIP_IN1 = 0; //차입금 상환
            double dCHAIP_IN2 = 0; //차입금 상환
            double dCHAIP_UP1 = 0;  //차입금 증가
            double dCHAIP_UP2 = 0;  //차입금 증가

            double dUPFUND1 = 0;  //증자
            double dUPFUND2 = 0;  //증자
            
            double dJUNWOLTOTAL1 = 0; //전월이월
            double dIPTOTAL1 = 0; //수입계
            double dCHTOTAL1 = 0; //지출계

            double dJUNWOLTOTAL2 = 0; //전월이월(예상)
            double dIPTOTAL2 = 0; //수입계(예상)
            double dCHTOTAL2 = 0; //지출계(예상)

            double dLastEAFSAMM = 0;
            double dLastEAFNEMM = 0;
            
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();



            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A81K001", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A81I998", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //원단위 금액
                        dEAFSAMMWon = Convert.ToDouble(dt.Rows[i]["EAFSAMM"].ToString());
                        dEAFNEMMWon = Convert.ToDouble(dt.Rows[i]["EAFNEMM"].ToString());

                        //100만원으로 정리
                        dEAFSAMM = Math.Floor(Convert.ToDouble(dt.Rows[i]["EAFSAMM"].ToString()) / 1000000) * 1000000;
                        dEAFNEMM = Math.Floor(Convert.ToDouble(dt.Rows[i]["EAFNEMM"].ToString()) / 1000000) * 1000000;

                        dLastEAFSAMM = dEAFSAMM;
                        dLastEAFNEMM = dEAFNEMM;

                        if (dt.Rows[i]["EAFSEQN"].ToString() == "1000")  //전월이월
                        {
                            dJUNWOLTOTAL1 = dEAFSAMM;
                            dJUNWOLTOTAL2 = dEAFNEMM;
                        }

                        if (dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //원가 row
                        {
                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "2" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //수입
                            {
                                dIPTOTAL1 = dIPTOTAL1 + dEAFSAMMWon;
                                dIPTOTAL2 = dIPTOTAL2 + dEAFNEMMWon;
                            }

                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //지출
                            {
                                dCHTOTAL1 = dCHTOTAL1 + dEAFSAMMWon;
                                dCHTOTAL2 = dCHTOTAL2 + dEAFNEMMWon;
                            }

                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 상환
                            {
                                dCHAIP_IN1 = dCHAIP_IN1 + dEAFSAMMWon;
                                dCHAIP_IN2 = dCHAIP_IN2 + dEAFNEMMWon;
                            }

                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "5" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //차입금 증가
                            {
                                dCHAIP_UP1 = dCHAIP_UP1 + dEAFSAMMWon;
                                dCHAIP_UP2 = dCHAIP_UP2 + dEAFNEMMWon;
                            }


                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "6" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "3")  //증자
                            {
                                dUPFUND1 = dUPFUND1 + dEAFSAMMWon;
                                dUPFUND2 = dUPFUND2 + dEAFNEMMWon;
                            }

                        }
                        else //집계 row
                        {
                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "2" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //수입계
                            {
                                dLastEAFSAMM = Math.Floor(dIPTOTAL1 / 1000000) * 1000000;
                                dLastEAFNEMM = Math.Floor(dIPTOTAL2 / 1000000) * 1000000;
                            }

                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "3" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "2")  //지출계
                            {
                                dLastEAFSAMM = Math.Floor(dCHTOTAL1 / 1000000) * 1000000;
                                dLastEAFNEMM = Math.Floor(dCHTOTAL2 / 1000000) * 1000000;
                            }
                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "4" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //자금과부족
                            {

                                dLastEAFSAMM = dJUNWOLTOTAL1 + (Math.Floor(dIPTOTAL1 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL1 / 1000000) * 1000000);
                                dLastEAFNEMM = dJUNWOLTOTAL2 + (Math.Floor(dIPTOTAL2 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL2 / 1000000) * 1000000);
                            }

                            if (dt.Rows[i]["EAFSEQN"].ToString().Substring(0, 1) == "9" && dt.Rows[i]["EAFLEVE"].ToString().Substring(0, 1) == "1")  //차월이월금
                            {
                                double dAmt1 = dJUNWOLTOTAL1 + (Math.Floor(dIPTOTAL1 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL1 / 1000000) * 1000000);
                                double dAmt2 = dJUNWOLTOTAL2 + (Math.Floor(dIPTOTAL2 / 1000000) * 1000000) - (Math.Floor(dCHTOTAL2 / 1000000) * 1000000);

                                dLastEAFSAMM = dAmt1 - (Math.Floor(dCHAIP_IN1 / 1000000) * 1000000) + (Math.Floor(dCHAIP_UP1 / 1000000) * 1000000) + (Math.Floor(dUPFUND1 / 1000000) * 1000000);
                                dLastEAFNEMM = dAmt2 - (Math.Floor(dCHAIP_IN2 / 1000000) * 1000000) + (Math.Floor(dCHAIP_UP2 / 1000000) * 1000000) + (Math.Floor(dUPFUND2 / 1000000) * 1000000);
                            }
                        }

                        datas.Add(new object[] {dt.Rows[i]["EAFSUBGN"].ToString(),  //1
                                            dt.Rows[i]["EAFYYMM"].ToString(),  //2
                                            dt.Rows[i]["EAFSEQN"].ToString(),  //3
                                            dt.Rows[i]["EAFTINM"].ToString(),  //4
                                            dt.Rows[i]["EAFLEVE"].ToString(),  //5
                                            dLastEAFSAMM.ToString(),  //6
                                            dLastEAFNEMM.ToString(),  //7
                                            ""   //8
                                           });
                    }

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_3A81K999", data);
                        }
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_GB_3A81B997");
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A81K001", this.fsCompany, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 확정 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {               

                if (!this.ShowMessage("TY_M_GB_3A81A996"))
                {
                    e.Successed = false;
                    return;
                }

            }
            else
            {
                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion
    }
}
