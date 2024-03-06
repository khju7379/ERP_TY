using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 기준정보관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.19 10:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_77JB5208 : 기준정보관리 INDEX 조회
    ///  TY_P_AC_77JBA209 : 기준정보관리 CODE 조회
    ///  TY_P_AC_77JBB210 : 기준정보관리 등록
    ///  TY_P_AC_77JBD211 : 기준정보관리 삭제
    ///  TY_P_AC_77JBE212 : 기준정보관리 수정
    ///  TY_P_AC_77JBF213 : 기준정보관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_77JBG214 : 기준정보관리 CODE 조회
    ///  TY_S_AC_77JBH215 : 기준정보관리 INDEX 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PRYEAR : 년도
    /// </summary>
    public partial class TYBSKB005I : TYBase
    {
        //private TYData DAT01_CDHISAB;

        private string fsCDYEAR;
        private string fsCDINDEX;

        #region Description : 폼 로드
        public TYBSKB005I()
        {
            InitializeComponent();
        }

        private void TYBSKB005I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77JBG214, "CDINDEX");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77JBG214, "CDCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_PRYEAR.SetValue(UP_Get_MaxYear());
            fsCDYEAR = System.DateTime.Now.ToString("yyyy");

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_PRYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_77JBH215.Initialize();
            this.FPS91_TY_S_AC_77JBG214.Initialize();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_77JB5208", this.TXT01_PRYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_77JBH215.SetValue(this.DbConnector.ExecuteDataTable());

            fsCDYEAR = this.TXT01_PRYEAR.GetValue().ToString();
            fsCDINDEX = "00";
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77JBD211", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.FPS91_TY_S_AC_77JBH215_CellDoubleClick(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_77JBG214.GetDataSourceInclude(TSpread.TActionType.Remove,"CDYEAR", "CDINDEX", "CDCODE");

            if (dt.Rows.Count > 0)
            {
                if (UP_PBUPCheck(dt.Rows[0]["CDYEAR"].ToString()))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CDINDEX"].ToString() == "00")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_77JBA209", dt.Rows[i]["CDYEAR"].ToString(),
                                                                        dt.Rows[i]["CDCODE"].ToString());
                            DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                            if (dtTmp.Rows.Count > 0)
                            {
                                this.ShowCustomMessage("하위 코드가 존재합니다.[" + dt.Rows[i]["CDCODE"].ToString() + "]",
                                                        "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
                //else
                //{
                //    this.ShowCustomMessage("사업계획 자료가 존재하여, 삭제가 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    e.Successed = false;
                //    return;
                //}
            }

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

                    this.DbConnector.Attach("TY_P_AC_77JBB210", ds.Tables[0].Rows[i]["CDYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDINDEX"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDDESC1"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDITEM1"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDITEM2"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDITEM3"].ToString(),
                                                                ds.Tables[0].Rows[i]["CDBIGO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_77JBE212", ds.Tables[1].Rows[i]["CDDESC1"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDITEM1"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDITEM2"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDITEM3"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDBIGO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["CDYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDINDEX"].ToString(),
                                                                ds.Tables[1].Rows[i]["CDCODE"].ToString()
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
                this.FPS91_TY_S_AC_77JBH215_CellDoubleClick(null, null);
                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77JBG214.GetDataSourceInclude(TSpread.TActionType.New, "CDYEAR", "CDINDEX", "CDCODE", "CDDESC1", "CDITEM1", "CDITEM2", "CDITEM3", "CDBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77JBG214.GetDataSourceInclude(TSpread.TActionType.Update, "CDYEAR", "CDINDEX", "CDCODE", "CDDESC1", "CDITEM1", "CDITEM2", "CDITEM3", "CDBIGO"));

            //신규
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (UP_PBUPCheck(ds.Tables[0].Rows[0]["CDYEAR"].ToString()))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //if (ds.Tables[0].Rows[i]["CDINDEX"].ToString() != "00")
                        //{
                        //    if (ds.Tables[0].Rows[i]["CDCODE"].ToString().Length > 3)
                        //    {
                        //        if (ds.Tables[0].Rows[i]["CDCODE"].ToString().Substring(0, 3) != ds.Tables[0].Rows[i]["CDINDEX"].ToString() + "-")
                        //        {
                        //            this.ShowCustomMessage("CODE 형식이 올바르지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        //            e.Successed = false;
                        //            return;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        this.ShowCustomMessage("CODE 형식이 올바르지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}
                        if (ds.Tables[0].Rows[i]["CDINDEX"].ToString() == "TA")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach(
                                               "TY_P_AC_77JH6224",
                                               ds.Tables[0].Rows[i]["CDITEM1"].ToString()
                                               );

                            if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                            {
                                this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["CDCODE"].ToString() + "][항목1:투자계정]을 확인하십시요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                            if (ds.Tables[0].Rows[i]["CDITEM2"].ToString() != "")
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach(
                                                   "TY_P_AC_77JH7225",
                                                   ds.Tables[0].Rows[i]["CDITEM2"].ToString()
                                                   );

                                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                                {
                                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["CDCODE"].ToString() + "][항목2:수선계정]을 확인하십시요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_AC_77JBF213",
                                               ds.Tables[0].Rows[i]["CDYEAR"].ToString(),
                                               ds.Tables[0].Rows[i]["CDINDEX"].ToString(),
                                               ds.Tables[0].Rows[i]["CDCODE"].ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_GB_23S40973");
                            e.Successed = false;
                            return;
                        }

                        if (fsCDINDEX.ToString() != ds.Tables[0].Rows[i]["CDINDEX"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_26B9D824");
                            e.Successed = false;
                            return;
                        }
                    }
                }
                //else
                //{
                //    this.ShowCustomMessage("사업계획 자료가 존재하여, 등록이 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    e.Successed = false;
                //    return;
                //}
            }
            //수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (UP_PBUPCheck(ds.Tables[1].Rows[0]["CDYEAR"].ToString()))
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["CDINDEX"].ToString() == "TA")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach(
                                               "TY_P_AC_77JH6224",
                                               ds.Tables[1].Rows[i]["CDITEM1"].ToString()
                                               );

                            if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                            {
                                this.ShowCustomMessage("[" + ds.Tables[1].Rows[i]["CDCODE"].ToString() + "][항목1:투자계정]을 확인하십시요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                            if (ds.Tables[1].Rows[i]["CDITEM2"].ToString() != "")
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach(
                                                   "TY_P_AC_77JH7225",
                                                   ds.Tables[1].Rows[i]["CDITEM2"].ToString()
                                                   );

                                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                                {
                                    this.ShowCustomMessage("[" + ds.Tables[1].Rows[i]["CDCODE"].ToString() + "][항목2:수선계정]을 확인하십시요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    this.ShowCustomMessage("사업계획 자료가 존재하여, 수정이 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    e.Successed = false;
                //    return;
                //}
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

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYBSKB005B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_AC_77JBH215_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_77JBA209", this.FPS91_TY_S_AC_77JBH215.GetValue("CDYEAR").ToString(), 
                                                        this.FPS91_TY_S_AC_77JBH215.GetValue("CDCODE").ToString());

            fsCDYEAR = this.FPS91_TY_S_AC_77JBH215.GetValue("CDYEAR").ToString();
            fsCDINDEX = this.FPS91_TY_S_AC_77JBH215.GetValue("CDCODE").ToString();

            this.FPS91_TY_S_AC_77JBG214.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : row 추가
        private void FPS91_TY_S_AC_77JBG214_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_77JBG214.SetValue(e.RowIndex, "CDYEAR", fsCDYEAR);
            this.FPS91_TY_S_AC_77JBG214.SetValue(e.RowIndex, "CDINDEX", fsCDINDEX);
            //if (fsCDINDEX != "00")
            //{
            //    this.FPS91_TY_S_AC_77JBG214.SetValue(e.RowIndex, "CDCODE", fsCDINDEX + "-");
            //}
        }
        #endregion

        #region Description : 사업계획-영업계획 등록 체크
        private bool UP_PBUPCheck(string sYEAR)
        {
            bool bRtn = true;
            DataTable dt = new DataTable();

            // 공통, 자제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAW449", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }
            // 매출액,취급량
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAX450", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }
            // 투자,수선
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAY451", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }

            return bRtn;
        }
        #endregion

        #region Description : 최근년도 가져오기
        private string UP_Get_MaxYear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8AMDV999");
            string sMaxYear = this.DbConnector.ExecuteScalar().ToString();

            return sMaxYear;
        }
        #endregion
    }
}
