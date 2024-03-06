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
    ///  TY_S_US_96AEJ748 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSNJ027I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        
        #region Description : 페이지 로드
        public TYUSNJ027I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_965G3730, "HDSHANGCHA", "VSDESC1", "HDSHANGCHA");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96AE5741, "HIEHANGCHA", "VSDESC1", "HIEHANGCHA");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96AEJ748, "HPWHANGCHA", "VSDESC1", "HPWHANGCHA");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_96AEJ748, "HPWGOKJONG", "GKDESC1", "HPWGOKJONG");
        }

        private void TYUSNJ027I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_965G3730, "HDSHANGCHA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_965G3730, "HDSDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_965G3730, "HDSGUBN");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96AE5741, "HIEHANGCHA");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96AEJ748, "HPWHANGCHA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96AEJ748, "HPWGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_96AEJ748, "HPWGUBN");

            this.FPS91_TY_S_US_965G3730.Initialize();
            this.FPS91_TY_S_US_96AE5741.Initialize();
            this.FPS91_TY_S_US_96AEJ748.Initialize();
            

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN62_BATCH.ProcessCheck += new TButton.CheckHandler(BTN62_BATCH_ProcessCheck);
            this.BTN63_BATCH.ProcessCheck += new TButton.CheckHandler(BTN63_BATCH_ProcessCheck);

            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.CBH01_STHANGCHA.CodeText.Focus();
            };

            tmr.Interval = 100;
            tmr.Start();

            //SetStartingFocus(this.CBH01_STHANGCHA.CodeText);

            //SetStartingFocus(this.BTN61_INQ);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            #region Description : 항운노조 소급단가 조회

            this.FPS91_TY_S_US_965G3730.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_965G3729",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_965G3730.SetValue(dt);

            #endregion



            #region Description : 항운노조 임금공제 조회

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96AE4740",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_96AE5741.SetValue(dt);

            #endregion



            #region Description : 항운노조 후생복지비 조회

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_96AEI747",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_96AEJ748.SetValue(dt);

            #endregion
        }
        #endregion

        #region Description : 소급단가 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sHDSSEQ = string.Empty;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
                {
                    // 순번 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_US_96ADG739",
                                           ds.Tables[0].Rows[i]["HDSHANGCHA"].ToString().Trim(),
                                           Get_Date(ds.Tables[0].Rows[i]["HDSDATE"].ToString().Trim())
                                           );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sHDSSEQ = dt.Rows[0]["HDSSEQ"].ToString();
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_96ADA736", ds.Tables[0].Rows[i]["HDSHANGCHA"].ToString().Trim(),
                                                                Get_Date(ds.Tables[0].Rows[i]["HDSDATE"].ToString().Trim()),
                                                                ds.Tables[0].Rows[i]["HDSGUBN"].ToString().Trim(),
                                                                sHDSSEQ.ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HDSAGOCOST"].ToString().Trim()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["HDSSOCOST"].ToString().Trim()),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
                {
                    this.DbConnector.Attach("TY_P_US_96ADB737", Get_Numeric(ds.Tables[1].Rows[i]["HDSAGOCOST"].ToString().Trim()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["HDSSOCOST"].ToString().Trim()),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["HDSHANGCHA"].ToString().Trim(),
                                                                Get_Date(ds.Tables[1].Rows[i]["HDSDATE"].ToString().Trim()),
                                                                ds.Tables[1].Rows[i]["HDSGUBN"].ToString().Trim(),
                                                                ds.Tables[1].Rows[i]["HDSSEQ"].ToString().Trim()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
                {
                    this.DbConnector.Attach("TY_P_US_96ADC738", ds.Tables[2].Rows[i]["HDSHANGCHA"].ToString().Trim(),
                                                                Get_Date(ds.Tables[2].Rows[i]["HDSDATE"].ToString().Trim()),
                                                                ds.Tables[2].Rows[i]["HDSGUBN"].ToString().Trim(),
                                                                ds.Tables[2].Rows[i]["HDSSEQ"].ToString().Trim()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_MR_2BF50354");

            // 조회
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 임금공제 처리 버튼
        private void BTN62_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                this.DbConnector.Attach("TY_P_US_96AEA744", ds.Tables[0].Rows[i]["HIEHANGCHA"].ToString().Trim(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HIEGAGAMT"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HIEGAGYUL"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HIENJYUL"].ToString().Trim()),
                                                            TYUserInfo.EmpNo
                                                            );
            }            

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                this.DbConnector.Attach("TY_P_US_96AEA745", Get_Numeric(ds.Tables[1].Rows[i]["HIEGAGAMT"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["HIEGAGYUL"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["HIENJYUL"].ToString().Trim()),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["HIEHANGCHA"].ToString().Trim()
                                                            );
            }

            for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                this.DbConnector.Attach("TY_P_US_96AEB746", ds.Tables[2].Rows[i]["HIEHANGCHA"].ToString().Trim()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();
            

            this.ShowMessage("TY_M_MR_2BF50354");

            // 조회
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 후생복지비 처리 버튼
        private void BTN63_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                this.DbConnector.Attach("TY_P_US_96AEN750", ds.Tables[0].Rows[i]["HPWHANGCHA"].ToString().Trim(),
                                                            ds.Tables[0].Rows[i]["HPWGOKJONG"].ToString().Trim(),
                                                            ds.Tables[0].Rows[i]["HPWGUBN"].ToString().Trim(),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HPWEAMT"].ToString().Trim()),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                this.DbConnector.Attach("TY_P_US_96AEO751", Get_Numeric(ds.Tables[1].Rows[i]["HPWEAMT"].ToString().Trim()),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["HPWHANGCHA"].ToString().Trim(),
                                                            ds.Tables[1].Rows[i]["HPWGOKJONG"].ToString().Trim(),
                                                            ds.Tables[1].Rows[i]["HPWGUBN"].ToString().Trim()
                                                            );
            }

            for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                this.DbConnector.Attach("TY_P_US_96AEO752", ds.Tables[2].Rows[i]["HPWHANGCHA"].ToString().Trim(),
                                                            ds.Tables[2].Rows[i]["HPWGOKJONG"].ToString().Trim(),
                                                            ds.Tables[2].Rows[i]["HPWGUBN"].ToString().Trim()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();


            this.ShowMessage("TY_M_MR_2BF50354");

            // 조회
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 소급단가 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_965G3730.GetDataSourceInclude(TSpread.TActionType.New,    "HDSHANGCHA", "HDSDATE", "HDSGUBN", "HDSSEQ", "HDSAGOCOST", "HDSSOCOST"));
            ds.Tables.Add(this.FPS91_TY_S_US_965G3730.GetDataSourceInclude(TSpread.TActionType.Update, "HDSHANGCHA", "HDSDATE", "HDSGUBN", "HDSSEQ", "HDSAGOCOST", "HDSSOCOST"));
            ds.Tables.Add(this.FPS91_TY_S_US_965G3730.GetDataSourceInclude(TSpread.TActionType.Remove, "HDSHANGCHA", "HDSDATE", "HDSGUBN", "HDSSEQ"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
                {
                    // 기존단가
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HDSAGOCOST"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_US_96AD9734");

                        e.Successed = false;
                        return;
                    }

                    // 소급단가
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HDSSOCOST"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_US_96AD9735");

                        e.Successed = false;
                        return;
                    }
                }
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

        #region Description : 임금공제 처리 ProcessCheck
        private void BTN62_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_96AE5741.GetDataSourceInclude(TSpread.TActionType.New,    "HIEHANGCHA", "HIEGAGAMT", "HIEGAGYUL", "HIENJYUL"));
            ds.Tables.Add(this.FPS91_TY_S_US_96AE5741.GetDataSourceInclude(TSpread.TActionType.Update, "HIEHANGCHA", "HIEGAGAMT", "HIEGAGYUL", "HIENJYUL"));
            ds.Tables.Add(this.FPS91_TY_S_US_96AE5741.GetDataSourceInclude(TSpread.TActionType.Remove, "HIEHANGCHA"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
                {
                    // 갑근세, 갑근세유, 노조비율
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HIEGAGAMT"].ToString())) == 0 &&
                        double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HIEGAGYUL"].ToString())) == 0 &&
                        double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HIENJYUL"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_US_96AE1743");

                        e.Successed = false;
                        return;
                    }
                }
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

        #region Description : 후생복지비 처리 ProcessCheck
        private void BTN63_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_96AEJ748.GetDataSourceInclude(TSpread.TActionType.New,    "HPWHANGCHA", "HPWGOKJONG", "HPWGUBN", "HPWEAMT"));
            ds.Tables.Add(this.FPS91_TY_S_US_96AEJ748.GetDataSourceInclude(TSpread.TActionType.Update, "HPWHANGCHA", "HPWGOKJONG", "HPWGUBN", "HPWEAMT"));
            ds.Tables.Add(this.FPS91_TY_S_US_96AEJ748.GetDataSourceInclude(TSpread.TActionType.Remove, "HPWHANGCHA", "HPWGOKJONG", "HPWGUBN"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
            {
                // 후생복지비
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HPWEAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_96AEM749");

                    e.Successed = false;
                    return;
                }
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                // 후생복지비
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["HPWEAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_US_96AEM749");

                    e.Successed = false;
                    return;
                }
            }

            // 처리하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = ds;

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

        #region Description : 소급단가 스프레드
        private void FPS91_TY_S_US_965G3730_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Row == 0)
            {
                if (this.FPS91_TY_S_US_965G3730.ActiveSheet.RowCount > 1)
                {
                    for (int i = 1; i < this.FPS91_TY_S_US_965G3730.ActiveSheet.RowCount; i++)
                    {
                        // 항차
                        if (this.FPS91_TY_S_US_965G3730.GetValue(i, "HDSHANGCHA").ToString() == "")
                        {
                            this.FPS91_TY_S_US_965G3730.SetValue(i, "HDSHANGCHA", this.FPS91_TY_S_US_965G3730.GetValue(0, "HDSHANGCHA").ToString());
                        }

                        // 항차명
                        if (this.FPS91_TY_S_US_965G3730.GetValue(i, "VSDESC1").ToString() == "")
                        {
                            this.FPS91_TY_S_US_965G3730.SetValue(i, "VSDESC1", this.FPS91_TY_S_US_965G3730.GetValue(0, "VSDESC1").ToString());
                        }

                        // 일자
                        if (this.FPS91_TY_S_US_965G3730.GetValue(i, "HDSDATE").ToString() == "" && this.FPS91_TY_S_US_965G3730.GetValue(0, "HDSDATE").ToString() != "")
                        {
                            this.FPS91_TY_S_US_965G3730.SetValue(i, "HDSDATE", this.FPS91_TY_S_US_965G3730.GetValue(0, "HDSDATE").ToString());
                        }

                        // 구분
                        if (this.FPS91_TY_S_US_965G3730.GetValue(i, "HDSGUBN").ToString() == "")
                        {
                            this.FPS91_TY_S_US_965G3730.SetValue(i, "HDSGUBN", this.FPS91_TY_S_US_965G3730.GetValue(0, "HDSGUBN").ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //#region Description : 필드 ReadOnly
        //private void UP_Field_ReadOnly(bool boolean)
        //{
        //    this.DTP01_HYMCYYMM.SetReadOnly(boolean);
        //    this.CBH01_HYHANGCHA.SetReadOnly(boolean);
        //    this.CBH01_HYGOKJONG.SetReadOnly(boolean);
        //    this.CBH01_HYHWAJU.SetReadOnly(boolean);
        //    this.DTP01_HYYYMMDD.SetReadOnly(boolean);

        //    this.TXT01_HYBEJNQTY.SetReadOnly(boolean);
        //    this.TXT01_HYHWAKQTY.SetReadOnly(boolean);
        //    this.TXT01_HYCONTNO.SetReadOnly(boolean);

        //    this.CBO01_HYCRGUBUN.SetReadOnly(boolean);
        //}
        //#endregion

        //#region Description : 필드 클리어
        //private void UP_Field_Clear()
        //{
        //    this.CBH01_HYHANGCHA.SetValue("");
        //    this.CBH01_HYGOKJONG.SetValue("");
        //    this.CBH01_HYHWAJU.SetValue("");
            
        //    this.TXT01_HYBEJNQTY.SetValue("");
        //    this.TXT01_HYHWAKQTY.SetValue("");
        //    this.TXT01_HYCHQTY.SetValue("");
        //    this.TXT01_HYYDQTY.SetValue("");
        //    this.TXT01_HYCONTNO.SetValue("");
        //    this.TXT01_HYJPNO.SetValue("");

        //    this.TXT01_HYHAYKAMT.SetValue("");
        //    this.TXT01_HYHAYKVAT.SetValue("");
        //    this.TXT01_HYISAMT.SetValue("");
        //    this.TXT01_HYISVAT.SetValue("");
        //}
        //#endregion

        //#region Description : 화주 이벤트
        //private void CBH01_GHWAJU_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SetFocus(this.BTN61_INQ);
        //    }
        //}
        //#endregion

        //#region Description : 계약번호 이벤트
        //private void BTN61_CONTNO_Click(object sender, EventArgs e)
        //{
        //    TYUSGB005S popup = new TYUSGB005S(this.CBH01_HYHWAJU.GetValue().ToString());

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.TXT01_HYCONTNO.SetValue(popup.fsCONTNO);

        //        this.SetFocus(this.TXT01_HYCONTNO);
        //    }
        //}

        //private void TXT01_HYCONTNO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1)
        //    {
        //        TYUSGB005S popup = new TYUSGB005S(this.CBH01_HYHWAJU.GetValue().ToString());

        //        if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.TXT01_HYCONTNO.SetValue(popup.fsCONTNO);

        //            this.SetFocus(this.TXT01_HYCONTNO);
        //        }
        //    }
        //}
        //#endregion
    }
}