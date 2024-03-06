using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// SOUNDING 대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.28 18:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73SIP124 : SOUNDING 대장 조회
    ///  TY_P_UT_73SIV126 : SERVER 파일 조회
    ///  TY_P_UT_73SIV127 : SOUNDING 파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_8BFF3139 : SOUNDING 대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  SNDATE : 일자
    ///  SNTANKNO :  TANK 번호
    /// </summary>
    public partial class TYUTPR030I : TYBase
    {
        #region Descriptin : 폼 로드
        public TYUTPR030I()
        {
            InitializeComponent();

            // 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_8BFEN138, "TMHWAMUL", "HMDESC1", "TMHWAMUL");
        }

        private void TYUTPR030I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_8BFEN138.Initialize();

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_8BFEN138, "TMDATE", "TMTANKNO");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_TMDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_TMDATE);
        }
        #endregion

        #region Description : SOUNDING(감사용) 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_8BFEN138.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_8BFF8140");

            this.FPS91_TY_S_UT_8BFEN138.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : SOUNDING(감사용) 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 저장
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    this.DbConnector.Attach("TY_P_UT_8BFFG142", Set_TankNo(ds.Tables[0].Rows[i]["TMTANKNO"].ToString()),
                                                                ds.Tables[0].Rows[i]["TMDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["TMHWAMUL"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_8BFFK143", ds.Tables[1].Rows[i]["TMHWAMUL"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["TMTANKNO"].ToString().Trim(),
                                                                ds.Tables[1].Rows[i]["TMDATE"].ToString()
                                                                );                    
                }
            }

            // 삭제
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_8BFFM144", ds.Tables[2].Rows[i]["TMTANKNO"].ToString().Trim(),
                                                                ds.Tables[2].Rows[i]["TMDATE"].ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_MR_2BF50354"); // 처리 메세지
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_8BFF3139.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_8BFHA145", this.DTP01_TMDATE.GetString(),
                                                        this.DTP01_EDIPHANG.GetString(),
                                                        this.TXT01_TMTANKNO.GetValue().ToString().Trim()
                                                        );

            this.FPS91_TY_S_UT_8BFF3139.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN62_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_8BFHA145", this.DTP01_TMDATE.GetString(),
                                                        this.DTP01_EDIPHANG.GetString(),
                                                        this.TXT01_TMTANKNO.GetValue().ToString().Trim()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR030R();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_8BFEN138.GetDataSourceInclude(TSpread.TActionType.New,    "TMDATE", "TMTANKNO", "TMHWAMUL"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_8BFEN138.GetDataSourceInclude(TSpread.TActionType.Update, "TMDATE", "TMTANKNO", "TMHWAMUL"));

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_8BFEN138.GetDataSourceInclude(TSpread.TActionType.Remove, "TMDATE", "TMTANKNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            // 신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 화물
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HM",
                                       ds.Tables[0].Rows[i]["TMHWAMUL"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66SDH426",
                                       ds.Tables[0].Rows[i]["TMTANKNO"].ToString().Trim()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    e.Successed = false;
                    return;
                }

                // 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_8BFF3141",
                                       ds.Tables[0].Rows[i]["TMDATE"].ToString(),
                                       ds.Tables[0].Rows[i]["TMTANKNO"].ToString().Trim()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            string sCJJISINO1 = string.Empty;

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 화물
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HM",
                                       ds.Tables[1].Rows[i]["TMHWAMUL"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66SDH426",
                                       ds.Tables[1].Rows[i]["TMTANKNO"].ToString().Trim()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_676GD601");
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

            e.ArgData = ds;
        }
        #endregion
    }
}
