using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

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
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_7CIA8290 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME050I : TYBase
    {
        private string fsHMYYMMDD = string.Empty;
        private string fsHMMCYYMM = string.Empty;
        private string fsHMHANGCHA = string.Empty;
        private string fsVSDESC1 = string.Empty;
        private string fsHMIPHANG = string.Empty;
        private string fsHMIANDAT = string.Empty;
        private string fsHMPAYDAT = string.Empty;

        #region Description : 페이지 로드
        public TYUSME050I()
        {
            InitializeComponent();

            // 곡종
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_7CIB6291, "HMGOKJONG", "GKDESC1", "HMGOKJONG");
            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_7CIB6291, "HMHWAJU", "VNSANGHO", "HMHWAJU");
        }

        private void TYUSME050I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_7CIA8290.Sheets[0].Columns[11].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_7CIA8290, "BTN");

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_7CIB6291, "HMGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_7CIB6291, "HMHWAJU");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_JUNPYO_OK.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_OK_ProcessCheck);
            this.BTN61_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_CANCEL_ProcessCheck);

            this.FPS91_TY_S_US_7CIA8290.Initialize();

            SetStartingFocus(this.DTP01_STYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_7CIA8290.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_7CIA0288",
                Get_Date(this.DTP01_STYYMM.GetValue().ToString()),
                this.CBH01_HMHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_7CIA8290.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_7CIA8290.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_7CIA8290.ActiveSheet.Cells[i, 9].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_7CIA8290.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sHMSEQ = string.Empty;
            string sHMHAMULAMT = string.Empty;

            double dHMBEJNQTY = 0;
            double dHMDANGA = 0;

            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dHMBEJNQTY = double.Parse(String.Format("{0,9:N0}", Get_Numeric(ds.Tables[0].Rows[i]["HMBEJNQTY"].ToString())));
                    dHMDANGA = double.Parse(String.Format("{0,9:N0}", Get_Numeric(ds.Tables[0].Rows[i]["HMDANGA"].ToString())));

                    sHMHAMULAMT = String.Format("{0,9:N0}", dHMBEJNQTY * dHMDANGA);

                    this.DbConnector.Attach("TY_P_US_7CID2292", Get_Date(ds.Tables[0].Rows[i]["HMYYMMDD"].ToString()),
                                                                ds.Tables[0].Rows[i]["HMMCYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMHANGCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMGOKJONG"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMHWAJU"].ToString(),
                                                                Get_Date(ds.Tables[0].Rows[i]["HMYYMMDD"].ToString()),
                                                                ds.Tables[0].Rows[i]["HMMCYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMHANGCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMGOKJONG"].ToString(),
                                                                ds.Tables[0].Rows[i]["HMHWAJU"].ToString(),
                                                                Get_Date(ds.Tables[0].Rows[i]["HMIPHANG"].ToString()),
                                                                Get_Date(ds.Tables[0].Rows[i]["HMIANDAT"].ToString()),
                                                                Get_Date(ds.Tables[0].Rows[i]["HMPAYDAT"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HMBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HMDANGA"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HMCONTNO"].ToString()),
                                                                Get_Numeric(sHMHAMULAMT.ToString()).Trim(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                ); // 저장
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_7CID2293", Get_Date(ds.Tables[1].Rows[i]["HMIPHANG"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HMBEJNQTY"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HMDANGA"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HMCONTNO"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HMHAMULAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HMCHARGEAMT"].ToString()),
                                                                TYUserInfo.EmpNo,
                                                                Get_Date(ds.Tables[1].Rows[i]["HMYYMMDD"].ToString()),
                                                                ds.Tables[1].Rows[i]["HMMCYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["HMHANGCHA"].ToString(),
                                                                ds.Tables[1].Rows[i]["HMGOKJONG"].ToString(),
                                                                ds.Tables[1].Rows[i]["HMHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["HMSEQ"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);

            UP_RUN();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_7CID3294", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);

            UP_RUN();
        }
        #endregion

        #region Description : 전표생성 버튼
        private void BTN61_JUNPYO_OK_Click(object sender, EventArgs e)
        {
            string sJPNOID = string.Empty;

            // 화물료 전표테이블의 전표생성
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 화물료 전표테이블의 전표생성 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_7CIEV306", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        fsHMYYMMDD.ToString(),
                                                        fsHMMCYYMM.ToString(),
                                                        fsHMHANGCHA.ToString(),
                                                        "TYUSME050I",
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        "A",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_25O8K620"); // 저장 메세지

                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_JUNPYO_OK.Visible = false;
                this.BTN61_JUNPYO_CANCEL.Visible = true;
            }
            else
            {
                this.ShowMessage("TY_M_UT_73D99886"); // 저장 메세지
            }

            this.BTN61_INQ_Click(null, null);

            UP_RUN();
        }
        #endregion

        #region Description : 전표취소 버튼
        private void BTN61_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sJPNOID = string.Empty;

            // 화물료 전표테이블의 전표생성
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 화물료 전표테이블의 전표생성 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_7CIEV306", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        fsHMYYMMDD.ToString(),
                                                        fsHMMCYYMM.ToString(),
                                                        fsHMHANGCHA.ToString(),
                                                        "TYUSME050I",
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        "D",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_25O8K620"); // 저장 메세지

                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_JUNPYO_OK.Visible = true;
                this.BTN61_JUNPYO_CANCEL.Visible = false;
            }
            else
            {
                this.ShowMessage("TY_M_UT_73D99886"); // 저장 메세지
            }

            this.BTN61_INQ_Click(null, null);

            UP_RUN();
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_7CIB6291.GetDataSourceInclude(TSpread.TActionType.New, "HMYYMMDD", "HMMCYYMM", "HMHANGCHA", "HMGOKJONG", "HMHWAJU", "HMIPHANG", "HMIANDAT", "HMPAYDAT", "HMBEJNQTY", "HMDANGA", "HMCONTNO", "HMHAMULAMT", "HMCHARGEAMT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_7CIB6291.GetDataSourceInclude(TSpread.TActionType.Update, "HMYYMMDD", "HMMCYYMM", "HMHANGCHA", "HMGOKJONG", "HMHWAJU", "HMSEQ", "HMIPHANG", "HMBEJNQTY", "HMDANGA", "HMCONTNO", "HMHAMULAMT", "HMCHARGEAMT", "HMJPNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 항차
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_7CI9N286",
                                       "VS",
                                       ds.Tables[0].Rows[i]["HMHANGCHA"].ToString(),
                                       ""
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_7CIDB295");
                    e.Successed = false;
                    return;
                }

                // 곡종
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_7CI9N286",
                                       "GK",
                                       ds.Tables[0].Rows[i]["HMGOKJONG"].ToString(),
                                       ""
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_US_7CIDB296");
                    e.Successed = false;
                    return;
                }

                // 화주
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_US_7CI9M285",
                                       ds.Tables[0].Rows[i]["HMHWAJU"].ToString(),
                                       ""
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                // 배정량
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMBEJNQTY"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDC297");
                    e.Successed = false;
                    return;
                }

                // 단가
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMDANGA"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDD298");
                    e.Successed = false;
                    return;
                }

                // 화물료 청구금액
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDD299");
                    e.Successed = false;
                    return;
                }
            }

            string sCJJISINO1 = string.Empty;

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 배정량
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["HMBEJNQTY"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDC297");
                    e.Successed = false;
                    return;
                }

                // 단가
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["HMDANGA"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDD298");
                    e.Successed = false;
                    return;
                }

                // 화물료 청구금액
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["HMCHARGEAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_7CIDD299");
                    e.Successed = false;
                    return;
                }

                // 화물료 전표번호
                if (ds.Tables[1].Rows[i]["HMJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");
                    e.Successed = false;
                    return;
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            DataTable dt = this.FPS91_TY_S_US_7CIB6291.GetDataSourceInclude(TSpread.TActionType.Remove, "HMYYMMDD", "HMMCYYMM", "HMHANGCHA", "HMGOKJONG", "HMHWAJU", "HMSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["HMJPNO"].ToString() != "")
                //    {
                //        this.ShowMessage("TY_M_US_7CIDF300");
                //        e.Successed = false;
                //        return;
                //    }
                //}
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 전표생성 ProcessCheck
        private void BTN61_JUNPYO_OK_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_7CIH4311",
                fsHMYYMMDD.ToString(),
                fsHMMCYYMM.ToString(),
                fsHMHANGCHA.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_7CIH4312");
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 전표취소 ProcessCheck
        private void BTN61_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_7CIH6314",
                fsHMYYMMDD.ToString(),
                fsHMMCYYMM.ToString(),
                fsHMHANGCHA.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_7CIH6313");
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_US_7CIA8290_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsHMYYMMDD = "";
            fsHMMCYYMM = "";
            fsHMHANGCHA = "";
            fsHMIPHANG = "";
            fsHMIANDAT = "";
            fsHMPAYDAT = "";

            fsHMYYMMDD = Get_Date(this.FPS91_TY_S_US_7CIA8290.GetValue("HMYYMMDD").ToString());
            fsHMMCYYMM = Get_Date(this.FPS91_TY_S_US_7CIA8290.GetValue("HMMCYYMM").ToString());
            fsHMHANGCHA = this.FPS91_TY_S_US_7CIA8290.GetValue("HMHANGCHA").ToString();
            fsVSDESC1 = this.FPS91_TY_S_US_7CIA8290.GetValue("VSDESC1").ToString();
            fsHMIPHANG = this.FPS91_TY_S_US_7CIA8290.GetValue("HMIPHANG").ToString();
            fsHMIANDAT = this.FPS91_TY_S_US_7CIA8290.GetValue("HMIANDAT").ToString();
            fsHMPAYDAT = this.FPS91_TY_S_US_7CIA8290.GetValue("HMPAYDAT").ToString();

            if (this.FPS91_TY_S_US_7CIA8290.GetValue("HMJPNO").ToString() != "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_JUNPYO_OK.Visible = false;
                this.BTN61_JUNPYO_CANCEL.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_JUNPYO_OK.Visible = true;
                this.BTN61_JUNPYO_CANCEL.Visible = false;
            }

            UP_RUN();
        }
        #endregion

        #region Description : 화물료 확인 이벤트
        private void UP_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_7CIA4289",
                fsHMYYMMDD.ToString(),
                fsHMMCYYMM.ToString(),
                fsHMHANGCHA.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_7CIB6291.SetValue(dt);

            for (int i = 0; i < FPS91_TY_S_US_7CIB6291.CurrentRowCount; i++)
            {
                if (this.FPS91_TY_S_US_7CIB6291.GetValue(i, "HMJPNO").ToString() != "")
                {
                    this.FPS91_TY_S_US_7CIB6291_Sheet1.Cells[i, 14].Locked = true;
                }
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_US_7CIB6291_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (this.FPS91_TY_S_US_7CIB6291.ActiveSheet.RowCount > 1)
            {
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMYYMMDD", fsHMYYMMDD);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMMCYYMM", fsHMMCYYMM);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMHANGCHA", fsHMHANGCHA);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "VSDESC1", fsVSDESC1);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMIPHANG", fsHMIPHANG);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMIANDAT", fsHMIANDAT);
                this.FPS91_TY_S_US_7CIB6291.SetValue(e.RowIndex, "HMPAYDAT", fsHMPAYDAT);
            }
        }
        #endregion

        #region Description : 전표 출력
        private void FPS91_TY_S_US_7CIA8290_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "11")
            {
                if (this.FPS91_TY_S_US_7CIA8290.GetValue("HMJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_US_7CIA8290.GetValue("HMJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_US_7CIA8290.GetValue("HMJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_US_7CIA8290.GetValue("HMJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }

            }
        }
        #endregion
    }
}