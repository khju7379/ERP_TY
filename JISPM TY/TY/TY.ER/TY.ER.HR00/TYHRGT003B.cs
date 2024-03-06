using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 휴무관리 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.01.31 16:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQFJ547 : 휴무관리 등록
    ///  TY_P_HR_4CJDX894 : 급여대상자관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_614E6381 : 급여항목추가 대상자 조회
    ///  TY_S_HR_71VH1613 : 휴무관리 일괄등록 대상자 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  GHCODE : 휴무코드
    ///  GHINSABUN : 입력사번
    ///  GHDATE : 휴무일자
    ///  GHEDDATE : 종료일자
    ///  GHEDTIME : 종료시간
    ///  GHHAENG : 행선지
    ///  GHSAYU : 휴무사유
    ///  GHSTDATE : 시작일자
    ///  GHSTTIME : 시작시간
    ///  GHTRWAY : 교통편
    /// </summary>
    public partial class TYHRGT003B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRGT003B()
        {
            InitializeComponent();
        }

        private void TYHRGT003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_SEL.ProcessCheck += new TButton.CheckHandler(BTN61_SEL_ProcessCheck);

            DTP01_GHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            DTP01_GHSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            DTP01_GHEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_GHCODE.SetValue("130");
            this.MTB01_GHSTTIME.SetValue("0900");
            this.MTB01_GHEDTIME.SetValue("1800");
            this.CBH01_GHINSABUN.SetValue("0259-F");

            BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_HR_71VH1613.Initialize();

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {         
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BQIG564", ds.Tables[0].Rows[i]["KBSABUN"].ToString(), this.DTP01_GHDATE.GetString(), this.CBH01_GHCODE.GetValue());
                Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                
                datas.Add(new object[] {  ds.Tables[0].Rows[i]["KBSABUN"].ToString(),
                                          DTP01_GHDATE.GetString().ToString(),
                                          CBH01_GHCODE.GetValue().ToString(),
                                          iSeq.ToString(),
                                          "1",
                                          DTP01_GHSTDATE.GetString().ToString(),
                                          MTB01_GHSTTIME.GetValue().ToString().Replace(":","").Trim(),
                                          DTP01_GHEDDATE.GetString().ToString(),
                                          MTB01_GHEDTIME.GetValue().ToString().Replace(":","").Trim(),
                                          TXT01_GHSAYU.GetValue().ToString(),
                                          "",
                                          "",
                                          CBH01_GHINSABUN.GetValue().ToString(),
                                          "",                                                              
                                          "",  
                                          "",  
                                          "",  
                                          TYUserInfo.EmpNo  });
          
            }
            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_HR_4BQFJ547", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }            

            this.ShowMessage("TY_M_GB_23NAD873");

            this.FPS91_TY_S_HR_71VH1613.Initialize();
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_71VH1613.GetDataSourceInclude(TSpread.TActionType.New, "KBSABUN", "KBHANGL"));

            if (ds.Tables[0].Rows.Count == 0)
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

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_614E6381.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_71VI3616", this.CBH01_KBSABUN.GetValue().ToString(), this.CBH01_KBJKCD.GetValue().ToString());
            this.FPS91_TY_S_HR_614E6381.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iRowIndex = iRowIndex + 1;

                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.AddRows(iRowIndex - 1, 1);
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 0].Text = dt.Rows[i]["KBSABUN"].ToString();
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 1].Text = dt.Rows[i]["KBHANGL"].ToString();
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 2].Text = dt.Rows[i]["KBJKCD"].ToString();
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["KBJKCDNM"].ToString();
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 4].Text = dt.Rows[i]["KBBUSEO"].ToString();
                    this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[iRowIndex - 1, 5].Text = dt.Rows[i]["KBBUSEONM"].ToString();
                }

                this.UP_PayListCount(Convert.ToInt16(this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Rows.Count));
            }

            this.BTN61_INQ_Click(null,null);
        }

        private void BTN61_SEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_614E6381.GetDataSourceInclude(TSpread.TActionType.Select, "KBSABUN", "KBHANGL", "KBBUSEO", "KBBUSEONM","KBJKCD","KBJKCDNM" );

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Rows.Count; j++)
                {
                    if (dt.Rows[i]["KBSABUN"].ToString() == this.FPS91_TY_S_HR_71VH1613.ActiveSheet.Cells[j, 0].Text.Trim())
                    {
                        this.ShowCustomMessage("등록되어있는 사번입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }            

            e.ArgData = dt;

        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            FPS91_TY_S_HR_71VH1613.Initialize();

            UP_PayListCount(0);
        }
        #endregion

        #region  Description : 대상자 표시
        private void UP_PayListCount(Int16 iCnt)
        {
            TXT01_ESCEMPCNT.SetValue(iCnt.ToString());
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
