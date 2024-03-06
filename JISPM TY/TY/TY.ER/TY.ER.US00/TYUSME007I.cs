using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_918B8447 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUSME007I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        private string fsVSJUBAN = string.Empty;
        private string fsVSGROSS = string.Empty;

        private string fsMCENBOHP   = string.Empty;
        private string fsMCENIPHP   = string.Empty;
        private string fsMCENCHHP   = string.Empty;
        private string fsMCENISHP   = string.Empty;
        private string fsMCENCANHP  = string.Empty;
        private string fsMCENDRHP   = string.Empty;
        private string fsMCENBOJHP  = string.Empty;
        private string fsMCENIPOVHP = string.Empty;
        private string fsMCENCHOVHP = string.Empty;

        #region Description : 페이지 로드
        public TYUSME007I()
        {
            InitializeComponent();
        }

        private void TYUSME007I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_918F7450, "BKDATE");

            this.FPS91_TY_S_US_918B8447.Initialize();
            this.FPS91_TY_S_US_918F7450.Initialize();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            UP_BTN_DISPLAY("");

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_918B8447.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918B6446",
                this.CBH01_GHANGCHA.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_GDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_918B8447.SetValue(dt);

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly(false);

            UP_Field_Clear();

            UP_BTN_DISPLAY("NEW");

            this.FPS91_TY_S_US_918F7450.Initialize();

            SetFocus(this.DTP01_BKMCYYMM);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                this.DbConnector.Attach("TY_P_US_919A3456", Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_BKGOKJONG.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_BKBLMSN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKBLHSN.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYSHWAJU.GetValue().ToString().Trim(),
                                                            Get_Numeric(Get_Date(this.MTB01_BKYSDATE.GetValue().ToString().Trim())),
                                                            Get_Numeric(this.TXT01_BKYSSEQ.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKYDSEQ.GetValue().ToString().Trim()),
                                                            this.CBO01_BKBOKGB.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[0].Rows[i]["BKDATE"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["BKCHULQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["BKJEGOQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["BKDANGA"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["BKBOKAMT"].ToString().Trim()),
                                                            "14",
                                                            this.TXT01_BKCONTNO.GetValue().ToString().Trim(),
                                                            "",
                                                            this.CBH01_BKWNHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_BKSIKBAEL.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                this.DbConnector.Attach("TY_P_US_919A3457", Get_Numeric(ds.Tables[1].Rows[i]["BKCHULQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["BKJEGOQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["BKDANGA"].ToString()).Trim(),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["BKBOKAMT"].ToString().Trim()),
                                                            this.CBH01_BKWNHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_BKSIKBAEL.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo,
                                                            Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_BKGOKJONG.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_BKBLMSN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKBLHSN.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYSHWAJU.GetValue().ToString().Trim(),
                                                            Get_Numeric(Get_Date(this.MTB01_BKYSDATE.GetValue().ToString().Trim())),
                                                            Get_Numeric(this.TXT01_BKYSSEQ.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKYDSEQ.GetValue().ToString().Trim()),
                                                            this.CBO01_BKBOKGB.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[1].Rows[i]["BKDATE"].ToString().Trim())
                                                            );
            }

            for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                this.DbConnector.Attach("TY_P_US_919A4458", Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_BKGOKJONG.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_BKBLMSN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKBLHSN.GetValue().ToString().Trim()),
                                                            this.CBH01_BKHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_BKYSHWAJU.GetValue().ToString().Trim(),
                                                            Get_Numeric(Get_Date(this.MTB01_BKYSDATE.GetValue().ToString().Trim())),
                                                            Get_Numeric(this.TXT01_BKYSSEQ.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_BKYDSEQ.GetValue().ToString().Trim()),
                                                            this.CBO01_BKBOKGB.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString().Trim()),
                                                            ds.Tables[2].Rows[i]["BKDATE"].ToString().Trim()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();
            

            this.ShowMessage("TY_M_MR_2BF50354");

            UP_RUN();
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            this.FPS91_TY_S_US_918F7450.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918F5449",
                Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString()),
                this.CBH01_BKHANGCHA.GetValue().ToString(),
                this.CBH01_BKGOKJONG.GetValue().ToString(),
                this.TXT01_BKBLMSN.GetValue().ToString(),
                this.TXT01_BKBLHSN.GetValue().ToString(),
                this.CBH01_BKHWAJU.GetValue().ToString(),
                this.CBH01_BKYDHWAJU.GetValue().ToString(),
                this.CBH01_BKYSHWAJU.GetValue().ToString(),
                Get_Date(this.MTB01_BKYSDATE.GetValue().ToString()),
                this.TXT01_BKYSSEQ.GetValue().ToString(),
                this.TXT01_BKYDSEQ.GetValue().ToString(),
                this.CBO01_BKBOKGB.GetValue().ToString(),
                Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_918F7450.SetValue(dt);

            if(dt.Rows.Count > 0)
            {
                // 원화주
                this.CBH01_BKWNHWAJU.SetValue(dt.Rows[0]["BKWNHWAJU"].ToString());
                // 식별자
                this.TXT01_BKSIKBAEL.SetValue(dt.Rows[0]["BKSIKBAEL"].ToString());
                // 계약번호
                this.TXT01_BKCONTNO.SetValue(dt.Rows[0]["BKCONTNO"].ToString());

                fsGUBUN = "UPT";

                if (this.TXT01_BKJPNO.GetValue().ToString() != "")
                {
                    fsGUBUN = "";
                }
                
                UP_Field_ReadOnly(true);
            }
            else
            {
                fsGUBUN = "NEW";
            }


            // 보관료 총금액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918G5452",
                Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString()),
                this.CBH01_BKHANGCHA.GetValue().ToString(),
                this.CBH01_BKGOKJONG.GetValue().ToString(),
                this.TXT01_BKBLMSN.GetValue().ToString(),
                this.TXT01_BKBLHSN.GetValue().ToString(),
                this.CBH01_BKHWAJU.GetValue().ToString(),
                this.CBH01_BKYDHWAJU.GetValue().ToString(),
                this.CBH01_BKYSHWAJU.GetValue().ToString(),
                Get_Date(this.MTB01_BKYSDATE.GetValue().ToString()),
                this.TXT01_BKYSSEQ.GetValue().ToString(),
                this.TXT01_BKYDSEQ.GetValue().ToString(),
                this.CBO01_BKBOKGB.GetValue().ToString(),
                Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_BKBOKAMT_TOT.SetValue(dt.Rows[0]["BKBOKAMT_TOT"].ToString());
                this.TXT01_BKBOKAMT_VAT.SetValue(dt.Rows[0]["BKBOKAMT_VAT"].ToString());
            }

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.FPS91_TY_S_US_918F7450);
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_918F7450.GetDataSourceInclude(TSpread.TActionType.New,    "BKDATE", "BKCHULQTY", "BKJEGOQTY", "BKDANGA", "BKBOKAMT"));
            ds.Tables.Add(this.FPS91_TY_S_US_918F7450.GetDataSourceInclude(TSpread.TActionType.Update, "BKDATE", "BKCHULQTY", "BKJEGOQTY", "BKDANGA", "BKBOKAMT"));
            ds.Tables.Add(this.FPS91_TY_S_US_918F7450.GetDataSourceInclude(TSpread.TActionType.Remove, "BKDATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString()) != Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString()).ToString().Substring(0, 6))
            {
                this.ShowMessage("TY_M_US_918HU455");

                SetFocus(this.DTP01_BKMCYYMM);

                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            // 매출발생월 다음달 미수금액이 존재하는지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        "14");
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.DTP01_BKMCYYMM);

                e.Successed = false;
                return;
            }

            // SILO 보관료 계약번호 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918HO453",
                this.CBH01_BKHANGCHA.GetValue().ToString(),
                this.CBH01_BKGOKJONG.GetValue().ToString(),
                this.CBH01_BKHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_BKCONTNO.SetValue(dt.Rows[0]["JGCONTNO"].ToString());
            }

            // SILO 보관료 원화주,식별자 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918HP454",
                Get_Date(this.DTP01_BKMCYYMM.GetValue().ToString()),
                this.CBH01_BKHANGCHA.GetValue().ToString(),
                this.CBH01_BKGOKJONG.GetValue().ToString(),
                this.CBH01_BKHWAJU.GetValue().ToString(),
                this.TXT01_BKBLMSN.GetValue().ToString(),
                this.TXT01_BKBLHSN.GetValue().ToString(),
                this.CBO01_BKBOKGB.GetValue().ToString(),
                this.CBH01_BKYDHWAJU.GetValue().ToString(),
                this.CBH01_BKYSHWAJU.GetValue().ToString(),
                Get_Date(this.MTB01_BKYSDATE.GetValue().ToString()),
                this.TXT01_BKYSSEQ.GetValue().ToString(),
                this.TXT01_BKYDSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_BKYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_BKWNHWAJU.SetValue(dt.Rows[0]["BKWNHWAJU"].ToString());
                this.TXT01_BKSIKBAEL.SetValue(dt.Rows[0]["BKSIKBAEL"].ToString());
            }

            int i = 0;

            double dBKJEGOQTY = 0;
            double dBKDANGA   = 0;
            double dBKBOKAMT = 0;
            
            string sBKBOKAMT = "0";

            // 보관료 금액 구하기
            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
            {
                dBKJEGOQTY = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["BKJEGOQTY"].ToString()));
                dBKDANGA   = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["BKDANGA"].ToString()));

                dBKBOKAMT = dBKJEGOQTY * dBKDANGA;
                sBKBOKAMT = UP_DotDelete(Convert.ToString(dBKBOKAMT));

                ds.Tables[0].Rows[i]["BKBOKAMT"] = sBKBOKAMT.ToString();
            }

            // 보관료 금액 구하기
            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                dBKJEGOQTY = Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["BKJEGOQTY"].ToString()));
                dBKDANGA   = Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["BKDANGA"].ToString()));

                dBKBOKAMT = dBKJEGOQTY * dBKDANGA;
                sBKBOKAMT = UP_DotDelete(Convert.ToString(dBKBOKAMT));

                ds.Tables[1].Rows[i]["BKBOKAMT"] = sBKBOKAMT.ToString();
            }


            // 처리하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_918B8447_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_BKMCYYMM.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKMCYYMM").ToString());
            this.CBH01_BKHANGCHA.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKHANGCHA").ToString());
            this.CBH01_BKGOKJONG.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKGOKJONG").ToString());
            this.CBH01_BKHWAJU.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKHWAJU").ToString());
            this.TXT01_BKBLMSN.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKBLMSN").ToString());
            this.TXT01_BKBLHSN.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKBLHSN").ToString());
            this.CBO01_BKBOKGB.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKBOKGB").ToString());
            this.CBH01_BKYDHWAJU.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYDHWAJU").ToString());
            this.CBH01_BKYSHWAJU.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYSHWAJU").ToString());
            this.MTB01_BKYSDATE.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYSDATE").ToString());
            this.TXT01_BKYSSEQ.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYSSEQ").ToString());
            this.TXT01_BKYDSEQ.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYDSEQ").ToString());
            this.DTP01_BKYYMMDD.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKYYMMDD").ToString());
            this.TXT01_BKJPNO.SetValue(this.FPS91_TY_S_US_918B8447.GetValue("BKJPNO").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            this.DTP01_BKMCYYMM.SetReadOnly(boolean);
            this.CBH01_BKHANGCHA.SetReadOnly(boolean);
            this.CBH01_BKGOKJONG.SetReadOnly(boolean);
            this.CBH01_BKHWAJU.SetReadOnly(boolean);
            this.TXT01_BKBLMSN.SetReadOnly(boolean);
            this.TXT01_BKBLHSN.SetReadOnly(boolean);
            this.CBO01_BKBOKGB.SetReadOnly(boolean);
            this.CBH01_BKYDHWAJU.SetReadOnly(boolean);
            this.CBH01_BKYSHWAJU.SetReadOnly(boolean);
            this.MTB01_BKYSDATE.SetReadOnly(boolean);
            this.TXT01_BKYSSEQ.SetReadOnly(boolean);
            this.TXT01_BKYDSEQ.SetReadOnly(boolean);
            this.DTP01_BKYYMMDD.SetReadOnly(boolean);
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_BATCH.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_BATCH.Visible = true;
            }
            else
            {
                this.BTN61_BATCH.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.CBH01_BKHANGCHA.SetValue("");
            this.CBH01_BKGOKJONG.SetValue("");
            this.CBH01_BKHWAJU.SetValue("");
            this.TXT01_BKBLMSN.SetValue("");
            this.TXT01_BKBLHSN.SetValue("");
            this.CBO01_BKBOKGB.SetValue("1");
            this.CBH01_BKYDHWAJU.SetValue("");
            this.CBH01_BKYSHWAJU.SetValue("");
            this.MTB01_BKYSDATE.SetValue("0");
            this.TXT01_BKYSSEQ.SetValue("0");
            this.TXT01_BKYDSEQ.SetValue("0");

            this.TXT01_BKBOKAMT_TOT.SetValue("");
            this.TXT01_BKBOKAMT_VAT.SetValue("");
            this.TXT01_BKJPNO.SetValue("");
        }
        #endregion

        #region Description : 화주 이벤트
        private void CBH01_GHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion

        #region Description : SILO 보관료 DATA 확인 스프레드 이벤트
        private void FPS91_TY_S_US_918F7450_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 4)
                return;

            int row = e.Row;

            double dBKJEGOQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_918F7450.GetValue(row, "BKJEGOQTY").ToString()));
            double dBKDANGA   = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_918F7450.GetValue(row, "BKDANGA").ToString()));
            double dBKBOKAMT = dBKJEGOQTY * dBKDANGA;

            string sBKBOKAMT = String.Format("{0,9:N0}", dBKBOKAMT);

            this.FPS91_TY_S_US_918F7450.SetValue(row, "BKBOKAMT", sBKBOKAMT);
        }

        private void FPS91_TY_S_US_918F7450_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column != 4)
                return;

            int row = e.Row;

            double dBKJEGOQTY = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_918F7450.GetValue(row, "BKJEGOQTY").ToString()));
            double dBKDANGA   = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_US_918F7450.GetValue(row, "BKDANGA").ToString()));
            double dBKBOKAMT = dBKJEGOQTY * dBKDANGA;

            string sBKBOKAMT = String.Format("{0,9:N0}", dBKBOKAMT);

            this.FPS91_TY_S_US_918F7450.SetValue(row, "BKBOKAMT", sBKBOKAMT);
        }
        #endregion

    }
}