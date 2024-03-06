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
    /// 폐기물 탱크일일관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.25 18:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6APIN506 : 폐기물 탱크일일관리 조회
    ///  TY_P_UT_6APIN507 : 출고 지시서 파일 체크
    ///  TY_P_UT_6APJ1512 : 폐수조 재고 등록
    ///  TY_P_UT_6APJ2513 : 폐수조 재고 수정
    ///  TY_P_UT_6APJ4514 : 폐수조 재고 체크
    ///  TY_P_UT_6APJ6508 : 폐기물 탱크일일관리 등록
    ///  TY_P_UT_6APJ8510 : 폐기물 탱크일일관리 수정
    ///  TY_P_UT_6APJ9511 : 폐기물 탱크일일관리 삭제
    ///  TY_P_UT_6APJA515 : 폐수조 마지막 재고 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6APJB518 : 폐기물 탱크일일관리 조회
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
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SFHWAMUL : 화물
    ///  SFILJA : 일자
    ///  SFTICKNO : 번호
    /// </summary>
    public partial class TYUTDI001I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTDI001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_6APJB518, "SFHWAMUL", "CDDESC1", "SFHWAMUL");
        }

        private void TYUTDI001I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SFILJA.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6APJB518, "SFILJA");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6APJB518, "SFTICKNO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.DTP01_SFILJA);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_6APJB518.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6APIN506", this.DTP01_SFILJA.GetString(),
                                                        this.TXT01_SFTICKNO.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6APJB518.SetValue(dt);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                double dSJJUNQTY = 0;
                double dSJIPGOQTY = 0;
                double dSJCHULQTY = 0;
                double dSJJEGOQTY = 0;

                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
                DataTable dtTmp = new DataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6APJ9511", dt.Rows[i]["SFILJA"].ToString(),
                                                                dt.Rows[i]["SFTICKNO"].ToString());
                    this.DbConnector.ExecuteNonQueryList();

                    // 전일 마지막 재고 조회
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJA515", this.DTP01_SFILJA.GetString());

                    dtTmp = this.DbConnector.ExecuteDataTable();

                    if (dtTmp.Rows.Count > 0)
                    {
                        // 전일 재고
                        dSJJUNQTY = Convert.ToDouble(dtTmp.Rows[0]["SJJUNQTY"].ToString());
                        // 입고량
                        dSJIPGOQTY = Convert.ToDouble(dtTmp.Rows[0]["SJIPGOQTY"].ToString());
                        // 출고량
                        dSJCHULQTY = Convert.ToDouble(dtTmp.Rows[0]["SJCHULQTY"].ToString());
                    }

                    // 입고량 = 전 입고량 - 확인시 입고량
                    dSJIPGOQTY = dSJIPGOQTY - Convert.ToDouble(dt.Rows[i]["SFIPGOQTY"].ToString());

                    // 출고량 = 전 출고량 - 확인시 출고량
                    dSJCHULQTY = dSJCHULQTY - Convert.ToDouble(dt.Rows[i]["SFCHULQTY"].ToString());

                    // 재고량 = 전일재고 + 입고량 - 출고량
                    dSJJEGOQTY = dSJJUNQTY + dSJIPGOQTY - dSJCHULQTY;

                    // 폐수조 재고 업데이트
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJ2513", dSJJUNQTY,
                                                                dSJIPGOQTY,
                                                                dSJCHULQTY,
                                                                dSJJEGOQTY,
                                                                dt.Rows[i]["SFILJA"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                this.BTN61_INQ_Click(null, null);
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
            DataTable dt = this.FPS91_TY_S_UT_6APJB518.GetDataSourceInclude(TSpread.TActionType.Remove, "SFILJA", "SFTICKNO", "SFTANKNO", "SFHWAMUL", "SFIPGOQTY", "SFCHULQTY", "SJJEGOQTY");

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

            double dSJJUNQTY = 0;
            double dSJIPGOQTY = 0;
            double dSJCHULQTY = 0;
            double dSJJEGOQTY = 0;

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJ6508", ds.Tables[0].Rows[i]["SFILJA"].ToString(),
                                                                ds.Tables[0].Rows[i]["SFTICKNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["SFTANKNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["SFHWAMUL"].ToString(),
                                                                ds.Tables[0].Rows[i]["SFIPGOQTY"].ToString(),
                                                                ds.Tables[0].Rows[i]["SFCHULQTY"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();

                    // 재고파일 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJ4514", this.DTP01_SFILJA.GetString());

                    dt = this.DbConnector.ExecuteDataTable();
                    
                    if (dt.Rows.Count > 0)
                    {
                        // 전일 마지막 재고 조회
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJA515", this.DTP01_SFILJA.GetString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 전일 재고
                            dSJJUNQTY = Convert.ToDouble(dt.Rows[0]["SJJUNQTY"].ToString());
                            // 입고량
                            dSJIPGOQTY = Convert.ToDouble(dt.Rows[0]["SJIPGOQTY"].ToString());
                            // 출고량
                            dSJCHULQTY = Convert.ToDouble(dt.Rows[0]["SJCHULQTY"].ToString());
                        }

                        // 입고량 = 전 입고량 + 입력된 입고량
                        dSJIPGOQTY = dSJIPGOQTY + Convert.ToDouble(ds.Tables[0].Rows[i]["SFIPGOQTY"].ToString());

						// 출고량 = 전 출고량 + 입력된 출고량
                        dSJCHULQTY = dSJCHULQTY + Convert.ToDouble(ds.Tables[0].Rows[i]["SFCHULQTY"].ToString());

						// 재고량 = 전일재고 + 입고량 - 출고량
						dSJJEGOQTY = dSJJUNQTY + dSJIPGOQTY - dSJCHULQTY;

                        // 폐수조 재고 업데이트
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJ2513", dSJJUNQTY,
                                                                    dSJIPGOQTY,
                                                                    dSJCHULQTY,
                                                                    dSJJEGOQTY,
                                                                    ds.Tables[0].Rows[i]["SFILJA"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                        
                    }
                    else
                    {
                        // 전일 마지막 재고 조회
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJA515", this.DTP01_SFILJA.GetString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 전일 재고
                            dSJJUNQTY = Convert.ToDouble(dt.Rows[0]["SJJUNQTY"].ToString());
                        }

                        // 입고량 = 전 입고량 + 입력된 입고량
                        dSJIPGOQTY = dSJIPGOQTY + Convert.ToDouble(ds.Tables[0].Rows[i]["SFIPGOQTY"].ToString());

                        // 출고량 = 전 출고량 + 입력된 출고량
                        dSJCHULQTY = dSJCHULQTY + Convert.ToDouble(ds.Tables[0].Rows[i]["SFCHULQTY"].ToString());

                        // 재고량 = 전일재고 + 입고량 - 출고량
                        dSJJEGOQTY = dSJJUNQTY + dSJIPGOQTY - dSJCHULQTY;

                        // 폐수조 재고 등록
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJ1512", ds.Tables[0].Rows[i]["SFILJA"].ToString(),
                                                                    dSJJUNQTY,
                                                                    dSJIPGOQTY,
                                                                    dSJCHULQTY,
                                                                    dSJJEGOQTY
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    dSJJUNQTY = 0;
                    dSJIPGOQTY = 0;
                    dSJCHULQTY = 0;
                    dSJJEGOQTY = 0;
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    // 기존 재고 확인
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APKB538", ds.Tables[1].Rows[i]["SFILJA"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFTICKNO"].ToString());

                    dt2 = this.DbConnector.ExecuteDataTable();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJ8510", ds.Tables[1].Rows[i]["SFTANKNO"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFHWAMUL"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFIPGOQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFCHULQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFILJA"].ToString(),
                                                                ds.Tables[1].Rows[i]["SFTICKNO"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQueryList();

                    // 재고파일 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6APJ4514", this.DTP01_SFILJA.GetString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 전일 마지막 재고 조회
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJA515", this.DTP01_SFILJA.GetString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 전일 재고
                            dSJJUNQTY = Convert.ToDouble(dt.Rows[0]["SJJUNQTY"].ToString());
                            // 입고량
                            dSJIPGOQTY = Convert.ToDouble(dt.Rows[0]["SJIPGOQTY"].ToString());
                            // 출고량
                            dSJCHULQTY = Convert.ToDouble(dt.Rows[0]["SJCHULQTY"].ToString());
                        }

                        // 입고량 = 전 입고량 - 확인시 입고량 + 입력된 입고량
                        dSJIPGOQTY = dSJIPGOQTY - Convert.ToDouble(dt2.Rows[0]["SFIPGOQTY"].ToString()) + Convert.ToDouble(ds.Tables[1].Rows[i]["SFIPGOQTY"].ToString());

                        // 출고량 = 전 출고량 - 확인시 출고량 + 입력된 출고량
                        dSJCHULQTY = dSJCHULQTY - Convert.ToDouble(dt2.Rows[0]["SFCHULQTY"].ToString()) + Convert.ToDouble(ds.Tables[1].Rows[i]["SFCHULQTY"].ToString());

                        // 재고량 = 전일재고 + 입고량 - 출고량
                        dSJJEGOQTY = dSJJUNQTY + dSJIPGOQTY - dSJCHULQTY;

                        // 폐수조 재고 업데이트
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJ2513", dSJJUNQTY,
                                                                    dSJIPGOQTY,
                                                                    dSJCHULQTY,
                                                                    dSJJEGOQTY,
                                                                    ds.Tables[0].Rows[i]["SFILJA"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    else
                    {
                        // 전일 마지막 재고 조회
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJA515", this.DTP01_SFILJA.GetString());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            // 전일 재고
                            dSJJUNQTY = Convert.ToDouble(dt.Rows[0]["SJJUNQTY"].ToString());
                        }

                        // 입고량 = 전 입고량 - 확인시 입고량 + 입력된 입고량
                        dSJIPGOQTY = dSJIPGOQTY - Convert.ToDouble(dt2.Rows[0]["SFIPGOQTY"].ToString()) + Convert.ToDouble(ds.Tables[1].Rows[i]["SFIPGOQTY"].ToString());

                        // 출고량 = 전 출고량 - 확인시 출고량 + 입력된 출고량
                        dSJCHULQTY = dSJCHULQTY - Convert.ToDouble(dt2.Rows[0]["SFCHULQTY"].ToString()) + Convert.ToDouble(ds.Tables[1].Rows[i]["SFCHULQTY"].ToString());

                        // 재고량 = 전일재고 + 입고량 - 출고량
                        dSJJEGOQTY = dSJJUNQTY + dSJIPGOQTY - dSJCHULQTY;

                        // 폐수조 재고 등록
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6APJ1512", ds.Tables[0].Rows[i]["SFILJA"].ToString(),
                                                                    dSJJUNQTY,
                                                                    dSJIPGOQTY,
                                                                    dSJCHULQTY,
                                                                    dSJJEGOQTY
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    dSJJUNQTY = 0;
                    dSJIPGOQTY = 0;
                    dSJCHULQTY = 0;
                    dSJJEGOQTY = 0;
                }
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_6APJB518.GetDataSourceInclude(TSpread.TActionType.New, "SFILJA", "SFTICKNO", "SFTANKNO", "SFHWAMUL", "SFIPGOQTY", "SFCHULQTY", "SJJEGOQTY"));

            ds.Tables.Add(this.FPS91_TY_S_UT_6APJB518.GetDataSourceInclude(TSpread.TActionType.Update, "SFILJA", "SFTICKNO", "SFTANKNO", "SFHWAMUL", "SFIPGOQTY", "SFCHULQTY", "SJJEGOQTY"));


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

        #region Description : 그리드 탱크 번호 체크
        private void FPS91_TY_S_UT_6APJB518_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sTANKNO = this.FPS91_TY_S_UT_6APJB518.GetValue("SFTANKNO").ToString();

            if (sTANKNO.Length >= 3)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66SDH426", sTANKNO);

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("탱크번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_UT_6APJB518.SetValue("SFTANKNO", "");
                }
            }
        }
        #endregion

        #region Description : Row 추가 이벤트
        private void FPS91_TY_S_UT_6APJB518_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_6APJB518.SetValue("SFILJA", this.DTP01_SFILJA.GetValue().ToString());
            this.FPS91_TY_S_UT_6APJB518.SetValue("SFIPGOQTY", "0.000");
            this.FPS91_TY_S_UT_6APJB518.SetValue("SFCHULQTY", "0.000");
        }
        #endregion
    }
}
