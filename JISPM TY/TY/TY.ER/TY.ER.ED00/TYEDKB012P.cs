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
    /// 반출기간연장신청서관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.06.05 13:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_865E9175 : 반출기간연장신청서 등록
    ///  TY_P_UT_865G2177 : 반출기간연장신청서 수정
    ///  TY_P_UT_865G3178 : 반출기간연장신청서 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  EDIAPPGUBN : EDIAPPGUBN
    ///  EDIEXTGUBN : EDIEXTGUBN
    ///  EDIGJ : 공장
    ///  EDIJSGB : 전송구분
    ///  EDIDATE : 반입일자
    ///  EDIEXTEDATE : EDIEXTEDATE
    ///  EDIEXTSDATE : EDIEXTSDATE
    ///  EDIAPPNAME : EDIAPPNAME
    ///  EDIBLHSN : HSN
    ///  EDIBLMSN : MSN
    ///  EDICSSINNO : EDICSSINNO
    ///  EDICUSTKWA : EDICUSTKWA
    ///  EDICUSTLOC : EDICUSTLOC
    ///  EDIJUKHA : 적하목록
    ///  EDIMSG : MSG
    ///  EDINO1 : NO1
    ///  EDINO2 : NO2
    ///  EDINO3 : NO3
    ///  EDIRCVGB : 접수구분
    ///  EDIREASON : EDIREASON
    ///  EDIWHNUMBER : EDIWHNUMBER
    /// </summary>
    public partial class TYEDKB012P : TYBase
    {
        private string fsEDAGJ;
        private string fsEDANO1;
        private string fsEDANO2;
        private string fsEDANO3;

        #region  Description : 폼 로드 이벤트
        public TYEDKB012P(string sEDAGJ, string sEDANO1, string sEDANO2, string sEDANO3)
        {
            InitializeComponent();

            this.SetPopupStyle();

             fsEDAGJ   = sEDAGJ;             
             fsEDANO1 = sEDANO1;
             fsEDANO2 = sEDANO2;
             fsEDANO3 =  sEDANO3;
        }       

        private void TYEDKB012P_Load(object sender, System.EventArgs e)
        {
           
            UP_SetLockCheck();

            CBO01_EDAGJ.SetValue(fsEDAGJ);
            TXT01_EDANO1.SetValue(fsEDANO1);
            TXT01_EDANO2.SetValue(fsEDANO2);
            TXT01_EDANO3.SetValue(fsEDANO3);
          
            UP_DataBinding();           

        }
        #endregion

        #region  Description : UP_DataBinding() 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_868HR198", this.CBO01_EDAGJ.GetValue().ToString(), TXT01_EDANO1.GetValue().ToString(), TXT01_EDANO2.GetValue().ToString(), TXT01_EDANO3.GetValue().ToString() );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                CBO01_EDAGJ.SetValue(dt.Rows[0]["EDAGJ"].ToString());
                DTP01_EDADATE.SetValue(dt.Rows[0]["EDADATE"].ToString());
                TXT01_EDANO1.SetValue(dt.Rows[0]["EDANO1"].ToString());
                TXT01_EDANO2.SetValue(dt.Rows[0]["EDANO2"].ToString());
                TXT01_EDANO3.SetValue(dt.Rows[0]["EDANO3"].ToString());

                TXT01_EDANOTETIME.SetValue(dt.Rows[0]["EDANOTETIME"].ToString());
                TXT01_EDABATGUBN.SetValue(dt.Rows[0]["EDABATGUBN"].ToString());
                DTP01_EDAEXTSDATE.SetValue(dt.Rows[0]["EDAEXTSDATE"].ToString());
                DTP01_EDAEXTEDATE.SetValue(dt.Rows[0]["EDAEXTEDATE"].ToString());
                TXT01_EDAREASON.SetValue(dt.Rows[0]["EDAREASON"].ToString());
                TXT01_EDAAPVALNO.SetValue(dt.Rows[0]["EDAAPVALNO"].ToString());
            }

        }
        #endregion    


        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDAGJ.SetValue("S");
            }
            else
            {
                CBO01_EDAGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDAGJ.SetReadOnly(true);
            }
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
