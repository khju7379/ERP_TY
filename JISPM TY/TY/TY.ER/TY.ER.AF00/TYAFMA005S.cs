using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 원장 생성 및 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.09.09 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3995N629 : EIS 계열사 원장실적 수정
    ///  TY_P_AC_3995P630 : EIS 계열사 원장실적 손익 조회
    ///  TY_P_AC_39A5L649 : EIS 계열사 원장 생성(상위계정 정리-SP)
    ///  TY_P_AC_39H20799 : EIS 계열사 원장실적 수정(TYGT)
    ///  TY_P_AC_39H22800 : EIS 계열사 원장 생성(상위계정 정리SP-TYGT)
    ///  TY_P_AC_39H25803 : EIS 계열사 원장실적 손익 조회(TYGT)
    ///  TY_P_AC_39N5B836 : EIS 계열사 원장실적 생성(확정생성-SP)
    ///  TY_P_AC_39O9C837 : EIS 계열사 원장실적 재무상태표 조회
    ///  TY_P_AC_39O9E838 : EIS 계열사 원장실적 재무상태표 조회(TYGT)
    ///  TY_P_AC_39OAA839 : EIS 계열사 원장실적 생성(확정생성SP-TYGT)
    ///  TY_P_AC_39OAG841 : EIS 계열사 원장확정시 삭제(TYGT용)
    ///  TY_P_AC_39OAH842 : EIS 계열사 원장실적 확정 읽기(TYGT용)
    ///  TY_P_AC_39OAH843 : EIS 계열사 원장실적 생성(TYGT용-DB 저장)
    ///  TY_P_AC_3A719972 : EIS 마감 조회(최종 마감월)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3994V627 : EIS 계열사 원장 조회(손익계산서)
    ///  TY_S_AC_39N2J828 : EIS 계열사 원장 조회(재무상태표)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27P47279 : 년도를 입력하세요.
    ///  TY_M_AC_27P4C280 : 계정과목을 입력하세요.
    ///  TY_M_AC_3992B618 : 작업 할 권한이 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_MR_2BD3Y285 : 수정하시겠습니까?
    ///  TY_M_MR_2BD3Z286 : 수정하였습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  ESBMCUST : 계열사구분
    ///  ESBMAM01 : 01월
    ///  ESBMAM02 : 02월
    ///  ESBMAM03 : 03월
    ///  ESBMAM04 : 04월
    ///  ESBMAM05 : 05월
    ///  ESBMAM06 : 06월
    ///  ESBMAM07 : 07월
    ///  ESBMAM08 : 08월
    ///  ESBMAM09 : 09월
    ///  ESBMAM10 : 10월
    ///  ESBMAM11 : 11월
    ///  ESBMAM12 : 12월
    ///  ESBMLVA01 : LAVEL
    ///  ESBMTAG01 : 차/대(D/C)
    ///  ESBMYYHD : 처리년
    /// </summary>
    public partial class TYAFMA005S : TYBase
    {
        private string sGUBUN = string.Empty;
        private string fsCompanyCode = string.Empty;

        public TYAFMA005S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load 
        private void TYAFMA005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3994V627, "ESBMCUST");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3994V627, "ESBMYYHD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3994V627, "ESBMCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3994V627, "ESBMAMTOT");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39N2J828, "ESBMCUST");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39N2J828, "ESBMYYHD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39N2J828, "ESBMCDAC");

            this.BTN61_CREATE.Visible = false;
            this.BTN61_BATCH.Visible = false;


            sGUBUN = "Tab1";

            this.TXT01_ESBMYYHD.SetValue(DateTime.Now.ToString("yyyy"));

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESBMCUST.SetValue(fsCompanyCode);
                this.CBH01_ESBMCUST.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ESBMYYHD);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESBMCUST.CodeText);
            }
            
        } 
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sConnID = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                sConnID = "TY_P_AC_39H20799";
            }
            else
            {
                sConnID = "TY_P_AC_3995N629";
            }

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach(sConnID, ds.Tables[0].Rows[i]["ESBMAM01"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM02"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM03"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM04"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM05"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM06"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM07"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM08"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM09"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM10"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM11"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMAM12"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMCUST"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMYYHD"].ToString(),
                                                 ds.Tables[0].Rows[i]["ESBMCDAC"].ToString()); //수정
            }

            this.DbConnector.ExecuteNonQuery();

            string sOUTMSG = string.Empty;
            sConnID = "";

            // 상위계정 정리 */
            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                sConnID = "TY_P_AC_39H22800";
            }
            else
            {
                sConnID = "TY_P_AC_39A5L649";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sConnID,
                this.CBH01_ESBMCUST.GetValue().ToString(),
                this.TXT01_ESBMYYHD.GetValue().ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());


            if (sOUTMSG.Substring(0, 2) != "OK")
            {
                return;
            }

            this.ShowMessage("TY_M_MR_2BD3Z286"); // 수정 메세지

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion


        #region Description : 저장 ProcessCheck 이벤트 
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            if (sGUBUN.ToString() == "Tab1")
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_3994V627.GetDataSourceInclude(TSpread.TActionType.Update, "ESBMCUST", "ESBMYYHD", "ESBMCDAC", "ESBMAM01", "ESBMAM02", "ESBMAM03", "ESBMAM04", "ESBMAM05", "ESBMAM06", "ESBMAM07", "ESBMAM08", "ESBMAM09", "ESBMAM10", "ESBMAM11", "ESBMAM12"));
            }
            else
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_39N2J828.GetDataSourceInclude(TSpread.TActionType.Update, "ESBMCUST", "ESBMYYHD", "ESBMCDAC", "ESBMAM01", "ESBMAM02", "ESBMAM03", "ESBMAM04", "ESBMAM05", "ESBMAM06", "ESBMAM07", "ESBMAM08", "ESBMAM09", "ESBMAM10", "ESBMAM11", "ESBMAM12"));
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 생성 버튼 (확정 백만단위 절삭 처리)
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            string sConnID = string.Empty;

            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                sConnID = "TY_P_AC_39OAA839";
            }
            else
            {
                sConnID = "TY_P_AC_39N5B836";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sConnID,
                this.TXT01_ESBMYYHD.GetValue(),
                this.CBH01_ESBMCUST.GetValue(),
                "US",
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
                {
                    // 기존 자료 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39OAG841", this.CBH01_ESBMCUST.GetValue(), this.TXT01_ESBMYYHD.GetValue());
                    this.DbConnector.ExecuteNonQuery();

                    // TYGT 그레인 자료 읽어서 192.168.100.8 --> 192.168.100.2(인더스트리 DB에 저장)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39OAH842", this.CBH01_ESBMCUST.GetValue(), this.TXT01_ESBMYYHD.GetValue());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_39OAH843",
                                                  dt.Rows[i]["ECFMCUST"].ToString(),
                                                  dt.Rows[i]["ECFMYYHD"].ToString(),
                                                  dt.Rows[i]["ECFMCDAC"].ToString(),
                                                  dt.Rows[i]["ECFMCDNM"].ToString(),
                                                  dt.Rows[i]["ECFMAM00"].ToString(),
                                                  dt.Rows[i]["ECFMAM01"].ToString(),
                                                  dt.Rows[i]["ECFMAM02"].ToString(),
                                                  dt.Rows[i]["ECFMAM03"].ToString(),
                                                  dt.Rows[i]["ECFMAM04"].ToString(),
                                                  dt.Rows[i]["ECFMAM05"].ToString(),
                                                  dt.Rows[i]["ECFMAM06"].ToString(),
                                                  dt.Rows[i]["ECFMAM07"].ToString(),
                                                  dt.Rows[i]["ECFMAM08"].ToString(),
                                                  dt.Rows[i]["ECFMAM09"].ToString(),
                                                  dt.Rows[i]["ECFMAM10"].ToString(),
                                                  dt.Rows[i]["ECFMAM11"].ToString(),
                                                  dt.Rows[i]["ECFMAM12"].ToString(),
                                                  dt.Rows[i]["ECFMTAG01"].ToString(),
                                                  dt.Rows[i]["ECFMLVA01"].ToString());
                            this.DbConnector.ExecuteTranQueryList();
                            //this.DbConnector.ExecuteNonQuery();
                        }
                    }
                }

                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

        } 
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            int iMonth = 4;
            int j = 0;

            //this.FPS91_TY_S_AC_3994V627.Initialize();
            //this.FPS91_TY_S_AC_39N2J828.Initialize();

            string sConnID = string.Empty;
            DataTable dt = new DataTable();

            if (this.CBH01_ESBMCUST.GetValue().ToString().Trim() == "TG") // 태영그레인터미널 처리
            {
                if (sGUBUN.ToString() == "Tab1")
                {
                    sConnID = "TY_P_AC_39H25803"; // 손익 현황
                }
                else
                {
                    sConnID = "TY_P_AC_39O9E838"; // 재무상태표
                }
            }
            else
            {
                if (sGUBUN.ToString() == "Tab1")
                {
                    sConnID = "TY_P_AC_3995P630"; // 손익 현황
                }
                else
                {
                    sConnID = "TY_P_AC_39O9C837"; // 재무상태표
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sConnID, this.CBH01_ESBMCUST.GetValue(), this.TXT01_ESBMYYHD.GetValue());

            //손익현황
            if (sGUBUN.ToString() == "Tab1")
            {
                this.FPS91_TY_S_AC_3994V627.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_3994V627.ActiveSheet.RowCount > 0)
                {
                    // 특정 ROW 색깔 입히기
                    for (int i = 0; i < this.FPS91_TY_S_AC_3994V627.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "01000000" || // 매출
                            this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "02000000" || // 원가
                            this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "03000000" || // 매출이익
                            this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "05000000" || // 영업이익
                            this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "10000000" || // 영업외손익
                            this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "11000000"    // 세전이익
                            )
                        {
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_AC_3994V627.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                        }
                        else if (this.FPS91_TY_S_AC_3994V627.GetValue(i, "ESBMCDAC").ToString() == "13000000")  // 당기순이익
                        {
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_AC_3994V627.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                        }
                    }
                }

                // 마감 월 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A719972", this.TXT01_ESBMYYHD.GetValue());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    iMonth = int.Parse(dt.Rows[0]["ECMONTH"].ToString()) + 4;
                }

                for (int i = 0; i < FPS91_TY_S_AC_3994V627.CurrentRowCount; i++)
                {
                    // TAG = 'Y'이면 잠금
                    if (this.FPS91_TY_S_AC_3994V627.GetValue(i, "EPCTAG02").ToString() == "Y")
                    {
                        // 마감 월 잠금
                        for (j = iMonth; j <= 16; j++)
                        {
                            this.FPS91_TY_S_AC_3994V627_Sheet1.Cells[i, j].Locked = false;

                            this.FPS91_TY_S_AC_3994V627_Sheet1.Cells[i, 16].Locked = true; // 합계 잠금
                                                       
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_AC_3994V627.ActiveSheet.Rows[i].Locked = true;
                    }
                }

                // 마감 ROW 색깔 입히기
                for (j = 4; j < iMonth; j++)
                {
                    this.FPS91_TY_S_AC_3994V627.ActiveSheet.Columns[j].BackColor = Color.FromArgb(218, 239, 194);
                }

                this.FPS91_TY_S_AC_3994V627.Focus();
            }


            //재무상태표
            if (sGUBUN.ToString() == "Tab2")
            {
                this.FPS91_TY_S_AC_39N2J828.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_AC_39N2J828.ActiveSheet.RowCount > 0)
                {
                    // 특정 ROW 색깔 입히기
                    for (int i = 0; i < this.FPS91_TY_S_AC_39N2J828.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_39N2J828.GetValue(i, "ESBMCDAC").ToString() == "61000000" || // 자산
                            this.FPS91_TY_S_AC_39N2J828.GetValue(i, "ESBMCDAC").ToString() == "62000000" || // 부채
                            this.FPS91_TY_S_AC_39N2J828.GetValue(i, "ESBMCDAC").ToString() == "63000000"    // 자본
                            )
                        {
                            // 특정 ROW 색깔 입히기
                            this.FPS91_TY_S_AC_39N2J828.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                        }
                    }
                }

                // 마감 월 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A719972", this.TXT01_ESBMYYHD.GetValue());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    iMonth = int.Parse(dt.Rows[0]["ECMONTH"].ToString()) + 4;
                }

                for (int i = 0; i < FPS91_TY_S_AC_39N2J828.CurrentRowCount; i++)
                {
                    // TAG = 'Y'이면 잠금
                    if (this.FPS91_TY_S_AC_39N2J828.GetValue(i, "EPCTAG02").ToString() == "Y")
                    {
                        // 마감 월 잠금
                        for (j = iMonth; j <= 16; j++)
                        {
                            this.FPS91_TY_S_AC_39N2J828_Sheet1.Cells[i, j].Locked = false;
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_AC_39N2J828.ActiveSheet.Rows[i].Locked = true;
                    }
                }

                // 마감 ROW 색깔 입히기
                for (j = 4; j < iMonth; j++)
                {
                    this.FPS91_TY_S_AC_39N2J828.ActiveSheet.Columns[j].BackColor = Color.FromArgb(218, 239, 194);
                }
                

                this.FPS91_TY_S_AC_39N2J828.Focus();
            }

        } 
        #endregion


        #region Description : tabControl
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 손익현황
            {
                sGUBUN = "Tab1";
            }
            else if (tabControl1.SelectedIndex == 1) // 재무상태표
            {
                sGUBUN = "Tab2";
            }
            else if (tabControl1.SelectedIndex == 2) // 
            {
                sGUBUN = "Tab3";
            }

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion


    }
}
