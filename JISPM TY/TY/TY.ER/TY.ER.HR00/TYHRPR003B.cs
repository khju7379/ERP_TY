using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인사 발령사항 생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.02.09 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_529G5302 : 인사 발령사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_529G7303 : 인사 발령사항 생성
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRPR003B : TYBase
    {
        #region Description : 폼 로드
        public TYHRPR003B()
        {
            InitializeComponent();
        }

        private void TYHRPR003B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-6).ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            SetStartingFocus(DTP01_STDATE);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_529G5302",
                DTP01_STDATE.GetString().Substring(0, 6),
                DTP01_EDDATE.GetString().Substring(0, 6)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_529G7303.SetValue(dt);

            if (dt.Rows.Count == 0)
            {
                ShowMessage("TY_M_GB_2BF7Y364");
            }
            
            for (int i = 0; i < this.FPS91_TY_S_HR_529G7303.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_529G7303.GetValue(i, "SNBALYY").ToString() == "" && this.FPS91_TY_S_HR_529G7303.GetValue(i, "SNBALSEQ").ToString() == "")
                {
                    this.FPS91_TY_S_HR_529G7303_Sheet1.Cells[i, 5].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
                else
                {
                    this.FPS91_TY_S_HR_529G7303_Sheet1.Cells[i, 4].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 그리드 버튼 이벤트
        private void FPS91_TY_S_HR_529G7303_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            bool bResult = false;
            if (e.Column.ToString() == "4") // 생성
            {
                bResult = UP_Check(this.FPS91_TY_S_HR_529G7303.GetValue("SNYYMM").ToString(),
                         "",
                         0
                         );

                if (bResult == true)
                {
                    UP_CREATE(this.FPS91_TY_S_HR_529G7303.GetValue("SNYYMM").ToString());
                }
            }
            else if (e.Column.ToString() == "5") // 취소
            {
                bResult = UP_Check(this.FPS91_TY_S_HR_529G7303.GetValue("SNYYMM").ToString(),
                                   this.FPS91_TY_S_HR_529G7303.GetValue("SNBALYY").ToString()+this.FPS91_TY_S_HR_529G7303.GetValue("SNBALSEQ").ToString(),
                         1
                         );

                if (bResult == true)
                {
                    UP_CANCLE(this.FPS91_TY_S_HR_529G7303.GetValue("SNYYMM").ToString(),
                              this.FPS91_TY_S_HR_529G7303.GetValue("SNBALYY").ToString(),
                              this.FPS91_TY_S_HR_529G7303.GetValue("SNBALSEQ").ToString());
                }
            }
            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 인사발령 생성
        private void UP_CREATE(string SNYYMM)
        {
            try
            {
                string sSEQ = string.Empty;

                // 발령번호 가져오기 (INSUNGMF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_52AE7315", SNYYMM.Substring(0,4));
                sSEQ = this.DbConnector.ExecuteScalar().ToString();

                // 승호파일 조회 (INSUNGMF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_523C3260", SNYYMM, "", "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 인사기본 수정  (INKIBNMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AEZ316",
                                            dt.Rows[i]["SNCHHOBN"].ToString(),
                                            "300",
                                            SNYYMM.Substring(0, 4) + "0301",
                                            TYUserInfo.EmpNo,
                                            dt.Rows[i]["SNSABUN"].ToString()
                                            );
                    this.DbConnector.ExecuteNonQuery();

                    // 인사 발령사항 등록  (INBALRMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4BJ8K449",
                                            SNYYMM.Substring(0, 4),
                                            sSEQ,
                                            dt.Rows[i]["SNSABUN"].ToString(),
                                            SNYYMM.Substring(0, 4) + "0301",
                                            "300",
                                            "",
                                            "",
                                            dt.Rows[i]["SNSOSOK"].ToString(),
                                            dt.Rows[i]["SNDEPT"].ToString(),
                                            dt.Rows[i]["KBBSTEAM"].ToString(),
                                            dt.Rows[i]["KBJCCD"].ToString(),
                                            dt.Rows[i]["SNJKCD"].ToString(),
                                            dt.Rows[i]["KBJJCD"].ToString(),
                                            dt.Rows[i]["SNCHHOBN"].ToString(),
                                            dt.Rows[i]["SNCHHOBN"].ToString(),
                                            TYUserInfo.EmpNo
                                            );

                    this.DbConnector.ExecuteNonQuery();


                    // 승호 파일 발령사항 수정  (INSUNGMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AGJ318",
                                            SNYYMM.Substring(0, 4),
                                            sSEQ,
                                            TYUserInfo.EmpNo,
                                            dt.Rows[i]["SNYYMM"].ToString(),
                                            dt.Rows[i]["SNSABUN"].ToString()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                this.ShowMessage("TY_M_GB_26E30875");
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 인사발령 취소
        private void UP_CANCLE(string SNYYMM, string SNBALYY, string SNBALSEQ)
        {
            try
            {
                // 승호파일 조회 (INSUNGMF)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_523C3260", SNYYMM, "", "");
                DataTable dt = this.DbConnector.ExecuteDataTable();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string SNHOBN = dt.Rows[i]["SNHOBN"].ToString();

                    // 인사 발령사항 삭제  (INBALRMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AGY320", SNBALYY, SNBALSEQ, dt.Rows[i]["SNSABUN"].ToString());
                    this.DbConnector.ExecuteTranQuery();

                    // 마지막 발령사항 조회  (INBALRMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AGW319", dt.Rows[i]["SNSABUN"].ToString());
                    DataTable dt2 = this.DbConnector.ExecuteDataTable();

                    // 인사기본 수정  (INKIBNMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AEZ316",
                                            SNHOBN,
                                            dt2.Rows[0]["BLCODE"],
                                            dt2.Rows[0]["BLDATE"],
                                            TYUserInfo.EmpNo,
                                            dt.Rows[i]["SNSABUN"].ToString()
                                            );
                    this.DbConnector.ExecuteNonQuery();

                    // 승호 파일 발령사항 삭제  (INSUNGMF)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_52AGJ318",
                                            "",
                                            0,
                                            TYUserInfo.EmpNo,
                                            dt.Rows[i]["SNYYMM"].ToString(),
                                            dt.Rows[i]["SNSABUN"].ToString()
                                            );
                    this.DbConnector.ExecuteNonQuery();
                }
                this.ShowMessage("TY_M_AC_2CDB1167");
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 생성 / 취소 유효성 체크
        private bool UP_Check(string SNYYMM, string SNSEQ, int GUBN)
        {
            bool bRtn = false;

            this.DbConnector.CommandClear();

            if (GUBN == 0)  //생성
            {
                this.DbConnector.Attach("TY_P_HR_52ADS313",SNYYMM);
            }
            else if (GUBN == 1) //취소
            {
                this.DbConnector.Attach("TY_P_HR_52AE4314", SNSEQ.Substring(0, 4), SNSEQ.Substring(4, 3));
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                bRtn = true;
            }
            else
            {
                this.ShowCustomMessage("기준년월 이후 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            //급여인상액 처리가 완료 되었는지 체크
            if (GUBN == 1)
            {
                Int16 iCnt = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_523C3260", SNYYMM, "", "");
                DataTable dk = this.DbConnector.ExecuteDataTable();
                if (dk.Rows.Count > 0)
                {
                    for (int i = 0; i < dk.Rows.Count; i++)
                    {
                        iCnt = Convert.ToInt16(dk.Rows[i]["SNPYCYY"].ToString() != "" ? iCnt + 1 : iCnt);
                    }

                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("호봉인상처리가 완료되었습니다! 취소할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return false;
                    }
                }
            }


            if (GUBN == 0)
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    bRtn = false;
                }
                else
                {
                    bRtn = true;
                }
            }
            else if (GUBN == 1)
            {
                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    bRtn = false;
                }
                else
                {
                    bRtn = true;
                }
            }
            

            return bRtn;
        }
        #endregion
    }
}
