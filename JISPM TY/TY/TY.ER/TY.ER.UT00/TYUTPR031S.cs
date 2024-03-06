using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.UT00
{
    /// <summary>
    /// 환적화물 관리대장 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.11.22 15:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_8BMFW222 : 환적화물 관리대장 조회(화주X)
    ///  TY_P_UT_8BMFX223 : 환적화물 관리대장 조회(화주O)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_8BMFY225 : 환적화물 관리대장 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  IPHWAJU : 화주
    ///  IPHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR031S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR031S()
        {
            InitializeComponent();
        }

        private void TYUTPR031S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.FPS91_TY_S_UT_8BMFY225.Initialize();

                this.DbConnector.CommandClear();

                if (CBH01_IPHWAJU.GetValue().ToString() != "")
                {
                    // 대표거래처 코드 가져오기
                    string sHWAJU = Get_VNCODE(this.CBH01_IPHWAJU.GetValue().ToString());

                    //this.DbConnector.Attach("TY_P_UT_8BMFX223",
                    this.DbConnector.Attach("TY_P_UT_8CC9M303",
                                            this.DTP01_STDATE.GetString(),
                                            this.DTP01_EDDATE.GetString(),
                                            sHWAJU,
                                            this.CBH01_IPHWAMUL.GetValue().ToString(),
                                            this.CBH01_IPHWAMULGB.GetValue().ToString()
                                            );
                }
                else
                {
                    //this.DbConnector.Attach("TY_P_UT_8BMFW222",
                    this.DbConnector.Attach("TY_P_UT_8CC9L302",
                                            this.DTP01_STDATE.GetString(),
                                            this.DTP01_EDDATE.GetString(),
                                            this.CBH01_IPHWAMUL.GetValue().ToString(),
                                            this.CBH01_IPHWAMULGB.GetValue().ToString()
                                            );
                }
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_8BMFY225.SetValue(UP_DatatableChange(dt));

                //if (dt.Rows.Count > 0)
                //{
                    // 특정 ROW 색깔 입히기
                    for (int i = 0; i < this.FPS91_TY_S_UT_8BMFY225.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_UT_8BMFY225.GetValue(i, "IPHWAJU").ToString() == "소  계")
                        {
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_UT_8BMFY225.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                        }
                        else if (this.FPS91_TY_S_UT_8BMFY225.GetValue(i, "IPHWAJU").ToString() == "합  계")
                        {
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_UT_8BMFY225.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                        }
                    }
                //}
            }
        }
        #endregion

        #region Description : 데이터 테이블 변경
        private DataTable UP_DatatableChange(DataTable dt)
        {
            DataTable dtRtn = new DataTable();
            DataRow row;

            double dIPBSQTYSUBTOT = 0;            
            double dIPBSQTYTOT = 0;
            double dCHJEQTYSUBTOT = 0;
            double dCHJEQTYTOT = 0;

            double dCHJEQTY = 0;
            double dCHMTQTY = 0;

            string sGUBUN = string.Empty;

            dtRtn.Columns.Add("CMBANIL", typeof(System.String));
            dtRtn.Columns.Add("IPHWAJU", typeof(System.String));
            dtRtn.Columns.Add("VNSANGHO", typeof(System.String));
            dtRtn.Columns.Add("IPHWAMUL", typeof(System.String));
            dtRtn.Columns.Add("HMDESC", typeof(System.String));
            dtRtn.Columns.Add("IPBLNO", typeof(System.String));
            dtRtn.Columns.Add("IPBSQTY", typeof(System.String));
            dtRtn.Columns.Add("IPSINO", typeof(System.String));
            dtRtn.Columns.Add("VSJUKHA", typeof(System.String));
            dtRtn.Columns.Add("IPMSNSEQ", typeof(System.String));
            dtRtn.Columns.Add("IPHSNSEQ", typeof(System.String));
            dtRtn.Columns.Add("CHCHULIL", typeof(System.String));
            dtRtn.Columns.Add("CHMTQTY", typeof(System.String));
            dtRtn.Columns.Add("CHJEQTY", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtRtn.NewRow();

                if (sGUBUN != dt.Rows[i]["GUBUN"].ToString())
                {
                    row["CMBANIL"] = dt.Rows[i]["CMBANIL"].ToString();
                    row["IPHWAJU"] = dt.Rows[i]["IPHWAJU"].ToString();
                    row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                    row["IPHWAMUL"] = dt.Rows[i]["IPHWAMUL"].ToString();
                    row["HMDESC"] = dt.Rows[i]["HMDESC"].ToString();
                    row["IPBLNO"] = dt.Rows[i]["IPBLNO"].ToString();
                    row["IPBSQTY"] = dt.Rows[i]["IPBSQTY"].ToString();
                    row["IPSINO"] = dt.Rows[i]["IPSINO"].ToString();
                    row["VSJUKHA"] = dt.Rows[i]["VSJUKHA"].ToString();
                    row["IPMSNSEQ"] = dt.Rows[i]["IPMSNSEQ"].ToString();
                    row["IPHSNSEQ"] = dt.Rows[i]["IPHSNSEQ"].ToString();
                    row["CHCHULIL"] = dt.Rows[i]["CHCHULIL"].ToString();
                    row["CHMTQTY"] = dt.Rows[i]["CHMTQTY"].ToString();

                    dCHMTQTY = Convert.ToDouble(dt.Rows[i]["CHMTQTY"].ToString());

                    if (dt.Rows[i]["IPHSNSEQ"].ToString() == "0")
                    {
                        //B/L 분할 포함한 출고량 조회
                        //this.DbConnector.CommandClear();

                        //this.DbConnector.Attach("TY_P_UT_8BRGF252",
                        //                        dt.Rows[i]["GUBUN2"].ToString()
                        //                        );

                        //DataTable dtTemp = this.DbConnector.ExecuteDataTable();

                        //if (dtTemp.Rows.Count > 0)
                        //{
                        //    dCHMTQTY = Convert.ToDouble(dtTemp.Rows[0]["CHMTQTY"].ToString());
                        //}
                        string sFilter = "GUBUN2  = '" + dt.Rows[i]["GUBUN2"].ToString() + "'";

                        dCHMTQTY = Convert.ToDouble(dt.Compute("Sum(CHMTQTY)", sFilter).ToString());
                    }

                    dCHJEQTY = Convert.ToDouble(dt.Rows[i]["IPBSQTY"].ToString()) - dCHMTQTY;

                    if (dCHJEQTY <= 15)
                    {
                        dCHJEQTY = 0;
                    }

                    row["CHJEQTY"] = dCHJEQTY;

                    sGUBUN = dt.Rows[i]["GUBUN"].ToString();

                    dIPBSQTYSUBTOT += Convert.ToDouble(dt.Rows[i]["IPBSQTY"].ToString());
                    dIPBSQTYTOT += Convert.ToDouble(dt.Rows[i]["IPBSQTY"].ToString());

                    dCHJEQTYSUBTOT += dCHJEQTY;
                    dCHJEQTYTOT += dCHJEQTY;
                }
                else
                {
                    row["CMBANIL"] = "";
                    row["IPHWAJU"] = "";
                    row["VNSANGHO"] = "";
                    row["IPHWAMUL"] = "";
                    row["HMDESC"] = "";
                    row["IPBLNO"] = "";
                    row["IPBSQTY"] = "";
                    row["IPSINO"] = "";
                    row["VSJUKHA"] = "";
                    row["IPMSNSEQ"] = "";
                    row["IPHSNSEQ"] = "";                    
                    row["CHCHULIL"] = dt.Rows[i]["CHCHULIL"].ToString();
                    row["CHMTQTY"] = dt.Rows[i]["CHMTQTY"].ToString();

                    dCHJEQTY = dCHJEQTY - Convert.ToDouble(dt.Rows[i]["CHMTQTY"].ToString());

                    if (dCHJEQTY <= 15)
                    {
                        dCHJEQTY = 0;
                    }

                    row["CHJEQTY"] = dCHJEQTY;

                    dCHJEQTYSUBTOT += dCHJEQTY;
                    dCHJEQTYTOT += dCHJEQTY;
                }

                dtRtn.Rows.Add(row);

                if (i + 1 < dt.Rows.Count)
                {
                    if (dt.Rows[i]["IPHWAJU"].ToString() != dt.Rows[i + 1]["IPHWAJU"].ToString())
                    {
                        row = dtRtn.NewRow();

                        row["CMBANIL"] = "";
                        row["IPHWAJU"] = "소  계";
                        row["VNSANGHO"] = "";
                        row["IPHWAMUL"] = "";
                        row["HMDESC"] = "";
                        row["IPBLNO"] = "";
                        row["IPBSQTY"] = dIPBSQTYSUBTOT;
                        row["IPSINO"] = "";
                        row["VSJUKHA"] = "";
                        row["IPMSNSEQ"] = "";
                        row["IPHSNSEQ"] = "";
                        row["CHCHULIL"] = "";
                        row["CHMTQTY"] = "";
                        row["CHJEQTY"] = dCHJEQTYSUBTOT;

                        dtRtn.Rows.Add(row);

                        dIPBSQTYSUBTOT = 0;
                        dCHJEQTYSUBTOT = 0;
                    }
                }
            }

            row = dtRtn.NewRow();

            row["CMBANIL"] = "";
            row["IPHWAJU"] = "소  계";
            row["VNSANGHO"] = "";
            row["IPHWAMUL"] = "";
            row["HMDESC"] = "";
            row["IPBLNO"] = "";
            row["IPBSQTY"] = dIPBSQTYSUBTOT;
            row["IPSINO"] = "";
            row["VSJUKHA"] = "";
            row["IPMSNSEQ"] = "";
            row["IPHSNSEQ"] = "";
            row["CHCHULIL"] = "";
            row["CHMTQTY"] = "";
            row["CHJEQTY"] = dCHJEQTYSUBTOT;

            dtRtn.Rows.Add(row);

            row = dtRtn.NewRow();

            row["CMBANIL"] = "";
            row["IPHWAJU"] = "합  계";
            row["VNSANGHO"] = "";
            row["IPHWAMUL"] = "";
            row["HMDESC"] = "";
            row["IPBLNO"] = "";
            row["IPBSQTY"] = dIPBSQTYTOT;
            row["IPSINO"] = "";
            row["VSJUKHA"] = "";
            row["IPMSNSEQ"] = "";
            row["IPHSNSEQ"] = "";
            row["CHCHULIL"] = "";
            row["CHMTQTY"] = "";
            row["CHJEQTY"] = dCHJEQTYTOT;

            dtRtn.Rows.Add(row);

            return dtRtn;
        }
        #endregion
    }
}
