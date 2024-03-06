using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 수선이력 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.22 14:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88MEF613 : 세대별 수선이력 등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_88MEH617 : 세대별 수선이력 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  APHOSU : 호수
    /// </summary>
    public partial class TYATKB007S : TYBase
    {
        #region Description : 폼 로드
        public TYATKB007S()
        {
            InitializeComponent();
        }

        private void TYATKB007S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_88MEH617.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_88MEG616", this.DTP01_STDATE.GetString(),
                                                       this.DTP01_EDDATE.GetString(),
                                                       this.TXT01_APHOSU.GetValue().ToString()
                                                       );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_88MEH617.SetValue(UP_ChangeDataTable(dt));

            // 특정 ROW 색깔 입히기
            for (int i = 0; i < this.FPS91_TY_S_HR_88MEH617.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_88MEH617.GetValue(i, "APLHOSU").ToString() == "소  계")
                {   
                    //this.FPS91_TY_S_HR_88MEH617.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_HR_88MEH617.ActiveSheet.Rows[i].BackColor = Color.FromArgb(243, 240, 229);
                }
                else if (this.FPS91_TY_S_HR_88MEH617.GetValue(i, "APLHOSU").ToString() == "총  계")
                {
                    //this.FPS91_TY_S_HR_88MEH617.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_HR_88MEH617.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB007I(string.Empty, string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_HR_88MEH617_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB007I(this.FPS91_TY_S_HR_88MEH617.GetValue("APLHOSU").ToString(),
                                                   this.FPS91_TY_S_HR_88MEH617.GetValue("APLDATE").ToString().Replace("-", ""),
                                                   this.FPS91_TY_S_HR_88MEH617.GetValue("APLSEQ").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 데이타 테이블 변경(합계 추가)
        private DataTable UP_ChangeDataTable(DataTable dt)
        {
            double dSUBTOTALAMT = 0;
            double dTOTALAMT = 0;

            DataTable dtRtn = new DataTable();

            dtRtn.Columns.Add("APLHOSU", typeof(System.String));
            dtRtn.Columns.Add("APLDATE", typeof(System.String));
            dtRtn.Columns.Add("APLSEQ", typeof(System.String));
            dtRtn.Columns.Add("APLCONTENTS", typeof(System.String));
            dtRtn.Columns.Add("APLREPAMOUNT", typeof(System.String));
            dtRtn.Columns.Add("APLVEND", typeof(System.String));
            dtRtn.Columns.Add("VNSANGHO", typeof(System.String));
            dtRtn.Columns.Add("APLBIGO", typeof(System.String));

            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtRtn.NewRow();

                row["APLHOSU"] = dt.Rows[i]["APLHOSU"].ToString();
                row["APLDATE"] = dt.Rows[i]["APLDATE"].ToString();
                row["APLSEQ"] = dt.Rows[i]["APLSEQ"].ToString();
                row["APLCONTENTS"] = dt.Rows[i]["APLCONTENTS"].ToString();
                row["APLREPAMOUNT"] = dt.Rows[i]["APLREPAMOUNT"].ToString();
                row["APLVEND"] = dt.Rows[i]["APLVEND"].ToString();
                row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                row["APLBIGO"] = dt.Rows[i]["APLBIGO"].ToString();

                dtRtn.Rows.Add(row);

                dSUBTOTALAMT += Convert.ToDouble(dt.Rows[i]["APLREPAMOUNT"].ToString());
                dTOTALAMT += Convert.ToDouble(dt.Rows[i]["APLREPAMOUNT"].ToString());

                if (i + 1 < dt.Rows.Count)
                {   
                    if (dt.Rows[i]["APLHOSU"].ToString() != dt.Rows[i + 1]["APLHOSU"].ToString())
                    {
                        row = dtRtn.NewRow();

                        row["APLHOSU"] = "소  계";
                        row["APLDATE"] = "";
                        row["APLSEQ"] = "";
                        row["APLCONTENTS"] = "";
                        row["APLREPAMOUNT"] = dSUBTOTALAMT;
                        row["APLVEND"] = "";
                        row["VNSANGHO"] = "";
                        row["APLBIGO"] = "";

                        dtRtn.Rows.Add(row);

                        dSUBTOTALAMT = 0;
                    }
                }
                else
                {  
                    row = dtRtn.NewRow();

                    row["APLHOSU"] = "소  계";
                    row["APLDATE"] = "";
                    row["APLSEQ"] = "";
                    row["APLCONTENTS"] = "";
                    row["APLREPAMOUNT"] = dSUBTOTALAMT;
                    row["APLVEND"] = "";
                    row["VNSANGHO"] = "";
                    row["APLBIGO"] = "";

                    dtRtn.Rows.Add(row);

                    dSUBTOTALAMT = 0;

                    row = dtRtn.NewRow();

                    row["APLHOSU"] = "총  계";
                    row["APLDATE"] = "";
                    row["APLSEQ"] = "";
                    row["APLCONTENTS"] = "";
                    row["APLREPAMOUNT"] = dTOTALAMT;
                    row["APLVEND"] = "";
                    row["VNSANGHO"] = "";
                    row["APLBIGO"] = "";

                    dtRtn.Rows.Add(row);

                    dSUBTOTALAMT = 0;
                }
            }

            return dtRtn;
        }
        #endregion
    }
}
