using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 자산이력 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.11 20:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CB8X085 : 고정자산 자산이력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2CB8Z086 : 고정자산 자산이력 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  FXMLASCODE : 자산구분
    ///  FXLSETDATE : 설정일자
    ///  FXLSETGN : 설정구분
    ///  FXSNAME : 자산명
    ///  FXSYEAR : 자산년도
    /// </summary>
    public partial class TYACHF005S : TYBase
    {
        private bool _Isloaded = false;

        private string _sFXLYEAR = string.Empty;
        private string _sFXLSEQ = string.Empty;
        private string _sFXLSUBNUM = string.Empty;
        private string _sFXLSETDATE = string.Empty;
        private string _sFXLSETGN = string.Empty;
        private string _sFXLNUM = string.Empty;

        private string _sCODEGROUP = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYACHF005S()
        {
            InitializeComponent();
        }

        private void TYACHF005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.CBH01_FXC2SAUP.CodeText.Enter += new EventHandler(this.CBH01_FXC2SAUP_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)
            this.CBH01_FXSLMCODE.CodeText.Enter += new EventHandler(this.CBH01_FXSLMCODE_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)
            this.CBH01_FXSMMCODE.CodeText.Enter += new EventHandler(this.CBH01_FXSMMCODE_CodeText_Enter); // 코드박스 커스 포커스 정의(Enter)

            this.CBH01_FXBUYDPMK.DummyValue = "19900101";

            this.CBH01_FXC2SAUP_CodeBoxDataBinded(null, null);

            this.CBH01_FXSLMCODE_CodeBoxDataBinded(null, null);

            this.CBH01_FXSMMCODE_CodeBoxDataBinded(null, null);

            this.CBH01_FXSMCODE_CodeBoxDataBinded(null, null);

            //this.CBH01_FXLMASCODE_CodeBoxDataBinded(null, null);
            //this.CBH01_FXSLMCODE_CodeBoxDataBinded(null, null);
            //this.CBH01_FXSMMCODE_CodeBoxDataBinded(null, null);

            //this.SetStartingFocus(this.TXT03_FXLYEAR);
            this.SetStartingFocus(this.CBH01_FXLMASCODE);
            

            this.BTN61_BATCH.Visible = false;
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sFXLNUM = string.Empty;
            string sFXLYEAR = string.Empty;
            string sFXLSEQ = string.Empty;
            string sFXLSUBNUM = string.Empty;
            string sFXLSETGN = string.Empty;
            string sFXSMMCODE = string.Empty;

            //sFXLNUM = this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString());
            sFXLYEAR = this.TXT03_FXLYEAR.GetValue().ToString();

            if (this.TXT03_FXLSEQ.GetValue().ToString().Trim() != "")
            {
                sFXLSEQ = Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString());
            }
            else
            {
                sFXLSEQ = "";
            }

            if (this.TXT03_FXLSUBNUM.GetValue().ToString().Trim() != "")
            {
                sFXLSUBNUM = Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString());
            }
            else
            {
                sFXLSUBNUM = "";
            }

            sFXLNUM = sFXLYEAR + sFXLSEQ + sFXLSUBNUM;

            sFXLSETGN = this.CBO01_FXLSETGN.GetValue().ToString();
            if (sFXLSETGN == "''")
            {
                sFXLSETGN = "";
            }

            sFXSMMCODE = this.CBH01_FXSMMCODE.GetValue().ToString();

            if (sFXSMMCODE != "")
            {
                sFXSMMCODE = Set_Fill4(sFXSMMCODE);
            }

            this.FPS91_TY_S_AC_2CB8Z086.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34M39553", sFXLNUM.ToString(),
                                                        this.MTB01_GSTDATE.GetValue().ToString().Replace("-", "").ToString(),
                                                        this.MTB01_GEDDATE.GetValue().ToString().Replace("-", "").ToString(),
                                                        sFXLSETGN ,
                                                        this.CBH01_FXBUYDPMK.GetValue().ToString(),
                                                        this.TXT01_FXSNAME.GetValue(),
                                                        this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXC2SAUP.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() + sFXSMMCODE  + this.CBH01_FXSMCODE.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_AC_2CB8Z086.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACHF005I(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            DataTable dm = new DataTable(); 

            this.DbConnector.CommandClear();
            if( dt.Rows.Count > 0 )
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_2CCAM101", dt.Rows[i]["FXLYEAR"].ToString(), 
                                                                dt.Rows[i]["FXLSEQ"].ToString(), 
                                                                dt.Rows[i]["FXLSUBNUM"].ToString(), 
                                                                dt.Rows[i]["FXLNUM"].ToString(), 
                                                                dt.Rows[i]["FXLSETDATE"].ToString(), 
                                                                dt.Rows[i]["FXLSETGN"].ToString() );                    
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CD7H154", dt.Rows[i]["FXLYEAR"].ToString(), dt.Rows[i]["FXLSEQ"].ToString(), dt.Rows[i]["FXLSUBNUM"].ToString());
                dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dm.Rows[0]["FXLSETDATE"].ToString()))
                    {
                        this.DbConnector.Attach("TY_P_AC_2CD3Y138", "", "", "", "", "",
                                                                    TYUserInfo.EmpNo,
                                                                    dt.Rows[i]["FXLYEAR"].ToString(), dt.Rows[i]["FXLSEQ"].ToString(), dt.Rows[i]["FXLSUBNUM"].ToString());
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_AC_2CD3Y138", dm.Rows[0]["FXLSETGN"].ToString(),
                                                                    dm.Rows[0]["FXLSETTEXT"].ToString(),
                                                                    dm.Rows[0]["FXLSETDATE"].ToString(),
                                                                    dm.Rows[0]["FXLMOVDPMK"].ToString(),
                                                                    dm.Rows[0]["FXLMOVSITE"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    dt.Rows[i]["FXLYEAR"].ToString(), dt.Rows[i]["FXLSEQ"].ToString(), dt.Rows[i]["FXLSUBNUM"].ToString());
                    }

                    // 이동(63) 삭제
                    if (dt.Rows[i]["FXLSETGN"].ToString() == "63")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_495FE781", 
                                                dt.Rows[i]["FXLYEAR"].ToString(),dt.Rows[i]["FXLSEQ"].ToString(),dt.Rows[i]["FXLSUBNUM"].ToString() ,
                                                dt.Rows[i]["FXLNUM"].ToString(), dt.Rows[i]["FXLSETDATE"].ToString(), dt.Rows[i]["FXLSETGN"].ToString());
                        DataTable de = this.DbConnector.ExecuteDataTable();

                        // 이전의 상태로 되 돌림
                        //this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_495FF782", de.Rows[i]["FXSCHGCODE"].ToString(),
                                                                    de.Rows[i]["FXSCHGTEXT"].ToString(),
                                                                    de.Rows[i]["FXSCHGDATE"].ToString(),
                                                                    de.Rows[i]["FXSCHGDPMK"].ToString(),
                                                                    de.Rows[i]["FXSCHGSITE"].ToString(),
                                                                    de.Rows[i]["FXSFITSITE"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    dt.Rows[i]["FXLYEAR"].ToString(), dt.Rows[i]["FXLSEQ"].ToString(), dt.Rows[i]["FXLSUBNUM"].ToString());

                        //this.DbConnector.ExecuteTranQueryList();
                    }
                }



                // 자본적지출(31) 삭제
                if (dt.Rows[i]["FXLSETGN"].ToString() == "31")
                {                   
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_A43AZ196", dt.Rows[i]["FXLYEAR"].ToString(),
                                                                dt.Rows[i]["FXLSEQ"].ToString(),
                                                                dt.Rows[i]["FXLSUBNUM"].ToString(),
                                                                dt.Rows[i]["FXLSETDATE"].ToString());
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count > 0)
                    {                       
                        double dFXDEPLAMT = Convert.ToDouble(dk.Rows[0]["FXDEPLAMT"].ToString()) - Convert.ToDouble(dt.Rows[i]["FXLAMOUNT"].ToString());

                        if (dFXDEPLAMT <= 0)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_35N8Z728", dt.Rows[i]["FXLYEAR"].ToString(),
                                                                        dt.Rows[i]["FXLSEQ"].ToString(),
                                                                        dt.Rows[i]["FXLSUBNUM"].ToString(),
                                                                        dt.Rows[i]["FXLSETDATE"].ToString()
                                                                        );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                        else
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_A43B5197", dFXDEPLAMT.ToString(), // 증가금액
                                                                        dt.Rows[i]["FXLYEAR"].ToString(),
                                                                        dt.Rows[i]["FXLSEQ"].ToString(),
                                                                        dt.Rows[i]["FXLSUBNUM"].ToString(),
                                                                        dt.Rows[i]["FXLSETDATE"].ToString()
                                                                        );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                }

            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2CB8Z086_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2CB8Z086_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACHF005I(this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLYEAR").ToString(),
                                this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSEQ").ToString(),
                                this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSUBNUM").ToString(),
                                this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSETDATE").ToString(),
                                this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSETGN").ToString(),
                                this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLNUM").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_2CB8Z086.GetDataSourceInclude(TSpread.TActionType.Remove, "FXLYEAR", "FXLSEQ", "FXLSUBNUM", "FXLSETDATE", "FXLSETGN" ,"FXLNUM","FXLAMOUNT");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //설정일이후 자료가 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CD48139",
                                        dt.Rows[i]["FXLYEAR"].ToString(),
                                        dt.Rows[i]["FXLSEQ"].ToString(),
                                        dt.Rows[i]["FXLSUBNUM"].ToString(),
                                        dt.Rows[i]["FXLSETDATE"].ToString());
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_2CD75151");
                    e.Successed = false;
                    return;
                }
            }

            //전표생성,그룹웨어 결재 자료가 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sMSG = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_34P4V567",
                                        dt.Rows[i]["FXLYEAR"].ToString(),
                                        dt.Rows[i]["FXLSEQ"].ToString(),
                                        dt.Rows[i]["FXLSUBNUM"].ToString(),
                                        dt.Rows[i]["FXLSETDATE"].ToString(),
                                        dt.Rows[i]["FXLSETGN"].ToString(),
                                        dt.Rows[i]["FXLNUM"].ToString()
                                        );

                DataTable dt_log = this.DbConnector.ExecuteDataTable();

                if (dt_log.Rows[0]["FXLGRURL"].ToString() != "")
                {
                    sMSG = dt.Rows[i]["FXLYEAR"].ToString() + "-" + Set_Fill4(dt.Rows[i]["FXLSEQ"].ToString()) + "-" + Set_Fill3(dt.Rows[i]["FXLSUBNUM"].ToString()) + "-" + dt.Rows[i]["FXLSETDATE"].ToString() + "-" +
                           dt.Rows[i]["FXLSETGN"].ToString() + " - 그룹웨어 결재자료가 존재 합니다[삭제불가]";
                    this.ShowCustomMessage(sMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (dt_log.Rows[0]["FXLUWJPNO"].ToString() != "")
                {
                    sMSG = dt.Rows[i]["FXLYEAR"].ToString() + "-" + Set_Fill4(dt.Rows[i]["FXLSEQ"].ToString()) + "-" + Set_Fill3(dt.Rows[i]["FXLSUBNUM"].ToString()) + "-" + dt.Rows[i]["FXLSETDATE"].ToString() + "-" +
                           dt.Rows[i]["FXLSETGN"].ToString() + " - 전표생성 자료가 존재 합니다[삭제불가] ";
                    this.ShowCustomMessage(sMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : 이력생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYACHF005B().ShowDialog() == System.Windows.Forms.DialogResult.OK))
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 자산번호 버튼
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZHF05C1 popup = new TYAZHF05C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT03_FXLYEAR.SetValue(popup.fsASNUM.Substring(0, 4));
                this.TXT03_FXLSEQ.SetValue(popup.fsASNUM.Substring(5, 4));
                this.TXT03_FXLSUBNUM.SetValue(popup.fsASNUM.Substring(10, 3));
            }
        }
        #endregion

        private void FPS91_TY_S_AC_2CB8Z086_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            _sFXLYEAR = this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLYEAR").ToString().Trim();
            _sFXLSEQ =  Set_Fill4(this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSEQ").ToString().Trim());
            _sFXLSUBNUM = Set_Fill3(this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSUBNUM").ToString().Trim());
            _sFXLSETDATE = this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSETDATE").ToString();
            _sFXLSETGN = this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLSETGN").ToString();
            _sFXLNUM = this.FPS91_TY_S_AC_2CB8Z086.GetValue("FXLNUM").ToString();
        }

        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            string sASSET = string.Empty;

            // 자산번호 구하기
            for (int i = 0; i < this.FPS91_TY_S_AC_2CB8Z086.CurrentRowCount; i++)
            {
                _sFXLYEAR = this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLYEAR").ToString().Trim();
                _sFXLSEQ = Set_Fill4(this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLSEQ").ToString().Trim());
                _sFXLSUBNUM = Set_Fill3(this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLSUBNUM").ToString().Trim());

                if (i != 0)
                {
                    sASSET = sASSET.ToString() + "," + _sFXLYEAR + _sFXLSEQ + _sFXLSUBNUM;
                }
                else
                {
                    sASSET = _sFXLYEAR + _sFXLSEQ + _sFXLSUBNUM;
                }
            }


            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_37A4Z075", sASSET 
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACHF005R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                //(new TYERGB001P(rpt, UP_ConvertDt(dt, "PRT"))).ShowDialog();
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_37ABP058");
                return;
            }

        }

        #region Description : 출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sASSET = string.Empty;

            // 자산번호 구하기
            for (int i = 0; i < this.FPS91_TY_S_AC_2CB8Z086.CurrentRowCount; i++)
            {
                _sFXLYEAR = this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLYEAR").ToString().Trim();
                _sFXLSEQ = Set_Fill4(this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLSEQ").ToString().Trim());
                _sFXLSUBNUM = Set_Fill3(this.FPS91_TY_S_AC_2CB8Z086.GetValue(i, "FXLSUBNUM").ToString().Trim());

                if (i != 0)
                {
                    sASSET = sASSET.ToString() + "," + _sFXLYEAR + _sFXLSEQ + _sFXLSUBNUM;
                }
                else
                {
                    sASSET = _sFXLYEAR + _sFXLSEQ + _sFXLSUBNUM;
                }
            }

            if (sASSET == "")
            {
                this.ShowMessage("TY_M_AC_37ABP058");
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 자산구분,대,중,소 CodeBoxDataBinded 이벤트
        // 자산 분류 HELP
        private void CBH01_FXLMASCODE_CodeBoxDataBinded(object sender, EventArgs e)
        {
            //string groupCode = this.CBH01_FXLMASCODE.GetValue().ToString(); 
            //this.CBH01_FXSLMCODE.DummyValue = groupCode;

            //// CBH01_FXSLMCODE
            //// CBH01_FXSMMCODE
            //// CBH01_FXSMCODE

            ////this.CBH01_FXSLMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            
            //if (this._Isloaded)
            //{
            this.CBH01_FXSLMCODE.Initialize();
            this.CBH01_FXSMMCODE.Initialize();
            this.CBH01_FXSMCODE.Initialize();
            //}

            _sCODEGROUP = this.CBH01_FXLMASCODE.GetValue().ToString();
        }

        // 대분류 코드 help 
        private void CBH01_FXSLMCODE_CodeBoxDataBinded(object sender, EventArgs e)
        {
            //string groupCode = this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString();

            //if (groupCode.Length > 1)
            //{
            //    groupCode = groupCode;
            //}
            //else
            //{
            //    groupCode = "";
            //}

            //this.CBH01_FXSMMCODE.DummyValue = groupCode;
            //this.CBH01_FXSMMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            //if (this._Isloaded) this.CBH01_FXSMMCODE.Initialize();

            //this.CBH01_FXSMMCODE.Initialize();
            //this.CBH01_FXSMCODE.Initialize();

            //string s자산 = string.Empty;
            //string s대 = string.Empty;

            //if (this.CBH01_FXSLMCODE.GetValue().ToString().Length == 3)
            //{
            //    s자산 = this.CBH01_FXSLMCODE.GetValue().ToString().Substring(0, 1);
            //    s대 = this.CBH01_FXSLMCODE.GetValue().ToString().Substring(2, 1);

            //    //_sCODEGROUP = s자산 + s대;

            //    _sCODEGROUP = CBH01_FXSLMCODE.GetValue().ToString();

            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", _sCODEGROUP };

            //    //this.CBH01_FXLMASCODE.SetValue(s자산);

            //    this.CBH01_FXSLMCODE.SetValue(_sCODEGROUP);

            //}
            //else if (this.CBH01_FXSLMCODE.GetValue().ToString().Length == 1)
            //{
            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() };
            //}

            string groupCode = this.CBH01_FXC2SAUP.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString();
            this.CBH01_FXSLMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));

            if (groupCode.Length > 1)
            {
                groupCode = groupCode;
            }
            else
            {
                groupCode = "";
            }
            this.CBH01_FXSMMCODE.DummyValue = groupCode;
            this.CBH01_FXSMMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXSMMCODE.Initialize();
            
        }

        // 중분류 코드 help 
        private void CBH01_FXSMMCODE_CodeBoxDataBinded(object sender, EventArgs e)
        {
            ////string groupCode = this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() + this.CBH01_FXSMMCODE.GetValue().ToString();

            ////if (groupCode.Length > 1)
            ////{
            ////    groupCode = groupCode;
            ////}
            ////else
            ////{
            ////    groupCode = "";
            ////}

            ////this.CBH01_FXSMCODE.DummyValue = groupCode;
            ////this.CBH01_FXSMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            ////if (this._Isloaded) this.CBH01_FXSMCODE.Initialize();

            //this.CBH01_FXSMCODE.Initialize();

            //string s자산 = string.Empty;
            //string s대 = string.Empty;
            //string s중 = string.Empty;

            //if (this.CBH01_FXSMMCODE.GetValue().ToString().Length == 7)
            //{
            //    //s자산 = this.CBH01_FXSMMCODE.GetValue().ToString().Substring(0, 1);
            //    s대 = this.CBH01_FXSMMCODE.GetValue().ToString().Substring(2, 1);
            //    s중 = this.CBH01_FXSMMCODE.GetValue().ToString().Substring(4, 3);

            //    _sCODEGROUP = s자산 + s대 + s중 ;
            //    this.CBH01_FXSMMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", _sCODEGROUP };

            //    this.CBH01_FXLMASCODE.SetValue(s자산);

            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", s자산 + s대 };
            //    this.CBH01_FXSLMCODE.SetValue(s대);

            //    this.CBH01_FXSMMCODE.SetValue(s중);
            //}
            //else if (this.CBH01_FXSLMCODE.GetValue().ToString().Length == 3)
            //{
            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() + this.CBH01_FXSMMCODE.GetValue().ToString() };
            //}

            string groupCode = this.CBH01_FXC2SAUP.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() + this.CBH01_FXSMMCODE.GetValue().ToString();
            this.CBH01_FXSMMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            
            if (groupCode.Length > 1)
            {
                groupCode = groupCode;
            }
            else
            {
                groupCode = "";
            }
            this.CBH01_FXSMCODE.DummyValue = groupCode;
            this.CBH01_FXSMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXSMCODE.Initialize();
            
        } 

        // 소분류 코드 help 
        private void CBH01_FXSMCODE_CodeBoxDataBinded(object sender, EventArgs e)
        {

            //string s자산 = string.Empty;
            //string s대 = string.Empty;
            //string s중 = string.Empty;
            //string s소 = string.Empty;

            //if (this.CBH01_FXSMCODE.GetValue().ToString().Length == 11)
            //{
            //    s자산 = this.CBH01_FXSMCODE.GetValue().ToString().Substring(0, 1);
            //    s대 = this.CBH01_FXSMCODE.GetValue().ToString().Substring(2, 1);
            //    s중 = this.CBH01_FXSMCODE.GetValue().ToString().Substring(4, 3);
            //    s소 = this.CBH01_FXSMCODE.GetValue().ToString().Substring(8, 3);

            //    _sCODEGROUP = s자산 + s대 + s중 + s소;
            //    this.CBH01_FXSMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", _sCODEGROUP };

            //    this.CBH01_FXLMASCODE.SetValue(s자산);

            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", s자산 + s대 };
            //    this.CBH01_FXSLMCODE.SetValue(s대);

            //    this.CBH01_FXSMMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", s자산 + s대 + s중 };
            //    this.CBH01_FXSMMCODE.SetValue(s중);

            //    this.CBH01_FXSMCODE.SetValue(s소);
            //}
            //else if (this.CBH01_FXSLMCODE.GetValue().ToString().Length == 3)
            //{
            //    this.CBH01_FXSLMCODE.DummyValue = new string[] { "ssid", Employer.EmpNo, "3", this.CBH01_FXLMASCODE.GetValue().ToString() + this.CBH01_FXSLMCODE.GetValue().ToString() + this.CBH01_FXSMMCODE.GetValue().ToString(), this.CBH01_FXSLMCODE.GetValue().ToString() };
            //}
           
                                    
        }

        private void CBH01_FXC2SAUP_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXC2SAUP.GetValue().ToString();
            this.CBH01_FXSLMCODE.DummyValue = groupCode;
            this.CBH01_FXSLMCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXSLMCODE.Initialize();

            UP_SetCodeBoxClear();
        }
        #endregion       

        #region Description : CBH01_FXC2SAUP_Enter 이벤트
        private void CBH01_FXC2SAUP_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }

        private void CBH01_FXC2SAUP_CodeText_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }

        private void CBH01_FXSLMCODE_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }

        private void CBH01_FXSLMCODE_CodeText_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }

        private void CBH01_FXSMMCODE_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }
        private void CBH01_FXSMMCODE_CodeText_Enter(object sender, EventArgs e)
        {
            UP_SetCodeBoxClear();
        }

        #endregion

        #region Description : UP_SetCodeBoxClear 이벤트
        private void UP_SetCodeBoxClear()
        {
            if (CBH01_FXC2SAUP.GetValue().ToString() == "")
            {
                this.CBH01_FXSLMCODE.SetValue("");
                this.CBH01_FXSMMCODE.SetValue("");
                this.CBH01_FXSMCODE.SetValue("");

                this.CBH01_FXSLMCODE_CodeBoxDataBinded(null, null);
                this.CBH01_FXSMMCODE_CodeBoxDataBinded(null, null);
                this.CBH01_FXSMCODE_CodeBoxDataBinded(null, null);

                return;
            }

            if (CBH01_FXSLMCODE.GetValue().ToString() == "")
            {
                this.CBH01_FXSMMCODE.SetValue("");
                this.CBH01_FXSMCODE.SetValue("");
                
                this.CBH01_FXSMMCODE_CodeBoxDataBinded(null, null);
                this.CBH01_FXSMCODE_CodeBoxDataBinded(null, null);

                return;
            }

            if (CBH01_FXSMMCODE.GetValue().ToString() == "")
            {
                this.CBH01_FXSMCODE.SetValue("");
                
                this.CBH01_FXSMCODE_CodeBoxDataBinded(null, null);

                return;
            }
        }
        #endregion

        

        


    }
}