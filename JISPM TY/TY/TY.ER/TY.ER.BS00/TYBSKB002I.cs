using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 영업외손익 항목코드 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.16 10:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_76GAA827 : 영업외손익 계정과목 전체 조회
    ///  TY_P_AC_76GAC828 : 영업외손익 항목코드 등록
    ///  TY_P_AC_76GAG830 : 영업외손익 항목코드 삭제
    ///  TY_P_AC_76GAH832 : 영업외손익 항목코드 수정
    ///  TY_P_AC_76GAJ833 : 영업외손익 항목코드 조회
    ///  TY_P_AC_76GAJ834 : 영업외손익 항목코드 확인
    ///  TY_P_AC_76GAJ836 : 영업외손익 계정과목 항목 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_76GAN838 : 영업외손익 계정과목 조회
    ///  TY_S_AC_76GAQ839 : 영업외손익 항목코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  COPY : 복사
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BSCDGB : 조회구분
    ///  BSCDAC : 계정과목
    ///  BSCDACNM : 계정과목명
    ///  BSCDHC : 계정세목
    ///  BSCDHCNM : 계정세목명
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYBSKB002I : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB002I()
        {
            InitializeComponent();
        }

        private void TYBSKB002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_76GAQ839, "BSXCDSC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.TXT01_BSYEAR.Text = UP_Get_MaxYear();

            this.TXT01_BSCDAC.ReadOnly = true;
            this.TXT01_BSCDACNM.ReadOnly = true;
            this.TXT01_BSCDHC.ReadOnly = true;
            this.TXT01_BSCDHCNM.ReadOnly = true;

            this.RB_BSCDGB1.Checked = true;

            SetStartingFocus(this.TXT01_BSYEAR);
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYBSKB002B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_76GAN838.Initialize();
                this.FPS91_TY_S_AC_76GAQ839.Initialize();

                this.DbConnector.CommandClear();

                if (this.RB_BSCDGB1.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_76GAA827", this.TXT01_BSYEAR.GetValue().ToString());
                }
                else if (this.RB_BSCDGB2.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_76GAJ836", this.TXT01_BSYEAR.GetValue().ToString());
                }

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_76GAN838.SetValue(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_76GAN838.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_76GAN838.GetValue(i, "CNT").ToString() != "0")
                        {
                            // 특정 ROW 글자색깔 입히기
                            this.FPS91_TY_S_AC_76GAN838.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                        }
                    }
                }

                

                UP_init();
            }
            catch
            {

            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_76GAG830", dt);
                this.DbConnector.ExecuteNonQuery();

                UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                           this.TXT01_BSCDAC.GetValue().ToString(),
                           this.TXT01_BSCDHC.GetValue().ToString());

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_76GAQ839.GetDataSourceInclude(TSpread.TActionType.Remove, "BSXYEAR", "BSXCDAC", "BSXCDHC", "BSXCDSC");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;

            }
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_76GAC828", ds.Tables[0].Rows[i]["BSXYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSXCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSXCDHC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSXCDSC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSXCDSCNM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSXBIGO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_76GAH832", ds.Tables[1].Rows[i]["BSXCDSCNM"].ToString(),
                                                                ds.Tables[1].Rows[i]["BSXBIGO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BSXYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BSXCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BSXCDHC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BSXCDSC"].ToString()
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }

                UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                           this.TXT01_BSCDAC.GetValue().ToString(),
                           this.TXT01_BSCDHC.GetValue().ToString());


                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_AC_76GAQ839.GetDataSourceInclude(TSpread.TActionType.New, "BSXYEAR", "BSXCDAC", "BSXCDHC", "BSXCDSC", "BSXCDSCNM", "BSXBIGO"));

            ds.Tables.Add(this.FPS91_TY_S_AC_76GAQ839.GetDataSourceInclude(TSpread.TActionType.Update, "BSXYEAR", "BSXCDAC", "BSXCDHC", "BSXCDSC", "BSXCDSCNM", "BSXBIGO"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_76GAJ834", ds.Tables[0].Rows[i]["BSXYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["BSXCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BSXCDHC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BSXCDSC"].ToString()
                                                            );

                DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 항목입니다.[" + ds.Tables[0].Rows[i]["BSXCDSC"].ToString() + "][" + ds.Tables[0].Rows[i]["BSXCDSCNM"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                if (ds.Tables[0].Rows[i]["BSXCDSC"].ToString() == "000")
                {
                    this.ShowCustomMessage("항목코드[000]은 등록할 수 없습니다.",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

        #region Description : 계정과목 그리드 더블클릭
        private void FPS91_TY_S_AC_76GAN838_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 계정과목이 전표계정인 경우
            if (this.FPS91_TY_S_AC_76GAN838.GetValue("A1TAG02").ToString() == "Y")
            {
                // 계정과목만 존재하는 경우(계정과목 = 계정세목)
                if (this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDHC").ToString() == "")
                {
                    UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                               this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString(),
                               this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString());

                    this.TXT01_BSCDAC.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString();
                    this.TXT01_BSCDACNM.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDACNM").ToString();
                    this.TXT01_BSCDHC.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString();
                    this.TXT01_BSCDHCNM.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDACNM").ToString();
                }
                else
                {

                    UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                               this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString(),
                               this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDHC").ToString());

                    this.TXT01_BSCDAC.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDAC").ToString();
                    this.TXT01_BSCDACNM.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDACNM").ToString();
                    this.TXT01_BSCDHC.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDHC").ToString();
                    this.TXT01_BSCDHCNM.Text = this.FPS91_TY_S_AC_76GAN838.GetValue("BSXCDHCNM").ToString();
                }
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_76GAQ839_RowInserted 이벤트
        private void FPS91_TY_S_AC_76GAQ839_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_76GAQ839.SetValue(e.RowIndex, "BSXYEAR", TXT01_BSYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_76GAQ839.SetValue(e.RowIndex, "BSXCDAC", TXT01_BSCDAC.GetValue().ToString());
            this.FPS91_TY_S_AC_76GAQ839.SetValue(e.RowIndex, "BSXCDHC", TXT01_BSCDHC.GetValue().ToString());
        }
        #endregion

        #region Description : 영업외손익 항목코드 조회
        private void UP_GetBSCD(string sBSYEAR, string sBSCDAC, string BSCDHC)
        {
            this.FPS91_TY_S_AC_76GAQ839.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_76GAJ833", sBSYEAR,
                                                        sBSCDAC,
                                                        BSCDHC
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_76GAQ839.SetValue(dt);
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_init()
        {
            this.TXT01_BSCDAC.Text = "";
            this.TXT01_BSCDACNM.Text = "";
            this.TXT01_BSCDHC.Text = "";
            this.TXT01_BSCDHCNM.Text = "";
        }
        #endregion

        private void FPS91_TY_S_AC_76GAQ839_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sBSCDSC = this.FPS91_TY_S_AC_76GAQ839.GetValue("BSXCDSC").ToString();
            this.FPS91_TY_S_AC_76GAQ839.SetValue("BSXCDSC", Set_Fill3(sBSCDSC));
        }

        #region Description : 최근년도 가져오기
        private string UP_Get_MaxYear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8AME5003");
            string sMaxYear = this.DbConnector.ExecuteScalar().ToString();

            return sMaxYear;
        }
        #endregion
    }
}
