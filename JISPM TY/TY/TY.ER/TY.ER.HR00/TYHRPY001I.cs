using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;

namespace TY.ER.HR00
{
    /// <summary>
    /// 호봉관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.16 11:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CGB1823 : 호봉관리 상세내역 등록
    ///  TY_P_HR_4CGB5818 : 호봉관리 마스타 조회
    ///  TY_P_HR_4CGB5826 : 호봉관리 마스타 수정
    ///  TY_P_HR_4CGB7819 : 호봉관리 상세내역 조회
    ///  TY_P_HR_4CGB9821 : 호봉관리 마스타 등록
    ///  TY_P_HR_4CGBB832 : 호봉관리 상세내역 수정
    ///  TY_P_HR_4CGBC833 : 호봉관리 마스타 삭제
    ///  TY_P_HR_4CGBD834 : 호봉관리 상세내역 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CGBU837 : 호봉관리 마스타 조회
    ///  TY_S_HR_4CGBV838 : 호봉관리 상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBJKCD : 직급
    ///  GGUBUN : 구분
    ///  AERDATE : 기준일자
    /// </summary>
    public partial class TYHRPY001I : TYBase
    {
        #region Description : 페이지 로드
        public TYHRPY001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CGBU837, "HBMJKCD", "HBMJKCDNM", "HBMJKCD");            
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CGBV838, "HBSJKCD", "HBSJKCDNM", "HBSJKCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CGBV838, "HBSPAYCODE", "HBSPAYCODENM", "HBSPAYCODE");
        }

        private void TYHRPY001I_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateHOBNCOPY = new ToolStripMenuItem("호봉복사");
            reateHOBNCOPY.Click += new EventHandler(HOBNCOPY_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_4CGBU837.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateHOBNCOPY });


            DTP01_AERDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));


            this.CBO01_GGUBUN.SelectedIndex = 1;

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBU837, "HBMJKCD");            
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBU837, "HBMSDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBV838, "HBSJKCD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBV838, "HBSPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBV838, "HBSHOBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CGBV838, "HBSSDATE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "0")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4CGB5818",
                    CBH01_KBJKCD.GetValue().ToString()
                    );
            }
            else
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4CGHN850",
                    CBH01_KBJKCD.GetValue().ToString(),
                    this.DTP01_AERDATE.GetString()
                    );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4CGBU837.SetValue(dt);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            string tmp = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                tmp = "M";
            }

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4CGBC833", ds.Tables[0].Rows[i]["HBMJKCD"],
                                                            ds.Tables[0].Rows[i]["HBMSDATE"]); //마스타 삭제
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4CGBD834", ds.Tables[1].Rows[i]["HBSJKCD"],
                                                            ds.Tables[1].Rows[i]["HBSPAYCODE"],
                                                            ds.Tables[1].Rows[i]["HBSHOBN"],
                                                            ds.Tables[1].Rows[i]["HBSSDATE"]
                                                            ); //상세내역 삭제
            }
            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
            if (tmp != "M")
            {
                FPS91_TY_S_HR_4CGBU837_CellDoubleClick(null, null);
            }
            else
            {
                FPS91_TY_S_HR_4CGBV838.Initialize();
            }
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            string sHBMEDATE = string.Empty;
            string sHBSEDATE = string.Empty;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["HBMEDATE"].ToString() != "19000101")
                {
                    sHBMEDATE = ds.Tables[0].Rows[i]["HBMEDATE"].ToString();
                }
                this.DbConnector.Attach("TY_P_HR_4CGB9821", ds.Tables[0].Rows[i]["HBMJKCD"],
                                                            ds.Tables[0].Rows[i]["HBMSDATE"],
                                                            sHBMEDATE,
                                                            TYUserInfo.EmpNo); //마스타 등록
                sHBMEDATE = "";
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["HBMEDATE"].ToString() != "19000101")
                {
                    sHBMEDATE = ds.Tables[1].Rows[i]["HBMEDATE"].ToString();
                }
                this.DbConnector.Attach("TY_P_HR_4CGB5826", sHBMEDATE,
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["HBMJKCD"],
                                                            ds.Tables[1].Rows[i]["HBMSDATE"]); //마스타 수정
                this.DbConnector.Attach("TY_P_HR_4CGFB844", sHBMEDATE,
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["HBMJKCD"],
                                                            ds.Tables[1].Rows[i]["HBMSDATE"]); //상세내역 종료일자 업데이트
                sHBMEDATE = "";
            }
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                if (ds.Tables[2].Rows[i]["HBSEDATE"].ToString() != "19000101")
                {
                    sHBSEDATE = ds.Tables[2].Rows[i]["HBSEDATE"].ToString();
                }
                this.DbConnector.Attach("TY_P_HR_4CGB1823", ds.Tables[2].Rows[i]["HBSJKCD"],
                                                            ds.Tables[2].Rows[i]["HBSPAYCODE"],
                                                            ds.Tables[2].Rows[i]["HBSHOBN"],
                                                            ds.Tables[2].Rows[i]["HBSSDATE"],
                                                            ds.Tables[2].Rows[i]["HBSSTAMOUNT"],
                                                            sHBSEDATE,
                                                            TYUserInfo.EmpNo); //상세내역 등록
                sHBSEDATE = "";
            }
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                if (ds.Tables[3].Rows[i]["HBSEDATE"].ToString() != "19000101")
                {
                    sHBSEDATE = ds.Tables[3].Rows[i]["HBSEDATE"].ToString();
                }
                this.DbConnector.Attach("TY_P_HR_4CGBB832", ds.Tables[3].Rows[i]["HBSSTAMOUNT"],
                                                            sHBSEDATE,
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[3].Rows[i]["HBSJKCD"],
                                                            ds.Tables[3].Rows[i]["HBSPAYCODE"],
                                                            ds.Tables[3].Rows[i]["HBSHOBN"],
                                                            ds.Tables[3].Rows[i]["HBSSDATE"]); //상세내역 수정
                sHBSEDATE = "";
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
            FPS91_TY_S_HR_4CGBU837_CellDoubleClick(null, null);
        }
        #endregion

        #region Description : 마스타 조회 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4CGBU837_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "0")
            {
                this.DbConnector.Attach(
                    "TY_P_HR_4CGB7819",
                    this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMJKCD").ToString(),
                    this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMSDATE").ToString()
                    );
            }
            else
            {
                this.DbConnector.Attach(
                    "TY_P_HR_4CGHH848",
                    this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMJKCD").ToString(),
                    this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMSDATE").ToString(),
                    this.DTP01_AERDATE.GetString()
                    );
            }
            this.FPS91_TY_S_HR_4CGBV838.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 마스타 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBU837.GetDataSourceInclude(TSpread.TActionType.New, "HBMJKCD", "HBMSDATE", "HBMEDATE"));
            // 마스타 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBU837.GetDataSourceInclude(TSpread.TActionType.Update, "HBMJKCD", "HBMSDATE", "HBMEDATE"));
            // 상세내역 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBV838.GetDataSourceInclude(TSpread.TActionType.New, "HBSJKCD", "HBSHOBN", "HBSPAYCODE", "HBSSTAMOUNT", "HBSSDATE", "HBSEDATE"));
            // 상세내역 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBV838.GetDataSourceInclude(TSpread.TActionType.Update, "HBSJKCD", "HBSHOBN", "HBSPAYCODE", "HBSSTAMOUNT", "HBSSDATE", "HBSEDATE"));

            //마스타 신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_HR_4CGDM842",
                                       ds.Tables[0].Rows[i]["HBMJKCD"].ToString(),
                                       ds.Tables[0].Rows[i]["HBMSDATE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[0].Rows[i]["HBMJKCD"].ToString() == ds.Tables[0].Rows[j]["HBMJKCD"].ToString() && 
                            ds.Tables[0].Rows[i]["HBMSDATE"].ToString() == ds.Tables[0].Rows[j]["HBMSDATE"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_3219C986");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
            //상세내역 신규
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMJKCD").ToString() != ds.Tables[2].Rows[i]["HBSJKCD"].ToString())
                {   
                    this.ShowCustomMessage("직급이 일치하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
                if (this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMSDATE").ToString() != ds.Tables[2].Rows[i]["HBSSDATE"].ToString())
                {
                    this.ShowCustomMessage("시작일자가 일치하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                    
                }
            
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_HR_4CGET843",
                                       ds.Tables[2].Rows[i]["HBSJKCD"].ToString(),
                                       ds.Tables[2].Rows[i]["HBSPAYCODE"].ToString(),
                                       ds.Tables[2].Rows[i]["HBSHOBN"].ToString(),
                                       ds.Tables[2].Rows[i]["HBSSDATE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
                for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[2].Rows[i]["HBSJKCD"].ToString() == ds.Tables[2].Rows[j]["HBSJKCD"].ToString() && ds.Tables[2].Rows[i]["HBSPAYCODE"].ToString() == ds.Tables[2].Rows[j]["HBSPAYCODE"].ToString() &&
                            ds.Tables[2].Rows[i]["HBSHOBN"].ToString() == ds.Tables[2].Rows[j]["HBSHOBN"].ToString() && ds.Tables[2].Rows[i]["HBSSDATE"].ToString() == ds.Tables[2].Rows[j]["HBSSDATE"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_3219C986");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0)
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBU837.GetDataSourceInclude(TSpread.TActionType.Remove, "HBMJKCD",  "HBMSDATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CGBV838.GetDataSourceInclude(TSpread.TActionType.Remove, "HBSJKCD", "HBSPAYCODE", "HBSHOBN", "HBSSDATE"));

            //상세내역 존재유무 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CGB7819",
                                        ds.Tables[0].Rows[i]["HBMJKCD"].ToString(),
                                        ds.Tables[0].Rows[i]["HBMSDATE"].ToString()
                                        );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_49PHQ078");
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 상세내역 행 추가 이벤트
        private void FPS91_TY_S_HR_4CGBV838_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CGBV838.SetValue(e.RowIndex, "HBSJKCD", this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMJKCD").ToString());
            this.FPS91_TY_S_HR_4CGBV838.SetValue(e.RowIndex, "HBSJKCDNM", this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMJKCDNM").ToString());
            this.FPS91_TY_S_HR_4CGBV838.SetValue(e.RowIndex, "HBSSDATE", this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMSDATE").ToString());
        }
        #endregion

        #region Description : 종료일자 미입력시 공백처리
        private void FPS91_TY_S_HR_4CGBU837_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (this.FPS91_TY_S_HR_4CGBU837.GetValue("HBMEDATE").ToString() == "19000101")
            {
                this.FPS91_TY_S_HR_4CGBU837.SetValue("HBMEDATE", "");
            }
        }
        #endregion

        #region  Description : 호봉복사 처리 팝업 이벤트
        private void HOBNCOPY_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataTable dk = (this.FPS91_TY_S_HR_4CGBU837.GetDataSourceInclude(TSpread.TActionType.All, "HBMJKCD", "HBMSDATE", "HBMEDATE"));

            DataTable dt = dk.Clone();            

            int iRowIndex = 0;

            for (int i = 0; i < this.FPS91_TY_S_HR_4CGBU837.ActiveSheet.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;

                if (this.FPS91_TY_S_HR_4CGBU837_Sheet1.Cells[iRowIndex - 1, 4].Text == "True")
                {
                    dt.Rows.Add(this.FPS91_TY_S_HR_4CGBU837_Sheet1.Cells[iRowIndex - 1, 0].Text,
                                this.FPS91_TY_S_HR_4CGBU837_Sheet1.Cells[iRowIndex - 1, 2].Value.ToString().Replace("19000101", ""),
                                this.FPS91_TY_S_HR_4CGBU837_Sheet1.Cells[iRowIndex - 1, 3].Value.ToString().Replace("19000101", ""));
                }
            }            

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("선택한 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
               if (dt.Rows[i]["HBMEDATE"].ToString().Replace("19000101", "") == "")
               {
                  this.ShowCustomMessage("종료일자가 없는경우 선택할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                  return;
               }

               if (i != 0)
               {
                   if (dt.Rows[i-1]["HBMEDATE"].ToString() != dt.Rows[i]["HBMEDATE"].ToString())
                   {
                       this.ShowCustomMessage("종료일자가 서로 다른경우 동시에 복사에 할수 없습니다 !", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                       return;
                   }
               }
            }

            TYHRPY01C1 popup = new TYHRPY01C1(dt);

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);  
            }

        }
        #endregion

        #region  Description : 일괄등록 버튼 이벤트
        private void BTN61_SAV_BATCH_Click(object sender, EventArgs e)
        {
            TYHRPY01C2 popup = new TYHRPY01C2();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion




    }
}
