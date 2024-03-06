using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 보증사항 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.12 15:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BCHV392 : 보증사항 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLSABUN : 사번
    ///  BJIDATE1 : 보증시작일
    ///  BJIDATE2 : 보증시작일
    ///  BJJDATE1 : 보증종료일
    ///  BJJDATE2 : 보증종료일
    ///  BJAMT : 보증금액
    ///  BJBONJUK1 : 본적１
    ///  BJBONJUK2 : 본적２
    ///  BJBUNNO : 계좌번호
    ///  BJGANG1 : 보증관계１
    ///  BJGANG2 : 보증관계２
    ///  BJGIGANG : 보증기관
    ///  BJJUSO1 : 주소１
    ///  BJJUSO2 : 주소２
    ///  BJNAME1 : 보증인１
    ///  BJNAME2 : 보증인２
    /// </summary>
    public partial class TYHRKB02C9 : TYBase
    {
        string fsBJSABUN = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C9(string sBJSABUN)
        {
            fsBJSABUN = sBJSABUN;

            InitializeComponent();
        }

        private void TYHRKB02C9_Load(object sender, System.EventArgs e)
        {
        }
        #endregion

        #region Description : 저장버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //if (fsGUBN == "ADD") //신규
            //{
            //    string KLSEQ = UP_GetSEQ(CBH01_HLSABUN.GetValue().ToString());

            //    this.DbConnector.CommandClear();

            //    this.DbConnector.Attach("TY_P_HR_4BD9O400", "2",
            //                                                CBH01_HLSABUN.GetValue().ToString(),
            //                                                KLSEQ,
            //                                                CBH01_GJCODE.GetValue().ToString(),
            //                                                TXT01_GJNAME.GetValue().ToString(),
            //                                                TXT01_GJJUMIN.GetValue().ToString(),
            //                                                CBO01_GJSEXGB.GetValue().ToString(),
            //                                                TYUserInfo.EmpNo
            //                                                ); //저장

            //    this.DbConnector.ExecuteTranQuery();

            //    this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
            //}
            //else //수정
            //{
            //    this.DbConnector.CommandClear();

            //    this.DbConnector.Attach("TY_P_HR_4BD9P401", CBH01_GJCODE.GetValue().ToString(),
            //                                                TXT01_GJNAME.GetValue().ToString(),
            //                                                TXT01_GJJUMIN.GetValue().ToString(),
            //                                                CBO01_GJSEXGB.GetValue().ToString(),
            //                                                TYUserInfo.EmpNo,
            //                                                "2",
            //                                                CBH01_HLSABUN.GetValue().ToString(),
            //                                                TXT01_SEQ.GetValue().ToString()
            //                                                ); //저장

            //    this.DbConnector.ExecuteTranQuery();

            //    this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            //}
            //UP_FieldClear();
        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
