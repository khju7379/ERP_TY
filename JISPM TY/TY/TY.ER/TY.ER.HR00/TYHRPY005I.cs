using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.AC00;


namespace TY.ER.HR00
{
    /// <summary>
    /// 개인급여기준관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.11 17:34
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_HR_4CBHT734 : 개인급여기준관리 조회(사번별)
    ///  TY_P_HR_4CBHW738 : 개인급여추가제외관리 조회(사번별)
    ///  TY_P_HR_4CBI3742 : 개인급여예외관리 조회(사번별)
    ///  TY_P_HR_4CBI3744 : 개인별급여계좌 조회(사번별)
    ///  TY_P_HR_4CBI7743 : 개인별 보수월액 조회(사번별)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CBHN733 : 인사기본사항 조회
    ///  TY_S_HR_4CBIL747 : 개인급여기준관리 조회
    ///  TY_S_HR_4CBIN748 : 개인급여항목추가관리 조회
    ///  TY_S_HR_4CBIP749 : 개인급여항목제외관리 조회
    ///  TY_S_HR_4CBIQ751 : 개인급여계좌관리 조회
    ///  TY_S_HR_4CBIR752 : 개인보수월액관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBBALCD : 발령코드
    ///  KBJJCD : 직위
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  KBCODE : 지급구분
    ///  KBGUNMU : 근무처
    ///  KBBDATE : 발령일자
    ///  KBIDATE : 입사일자
    ///  KBBSTEAM : 부서(반)
    ///  KBBUSEO : 부서
    ///  KBHANGL : 한글이름
    ///  KBHOBN : 호봉
    ///  KBSOSOK : 소속
    /// </summary>
    public partial class TYHRPY005I : TYBase
    {
        private string fsKBSABN;

        #region  Description : 폼 로드 이벤트
        public TYHRPY005I(string sKBSABUN)
        {
            fsKBSABN = sKBSABUN;

            InitializeComponent();
            //개인급여기준관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIL747, "PDPAYCODE", "PDPAYCODENM", "PDPAYCODE");
            //개인급여추가
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIN748, "PCGUBN", "PCGUBNNM", "PCGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIN748, "PCPAYCODE", "PCPAYCODENM", "PCPAYCODE");
            //개인급여제외관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIP749, "PCGUBN", "PCGUBNNM", "PCGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIP749, "PCPAYCODE", "PCPAYCODENM", "PCPAYCODE");
            //개인급여예외관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBJD769, "PXGUBN", "PXGUBNNM", "PXGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBJD769, "PXEXCODE", "PXEXCODENM", "PXEXCODE");
            //개인 보수월액관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIR752, "TAPAYCODE", "TAPAYCODENM", "TAPAYCODE");

            //계좌관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CBIQ751, "TCBANKCODE", "TCBANKCODENM", "TCBANKCODE");
            
        }

        private void TYHRPY005I_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateDATE = new ToolStripMenuItem("일괄종료");
            reateDATE.Click += new EventHandler(UpdateDATE_ToolStripMenuItem_Click);

            ToolStripMenuItem reatePAYCOPY = new ToolStripMenuItem("급여복사");
            reatePAYCOPY.Click += new EventHandler(PAYCOPY_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_4CBIL747.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateDATE, reatePAYCOPY });


