using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using DataDynamics.ActiveReports;
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
    ///  TY_S_US_91SHW628 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME013I : TYBase
    {
        string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYUSME013I()
        {
            InitializeComponent();
        }

        private void TYUSME013I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_US_91SHW628.Sheets[0].Columns[12].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_91SHW628, "BTN");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.FPS91_TY_S_US_91SHW628.Initialize();

            SetStartingFocus(this.DTP01_STYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_91SHW628.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_91SHW627",
                Get_Date(this.DTP01_STYYMM.GetValue().ToString()),
                Get_Date(this.DTP01_EDYYMM.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91SHW628.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_91SHW628.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_91SHW628.ActiveSheet.Cells[i, 9].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91SHW628.ActiveSheet.Cells[i, 9].BackColor = Color.SkyBlue;

                this.FPS91_TY_S_US_91SHW628.ActiveSheet.Cells[i, 10].Font = new Font("굴림", 9, FontStyle.Bold);
                this.FPS91_TY_S_US_91SHW628.ActiveSheet.Cells[i, 10].BackColor = Color.SkyBlue;
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly(false);

            UP_FleldClear();

            UP_Btn_Display(fsGUBUN);

            this.SetFocus(this.DTP01_EXYYMM);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91TGG636", this.DTP01_EXYYMM.GetValue().ToString(),
                                                            this.DTP01_EXYYMMDD.GetValue().ToString(),
                                                            this.CBH01_EXHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EXGOKJONG.GetValue().ToString(),
                                                            this.CBH01_EXHWAJU.GetValue().ToString(),
                                                            "1",
                                                            this.TXT01_EXDESC1.GetValue().ToString(),
                                                            this.TXT01_EXDESC2.GetValue().ToString(),
                                                            this.TXT01_EXDESC3.GetValue().ToString(),
                                                            this.TXT01_EXDESC4.GetValue().ToString(),
                                                            this.TXT01_EXDESC5.GetValue().ToString(),
                                                            this.TXT01_EXDESC6.GetValue().ToString(),
                                                            this.TXT01_EXDESC7.GetValue().ToString(),
                                                            this.TXT01_EXDESC8.GetValue().ToString(),
                                                            this.TXT01_EXDESC9.GetValue().ToString(),
                                                            this.TXT01_EXDESC10.GetValue().ToString(),
                                                            this.TXT01_EXDESC11.GetValue().ToString(),
                                                            this.TXT01_EXDESC12.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_EXAMT.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_EXVAT.GetValue().ToString()),
                                                            this.CBO01_EXTAXCODE.GetValue().ToString(),
                                                            "17",
                                                            TYUserInfo.EmpNo
                                                            ); // 저장
                this.DbConnector.ExecuteTranQueryList();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91TGK637", this.TXT01_EXDESC1.GetValue().ToString(),
                                                            this.TXT01_EXDESC2.GetValue().ToString(),
                                                            this.TXT01_EXDESC3.GetValue().ToString(),
                                                            this.TXT01_EXDESC4.GetValue().ToString(),
                                                            this.TXT01_EXDESC5.GetValue().ToString(),
                                                            this.TXT01_EXDESC6.GetValue().ToString(),
                                                            this.TXT01_EXDESC7.GetValue().ToString(),
                                                            this.TXT01_EXDESC8.GetValue().ToString(),
                                                            this.TXT01_EXDESC9.GetValue().ToString(),
                                                            this.TXT01_EXDESC10.GetValue().ToString(),
                                                            this.TXT01_EXDESC11.GetValue().ToString(),
                                                            this.TXT01_EXDESC12.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_EXAMT.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_EXVAT.GetValue().ToString()),
                                                            this.CBO01_EXTAXCODE.GetValue().ToString(),
                                                            "17",
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_EXYYMM.GetValue().ToString(),
                                                            this.DTP01_EXYYMMDD.GetValue().ToString(),
                                                            this.CBH01_EXHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EXGOKJONG.GetValue().ToString(),
                                                            this.CBH01_EXHWAJU.GetValue().ToString(),
                                                            "1"
                                                            ); // 저장
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91TGC635", this.DTP01_EXYYMM.GetValue().ToString(),
                                                        this.DTP01_EXYYMMDD.GetValue().ToString(),
                                                        this.CBH01_EXHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EXGOKJONG.GetValue().ToString(),
                                                        this.CBH01_EXHWAJU.GetValue().ToString(),
                                                        "1"
                                                        ); // 삭제
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91TBT629",
                this.DTP01_EXYYMM.GetValue().ToString(),
                this.DTP01_EXYYMMDD.GetValue().ToString(),
                this.CBH01_EXHANGCHA.GetValue().ToString(),
                this.CBH01_EXGOKJONG.GetValue().ToString(),
                this.CBH01_EXHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                UP_Btn_Display(fsGUBUN);

                if (this.TXT01_EXJPNO.GetValue().ToString().Trim() != "")
                {
                    this.BTN61_SAV.Visible = false;
                    this.BTN61_REM.Visible = false;
                }

                UP_Field_ReadOnly(true);

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.TXT01_EXDESC1);
                };

                tmr.Interval = 100;
                tmr.Start();
            }
            else
            {
                fsGUBUN = "NEW";

                UP_Btn_Display(fsGUBUN);

                UP_Field_ReadOnly(false);
            }
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            this.DTP01_EXYYMM.SetReadOnly(boolean);
            this.DTP01_EXYYMMDD.SetReadOnly(boolean);
            this.CBH01_EXHANGCHA.SetReadOnly(boolean);
            this.CBH01_EXGOKJONG.SetReadOnly(boolean);
            this.CBH01_EXHWAJU.SetReadOnly(boolean);
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FleldClear()
        {
            this.DTP01_EXYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EXYYMMDD.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_EXHANGCHA.SetValue("");
            this.CBH01_EXGOKJONG.SetValue("");
            this.CBH01_EXHWAJU.SetValue("");

            this.TXT01_EXDESC1.SetValue("");
            this.TXT01_EXDESC2.SetValue("");
            this.TXT01_EXDESC3.SetValue("");
            this.TXT01_EXDESC4.SetValue("");
            this.TXT01_EXDESC5.SetValue("");
            this.TXT01_EXDESC6.SetValue("");
            this.TXT01_EXDESC7.SetValue("");
            this.TXT01_EXDESC8.SetValue("");
            this.TXT01_EXDESC9.SetValue("");
            this.TXT01_EXDESC10.SetValue("");
            this.TXT01_EXDESC11.SetValue("");
            this.TXT01_EXDESC12.SetValue("");

            this.TXT01_EXAMT.SetValue("0");
            this.TXT01_EXVAT.SetValue("0");
            this.CBO01_EXTAXCODE.SetValue("61");

            this.TXT01_EXJPNO.SetValue("");
        }
        #endregion

        #region Description : 버튼 활성화
        private void UP_Btn_Display(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 매출발생월 다음달 미수금액이 존재하는지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.DTP01_EXYYMM.GetValue().ToString()),
                                                        "");
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.DTP01_EXYYMM);

                e.Successed = false;
                return;
            }

            // 회계 거래처 등록 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_US_91OHO588",
                                   this.CBH01_EXHWAJU.GetValue().ToString().Trim()
                                   );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_US_91OHP589");

                SetFocus(this.CBH01_EXHWAJU.CodeText);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_EXAMT.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_AC_382BD291");

                SetFocus(this.TXT01_EXAMT);

                e.Successed = false;
                return;
            }

            if (this.CBO01_EXTAXCODE.GetValue().ToString() == "11" || this.CBO01_EXTAXCODE.GetValue().ToString() == "61")
            {
                // 부가세
                this.TXT01_EXVAT.SetValue(Convert.ToString(double.Parse(Get_Numeric(this.TXT01_EXAMT.GetValue().ToString())) * 0.1));

                if (double.Parse(Get_Numeric(this.TXT01_EXVAT.GetValue().ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_3CVB8907");

                    SetFocus(this.TXT01_EXVAT);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_EXTAXCODE.GetValue().ToString() != "11" && this.CBO01_EXTAXCODE.GetValue().ToString() != "61")
            {
                this.TXT01_EXVAT.SetValue("0");
            }

            if (fsGUBUN == "UPT")
            {
                if (this.TXT01_EXJPNO.GetValue().ToString().Trim() != "")
                {
                    this.ShowMessage("TY_M_UT_73LI0046");

                    SetFocus(this.DTP01_EXYYMM);

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
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_EXJPNO.GetValue().ToString().Trim() != "")
            {
                this.ShowMessage("TY_M_UT_73LI0046");

                SetFocus(this.DTP01_EXYYMM);

                e.Successed = false;
                return;
            }

            // 삭제하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 전표 출력
        private void FPS91_TY_S_US_91SHW628_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "12")
            {
                if (this.FPS91_TY_S_US_91SHW628.GetValue("EXJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_US_91SHW628.GetValue("EXJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_US_91SHW628.GetValue("EXJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_US_91SHW628.GetValue("EXJPNO").ToString().Substring(14, 3);

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
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
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

        #region Description : 발생월 이벤트
        private void DTP01_EDYYMM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_INQ_Click(null, null);

                this.SetFocus(this.DTP01_EDYYMM);
            }
        }
        #endregion

        private void FPS91_TY_S_US_91SHW628_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_EXYYMM.SetValue(this.FPS91_TY_S_US_91SHW628.GetValue("EXYYMM").ToString());
            this.DTP01_EXYYMMDD.SetValue(this.FPS91_TY_S_US_91SHW628.GetValue("EXYYMMDD").ToString());
            this.CBH01_EXHANGCHA.SetValue(this.FPS91_TY_S_US_91SHW628.GetValue("EXHANGCHA").ToString());
            this.CBH01_EXGOKJONG.SetValue(this.FPS91_TY_S_US_91SHW628.GetValue("EXGOKJONG").ToString());
            this.CBH01_EXHWAJU.SetValue(this.FPS91_TY_S_US_91SHW628.GetValue("EXHWAJU").ToString());

            // 확인
            UP_RUN();
        }
    }
}