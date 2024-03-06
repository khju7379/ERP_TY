using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전표POSTING 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.13 22:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CDAW160 : 전표POSTING
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ004B : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACBJ004B()
        {
            InitializeComponent();
            this.SetPopupStyle(); 
        }

        private void TYACBJ004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));     

            this.SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            try
            {
                // 일 총계정 원장 및 일 관리 원장, 월 관리 원장 생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CA1L004", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), TYUserInfo.EmpNo, this.CBO01_GOKCR.GetValue(), sOUTMSG.ToString());
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
            }
            finally
            {
                if (sOUTMSG.Length > 0)
                {
                    if (CBO01_GOKCR.GetValue().ToString() == "INS")  //생성
                    {
                        if (sOUTMSG.Substring(0, 2) == "ER")
                        {
                            this.ShowMessage("TY_M_GB_26E31876");
                        }
                        else
                        {
                            this.ShowMessage("TY_M_GB_26E30875");
                        }
                    }
                    else  //취소
                    {
                        if (sOUTMSG.Substring(0, 2) == "ER")
                        {
                            this.ShowMessage("TY_M_AC_2CDB1168");
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2CDB1167");
                        }
                    }
                }
                else
                {
                    if (CBO01_GOKCR.GetValue().ToString() == "INS")  //생성
                    {
                        this.ShowMessage("TY_M_GB_26E31876");
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2CDB1168");
                    }
                }
            }

        }
        #endregion

        #region Description : BTN61_BATCH_ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double d2DR01 = 0 ; 
            double d2DR02 = 0 ; 
            double d2DR03 = 0 ; 
            double d2DR04 = 0 ; 
            double d2DR05 = 0 ; 
            double d2DR06 = 0 ; 
            double d2DR07 = 0 ; 
            double d2DR08 = 0 ; 
            double d2DR09 = 0 ; 
            double d2DR10 = 0 ; 
            double d2DR11 = 0 ; 
            double d2DR12 = 0 ; 
            double d2CR01 = 0 ; 
            double d2CR02 = 0 ; 
            double d2CR03 = 0 ; 
            double d2CR04 = 0 ; 
            double d2CR05 = 0 ; 
            double d2CR06 = 0 ; 
            double d2CR07 = 0 ; 
            double d2CR08 = 0 ; 
            double d2CR09 = 0 ; 
            double d2CR10 = 0 ; 
            double d2CR11 = 0 ; 
            double d2CR12 = 0 ; 

            if ((this.DTP01_GSTDATE.GetString().ToString().Substring(0, 4) != this.DTP01_GEDDATE.GetString().ToString().Substring(0, 4)) ||
                 (this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) != this.DTP01_GEDDATE.GetString().ToString().Substring(4, 2)))
            {
                this.ShowMessage("TY_M_AC_38C4A377");
                e.Successed = false;
                this.SetFocus(this.DTP01_GEDDATE);
                return;
            }
            
            if (CBO01_GOKCR.GetValue().ToString() == "INS")  //생성
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                d2DR01 = 0 ; 
                d2DR02 = 0 ; 
                d2DR03 = 0 ; 
                d2DR04 = 0 ; 
                d2DR05 = 0 ; 
                d2DR06 = 0 ; 
                d2DR07 = 0 ; 
                d2DR08 = 0 ; 
                d2DR09 = 0 ; 
                d2DR10 = 0 ; 
                d2DR11 = 0 ; 
                d2DR12 = 0 ; 
                d2CR01 = 0 ; 
                d2CR02 = 0 ; 
                d2CR03 = 0 ; 
                d2CR04 = 0 ; 
                d2CR05 = 0 ; 
                d2CR06 = 0 ; 
                d2CR07 = 0 ; 
                d2CR08 = 0 ; 
                d2CR09 = 0 ; 
                d2CR10 = 0 ; 
                d2CR11 = 0 ; 
                d2CR12 = 0 ; 

                // 삭제 체크(월마감 자료 존재 체크)

                DataTable dt = new DataTable();
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_C3BAQ144", this.DTP01_GSTDATE.GetString().ToString().Substring(0, 4));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    d2DR01 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR01"].ToString()));
                    d2DR02 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR02"].ToString()));
                    d2DR03 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR03"].ToString()));
                    d2DR04 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR04"].ToString()));
                    d2DR05 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR05"].ToString()));
                    d2DR06 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR06"].ToString()));
                    d2DR07 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR07"].ToString())); 
                    d2DR08 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR08"].ToString())); 
                    d2DR09 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR09"].ToString())); 
                    d2DR10 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR10"].ToString())); 
                    d2DR11 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR11"].ToString())); 
                    d2DR12 = double.Parse(Get_Numeric(dt.Rows[0]["C2DR12"].ToString())); 
                    d2CR01 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR01"].ToString())); 
                    d2CR02 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR02"].ToString())); 
                    d2CR03 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR03"].ToString())); 
                    d2CR04 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR04"].ToString())); 
                    d2CR05 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR05"].ToString())); 
                    d2CR06 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR06"].ToString())); 
                    d2CR07 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR07"].ToString())); 
                    d2CR08 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR08"].ToString())); 
                    d2CR09 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR09"].ToString())); 
                    d2CR10 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR10"].ToString())); 
                    d2CR11 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR11"].ToString())); 
                    d2CR12 = double.Parse(Get_Numeric(dt.Rows[0]["C2CR12"].ToString())); 

                    if((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "01") && (d2DR01 != 0 || d2CR01 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "02") && (d2DR02 != 0 || d2CR02 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "03") && (d2DR03 != 0 || d2CR03 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "04") && (d2DR04 != 0 || d2CR04 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "05") && (d2DR05 != 0 || d2CR05 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "06") && (d2DR06 != 0 || d2CR06 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "07") && (d2DR07 != 0 || d2CR07 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "08") && (d2DR08 != 0 || d2CR08 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "09") && (d2DR09 != 0 || d2CR09 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "10") && (d2DR10 != 0 || d2CR10 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "11") && (d2DR11 != 0 || d2CR11 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                    else if ((this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) == "12") && (d2DR12 != 0 || d2CR12 != 0))
                    {
                        this.ShowMessage("TY_M_AC_C3BAU145");
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GEDDATE);
                        return;
                    }
                }

                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
