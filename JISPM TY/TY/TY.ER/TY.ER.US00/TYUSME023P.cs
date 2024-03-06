using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_92CE5728 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUSME023P : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME023P()
        {
            InitializeComponent();
        }

        private void TYUSME023P_Load(object sender, System.EventArgs e)
        {
            this.SetFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95DDP536",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString()
                );

            dt = UP_Get_Convert(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME023R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable UP_Get_Convert(DataTable Orgdt)
        {
            string sHANGCHA = string.Empty;
            string sTMSOSOK = string.Empty;
            string sTMSAVE = string.Empty;

            int iTMSAVED = 0;
            int iTMSAVEM = 0;
            int iTMSAVEH = 0;
            int iTMLOSTD = 0;
            int iTMLOSTM = 0;
            int iTMLOSTH = 0;

            int iTMSAVEDTot = 0;
            int iTMSAVEMTot = 0;
            int iTMSAVEHTot = 0;
            int iTMLOSTDTot = 0;
            int iTMLOSTMTot = 0;
            int iTMLOSTHTot = 0;

            double dTMTOTSAVE = 0;
            double dTMTOTLOST = 0;
            double dTMTOTSAVETot = 0;
            double dTMTOTLOSTTot = 0;

            int i = 0;

            sHANGCHA = this.CBH01_STHANGCHA.GetValue().ToString() + " ~ " + this.CBH01_EDHANGCHA.GetValue().ToString();

            DataTable dt = new DataTable();
            DataTable retDt = new DataTable();

            DataRow dtRow;

            retDt.Columns.Add("TMHANGCHA", typeof(System.String));
            retDt.Columns.Add("TMSOSOK", typeof(System.String));
            retDt.Columns.Add("TMSAVE_TIME", typeof(System.String));
            retDt.Columns.Add("TMLOST_TIME", typeof(System.String));
            retDt.Columns.Add("TMTOTSAVE", typeof(System.Double));
            retDt.Columns.Add("TMTOTLOST", typeof(System.Double));


            for (i = 0; i < Orgdt.Rows.Count; i++)
            {
                if (i != 0 && (Orgdt.Rows[i - 1]["IHSOSOK"].ToString() != Orgdt.Rows[i]["IHSOSOK"].ToString()))
                {
                    if (iTMSAVEM > 59)
                    {
                        iTMSAVEH = iTMSAVEH + 1;
                        iTMSAVEM = iTMSAVEM - 60;
                    }

                    if (iTMSAVEH > 23)
                    {
                        iTMSAVED = iTMSAVED + 1;
                        iTMSAVEH = iTMSAVEH - 24;
                    }



                    if (iTMLOSTM > 59)
                    {
                        iTMLOSTH = iTMLOSTH + 1;
                        iTMLOSTM = iTMLOSTM - 60;
                    }

                    if (iTMLOSTH > 23)
                    {
                        iTMLOSTD = iTMLOSTD + 1;
                        iTMLOSTH = iTMLOSTH - 24;
                    }

                    dtRow = retDt.NewRow();

                    dtRow["TMHANGCHA"] = sHANGCHA.ToString();
                    dtRow["TMSOSOK"] = sTMSOSOK;
                    dtRow["TMSAVE_TIME"] = Set_Fill2(Convert.ToString(iTMSAVED)) + "D   " + Set_Fill2(Convert.ToString(iTMSAVEH)) + "H   " + Set_Fill2(Convert.ToString(iTMSAVEM)) + "M";
                    dtRow["TMLOST_TIME"] = Set_Fill2(Convert.ToString(iTMLOSTD)) + "D   " + Set_Fill2(Convert.ToString(iTMLOSTH)) + "H   " + Set_Fill2(Convert.ToString(iTMLOSTM)) + "M";
                    dtRow["TMTOTSAVE"] = dTMTOTSAVE;
                    dtRow["TMTOTLOST"] = dTMTOTLOST;

                    retDt.Rows.Add(dtRow);

                    sTMSOSOK = "";
                    sTMSAVE = "";
                    iTMSAVED = 0;
                    iTMSAVEM = 0;
                    iTMSAVEH = 0;
                    iTMLOSTD = 0;
                    iTMLOSTM = 0;
                    iTMLOSTH = 0;
                    dTMTOTSAVE = 0;
                    dTMTOTLOST = 0;

                }
                //else
                //{
                //조출료 (허용기간 >= 사용기간)
                //체선료 (허용기간 < 사용기간)
                if (Convert.ToInt64(Orgdt.Rows[i]["LTALLOW"].ToString()) >
                    Convert.ToInt64(Orgdt.Rows[i]["LTUSED"].ToString()))
                {
                    sTMSOSOK = Orgdt.Rows[i]["IHSOSOKNM"].ToString();

                    sTMSAVE = Convert.ToString(Orgdt.Rows[i]["LTSAVE"].ToString());

                    if (iTMSAVEM > 59)
                    {
                        iTMSAVEH = iTMSAVEH + 1;
                        iTMSAVEM = iTMSAVEM - 60;
                    }

                    iTMSAVEM = iTMSAVEM + Convert.ToInt16(sTMSAVE.Substring(4, 2));

                    //합계--------------------------------
                    iTMSAVEMTot = iTMSAVEMTot + Convert.ToInt16(sTMSAVE.Substring(4, 2));
                    if (iTMSAVEMTot > 59)
                    {
                        iTMSAVEHTot = iTMSAVEHTot + 1;
                        iTMSAVEMTot = iTMSAVEMTot - 60;
                    }
                    //--------------------------------								


                    iTMSAVEH = iTMSAVEH + Convert.ToInt16(sTMSAVE.Substring(2, 2));
                    if (iTMSAVEH > 23)
                    {
                        iTMSAVED = iTMSAVED + 1;
                        iTMSAVEH = iTMSAVEH - 24;
                    }
                    //합계
                    iTMSAVEHTot = iTMSAVEHTot + Convert.ToInt16(sTMSAVE.Substring(2, 2));
                    if (iTMSAVEHTot > 23)
                    {
                        iTMSAVEDTot = iTMSAVEDTot + 1;
                        iTMSAVEHTot = iTMSAVEHTot - 24;
                    }
                    iTMSAVED = iTMSAVED + Convert.ToInt16(sTMSAVE.Substring(0, 2));
                    iTMSAVEDTot = iTMSAVEDTot + Convert.ToInt16(sTMSAVE.Substring(0, 2));

                    dTMTOTSAVE = dTMTOTSAVE + Convert.ToDouble(Orgdt.Rows[i]["LTTOTSAVE"].ToString());
                    dTMTOTSAVETot = dTMTOTSAVETot + Convert.ToDouble(Orgdt.Rows[i]["LTTOTSAVE"].ToString());

                }
                else
                {
                    sTMSOSOK = Orgdt.Rows[i]["IHSOSOKNM"].ToString();

                    sTMSAVE = Orgdt.Rows[i]["LTSAVE"].ToString();


                    iTMLOSTM = iTMLOSTM + Convert.ToInt16(sTMSAVE.Substring(4, 2));
                    if (iTMLOSTM > 59)
                    {
                        iTMLOSTH = iTMLOSTH + 1;
                        iTMLOSTM = iTMLOSTM - 60;
                    }
                    //합계
                    iTMLOSTMTot = iTMLOSTMTot + Convert.ToInt16(sTMSAVE.Substring(4, 2));
                    if (iTMLOSTMTot > 59)
                    {
                        iTMLOSTHTot = iTMLOSTHTot + 1;
                        iTMLOSTMTot = iTMLOSTMTot - 60;
                    }

                    iTMLOSTH = iTMLOSTH + Convert.ToInt16(sTMSAVE.Substring(2, 2));
                    if (iTMLOSTH > 23)
                    {
                        iTMLOSTD = iTMLOSTD + 1;
                        iTMLOSTH = iTMLOSTH - 24;
                    }

                    //합계
                    iTMLOSTHTot = iTMLOSTHTot + Convert.ToInt16(sTMSAVE.Substring(2, 2));
                    if (iTMLOSTHTot > 23)
                    {
                        iTMLOSTDTot = iTMLOSTDTot + 1;
                        iTMLOSTHTot = iTMLOSTHTot - 24;
                    }

                    iTMLOSTD = iTMLOSTD + Convert.ToInt16(sTMSAVE.Substring(0, 2));
                    iTMLOSTDTot = iTMLOSTDTot + Convert.ToInt16(sTMSAVE.Substring(0, 2));

                    dTMTOTLOST = dTMTOTLOST + Convert.ToDouble(Orgdt.Rows[i]["LTTOTSAVE"].ToString());
                    dTMTOTLOSTTot = dTMTOTLOSTTot + Convert.ToDouble(Orgdt.Rows[i]["LTTOTSAVE"].ToString());

                }
                //}
            }

            if (Orgdt.Rows.Count > 0)
            {
                if (iTMSAVEM > 59)
                {
                    iTMSAVEH = iTMSAVEH + 1;
                    iTMSAVEM = iTMSAVEM - 60;
                }

                if (iTMSAVEH > 23)
                {
                    iTMSAVED = iTMSAVED + 1;
                    iTMSAVEH = iTMSAVEH - 24;
                }



                if (iTMLOSTM > 59)
                {
                    iTMLOSTH = iTMLOSTH + 1;
                    iTMLOSTM = iTMLOSTM - 60;
                }

                if (iTMLOSTH > 23)
                {
                    iTMLOSTD = iTMLOSTD + 1;
                    iTMLOSTH = iTMLOSTH - 24;
                }

                dtRow = retDt.NewRow();

                dtRow["TMHANGCHA"] = sHANGCHA.ToString();
                dtRow["TMSOSOK"] = sTMSOSOK;
                dtRow["TMSAVE_TIME"] = Set_Fill2(Convert.ToString(iTMSAVED)) + "D   " + Set_Fill2(Convert.ToString(iTMSAVEH)) + "H   " + Set_Fill2(Convert.ToString(iTMSAVEM)) + "M";
                dtRow["TMLOST_TIME"] = Set_Fill2(Convert.ToString(iTMLOSTD)) + "D   " + Set_Fill2(Convert.ToString(iTMLOSTH)) + "H   " + Set_Fill2(Convert.ToString(iTMLOSTM)) + "M";
                dtRow["TMTOTSAVE"] = dTMTOTSAVE;
                dtRow["TMTOTLOST"] = dTMTOTLOST;

                retDt.Rows.Add(dtRow);

                sTMSOSOK = "";
                sTMSAVE = "";
                iTMSAVED = 0;
                iTMSAVEM = 0;
                iTMSAVEH = 0;
                iTMLOSTD = 0;
                iTMLOSTM = 0;
                iTMLOSTH = 0;
                dTMTOTSAVE = 0;
                dTMTOTLOST = 0;

                dtRow = retDt.NewRow();

                retDt.Rows.InsertAt(dtRow, Orgdt.Rows.Count);


                if (iTMSAVEMTot > 59)
                {
                    iTMSAVEHTot = iTMSAVEHTot + 1;
                    iTMSAVEMTot = iTMSAVEMTot - 60;
                }

                if (iTMSAVEHTot > 23)
                {
                    iTMSAVEDTot = iTMSAVEDTot + 1;
                    iTMSAVEHTot = iTMSAVEHTot - 24;
                }



                if (iTMLOSTMTot > 59)
                {
                    iTMLOSTHTot = iTMLOSTHTot + 1;
                    iTMLOSTMTot = iTMLOSTMTot - 60;
                }

                if (iTMLOSTHTot > 23)
                {
                    iTMLOSTDTot = iTMLOSTDTot + 1;
                    iTMLOSTHTot = iTMLOSTHTot - 24;
                }

                dtRow["TMHANGCHA"] = sHANGCHA.ToString();
                dtRow["TMSOSOK"] = "합   계";
                dtRow["TMSAVE_TIME"] = Set_Fill2(Convert.ToString(iTMSAVEDTot)) + "D   " + Set_Fill2(Convert.ToString(iTMSAVEHTot)) + "H   " + Set_Fill2(Convert.ToString(iTMSAVEMTot)) + "M";
                dtRow["TMLOST_TIME"] = Set_Fill2(Convert.ToString(iTMLOSTDTot)) + "D   " + Set_Fill2(Convert.ToString(iTMLOSTHTot)) + "H   " + Set_Fill2(Convert.ToString(iTMLOSTMTot)) + "M";
                dtRow["TMTOTSAVE"] = dTMTOTSAVETot;
                dtRow["TMTOTLOST"] = dTMTOTLOSTTot;
            }

            return retDt;
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}