using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 교육비 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.11.13 16:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77JC6219 : 연말정산 소득자공제명세서 조회
    ///  TY_P_HR_77JDB223 : 연말정산 소득공제명세 국세청자료 확정
    ///  TY_P_HR_7BDH6979 : 연말정산 교육비 등록
    ///  TY_P_HR_7BDH9980 : 연말정산 교육비 수정
    ///  TY_P_HR_7BDH9981 : 연말정산 교육비 삭제
    ///  TY_P_HR_7BDHA983 : 연말정산 교육비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_7BDI3986 : 연말정산 부양가족명세 조회
    ///  TY_S_HR_7BDHA984 : 연말정산 교육비 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WFSABUN : 사　　번
    ///  EDEDU_TP : 교육비종류
    ///  WFCODE : 가족코드
    ///  WFJUMIN : 주민번호
    ///  WFNAME : 가족이름
    ///  WFYEAR : 년    도
    /// </summary>
    public partial class TYHRNT01C6 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C6(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C6_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Spread_PayItemTitle();

            TXT01_WFYEAR.SetValue(fsWKYEAR);
            CBH01_WFSABUN.SetValue(fsWKSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_SAV.Visible = false;
            }

            this.UP_Grid_DataBinding();
        }
        #endregion

        #region  Description : 그리드 데이타 바인딩 이벤트
        private void UP_Grid_DataBinding()
        {
            this.FPS91_TY_S_HR_7BGEE018.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JC6219", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_HR_7BGEE018.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7BGEE018.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_7BGEE018.CurrentRowCount; i++)
                {
                    //국세청 자료는 필드 잠금
                    if (this.FPS91_TY_S_HR_7BGEE018.GetValue(i, "NTSGUBN").ToString() == "1")
                    {
                        for (int j = 19; j < 22; j++)
                        {
                            this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].Locked = true;
                            this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].BackColor = Color.WhiteSmoke;
                        }
                    }
                    else
                    {
                        for (int j = 19; j < 22; j++)
                        {
                            //교육구분이 해당없음이면 잠금
                            if (this.FPS91_TY_S_HR_7BGEE018.GetValue(i, "WFEDUGN").ToString() == "")
                            {
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].Locked = true;
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].Text = "";
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].BackColor = Color.WhiteSmoke;

                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].Locked = true;
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].Text = "";
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].BackColor = Color.WhiteSmoke;
                            }

                            //교육구분에 장애인일 경우 일반은 잠금
                            if (this.FPS91_TY_S_HR_7BGEE018.GetValue(i, "WFEDUGN").ToString() == "4")
                            {
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].Locked = true;
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].Text = "";
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 21].BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].Locked = true;
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].Text = "";
                                this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, 22].BackColor = Color.WhiteSmoke;
                            }

                            this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].ForeColor = Color.DarkRed;
                            this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Underline);


                            ////현금영수증은 잠금
                            //if (j == 25 || j == 28)
                            //{
                            //    this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].Locked = true;
                            //    this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].Text = "";
                            //    this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].BackColor = Color.WhiteSmoke;
                            //}

                            //if (j != 25 || j != 28)
                            //{
                            //    this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].ForeColor = Color.DarkRed;
                            //    this.FPS91_TY_S_HR_7BGEE018_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Underline);
                            //}
                        }
                    }
                }
            }

        }
        #endregion       

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                this.DbConnector.Attach("TY_P_HR_7BGEX019", ds.Tables[0].Rows[i]["WFTAXPROBOHUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFTAXOBJBOHUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFTAXGYOUK"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFTAXGYOUKJANG"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[0].Rows[i]["WFCOMPANY"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFSABUN"].ToString(),
                                                            TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[0].Rows[i]["WFJUMIN"].ToString()
                                                           );
            }
            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.UP_ProCedure_FixCall();
            
            this.UP_Grid_DataBinding();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dGYOUKHAP = 0;
            double dGYOUKLimitAmt = 0;

            DataSet ds = new DataSet();
            //ds.Tables.Add(this.FPS91_TY_S_HR_7BGEE018.GetDataSourceInclude(TSpread.TActionType.Update, "WFCOMPANY", "WFYEAR", "WFSABUN", "WFJUMIN", "WFCODE", "WFGUBUN", "WFEDUGN", "WFJANG",
            //                                                                                           "WFTAXPROBOHUM", "WFTAXOBJBOHUM", "WFTAXGYOUK", "WFTAXGYOUKJANG", "WFTAXCARD", "WFTAXDEBCARD", "WFTAXCARDBOOK", "WFTAXDEBBOOK", "WFTAXCASHBOOK", "WFTAXMARKET", "WFTAXPUBTRANS"));

            ds.Tables.Add(this.FPS91_TY_S_HR_7BGEE018.GetDataSourceInclude(TSpread.TActionType.Update, "WFCOMPANY", "WFYEAR", "WFSABUN", "WFJUMIN", "WFCODE", "WFGUBUN", "WFEDUGN", "WFJANG",
                                                                                                       "WFTAXPROBOHUM", "WFTAXOBJBOHUM", "WFTAXGYOUK", "WFTAXGYOUKJANG" ));
            if( ds.Tables[0].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }         

            //교육비 한도 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77BBG100", ds.Tables[0].Rows[i]["WFCOMPANY"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["WFSABUN"].ToString(),
                                                            TYUserInfo.SecureKey, "Y",
                                                            ds.Tables[0].Rows[i]["WFJUMIN"].ToString()
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    switch (ds.Tables[0].Rows[i]["WFEDUGN"].ToString().Trim())
                    {
                        case "0": //본인(한도 없음)
                            dGYOUKLimitAmt = 0;
                            break;
                        case "1":  //미취학
                            dGYOUKLimitAmt = 3000000;
                            break;
                        case "2": //초중고
                            dGYOUKLimitAmt = 3000000;
                            break;
                        case "3": //대학생
                            dGYOUKLimitAmt = 9000000;
                            break;
                        case "4": //장애인(한도 없음)            
                            dGYOUKLimitAmt = 0;  
                            break;
                        default:
                            break;
                    }

                    dGYOUKHAP = Convert.ToDouble(Get_Numeric(dt.Rows[0]["WFTAXGYOUK"].ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(dt.Rows[0]["WFTAXGYOUKJANG"].ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["WFTAXGYOUK"].ToString().Trim())) +
                                Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["WFTAXGYOUKJANG"].ToString().Trim()));

                    if (ds.Tables[0].Rows[i]["WFEDUGN"].ToString().Trim() == "" && dGYOUKHAP > 0 )
                    {
                        this.ShowCustomMessage("교육비구분이 없는 부양가족은 교육비등록이 불가합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }

                    //교육비한도가 있으면서 교육비가 한도를 초과한경우
                    if (dGYOUKLimitAmt > 0 && dGYOUKHAP > dGYOUKLimitAmt)
                    {
                        this.ShowCustomMessage("교육비 공제금액이 한도를 초과하였습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

            }

            //장애인 보험료 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["WFTAXOBJBOHUM"].ToString().Trim())) > 0 && ds.Tables[0].Rows[i]["WFJANG"].ToString().Trim() != "Y")
                {
                    this.ShowCustomMessage("장애인의 경우만 장애인 보험료를 등록할 수 있습니다!!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                if (Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["WFTAXGYOUKJANG"].ToString().Trim())) > 0 && ds.Tables[0].Rows[i]["WFEDUGN"].ToString().Trim() != "4")
                {
                    this.ShowCustomMessage("장애인의 경우만 장애인 교육비를 등록할 수 있습니다!!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
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

        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y", "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion       

        #region Description : 소득명세 스프레드 타이틀 변경
        private void UP_Spread_PayItemTitle()
        {
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 19; i++)
            {
                this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 0].Value = "회사구분";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 1].Value = "귀속년도";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 2].Value = "사 번";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 3].Value = "성 명";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민등록번호";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 5].Value = "가족코드";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 6].Value = "가족구분";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 7].Value = "가족관계";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 8].Value = "가족관계";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 9].Value = "교육구분";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 10].Value = "교육구분";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 11].Value = "기본공제";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 12].Value = "장애인";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 13].Value = "부녀자";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 14].Value = "6세이하";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 15].Value = "출산.입양자";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 16].Value = "한부모";            
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 17].Value = "자료구분";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 18].Value = "자료구분";            

            this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 2);
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 19].Value = "보험료";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 19].Value = "보장성";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 20].Value = "장애인";

            this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 2);            
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 21].Value = "교육비";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 21].Value = "일  반";
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 22].Value = "장애인";

            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 23, 1, 3);
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 23].Value = "신용카드등 사용액";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 23].Value = "신용카드";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 24].Value = "직불카드";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 25].Value = "현금영수증";

            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 26, 1, 3);
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 26].Value = "도서.공연";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 26].Value = "신용카드";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 27].Value = "직불카드";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 28].Value = "현금영수증";

            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 29, 2, 1);
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 29].Value = "전통시장";
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.AddColumnHeaderSpanCell(0, 30, 2, 1);
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[1, 30].Value = "대중교통";

            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 23].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.FPS91_TY_S_HR_7BGEE018_Sheet1.ColumnHeader.Cells[0, 26].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
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
