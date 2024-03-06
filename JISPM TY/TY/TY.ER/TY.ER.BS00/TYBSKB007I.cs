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
    /// 관리공통비 배부관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.27 17:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_77RHG287 : 관리공통비 배부관리 등록(사업계획)
    ///  TY_P_AC_77SCP301 : 관리공통비 배부관리 수정(사업계획)
    ///  TY_P_AC_77SCX305 : 관리공통비 배부관리 조회(사업계획)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_77SCY307 : 관리공통비 배부관리 조회(사업계획)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYBSKB007I : TYBase
    {
        #region Descripton : 폼 로드
        public TYBSKB007I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_77SCY307, "MCUNIT", "UNDESC1", "MCUNIT");
        }

        private void TYBSKB007I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77SCY307, "MCYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77SCY307, "MCCODE");
            
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_VSYEAR.SetValue(UP_Get_MaxYear());
            
            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_VSYEAR);
        }
        #endregion

        #region Descripton : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {   
            this.FPS91_TY_S_AC_77SCY307.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_77SCX305", this.TXT01_VSYEAR.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_77SCY307.SetValue(UP_DtChange(dt));

            // 단위코드을 가져오기 위해서 년도 파라미터 날짜를 넣음.
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_77SCY307, "MCUNIT");
            if (tyCodeBox != null)
            {
                tyCodeBox.DummyValue = this.TXT01_VSYEAR.GetValue() + "UN";
            }

            //if (dt.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_AC_77SCY307.SetValue(UP_DtChange(dt));
            //}
            //else
            //{
            //    this.FPS91_TY_S_AC_77SCY307.SetValue(Set_EmptyDt());
            //}
            
        }
        #endregion

        #region Descripton : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            DataTable dt = new DataTable();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++ )
                {
                    // 등록 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_78LEN467", ds.Tables[0].Rows[i]["MCYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["MCCODE"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0) //수정
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_77SCP301", ds.Tables[0].Rows[i]["MCUNIT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["MCRATEUTT"].ToString().Replace(",", "").Trim(),
                                                                    ds.Tables[0].Rows[i]["MCRATESILO"].ToString().Replace(",", "").Trim(),
                                                                    ds.Tables[0].Rows[i]["MCRATETOTAL"].ToString().Replace(",", "").Trim(),
                                                                    ds.Tables[0].Rows[i]["MCRATEDESC"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["MCYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["MCCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteNonQuery();
                    }
                    else // 신규등록
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_77RHG287", ds.Tables[0].Rows[i]["MCYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["MCCODE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["MCUNIT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["MCRATEUTT"].ToString().Replace(",", ""),
                                                                    ds.Tables[0].Rows[i]["MCRATESILO"].ToString().Replace(",", ""),
                                                                    ds.Tables[0].Rows[i]["MCRATETOTAL"].ToString().Replace(",", ""),
                                                                    ds.Tables[0].Rows[i]["MCRATEDESC"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteNonQuery();
                    }
                }

                this.BTN61_INQ_Click(null, null);
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

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77SCY307.GetDataSourceInclude(TSpread.TActionType.Update, "MCYEAR", "MCCODE", "MCUNIT", "MCRATEUTT", "MCRATESILO", "MCRATETOTAL", "MCRATEDESC"));

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

        private DataTable UP_DtChange(DataTable dt)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("MCYEAR", typeof(System.String));
            retDt.Columns.Add("MCCODE", typeof(System.String));
            retDt.Columns.Add("MCDESC1", typeof(System.String));
            retDt.Columns.Add("MCUNIT", typeof(System.String));
            retDt.Columns.Add("UNDESC1", typeof(System.String));
            retDt.Columns.Add("MCRATEUTT", typeof(System.String));
            retDt.Columns.Add("MCRATESILO", typeof(System.String));
            retDt.Columns.Add("MCRATETOTAL", typeof(System.String));
            retDt.Columns.Add("MCRATEDESC", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                string sYEAR = this.TXT01_VSYEAR.GetValue().ToString();

                row["MCYEAR"] = dt.Rows[i]["MCYEAR"].ToString();
                row["MCCODE"] = dt.Rows[i]["MCCODE"].ToString();
                if (dt.Rows[i]["MCCODE"].ToString() == "01")
                {
                    row["MCDESC1"] = dt.Rows[i]["MCDESC1"].ToString();    
                }
                else
                {
                    row["MCDESC1"] = "    " + dt.Rows[i]["MCDESC1"].ToString();
                }
                
                row["MCUNIT"] = dt.Rows[i]["MCUNIT"].ToString();
                row["UNDESC1"] = dt.Rows[i]["UNDESC1"].ToString();
                row["MCRATEUTT"] = dt.Rows[i]["MCRATEUTT"].ToString();
                row["MCRATESILO"] = dt.Rows[i]["MCRATESILO"].ToString();
                row["MCRATETOTAL"] = dt.Rows[i]["MCRATETOTAL"].ToString();
                //row["MCRATEUTT"] = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[i]["MCRATEUTT"].ToString()));
                //row["MCRATESILO"] = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[i]["MCRATESILO"].ToString()));
                //row["MCRATETOTAL"] = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[i]["MCRATETOTAL"].ToString()));
                row["MCRATEDESC"] = dt.Rows[i]["MCRATEDESC"].ToString();

                retDt.Rows.Add(row);
            }

            return retDt;
        }

        #region Description : 복사버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYBSKB007B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 합계 구하기
        private void FPS91_TY_S_AC_77SCY307_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            double dMCRATEUTT = 0;
            double dMCRATESILO = 0;
            double dMCRATETOTAL = 0;

            if (this.FPS91_TY_S_AC_77SCY307.GetValue("MCRATEUTT").ToString() != "")
            {
                dMCRATEUTT = Convert.ToDouble(this.FPS91_TY_S_AC_77SCY307.GetValue("MCRATEUTT").ToString());
            }
            if (this.FPS91_TY_S_AC_77SCY307.GetValue("MCRATESILO").ToString() != "")
            {
                dMCRATESILO = Convert.ToDouble(this.FPS91_TY_S_AC_77SCY307.GetValue("MCRATESILO").ToString());
            }
            dMCRATETOTAL = dMCRATEUTT + dMCRATESILO;

            this.FPS91_TY_S_AC_77SCY307.SetValue("MCRATETOTAL", dMCRATETOTAL.ToString());
        }
        #endregion

        #region Description : 최근년도 가져오기
        private string UP_Get_MaxYear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8AME1005");
            string sMaxYear = this.DbConnector.ExecuteScalar().ToString();

            return sMaxYear;
        }
        #endregion
    }
}
