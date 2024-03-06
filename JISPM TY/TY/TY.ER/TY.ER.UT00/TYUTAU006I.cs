using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 무인계근 차량 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.05 14:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6A5D4295 : 무인계근 차량 조회
    ///  TY_P_UT_6A5DA296 : 무인계근 차량 등록
    ///  TY_P_UT_6A5DB297 : 무인계근 차량 수정
    ///  TY_P_UT_6A5DC298 : 무인계근 차량 삭제
    ///  TY_P_UT_6A5DD299 : 무인계근 차량 바코드 조회
    ///  TY_P_UT_6A5DF300 : 차량 중량파일 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6A5EX301 : 무인계근 차량 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  TRHWAJU1 : 출고화주1
    ///  TRHWAJU2 : 출고화주2
    ///  TRHWAJU3 : 출고화주3
    ///  TRHWAMUL : 화물명
    ///  TRHYUNGT : 탱크로리
    ///  TRPUMM : 품목명
    ///  TRUNSONG : 차량소속
    ///  BARCODE : BAR-CODE
    ///  TRBALSU : 카드발급횟수
    ///  TRBIGO : 특기사항
    ///  TRCHJUSO : 차량주소
    ///  TRCHTEL : 차량전화
    ///  TRCOUNT : 유창개수
    ///  TRGIJUGO : 기사주소
    ///  TRGITEL : 기사전화
    ///  TRGUBUN : 구분
    ///  TRJUMIN1 : 주민번호1
    ///  TRJUMIN2 : 주민번호2
    ///  TRJUNGRY : 적재중량(MT)
    ///  TRMUMNO1 : 차량번호1
    ///  TRMUMNO2 : 차량번호2
    ///  TRTOTAL : 허가용량
    ///  TRUNNAME : 기사성명
    /// </summary>
    public partial class TYUTAU006I : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYUTAU006I()
        {
            InitializeComponent();
        }

        private void TYUTAU006I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_UT_6A5EX301.Initialize();

            BTN61_SAV.Visible = false;
            BTN61_REM.Visible = false;


            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmrPage_Tick);
            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.TXT01_TRNUMNO1.GetValue().ToString() != "")
            {
                string sSTNUM = string.Empty;
                string sEDNUM = string.Empty;
                string sNUM = string.Empty;
                string sGUBUN = string.Empty;

                if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                {
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(2, 1))
                    {
                        case "1":
                            sSTNUM = "＇１＇";
                            sGUBUN = "1";
                            break;
                        case "2":
                            sSTNUM = "＇２＇";
                            sGUBUN = "1";
                            break;
                        case "3":
                            sSTNUM = "＇３＇";
                            sGUBUN = "1";
                            break;
                        case "4":
                            sSTNUM = "＇４＇";
                            sGUBUN = "1";
                            break;
                        case "5":
                            sSTNUM = "＇５＇";
                            sGUBUN = "1";
                            break;
                        case "6":
                            sSTNUM = "＇６＇";
                            sGUBUN = "1";
                            break;
                        case "7":
                            sSTNUM = "＇７＇";
                            sGUBUN = "1";
                            break;
                        case "8":
                            sSTNUM = "＇８＇";
                            sGUBUN = "1";
                            break;
                        case "9":
                            sSTNUM = "＇９＇";
                            sGUBUN = "1";
                            break;
                        case "0":
                            sSTNUM = "＇０＇";
                            sGUBUN = "1";
                            break;
                    }
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(3, 1))
                    {
                        case "1":
                            sEDNUM = "＇１＇";
                            break;
                        case "2":
                            sEDNUM = "＇２＇";
                            break;
                        case "3":
                            sEDNUM = "＇３＇";
                            break;
                        case "4":
                            sEDNUM = "＇４＇";
                            break;
                        case "5":
                            sEDNUM = "＇５＇";
                            break;
                        case "6":
                            sEDNUM = "＇６＇";
                            break;
                        case "7":
                            sEDNUM = "＇７＇";
                            break;
                        case "8":
                            sEDNUM = "＇８＇";
                            break;
                        case "9":
                            sEDNUM = "＇９＇";
                            break;
                        case "0":
                            sEDNUM = "＇０＇";
                            break;
                    }
                }
                else
                {
                    switch (this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 1))
                    {
                        case "1":
                            sGUBUN = "1";
                            break;
                        case "2":
                            sGUBUN = "1";
                            break;
                        case "3":
                            sGUBUN = "1";
                            break;
                        case "4":
                            sGUBUN = "1";
                            break;
                        case "5":
                            sGUBUN = "1";
                            break;
                        case "6":
                            sGUBUN = "1";
                            break;
                        case "7":
                            sGUBUN = "1";
                            break;
                        case "8":
                            sGUBUN = "1";
                            break;
                        case "9":
                            sGUBUN = "1";
                            break;
                        case "0":
                            sGUBUN = "1";
                            break;
                    }
                }

                if (sGUBUN == "")
                {
                    if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                    {
                        sSTNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(2, 1);
                        sEDNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(3, 1);
                    }
                    else
                    {
                        sSTNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 1);
                        sEDNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(1, 1);
                    }
                }
                else
                {
                    if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                    {
                        sSTNUM = sSTNUM.Substring(1, 1).ToString();
                        sEDNUM = sEDNUM.Substring(1, 1).ToString();
                    }
                }

                if (this.TXT01_TRNUMNO1.GetValue().ToString().Length >= 5)
                {
                    sNUM = this.TXT01_TRNUMNO1.GetValue().ToString().Substring(0, 2) + sSTNUM + sEDNUM + this.TXT01_TRNUMNO1.GetValue().ToString().Substring(4, 1);
                }
                else
                {
                    sNUM = this.TXT01_TRNUMNO1.GetValue().ToString();
                }
                this.TXT01_TRNUMNO1.SetValue(sNUM.ToString());
            }

            this.FPS91_TY_S_UT_6A5EX301.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_UT_6A5D4295",
                this.TXT01_TRNUMNO2.GetValue().ToString(),
                this.TXT01_TRNUMNO1.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6A5EX301.SetValue(dt);

                string sBARCODE = dt.Rows[0]["BDCODE"].ToString().Trim();
                string sTRMUMN = dt.Rows[0]["TRNUMNO1"].ToString().Trim();
                string sTRMUMNO2 = dt.Rows[0]["TRNUMNO2"].ToString().Trim();

                string sTRMUMNO1 = string.Empty;

                if (sTRMUMN.ToString().Length == 5)
                {
                    sTRMUMNO1 = sTRMUMN.Substring(4, 1).ToString();
                }
                else if (sTRMUMN.ToString().Length == 3)
                {
                    sTRMUMNO1 = sTRMUMN.Substring(2, 1).ToString();
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6A5DD299", sTRMUMNO1.Trim());

                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count > 0)
                {
                    sTRMUMNO1 = dt2.Rows[0]["BDCODE"].ToString();
                }

                this.TXT01_BARCODE.SetValue(sBARCODE + sTRMUMNO1 + sTRMUMNO2);
                this.TXT01_TRNUMNO2.SetValue(dt.Rows[0]["TRNUMNO2"].ToString());
                this.TXT01_TRNUMNO1.SetValue(dt.Rows[0]["TRNUMNO1"].ToString());
                this.CBH01_TRUNSONG.SetValue(dt.Rows[0]["TRUNSONG"].ToString());
                this.CBH01_TRHYUNGT.SetValue(dt.Rows[0]["TRHYUNGT"].ToString());
                this.TXT01_TRCHJUSO.SetValue(dt.Rows[0]["TRCHJUSO"].ToString());
                this.TXT01_TRCHTEL.SetValue(dt.Rows[0]["TRCHTEL"].ToString());
                this.TXT01_TRUNNAME.SetValue(dt.Rows[0]["TRUNNAME"].ToString());
                this.TXT01_TRJUMIN1.SetValue(dt.Rows[0]["TRJUMIN1"].ToString());
                this.TXT01_TRJUMIN2.SetValue(dt.Rows[0]["TRJUMIN2"].ToString());
                this.TXT01_TRGIJUSO.SetValue(dt.Rows[0]["TRGIJUSO"].ToString());
                this.TXT01_TRGITEL.SetValue(dt.Rows[0]["TRGITEL"].ToString());
                this.CBH01_TRHWAJU1.SetValue(dt.Rows[0]["TRHWAJU1"].ToString());
                this.CBH01_TRHWAJU2.SetValue(dt.Rows[0]["TRHWAJU2"].ToString());
                this.CBH01_TRHWAJU3.SetValue(dt.Rows[0]["TRHWAJU3"].ToString());
                this.TXT01_TRBALSU.SetValue(dt.Rows[0]["TRBALSU"].ToString());
                this.TXT01_TRBIGO.SetValue(dt.Rows[0]["TRBIGO"].ToString());
                this.TXT01_TRGUBUN.SetValue(dt.Rows[0]["TRGUBUN"].ToString());
                this.TXT01_TRTOTAL.SetValue(dt.Rows[0]["TRTOTAL"].ToString());
                this.TXT01_TRCOUNT.SetValue(dt.Rows[0]["TRCOUNT"].ToString());
                if (dt.Rows[0]["TRPUMM"].ToString() == "")
                {
                    this.CBH01_TRPUMM.SetValue(dt.Rows[0]["TRPUMM"].ToString());
                }
                else
                {
                    this.CBH01_TRPUMM.Initialize();
                }
                
                if (dt.Rows[0]["TRHWAMUL"].ToString() == "")
                {
                    this.CBH01_TRHWAMUL.SetValue(dt.Rows[0]["TRHWAMUL"].ToString());
                }
                else
                {
                    this.CBH01_TRHWAMUL.Initialize();
                }

                this.TXT01_TRJUNGRY.SetValue(dt.Rows[0]["TRJUNGRY"].ToString());

                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                SetFocus(this.CBH01_TRUNSONG.CodeText);
            }
            else
            {
                UP_Field_Init();
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            BTN61_SAV.Visible = true;
            BTN61_REM.Visible = false;

            UP_Field_Init();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {   
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6A5DC298",this.FPS91_TY_S_UT_6A5EX301.GetValue("TRNUMNO2").ToString(),
                                                        this.FPS91_TY_S_UT_6A5EX301.GetValue("TRNUMNO1").ToString());

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");

            UP_Field_Init();

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                if (fsGUBUN == "NEW")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6A5DA296", this.TXT01_TRNUMNO2.GetValue(),
                                                                this.TXT01_TRNUMNO1.GetValue(),
                                                                this.CBH01_TRUNSONG.GetValue(),
                                                                this.CBH01_TRHYUNGT.GetValue(),
                                                                this.TXT01_TRCHJUSO.GetValue(),
                                                                this.TXT01_TRCHTEL.GetValue(),
                                                                this.TXT01_TRUNNAME.GetValue(),
                                                                this.TXT01_TRJUMIN1.GetValue(),
                                                                this.TXT01_TRJUMIN2.GetValue(),
                                                                this.TXT01_TRGIJUSO.GetValue(),
                                                                this.TXT01_TRGITEL.GetValue(),
                                                                this.CBH01_TRHWAJU1.GetValue(),
                                                                this.CBH01_TRHWAJU2.GetValue(),
                                                                this.CBH01_TRHWAJU3.GetValue(),
                                                                this.TXT01_TRBALSU.GetValue(),
                                                                this.TXT01_TRBIGO.GetValue(),
                                                                this.TXT01_TRGUBUN.GetValue(),
                                                                this.TXT01_TRTOTAL.GetValue(),
                                                                this.TXT01_TRCOUNT.GetValue(),
                                                                this.CBH01_TRPUMM.GetValue(),
                                                                this.TXT01_TRJUNGRY.GetValue(),
                                                                this.CBH01_TRHWAMUL.GetValue());
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6A5DB297", this.CBH01_TRUNSONG.GetValue(), 
                                                                this.CBH01_TRHYUNGT.GetValue(),
                                                                this.TXT01_TRCHJUSO.GetValue(),
                                                                this.TXT01_TRCHTEL.GetValue(),
                                                                this.TXT01_TRUNNAME.GetValue(),
                                                                this.TXT01_TRJUMIN1.GetValue(),
                                                                this.TXT01_TRJUMIN2.GetValue(),
                                                                this.TXT01_TRGIJUSO.GetValue(),
                                                                this.TXT01_TRGITEL.GetValue(),
                                                                this.CBH01_TRHWAJU1.GetValue(),
                                                                this.CBH01_TRHWAJU2.GetValue(),
                                                                this.CBH01_TRHWAJU3.GetValue(),
                                                                this.TXT01_TRBALSU.GetValue(),
                                                                this.TXT01_TRBIGO.GetValue(),
                                                                this.TXT01_TRGUBUN.GetValue(),
                                                                this.TXT01_TRTOTAL.GetValue(),
                                                                this.TXT01_TRCOUNT.GetValue(),
                                                                this.CBH01_TRPUMM.GetValue(),
                                                                this.TXT01_TRJUNGRY.GetValue(),
                                                                this.CBH01_TRHWAMUL.GetValue(),
                                                                this.TXT01_TRNUMNO2.GetValue(),
                                                                this.TXT01_TRNUMNO1.GetValue());
                    this.DbConnector.ExecuteNonQuery();
                }

                fsGUBUN = "MOD";
                this.BTN61_REM.Visible = true;

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AEDS371",
                    this.TXT01_TRNUMNO2.GetValue().ToString(),
                    this.TXT01_TRNUMNO1.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 차량번호 입니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBH01_TRPUMM.GetValue().ToString() == "0")
            {
                if (this.CBH01_TRHWAMUL.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("화물 코드를 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    this.CBH01_TRHWAMUL.CodeText.Focus();

                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
        
        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_UT_6A5EX301_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sSTNUM = string.Empty;
            string sEDNUM = string.Empty;
            string sNUM = string.Empty;
            string sGUBUN = string.Empty;
            string sTRMUMN = string.Empty;
            string sTRMUMNO1 = string.Empty;
            string sTRMUMNO2 = string.Empty;
            string sBARCODE = string.Empty;

            fsGUBUN = "MOD";

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6AEDS371", this.FPS91_TY_S_UT_6A5EX301.GetValue("TRNUMNO2").ToString(),
                                                        this.FPS91_TY_S_UT_6A5EX301.GetValue("TRNUMNO1").ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            sBARCODE = dt.Rows[0]["BDCODE"].ToString().Trim();
            sTRMUMN = dt.Rows[0]["TRNUMNO1"].ToString().Trim();
            sTRMUMNO2 = dt.Rows[0]["TRNUMNO2"].ToString().Trim();

            if (sTRMUMN.ToString().Length == 5)
            {
                sTRMUMNO1 = sTRMUMN.Substring(4, 1).ToString();
            }
            else if (sTRMUMN.ToString().Length == 3)            
            {
                sTRMUMNO1 = sTRMUMN.Substring(2, 1).ToString();
            }
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6A5DD299", sTRMUMNO1.Trim());

            DataTable dt2 = this.DbConnector.ExecuteDataTable();

            if (dt2.Rows.Count > 0)
            {
                sTRMUMNO1 = dt2.Rows[0]["BDCODE"].ToString();
            }

            this.TXT01_BARCODE.SetValue(sBARCODE + sTRMUMNO1 + sTRMUMNO2);
            this.TXT01_TRNUMNO2.SetValue(dt.Rows[0]["TRNUMNO2"].ToString());
            this.TXT01_TRNUMNO1.SetValue(dt.Rows[0]["TRNUMNO1"].ToString());
            this.CBH01_TRUNSONG.SetValue(dt.Rows[0]["TRUNSONG"].ToString());
            this.CBH01_TRHYUNGT.SetValue(dt.Rows[0]["TRHYUNGT"].ToString());
            this.TXT01_TRCHJUSO.SetValue(dt.Rows[0]["TRCHJUSO"].ToString());
            this.TXT01_TRCHTEL.SetValue(dt.Rows[0]["TRCHTEL"].ToString());
            this.TXT01_TRUNNAME.SetValue(dt.Rows[0]["TRUNNAME"].ToString());
            this.TXT01_TRJUMIN1.SetValue(dt.Rows[0]["TRJUMIN1"].ToString());
            this.TXT01_TRJUMIN2.SetValue(dt.Rows[0]["TRJUMIN2"].ToString());
            this.TXT01_TRGIJUSO.SetValue(dt.Rows[0]["TRGIJUSO"].ToString());
            this.TXT01_TRGITEL.SetValue(dt.Rows[0]["TRGITEL"].ToString());
            this.CBH01_TRHWAJU1.SetValue(dt.Rows[0]["TRHWAJU1"].ToString());
            this.CBH01_TRHWAJU2.SetValue(dt.Rows[0]["TRHWAJU2"].ToString());
            this.CBH01_TRHWAJU3.SetValue(dt.Rows[0]["TRHWAJU3"].ToString());
            this.TXT01_TRBALSU.SetValue(dt.Rows[0]["TRBALSU"].ToString());
            this.TXT01_TRBIGO.SetValue(dt.Rows[0]["TRBIGO"].ToString());
            this.TXT01_TRGUBUN.SetValue(dt.Rows[0]["TRGUBUN"].ToString());
            this.TXT01_TRTOTAL.SetValue(dt.Rows[0]["TRTOTAL"].ToString());
            this.TXT01_TRCOUNT.SetValue(dt.Rows[0]["TRCOUNT"].ToString());
            if (dt.Rows[0]["TRPUMM"].ToString() == "")
            {
                this.CBH01_TRPUMM.SetValue(dt.Rows[0]["TRPUMM"].ToString());
            }
            else
            {
                this.CBH01_TRPUMM.Initialize();
            }

            if (dt.Rows[0]["TRHWAMUL"].ToString() == "")
            {
                this.CBH01_TRHWAMUL.SetValue(dt.Rows[0]["TRHWAMUL"].ToString());
            }
            else
            {
                this.CBH01_TRHWAMUL.Initialize();
            }
            this.TXT01_TRJUNGRY.SetValue(dt.Rows[0]["TRJUNGRY"].ToString());

            this.BTN61_SAV.Visible = true;
            this.BTN61_REM.Visible = true;

            Timer tmr = new Timer();
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Field_Init()
        {
            this.TXT01_BARCODE.SetValue("");
            this.TXT01_TRNUMNO2.SetValue("");
            this.TXT01_TRNUMNO1.SetValue("");
            this.CBH01_TRUNSONG.SetValue("");
            this.CBH01_TRHYUNGT.SetValue("");
            this.TXT01_TRCHJUSO.SetValue("");
            this.TXT01_TRCHTEL.SetValue("");
            this.TXT01_TRUNNAME.SetValue("");
            this.TXT01_TRJUMIN1.SetValue("");
            this.TXT01_TRJUMIN2.SetValue("");
            this.TXT01_TRGIJUSO.SetValue("");
            this.TXT01_TRGITEL.SetValue("");
            this.CBH01_TRHWAJU1.SetValue("");
            this.CBH01_TRHWAJU2.SetValue("");
            this.CBH01_TRHWAJU3.SetValue("");
            this.TXT01_TRBALSU.SetValue("");
            this.TXT01_TRBIGO.SetValue("");
            this.TXT01_TRGUBUN.SetValue("");
            this.TXT01_TRTOTAL.SetValue("");
            this.TXT01_TRCOUNT.SetValue("");
            this.CBH01_TRPUMM.SetValue("");
            this.CBH01_TRHWAMUL.SetValue("");
            this.TXT01_TRJUNGRY.SetValue("");
        }
        #endregion

        #region Description : 텍스트 박스 엔터키
        private void TXT01_BARCODE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }

        private void TXT01_TRJUNGRY_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 페이지 로드 포커스
        void tmrPage_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.TXT01_TRNUMNO2.Focus();
        }
        #endregion

        #region Description : 그리드 더블클릭 포커스
        void tmr_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            this.SetFocus(this.CBH01_TRUNSONG.CodeText);
        }
        #endregion
    }
}
