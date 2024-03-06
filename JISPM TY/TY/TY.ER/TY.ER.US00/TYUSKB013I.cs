using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 모선 스케줄관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.25 11:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92PE0889 : 모선 스케줄관리 등록
    ///  TY_P_US_92PE2890 : 모선 스케줄관리 수정
    ///  TY_P_US_92PE3891 : 모선 스케줄관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  SHAGENT : 대리점
    ///  SHGOKJONG : 곡 종
    ///  SHSOSOK : 협 회
    ///  SHSURVEY : 검정사
    ///  SHWONSAN : 원산지
    ///  SHHWDATE : 적용년월
    ///  SHBUSANQTY : B/L량
    ///  SHCOMPANY : 회사구분
    ///  SHDATE : 기준일자
    ///  SHETABUSAN : 부 산
    ///  SHETABUTIME : 시 간
    ///  SHETAINCHE : 인 천
    ///  SHETAINTIME : 시 간
    ///  SHETAULSAN : 울 산
    ///  SHETAULTIME : 시 간
    ///  SHETBDATE : 접안예상일
    ///  SHETBTIME : 접안예상시간
    ///  SHETCD_E : 작업기간
    ///  SHETCD_S : 작업기간
    ///  SHHANGCHA : 모 선
    ///  SHINCHEQTY : B/L량
    ///  SHREMARK : 비 고
    ///  SHSEQ : 순 번
    ///  SHULSANQTY : B/L량
    ///  SHVESSEL : 모선명
    /// </summary>
    public partial class TYUSKB013I : TYBase
    {
        private string fsSHDATE;
        private string fsSHSEQ;

        #region  Description : 폼 로드 이벤트
        public TYUSKB013I(string sSHDATE, string sSHSEQ)
        {
            InitializeComponent();

            //this.SetPopupStyle();

            fsSHDATE = sSHDATE;
            fsSHSEQ = sSHSEQ;

        }

        private void TYUSKB013I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsSHDATE) )
            {
                DTP01_SHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                DTP01_SHHWDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

                MTB01_SHETCD_S.SetValue("");
                MTB01_SHETCD_E.SetValue("");
                MTB01_SHETBDATE.SetValue("");

                DataTable dt = new DataTable();

                UP_Set_Grid(dt);

                this.SetStartingFocus(DTP01_SHDATE);
            }
            else
            {
                UP_DataBinding(fsSHDATE, fsSHSEQ);

                UP_SetReadOnly();

                this.SetStartingFocus(TXT01_SHVESSEL);
            }

        }
        #endregion

        #region  Description : UP_SetReadOnly 이벤트
        private void UP_SetReadOnly()
        {
            DTP01_SHDATE.SetReadOnly(true);
        }
        #endregion

        #region  Description : UP_Set_Grid 바인딩
        private void UP_Set_Grid(DataTable dt)
        {
            DataTable dGrid = UP_SetDataTable();
            DataRow row;

            if (dt.Rows.Count > 0)
            {
                row = dGrid.NewRow();
                row["GUBN"] = "울산";
                row["IPHANG"] = dt.Rows[0]["SHETAULSAN"].ToString();
                row["TIME"] =  dt.Rows[0]["SHETAULTIME"].ToString().Trim() != "" ?  dt.Rows[0]["SHETAULTIME"].ToString().Trim().Substring(0,2)+":"+dt.Rows[0]["SHETAULTIME"].ToString().Trim().Substring(2,2) : "00:00";
                row["BLQTY"] = dt.Rows[0]["SHULSANQTY"].ToString();
                dGrid.Rows.Add(row);

                row = dGrid.NewRow();
                row["GUBN"] = "부산";
                row["IPHANG"] = dt.Rows[0]["SHETABUSAN"].ToString();
                row["TIME"] = dt.Rows[0]["SHETABUTIME"].ToString().Trim() != "" ? dt.Rows[0]["SHETABUTIME"].ToString().Trim().Substring(0, 2) + ":" + dt.Rows[0]["SHETABUTIME"].ToString().Trim().Substring(2, 2) : "00:00"; 
                row["BLQTY"] = dt.Rows[0]["SHBUSANQTY"].ToString();
                dGrid.Rows.Add(row);

                row = dGrid.NewRow();
                row["GUBN"] = "평택";
                row["IPHANG"] = dt.Rows[0]["SHETAINCHE"].ToString();
                row["TIME"] = dt.Rows[0]["SHETAINTIME"].ToString().Trim() != "" ? dt.Rows[0]["SHETAINTIME"].ToString().Trim().Substring(0, 2) + ":" + dt.Rows[0]["SHETAINTIME"].ToString().Trim().Substring(2, 2) : "00:00"; 
                row["BLQTY"] = dt.Rows[0]["SHINCHEQTY"].ToString();
                dGrid.Rows.Add(row);
            }
            else
            {
                row = dGrid.NewRow();
                row["GUBN"] = "울산";
                row["IPHANG"] = "";
                row["TIME"] = "";
                row["BLQTY"] = 0;
                dGrid.Rows.Add(row);

                row = dGrid.NewRow();
                row["GUBN"] = "부산";
                row["IPHANG"] = "";
                row["TIME"] = "";
                row["BLQTY"] = 0;
                dGrid.Rows.Add(row);

                row = dGrid.NewRow();
                row["GUBN"] = "평택";
                row["IPHANG"] = "";
                row["TIME"] = "";
                row["BLQTY"] = 0;
                dGrid.Rows.Add(row);
            }

            FPS91_TY_S_US_92QAJ908.Initialize();
            FPS91_TY_S_US_92QAJ908.SetValue(dGrid);
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding(string sSHDATE, string sSHSEQ)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92PE3891", sSHDATE, sSHSEQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_SHDATE.SetValue(dt.Rows[0]["SHDATE"].ToString());
                TXT01_SHSEQ.SetValue(dt.Rows[0]["SHSEQ"].ToString());

                CBO01_SHCOMPANY.SetValue(dt.Rows[0]["SHCOMPANY"].ToString());
                DTP01_SHHWDATE.SetValue(dt.Rows[0]["SHHWDATE"].ToString());
                TXT01_SHVESSEL.SetValue(dt.Rows[0]["SHVESSEL"].ToString());
                CBH01_SHHANGCHA.SetValue(dt.Rows[0]["SHHANGCHA"].ToString());
                CBH01_SHGOKJONG.SetValue(dt.Rows[0]["SHGOKJONG"].ToString());
                CBH01_SHAGENT.SetValue(dt.Rows[0]["SHAGENT"].ToString());
                CBH01_SHSOSOK.SetValue(dt.Rows[0]["SHSOSOK"].ToString());
                CBH01_SHSURVEY.SetValue(dt.Rows[0]["SHSURVEY"].ToString());
                CBH01_SHWONSAN.SetValue(dt.Rows[0]["SHWONSAN"].ToString());
                MTB01_SHETCD_S.SetValue(dt.Rows[0]["SHETCD_S"].ToString());
                MTB01_SHETCD_E.SetValue(dt.Rows[0]["SHETCD_E"].ToString());
                MTB01_SHETBDATE.SetValue(dt.Rows[0]["SHETBDATE"].ToString());
                MTB01_SHETBTIME.SetValue(dt.Rows[0]["SHETBTIME"].ToString());
                TXT01_SHREMARK.SetValue(dt.Rows[0]["SHREMARK"].ToString());

                UP_Set_Grid(dt);
            }
            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
             string sSHETAULSAN  = string.Empty;  
             string sSHETAULTIME = string.Empty;
             string sSHULSANQTY  = "0";    
             string sSHETABUSAN  = string.Empty;    
             string sSHETABUTIME = string.Empty;
             string sSHBUSANQTY  = "0";    
             string sSHETAPTAEK  = string.Empty;    
             string sSHETAPTTIME = string.Empty;
             string sSHPTAEKQTY = "0";                

            if ( FPS91_TY_S_US_92QAJ908.CurrentRowCount > 0)
            {             
                for (int i = 0; i < FPS91_TY_S_US_92QAJ908.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_US_92QAJ908.GetValue(i, "GUBN").ToString() == "울산")
                    {
                        sSHETAULSAN = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "IPHANG").ToString().Replace("19000101", "").ToString();
                        sSHETAULTIME = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString() == "0000" ? "" : this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString();
                        sSHULSANQTY = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "BLQTY").ToString();
                    }
                    if (this.FPS91_TY_S_US_92QAJ908.GetValue(i, "GUBN").ToString() == "부산")
                    {
                        sSHETABUSAN = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "IPHANG").ToString().Replace("19000101", "").ToString();
                        sSHETABUTIME = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString() == "0000" ? "" : this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString();
                        sSHBUSANQTY = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "BLQTY").ToString();
                    }
                    if (this.FPS91_TY_S_US_92QAJ908.GetValue(i, "GUBN").ToString() == "평택")
                    {
                        sSHETAPTAEK = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "IPHANG").ToString().Replace("19000101", "").ToString();
                        sSHETAPTTIME = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString() == "0000" ? "" : this.FPS91_TY_S_US_92QAJ908.GetValue(i, "TIME").ToString().Replace(":", "").ToString();
                        sSHPTAEKQTY = this.FPS91_TY_S_US_92QAJ908.GetValue(i, "BLQTY").ToString();
                    }
                }
            }


            if (string.IsNullOrEmpty(this.fsSHDATE))
            {
                //등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92PE0889", DTP01_SHDATE.GetString().ToString(),
                                                            TXT01_SHSEQ.GetValue().ToString(),
                                                            CBO01_SHCOMPANY.GetValue().ToString(),
                                                            CBH01_SHHANGCHA.GetValue().ToString(),
                                                            TXT01_SHVESSEL.GetValue().ToString(),
                                                            CBH01_SHSOSOK.GetValue().ToString(),
                                                            CBH01_SHGOKJONG.GetValue().ToString(),
                                                            CBH01_SHWONSAN.GetValue().ToString(),
                                                            CBH01_SHAGENT.GetValue().ToString(),
                                                            CBH01_SHSURVEY.GetValue().ToString(),
                                                            MTB01_SHETCD_S.GetValue().ToString().Replace("-", "").ToString(),
                                                            MTB01_SHETCD_E.GetValue().ToString().Replace("-", "").ToString(),
                                                            sSHETAULSAN ,
                                                            sSHETAULTIME,
                                                            sSHULSANQTY,
                                                            sSHETABUSAN, 
                                                            sSHETABUTIME,
                                                            sSHBUSANQTY, 
                                                            sSHETAPTAEK, 
                                                            sSHETAPTTIME,
                                                            sSHPTAEKQTY,
                                                            TXT01_SHREMARK.GetValue().ToString(),
                                                            DTP01_SHHWDATE.GetString().ToString().Substring(0,6),
                                                            MTB01_SHETBDATE.GetValue().ToString().Replace("-", "").ToString(),
                                                            MTB01_SHETBTIME.GetValue().ToString().Replace(":",""),
                                                            TYUserInfo.EmpNo
                                       );
                this.DbConnector.ExecuteTranQuery();
                
            }
            else
            {
                //수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92PE2890", CBO01_SHCOMPANY.GetValue().ToString(),
                                                            CBH01_SHHANGCHA.GetValue().ToString(),
                                                            TXT01_SHVESSEL.GetValue().ToString(),
                                                            CBH01_SHSOSOK.GetValue().ToString(),
                                                            CBH01_SHGOKJONG.GetValue().ToString(),
                                                            CBH01_SHWONSAN.GetValue().ToString(),
                                                            CBH01_SHAGENT.GetValue().ToString(),
                                                            CBH01_SHSURVEY.GetValue().ToString(),
                                                            MTB01_SHETCD_S.GetValue().ToString().Replace("-", "").ToString(),
                                                            MTB01_SHETCD_E.GetValue().ToString().Replace("-", "").ToString(),
                                                            sSHETAULSAN,
                                                            sSHETAULTIME,
                                                            sSHULSANQTY,
                                                            sSHETABUSAN, 
                                                            sSHETABUTIME,
                                                            sSHBUSANQTY, 
                                                            sSHETAPTAEK, 
                                                            sSHETAPTTIME,
                                                            sSHPTAEKQTY,
                                                            TXT01_SHREMARK.GetValue().ToString(),
                                                            DTP01_SHHWDATE.GetString().ToString().Substring(0,6),
                                                            MTB01_SHETBDATE.GetValue().ToString().Replace("-", "").ToString(),
                                                            MTB01_SHETBTIME.GetValue().ToString().Replace(":",""),
                                                            TYUserInfo.EmpNo,
                                                            DTP01_SHDATE.GetString().ToString(),
                                                            TXT01_SHSEQ.GetValue().ToString()
                                       );
                this.DbConnector.ExecuteTranQuery();
            }

            fsSHDATE = DTP01_SHDATE.GetString().ToString();
            fsSHSEQ = TXT01_SHSEQ.GetValue().ToString();
            
            UP_DataBinding(fsSHDATE, fsSHSEQ);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsSHDATE))
            {
                 this.DbConnector.CommandClear();
                 this.DbConnector.Attach("TY_P_US_92QBV910", DTP01_SHDATE.GetString().ToString());
                 TXT01_SHSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }

            if (CBO01_SHCOMPANY.GetValue().ToString() == "TY" && TXT01_SHVESSEL.GetValue().ToString() == "" )
            {
                this.SetFocus(TXT01_SHVESSEL);
                this.ShowCustomMessage("모선명을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }         
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("GUBN", typeof(System.String));
            dt.Columns.Add("IPHANG", typeof(System.String));
            dt.Columns.Add("TIME", typeof(System.String));
            dt.Columns.Add("BLQTY", typeof(System.Double));
            
            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region  Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
