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

namespace TY.ER.UT00
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
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_73EI6944 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUTME012I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        private string fsVSJUBAN = string.Empty;
        private string fsVSGROSS = string.Empty;

        #region Description : 페이지 로드
        public TYUTME012I()
        {
            InitializeComponent();
        }

        private void TYUTME012I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_73EI6944.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_73EI6944.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_C9RGK063",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_VSBONSUN.GetValue().ToString(),
                this.CBH01_VESLAJET.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_73EI6944.SetValue(UP_Get_SmartBillStatus(dt));

            for (int i = 0; i < this.FPS91_TY_S_UT_73EI6944.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_73EI6944.GetValue(i, "JBDATE1").ToString() != "")
                {
                    if (this.FPS91_TY_S_UT_73EI6944.GetValue(i, "JBJPNO").ToString() != "" && this.FPS91_TY_S_UT_73EI6944.GetValue(i, "JBBILLSTATUSNM").ToString() == ""
                        && this.FPS91_TY_S_UT_73EI6944.GetValue(i, "JBVATCD").ToString() != "13-면세")
                    {
                        this.FPS91_TY_S_UT_73EI6944.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    }
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_73EI6944.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region  Description : 스마트빌 전송 상태 확인 이벤트
        private DataTable UP_Get_SmartBillStatus(DataTable dt)
        {
            DataTable dtSmartTable = new DataTable();
            DataRow rw;

            dtSmartTable.Columns.Add("VSIPHANG", typeof(System.String));
            dtSmartTable.Columns.Add("VSBONSUN", typeof(System.String));
            dtSmartTable.Columns.Add("VSDESC1", typeof(System.String));
            dtSmartTable.Columns.Add("VESLAJET", typeof(System.String));
            dtSmartTable.Columns.Add("BRDESC1", typeof(System.String));
            dtSmartTable.Columns.Add("VESLGLOS", typeof(System.String));
            dtSmartTable.Columns.Add("VSHMGB", typeof(System.String));
            dtSmartTable.Columns.Add("CGDESC1", typeof(System.String));
            dtSmartTable.Columns.Add("VSVSGB", typeof(System.String));
            dtSmartTable.Columns.Add("VGDESC1", typeof(System.String));

            dtSmartTable.Columns.Add("VSIPHANG2", typeof(System.String));
            dtSmartTable.Columns.Add("VSIPTIM", typeof(System.String));
            dtSmartTable.Columns.Add("VSCHDAT", typeof(System.String));
            dtSmartTable.Columns.Add("VSCHTIM", typeof(System.String));
            dtSmartTable.Columns.Add("JBTOTTIM", typeof(System.String));
            dtSmartTable.Columns.Add("JBAMT", typeof(System.String));
            dtSmartTable.Columns.Add("JBVAT", typeof(System.String));
            dtSmartTable.Columns.Add("JBVATCD", typeof(System.String));
            dtSmartTable.Columns.Add("JBRATE", typeof(System.String));
            dtSmartTable.Columns.Add("JBDOLLAR", typeof(System.String));

            dtSmartTable.Columns.Add("JBDATE", typeof(System.String));
            dtSmartTable.Columns.Add("JBSEQ", typeof(System.String));
            dtSmartTable.Columns.Add("JBJPNO", typeof(System.String));
            dtSmartTable.Columns.Add("JBDATE1", typeof(System.String));
            dtSmartTable.Columns.Add("JBBILLSTATUSNM", typeof(System.String));
            dtSmartTable.Columns.Add("JBHALIN", typeof(System.String));


            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] sSMSCODE = UP_SMSBILL_STATUS(dt.Rows[i]["JBDATE"].ToString(), dt.Rows[i]["VESLAJET"].ToString() + "V", "01", dt.Rows[i]["JBJPNO"].ToString());

                    rw = dtSmartTable.NewRow();

                    rw["VSIPHANG"] = dt.Rows[i]["VSIPHANG"].ToString();
                    rw["VSBONSUN"] = dt.Rows[i]["VSBONSUN"].ToString();
                    rw["VSDESC1"] = dt.Rows[i]["VSDESC1"].ToString();
                    rw["VESLAJET"] = dt.Rows[i]["VESLAJET"].ToString();
                    rw["BRDESC1"] = dt.Rows[i]["BRDESC1"].ToString();
                    rw["VESLGLOS"] = dt.Rows[i]["VESLGLOS"].ToString();
                    rw["VSHMGB"] = dt.Rows[i]["VSHMGB"].ToString();
                    rw["CGDESC1"] = dt.Rows[i]["CGDESC1"].ToString();
                    rw["VSVSGB"] = dt.Rows[i]["VSVSGB"].ToString();
                    rw["VGDESC1"] = dt.Rows[i]["VGDESC1"].ToString();

                    rw["VSIPHANG2"] = dt.Rows[i]["VSIPHANG2"].ToString();
                    rw["VSIPTIM"] = dt.Rows[i]["VSIPTIM"].ToString();
                    rw["VSCHDAT"] = dt.Rows[i]["VSCHDAT"].ToString();
                    rw["VSCHTIM"] = dt.Rows[i]["VSCHTIM"].ToString();
                    rw["JBTOTTIM"] = dt.Rows[i]["JBTOTTIM"].ToString();
                    rw["JBAMT"] = dt.Rows[i]["JBAMT"].ToString();
                    rw["JBVAT"] = dt.Rows[i]["JBVAT"].ToString();
                    rw["JBVATCD"] = dt.Rows[i]["JBVATCD"].ToString();
                    rw["JBRATE"] = dt.Rows[i]["JBRATE"].ToString();
                    rw["JBDOLLAR"] = dt.Rows[i]["JBDOLLAR"].ToString();

                    rw["JBDATE"] = dt.Rows[i]["JBDATE"].ToString();
                    rw["JBSEQ"] = dt.Rows[i]["JBSEQ"].ToString();
                    rw["JBJPNO"] = dt.Rows[i]["JBJPNO"].ToString();
                    rw["JBDATE1"] = dt.Rows[i]["JBDATE1"].ToString();
                    rw["JBBILLSTATUSNM"] = sSMSCODE[0];
                    rw["JBHALIN"] = dt.Rows[i]["JBHALIN"].ToString();

                    dtSmartTable.Rows.Add(rw);
                }
            }

            return dtSmartTable;
        }
        #endregion    
  
        #region  Description : 스마빌 전송 상태 조회
        private string[] UP_SMSBILL_STATUS(string sMCDATE, string sMCHWAJU, string sMCGUBN, string sMCJPNO)
        {

            string sValue = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75Q8E625", sMCDATE, sMCHWAJU, sMCGUBN, sMCJPNO);
            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                string sCONID = dt.Rows[0]["XDCONVERSATION_ID"].ToString();

                dt.Clear();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75Q8A624", sCONID);
                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sValue = dt.Rows[0]["DTI_STATUS"].ToString() + ",";
                    sValue = sValue + dt.Rows[0]["TAXSTATUS"].ToString() + ",";
                    sValue = sValue + dt.Rows[0]["BYR_EMAIL"].ToString() + ",";
                    sValue = sValue + dt.Rows[0]["ATTFILEYN"].ToString() + ",";
                }
            }

            string[] arrayStatus = sValue.Split(',');

            return arrayStatus;
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_Clear();

            UP_BTN_DISPLAY("NEW");

            SetFocus(this.TXT01_JBGROSS);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_Save();
        }
        #endregion

        //#region Description : 삭제 버튼
        //private void BTN61_REM_Click(object sender, EventArgs e)
        //{
        //    UP_Del();
        //}
        //#endregion

        #region Description : 저장 메소드
        private void UP_Save()
        {
            string sIPTIM = string.Empty;
            string sCHTIM = string.Empty;

            sIPTIM = Set_Fill2(this.TXT01_JBJBTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBJBTIM2.GetValue().ToString().Trim());
            sCHTIM = Set_Fill2(this.TXT01_JBIANTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBIANTIM2.GetValue().ToString().Trim());

            // 입항파일 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73KAK981",
                                    Get_Numeric(this.TXT01_JBGROSS.GetValue().ToString().Trim()),            // G/T
						            this.CBH01_JBBRANCH.GetValue().ToString().Substring(0,2).ToString(),     // 대리점
						            Get_Numeric(Get_Date(this.DTP01_JBJBDAT.GetValue().ToString().Trim())),	 // 외항입항일자
						            sIPTIM.ToString(),									                     // 입항시간
						            Get_Numeric(Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim())), // 출항일자
						            sCHTIM.ToString(),                                                       // 출항시간
						            this.CBH01_JBVSGB.GetValue().ToString().Trim(),                          // 선박구분
						            this.CBH01_JBHMGB.GetValue().ToString().Trim(),                          // 화물구분
						            this.CBH01_VSJUBAN.GetValue().ToString().Trim(),                         // 접안장소
						            Get_Numeric(this.TXT01_VSCUSTOMS.GetValue().ToString().Trim()),	         // 입항세관
						            this.TXT01_VSBANIP.GetValue().ToString().Trim(),                         // 반입근거번호
						            this.TXT01_VSJUKHA.GetValue().ToString().Trim(),                         // 적하목록번호
						            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                            // 작성사번
                                    Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),              // 입항일자
						            this.CBH01_JBBONSUN.GetValue().ToString().Trim().ToUpper()               // 본선
                                    );

            this.DbConnector.ExecuteNonQuery();


            DataTable dt = new DataTable();

            // 부두 접안파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66GE8274",
                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                fsVSJUBAN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66GE1276",
                                        "1",                                                                     // 척수
                                        fsVSGROSS,                                                               // 수정 전 G/T
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                            // 작성사번
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                        fsVSJUBAN.ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 부두 접안파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66GE8274",
                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                this.CBH01_VSJUBAN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66LHH340",
                                        "1",                                                                     // 척수
                                        this.TXT01_JBGROSS.GetValue().ToString(),                                // 수정 후 G/T
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                            // 작성사번
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                        this.CBH01_VSJUBAN.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66LHM341",
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
                                        this.CBH01_VSJUBAN.GetValue().ToString(),
                                        "1",                                                                     // 척수
                                        this.TXT01_JBGROSS.GetValue().ToString(),                                // G/T
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()                             // 작성사번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // 선박사양 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73KBW985",
                                    this.TXT01_JBGROSS.GetValue().ToString(), // G/T
                                    this.CBH01_JBBONSUN.GetValue().ToString() // 본선
                                    );

            this.DbConnector.ExecuteNonQuery();

            if (this.CBO01_JBCRGB.GetValue().ToString() == "Y")
            {
                string sJBDATE = Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim());
                string sJBIPHANG = Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim());
                string sJBBONSUN = this.CBH01_JBBONSUN.GetValue().ToString().Trim().ToUpper();

                //시간 계산 
                string sJBJBDAT = Get_Date(this.DTP01_JBJBDAT.GetValue().ToString().Trim());
                string sJBJBTIM = Set_Fill2(this.TXT01_JBJBTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBJBTIM2.GetValue().ToString().Trim());
                string sJBIANDAT = Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim());
                string sJBIANTIM = Set_Fill2(this.TXT01_JBIANTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBIANTIM2.GetValue().ToString().Trim());

                //총접안시간
                string sJBTOTTIM = Work_Time(sJBJBDAT + sJBJBTIM, sJBIANDAT + sJBIANTIM);

                //부가세 코드
                string sJBVATCD = SetDefaultValue(this.CBO01_JBVATCD.GetValue().ToString().Trim());

                //선박구분 1-외항선 2-내항선
                string sJBVSGB = SetDefaultValue(this.CBH01_JBVSGB.GetValue().ToString().Trim());
                string sJBGROSS = Convert.ToString(Math.Ceiling((Convert.ToDouble(Convert.ToDouble(Get_Numeric(this.TXT01_JBGROSS.GetValue().ToString().Trim().ToString())) / 10))));

                //할인
                string sJBHALIN = SetDefaultValue(this.TXT01_JBHALIN.GetValue().ToString().Trim().ToUpper());
                //선저시설
                string sJBSUNJU = SetDefaultValue(this.TXT01_JBSUNJU.GetValue().ToString().Trim().ToUpper());

                double dDanga = 0;
                double dHalDanga = 0;
                double dJBMSUNHALYUL = 0;
                double dJBMHALYUL = 0;


                string sWork_Amt = "0";
                string sWork_HalAmt = "0";
                string sJBAMT = "0";

                string sJBDOLLAR = "0";

                // 접안료 현재요율이 적용되는 SQL문
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_73KCS987",
                    Get_Date(this.DTP01_JBIPHANG.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //외항선
                    if (sJBVSGB == "1")
                    {
                        // 외항단가
                        dDanga = double.Parse(dt.Rows[0]["JBMOUTAMT"].ToString());
                    }
                    else  //내항선
                    {
                        // 내항단가
                        dDanga = double.Parse(dt.Rows[0]["JBMINAMT"].ToString());
                    }

                    // GROSS * 단가
                    sWork_Amt = Convert.ToString(Convert.ToDouble(sJBGROSS) * dDanga);


                    //할증
                    if (Convert.ToInt32(sJBTOTTIM) > 12)
                    {
                        // 외항선
                        if (sJBVSGB == "1")
                        {
                            // 외항할증단가
                            dHalDanga = double.Parse(dt.Rows[0]["JBMHALOUTAMT"].ToString());
                        }
                        else // 내항선
                        {
                            // 내항할증단가
                            dHalDanga = double.Parse(dt.Rows[0]["JBMHALINAMT"].ToString());
                        }

                        double dHal_Amt = 0;
                        dHal_Amt = ((Convert.ToInt32(sJBTOTTIM) - 12) * dHalDanga) * Convert.ToDouble(sJBGROSS);
                        sWork_HalAmt = Convert.ToString(dHal_Amt);
                    }

                    sJBAMT = Convert.ToString(Convert.ToDouble(sWork_Amt) + Convert.ToDouble(sWork_HalAmt));

                    //선저시설 15%할인
                    if (sJBSUNJU == "Y")
                    {
                        dJBMSUNHALYUL = 0;

                        dJBMSUNHALYUL = double.Parse(dt.Rows[0]["JBMSUNHALYUL"].ToString());
                        sJBAMT = Convert.ToString(Math.Round(Convert.ToDouble(sJBAMT) * ((100 - dJBMSUNHALYUL) / 100)));
                    }
                    else
                    {
                        //할인모선 10%
                        if (sJBHALIN == "Y")
                        {
                            dJBMHALYUL = 0;

                            dJBMHALYUL = double.Parse(dt.Rows[0]["JBMHALYUL"].ToString());

                            sJBAMT = Convert.ToString(Math.Round(Convert.ToDouble(sJBAMT) * ((100 - dJBMHALYUL) / 100)));
                        }
                    }

                    //10원단위 절사
                    sJBAMT = Convert.ToString(Convert.ToDouble(UP_DotDelete(Convert.ToString(Convert.ToDouble(sJBAMT) / 10))) * 10);

                    this.TXT01_JBDOLLAR.SetValue("0");

                    if (double.Parse(Get_Numeric(this.TXT01_JBRATE.GetValue().ToString().Trim())) != 0)
                    {
                        sJBDOLLAR = UP_DotDelete(Convert.ToString(((Convert.ToDouble(sJBAMT.ToString()) / Convert.ToDouble(Get_Numeric(this.TXT01_JBRATE.GetValue().ToString().Trim()))) + 0.005) * 100));

                        sJBDOLLAR = Convert.ToString(Convert.ToDouble(sJBDOLLAR.ToString()) / 100);

                        this.TXT01_JBDOLLAR.SetValue(sJBDOLLAR.ToString());
                    }

                    string sJBVAT = "0";
                    //부가세코드 11이면
                    if (int.Parse(Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim())) <= 20101031)
                    {
                        if (sJBVATCD == "11")
                        {
                            sJBVAT = Convert.ToString(Convert.ToDouble(sJBAMT) * 0.1);
                        }
                    }
                    else
                    {
                        if (sJBVATCD == "61")
                        {
                            sJBVAT = Convert.ToString(Convert.ToDouble(sJBAMT) * 0.1);
                        }
                    }

                    this.TXT01_JBAMT.SetValue(sJBAMT);

                    if (fsGUBUN == "NEW")
                    {
                        // 접안료 등록
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_73KAN982",
                                                Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim()),   // 매출일자
                                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()), // 입항일자
                                                this.CBH01_JBBONSUN.GetValue().ToString(),                  // 본선
                                                this.TXT01_JBSEQ.GetValue().ToString(),                     // 순번
                                                this.CBH01_JBBRANCH.GetValue().ToString().Trim().Substring(0, 2),
                                                Get_Numeric(this.TXT01_JBGROSS.GetValue().ToString().Trim()),
                                                SetDefaultValue(this.CBH01_JBVSGB.GetValue().ToString().Trim()),
                                                this.TXT01_JBHALIN.GetValue().ToString().Trim().ToUpper(),
                                                Get_Date(this.DTP01_JBJBDAT.GetValue().ToString().Trim()),
                                                sJBJBTIM.Trim(),
                                                Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim()),
                                                sJBIANTIM.Trim(),
                                                sJBTOTTIM,
                                                "0",
                                                this.TXT01_JBAMT.GetValue().ToString(),
                                                sJBVAT.ToString(),
                                                SetDefaultValue(this.CBO01_JBVATCD.GetValue().ToString().Trim()),
                                                this.CBH01_JBHMGB.GetValue().ToString().Trim().ToUpper(),
                                                "",
                                                this.TXT01_JBSUNJU.GetValue().ToString().Trim().ToUpper(),
                                                Get_Numeric(this.TXT01_JBRATE.GetValue().ToString().Trim()),
                                                Get_Numeric(this.TXT01_JBDOLLAR.GetValue().ToString().Trim()),
                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper()                             // 작성사번
                                                );

                        this.DbConnector.ExecuteNonQuery();
                    }
                    else
                    {
                        // 접안료 수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_73KAN983",
                                                this.CBH01_JBBRANCH.GetValue().ToString().Trim().Substring(0, 2),
                                                Get_Numeric(this.TXT01_JBGROSS.GetValue().ToString().Trim()),
                                                SetDefaultValue(this.CBH01_JBVSGB.GetValue().ToString().Trim()),
                                                this.TXT01_JBHALIN.GetValue().ToString().Trim().ToUpper(),
                                                Get_Date(this.DTP01_JBJBDAT.GetValue().ToString().Trim()),
                                                sJBJBTIM.Trim(),
                                                Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim()),
                                                sJBIANTIM.Trim(),
                                                sJBTOTTIM,
                                                "0",
                                                this.TXT01_JBAMT.GetValue().ToString(),
                                                sJBVAT.ToString(),
                                                SetDefaultValue(this.CBO01_JBVATCD.GetValue().ToString().Trim()),
                                                this.CBH01_JBHMGB.GetValue().ToString().Trim().ToUpper(),
                                                "",
                                                this.TXT01_JBSUNJU.GetValue().ToString().Trim().ToUpper(),
                                                Get_Numeric(this.TXT01_JBRATE.GetValue().ToString().Trim()),
                                                Get_Numeric(this.TXT01_JBDOLLAR.GetValue().ToString().Trim()),
                                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                     // 작성사번
                                                Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim()),         // 매출일자
                                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),       // 입항일자
                                                this.CBH01_JBBONSUN.GetValue().ToString(),                        // 본선
                                                this.TXT01_JBSEQ.GetValue().ToString()                            // 순번
                                                );

                        this.DbConnector.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                // 접안료 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73KAO984",
                                        Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim()),         // 매출일자
                                        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),       // 입항일자
                                        this.CBH01_JBBONSUN.GetValue().ToString(),                        // 본선
                                        this.TXT01_JBSEQ.GetValue().ToString()                            // 순번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        //#region Description : 삭제 메소드
        //private void UP_Del()
        //{
        //    DataTable dt = new DataTable();

        //    // 부두 접안파일 체크
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_66GE8274",
        //        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
        //        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
        //        fsVSJUBAN.ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach("TY_P_UT_66GE1276",
        //                                "1",                                                                     // 척수
        //                                fsVSGROSS,                                                               // 수정 전 G/T
        //                                TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                            // 작성사번
        //                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(0, 4).ToString(),
        //                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()).Substring(4, 2).ToString(),
        //                                fsVSJUBAN.ToString()
        //                                );

        //        this.DbConnector.ExecuteNonQuery();
        //    }

        //    if (this.CBO01_JBCRGB.GetValue().ToString() == "Y")
        //    {
        //        // 접안료 삭제
        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach("TY_P_UT_73KAO984",
        //                                Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim()),         // 매출일자
        //                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),       // 입항일자
        //                                this.CBH01_JBBONSUN.GetValue().ToString(),                        // 본선
        //                                this.TXT01_JBSEQ.GetValue().ToString()                            // 순번
        //                                );

        //        this.DbConnector.ExecuteNonQuery();
        //    }

        //    this.BTN61_INQ_Click(null, null);

        //    this.ShowMessage("TY_M_GB_23NAD874");
        //}
        //#endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_73FHP951",
                this.DTP01_JBIPHANG.GetValue().ToString(),
                this.CBH01_JBBONSUN.GetValue().ToString(),
                this.TXT01_JBSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                string sJBCRGB = dt.Rows[0]["JBCRGB"].ToString();

                fsVSJUBAN = dt.Rows[0]["VSJUBAN"].ToString();
                fsVSGROSS = dt.Rows[0]["JBGROSS"].ToString();

                if (this.TXT01_JBJPNO.GetValue().ToString() != "")
                {
                    UP_BTN_DISPLAY("");
                }
                else
                {
                    if (Get_Numeric(this.TXT01_JBSEQ.GetValue().ToString()) == "0")
                    {
                        fsGUBUN = "NEW";
                        UP_BTN_DISPLAY("NEW");
                    }
                    else
                    {
                        fsGUBUN = "UPT";
                        UP_BTN_DISPLAY("UPT");
                    }
                }
            }
            else
            {
                UP_BTN_DISPLAY("");
            }

            //SetFocus(this.TXT01_JBGROSS);
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sVSBONSUN = string.Empty;

            sVSBONSUN = this.CBH01_JBBONSUN.GetValue().ToString();

            if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
                sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3"
                )
            {
                // 접안시간(시)
                if (Int64.Parse(Set_Fill2(this.TXT01_JBJBTIM1.GetValue().ToString().Trim())) > 24)
                {
                    this.ShowMessage("TY_M_UT_66LFV325");
                    e.Successed = false;

                    SetFocus(this.TXT01_JBJBTIM1);
                    return;
                }

                // 접안시간(분)
                if (Int64.Parse(Set_Fill2(this.TXT01_JBJBTIM2.GetValue().ToString().Trim())) > 59)
                {
                    this.ShowMessage("TY_M_UT_66LFV325");
                    e.Successed = false;

                    SetFocus(this.TXT01_JBJBTIM2);
                    return;
                }

                // 이안일자
                if (Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim()) != "" && this.TXT01_JBIANTIM1.GetValue().ToString().Trim() == "" && this.TXT01_JBIANTIM2.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_66LFV323");
                    e.Successed = false;

                    SetFocus(this.TXT01_JBIANTIM1);
                    return;
                }

                if (Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim()) == "" && this.TXT01_JBIANTIM1.GetValue().ToString().Trim() != "" && this.TXT01_JBIANTIM2.GetValue().ToString().Trim() != "")
                {
                    this.ShowMessage("TY_M_UT_66LFT320");
                    e.Successed = false;

                    SetFocus(this.DTP01_JBIANDAT);
                    return;
                }

                if (Int64.Parse(Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim())) > Int64.Parse(Get_Date(this.DTP01_JBIANDAT.GetValue().ToString().Trim())))
                {
                    this.ShowMessage("TY_M_UT_66LFT320");
                    e.Successed = false;

                    SetFocus(this.DTP01_JBIANDAT);
                    return;
                }

                // 이안시간(시)
                if (Int64.Parse(Set_Fill2(this.TXT01_JBIANTIM1.GetValue().ToString().Trim())) > 24)
                {
                    this.ShowMessage("TY_M_UT_66LFV323");
                    e.Successed = false;

                    SetFocus(this.TXT01_JBIANTIM1);
                    return;
                }

                // 이안시간(분)
                if (Int64.Parse(Set_Fill2(this.TXT01_JBIANTIM2.GetValue().ToString().Trim())) > 59)
                {
                    this.ShowMessage("TY_M_UT_66LFV323");
                    e.Successed = false;

                    SetFocus(this.TXT01_JBIANTIM2);
                    return;
                }
                string sIPTIM = string.Empty;
                string sCHTIM = string.Empty;
                sIPTIM = Set_Fill2(this.TXT01_JBJBTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBJBTIM2.GetValue().ToString().Trim());
                sCHTIM = Set_Fill2(this.TXT01_JBIANTIM1.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_JBIANTIM2.GetValue().ToString().Trim());

                if (Convert.ToInt32(this.DTP01_JBJBDAT.GetString()) > Convert.ToInt32(this.DTP01_JBIANDAT.GetString()))
                {
                    this.ShowCustomMessage("외항입항일이 이안일자보다 큽니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    e.Successed = false;
                    return;
                }

                // 20160407 김종술 과장 - 체크 빼달라고 요청
                //				if(Get_Date(txtVSIPHANG.GetValue().ToString().Trim()) == Get_Date(txtVSCHDAT.GetValue().ToString().Trim()))
                //				{
                //					if(Int64.Parse(sIPTIM.ToString()) >= Int64.Parse(sCHTIM.ToString()))
                //					{
                //						Message_Alert("접안시간을 확인 하십시요!");
                //						UP_ControlFocus("txtVSIPTIM1");   
                //						return false ;
                //					}
                //				}


                DataTable dt = new DataTable();

                //승인전표 수정 삭제 불가
                if (this.TXT01_JBJPNO.GetValue().ToString().Trim().Length > 17)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_73KA5977",
                        this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(0, 6),
                        this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(6, 8),
                        this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(14, 3)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["B1NOJP"].ToString() != "")
                        {
                            this.ShowMessage("TY_M_UT_73K9W967");
                            e.Successed = false;

                            SetFocus(this.DTP01_JBDATE);
                            return;
                        }
                        else
                        {
                            this.ShowMessage("TY_M_UT_73K9X969");
                            e.Successed = false;

                            SetFocus(this.DTP01_JBDATE);
                            return;
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_UT_73K9X970");
                        e.Successed = false;

                        SetFocus(this.DTP01_JBDATE);
                        return;
                    }
                }

                //할인구분
                if (this.TXT01_JBHALIN.GetValue().ToString().Trim() != "")
                {
                    if (this.TXT01_JBHALIN.GetValue().ToString().Trim() != "Y" && this.TXT01_JBHALIN.GetValue().ToString().Trim() != "N")
                    {
                        this.ShowMessage("TY_M_UT_73K9Y971");
                        e.Successed = false;

                        SetFocus(this.DTP01_JBDATE);
                        return;
                    }
                }
                
                //선저시설                 
                if (this.TXT01_JBSUNJU.GetValue().ToString().Trim() != "")
                {
                    if (this.TXT01_JBSUNJU.GetValue().ToString().Trim() != "Y" && this.TXT01_JBSUNJU.GetValue().ToString().Trim() != "N")
                    {
                        this.ShowMessage("TY_M_UT_73K9Y972");
                        e.Successed = false;

                        SetFocus(this.DTP01_JBDATE);
                        return;
                    }
                }

                //접안료생성유무 
                if (this.CBO01_JBCRGB.GetValue().ToString().Trim() != "")
                {
                    if (this.CBO01_JBCRGB.GetValue().ToString().Trim() != "Y" && this.CBO01_JBCRGB.GetValue().ToString().Trim() != "N")
                    {
                        this.ShowMessage("TY_M_UT_73K9Y973");
                        e.Successed = false;

                        SetFocus(this.CBO01_JBCRGB);
                        return;
                    }
                }

                // 접안료 현재요율이 적용되는 SQL문
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_73KCS987",
                    Get_Date(this.DTP01_JBIPHANG.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_73KD1988");
                    e.Successed = false;

                    SetFocus(this.DTP01_JBDATE);
                    return;
                }

                
                if (this.CBO01_JBCRGB.GetValue().ToString().Trim().ToUpper() == "Y")
                {
                    //매출일자
                    if (Get_Date(DTP01_JBDATE.GetValue().ToString().Trim()) == "")
                    {
                        this.ShowMessage("TY_M_UT_73K9Y974");
                        e.Successed = false;

                        SetFocus(this.CBO01_JBCRGB);
                        return;
                    }

                    //이월 미수금 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_73KA1975",
                        (Get_Date(this.DTP01_JBDATE.GetValue().ToString().Trim())).Substring(0, 6)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_73K9W966");
                        e.Successed = false;

                        SetFocus(this.DTP01_JBDATE);
                        return;
                    }

                    // 부가세 구분
                    if (this.CBO01_JBVATCD.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_73KF0992");
                        e.Successed = false;

                        SetFocus(this.CBO01_JBVATCD);
                        return;
                    }

                    if (fsGUBUN == "NEW")
                    {
                        if (Get_Numeric(this.TXT01_JBSEQ.GetValue().ToString().Trim()) == "0")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_UT_73KA3978",
                                Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),
                                this.CBH01_JBBONSUN.GetValue().ToString().Trim().ToUpper()
                                );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                this.TXT01_JBSEQ.SetValue(dt.Rows[0]["JBSEQ"].ToString().Trim());
                            }
                        }

                        
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_UT_7B4D1941",
                            this.DTP01_JBDATE.GetValue().ToString(),
                            this.DTP01_JBIPHANG.GetValue().ToString(),
                            this.CBH01_JBBONSUN.GetValue().ToString(),
                            this.TXT01_JBSEQ.GetValue().ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.ShowMessage("TY_M_UT_7B495940");
                            this.TXT01_JBSEQ.Focus();

                            e.Successed = false;
                            return;
                        }
                    }
                }                
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        //#region Description : 삭제 ProcessCheck
        //private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    string sVSBONSUN = string.Empty;

        //    DataTable dt = new DataTable();

        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_UT_73KEO991",
        //        Get_Date(this.DTP01_JBIPHANG.GetValue().ToString().Trim()),       // 입항일자
        //        this.CBH01_JBBONSUN.GetValue().ToString()                        // 본선
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        this.ShowMessage("TY_M_UT_722JD638");
        //        e.Successed = false;

        //        return;
        //    }

        //    sVSBONSUN = this.CBH01_JBBONSUN.GetValue().ToString();

        //    if (sVSBONSUN != "PP1" && sVSBONSUN != "PP2" && sVSBONSUN != "PP3" && sVSBONSUN != "PP4" &&
        //        sVSBONSUN != "PP5" && sVSBONSUN != "TK1" && sVSBONSUN != "TK2" && sVSBONSUN != "TK3"
        //        )
        //    {
        //        //승인전표 수정 삭제 불가
        //        if (this.TXT01_JBJPNO.GetValue().ToString().Trim().Length > 17)
        //        {
        //            this.DbConnector.CommandClear();
        //            this.DbConnector.Attach
        //                (
        //                "TY_P_UT_73KA5977",
        //                this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(0, 6),
        //                this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(6, 8),
        //                this.TXT01_JBJPNO.GetValue().ToString().Trim().Substring(14, 3)
        //                );

        //            dt = this.DbConnector.ExecuteDataTable();

        //            if (dt.Rows.Count > 0)
        //            {
        //                if (dt.Rows[0]["B1NOJP"].ToString() != "")
        //                {
        //                    this.ShowMessage("TY_M_UT_73K9W967");
        //                    e.Successed = false;

        //                    SetFocus(this.DTP01_JBDATE);
        //                    return;
        //                }
        //                else
        //                {
        //                    this.ShowMessage("TY_M_UT_73K9X969");
        //                    e.Successed = false;

        //                    SetFocus(this.DTP01_JBDATE);
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                this.ShowMessage("TY_M_UT_73K9X970");
        //                e.Successed = false;

        //                SetFocus(this.DTP01_JBDATE);
        //                return;
        //            }
        //        }
        //    }

        //    // 삭제 하시겠습니까?
        //    if (!this.ShowMessage("TY_M_GB_23NAD872"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region Description : 작업시간 계산
        public string Work_Time(string sbegin_date_time, string send_date_time)
        {
            int years, months, days, hours, minutes, seconds, milliseconds, ticks;

            string sParamHours = "";

            int begin_paramyears, begin_parammonths, begin_paramdays;
            int begin_paramhours, begin_paramminutes;
            int end_paramyears, end_parammonths, end_paramdays;
            int end_paramhours, end_paramminutes;

            //시작일자시간			
            begin_paramyears = Convert.ToInt32(sbegin_date_time.Substring(0, 4));
            begin_parammonths = Convert.ToInt32(sbegin_date_time.Substring(4, 2));
            begin_paramdays = Convert.ToInt32(sbegin_date_time.Substring(6, 2));
            begin_paramhours = Convert.ToInt32(sbegin_date_time.Substring(8, 2));
            begin_paramminutes = Convert.ToInt32(sbegin_date_time.Substring(10, 2));

            //시작일자시간			
            end_paramyears = Convert.ToInt32(send_date_time.Substring(0, 4));
            end_parammonths = Convert.ToInt32(send_date_time.Substring(4, 2));
            end_paramdays = Convert.ToInt32(send_date_time.Substring(6, 2));
            end_paramhours = Convert.ToInt32(send_date_time.Substring(8, 2));
            end_paramminutes = Convert.ToInt32(send_date_time.Substring(10, 2));


            DateTime begin_date_time = new DateTime(begin_paramyears, begin_parammonths, begin_paramdays, begin_paramhours, begin_paramminutes, 0, 0);

            DateTime end_date_time = new DateTime(end_paramyears, end_parammonths, end_paramdays, end_paramhours, end_paramminutes, 0, 0);

            CalculateBetween(begin_date_time, end_date_time, out years, out months, out days, out hours, out minutes, out seconds, out milliseconds, out ticks);


            if (days > 0)
            {
                hours = hours + (24 * days);
            }

            //무조건 시간으로 계산
            if (minutes > 0)
            {
                hours = hours + 1;
            }

            sParamHours = Convert.ToString(hours);

            return sParamHours;

        }
        public void CalculateBetween(DateTime begin_date_time, DateTime end_date_time, out int years, out int months, out int days, out int hours, out int minutes, out int seconds, out int milliseconds, out int ticks)
        {

            years = 0;

            while (begin_date_time.AddYears(1).Ticks < end_date_time.Ticks)
            {

                years += 1;

                begin_date_time = begin_date_time.AddYears(1);

            }



            months = 0;

            while (begin_date_time.AddMonths(1).Ticks < end_date_time.Ticks)
            {

                months += 1;

                begin_date_time = begin_date_time.AddMonths(1);

            }



            days = 0;

            while (begin_date_time.AddDays(1).Ticks < end_date_time.Ticks)
            {

                days += 1;

                begin_date_time = begin_date_time.AddDays(1);

            }



            hours = 0;

            while (begin_date_time.AddHours(1).Ticks < end_date_time.Ticks)
            {

                hours += 1;

                begin_date_time = begin_date_time.AddHours(1);

            }



            minutes = 0;

            while (begin_date_time.AddMinutes(1).Ticks < end_date_time.Ticks)
            {

                minutes += 1;

                begin_date_time = begin_date_time.AddMinutes(1);

            }



            seconds = 0;

            while (begin_date_time.AddSeconds(1).Ticks < end_date_time.Ticks)
            {

                seconds += 1;

                begin_date_time = begin_date_time.AddSeconds(1);

            }



            milliseconds = 0;

            while (begin_date_time.AddMilliseconds(1).Ticks < end_date_time.Ticks)
            {

                milliseconds += 1;

                begin_date_time = begin_date_time.AddMilliseconds(1);

            }

            // 밀리초까지 계산하고도 남은 ticks 이기 때문에 int 형으로 캐스팅하는 것은 확실히 안전하다.
            ticks = (int)(end_date_time.Ticks - begin_date_time.Ticks);

        }
        #endregion	

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_UT_73EI6944_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_JBIPHANG.SetValue(this.FPS91_TY_S_UT_73EI6944.GetValue("VSIPHANG").ToString());
            this.CBH01_JBBONSUN.SetValue(this.FPS91_TY_S_UT_73EI6944.GetValue("VSBONSUN").ToString());
            this.TXT01_JBSEQ.SetValue(this.FPS91_TY_S_UT_73EI6944.GetValue("JBSEQ").ToString());

            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Interval = 100;
            tmr.Start();

            UP_RUN();
        }
        #endregion

        void tmr_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            TXT01_JBGROSS.Focus();
        } 

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                //this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                //this.BTN61_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                //this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.TXT01_JBSEQ.SetValue("");

            this.DTP01_JBDATE.SetValue("");
            this.CBO01_JBCRGB.SetValue("N");

            this.CBO01_JBVATCD.SetValue("61");
            this.TXT01_JBRATE.SetValue("");
            this.TXT01_JBAMT.SetValue("");
            this.TXT01_JBDOLLAR.SetValue("");
            this.TXT01_JBJPNO.SetValue("");
        }
        #endregion

        #region Description : 날짜 이벤트
        private void DTP01_EDDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetFocus(this.BTN61_INQ);
        }
        #endregion

        #region Description : 선박 구분 이벤트
        private void CBH01_JBVSGB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.DTP01_JBDATE);
            }
        }
        #endregion

        #region Description : 환율 이벤트
        private void TXT01_JBRATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_SAV);
            }
        }

        private void TXT01_JBRATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        private void CBH01_VESLAJET_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {   
                SetFocus(this.BTN61_INQ);
            }
        }
    }
}