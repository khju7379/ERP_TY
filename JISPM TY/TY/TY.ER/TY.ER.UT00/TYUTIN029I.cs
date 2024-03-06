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
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    public partial class TYUTIN029I : TYBase
    {
        private int fiRow = 0;

        string fsPOPUP = string.Empty;

        private string fsCJJGHWAJU = string.Empty;
        private string fsCJYSHWAJU = string.Empty;

        private string fsCJYDHWAJU = string.Empty;
        private string fsCJYSDATE = string.Empty;
        private string fsCJYSSEQ = string.Empty;
        private string fsCJYDSEQ = string.Empty;

        private string fsYNQTY = string.Empty;

        private string fsYNYSHWAJU = string.Empty;
        private string fsYNYDHWAJU = string.Empty;
        private string fsYNYSDATE = string.Empty;
        private string fsYNYDSEQ = string.Empty;
        private string fsYNYSSEQ = string.Empty;

        private string fsYNYSYDHWAJU = string.Empty;
        private string fsYNYSYDDATE = string.Empty;
        private string fsYNYSYDSEQ = string.Empty;
        private string fsYNYSYSSEQ = string.Empty;

        private string fsYNWNYSHWAJU = string.Empty;
        private string fsYNWNYDHWAJU = string.Empty;
        private string fsYNWNYSDATE = string.Empty;
        private string fsYNWNYDSEQ = string.Empty;
        private string fsYNWNYSSEQ = string.Empty;



        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIN029I()
        {
            InitializeComponent();
        }

        private void TYUTIN029I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            this.DTP01_STIPHANG.SetValue("");
            this.DTP01_EDIPHANG.SetValue("");

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
            UP_Field_ReadOnly("");
            UP_BUTTON_Visible("NEW");

            UP_FieldClear();

            fsGUBUN = "NEW";
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sYNQTY = "0";
            string sYNYSYDQTY = "0";

            if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            {
                sYNQTY = Get_Numeric(this.TXT01_YNQTY.GetValue().ToString());

                fsYNWNYSHWAJU = "";
                fsYNWNYDHWAJU = "";
                fsYNWNYSDATE = "0";
                fsYNWNYDSEQ = "0";
                fsYNWNYSSEQ = "0";

                fsYNWNYSHWAJU = this.CBH01_YNYSHWAJU.GetValue().ToString();
                fsYNWNYDHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();
                fsYNWNYSDATE = Get_Date(this.DTP01_YNYSDATE.GetValue().ToString());
                fsYNWNYDSEQ = this.TXT01_YNYDSEQ.GetValue().ToString();
                fsYNWNYSSEQ = this.TXT01_YNYSSEQ.GetValue().ToString();
            }
            else
            {
                sYNYSYDQTY = Get_Numeric(this.TXT01_YNQTY.GetValue().ToString());

                // 원천 양수/양도화주, 양수일자, 양도순번, 양수순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_77IA2198",
                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                    fsCJYDHWAJU.ToString(),                              // 양도화주
                    this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양수화주(이전 데이터의 양수화주)
                    fsCJYSDATE.ToString(),                               // 양수분양도일자(이전 데이터의 양수일자)
                    fsCJYDSEQ.ToString(),                                // 양수분양도순번(이전 데이터의 양도순번)
                    fsCJYSSEQ.ToString()                                 // 양수분양수순번(이전 데이터의 양수순번)                    
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsYNWNYSHWAJU = dt.Rows[0]["YNWNYSHWAJU"].ToString();
                    fsYNWNYDHWAJU = dt.Rows[0]["YNWNYDHWAJU"].ToString();
                    fsYNWNYSDATE = dt.Rows[0]["YNWNYSDATE"].ToString();
                    fsYNWNYDSEQ = dt.Rows[0]["YNWNYDSEQ"].ToString();
                    fsYNWNYSSEQ = dt.Rows[0]["YNWNYSSEQ"].ToString();
                }
            }

            if (fsGUBUN == "NEW") // 저장
            {
                // 양수도 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6999O120",
                                        Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                        this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                        this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                        Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_YNYSSEQ.GetValue().ToString(),            // 양수순번
                                        sYNQTY.ToString(),                                   // 통관분양도량
                                        sYNYSYDQTY.ToString(),                               // 양수분양도량
                                        fsCJYDHWAJU.ToString(),                              // 양수양도화주
                                        fsCJYSDATE.ToString(),                               // 양수분양도일자
                                        fsCJYDSEQ.ToString(),                                // 양수분양도순번
                                        fsCJYSSEQ.ToString(),                                // 양수분양수순번
                                        "0",                                                 // 출고량
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()), // 재고량
                                        "",                                                  // 식별자
                                        fsYNWNYSHWAJU.ToString(),                            // 원천양수화주
                                        fsYNWNYDHWAJU.ToString(),                            // 원천양도화주
                                        fsYNWNYSDATE.ToString(),                             // 원천양수일자
                                        fsYNWNYDSEQ.ToString(),                              // 원천양도순번
                                        fsYNWNYSSEQ.ToString(),                              // 원천양수순번
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()         // 작성사번
                                        );

                this.DbConnector.ExecuteNonQuery();

                // 통관화주파일 등록
                UP_UTICUHJF_INS();
            }
            else // 수정
            {
                // 양수도 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6999P121",
                                        sYNQTY.ToString(),                                   // 통관분양도량
                                        sYNYSYDQTY.ToString(),                               // 양수분양도량
                                        fsCJYDHWAJU.ToString(),                              // 양수양도화주
                                        fsCJYSDATE.ToString(),                               // 양수분양도일자
                                        fsCJYDSEQ.ToString(),                                // 양수분양도순번
                                        fsCJYSSEQ.ToString(),                                // 양수분양수순번
                                        "0",                                                 // 출고량
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()), // 재고량
                                        "",                                                  // 식별자
                                        fsYNWNYSHWAJU.ToString(),                            // 원천양수화주
                                        fsYNWNYDHWAJU.ToString(),                            // 원천양도화주
                                        fsYNWNYSDATE.ToString(),                             // 원천양수일자
                                        fsYNWNYDSEQ.ToString(),                              // 원천양도순번
                                        fsYNWNYSSEQ.ToString(),                              // 원천양수순번
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),        // 작성사번
                                        Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                        this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                        this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                        Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                                        );

                this.DbConnector.ExecuteNonQuery();

                // 통관화주파일 삭제
                UP_UTICUHJF_DEL();

                // 통관화주파일 등록
                UP_UTICUHJF_INS();
            }

            UP_BUTTON_Visible("UPT");

            // 조회
            BTN61_INQ_Click(null, null);

            // 양수도관리 조회
            UP_UTIYANGF_SEARCH();

            SetFocus(this.TXT01_YNQTY);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            // 양수도 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6999P122",
                                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                    this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                    this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                    Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                    this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                    this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                                    );

            this.DbConnector.ExecuteNonQuery();

            // 통관화주파일 삭제
            UP_UTICUHJF_DEL();

            // 조회
            BTN61_INQ_Click(null, null);

            // 양수도관리 조회
            UP_UTIYANGF_SEARCH();

            UP_BUTTON_Visible("");

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 통관화주파일 등록
        private void UP_UTICUHJF_INS()
        {
            string sProcedure = string.Empty;

            fsCJJGHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();

            if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            {
                // 원화주 양도량 업데이트
                sProcedure = "TY_P_UT_69JG1160";
            }
            else
            {
                // 양수분 양도화주 양수분양도량 업데이트
                sProcedure = "TY_P_UT_69JG7162";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure.ToString(),
                                    Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()),
                                    Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()),
                                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                    fsCJJGHWAJU.ToString(),                              // 재고화주
                                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                    fsCJYSHWAJU.ToString(),                              // 양수화주
                                    fsCJYDHWAJU.ToString(),                              // 양도화주
                                    fsCJYSDATE.ToString(),                               // 양수일자
                                    fsCJYDSEQ.ToString(),                                // 양도차수
                                    fsCJYSSEQ.ToString()                                 // 양수순번
                                    );

            this.DbConnector.ExecuteNonQuery();





            #region Description : 양수화주의 양수량 업데이트

            fsCJJGHWAJU = this.CBH01_YNYSHWAJU.GetValue().ToString();

            DataTable dt = new DataTable();

            // 통관화주파일 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_69JGC164",
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                fsCJJGHWAJU.ToString(),                              // 재고화주
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_69JGK166",
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()),
                                        this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                        fsCJJGHWAJU.ToString(),                              // 재고화주
                                        Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                        this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                        Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_689CM001",
                                        this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                        fsCJJGHWAJU.ToString(),                              // 재고화주
                                        Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                        this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                        Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_YNYSSEQ.GetValue().ToString(),            // 양수순번
                                        "0",                                                 // 양도량
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()), // 양수량
                                        "0",                                                 // 양수분양도량
                                        "0",                                                 // 양수출고량
                                        "0",                                                 // 통관량
                                        "0",                                                 // 출고량
                                        Get_Numeric(this.TXT01_YNQTY.GetValue().ToString()), // 재고량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()         // 작성사번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            #endregion

        }
        #endregion

        #region Description : 통관화주파일 삭제
        private void UP_UTICUHJF_DEL()
        {
            string sProcedure = string.Empty;

            fsCJJGHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();

            if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            {
                // 원화주 양도량 업데이트
                sProcedure = "TY_P_UT_69JHJ167";
            }
            else
            {
                // 양수분 양도화주 양수분양도량 업데이트
                sProcedure = "TY_P_UT_69JHJ168";
            }

            #region Description : 양도화주의 양도량 업데이트

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure.ToString(),
                                    fsYNQTY.ToString(),
                                    fsYNQTY.ToString(),
                                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                    fsCJJGHWAJU.ToString(),                              // 이전양수화주(재고화주)
                                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                    fsCJYDHWAJU.ToString(),                              // 이전양도화주
                                    fsCJYSHWAJU.ToString(),                              // 이전양수화주
                                    fsCJYSDATE.ToString(),                               // 이전양수일자
                                    fsCJYDSEQ.ToString(),                                // 이전양도차수
                                    fsCJYSSEQ.ToString()                                 // 이전양수순번
                                    );

            this.DbConnector.ExecuteNonQuery();

            #endregion


            #region Description : 양수화주의 양수량 업데이트

            fsCJJGHWAJU = this.CBH01_YNYSHWAJU.GetValue().ToString();

            DataTable dt = new DataTable();

            // 통관화주파일 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_69JGC164",
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                fsCJJGHWAJU.ToString(),                              // 재고화주
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_69JHK169",
                                        fsYNQTY.ToString(),
                                        fsYNQTY.ToString(),
                                        this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                                        fsCJJGHWAJU.ToString(),                              // 재고화주
                                        Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                                        this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                                        Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            #endregion
        }
        #endregion

        #region Description : 양수도 전체 조회 메소드
        private void UP_SEARCH()
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            if (Get_Date(this.DTP01_STIPHANG.GetValue().ToString()) == "")
            {
                sSTDATE = "19800101";
            }
            else
            {
                sSTDATE = Get_Date(this.DTP01_STIPHANG.GetValue().ToString());
            }

            if (Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()) == "")
            {
                sEDDATE = Get_Date(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                sEDDATE = Get_Date(this.DTP01_EDIPHANG.GetValue().ToString());
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_699AE124",
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_69CFV141.SetValue(dt);
            }
        }
        #endregion

        #region Description : 양수도관리 확인
        private void UP_UTIYANGF_RUN()
        {
            fsCJYDHWAJU = "";
            fsCJYSDATE = "0";
            fsCJYSSEQ = "0";
            fsCJYDSEQ = "0";

            fsYNQTY = "0";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_699AD123",
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsYNQTY = dt.Rows[0]["YNQTY"].ToString();

                fsCJYDHWAJU = dt.Rows[0]["YNYSYDHWAJU"].ToString();
                fsCJYSDATE = dt.Rows[0]["YNYSYDDATE"].ToString();
                fsCJYDSEQ = dt.Rows[0]["YNYSYDSEQ"].ToString();
                fsCJYSSEQ = dt.Rows[0]["YNYSYSSEQ"].ToString();

                UP_UTICUHJF_SEARCH();

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);

                UP_Field_ReadOnly("INDEX");
            }
        }
        #endregion

        #region Description : 양수도관리 조회
        private void UP_UTIYANGF_SEARCH()
        {
            // 원본 소스
            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_699AE125",
            //    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
            //    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
            //    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
            //    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
            //    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
            //    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
            //    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
            //    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
            //    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
            //    this.CBH01_YNACTHJ.GetValue().ToString()             // 통관화주
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_UT_69KH8180.SetValue(dt);
            //}

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_699AE125",
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주                
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString(),            // 양수순번
                fsYNWNYSHWAJU.ToString(),                            // 원천양수화주
                fsYNWNYDHWAJU.ToString(),                            // 원천양도화주
                fsYNWNYSDATE.ToString(),                             // 원천양수일자
                fsYNWNYDSEQ.ToString(),                              // 원천양도순번
                fsYNWNYSSEQ.ToString()                               // 원천양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_69KH8180.SetValue(dt);

            // 양수도 확인
            UP_UTIYANGF_RUN();
        }
        #endregion

        #region Description : 통관화주파일 조회
        private void UP_UTICUHJF_SEARCH()
        {
            fsCJJGHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();

            if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            {
                fsCJYSHWAJU = "";
            }
            else
            {
                fsCJYSHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_69JGC164",
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 재고화주
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                fsCJYSHWAJU.ToString(),                              // 양수화주
                fsCJYDHWAJU.ToString(),                              // 양도화주
                fsCJYSDATE.ToString(),                               // 양수일자
                fsCJYDSEQ.ToString(),                                // 양도차수
                fsCJYSSEQ.ToString()                                 // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 통관량
                this.TXT01_CJCUQTY.SetValue(dt.Rows[0]["CJCUQTY"].ToString());
                // 양수량
                this.TXT01_CJYSQTY.SetValue(dt.Rows[0]["CJYSQTY"].ToString());
                // 양도량
                this.TXT01_CJYDQTY.SetValue(dt.Rows[0]["CJYDQTY"].ToString());
                // 양수양도량
                this.TXT01_CJYSYDQTY.SetValue(dt.Rows[0]["CJYSYDQTY"].ToString());
                // 양수출고량
                this.TXT01_CJYSCHQTY.SetValue(dt.Rows[0]["CJYSCHQTY"].ToString());
                // 출고량
                this.TXT01_CJCHQTY.SetValue(dt.Rows[0]["CJCHQTY"].ToString());
                // 재고량
                this.TXT01_CJJEQTY.SetValue(dt.Rows[0]["CJJEQTY"].ToString());
            }
        }
        #endregion

        #region Description : 양수도관리 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN.ToString() == "NEW")
            {
                // 양수순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_69JI3171",
                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                    this.TXT01_YNYDSEQ.GetValue().ToString()             // 양도차수
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_YNYSSEQ.SetValue(dt.Rows[0]["YNYSSEQ"].ToString());
                }
            }

            if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            {
                fsCJYSHWAJU = "";
            }
            else
            {
                fsCJYSHWAJU = this.CBH01_YNYDHWAJU.GetValue().ToString();
            }

            double dCJYDQTY = 0;
            double dCJYSQTY = 0;
            double dCJCUQTY = 0;
            double dCJCHQTY = 0;
            double dCJJEQTY = 0;
            double dCJYSYDQTY = 0;
            double dCJYSCHQTY = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_69JGC164",
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 재고화주
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                fsCJYSHWAJU.ToString(),                              // 양수화주
                fsCJYDHWAJU.ToString(),                              // 양도화주
                fsCJYSDATE.ToString(),                               // 양수일자
                fsCJYDSEQ.ToString(),                                // 양도차수
                fsCJYSSEQ.ToString()                                 // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dCJYDQTY = double.Parse(dt.Rows[0]["CJYDQTY"].ToString());
                dCJYSQTY = double.Parse(dt.Rows[0]["CJYSQTY"].ToString());
                dCJCUQTY = double.Parse(dt.Rows[0]["CJCUQTY"].ToString());
                dCJCHQTY = double.Parse(dt.Rows[0]["CJCHQTY"].ToString());
                dCJJEQTY = double.Parse(dt.Rows[0]["CJJEQTY"].ToString());
                dCJYSYDQTY = double.Parse(dt.Rows[0]["CJYSYDQTY"].ToString());
                dCJYSCHQTY = double.Parse(dt.Rows[0]["CJYSCHQTY"].ToString());
            }

            double dJEGOQTY = 0;

            if (fsGUBUN == "NEW")
            {
                dJEGOQTY = double.Parse(String.Format("{0,9:N3}", (dCJCUQTY + dCJYSQTY) - (dCJYDQTY + dCJYSYDQTY + dCJCHQTY + dCJYSCHQTY)));

                if (dJEGOQTY < double.Parse(this.TXT01_YNQTY.GetValue().ToString()))
                {
                    this.ShowMessage("TY_M_UT_69KBQ174");
                    this.TXT01_YNQTY.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else // 수정
            {
                // (통관량 + 양수량) - (입력양도량 - 수정전양도량 + 양도량 + 양수분양도량 + 출고량 + 양수분출고량)
                dJEGOQTY = double.Parse(String.Format("{0,9:N3}", (dCJCUQTY + dCJYSQTY)
                                                                - (double.Parse(this.TXT01_YNQTY.GetValue().ToString()) - double.Parse(fsYNQTY.ToString()) + dCJYDQTY + dCJYSYDQTY + dCJCHQTY + dCJYSCHQTY)));

                if (dJEGOQTY < 0)
                {
                    this.ShowMessage("TY_M_UT_69KDE175");
                    this.TXT01_YNQTY.Focus();

                    e.Successed = false;
                    return;
                }


                dCJYDQTY = 0;
                dCJYSQTY = 0;
                dCJCUQTY = 0;
                dCJCHQTY = 0;
                dCJJEQTY = 0;
                dCJYSYDQTY = 0;
                dCJYSCHQTY = 0;

                dJEGOQTY = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_69JGC164",
                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                    this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 재고화주
                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                    this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                    Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                    this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                    this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dCJYDQTY = double.Parse(dt.Rows[0]["CJYDQTY"].ToString());
                    dCJYSQTY = double.Parse(dt.Rows[0]["CJYSQTY"].ToString());
                    dCJCUQTY = double.Parse(dt.Rows[0]["CJCUQTY"].ToString());
                    dCJCHQTY = double.Parse(dt.Rows[0]["CJCHQTY"].ToString());
                    dCJJEQTY = double.Parse(dt.Rows[0]["CJJEQTY"].ToString());
                    dCJYSYDQTY = double.Parse(dt.Rows[0]["CJYSYDQTY"].ToString());
                    dCJYSCHQTY = double.Parse(dt.Rows[0]["CJYSCHQTY"].ToString());
                }

                dJEGOQTY = double.Parse(String.Format("{0,9:N3}", (dCJCUQTY + dCJYSQTY + double.Parse(this.TXT01_YNQTY.GetValue().ToString()))
                                                                - (double.Parse(fsYNQTY.ToString()) + dCJYDQTY + dCJYSYDQTY + dCJCHQTY + dCJYSCHQTY)));

                if (dJEGOQTY < 0)
                {
                    this.ShowMessage("TY_M_UT_69KBQ174");
                    this.TXT01_YNQTY.Focus();

                    e.Successed = false;
                    return;
                }






                // 출고가 되었으면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_699AD123",
                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                    this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                    this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                    this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                    this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                    this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                    Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                    this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                    this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (double.Parse(Get_Numeric(dt.Rows[0]["YNYSCHQTY"].ToString())) > 0)
                    {
                        this.ShowMessage("TY_M_UT_69KDH176");
                        this.CBH01_YNYSHWAJU.Focus();

                        e.Successed = false;
                        return;
                    }
                }

                // 출고량 있으면 수정 안됨
                if (decimal.Parse(Get_Numeric(this.TXT01_YNYSCHQTY.GetValue().ToString())) > 0)
                {
                    this.ShowMessage("TY_M_UT_69KDH176");
                    this.CBH01_YNYSHWAJU.Focus();

                    e.Successed = false;
                    return;
                }

                // 이후자료가 있으면 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_699AD123",
                    Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()),   // 입항일자
                    this.CBH01_YNBONSUN.GetValue().ToString(),             // 본선
                    this.CBH01_YNHWAJU.GetValue().ToString(),              // 화주
                    this.CBH01_YNHWAMUL.GetValue().ToString(),             // 화물
                    this.TXT01_YNBLNO.GetValue().ToString(),               // BL번호
                    this.TXT01_YNMSNSEQ.GetValue().ToString(),             // MSN번호
                    this.TXT01_YNHSNSEQ.GetValue().ToString(),             // HSN번호
                    Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()),   // 통관일자
                    this.TXT01_YNCHASU.GetValue().ToString(),              // 통관차수
                    this.CBH01_YNACTHJ.GetValue().ToString(),              // 통관화주
                    this.CBH01_YNYDHWAJU.GetValue().ToString(),            // 양도화주
                    this.CBH01_YNYSHWAJU.GetValue().ToString(),            // 이전양도화주
                    Get_Date(this.TXT01_YNYSYDDATE.GetValue().ToString()), // 이전양수일자
                    this.TXT01_YNYSYDSEQ.GetValue().ToString(),            // 이전양도차수
                    this.TXT01_YNYSYSSEQ.GetValue().ToString()             // 이전양수순번
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_69KGT179");
                    this.CBH01_YNYSHWAJU.Focus();

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
            // 출고된 내역 있으면 삭제 불가
            if (decimal.Parse(Get_Numeric(this.TXT01_YNYSCHQTY.GetValue().ToString())) > 0)
            {
                this.ShowMessage("TY_M_UT_69KDH176");
                this.CBH01_YNYSHWAJU.Focus();

                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            // 출고가 되었으면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_699AD123",
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 양도화주
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양수화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString()             // 양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (double.Parse(Get_Numeric(dt.Rows[0]["YNYSCHQTY"].ToString())) > 0)
                {
                    this.ShowMessage("TY_M_UT_69KDH176");
                    this.CBH01_YNYSHWAJU.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 이후자료가 있으면 삭제 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_69M9W208",
                Get_Date(this.DTP01_YNIPHANG.GetValue().ToString()), // 입항일자
                this.CBH01_YNBONSUN.GetValue().ToString(),           // 본선
                this.CBH01_YNHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_YNHWAMUL.GetValue().ToString(),           // 화물
                this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
                this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
                this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
                Get_Date(this.DTP01_YNCUSTIL.GetValue().ToString()), // 통관일자
                this.TXT01_YNCHASU.GetValue().ToString(),            // 통관차수
                this.CBH01_YNACTHJ.GetValue().ToString(),            // 통관화주
                this.CBH01_YNYSHWAJU.GetValue().ToString(),          // 양도화주
                this.CBH01_YNYDHWAJU.GetValue().ToString(),          // 이전양도화주
                Get_Date(this.DTP01_YNYSDATE.GetValue().ToString()), // 이전양수일자
                this.TXT01_YNYDSEQ.GetValue().ToString(),            // 이전양도차수
                this.TXT01_YNYSSEQ.GetValue().ToString()             // 이전양수순번
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_69KGT179");
                this.CBH01_YNYSHWAJU.Focus();

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
        private void UP_FieldClear()
        {
            this.DTP01_YNIPHANG.SetValue("");  // 입항일자
            this.CBH01_YNBONSUN.SetValue("");  // 본선
            this.CBH01_YNHWAJU.SetValue("");   // 화주
            this.CBH01_YNHWAMUL.SetValue("");  // 화물
            this.TXT01_YNBLNO.SetValue("");    // BL번호
            this.TXT01_YNMSNSEQ.SetValue("");  // MSN번호
            this.TXT01_YNHSNSEQ.SetValue("");  // HSN번호
            this.DTP01_YNCUSTIL.SetValue("");  // 통관일자
            this.TXT01_YNCHASU.SetValue("");   // 통관차수
            this.CBH01_YNACTHJ.SetValue("");   // 통관화주
            this.CBH01_YNYDHWAJU.SetValue(""); // 양도화주
            this.CBH01_YNYSHWAJU.SetValue(""); // 양수화주
            this.DTP01_YNYSDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));  // 양수일자
            this.TXT01_YNYSSEQ.SetValue("");   // 양수순번
            this.TXT01_YNYDSEQ.SetValue("");   // 양도차수

            this.TXT01_CJCUQTY.SetValue("");
            this.TXT01_CJYSQTY.SetValue("");
            this.TXT01_CJYDQTY.SetValue("");
            this.TXT01_CJYSYDQTY.SetValue("");
            this.TXT01_CJYSCHQTY.SetValue("");
            this.TXT01_CJCHQTY.SetValue("");
            this.TXT01_CJJEQTY.SetValue("");

            this.TXT01_YNQTY.SetValue("");

            this.TXT01_YNYSCHQTY.SetValue("");
            //this.TXT01_YNJEQTY.SetValue("");
            this.TXT01_YNYSYDDATE.SetValue("");
            this.CBH01_YNYSYDHWAJU.SetValue("");
            this.TXT01_YNYSYSSEQ.SetValue("");
            this.TXT01_YNYSYDSEQ.SetValue("");
        }
        #endregion

        #region Description : 텍스트 ReadOnly
        private void UP_Field_ReadOnly(string sGUBUN)
        {
            if (sGUBUN == "INDEX")
            {
                this.CBH01_YNYSHWAJU.SetReadOnly(true);
                this.DTP01_YNYSDATE.SetReadOnly(true);
            }
            else
            {
                this.CBH01_YNYSHWAJU.SetReadOnly(false);
                this.DTP01_YNYSDATE.SetReadOnly(false);
            }
        }
        #endregion

        #region Description : 버튼 Visible
        private void UP_BUTTON_Visible(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                this.BTN61_UTTCODEHELP6.Visible = true;
            }
            else if (sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_UTTCODEHELP6.Visible = false;
            }
            else if (sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_UTTCODEHELP6.Visible = false;
            }
        }
        #endregion

        #region Description : 통관조회
        private void BTN61_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            TYUTGB007S popup = new TYUTGB007S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsCJYSHWAJU = "";
                fsCJYDHWAJU = "";
                fsCJYSDATE = "0";
                fsCJYSSEQ = "0";
                fsCJYDSEQ = "0";

                this.DTP01_YNIPHANG.SetValue(popup.fsIPHANG);   // 입항일자
                this.CBH01_YNBONSUN.SetValue(popup.fsBONSUN);   // 본선
                this.CBH01_YNHWAJU.SetValue(popup.fsHWAJU);     // 화주
                this.CBH01_YNHWAMUL.SetValue(popup.fsHWAMUL);   // 화물
                this.TXT01_YNBLNO.SetValue(popup.fsBLNO);       // BL번호
                this.TXT01_YNMSNSEQ.SetValue(popup.fsMSNSEQ);   // MSN번호
                this.TXT01_YNHSNSEQ.SetValue(popup.fsHSNSEQ);   // HSN번호
                this.DTP01_YNCUSTIL.SetValue(popup.fsCUSTIL);   // 통관일자
                this.TXT01_YNCHASU.SetValue(popup.fsCHASU);     // 통관차수
                this.CBH01_YNACTHJ.SetValue(popup.fsACTHJ);     // 통관화주

                // 이전화주에 대한 자료임
                fsCJYSHWAJU = popup.fsYSHWAJU;
                fsCJYDHWAJU = popup.fsYDHWAJU;
                fsCJYSDATE = popup.fsYSDATE;
                fsCJYSSEQ = popup.fsYSSEQ;
                fsCJYDSEQ = popup.fsYDSEQ;

                if (popup.fsYDHWAJU == "" && popup.fsYSHWAJU == "")
                {
                    this.CBH01_YNYDHWAJU.SetValue(popup.fsACTHJ); // 양도화주
                }
                else if (popup.fsYDHWAJU != "" && popup.fsYSHWAJU != "")
                {
                    this.CBH01_YNYDHWAJU.SetValue(popup.fsYSHWAJU); // 양도화주
                }

                this.TXT01_YNYDSEQ.SetValue(Convert.ToString(int.Parse(Get_Numeric(popup.fsYDSEQ)) + 1));     // 양도차수

                // 통관화주파일 조회
                UP_UTICUHJF_SEARCH();

                SetFocus(this.CBH01_YNYSHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 양수도관리 스프레드 이벤트
        private void FPS91_TY_S_UT_69KH8180_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_YNYDHWAJU.SetValue(this.FPS91_TY_S_UT_69KH8180.GetValue("YNYDHWAJU").ToString());
            this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_UT_69KH8180.GetValue("YNYSHWAJU").ToString());
            this.DTP01_YNYSDATE.SetValue(this.FPS91_TY_S_UT_69KH8180.GetValue("YNYSDATE").ToString());
            this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_UT_69KH8180.GetValue("YNYSSEQ").ToString());
            this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_UT_69KH8180.GetValue("YNYDSEQ").ToString());

            // 양수도 확인
            UP_UTIYANGF_RUN();
        }
        #endregion

        #region Description : 양수도관리 전체 스프레드 이벤트
        private void FPS91_TY_S_UT_69CFV141_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsYNYSHWAJU = "";
            fsYNYDHWAJU = "";
            fsYNYSDATE = "0";
            fsYNYDSEQ = "0";
            fsYNYSSEQ = "0";

            fsYNYSYDHWAJU = "";
            fsYNYSYDDATE = "0";
            fsYNYSYDSEQ = "0";
            fsYNYSYSSEQ = "0";

            this.DTP01_YNIPHANG.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNIPHANG").ToString());
            this.CBH01_YNBONSUN.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNBONSUN").ToString());
            this.CBH01_YNHWAJU.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNHWAJU").ToString());
            this.CBH01_YNHWAMUL.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNHWAMUL").ToString());
            this.TXT01_YNBLNO.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNBLNO").ToString());
            this.TXT01_YNMSNSEQ.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNMSNSEQ").ToString());
            this.TXT01_YNHSNSEQ.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNHSNSEQ").ToString());
            this.DTP01_YNCUSTIL.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNCUSTIL").ToString());
            this.TXT01_YNCHASU.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNCHASU").ToString());
            this.CBH01_YNACTHJ.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNACTHJ").ToString());


            this.CBH01_YNYSHWAJU.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSHWAJU").ToString());
            this.CBH01_YNYDHWAJU.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNYDHWAJU").ToString());
            this.DTP01_YNYSDATE.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSDATE").ToString());
            this.TXT01_YNYDSEQ.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNYDSEQ").ToString());
            this.TXT01_YNYSSEQ.SetValue(this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSSEQ").ToString());


            fsYNYSHWAJU = this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSHWAJU").ToString();
            fsYNYDHWAJU = this.FPS91_TY_S_UT_69CFV141.GetValue("YNYDHWAJU").ToString();
            fsYNYSDATE = this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSDATE").ToString();
            fsYNYDSEQ = this.FPS91_TY_S_UT_69CFV141.GetValue("YNYDSEQ").ToString();
            fsYNYSSEQ = this.FPS91_TY_S_UT_69CFV141.GetValue("YNYSSEQ").ToString();

            fsYNWNYSHWAJU = this.FPS91_TY_S_UT_69CFV141.GetValue("YNWNYSHWAJU").ToString();
            fsYNWNYDHWAJU = this.FPS91_TY_S_UT_69CFV141.GetValue("YNWNYDHWAJU").ToString();
            fsYNWNYSDATE = this.FPS91_TY_S_UT_69CFV141.GetValue("YNWNYSDATE").ToString();
            fsYNWNYDSEQ = this.FPS91_TY_S_UT_69CFV141.GetValue("YNWNYDSEQ").ToString();
            fsYNWNYSSEQ = this.FPS91_TY_S_UT_69CFV141.GetValue("YNWNYSSEQ").ToString();

            // 양수도관리 조회
            UP_UTIYANGF_SEARCH();
        }
        #endregion

        #region Description : 양수량 텍스트박스 이벤트
        private void TXT01_YNQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 화물 이벤트
        private void CBH01_SHWAMUL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion
    }
}