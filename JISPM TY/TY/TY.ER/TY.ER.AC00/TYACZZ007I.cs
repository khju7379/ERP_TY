using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Windows.Forms;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전자세금계산서 수기발급 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.04.17 11:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_34HB1504 : 전자세금계산서 확인
    ///  TY_P_AC_34HB4505 : 전자세금계산서 마스타 입력
    ///  TY_P_AC_34HBA506 : 전자세금계산서 내역 입력
    ///  TY_P_AC_34HBA507 : 전자세금계산서 마스타 삭제
    ///  TY_P_AC_34HBB508 : 전자세금계산서 내역 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BILL_REM : 계산서취소
    ///  BILL_SEND : 계산서발행
    ///  CLO : 닫기
    ///  CREATOR_ID : 등록자
    ///  BILL_DATE : 작성일자
    ///  BILL_NO : 전표번호
    ///  CHARGE_AMOUNT : 공급가액
    ///  PRODUCT_NAME : 내역
    ///  RECV_ADDRESS : 주소
    ///  RECV_BIZ_REGIST_NO : 공급받는자사업자번호
    ///  RECV_BUSINESS_CLASS : 업종
    ///  RECV_BUSINESS_STATUS : 업태
    ///  RECV_CEO_NAME : 공급받는자대표자
    ///  RECV_COMPANY_CODE : 공급받는자코드
    ///  RECV_COMPANY_NAME : 공급받는자상호
    ///  RECV_DEPARTMENT_NAME : 담당부서
    ///  RECV_USER_EMAIL1 : 메일
    ///  RECV_USER_EMAIL2 : 메일
    ///  RECV_USER_NAME : 담당자명
    ///  RECV_USER_TEL_NO : 전화번호
    ///  SEND_ADDRESS : 공급자주소
    ///  SEND_BIZ_REGIST_NO : 공급자사업자번호
    ///  SEND_BUSINESS_CLASS : 업종
    ///  SEND_BUSINESS_STATUS : 업태
    ///  SEND_CEO_NAME : 대표자
    ///  SEND_COMPANY_CODE : 공급자코드
    ///  SEND_COMPANY_NAME : 공급자상호
    ///  SEND_DEPARTMENT_NAME : 담당부서
    ///  SEND_STATUS : 계산서상태
    ///  SEND_USER_EMAIL : 메일
    ///  SEND_USER_NAME : 담당자명
    ///  SEND_USER_TEL_NO : 전화번호
    ///  TOTAL_AMOUNT : 총액
    ///  TOTAL_TAX_AMOUNT : 부가세
    /// </summary>
    public partial class TYACZZ007I : TYBase
    {
        private string fsBILL_NO;

        private string fsSMSPORT;
        private string fsSMSID;
        private string fsSMSPASS;

        private string fsBATCH_ID;

        private TYData DAT02_SN;
        private TYData DAT02_BILL_NO;
        private TYData DAT02_BILL_DATE;
        private TYData DAT02_BLANK_NO;
        private TYData DAT02_BILL_TYPE_CODE;
        private TYData DAT02_BILL_DEMAND_TYPE_CODE;
        private TYData DAT02_VOLUME_ID;
        private TYData DAT02_ISSUE_ID;
        private TYData DAT02_SEQ_ID;
        private TYData DAT02_REF_PO_NO;
        private TYData DAT02_REF_PO_DATE;
        private TYData DAT02_REF_INVOICE_NO;
        private TYData DAT02_REF_INVOICE_DATE;
        private TYData DAT02_REF_DOC_NO;
        private TYData DAT02_BATCH_ISSUE_START_DATE;
        private TYData DAT02_BATCH_ISSUE_END_DATE;
        private TYData DAT02_SEND_COMPANY_CODE;
        private TYData DAT02_SEND_BIZ_REGIST_NO;
        private TYData DAT02_SEND_COMPANY_NAME;
        private TYData DAT02_SEND_CEO_NAME;
        private TYData DAT02_SEND_ADDRESS;
        private TYData DAT02_SEND_BUSINESS_STATUS;
        private TYData DAT02_SEND_BUSINESS_CLASS;
        private TYData DAT02_SEND_DEPARTMENT_NAME;
        private TYData DAT02_SEND_USER_NAME;
        private TYData DAT02_SEND_USER_TEL_NO;
        private TYData DAT02_SEND_USER_EMAIL;
        private TYData DAT02_RECV_COMPANY_CODE;
        private TYData DAT02_RECV_BIZ_REGIST_NO;
        private TYData DAT02_RECV_COMPANY_NAME;
        private TYData DAT02_RECV_CEO_NAME;
        private TYData DAT02_RECV_ADDRESS;
        private TYData DAT02_RECV_BUSINESS_STATUS;
        private TYData DAT02_RECV_BUSINESS_CLASS;
        private TYData DAT02_RECV_DEPARTMENT_NAME;
        private TYData DAT02_RECV_USER_NAME;
        private TYData DAT02_RECV_USER_TEL_NO;
        private TYData DAT02_RECV_USER_EMAIL;
        private TYData DAT02_RECV_USER2_EMAIL;
        private TYData DAT02_PAYMENT_TYPE_CODE;
        private TYData DAT02_TAX_FLAG;
        private TYData DAT02_TOTAL_TAX_AMOUNT;
        private TYData DAT02_CHARGE_AMOUNT;
        private TYData DAT02_TOTAL_AMOUNT;
        private TYData DAT02_TOTAL_CASH_AMOUNT;
        private TYData DAT02_TOTAL_CHECK_AMOUNT;
        private TYData DAT02_TOTAL_BILL_AMOUNT;
        private TYData DAT02_TOTAL_CREDIT_AMOUNT;
        private TYData DAT02_DESCRIPTION;
        private TYData DAT02_BILL_SECTION;
        private TYData DAT02_SEND_SALE_NO;
        private TYData DAT02_SEND_STATUS;
        private TYData DAT02_CREATOR_ID;
        private TYData DAT02_CREATOR_NAME;
        private TYData DAT02_CREATE_DATE;
        private TYData DAT02_CHANGER_ID;
        private TYData DAT02_CHANGER_NAME;
        private TYData DAT02_CHANGE_DATE;
        private TYData DAT02_SIGNATURE_VALUE;
        private TYData DAT02_SIGNATURE_ALGORITHM;
        private TYData DAT02_SIGNATURE_CERT_VALUE;
        private TYData DAT02_SIGNATURE_ORG_VALUE;
        private TYData DAT02_RECV_STATUS;
        private TYData DAT02_SEND_USER_ID;
        private TYData DAT02_RECV_USER_ID;
        private TYData DAT02_DIVISION_RATE;
        private TYData DAT02_CURRENCY_CODE;
        private TYData DAT02_DOWNPAYMENT_TYPE_CODE;
        private TYData DAT02_TAX_TYPE_CODE;
        private TYData DAT02_SEND_REMARK;
        private TYData DAT02_RECV_REMARK;
        private TYData DAT02_ISSUE_DATE;
        private TYData DAT02_EXPORT_VATBIL_H_SN;
        private TYData DAT02_IF_SN;
        private TYData DAT02_PROCESS;
        private TYData DAT02_BILL_CLASS;
        private TYData DAT02_AMEND_STATUS;
        private TYData DAT02_ATTRIBUTES6;
        private TYData DAT02_ATTRIBUTES7;  //종사업장


        private TYData DAT03_SN;
        private TYData DAT03_BILL_SN;
        private TYData DAT03_BILL_ITEM_NO;
        private TYData DAT03_BILL_ITEM_DATE;
        private TYData DAT03_PRODUCT_CODE;
        private TYData DAT03_PRODUCT_NAME;
        private TYData DAT03_DEFINITION;
        private TYData DAT03_DESCRIPTION;
        private TYData DAT03_PRODUCT_QUANTITY;
        private TYData DAT03_UNIT_CODE;
        private TYData DAT03_UNIT_PRICE;
        private TYData DAT03_CURRENCY_CODE;
        private TYData DAT03_PRODUCT_AMOUNT;
        private TYData DAT03_TAX_AMOUNT;
        private TYData DAT03_EXCHANGE_CURRENCY_RATE;
        private TYData DAT03_FOREIGN_CHARGE_AMOUNT;
        private TYData DAT03_PO_NO;
        private TYData DAT03_PO_ITEM_NO;
        private TYData DAT03_RETURN_TYPE_CODE;

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

        private byte[] _fbAttachFile;
        

        #region Description : 페이지 로드
        public TYACZZ007I(string sBILL_NO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            _fbAttachFile = null;

            this.fsBILL_NO = sBILL_NO;

            this.DAT02_SN = new TYData("DAT02_SN", null);
            this.DAT02_BILL_NO = new TYData("DAT02_BILL_NO", null);
            this.DAT02_BILL_DATE = new TYData("DAT02_BILL_DATE", null);
            this.DAT02_BLANK_NO = new TYData("DAT02_BLANK_NO", null);
            this.DAT02_BILL_TYPE_CODE = new TYData("DAT02_BILL_TYPE_CODE", null);
            this.DAT02_BILL_DEMAND_TYPE_CODE = new TYData("DAT02_BILL_DEMAND_TYPE_CODE", null);
            this.DAT02_VOLUME_ID = new TYData("DAT02_VOLUME_ID", null);
            this.DAT02_ISSUE_ID = new TYData("DAT02_ISSUE_ID", null);
            this.DAT02_SEQ_ID = new TYData("DAT02_SEQ_ID", null);
            this.DAT02_REF_PO_NO = new TYData("DAT02_REF_PO_NO", null);
            this.DAT02_REF_PO_DATE = new TYData("DAT02_REF_PO_DATE", null);
            this.DAT02_REF_INVOICE_NO = new TYData("DAT02_REF_INVOICE_NO", null);
            this.DAT02_REF_INVOICE_DATE = new TYData("DAT02_REF_INVOICE_DATE", null);
            this.DAT02_REF_DOC_NO = new TYData("DAT02_REF_DOC_NO", null);
            this.DAT02_BATCH_ISSUE_START_DATE = new TYData("DAT02_BATCH_ISSUE_START_DATE", null);
            this.DAT02_BATCH_ISSUE_END_DATE = new TYData("DAT02_BATCH_ISSUE_END_DATE", null);
            this.DAT02_SEND_COMPANY_CODE = new TYData("DAT02_SEND_COMPANY_CODE", null);
            this.DAT02_SEND_BIZ_REGIST_NO = new TYData("DAT02_SEND_BIZ_REGIST_NO", null);
            this.DAT02_SEND_COMPANY_NAME = new TYData("DAT02_SEND_COMPANY_NAME", null);
            this.DAT02_SEND_CEO_NAME = new TYData("DAT02_SEND_CEO_NAME", null);
            this.DAT02_SEND_ADDRESS = new TYData("DAT02_SEND_ADDRESS", null);
            this.DAT02_SEND_BUSINESS_STATUS = new TYData("DAT02_SEND_BUSINESS_STATUS", null);
            this.DAT02_SEND_BUSINESS_CLASS = new TYData("DAT02_SEND_BUSINESS_CLASS", null);
            this.DAT02_SEND_DEPARTMENT_NAME = new TYData("DAT02_SEND_DEPARTMENT_NAME", null);
            this.DAT02_SEND_USER_NAME = new TYData("DAT02_SEND_USER_NAME", null);
            this.DAT02_SEND_USER_TEL_NO = new TYData("DAT02_SEND_USER_TEL_NO", null);
            this.DAT02_SEND_USER_EMAIL = new TYData("DAT02_SEND_USER_EMAIL", null);
            this.DAT02_RECV_COMPANY_CODE = new TYData("DAT02_RECV_COMPANY_CODE", null);
            this.DAT02_RECV_BIZ_REGIST_NO = new TYData("DAT02_RECV_BIZ_REGIST_NO", null);
            this.DAT02_RECV_COMPANY_NAME = new TYData("DAT02_RECV_COMPANY_NAME", null);
            this.DAT02_RECV_CEO_NAME = new TYData("DAT02_RECV_CEO_NAME", null);
            this.DAT02_RECV_ADDRESS = new TYData("DAT02_RECV_ADDRESS", null);
            this.DAT02_RECV_BUSINESS_STATUS = new TYData("DAT02_RECV_BUSINESS_STATUS", null);
            this.DAT02_RECV_BUSINESS_CLASS = new TYData("DAT02_RECV_BUSINESS_CLASS", null);
            this.DAT02_RECV_DEPARTMENT_NAME = new TYData("DAT02_RECV_DEPARTMENT_NAME", null);
            this.DAT02_RECV_USER_NAME = new TYData("DAT02_RECV_USER_NAME", null);
            this.DAT02_RECV_USER_TEL_NO = new TYData("DAT02_RECV_USER_TEL_NO", null);
            this.DAT02_RECV_USER_EMAIL = new TYData("DAT02_RECV_USER_EMAIL", null);
            this.DAT02_RECV_USER2_EMAIL = new TYData("DAT02_RECV_USER2_EMAIL", null);
            this.DAT02_PAYMENT_TYPE_CODE = new TYData("DAT02_PAYMENT_TYPE_CODE", null);
            this.DAT02_TAX_FLAG = new TYData("DAT02_TAX_FLAG", null);
            this.DAT02_TOTAL_TAX_AMOUNT = new TYData("DAT02_TOTAL_TAX_AMOUNT", null);
            this.DAT02_CHARGE_AMOUNT = new TYData("DAT02_CHARGE_AMOUNT", null);
            this.DAT02_TOTAL_AMOUNT = new TYData("DAT02_TOTAL_AMOUNT", null);
            this.DAT02_TOTAL_CASH_AMOUNT = new TYData("DAT02_TOTAL_CASH_AMOUNT", null);
            this.DAT02_TOTAL_CHECK_AMOUNT = new TYData("DAT02_TOTAL_CHECK_AMOUNT", null);
            this.DAT02_TOTAL_BILL_AMOUNT = new TYData("DAT02_TOTAL_BILL_AMOUNT", null);
            this.DAT02_TOTAL_CREDIT_AMOUNT = new TYData("DAT02_TOTAL_CREDIT_AMOUNT", null);
            this.DAT02_DESCRIPTION = new TYData("DAT02_DESCRIPTION", null);
            this.DAT02_BILL_SECTION = new TYData("DAT02_BILL_SECTION", null);
            this.DAT02_SEND_SALE_NO = new TYData("DAT02_SEND_SALE_NO", null);
            this.DAT02_SEND_STATUS = new TYData("DAT02_SEND_STATUS", null);
            this.DAT02_CREATOR_ID = new TYData("DAT02_CREATOR_ID", null);
            this.DAT02_CREATOR_NAME = new TYData("DAT02_CREATOR_NAME", null);
            this.DAT02_CREATE_DATE = new TYData("DAT02_CREATE_DATE", null);
            this.DAT02_CHANGER_ID = new TYData("DAT02_CHANGER_ID", null);
            this.DAT02_CHANGER_NAME = new TYData("DAT02_CHANGER_NAME", null);
            this.DAT02_CHANGE_DATE = new TYData("DAT02_CHANGE_DATE", null);
            this.DAT02_SIGNATURE_VALUE = new TYData("DAT02_SIGNATURE_VALUE", null);
            this.DAT02_SIGNATURE_ALGORITHM = new TYData("DAT02_SIGNATURE_ALGORITHM", null);
            this.DAT02_SIGNATURE_CERT_VALUE = new TYData("DAT02_SIGNATURE_CERT_VALUE", null);
            this.DAT02_SIGNATURE_ORG_VALUE = new TYData("DAT02_SIGNATURE_ORG_VALUE", null);
            this.DAT02_RECV_STATUS = new TYData("DAT02_RECV_STATUS", null);
            this.DAT02_SEND_USER_ID = new TYData("DAT02_SEND_USER_ID", null);
            this.DAT02_RECV_USER_ID = new TYData("DAT02_RECV_USER_ID", null);
            this.DAT02_DIVISION_RATE = new TYData("DAT02_DIVISION_RATE", null);
            this.DAT02_CURRENCY_CODE = new TYData("DAT02_CURRENCY_CODE", null);
            this.DAT02_DOWNPAYMENT_TYPE_CODE = new TYData("DAT02_DOWNPAYMENT_TYPE_CODE", null);
            this.DAT02_TAX_TYPE_CODE = new TYData("DAT02_TAX_TYPE_CODE", null);
            this.DAT02_SEND_REMARK = new TYData("DAT02_SEND_REMARK", null);
            this.DAT02_RECV_REMARK = new TYData("DAT02_RECV_REMARK", null);
            this.DAT02_ISSUE_DATE = new TYData("DAT02_ISSUE_DATE", null);
            this.DAT02_EXPORT_VATBIL_H_SN = new TYData("DAT02_EXPORT_VATBIL_H_SN", null);
            this.DAT02_IF_SN = new TYData("DAT02_IF_SN", null);
            this.DAT02_PROCESS = new TYData("DAT02_PROCESS", null);
            this.DAT02_BILL_CLASS = new TYData("DAT02_BILL_CLASS", null);
            this.DAT02_AMEND_STATUS = new TYData("DAT02_AMEND_STATUS", null);
            this.DAT02_ATTRIBUTES6 = new TYData("DAT02_ATTRIBUTES6", null);
            this.DAT02_ATTRIBUTES7 = new TYData("DAT02_ATTRIBUTES7", null);   

            this.DAT03_SN		 = new TYData("DAT03_SN",null);
            this.DAT03_BILL_SN	 = new TYData("DAT03_BILL_SN",null);                    
            this.DAT03_BILL_ITEM_NO = new TYData("DAT03_BILL_ITEM_NO",null);
            this.DAT03_BILL_ITEM_DATE = new TYData("DAT03_BILL_ITEM_DATE",null);
            this.DAT03_PRODUCT_CODE    = new TYData("DAT03_PRODUCT_CODE",null);
            this.DAT03_PRODUCT_NAME    = new TYData("DAT03_PRODUCT_NAME",null);
            this.DAT03_DEFINITION	   = new TYData("DAT03_DEFINITION",null);
            this.DAT03_DESCRIPTION	   = new TYData("DAT03_DESCRIPTION",null);
            this.DAT03_PRODUCT_QUANTITY = new TYData("DAT03_PRODUCT_QUANTITY",null);
            this.DAT03_UNIT_CODE        = new TYData("DAT03_UNIT_CODE",null);
            this.DAT03_UNIT_PRICE       = new TYData("DAT03_UNIT_PRICE",null);
            this.DAT03_CURRENCY_CODE    = new TYData("DAT03_CURRENCY_CODE",null);
            this.DAT03_PRODUCT_AMOUNT   = new TYData("DAT03_PRODUCT_AMOUNT",null);
            this.DAT03_TAX_AMOUNT       = new TYData("DAT03_TAX_AMOUNT",null);
            this.DAT03_EXCHANGE_CURRENCY_RATE = new TYData("DAT03_EXCHANGE_CURRENCY_RATE",null);
            this.DAT03_FOREIGN_CHARGE_AMOUNT  = new TYData("DAT03_FOREIGN_CHARGE_AMOUNT",null);
            this.DAT03_PO_NO              = new TYData("DAT03_PO_NO",null);
            this.DAT03_PO_ITEM_NO         = new TYData("DAT03_PO_ITEM_NO",null);
            this.DAT03_RETURN_TYPE_CODE  = new TYData("DAT03_RETURN_TYPE_CODE",null);

            CBO01_AMEND_STATUS.Enabled = false;

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

        private void TYACZZ007I_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_SN);
            this.ControlFactory.Add(this.DAT02_BILL_NO);
            this.ControlFactory.Add(this.DAT02_BILL_DATE);
            this.ControlFactory.Add(this.DAT02_BLANK_NO);
            this.ControlFactory.Add(this.DAT02_BILL_TYPE_CODE);
            this.ControlFactory.Add(this.DAT02_BILL_DEMAND_TYPE_CODE);
            this.ControlFactory.Add(this.DAT02_VOLUME_ID);
            this.ControlFactory.Add(this.DAT02_ISSUE_ID);
            this.ControlFactory.Add(this.DAT02_SEQ_ID);
            this.ControlFactory.Add(this.DAT02_REF_PO_NO);
            this.ControlFactory.Add(this.DAT02_REF_PO_DATE);
            this.ControlFactory.Add(this.DAT02_REF_INVOICE_NO);
            this.ControlFactory.Add(this.DAT02_REF_INVOICE_DATE);
            this.ControlFactory.Add(this.DAT02_REF_DOC_NO);
            this.ControlFactory.Add(this.DAT02_BATCH_ISSUE_START_DATE);
            this.ControlFactory.Add(this.DAT02_BATCH_ISSUE_END_DATE);
            this.ControlFactory.Add(this.DAT02_SEND_COMPANY_CODE);
            this.ControlFactory.Add(this.DAT02_SEND_BIZ_REGIST_NO);
            this.ControlFactory.Add(this.DAT02_SEND_COMPANY_NAME);
            this.ControlFactory.Add(this.DAT02_SEND_CEO_NAME);
            this.ControlFactory.Add(this.DAT02_SEND_ADDRESS);
            this.ControlFactory.Add(this.DAT02_SEND_BUSINESS_STATUS);
            this.ControlFactory.Add(this.DAT02_SEND_BUSINESS_CLASS);
            this.ControlFactory.Add(this.DAT02_SEND_DEPARTMENT_NAME);
            this.ControlFactory.Add(this.DAT02_SEND_USER_NAME);
            this.ControlFactory.Add(this.DAT02_SEND_USER_TEL_NO);
            this.ControlFactory.Add(this.DAT02_SEND_USER_EMAIL);
            this.ControlFactory.Add(this.DAT02_RECV_COMPANY_CODE);
            this.ControlFactory.Add(this.DAT02_RECV_BIZ_REGIST_NO);
            this.ControlFactory.Add(this.DAT02_RECV_COMPANY_NAME);
            this.ControlFactory.Add(this.DAT02_RECV_CEO_NAME);
            this.ControlFactory.Add(this.DAT02_RECV_ADDRESS);
            this.ControlFactory.Add(this.DAT02_RECV_BUSINESS_STATUS);
            this.ControlFactory.Add(this.DAT02_RECV_BUSINESS_CLASS);
            this.ControlFactory.Add(this.DAT02_RECV_DEPARTMENT_NAME);
            this.ControlFactory.Add(this.DAT02_RECV_USER_NAME);
            this.ControlFactory.Add(this.DAT02_RECV_USER_TEL_NO);
            this.ControlFactory.Add(this.DAT02_RECV_USER_EMAIL);
            this.ControlFactory.Add(this.DAT02_RECV_USER2_EMAIL);
            this.ControlFactory.Add(this.DAT02_PAYMENT_TYPE_CODE);
            this.ControlFactory.Add(this.DAT02_TAX_FLAG);
            this.ControlFactory.Add(this.DAT02_TOTAL_TAX_AMOUNT);
            this.ControlFactory.Add(this.DAT02_CHARGE_AMOUNT);
            this.ControlFactory.Add(this.DAT02_TOTAL_AMOUNT);
            this.ControlFactory.Add(this.DAT02_TOTAL_CASH_AMOUNT);
            this.ControlFactory.Add(this.DAT02_TOTAL_CHECK_AMOUNT);
            this.ControlFactory.Add(this.DAT02_TOTAL_BILL_AMOUNT);
            this.ControlFactory.Add(this.DAT02_TOTAL_CREDIT_AMOUNT);
            this.ControlFactory.Add(this.DAT02_DESCRIPTION);
            this.ControlFactory.Add(this.DAT02_BILL_SECTION);
            this.ControlFactory.Add(this.DAT02_SEND_SALE_NO);
            this.ControlFactory.Add(this.DAT02_SEND_STATUS);
            this.ControlFactory.Add(this.DAT02_CREATOR_ID);
            this.ControlFactory.Add(this.DAT02_CREATOR_NAME);
            this.ControlFactory.Add(this.DAT02_CREATE_DATE);
            this.ControlFactory.Add(this.DAT02_CHANGER_ID);
            this.ControlFactory.Add(this.DAT02_CHANGER_NAME);
            this.ControlFactory.Add(this.DAT02_CHANGE_DATE);
            this.ControlFactory.Add(this.DAT02_SIGNATURE_VALUE);
            this.ControlFactory.Add(this.DAT02_SIGNATURE_ALGORITHM);
            this.ControlFactory.Add(this.DAT02_SIGNATURE_CERT_VALUE);
            this.ControlFactory.Add(this.DAT02_SIGNATURE_ORG_VALUE);
            this.ControlFactory.Add(this.DAT02_RECV_STATUS);
            this.ControlFactory.Add(this.DAT02_SEND_USER_ID);
            this.ControlFactory.Add(this.DAT02_RECV_USER_ID);
            this.ControlFactory.Add(this.DAT02_DIVISION_RATE);
            this.ControlFactory.Add(this.DAT02_CURRENCY_CODE);
            this.ControlFactory.Add(this.DAT02_DOWNPAYMENT_TYPE_CODE);
            this.ControlFactory.Add(this.DAT02_TAX_TYPE_CODE);
            this.ControlFactory.Add(this.DAT02_SEND_REMARK);
            this.ControlFactory.Add(this.DAT02_RECV_REMARK);
            this.ControlFactory.Add(this.DAT02_ISSUE_DATE);
            this.ControlFactory.Add(this.DAT02_EXPORT_VATBIL_H_SN);
            this.ControlFactory.Add(this.DAT02_IF_SN);
            this.ControlFactory.Add(this.DAT02_PROCESS);
            this.ControlFactory.Add(this.DAT02_BILL_CLASS);
            this.ControlFactory.Add(this.DAT02_AMEND_STATUS);
            this.ControlFactory.Add(this.DAT02_ATTRIBUTES6);
            this.ControlFactory.Add(this.DAT02_ATTRIBUTES7);

            this.ControlFactory.Add(this.DAT03_SN);
            this.ControlFactory.Add(this.DAT03_BILL_SN);
            this.ControlFactory.Add(this.DAT03_BILL_ITEM_NO);
            this.ControlFactory.Add(this.DAT03_BILL_ITEM_DATE);
            this.ControlFactory.Add(this.DAT03_PRODUCT_CODE);
            this.ControlFactory.Add(this.DAT03_PRODUCT_NAME);
            this.ControlFactory.Add(this.DAT03_DEFINITION);
            this.ControlFactory.Add(this.DAT03_DESCRIPTION);
            this.ControlFactory.Add(this.DAT03_PRODUCT_QUANTITY);
            this.ControlFactory.Add(this.DAT03_UNIT_CODE);
            this.ControlFactory.Add(this.DAT03_UNIT_PRICE);
            this.ControlFactory.Add(this.DAT03_CURRENCY_CODE);
            this.ControlFactory.Add(this.DAT03_PRODUCT_AMOUNT);
            this.ControlFactory.Add(this.DAT03_TAX_AMOUNT);
            this.ControlFactory.Add(this.DAT03_EXCHANGE_CURRENCY_RATE);
            this.ControlFactory.Add(this.DAT03_FOREIGN_CHARGE_AMOUNT);
            this.ControlFactory.Add(this.DAT03_PO_NO);
            this.ControlFactory.Add(this.DAT03_PO_ITEM_NO);
            this.ControlFactory.Add(this.DAT03_RETURN_TYPE_CODE);

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

            this.BTN61_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN62_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN61_DOWN.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_BILL_SEND.ProcessCheck += new TButton.CheckHandler(BTN61_BILL_SEND_ProcessCheck);
            this.BTN61_BILL_REM.ProcessCheck += new TButton.CheckHandler(BTN61_BILL_REM_ProcessCheck);

            LBL52_BILL_NO.SetValue("원천전표");

            if (string.IsNullOrEmpty(this.fsBILL_NO))
            {

                BTN61_BILL_SEND.Visible = true;
                BTN61_BILL_REM.Visible = false;
                this.BTN61_DOWN.Visible = false;

                this.SetStartingFocus(this.TXT01_BILL_NO);
            }
            else
            {
                this.TXT01_BILL_NO.SetValue(this.fsBILL_NO);
 
                this.TXT01_BILL_NO.SetReadOnly(true);
                BTN61_INQ_FXL.Visible = false;
                CBO01_BILL_SENDID.SetReadOnly(true);

                //UP_BILL_DETAIL(); 

                UP_XXSBBILL_DETAIL();
            }

            UP_Fild_Lock();

            BTN62_INQ_FXL.Visible = false;
        }
        #endregion

        #region Description : 계산서취소
        private void BTN61_BILL_REM_Click(object sender, EventArgs e)
        {
            Int64 iBATCH_ID = 0;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_34HBA507", this.DAT02_SN.GetValue());
            //this.DbConnector.Attach("TY_P_AC_34HBB508", this.DAT02_SN.GetValue());
            //this.DbConnector.ExecuteTranQueryList();

            ////계산서 호출            
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_34JB5537",
            //    "",
            //    "",
            //    "0",
            //    this.DAT02_SN.GetValue()
            //    );

            //string sMessage = this.DbConnector.ExecuteScalar(0).ToString();

            //iBATCH_ID = Convert.ToInt64(Get_Numeric(sMessage));

            //string sUrl = "http://192.168.100.32:" + fsSMSPORT + "/callSB_V3/XXSB_DTI_STATUS_CHANGE2.asp?BATCH_ID=" + iBATCH_ID.ToString() + "&STATUS=O&SIGNAL=CANCELALL&ID=" + fsSMSID + "&PASS=" + fsSMSPASS;                       

            //if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //전송결과 체크
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_34JBQ539", this.DAT02_SN.GetValue());
            //    DataTable dt = this.DbConnector.ExecuteDataTable();
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["DTI_STATUS"].ToString().Trim() == "O" && dt.Rows[0]["RETURN_CODE"].ToString().Trim() == "30000")
            //        {
            //            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //            this.ShowMessage("TY_M_GB_34I9X524");
            //            this.Close();
            //        }
            //        else
            //        {
            //            this.ShowCustomMessage(dt.Rows[0]["RETURN_DESCRIPTION"].ToString().Trim(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        }
            //    }               
            //}

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            string sOutMsg = string.Empty;
            string sCONVERSATION_IDList = string.Empty;

            sCONVERSATION_IDList = TXT01_CONVERSATION_ID.GetValue().ToString();

            if (sCONVERSATION_IDList != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75TIE669", "", sCONVERSATION_IDList);

                string[] arrayMessage = this.DbConnector.ExecuteScalar(0).ToString().Split(',');

                if (arrayMessage[0].ToString() == "I")
                {
                    iBATCH_ID = Convert.ToInt64(Get_Numeric(arrayMessage[2]));

                    string sUrl = "http://192.168.100.32:" + fsSMSPORT + "/callSB_V3/XXSB_DTI_STATUS_CHANGE2.asp?BATCH_ID=" + iBATCH_ID.ToString() + "&STATUS=O&SIGNAL=CANCELALL&ID=" + fsSMSID + "&PASS=" + fsSMSPASS;

                    if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.UP_SMSBILL_TransResult(iBATCH_ID.ToString());
                    }
                }
                else
                {
                    this.ShowCustomMessage(arrayMessage[1], "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }


        }
        #endregion

        #region Description : 계산서발행
        private void BTN61_BILL_SEND_Click(object sender, EventArgs e)
        {
            Int64 iBATCH_ID = 0;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_34HB4505", this.ControlFactory, "02");
            //this.DbConnector.Attach("TY_P_AC_34HBA506", this.ControlFactory, "03");
            //this.DbConnector.ExecuteTranQueryList();

            ////계산서 호출            
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_34I2P528",
            //    "",
            //    "",
            //    "0",
            //    this.DAT02_SN.GetValue(),
            //    this.DAT02_SN.GetValue(),
            //    fsSMSID
            //    );

            //string sMessage = this.DbConnector.ExecuteScalar(0).ToString();

            //iBATCH_ID = Convert.ToInt64(Get_Numeric(sMessage));

            //string sUrl = "http://192.168.100.32:" + fsSMSPORT + "/callSB_V3/XXSB_DTI_ARISSUE2.asp?BATCH_ID=" + iBATCH_ID.ToString() + "&ID=" + fsSMSID + "&PASS=" + fsSMSPASS;

            //if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //전송결과 체크
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_34JBQ539", this.DAT02_SN.GetValue());
            //    DataTable dt = this.DbConnector.ExecuteDataTable();
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["DTI_STATUS"].ToString().Trim() == "I" && dt.Rows[0]["RETURN_CODE"].ToString().Trim() == "30000")
            //        {
            //            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //            this.ShowMessage("TY_M_GB_34I9W523");
            //            this.Close();
            //        }
            //        else
            //        {
            //            this.ShowCustomMessage(dt.Rows[0]["RETURN_DESCRIPTION"].ToString().Trim(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        }
            //    }
            //}

            #region  Description : XXSB_DTI_MAIN 저장
            if (data_Main.Count > 0)
            {

                DAT05_EVENT_ID.SetValue(UP_Get_EventID());
                UP_DataCollections_Add("Msg");

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

                #region  Description : XXSB_DELIVERY_DTI_FILE 저장

                string filePath = this.TXT01_ATTACH_FILENAME.GetValue().ToString();

                if (filePath.Trim() != "")
                {
                   byte[] _AttachFile = null;
                   object _objAttachFile = null;                   
                   _AttachFile = UP_Get_Byte(filePath);
                   _objAttachFile = _AttachFile;                

                    this.DAT05_CONVERSATION_ID.SetValue(this.DAT01_CONVERSATION_ID.GetValue().ToString());
                    this.DAT05_FILE_SEQ.SetValue("1");
                    //this.DAT05_EVENT_ID.SetValue(sEventID);
                    this.DAT05_FILE_NAME.SetValue(this.TXT01_AFFILENAME.GetValue().ToString());
                    this.DAT05_FILE_SAVE_TYPE.SetValue("DB");
                    this.DAT05_FILE_SIZE.SetValue(TXT01_AFFILESIZE.GetValue().ToString());
                    this.DAT05_FILE_BINARY.SetValue(_objAttachFile);
                    this.DAT05_FILE_STATUS.SetValue("TRANSFER_REQUEST");
                    this.DAT05_CREATION_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    this.DAT05_CREATION_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());
                    this.DAT05_LAST_UPDATE_BY.SetValue(DAT01_CREATED_BY.GetValue().ToString());
                    this.DAT05_LAST_UPDATE_DATE.SetValue(DAT01_CREATION_DATE.GetValue().ToString());
                    this.DAT05_IN_OUT.SetValue("OUT");

                    this.DbConnector.Attach("TY_P_UT_75QGM640", this.ControlFactory, "05");
                }

                #endregion            

                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();
                
                //스마트빌 사이트 전송
                UP_SMSBILL_WebServiceCall(fsBATCH_ID);               
            }
            #endregion


        }
        #endregion       

        #region Description : 계산서발행 ProcessCheck 이벤트
        private void BTN61_BILL_SEND_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            string sSN = "";

            //전표번호 존재유무 체크
            if (TXT01_BILL_NO.GetValue().ToString().Trim() == "")
            {
                this.ShowMessage("TY_M_AC_25M7B594");
                e.Successed = false;
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_34H3E517", this.TXT01_BILL_NO.GetValue().ToString().Substring(0, 6), this.TXT01_BILL_NO.GetValue().ToString().Substring(6, 8), this.TXT01_BILL_NO.GetValue().ToString().Substring(14, 3), this.TXT01_BILL_NO.GetValue().ToString().Substring(17, 2));
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_MR_31933569");
                    e.Successed = false;
                    return;
                }
            }

            //수정계산서일경우 원천전표, 승인번호 존재 체크
            if (CBO01_BILL_CLASS.GetValue().ToString() == "02")
            {
                if (TXT04_BILL_NO.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_AC_25M7B594");
                    e.Successed = false;
                    return;
                }

                if (TXT01_ORI_ISSUE_ID.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_AC_43EB3724");
                    e.Successed = false;
                    return;
                }
            }

            //첨부파일 용량 체크
            string filePath = this.TXT01_ATTACH_FILENAME.GetValue().ToString();

            if (filePath.Trim() != "")
            {
                byte[] _AttachFile = null;
                _AttachFile = UP_Get_Byte(filePath);
                int ArraySize = _AttachFile.GetUpperBound(0);

                // 용량체크(1메가)            
                if (ArraySize > 1000000)
                {
                    this.ShowMessage("TY_M_AC_965FD725");
                    e.Successed = false;
                    return;
                }
            }

            #region  Description : BIZ_BILL_MASTER 저장

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34I8P520");
            sSN = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.DAT02_SN.SetValue(sSN);
            this.DAT02_BILL_NO.SetValue(TXT01_BILL_NO.GetValue().ToString().Substring(0,19));
            this.DAT02_BILL_DATE.SetValue(DTP01_BILL_DATE.GetString().ToString());

            int lengthDS11 = Convert.ToDouble(Get_Numeric(TXT01_CHARGE_AMOUNT.GetValue().ToString())).ToString().Length; 
            this.DAT02_BLANK_NO.SetValue(Convert.ToString(11 - lengthDS11));
            this.DAT02_BILL_TYPE_CODE.SetValue("F");
            this.DAT02_BILL_DEMAND_TYPE_CODE.SetValue("18"); //"영수발행구분코드 : 1-영수, 18-청구"
            this.DAT02_VOLUME_ID.SetValue("0");
            this.DAT02_ISSUE_ID.SetValue("0");
            this.DAT02_SEQ_ID.SetValue("0");
            this.DAT02_REF_PO_NO.SetValue("0");
            this.DAT02_REF_PO_DATE.SetValue("");
            this.DAT02_REF_INVOICE_NO.SetValue("");
            this.DAT02_REF_INVOICE_DATE.SetValue("");
            this.DAT02_REF_DOC_NO.SetValue("");
            this.DAT02_BATCH_ISSUE_START_DATE.SetValue("");
            this.DAT02_BATCH_ISSUE_END_DATE.SetValue("");
           
            this.DAT02_SEND_COMPANY_CODE.SetValue(TXT01_SEND_COMPANY_CODE.GetValue());
            this.DAT02_SEND_BIZ_REGIST_NO.SetValue(TXT01_SEND_BIZ_REGIST_NO.GetValue());
            this.DAT02_SEND_COMPANY_NAME.SetValue(TXT01_SEND_COMPANY_NAME.GetValue());
            this.DAT02_SEND_CEO_NAME.SetValue(TXT01_SEND_CEO_NAME.GetValue());
            this.DAT02_SEND_ADDRESS.SetValue(TXT01_SEND_ADDRESS.GetValue());
            this.DAT02_SEND_BUSINESS_STATUS.SetValue(TXT01_SEND_BUSINESS_STATUS.GetValue());
            this.DAT02_SEND_BUSINESS_CLASS.SetValue(TXT01_SEND_BUSINESS_CLASS.GetValue());
            this.DAT02_SEND_DEPARTMENT_NAME.SetValue(TXT01_SEND_DEPARTMENT_NAME.GetValue());
            this.DAT02_SEND_USER_NAME.SetValue(TXT01_SEND_USER_NAME.GetValue());
            this.DAT02_SEND_USER_TEL_NO.SetValue(TXT01_SEND_USER_TEL_NO.GetValue());
            this.DAT02_SEND_USER_EMAIL.SetValue(TXT01_SEND_USER_EMAIL.GetValue());

            this.DAT02_RECV_COMPANY_CODE.SetValue(TXT01_RECV_COMPANY_CODE.GetValue());
            this.DAT02_RECV_BIZ_REGIST_NO.SetValue(TXT01_RECV_BIZ_REGIST_NO.GetValue());
            this.DAT02_RECV_COMPANY_NAME.SetValue(TXT01_RECV_COMPANY_NAME.GetValue());
            this.DAT02_RECV_CEO_NAME.SetValue(TXT01_RECV_CEO_NAME.GetValue());
            this.DAT02_RECV_ADDRESS.SetValue(TXT01_RECV_ADDRESS.GetValue());
            this.DAT02_RECV_BUSINESS_STATUS.SetValue(TXT01_RECV_BUSINESS_STATUS.GetValue());
            this.DAT02_RECV_BUSINESS_CLASS.SetValue(TXT01_RECV_BUSINESS_CLASS.GetValue());
            this.DAT02_RECV_DEPARTMENT_NAME.SetValue(TXT01_RECV_DEPARTMENT_NAME.GetValue());
            this.DAT02_RECV_USER_NAME.SetValue(TXT01_RECV_USER_NAME.GetValue());
            this.DAT02_RECV_USER_TEL_NO.SetValue(TXT01_RECV_USER_TEL_NO.GetValue());
            this.DAT02_RECV_USER_EMAIL.SetValue(TXT01_RECV_USER_EMAIL1.GetValue());
            this.DAT02_RECV_USER2_EMAIL.SetValue(TXT01_RECV_USER_EMAIL2.GetValue());

            this.DAT02_PAYMENT_TYPE_CODE.SetValue("");
            this.DAT02_TAX_FLAG.SetValue("Y");

            this.DAT02_TOTAL_TAX_AMOUNT.SetValue(Get_Numeric(TXT01_TOTAL_TAX_AMOUNT.GetValue().ToString().Trim()));
            this.DAT02_CHARGE_AMOUNT.SetValue(Get_Numeric(TXT01_CHARGE_AMOUNT.GetValue().ToString().Trim()));
            this.DAT02_TOTAL_AMOUNT.SetValue(Get_Numeric(TXT01_TOTAL_AMOUNT.GetValue().ToString().Trim()));

            this.DAT02_TOTAL_CASH_AMOUNT.SetValue("0");
            this.DAT02_TOTAL_CHECK_AMOUNT.SetValue("0");
            this.DAT02_TOTAL_BILL_AMOUNT.SetValue("0");
            this.DAT02_TOTAL_CREDIT_AMOUNT.SetValue("0");

            this.DAT02_DESCRIPTION.SetValue(TXT01_DESCRIPTION.GetValue().ToString().Trim());
            this.DAT02_BILL_SECTION.SetValue("");
            this.DAT02_SEND_SALE_NO.SetValue("");
            this.DAT02_SEND_STATUS.SetValue("FS");

            this.DAT02_CREATOR_ID.SetValue(CBH01_CREATOR_ID.GetValue());
            this.DAT02_CREATOR_NAME.SetValue(CBH01_CREATOR_ID.GetText());
            this.DAT02_CREATE_DATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.DAT02_CHANGER_ID.SetValue(CBO01_BILL_SENDID.GetValue().ToString()); //발행id
            this.DAT02_CHANGER_NAME.SetValue("");
            this.DAT02_CHANGE_DATE.SetValue("");

            this.DAT02_SIGNATURE_VALUE.SetValue("");
            this.DAT02_SIGNATURE_ALGORITHM.SetValue("");
            this.DAT02_SIGNATURE_CERT_VALUE.SetValue("");
            this.DAT02_SIGNATURE_ORG_VALUE.SetValue("");

            this.DAT02_RECV_STATUS.SetValue("FS");
            this.DAT02_SEND_USER_ID.SetValue("");
            this.DAT02_RECV_USER_ID.SetValue("");
            this.DAT02_DIVISION_RATE.SetValue("0");
            this.DAT02_CURRENCY_CODE.SetValue("");
            this.DAT02_DOWNPAYMENT_TYPE_CODE.SetValue("");

            if (TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "61" || TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "69")
            {
                this.DAT02_TAX_TYPE_CODE.SetValue("01");
            }
            else if (TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "62")
            {
                this.DAT02_TAX_TYPE_CODE.SetValue("03");
            }
            else if (TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "66")
            {
                this.DAT02_TAX_TYPE_CODE.SetValue("02");
            }
            else
            {
                this.DAT02_TAX_TYPE_CODE.SetValue("01");
            }

            this.DAT02_SEND_REMARK.SetValue("");
            this.DAT02_RECV_REMARK.SetValue("");
            this.DAT02_ISSUE_DATE.SetValue("");
            this.DAT02_EXPORT_VATBIL_H_SN.SetValue("0");
            this.DAT02_IF_SN.SetValue("0");
            this.DAT02_PROCESS.SetValue("N");

            this.DAT02_BILL_CLASS.SetValue(CBO01_BILL_CLASS.GetValue());

            if (CBO01_BILL_CLASS.GetValue().ToString() == "02")
            {
                this.DAT02_AMEND_STATUS.SetValue(CBO01_AMEND_STATUS.GetValue());
                this.DAT02_ATTRIBUTES6.SetValue(TXT01_ORI_ISSUE_ID.GetValue());
            }
            else
            {
                this.DAT02_AMEND_STATUS.SetValue("");
                this.DAT02_ATTRIBUTES6.SetValue("");
            }            

            //종사업장 추가
            if (TXT01_BYR_BIZPLACE_CODE.GetValue().ToString().Trim() != "" && TXT01_BYR_BIZPLACE_CODE.GetValue().ToString().Trim().Length < 4)
            {
                this.ShowCustomMessage("종사업장번호는 4자리여야 합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }
            else
            {
                this.DAT02_ATTRIBUTES7.SetValue(TXT01_BYR_BIZPLACE_CODE.GetValue().ToString().Trim());
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34I9R521");
            sSN = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.DAT03_SN.SetValue(sSN);
            this.DAT03_BILL_SN.SetValue(this.DAT02_SN.GetValue());
            this.DAT03_BILL_ITEM_NO.SetValue("1");
            this.DAT03_BILL_ITEM_DATE.SetValue(DAT02_BILL_DATE.GetValue());
            this.DAT03_PRODUCT_CODE.SetValue("");
            this.DAT03_PRODUCT_NAME.SetValue(TXT01_PRODUCT_NAME.GetValue());
            this.DAT03_DEFINITION.SetValue("");
            this.DAT03_DESCRIPTION.SetValue("");
            this.DAT03_PRODUCT_QUANTITY.SetValue("0");
            this.DAT03_UNIT_CODE.SetValue("");
            this.DAT03_UNIT_PRICE.SetValue("0");
            this.DAT03_CURRENCY_CODE.SetValue("");
            this.DAT03_PRODUCT_AMOUNT.SetValue(Get_Numeric(DAT02_CHARGE_AMOUNT.GetValue().ToString()));
            this.DAT03_TAX_AMOUNT.SetValue(Get_Numeric(DAT02_TOTAL_TAX_AMOUNT.GetValue().ToString()));
            this.DAT03_EXCHANGE_CURRENCY_RATE.SetValue("0");
            this.DAT03_FOREIGN_CHARGE_AMOUNT.SetValue("0");
            this.DAT03_PO_NO.SetValue("");
            this.DAT03_PO_ITEM_NO.SetValue("0");
            this.DAT03_RETURN_TYPE_CODE.SetValue("");

            #endregion

            switch (CBO01_BILL_SENDID.GetValue().ToString())
            {
                case "tyccul":  //울산
                    fsSMSPORT = "10000";
                    fsSMSID = "tyccul";
                    fsSMSPASS = "tyculsan29220";
                    break;
                case "tycculsilo":  //울산silo전용
                    fsSMSPORT = "10000";
                    fsSMSID = "tycculsilo";
                    fsSMSPASS = "tyculsan2922";
                    break;
                case "tycculutt":  //울산utt전용
                    fsSMSPORT = "10000";
                    fsSMSID = "tycculutt";
                    fsSMSPASS = "tyculsan2922";
                    break;
                case "tyccsb":    //서울
                    fsSMSPORT = "10001";
                    fsSMSID = "tyccsb";
                    fsSMSPASS = "tyseoul2009";
                    break;
            }


            fsBATCH_ID = UP_Get_BATCH_ID();

            #region  Description : XXSB_DTI_MAIN 저장

            string sCONVERSATION_ID = string.Empty;
            string sBATCH_ID = string.Empty;

            //공급자사업자번호(10)+공급받는자사업자번호(10)+계산서일자(8)+일련번호(4)+003
            sCONVERSATION_ID = TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Replace("-", "") + TXT01_RECV_BIZ_REGIST_NO.GetValue().ToString().Replace("-", "").Substring(0,10) + DTP01_BILL_DATE.GetString().ToString() +
                               UP_Get_CONVERSATION_Seq(TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Replace("-", "") + TXT01_RECV_BIZ_REGIST_NO.GetValue().ToString().Replace("-", "").Substring(0,10) + DTP01_BILL_DATE.GetString().ToString()) + "003";

            DAT01_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
            //매입(AP)/매출(AR)구분(
            DAT01_SUPBUY_TYPE.SetValue("AR");
            //정/역 구분
            DAT01_DIRECTION.SetValue("2");
            DAT01_INTERFACE_BATCH_ID.SetValue(fsBATCH_ID);
            DAT01_DTI_WDATE.SetValue(DTP01_BILL_DATE.GetString().ToString().Substring(0, 4) + "-" + DTP01_BILL_DATE.GetString().ToString().Substring(4, 2) + "-" + DTP01_BILL_DATE.GetString().ToString().Substring(6, 2));
            //세금유형코드 : 01-과세, 02-면세, 03-영세율"
            DAT01_DTI_TYPE.SetValue(TXT01_TAX_TYPE_CODE.GetValue().ToString());

            if (TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "61" || TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "62" || TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "69")
            {
                DAT01_DTI_TYPE.SetValue("01");  //일반, 영세
            }
            else if (TXT01_TAX_TYPE_CODE.GetValue().ToString().Trim() == "66")
            {
                this.DAT01_DTI_TYPE.SetValue("02");   //면세            
            }
            else
            {
                this.DAT01_DTI_TYPE.SetValue("01"); //일반, 영세
            }

            if (TXT01_RECV_BIZ_REGIST_NO.GetValue().ToString().Replace("-", "") == "6018101970")
            {
                DAT01_TAX_DEMAND.SetValue("01");
            }
            else
            {
                DAT01_TAX_DEMAND.SetValue("18");
            }
            DAT01_SUP_COM_ID.SetValue(fsSMSID);
            DAT01_SUP_COM_REGNO.SetValue(TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Replace("-", ""));
            DAT01_SUP_REP_NAME.SetValue(TXT01_SEND_CEO_NAME.GetValue().ToString());
            DAT01_SUP_COM_NAME.SetValue(TXT01_SEND_COMPANY_NAME.GetValue().ToString());
            DAT01_SUP_COM_TYPE.SetValue(TXT01_SEND_BUSINESS_STATUS.GetValue().ToString().Replace(",", ""));
            DAT01_SUP_COM_CLASSIFY.SetValue(TXT01_SEND_BUSINESS_CLASS.GetValue().ToString().Replace(",", ""));
            DAT01_SUP_COM_ADDR.SetValue(TXT01_SEND_ADDRESS.GetValue().ToString());

            //발행자 정보
            DAT01_SUP_DEPT_NAME.SetValue(TXT01_SEND_DEPARTMENT_NAME.GetValue().ToString());
            DAT01_SUP_EMP_NAME.SetValue(TXT01_SEND_USER_NAME.GetValue().ToString());
            DAT01_SUP_TEL_NUM.SetValue(TXT01_SEND_USER_TEL_NO.GetValue().ToString());
            DAT01_SUP_EMAIL.SetValue(TXT01_SEND_USER_EMAIL.GetValue().ToString());

            DAT01_BYR_COM_REGNO.SetValue(TXT01_RECV_BIZ_REGIST_NO.GetValue().ToString().Replace("-", ""));
            DAT01_BYR_REP_NAME.SetValue(TXT01_RECV_CEO_NAME.GetValue().ToString());
            DAT01_BYR_COM_NAME.SetValue(TXT01_RECV_COMPANY_NAME.GetValue().ToString());
            DAT01_BYR_COM_TYPE.SetValue(TXT01_RECV_BUSINESS_STATUS.GetValue().ToString());
            DAT01_BYR_COM_CLASSIFY.SetValue(TXT01_RECV_BUSINESS_CLASS.GetValue().ToString());
            DAT01_BYR_COM_ADDR.SetValue(TXT01_RECV_ADDRESS.GetValue().ToString());
            DAT01_BYR_DEPT_NAME.SetValue(TXT01_RECV_DEPARTMENT_NAME.GetValue().ToString());
            DAT01_BYR_EMP_NAME.SetValue(TXT01_RECV_USER_NAME.GetValue().ToString());
            DAT01_BYR_TEL_NUM.SetValue(TXT01_RECV_USER_TEL_NO.GetValue().ToString());
            
            DAT01_BYR_EMAIL.SetValue(TXT01_RECV_USER_EMAIL1.GetValue().ToString());

            DAT01_SUP_AMOUNT.SetValue(Get_Numeric(TXT01_CHARGE_AMOUNT.GetValue().ToString()));
            DAT01_TAX_AMOUNT.SetValue(Get_Numeric(TXT01_TOTAL_TAX_AMOUNT.GetValue().ToString()));
            DAT01_TOTAL_AMOUNT.SetValue(Get_Numeric(TXT01_TOTAL_AMOUNT.GetValue().ToString()));
            DAT01_DTT_YN.SetValue("Y");

            DAT01_REMARK.SetValue(TXT01_DESCRIPTION.GetValue().ToString());

            DAT01_CREATED_BY.SetValue(TYUserInfo.EmpNo);
            DAT01_CREATION_DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            if (CBO01_BILL_CLASS.GetValue().ToString() == "02")
            {
                DAT01_AMEND_CODE.SetValue(CBO01_AMEND_STATUS.GetValue().ToString());
                DAT01_ORI_ISSUE_ID.SetValue(TXT01_ORI_ISSUE_ID.GetValue().ToString());
            }
            else
            {
                DAT01_AMEND_CODE.SetValue("");
                DAT01_ORI_ISSUE_ID.SetValue("");
            }

            //첨부
            if (TXT01_AFFILENAME.GetValue().ToString() != "")
            {
                DAT01_ATTACHFILE_YN.SetValue("Y");
            }
            else
            {
                DAT01_ATTACHFILE_YN.SetValue("N");
            }

            DAT01_BYR_BIZPLACE_CODE.SetValue(TXT01_BYR_BIZPLACE_CODE.GetValue().ToString());

            UP_DataCollections_Add("Main");
            #endregion

            #region  Description : XXSB_DTI_ITEM 저장
            //라인 1
            DAT02_CONVERSATION_ID.SetValue(sCONVERSATION_ID);
            DAT02_SUPBUY_TYPE.SetValue(DAT01_SUPBUY_TYPE.GetValue().ToString());
            DAT02_DIRECTION.SetValue(DAT01_DIRECTION.GetValue().ToString());
            DAT02_DTI_LINE_NUM.SetValue("1");
            DAT02_ITEM_CODE.SetValue("");
            DAT02_ITEM_NAME.SetValue(TXT01_PRODUCT_NAME.GetValue().ToString());
            DAT02_ITEM_SIZE.SetValue("");
            DAT02_ITEM_MD.SetValue(DTP01_BILL_DATE.GetString().ToString());
            DAT02_UNIT_PRICE.SetValue("0");
            DAT02_ITEM_QTY.SetValue("0");

            DAT02_SUP_AMOUNT.SetValue(Get_Numeric(TXT01_CHARGE_AMOUNT.GetValue().ToString()));
            DAT02_TAX_AMOUNT.SetValue(Get_Numeric(TXT01_TOTAL_TAX_AMOUNT.GetValue().ToString()));

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
            this.DAT04_INVOICE_NUM.SetValue(TXT01_BILL_NO.GetValue().ToString());

            UP_DataCollections_Add("Inv");
            #endregion

           
            

            ////서울
            //if (this.TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Substring(0, 3) == "105")
            //{
            //    fsSMSPORT = "10001";
            //    fsSMSID = "tyccsb";
            //    fsSMSPASS = "tyseoul2009";
            //}
            //else //울산
            //{
            //    fsSMSPORT = "10000";
            //    fsSMSID = "tyccul";
            //    fsSMSPASS = "tyculsan29220";
            //}


           

            if (this.TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Substring(0, 3) == "105")
            {
                if (fsSMSID != "tyccsb")
                {
                    this.ShowCustomMessage("서울지점 전송ID는 tyccsb 입니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (fsSMSID == "tyccsb")
                {
                    this.ShowCustomMessage("본점 계산서는 전송ID는 tyccsb를 사용할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }  
            }


            if (!this.ShowMessage("TY_M_GB_34I34532"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 계산서취소 ProcessCheck 이벤트
        private void BTN61_BILL_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            /*
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34HB1504", this.TXT01_BILL_NO.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DAT02_SN.SetValue(dt.Rows[0]["SN"].ToString());
                this.DAT03_SN.SetValue(dt.Rows[0]["SN"].ToString());
            }

            //계산서 상태 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34JA6535", this.DAT02_SN.GetValue());
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                if (dk.Rows[0]["SEND_STATUS"].ToString() != "I")
                {
                    this.ShowMessage("TY_M_GB_34JA0536");
                    e.Successed = false;
                    return;
                }
                if (dk.Rows[0]["TAXSTATUS"].ToString() != "전송실패" && dk.Rows[0]["TAXSTATUS"].ToString() != "미전송")
                {
                    this.ShowMessage("TY_M_GB_34JA0536");
                    e.Successed = false;
                    return;
                }
            }*/

            //계산서 상태 체크

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_88TA2649", this.TXT01_BILL_NO.GetValue());

            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                if (dk.Rows[0]["DTI_STATUS"].ToString() != "I")
                {
                    this.ShowMessage("TY_M_GB_34JA0536");
                    e.Successed = false;
                    return;
                }
                if (dk.Rows[0]["TAXTRANSSTATUS"].ToString() != "전송실패" && dk.Rows[0]["TAXTRANSSTATUS"].ToString() != "미전송")
                {
                    this.ShowMessage("TY_M_GB_34JA0536");
                    e.Successed = false;
                    return;
                }

                switch (dk.Rows[0]["SUP_COM_ID"].ToString())
                {
                    case "tyccul":  //울산
                        fsSMSPORT = "10000";
                        fsSMSID = "tyccul";
                        fsSMSPASS = "tyculsan29220";
                        break;
                    case "tycculsilo":  //울산silo전용
                        fsSMSPORT = "10000";
                        fsSMSID = "tycculsilo";
                        fsSMSPASS = "tyculsan2922";
                        break;
                    case "tycculutt":  //울산utt전용
                        fsSMSPORT = "10000";
                        fsSMSID = "tycculutt";
                        fsSMSPASS = "tyculsan2922";
                        break;
                    case "tyccsb":    //서울
                        fsSMSPORT = "10001";
                        fsSMSID = "tyccsb";
                        fsSMSPASS = "tyseoul2009";
                        break;
                }
            }


            if (!this.ShowMessage("TY_M_GB_34I35533"))
            {
                e.Successed = false;
                return;
            }


        }
        #endregion

        #region Description : 전표번호 조회
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZZZ07C1 popup = new TYAZZZ07C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_BILL_NO.SetValue(popup.fsJUNNO);

                UP_BILL_DETAIL();
            }            
        }
        #endregion

        #region Description : 전표상세정보 조회
        private void UP_BILL_DETAIL()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_34HB1504", this.TXT01_BILL_NO.GetValue());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBO01_AMEND_STATUS.Enabled = false;

            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["CHANGER_ID"].ToString() != "")
                {
                    CBO01_BILL_SENDID.SetValue(dt.Rows[0]["CHANGER_ID"].ToString());
                }
                else
                {
                    CBO01_BILL_SENDID.SetValue("tyccul");
                }

                TXT01_TAX_TYPE_CODE.SetValue( dt.Rows[0]["TAX_TYPE_CODE"].ToString());

                CBO01_BILL_CLASS.SetValue(dt.Rows[0]["BILL_CLASS"].ToString());
                if (dt.Rows[0]["BILL_CLASS"].ToString().Trim() == "02")
                {
                    CBO01_AMEND_STATUS.Enabled = true;
                    CBO01_AMEND_STATUS.SetValue(dt.Rows[0]["AMEND_STATUS"].ToString());
                    TXT01_ORI_ISSUE_ID.SetValue(dt.Rows[0]["ATTRIBUTES6"].ToString());
                }

                DTP01_BILL_DATE.SetValue(dt.Rows[0]["BILL_DATE"].ToString().Trim());
                TXT01_CHARGE_AMOUNT.SetValue( dt.Rows[0]["CHARGE_AMOUNT"].ToString());
                TXT01_TOTAL_TAX_AMOUNT.SetValue(dt.Rows[0]["TOTAL_TAX_AMOUNT"].ToString());
                TXT01_TOTAL_AMOUNT.SetValue( dt.Rows[0]["TOTAL_AMOUNT"].ToString().Trim());

                TXT01_SEND_BIZ_REGIST_NO.SetValue( dt.Rows[0]["SEND_BIZ_REGIST_NO"].ToString().Trim());
                TXT01_SEND_COMPANY_CODE.SetValue(dt.Rows[0]["SEND_COMPANY_CODE"].ToString().Trim());
                TXT01_SEND_COMPANY_NAME.SetValue(dt.Rows[0]["SEND_COMPANY_NAME"].ToString().Trim());
                TXT01_SEND_CEO_NAME.SetValue( dt.Rows[0]["SEND_CEO_NAME"].ToString().Trim());
                TXT01_SEND_ADDRESS.SetValue( dt.Rows[0]["SEND_ADDRESS"].ToString().Trim());
                TXT01_SEND_BUSINESS_STATUS.SetValue( dt.Rows[0]["SEND_BUSINESS_STATUS"].ToString().Trim());
                TXT01_SEND_BUSINESS_CLASS.SetValue( dt.Rows[0]["SEND_BUSINESS_CLASS"].ToString().Trim());
                TXT01_SEND_DEPARTMENT_NAME.SetValue( dt.Rows[0]["SEND_DEPARTMENT_NAME"].ToString().Trim());
                TXT01_SEND_USER_NAME.SetValue( dt.Rows[0]["SEND_USER_NAME"].ToString().Trim());
                TXT01_SEND_USER_TEL_NO.SetValue( dt.Rows[0]["SEND_USER_TEL_NO"].ToString().Trim());
                TXT01_SEND_USER_EMAIL.SetValue( dt.Rows[0]["SEND_USER_EMAIL"].ToString().Trim());

                TXT01_PRODUCT_NAME.SetValue( dt.Rows[0]["PRODUCT_NAME"].ToString().Trim());

                TXT01_RECV_BIZ_REGIST_NO.SetValue( dt.Rows[0]["RECV_BIZ_REGIST_NO"].ToString().Trim());

                TXT01_RECV_COMPANY_CODE.SetValue( dt.Rows[0]["RECV_COMPANY_CODE"].ToString().Trim());
                TXT01_RECV_COMPANY_NAME.SetValue( dt.Rows[0]["RECV_COMPANY_NAME"].ToString().Trim());
                TXT01_RECV_CEO_NAME.SetValue( dt.Rows[0]["RECV_CEO_NAME"].ToString().Trim());
                TXT01_RECV_ADDRESS.SetValue( dt.Rows[0]["RECV_ADDRESS"].ToString().Trim());
                TXT01_RECV_BUSINESS_STATUS.SetValue( dt.Rows[0]["RECV_BUSINESS_STATUS"].ToString().Trim());
                TXT01_RECV_BUSINESS_CLASS.SetValue( dt.Rows[0]["RECV_BUSINESS_CLASS"].ToString().Trim());

                TXT01_RECV_DEPARTMENT_NAME.SetValue( dt.Rows[0]["RECV_DEPARTMENT_NAME"].ToString().Trim());
                TXT01_RECV_USER_NAME.SetValue( dt.Rows[0]["RECV_USER_NAME"].ToString().Trim());
                TXT01_RECV_USER_TEL_NO.SetValue( dt.Rows[0]["RECV_USER_TEL_NO"].ToString().Trim());
                TXT01_RECV_USER_EMAIL1.SetValue( dt.Rows[0]["RECV_USER_EMAIL"].ToString().Trim());
                TXT01_RECV_USER_EMAIL2.SetValue( dt.Rows[0]["RECV_USER2_EMAIL"].ToString().Trim());
                TXT01_DESCRIPTION.SetValue( dt.Rows[0]["DESCRIPTION"].ToString().Trim());
                
                TXT01_SEND_STATUS.SetValue( dt.Rows[0]["SEND_STATUS_NAME"].ToString().Trim());

                TXT01_TAX_SEND_STATUS.SetValue(dt.Rows[0]["TAXSTATUS"].ToString().Trim());

                CBH01_CREATOR_ID.SetValue(dt.Rows[0]["CREATOR_ID"].ToString().Trim());

                //종사업장
                TXT01_BYR_BIZPLACE_CODE.SetValue(dt.Rows[0]["ATTRIBUTES7"].ToString().Trim());

                if (TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "전송실패" || TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "미전송" || TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "" )
                {
                    BTN61_BILL_SEND.Visible = false;
                    BTN61_BILL_REM.Visible = true;
                }
                else
                {
                    TXT01_SEND_STATUS.ForeColor = Color.Red; 
                    TXT01_TAX_SEND_STATUS.ForeColor = Color.Red; 

                    BTN61_BILL_SEND.Visible = false;
                    BTN61_BILL_REM.Visible = false;
                }
            }
            else
            {
                //미승인전표 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_34H3E517", this.TXT01_BILL_NO.GetValue().ToString().Substring(0, 6), this.TXT01_BILL_NO.GetValue().ToString().Substring(6, 8), this.TXT01_BILL_NO.GetValue().ToString().Substring(14, 3), this.TXT01_BILL_NO.GetValue().ToString().Substring(17, 2));

                DataTable dk = this.DbConnector.ExecuteDataTable();

                if (dk.Rows.Count > 0)
                {
                    DTP01_BILL_DATE.SetValue(dk.Rows[0]["B2VLMI3"].ToString().Trim());

                    TXT01_TAX_TYPE_CODE.SetValue(dk.Rows[0]["B2VLMI1"].ToString());

                    TXT01_CHARGE_AMOUNT.SetValue(dk.Rows[0]["B2VLMI4"].ToString().Trim());
                    TXT01_TOTAL_TAX_AMOUNT.SetValue(dk.Rows[0]["B2AMCR"].ToString().Trim());

                    double dTOTAL_AMOUNT = Convert.ToDouble(dk.Rows[0]["B2VLMI4"].ToString()) +
                                           Convert.ToDouble(dk.Rows[0]["B2AMCR"].ToString());

                    TXT01_TOTAL_AMOUNT.SetValue(dTOTAL_AMOUNT.ToString());

                    if ( dk.Rows[0]["B2CDAC"].ToString().Trim()  == "21103102")
                    {
                        TXT01_SEND_BIZ_REGIST_NO.SetValue("105-85-16181");
                        TXT01_SEND_COMPANY_CODE.SetValue("TAE001");
                        TXT01_SEND_COMPANY_NAME.SetValue("(주)태영인더스트리지점");
                        //TXT01_SEND_CEO_NAME.SetValue("정세진,윤석민");
                        TXT01_SEND_ADDRESS.SetValue("서울특별시 영등포구 여의공원로 111 태영빌딩 10층");
                        TXT01_SEND_BUSINESS_STATUS.SetValue("도매");
                        TXT01_SEND_BUSINESS_CLASS.SetValue("무역");
                    }
                    else
                    {
                        TXT01_SEND_BIZ_REGIST_NO.SetValue("610-81-10449");
                        TXT01_SEND_COMPANY_CODE.SetValue("TAE001");
                        TXT01_SEND_COMPANY_NAME.SetValue("(주)태영인더스트리");
                        //TXT01_SEND_CEO_NAME.SetValue("정세진,윤석민");
                        TXT01_SEND_ADDRESS.SetValue("울산 남구 용잠로 459");
                        TXT01_SEND_BUSINESS_STATUS.SetValue("운보서비스");
                        TXT01_SEND_BUSINESS_CLASS.SetValue("하역및보관업");
                    }

                    if (Convert.ToInt32(DTP01_BILL_DATE.GetString().ToString().Substring(0, 6)) > 201903)
                    {
                        TXT01_SEND_CEO_NAME.SetValue("정세진");
                    }
                    else
                    {
                        TXT01_SEND_CEO_NAME.SetValue("정세진,윤석민");
                    }


                    TXT01_SEND_DEPARTMENT_NAME.SetValue("");
                    TXT01_SEND_USER_NAME.SetValue("");
                    TXT01_SEND_USER_TEL_NO.SetValue("");
                    TXT01_SEND_USER_EMAIL.SetValue("");
                    TXT01_PRODUCT_NAME.SetValue("");

                    if (dk.Rows[0]["VNSJGB"].ToString().Trim() == "1")
                    {
                        //사업자
                        TXT01_RECV_BIZ_REGIST_NO.SetValue(dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(0, 3) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(3, 2) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(5, 5));
                    }
                    else
                    {
                        //개인
                        TXT01_RECV_BIZ_REGIST_NO.SetValue(dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(0, 6) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(6, 7));
                    }

                    TXT01_RECV_COMPANY_CODE.SetValue(dk.Rows[0]["VNCODE"].ToString().Trim());
                    TXT01_RECV_COMPANY_NAME.SetValue(dk.Rows[0]["VNSANGHO"].ToString().Trim());
                    TXT01_RECV_CEO_NAME.SetValue(dk.Rows[0]["VNIRUM"].ToString().Trim());

                    if (dk.Rows[0]["VNNEWADD"].ToString().Trim() == "")
                    {
                        TXT01_RECV_ADDRESS.SetValue(dk.Rows[0]["VNJUSO"].ToString().Trim());
                    }
                    else
                    {
                        TXT01_RECV_ADDRESS.SetValue(dk.Rows[0]["VNNEWADD"].ToString().Trim());
                    }

                    TXT01_RECV_BUSINESS_STATUS.SetValue(dk.Rows[0]["VNUPTE"].ToString().Trim());
                    TXT01_RECV_BUSINESS_CLASS.SetValue(dk.Rows[0]["VNUPJONG"].ToString().Trim());


                    TXT01_RECV_USER_EMAIL2.SetValue("");
                    TXT01_DESCRIPTION.SetValue("");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_C2P90113", this.TXT01_SEND_BIZ_REGIST_NO.GetValue().ToString().Replace("-", ""),
                                                                this.TXT01_RECV_BIZ_REGIST_NO.GetValue().ToString().Replace("-", ""));
                    DataTable dlog1 = this.DbConnector.ExecuteDataTable();

                    if (dlog1.Rows.Count > 0)
                    {
                        TXT01_RECV_DEPARTMENT_NAME.SetValue(dlog1.Rows[0]["BYR_DEPT_NAME"].ToString());
                        TXT01_RECV_USER_NAME.SetValue(dlog1.Rows[0]["BYR_EMP_NAME"].ToString());
                        TXT01_RECV_USER_TEL_NO.SetValue(dlog1.Rows[0]["BYR_TEL_NUM"].ToString());
                        TXT01_RECV_USER_EMAIL1.SetValue(dlog1.Rows[0]["BYR_EMAIL"].ToString());
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_34H3S518", TXT01_RECV_COMPANY_CODE.GetValue());
                        DataTable dlog = this.DbConnector.ExecuteDataTable();

                        if (dlog.Rows.Count > 0)
                        {
                            TXT01_RECV_DEPARTMENT_NAME.SetValue(dlog.Rows[0]["RECV_DEPARTMENT_NAME"].ToString().Trim());
                            TXT01_RECV_USER_NAME.SetValue(dlog.Rows[0]["RECV_USER_NAME"].ToString().Trim());
                            TXT01_RECV_USER_TEL_NO.SetValue(dlog.Rows[0]["RECV_USER_TEL_NO"].ToString().Trim());
                            TXT01_RECV_USER_EMAIL1.SetValue(dlog.Rows[0]["RECV_USER_EMAIL"].ToString().Trim());
                            TXT01_RECV_USER_EMAIL2.SetValue(dlog.Rows[0]["RECV_USER2_EMAIL"].ToString().Trim());
                            TXT01_DESCRIPTION.SetValue(dlog.Rows[0]["DESCRIPTION"].ToString().Trim());
                        }
                        else
                        {
                            TXT01_RECV_DEPARTMENT_NAME.SetValue("");
                            TXT01_RECV_USER_NAME.SetValue("");
                            TXT01_RECV_USER_TEL_NO.SetValue("");
                            TXT01_RECV_USER_EMAIL1.SetValue("");
                        }
                    }
                    

                    TXT01_SEND_STATUS.SetValue("수기");

                    TXT01_TAX_SEND_STATUS.SetValue("미전송");

                    CBH01_CREATOR_ID.SetValue(TYUserInfo.EmpNo); 

                    BTN61_BILL_SEND.Visible = true;
                    BTN61_BILL_REM.Visible = false; 
                }
            }

            this.SetFocus(TXT01_SEND_DEPARTMENT_NAME);

        }
        #endregion

        #region Description : 필드 readonly 설정
        private void UP_Fild_Lock()
        {
            TXT01_TAX_SEND_STATUS.SetReadOnly(true);

            TXT01_SEND_STATUS.SetReadOnly(true);
            DTP01_BILL_DATE.SetReadOnly(true);
            TXT01_TAX_TYPE_CODE.SetReadOnly(true);

            TXT01_CHARGE_AMOUNT.SetReadOnly(true);
            TXT01_TOTAL_TAX_AMOUNT.SetReadOnly(true);
            TXT01_TOTAL_AMOUNT.SetReadOnly(true);
            TXT01_SEND_BIZ_REGIST_NO.SetReadOnly(true);
            TXT01_SEND_COMPANY_CODE.SetReadOnly(true);
            TXT01_SEND_COMPANY_NAME.SetReadOnly(true);

            TXT01_SEND_CEO_NAME.SetReadOnly(true);
            TXT01_SEND_ADDRESS.SetReadOnly(true);
            TXT01_SEND_BUSINESS_STATUS.SetReadOnly(true);
            TXT01_SEND_BUSINESS_CLASS.SetReadOnly(true);

            TXT01_RECV_BIZ_REGIST_NO.SetReadOnly(true);
            TXT01_RECV_COMPANY_CODE.SetReadOnly(true);
            TXT01_RECV_COMPANY_NAME.SetReadOnly(true);
            TXT01_RECV_CEO_NAME.SetReadOnly(true);
            TXT01_RECV_ADDRESS.SetReadOnly(true);
            TXT01_RECV_BUSINESS_STATUS.SetReadOnly(true);
            TXT01_RECV_BUSINESS_CLASS.SetReadOnly(true);

            TXT04_BILL_NO.SetReadOnly(true);
            TXT01_ORI_ISSUE_ID.SetReadOnly(true);
            TXT01_APPROVE_ID.SetReadOnly(true);

            TXT01_AFFILENAME.SetReadOnly(true);
            TXT01_AFFILESIZE.SetReadOnly(true);
            TXT01_ATTACH_FILENAME.SetReadOnly(true);
        }
        #endregion

        #region Description : TXT01_BILL_NO_KeyDown
        private void TXT01_BILL_NO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN61_INQ_FXL_Click(null, null);
            }
        }
        #endregion

        #region Description : TXT01_BILL_NO_KeyDown
        private void CBO01_BILL_CLASS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_BILL_CLASS.GetValue().ToString() == "02")
            {
                BTN62_INQ_FXL.Visible = true;
                CBO01_AMEND_STATUS.Enabled = true;
                TXT04_BILL_NO.SetReadOnly(false);
                TXT01_ORI_ISSUE_ID.SetReadOnly(false);
            }
            else
            {
                BTN62_INQ_FXL.Visible = false;
                CBO01_AMEND_STATUS.Enabled = false;
                TXT04_BILL_NO.SetReadOnly(true);
                TXT01_ORI_ISSUE_ID.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 승인번호 조회
        private void BTN62_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZZZ07C2 popup = new TYAZZZ07C2();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT04_BILL_NO.SetValue(popup.fsJUNNO);
                this.TXT01_ORI_ISSUE_ID.SetValue(popup.fsAPPROVE_ID);
            }
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
                    sChkCONVERSATION = data_Main[i][0].ToString().Substring(0, 28);

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

        #region  Description :  UP_DataCollections_Add 이벤트
        private void UP_DataCollections_Add(string sGubn)
        {
            if (sGubn == "Main")
            {             
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

        #region  Description : 계산서 사이트 전송 / 수신 함수
        //Description : 계산서 사이트 전송

        private void UP_SMSBILL_WebServiceCall(string sBATCH_ID)
        {
            string sUrl = "http://192.168.100.32:" + fsSMSPORT + "/callSB_V3/XXSB_DTI_ARISSUE2.asp?BATCH_ID=" + sBATCH_ID.ToString() + "&ID=" + fsSMSID + "&PASS=" + fsSMSPASS;

            if ((new TYERGB013P(sUrl)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.UP_SMSBILL_TransResult(sBATCH_ID);
            }
        }

        // Description : 계산서 사이트 결과 처리 함수
        private void UP_SMSBILL_TransResult(string sBATCH_ID)
        {
            //전송결과 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75QH9644", sBATCH_ID);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if ((dt.Rows[0]["DTI_STATUS"].ToString().Trim() == "I" || dt.Rows[0]["DTI_STATUS"].ToString().Trim() == "O") && dt.Rows[0]["RETURN_CODE"].ToString().Trim() == "30000")
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.ShowMessage("TY_M_GB_34I9W523");
                    this.Close();
                }                
            }
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

        #region Description : XXSB 전표상세정보 조회
        private void UP_XXSBBILL_DETAIL()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_88TA2649", this.TXT01_BILL_NO.GetValue());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBO01_AMEND_STATUS.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                CBO01_BILL_SENDID.SetValue(dt.Rows[0]["SUP_COM_ID"].ToString());

                TXT01_CONVERSATION_ID.SetValue(dt.Rows[0]["CONVERSATION_ID"].ToString());

                TXT01_TAX_TYPE_CODE.SetValue(dt.Rows[0]["DTI_TYPE"].ToString());

                if (dt.Rows[0]["AMEND_CODE"].ToString() != "")
                {
                    CBO01_BILL_CLASS.SetValue("02");  //수정세금계산서
                }
                else
                {
                    CBO01_BILL_CLASS.SetValue("01");  //세금계산서
                }

                if (dt.Rows[0]["AMEND_CODE"].ToString().Trim() != "")
                {
                    CBO01_AMEND_STATUS.Enabled = true;
                    CBO01_AMEND_STATUS.SetValue(dt.Rows[0]["AMEND_CODE"].ToString());                    
                }

                TXT01_ORI_ISSUE_ID.SetValue(dt.Rows[0]["ORI_ISSUE_ID"].ToString());  //당초승인번호
                TXT01_APPROVE_ID.SetValue(dt.Rows[0]["APPROVE_ID"].ToString());      //승인번호

                DTP01_BILL_DATE.SetValue(dt.Rows[0]["DTI_WDATE"].ToString().Trim());
                TXT01_CHARGE_AMOUNT.SetValue(dt.Rows[0]["SUP_AMOUNT"].ToString());
                TXT01_TOTAL_TAX_AMOUNT.SetValue(dt.Rows[0]["TAX_AMOUNT"].ToString());
                TXT01_TOTAL_AMOUNT.SetValue(dt.Rows[0]["TOTAL_AMOUNT"].ToString().Trim());

                TXT01_SEND_BIZ_REGIST_NO.SetValue(dt.Rows[0]["SUP_COM_REGNO"].ToString().Trim());
                TXT01_SEND_COMPANY_CODE.SetValue(UP_Get_VNCODE(dt.Rows[0]["SUP_COM_REGNO"].ToString().Trim()));
                TXT01_SEND_COMPANY_NAME.SetValue(dt.Rows[0]["SUP_COM_NAME"].ToString().Trim());
                TXT01_SEND_CEO_NAME.SetValue(dt.Rows[0]["SUP_REP_NAME"].ToString().Trim());
                TXT01_SEND_ADDRESS.SetValue(dt.Rows[0]["SUP_COM_ADDR"].ToString().Trim());
                TXT01_SEND_BUSINESS_STATUS.SetValue(dt.Rows[0]["SUP_COM_TYPE"].ToString().Trim());
                TXT01_SEND_BUSINESS_CLASS.SetValue(dt.Rows[0]["SUP_COM_CLASSIFY"].ToString().Trim());
                TXT01_SEND_DEPARTMENT_NAME.SetValue(dt.Rows[0]["SUP_DEPT_NAME"].ToString().Trim());
                TXT01_SEND_USER_NAME.SetValue(dt.Rows[0]["SUP_EMP_NAME"].ToString().Trim());
                TXT01_SEND_USER_TEL_NO.SetValue(dt.Rows[0]["SUP_TEL_NUM"].ToString().Trim());
                TXT01_SEND_USER_EMAIL.SetValue(dt.Rows[0]["SUP_EMAIL"].ToString().Trim());

                TXT01_PRODUCT_NAME.SetValue(dt.Rows[0]["ITEM_NAME"].ToString().Trim());

                TXT01_RECV_BIZ_REGIST_NO.SetValue(dt.Rows[0]["BYR_COM_REGNO"].ToString().Trim());
                TXT01_RECV_COMPANY_CODE.SetValue(UP_Get_VNCODE(dt.Rows[0]["BYR_COM_REGNO"].ToString().Trim()));
                TXT01_RECV_COMPANY_NAME.SetValue(dt.Rows[0]["BYR_COM_NAME"].ToString().Trim());
                TXT01_RECV_CEO_NAME.SetValue(dt.Rows[0]["BYR_REP_NAME"].ToString().Trim());
                TXT01_RECV_ADDRESS.SetValue(dt.Rows[0]["BYR_COM_ADDR"].ToString().Trim());
                TXT01_RECV_BUSINESS_STATUS.SetValue(dt.Rows[0]["BYR_COM_TYPE"].ToString().Trim());
                TXT01_RECV_BUSINESS_CLASS.SetValue(dt.Rows[0]["BYR_COM_CLASSIFY"].ToString().Trim());
                TXT01_RECV_DEPARTMENT_NAME.SetValue(dt.Rows[0]["BYR_DEPT_NAME"].ToString().Trim());
                TXT01_RECV_USER_NAME.SetValue(dt.Rows[0]["BYR_EMP_NAME"].ToString().Trim());

                TXT01_RECV_USER_TEL_NO.SetValue(dt.Rows[0]["BYR_TEL_NUM"].ToString().Trim());
                TXT01_RECV_USER_EMAIL1.SetValue(dt.Rows[0]["BYR_EMAIL"].ToString().Trim());
                TXT01_RECV_USER_EMAIL2.SetValue("");
                TXT01_DESCRIPTION.SetValue(dt.Rows[0]["REMARK"].ToString().Trim());

                TXT01_SEND_STATUS.SetValue(dt.Rows[0]["DTI_STATUSNM"].ToString().Trim());  //계산서 전송상태

                TXT01_TAX_SEND_STATUS.SetValue(dt.Rows[0]["TAXTRANSSTATUS"].ToString().Trim());  //국세청 전송상태

                CBH01_CREATOR_ID.SetValue(dt.Rows[0]["CREATED_BY"].ToString().Trim());

                //종사업장
                TXT01_BYR_BIZPLACE_CODE.SetValue(dt.Rows[0]["BYR_BIZPLACE_CODE"].ToString().Trim());

                if (TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "전송실패" || TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "미전송" || TXT01_TAX_SEND_STATUS.GetValue().ToString().Trim() == "")
                {
                    BTN61_BILL_SEND.Visible = false;
                    BTN61_BILL_REM.Visible = true;
                }
                else
                {
                    TXT01_SEND_STATUS.ForeColor = Color.Red;
                    TXT01_TAX_SEND_STATUS.ForeColor = Color.Red;

                    BTN61_BILL_SEND.Visible = false;
                    BTN61_BILL_REM.Visible = false;
                }

                if (dt.Rows[0]["ATTACHFILE_YN"].ToString().Trim() == "Y")
                {
                    //첨부파일 표시
                    TXT01_AFFILENAME.SetValue(dt.Rows[0]["FILE_NAME"].ToString().Trim());
                    TXT01_AFFILESIZE.SetValue(dt.Rows[0]["FILE_SIZE"].ToString().Trim());

                    _fbAttachFile = dt.Rows[0]["FILE_BINARY"] as byte[];

                    this.BTN61_DOWN.Visible = true;
                    this.BTN61_SEARCH.Visible = false;
                }
            }
            else
            {
                //미승인전표 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_34H3E517", this.TXT01_BILL_NO.GetValue().ToString().Substring(0, 6), this.TXT01_BILL_NO.GetValue().ToString().Substring(6, 8), this.TXT01_BILL_NO.GetValue().ToString().Substring(14, 3), this.TXT01_BILL_NO.GetValue().ToString().Substring(17, 2));

                DataTable dk = this.DbConnector.ExecuteDataTable();

                if (dk.Rows.Count > 0)
                {
                    DTP01_BILL_DATE.SetValue(dk.Rows[0]["B2VLMI3"].ToString().Trim());

                    TXT01_TAX_TYPE_CODE.SetValue(dk.Rows[0]["B2VLMI1"].ToString());

                    TXT01_CHARGE_AMOUNT.SetValue(dk.Rows[0]["B2VLMI4"].ToString().Trim());
                    TXT01_TOTAL_TAX_AMOUNT.SetValue(dk.Rows[0]["B2AMCR"].ToString().Trim());

                    double dTOTAL_AMOUNT = Convert.ToDouble(dk.Rows[0]["B2VLMI4"].ToString()) +
                                           Convert.ToDouble(dk.Rows[0]["B2AMCR"].ToString());

                    TXT01_TOTAL_AMOUNT.SetValue(dTOTAL_AMOUNT.ToString());

                    if (dk.Rows[0]["B2CDAC"].ToString().Trim() == "21103102")
                    {
                        TXT01_SEND_BIZ_REGIST_NO.SetValue("105-85-16181");
                        TXT01_SEND_COMPANY_CODE.SetValue("TAE001");
                        TXT01_SEND_COMPANY_NAME.SetValue("(주)태영인더스트리지점");
                        TXT01_SEND_CEO_NAME.SetValue("정세진,윤석민");
                        TXT01_SEND_ADDRESS.SetValue("서울특별시 영등포구 여의공원로 111 태영빌딩 10층");
                        TXT01_SEND_BUSINESS_STATUS.SetValue("도매");
                        TXT01_SEND_BUSINESS_CLASS.SetValue("무역");
                    }
                    else
                    {
                        TXT01_SEND_BIZ_REGIST_NO.SetValue("610-81-10449");
                        TXT01_SEND_COMPANY_CODE.SetValue("TAE001");
                        TXT01_SEND_COMPANY_NAME.SetValue("(주)태영인더스트리");
                        TXT01_SEND_CEO_NAME.SetValue("정세진,윤석민");
                        TXT01_SEND_ADDRESS.SetValue("울산 남구 용잠로 459");
                        TXT01_SEND_BUSINESS_STATUS.SetValue("운보서비스");
                        TXT01_SEND_BUSINESS_CLASS.SetValue("하역및보관업");
                    }


                    TXT01_SEND_DEPARTMENT_NAME.SetValue("");
                    TXT01_SEND_USER_NAME.SetValue("");
                    TXT01_SEND_USER_TEL_NO.SetValue("");
                    TXT01_SEND_USER_EMAIL.SetValue("");
                    TXT01_PRODUCT_NAME.SetValue("");

                    if (dk.Rows[0]["VNSJGB"].ToString().Trim() == "1")
                    {
                        //사업자
                        TXT01_RECV_BIZ_REGIST_NO.SetValue(dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(0, 3) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(3, 2) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(5, 5));
                    }
                    else
                    {
                        //개인
                        TXT01_RECV_BIZ_REGIST_NO.SetValue(dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(0, 6) + "-" +
                                                          dk.Rows[0]["VNSAUPNO"].ToString().Trim().Substring(6, 7));
                    }

                    TXT01_RECV_COMPANY_CODE.SetValue(dk.Rows[0]["VNCODE"].ToString().Trim());
                    TXT01_RECV_COMPANY_NAME.SetValue(dk.Rows[0]["VNSANGHO"].ToString().Trim());
                    TXT01_RECV_CEO_NAME.SetValue(dk.Rows[0]["VNIRUM"].ToString().Trim());

                    if (dk.Rows[0]["VNNEWADD"].ToString().Trim() == "")
                    {
                        TXT01_RECV_ADDRESS.SetValue(dk.Rows[0]["VNJUSO"].ToString().Trim());
                    }
                    else
                    {
                        TXT01_RECV_ADDRESS.SetValue(dk.Rows[0]["VNNEWADD"].ToString().Trim());
                    }

                    TXT01_RECV_BUSINESS_STATUS.SetValue(dk.Rows[0]["VNUPTE"].ToString().Trim());
                    TXT01_RECV_BUSINESS_CLASS.SetValue(dk.Rows[0]["VNUPJONG"].ToString().Trim());



                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_34H3S518", TXT01_RECV_COMPANY_CODE.GetValue());
                    DataTable dlog = this.DbConnector.ExecuteDataTable();

                    if (dlog.Rows.Count > 0)
                    {
                        TXT01_RECV_DEPARTMENT_NAME.SetValue(dlog.Rows[0]["RECV_DEPARTMENT_NAME"].ToString().Trim());
                        TXT01_RECV_USER_NAME.SetValue(dlog.Rows[0]["RECV_USER_NAME"].ToString().Trim());
                        TXT01_RECV_USER_TEL_NO.SetValue(dlog.Rows[0]["RECV_USER_TEL_NO"].ToString().Trim());
                        TXT01_RECV_USER_EMAIL1.SetValue(dlog.Rows[0]["RECV_USER_EMAIL"].ToString().Trim());
                        TXT01_RECV_USER_EMAIL2.SetValue(dlog.Rows[0]["RECV_USER2_EMAIL"].ToString().Trim());
                        TXT01_DESCRIPTION.SetValue(dlog.Rows[0]["DESCRIPTION"].ToString().Trim());
                    }
                    else
                    {
                        TXT01_RECV_DEPARTMENT_NAME.SetValue("");
                        TXT01_RECV_USER_NAME.SetValue("");
                        TXT01_RECV_USER_TEL_NO.SetValue("");
                        TXT01_RECV_USER_EMAIL1.SetValue("");
                        TXT01_RECV_USER_EMAIL2.SetValue("");
                        TXT01_DESCRIPTION.SetValue("");
                    }

                    TXT01_SEND_STATUS.SetValue("수기");

                    TXT01_TAX_SEND_STATUS.SetValue("미전송");

                    CBH01_CREATOR_ID.SetValue(TYUserInfo.EmpNo);

                    BTN61_BILL_SEND.Visible = true;
                    BTN61_BILL_REM.Visible = false;

                    this.BTN61_DOWN.Visible = false;
                }
            }

            this.SetFocus(TXT01_SEND_DEPARTMENT_NAME);

        }
        #endregion

        #region Description : 거래처코드 리턴 함수
        private string UP_Get_VNCODE(string sVNSAUPNO)
        {
            string sVNCODE = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B19E974", sVNSAUPNO.Replace("-","").Trim());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sVNCODE = dt.Rows[0]["VNCODE"].ToString();
            }

            return sVNCODE;
        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 찾아보기 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            this.TXT01_AFFILENAME.SetValue("");

            OpenFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (this.OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_ATTACH_FILENAME.Text = this.OpenFileDialog.FileName;

                this.TXT01_AFFILENAME.SetValue(UP_Set_FileName(this.TXT01_ATTACH_FILENAME.Text));

                byte[] _AttachFile = null;                
                string filePath = this.TXT01_ATTACH_FILENAME.GetValue().ToString();
                _AttachFile = UP_Get_Byte(filePath);

                int ArraySize = _AttachFile.GetUpperBound(0);

                this.TXT01_AFFILESIZE.SetValue(ArraySize.ToString());
            }  
        }
        #endregion

        #region Descrioption : 파일 이름 가져오기
        protected string UP_Set_FileName(string sStr)
        {
            string sValue = "";
            int i = 0;
            int iPoint = 0;
            for (i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) == "\\")
                {
                    iPoint = i;
                }
            }

            for (i = iPoint + 1; i < sStr.Length; i++)
            {
                sValue = sValue + sStr.Substring(i, 1);
            }

            return sValue;
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

        #region  Description : 파일 다운 로드
        private void BTN61_DOWN_Click(object sender, EventArgs e)
        {           

            FileStream stream = null;            

            try
            {
                this.SaveFileDialog.FileName = this.TXT01_AFFILENAME.GetValue().ToString();

                if (this.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                string fileName = this.SaveFileDialog.FileName;

                int ArraySize = _fbAttachFile.GetUpperBound(0);
                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_fbAttachFile, 0, ArraySize + 1);

                this.ShowMessage("TY_M_GB_25UAA711");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        #endregion
    }
}
