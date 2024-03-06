using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매요청테스트 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.10.26 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2AQB5842 : 구매요청 마스터
    ///  TY_P_MR_2BC54263 : 구매요청 삭제 - 입고번호체크(계약번호 존재시)
    ///  TY_P_MR_2BC50267 : 구매요청 삭제 - 발주번호체크(계약번호 없을경우)
    ///  TY_P_MR_2BC53268 : 구매요청 삭제 - 결재 완료 체크
    ///  TY_P_MR_2BC5M269 : 구매요청 삭제 - 예산 업데이트-가용금액(마이너스) 프로시저
    ///  TY_P_MR_2BC66270 : 구매요청 삭제 - 마스터, 내역, 특기(삭제)
    /// 
    ///  TY_P_MR_2BCAF252 : 구매요청 예산 조회(마스터)
    ///  TY_P_MR_2BC1I256 : 구매요청 마스터(결재 내용 가져오기)
    ///  TY_P_MR_2AQBC843 : 구매요청 내역사항 조회
    ///  TY_P_MR_2BC38257 : 구매요청 특기사항 조회
    ///  TY_P_MR_2BC3I259 : 구매요청 특기사항 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2AQBH845 : 구매요청 마스터
    ///  
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    ///  TY_S_MR_2BCBY255 : 구매요청 예산 조회(마스터)
    ///  TY_S_MR_2AQBK846 : 구매요청 내역사항 조회
    ///  TY_S_MR_2BC30258 : 구매요청 특기사항 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_MR_2BC57264 : 입고 자료에 요청번호가 존재합니다.
    ///  TY_M_MR_2BC58265 : 발주 자료에 요청번호가 존재합니다!
    ///  TY_M_MR_2BC59266 : 결재 완료 된 자료이므로 작업이 불가합니다!
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    ///  TY_M_MR_2BC3J261 : 특기사항 내용을 입력하세요.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    ///  
    ///  # 필드사전 정보 ####
    ///  INQ      : 조회
    ///  NEW      : 신규
    ///  REM      : 삭제
    ///  PRM1000  : 사업부
    ///  PRM1010  : 요청구분
    ///  PRM1020  : 년월
    ///  PRM2120  : 구매명
    ///  
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    ///  PRM4000  : 발주(입고) 완료 구분
    ///  PRM4010  : 발주(입고)번호
    ///  PRM4050  : 그룹웨어문서번호
    ///  PRM4020  : 결재구분
    ///  PRM4030  : 결재사번
    ///  KBHANGL1 : 결재자 이름
    ///  PRM4040  : 결재일자
    
    /// </summary>
    public partial class TYMRPR001S : TYBase
    {
        private string fsPRM1000 = string.Empty;
        private string fsPRM1010 = string.Empty;
        private string fsPRM1020 = string.Empty;
        private string fsPRM1030 = string.Empty;

        #region Description : 페이지 로드
        public TYMRPR001S()
        {
            InitializeComponent();
        }

        private void TYMRPR001S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_MR_2AQBH845.Initialize();

            UP_SET_BUSEO();
            
            this.TXT01_PRM1010.SetValue("P");
            this.TXT01_PRM1010.SetReadOnly(true);

            SetStartingFocus(TXT01_PRM1000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2AQBH845.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBO01_PRGUBN.GetValue().ToString() == "1")         //전체
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_2AQB5842",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM2120.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );
            }
            else if (this.CBO01_PRGUBN.GetValue().ToString() == "2")    //결재중
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_74EGF284",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM2120.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );
            }
            else if (this.CBO01_PRGUBN.GetValue().ToString() == "3")    //결재완료
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_74EGL286",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM2120.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );
            }
            else if (this.CBO01_PRGUBN.GetValue().ToString() == "4")    //미발주
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_74EHB287",
                this.TXT01_PRM1000.GetValue(),
                this.TXT01_PRM1010.GetValue(),
                this.TXT01_PRM1020.GetValue(),
                this.TXT01_PRM2120.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );
            }

            this.FPS91_TY_S_MR_2AQBH845.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_MR_2AQBH845.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_2AQBH845.GetValue(i, "PRM4050").ToString() == "")
                {
                    this.FPS91_TY_S_MR_2AQBH845_Sheet1.Cells[i, 17].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMRPR001I("", "P", "", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2AQBH845_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRPR001I(this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM1000").ToString(), this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM1010").ToString(),
                                this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM1020").ToString(), this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM1030").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 구매요청 마스터 그리드 이벤트(그룹웨어 문서 바로가기)
        private void FPS91_TY_S_MR_2AQBH845_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "17") // (그룹웨어 요청문서 바로가기)
            {
                if (this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM4050").ToString() != "")
                {
                    if ((new TYMRPR005S(this.FPS91_TY_S_MR_2AQBH845.GetValue("PRM4050").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BC51262");
                    return;
                }
            }
            else if (e.Column.ToString() == "18") // (구매요청자료 복사)
            {
                if ((new TYMRPR008B(this.FPS91_TY_S_MR_2AQBH845.GetValue("PRNUM").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
        }
        #endregion

        #region Description : 부서코드 가져오기
        private void UP_SET_BUSEO()
        {
            // 부서코드
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 부서코드
                this.CBH01_GCDDP.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
            }

            // 사업부
            this.TXT01_PRM1000.SetValue(this.CBH01_GCDDP.GetValue().ToString().Substring(0, 1));

            // 요청년월
            this.TXT01_PRM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));
        }
        #endregion

        #region Description : 요청년월 이벤트
        private void TXT01_PRM1020_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_PRM1020.GetValue().ToString().Length == 6)
            {
                // 부서코드
                this.CBH01_GCDDP.DummyValue = this.TXT01_PRM1020.GetValue().ToString() + "01";
            }
        }
        #endregion
    }
}