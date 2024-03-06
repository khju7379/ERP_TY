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
    public partial class TYUTIN015I : TYBase
    {
        string fsPOPUP = string.Empty;

        private string fsGUBUN = string.Empty;

        private string fsJDJIQTY = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN015I()
        {
            InitializeComponent();
        }

        private void TYUTIN015I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            this.SetStartingFocus(this.DTP01_STIPHANG);
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
            UP_Set_ReadOnly("NEW");

            UP_BUTTON_Visible("NEW");

            UP_FieldClear();

            fsGUBUN = "NEW";

            this.CBH01_JDHISAB.SetValue(TYUserInfo.EmpNo.ToString().Trim().ToUpper());

            SetFocus(this.TXT01_JDJGHWAJU);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sJCSEQ = string.Empty;

            DataTable dt = new DataTable();

            if (fsGUBUN == "NEW") // 저장
            {
                // 순번 가져오기
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6BBD7735",
                    Get_Date(this.DTP01_JDYYMM.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    sJCSEQ = "1";
                }
                else
                {
                    this.DbConnector.Attach
                       (
                       "TY_P_UT_6BBD1736",
                       Get_Date(this.DTP01_JDYYMM.GetValue().ToString())
                       );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sJCSEQ = dt.Rows[0]["JCSEQ"].ToString();
                    }
                }

                // 지시 순번
                this.TXT01_JDSEQ.SetValue(sJCSEQ.ToString());

                this.DbConnector.CommandClear();
                if (sJCSEQ == "1")
                {
                    // 출고지시번호 등록
                    this.DbConnector.Attach("TY_P_UT_6BBD4737", Get_Date(this.DTP01_JDYYMM.GetValue().ToString()),
                                                                sJCSEQ.ToString()
                                                                );
                }
                else
                {
                    // 출고지시번호 수정
                    this.DbConnector.Attach("TY_P_UT_6BBD4738", sJCSEQ.ToString(),
                                                                Get_Date(this.DTP01_JDYYMM.GetValue().ToString())
                                                                );
                }

                // DRUM 출고지시 등록
                this.DbConnector.Attach("TY_P_UT_6C9GM052",
                                        Get_Date(this.DTP01_JDYYMM.GetValue().ToString()),   // 지시일자
                                        this.TXT01_JDSEQ.GetValue().ToString(),              // 지시순번
                                        Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_JDBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_JDHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_JDHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_JDBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_JDMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_JDHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_JDCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_JDACTHJ.GetValue().ToString(),            // 통관화주
                                        this.TXT01_JDJGHWAJU.GetValue().ToString(),          // 재고화주
                                        this.CBH01_JDYSHWAJU.GetValue().ToString(),          // 양수화주
                                        this.CBH01_JDYDHWAJU.GetValue().ToString(),          // 양도화주
                                        Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_JDYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_JDYSSEQ.GetValue().ToString(),            // 양수순번
                                        this.TXT01_JDJUNG.GetValue().ToString(),             // 중량
                                        this.CBH01_JDCHHJ.GetValue().ToString(),             // 출고화주
                                        this.TXT01_JDTANKNO.GetValue().ToString(),           // 출고탱크
                                        this.TXT01_JDIPTANK.GetValue().ToString(),           // 입고탱크
                                        this.TXT01_JDIPQTY.GetValue().ToString(),            // 포장수량
                                        this.TXT01_JDCHQTY.GetValue().ToString(),            // 출고수량
                                        this.TXT01_JDJANQTY.GetValue().ToString(),           // 재고수량
                                        this.TXT01_JDJIQTY.GetValue().ToString(),            // 지시수량
                                        this.TXT01_JDJIJAN.GetValue().ToString(),            // 지시잔량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()         // 작성사번
                                        );

                this.DbConnector.ExecuteTranQueryList();

            }
            else // 수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6C9GM053",
                                        Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()), // 입항일자
                                        this.CBH01_JDBONSUN.GetValue().ToString(),           // 본선
                                        this.CBH01_JDHWAJU.GetValue().ToString(),            // 화주
                                        this.CBH01_JDHWAMUL.GetValue().ToString(),           // 화물
                                        this.TXT01_JDBLNO.GetValue().ToString(),             // BL번호
                                        this.TXT01_JDMSNSEQ.GetValue().ToString(),           // MSN번호
                                        this.TXT01_JDHSNSEQ.GetValue().ToString(),           // HSN번호
                                        Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()), // 통관일자
                                        this.TXT01_JDCHASU.GetValue().ToString(),            // 통관차수
                                        this.CBH01_JDACTHJ.GetValue().ToString(),            // 통관화주
                                        this.TXT01_JDJGHWAJU.GetValue().ToString(),          // 재고화주
                                        this.CBH01_JDYSHWAJU.GetValue().ToString(),          // 양수화주
                                        this.CBH01_JDYDHWAJU.GetValue().ToString(),          // 양도화주
                                        Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()), // 양수일자
                                        this.TXT01_JDYDSEQ.GetValue().ToString(),            // 양도차수
                                        this.TXT01_JDYSSEQ.GetValue().ToString(),            // 양수순번
                                        this.TXT01_JDJUNG.GetValue().ToString(),             // 중량
                                        this.CBH01_JDCHHJ.GetValue().ToString(),             // 출고화주
                                        this.TXT01_JDTANKNO.GetValue().ToString(),           // 출고탱크
                                        this.TXT01_JDIPTANK.GetValue().ToString(),           // 입고탱크
                                        this.TXT01_JDIPQTY.GetValue().ToString(),            // 포장수량
                                        this.TXT01_JDCHQTY.GetValue().ToString(),            // 출고수량
                                        this.TXT01_JDJANQTY.GetValue().ToString(),           // 재고수량
                                        this.TXT01_JDJIQTY.GetValue().ToString(),            // 지시수량
                                        this.TXT01_JDJIJAN.GetValue().ToString(),            // 지시잔량
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),        // 작성사번
                                        Get_Date(this.DTP01_JDYYMM.GetValue().ToString()),   // 입항일자
                                        this.TXT01_JDSEQ.GetValue().ToString()               // 순번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            UP_BUTTON_Visible("");

            SetFocus(this.TXT01_JDJGHWAJU);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            // 양수도 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6C9GN054",
                                    Get_Date(this.DTP01_JDYYMM.GetValue().ToString()), // 입항일자
                                    this.TXT01_JDSEQ.GetValue().ToString()             // 순번
                                    );

            this.DbConnector.ExecuteNonQuery();

            UP_BUTTON_Visible("");

            SetFocus(this.TXT01_JDJGHWAJU);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 통관화주파일 등록
        private void UP_UTICUHJF_INS()
        {


        }
        #endregion

        #region Description : 통관화주파일 삭제
        private void UP_UTICUHJF_DEL()
        {
        }
        #endregion

        #region Description : 조회 메소드
        private void UP_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6C9DW043",
                Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                Get_Date(this.DTP01_EDIPHANG.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6C9FQ046.SetValue(dt);
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            UP_Set_ReadOnly("OK");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6C9GH047",
                Get_Date(this.DTP01_JDYYMM.GetValue().ToString()), // 입항일자
                this.TXT01_JDSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsJDJIQTY = dt.Rows[0]["JDJIQTY"].ToString();

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);
            }
        }
        #endregion

        #region Description : 출고지시 - 지시총량 가져오기
        private void UP_Get_SumJiqty()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AJJS430",
                Get_Date(this.DTP01_JDYYMM.GetValue().ToString()), // 입항일자
                this.TXT01_JDSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

            }
        }
        #endregion

        #region Description : 통관화주파일 조회
        private void UP_UTICUHJF_SEARCH()
        {
            //if (fsCJYDHWAJU.ToString() == "" && fsCJYSDATE.ToString() == "0" && fsCJYDSEQ.ToString() == "0" && fsCJYSSEQ.ToString() == "0")
            //{
            //    fsCJYSHWAJU = "";
            //}
            //else
            //{
            //    fsCJYSHWAJU = this.CBH01_JDYDHWAJU.GetValue().ToString();
            //}

            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_69JGC164",
            //    this.CBH01_JDACTHJ.GetValue().ToString(),            // 통관화주
            //    Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()), // 입항일자
            //    this.CBH01_JDBONSUN.GetValue().ToString(),           // 본선
            //    this.CBH01_JDHWAJU.GetValue().ToString(),            // 화주
            //    this.CBH01_JDHWAMUL.GetValue().ToString(),           // 화물
            //    this.TXT01_YNBLNO.GetValue().ToString(),             // BL번호
            //    this.TXT01_YNMSNSEQ.GetValue().ToString(),           // MSN번호
            //    this.TXT01_YNHSNSEQ.GetValue().ToString(),           // HSN번호
            //    Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()), // 통관일자
            //    this.TXT01_JDCHASU.GetValue().ToString(),            // 통관차수
            //    fsCJYSHWAJU.ToString(),                              // 양수화주
            //    fsCJYDHWAJU.ToString(),                              // 양도화주
            //    fsCJYSDATE.ToString(),                               // 양수일자
            //    fsCJYDSEQ.ToString(),                                // 양도차수
            //    fsCJYSSEQ.ToString()                                 // 양수순번
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    // 통관량
            //    this.TXT01_CJCUQTY.SetValue(dt.Rows[0]["CJCUQTY"].ToString());
            //    // 양수량
            //    this.TXT01_CJYSQTY.SetValue(dt.Rows[0]["CJYSQTY"].ToString());
            //    // 양도량
            //    this.TXT01_CJYDQTY.SetValue(dt.Rows[0]["CJYDQTY"].ToString());
            //    // 양수양도량
            //    this.TXT01_CJYSYDQTY.SetValue(dt.Rows[0]["CJYSYDQTY"].ToString());
            //    // 양수출고량
            //    this.TXT01_CJYSCHQTY.SetValue(dt.Rows[0]["CJYSCHQTY"].ToString());
            //    // 출고량
            //    this.TXT01_CJCHQTY.SetValue(dt.Rows[0]["CJCHQTY"].ToString());
            //    // 재고량
            //    this.TXT01_CJJEQTY.SetValue(dt.Rows[0]["CJJEQTY"].ToString());
            //}
        }
        #endregion

        #region Description : 재고 조회(통관 및 양수도)
        private void UP_CALL_JEGO()
        {
            TYUTGB015S popup = new TYUTGB015S(this.TXT01_JDJGHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JDJGHWAJU.SetValue(popup.fsJGHWAJU);     // 재고화주
                this.TXT01_JDJGHWAJUNM.SetValue(popup.fsJGHWAJUNM); // 재고화주명
                this.DTP01_JDIPHANG.SetValue(popup.fsIPHANG);       // 입항일자
                this.CBH01_JDBONSUN.SetValue(popup.fsBONSUN);       // 본선
                this.CBH01_JDHWAJU.SetValue(popup.fsHWAJU);         // 화주
                this.CBH01_JDHWAMUL.SetValue(popup.fsHWAMUL);       // 화물
                this.TXT01_JDBLNO.SetValue(popup.fsBLNO);           // BL번호
                this.TXT01_JDMSNSEQ.SetValue(popup.fsMSNSEQ);       // MSN번호
                this.TXT01_JDHSNSEQ.SetValue(popup.fsHSNSEQ);       // HSN번호
                this.DTP01_JDCUSTIL.SetValue(popup.fsCUSTIL);       // 통관일자
                this.TXT01_JDCHASU.SetValue(popup.fsCHASU);         // 통관차수
                this.CBH01_JDACTHJ.SetValue(popup.fsACTHJ);         // 통관화주
                this.CBH01_JDYSHWAJU.SetValue(popup.fsYSHWAJU);     // 양수화주
                this.CBH01_JDYDHWAJU.SetValue(popup.fsYDHWAJU);     // 양도화주
                this.TXT01_JDYSDATE.SetValue(popup.fsYSDATE);       // 양수일자
                this.TXT01_JDYDSEQ.SetValue(popup.fsYDSEQ);         // 양도순번
                this.TXT01_JDYSSEQ.SetValue(popup.fsYSSEQ);         // 양수순번

                this.TXT01_CJCUQTY.SetValue(popup.fsCJCUQTY);       // 통관수량
                this.TXT01_CJCHQTY.SetValue(popup.fsCJCHQTY);       // 출고수량
                this.TXT01_CJJEQTY.SetValue(popup.fsCJJEQTY);       // 재고수량

                this.TXT01_JDJUNG.SetValue(popup.fsJUNG);           // 중량

                this.TXT01_JDIPQTY.SetValue(popup.fsDJPOQTY);       // 포장수량
                this.TXT01_JDCHQTY.SetValue(popup.fsDJCHQTY);       // 출고수량
                this.TXT01_JDJANQTY.SetValue(popup.fsDJJEQTY);      // 포장재고

                this.TXT01_JDTANKNO.SetValue(popup.fsCHTANK);       // 출고탱크

                SetFocus(this.TXT01_JDJGHWAJU);
            }
        }
        #endregion

        #region Description : 양수도관리 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            // 재고 화주 존재 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_66FAH184",
                "HJ",
                this.TXT01_JDJGHWAJU.GetValue().ToString().ToUpper(),
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6C9GW055");
                this.TXT01_JDJGHWAJU.Focus();

                e.Successed = false;
                return;
            }

            // 입고탱크 체크
            if (this.TXT01_JDIPTANK.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AKBS435",
                    this.TXT01_JDIPTANK.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6AKBV437");
                    this.TXT01_JDIPTANK.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // B/L별 입고 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKBZ440",
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JDMSNSEQ.GetValue().ToString(),
                this.TXT01_JDHSNSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKBZ441");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }

            // SURVEY파일 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN442",
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCN443");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }

            // 매출입고 할증 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKCN444",
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDIPTANK.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKCO445");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }

            // 통관화주 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AKDD446",
                this.CBH01_JDACTHJ.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JDMSNSEQ.GetValue().ToString(),
                this.TXT01_JDHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()),
                this.TXT01_JDCHASU.GetValue().ToString(),
                this.TXT01_JDJGHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDYSHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDYDHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()),
                this.TXT01_JDYDSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_JDYSSEQ.GetValue().ToString().ToUpper()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6AKDD447");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }

            // 양수도일 경우 체크
            if (this.CBH01_JDYDHWAJU.GetValue().ToString() != "" && this.CBH01_JDYSHWAJU.GetValue().ToString() != "" && Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()) != "0" &&
                this.TXT01_JDYDSEQ.GetValue().ToString() != "0" && this.TXT01_JDYSSEQ.GetValue().ToString() != "0")
            {
                // 양수도 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_699AD123",
                    Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                    this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                    this.TXT01_JDMSNSEQ.GetValue().ToString(),
                    this.TXT01_JDHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()),
                    this.TXT01_JDCHASU.GetValue().ToString(),
                    this.CBH01_JDACTHJ.GetValue().ToString().ToUpper(),
                    this.CBH01_JDYDHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JDYSHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()),
                    this.TXT01_JDYDSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_JDYSSEQ.GetValue().ToString().ToUpper()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_6C9H8056");
                    SetFocus(this.TXT01_JDJGHWAJU);

                    e.Successed = false;
                    return;
                }
            }

            // 통관화일 체크			
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAGW725",
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JDMSNSEQ.GetValue().ToString(),
                this.TXT01_JDHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()),
                this.TXT01_JDCHASU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6BAGY726");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }

            // DRUM포장개수 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_7CKBT340",
                this.CBH01_JDACTHJ.GetValue().ToString().ToUpper(),
                Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                this.TXT01_JDMSNSEQ.GetValue().ToString(),
                this.TXT01_JDHSNSEQ.GetValue().ToString(),
                Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()),
                this.TXT01_JDCHASU.GetValue().ToString(),
                this.TXT01_JDJGHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDYSHWAJU.GetValue().ToString().ToUpper(),
                this.CBH01_JDYDHWAJU.GetValue().ToString().ToUpper(),
                Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()),
                this.TXT01_JDYDSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_JDYSSEQ.GetValue().ToString().ToUpper(),
                this.TXT01_JDJUNG.GetValue().ToString(),
                this.TXT01_JDTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_7CKBV341");
                SetFocus(this.TXT01_JDJGHWAJU);

                e.Successed = false;
                return;
            }
            else
            {
                double dDPDRQTY = 0;
                double dJDJIQTY = 0;

                dDPDRQTY = double.Parse(String.Format("{0,9:N0}", Get_Numeric(dt.Rows[0]["DPDRQTY"].ToString())));

                // DRUM포장개수 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_7CKBZ342",
                    Get_Date(this.DTP01_JDIPHANG.GetValue().ToString()),
                    this.CBH01_JDBONSUN.GetValue().ToString().ToUpper(),
                    this.CBH01_JDHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JDHWAMUL.GetValue().ToString().ToUpper(),
                    this.TXT01_JDBLNO.GetValue().ToString().ToUpper(),
                    this.TXT01_JDMSNSEQ.GetValue().ToString(),
                    this.TXT01_JDHSNSEQ.GetValue().ToString(),
                    Get_Date(this.DTP01_JDCUSTIL.GetValue().ToString()),
                    this.TXT01_JDCHASU.GetValue().ToString(),
                    this.CBH01_JDACTHJ.GetValue().ToString().ToUpper(),
                    this.TXT01_JDJGHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JDYSHWAJU.GetValue().ToString().ToUpper(),
                    this.CBH01_JDYDHWAJU.GetValue().ToString().ToUpper(),
                    Get_Date(this.TXT01_JDYSDATE.GetValue().ToString()),
                    this.TXT01_JDYDSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_JDYSSEQ.GetValue().ToString().ToUpper(),
                    this.TXT01_JDJUNG.GetValue().ToString(),
                    this.TXT01_JDTANKNO.GetValue().ToString().Trim()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dJDJIQTY = double.Parse(String.Format("{0,9:N0}", Get_Numeric(dt.Rows[0]["JDJIQTY"].ToString())));
                }

                if (dDPDRQTY < dJDJIQTY + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.TXT01_JDJIQTY.GetValue().ToString()))))
                {
                    this.ShowMessage("TY_M_UT_7CKC3343");
                    SetFocus(this.TXT01_JDJIQTY);

                    e.Successed = false;
                    return;
                }
            }

            if (Int32.Parse(Get_Numeric(this.TXT01_JDJIQTY.GetValue().ToString())) > Int32.Parse(Get_Numeric(this.TXT01_JDJANQTY.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_UT_6C9H3057");
                SetFocus(this.TXT01_JDJIQTY);

                e.Successed = false;
                return;
            }


            if (fsGUBUN.ToString() == "NEW")
            {
                this.TXT01_JDJIJAN.SetValue(this.TXT01_JDJIQTY.GetValue().ToString());
            }

            if (fsGUBUN.ToString() == "UPT")
            {
                string sQTY = string.Empty;

                if (Int32.Parse(Get_Numeric(fsJDJIQTY.ToString())) > Int32.Parse(Get_Numeric(this.TXT01_JDJIQTY.GetValue().ToString())))
                {
                    sQTY = Convert.ToString(Int32.Parse(fsJDJIQTY.ToString()) - Int32.Parse(Get_Numeric(this.TXT01_JDJIQTY.GetValue().ToString())));
                    this.TXT01_JDJIJAN.SetValue(Convert.ToString(Int32.Parse(Get_Numeric(this.TXT01_JDJIJAN.GetValue().ToString())) - Int32.Parse(sQTY.ToString())));
                }
                else
                {
                    sQTY = Convert.ToString(Int32.Parse(Get_Numeric(this.TXT01_JDJIQTY.GetValue().ToString())) - Int32.Parse(fsJDJIQTY.ToString()));
                    this.TXT01_JDJIJAN.SetValue(Convert.ToString(Int32.Parse(Get_Numeric(this.TXT01_JDJIJAN.GetValue().ToString())) + Int32.Parse(sQTY.ToString())));
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
            this.TXT01_JDSEQ.SetValue("");       // 지시순번

            this.DTP01_JDIPHANG.SetValue("");    // 입항일자
            this.CBH01_JDBONSUN.SetValue("");    // 본선
            this.CBH01_JDHWAJU.SetValue("");     // 화주
            this.CBH01_JDHWAMUL.SetValue("");    // 화물
            this.TXT01_JDBLNO.SetValue("");      // BL번호
            this.TXT01_JDMSNSEQ.SetValue("");    // MSN번호
            this.TXT01_JDHSNSEQ.SetValue("");    // HSN번호
            this.DTP01_JDCUSTIL.SetValue("");    // 통관일자
            this.TXT01_JDCHASU.SetValue("");     // 통관차수
            this.TXT01_JDJGHWAJU.SetValue("");   // 재고화주
            this.TXT01_JDJGHWAJUNM.SetValue(""); // 재고화주명
            this.CBH01_JDACTHJ.SetValue("");     // 통관화주
            this.CBH01_JDYDHWAJU.SetValue("");   // 양도화주
            this.CBH01_JDYSHWAJU.SetValue("");   // 양수화주
            this.TXT01_JDYSDATE.SetValue("0");   // 양수일자
            this.TXT01_JDYSSEQ.SetValue("");     // 양수순번
            this.TXT01_JDYDSEQ.SetValue("");     // 양도차수



            this.TXT01_CJCUQTY.SetValue("");
            this.TXT01_CJCHQTY.SetValue("");
            this.TXT01_CJJEQTY.SetValue("");
            this.TXT01_JDTANKNO.SetValue("");
            this.TXT01_JDIPTANK.SetValue("");
            this.CBH01_JDHISAB.SetValue("");
            this.TXT01_JDJIQTY.SetValue("");


            this.TXT01_JDJUNG.SetValue("");
            this.TXT01_JDIPQTY.SetValue("");
            this.TXT01_JDCHQTY.SetValue("");
            this.TXT01_JDJANQTY.SetValue("");
            this.TXT01_JDTANKNO.SetValue("");
            this.CBH01_JDCHHJ.SetValue("");
            this.CBH01_JDHISAB.SetValue("");
            this.TXT01_JDJIQTY.SetValue("");
            this.TXT01_JDJIJAN.SetValue("");
            this.TXT01_JDIPTANK.SetValue("");
        }
        #endregion

        #region Description : 텍스트 ReadOnly
        private void UP_Set_ReadOnly(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DTP01_JDYYMM.SetReadOnly(false);
                this.TXT01_JDSEQ.SetReadOnly(false);
            }
            else
            {
                this.DTP01_JDYYMM.SetReadOnly(true);
                this.TXT01_JDSEQ.SetReadOnly(true);
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
            }
            else if (sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else if (sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_6C9FQ046_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_JDYYMM.SetValue(this.FPS91_TY_S_UT_6C9FQ046.GetValue("JDYYMM").ToString());
            this.TXT01_JDSEQ.SetValue(this.FPS91_TY_S_UT_6C9FQ046.GetValue("JDSEQ").ToString());

            // 확인
            UP_RUN();
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

        #region Description : 재고화주 이벤트
        private void BTN61_JGHWAJU_Click(object sender, EventArgs e)
        {
            UP_CALL_JEGO();

            // 입고탱크
            UP_GET_JDIPTANK();

            SetStartingFocus(this.CBH01_JDCHHJ.CodeText);
        }

        private void TXT01_JDJGHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                UP_CALL_JEGO();

                // 입고탱크
                UP_GET_JDIPTANK();

                SetStartingFocus(this.CBH01_JDCHHJ.CodeText);
            }
        }
        #endregion

        #region Description : 출고탱크 이벤트
        private void BTN61_CHTANK_Click(object sender, EventArgs e)
        {
            TYUTGB009S popup = new TYUTGB009S(this.DTP01_JDIPHANG.GetValue().ToString(), this.CBH01_JDBONSUN.GetValue().ToString(),
                                              this.CBH01_JDHWAJU.GetValue().ToString(), this.CBH01_JDHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JDTANKNO.SetValue(popup.fsTANKNO); // 출고탱크
                SetFocus(this.TXT01_JDIPTANK);
            }
        }

        private void TXT01_JDTANKNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB009S popup = new TYUTGB009S(this.DTP01_JDIPHANG.GetValue().ToString(), this.CBH01_JDBONSUN.GetValue().ToString(),
                                              this.CBH01_JDHWAJU.GetValue().ToString(), this.CBH01_JDHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_JDTANKNO.SetValue(popup.fsTANKNO); // 출고탱크
                    SetFocus(this.TXT01_JDIPTANK);
                }
            }
        }
        #endregion

        #region Description : 입고탱크 이벤트
        private void BTN61_IPTANK_Click(object sender, EventArgs e)
        {
            TYUTGB010S popup = new TYUTGB010S(this.DTP01_JDIPHANG.GetValue().ToString(), this.CBH01_JDBONSUN.GetValue().ToString(),
                                              this.CBH01_JDHWAJU.GetValue().ToString(), this.CBH01_JDHWAMUL.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_JDIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                SetFocus(this.CBH01_JDHISAB.CodeText);
            }
        }

        private void TXT01_JDIPTANK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUTGB010S popup = new TYUTGB010S(this.DTP01_JDIPHANG.GetValue().ToString(), this.CBH01_JDBONSUN.GetValue().ToString(),
                                              this.CBH01_JDHWAJU.GetValue().ToString(), this.CBH01_JDHWAMUL.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_JDIPTANK.SetValue(popup.fsTANKNO); // 출고탱크
                    SetFocus(this.CBH01_JDHISAB.CodeText);
                }
            }
        }
        #endregion

        #region Description : 입고탱크 가져오기
        private void UP_GET_JDIPTANK()
        {
            this.TXT01_JDIPTANK.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6BAF3716",
                this.DTP01_JDIPHANG.GetValue().ToString(),       // 입항일자
                this.CBH01_JDBONSUN.GetValue().ToString(),       // 본선
                this.CBH01_JDHWAJU.GetValue().ToString(),        // 화주
                this.CBH01_JDHWAMUL.GetValue().ToString()        // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.TXT01_JDIPTANK.SetValue(dt.Rows[0]["COTANKNO"].ToString());
                }
            }
        }
        #endregion

        #region Description : 지시수량 이벤트
        private void TXT01_JDJIQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Description : 조회 종료일자 이벤트
        private void DTP01_EDIPHANG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }

        private void DTP01_EDIPHANG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_INQ.Visible == true)
                {
                    SetFocus(this.BTN61_INQ);
                }
            }
        }
        #endregion
    }
}