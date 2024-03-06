using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근태파일 Upload 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 09:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPFN497 : 인사 근태파일 등록(임시)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_HR_3564B614 : 인사 근태파일 Upload를 하시겠습니까?
    ///  TY_M_HR_35651618 : 인사 근태파일 Upload를 완료하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    ///  AFFILENAME : 파일명
    /// </summary>
    public partial class TYHRKB006B : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYHRKB006B()
        {
            InitializeComponent();
        }

        private void TYHRKB006B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_SAV.IsAsynchronous = true;
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion      

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //경로선택 체크
            if (TXT01_AFFILENAME.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_HR_4BPFV500");
                e.Successed = false;
                return;
            }
            //실제 파일 존재 체크
            if (!System.IO.File.Exists(TXT01_AFFILENAME.GetValue().ToString()))
            {
                this.ShowMessage("TY_M_HR_4BPFW501");
                e.Successed = false;
                return;                
            }

            if (!this.ShowMessage("TY_M_HR_3564B614"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion        

        #region Description : 파일선택 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            this.TXT01_AFFILENAME.SetValue("");

            openFileDialog.Filter = "mdb(*.mdb)|*.mdb|All Files (*.*)|*.*";
            openFileDialog.FileName = "";
            if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_AFFILENAME.SetValue(this.openFileDialog.FileName);
            }            
        }
        #endregion

        #region Description : BTN61_SAV_InvokerStart 이벤트
        private void BTN61_SAV_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;
            string sKBSABUN = string.Empty;
            string sSql = string.Empty;
            //mdb open
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + TXT01_AFFILENAME.GetValue().ToString();
            OleDbConnection conn = new OleDbConnection(connStr);
            conn.Open();

            sSql = "Select * From INSA ";

            OleDbDataAdapter adpt = new OleDbDataAdapter(sSql, conn);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "INSA");
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][1].ToString().Trim().Length <= 5)
                    {
                        if (ds.Tables[0].Rows[i][1].ToString().Trim().Substring(0, 1) == "C")
                        {
                            sKBSABUN = ds.Tables[0].Rows[i][1].ToString().Trim().Substring(0, 1) + "0" + ds.Tables[0].Rows[i][1].ToString().Trim().Substring(1, 4);
                        }
                        else
                        {
                            sKBSABUN = "0" + ds.Tables[0].Rows[i][1].ToString().Trim();
                        }
                    }
                    else
                    {
                        sKBSABUN = ds.Tables[0].Rows[i][1].ToString().Trim();
                    }

                    datas.Add(new object[] {ds.Tables[0].Rows[i][0].ToString(),   //날짜
                                            sKBSABUN,   //사번
                                            ds.Tables[0].Rows[i][2].ToString(),   //카드번호
                                            ds.Tables[0].Rows[i][3].ToString(),   //구분
                                            ds.Tables[0].Rows[i][4].ToString(),   //시간
                                            ds.Tables[0].Rows[i][5].ToString(),   //출입구분
                                            ds.Tables[0].Rows[i][6].ToString()}); //카드미사용
                }
                if (datas.Count > 0)
                {
                    e.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        e.DbConnector.Attach("TY_P_HR_4BPFN497", data);
                    }
                    e.DbConnector.ExecuteTranQueryList();
                }
                iRowCnt = ds.Tables[0].Rows.Count;
            }
            this.ShowCustomMessage(iRowCnt.ToString() + "건 근태자료가 등록 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        #endregion

        #region Description : BTN61_SAV_InvokerEnd 이벤트
        private void BTN61_SAV_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {

        }
        #endregion

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

        }


    }
}
