using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금 관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.10 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    ///  # 프로시저 정보 ####
    ///  TY_P_AC_25A1Y223 : 미지급금 체크(현금일 경우)
    ///  TY_P_AC_25AAH217 : 미지급금 확인
    ///  TY_P_AC_25ABI218 : 미지급금 체크(어음번호 변경)
    ///  TY_P_AC_25ABJ219 : 미지급금 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25A41236 : 만기예정일을 입력하세요.
    ///  TY_M_AC_25A2E230 : 지급형태를 선택하세요!
    ///  TY_M_AC_25A1V222 : 어음번호를 입력하세요.
    ///  TY_M_AC_25ABS221 : 입력하신 어음번호가 존재하지 않습니다!
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  ABANCHK : 반제체크
    ///  F3BKPY : 지급은행
    ///  M1SABN : 사　번
    ///  M1VNCD : 거래처
    ///  M1GUBN : 지급형태
    ///  M1SAGB : 지역구분
    ///  M1DATE : 만기예정일
    ///  F3DTED : 만기일자
    ///  M1AMT : 지급금액
    ///  M1CDBK : 금융기관
    ///  M1CDBKNM : 금융기관명
    ///  M1DTAC : 회계일자
    ///  M1DTED : 지급일자
    ///  M1NOAC : 계좌번호
    ///  M1NONY : 어음번호
    ///  M1NOSQ : 순차번호
    ///  M1RKAC : 적　요
    ///  M1WNJP : 설정전표번호
    ///  VNBKYN : 전자계좌계설
    /// </summary>
    public partial class TYACFP002I : TYBase
    {
        private string _M1DTED;
        private string _M1VNCD;
        private string _M1NOSQ;
        private string _M1HISAB;
        private string fsM1CDBK = string.Empty;

        private TYData DAT01_M1HISAB;

        #region Description : 페이지 로드
        public TYACFP002I(string sM1DTED, string sM1VNCD, string sM1NOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._M1DTED = sM1DTED;
            this._M1VNCD = sM1VNCD;
            this._M1NOSQ = sM1NOSQ;

            // 로그인 사번 가져오기
            this.DAT01_M1HISAB = new TYData("DAT01_M1HISAB", TYUserInfo.EmpNo);
        }

        private void TYACFP002I_Load(object sender, System.EventArgs e)
        {
            this.CBO01_M1SAGB.Enabled = true;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_B3BCV884",
                this._M1DTED.ToString().Replace("-",""),
                this._M1VNCD,
                this._M1NOSQ
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }

            // 콤보박스 인덱스체인지 이벤트
            // 데이터 바인딩 후 이벤트 넣음
            this.CBO01_M1GUBN.SelectedIndexChanged += new EventHandler(CBO01_M1GUBN_SelectedIndexChanged);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25ABJ219",
                this.CBO01_M1GUBN.GetValue(),
                this.DTP01_M1DATE.GetValue().ToString().Replace("-", ""),
                fsM1CDBK.ToString(),
                this.TXT01_M1NOAC.GetValue(),
                this.TXT01_M1NONY.GetValue(),
                this.CBH01_M1SABN.GetValue(),
                this.CKB01_M1COGUBN.GetValue().ToString(),
                this._M1HISAB,
                this.TXT01_M1DTED.GetValue().ToString().Replace("-",""),
                this.CBH01_M1VNCD.GetValue(),
                this.TXT01_M1NOSQ.GetValue()
                );

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            fsM1CDBK = "";

            if (this.CBO01_M1GUBN.GetValue().ToString() == "2")
            {
                // 만기예정일
                if (this.DTP01_M1DATE.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_25A41236");
                    e.Successed = false;
                    return;
                }

                // 어음번호
                //if (this.TXT01_M1NONY.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_AC_25A1V222");
                //    e.Successed = false;
                //    return;
                //}
            }

            DataTable dt = new DataTable();

            // 지급형태 = 현금
            if (this.CBO01_M1GUBN.GetValue().ToString() == "1")
            {
                // 2021.03.11 거래처관리 노무비 닷컴과 동일하게 처리(회계팀 요청)
                if (this.CKB01_M1COGUBN.Checked == true)
                {
                    fsM1CDBK = this.CBH01_M1CDBK.GetValue().ToString().Substring(1, 2);

                    //this.CBH01_M1CDBK.SetValue(this.CBH01_M1CDBK.GetValue().ToString().Substring(1,2));
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_25A1Y223", this.CBH01_M1VNCD.GetValue());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        fsM1CDBK = dt.Rows[0]["VNCDBK"].ToString().Substring(1, 2);
                        this.TXT01_M1NOAC.SetValue(dt.Rows[0]["VNNOAC"].ToString());

                        //this.CBH01_M1CDBK.SetValue(dt.Rows[0]["VNCDBK"].ToString().Substring(1, 2));
                        //this.TXT01_M1NOAC.SetValue(dt.Rows[0]["VNNOAC"].ToString());
                    }
                }
            }
            else if (this.CBO01_M1GUBN.GetValue().ToString() == "2") // 지급형태 = 어음
            {
                if (this.TXT01_M1NONY.GetValue().ToString().Trim() == "")
                {
                    if (this.DTP01_M1DATE.GetValue().ToString() == "" || int.Parse(Get_Numeric(this.DTP01_M1DATE.GetValue().ToString())) == 0)
                    {
                        //DateTime.Now.ToString("yyyyMMdd");
                        string sDate = this.DTP01_M1DATE.GetValue().ToString();

                        if (sDate.Length == 8)
                        {
                            DateTime Dt = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));
                            sDate = Dt.AddMonths(3).ToString("yyyyMM") + "15";
                        }

                        this.DTP01_M1DATE.SetValue(sDate);
                    }

                    this.CBO01_M1GUBN.Focus();
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_25ABI218", this.TXT01_M1NONY.GetValue().ToString().Trim());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_F3DTED.SetValue(dt.Rows[0]["F3DTED"].ToString());
                        this.CBH01_F3BKPY.SetValue(dt.Rows[0]["F3BKPY"].ToString());
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_25ABS221");
                        e.Successed = false;
                        return;

                        this.TXT01_M1NONY.Focus();
                    }
                }

                if (this.CBH01_M1CDBK.GetValue().ToString() != "")
                {
                    fsM1CDBK = this.CBH01_M1CDBK.GetValue().ToString().Substring(1, 2);
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_25A2E230");
                e.Successed = false;
                return;

                this.CBO01_M1GUBN.Focus();
            }

            dt.Dispose();
        }
        #endregion

        #region Description : 반제 체크
        private void BTN61_ABANCHK_Click(object sender, EventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACFP002C(this.TXT01_M1DTED.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 지급형태 콤보박스 이벤트
        void CBO01_M1GUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_M1GUBN.GetValue().ToString() == "1")
            {
                this.DTP01_M1DATE.SetValue("");
                this.TXT01_M1NONY.SetValue("");
                this.TXT01_F3DTED.SetValue("");
                this.CBH01_F3BKPY.SetValue("");
            }
            else if (this.CBO01_M1GUBN.GetValue().ToString() == "2")
            {
                fsM1CDBK = "";
                //this.CBH01_M1CDBK.SetValue("");
                //this.TXT01_M1NOAC.SetValue("");
            }
        }
        #endregion

        #region Description : 노무비 닷컴 이벤트
        private void CKB01_M1COGUBN_CheckedChanged(object sender, EventArgs e)
        {
            //// 2021.03.11 거래처관리 노무비 닷컴과 동일하게 처리(회계팀 요청)
            //if (this.CKB01_M1COGUBN.Checked == true)
            //{
            //    this.CBH01_M1CDBK.Enabled = true;
            //    this.TXT01_M1NOAC.Enabled = true;
            //}
            //else
            //{
            //    //this.CBH01_VNCOCDBK.SetValue("");
            //    //this.TXT01_VNCONOAC.SetValue("");

            //    this.CBH01_M1CDBK.Enabled = false;
            //    this.TXT01_M1NOAC.Enabled = false;
            //}
        }
        #endregion
    }
}