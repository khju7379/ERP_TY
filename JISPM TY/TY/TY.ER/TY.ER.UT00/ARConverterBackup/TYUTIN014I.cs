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
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    public partial class TYUTIN014I : TYBase
    {
        private int fiRow          = 0;

        string fsSVMTQTY           = string.Empty;
        string fsDPMTQTY           = string.Empty;
        string fsDPDRQTY           = string.Empty;
        string fsPOPUP             = string.Empty;

        private string fsCJYSHWAJU = string.Empty;

        private string fsCJYDHWAJU = string.Empty;
        private string fsCJYSDATE  = string.Empty;
        private string fsCJYSSEQ   = string.Empty;
        private string fsCJYDSEQ   = string.Empty;

        private string fsYNQTY     = string.Empty;

        private string fsGUBUN     = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIN014I()
        {
            InitializeComponent();
        }

        private void TYUTIN014I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            SetStartingFocus(this.DTP01_STIPHANG);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_SEARCH();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_TXTBOX_ReadOnly("NEW");
            UP_BUTTON_Visible("NEW");

            UP_FieldClear("");

            fsGUBUN = "NEW";
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sDRUJ_GUBUN  = string.Empty;
            string sDRUT_GUBUN  = string.Empty;
            string sSURV_GUBUN  = string.Empty;

            string sDRUJ_GUBUN1 = string.Empty;
            string sDRUT_GUBUN1 = string.Empty;
            string sSURV_GUBUN1 = string.Empty;

            string sDJPOQTY     = string.Empty;
            string sDJCHQTY     = string.Empty;
            string sDJJEQTY     = string.Empty;

            DataTable dt = new DataTable();

            // 화물별 드럼 재고 파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7907",
                this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_DPBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_DPMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_DPHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_DPCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_DPJGHWAJU.GetValue().ToString(),          // 재고화주

                this.CBH01_DPYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_DPYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_DPYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_DPYSSEQ.GetValue().ToString(),            // 양수순번
                this.TXT01_DPJUNG.GetValue().ToString()              // 중량
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDJPOQTY = dt.Rows[0]["DJPOQTY"].ToString();
                sDJCHQTY = dt.Rows[0]["DJCHQTY"].ToString();
                sDJJEQTY = dt.Rows[0]["DJJEQTY"].ToString();
            }
            else
            {
                sDRUJ_GUBUN = "INVALID";
            }

            if (sDRUJ_GUBUN == "INVALID")
            {
                sDRUJ_GUBUN1 = "등록";
                sDJPOQTY = Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim());
                sDJJEQTY = Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim());
            }

            if ((sDRUJ_GUBUN != "INVALID") && (fsGUBUN == "NEW"))
            {
                sDRUJ_GUBUN1 = "수정";
                sDJPOQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJPOQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");

                sDJJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJJEQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");
            }

            if ((sDRUJ_GUBUN != "INVALID") && (fsGUBUN == "UPT"))
            {
                sDRUJ_GUBUN1 = "수정";
                sDJPOQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJPOQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");

                sDJJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJJEQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");
            }

            string sDTPOQTY = string.Empty;
            string sDTCHQTY = string.Empty;
            string sDTJEQTY = string.Empty;

            // 탱크별 드럼재고 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7908",
                this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_DPCHTANK.GetValue().ToString().Trim(),    // 탱크번호
                this.TXT01_DPJUNG.GetValue().ToString()              // 중량
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDTPOQTY = dt.Rows[0]["DTPOQTY"].ToString();
                sDTCHQTY = dt.Rows[0]["DTCHQTY"].ToString();
                sDTJEQTY = dt.Rows[0]["DTJEQTY"].ToString();
            }
            else
            {
                sDRUT_GUBUN = "INVALID";
            }

            if (sDRUT_GUBUN == "INVALID")
            {
                sDRUT_GUBUN1 = "등록";
                sDTPOQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
                sDTJEQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
            }

            if ((sDRUT_GUBUN != "INVALID") && (fsGUBUN == "NEW"))
            {
                sDRUT_GUBUN1 = "수정";
                sDTPOQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTPOQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");

                sDTJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTJEQTY.ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");
            }

            if ((sDRUT_GUBUN != "INVALID") && (fsGUBUN == "UPT"))
            {
                sDRUT_GUBUN1 = "수정";
                sDTPOQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTPOQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString().ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");

                sDTJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTJEQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString().ToString())))
                           + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");
            }

            string sSVCHULQTY = string.Empty;

            // SURVEY파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN442",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVCHULQTY = dt.Rows[0]["SVCHULQTY"].ToString();
            }

            if (fsGUBUN == "NEW") // 저장
            {
                sSVCHULQTY = (
                               double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVCHULQTY.ToString())))
                             + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString().Trim())))
                             ).ToString("0.000");
            }

            if (fsGUBUN == "UPT") // 수정
            {
                sSVCHULQTY = (
                               double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVCHULQTY.ToString())))
                             + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString().Trim())))
                             - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPMTQTY.ToString())))
                             ).ToString("0.000");
            }

            this.DbConnector.CommandClear();

            if (fsGUBUN == "NEW") // 저장
            {
                // 일자별 DRUM 포장 등록
                this.DbConnector.Attach("TY_P_UT_6C7G8988",
                                        Get_Date(this.DTP01_DPDATE.GetValue().ToString()),     // 포장일자
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                        this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                        this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                        Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                        this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                        this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                        this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                        this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                        Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                        this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                        this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        Set_TankNo(this.TXT01_DPCHTANK.GetValue().ToString()), // 출고탱크
                                        Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString()), // 포장수량
                                        Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString()), // MT량
                                        Get_Numeric(this.TXT01_DPKLQTY.GetValue().ToString()), // KL량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }
            else // 수정
            {
                // 일자별 DRUM 포장 수정
                this.DbConnector.Attach("TY_P_UT_6C7G4989",
                                        Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString()), // 포장수량
                                        Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString()), // MT량
                                        Get_Numeric(this.TXT01_DPKLQTY.GetValue().ToString()), // KL량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                        Get_Date(this.DTP01_DPDATE.GetValue().ToString()),     // 포장일자
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                        this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                        this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                        Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                        this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                        this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                        this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                        this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                        Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                        this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                        this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        this.TXT01_DPCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                        );
            }


            if (sDRUJ_GUBUN1.ToString() == "등록")
            {
                // 화물별 DRUM 재고파일 등록
                this.DbConnector.Attach("TY_P_UT_6C7H2991",
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                        this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                        this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                        Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                        this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                        this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                        this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                        this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                        Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                        this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                        this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        Get_Numeric(sDJPOQTY.ToString()),                      // 포장수량
                                        Get_Numeric(sDJCHQTY.ToString()),                      // 출고수량
                                        Get_Numeric(sDJJEQTY.ToString()),                      // 재고수량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }

            if (sDRUJ_GUBUN1.ToString() == "수정")
            {
                if (double.Parse(Get_Numeric(sDJPOQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDJCHQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDJJEQTY.ToString())) == 0)
                {
                    // 화물별 DRUM 재고파일 삭제
                    this.DbConnector.Attach("TY_P_UT_6C7H5993",
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                            this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                            this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                            Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                            this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                            this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                            this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                            this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                            Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                            this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                            this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
                else
                {
                    // 화물별 DRUM 재고파일 수정
                    this.DbConnector.Attach("TY_P_UT_6C7H4992",
                                            Get_Numeric(sDJPOQTY.ToString()),                      // 포장수량
                                            Get_Numeric(sDJCHQTY.ToString()),                      // 출고수량
                                            Get_Numeric(sDJJEQTY.ToString()),                      // 재고수량
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                            this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                            this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                            Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                            this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                            this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                            this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                            this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                            Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                            this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                            this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
            }

            if (sDRUT_GUBUN1.ToString() == "등록")
            {
                // 탱크별 DRUM 파일 등록
                this.DbConnector.Attach("TY_P_UT_6C7H3994",
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        Set_TankNo(this.TXT01_DPCHTANK.GetValue().ToString()), // 탱크번호
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        Get_Numeric(sDTPOQTY.ToString()),                      // 포장수량
                                        Get_Numeric(sDTCHQTY.ToString()),                      // 출고수량
                                        Get_Numeric(sDTJEQTY.ToString()),                      // 재고수량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }

            if (sDRUT_GUBUN1.ToString() == "수정")
            {
                if (double.Parse(Get_Numeric(sDTPOQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDTCHQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDTJEQTY.ToString())) == 0)
                {
                    // 탱크별 DRUM 파일 삭제
                    this.DbConnector.Attach("TY_P_UT_6C7HC995",
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPCHTANK.GetValue().ToString().Trim(),      // 탱크번호
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
                else
                {
                    // 화물별 DRUM 재고파일 수정
                    this.DbConnector.Attach("TY_P_UT_6C7HD996",
                                            Get_Numeric(sDTPOQTY.ToString()),                      // 포장수량
                                            Get_Numeric(sDTCHQTY.ToString()),                      // 출고수량
                                            Get_Numeric(sDTJEQTY.ToString()),                      // 재고수량
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPCHTANK.GetValue().ToString().Trim(),      // 탱크번호
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
            }

            // SURVEY 수정
            this.DbConnector.Attach("TY_P_UT_6C7G4987",
                                    sSVCHULQTY.ToString(),                                 // 출고수량
                                    
                                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                    this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                    this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                    this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                    this.TXT01_DPCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            SetFocus(this.DTP01_DPDATE);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sDRUJ_GUBUN = string.Empty;
            string sDRUT_GUBUN = string.Empty;
            string sSURV_GUBUN = string.Empty;

            string sDRUJ_GUBUN1 = string.Empty;
            string sDRUT_GUBUN1 = string.Empty;
            string sSURV_GUBUN1 = string.Empty;

            string sDJPOQTY = string.Empty;
            string sDJCHQTY = string.Empty;
            string sDJJEQTY = string.Empty;

            DataTable dt = new DataTable();

            // 화물별 드럼 재고 파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7907",
                this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_DPBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_DPMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_DPHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_DPCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_DPJGHWAJU.GetValue().ToString(),          // 재고화주

                this.CBH01_DPYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_DPYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_DPYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_DPYSSEQ.GetValue().ToString(),            // 양수순번
                this.TXT01_DPJUNG.GetValue().ToString()              // 중량
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDJPOQTY = dt.Rows[0]["DJPOQTY"].ToString();
                sDJCHQTY = dt.Rows[0]["DJCHQTY"].ToString();
                sDJJEQTY = dt.Rows[0]["DJJEQTY"].ToString();
            }
            else
            {
                sDRUJ_GUBUN = "INVALID";
            }

            if (sDRUJ_GUBUN == "INVALID")
            {
                sDRUJ_GUBUN1 = "등록";
                sDJPOQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
                sDJJEQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
            }

            if (sDRUJ_GUBUN != "INVALID")
            {
                sDRUJ_GUBUN1 = "수정";
                sDJPOQTY = Convert.ToString(
                           double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJPOQTY.ToString())))
                         - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim()))));

                sDJJEQTY = Convert.ToString(
                           double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJJEQTY.ToString())))
                         - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim()))));
            }


            string sDTPOQTY = string.Empty;
            string sDTCHQTY = string.Empty;
            string sDTJEQTY = string.Empty;

            // 탱크별 드럼재고 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7908",
                this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_DPCHTANK.GetValue().ToString(),           // 탱크번호
                this.TXT01_DPJUNG.GetValue().ToString()              // 중량
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sDTPOQTY = dt.Rows[0]["DTPOQTY"].ToString();
                sDTCHQTY = dt.Rows[0]["DTCHQTY"].ToString();
                sDTJEQTY = dt.Rows[0]["DTJEQTY"].ToString();
            }
            else
            {
                sDRUT_GUBUN = "INVALID";
            }

            if (sDRUT_GUBUN == "INVALID")
            {
                sDRUT_GUBUN1 = "등록";
                sDTPOQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
                sDTJEQTY = this.TXT01_DPDRQTY.GetValue().ToString().Trim();
            }

            if (sDRUT_GUBUN != "INVALID")
            {
                sDRUT_GUBUN1 = "수정";

                sDTPOQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTPOQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");

                sDTJEQTY = (
                             double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTJEQTY.ToString())))
                           - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString().Trim())))
                           ).ToString("0.000");
            }

            string sSVCHULQTY = string.Empty;

            // SURVEY파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN442",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSVCHULQTY = dt.Rows[0]["SVCHULQTY"].ToString();
            }

            sSVCHULQTY = (
                            double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVCHULQTY.ToString())))
                          - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPMTQTY.ToString())))
                         ).ToString("0.000");


            this.DbConnector.CommandClear();

            // 일자별 DRUM 포장 삭제
            this.DbConnector.Attach("TY_P_UT_6C7G4990",
                                    Get_Date(this.DTP01_DPDATE.GetValue().ToString()),     // 포장일자
                                    this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                    this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                    this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                    this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                    this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                    this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                    this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                    Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                    this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                    this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                    this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                    this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                    Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                    this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                    this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                    this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                    this.TXT01_DPCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                    );


            if (sDRUJ_GUBUN1.ToString() == "등록")
            {
                // 화물별 DRUM 재고파일 등록
                this.DbConnector.Attach("TY_P_UT_6C7H2991",
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                        this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                        this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                        Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                        this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                        this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                        this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                        this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                        Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                        this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                        this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        Get_Numeric(sDJPOQTY.ToString()),                      // 포장수량
                                        Get_Numeric(sDJCHQTY.ToString()),                      // 출고수량
                                        Get_Numeric(sDJJEQTY.ToString()),                      // 재고수량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }

            if (sDRUJ_GUBUN1.ToString() == "수정")
            {
                if (double.Parse(Get_Numeric(sDJPOQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDJCHQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDJJEQTY.ToString())) == 0)
                {
                    // 화물별 DRUM 재고파일 삭제
                    this.DbConnector.Attach("TY_P_UT_6C7H5993",
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                            this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                            this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                            Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                            this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                            this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                            this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                            this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                            Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                            this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                            this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
                else
                {
                    // 화물별 DRUM 재고파일 수정
                    this.DbConnector.Attach("TY_P_UT_6C7H4992",
                                            Get_Numeric(sDJPOQTY.ToString()),                      // 포장수량
                                            Get_Numeric(sDJCHQTY.ToString()),                      // 출고수량
                                            Get_Numeric(sDJJEQTY.ToString()),                      // 재고수량
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPBLNO.GetValue().ToString(),               // BL번호
                                            this.TXT01_DPMSNSEQ.GetValue().ToString(),             // MSN번호
                                            this.TXT01_DPHSNSEQ.GetValue().ToString(),             // HSN번호
                                            Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),   // 통관일자
                                            this.TXT01_DPCHASU.GetValue().ToString(),              // 통관차수
                                            this.CBH01_DPJGHWAJU.GetValue().ToString(),            // 재고화주
                                            this.CBH01_DPYSHWAJU.GetValue().ToString(),            // 양수화주
                                            this.CBH01_DPYDHWAJU.GetValue().ToString(),            // 양도화주
                                            Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),   // 양수일자
                                            this.TXT01_DPYDSEQ.GetValue().ToString(),              // 양도차수
                                            this.TXT01_DPYSSEQ.GetValue().ToString(),              // 양수순번
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
            }

            if (sDRUT_GUBUN1.ToString() == "등록")
            {
                // 탱크별 DRUM 파일 등록
                this.DbConnector.Attach("TY_P_UT_6C7H3994",
                                        this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                        Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                        this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                        this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                        this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                        Set_TankNo(this.TXT01_DPCHTANK.GetValue().ToString()), // 탱크번호
                                        this.TXT01_DPJUNG.GetValue().ToString(),               // 중량
                                        Get_Numeric(sDTPOQTY.ToString()),                      // 포장수량
                                        Get_Numeric(sDTCHQTY.ToString()),                      // 출고수량
                                        Get_Numeric(sDTJEQTY.ToString()),                      // 재고수량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }

            if (sDRUT_GUBUN1.ToString() == "수정")
            {
                if (double.Parse(Get_Numeric(sDTPOQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDTCHQTY.ToString())) == 0 && double.Parse(Get_Numeric(sDTJEQTY.ToString())) == 0)
                {
                    // 탱크별 DRUM 파일 삭제
                    this.DbConnector.Attach("TY_P_UT_6C7HC995",
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPCHTANK.GetValue().ToString().Trim(),      // 탱크번호
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
                else
                {
                    // 화물별 DRUM 재고파일 수정
                    this.DbConnector.Attach("TY_P_UT_6C7HD996",
                                            Get_Numeric(sDTPOQTY.ToString()),                      // 포장수량
                                            Get_Numeric(sDTCHQTY.ToString()),                      // 출고수량
                                            Get_Numeric(sDTJEQTY.ToString()),                      // 재고수량
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                            this.CBH01_DPACTHWAJU.GetValue().ToString(),           // 통관화주
                                            Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                            this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                            this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                            this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                            this.TXT01_DPCHTANK.GetValue().ToString().Trim(),      // 탱크번호
                                            this.TXT01_DPJUNG.GetValue().ToString()                // 중량
                                            );
                }
            }

            // SURVEY 수정
            this.DbConnector.Attach("TY_P_UT_6C7G4987",
                                    sSVCHULQTY.ToString(),                                 // 출고수량
                                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),   // 입항일자
                                    this.CBH01_DPBONSUN.GetValue().ToString(),             // 본선
                                    this.CBH01_DPHWAJU.GetValue().ToString(),              // 화주
                                    this.CBH01_DPHWAMUL.GetValue().ToString(),             // 화물
                                    this.TXT01_DPCHTANK.GetValue().ToString().Trim()       // 출고탱크
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            UP_FieldClear("DEL");

            SetFocus(this.DTP01_DPDATE);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : DRUM 포장 전체 조회 메소드
        private void UP_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BUB1918",
                Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                this.CBH01_SBONSUN.GetValue().ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6BUBA919.SetValue(dt);
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BUF4928",
                Get_Date(this.DTP01_DPDATE.GetValue().ToString()),   // 포장일자
                this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_DPBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_DPMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_DPHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_DPCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_DPJGHWAJU.GetValue().ToString(),          // 재고화주
                this.CBH01_DPYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_DPYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(Get_Numeric(this.DTP01_DPYSDATE.GetValue().ToString())), // 양수일자
                this.TXT01_DPYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_DPYSSEQ.GetValue().ToString(),            // 양수순번
                this.TXT01_DPJUNG.GetValue().ToString(),             // 중량
                this.TXT01_DPCHTANK.GetValue().ToString().Trim()     // 출고탱크
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsDPMTQTY = dt.Rows[0]["DPMTQTY"].ToString();
                fsDPDRQTY = dt.Rows[0]["DPDRQTY"].ToString();

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);

                UP_TXTBOX_ReadOnly("UPT");

                SetFocus(this.TXT01_DPDRQTY);
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN.ToString() == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BUF4928",
                    Get_Date(this.DTP01_DPDATE.GetValue().ToString()),   // 포장일자
                    this.CBH01_DPACTHWAJU.GetValue().ToString(),         // 통관화주
                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_DPBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_DPHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_DPHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_DPBLNO.GetValue().ToString(),             // BL번호
                    this.TXT01_DPMSNSEQ.GetValue().ToString(),           // MSN번호
                    this.TXT01_DPHSNSEQ.GetValue().ToString(),           // HSN번호
                    Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()), // 통관일자
                    this.TXT01_DPCHASU.GetValue().ToString(),            // 통관차수
                    this.CBH01_DPJGHWAJU.GetValue().ToString(),          // 재고화주
                    this.CBH01_DPYSHWAJU.GetValue().ToString(),          // 양수화주
                    this.CBH01_DPYDHWAJU.GetValue().ToString(),          // 양도화주
                    Get_Date(Get_Numeric(this.DTP01_DPYSDATE.GetValue().ToString())), // 양수일자
                    this.TXT01_DPYDSEQ.GetValue().ToString(),            // 양도차수
                    this.TXT01_DPYSSEQ.GetValue().ToString(),            // 양수순번
                    this.TXT01_DPJUNG.GetValue().ToString(),             // 중량
                    this.TXT01_DPCHTANK.GetValue().ToString().Trim()     // 출고탱크
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    SetFocus(this.DTP01_DPDATE);

                    e.Successed = false;
                    return;
                }
            }

            // 입고화물 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBY438",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBY439");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }


            // B/L별 입고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBZ440",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_DPMSNSEQ.GetValue().ToString(),
                this.TXT01_DPHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBZ441");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }

            string sSVMTQTY   = string.Empty;
            string sSVKLQTY   = string.Empty;
            string sSVCHULQTY = string.Empty;
            string sQTY       = string.Empty;


            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN442",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPCHTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }
            else
            {
                sSVMTQTY   = dt.Rows[0]["SVMTQTY"].ToString();
                sSVKLQTY   = dt.Rows[0]["SVKLQTY"].ToString();
                sSVCHULQTY = dt.Rows[0]["SVCHULQTY"].ToString();
            }

            // 통관화일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAGW725",
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_DPMSNSEQ.GetValue().ToString(),
                this.TXT01_DPHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),
                this.TXT01_DPCHASU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6BAGY726");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }

            // 통관화주 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKDD446",
                this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_DPMSNSEQ.GetValue().ToString(),
                this.TXT01_DPHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),
                this.TXT01_DPCHASU.GetValue().ToString(),
                this.CBH01_DPJGHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPYSHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPYDHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),
                this.TXT01_DPYDSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_DPYSSEQ.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKDD447");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }

            // 양수도일 경우 체크
            if (this.CBH01_DPYSHWAJU.GetValue().ToString() != "" && this.CBH01_DPYDHWAJU.GetValue().ToString() != "")
            {
                // 양수도 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_699AD123",
                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                    this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                    this.TXT01_DPMSNSEQ.GetValue().ToString(),
                    this.TXT01_DPHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),
                    this.TXT01_DPCHASU.GetValue().ToString(),
                    this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPYDHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPYSHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),
                    this.TXT01_DPYDSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_DPYSSEQ.GetValue().ToString().ToUpper()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKDD447");
                    SetFocus(this.DTP01_DPDATE);

                    e.Successed = false;
                    return;
                }
            }


            // 드럼 M/T 계산
            // M/T수량
            TXT01_DPMTQTY.SetValue(
                (
                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(TXT01_DPJUNG.GetValue().ToString())))
                * double.Parse(String.Format("{0,9:N3}", Get_Numeric(TXT01_DPDRQTY.GetValue().ToString())))
                ).ToString("0.000"));

            // 탱크재고
            TXT01_DPJEQTY.SetValue(
                (
                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY.ToString())))
                - double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVCHULQTY.ToString())))
                ).ToString("0.000"));

            // 드럼 M/T를 K/L로 환산
            TXT01_DPKLQTY.SetValue(
                (
                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString())))
                / double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY.ToString())))
                * double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVKLQTY.ToString())))
                ).ToString("0.000"));

            sQTY =
                (
                  double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVCHULQTY.ToString())))
                + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPMTQTY.GetValue().ToString())))
                ).ToString("0.000");

            if (fsGUBUN.ToString() == "NEW")
            {
                if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY.ToString()))) < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6BUAD914");
                    SetFocus(this.TXT01_DPDRQTY);

                    e.Successed = false;
                    return;
                }
            }

            if (fsGUBUN.ToString() == "UPT")
            {
                if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sSVMTQTY.ToString()))) < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sQTY.ToString()))) - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPMTQTY.ToString()))))
                {
                    this.ShowMessage("TY_M_UT_6BUAD914");
                    SetFocus(this.TXT01_DPDRQTY);

                    e.Successed = false;
                    return;
                }
            
                string sDJPOQTY = string.Empty;
                string sDJCHQTY = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BTH7907",
                    this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                    this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                    this.TXT01_DPMSNSEQ.GetValue().ToString(),
                    this.TXT01_DPHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),
                    this.TXT01_DPCHASU.GetValue().ToString(),
                    this.CBH01_DPJGHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPYSHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPYDHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),
                    this.TXT01_DPYDSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_DPYSSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_DPJUNG.GetValue().ToString()                    
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6BUAJ916");
                    SetFocus(this.DTP01_DPDATE);

                    e.Successed = false;
                    return;
                }
                else
                {
                    sDJPOQTY = dt.Rows[0]["DJPOQTY"].ToString();
                    sDJCHQTY = dt.Rows[0]["DJCHQTY"].ToString();
                }

                if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString())))
                        + double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJPOQTY.ToString())))
                        - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString())))
                        < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJCHQTY.ToString())))
                        )
                {
                    this.ShowMessage("TY_M_UT_6BUAD914");
                    SetFocus(this.TXT01_DPDRQTY);

                    e.Successed = false;
                    return;
                }


                string sDTPOQTY = string.Empty;
                string sDTCHQTY = string.Empty;

                // 화물별 드럼 재고 파일 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BTH7908",
                    this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                    this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_DPCHTANK.GetValue().ToString().Trim(),
                    this.TXT01_DPJUNG.GetValue().ToString()                    
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6BUAM917");
                    SetFocus(this.DTP01_DPDATE);

                    e.Successed = false;
                    return;
                }
                else
                {
                    sDTPOQTY = dt.Rows[0]["DTPOQTY"].ToString();
                    sDTCHQTY = dt.Rows[0]["DTCHQTY"].ToString();
                }

                if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTPOQTY.ToString())))
                        - double.Parse(String.Format("{0,9:N3}", Get_Numeric(fsDPDRQTY.ToString())))
                        + double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString())))
                        < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTCHQTY.ToString())))
                        )
                {
                    this.ShowMessage("TY_M_UT_6BUAD914");
                    SetFocus(this.TXT01_DPDRQTY);

                    e.Successed = false;
                    return;
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

        #region Description : 양수도관리 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            string sDJPOQTY = string.Empty;
            string sDJCHQTY = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7907",
                this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_DPMSNSEQ.GetValue().ToString(),
                this.TXT01_DPHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_DPCUSTIL.GetValue().ToString()),
                this.TXT01_DPCHASU.GetValue().ToString(),
                this.CBH01_DPJGHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPYSHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPYDHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_DPYSDATE.GetValue().ToString()),
                this.TXT01_DPYDSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_DPYSSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_DPJUNG.GetValue().ToString()                    
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6BUAJ916");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }
            else
            {
                sDJPOQTY = dt.Rows[0]["DJPOQTY"].ToString();
                sDJCHQTY = dt.Rows[0]["DJCHQTY"].ToString();
            }

            
            if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJPOQTY.ToString())))
                - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString())))
                < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDJCHQTY.ToString())))
                )
            {
                this.ShowMessage("TY_M_UT_6BUAD914");
            SetFocus(this.TXT01_DPDRQTY);

            e.Successed = false;
            return;
            }



            string sDTPOQTY = string.Empty;
            string sDTCHQTY = string.Empty;

            // 화물별 드럼 재고 파일 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTH7908",
                this.CBH01_DPACTHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()),
                this.CBH01_DPBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_DPHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_DPCHTANK.GetValue().ToString().Trim(),
                this.TXT01_DPJUNG.GetValue().ToString()                    
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6BUAM917");
                SetFocus(this.DTP01_DPDATE);

                e.Successed = false;
                return;
            }
            else
            {
                sDTPOQTY = dt.Rows[0]["DTPOQTY"].ToString();
                sDTCHQTY = dt.Rows[0]["DTCHQTY"].ToString();
            }

            if (double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTPOQTY.ToString())))
                - double.Parse(String.Format("{0,9:N3}", Get_Numeric(this.TXT01_DPDRQTY.GetValue().ToString())))
                < double.Parse(String.Format("{0,9:N3}", Get_Numeric(sDTCHQTY.ToString())))
                )
            {
                this.ShowMessage("TY_M_UT_6BUAD914");
                SetFocus(this.TXT01_DPDRQTY);

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN != "DEL")
            {
                this.DTP01_DPIPHANG.SetValue("");   // 입항일자
                this.CBH01_DPBONSUN.SetValue("");   // 본선
                this.CBH01_DPHWAJU.SetValue("");    // 화주
                this.CBH01_DPHWAMUL.SetValue("");   // 화물
                this.TXT01_DPBLNO.SetValue("");     // BL번호
                this.TXT01_DPMSNSEQ.SetValue("");   // MSN번호
                this.TXT01_DPHSNSEQ.SetValue("");   // HSN번호
                this.DTP01_DPCUSTIL.SetValue("");   // 통관일자
                this.TXT01_DPCHASU.SetValue("");    // 통관차수
                this.CBH01_DPACTHWAJU.SetValue(""); // 통관화주
                this.CBH01_DPJGHWAJU.SetValue("");  // 양수화주
                this.CBH01_DPYDHWAJU.SetValue("");  // 양도화주
                this.CBH01_DPYSHWAJU.SetValue("");  // 양수화주
                this.DTP01_DPYSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));  // 양수일자
                this.TXT01_DPYSSEQ.SetValue("");    // 양수순번
                this.TXT01_DPYDSEQ.SetValue("");    // 양도차수

                this.TXT01_DPJUNG.SetValue("");
                this.TXT01_DPCHTANK.SetValue("");
            }
            this.TXT01_DPJEQTY.SetValue("");
            this.TXT01_DPDRQTY.SetValue("");
            this.TXT01_DPMTQTY.SetValue("");
            this.TXT01_DPKLQTY.SetValue("");
        }
        #endregion

        #region Description : 버튼 Visible
        private void UP_BUTTON_Visible(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                this.BTN61_CHTANK.Visible = true;
                this.BTN61_UTTCODEHELP6.Visible = true;
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_CHTANK.Visible = false;
                this.BTN61_UTTCODEHELP6.Visible = false;
            }
            else if(sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_CHTANK.Visible = false;
                this.BTN61_UTTCODEHELP6.Visible = false;
            }
        }
        #endregion

        #region Description : TEXTBOX - ReadOnly
        private void UP_TXTBOX_ReadOnly(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.TXT01_DPJUNG.SetReadOnly(false);
                this.TXT01_DPCHTANK.SetReadOnly(false);
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.TXT01_DPJUNG.SetReadOnly(true);
                this.TXT01_DPCHTANK.SetReadOnly(true);
            }
            else if(sGUBUN.ToString() == "")
            {
                this.TXT01_DPJUNG.SetReadOnly(true);
                this.TXT01_DPCHTANK.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 통관조회
        private void BTN61_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            UP_CALL_JEGO();

            if (Get_Date(this.DTP01_DPIPHANG.GetValue().ToString()) != "" && this.CBH01_DPBONSUN.GetValue().ToString() != "" &&
                this.CBH01_DPHWAJU.GetValue().ToString() != "" && this.CBH01_DPHWAMUL.GetValue().ToString() != "")
            {
                // 출고탱크
                UP_GET_DPTANKNO();

                // 탱크 입고량 가져오기
                UP_GET_SVMTQTY();

                SetFocus(this.TXT01_DPJUNG);
            }
        }
        #endregion

        #region Description : 재고 조회(통관 및 양수도)
        private void UP_CALL_JEGO()
        {
            TYUTGB008S popup = new TYUTGB008S("");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_DPIPHANG.SetValue(popup.fsIPHANG);   // 입항일자
                this.CBH01_DPBONSUN.SetValue(popup.fsBONSUN);   // 본선
                this.CBH01_DPHWAJU.SetValue(popup.fsHWAJU);     // 화주
                this.CBH01_DPHWAMUL.SetValue(popup.fsHWAMUL);   // 화물
                this.TXT01_DPBLNO.SetValue(popup.fsBLNO);       // BL번호
                this.TXT01_DPMSNSEQ.SetValue(popup.fsMSNSEQ);   // MSN번호
                this.TXT01_DPHSNSEQ.SetValue(popup.fsHSNSEQ);   // HSN번호
                this.DTP01_DPCUSTIL.SetValue(popup.fsCUSTIL);   // 통관일자
                this.TXT01_DPCHASU.SetValue(popup.fsCHASU);     // 통관차수
                this.CBH01_DPACTHWAJU.SetValue(popup.fsACTHJ);  // 통관화주
                this.CBH01_DPJGHWAJU.SetValue(popup.fsJGHWAJU); // 재고화주
                this.CBH01_DPYSHWAJU.SetValue(popup.fsYSHWAJU); // 양수화주
                this.CBH01_DPYDHWAJU.SetValue(popup.fsYDHWAJU); // 양도화주
                this.DTP01_DPYSDATE.SetValue(popup.fsYSDATE);   // 양수일자
                this.TXT01_DPYDSEQ.SetValue(popup.fsYDSEQ);     // 양도순번
                this.TXT01_DPYSSEQ.SetValue(popup.fsYSSEQ);     // 양수순번
            }
        }
        #endregion

        #region Description : 출고탱크 가져오기
        private void UP_GET_DPTANKNO()
        {
            this.TXT01_DPCHTANK.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3715",
                this.DTP01_DPIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_DPCHTANK.SetValue(dt.Rows[0]["SVTANKNO"].ToString());
                }
            }
        }
        #endregion

        #region Description : 출고탱크 입고량 가져오기
        private void UP_GET_SVMTQTY()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BTGX906",
                this.DTP01_DPIPHANG.GetValue().ToString(), // 입항일자
                this.CBH01_DPBONSUN.GetValue().ToString(), // 본선
                this.CBH01_DPHWAJU.GetValue().ToString(),  // 화주
                this.CBH01_DPHWAMUL.GetValue().ToString(), // 화물
                this.TXT01_DPCHTANK.GetValue().ToString().Trim()  // 출고탱크
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsSVMTQTY = dt.Rows[0]["SVMTQTY"].ToString();
            }
        }
        #endregion

        #region Description : 포장수량 텍스트박스 이벤트
        private void TXT01_DPDRQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 출고탱크 이벤트
        private void BTN61_CHTANK_Click(object sender, EventArgs e)
        {
            TYUTGB009S popup = new TYUTGB009S(this.DTP01_DPIPHANG.GetValue().ToString(), this.CBH01_DPBONSUN.GetValue().ToString(),
                                              this.CBH01_DPHWAJU.GetValue().ToString(),  this.CBH01_DPHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_DPCHTANK.SetValue(popup.fsTANKNO); // 출고탱크
                this.TXT01_DPJEQTY.SetValue(popup.fsSVJGQTY);

                SetFocus(this.TXT01_DPDRQTY);
            }
        }

        private void TXT01_DPCHTANK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB009S popup = new TYUTGB009S(this.DTP01_DPIPHANG.GetValue().ToString(), this.CBH01_DPBONSUN.GetValue().ToString(),
                                                  this.CBH01_DPHWAJU.GetValue().ToString(),  this.CBH01_DPHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_DPCHTANK.SetValue(popup.fsTANKNO); // 출고탱크
                    this.TXT01_DPJEQTY.SetValue(popup.fsSVJGQTY);

                    SetFocus(this.TXT01_DPDRQTY);
                }
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6BUBA919_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_DPDATE.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPDATE").ToString());         // 포장일자
            this.DTP01_DPIPHANG.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPIPHANG").ToString());     // 입항일자
            this.CBH01_DPBONSUN.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPBONSUN").ToString());     // 본선
            this.CBH01_DPHWAJU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPHWAJU").ToString());       // 화주
            this.CBH01_DPHWAMUL.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPHWAMUL").ToString());     // 화물
            this.TXT01_DPBLNO.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPBLNO").ToString());         // BL번호
            this.TXT01_DPMSNSEQ.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPMSNSEQ").ToString());     // MSN번호
            this.TXT01_DPHSNSEQ.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPHSNSEQ").ToString());     // HSN번호
            this.DTP01_DPCUSTIL.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPCUSTIL").ToString());     // 통관일자
            this.TXT01_DPCHASU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPCHASU").ToString());       // 통관차수
            this.CBH01_DPACTHWAJU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPACTHWAJU").ToString()); // 통관화주
            this.CBH01_DPJGHWAJU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPJGHWAJU").ToString());   // 재고화주
            this.CBH01_DPYSHWAJU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPYSHWAJU").ToString());   // 양수화주
            this.CBH01_DPYDHWAJU.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPYDHWAJU").ToString());   // 양도화주
            this.DTP01_DPYSDATE.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPYSDATE").ToString());     // 양수일자
            this.TXT01_DPYDSEQ.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPYDSEQ").ToString());       // 양도순번
            this.TXT01_DPYSSEQ.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPYSSEQ").ToString());       // 양수순번
            this.TXT01_DPJUNG.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPJUNG").ToString());         // 중량
            this.TXT01_DPCHTANK.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DPCHTANK").ToString());     // 출고탱크
            this.TXT01_DRMTQTY.SetValue(this.FPS91_TY_S_UT_6BUBA919.GetValue("DRMTQTY").ToString());       // MT량            

            // 확인
            UP_RUN();
        }
        #endregion

        #region Description : 화물 이벤트
        private void CBH01_SHWAMUL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 포장일자별 조회
        private void BTN61_UTTCODEHELP5_Click(object sender, EventArgs e)
        {
            TYUTGB024S popup = new TYUTGB024S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion
    }
}