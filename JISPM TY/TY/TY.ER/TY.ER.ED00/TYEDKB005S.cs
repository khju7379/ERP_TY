using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출승인정보 수신/변환 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.04.05 14:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_74690215 : 반출승인정보내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_74691216 : 반출승인정보내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EDIGJ : 공장
    /// </summary>
    public partial class TYEDKB005S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB005S()
        {
            InitializeComponent();
        }

        private void TYEDKB005S_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_SetLockCheck();

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 수신/변환 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYEDKB005I()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_74691216.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74690215", this.CBO01_EDIGJ.GetValue().ToString());
            this.FPS91_TY_S_UT_74691216.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : 통관등록 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sIBHANGCHA = string.Empty;
            string sIBGOKJONG = string.Empty;
            string sIBHWAJU = string.Empty;
            string sIBBLNO = string.Empty;
            string sIBBLMSN = string.Empty;
            string sIBBLHSN = string.Empty;
            string sIBHMNO1 = string.Empty;
            string sIBHMNO2 = string.Empty;
            string sChasu   = string.Empty;
            string sCSSINNM = string.Empty;

            string sJBSOSOK   = string.Empty;
            string sJBJESTDAT = string.Empty;
            string sJBCONTNO  = string.Empty;
            string sJBGOKJONG = string.Empty;
            string sJCWONSAN = string.Empty;

            double dJGCSQTY = 0;
            double dJGCSJANQTY = 0;
            double dJGHWAKQTY = 0;
            double dJBHWAKQTY = 0;
            double dJBCSQTY = 0;
            double dJBCSJANQTY = 0;
            double dWK_QTY = 0;
            double dBL_QTY = 0;
            int iCnt = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (CBO01_EDIGJ.GetValue().ToString() == "T")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {                        
                        //신고처리 된건인지 확인
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_75GAM503", ds.Tables[0].Rows[i]["BGM0101"].ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt > 0)
                        {
                            this.ShowCustomMessage("통관번호:" + ds.Tables[0].Rows[i]["BGM0101"].ToString() + " 은 이미 통관처리 되었습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }

                        //통관 차수 가져오기
                        sChasu = UP_Get_Chasu(ds.Tables[0].Rows[i]["IPIPHANG"].ToString(),
                                              ds.Tables[0].Rows[i]["IPBONSUN"].ToString(),
                                              ds.Tables[0].Rows[i]["IPHWAJU"].ToString(),
                                              ds.Tables[0].Rows[i]["IPHWAMUL"].ToString(),
                                              ds.Tables[0].Rows[i]["IPBLNO"].ToString(),
                                              ds.Tables[0].Rows[i]["DTM0201"].ToString(),
                                              ds.Tables[0].Rows[i]["IPMSNSEQ"].ToString(),
                                              ds.Tables[0].Rows[i]["IPHSNSEQ"].ToString()
                                              );

                        //통관관리 등록
                        this.DbConnector.Attach("TY_P_UT_75GBA510", ds.Tables[0].Rows[i]["IPIPHANG"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPBONSUN"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHWAJU"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHWAMUL"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPBLNO"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPMSNSEQ"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHSNSEQ"].ToString(),
                                                                      ds.Tables[0].Rows[i]["DTM0201"].ToString(),
                                                                      sChasu,
                                                                      ds.Tables[0].Rows[i]["CUQTY"].ToString(),
                                                                      "0",
                                                                      "0",
                                                                      ds.Tables[0].Rows[i]["MOA0201"].ToString(),
                                                                      ds.Tables[0].Rows[i]["BGM0101"].ToString(),
                                                                      ds.Tables[0].Rows[i]["CNT0201"].ToString(),
                                                                      ds.Tables[0].Rows[i]["KSCODE"].ToString(),
                                                                      ds.Tables[0].Rows[i]["TONGHWAJU"].ToString(),
                                                                      "50",
                                                                      "0",
                                                                      "0",
                                                                      "0",
                                                                      "0",
                                                                      TYUserInfo.EmpNo
                                                                    );
                        //BL입고관리 통관량 UPDATE
                        this.DbConnector.Attach("TY_P_UT_75GDO516", ds.Tables[0].Rows[i]["CUQTY"].ToString(),
                                                                     ds.Tables[0].Rows[i]["TONGHWAJU"].ToString(),
                                                                      TYUserInfo.EmpNo,
                                                                      ds.Tables[0].Rows[i]["IPIPHANG"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPBONSUN"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHWAJU"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHWAMUL"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPBLNO"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPMSNSEQ"].ToString(),
                                                                      ds.Tables[0].Rows[i]["IPHSNSEQ"].ToString()
                                                                    );

                        //통관화주 파일 
                        this.DbConnector.Attach("TY_P_UT_689CM001",
                                                                     ds.Tables[0].Rows[i]["TONGHWAJU"].ToString(),
                                                                     ds.Tables[0].Rows[i]["TONGHWAJU"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPIPHANG"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPBONSUN"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPHWAJU"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPHWAMUL"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPBLNO"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPMSNSEQ"].ToString(),
                                                                     ds.Tables[0].Rows[i]["IPHSNSEQ"].ToString(),
                                                                     ds.Tables[0].Rows[i]["DTM0201"].ToString(),
                                                                     sChasu,
                                                                     "",
                                                                     "",
                                                                     "0",
                                                                     "0",
                                                                     "0",
                                                                     "0",
                                                                     "0",
                                                                     "0",
                                                                     "0",
                                                                     ds.Tables[0].Rows[i]["CUQTY"].ToString(),
                                                                     "0",
                                                                     ds.Tables[0].Rows[i]["CUQTY"].ToString(),
                                                                     TYUserInfo.EmpNo
                                                                   );

                        //반출승인내역 처리상황 표시
                        this.DbConnector.Attach("TY_P_UT_75GES518", "/",
                                                                     TYUserInfo.EmpNo,
                                                                     ds.Tables[0].Rows[i]["BGM0101"].ToString()
                                                                   );
                    }

                    if (this.DbConnector.CommandCount > 0)
                        this.DbConnector.ExecuteTranQueryList();
                }
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        if (Convert.ToInt16(Get_Numeric(ds.Tables[0].Rows[i]["DOC0202"].ToString())) <= 0)
                        {
                            this.DbConnector.Attach("TY_P_US_89A90702", ds.Tables[0].Rows[i]["EDIHMNO1"].ToString(),
                                                                        ds.Tables[0].Rows[i]["EDIHMNO2"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0101"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0201"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0202"].ToString()
                                                                       );
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_US_89A91703", ds.Tables[0].Rows[i]["EDIHMNO1"].ToString(),
                                                                        ds.Tables[0].Rows[i]["EDIHMNO2"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0101"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0201"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0202"].ToString()
                                                                       );
                        }

                        DataTable dk = this.DbConnector.ExecuteDataTable();

                        if (dk.Rows.Count > 0)
                        {
                            sIBHANGCHA = dk.Rows[0]["IBHANGCHA"].ToString();
                            sIBGOKJONG = dk.Rows[0]["IBGOKJONG"].ToString();
                            sIBHWAJU = dk.Rows[0]["IBHWAJU"].ToString();
                            sIBBLNO = dk.Rows[0]["IBBLNO"].ToString();
                            sIBBLMSN = dk.Rows[0]["IBBLMSN"].ToString();
                            sIBBLHSN = dk.Rows[0]["IBBLHSN"].ToString();
                            sIBHMNO1 = dk.Rows[0]["IBHMNO1"].ToString();
                            sIBHMNO2 = dk.Rows[0]["IBHMNO2"].ToString();
                        }

                        //통관차수                         
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89ABN708", sIBHANGCHA, sIBGOKJONG, sIBHWAJU, sIBBLNO, sIBBLMSN, sIBBLHSN);
                        DataTable dtchasu = this.DbConnector.ExecuteDataTable();
                        if (dtchasu.Rows.Count > 0)
                        {
                            sChasu = dtchasu.Rows[0][0].ToString();
                        }
                        
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AAY705", sIBHANGCHA,sIBGOKJONG,sIBHWAJU,sIBBLNO,sIBBLMSN,sIBBLHSN,sIBHMNO1,sIBHMNO2);
                        DataTable dtjebl = this.DbConnector.ExecuteDataTable();
                        if (dtjebl.Rows.Count > 0)
                        {                       
                             dJBHWAKQTY = Convert.ToDouble(dtjebl.Rows[0]["JBHWAKQTY"].ToString());
                             dJBCSQTY = Convert.ToDouble(dtjebl.Rows[0]["JBCSQTY"].ToString());
                             dJBCSJANQTY = Convert.ToDouble(dtjebl.Rows[0]["JBCSJANQTY"].ToString());
                             sJBSOSOK = dtjebl.Rows[0]["JBSOSOK"].ToString();
                             sJBJESTDAT = dtjebl.Rows[0]["JBJESTDAT"].ToString();
                             sJBCONTNO = dtjebl.Rows[0]["JBCONTNO"].ToString();
                             sJBGOKJONG = dtjebl.Rows[0]["JBGOKJONG"].ToString();
                             sJCWONSAN = dtjebl.Rows[0]["IBWONSAN"].ToString();
                        }
                        
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AB2706", sIBHANGCHA, sIBGOKJONG, sIBHWAJU);
                        DataTable dtjego = this.DbConnector.ExecuteDataTable();
                        if (dtjego.Rows.Count > 0)
                        {
                            dJGCSQTY = Convert.ToDouble(dtjego.Rows[0]["JGCSQTY"].ToString());
                            dJGCSJANQTY = Convert.ToDouble(dtjego.Rows[0]["JGCSJANQTY"].ToString());
                            dJGHWAKQTY = Convert.ToDouble(dtjego.Rows[0]["JGHWAKQTY"].ToString());
                        }

                        //승인중량
                        dWK_QTY = Convert.ToDouble(ds.Tables[0].Rows[i]["CNT0201"].ToString());
                        dBL_QTY = dWK_QTY;

                        if( ( dJBHWAKQTY - (  dJBCSQTY + dWK_QTY)) < 0 )
                        {
                            dWK_QTY = Convert.ToDouble(string.Format("{0:#,##0.000}", dWK_QTY + (dJBHWAKQTY - (dJBCSQTY + dWK_QTY))));                            
                        }

                        dJBCSQTY = Convert.ToDouble(string.Format("{0:#,##0.000}",dJBCSQTY + dWK_QTY));                        

                        dJBCSJANQTY = Convert.ToDouble(string.Format("{0:#,##0.000}",dJBCSJANQTY)) - Convert.ToDouble(string.Format("{0:#,##0.000}",dWK_QTY));

                        dJGCSQTY = Convert.ToDouble(string.Format("{0:#,##0.000}", dJGCSQTY + dWK_QTY)); 

                        dJGCSJANQTY = Convert.ToDouble(string.Format("{0:#,##0.000}",dJGCSJANQTY)) - Convert.ToDouble(string.Format("{0:#,##0.000}",dWK_QTY));

                        if( ( dJBCSJANQTY < 0 ) || ( dJGCSJANQTY < 0) )
                        {
                            this.ShowCustomMessage("총통관량이 확정량을 초과하였습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);                            
                            return;
                        }

                        //관세사 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AB7707", ds.Tables[0].Rows[i]["BGM0101"].ToString().Substring(0,5));
                        DataTable dtCSS = this.DbConnector.ExecuteDataTable();
                        if (dtCSS.Rows.Count > 0)
                        {
                            sCSSINNM = dtCSS.Rows[0]["CDCODE"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        //USICUSTF
                        this.DbConnector.Attach("TY_P_US_89AD1713", sIBHANGCHA,
                                                                    sIBGOKJONG,
                                                                    sIBHWAJU,
                                                                    sIBBLNO,
                                                                    sIBBLMSN,
                                                                    sIBBLHSN,
                                                                    ds.Tables[0].Rows[i]["DTM0201"].ToString(),  //통관일자
                                                                    sChasu,  //차수
                                                                    dWK_QTY,
                                                                    dWK_QTY,
                                                                    sCSSINNM,   //관세차코드
                                                                    ds.Tables[0].Rows[i]["MOA0201"].ToString(),  //감정가
                                                                    ds.Tables[0].Rows[i]["BGM0101"].ToString(),  //신고번호
                                                                    dBL_QTY, "50", sIBHMNO1, sIBHMNO2,
                                                                    TYUserInfo.EmpNo
                                                                   );
                        //USIJECSNF
                        this.DbConnector.Attach("TY_P_US_89AD3714", sIBHANGCHA,
                                                                    sIBGOKJONG,
                                                                    sIBHWAJU,
                                                                    sIBBLNO,
                                                                    sIBBLMSN,
                                                                    sIBBLHSN,
                                                                    ds.Tables[0].Rows[i]["DTM0201"].ToString(),  //통관일자
                                                                    sChasu,  //차수
                                                                    dWK_QTY,
                                                                    dWK_QTY,
                                                                    "99",
                                                                    sIBHMNO1,
                                                                    sIBHMNO2,
                                                                    sJBSOSOK,
                                                                    sJBJESTDAT,
                                                                    sJBCONTNO,
                                                                    sIBHWAJU,
                                                                    sJCWONSAN,
                                                                    TYUserInfo.EmpNo
                                                                   );

                        //USIJEBLF
                        this.DbConnector.Attach("TY_P_US_89AD2715",
                                                                  dJBCSQTY, dJBCSJANQTY,
                                                                  TYUserInfo.EmpNo,
                                                                  sIBHANGCHA,
                                                                  sIBGOKJONG,
                                                                  sIBHWAJU,
                                                                  sIBBLNO,
                                                                  sIBBLMSN,
                                                                  sIBBLHSN,
                                                                  sIBHMNO1, sIBHMNO2
                                                                 );
                        //USIJEGOF
                        this.DbConnector.Attach("TY_P_US_89ADA716",
                                                                 dJGCSQTY, dJGCSJANQTY,
                                                                 TYUserInfo.EmpNo,
                                                                 sIBHANGCHA,
                                                                 sIBGOKJONG,
                                                                 sIBHWAJU
                                                                );

                        //반출승인내역 처리상황 표시
                        this.DbConnector.Attach("TY_P_UT_75GES518", "/",
                                                                     TYUserInfo.EmpNo,
                                                                     ds.Tables[0].Rows[i]["BGM0101"].ToString()
                                                                   );

                        if (this.DbConnector.CommandCount > 0)
                            this.DbConnector.ExecuteNonQueryList();
                    }
                }
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_UT_75GAF500");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            string    sIBHANGCHA = string.Empty;
            string    sIBGOKJONG = string.Empty;
            string    sIBHWAJU = string.Empty;
            string    sIBBLNO = string.Empty;
            string    sIBBLMSN = string.Empty;
            string    sIBBLHSN = string.Empty;
            string    sIBHMNO1 = string.Empty;
            string sIBHMNO2 = string.Empty;

            string sCHASU = string.Empty;

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_74691216.GetDataSourceInclude(TSpread.TActionType.Select, "EDIHMNO1", "EDIHMNO2", "BGM0101", "DTM0201", "NAD0101", "CNT0201", "MOA0201", "IPIPHANG", "IPBONSUN", "IPHWAJU", "IPHWAMUL", "IPBLNO", "IPMSNSEQ", "IPHSNSEQ", "KSCODE", "CUQTY", "TONGHWAJU", "DOC0101","DOC0201","DOC0202"));

            if (ds.Tables[0].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (CBO01_EDIGJ.GetValue().ToString() == "T")
            {
                //UTT
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //BL입고관리 자료 존재 체크
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_75GAJ501", ds.Tables[0].Rows[i]["EDIHMNO1"].ToString(),
                                                                    ds.Tables[0].Rows[i]["EDIHMNO2"].ToString()
                                                                   );
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowMessage("TY_M_UT_75GAJ502");
                            e.Successed = false;
                            return;
                        }
                        else
                        {
                            //화물번호 비교
                            if ((ds.Tables[0].Rows[i]["DOC0101"].ToString().Trim() != dt.Rows[0]["VSJUKHA"].ToString().Trim()) &&
                                  (ds.Tables[0].Rows[i]["DOC0201"].ToString().Trim() != Set_Fill4(dt.Rows[0]["IPMSNSEQ"].ToString().Trim()))
                                )
                            {
                                this.ShowCustomMessage("관리번호를 확인하세요! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }

                        //신고처리 된건인지 확인
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_75GAM503", ds.Tables[0].Rows[i]["BGM0101"].ToString());
                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCnt > 0)
                        {
                            this.ShowCustomMessage("통관번호:" + ds.Tables[0].Rows[i]["BGM0101"].ToString() + " 은 이미 통관처리 되었습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        ////사업자번호의 화주 체크
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach("TY_P_UT_75GAY509", ds.Tables[0].Rows[i]["NAD0101"].ToString());
                        //iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        //if (iCnt > 0)
                        //{
                        //    this.ShowCustomMessage("사업자번호:" + ds.Tables[0].Rows[i]["NAD0101"].ToString() + "의 통관화주 확인이 불가능 합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        //    e.Successed = false;
                        //    return;
                        //}
                    }
                }
            }
            else
            {
                //SILO 
                DataTable dk = new DataTable();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        if (Convert.ToInt16(Get_Numeric(ds.Tables[0].Rows[i]["DOC0202"].ToString())) <= 0)
                        {
                            this.DbConnector.Attach("TY_P_US_89A90702", ds.Tables[0].Rows[i]["EDIHMNO1"].ToString(),
                                                                        ds.Tables[0].Rows[i]["EDIHMNO2"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0101"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0201"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0202"].ToString()
                                                                       );
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_US_89A91703", ds.Tables[0].Rows[i]["EDIHMNO1"].ToString(),
                                                                        ds.Tables[0].Rows[i]["EDIHMNO2"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0101"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0201"].ToString(),
                                                                        ds.Tables[0].Rows[i]["DOC0202"].ToString()
                                                                       );
                        }
                        dk = this.DbConnector.ExecuteDataTable();

                        if (dk.Rows.Count > 0)
                        {
                            if (dk.Rows[0]["IBHWAKIL"].ToString() == "0")
                            {
                                this.ShowCustomMessage("확정되지 않은 재고입니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }

                            //통관일자 체크
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DTM0201"].ToString()) < Convert.ToInt32(dk.Rows[0]["IBHWAKIL"].ToString()) && dk.Rows[0]["IBGUBUN"].ToString() == "12")
                            {
                                this.ShowCustomMessage("통관일이 입고일보다 작습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }

                            sIBHANGCHA = dk.Rows[0]["IBHANGCHA"].ToString();
                            sIBGOKJONG = dk.Rows[0]["IBGOKJONG"].ToString();
                            sIBHWAJU = dk.Rows[0]["IBHWAJU"].ToString();
                            sIBBLNO = dk.Rows[0]["IBBLNO"].ToString();
                            sIBBLMSN = dk.Rows[0]["IBBLMSN"].ToString();
                            sIBBLHSN = dk.Rows[0]["IBBLHSN"].ToString();
                            sIBHMNO1 = dk.Rows[0]["IBHMNO1"].ToString();
                            sIBHMNO2 = dk.Rows[0]["IBHMNO2"].ToString();

                        }
                        else
                        {
                            this.ShowCustomMessage("입고내역 자료가 존재하지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        //JEBL 재고내역 체크
                        dk.Clear();
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AAY705", sIBHANGCHA,sIBGOKJONG,sIBHWAJU,sIBBLNO,sIBBLMSN,sIBBLHSN,sIBHMNO1,sIBHMNO2);
                        dk = this.DbConnector.ExecuteDataTable();
                        if (dk.Rows.Count <= 0)
                        {                       
                            this.ShowCustomMessage("JEBL 재고내역이 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        //JEGO 재고내역 체크
                        dk.Clear();
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AB2706", sIBHANGCHA, sIBGOKJONG, sIBHWAJU );
                        dk = this.DbConnector.ExecuteDataTable();
                        if (dk.Rows.Count <= 0)
                        {
                            this.ShowCustomMessage("JEGO 재고내역이 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }

                        //관세사 체크
                        dk.Clear();
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_89AB7707", ds.Tables[0].Rows[i]["BGM0101"].ToString().Substring(0,5));
                        dk = this.DbConnector.ExecuteDataTable();
                        if (dk.Rows.Count <= 0)
                        {
                            this.ShowCustomMessage("미등록 관세사", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }                       

                    }
                }
            }

            if (!this.ShowMessage("TY_M_UT_75GAE499"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region  Description :  통관삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sChasu = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                   
                    //반출승인내역 처리상황 표시
                    this.DbConnector.Attach("TY_P_UT_75GES518", "D",
                                                                 TYUserInfo.EmpNo,
                                                                 ds.Tables[0].Rows[i]["BGM0101"].ToString()
                                                               );
                }

                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_UT_75GAN505");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {           
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_74691216.GetDataSourceInclude(TSpread.TActionType.Select, "EDIHMNO1", "EDIHMNO2", "BGM0101" ));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }          

            if (!this.ShowMessage("TY_M_UT_75GEV519"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description :  통관차수 조회
        private string UP_Get_Chasu(string sCSIPHANG,  string sCSBONSUN,  string sCSHWAJU,   string sCSHWAMUL,  string sCSBLNO,    string sCSCUSTIL,  string sCSMSNSEQ,  string sCSHSNSEQ  )
        {
            string sChasu = string.Empty;

            sChasu = "0";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GAS506", sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSBLNO, sCSCUSTIL, sCSMSNSEQ, sCSHSNSEQ );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if( dt.Rows.Count > 0 )
            {
                sChasu = dt.Rows[0]["CHASU"].ToString();
            }

            return sChasu;
        }
        #endregion
    }
}
