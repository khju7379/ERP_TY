using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 제출자정보 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.20 15:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BK33387 : 제출자정보 등록
    ///  TY_P_AC_3BK5L392 : 제출자정보 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  EDIT : 수정
    ///  SAV : 저장
    ///  VNGUBUN : 구분
    ///  EJNAME : 성명
    ///  ESCCORPNO : 법인등록번호
    ///  ESCSAUPNO : 사업자번호
    ///  ESCUPJONG : 업종
    ///  ESLSBKNM : 금융기관명
    ///  TSNMCP : 상호
    ///  VNCODE : 거래처코드
    ///  VNJUSO : 주소
    ///  VNTEL : 전화번호
    ///  VNUPJONG : 업종
    ///  VNUPTE : 업태
    /// </summary>
    public partial class TYACTX004I : TYBase
    {
        #region Description : 페이지 로드

        private TYData DAT01_MAHISAB;
        public string fsVNCODE = string.Empty;
        
        public TYACTX004I(string sVNCODE)
        {
            InitializeComponent();

            //파라미터 값 가져오기
            this.fsVNCODE = sVNCODE;
            this.DAT01_MAHISAB = new TYData("DAT01_MAHISAB", TYUserInfo.EmpNo);
        }

        private void TYACTX004I_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT01_MAHISAB);

            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                UP_GET_VNCODE("1");

                this.SetStartingFocus(this.CBO01_VNGUBUN);
            }
            else
            {   
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BK25384",
                    "",
                    fsVNCODE
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.CBO01_VNGUBUN.SetValue(dt.Rows[0]["ASMBRANCH"].ToString());
                this.CBH01_BADVEND.SetValue(dt.Rows[0]["ASMVNEDCD"].ToString());
                this.TXT01_ESCSAUPNO.SetValue(dt.Rows[0]["ASMSAUPNO"].ToString());
                this.TXT01_TSNMCP.SetValue(dt.Rows[0]["ASMSANGHO"].ToString());
                this.TXT01_EJNAME.SetValue(dt.Rows[0]["ASMNAMENM"].ToString());
                this.TXT01_ESCCORPNO.SetValue(dt.Rows[0]["ASMCORPNO"].ToString());
                this.TXT01_VNUPTE.SetValue(dt.Rows[0]["ASMUPTAE"].ToString());
                this.TXT01_ESCUPJONG.SetValue(dt.Rows[0]["ASMEVENT"].ToString());
                this.TXT01_VNUPJONG.SetValue(dt.Rows[0]["ASMBUSTYPE"].ToString());
                this.TXT01_VNTEL.SetValue(dt.Rows[0]["ASMTELNUM"].ToString());
                this.TXT01_VNJUSO.SetValue(dt.Rows[0]["ASMVNADDRS"].ToString());
                this.CBH01_ESLSBKNM.SetValue(dt.Rows[0]["ASMTAXAREA"].ToString());

                this.CBO01_VNGUBUN.SetReadOnly(true);
                this.CBH01_BADVEND.SetReadOnly(true);
                this.SetStartingFocus(this.TXT01_TSNMCP);
            }
            this.TXT01_ESCSAUPNO.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    if (string.IsNullOrEmpty(this.fsVNCODE))
                    {
                        if (UP_ASMVNEDCD_Check(this.CBH01_BADVEND.GetValue().ToString()))
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_3BK33387",
                                this.CBO01_VNGUBUN.GetValue(),
                                this.CBH01_BADVEND.GetValue(),
                                this.TXT01_ESCSAUPNO.GetValue(),
                                this.TXT01_TSNMCP.GetValue(),
                                this.TXT01_EJNAME.GetValue(),
                                this.TXT01_ESCCORPNO.GetValue(),
                                this.TXT01_VNUPTE.GetValue(),
                                this.TXT01_ESCUPJONG.GetValue(),
                                this.TXT01_VNUPJONG.GetValue(),
                                this.TXT01_VNTEL.GetValue(),
                                this.TXT01_VNJUSO.GetValue(),
                                this.CBH01_ESLSBKNM.GetValue()
                                );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                    else
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach
                            (
                            "TY_P_AC_3BK5L392",
                            this.TXT01_ESCSAUPNO.GetValue(),
                            this.TXT01_TSNMCP.GetValue(),
                            this.TXT01_EJNAME.GetValue(),
                            this.TXT01_ESCCORPNO.GetValue(),
                            this.TXT01_VNUPTE.GetValue(),
                            this.TXT01_ESCUPJONG.GetValue(),
                            this.TXT01_VNUPJONG.GetValue(),
                            this.TXT01_VNTEL.GetValue(),
                            this.TXT01_VNJUSO.GetValue(),
                            this.CBH01_ESLSBKNM.GetValue(),
                            this.CBO01_VNGUBUN.GetValue(),
                            this.CBH01_BADVEND.GetValue()
                            );
                        this.DbConnector.ExecuteTranQueryList();
                    }
                    this.ShowMessage("TY_M_GB_23NAD873");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 닫기버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 본점, 지점
        private void CBO01_VNGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_GET_VNCODE(this.CBO01_VNGUBUN.GetValue().ToString());
                
        }
        #endregion

        #region Description : 거래처 가져오기
        private void UP_GET_VNCODE(string sGUBUN)
        {
            string sVNCODE;

            if (sGUBUN.ToString() == "1")
            {
                sVNCODE = "102885";
            }
            else
            {
                sVNCODE = "349876";
            }

            if (UP_ASMVNEDCD_Check(sVNCODE))
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BL5D405",
                    sVNCODE
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_BADVEND.SetValue(sVNCODE);

                    this.TXT01_ESCSAUPNO.SetValue(dt.Rows[0]["VNSAUPNO"]);
                    this.TXT01_TSNMCP.SetValue(dt.Rows[0]["VNSANGHO"]);
                    this.TXT01_EJNAME.SetValue(dt.Rows[0]["VNIRUM"]);
                    this.TXT01_VNUPTE.SetValue(dt.Rows[0]["VNUPTE"]);
                    this.TXT01_ESCUPJONG.SetValue(dt.Rows[0]["VNUPJONG"]);
                    this.TXT01_VNTEL.SetValue(dt.Rows[0]["VNTEL"]);
                    this.TXT01_VNJUSO.SetValue(dt.Rows[0]["VNJUSO"]);

                    this.CBH01_BADVEND.SetReadOnly(true);
                }
            }
            else
            {
                this.CBH01_BADVEND.SetValue("");
                this.TXT01_ESCSAUPNO.SetValue("");
                this.TXT01_TSNMCP.SetValue("");
                this.TXT01_EJNAME.SetValue("");
                this.TXT01_VNUPTE.SetValue("");
                this.TXT01_ESCUPJONG.SetValue("");
                this.TXT01_VNTEL.SetValue("");
                this.TXT01_VNJUSO.SetValue("");
            }
        }
        #endregion

        #region Description : 거래처 유효성 체크
        public bool UP_ASMVNEDCD_Check(string sASMVNEDCD){
            
            this.DbConnector.Attach
                (
                "TY_P_AC_3BM3Z415",
                sASMVNEDCD
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Description : 사업자번호 가져오기
        private void CBH01_BADVEND_CodeBoxDataBinded(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3BL5D405",
                this.CBH01_BADVEND.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_ESCSAUPNO.SetValue(dt.Rows[0]["VNSAUPNO"]);
                this.TXT01_TSNMCP.SetValue(dt.Rows[0]["VNSANGHO"]);
                this.TXT01_EJNAME.SetValue(dt.Rows[0]["VNIRUM"]);
                this.TXT01_VNUPTE.SetValue(dt.Rows[0]["VNUPTE"]);
                this.TXT01_ESCUPJONG.SetValue(dt.Rows[0]["VNUPJONG"]);
                this.TXT01_VNTEL.SetValue(dt.Rows[0]["VNTEL"]);
                this.TXT01_VNJUSO.SetValue(dt.Rows[0]["VNJUSO"]);

                this.CBH01_BADVEND.SetReadOnly(true);
            }
        }
        #endregion
    }
}