            //개입급여기준관리
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIL747, "PDPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIL747, "PDPAYCODENM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIL747, "PDSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDPAYCODE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDPAYCODENM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDSTAMOUNT");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDSTRATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDSDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIL747, "PDEDATE");                      

            //개인급여추가관리
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIN748, "PCGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIN748, "PCGUBNNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIN748, "PCPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIN748, "PCPAYCODENM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIN748, "PCSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCGUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCPAYCODE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCPAYCODENM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCGVAMOUNT");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCSDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIN748, "PCEDATE");

            //개인급여제외관리
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIP749, "PCGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIP749, "PCGUBNNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIP749, "PCPAYCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIP749, "PCPAYCODENM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIP749, "PCSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIP749, "PCGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIP749, "PCGUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIP749, "PCPAYCODE");            
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIP749, "PCSDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIP749, "PCEDATE");

            //개인급여예외관리
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBJD769, "PXGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBJD769, "PXGUBNNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBJD769, "PXEXCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBJD769, "PXEXCODENM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBJD769, "PXSDATE");

            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXGUBN");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXGUBNNM");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXEXCODE");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXEXCODENM");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXSDATE");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBJD769, "PXEDATE");

            //월보수
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIR752,"TAPAYCODE", "TASDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIR752, "TAPAYCODE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIR752, "TASDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIR752, "TAEDATE");

            //계좌관리
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CBIQ751, "TCSDATE");

            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIQ751, "TCBANKCODE");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIQ751, "TCBANKCODENM");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIQ751, "TCSDATE");
            //this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4CBIQ751, "TCEDATE");

            (this.FPS91_TY_S_HR_4CBJD769.Sheets[0].Columns[9].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            
                        
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_KBSABUN.SetValue(fsKBSABN);
            this.BTN61_INQ_Click(null, null);

            UP_Run(fsKBSABN);

            this.CBH02_KBSABUN.SetReadOnly(true);

        }
        #endregion

        #region  Description : 데이타 바인딩
        private void UP_Run(string sKBSABUN)
        {
            this.UP_GetInsaStandDataBinding(sKBSABUN);
            this.UP_GetPayDataBinding(sKBSABUN);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            if( this.CBH02_KBSABUN.GetValue().ToString() != "" )
              UP_Run(this.CBH02_KBSABUN.GetValue().ToString());
        }
        #endregion

        #region  Description : 사번 리스트 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4CBHN733.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A2REZ964", this.TXT01_KBHANGL.GetValue(), this.CBH01_KBSABUN.GetValue().ToString());
            this.FPS91_TY_S_HR_4CBHN733.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //개인급여기준관리
            this.DbConnector.Attach("TY_P_HR_4CCDE788", ds.Tables[0]);
            //개인급여추가항목관리
            this.DbConnector.Attach("TY_P_HR_4CCDM791", ds.Tables[1]);
            //개인급여제외관리
            this.DbConnector.Attach("TY_P_HR_4CCDM791", ds.Tables[2]);
            //개인예외사항관리
            this.DbConnector.Attach("TY_P_HR_4CCDS796", ds.Tables[3]);
            //보수월액
            this.DbConnector.Attach("TY_P_HR_4CCDZ802", ds.Tables[4]);
            //급여계좌
            this.DbConnector.Attach("TY_P_HR_4CCEB805", ds.Tables[5]);

            this.DbConnector.ExecuteTranQueryList();

            UP_Run(this.CBH02_KBSABUN.GetValue().ToString());

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();
            //개인급여기준관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.Remove, "PDSABUN", "PDPAYCODE", "PDSDATE"));
            //개인급여추가항목관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIN748.GetDataSourceInclude(TSpread.TActionType.Remove, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCSDATE"));
            //개인급여제외관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIP749.GetDataSourceInclude(TSpread.TActionType.Remove, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCSDATE"));
            //개인예외사항관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBJD769.GetDataSourceInclude(TSpread.TActionType.Remove, "PXSABUN", "PXGUBN", "PXEXCODE", "PXSDATE"));
            //보수월액
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIR752.GetDataSourceInclude(TSpread.TActionType.Remove, "TASABUN","TAPAYCODE", "TASDATE"));
            //급여계좌
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIQ751.GetDataSourceInclude(TSpread.TActionType.Remove, "TCSABUN", "TCSDATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 &&
                ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0 &&
                ds.Tables[4].Rows.Count == 0 && ds.Tables[5].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //체크
            //개인급여기준관리
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //급여결과내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[0].Rows[i]["PDSABUN"].ToString(), ds.Tables[0].Rows[i]["PDPAYCODE"].ToString(), ds.Tables[0].Rows[i]["PDSDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여결과내역에 급여코드가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //개인급여추가항목관리
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                //급여결과내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[1].Rows[i]["PCSABUN"].ToString(), ds.Tables[1].Rows[i]["PCPAYCODE"].ToString(), ds.Tables[1].Rows[i]["PCSDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여결과내역에 급여코드가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            //개인급여제외관리
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                //급여결과내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[1].Rows[i]["PCSABUN"].ToString(), ds.Tables[1].Rows[i]["PCPAYCODE"].ToString(), ds.Tables[1].Rows[i]["PCSDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여결과내역에 급여코드가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            //개인예외사항관리


            //보수월액
            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {
                //급여결과내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[4].Rows[i]["TASABUN"].ToString(), ds.Tables[4].Rows[i]["TAPAYCODE"].ToString(), ds.Tables[4].Rows[i]["TASDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여결과내역에 급여코드가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //급여계좌
            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
            {
                //급여마스타내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BO8W207", ds.Tables[5].Rows[i]["TCSABUN"].ToString(), ds.Tables[5].Rows[i]["TCSDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여자료가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }                
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            
            //개인급여기준관리
            this.DataTableColumnAdd(ds.Tables[0], "PDHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PDHISAB", TYUserInfo.EmpNo);

            //인사기본사항 장기요양 할인율 update
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_91ODX579",  Get_Numeric(TXT02_KBLTERMDCRATE.GetValue().ToString()), TYUserInfo.EmpNo, "2",  CBH02_KBSABUN.GetValue().ToString()
                                                        );

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDC786", ds.Tables[0].Rows[i]["PDSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDPAYCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDSDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDSTAMOUNT"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDSTRATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["PDBIGO"].ToString(),
                                                                ds.Tables[0].Rows[i]["PDHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDE787", ds.Tables[1].Rows[i]["PDSTAMOUNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDSTRATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["PDBIGO"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDPAYCODE"].ToString(),
                                                                ds.Tables[1].Rows[i]["PDSDATE"].ToString()                                                                
                                                                );
                }
            }

            //개인급여추가항목관리
            this.DataTableColumnAdd(ds.Tables[2], "PCHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[3], "PCHISAB", TYUserInfo.EmpNo);
            
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDK789", ds.Tables[2].Rows[i]["PCJOBGN"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCGUBN"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCPAYCODE"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCSDATE"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCGVAMOUNT"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[2].Rows[i]["PCMEMO"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCBIGO"].ToString(),
                                                                ds.Tables[2].Rows[i]["PCHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDM790", ds.Tables[3].Rows[i]["PCGVAMOUNT"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[3].Rows[i]["PCMEMO"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCBIGO"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCHISAB"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCJOBGN"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCGUBN"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCPAYCODE"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCSDATE"].ToString()
                                                                );
                }
            }

            //개인급여제외항목관리
            this.DataTableColumnAdd(ds.Tables[4], "PCHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[5], "PCHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[4].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDK789", ds.Tables[4].Rows[i]["PCJOBGN"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCGUBN"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCPAYCODE"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCSDATE"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCGVAMOUNT"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[4].Rows[i]["PCMEMO"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCBIGO"].ToString(),
                                                                ds.Tables[4].Rows[i]["PCHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDM790", ds.Tables[5].Rows[i]["PCGVAMOUNT"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[5].Rows[i]["PCMEMO"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCBIGO"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCHISAB"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCJOBGN"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCGUBN"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCPAYCODE"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCSDATE"].ToString()
                                                                );
                }
            }

            //개인예외사항관리
            this.DataTableColumnAdd(ds.Tables[6], "PXHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[7], "PXHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[6].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDP792", ds.Tables[6].Rows[i]["PXSABUN"].ToString(),
                                                                ds.Tables[6].Rows[i]["PXGUBN"].ToString(),
                                                                ds.Tables[6].Rows[i]["PXEXCODE"].ToString(),
                                                                ds.Tables[6].Rows[i]["PXSDATE"].ToString(),                                                                
                                                                ds.Tables[6].Rows[i]["PXEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[6].Rows[i]["PXPAYRATE"].ToString(),
                                                                ds.Tables[6].Rows[i]["PXMEMO"].ToString(),
                                                                ds.Tables[6].Rows[i]["PXHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDQ793", ds.Tables[7].Rows[i]["PXEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[7].Rows[i]["PXPAYRATE"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXMEMO"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXHISAB"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXSABUN"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXGUBN"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXEXCODE"].ToString(),
                                                                ds.Tables[7].Rows[i]["PXSDATE"].ToString()
                                                                );

                    this.DbConnector.Attach("TY_P_HR_53B8H646", ds.Tables[7].Rows[i]["PXEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                                 ds.Tables[7].Rows[i]["PXHISAB"].ToString(),
                                                                                 ds.Tables[7].Rows[i]["PXSABUN"].ToString(),
                                                                                 ds.Tables[7].Rows[i]["PXGUBN"].ToString(),
                                                                                 ds.Tables[7].Rows[i]["PXEXCODE"].ToString(),
                                                                                 ds.Tables[7].Rows[i]["PXSDATE"].ToString()
                                                                                 );
                }
            }

            //보수월액
            this.DataTableColumnAdd(ds.Tables[8], "TAHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[9], "TAHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[8].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDY800", ds.Tables[8].Rows[i]["TASABUN"].ToString(),
                                                                ds.Tables[8].Rows[i]["TAPAYCODE"].ToString(),
                                                                ds.Tables[8].Rows[i]["TASDATE"].ToString(),
                                                                ds.Tables[8].Rows[i]["TAAVGAMOUNT"].ToString(),
                                                                ds.Tables[8].Rows[i]["TAEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[8].Rows[i]["TAHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[9].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDY801", ds.Tables[9].Rows[i]["TAAVGAMOUNT"].ToString(),
                                                                ds.Tables[9].Rows[i]["TAEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[9].Rows[i]["TAHISAB"].ToString(),
                                                                ds.Tables[9].Rows[i]["TASABUN"].ToString(),
                                                                ds.Tables[9].Rows[i]["TAPAYCODE"].ToString(),
                                                                ds.Tables[9].Rows[i]["TASDATE"].ToString()
                                                                );
                }
            }

            //급여계좌
            this.DataTableColumnAdd(ds.Tables[10], "TCHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[11], "TCHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[10].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCE5803", ds.Tables[10].Rows[i]["TCSABUN"].ToString(),
                                                                ds.Tables[10].Rows[i]["TCSDATE"].ToString(),
                                                                ds.Tables[10].Rows[i]["TCBANKCODE"].ToString(),
                                                                ds.Tables[10].Rows[i]["TCACCOUNT"].ToString(),
                                                                ds.Tables[10].Rows[i]["TCACNAME"].ToString(),
                                                                ds.Tables[10].Rows[i]["TCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[10].Rows[i]["TCHISAB"].ToString()
                                                                );
                }
            }
            if (ds.Tables[11].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCEB804", ds.Tables[11].Rows[i]["TCBANKCODE"].ToString(),
                                                                ds.Tables[11].Rows[i]["TCACCOUNT"].ToString(),
                                                                ds.Tables[11].Rows[i]["TCACNAME"].ToString(),
                                                                ds.Tables[11].Rows[i]["TCEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[11].Rows[i]["TCHISAB"].ToString(),
                                                                ds.Tables[11].Rows[i]["TCSABUN"].ToString(),
                                                                ds.Tables[11].Rows[i]["TCSDATE"].ToString()
                                                                );
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            UP_Run(this.CBH02_KBSABUN.GetValue().ToString());

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();
            //개인급여기준관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.New, "PDSABUN", "PDPAYCODE", "PDSTAMOUNT", "PDSTRATE", "PDSDATE", "PDEDATE", "PDBIGO"));
            //ds.Tables.Add(this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.Update, "PDSABUN", "PDPAYCODE", "PDSTAMOUNT", "PDSDATE", "PDEDATE", "PDBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.Select, "PDSABUN", "PDPAYCODE", "PDSTAMOUNT", "PDSTRATE", "PDSDATE", "PDEDATE", "PDBIGO"));
            //개인급여추가항목관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIN748.GetDataSourceInclude(TSpread.TActionType.New, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCGVAMOUNT", "PCSDATE", "PCEDATE", "PCMEMO", "PCBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIN748.GetDataSourceInclude(TSpread.TActionType.Update, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCGVAMOUNT", "PCSDATE", "PCEDATE", "PCMEMO", "PCBIGO"));
            //개인급여제외관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIP749.GetDataSourceInclude(TSpread.TActionType.New, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCGVAMOUNT", "PCSDATE", "PCEDATE", "PCMEMO","PCBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIP749.GetDataSourceInclude(TSpread.TActionType.Update, "PCJOBGN", "PCSABUN", "PCGUBN", "PCPAYCODE", "PCGVAMOUNT", "PCSDATE", "PCEDATE", "PCMEMO","PCBIGO"));
            //개인예외사항관리
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBJD769.GetDataSourceInclude(TSpread.TActionType.New, "PXSABUN", "PXGUBN", "PXEXCODE", "PXPAYRATE","PXSDATE","PXEDATE","PXMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBJD769.GetDataSourceInclude(TSpread.TActionType.Update, "PXSABUN", "PXGUBN", "PXEXCODE", "PXPAYRATE", "PXSDATE", "PXEDATE", "PXMEMO"));
            //보수월액
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIR752.GetDataSourceInclude(TSpread.TActionType.New, "TASABUN","TAPAYCODE", "TAAVGAMOUNT", "TASDATE", "TAEDATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIR752.GetDataSourceInclude(TSpread.TActionType.Update, "TASABUN", "TAPAYCODE","TAAVGAMOUNT", "TASDATE", "TAEDATE"));
            //급여계좌
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIQ751.GetDataSourceInclude(TSpread.TActionType.New, "TCSABUN", "TCBANKCODE", "TCACCOUNT", "TCACNAME", "TCSDATE", "TCEDATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CBIQ751.GetDataSourceInclude(TSpread.TActionType.Update, "TCSABUN", "TCBANKCODE", "TCACCOUNT", "TCACNAME", "TCSDATE", "TCEDATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 &&
                ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0 &&
                ds.Tables[4].Rows.Count == 0 && ds.Tables[5].Rows.Count == 0 &&
                ds.Tables[6].Rows.Count == 0 && ds.Tables[7].Rows.Count == 0 &&
                ds.Tables[8].Rows.Count == 0 && ds.Tables[9].Rows.Count == 0 &&
                ds.Tables[10].Rows.Count == 0 && ds.Tables[11].Rows.Count == 0 )
            {
                //this.ShowMessage("TY_M_GB_2452W459");
                //e.Successed = false;
                //return;
            }
            else
            {
                //개인급여체크
                DataTable dt = new DataTable();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                    
                    if (ds.Tables[0].Rows[i]["PDSDATE"].ToString().Replace("19000101", "") == "") 
                    {
                        this.ShowCustomMessage("급여기준정보의 시작일자는 필수사항입니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }

                    //진행중인 급여코드가 있는지 체크
                    if (i == 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_4CBHT734", ds.Tables[0].Rows[i]["PDSABUN"].ToString(), "A");
                        dt = this.DbConnector.ExecuteDataTable();
                    }

                    foreach (DataRow dr in dt.Select("PDPAYCODE = '" + ds.Tables[0].Rows[i]["PDPAYCODE"].ToString() + "'", "PDPAYCODE ASC"))
                    {
                        if (dr["PDEDATE"].ToString() == "")
                        {
                            this.ShowCustomMessage("진행중인 급여가 존재합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            e.Successed = false;
                            return;
                        }
                    }

                    //기준금액, 기준율을 동시에 등록 할수 없다.
                    if (Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["PDSTAMOUNT"].ToString().Trim())) > 0 && Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["PDSTRATE"].ToString().Trim())) > 0 )
                     {
                         this.ShowCustomMessage("기준금액과 기준율을 동시에 등록 할 수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                         e.Successed = false;
                         return;
                     }
                }

                // 개인급여기준관리 수정시 체크               
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    ////기준일이후 급여지급시 수정 불가
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[1].Rows[i]["PDSABUN"].ToString(),
                    //                                            ds.Tables[1].Rows[i]["PDPAYCODE"].ToString(),
                    //                                            ds.Tables[1].Rows[i]["PDSDATE"].ToString()
                    //                                            );
                    //iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                    //if (iCnt > 0)
                    //{
                    //    this.ShowCustomMessage("급여지급내역이 존재합니다! 수정 할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    //    e.Successed = false;
                    //    return;
                    //}

                    //기준금액, 기준율을 동시에 등록 할수 없다.
                    if (Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["PDSTAMOUNT"].ToString().Trim())) > 0 && Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["PDSTRATE"].ToString().Trim())) > 0)
                    {
                        this.ShowCustomMessage("기준금액과 기준율을 동시에 등록 할 수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                //개인급여항목추가 체크
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    if (ds.Tables[2].Rows[i]["PCSDATE"].ToString().Replace("19000101", "") == "")
                    {
                        this.ShowCustomMessage("급여항목추가시에는 시작일자 필수사항입니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[2].Rows[i]["PCEDATE"].ToString().Replace("19000101", "") == "")
                    {
                        this.ShowCustomMessage("급여항목추가시에는 종료일자 필수사항입니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                //개인급여항목추가 수정 체크
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    //기준일이후 급여지급시 수정 불가
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[3].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCPAYCODE"].ToString(),
                                                                ds.Tables[3].Rows[i]["PCEDATE"].ToString()
                                                                //ds.Tables[3].Rows[i]["PCSDATE"].ToString()                                                                
                                                                );
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("급여지급내역이 존재합니다! 수정 할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                //개인급여제외 체크
                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    //기준일이후 급여지급시 수정 불가
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4CF8O810", ds.Tables[5].Rows[i]["PCSABUN"].ToString(),
                                                                ds.Tables[5].Rows[i]["PCPAYCODE"].ToString(), 
                                                                ds.Tables[5].Rows[i]["PCSDATE"].ToString()
                                                                );
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("급여지급내역이 존재합니다! 수정 할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }

                //개인예외사항체크
                //for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                //{
                //    //기준일이후 급여지급시 수정 불가
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_5BO8W207", ds.Tables[7].Rows[i]["PXSABUN"].ToString(),
                //                                                ds.Tables[7].Rows[i]["PXSDATE"].ToString()
                //                                                );
                //    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                //    if (iCnt > 0)
                //    {
                //        this.ShowCustomMessage("급여지급내역이 존재합니다! 수정 할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                //        e.Successed = false;
                //        return;
                //    }
                //}

                //보수월액 체크
                //계좌번호 체크
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : 스프레드 더블클릭 이벤트 
        private void FPS91_TY_S_HR_4CBHN733_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Run(this.FPS91_TY_S_HR_4CBHN733.GetValue("KBSABUN").ToString());
        }
        #endregion

        #region  Description : 개인정보 조회 함수
        private void UP_GetInsaStandDataBinding(string sKBSABUN)
        {            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", sKBSABUN, TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBH02_KBBSTEAM.DummyValue = dt.Rows[0]["KBBDATE"].ToString();
            CBH02_KBBUSEO.DummyValue = dt.Rows[0]["KBBDATE"].ToString();

            this.CurrentDataTableRowMapping(dt, "02");
               
        }
        #endregion

        #region  Description : 개인급여 정보 조회 함수
        private void UP_GetPayDataBinding(string sKBSABUN)
        {
            DataTable dt = new DataTable();

            //개인급여기준관리
            dt.Clear();
            this.FPS91_TY_S_HR_4CBIL747.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBHT734", sKBSABUN, this.CBO02_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_4CBIL747.SetValue(this.DbConnector.ExecuteDataTable());
            dt = this.DbConnector.ExecuteDataTable();

            //for (int i = 0; i < this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.RowCount; i++)
            //{
            //    if (Convert.ToInt32(dt.Rows[i]["PAYCOUNT"].ToString()) > 0)
            //    {
            //        this.FPS91_TY_S_HR_4CBIL747_Sheet1.Cells[i, 3].Locked = true;
            //        this.FPS91_TY_S_HR_4CBIL747_Sheet1.Cells[i, 4].Locked = true;
            //    }
            //}
            //dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    this.FPS91_TY_S_HR_4CBIL747.SetValue(dt);
            //    //for (int i = 0; i < this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.RowCount; i++)
            //    //{
            //    //    if (Convert.ToInt32(dt.Rows[i]["PAYCOUNT"].ToString()) > 0)
            //    //    {
            //    //        this.FPS91_TY_S_HR_4CBIL747_Sheet1.Cells[i, 3].Locked = true;
            //    //    }
            //    //}
            //}

            //개인급여추가항목
            dt.Clear();
            this.FPS91_TY_S_HR_4CBIN748.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBHW738", "A", sKBSABUN, this.CBO02_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_4CBIN748.SetValue(this.DbConnector.ExecuteDataTable());
            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //this.FPS91_TY_S_HR_4CBIN748.SetValue(dt);
                for (int i = 0; i < this.FPS91_TY_S_HR_4CBIN748.ActiveSheet.RowCount; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["PAYCOUNT"].ToString()) > 0)
                    {
                        this.FPS91_TY_S_HR_4CBIN748_Sheet1.Cells[i, 6].Locked = true;
                    }
                }
            }

            //개인급여제외
            dt.Clear();
            this.FPS91_TY_S_HR_4CBIP749.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBHW738", "D", sKBSABUN, this.CBO02_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_4CBIP749.SetValue(this.DbConnector.ExecuteDataTable());
            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //this.FPS91_TY_S_HR_4CBIP749.SetValue(dt);
                for (int i = 0; i < this.FPS91_TY_S_HR_4CBIP749.ActiveSheet.RowCount; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["PAYCOUNT"].ToString()) > 0)
                    {
                        this.FPS91_TY_S_HR_4CBIP749_Sheet1.Cells[i, 6].Locked = true;
                    }
                }
            }

            //개인급여예외관리
            this.FPS91_TY_S_HR_4CBJD769.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBI3742", sKBSABUN, this.CBO02_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_4CBJD769.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_HR_4CBJD769.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_4CBJD769.GetValue(i, "PXSABUN").ToString() == "")
                {
                    this.FPS91_TY_S_HR_4CBJD769_Sheet1.Cells[i, 9].CellType = new FarPoint.Win.Spread.CellType.TextCellType();                    
                }
            }

            //보수월액
            this.FPS91_TY_S_HR_4CBIR752.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBI7743", sKBSABUN, this.CBO02_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_4CBIR752.SetValue(this.DbConnector.ExecuteDataTable());

            //급여계좌
            this.FPS91_TY_S_HR_4CBIQ751.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CBI3744", sKBSABUN);
            this.FPS91_TY_S_HR_4CBIQ751.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 스프레드 ROW 입력 이벤트
        private void FPS91_TY_S_HR_4CBIL747_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBIL747.SetValue(e.RowIndex, "PDSABUN", this.CBH02_KBSABUN.GetValue());
        }

        private void FPS91_TY_S_HR_4CBIN748_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBIN748.SetValue(e.RowIndex, "PCJOBGN", "A");
            this.FPS91_TY_S_HR_4CBIN748.SetValue(e.RowIndex, "PCSABUN", this.CBH02_KBSABUN.GetValue());
        }

        private void FPS91_TY_S_HR_4CBIP749_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBIP749.SetValue(e.RowIndex, "PCJOBGN", "D");
            this.FPS91_TY_S_HR_4CBIP749.SetValue(e.RowIndex, "PCSABUN", this.CBH02_KBSABUN.GetValue());
        }

        private void FPS91_TY_S_HR_4CBJD769_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBJD769.SetValue(e.RowIndex, "PXSABUN", this.CBH02_KBSABUN.GetValue());

            this.FPS91_TY_S_HR_4CBJD769_Sheet1.Cells[e.RowIndex, 9].Locked = true;
        }

        private void FPS91_TY_S_HR_4CBIR752_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBIR752.SetValue(e.RowIndex, "TASABUN", this.CBH02_KBSABUN.GetValue());
        }

        private void FPS91_TY_S_HR_4CBIQ751_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CBIQ751.SetValue(e.RowIndex, "TCSABUN", this.CBH02_KBSABUN.GetValue());
        }
        #endregion

        #region  Description : 스프레드 버튼 클릭 이벤트
        private void FPS91_TY_S_HR_4CBJD769_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if ((new TYHRPY05C1(this.FPS91_TY_S_HR_4CBJD769.GetValue("PXSABUN").ToString(),
                                this.FPS91_TY_S_HR_4CBJD769.GetValue("PXGUBN").ToString(),
                                this.FPS91_TY_S_HR_4CBJD769.GetValue("PXEXCODE").ToString(),
                                this.FPS91_TY_S_HR_4CBJD769.GetValue("PXSDATE").ToString(),
                                this.FPS91_TY_S_HR_4CBJD769.GetValue("PXEDATE").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
        
        #region  Description : 종료일자 처리 팝업 이벤트
        private void UpdateDATE_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;

            DataTable dt = (this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.Select, "PDSABUN", "PDPAYCODE", "PDSTAMOUNT","PDSTRATE", "PDSDATE", "PDEDATE", "PDBIGO"));

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("선택한 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;

                if (this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text != "False")
                {
                    if (this.FPS91_TY_S_HR_4CBIL747_Sheet1.Cells[iRowIndex - 1, 6].Text.Replace("19000101","") != "")
                    {
                        this.ShowCustomMessage("종료일자 있는경우 선택할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            
            TYHRPY05C2 popup = new TYHRPY05C2();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string sEndDate = popup.fsEndDate;

                if (sEndDate != "" )
                {
                    iRowIndex = 0;

                    for (int i = 0; i < this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.Rows.Count; i++)
                    {
                        iRowIndex = iRowIndex + 1;

                        if (this.FPS91_TY_S_HR_4CBIL747.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text != "False")
                        {
                            this.FPS91_TY_S_HR_4CBIL747_Sheet1.Cells[iRowIndex - 1, 6].Value = sEndDate;
                        }
                    }
                }
            }

        }
        #endregion

        #region  Description : 급여복사 처리 팝업 이벤트
        private void PAYCOPY_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int16 iCnt = 0;

            DataTable dt = (this.FPS91_TY_S_HR_4CBIL747.GetDataSourceInclude(TSpread.TActionType.Select, "PDSABUN", "PDPAYCODE", "PDSTAMOUNT","PDSTRATE", "PDSDATE", "PDEDATE", "PDBIGO"));

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PDEDATE"].ToString() == "" || dt.Rows[i]["PDEDATE"].ToString().Replace("19000101","") == "" )
                    {
                        this.ShowCustomMessage("종료되지 않은 급여는 복사할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    //종료되지않은 급여코드가 있는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_54AHZ169", dt.Rows[i]["PDSABUN"].ToString(), dt.Rows[i]["PDPAYCODE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("종료되지 않은 동일한 급여코드가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                if (!this.ShowMessage("TY_M_AC_27J81133"))
                {
                    return;
                }
            }
            else
            {
                this.ShowCustomMessage("선택한 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

            if (this.OpenModalPopup(new TYHRPY05C3(dt)) == System.Windows.Forms.DialogResult.OK)
            {
                UP_Run(this.CBH02_KBSABUN.GetValue().ToString());
            }
            
        }
        #endregion

        #region  Description : 구분 콤보 이벤트
        private void CBO02_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
