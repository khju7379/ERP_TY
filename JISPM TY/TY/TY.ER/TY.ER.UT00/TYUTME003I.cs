using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
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
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_736DE856 : 하역료 단가 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUTME003I : TYBase
    {
        private string fsYODATE     = string.Empty;
        private string fsYOINGUB    = string.Empty;
        private string fsIGDESC1    = string.Empty;
        private string fsYOBASICAMT = string.Empty;

        private string fsGUBUN = string.Empty;

        #region Descriptino : 페이지 로드
        public TYUTME003I()
        {
            InitializeComponent();
        }

        private void TYUTME003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));            

            this.DTP01_STDATE.SetValue(Get_Date(this.DTP01_STDATE.GetValue().ToString()).Substring(0, 6) + "27");            

            this.FPS91_TY_S_UT_736DE856.Initialize();

            fsGUBUN = "";

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptino : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_736DE856.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736D3850",
                                    Get_Date(this.DTP01_STDATE.GetValue().ToString())
                                    );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_736DE856.SetValue(dt);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly();

            UP_Field_Clear();

            SetFocus(this.CBH01_M2HWAJU.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_736DD852", Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                                            this.CBH01_M2HWAJU.GetValue().ToString(),
                                                            this.CBH01_M2HWAMUL.GetValue().ToString(),
                                                            this.TXT01_M2ENINAM.GetValue().ToString(),
                                                            this.TXT01_M2ENINDOL.GetValue().ToString(),
                                                            this.TXT01_M2ENTUAM.GetValue().ToString(),
                                                            this.TXT01_M2ENTUDOL.GetValue().ToString(),
                                                            this.TXT01_M2RATE.GetValue().ToString(),
                                                            this.CBH01_M2CURRCD.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); // 저장
                this.DbConnector.ExecuteNonQuery();
            }
            else if (fsGUBUN == "UPT")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_736DD853", this.TXT01_M2ENINAM.GetValue().ToString(),
                                                            this.TXT01_M2ENINDOL.GetValue().ToString(),
                                                            this.TXT01_M2ENTUAM.GetValue().ToString(),
                                                            this.TXT01_M2ENTUDOL.GetValue().ToString(),
                                                            this.TXT01_M2RATE.GetValue().ToString(),
                                                            this.CBH01_M2CURRCD.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                                            this.CBH01_M2HWAJU.GetValue().ToString(),
                                                            this.CBH01_M2HWAMUL.GetValue().ToString()
                                                            ); // 수정
                this.DbConnector.ExecuteNonQuery();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_736DD854", Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                                            this.CBH01_M2HWAJU.GetValue().ToString(),
                                                            this.CBH01_M2HWAMUL.GetValue().ToString()
                                                            ); // 수정
                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 선급금 생성 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYUTME003B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736D4851",
                                    Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                    this.CBH01_M2HWAJU.GetValue().ToString(),
                                    this.CBH01_M2HWAMUL.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";
            }

            UP_Field_ReadOnly();
        }
        #endregion

        #region Description : 전표생성/출력
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            if ((new TYUTME005B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : FieldClear
        private void UP_Field_Clear()
        {
            this.CBH01_M2HWAJU.SetValue("");
            this.CBH01_M2HWAMUL.SetValue("");

            this.TXT01_M2ENINAM.SetValue("");
            this.TXT01_M2ENINDOL.SetValue("");
            this.TXT01_M2ENTUAM.SetValue("");
            this.TXT01_M2ENTUDOL.SetValue("");
            this.TXT01_M2RATE.SetValue("");
            this.CBH01_M2CURRCD.SetValue("");
            this.TXT01_M2JPNO.SetValue("");
        }
        #endregion

        #region Description : Field ReadOnly
        private void UP_Field_ReadOnly()
        {
            if (fsGUBUN == "NEW")
            {
                this.DTP01_M2DATE.SetReadOnly(false);
                this.CBH01_M2HWAJU.SetReadOnly(false);
                this.CBH01_M2HWAMUL.SetReadOnly(false);
            }
            else
            {
                this.DTP01_M2DATE.SetReadOnly(true);
                this.CBH01_M2HWAJU.SetReadOnly(true);
                this.CBH01_M2HWAMUL.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_736D4851",
                                        Get_Date(this.DTP01_M2DATE.GetValue().ToString()),
                                        this.CBH01_M2HWAJU.GetValue().ToString(),
                                        this.CBH01_M2HWAMUL.GetValue().ToString()
                                        );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    this.DTP01_M2DATE.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736DD855", this.CBH01_M2HWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VNPGUBN"].ToString() == "6")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_M2RATE.GetValue().ToString().Trim())) == 0)
                    {
                        this.ShowMessage("TY_M_UT_736D8847");

                        SetFocus(this.TXT01_M2RATE);

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_M2CURRCD.GetValue().ToString().Trim() == "")
                    {
                        this.ShowMessage("TY_M_UT_736D8848");

                        SetFocus(this.CBH01_M2CURRCD.CodeText);

                        e.Successed = false;
                        return;
                    }

                    double dFieldCompute = 0;

                    dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_M2ENINAM.GetValue().ToString().Trim())) / Convert.ToDouble(Get_Numeric(this.TXT01_M2RATE.GetValue().ToString().Trim()))) * 100);
                    dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                    dFieldCompute = (dFieldCompute / 100);

                    if (double.Parse(Get_Numeric(this.TXT01_M2ENINDOL.GetValue().ToString().Trim())) != dFieldCompute)
                    {
                        this.ShowMessage("TY_M_UT_736D9849");

                        SetFocus(this.TXT01_M2ENINDOL);

                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_UT_736F3857");

                SetFocus(this.CBH01_M2HWAJU.CodeText);

                e.Successed = false;
                return;
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
            if (this.TXT01_M2JPNO.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_GB_25F8V482");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_736DE856_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_M2DATE.SetValue(this.FPS91_TY_S_UT_736DE856.GetValue("M2DATE").ToString());
            this.CBH01_M2HWAJU.SetValue(this.FPS91_TY_S_UT_736DE856.GetValue("M2HWAJU").ToString());
            this.CBH01_M2HWAMUL.SetValue(this.FPS91_TY_S_UT_736DE856.GetValue("M2HWAMUL").ToString());

            // 확인
            UP_RUN();
        }
        #endregion
    }
}
