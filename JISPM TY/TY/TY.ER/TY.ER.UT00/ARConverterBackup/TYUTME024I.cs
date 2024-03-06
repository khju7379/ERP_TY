using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.AC00;
using System.IO;
using System.Drawing;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Html;
using DataDynamics.ActiveReports.Export.Pdf;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 전자세금계산서 발행 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.05.25 13:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_244BN404 : 거래처관리 조회
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_UT_75PDZ597 : 접안료 매출 조회
    ///  TY_P_UT_75Q8A624 : 스마트빌 전송 상태 조회
    ///  TY_P_UT_75Q8E625 : 스마트빌 전송 ID 조회
    ///  TY_P_UT_75QB6627 : 접안료 매출 계산서발행 조회
    ///  TY_P_UT_75QC0629 : 스마트빌 계산서 순번 조회
    ///  TY_P_UT_75QCU630 : 스마트빌 계산서 BATCH_ID 부여
    ///  TY_P_UT_75QE2632 : 스마트빌 전자세금계산서 메인 등록
    ///  TY_P_UT_75QEB633 : 스마트빌 전자세금계산서 ITEM 등록
    ///  TY_P_UT_75QG2637 : 전자세금계산서 전송 이력 등록
    ///  TY_P_UT_75QGE638 : 스마트빌 전자세금계산서 상태 등록
    ///  TY_P_UT_75QGJ639 : 스마트빌 전자세금계산서 INVOICE 등록
    ///  TY_P_UT_75QGM640 : 스마트빌 전자세금계산서 첨부파일 등록
    ///  TY_P_UT_75QH9644 : 전자세금계산서 최초 전송 상태 조회
    ///  TY_P_UT_75T9D652 : 스마트빌 전자세금계산서 첨부파일 테스트
    ///  TY_P_UT_75TDF654 : 스마트빌 전자세금계산서 메세지 등록
    ///  TY_P_UT_75TIE669 : 전자세금계산서 전송 호출(취소)
    ///  TY_P_UT_75U9J671 : 스마트빌 전자세금계산서 EVENTID 생성
    ///  TY_P_UT_75UEM672 : 하역료 매출 조회
    ///  TY_P_UT_75UFE674 : 하역료 매출 계산서발행 조회
    ///  TY_P_UT_762G5714 : 보관취급료 매출 조회
    ///  TY_P_UT_767A3718 : 보관취급료 매출 계산서발행 조회
    ///  TY_P_UT_76J9J854 : 보관취급료 거래명세서 SP
    ///  TY_P_UT_76LBJ923 : 하역료 거래명세서 SP
    ///  TY_P_UT_773DB999 : 스마트빌 전자세금계산서 접안료 거래명세서 출력
    ///  TY_P_UT_773FP001 : 스마트빌 전자세금계산서 하역료 거래명세서 출력
    ///  TY_P_UT_773GE002 : 스마트빌 전자세금계산서 보관취급료 거래명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_75PDO593 : 전자세금계산서 발행 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    ///  TY_M_GB_34I35533 : 계산서를 취소하시겠습니까?
    ///  TY_M_GB_34I9W523 : 계산서서를 발행하였습니다.
    ///  TY_M_GB_34I9X524 : 계산서를 취소하였습니다.
    ///  TY_M_GB_34JA0536 : 계산서 상태를 확인하세요!
    ///  TY_M_GB_75PE1599 : 계산서를 발행 하시겠습니까?
    ///  TY_M_UT_75QAM626 : 이미 발행된 계산서입니다! 계산서 취소후 발행하세요!
    ///  TY_M_UT_75UEU673 : 메일 주소를 확인하세요!
    /// 
    ///  # 필드사전 정보 ####
    ///  BILL_REM : 계산서취소
    ///  BILL_SEND : 계산서발행
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  MCHWAJU : 화주
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    
    public partial class TYUTME024I : TYBase
    {
        
        private string fsFileDownPath = "C:\\Invoice\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

        private DataTable ftSmartTable;
        private DataTable ftInvocieTable;

        private string fsSUP_COMID = "tycculutt";
        private string fsSUP_COMPASS = "tyculsan2922";

        private string fsSUP_SAUPNO;
        private string fsSUP_SANGHO;
        private string fsSUP_IRUM;
        private string fsSUP_UPJONG;
        private string fsSUP_UPTE;
        private string fsSUP_JUSO;

        private string fsBATCH_ID;
        
        #region  Description : 전자세금계산용 DATA 선언
        private TYData DAT01_CONVERSATION_ID;
        private TYData DAT01_SUPBUY_TYPE;
        private TYData DAT01_DIRECTION; 
        private TYData DAT01_INTERFACE_BATCH_ID;
        private TYData DAT01_DTI_WDATE;
        private TYData DAT01_DTI_TYPE;
        private TYData DAT01_TAX_DEMAND;
        private TYData DAT01_SUP_COM_ID;
        private TYData DAT01_SUP_COM_REGNO;
        private TYData DAT01_SUP_REP_NAME;
        private TYData DAT01_SUP_COM_NAME;
        private TYData DAT01_SUP_COM_TYPE;
        private TYData DAT01_SUP_COM_CLASSIFY;
        private TYData DAT01_SUP_COM_ADDR;
        private TYData DAT01_SUP_DEPT_NAME;
        private TYData DAT01_SUP_EMP_NAME;
        private TYData DAT01_SUP_TEL_NUM;
        private TYData DAT01_SUP_EMAIL;
        private TYData DAT01_BYR_COM_REGNO;
        private TYData DAT01_BYR_REP_NAME;
        private TYData DAT01_BYR_COM_NAME;
        private TYData DAT01_BYR_COM_TYPE;
        private TYData DAT01_BYR_COM_CLASSIFY;
        private TYData DAT01_BYR_COM_ADDR;
        private TYData DAT01_BYR_DEPT_NAME;
        private TYData DAT01_BYR_EMP_NAME;
        private TYData DAT01_BYR_TEL_NUM;
        private TYData DAT01_BYR_EMAIL;
        private TYData DAT01_SUP_AMOUNT;
        private TYData DAT01_TAX_AMOUNT;
        private TYData DAT01_TOTAL_AMOUNT;
        private TYData DAT01_DTT_YN;
        private TYData DAT01_REMARK;
        private TYData DAT01_CREATED_BY;
        private TYData DAT01_CREATION_DATE;
        private TYData DAT01_AMEND_CODE;
        private TYData DAT01_ORI_ISSUE_ID;
        private TYData DAT01_ATTACHFILE_YN;
        private TYData DAT01_BYR_BIZPLACE_CODE;
        private TYData DAT01_XDDATE;
        private TYData DAT01_XDHWAJU;
        private TYData DAT01_XDMHGUBN;
        private TYData DAT01_XDMJPNO;
        private TYData DAT01_XDCONVERSATION_ID;


        private TYData DAT02_CONVERSATION_ID;
        private TYData DAT02_SUPBUY_TYPE;
        private TYData DAT02_DIRECTION;
        private TYData DAT02_DTI_LINE_NUM;
        private TYData DAT02_ITEM_CODE;
        private TYData DAT02_ITEM_NAME;
        private TYData DAT02_ITEM_SIZE;
        private TYData DAT02_ITEM_MD;
        private TYData DAT02_UNIT_PRICE;
        private TYData DAT02_ITEM_QTY;
        private TYData DAT02_SUP_AMOUNT;
        private TYData DAT02_TAX_AMOUNT;
        private TYData DAT02_REMARK;
        private TYData DAT02_CREATED_BY;
        private TYData DAT02_CREATION_DATE;

        private TYData DAT03_DTI_STATUS;

        private TYData DAT04_INVOICE_IDX;
        private TYData DAT04_INVOICE_ID;
        private TYData DAT04_INVOICE_NUM;

        private TYData DAT05_CONVERSATION_ID;
        private TYData DAT05_FILE_SEQ;
        private TYData DAT05_EVENT_ID;
        private TYData DAT05_FILE_NAME;
        private TYData DAT05_FILE_SAVE_TYPE;
        private TYData DAT05_FILE_SIZE;
        private TYData DAT05_FILE_BINARY;
        private TYData DAT05_FILE_STATUS;
        private TYData DAT05_CREATION_BY;
        private TYData DAT05_CREATION_DATE;
        private TYData DAT05_LAST_UPDATE_BY;
        private TYData DAT05_LAST_UPDATE_DATE;
        private TYData DAT05_IN_OUT;


        #endregion

        System.Collections.Generic.List<object[]> data_Main = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_Item = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_Sts = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_Inv = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> data_msg = new System.Collections.Generic.List<object[]>();

        System.Collections.Generic.List<object[]> data_UTIMain = new System.Collections.Generic.List<object[]>();

        private DataSet fsds = new DataSet();

        private object _CBH01_KBSABUN_Value;
        private object _CBO01_INQOPTION_Value;
        private object _DTP01_SDATE_Value;
        private object _DTP01_EDATE_Value;
        private object _CBH01_MCHWAJU_Value;

        #region  Description : 폼 로드 이벤트
        public TYUTME024I()
        {
            InitializeComponent();

            #region  Description : 전자세금계산용 DATA 선언
            this.DAT01_CONVERSATION_ID = new TYData("DAT01_CONVERSATION_ID", null);
            this.DAT01_SUPBUY_TYPE = new TYData("DAT01_SUPBUY_TYPE", null);
            this.DAT01_DIRECTION = new TYData("DAT01_DIRECTION", null);
            this.DAT01_INTERFACE_BATCH_ID = new TYData("DAT01_INTERFACE_BATCH_ID", null);
            this.DAT01_DTI_WDATE = new TYData("DAT01_DTI_WDATE", null);
            this.DAT01_DTI_TYPE = new TYData("DAT01_DTI_TYPE", null);
            this.DAT01_TAX_DEMAND = new TYData("DAT01_TAX_DEMAND", null);
            this.DAT01_SUP_COM_ID = new TYData("DAT01_SUP_COM_ID", null);
            this.DAT01_SUP_COM_REGNO = new TYData("DAT01_SUP_COM_REGNO", null);
            this.DAT01_SUP_REP_NAME = new TYData("DAT01_SUP_REP_NAME", null);
            this.DAT01_SUP_COM_NAME = new TYData("DAT01_SUP_COM_NAME", null);
            this.DAT01_SUP_COM_TYPE = new TYData("DAT01_SUP_COM_TYPE", null);
            this.DAT01_SUP_COM_CLASSIFY = new TYData("DAT01_SUP_COM_CLASSIFY", null);
            this.DAT01_SUP_COM_ADDR = new TYData("DAT01_SUP_COM_ADDR", null);
            this.DAT01_SUP_DEPT_NAME = new TYData("DAT01_SUP_DEPT_NAME", null);
            this.DAT01_SUP_EMP_NAME = new TYData("DAT01_SUP_EMP_NAME", null);
            this.DAT01_SUP_TEL_NUM = new TYData("DAT01_SUP_TEL_NUM", null);
            this.DAT01_SUP_EMAIL = new TYData("DAT01_SUP_EMAIL", null);
            this.DAT01_BYR_COM_REGNO = new TYData("DAT01_BYR_COM_REGNO", null);
            this.DAT01_BYR_REP_NAME = new TYData("DAT01_BYR_REP_NAME", null);
            this.DAT01_BYR_COM_NAME = new TYData("DAT01_BYR_COM_NAME", null);
            this.DAT01_BYR_COM_TYPE = new TYData("DAT01_BYR_COM_TYPE", null);
            this.DAT01_BYR_COM_CLASSIFY = new TYData("DAT01_BYR_COM_CLASSIFY", null);
            this.DAT01_BYR_COM_ADDR = new TYData("DAT01_BYR_COM_ADDR", null);
            this.DAT01_BYR_DEPT_NAME = new TYData("DAT01_BYR_DEPT_NAME", null);
            this.DAT01_BYR_EMP_NAME = new TYData("DAT01_BYR_EMP_NAME", null);
            this.DAT01_BYR_TEL_NUM = new TYData("DAT01_BYR_TEL_NUM", null);
            this.DAT01_BYR_EMAIL = new TYData("DAT01_BYR_EMAIL", null);
            this.DAT01_SUP_AMOUNT = new TYData("DAT01_SUP_AMOUNT", null);
            this.DAT01_TAX_AMOUNT = new TYData("DAT01_TAX_AMOUNT", null);
            this.DAT01_TOTAL_AMOUNT = new TYData("DAT01_TOTAL_AMOUNT", null);
            this.DAT01_DTT_YN = new TYData("DAT01_DTT_YN", null);
            this.DAT01_REMARK = new TYData("DAT01_REMARK", null);
            this.DAT01_CREATED_BY = new TYData("DAT01_CREATED_BY", null);
            this.DAT01_CREATION_DATE = new TYData("DAT01_CREATION_DATE", null);
            this.DAT01_AMEND_CODE = new TYData("DAT01_AMEND_CODE", null);
            this.DAT01_ORI_ISSUE_ID = new TYData("DAT01_ORI_ISSUE_ID", null);
            this.DAT01_ATTACHFILE_YN = new TYData("DAT01_ATTACHFILE_YN", null);
            this.DAT01_BYR_BIZPLACE_CODE = new TYData("DAT01_BYR_BIZPLACE_CODE", null);

            DAT01_XDDATE = new TYData("DAT01_XDDATE", null);
            DAT01_XDHWAJU = new TYData("DAT01_XDHWAJU", null);
            DAT01_XDMHGUBN = new TYData("DAT01_XDMHGUBN", null);
            DAT01_XDMJPNO = new TYData("DAT01_XDMJPNO", null);
            DAT01_XDCONVERSATION_ID = new TYData("DAT01_XDCONVERSATION_ID", null);
            
            this.DAT02_CONVERSATION_ID = new TYData("DAT02_CONVERSATION_ID", null);
            this.DAT02_SUPBUY_TYPE = new TYData("DAT02_SUPBUY_TYPE", null);
            this.DAT02_DIRECTION = new TYData("DAT02_DIRECTION", null);
            this.DAT02_DTI_LINE_NUM = new TYData("DAT02_DTI_LINE_NUM", null);
            this.DAT02_ITEM_CODE = new TYData("DAT02_ITEM_CODE", null);
            this.DAT02_ITEM_NAME = new TYData("DAT02_ITEM_NAME", null);
            this.DAT02_ITEM_SIZE = new TYData("DAT02_ITEM_SIZE", null);
            this.DAT02_ITEM_MD = new TYData("DAT02_ITEM_MD", null);
            this.DAT02_UNIT_PRICE = new TYData("DAT02_UNIT_PRICE", null);
            this.DAT02_ITEM_QTY = new TYData("DAT02_ITEM_QTY", null);
            this.DAT02_SUP_AMOUNT = new TYData("DAT02_SUP_AMOUNT", null);
            this.DAT02_TAX_AMOUNT = new TYData("DAT02_TAX_AMOUNT", null);
            this.DAT02_REMARK = new TYData("DAT02_REMARK", null);
            this.DAT02_CREATED_BY = new TYData("DAT02_CREATED_BY", null);
            this.DAT02_CREATION_DATE = new TYData("DAT02_CREATION_DATE", null);
            
            this.DAT03_DTI_STATUS = new TYData("DAT03_DTI_STATUS", null);

            this.DAT04_INVOICE_IDX = new TYData("DAT04_INVOICE_IDX", null);
            this.DAT04_INVOICE_ID = new TYData("DAT04_INVOICE_ID", null);
            this.DAT04_INVOICE_NUM = new TYData("DAT04_INVOICE_NUM", null);

            this.DAT05_CONVERSATION_ID = new TYData("DAT05_CONVERSATION_ID", null);
            this.DAT05_FILE_SEQ = new TYData("DAT05_FILE_SEQ", null);
            this.DAT05_EVENT_ID = new TYData("DAT05_EVENT_ID", null);
            this.DAT05_FILE_NAME = new TYData("DAT05_FILE_NAME", null);
            this.DAT05_FILE_SAVE_TYPE = new TYData("DAT05_FILE_SAVE_TYPE", null);
            this.DAT05_FILE_SIZE = new TYData("DAT05_FILE_SIZE", null);
            this.DAT05_FILE_BINARY = new TYData("DAT05_FILE_BINARY", null);
            this.DAT05_FILE_STATUS = new TYData("DAT05_FILE_STATUS", null);
            this.DAT05_CREATION_BY = new TYData("DAT05_CREATION_BY", null);
            this.DAT05_CREATION_DATE = new TYData("DAT05_CREATION_DATE", null);
            this.DAT05_LAST_UPDATE_BY = new TYData("DAT05_LAST_UPDATE_BY", null);
            this.DAT05_LAST_UPDATE_DATE = new TYData("DAT05_LAST_UPDATE_DATE", null);
            this.DAT05_IN_OUT = new TYData("DAT05_IN_OUT", null);

            #endregion

        }

        private void TYUTME024I_Load(object sender, System.EventArgs e)
        {
            #region  Description : 전자세금계산용 DATA 선언
            this.ControlFactory.Add(this.DAT01_CONVERSATION_ID);
            this.ControlFactory.Add(this.DAT01_SUPBUY_TYPE);     
            this.ControlFactory.Add(this.DAT01_DIRECTION);       
            this.ControlFactory.Add(this.DAT01_INTERFACE_BATCH_ID); 
            this.ControlFactory.Add(this.DAT01_DTI_WDATE);      
            this.ControlFactory.Add(this.DAT01_DTI_TYPE);       
            this.ControlFactory.Add(this.DAT01_TAX_DEMAND);     		  
            this.ControlFactory.Add(this.DAT01_SUP_COM_ID);     
            this.ControlFactory.Add(this.DAT01_SUP_COM_REGNO);  
            this.ControlFactory.Add(this.DAT01_SUP_REP_NAME);   
            this.ControlFactory.Add(this.DAT01_SUP_COM_NAME);   
            this.ControlFactory.Add(this.DAT01_SUP_COM_TYPE);   
            this.ControlFactory.Add(this.DAT01_SUP_COM_CLASSIFY);
            this.ControlFactory.Add(this.DAT01_SUP_COM_ADDR);    
            this.ControlFactory.Add(this.DAT01_SUP_DEPT_NAME);   
            this.ControlFactory.Add(this.DAT01_SUP_EMP_NAME);    
            this.ControlFactory.Add(this.DAT01_SUP_TEL_NUM);     
            this.ControlFactory.Add(this.DAT01_SUP_EMAIL);       
            this.ControlFactory.Add(this.DAT01_BYR_COM_REGNO);   
            this.ControlFactory.Add(this.DAT01_BYR_REP_NAME);    
            this.ControlFactory.Add(this.DAT01_BYR_COM_NAME);    
            this.ControlFactory.Add(this.DAT01_BYR_COM_TYPE);    
            this.ControlFactory.Add(this.DAT01_BYR_COM_CLASSIFY);
            this.ControlFactory.Add(this.DAT01_BYR_COM_ADDR);    
            this.ControlFactory.Add(this.DAT01_BYR_DEPT_NAME);   
            this.ControlFactory.Add(this.DAT01_BYR_EMP_NAME);    
            this.ControlFactory.Add(this.DAT01_BYR_TEL_NUM);     
            this.ControlFactory.Add(this.DAT01_BYR_EMAIL);       
            this.ControlFactory.Add(this.DAT01_SUP_AMOUNT);      
            this.ControlFactory.Add(this.DAT01_TAX_AMOUNT);      
            this.ControlFactory.Add(this.DAT01_TOTAL_AMOUNT);    
            this.ControlFactory.Add(this.DAT01_DTT_YN);          
            this.ControlFactory.Add(this.DAT01_REMARK);          
            this.ControlFactory.Add(this.DAT01_CREATED_BY);      
            this.ControlFactory.Add(this.DAT01_CREATION_DATE);   
            this.ControlFactory.Add(this.DAT01_AMEND_CODE);      
            this.ControlFactory.Add(this.DAT01_ORI_ISSUE_ID);    
            this.ControlFactory.Add(this.DAT01_ATTACHFILE_YN);
            this.ControlFactory.Add(this.DAT01_BYR_BIZPLACE_CODE);

            this.ControlFactory.Add(DAT01_XDDATE);
            this.ControlFactory.Add(DAT01_XDHWAJU);
            this.ControlFactory.Add(DAT01_XDMHGUBN);
            this.ControlFactory.Add(DAT01_XDMJPNO);
            this.ControlFactory.Add(DAT01_XDCONVERSATION_ID);
            
            this.ControlFactory.Add(this.DAT02_CONVERSATION_ID);
            this.ControlFactory.Add(this.DAT02_SUPBUY_TYPE);    
            this.ControlFactory.Add(this.DAT02_DIRECTION);      
            this.ControlFactory.Add(this.DAT02_DTI_LINE_NUM);   
            this.ControlFactory.Add(this.DAT02_ITEM_CODE);      
            this.ControlFactory.Add(this.DAT02_ITEM_NAME);      
            this.ControlFactory.Add(this.DAT02_ITEM_SIZE);      
            this.ControlFactory.Add(this.DAT02_ITEM_MD);        
            this.ControlFactory.Add(this.DAT02_UNIT_PRICE);     
            this.ControlFactory.Add(this.DAT02_ITEM_QTY);       
            this.ControlFactory.Add(this.DAT02_SUP_AMOUNT);     
            this.ControlFactory.Add(this.DAT02_TAX_AMOUNT);    
            this.ControlFactory.Add(this.DAT02_REMARK);         
            this.ControlFactory.Add(this.DAT02_CREATED_BY);
            this.ControlFactory.Add(this.DAT02_CREATION_DATE);

            this.ControlFactory.Add(this.DAT03_DTI_STATUS);

            this.ControlFactory.Add(this.DAT04_INVOICE_IDX);
            this.ControlFactory.Add(this.DAT04_INVOICE_ID);
            this.ControlFactory.Add(this.DAT04_INVOICE_NUM);

            this.ControlFactory.Add(this.DAT05_CONVERSATION_ID);
            this.ControlFactory.Add(this.DAT05_FILE_SEQ);
            this.ControlFactory.Add(this.DAT05_EVENT_ID);            
            this.ControlFactory.Add(this.DAT05_FILE_NAME);
            this.ControlFactory.Add(this.DAT05_FILE_SAVE_TYPE);
            this.ControlFactory.Add(this.DAT05_FILE_SIZE);
            this.ControlFactory.Add(this.DAT05_FILE_BINARY);
            this.ControlFactory.Add(this.DAT05_FILE_STATUS);
            this.ControlFactory.Add(this.DAT05_CREATION_BY);
            this.ControlFactory.Add(this.DAT05_CREATION_DATE);
            this.ControlFactory.Add(this.DAT05_LAST_UPDATE_BY);
            this.ControlFactory.Add(this.DAT05_LAST_UPDATE_DATE);
            this.ControlFactory.Add(this.DAT05_IN_OUT);

            #endregion           

            //this.BTN61_BILL_SEND.IsAsynchronous = true;  

            (this.FPS91_TY_S_UT_75PDO593.Sheets[0].Columns[15].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            (this.FPS91_TY_S_UT_75PDO593.Sheets[0].Columns[16].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_75PDO593, "MCLOG");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_75PDO593, "MCPRT");

            ToolStripMenuItem reateMail = new ToolStripMenuItem("메일재발송");
            reateMail.Click += new EventHandler(reateMail_ToolStripMenuItem_Click);

            this.FPS91_TY_S_UT_75PDO593.CurrentContextMenu.Items.AddRange(
                                     new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateMail });
            
            ftSmartTable = UP_SetDataTable();
            ftInvocieTable = UP_Set_InvocieFileAttachTable();

            this.BTN61_BILL_SEND.ProcessCheck += new TButton.CheckHandler(BTN61_BILL_SEND_ProcessCheck);
            this.BTN61_BILL_REM.ProcessCheck += new TButton.CheckHandler(BTN61_BILL_REM_ProcessCheck);

            //Invoice 폴더가 없으면 만들어준다
            if (Directory.Exists(fsFileDownPath) == false)
            {
                Directory.CreateDirectory(fsFileDownPath);
            }

            //하위폴더 자료 무조건 삭제
            string[] subDirectories = System.IO.Directory.GetDirectories("C:\\Invoice\\");
            string sDirName = "";
            if (subDirectories.Length > 0)
            {
                for (int i = 0; i < subDirectories.Length; i++)
                {
                    sDirName = subDirectories[i].ToString();
                    Directory.Delete(sDirName, true);
                }
            }            

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH01_KBSABUN.SetValue(TYUserInfo.EmpNo);

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.UP_DataBinding(CBO01_INQOPTION.GetValue().ToString(), this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), CBH01_MCHWAJU.GetValue().ToString() );
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding(string sINQOPTION, string sSDATE, string sEDATE, string sMCHWAJU)
        {
            string sRET_MSG = string.Empty;
            string sWORKGB = "";

            ftSmartTable.Clear();
            ftInvocieTable.Clear();

            DateTime dtSDATE = Convert.ToDateTime(sSDATE.Substring(0, 4) + "-" + sSDATE.Substring(4, 2) + "-" + sSDATE.Substring(6, 2));
            DateTime dtEDATE = Convert.ToDateTime(sEDATE.Substring(0, 4) + "-" + sEDATE.Substring(4, 2) + "-" + sEDATE.Substring(6, 2));
            DateTime dtDATE = dtSDATE;

            TimeSpan tsDATE = dtEDATE - dtSDATE;

            for (int i = 0; i <= Convert.ToInt32(tsDATE.Days.ToString()); i++)
            {
                //거래명세서 자료을 위해 미리 SP 호출
                if (sINQOPTION == "03")  //하역료
                {   
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_76LBJ923", dtDATE.ToString("yyyyMMdd"),
                                                                sMCHWAJU,
                                                                "",
                                                                "",
                                                                "",
                                                                sWORKGB,
                                                                sRET_MSG
                                                                );
                    sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
                else if (sINQOPTION == "05")  //보관취급료
                {
                    this.DbConnector.CommandClear();
                    // 20190523 수정전
                    //this.DbConnector.Attach("TY_P_UT_76J9J854", sSDATE,
                    //                                            sMCHWAJU,
                    //                                            "",
                    //                                            sRET_MSG
                    //                                            );

                    // 20190523 수정후(LPG 포함)
                    this.DbConnector.Attach("TY_P_UT_95NIH616", dtDATE.ToString("yyyyMMdd"),
                                                                sMCHWAJU,
                                                                "",
                                                                sWORKGB,
                                                                sRET_MSG
                                                                );

                    sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                }
                dtDATE = dtDATE.AddDays(1);

                if (i == 0)
                {
                    sWORKGB = "P";
                }
            }

            FPS91_TY_S_UT_75PDO593.Initialize();

            this.DbConnector.CommandClear();
            if (sINQOPTION == "01")
            {
                //this.DbConnector.Attach("TY_P_UT_75PDZ597", sSDATE, sMCHWAJU);
                this.DbConnector.Attach("TY_P_UT_C8GDP884", sSDATE, sEDATE, sMCHWAJU);
            }
            else if (sINQOPTION == "03")
            {
                //this.DbConnector.Attach("TY_P_UT_75UEM672", sSDATE, sMCHWAJU);
                this.DbConnector.Attach("TY_P_UT_C8GDQ887", sSDATE, sEDATE, sMCHWAJU);
            }
            else if (sINQOPTION == "05")
            {
                //this.DbConnector.Attach("TY_P_UT_762G5714", sSDATE, sMCHWAJU);
                this.DbConnector.Attach("TY_P_UT_C8GDR888", sSDATE, sEDATE, sMCHWAJU);
            }

            FPS91_TY_S_UT_75PDO593.SetValue(UP_Get_SmartBillStatus(this.DbConnector.ExecuteDataTable()));

            if (FPS91_TY_S_UT_75PDO593.CurrentRowCount > 0)
            {
                for (int i = 0; i < FPS91_TY_S_UT_75PDO593.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_75PDO593_Sheet1.Cells[i, 14].Text == "N")
                    {
                        this.FPS91_TY_S_UT_75PDO593_Sheet1.Cells[i, 16].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    this.FPS91_TY_S_UT_75PDO593_Sheet1.Cells[i, 5].ForeColor = UP_StatusTextColor(this.FPS91_TY_S_UT_75PDO593.GetValue(i, "MCBILLSTATUS").ToString());
                    this.FPS91_TY_S_UT_75PDO593_Sheet1.Cells[i, 6].ForeColor = UP_StatusTextColor(this.FPS91_TY_S_UT_75PDO593.GetValue(i, "MCBILLSTATUS").ToString());
                }
            }

        }
        #endregion

        #region  Description : 스마트빌 전송 상태 확인 이벤트
        private DataTable UP_Get_SmartBillStatus(DataTable dt)
        {
            DataRow rw;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] sSMSCODE = UP_SMSBILL_STATUS(dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCGUBN"].ToString(), dt.Rows[i]["MCJPNO"].ToString());

                    rw = ftSmartTable.NewRow();
                    rw["MCGUBN"] = dt.Rows[i]["MCGUBN"].ToString();
                    rw["MCDATE"] = dt.Rows[i]["MCDATE"].ToString();
                    rw["MCHWAJU"] = dt.Rows[i]["MCHWAJU"].ToString();
                    rw["MCHWAJUNM"] = dt.Rows[i]["MCHWAJUNM"].ToString();

                    rw["MCBILLMAIL"] = sSMSCODE.Length > 1 ?  sSMSCODE[2] : dt.Rows[i]["MCBILLMAIL"].ToString();                    

                    rw["MCBILLSTATUS"] = sSMSCODE[0];
                    rw["MCBILLSTATUSNM"] = UP_SMSBILL_CODENAME(sSMSCODE[0]);
                    rw["MCJPNO"] = dt.Rows[i]["MCJPNO"].ToString();
                    rw["MCDANGAMT"] = dt.Rows[i]["MCDANGAMT"].ToString();
                    rw["MCVATAMT"] = dt.Rows[i]["MCVATAMT"].ToString();
                    rw["MCTOTALAMT"] = Convert.ToDouble(dt.Rows[i]["MCDANGAMT"].ToString()) + Convert.ToDouble(dt.Rows[i]["MCVATAMT"].ToString());
                    rw["MCTAXSTATUS"] = sSMSCODE.Length > 1 ? sSMSCODE[1] : "";
                    rw["MCBIGO"] = dt.Rows[i]["MCBIGO"].ToString();
                    rw["MCCONVERSATION_ID"] = dt.Rows[i]["MCCONVERSATION_ID"].ToString();
                    rw["MCINV"] = sSMSCODE.Length > 1 ? sSMSCODE[3] : "N";
                    ftSmartTable.Rows.Add(rw);                  
                }
            }

            return ftSmartTable;
        }
        #endregion      

        #region  Description : 발행 버튼 이벤트
        private void BTN61_BILL_SEND_Click(object sender, EventArgs e)
        {
            this._CBH01_KBSABUN_Value = this.CBH01_KBSABUN.GetValue();
            this._CBO01_INQOPTION_Value = this.CBO01_INQOPTION.GetValue();
            this._DTP01_SDATE_Value = this.DTP01_SDATE.GetString().ToString();
            this._DTP01_EDATE_Value = this.DTP01_EDATE.GetString().ToString();
            this._CBH01_MCHWAJU_Value = this.CBH01_MCHWAJU.GetValue().ToString();

            string sCONVERSATION_ID = string.Empty;


            

            //string sBATCH_ID = string.Empty;

            //ftInvocieTable.Clear();

            //UP_SetDataList_Clear();

            //UP_Get_SupInfo();

            //DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            
            DataSet ds = fsds;

            if (ds.Tables[0].Rows.Count > 0)
            {
                ////세금계산서 
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    if (i == 0)
                //    {
                //        sBATCH_ID = UP_Get_BATCH_ID();
                //    }
                //    if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "01") //접안료
                //    {
                //        UP_ProCess_ShipSales(ds.Tables[0], i, sBATCH_ID);
                //    }
                //    else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "03") //하역료
                //    {
                //        UP_ProCess_LoadSales(ds.Tables[0], i, sBATCH_ID);
                //    }
                //    else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "05") //보관취급료
                //    {
                //        UP_ProCess_StorageSales(ds.Tables[0], i, sBATCH_ID);
                //    }
                //}

                if (data_UTIMain.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in data_UTIMain)
                    {
                        this.DbConnector.Attach("TY_P_UT_75QG2637", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
                else
                {
                    this.ShowCustomMessage("선택한 계산서자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                if (data_Main.Count > 0)
                {
                    
                    this.DbConnector.CommandClear();

                    foreach (object[] data in data_Main)  //XXSB_DTI_MAIN 
                    {
                        this.DbConnector.Attach("TY_P_UT_75QE2632", data);
                    }
                    foreach (object[] data in data_Item) //XXSB_DTI_ITEM 
                    {
                        this.DbConnector.Attach("TY_P_UT_75QEB633", data);
                    }
                    foreach (object[] data in data_Sts) //XXSB_DTI_STATUS 
                    {
                        this.DbConnector.Attach("TY_P_UT_75QGE638", data);
                    }
                    foreach (object[] data in data_Inv) //XXSB_DTI_INVOICE 
                    {
                        this.DbConnector.Attach("TY_P_UT_75QGJ639", data);
                    }
                    foreach (object[] data in data_msg) //XXSB_DELIVERY_EVENT_MSG
                    {
                        this.DbConnector.Attach("TY_P_UT_75TDF654", data);
                    }

                    if (ftInvocieTable.Rows.Count > 0)  //XXSB_DELIVERY_DTI_FILE 
                    {
                        for (int i = 0; i < ftInvocieTable.Rows.Count; i++)
                        {
                            DAT05_CONVERSATION_ID.SetValue(ftInvocieTable.Rows[i]["CONVERSATION_ID"].ToString());
                            DAT05_FILE_SEQ.SetValue(ftInvocieTable.Rows[i]["FILE_SEQ"].ToString());
                            DAT05_EVENT_ID.SetValue(ftInvocieTable.Rows[i]["EVENT_ID"].ToString());
                            DAT05_FILE_NAME.SetValue(ftInvocieTable.Rows[i]["FILE_NAME"].ToString());
                            DAT05_FILE_SAVE_TYPE.SetValue(ftInvocieTable.Rows[i]["FILE_SAVE_TYPE"].ToString());
                            DAT05_FILE_SIZE.SetValue(ftInvocieTable.Rows[i]["FILE_SIZE"].ToString());
                            DAT05_FILE_BINARY.SetValue(ftInvocieTable.Rows[i]["FILE_BINARY"]);
                            DAT05_FILE_STATUS.SetValue(ftInvocieTable.Rows[i]["FILE_STATUS"].ToString());
                            DAT05_CREATION_BY.SetValue(ftInvocieTable.Rows[i]["CREATION_BY"].ToString());
                            DAT05_CREATION_DATE.SetValue(ftInvocieTable.Rows[i]["CREATION_DATE"].ToString());
                            DAT05_LAST_UPDATE_BY.SetValue(ftInvocieTable.Rows[i]["LAST_UPDATE_BY"].ToString());
                            DAT05_LAST_UPDATE_DATE.SetValue(ftInvocieTable.Rows[i]["LAST_UPDATE_DATE"].ToString());
                            DAT05_IN_OUT.SetValue(ftInvocieTable.Rows[i]["IN_OUT"].ToString());

                            this.DbConnector.Attach("TY_P_UT_75QGM640", this.ControlFactory, "05");
                        }
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                //스마트빌 사이트 전송
                UP_SMSBILL_WebServiceCall(fsBATCH_ID);
            }

            this.UP_DataBinding(_CBO01_INQOPTION_Value.ToString(), _DTP01_SDATE_Value.ToString(), _DTP01_EDATE_Value.ToString(), _CBH01_MCHWAJU_Value.ToString());
            

        }
        private void BTN61_BILL_SEND_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bResultSts = false;
            fsBATCH_ID = "";

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_75PDO593.GetDataSourceInclude(TSpread.TActionType.Select, "MCGUBN", "MCDATE", "MCHWAJU", "MCBILLMAIL", "MCBILLSTATUS", "MCJPNO", "MCDANGAMT", "MCVATAMT", "MCTOTALAMT", "MCTAXSTATUS", "MCCONVERSATION_ID", "MCBIGO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //전송상태 체크
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //메일주소 체크
                    if (ds.Tables[0].Rows[i]["MCBILLMAIL"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_75UEU673");
                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_UT_93RAY189", ds.Tables[0].Rows[i]["MCJPNO"].ToString(), ds.Tables[0].Rows[i]["MCCONVERSATION_ID"].ToString());
                    this.DbConnector.Attach("TY_P_UT_93RAY189", ds.Tables[0].Rows[i]["MCJPNO"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string sDtiSts = dt.Rows[j]["DTI_STATUS"].ToString();
                            switch (sDtiSts)
                            {
                                case "A":
                                    //sSTATUS = "전자(저장)";
                                    bResultSts = true;
                                    break;
                                case "S":
                                    //sSTATUS = "전자(저장)";
                                    bResultSts = true;
                                    break;
                                case "I":
                                    //sSTATUS = "수신미승인";
                                    bResultSts = false;
                                    break;
                                case "C":
                                    //sSTATUS = "수신승인";
                                    bResultSts = false;
                                    break;
                                case "R":
                                    //sSTATUS = "수신거부";
                                    bResultSts = true;
                                    break;
                                case "V":
                                    //sSTATUS = "역발행요청";
                                    bResultSts = true;
                                    break;
                                case "W":
                                    //sSTATUS = "역발행요청취소";
                                    bResultSts = true;
                                    break;
                                case "T":
                                    //sSTATUS = "역발행요청거부";
                                    bResultSts = true;
                                    break;
                                case "N":
                                    //sSTATUS = "공급자발행취소요청";
                                    bResultSts = true;
                                    break;
                                case "M":
                                    //sSTATUS = "공급받는자발행취소요청";
                                    bResultSts = true;
                                    break;
                                case "O":
                                    //sSTATUS = "취소완료";
                                    bResultSts = true;
                                    break;
                                default:
                                    //sSTATUS = "XX";
                                    bResultSts = true;
                                    break;
                            }

                            if (bResultSts != true)
                            {
                                this.ShowMessage("TY_M_GB_34JA0536");
                                e.Successed = false;
                                return;
                            }
                        }

                    }

                }
            
            }
            
            ftInvocieTable.Clear();
            UP_SetDataList_Clear();

            //거래명세서 생성
            if (ds.Tables[0].Rows.Count > 0)
            {
                UP_Get_SupInfo();

                //세금계산서 
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        fsBATCH_ID = UP_Get_BATCH_ID();
                    }
                    if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "01") //접안료
                    {
                        UP_ProCess_ShipSales(ds.Tables[0], i, fsBATCH_ID);
                    }
                    else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "03") //하역료
                    {
                        UP_ProCess_LoadSales(ds.Tables[0], i, fsBATCH_ID);
                    }
                    else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "05") //보관취급료
                    {
                        UP_ProCess_StorageSales(ds.Tables[0], i, fsBATCH_ID);
                    }
                }

                //전송이력 삭제
                if (data_UTIMain.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in data_UTIMain)
                    {
                        this.DbConnector.Attach("TY_P_UT_81OAN525", data[0].ToString(),
                                                                    data[1].ToString(),
                                                                    data[2].ToString(),
                                                                    data[3].ToString(),
                                                                    data[4].ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                if (ds.Tables[0].Rows.Count != ftInvocieTable.Rows.Count)
                {
                    this.ShowCustomMessage("거래명세서 첨부파일이 생성되지 않았습니다! 다시 작업해주세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_75PE1599"))
            {
                e.Successed = false;
                return;
            }

            //e.ArgData = ds;

            fsds.Clear();

            fsds = ds;
        }
        #endregion

        #region  Description : 취소 버튼 이벤트
        private void BTN61_BILL_REM_Click(object sender, EventArgs e)
        {
            Int64 iBATCH_ID = 0;
            string sOutMsg = string.Empty;
            string sCONVERSATION_IDList = string.Empty;
            
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                //세금계산서 
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sCONVERSATION_IDList += ds.Tables[0].Rows[i]["MCCONVERSATION_ID"].ToString() + ",";                    
                }

                sCONVERSATION_IDList = sCONVERSATION_IDList.Substring(0, sCONVERSATION_IDList.Length - 1);
            }

            if (sCONVERSATION_IDList != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75TIE669",  "", sCONVERSATION_IDList);

                string[] sMessage = this.DbConnector.ExecuteScalar(0).ToString().Split(',');

                if (sMessage[0].ToString() == "I")
                {
                    iBATCH_ID = Convert.ToInt64(Get_Numeric(sMessage[2]));

                    string sUrl = "http://192.168.100.32:10000/callSB_V3/XXSB_DTI_STATUS_CHANGE2.asp?BATCH_ID=" + iBATCH_ID.ToString() + "&STATUS=O&SIGNAL=CANCELALL&ID=" + fsSUP_COMID + "&PASS=" + fsSUP_COMPASS;

                    if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.UP_SMSBILL_TransResult(iBATCH_ID.ToString());                      
                    }
                }
                else
                {
                    this.ShowCustomMessage(sMessage[1], "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_34I9X524");

        }
        private void BTN61_BILL_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bResultSts = false;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_75PDO593.GetDataSourceInclude(TSpread.TActionType.Select, "MCGUBN", "MCDATE", "MCHWAJU", "MCBILLMAIL", "MCBILLSTATUS", "MCJPNO", "MCDANGAMT", "MCVATAMT", "MCTOTALAMT", "MCTAXSTATUS", "MCCONVERSATION_ID", "MCBIGO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //전송상태 체크
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_UT_75Q8A624", ds.Tables[0].Rows[i]["MCCONVERSATION_ID"].ToString());
                    //this.DbConnector.Attach("TY_P_UT_93RAY189", ds.Tables[0].Rows[i]["MCJPNO"].ToString(), ds.Tables[0].Rows[i]["MCCONVERSATION_ID"].ToString());
                    this.DbConnector.Attach("TY_P_UT_93RAY189", ds.Tables[0].Rows[i]["MCJPNO"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        //for (int j = 0; j < dt.Rows.Count; j++)
                        //{
                        //    string sDtiSts = dt.Rows[j]["DTI_STATUS"].ToString();
                        //    switch (sDtiSts)
                        //    {
                        //        case "A":
                        //            //sSTATUS = "전자(저장)";
                        //            bResultSts = true;
                        //            break;
                        //        case "S":
                        //            //sSTATUS = "전자(저장)";
                        //            bResultSts = true;
                        //            break;
                        //        case "I":
                        //            //sSTATUS = "수신미승인";
                        //            bResultSts = true;
                        //            break;
                        //        case "C":
                        //            //sSTATUS = "수신승인";
                        //            bResultSts = false;
                        //            break;
                        //        case "R":
                        //            //sSTATUS = "수신거부";
                        //            bResultSts = false;
                        //            break;
                        //        case "V":
                        //            //sSTATUS = "역발행요청";
                        //            bResultSts = true;
                        //            break;
                        //        case "W":
                        //            //sSTATUS = "역발행요청취소";
                        //            bResultSts = true;
                        //            break;
                        //        case "T":
                        //            //sSTATUS = "역발행요청거부";
                        //            bResultSts = true;
                        //            break;
                        //        case "N":
                        //            //sSTATUS = "공급자발행취소요청";
                        //            bResultSts = true;
                        //            break;
                        //        case "M":
                        //            //sSTATUS = "공급받는자발행취소요청";
                        //            bResultSts = true;
                        //            break;
                        //        case "O":
                        //            //sSTATUS = "취소완료";
                        //            bResultSts = true;
                        //            break;
                        //        default:
                        //            //sSTATUS = "XX";
                        //            bResultSts = true;
                        //            break;
                        //    }

                        //    if (bResultSts != true)
                        //    {
                        //        this.ShowMessage("TY_M_GB_34JA0536");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}

                        string sDtiSts = dt.Rows[0]["DTI_STATUS"].ToString();
                        switch (sDtiSts)
                        {
                            case "A":
                                //sSTATUS = "전자(저장)";
                                bResultSts = true;
                                break;
                            case "S":
                                //sSTATUS = "전자(저장)";
                                bResultSts = true;
                                break;
                            case "I":
                                //sSTATUS = "수신미승인";
                                bResultSts = true;
                                break;
                            case "C":
                                //sSTATUS = "수신승인";
                                bResultSts = false;
                                break;
                            case "R":
                                //sSTATUS = "수신거부";
                                bResultSts = false;
                                break;
                            case "V":
                                //sSTATUS = "역발행요청";
                                bResultSts = true;
                                break;
                            case "W":
                                //sSTATUS = "역발행요청취소";
                                bResultSts = true;
                                break;
                            case "T":
                                //sSTATUS = "역발행요청거부";
                                bResultSts = true;
                                break;
                            case "N":
                                //sSTATUS = "공급자발행취소요청";
                                bResultSts = true;
                                break;
                            case "M":
                                //sSTATUS = "공급받는자발행취소요청";
                                bResultSts = true;
                                break;
                            case "O":
                                //sSTATUS = "취소완료";
                                bResultSts = true;
                                break;
                            default:
                                //sSTATUS = "XX";
                                bResultSts = true;
                                break;
                        }

                        if (bResultSts != true)
                        {
                            this.ShowMessage("TY_M_GB_34JA0536");
                            e.Successed = false;
                            return;
                        }



                    }

                }
            }

            if (!this.ShowMessage("TY_M_GB_34I35533"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;


        }
        #endregion        

        #region  Description : 접안료 매출 발송
        private void UP_ProCess_ShipSales(DataTable dt, int i, string sBATCH_ID)
        {
            string sCONVERSATION_ID = string.Empty;
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75QB6627", dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());
            DataTable dsms = this.DbConnector.ExecuteDataTable();
            if (dsms.Rows.Count > 0)
            {                
                #region  Description : XXSB_DTI_MAIN 저장
                //공급자사업자번호(10)+공급받는자사업자번호(10)+계산서일자(8)+일련번호(4)+003
                sCONVERSATION_ID = fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString() + UP_Get_CONVERSATION_Seq(fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString()) + "003";
                DAT01_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                //매입(AP)/매출(AR)구분(
                DAT01_SUPBUY_TYPE.SetValue("AR");
                //정/역 구분
                DAT01_DIRECTION.SetValue("2");
                DAT01_INTERFACE_BATCH_ID.SetValue(sBATCH_ID);
                DAT01_DTI_WDATE.SetValue(dt.Rows[i]["MCDATE"].ToString().Substring(0, 4) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(4,2) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(6,2));
                //세금유형코드 : 01-과세, 02-면세, 03-영세율"
                DAT01_DTI_TYPE.SetValue(dsms.Rows[0]["DTI_TYPE"].ToString());
                if (dsms.Rows[0]["VNSAUPNO"].ToString() == "6018101970")
                {
                    DAT01_TAX_DEMAND.SetValue("01");
                }
                else
                {
                    DAT01_TAX_DEMAND.SetValue("18");
                }
                DAT01_SUP_COM_ID.SetValue(fsSUP_COMID);
                DAT01_SUP_COM_REGNO.SetValue(fsSUP_SAUPNO);
                DAT01_SUP_REP_NAME.SetValue(fsSUP_IRUM);
                DAT01_SUP_COM_NAME.SetValue(fsSUP_SANGHO);
                DAT01_SUP_COM_TYPE.SetValue(fsSUP_UPTE.Replace(",", ""));
                DAT01_SUP_COM_CLASSIFY.SetValue(fsSUP_UPJONG.Replace(",", ""));
                DAT01_SUP_COM_ADDR.SetValue(fsSUP_JUSO);

                //발행자 정보
                UP_Get_BillSendInfo();

                //DAT01_SUP_DEPT_NAME.SetValue("");
                //DAT01_SUP_EMP_NAME.SetValue("");
                //DAT01_SUP_TEL_NUM.SetValue("");
                //DAT01_SUP_EMAIL.SetValue("");

                DAT01_BYR_COM_REGNO.SetValue(dsms.Rows[0]["VNSAUPNO"].ToString());
                DAT01_BYR_REP_NAME.SetValue(dsms.Rows[0]["VNIRUM"].ToString());
                DAT01_BYR_COM_NAME.SetValue(dsms.Rows[0]["VNSANGHO"].ToString());
                DAT01_BYR_COM_TYPE.SetValue(dsms.Rows[0]["VNUPTE"].ToString());
                DAT01_BYR_COM_CLASSIFY.SetValue(dsms.Rows[0]["VNUPJONG"].ToString());
                DAT01_BYR_COM_ADDR.SetValue(dsms.Rows[0]["VNJUSO"].ToString());
                DAT01_BYR_DEPT_NAME.SetValue(dsms.Rows[0]["VNREDPMK"].ToString());
                DAT01_BYR_EMP_NAME.SetValue(dsms.Rows[0]["VNRENAME"].ToString());
                DAT01_BYR_TEL_NUM.SetValue(dsms.Rows[0]["VNRETEL"].ToString());

                //DAT01_BYR_EMAIL.SetValue(dsms.Rows[0]["VNREMAIL"].ToString());

                DAT01_BYR_EMAIL.SetValue(dt.Rows[i]["MCBILLMAIL"].ToString());

                DAT01_SUP_AMOUNT.SetValue(dsms.Rows[0]["JBAMT"].ToString());
                DAT01_TAX_AMOUNT.SetValue(dsms.Rows[0]["JBVAT"].ToString());
                DAT01_TOTAL_AMOUNT.SetValue(dsms.Rows[0]["JBTOTAL"].ToString());
                DAT01_DTT_YN.SetValue("Y");
                if (dt.Rows[i]["MCBIGO"].ToString() != "")
                {
                    DAT01_REMARK.SetValue(dt.Rows[i]["MCBIGO"].ToString());
                }
                else
                {
                    DAT01_REMARK.SetValue("접안료");
                }                
                DAT01_CREATED_BY.SetValue(TYUserInfo.EmpNo);
                DAT01_CREATION_DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                DAT01_AMEND_CODE.SetValue("");
                DAT01_ORI_ISSUE_ID.SetValue("");

                DAT01_ATTACHFILE_YN.SetValue("Y");
                if (dsms.Rows[0]["VNJGSPNO"].ToString() != "")
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue(Set_Fill4(dsms.Rows[0]["VNJGSPNO"].ToString()));
                }
                else
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue("");
                }

                DAT01_XDDATE.SetValue(dt.Rows[i]["MCDATE"].ToString());
                DAT01_XDHWAJU.SetValue(dsms.Rows[0]["JBHWAJU"].ToString());
                DAT01_XDMHGUBN.SetValue(dt.Rows[i]["MCGUBN"].ToString());
                DAT01_XDMJPNO.SetValue(dsms.Rows[0]["JBJPNO"].ToString());
                DAT01_XDCONVERSATION_ID.SetValue(DAT01_CONVERSATION_ID.GetValue().ToString());

                UP_DataCollections_Add("Main");

                #endregion

                #region  Description : XXSB_DTI_ITEM 저장
                DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                DAT02_DTI_LINE_NUM.SetValue("1");
                DAT02_ITEM_CODE.SetValue("");
                DAT02_ITEM_NAME.SetValue(dsms.Rows[0]["VSCDDESC1"].ToString());
                DAT02_ITEM_SIZE.SetValue("");
                DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                DAT02_UNIT_PRICE.SetValue("0");
                DAT02_ITEM_QTY.SetValue("0");

                DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["JBAMT"].ToString());
                DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["JBVAT"].ToString());
                DAT02_REMARK.SetValue("");
                DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                UP_DataCollections_Add("Item");
                #endregion

                #region  Description : XXSB_DTI_STATUS 저장
                this.DAT03_DTI_STATUS.SetValue("S");

                UP_DataCollections_Add("Sts");
                #endregion

                #region  Description : XXSB_DTI_INVOICE 저장
                this.DAT04_INVOICE_IDX.SetValue("1");
                this.DAT04_INVOICE_ID.SetValue("0");
                this.DAT04_INVOICE_NUM.SetValue(dsms.Rows[0]["JBJPNO"].ToString());

                UP_DataCollections_Add("Inv");
                #endregion

                //거래명세서 첨부
                UP_Set_InvoiceE_AttachFile(dt.Rows[i]["MCGUBN"].ToString(), dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());
            }
        }
        #endregion  

        #region  Description : 하역료 매출 발송
        private void UP_ProCess_LoadSales(DataTable dt, int i, string sBATCH_ID)
        {
            string sCONVERSATION_ID = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75UFE674", dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());
            DataTable dsms = this.DbConnector.ExecuteDataTable();
            if (dsms.Rows.Count > 0)
            {
                #region  Description : XXSB_DTI_MAIN 저장
                //공급자사업자번호(10)+공급받는자사업자번호(10)+계산서일자(8)+일련번호(4)+003
                sCONVERSATION_ID = fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString() + UP_Get_CONVERSATION_Seq(fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString()) + "003";

                DAT01_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                //매입(AP)/매출(AR)구분(
                DAT01_SUPBUY_TYPE.SetValue("AR");
                //정/역 구분
                DAT01_DIRECTION.SetValue("2");
                DAT01_INTERFACE_BATCH_ID.SetValue(sBATCH_ID);
                DAT01_DTI_WDATE.SetValue(dt.Rows[i]["MCDATE"].ToString().Substring(0, 4) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(6, 2));
                //세금유형코드 : 01-과세, 02-면세, 03-영세율"
                DAT01_DTI_TYPE.SetValue(dsms.Rows[0]["DTI_TYPE"].ToString());
                if (dsms.Rows[0]["VNSAUPNO"].ToString() == "6018101970")
                {
                    DAT01_TAX_DEMAND.SetValue("01");
                }
                else
                {
                    DAT01_TAX_DEMAND.SetValue("18");
                }
                DAT01_SUP_COM_ID.SetValue(fsSUP_COMID);
                DAT01_SUP_COM_REGNO.SetValue(fsSUP_SAUPNO);
                DAT01_SUP_REP_NAME.SetValue(fsSUP_IRUM);
                DAT01_SUP_COM_NAME.SetValue(fsSUP_SANGHO);
                DAT01_SUP_COM_TYPE.SetValue(fsSUP_UPTE.Replace(",", ""));
                DAT01_SUP_COM_CLASSIFY.SetValue(fsSUP_UPJONG.Replace(",", ""));
                DAT01_SUP_COM_ADDR.SetValue(fsSUP_JUSO);

                //발행자 정보
                UP_Get_BillSendInfo();

                //DAT01_SUP_DEPT_NAME.SetValue("");
                //DAT01_SUP_EMP_NAME.SetValue("");
                //DAT01_SUP_TEL_NUM.SetValue("");
                //DAT01_SUP_EMAIL.SetValue("");

                DAT01_BYR_COM_REGNO.SetValue(dsms.Rows[0]["VNSAUPNO"].ToString());
                DAT01_BYR_REP_NAME.SetValue(dsms.Rows[0]["VNIRUM"].ToString());
                DAT01_BYR_COM_NAME.SetValue(dsms.Rows[0]["VNSANGHO"].ToString());
                DAT01_BYR_COM_TYPE.SetValue(dsms.Rows[0]["VNUPTE"].ToString());
                DAT01_BYR_COM_CLASSIFY.SetValue(dsms.Rows[0]["VNUPJONG"].ToString());
                DAT01_BYR_COM_ADDR.SetValue(dsms.Rows[0]["VNJUSO"].ToString());
                DAT01_BYR_DEPT_NAME.SetValue(dsms.Rows[0]["VNREDPMK"].ToString());
                DAT01_BYR_EMP_NAME.SetValue(dsms.Rows[0]["VNRENAME"].ToString());
                DAT01_BYR_TEL_NUM.SetValue(dsms.Rows[0]["VNRETEL"].ToString());
                //DAT01_BYR_EMAIL.SetValue(dsms.Rows[0]["VNREMAIL"].ToString());

                DAT01_BYR_EMAIL.SetValue(dt.Rows[i]["MCBILLMAIL"].ToString());

                DAT01_SUP_AMOUNT.SetValue(dsms.Rows[0]["M1DANGAMT"].ToString());
                DAT01_TAX_AMOUNT.SetValue(dsms.Rows[0]["M1DANGVAT"].ToString());
                DAT01_TOTAL_AMOUNT.SetValue(dsms.Rows[0]["M1TOTALAMT"].ToString());
                DAT01_DTT_YN.SetValue("Y");                

                if (dt.Rows[i]["MCBIGO"].ToString() != "")
                {
                    DAT01_REMARK.SetValue(dt.Rows[i]["MCBIGO"].ToString());
                }
                else
                {
                    DAT01_REMARK.SetValue("하역료");
                }                                

                DAT01_CREATED_BY.SetValue(TYUserInfo.EmpNo);
                DAT01_CREATION_DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                DAT01_AMEND_CODE.SetValue("");
                DAT01_ORI_ISSUE_ID.SetValue("");

                DAT01_ATTACHFILE_YN.SetValue("Y");
                if (dsms.Rows[0]["VNJGSPNO"].ToString() != "")
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue(Set_Fill4(dsms.Rows[0]["VNJGSPNO"].ToString()));
                }
                else
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue("");
                }

                DAT01_XDDATE.SetValue(dt.Rows[i]["MCDATE"].ToString());
                DAT01_XDHWAJU.SetValue(dsms.Rows[0]["M1HWAJU"].ToString());
                DAT01_XDMHGUBN.SetValue(dt.Rows[i]["MCGUBN"].ToString());
                DAT01_XDMJPNO.SetValue(dsms.Rows[0]["M1JPNO"].ToString());
                DAT01_XDCONVERSATION_ID.SetValue(DAT01_CONVERSATION_ID.GetValue().ToString());


                UP_DataCollections_Add("Main");
                #endregion

                #region  Description : XXSB_DTI_ITEM 저장
                //라인 1
                DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                DAT02_DTI_LINE_NUM.SetValue("1");
                DAT02_ITEM_CODE.SetValue("");
                DAT02_ITEM_NAME.SetValue("하역수수료(하역)");
                DAT02_ITEM_SIZE.SetValue("");
                DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                DAT02_UNIT_PRICE.SetValue("0");
                DAT02_ITEM_QTY.SetValue("0");

                DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["M1DANGAMT"].ToString());
                DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["M1DANGVAT"].ToString());
                DAT02_REMARK.SetValue("");
                DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                UP_DataCollections_Add("Item");

                //라인 2
                DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                DAT02_DTI_LINE_NUM.SetValue("2");
                DAT02_ITEM_CODE.SetValue("");
                DAT02_ITEM_NAME.SetValue(Set_Date(dsms.Rows[0]["M1IPHANG"].ToString()) + dsms.Rows[0]["M1BONSUNNM"].ToString());
                DAT02_ITEM_SIZE.SetValue("");
                DAT02_ITEM_MD.SetValue("");
                DAT02_UNIT_PRICE.SetValue("0");
                DAT02_ITEM_QTY.SetValue("0");

                DAT02_SUP_AMOUNT.SetValue("0");
                DAT02_TAX_AMOUNT.SetValue("0");
                DAT02_REMARK.SetValue("");
                DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                UP_DataCollections_Add("Item");

                //라인 3
                DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                DAT02_DTI_LINE_NUM.SetValue("3");
                DAT02_ITEM_CODE.SetValue("");
                DAT02_ITEM_NAME.SetValue(dsms.Rows[0]["M1HWAMULNM"].ToString() + "외" + dsms.Rows[0]["M1ENIPGO"].ToString()+"M/T" );
                DAT02_ITEM_SIZE.SetValue("");
                DAT02_ITEM_MD.SetValue("");
                DAT02_UNIT_PRICE.SetValue("0");
                DAT02_ITEM_QTY.SetValue("0");

                DAT02_SUP_AMOUNT.SetValue("0");
                DAT02_TAX_AMOUNT.SetValue("0");
                DAT02_REMARK.SetValue("");
                DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                UP_DataCollections_Add("Item");

                #endregion

                #region  Description : XXSB_DTI_STATUS 저장
                this.DAT03_DTI_STATUS.SetValue("S");

                UP_DataCollections_Add("Sts");
                #endregion

                #region  Description : XXSB_DTI_INVOICE 저장
                this.DAT04_INVOICE_IDX.SetValue("1");
                this.DAT04_INVOICE_ID.SetValue("0");
                this.DAT04_INVOICE_NUM.SetValue(dsms.Rows[0]["M1JPNO"].ToString());

                UP_DataCollections_Add("Inv");
                #endregion

                //거래명세서 첨부
                UP_Set_InvoiceE_AttachFile(dt.Rows[i]["MCGUBN"].ToString(), dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());
            }
        }
        #endregion  

        #region  Description : 보관료 매출 발송
        private void UP_ProCess_StorageSales(DataTable dt, int i, string sBATCH_ID)
        {
            string sCONVERSATION_ID = string.Empty;
            int iLineNum = 0;


            this.DbConnector.CommandClear();
            
            // 원본(20171116)
            //this.DbConnector.Attach("TY_P_UT_767A3718", dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());

            this.DbConnector.Attach("TY_P_UT_7BGFK020", dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());
            DataTable dsms = this.DbConnector.ExecuteDataTable();
            if (dsms.Rows.Count > 0)
            {
                #region  Description : XXSB_DTI_MAIN 저장
                //공급자사업자번호(10)+공급받는자사업자번호(10)+계산서일자(8)+일련번호(4)+003
                sCONVERSATION_ID = fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString() + UP_Get_CONVERSATION_Seq(fsSUP_SAUPNO + dsms.Rows[0]["VNSAUPNO"].ToString() + dt.Rows[i]["MCDATE"].ToString()) + "003";

                DAT01_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                //매입(AP)/매출(AR)구분(
                DAT01_SUPBUY_TYPE.SetValue("AR");
                //정/역 구분
                DAT01_DIRECTION.SetValue("2");
                DAT01_INTERFACE_BATCH_ID.SetValue(sBATCH_ID);
                DAT01_DTI_WDATE.SetValue(dt.Rows[i]["MCDATE"].ToString().Substring(0, 4) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["MCDATE"].ToString().Substring(6, 2));
                //세금유형코드 : 01-과세, 02-면세, 03-영세율"
                DAT01_DTI_TYPE.SetValue(dsms.Rows[0]["DTI_TYPE"].ToString());
                if (dsms.Rows[0]["VNSAUPNO"].ToString() == "6018101970")
                {
                    DAT01_TAX_DEMAND.SetValue("01");
                }
                else
                {
                    DAT01_TAX_DEMAND.SetValue("18");
                }
                DAT01_SUP_COM_ID.SetValue(fsSUP_COMID);
                DAT01_SUP_COM_REGNO.SetValue(fsSUP_SAUPNO);
                DAT01_SUP_REP_NAME.SetValue(fsSUP_IRUM);
                DAT01_SUP_COM_NAME.SetValue(fsSUP_SANGHO);
                DAT01_SUP_COM_TYPE.SetValue(fsSUP_UPTE.Replace(",", ""));
                DAT01_SUP_COM_CLASSIFY.SetValue(fsSUP_UPJONG.Replace(",", ""));
                DAT01_SUP_COM_ADDR.SetValue(fsSUP_JUSO);

                //발행자 정보
                UP_Get_BillSendInfo();

                //DAT01_SUP_DEPT_NAME.SetValue("");
                //DAT01_SUP_EMP_NAME.SetValue("");
                //DAT01_SUP_TEL_NUM.SetValue("");
                //DAT01_SUP_EMAIL.SetValue("");

                DAT01_BYR_COM_REGNO.SetValue(dsms.Rows[0]["VNSAUPNO"].ToString());
                DAT01_BYR_REP_NAME.SetValue(dsms.Rows[0]["VNIRUM"].ToString());
                DAT01_BYR_COM_NAME.SetValue(dsms.Rows[0]["VNSANGHO"].ToString());
                DAT01_BYR_COM_TYPE.SetValue(dsms.Rows[0]["VNUPTE"].ToString());
                DAT01_BYR_COM_CLASSIFY.SetValue(dsms.Rows[0]["VNUPJONG"].ToString());
                DAT01_BYR_COM_ADDR.SetValue(dsms.Rows[0]["VNJUSO"].ToString());
                DAT01_BYR_DEPT_NAME.SetValue(dsms.Rows[0]["VNREDPMK"].ToString());
                DAT01_BYR_EMP_NAME.SetValue(dsms.Rows[0]["VNRENAME"].ToString());
                DAT01_BYR_TEL_NUM.SetValue(dsms.Rows[0]["VNRETEL"].ToString());
                //DAT01_BYR_EMAIL.SetValue(dsms.Rows[0]["VNREMAIL"].ToString());

                DAT01_BYR_EMAIL.SetValue(dt.Rows[i]["MCBILLMAIL"].ToString());

                DAT01_SUP_AMOUNT.SetValue(dsms.Rows[0]["M1DANGAMT"].ToString());
                DAT01_TAX_AMOUNT.SetValue(dsms.Rows[0]["M1DANGVAT"].ToString());
                DAT01_TOTAL_AMOUNT.SetValue(dsms.Rows[0]["M1TOTALAMT"].ToString());
                DAT01_DTT_YN.SetValue("Y");
                if (dt.Rows[i]["MCBIGO"].ToString() != "")
                {
                    DAT01_REMARK.SetValue(dt.Rows[i]["MCBIGO"].ToString());
                }
                else
                {
                    DAT01_REMARK.SetValue("보관료");
                }
                DAT01_CREATED_BY.SetValue(TYUserInfo.EmpNo);
                DAT01_CREATION_DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                DAT01_AMEND_CODE.SetValue("");
                DAT01_ORI_ISSUE_ID.SetValue("");

                DAT01_ATTACHFILE_YN.SetValue("Y");
                if (dsms.Rows[0]["VNJGSPNO"].ToString() != "")
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue(Set_Fill4(dsms.Rows[0]["VNJGSPNO"].ToString()));
                }
                else
                {
                    DAT01_BYR_BIZPLACE_CODE.SetValue("");
                }

                DAT01_XDDATE.SetValue(dt.Rows[i]["MCDATE"].ToString());
                DAT01_XDHWAJU.SetValue(dsms.Rows[0]["M1HWAJU"].ToString());
                DAT01_XDMHGUBN.SetValue(dt.Rows[i]["MCGUBN"].ToString());
                DAT01_XDMJPNO.SetValue(dsms.Rows[0]["M1JPNO"].ToString());
                DAT01_XDCONVERSATION_ID.SetValue(DAT01_CONVERSATION_ID.GetValue().ToString());


                UP_DataCollections_Add("Main");
                #endregion

                #region  Description : XXSB_DTI_ITEM 저장                               

                //라인 1
                //보관료
                if ( Convert.ToDouble(dsms.Rows[0]["BOGANAMT"].ToString()) > 0)
                {
                    iLineNum += 1;

                    DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                    DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                    DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                    DAT02_DTI_LINE_NUM.SetValue(iLineNum.ToString());
                    DAT02_ITEM_CODE.SetValue("");
                    DAT02_ITEM_NAME.SetValue("보관료");
                    DAT02_ITEM_SIZE.SetValue("");
                    DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                    DAT02_UNIT_PRICE.SetValue("0");
                    DAT02_ITEM_QTY.SetValue("0");

                    DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["BOGANAMT"].ToString());
                    DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["BOGANVAT"].ToString());
                    DAT02_REMARK.SetValue("");
                    DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                    UP_DataCollections_Add("Item");
                }

                //라인 2
                //취급료
                if (Convert.ToDouble(dsms.Rows[0]["CHWIKEUPAMT"].ToString()) > 0)
                {
                    iLineNum += 1;

                    DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                    DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                    DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                    DAT02_DTI_LINE_NUM.SetValue(iLineNum.ToString());
                    DAT02_ITEM_CODE.SetValue("");
                    DAT02_ITEM_NAME.SetValue("취급료");
                    DAT02_ITEM_SIZE.SetValue("");
                    DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                    DAT02_UNIT_PRICE.SetValue("0");
                    DAT02_ITEM_QTY.SetValue("0");

                    DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["CHWIKEUPAMT"].ToString());
                    DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["CHWIKEUPVAT"].ToString());
                    DAT02_REMARK.SetValue("");
                    DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                    UP_DataCollections_Add("Item");
                }

                //라인 3
                //할증료
                if (Convert.ToDouble(dsms.Rows[0]["HALJEUNGAMT"].ToString()) > 0)
                {
                    iLineNum += 1;

                    DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                    DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                    DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                    DAT02_DTI_LINE_NUM.SetValue(iLineNum.ToString());
                    DAT02_ITEM_CODE.SetValue("");
                    DAT02_ITEM_NAME.SetValue("할증료");
                    DAT02_ITEM_SIZE.SetValue("");
                    DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                    DAT02_UNIT_PRICE.SetValue("0");
                    DAT02_ITEM_QTY.SetValue("0");

                    DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["HALJEUNGAMT"].ToString());
                    DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["HALJEUNGVAT"].ToString());
                    DAT02_REMARK.SetValue("");
                    DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                    UP_DataCollections_Add("Item");
                }

                //라인 4
                //유틸리티，기타
                if (Convert.ToDouble(dsms.Rows[0]["UTILAMT"].ToString()) > 0)
                {
                    iLineNum += 1;

                    DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                    DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                    DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                    DAT02_DTI_LINE_NUM.SetValue(iLineNum.ToString());
                    DAT02_ITEM_CODE.SetValue("");
                    DAT02_ITEM_NAME.SetValue("유틸리티，기타");
                    DAT02_ITEM_SIZE.SetValue("");
                    DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                    DAT02_UNIT_PRICE.SetValue("0");
                    DAT02_ITEM_QTY.SetValue("0");

                    DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["UTILAMT"].ToString());
                    DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["UTILVAT"].ToString());
                    DAT02_REMARK.SetValue("");
                    DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                    UP_DataCollections_Add("Item");
                    
                }

                // 20171116 추가
                //라인 5
                // 토지사용료
                if (Convert.ToDouble(dsms.Rows[0]["TOJIAMT"].ToString()) > 0)
                {
                    iLineNum += 1;

                    DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
                    DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
                    DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
                    DAT02_DTI_LINE_NUM.SetValue(iLineNum.ToString());
                    DAT02_ITEM_CODE.SetValue("");
                    DAT02_ITEM_NAME.SetValue("토지사용료");
                    DAT02_ITEM_SIZE.SetValue("");
                    DAT02_ITEM_MD.SetValue(dt.Rows[i]["MCDATE"].ToString());
                    DAT02_UNIT_PRICE.SetValue("0");
                    DAT02_ITEM_QTY.SetValue("0");

                    DAT02_SUP_AMOUNT.SetValue(dsms.Rows[0]["TOJIAMT"].ToString());
                    DAT02_TAX_AMOUNT.SetValue(dsms.Rows[0]["TOJIVAT"].ToString());
                    DAT02_REMARK.SetValue("");
                    DAT02_CREATED_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    DAT02_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());

                    UP_DataCollections_Add("Item");

                }

                #endregion

                #region  Description : XXSB_DTI_STATUS 저장
                this.DAT03_DTI_STATUS.SetValue("S");

                UP_DataCollections_Add("Sts");
                #endregion

                #region  Description : XXSB_DTI_INVOICE 저장
                this.DAT04_INVOICE_IDX.SetValue("1");
                this.DAT04_INVOICE_ID.SetValue("0");
                this.DAT04_INVOICE_NUM.SetValue(dsms.Rows[0]["M1JPNO"].ToString());

                UP_DataCollections_Add("Inv");
                #endregion


                //거래명세서 첨부
                UP_Set_InvoiceE_AttachFile(dt.Rows[i]["MCGUBN"].ToString(), dt.Rows[i]["MCDATE"].ToString(), dt.Rows[i]["MCHWAJU"].ToString(), dt.Rows[i]["MCJPNO"].ToString());


            }
        }
        #endregion  
  
        #region  Description : 거래명세서 첨부 
        private void UP_Set_InvoiceE_AttachFile(string sMCGUBN, string sMCDATE, string sMCHWAJU, string sMCJPNO)
        {
            //거래명세서 생성            
            this.DbConnector.CommandClear();

            if (sMCGUBN == "01")
            {
                //접안료
                this.DbConnector.Attach("TY_P_UT_773DB999", sMCDATE, sMCDATE, sMCHWAJU, sMCJPNO);
            }
            else if (sMCGUBN == "03")
            {
                //하역료
                this.DbConnector.Attach("TY_P_UT_773FP001", sMCDATE.Substring(0, 6), sMCHWAJU, sMCDATE, sMCHWAJU, sMCJPNO);
            }
            else if (sMCGUBN == "05")
            {
                //보관취급료
                this.DbConnector.Attach("TY_P_UT_773GE002", sMCDATE.Substring(0, 6), sMCHWAJU, sMCDATE, sMCHWAJU, sMCJPNO);
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new ActiveReport();

                if (sMCGUBN == "01")
                {
                    //// 접안료 현재요율이 적용되는 SQL문
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_UT_73KCS987",
                    //    Get_Date(sMCDATE.Replace("-",""))
                    //    );

                    //DataTable dtDG = this.DbConnector.ExecuteDataTable();

                    rpt = new TYUTME013R();
                }
                else if (sMCGUBN == "03")
                {
                    if (TYUserInfo.EmpNo.ToString() == "0391-F" || TYUserInfo.EmpNo.ToString() == "0185-M" ||
                        TYUserInfo.EmpNo.ToString() == "0280-M")
                    {

                        if (dt.Rows.Count > 0)
                        {
                            rpt = new TYUTME017R2();
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            rpt = new TYUTME017R1();
                        }
                    }
                }
                else if (sMCGUBN == "05")
                {
                    if (TYUserInfo.EmpNo.ToString() == "0391-F" || TYUserInfo.EmpNo.ToString() == "0185-M" ||
                       TYUserInfo.EmpNo.ToString() == "0280-M")
                    {

                        if (dt.Rows.Count > 0)
                        {
                            rpt = new TYUTME022R2();
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            rpt = new TYUTME022R1();
                        }
                    }
                }                

                UP_Invoice_PdfFileDown(rpt, dt, sMCGUBN,  DAT01_CONVERSATION_ID.GetValue().ToString() + ".pdf");

                rpt.Dispose();
            }

            //거래명세서 파일 첨부
            string sFileName = DAT01_CONVERSATION_ID.GetValue().ToString() + ".pdf";

            byte[] _AttachFile = null;

            object _objAttachFile = null;

            string filePath = fsFileDownPath + sMCGUBN + "\\"+ sFileName;

            //파일 존재 체크
            if (System.IO.File.Exists(filePath))
            {
                _AttachFile = UP_Get_Byte(filePath);

                _objAttachFile = _AttachFile;

                int ArraySize = _AttachFile.GetUpperBound(0);

                DAT05_EVENT_ID.SetValue(UP_Get_EventID());

                UP_DataCollections_Add("Msg");

                DataRow rw;
                rw = ftInvocieTable.NewRow();
                rw["CONVERSATION_ID"] = DAT01_CONVERSATION_ID.GetValue().ToString();
                rw["FILE_SEQ"] = "1";
                rw["EVENT_ID"] = DAT05_EVENT_ID.GetValue().ToString();
                rw["FILE_NAME"] = sFileName;
                rw["FILE_SAVE_TYPE"] = "DB";
                rw["FILE_SIZE"] = ArraySize.ToString();
                rw["FILE_BINARY"] = _objAttachFile;
                rw["FILE_STATUS"] = "TRANSFER_REQUEST";
                rw["CREATION_BY"] = DAT01_CREATED_BY.GetValue().ToString();
                rw["CREATION_DATE"] = DAT01_CREATION_DATE.GetValue().ToString();
                rw["LAST_UPDATE_BY"] = DAT01_CREATED_BY.GetValue().ToString();
                rw["LAST_UPDATE_DATE"] = DAT01_CREATION_DATE.GetValue().ToString();
                rw["IN_OUT"] = "OUT";

                ftInvocieTable.Rows.Add(rw);
            }
        }
        #endregion       

        #region  Description : 스마트빌 전자세금계산서 처리 함수 모음

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

            #region  Description : 스마빌 전송코드별 상태명 확인
            private string UP_SMSBILL_CODENAME(string sCODE)
            {

                string sSTATUS = string.Empty;

                switch (sCODE)
                {
                    case "FS":
                        sSTATUS = "전자(저장)";
                        break;
                    case "FD":
                        sSTATUS = "전자(미발급)";
                        break;
                    case "FT":
                        sSTATUS = "전자(발급)";
                        break;
                    case "FA":
                        sSTATUS = "전자(확인)";
                        break;
                    case "FC":
                        sSTATUS = "전자(취소)";
                        break;
                    case "FR":
                        sSTATUS = "전자(정정요청)";
                        break;
                    case "RT":
                        sSTATUS = "전자(신규(역))";
                        break;
                    case "RA":
                        sSTATUS = "전자(접수(역))";
                        break;
                    case "RR":
                        sSTATUS = "전자(재요청(역))";
                        break;
                    case "FX":
                        sSTATUS = "전자(삭제)";
                        break;
                    case "DT":
                        sSTATUS = "전자(삭제)";
                        break;
                    case "ER":
                        sSTATUS = "전자(실패)";
                        break;
                    case "A":
                        sSTATUS = "전자(저장)";
                        break;
                    case "S":
                        sSTATUS = "전자(저장)";
                        break;
                    case "I":
                        sSTATUS = "수신미승인";
                        break;
                    case "C":
                        sSTATUS = "수신승인";
                        break;
                    case "R":
                        sSTATUS = "수신거부";
                        break;
                    case "V":
                        sSTATUS = "역발행요청";
                        break;
                    case "W":
                        sSTATUS = "역발행요청취소";
                        break;
                    case "T":
                        sSTATUS = "역발행요청거부";
                        break;
                    case "N":
                        sSTATUS = "공급자발행취소요청";
                        break;
                    case "M":
                        sSTATUS = "공급받는자발행취소요청";
                        break;
                    case "O":
                        sSTATUS = "취소완료";
                        break;
                    default:
                        sSTATUS = "수기";
                        break;
                }



                return sSTATUS;
            }
            #endregion

            #region  Description : CONVERSATION 일련번호 조회
            private string UP_Get_CONVERSATION_Seq(string sCONVERSATION)
            {
                string sChkCONVERSATION = string.Empty;
                string sChkSeq = string.Empty;
                string sSeq = string.Empty;

                if (data_Main.Count > 0)
                {
                    for (int i = 0; i < data_Main.Count; i++)
                    {
                        sChkCONVERSATION = data_Main[i][0].ToString().Substring(0,28);

                        if (sChkCONVERSATION == sCONVERSATION)
                        {
                            sChkSeq = data_Main[i][0].ToString().Substring(28, 4);
                        }
                    }
                }

                if (sChkSeq == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_75QC0629", sCONVERSATION);
                    sSeq = this.DbConnector.ExecuteScalar().ToString();
                }
                else
                {
                    sSeq = Set_Fill4((Convert.ToInt16(sChkSeq) + 1).ToString());
                }
                
                return sSeq;
            }
            #endregion

            #region  Description : BATCH_ID 조회
            private string UP_Get_BATCH_ID()
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75QCU630");
                string sBATCH_ID = this.DbConnector.ExecuteScalar().ToString();

                return sBATCH_ID;
            }
            #endregion

            #region  Description : 공급자 사업자정보 조회
            private void UP_Get_SupInfo()
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_244BN404", "", "6108110449", "");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    fsSUP_SAUPNO = dt.Rows[0]["VNSAUPNO"].ToString();
                    fsSUP_IRUM = dt.Rows[0]["VNIRUM"].ToString();
                    fsSUP_JUSO = dt.Rows[0]["VNJUSO"].ToString();
                    fsSUP_SANGHO = dt.Rows[0]["VNSANGHO"].ToString();
                    fsSUP_UPJONG = dt.Rows[0]["VNUPJONG"].ToString();
                    fsSUP_UPTE = dt.Rows[0]["VNUPTE"].ToString();
                }
            }
            #endregion

            #region  Description : 발행자 정보 조회
            private void UP_Get_BillSendInfo()
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BBGV367", "", _CBH01_KBSABUN_Value, TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    DAT01_SUP_DEPT_NAME.SetValue(dt.Rows[0]["KBBSTEAMNM"].ToString());
                    DAT01_SUP_EMP_NAME.SetValue(dt.Rows[0]["KBHANGL"].ToString());
                    DAT01_SUP_TEL_NUM.SetValue(dt.Rows[0]["KBCOMTEL"].ToString());
                    DAT01_SUP_EMAIL.SetValue(dt.Rows[0]["KBMAILID"].ToString() + "@taeyoung.co.kr");
                }
            }
            #endregion

            #region  Description : 첨부파일 EVENTID 조회
            private string UP_Get_EventID()
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75U9J671");
                string sEvnentid = this.DbConnector.ExecuteScalar().ToString();

                return sEvnentid;
            }
            #endregion

        #endregion

        #region  Description : 계산서 사이트 전송 / 수신 함수
        //Description : 계산서 사이트 전송
        
        private void UP_SMSBILL_WebServiceCall(string sBATCH_ID)
        {
            string sUrl = "http://192.168.100.32:10000/callSB_V3/XXSB_DTI_ARISSUE2.asp?BATCH_ID=" + sBATCH_ID.ToString() + "&ID=" + fsSUP_COMID + "&PASS=" + fsSUP_COMPASS;

            if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_SMSBILL_TransResult(sBATCH_ID);
            }
        }       

        // Description : 계산서 사이트 결과 처리 함수
        private void UP_SMSBILL_TransResult(string sBATCH_ID)
        {
            Int16 iOKCNT = 0;
            Int16 iERCNT = 0;
          
                //전송결과 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75QH9644", sBATCH_ID);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[0]["DTI_STATUS"].ToString().Trim() == "I" && dt.Rows[0]["RETURN_CODE"].ToString().Trim() == "30000")
                        {
                            iOKCNT += 1;

                        }
                        else
                        {
                            iERCNT += 1;
                        }
                    }
                    //this.ShowCustomMessage("성공:" + iOKCNT.ToString() + "건  실패:" + iERCNT.ToString() + "건이 처리 되었습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }           
        }
        #endregion

        #region  Description : 거래명세서 파일 첨부관련 함수 모음     

        // Description : 거래명세서 파일 경로에 찾아서 byte 변환 함수
        private object UP_Find_InvFileObject(string sCONVERSATION_ID)
        {
            object _objAttachFile = null;
            byte[] _AttachFile = null;
            string sFileName = string.Empty;
            int iPoint = 0;

            string sSYSDATE = DateTime.Now.ToString("yyyyMMdd");
            string sPath = "C:\\Invoice\\" + sSYSDATE + "\\";

            sCONVERSATION_ID = sCONVERSATION_ID + ".pdf";
            
            if (Directory.Exists(sPath))
            {
                string[] sFile = Directory.GetFiles(sPath);

                    for (int i = 0; i < sFile.Length; i++)
                    {
                        iPoint = sFile[i].ToString().IndexOf(sSYSDATE);
                        iPoint += 9;

                        sFileName = sFile[i].ToString().Substring(iPoint, sFile[i].Length - iPoint);

                        if (sCONVERSATION_ID == sFileName)
                        {
                            _AttachFile = UP_Get_Byte(sFile[i]);

                            _objAttachFile = _AttachFile;

                            return _objAttachFile;     
                        }
                    }
            }

            return _objAttachFile;                 
        }       

        //  Description : 거래명세서 파일 다운 함수
        private void UP_Invoice_PdfFileDown(ActiveReport rpt, DataTable dt, string sMCGUBN,  string sFileName)
        {
            DataDynamics.ActiveReports.Document.Document doc;

            try
            {

                if (Directory.Exists(fsFileDownPath + sMCGUBN+"\\") == false)
                {
                    Directory.CreateDirectory(fsFileDownPath+sMCGUBN+"\\");
                }

                

                rpt.DataSource = dt;
                rpt.Run(false);

                string sfilename = fsFileDownPath +sMCGUBN+"\\"+sFileName;

                object export = null;

                doc = rpt.Document;

                export = new PdfExport();

                ((PdfExport)export).Export(doc, sfilename);                
                
            }
            catch
            {

            }
            finally
            {

            }
           
        }        
        #endregion

        #region  Description :  UP_DataCollections_Add 이벤트
        private void UP_DataCollections_Add(string sGubn)
        {
            if (sGubn == "Main")
            {
                data_UTIMain.Add(new object[] {
                    DAT01_XDDATE.GetValue(),
                    DAT01_XDHWAJU.GetValue(),
                    DAT01_XDMHGUBN.GetValue(),
                    DAT01_XDMJPNO.GetValue(),
                    DAT01_XDCONVERSATION_ID.GetValue(),
                    TYUserInfo.EmpNo
                  });
          
                data_Main.Add(new object[] {
                DAT01_CONVERSATION_ID.GetValue(),
                DAT01_SUPBUY_TYPE.GetValue(),
                DAT01_DIRECTION.GetValue(),
                DAT01_INTERFACE_BATCH_ID.GetValue(),
                DAT01_DTI_WDATE.GetValue(),
                DAT01_DTI_TYPE.GetValue(),
                DAT01_TAX_DEMAND.GetValue(),
                DAT01_SUP_COM_ID.GetValue(),
                DAT01_SUP_COM_REGNO.GetValue(),
                DAT01_SUP_REP_NAME.GetValue(),
                DAT01_SUP_COM_NAME.GetValue(),
                DAT01_SUP_COM_TYPE.GetValue(),
                DAT01_SUP_COM_CLASSIFY.GetValue(),
                DAT01_SUP_COM_ADDR.GetValue(),
                DAT01_SUP_DEPT_NAME.GetValue(),
                DAT01_SUP_EMP_NAME.GetValue(),
                DAT01_SUP_TEL_NUM.GetValue(),
                DAT01_SUP_EMAIL.GetValue(),
                DAT01_BYR_COM_REGNO.GetValue(),
                DAT01_BYR_REP_NAME.GetValue(),
                DAT01_BYR_COM_NAME.GetValue(),
                DAT01_BYR_COM_TYPE.GetValue(),
                DAT01_BYR_COM_CLASSIFY.GetValue(),
                DAT01_BYR_COM_ADDR.GetValue(),
                DAT01_BYR_DEPT_NAME.GetValue(),
                DAT01_BYR_EMP_NAME.GetValue(),
                DAT01_BYR_TEL_NUM.GetValue(),
                DAT01_BYR_EMAIL.GetValue(),
                DAT01_SUP_AMOUNT.GetValue(),
                DAT01_TAX_AMOUNT.GetValue(),
                DAT01_TOTAL_AMOUNT.GetValue(),
                DAT01_DTT_YN.GetValue(),
                DAT01_REMARK.GetValue(),
                DAT01_CREATED_BY.GetValue(),
                DAT01_CREATION_DATE.GetValue(),
                DAT01_AMEND_CODE.GetValue(),
                DAT01_ORI_ISSUE_ID.GetValue(),
                DAT01_ATTACHFILE_YN.GetValue(),
                DAT01_BYR_BIZPLACE_CODE.GetValue()
                });
            }

            //Item
            if (sGubn == "Item")
            {
                data_Item.Add(new object[] {
                    DAT02_CONVERSATION_ID.GetValue(),
                    DAT02_SUPBUY_TYPE.GetValue(),
                    DAT02_DIRECTION.GetValue(),
                    DAT02_DTI_LINE_NUM.GetValue(),
                    DAT02_ITEM_CODE.GetValue(),
                    DAT02_ITEM_NAME.GetValue(),
                    DAT02_ITEM_SIZE.GetValue(),
                    DAT02_ITEM_MD.GetValue(),
                    DAT02_UNIT_PRICE.GetValue(),
                    DAT02_ITEM_QTY.GetValue(),
                    DAT02_SUP_AMOUNT.GetValue(),
                    DAT02_TAX_AMOUNT.GetValue(),
                    DAT02_REMARK.GetValue(),
                    DAT02_CREATED_BY.GetValue(),
                    DAT02_CREATION_DATE.GetValue()
                });
            }


            if (sGubn == "Sts")
            {
                data_Sts.Add(new object[] {
                    DAT01_CONVERSATION_ID.GetValue(),
                    DAT01_SUPBUY_TYPE.GetValue(),
                    DAT01_DIRECTION.GetValue(),
                    DAT03_DTI_STATUS.GetValue(),
                    DAT01_CREATED_BY.GetValue(),
                    DAT01_CREATION_DATE.GetValue()
               });
            }

            if (sGubn == "Inv")
            {
                data_Inv.Add(new object[] {
                    DAT01_CONVERSATION_ID.GetValue(),
                    DAT01_SUPBUY_TYPE.GetValue(),
                    DAT01_DIRECTION.GetValue(),
                    DAT04_INVOICE_IDX.GetValue(),
                    DAT04_INVOICE_ID.GetValue(),
                    DAT04_INVOICE_NUM.GetValue()
               });
            }


            if (sGubn == "Msg")
            {
                data_msg.Add(new object[] {
                    DAT05_EVENT_ID.GetValue(),
                    "DTI",
                    "N",
                    "OUT",
                    DAT01_SUP_COM_REGNO.GetValue(),
                    DAT01_SUP_COM_ID.GetValue(),
                    DAT01_CREATED_BY.GetValue(),
                    DAT01_CREATION_DATE.GetValue(),
                    DAT01_CREATED_BY.GetValue(),
                    DAT01_CREATION_DATE.GetValue()
               });
            }

            

        }
        #endregion

        #region  Description : UP_SetDataList_Clear 이벤트
        private void UP_SetDataList_Clear()
        {
            data_Main.Clear();
            data_Item.Clear();
            data_Inv.Clear();
            data_Sts.Clear();
            data_msg.Clear();
            data_UTIMain.Clear();
        }
        #endregion        

        #region  Description : 기본 조회 DataTable, 거래명세서 첨부 테이블 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("MCGUBN", typeof(System.String));
            dt.Columns.Add("MCDATE", typeof(System.String));
            dt.Columns.Add("MCHWAJU", typeof(System.String));
            dt.Columns.Add("MCHWAJUNM", typeof(System.String));
            dt.Columns.Add("MCBILLMAIL", typeof(System.String));
            dt.Columns.Add("MCBILLSTATUS", typeof(System.String));
            dt.Columns.Add("MCBILLSTATUSNM", typeof(System.String));
            dt.Columns.Add("MCJPNO", typeof(System.String));
            dt.Columns.Add("MCDANGAMT", typeof(System.Double));
            dt.Columns.Add("MCVATAMT", typeof(System.Double));
            dt.Columns.Add("MCTOTALAMT", typeof(System.Double));
            dt.Columns.Add("MCTAXSTATUS", typeof(System.String));
            dt.Columns.Add("MCBIGO", typeof(System.String));
            dt.Columns.Add("MCCONVERSATION_ID", typeof(System.String));
            dt.Columns.Add("MCINV", typeof(System.String));

            dt.TableName = "TableNames";

            return dt;
        }      
        private DataTable UP_Set_InvocieFileAttachTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("CONVERSATION_ID", typeof(System.String));
            dt.Columns.Add("FILE_SEQ", typeof(System.String));
            dt.Columns.Add("EVENT_ID", typeof(System.String));
            dt.Columns.Add("FILE_NAME", typeof(System.String));
            dt.Columns.Add("FILE_SAVE_TYPE", typeof(System.String));
            dt.Columns.Add("FILE_SIZE", typeof(System.String));
            dt.Columns.Add("FILE_BINARY", typeof(System.Object));
            dt.Columns.Add("FILE_STATUS", typeof(System.String));
            dt.Columns.Add("CREATION_BY", typeof(System.String));
            dt.Columns.Add("CREATION_DATE", typeof(System.String));
            dt.Columns.Add("LAST_UPDATE_BY", typeof(System.String));
            dt.Columns.Add("LAST_UPDATE_DATE", typeof(System.String));
            dt.Columns.Add("IN_OUT", typeof(System.String));
            
            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region  Description : 메일 재발송 처리 팝업 이벤트
        private void reateMail_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string sBILLSTATUS = this.FPS91_TY_S_UT_75PDO593.GetValue("MCBILLSTATUS").ToString();
            string sCONVERSATION_ID = this.FPS91_TY_S_UT_75PDO593.GetValue("MCCONVERSATION_ID").ToString();
            string sMail = this.FPS91_TY_S_UT_75PDO593.GetValue("MCBILLMAIL").ToString();

            if (sBILLSTATUS == "I" || sBILLSTATUS == "C" || sBILLSTATUS == "V" || sBILLSTATUS == "N")
            {
                if ((new TYUTME24C1(sBILLSTATUS, sCONVERSATION_ID, sMail, fsSUP_COMID, fsSUP_COMPASS
                         )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else
            {
                this.ShowCustomMessage("수신미승인(I),수신승인(C) 건에 대해서만 메일 재 발송이 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return; 

            }          
        }
        #endregion

        #region  Description : 스마트빌 전송 이력 조회 팝업
        private void FPS91_TY_S_UT_75PDO593_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 15)
            {
                if ((new TYUTME24C2(this.FPS91_TY_S_UT_75PDO593.GetValue("MCDATE").ToString(), this.FPS91_TY_S_UT_75PDO593.GetValue("MCHWAJU").ToString(), this.FPS91_TY_S_UT_75PDO593.GetValue("MCJPNO").ToString(), this.FPS91_TY_S_UT_75PDO593.GetValue("MCCONVERSATION_ID").ToString()
                            )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else if (e.Column == 16)
            {               
                if (this.OpenModalPopup(new TYUTME24C3(this.FPS91_TY_S_UT_75PDO593.GetValue("MCCONVERSATION_ID").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description :  스마트빌 전송 상태별 색깔 선정
        private Color UP_StatusTextColor(string sValue)
        {
            Color _Color;

            switch (sValue)
            {
                case "A":
                    //sSTATUS = "전자(저장)";
                    _Color = Color.Black;
                    break;
                case "S":
                    //sSTATUS = "전자(저장)";
                    _Color = Color.Black;
                    break;
                case "I":
                    //sSTATUS = "수신미승인";
                    _Color = Color.Blue;
                    break;
                case "C":
                    //sSTATUS = "수신승인";
                    _Color = Color.Red;
                    break;
                case "R":
                    //sSTATUS = "수신거부";
                    _Color = Color.Black;
                    break;
                case "V":
                    //sSTATUS = "역발행요청";
                    _Color = Color.Black;
                    break;
                case "W":
                    //sSTATUS = "역발행요청취소";
                    _Color = Color.Black;
                    break;
                case "T":
                    //sSTATUS = "역발행요청거부";
                    _Color = Color.Black;
                    break;
                case "N":
                    //sSTATUS = "공급자발행취소요청";
                    _Color = Color.Black;
                    break;
                case "M":
                    //sSTATUS = "공급받는자발행취소요청";
                    _Color = Color.Black;
                    break;
                case "O":
                    //sSTATUS = "취소완료";
                    _Color = Color.Black;
                    break;
                default:
                    //sSTATUS = "XX";
                    _Color = Color.Black;
                    break;
            }

            return _Color;
        }
        #endregion

        #region  Description : InvokerStart, InvokerEnd 이벤트
        private void BTN61_BILL_SEND_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            //string sCONVERSATION_ID = string.Empty;
            //string sBATCH_ID = string.Empty;

            //ftInvocieTable.Clear();

            //UP_SetDataList_Clear();

            //UP_Get_SupInfo();

            ////DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //DataSet ds = fsds;

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    //세금계산서 
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            sBATCH_ID = UP_Get_BATCH_ID();
            //        }
            //        if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "01") //접안료
            //        {
            //            UP_ProCess_ShipSales(ds.Tables[0], i, sBATCH_ID);
            //        }
            //        else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "03") //하역료
            //        {
            //            UP_ProCess_LoadSales(ds.Tables[0], i, sBATCH_ID);
            //        }
            //        else if (ds.Tables[0].Rows[i]["MCGUBN"].ToString() == "05") //보관취급료
            //        {
            //            UP_ProCess_StorageSales(ds.Tables[0], i, sBATCH_ID);
            //        }
            //    }

            //    if (data_UTIMain.Count > 0)
            //    {
            //        e.DbConnector.CommandClear();
            //        foreach (object[] data in data_UTIMain)
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75QG2637", data);
            //        }
            //        e.DbConnector.ExecuteTranQueryList();
            //    }

            //    if (data_Main.Count > 0)
            //    {

            //        e.DbConnector.CommandClear();

            //        foreach (object[] data in data_Main)  //XXSB_DTI_MAIN 
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75QE2632", data);
            //        }
            //        foreach (object[] data in data_Item) //XXSB_DTI_ITEM 
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75QEB633", data);
            //        }
            //        foreach (object[] data in data_Sts) //XXSB_DTI_STATUS 
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75QGE638", data);
            //        }
            //        foreach (object[] data in data_Inv) //XXSB_DTI_INVOICE 
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75QGJ639", data);
            //        }
            //        foreach (object[] data in data_msg) //XXSB_DELIVERY_EVENT_MSG
            //        {
            //            e.DbConnector.Attach("TY_P_UT_75TDF654", data);
            //        }

            //        if (ftInvocieTable.Rows.Count > 0)  //XXSB_DELIVERY_DTI_FILE 
            //        {
            //            for (int i = 0; i < ftInvocieTable.Rows.Count; i++)
            //            {
            //                DAT05_CONVERSATION_ID.SetValue(ftInvocieTable.Rows[i]["CONVERSATION_ID"].ToString());
            //                DAT05_FILE_SEQ.SetValue(ftInvocieTable.Rows[i]["FILE_SEQ"].ToString());
            //                DAT05_EVENT_ID.SetValue(ftInvocieTable.Rows[i]["EVENT_ID"].ToString());
            //                DAT05_FILE_NAME.SetValue(ftInvocieTable.Rows[i]["FILE_NAME"].ToString());
            //                DAT05_FILE_SAVE_TYPE.SetValue(ftInvocieTable.Rows[i]["FILE_SAVE_TYPE"].ToString());
            //                DAT05_FILE_SIZE.SetValue(ftInvocieTable.Rows[i]["FILE_SIZE"].ToString());
            //                DAT05_FILE_BINARY.SetValue(ftInvocieTable.Rows[i]["FILE_BINARY"]);
            //                DAT05_FILE_STATUS.SetValue(ftInvocieTable.Rows[i]["FILE_STATUS"].ToString());
            //                DAT05_CREATION_BY.SetValue(ftInvocieTable.Rows[i]["CREATION_BY"].ToString());
            //                DAT05_CREATION_DATE.SetValue(ftInvocieTable.Rows[i]["CREATION_DATE"].ToString());
            //                DAT05_LAST_UPDATE_BY.SetValue(ftInvocieTable.Rows[i]["LAST_UPDATE_BY"].ToString());
            //                DAT05_LAST_UPDATE_DATE.SetValue(ftInvocieTable.Rows[i]["LAST_UPDATE_DATE"].ToString());
            //                DAT05_IN_OUT.SetValue(ftInvocieTable.Rows[i]["IN_OUT"].ToString());

            //                e.DbConnector.Attach("TY_P_UT_75QGM640", this.ControlFactory, "05");
            //            }
            //        }
            //        e.DbConnector.ExecuteTranQueryList();
            //    }

            //    //스마트빌 사이트 전송
            //    UP_SMSBILL_WebServiceCall(sBATCH_ID);
            //}

            //this.UP_DataBinding(_CBO01_INQOPTION_Value.ToString(), _DTP01_SDATE_Value.ToString(), _CBH01_MCHWAJU_Value.ToString());
           
            ////this.ShowMessage("TY_M_GB_34I9W523");
        }

        private void BTN61_BILL_SEND_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            if (e.Successed)
            {
                this.ShowMessage("TY_M_GB_34I9W523");
            }
        }
        #endregion
    }
}
